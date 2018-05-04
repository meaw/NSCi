using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Json;
using org.gnu.glpk;
using System.IO;

namespace Warts2SQL
{
    class Program
    {


        static void write_lp_solution(glp_prob lp)
        {
            int i;
            int n;
            String name;
            double val;

            name = GLPK.glp_get_obj_name(lp);
            val = GLPK.glp_get_obj_val(lp);
            Console.Write(name);
            Console.Write(" = ");
            Console.WriteLine(val);
            n = GLPK.glp_get_num_cols(lp);
            for (i = 1; i <= n; i++)
            {
                name = GLPK.glp_get_col_name(lp, i);
                val = GLPK.glp_get_col_prim(lp, i);
                Console.Write(name);
                Console.Write(" = ");
                Console.WriteLine(val);
            }
        }


        static double[] array_lp_solution(glp_prob lp)
        {
            int i;
            int n;
            String name;
            double val;


            n = GLPK.glp_get_num_cols(lp);

            double[] results = new double[n + 1];

            name = GLPK.glp_get_obj_name(lp);
            val = GLPK.glp_get_obj_val(lp);
            //Console.Write(name);
            //Console.Write(" = ");
            //Console.WriteLine(val);

            results[0] = val;


            n = GLPK.glp_get_num_cols(lp);
            for (i = 1; i <= n; i++)
            {
                name = GLPK.glp_get_col_name(lp, i);
                val = GLPK.glp_get_col_prim(lp, i);
                //Console.Write(name);
                // Console.Write(" = ");
                //  Console.WriteLine(val);
                results[i] = val;
            }
            return results;
        }

        static double[] Caller(double[] rotts)
        {
            int ret;

            double[] results;

            glp_prob lp;
            glp_smcp parm;

            SWIGTYPE_p_double val;

            SWIGTYPE_p_int ia;
            SWIGTYPE_p_int ja;
            SWIGTYPE_p_double ar;
            try
            {

                lp = GLPK.glp_create_prob();
                Console.WriteLine("Problem created VM");







                //GLPK.glp_cli_set_msg_lvl(GLPK.GLP_CLI_MSG_LVL_ALL);

                // Define columns

                int m = rotts.Length;

                int i, j;

                GLPK.glp_add_cols(lp, m);
                for (i = 1; i <= m; i++)
                {
                    GLPK.glp_set_col_name(lp, i, "x" + i.ToString());
                    GLPK.glp_set_col_kind(lp, i, GLPK.GLP_CV);
                    GLPK.glp_set_col_bnds(lp, i, GLPK.GLP_LO, 0, 9999);
                }

                /* GLPK.glp_add_cols(lp, m);    //-add new columns to problem object
                 GLPK.glp_set_col_name(lp, 1, "x1");
                 GLPK.glp_set_col_kind(lp, 1, GLPK.GLP_CV);
                 GLPK.glp_set_col_bnds(lp, 1, GLPK.GLP_LO, 0, 9999);
                 GLPK.glp_set_col_name(lp, 2, "x2");
                 GLPK.glp_set_col_kind(lp, 2, GLPK.GLP_CV);
                 GLPK.glp_set_col_bnds(lp, 2, GLPK.GLP_LO, 0, 9999);
                 GLPK.glp_set_col_name(lp, 3, "x3");
                 GLPK.glp_set_col_kind(lp, 3, GLPK.GLP_CV); // Continuos unbounded variable
                 GLPK.glp_set_col_bnds(lp, 3, GLPK.GLP_LO, 0, 9999);
                 */

                // Create rows


                /*
                GLPK.glp_add_rows(lp, 3);  //add new rows to problem object

                // Set row details
                GLPK.glp_set_row_name(lp, 1, "c1");
                GLPK.glp_set_row_bnds(lp, 1, GLPK.GLP_UP, 0, .759);
                GLPK.glp_set_row_name(lp, 2, "c2");
                GLPK.glp_set_row_bnds(lp, 2, GLPK.GLP_UP, 0, 0.627);
                GLPK.glp_set_row_name(lp, 3, "c3");
                GLPK.glp_set_row_bnds(lp, 3, GLPK.GLP_UP, 0, 0.762);
                */

                GLPK.glp_add_rows(lp, m);
                for (i = 1; i <= m; i++)
                {
                    GLPK.glp_set_row_name(lp, i, "c" + i.ToString());
                    GLPK.glp_set_row_bnds(lp, i, GLPK.GLP_UP, 0, rotts[i - 1]);
                }



                // Allocate memory
                ia = GLPK.new_intArray(m * m + 1);
                ja = GLPK.new_intArray(m * m + 1);
                ar = GLPK.new_doubleArray(m * m + 1);

                //Row index

                for (i = 1; i <= m; i++)
                {
                    for (j = 1; j <= m; j++)
                    {
                        GLPK.intArray_setitem(ia, (i - 1) * m + j, i);
                    }
                }

                /*
                GLPK.intArray_setitem(ia, 1, 1);
                GLPK.intArray_setitem(ia, 2, 1);
                GLPK.intArray_setitem(ia, 3, 1);
                GLPK.intArray_setitem(ia, 4, 2);
                GLPK.intArray_setitem(ia, 5, 2);
                GLPK.intArray_setitem(ia, 6, 2);
                GLPK.intArray_setitem(ia, 7, 3);
                GLPK.intArray_setitem(ia, 8, 3);
                GLPK.intArray_setitem(ia, 9, 3);
                */

                for (i = 1; i <= m; i++)
                {
                    for (j = 1; j <= m; j++)
                    {
                        GLPK.intArray_setitem(ja, (i - 1) * m + j, j);
                    }
                }

                //Col index
                /* GLPK.intArray_setitem(ja, 1, 1);
                 GLPK.intArray_setitem(ja, 2, 2);
                 GLPK.intArray_setitem(ja, 3, 3);
                 GLPK.intArray_setitem(ja, 4, 1);
                 GLPK.intArray_setitem(ja, 5, 2);
                 GLPK.intArray_setitem(ja, 6, 3);
                 GLPK.intArray_setitem(ja, 7, 1);
                 GLPK.intArray_setitem(ja, 8, 2);
                 GLPK.intArray_setitem(ja, 9, 3);
                 */

                //Values index  ---; ----;---

                for (i = 1; i <= m; i++)
                {
                    for (j = 1; j <= m; j++)
                    {
                        if (j <= i)
                        {
                            GLPK.doubleArray_setitem(ar, (i - 1) * m + j, 1.0);
                        }
                        else
                        {
                            GLPK.doubleArray_setitem(ar, (i - 1) * m + j, 0.0);
                        }
                    }
                }
                /*
                GLPK.doubleArray_setitem(ar, 1, 1.0);
                GLPK.doubleArray_setitem(ar, 2, 0);
                GLPK.doubleArray_setitem(ar, 3, 0);
                GLPK.doubleArray_setitem(ar, 4, 1.0);
                GLPK.doubleArray_setitem(ar, 5, 1.0);
                GLPK.doubleArray_setitem(ar, 6, 0.0);
                GLPK.doubleArray_setitem(ar, 7, 1.0);
                GLPK.doubleArray_setitem(ar, 8, 1.0);
                GLPK.doubleArray_setitem(ar, 9, 1.0);
                */
                Console.WriteLine("Matrix Load solving");



                GLPK.glp_load_matrix(lp, m * m, ia, ja, ar);

                // Free memory
                GLPK.delete_intArray(ia);
                GLPK.delete_intArray(ja);

                GLPK.delete_doubleArray(ar);

                // Define objective
                GLPK.glp_set_obj_name(lp, "z");
                GLPK.glp_set_obj_dir(lp, GLPK.GLP_MAX);
                GLPK.glp_set_obj_coef(lp, 0, 0.0);
                for (i = 1; i <= m; i++)
                {
                    GLPK.glp_set_obj_coef(lp, i, 1.0);
                }



                // Solve model

                Console.WriteLine("Problem solving");
                parm = new glp_smcp();
                GLPK.glp_init_smcp(parm);
                ret = GLPK.glp_simplex(lp, parm);




                // Retrieve solution
                if (ret == 0)
                {
                    results = array_lp_solution(lp);
                    GLPK.glp_delete_prob(lp);
                    return results;

                }
                else
                {
                    Console.WriteLine("The problem could not be solved");
                    GLPK.glp_delete_prob(lp);
                    return (new double[0]);

                }

                // Free memory

            }
            catch (GlpkException)
            {
                Console.WriteLine("Error caught");
                ret = 1;
                return (new double[0]);
            }



        }


        public static string path = "E:\\LinuxHolder\\txp";


        static void Main(string[] args)
        {

            string lineOfText;
            var filestream = new System.IO.FileStream(path + "\\" + "all.dbjson",
                                          System.IO.FileMode.Open,
                                          System.IO.FileAccess.Read,
                                          System.IO.FileShare.ReadWrite);

            long line_counter = 0;



            var file = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);


            long lines_read = 0;
            long total_hops = 0;
            double total_rtt=0;
            using (var fs = new FileStream(path + "\\out\\logs.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                ////////////////
                while ((lineOfText = file.ReadLine()) != null)
                {
                    JsonValue root = JsonValue.Parse(lineOfText);
                    string last_addr = (string)root["src"];
                    //IDictionary<string, JToken> dict = root;
                    if (root.ContainsKey("hops"))
                    {
                        int nhops = root["hops"].Count;
                        JsonValue hop = root["hops"][nhops-1];
                        float new_rtt = (float)hop["rtt"];
                        total_rtt = total_rtt + new_rtt;
                        total_hops = total_hops + nhops;
                        lines_read++;
                    }
    

                }

            }

            Console.WriteLine("Average number of hops: " + (float) total_hops / lines_read);
            Console.WriteLine("Average path time: " + (float)total_rtt / lines_read);
            Console.WriteLine("total_hops: " + total_hops );

            Console.WriteLine("lines_read: " +  lines_read);

           





            try
            {
                using (var fs = new FileStream(path + "\\out\\logs.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    using (var fw = new StreamWriter(fs))
                    {


                        ////////////////

                        while ((lineOfText = file.ReadLine()) != null)
                        {



                            JsonValue root = JsonValue.Parse(lineOfText);
                            string last_addr = (string)root["src"];
                            float last_rtt = 0;
                            int i, j;


                            //IDictionary<string, JToken> dict = root;
                            if (root.ContainsKey("hops"))
                            {

                                //    if (root["hops"] != null)
                                //{
                                int nhops = root["hops"].Count;
                                double[] rtts = new double[nhops];
                                double[] rtts_fitted;
                                for (i = 0; i < nhops; i++)
                                {
                                    JsonValue hop = root["hops"][i];
                                    float new_rtt = (float)hop["rtt"];
                                    rtts[i] = new_rtt;
                                }


                                rtts_fitted = Caller(rtts);

                                if (rtts_fitted.Length > 1)
                                {

                                    double sum = 0;
                                    for (i = 0; i < root["hops"].Count; i++)
                                    {
                                        JsonValue hop = root["hops"][i];
                                        float new_rtt = (float)hop["rtt"];
                                        float new_ttl = (float)hop["reply_ttl"];
                                        string new_addr = (string)hop["addr"];

                                        sum = sum + rtts_fitted[i + 1];

                                        // Console.WriteLine(last_addr + " " + new_addr + " " + rtts_fitted[i + 1] + " " + new_rtt + " " + line_counter);

                                        fw.WriteLine(last_addr + " " + new_addr + " " + rtts_fitted[i + 1] + " " + sum + " " + new_rtt + " " + line_counter);

                                        last_rtt = new_rtt;
                                        last_addr = new_addr;
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Hops is empty");
                            }
                            line_counter++;
                            fw.WriteLine("");
                            fw.Flush(); // Added
                            //Do something with the lineOfText

                        }


                        /////////////



                    }
                }
            }
            catch (IOException)
            {
                Console.WriteLine("error in outpu file");
            }




        }
    }
}

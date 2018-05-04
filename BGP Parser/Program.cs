using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGP_Parser
{

    class ip_map
    {
        public List<string> ASList = new List<string>();
    }
    class ASprops
    {
        public int is_at_end = 0;
    }
    class Program
    {

        static Dictionary<string, string> sample = new Dictionary<string, string>();
        static Dictionary<string, ip_map> IP_AS = new Dictionary<string, ip_map>();
        static Dictionary<string, ip_map> IP_RELS = new Dictionary<string, ip_map>();
        static Dictionary<string, ASprops> AS_CHAR = new Dictionary<string, ASprops>();
        static void adjunct(string a, string b, int internalcall = 0)
        {
            int i;
            ip_map list;
            if (IP_RELS.TryGetValue(a, out list))
            {
                int found = 0;
                for (i = 0; i < list.ASList.Count; i++)
                {
                    if (list.ASList[i] == b)
                    {
                        found = 1;
                    }
                }
                if (found == 0)
                {
                    list.ASList.Add(b);
                }
            }
            else
            {
                list = new ip_map();
                list.ASList.Add(b);
                IP_RELS.Add(a, list);
            }
            if (internalcall == 0)
            {
                adjunct(b, a, 1);
            }
        }
        static void networkize(string[] filtered_tokens)
        {
            int i;
            //each item is related to both parent and child
            //parent search (skip the first)
            for (i = 1; i < filtered_tokens.Length ; i++)
            {
                adjunct(filtered_tokens[i - 1], filtered_tokens[i]);
            }
            //child search (skip the last one)
            for (i = 0; i < filtered_tokens.Length - 1; i++)
            {
                adjunct(filtered_tokens[i], filtered_tokens[i + 1]);
            }


            //all but the last are transit
            for (i = 0; i < filtered_tokens.Length - 1; i++)
            {
                ASprops xtemp;
                if (AS_CHAR.TryGetValue(filtered_tokens[i], out xtemp))
                {
                    xtemp.is_at_end = 0 & xtemp.is_at_end;
                }
                else
                {
                    xtemp = new ASprops();
                    xtemp.is_at_end = 0;
                    AS_CHAR.Add(filtered_tokens[i], xtemp);
                }
            }
            //the last one is a terminal node
            i = filtered_tokens.Length - 1;

            ASprops temp;
            if (AS_CHAR.TryGetValue(filtered_tokens[i], out temp))
            {
                temp.is_at_end = 1 & temp.is_at_end;
            }
            else
            {
                temp = new ASprops();
                temp.is_at_end = 1;
                AS_CHAR.Add(filtered_tokens[i], temp);
            }
        }

        static string[] filter(string[] simple_tokens)
        {
            int i;
            if (simple_tokens.Length<2)
            {
                Console.WriteLine("Fatal");
            }

            string[] newtok = new string[simple_tokens.Length -1];
            for (i = 0; i < simple_tokens.Length - 1; i++)
            {
                newtok[i] = simple_tokens[i];
            }
            return newtok;
        }
            static string[] validate(string[] simple_tokens, string line)
        {
            //the last one shall be i,e
            string result = simple_tokens[simple_tokens.Length - 1];
            int i, j;
            List<string> Filtered_Tokens = new List<string>();
            List<string> Expanded_tokens = new List<string>();
            for (i = 0; i < simple_tokens.Length - 1; i++)
            {
                simple_tokens[i] = simple_tokens[i].Trim();
                simple_tokens[i] = simple_tokens[i].Replace(",", ";");
                if (simple_tokens[i].IndexOf('{') > -1)
                {
                    simple_tokens[i] = simple_tokens[i].Replace("{", "").Replace("}", "");
                    string[] ttok = simple_tokens[i].Split(';');
                    if (ttok.Length > 1)
                    {
                        Console.Write(line + " ");
                        Console.WriteLine(simple_tokens[i]);
                    }
                    /*
                    for (j=0;j<ttok.Length; j++)
                    {
                        xlt.Add(ttok[j]);
                    }*/
                    Expanded_tokens.Add(simple_tokens[i]);
                }
                else
                {
                    Expanded_tokens.Add(simple_tokens[i]);
                }
            }

            for (i = 0; i < Expanded_tokens.Count; i++)
            {
                int repeated = 0;
                for (j = 0; j < Filtered_Tokens.Count; j++)
                {
                    if (Filtered_Tokens[j] == Expanded_tokens[i])
                    {
                        repeated = 1;
                    }
                }
                if (repeated == 0)
                {
                    Filtered_Tokens.Add(Expanded_tokens[i]);
                }
            }

            string[] newtok = new string[Filtered_Tokens.Count + 1];
            for (j = 0; j < Filtered_Tokens.Count; j++)
            {
                newtok[j] = Filtered_Tokens[j].Trim();

            }
            newtok[j] = result;
            return newtok;
        }
        static void Main(string[] args)
        {
            int i;
            System.IO.StreamWriter IP2AS_MAP, AS2AS_MAP;
            string path = @"E:\LinuxHolder\DONOT REMOVE\";
    
            string source = "BGP DUMP oix-full-snapshot-2017-03-28-0800.txt";




            using (StreamReader sr = new StreamReader(path + source))
            {
                string line;
                for (i = 0; i < 5; i++)
                {
                    line = sr.ReadLine();
                    Console.WriteLine(line);
                }
                while (sr.Peek() >= 0)
                {
                    /*
*  69.166.44.0/22     144.228.241.130         84      0      0 1239 6461 46435 11827 11827 11827 11827 11827 11827 i
*  69.166.44.0/22     194.153.0.253         2000      0      0 5413 6461 46435 11827 11827 11827 11827 11827 11827 i     
0  3                  22                41      48     55     62     
                     */
                    line = sr.ReadLine();
                    if (line.Length > 62)
                    {
                        string[] fields = new string[10];
                        fields[0] = line.Substring(0, 3);
                        fields[1] = line.Substring(3, 19);
                        fields[2] = line.Substring(22, 19);
                        fields[3] = line.Substring(41, 7);
                        fields[4] = line.Substring(48, 7);
                        fields[5] = line.Substring(55, 7);
                        fields[6] = line.Substring(62).Trim();

                        string status = fields[0].Trim();
                        string ip_segment = fields[1].Trim();
                        string nextIPhop = fields[2].Trim();

                        if (status == "*")
                        {
                            string[] simple_tokens = fields[6].Split(' ');
                            string[] AESInfo;
                            if ((simple_tokens[simple_tokens.Length - 1] == "i") || (simple_tokens[simple_tokens.Length - 1] == "e"))//|| (tokens[tokens.Length - 1] == "?"))
                            {
                                AESInfo = validate(simple_tokens, line);
                                networkize(filter(AESInfo));
                                string nextASNhop = filter(AESInfo)[0];
                                string str="";
                               if ( sample.TryGetValue(nextIPhop, out str)){
                                    if (str!= nextASNhop)
                                    {


                                        Console.WriteLine("Duplicate \t"+ nextIPhop + "\t" + str + "\t" + nextASNhop);
                                    }
                                }
                                else{
                                    str = nextASNhop;
                                    sample.Add(nextIPhop, str);
                                }

                                string ASNetwork = AESInfo[AESInfo.Length - 2];
                                ip_map vars;
                                if (IP_AS.TryGetValue(ip_segment, out vars))
                                {
                                    int found = 0;
                                    for (i = 0; i < vars.ASList.Count; i++)
                                    {
                                        if ((vars.ASList[i] == ASNetwork))
                                        {
                                            found = 1;
                                        }
                                    }
                                    if (found == 0)
                                    {
                                        vars.ASList.Add(ASNetwork);
                                    }
                                }
                                else
                                {
                                    vars = new ip_map();
                                    vars.ASList.Add(ASNetwork);
                                    IP_AS.Add(ip_segment, vars);
                                }
                            }
                            else if (simple_tokens[simple_tokens.Length - 1] == "?")
                            {

                            }
                            else
                            {
                                Console.WriteLine("Fatal!");

                            }
                        }
                        else
                        {
                            Console.WriteLine("Unhandles!");
                        }
                    }
                }
            }

            IP2AS_MAP = new System.IO.StreamWriter(path + "\\out\\" + "IP_ASN.csv");
            AS2AS_MAP = new System.IO.StreamWriter(path + "\\out\\" + "AS_RELS.csv");
            foreach (KeyValuePair<string, ip_map> entry in IP_AS)
            {
               

                IP2AS_MAP.Write(entry.Key + ",");
                ip_map temp = entry.Value;

                for (i = 0; i < temp.ASList.Count; i++)
                {
                    IP2AS_MAP.Write(temp.ASList[i] + ",");
                }
                IP2AS_MAP.WriteLine();
                // do something with entry.Value or entry.Key
            }


            foreach (KeyValuePair<string, ip_map> entry in IP_RELS)
            {
                int status = -1;
                ASprops xtemp;
                if (AS_CHAR.TryGetValue(entry.Key, out xtemp))
                {
                    status = xtemp.is_at_end;
                }
                else
                {
                    Console.WriteLine("xFatal error");
                }


                AS2AS_MAP.Write(entry.Key + ","+ status.ToString()+",");
                ip_map temp = entry.Value;

                for (i = 0; i < temp.ASList.Count; i++)
                {
                    AS2AS_MAP.Write(temp.ASList[i] + ",");
                }
                AS2AS_MAP.WriteLine();
                // do something with entry.Value or entry.Key
            }
            AS2AS_MAP.Close();
            IP2AS_MAP.Close();
        }

    }
}

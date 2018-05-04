using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphHelpers
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> nodes = new Dictionary<string, int>();
            string path = @"E:\LinuxHolder\AlmostProcessed\";
            int i;
            System.IO.StreamWriter file2;
            file2 = new System.IO.StreamWriter(path + "\\temp\\" + "graph.gml");
            string ASN_RELS = @"AS_RELS.csv";
            int counter = 0;

            file2.WriteLine("Creator \"ME\"");
            file2.WriteLine("graph");
            file2.WriteLine("[");
            file2.WriteLine("   directed 0");

            using (StreamReader sr = new StreamReader(path + ASN_RELS))
            {
                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine();
                    string[] Tokens = line.Split(',');
                    if (Tokens.Length > 2)
                    {

                        string nsource = Tokens[0];
                        nodes.Add(nsource, counter);
                        file2.WriteLine("   node");
                        file2.WriteLine("   [");
                        file2.WriteLine("      id " + counter);
                        file2.WriteLine("      label \"" + nsource + "\"");
                        file2.WriteLine("   ]");
                        counter++;
                        //"startIpNum,endIpNum,locId"

                    }
                    // ASN_DUPS.Add(Tokens[1], Tokens[0]);
                    //196.201.208.0
                }

                sr.BaseStream.Position = 0;
                sr.DiscardBufferedData();

                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine();
                    string[] Tokens = line.Split(',');
                    if (Tokens.Length > 2)
                    {

                        string nsource = Tokens[0];
                        int nsrc = nodes[nsource];
                        string ntype = Tokens[1];
                        for (i = 2; i < Tokens.Length; i++)
                        {
                            if (Tokens[i].Trim() != "")
                            {
                                int ndst = nodes[Tokens[i]];
                                file2.WriteLine("   edge");
                                file2.WriteLine("   [");
                                file2.WriteLine("      source " + nsrc.ToString());
                                file2.WriteLine("      target " + ndst.ToString());
                                file2.WriteLine("   ]");


                               //file2.WriteLine(nsource + "," + Tokens[i]);
                            }
                        }
                        //"startIpNum,endIpNum,locId"

                    }
                    // ASN_DUPS.Add(Tokens[1], Tokens[0]);
                    //196.201.208.0
                }

         
                file2.WriteLine("]");
   

            }

            file2.Close();
        }
    }
}

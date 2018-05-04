using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Links2JSON
{
    class Program
    {
        static void Main(string[] args)
        {
            System.IO.StreamWriter file;
            string path = @"E:\LinuxHolder\AlmostProcessed\";
            file = new System.IO.StreamWriter(path + "\\out\\" + "IP_PATHS.json");
            string source = "IP_PATHS.csv";



            using (StreamReader sr = new StreamReader(path + source))
            {
                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine();
                    string[] tokens = line.Split(',');
                    if (tokens.Length > 5)
                    {
                        if (tokens[13]== "1")
                        {
                            float p = float.Parse(tokens[4]);
                            p = p + .001f;
                            p = 1 / p;
                            file.WriteLine("{\"i\": \"United States\", \"wc\": \"mil\",\"e\": \"Canada\",\"ix\": [ " + tokens[5] + ", " + tokens[6] + " ],\"ex\": [ " + tokens[7] + ", " + tokens[8] + " ], \"v\": " + p.ToString() + "},");
                        }
                    }
                }
            }
            file.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Text3Sql
{
    class Program
    {
        static int tester(IPAddress ip)
        {
            /* IPAddress r10_8_low = IPAddress.Parse("10.0.0.0");
             IPAddress r10_8_high = IPAddress.Parse("10.255.255.255");

             IPAddress r172_12_low = IPAddress.Parse("172.16.0.0");
             IPAddress r172_12_high = IPAddress.Parse("172.31.255.255");

             IPAddress r192_16_low = IPAddress.Parse("192.168.0.0");
             IPAddress r192_16_high = IPAddress.Parse("192.168.255.255");*/
            byte[] k = ip.GetAddressBytes();

            if (k[0] == 10) {
                return 1;
            }

            if (k[0] == 172)
            {
                if (k[1] >= 16 && k[1] < 32)
                {
                    return 1;
                }
            }

            if (k[0] == 192)
            {
                if (k[1] == 168)
                {
                    return 1;
                }
            }
            return 0;

        }
        static uint toInt(IPAddress ip)
        {
            uint t1;
            byte[] k = ip.GetAddressBytes();
            uint kp0 = k[0];
            uint kp1 = k[1];
            uint kp2 = k[2];
            uint kp3 = k[3];
            t1 = kp0 * 256 * 256 * 256 + kp1 * 256 * 256 + kp2 * 256 + kp3;

            return t1;
        }


        public struct pathsTimers
        {
            public uint times;
            public double accum;
        }

        public struct paths
        {
            public uint src;
            public uint dst;
        }

        static void Main(string[] args)
        {
            var dict = new Dictionary<paths, pathsTimers>();

            string path = "E:\\LinuxHolder\\txp";
            const Int32 BufferSize = 128;





            using (var fileStream = File.OpenRead(path + "\\out\\" + "logs.txt"))
            {
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
                {
                    String line;
                    while ((line = streamReader.ReadLine()) != null)
                    {

                        if (line != "")
                        {

                            string[] splits = line.Split(' ');



                            IPAddress src = IPAddress.Parse(splits[0]);
                            IPAddress dst = IPAddress.Parse(splits[1]);

                            int dom_src = tester(src);
                            int dom_dst = tester(src);

                            if (dom_src == 0 && dom_dst == 0)  //this is intenal ip tied to a public IP
                            {
                                uint i_src = toInt(src);
                                uint i_dst = toInt(dst);

                                paths linkA;
                                paths linkB;

                                linkA.src = i_src;
                                linkA.dst = i_dst;
                                linkB.dst = i_src;
                                linkB.src = i_dst;

                                if (dict.ContainsKey(linkA) == true)
                                {
                                    pathsTimers Temp = dict[linkA];
                                    Temp.accum = Temp.accum + double.Parse(splits[2]);
                                    Temp.times++;
                                    dict[linkA] = Temp;
                                }
                                else if (dict.ContainsKey(linkB) == true)
                                {
                                    pathsTimers Temp = dict[linkB];
                                    Temp.accum = Temp.accum + double.Parse(splits[2]);
                                    Temp.times++;
                                    dict[linkB] = Temp;
                                }
                                else
                                {

                                    pathsTimers Temp;
                                    Temp.accum = double.Parse(splits[2]);
                                    Temp.times = 1;
                                    dict.Add(linkA, Temp);
                                }


                            }
                        }
                        else
                        {
                            // fw.Flush(); // Added
                        }
                    }
                    // Process line
                }
                //end line loop
            }



            try
            {
                using (var fs = new FileStream(path + "\\out\\AverageLinkTime.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    using (var fw = new StreamWriter(fs))
                    {

                        foreach (KeyValuePair<paths, pathsTimers> entry in dict)
                        {
                            fw.WriteLine(entry.Key.src.ToString() + "," + entry.Key.dst.ToString() + "," + entry.Value.accum + "," + entry.Value.times );
                            // do something with entry.Value or entry.Key
                        }
                        // 



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

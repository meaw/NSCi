using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AgregateLinkTiminngs
{
    class Program
    {
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

        static int tester(IPAddress ip)
        {
            /* IPAddress r10_8_low = IPAddress.Parse("10.0.0.0");
             IPAddress r10_8_high = IPAddress.Parse("10.255.255.255");

             IPAddress r172_12_low = IPAddress.Parse("172.16.0.0");
             IPAddress r172_12_high = IPAddress.Parse("172.31.255.255");

             IPAddress r192_16_low = IPAddress.Parse("192.168.0.0");
             IPAddress r192_16_high = IPAddress.Parse("192.168.255.255");*/
            byte[] k = ip.GetAddressBytes();

            if (k[0] == 10)
            {
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

        static string beauty(uint ip)
        {
            IPAddress temp = new IPAddress((long)ip);
            byte[] k = temp.GetAddressBytes();
            uint kp0 = k[0];
            uint kp1 = k[1];
            uint kp2 = k[2];
            uint kp3 = k[3];
            return kp3.ToString() + "." + kp2.ToString() + "." + kp1.ToString() + "." + kp0.ToString();
        }

        class ASN_segment { public uint start; public uint end; public List<string> ASN; public string registrar; public string status; }
        class Geo_segment { public uint start; public uint end; public int LocID; public float lat; public float lon; public int linenum; }
        class multientry { public List<string> his = new List<string>(); }

        static uint[] filters;
        static string remap(uint ip, int bits)
        {
            uint mask = filters[bits - 1];
            uint newip = ip & mask;

            string x = beauty(newip) + "/" + bits.ToString();
            return x;
        }


        static float[] getGeo(uint ip)
        {
            string sip = beauty(ip);
            float[] p = new float[3];
            int i;
            for (i = 32; i > 0; i--)
            {
                string key = remap(ip, i);
                Geo_segment TGeo_segment;
                if (FastGEO.TryGetValue(key, out TGeo_segment))
                {
                    p[0] = TGeo_segment.lat;
                    p[1] = TGeo_segment.lon;
                    p[2] = i;
                    return p;
                }


            }


            p[0] = -999;
            p[1] = -999;
            p[2] = -1;
            return p;
        }

        static string[] getASN(uint ip)
        {
            string sip = beauty(ip);
            string[] p = new string[2];
            int i;
            for (i = 32; i > 0; i--)
            {
                string key = remap(ip, i);
                ASN_segment TASN_segment;
                if (FastASN.TryGetValue(key, out TASN_segment))
                {
                    if (TASN_segment.ASN.Count == 1)
                    {
                        p[0] = "{" + TASN_segment.ASN[0] + "}";
                    }
                    else
                    {
                        p[0] = "{" + string.Join(";", TASN_segment.ASN) + "}";
                    }

                    p[1] = i.ToString();
                    return p;
                }


            }

            p[0] = "{}";
            p[1] = "-1";
            return p;
        }
        static Dictionary<string, ASN_segment> FastASN = new Dictionary<string, ASN_segment>();
        static Dictionary<string, Geo_segment> FastGEO = new Dictionary<string, Geo_segment>();
        static Dictionary<int, multientry> GeoLiteBlocks = new Dictionary<int, multientry>();

        static public void populateloc(int sLocID, string index)
        {
            multientry hlist;
            if (GeoLiteBlocks.TryGetValue(sLocID, out hlist))
            {
                hlist.his.Add(index);
            }
            else
            {
                hlist = new multientry();
                hlist.his.Add(index);
                GeoLiteBlocks.Add(sLocID, hlist);
            }
        }

        static void advertise(IPAddress start, IPAddress endip, string location, int linenum)
        {

            uint start_ip = toInt(start);
            uint end_ip = toInt(endip);

            uint dif = end_ip - start_ip + 1;
            int pmask = (int)(Math.Log10(dif) / Math.Log10(2));

            int mask = 24 - pmask;
            Geo_segment s = new Geo_segment();
            s.start = start_ip;
            s.end = end_ip;
            s.linenum = linenum;
            s.LocID = int.Parse(location);

            populateloc(s.LocID, start.ToString() + "/" + mask);
            FastGEO.Add(start.ToString() + "/" + mask, s);

        }

        static int splitter(IPAddress start, IPAddress endip, string location, int linenum)
        {
            uint start_ip = toInt(start);
            uint end_ip = toInt(endip);

            uint dif = end_ip - start_ip + 1;
            int pmask = (int)Math.Ceiling((Math.Log10(dif) / Math.Log10(2)));
            if (Math.Pow(2, pmask) != dif)
            {
                if ((Math.Pow(2, pmask) > dif) && (Math.Pow(2, pmask - 1) < dif))
                {

                    uint n0start = start_ip;
                    uint n0end = start_ip + (uint)Math.Pow(2, pmask - 1) - 1;
                    uint n1start = n0end + 1;
                    IPAddress start0 = IPAddress.Parse(beauty(n0start));
                    IPAddress end0 = IPAddress.Parse(beauty(n0end));
                    IPAddress start1 = IPAddress.Parse(beauty(n1start));


                    splitter(start0, end0, location, linenum);
                    splitter(start1, endip, location, linenum);
                }
                else
                {
                    Console.WriteLine("Mask Error");
                }
            }
            else
            {
                advertise(start, endip, location, linenum);
            }


            return 0;
        }
        static void Main(string[] args)
        {
            System.IO.StreamWriter file, file2;
            string path = @"E:\LinuxHolder\AlmostProcessed\";


            file2 = new System.IO.StreamWriter(path + "\\out\\" + "serverlocs.csv");
            string servers = @"outvuls2.csv";
            string links = @"AverageLinkTime.csv";
            string GeoLoc = @"GeoLiteCity-Location.csv";
            string GeoBlock = @"GeoLiteCity-Blocks.csv";
            string ASN = @"IP_ASN.csv";

            string ip = beauty(767726175);
            filters = new uint[32];
            int i;
            for (i = 0; i < 32; i++)
            {
                filters[31 - i] = 0xFFFFFFFF << i;
            }


            int linenum = 0;
            using (StreamReader sr = new StreamReader(path + GeoBlock))
            {
                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine();
                    string[] Tokens = line.Split(',');
                    if (Tokens.Length == 3)
                    {
                        linenum++;
                        if (Tokens[0] != "startIpNum")
                        {
                            //"startIpNum,endIpNum,locId"

                            Tokens[0] = Tokens[0].Replace("\"", "");
                            Tokens[1] = Tokens[1].Replace("\"", "");
                            Tokens[2] = Tokens[2].Replace("\"", "");
                            IPAddress start = IPAddress.Parse(Tokens[0]);
                            IPAddress endip = IPAddress.Parse(Tokens[1]);
                            splitter(start, endip, Tokens[2], linenum);
                        }
                    }
                    // ASN_DUPS.Add(Tokens[1], Tokens[0]);
                    //196.201.208.0
                }
            }


            using (StreamReader sr = new StreamReader(path + GeoLoc))
            {
                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine();
                    string[] Tokens = line.Split(',');
                    if (Tokens.Length == 9)
                    {
                        if (Tokens[0] != "locId")
                        {

                            //"locId,country,region,city,postalCode,latitude,longitude,metroCode,areaCode"
                            Tokens[0] = Tokens[0].Replace("\"", "");
                            Tokens[5] = Tokens[5].Replace("\"", "");
                            Tokens[6] = Tokens[6].Replace("\"", "");

                            int LocID = int.Parse(Tokens[0]);

                            multientry hlist;
                            if (GeoLiteBlocks.TryGetValue(LocID, out hlist))
                            {
                                for (i = 0; i < hlist.his.Count; i++)
                                {
                                    string jindex = hlist.his[i];
                                    Geo_segment tGeo_segment;
                                    if (FastGEO.TryGetValue(jindex, out tGeo_segment))
                                    {
                                        tGeo_segment.lat = float.Parse(Tokens[5]);
                                        tGeo_segment.lon = float.Parse(Tokens[6]);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Fatal");
                                    }
                                }
                            }

                            // do something with entry.Value or entry.Key

                        }
                    }
                }
            }

            using (StreamReader sr = new StreamReader(path + ASN))
            {
                while (sr.Peek() >= 0)
                {

                    string line = sr.ReadLine();
                    string[] Tokens = line.Split(',');
                    ASN_segment m = new ASN_segment();
                    string[] add = Tokens[0].Split('/');

                    m.start = toInt(IPAddress.Parse(add[0]));
                    int len = 0;
                    len = 32 - int.Parse(add[1]);

                    if (len <= 24)
                    {
                        m.end = m.start + (uint)Math.Pow(2, len);
                        m.ASN = new List<string>();
                        for (i = 1; i < Tokens.Length; i++)
                        {
                            if (Tokens[i].Trim() != "")
                            {
                                m.ASN.Add(Tokens[i]);
                            }
                        }

                        FastASN.Add(Tokens[0], m);

                    }
                    else
                    {
                        Console.WriteLine("ip is too long " + line);
                    }

                }

            }
            /*
               file = new System.IO.StreamWriter(path + "\\out\\" + "IP_PATHS.csv");
                       int count = 0;
                        using (StreamReader sr = new StreamReader(path + links))
                        {
                            while (sr.Peek() >= 0)
                            {
                                string line = sr.ReadLine();


                                string[] Tokens = line.Split(',');
                                if (Tokens.Length > 3)
                                {


                                    uint src = uint.Parse(Tokens[0]);
                                    uint dst = uint.Parse(Tokens[1]);

                                    string ipsrc = beauty(src);
                                    string ipdst = beauty(dst);




                                    double time = double.Parse(Tokens[2]);
                                    int passes = int.Parse(Tokens[3]);

                                    double avg = time / passes;
                                    float[] x;
                                    x = getGeo(src);

                                    double lat0 = x[0];
                                    double lon0 = x[1];
                                    x = getGeo(dst);
                                    double lat1 = x[0];
                                    double lon1 = x[1];



                                    string[] asn_rep;
                                    asn_rep = getASN(src);
                                    string ASN0 = asn_rep[0];
                                    string ASN_STATUS0 = asn_rep[1];

                                    asn_rep = getASN(dst);
                                    string ASN1 = asn_rep[0];
                                    string ASN_STATUS1 = asn_rep[1];


                                    //check for private to public translations
                                    if (tester(IPAddress.Parse(ipsrc)) == 1)
                                    {
                                        lat0 = lat1;
                                        lon0 = lon1;
                                        ASN0 = ASN1;
                                        ASN_STATUS0 = ASN1;
                                    }


                                    if (tester(IPAddress.Parse(ipdst)) == 1)
                                    {
                                        lat1 = lat0;
                                        lon1 = lon0;
                                        ASN1 = ASN0;
                                        ASN_STATUS1 = ASN0;
                                    }


                                    string equal = "0";
                                    if (ASN0 == ASN1)
                                    {
                                        equal = "1";
                                    }
                                    file.WriteLine(src.ToString() + "," + dst.ToString() + "," + ipsrc + "," + ipdst + "," + avg.ToString() + "," + passes.ToString() + "," + lat0.ToString() + "," + lon0.ToString() + "," + lat1.ToString() + "," + lon1.ToString() + "," + ASN0 + "," + ASN_STATUS0 + "," + ASN1 + "," + ASN_STATUS1 + "," + equal);
                                    count++;
                                    if (count % 50000 == 0)
                                    {
                                        Console.WriteLine("links " +(float)(count) / 9076462*100);
                                    }

                                    if (count % 50000 == 0)
                                    {
                                        file.Flush();
                                    }

                                }
                            }
                        }

                        file.Close();
                       */

            int xcount = 0;
            using (StreamReader sr = new StreamReader(path + servers))
            {
                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine();


                    string[] Tokens = line.Split(',');
                    if (Tokens.Length > 3)
                    {


                        uint src = uint.Parse(Tokens[0]);

                        string ipsrc = beauty(src);





                        float[] x;
                        x = getGeo(src);

                        double lat0 = x[0];
                        double lon0 = x[1];



                        string[] asn_rep;
                        asn_rep = getASN(src);
                        string ASN0 = asn_rep[0];
                        string ASN_STATUS0 = asn_rep[1];




                        string equal = "0";

                        file2.WriteLine(src.ToString() + "," + ipsrc + "," + lat0.ToString() + "," + lon0.ToString() + "," + ASN0 + "," + Tokens[1] + "," + Tokens[2] + "," + Tokens[3] + "," + Tokens[4]);
                        xcount++;
                        if (xcount % 100000 == 0)
                        {
                            Console.WriteLine((float)(xcount) / 45000000);
                        }

                        if (xcount % 10000 == 0)
                        {
                            file2.Flush();
                        }

                    }
                }
            }
            file2.Close();




        }
    }
}

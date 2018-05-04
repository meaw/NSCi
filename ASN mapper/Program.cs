using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;




namespace ASN_mapper
{
    class tag { public string AS; public int flag = 0; }
    public class Whois
    {
        private const int Whois_Server_Default_PortNumber = 43;
        private const string Domain_Record_Type = "domain";
        private const string DotCom_Whois_Server = "whois.radb.net";

        /// <summary>
        /// Retrieves whois information
        /// </summary>
        /// <param name="domainName">The registrar or domain or name server whose whois information to be retrieved</param>
        /// <param name="recordType">The type of record i.e a domain, nameserver or a registrar</param>
        /// <returns></returns>
        public static string Lookup(string domainName)
        {
            using (TcpClient whoisClient = new TcpClient())
            {
                whoisClient.Connect(DotCom_Whois_Server, Whois_Server_Default_PortNumber);

                string domainQuery = domainName + "\r\n";
                byte[] domainQueryBytes = Encoding.ASCII.GetBytes(domainQuery.ToCharArray());

                Stream whoisStream = whoisClient.GetStream();
                whoisStream.Write(domainQueryBytes, 0, domainQueryBytes.Length);

                StreamReader whoisStreamReader = new StreamReader(whoisClient.GetStream(), Encoding.ASCII);

                string streamOutputContent = "";
                List<string> whoisData = new List<string>();
                while (null != (streamOutputContent = whoisStreamReader.ReadLine()))
                {
                    whoisData.Add(streamOutputContent);
                }

                whoisClient.Close();

                return String.Join(Environment.NewLine, whoisData);
            }
        }
    }

    class rg
    {
        public static uint toInt(IPAddress ip)
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

        public static string IPtoString(uint n_ip)
        {
            IPAddress temp = new IPAddress(n_ip);
            string stip = temp.ToString();
            string[] toks = stip.Split('.');
            return toks[3] + "." + toks[2] + "." + toks[1] + "." + toks[0];
        }
        public string s_start;
        public string s_seg;
        public string s_xend;
        public uint start;
        public uint end;
        public rg(string parser)
        {
            string[] toks = parser.Split('/');
            IPAddress dst = IPAddress.Parse(toks[0]);

            this.s_start = toks[0];
            this.s_seg = toks[1];
            this.start = toInt(dst);
            uint psize = 32 - (uint)int.Parse(toks[1]);

            if (psize > 24)
            {
                Console.WriteLine(psize);
                this.end = this.start;
                this.s_xend = this.s_start;

            }
            else
            {
                uint size = (uint)Math.Pow(2, psize);
                this.end = this.start + (uint)size - 1;
                this.s_xend = IPtoString(this.end);
            }
        }
        public List<string> ASN_List;
    }
    class Program
    {
        public static string queryIP(string ipstr)
        {
            int i;
            Whois temp = new Whois();
            string p = Whois.Lookup(ipstr);
            string[] results = p.Split('\r');
            string AStest = "";
            int flag = 1;
            for (i = 0; i < results.Length; i++)
            {
                if (results[i].IndexOf("origin:") > 0)
                {
                    AStest = results[i];
                    AStest = AStest.Replace("origin:", "");
                    AStest = AStest.Replace("AS", "");
                    AStest = AStest.Replace("as", "");
                    AStest = AStest.Replace("As", "");
                    AStest = AStest.Trim();
                    flag = 0;
                    break;
                }
            }
            if (flag == 1 && !(results[0].IndexOf("No entries found for the") > 0))
            {

                AStest = "";
            }
            return AStest;
        }


        public static void Main()
        {

            //this method has a preference for using the BGP
            List<rg> IP_ASN_Map = new List<rg>();


            Dictionary<string, List<string>> RASN_DUPS = new Dictionary<string, List<string>>();
            Dictionary<string, string> ASN_DUPS = new Dictionary<string, string>();
            Dictionary<string, string> ASN_CORRECTED = new Dictionary<string, string>();
            System.IO.StreamWriter OutIPASN, file2;
            string path = @"E:\LinuxHolder\DONOT REMOVE\ARIN Regs\out\";
            string UnassingedAS_File = @"xdelegated-all-extended-20170328.txt_dec.txt";
            string ConflictASN_File = @"xdelegated-all-extended-20170328.txt_asn.txt";
            //this file is from BGP output
            string IP_ASN_FILE = @"IP_ASN.csv";


            using (StreamReader sr = new StreamReader(path + ConflictASN_File))
            {
                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine();
                    string[] Tokens = line.Split(' ');
                    if (Tokens.Length > 1)
                    {
                        ASN_DUPS.Add(Tokens[1], Tokens[0]);

                        List<string> plist;
                        if (RASN_DUPS.TryGetValue(Tokens[0], out plist))
                        {
                            int repeated = 0;
                            int i;
                            for (i = 0; i < plist.Count; i++)
                            {
                                if (plist[i] == Tokens[1]) { repeated = 1; }
                            }
                            if (repeated == 0)
                            {
                                plist.Add(Tokens[1]);
                            }


                        }
                        else
                        {
                            plist = new List<string>();
                            plist.Add(Tokens[1]);
                            RASN_DUPS.Add(Tokens[0], plist);

                        }
                    }

                    //196.201.208.0
                }
            }


            using (StreamReader sr2 = new StreamReader(path + IP_ASN_FILE))
            {
                while (sr2.Peek() >= 0)
                {
                    int i;
                    string line = sr2.ReadLine();
                    string[] Tokens = line.Split(',');

                    List<string> ghp = new List<string>();
                    for (i = 1; i < Tokens.Length - 1; i++)
                    {
                        ghp.Add(Tokens[i]);
                    }

                    rg myrange = new rg(Tokens[0]);
                    myrange.ASN_List = ghp;

                    IP_ASN_Map.Add(myrange);
                    //196.201.208.0
                }
            }


            string status_code = "";
            string ASTest = "";
            OutIPASN = new System.IO.StreamWriter(path + "\\out\\" + "All_IP_ASN_BGP.csv");
            int counter = 0;
            using (StreamReader sr = new StreamReader(path + UnassingedAS_File))
            {

                while (sr.Peek() >= 0)
                {//196.201.208.0
                    counter++;
                    string line = sr.ReadLine();
                    int i = 0;
                    string[] data = line.Split(',');
                    //0 a single ASN owns the ip
                    //1 THe ASN has multiple ASN choices (it is expected that only 1 is valid)
                    //2 The IP is allocated but no ASN broadcast it
                    if (data[3] == "1" || data[3] == "2" || data[3] == "0")
                    {

                        IPAddress test_ip = IPAddress.Parse(data[1]);


                        uint testip = rg.toInt(test_ip);
                        List<string> BGP_AnnounceList = new List<string>();
                        int bgp_anounced = 0;
                        for (i = 0; i < IP_ASN_Map.Count; i++)
                        {
                            if ((testip >= IP_ASN_Map[i].start) && (testip <= IP_ASN_Map[i].end))  //end is -1 already
                            {
                                BGP_AnnounceList = IP_ASN_Map[i].ASN_List;
                                bgp_anounced = 1;
                                break;
                            }
                        }
                        if (data[1] == "154.65.88.0")
                        {
                            data[1] = data[1];
                        }
                        if (bgp_anounced == 1)
                        {

                            if (BGP_AnnounceList.Count == 1)
                            {
                                ASTest = BGP_AnnounceList[0];
                                //use the default 
                                status_code =( -10-int.Parse(data[3])).ToString();
                            }
                            else
                            {  //prefer the one that is registered
                                List<string> tlist;
                                List<string> xlist;
                                if (RASN_DUPS.TryGetValue(data[4], out xlist))
                                {
                                    tlist = xlist;
                                }
                                else
                                {
                                    tlist = new List<string>();
                                }
                                int alreadyinlist = 0;
                                for (i = 0; i < tlist.Count; i++)
                                {
                                    if (tlist[i] == data[5])
                                    {
                                        alreadyinlist = 1;
                                    }
                                }
                                if (alreadyinlist == 0)
                                {
                                    if (data[3] == "0")
                                    {
                                        tlist.Add(data[5]);
                                    }
                                }


                                if (tlist.Count == 1)
                                {
                                    int BGP_is_Matched = 0;
                                    for (i = 0; i < BGP_AnnounceList.Count; i++)
                                    {
                                        if (BGP_AnnounceList[i] == tlist[0])
                                        {
                                            ASTest = BGP_AnnounceList[i];
                                            status_code = "-2";  //matched based on ARIN preference
                                            BGP_is_Matched = 1;
                                        }
                                    }
                                    if (BGP_is_Matched != 1)
                                    {
                                        ASTest = "{" + string.Join(";", BGP_AnnounceList) + "}";
                                        status_code = "-3";  //Unable to match, so we list all
                                    }

                                }
                                else
                                {

                                    //check all BGPs that are on  the ARIN allowed list
                                    int m1, m2;
                                    List<string> tempASN = new List<string>();
                                    for (m1 = 0; m1 < tlist.Count; m1++)
                                    {
                                        for (m2 = 0; m2 < BGP_AnnounceList.Count; m2++)
                                        {
                                            if (tlist[m1] == BGP_AnnounceList[m2])
                                            {
                                                tempASN.Add(BGP_AnnounceList[m2]);
                                            }
                                        }
                                    }
                                    if (tempASN.Count > 0)
                                    {
                                        ASTest = "{" + string.Join(";", tempASN) + "}";
                                        status_code = "-4";  //List all allowed BGP announcements
                                    }
                                    else
                                    {
                                        if (tlist.Count != 0)
                                        {
                                            ASTest = "{" + string.Join(";", tlist) + "}";
                                            status_code = "-5";  //List all allowed ARIN-registerede announcements
                                        }
                                        else
                                        {
                                            //this is a forbidden address
                                            Console.WriteLine("Address is announced but not in registered to any ASN " + data[1]);
                                            ASTest = "";
                                        }
                                    }

                                }




                            }

                        } //BGP has not been anounced
                        else
                        {
                            if (data[3] == "2")
                            {

                                //ok
                            }

                            else if (data[3] == "0")
                            {
                                ASTest = data[5];
                                status_code = "-0";
                                //ok
                            }
                            else
                            {
                                List<string> xlist;
                                if (RASN_DUPS.TryGetValue(data[4], out xlist))
                                {
                                    xlist = xlist;
                                    ASTest = "{" + string.Join(";", xlist) + "}";
                                    status_code = "-6";  //Not anounced but ARIN blocks
                                }
                                else
                                {

                                    ASTest = "";
                                }
                            }
                        }

                        //string AStest = data[5];  //this line shall be commented
                        string AS = "";
                        if (ASTest != "")
                        {
                            AS = ASTest;
                        }


                        if (AS == "")
                        {
                            string linear = "{" + string.Join(";", BGP_AnnounceList) + "}";

                            uint addr = rg.toInt(IPAddress.Parse(data[1]));
                            string temp = addr.ToString() + "," + data[0] + "," + data[1] + "," + data[2] + "," + "-99" + "," + data[4] + "," + linear;
                            //  ASN_CORRECTED.Add(data[1], temp);
                            OutIPASN.WriteLine(temp);
                        }
                        if (AS != "")
                        {
                            uint addr = rg.toInt(IPAddress.Parse(data[1]));
                            string temp = addr.ToString() + "," + data[0] + "," + data[1] + "," + data[2] + "," + status_code.ToString() + "," + data[4] + "," + AS;
                            //ASN_CORRECTED.Add(data[1], temp);
                            OutIPASN.WriteLine(temp);
                        }

                        if (counter % 2000 == 0)
                        {
                            Console.WriteLine("progress " + counter.ToString());
                        }


                    }//end read only 1s
                    else
                    {
                        uint addr = rg.toInt(IPAddress.Parse(data[1]));
                        string temp = addr.ToString() + "," + data[0] + "," + data[1] + "," + data[2] + "," + data[3] + "," + data[4] + "," + data[5];
                        OutIPASN.WriteLine(temp);
                    }

                }


                OutIPASN.Close();






            }
        }

        public static void MainARIN()
        {

            //this method has a preference for using the ARIN reg DB
            List<rg> IP_ASN_Map = new List<rg>();

            Dictionary<string, List<string>> RASN_DUPS = new Dictionary<string, List<string>>();
            Dictionary<string, string> ASN_DUPS = new Dictionary<string, string>();
            Dictionary<string, string> ASN_CORRECTED = new Dictionary<string, string>();
            System.IO.StreamWriter OutIPASN, file2;
            string path = @"E:\LinuxHolder\DONOT REMOVE\ARIN Regs\out\";
            string UnassingedAS_File = @"xdelegated-all-extended-20170328.txt_dec.txt";
            string ConflictASN_File = @"xdelegated-all-extended-20170328.txt_asn.txt";
            //this file is from BGP output
            string IP_ASN_FILE = @"IP_ASN.csv";


            using (StreamReader sr = new StreamReader(path + ConflictASN_File))
            {
                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine();
                    string[] Tokens = line.Split(' ');
                    if (Tokens.Length > 1)
                    {
                        ASN_DUPS.Add(Tokens[1], Tokens[0]);

                        List<string> plist;
                        if (RASN_DUPS.TryGetValue(Tokens[0], out plist))
                        {
                            int repeated = 0;
                            int i;
                            for (i = 0; i < plist.Count; i++)
                            {
                                if (plist[i] == Tokens[1]) { repeated = 1; }
                            }
                            if (repeated == 0)
                            {
                                plist.Add(Tokens[1]);
                            }


                        }
                        else
                        {
                            plist = new List<string>();
                            plist.Add(Tokens[1]);
                            RASN_DUPS.Add(Tokens[0], plist);

                        }
                    }

                    //196.201.208.0
                }
            }


            using (StreamReader sr2 = new StreamReader(path + IP_ASN_FILE))
            {
                while (sr2.Peek() >= 0)
                {
                    int i;
                    string line = sr2.ReadLine();
                    string[] Tokens = line.Split(',');

                    List<string> ghp = new List<string>();
                    for (i = 1; i < Tokens.Length - 1; i++)
                    {
                        ghp.Add(Tokens[i]);
                    }

                    rg myrange = new rg(Tokens[0]);
                    myrange.ASN_List = ghp;

                    IP_ASN_Map.Add(myrange);
                    //196.201.208.0
                }
            }



            OutIPASN = new System.IO.StreamWriter(path + "\\out\\" + "All_IP_ASN_reg.csv");
            int counter = 0;
            using (StreamReader sr = new StreamReader(path + UnassingedAS_File))
            {

                while (sr.Peek() >= 0)
                {//196.201.208.0
                    counter++;
                    string line = sr.ReadLine();
                    int i = 0;
                    string[] data = line.Split(',');
                    if (data[3] == "1")
                    {
                        string AStest = "";
                        IPAddress test_ip = IPAddress.Parse(data[1]);


                        uint testip = rg.toInt(test_ip);
                        List<string> ASN_Announced = new List<string>();
                        int bgp_anounced = 0;
                        for (i = 0; i < IP_ASN_Map.Count; i++)
                        {
                            if ((testip >= IP_ASN_Map[i].start) && (testip <= IP_ASN_Map[i].end))  //end is -1 already
                            {
                                ASN_Announced = IP_ASN_Map[i].ASN_List;
                                bgp_anounced = 1;
                                break;
                            }
                        }
                        if (data[1] == "41.72.192.0")
                        {
                            data[1] = data[1];
                        }
                        if (bgp_anounced == 1)
                        {
                            if (ASN_Announced.Count == 1)
                            {
                                AStest = ASN_Announced[0];
                            }
                            else if (ASN_Announced.Count == 0)
                            {   //it was n't announced by anyone, just try a recent anouncement
                                AStest = queryIP(data[1]);
                            }
                            else
                            {
                                List<string> ASN_Announced_temp = new List<string>();
                                //Filtered by the supposely announced AS.
                                for (i = 0; i < ASN_Announced.Count; i++)
                                {
                                    string temp;
                                    if (ASN_DUPS.TryGetValue(ASN_Announced[i], out temp))
                                    {
                                        if (temp == data[4])
                                        {
                                            ASN_Announced_temp.Add(ASN_Announced[i]);
                                        }
                                    }
                                }
                                ASN_Announced = ASN_Announced_temp;
                                //Check if a single instance has been found.
                                if (ASN_Announced.Count == 1)
                                {
                                    AStest = ASN_Announced[0];
                                }
                                else
                                {
                                    int found = 0;
                                    //check who is announcing now!
                                    AStest = queryIP(data[1]);
                                    for (i = 0; i < ASN_Announced.Count; i++)
                                    {
                                        if (AStest == ASN_Announced[i])
                                        {
                                            found++;
                                        }
                                    }
                                    if (found != 1)
                                    {
                                        AStest = "";
                                        string track = "";
                                        for (i = 0; i < ASN_Announced.Count; i++)
                                        {
                                            track += ASN_Announced[i] + " [";
                                            string temp;
                                            if (ASN_DUPS.TryGetValue(ASN_Announced[i], out temp))
                                            {
                                                track += temp;
                                            }
                                            track += "]\t";
                                        }
                                        Console.WriteLine(data[1] + "\t unable to match ip \t" + track);
                                    }

                                }
                            }
                        }
                        else
                        {
                            AStest = queryIP(data[1]);
                        }
                        /*
                       
                        if (counter % 200 == 0) {
                            Console.WriteLine("progress " + counter.ToString());
                        }
                        */
                        //string AStest = data[5];  //this line shall be commented
                        string AS = "";
                        int status_code = -99;
                        if (AStest != "")
                        {
                            string temp;
                            if (ASN_DUPS.TryGetValue(AStest, out temp))
                            {
                                if (temp == data[4])
                                {
                                    AS = AStest;
                                    status_code = -1;

                                }
                                else
                                {
                                    string TAStest = queryIP(data[1]);
                                    if (TAStest == AStest)
                                    {
                                        AS = AStest;
                                        status_code = -2;
                                    }
                                    else
                                    {
                                        AS = "";
                                    }

                                }
                            }

                        }
                        if (AS == "")
                        {
                            string linear = "{" + string.Join(";", RASN_DUPS[data[4]]) + "}";
                            string temp = data[0] + "," + data[1] + "," + data[2] + "," + data[3] + "," + data[4] + "," + linear;
                            //  ASN_CORRECTED.Add(data[1], temp);
                            OutIPASN.WriteLine(temp);
                        }
                        if (AS != "")
                        {
                            string temp = data[0] + "," + data[1] + "," + data[2] + "," + status_code.ToString() + "," + data[4] + "," + AS;
                            //ASN_CORRECTED.Add(data[1], temp);
                            OutIPASN.WriteLine(temp);
                        }


                        if (counter % 200 == 0)
                        {
                            Console.WriteLine("progress " + counter.ToString());
                        }

                    }//end read only 1s
                    else
                    {
                        string temp = data[0] + "," + data[1] + "," + data[2] + "," + data[3] + "," + data[4] + "," + data[5];
                        OutIPASN.WriteLine(temp);
                    }

                }
            }

            OutIPASN.Close();

            file2 = new System.IO.StreamWriter(path + "\\out\\" + "postprocessedASN_ALL.csv");
            using (StreamReader sr = new StreamReader(path + "ALL.csv"))
            {
                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine();
                    string[] Tokens = line.Split(',');
                    string temp;
                    if (ASN_CORRECTED.TryGetValue(Tokens[1], out temp))
                    {
                        file2.WriteLine(temp);
                    }
                    else
                    {
                        file2.WriteLine(line);
                    }
                    //196.201.208.0
                }
            }
            file2.Close();




        }

        public static void Main5()
        {
            /*This joins all AS to IPV4 pairs*/
            Dictionary<string, tag> ASN_MAP = new Dictionary<string, tag>();
            System.IO.StreamWriter IP2ASN_FILE, Dupl_ASN_File;


            string area = "ripencc";
            string path = @"E:\LinuxHolder\DONOT REMOVE\ARIN Regs\";
            string name = @"delegated-" + area + "-extended-20170328.txt";


            IP2ASN_FILE = new System.IO.StreamWriter(path + "\\out\\" + name + "_dec.txt");

            Dupl_ASN_File = new System.IO.StreamWriter(path + "\\out\\" + name + "_asn.txt");


            //Based on ASN info find those with duplicate owner.

            using (StreamReader sr = new StreamReader(path + name))
            {

                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine();
                    if (line.Length > 0)
                    {
                        if (line.Substring(0, 1) != "#")
                        {
                            string[] codes = line.Split('|');
                            if (codes[2].ToUpper() == "ASN" && codes.Length > 6)
                            {
                                if (!((codes[6].ToUpper() == "RESERVED") || (codes[6].ToUpper() == "AVAILABLE")))
                                {
                                    if (codes[7].Length < 2)
                                    {
                                        codes[7] = codes[7];

                                    }
                                    if (!ASN_MAP.ContainsKey(codes[7]))
                                    {
                                        tag Create_newTag = new tag();
                                        Create_newTag.AS = codes[3];
                                        ASN_MAP.Add(codes[7], Create_newTag);
                                    }
                                    else
                                    {
                                        tag Temp_Tag = new tag();

                                        if (ASN_MAP.TryGetValue(codes[7], out Temp_Tag))
                                        {
                                            if (Temp_Tag.flag == 0)
                                            {

                                                Console.WriteLine("ASN DUP FOUND " + codes[7]);
                                                Dupl_ASN_File.WriteLine(codes[7] + " " + Temp_Tag.AS);
                                                Temp_Tag.flag = 1;
                                            }
                                            Dupl_ASN_File.WriteLine(codes[7] + " " + codes[3]);
                                        }
                                    }
                                }
                            }

                        }
                    }
                }

                sr.DiscardBufferedData();
                sr.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine();
                    if (line.Length > 0)
                    {
                        if (line.Substring(0, 1) != "#")
                        {
                            string[] codes = line.Split('|');

                            if (codes[2].ToUpper() == "ASN")
                            {
                            }
                            else if (codes[2].ToUpper() == "IPV4" && codes.Length > 6)
                            {
                                tag val = new tag();
                                if ((codes[6].ToUpper() == "ALLOCATED") || (codes[6].ToUpper() == "ASSIGNED"))
                                {

                                    if (ASN_MAP.TryGetValue(codes[7], out val))
                                    {
                                        if (val.flag == 0)
                                        {   //the range has been mapped to a single AS
                                            IP2ASN_FILE.WriteLine(area + "," + codes[3] + "," + codes[4] + ",0," + codes[7] + "," + val.AS);
                                        }
                                        else
                                        {//the range has been mapped to a mutiples AS
                                            IP2ASN_FILE.WriteLine(area + "," + codes[3] + "," + codes[4] + ",1," + codes[7] + "," + codes[7]);
                                        }
                                    }
                                    else
                                    {
                                        //This has been allocated, but no AS appears to anounce it
                                        IP2ASN_FILE.WriteLine(area + "," + codes[3] + "," + codes[4] + ",2," + codes[7] + "," + codes[7]);
                                    }
                                }
                                else if ((codes[6].ToUpper() == "RESERVED"))
                                {
                                    if (codes.Length > 7)
                                    {
                                        IP2ASN_FILE.WriteLine(area + "," + codes[3] + "," + codes[4] + ",3," + codes[7] + "," + "RESERVED");

                                    }
                                    else
                                    {
                                        IP2ASN_FILE.WriteLine(area + "," + codes[3] + "," + codes[4] + ",3," + "" + "," + "RESERVED");
                                    }
                                }
                                else if ((codes[6].ToUpper() == "AVAILABLE"))
                                {
                                    if (codes.Length > 7)
                                    {
                                        IP2ASN_FILE.WriteLine(area + "," + codes[3] + "," + codes[4] + ",3," + codes[7] + "," + "AVAILABLE");
                                    }
                                    else
                                    {
                                        IP2ASN_FILE.WriteLine(area + "," + codes[3] + "," + codes[4] + ",3," + "" + "," + "AVAILABLE");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine(line);
                                }
                            }
                            else if (codes[2].ToUpper() == "IPV6" || (codes[2].ToUpper() == "IPV4" && codes[3].ToUpper() == "*"))
                            {

                            }
                            else
                            {
                                Console.WriteLine(line);
                            }
                        }
                    }



                }
            }

            IP2ASN_FILE.Close();
            Dupl_ASN_File.Close();

        }
    }
}

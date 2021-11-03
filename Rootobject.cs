using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Configuration;
namespace ServicePosilka
{
    public partial class Checkpoint
    {
        public long id { get; set; }
        public string time { get; set; }
        public Courier1 courier { get; set; }
        public string status_code { get; set; }
        public string status_name { get; set; }
        public string status_raw { get; set; }
        public string location_translated { get; set; }
        public string location_raw { get; set; }
        public object location_color_light { get; set; }
        public object location_color_dark { get; set; }
        public object location_zip_code { get; set; }

        //-----------------
        public string track { get; set; }
        public string date { get; set; }
        //public string time_delivered { get; set; }

        // public string time_accepted { get; set; }
        public string act { get; set; }
        public string marking { get; set; }
        public string status { get; set; }
        public string location { get; set; }
        public string estimation_date { get; set; }

        public partial class Courier1
        {
            public string slug { get; set; }
            public string name { get; set; }
            public string name_alt { get; set; }
            public string country_code { get; set; }
            public int review_count { get; set; }
            public string review_score { get; set; }
        }
    }

    public partial class Rootobject<T> //where T : string, Checkpoint
    {
        public T result { get; set; }
        public Data data { get; set; }
        public string[] messages { get; set; }

        public bool status { get; set; }

        public int error { get; set; }

        public object message { get; set; }

        //public Checkpoint result { get; set; }

        //----------------------------

        private string BaseUrl = "";

        List<Checkpoint> checkpoints = new List<Checkpoint>();
        List<Checkpoint> checkpoints2 = new List<Checkpoint>();
        List<Checkpoint> checkpoints3 = new List<Checkpoint>();



        public partial class Data
        {
            public int id { get; set; }
            public string tracking_number { get; set; }
            public object tracking_number_secondary { get; set; }
            public object tracking_number_current { get; set; }
            public Courier courier { get; set; }
            public bool is_active { get; set; }
            public bool is_delivered { get; set; }
            public string last_check { get; set; }
            public Checkpoint[] checkpoints { get; set; }
            public Extra[] extra { get; set; }
        }

        public partial class Courier
        {
            public string slug { get; set; }
            public string name { get; set; }
            public string name_alt { get; set; }
            public string country_code { get; set; }
            public int review_count { get; set; }
            public string review_score { get; set; }
        }

       

        public partial class Courier1
        {
            public string slug { get; set; }
            public string name { get; set; }
            public string name_alt { get; set; }
            public string country_code { get; set; }
            public int review_count { get; set; }
            public string review_score { get; set; }
        }

        public partial class Extra
        {
            public string courier_slug { get; set; }
            public Data1 data { get; set; }
        }

        public partial class Data1
        {
            public string weightvolume { get; set; }
            public int weightactual { get; set; }
            public object weightdimensions { get; set; }
            public string weightseats { get; set; }
            public object recipienttitle { get; set; }
            public string recipientlocationtitle { get; set; }
            public object recipientlocationzip_code { get; set; }
            public string recipientlocationphone { get; set; }
            public object sendertitle { get; set; }
            public string senderlocationtitle { get; set; }
            public object senderlocationzip_code { get; set; }
            public object servicename { get; set; }
            public object shipmentorder_number { get; set; }
            public object shipmentdelivery_date { get; set; }
            public string shipmentpickup_ready_date { get; set; }
            public object shipmentpickup_storage_date { get; set; }
            public object shipmentvalue { get; set; }
            public object shipmentcash_on_delivery { get; set; }
            public object deliveryleft_at { get; set; }
            public object deliverysigned_by { get; set; }
            public object alternativetracking_number { get; set; }
        }
        public List<Checkpoint> GetResponceGdePosilka(string slugName, string trackNumber, Client client)
        {
            BaseUrl = $"{client.BaseAddress}{slugName}/{trackNumber}";

            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, BaseUrl))
            {
                var response = client.SendAsync(request).Result;

                var content = response.Content.ReadAsStringAsync().Result;

                Rootobject<string> rootobject = JsonConvert.DeserializeObject<Rootobject<string>>(content);

                foreach (var item in rootobject.data.checkpoints)
                {
                    checkpoints.Add(item);
                }
                return checkpoints;
            }
        }


        public List<Checkpoint> GetResponceMagictarnceAsync(string trackNumber, Client client)
        {
            BaseUrl = $"{client.BaseAddress}track={trackNumber}";
            List<string> neoCheck = new List<string>();
            string timeStatusAccepted = "";
            string timeStatusDelivered = "";
            string statuscheck = "";
            string statusCode = "";
            string statusPattern = "Груз выдан";

            var pathFileLog2 = @"C:\GdePosilka\ServicePosilka\logTrackerPost.txt";
            //var pathFileLog2 = @"\\IP-address\reports_recieve\logTrackerPost3.xml";

            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "response";
            var rootElem = new XElement("response");
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Checkpoint), xRoot);
            string newTimeAcc = "";
            string newTimeDel = "";
            string SubnewTimeAcc = "";
            string SubnewTimeDel = "";
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, BaseUrl))
            {
                var response = client.SendAsync(request).Result;

                var content = response.Content.ReadAsStringAsync().Result;

                Rootobject<Checkpoint> rootobject = JsonConvert.DeserializeObject<Rootobject<Checkpoint>>(content);
                for (int i = 0; i < 1; i++)
                {
                    checkpoints2.Add(rootobject.result);
                    statusCode = checkpoints2.Select(ss => ss.status).FirstOrDefault();

                    string GetTime(string str)
                    {
                        string res = "";
                        char[] ch = str.ToCharArray();
                        for (int j = 0; j < str.Length; j++)
                        {
                            if (Char.IsDigit(ch[j]))
                            {
                                res = str.Substring(j, str.Length - j);
                                break;
                            }
                        }
                        return res;
                    }
                    if (statusCode.Substring(0, 10) == statusPattern)
                    {
                        checkpoints2.Add(rootobject.result);
                        timeStatusDelivered = GetTime(statusCode);
                        DateTime dateTimeDelivered = Convert.ToDateTime(timeStatusDelivered);
                       // 
                        newTimeDel = dateTimeDelivered.ToString("u");
                        SubnewTimeDel = newTimeDel.Substring(0, newTimeDel.Length - 1);

                    }
                    else
                    {
                        break;
                    }
                }
                 
                for (int i = 0; i < checkpoints2.Count; i++)
                {
                    timeStatusAccepted = checkpoints2[i].date;
                    DateTime dateTimeAccepted = Convert.ToDateTime(timeStatusAccepted);
                    
                    newTimeAcc = dateTimeAccepted.ToString("u");
                    SubnewTimeAcc = newTimeAcc.Substring(0, newTimeAcc.Length - 1);
                    neoCheck.Add(SubnewTimeAcc);

                    

                    statuscheck = checkpoints2[i].status;
                    neoCheck.Add(statuscheck);
                    XDocument newXmlDocFirstNode = new XDocument();
                    XDocument newXmlDocSecondNode = new XDocument();
                    XElement timeStatusDeliv = new XElement("time", SubnewTimeDel);
                    XElement timeStatusAcc = new XElement("time", SubnewTimeAcc);
                    XElement statusCode_ = new XElement("status_code", "accepted");
                    //  XElement statusch = new XElement("status_code", statuscheck);
                    switch (i)
                    {
                        case 0:
                            newXmlDocFirstNode = new XDocument(
                                                                new XElement(rootElem.Name,
                                                                             timeStatusAcc,
                                                                             statusCode_
                                                                             )
                                                                );
                            //rootElem.Add(new XElement(timeStatusAcc));
                            //rootElem.Add(new XElement(statusCode_));
                            //newXmlDocFirstNode.Add(rootElem);

                            newXmlDocFirstNode.Save(pathFileLog2);
                            using (StreamReader sr = new StreamReader(pathFileLog2, Encoding.UTF8))
                            {
                                Checkpoint magicResponse2 = (Checkpoint)xmlSerializer.Deserialize(sr);
                                magicResponse2.time = (string)timeStatusAcc;
                                magicResponse2.status_code = "accepted";
                                //magicResponse2.status = "accepted";
                                // magicResponse2.time = (string)timeStatusDeliv;
                                checkpoints3.Add(magicResponse2);
                            }
                            break;
                        case 1:
                            newXmlDocFirstNode = new XDocument(
                                                                new XElement(rootElem.Name,
                                                                             timeStatusAcc,
                                                                             statusCode_
                                                                             )
                                                                );
                            newXmlDocFirstNode.Save(pathFileLog2);
                            using (StreamReader sr = new StreamReader(pathFileLog2, Encoding.UTF8))
                            {
                                Checkpoint magicResponse2 = (Checkpoint)xmlSerializer.Deserialize(sr);
                                //magicResponse2.time_accepted = (string)timeStatusAcc;
                                //magicResponse2.status = "delivered";
                                magicResponse2.time = (string)timeStatusDeliv;
                                magicResponse2.status_code = "delivered";
                                checkpoints3.Add(magicResponse2);
                            }
                            break;
                        default:
                            break;
                    }

                }
                return checkpoints3;
            }
        }
    }
}

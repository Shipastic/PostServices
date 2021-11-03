//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net.Http;
//using System.Text;
//using System.Web;
//using System.Xml.Linq;
//using System.Xml.Serialization;

//namespace ServicePosilka
//{
//    public class MagicTransJson
//    {
//        public bool status { get; set; }
//        public int error { get; set; }
//        public object message { get; set; }
//        public Checkpointer result { get; set; }


//        public  class Checkpointer
//        {
//            public string track { get; set; }
//            public string date { get; set; }
//            public string time_delivered { get; set; }

//            public string time_accepted { get; set; }
//            public string act { get; set; }
//            public string marking { get; set; }
//            public string status { get; set; }
//            public string location { get; set; }
//            public string estimation_date { get; set; }
//        }

//        private string BaseUrl = "";

//        List<Checkpointer> checkpoints = new List<Checkpointer>();

//        List<Checkpointer> checkpoints2 = new List<Checkpointer>();
//        public List<Checkpointer> GetResponceMagictarnceAsync(string trackNumber, Client client)
//        {
//            BaseUrl = $"{client.BaseAddress}track={trackNumber}";
//            List<string> neoCheck = new List<string>();
//            string timeStatusAccepted = "";
//            string timeStatusDelivered = "";
//            string statuscheck = "";
//            string statusCode = "";
//            string statusPattern = "Груз выдан";

//            var pathFileLog = @"\\192.168.48.25\reports_recieve\logTrackerPost.xml";
//            var pathFileLog2 = @"\\192.168.48.25\reports_recieve\logTrackerPost2.xml";
//            var pathFileLog3 = @"\\192.168.48.25\reports_recieve\logTrackerPost3.xml";

//            XmlRootAttribute xRoot = new XmlRootAttribute();
//            xRoot.ElementName = "response";
//            var rootElem = new XElement("response");
//            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Checkpointer), xRoot);

//            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, BaseUrl))
//            {
//                var response = client.SendAsync(request).Result;

//                var content = response.Content.ReadAsStringAsync().Result;

//                MagicTransJson rootobject = JsonConvert.DeserializeObject<MagicTransJson>(content);
//                for (int i = 0; i < 1; i++)
//                {
//                    checkpoints.Add(rootobject.result);
//                    statusCode = checkpoints.Select(ss => ss.status).FirstOrDefault();

//                    string GetTime(string str)
//                    {
//                        string res = "";
//                        char[] ch = str.ToCharArray();
//                        for (int j = 0; j < str.Length; j++)
//                        {
//                            if (Char.IsDigit(ch[j]))
//                            {
//                                res = str.Substring(j, str.Length - j);
//                                break;
//                            }
//                        }
//                        return res;
//                    }
//                    if (statusCode.Substring(0, 10) == statusPattern)
//                    {
//                        checkpoints.Add(rootobject.result);
//                        timeStatusDelivered = GetTime(statusCode);

//                    }
//                    else
//                    {
//                        break;
//                    }
//                }
//                //foreach (var item in rootobject.data.checkpoints)
//                //{
//                //    checkpoints.Add(item);
//                //}                   
//                for (int i = 0; i < checkpoints.Count; i++)
//                {
//                    timeStatusAccepted = checkpoints[i].date;
//                    neoCheck.Add(timeStatusAccepted);
//                    statuscheck = checkpoints[i].status;
//                    neoCheck.Add(statuscheck);
//                    XDocument newXmlDocFirstNode = new XDocument();
//                    XDocument newXmlDocSecondNode = new XDocument();
//                    XElement timeStatusDeliv = new XElement("time_delivered", timeStatusDelivered);
//                    XElement timeStatusAcc = new XElement("time_accepted", timeStatusAccepted);
//                    XElement statusch = new XElement("status_code", statuscheck);
//                    switch (i)
//                    {
//                        case 0:
//                            //newXmlDocFirstNode = new XDocument(
//                            //                                    new XElement("response",
//                            //                                                 timeStatusAcc,
//                            //                                                 statusch
//                            //                                                 )
//                            //                                    );
//                            //rootElem.Add(new XElement(timeStatusAcc));
//                            //rootElem.Add(new XElement(statusch));
//                            //newXmlDocFirstNode.Add(rootElem);

//                            //newXmlDocFirstNode.Save(pathFileLog2);

//                            //using (FileStream fileStream1 = new FileStream(pathFileLog2, FileMode.Create))
//                            //    {
//                            //        byte[] array = Encoding.UTF8.GetBytes(newXmlDocFirstNode.ToString());
//                            //        fileStream1.Write(array, 0, array.Length);
//                            //    }
//                            using (StreamReader sr = new StreamReader(pathFileLog2, Encoding.UTF8))
//                            {
//                                Checkpointer magicResponse2 = (Checkpointer)xmlSerializer.Deserialize(sr);
//                                magicResponse2.time_accepted = (string)timeStatusAcc;
//                                magicResponse2.status = (string)statusch;
//                                magicResponse2.time_delivered = null;

//                                checkpoints2.Add(magicResponse2);
//                            }
//                            break;
//                        case 1:

//                            //rootElem.Add(new XElement(timeStatusAcc));
//                            //rootElem.Add(new XElement(statusch));
//                            //rootElem.Add(new XElement(timeStatusDeliv));

//                            //newXmlDocSecondNode.Add(rootElem);

//                            //newXmlDocSecondNode.Save(pathFileLog2);

//                            //newXmlDocSecondNode = new XDocument(
//                            //                                    rootElem.Add(
//                            //                                                 new XElement("time_accepted", timeStatusAccepted),
//                            //                                                 new XElement("status_code", statuscheck),
//                            //                                                 timeStatusDeliv
//                            //                                                 )
//                            //                                    );
//                            //newXmlDocFirstNode.Add(eee);
//                            //newXmlDocFirstNode.Element("time_delivered", timeStatusDelivered).Add()
//                            //newXmlDocSecondNode =  new XDocument(
//                            //                                       newXmlDocFirstNode.Root.NextNode,
//                            //                                       new XElement("time_delivered", timeStatusDelivered).);
//                            //using (FileStream fileStream1 = new FileStream(pathFileLog2, FileMode.Append))
//                            //{
//                            //    byte[] array = Encoding.UTF8.GetBytes(newXmlDocSecondNode.ToString());
//                            //    fileStream1.Write(array, 0, array.Length);
//                            //}
//                            using (StreamReader sr = new StreamReader(pathFileLog2, Encoding.UTF8))
//                            {
//                                Checkpointer magicResponse2 = (Checkpointer)xmlSerializer.Deserialize(sr);
//                                magicResponse2.time_accepted = (string)timeStatusAcc;
//                                magicResponse2.status = (string)statusch;
//                                magicResponse2.time_delivered = (string)timeStatusDeliv;

//                                checkpoints2.Add(magicResponse2);
//                            }
//                            break;
//                        default:
//                            break;
//                    }
                    
//                }               
//                return checkpoints2;
//            }
//        }
//    }
//}
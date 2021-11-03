using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ServicePosilka
{
    //public class MagicTranceRoot
    //{

    // Примечание. Для запуска созданного кода может потребоваться NET Framework версии 4.5 или более поздней версии и .NET Core или Standard версии 2.0 или более поздней.
    /// <remarks/>
    //[System.SerializableAttribute(), XmlRoot("response")]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    ////[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    ////public partial class Checkpoint
    ////{

    ////    private bool statusField;

    ////    private byte errorField;

    ////    private object messageField;

    ////    private resultResponse resultField;

    ////    private string BaseUrl = "";

    ////    List<Checkpoint> checkpoints = new List<Checkpoint>();
    ////    List<Checkpoint> checkpoints2 = new List<Checkpoint>();

    ////    List<string> neoChecks = new List<string>();
    ////    /// <remarks/>
    ////    public bool status
    ////    {
    ////        get
    ////        {
    ////            return this.statusField;
    ////        }
    ////        set
    ////        {
    ////            this.statusField = value;
    ////        }
    ////    }

    ////    /// <remarks/>
    ////    public byte error
    ////    {
    ////        get
    ////        {
    ////            return this.errorField;
    ////        }
    ////        set
    ////        {
    ////            this.errorField = value;
    ////        }
    ////    }

    ////    /// <remarks/>
    ////    public object message
    ////    {
    ////        get
    ////        {
    ////            return this.messageField;
    ////        }
    ////        set
    ////        {
    ////            this.messageField = value;
    ////        }
    ////    }

    ////    /// <remarks/>
    ////    public resultResponse result
    ////    {
    ////        get
    ////        {
    ////            return this.resultField;
    ////        }
    ////        set
    ////        {
    ////            this.resultField = value;
    ////        }
    ////    }

    ////    public List<Checkpoint> GetResponceMagictarnceAsync(string trackNumber, Client client)
    ////    {
    ////        BaseUrl = $"{client.BaseAddress}track={trackNumber}";

    ////        var pathFileLog = @"\\192.168.48.25\reports_recieve\logTrackerPost.xml";
    ////        var pathFileLog2 = @"\\192.168.48.25\reports_recieve\logTrackerPost2.xml";

    ////        using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, BaseUrl))
    ////        {
    ////            XmlRootAttribute xRoot = new XmlRootAttribute();
    ////            xRoot.ElementName = "response";
    ////            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Checkpoint), xRoot);
    ////            var response = client.SendAsync(request).Result;
    ////            var content = response.Content.ReadAsStringAsync().Result;

    ////            using (FileStream fileStream1 = new FileStream(pathFileLog, FileMode.Create))
    ////            {
    ////                byte[] array = Encoding.UTF8.GetBytes(content);
    ////                fileStream1.Write(array, 0, array.Length);
    ////            }

    ////            Checkpoint magicResponse = (Checkpoint)xmlSerializer.Deserialize(new StreamReader(pathFileLog));

    ////            checkpoints.Add(magicResponse);
    ////            foreach (var item in checkpoints)
    ////            {
    ////                var timecheck = checkpoints.Select(e => e.result.date).FirstOrDefault();
    ////                neoChecks.Add(timecheck);
    ////                var statusCheck = checkpoints.Select(s => s.result.status).FirstOrDefault();
    ////                neoChecks.Add(statusCheck);
    ////            }
    ////            var newXmlDoc = new XDocument(
    ////                new XElement("response",
    ////                    new XElement("time", checkpoints.Select(e => e.result.date).FirstOrDefault()),
    ////                    new XElement("status_code", checkpoints.Select(s => s.result.status).FirstOrDefault())));

    ////            using (FileStream fileStream1 = new FileStream(pathFileLog2, FileMode.Create))
    ////            {
    ////                byte[] array = Encoding.UTF8.GetBytes(newXmlDoc.ToString());
    ////                fileStream1.Write(array, 0, array.Length);
    ////            }

    ////            Checkpoint magicResponse2 = (Checkpoint)xmlSerializer.Deserialize(new StreamReader(pathFileLog2, Encoding.UTF8));
    ////            checkpoints2.Add(magicResponse2);
    ////            //magicResponse = (Checkpoint)neoChecks;
    ////            return checkpoints2;
    ////        }
    ////    }
    ////}

    ///// <remarks/>
    //[System.SerializableAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    //public partial class resultResponse
    //{

    //    private string trackField;

    //    private string dateField;

    //    private uint actField;

    //    private string markingField;

    //    private string statusField;

    //    private object locationField;

    //    private string estimation_dateField;

    //    /// <remarks/>
    //    public string track
    //    {
    //        get
    //        {
    //            return this.trackField;
    //        }
    //        set
    //        {
    //            this.trackField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public string date
    //    {
    //        get
    //        {
    //            return this.dateField;
    //        }
    //        set
    //        {
    //            this.dateField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public uint act
    //    {
    //        get
    //        {
    //            return this.actField;
    //        }
    //        set
    //        {
    //            this.actField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public string marking
    //    {
    //        get
    //        {
    //            return this.markingField;
    //        }
    //        set
    //        {
    //            this.markingField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public string status
    //    {
    //        get
    //        {
    //            return this.statusField;
    //        }
    //        set
    //        {
    //            this.statusField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public object location
    //    {
    //        get
    //        {
    //            return this.locationField;
    //        }
    //        set
    //        {
    //            this.locationField = value;
    //        }
    //    }

    //    /// <remarks/>
    //    public string estimation_date
    //    {
    //        get
    //        {
    //            return this.estimation_dateField;
    //        }
    //        set
    //        {
    //            this.estimation_dateField = value;
    //        }
    //    }
    //}


    //}
}
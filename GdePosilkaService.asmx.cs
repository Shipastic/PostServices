using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Services;


//using static ServicePosilka.Rootobject<T>;
//using static ServicePosilka.MagicTransJson;

namespace ServicePosilka
{
    /// <summary>
    /// Сводное описание для GdePosilkaService
    /// </summary>
    [WebService(Namespace = "http://gdeposilka.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Чтобы разрешить вызывать веб-службу из скрипта с помощью ASP.NET AJAX, раскомментируйте следующую строку. 
    // [System.Web.Script.Services.ScriptService]
    public class GdePosilkaService : WebService
    {
        private static string token = "33fa5d4de52f0693a4466b42998b86f62fa37ff8531f607a5962a7c829f5c2a3bf5988e69833f3c4";

        private static string FirstUrl = "https://gdeposylka.ru/api/v4/tracker/";

        private string MagicTranceUrlApi = "http://magic-trans.ru/api/v1/delivery/getOrderInfo.json?";

        //private string BaseUrl = "";

        Rootobject<Checkpoint> magicTranceResponse = new Rootobject<Checkpoint>();

        Rootobject<Checkpoint> gdePosilkaService = new Rootobject<Checkpoint>();

        //[WebMethod]
        //public List<Checkpoint> GetResponceGdePosilka(string slugName, string trackNumber)
        //{
        //    BaseUrl = $"{FirstUrl}{slugName}/{trackNumber}";

        //    using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, BaseUrl))
        //    {
        //        var response = client1.SendAsync(request).Result;

        //        var content = response.Content.ReadAsStringAsync().Result;

        //        Rootobject rootobject = JsonConvert.DeserializeObject<Rootobject>(content);

        //        foreach (var item in rootobject.data.checkpoints)
        //        {
        //            checkpoints.Add(item);
        //        }
        //        return checkpoints;
        //    }
        //}

       // [WebMethod]

        public void GetVariantPost(string slugName, string trackNumber)
        {
            if (slugName != "magictrans")
            {
                Client client = new Client(token, FirstUrl);
                GetPostGdePosilka(slugName, trackNumber);
            }
            else
            {
                Client client = new Client(MagicTranceUrlApi);
                GetPostMagicTrans(slugName, trackNumber);
            }
        }

        //[WebMethod]
        //public List<string> EchoSoapRequest(string slugName, string trackNumber)
        //{
        //    var pathFileLog = @"\\192.168.48.25\reports_recieve\logTrackerPost.txt";
        //    string resultStringHeader = "";
        //    string headerValue = "";

        //    BaseUrl = $"{FirstUrl}{slugName}/{trackNumber}";

        //    using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, BaseUrl))
        //    {
        //        string textBegin = $"============  Запрос отправлен в {DateTime.Now}  ============  \n Вывод Header-ов запроса\n\n";
        //        using (FileStream fileStream1 = new FileStream(pathFileLog, FileMode.Append))
        //        {
        //            byte[] array = Encoding.Default.GetBytes(textBegin);
        //            fileStream1.Write(array, 0, array.Length);
        //        }
        //        var response = client1.SendAsync(request).Result;

        //        var receiveStream = HttpContext.Current.Request.Headers;

        //        List<string> haedersValues = new List<string>();

        //        foreach (var item in receiveStream.Keys)
        //        {
        //            string headerKey = item.ToString();

        //            haedersValues.Add($"{headerKey} - {receiveStream.Get(item.ToString())}\n");

        //            using (FileStream fileStream1 = new FileStream(pathFileLog, FileMode.Append))
        //            {
        //                byte[] array = Encoding.Default.GetBytes($"{headerKey} - {receiveStream.Get(item.ToString())}\n");

        //                fileStream1.Write(array, 0, array.Length);
        //            }
        //        }
        //        string textEnd = $"============  Конец запроса  ============  \n\n";
        //        using (FileStream fileStream1 = new FileStream(pathFileLog, FileMode.Append))
        //        {
        //            byte[] array = Encoding.Default.GetBytes(textEnd);
        //            fileStream1.Write(array, 0, array.Length);
        //        }

        //        return haedersValues;
        //    }
        //}

       
        [WebMethod]
        public List<Checkpoint> GetPostGdePosilka(string slugName, string trackNumber)
        {
            if (slugName != "magictrans".ToUpper())
            {
                Client client = new Client(token, FirstUrl);
                return gdePosilkaService.GetResponceGdePosilka(slugName, trackNumber, client);
            }
             return null;
        }


        [WebMethod]
        public List<Checkpoint> GetPostMagicTrans(string slugName, string trackNumber)
        {
            if (slugName == "magictrans")
            {
                Client client = new Client(MagicTranceUrlApi);

                return  magicTranceResponse.GetResponceMagictarnceAsync(trackNumber, client);
            }
            return null;
        }
    }
}

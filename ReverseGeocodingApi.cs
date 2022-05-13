using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAppUWP
{
    class ReverseGeocodingApi
    {
        public async static Task<string> GetLocationName(double lat, double lon)
        {
            var http = new HttpClient();
            var url = String.Format("http://api.openweathermap.org/geo/1.0/reverse?lat={0}&lon={1}&limit=1&appid={2}", lat, lon, MainPage.ApiKey);
            var response = await http.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            List<RootobjectReGeo> myDeserializedObjList = (List<RootobjectReGeo>)JsonConvert.DeserializeObject(result, typeof(List<RootobjectReGeo>));
            string ru_name = myDeserializedObjList[0].local_names.ru;
            return ru_name;
        }
    }

    public class RootobjectReGeo
    {
        public string name { get; set; }
        public Local_Names local_names { get; set; }
        public float lat { get; set; }
        public float lon { get; set; }
        public string country { get; set; }
        public string state { get; set; }
    }

    public class Local_Names
    {
        //public string nl { get; set; }
        //public string fr { get; set; }
        //public string ko { get; set; }
        //public string fi { get; set; }
        //public string be { get; set; }
        //public string kn { get; set; }
        //public string lt { get; set; }
        //public string tr { get; set; }
        //public string pl { get; set; }
        //public string de { get; set; }
        //public string sv { get; set; }
        //public string hr { get; set; }
        //public string sl { get; set; }
        public string ru { get; set; }
        //public string es { get; set; }
        //public string uk { get; set; }
        //public string it { get; set; }
        //public string ar { get; set; }
        //public string sk { get; set; }
        //public string ja { get; set; }
        //public string en { get; set; }
        //public string zh { get; set; }
        //public string hi { get; set; }
        //public string ca { get; set; }
        //public string et { get; set; }
        //public string ro { get; set; }
        //public string da { get; set; }
        //public string cs { get; set; }
        //public string hu { get; set; }
    }
}

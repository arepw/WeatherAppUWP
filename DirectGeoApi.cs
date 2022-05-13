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
    class DirectGeoApi
    {
        public async static Task<(double lat, double lon)> SearchLocation(string query)
        {
            var http = new HttpClient();
            var url = String.Format("https://api.openweathermap.org/geo/1.0/direct?q={0}&limit=1&appid={1}", query, MainPage.ApiKey);
            var response = await http.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            List<RootObjectDirectGeo> myDeserializedObjList = (List<RootObjectDirectGeo>)JsonConvert.DeserializeObject(result, typeof(List<RootObjectDirectGeo>));
            double lat = myDeserializedObjList[0].lat;
            double lon = myDeserializedObjList[0].lon;
            return (lat, lon);
        }
    }
    public class RootObjectDirectGeo
    {
        public string name { get; set; }
        public Local_Names_Search local_names { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string country { get; set; }
        public string state { get; set; }
    }

    public class Local_Names_Search
    {
        public string nl { get; set; }
        public string fr { get; set; }
        public string ro { get; set; }
        public string ja { get; set; }
        public string be { get; set; }
        public string sl { get; set; }
        public string kn { get; set; }
        public string uk { get; set; }
        public string da { get; set; }
        public string ca { get; set; }
        public string ru { get; set; }
        public string hu { get; set; }
        public string sk { get; set; }
        public string zh { get; set; }
        public string fi { get; set; }
        public string hr { get; set; }
        public string sv { get; set; }
        public string pl { get; set; }
        public string de { get; set; }
        public string cs { get; set; }
        public string en { get; set; }
        public string es { get; set; }
        public string ar { get; set; }
        public string tr { get; set; }
        public string hi { get; set; }
        public string lt { get; set; }
        public string it { get; set; }
        public string ko { get; set; }
        public string et { get; set; }
    }
}

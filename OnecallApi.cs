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
    class OnecallApi
    {
        public async static Task<Rootobject> GetCurrentWeather1C(double lat, double lon)
        {
            var http = new HttpClient();
            var url = String.Format("https://api.openweathermap.org/data/2.5/onecall?lat={0}&lon={1}&exclude=hourly,minutely,daily&units=metric&lang=ru&appid=fae6086566bfc2ee3f3361ed5f17fe7c", lat, lon);
            var response = await http.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(Rootobject));
            var memStream = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (Rootobject)serializer.ReadObject(memStream);
            return data;
        }
    }

    [DataContract]
    public class Rootobject
    {
        //public float lat { get; set; }
        //public float lon { get; set; }
        //public string timezone { get; set; }
        //public int timezone_offset { get; set; }
        [DataMember]
        public Current current { get; set; }
    }

    [DataContract]
    public class Current
    {
        //public int dt { get; set; }
        //public int sunrise { get; set; }
        //public int sunset { get; set; }
        //public float temp { get; set; }
        //public float feels_like { get; set; }
        //public int pressure { get; set; }
        //public int humidity { get; set; }
        [DataMember]
        public float dew_point { get; set; }
        [DataMember]
        public int uvi { get; set; }
        [DataMember]
        public int clouds { get; set; }
        [DataMember]
        public int visibility { get; set; }
        //public float wind_speed { get; set; }
        //public int wind_deg { get; set; }
        //public Weather[] weather { get; set; }
    }

    //public class WeatherOne
    //{
    //    public int id { get; set; }
    //    public string main { get; set; }
    //    public string description { get; set; }
    //    public string icon { get; set; }
    //}


}

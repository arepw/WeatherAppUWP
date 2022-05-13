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
        public async static Task<RootobjectOneCall> GetCurrentWeather1C(double lat, double lon)
        {
            var http = new HttpClient();
            var url = String.Format("https://api.openweathermap.org/data/2.5/onecall?lat={0}&lon={1}&exclude=minutely&units=metric&lang=ru&appid={2}", lat, lon, MainPage.ApiKey);
            var response = await http.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(RootobjectOneCall));
            var memStream = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (RootobjectOneCall)serializer.ReadObject(memStream);
            return data;
        }
    }

    public class RootobjectOneCall
    {
        public float lat { get; set; }
        public float lon { get; set; }
        public string timezone { get; set; }
        //public int timezone_offset { get; set; }
        public Current current { get; set; }
        //public Hourly[] hourly { get; set; }
        public Daily[] daily { get; set; }
        //public Alert[] alerts { get; set; }
    }

    public class Current
    {
        public int dt { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
        public float temp { get; set; }
        public float feels_like { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public float dew_point { get; set; }
        public float uvi { get; set; }
        public int clouds { get; set; }
        public int visibility { get; set; }
        public float wind_speed { get; set; }
        public int wind_deg { get; set; }
        public float wind_gust { get; set; }
        public Weather0[] weather { get; set; }
    }

    public class Weather0
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Hourly
    {
        public int dt { get; set; }
        public float temp { get; set; }
        public float feels_like { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public float dew_point { get; set; }
        public float uvi { get; set; }
        public int clouds { get; set; }
        public int visibility { get; set; }
        public float wind_speed { get; set; }
        public int wind_deg { get; set; }
        public float wind_gust { get; set; }
        public Weather1[] weather { get; set; }
        public int pop { get; set; }
    }

    public class Weather1
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Daily
    {
        public int dt { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
        public int moonrise { get; set; }
        public int moonset { get; set; }
        public float moon_phase { get; set; }
        public Temp temp { get; set; }
        public Feels_Like feels_like { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public float dew_point { get; set; }
        public float wind_speed { get; set; }
        public int wind_deg { get; set; }
        public float wind_gust { get; set; }
        public Weather2[] weather { get; set; }
        public int clouds { get; set; }
        public float pop { get; set; }
        public float uvi { get; set; }
        public float rain { get; set; }
    }

    public class Temp
    {
        public float day { get; set; }
        public float min { get; set; }
        public float max { get; set; }
        public float night { get; set; }
        public float eve { get; set; }
        public float morn { get; set; }
    }

    public class Feels_Like
    {
        public float day { get; set; }
        public float night { get; set; }
        public float eve { get; set; }
        public float morn { get; set; }
    }

    public class Weather2
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Alert
    {
        public string sender_name { get; set; }
        public string _event { get; set; }
        public int start { get; set; }
        public int end { get; set; }
        public string description { get; set; }
        public string[] tags { get; set; }
    }
}

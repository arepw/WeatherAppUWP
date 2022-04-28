using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

//GuZ8PfHkdhdNZ3RQTgNFcn323vW1u9YJ
//http://dataservice.accuweather.com/locations/v1/cities/geoposition/search?apikey=GuZ8PfHkdhdNZ3RQTgNFcn323vW1u9YJ&q={0}%2C{1}&language=ru&toplevel=true

namespace WeatherAppUWP
{
    class WeekApi
    {
        public async static Task<(string[] phrases, float[] min_temps, float[] max_temps, int[] icons, string[] dates)> GetWeekForecast(int locationkey, string apikey)
        {
            string[] pharses = new string[5];
            float[] min_temps = new float[5];
            float[] max_temps = new float[5];
            int[] icons = new int[5];
            string[] dates = new string[5];
            var http = new HttpClient();
            var url = String.Format("http://dataservice.accuweather.com/forecasts/v1/daily/5day/{0}?apikey={1}&language=ru&metric=true", locationkey, apikey);
            var response = await http.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
            //var serializer = new DataContractJsonSerializer(typeof(WeekRoot));
            //var memStream = new MemoryStream(Encoding.UTF8.GetBytes(result));
            //var data = (WeekRoot)serializer.ReadObject(memStream);
            var temp = (WeekRoot)JsonConvert.DeserializeObject(result, typeof(WeekRoot));
            int i = 0;
            foreach (DailyForecast item in temp.DailyForecasts)
            {
                pharses[i] = item.Day.IconPhrase;
                max_temps[i] = item.Temperature.Maximum.Value;
                min_temps[i] = item.Temperature.Minimum.Value;
                icons[i] = item.Day.Icon;
                dates[i] = Convert.ToString(item.Date.DayOfWeek);
                i++;
            }
            for(int j = 1; j < dates.Length; j++)
            {
                switch (dates[j])
                {
                    case "Monday":
                        dates[j] = "Понедельник";
                        break;
                    case "Tuesday":
                        dates[j] = "Вторник";
                        break;
                    case "Wednesday":
                        dates[j] = "Среда";
                        break;
                    case "Thursday":
                        dates[j] = "Четверг";
                        break;
                    case "Friday":
                        dates[j] = "Пятница";
                        break;
                    case "Saturday":
                        dates[j] = "Суббота";
                        break;
                    case "Sunday":
                        dates[j] = "Воскресенье";
                        break;
                }
            }

            return (pharses, min_temps, max_temps, icons, dates);
        }

        [DataContract]
        public class WeekRoot
        {
            //[DataMember]
            //public Headline Headline { get; set; }
            [DataMember]
            public DailyForecast[] DailyForecasts { get; set; }
        }



        //[DataContract]
        //public class Headline
        //{
        //    [DataMember]
        //    public DateTime EffectiveDate { get; set; }
        //    [DataMember]
        //    public int EffectiveEpochDate { get; set; }
        //    [DataMember]
        //    public int Severity { get; set; }
        //    [DataMember]
        //    public string Text { get; set; }
        //    [DataMember]
        //    public string Category { get; set; }
        //    [DataMember]
        //    public DateTime EndDate { get; set; }
        //    [DataMember]
        //    public int EndEpochDate { get; set; }
        //    [DataMember]
        //    public string MobileLink { get; set; }
        //    [DataMember]
        //    public string Link { get; set; }
        //}

        [DataContract]
        public class DailyForecast
        {
            [DataMember]
            public DateTime Date { get; set; }
            [DataMember]
            public int EpochDate { get; set; }
            [DataMember]
            public Temperature Temperature { get; set; }
            [DataMember]
            public Day Day { get; set; }
            [DataMember]
            public Night Night { get; set; }
            [DataMember]
            public string[] Sources { get; set; }
            [DataMember]
            public string MobileLink { get; set; }
            [DataMember]
            public string Link { get; set; }
        }

        [DataContract]
        public class Temperature
        {
            [DataMember]
            public Minimum Minimum { get; set; }
            [DataMember]
            public Maximum Maximum { get; set; }
        }

        [DataContract]
        public class Minimum
        {
            [DataMember]
            public float Value { get; set; }
            [DataMember]
            public string Unit { get; set; }
            [DataMember]
            public int UnitType { get; set; }
        }

        [DataContract]
        public class Maximum
        {
            [DataMember]
            public float Value { get; set; }
            [DataMember]
            public string Unit { get; set; }
            [DataMember]
            public int UnitType { get; set; }
        }

        [DataContract]
        public class Day
        {
            [DataMember]
            public int Icon { get; set; }
            [DataMember]
            public string IconPhrase { get; set; }
            [DataMember]
            public bool HasPrecipitation { get; set; }
            [DataMember]
            public string PrecipitationType { get; set; }
            [DataMember]
            public string PrecipitationIntensity { get; set; }
        }

        [DataContract]
        public class Night
        {
            [DataMember]
            public int Icon { get; set; }
            [DataMember]
            public string IconPhrase { get; set; }
            [DataMember]
            public bool HasPrecipitation { get; set; }
            [DataMember]
            public string PrecipitationType { get; set; }
            [DataMember]
            public string PrecipitationIntensity { get; set; }
        }
    }
}

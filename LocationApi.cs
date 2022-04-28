using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
//GuZ8PfHkdhdNZ3RQTgNFcn323vW1u9YJ
//853JUGFNxQ2YKTUN0f39EKGiBe8ZdAT9
//http://dataservice.accuweather.com/locations/v1/cities/geoposition/search?apikey=GuZ8PfHkdhdNZ3RQTgNFcn323vW1u9YJ&q={0}%2C{1}&language=ru&toplevel=true


namespace WeatherAppUWP
{
    class LocationApi
    {
        public async static Task<LocRoot> ReverseGeocoding(double lat, double lon, string apikey)
        {
            var http = new HttpClient();
            var url = String.Format("http://dataservice.accuweather.com/locations/v1/cities/geoposition/search?apikey={0}&q={1}%2C{2}&language=ru&toplevel=true", apikey, lat, lon);
            var response = await http.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(LocRoot));
            var memStream = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (LocRoot)serializer.ReadObject(memStream);
            return data;
        }
    }
    [DataContract]
    public class LocRoot
    {
        [DataMember]
        public int Key { get; set; }
        [DataMember]
        public string LocalizedName { get; set; }
    }
}

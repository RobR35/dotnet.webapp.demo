using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;

namespace net.core.webapp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        private static readonly HttpClient client = new HttpClient();

        private static readonly string[] stationIDs =
        {
            "6193",             // Coast
            "6159",             // Flava
            "6187",             // Newstalk ZB
            "6191",             // Radio Hauraki
            "6194",             // Radio Sport
            "6197",             // The Hits
            "6190",             // ZM
        };

        public static async Task<string> ProcessStations()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET iHeart Retriever");

            var serializer = new DataContractJsonSerializer(typeof(Content));

            // Pull the station information
            var stations = new List<Station>();
            foreach (string ID in stationIDs)
            {
                var streamTask = client.GetStreamAsync("https://nz.api.iheart.com/api/v2/content/liveStations/" + ID);
                var content = serializer.ReadObject(await streamTask) as Content;
                if (content != null)
                {
                    if (content.Stations.Count > 0)
                    {
                        stations.Add(content.Stations[0]);
                    }
                }
            }

            if (stations.Count > 0)
            {
                var resultString = "";
                foreach (var station in stations)
                {
                    resultString += "<img src=\"" + station.Logo+ "\" margin=0 width=300 height=300>" + station.Name + "<br>";
                }
                return resultString;
            }
            return "No content found";
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}

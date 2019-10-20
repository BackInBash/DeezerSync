using DeezerSync.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DeezerSync.Models.API;
using DeezerSync.Log;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeezerSync.DeezerAPI
{
    public class Official
    {
        private const string Official_api = "https://api.deezer.com/search?q=";
        private readonly HttpClient client = new HttpClient();
        private StandardTitle title = null;
        private string Request_Query = string.Empty;
        public NLogger log;

        public Official(StandardTitle title)
        {
            var config = new ConfigurationBuilder()
            .SetBasePath(System.IO.Directory.GetCurrentDirectory()) //From NuGet Package Microsoft.Extensions.Configuration.Json
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

            var servicesProvider = DeezerSync.Log.Logging.BuildDi(config);
            using (servicesProvider as IDisposable)
            {
                log = servicesProvider.GetRequiredService<NLogger>();
            }

            this.title = title;
            if (!string.IsNullOrWhiteSpace(title.username) || string.IsNullOrWhiteSpace(title.title) || title.duration != 0)
            {
                log.Debug("Send Request with Artist: " + title.username + " Track: " + title.title + " Duration: " + title.duration);
                Request_Query = Official_api + "artist:" + "\"" + title.username + "\" " + "track:" + "\"" + title.title + "\" " + "dur_min:" + (title.duration - 1).ToString() + " dur_max:" + (title.duration + 1).ToString();
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(title.username) || string.IsNullOrWhiteSpace(title.title))
                {
                    log.Debug("Send Request with Artist: " + title.username + " Track: " + title.title);
                    Request_Query = Official_api + "artist:" + "\"" + title.username + "\" " + "track:" + "\"" + title.title + "\"";
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(title.title))
                    {
                        log.Debug("Send Request with Track: " + title.title);
                        Request_Query = Official_api + title.title;
                    }
                }
            }
        }

        public async Task<ResultSearch.Search> Search()
        {
            return JsonConvert.DeserializeObject<ResultSearch.Search>(await request());
        }

        private async Task<string> request()
        {
            string res = await client.GetStringAsync(Request_Query);
            if (res.Equals("{\"error\":{\"type\":\"Exception\",\"message\":\"Quota limit exceeded\",\"code\":4}}"))
            {
                log.Info("API Rate Limit waiting 2 sec.");
                Thread.Sleep(2000);
                res = await request();
            }
            if (string.IsNullOrWhiteSpace(res))
            {
                log.Info("Response is Empty");
            }
            return res;
        }
    }
}

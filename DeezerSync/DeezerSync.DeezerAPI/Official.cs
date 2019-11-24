using DeezerSync.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DeezerSync.Models.API;
using DeezerSync.Log;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace DeezerSync.DeezerAPI
{
    public class Official
    {
        private const string Official_api = "https://api.deezer.com/search/track?strict=on&q=";
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
            if (title.search_stage == 1)
            {
                log.Debug("Send Request with Artist: " + title.artist ?? title.username + " Track: " + title.title + " Duration: " + title.duration);
                Request_Query = Official_api + "artist:" + "\"" + WebUtility.UrlEncode(title.artist.Trim() ?? title.username.Trim()) + "\" " + "track:" + "\"" + WebUtility.UrlEncode(title.title.Trim()) + "\" " + "dur_min:" + (title.duration - 1).ToString() + " dur_max:" + (title.duration + 1).ToString();
            }
            else
            {
                if (title.search_stage == 2)
                {
                    if (title.isRemix)
                    {
                        log.Debug("Send Request with Remix Artist: " + title.remixArtist ?? title.username + " Track: " + title.title + " Duration: " + title.duration);
                        Request_Query = Official_api + "artist:" + "\"" + WebUtility.UrlEncode(title.remixArtist.Trim() ?? title.username.Trim()) + "\" " + "track:" + "\"" + WebUtility.UrlEncode(title.title.Trim()) + "\" " + "dur_min:" + (title.duration - 1).ToString() + " dur_max:" + (title.duration + 1).ToString();
                    }
                    else
                    {
                        log.Debug("Send Request with Artist: " + title.artist ?? title.username + " Track: " + title.title + " Duration: " + title.duration);
                        Request_Query = Official_api + "artist:" + "\"" + WebUtility.UrlEncode(title.artist.Trim() ?? title.username.Trim()) + "\" " + "track:" + "\"" + WebUtility.UrlEncode(title.title.Trim()) + "\" " + "dur_min:" + (title.duration - 1).ToString() + " dur_max:" + (title.duration + 1).ToString();
                    }
                }
                else
                {
                    if (title.search_stage == 3)
                    {
                        log.Debug("Send Request with Track: " + title.artist ?? title.username + " " + title.title);
                        Request_Query = Official_api + WebUtility.UrlEncode(title.artist.Trim() ?? title.username.Trim() + " " + title.title.Trim());
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
            HttpClient client = new HttpClient();
            string res = string.Empty;
            try
            {
                res = await client.GetStringAsync(Request_Query);
            }
            catch (Exception e)
            {
                log.Error(e.Message + " Link: " + Request_Query);
            }
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

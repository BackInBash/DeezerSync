using DeezerSync.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DeezerSync.Models.API;

namespace DeezerSync.DeezerAPI
{
    public class Official
    {
        private const string Official_api = "https://api.deezer.com/search?q=";
        private readonly HttpClient client = new HttpClient();
        private StandardTitle title = null;
        private string Request_Query = string.Empty;

        public Official(StandardTitle title)
        {
            this.title = title;
            if (!string.IsNullOrWhiteSpace(title.username) || string.IsNullOrWhiteSpace(title.title) || title.duration != 0)
            {
                Request_Query = Official_api + "artist:" + "\"" + title.username + "\" " + "track:" + "\"" + title.title + "\" " + "dur_min:" + (title.duration - 1).ToString() + " dur_max:" + (title.duration + 1).ToString();
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(title.username) || string.IsNullOrWhiteSpace(title.title))
                {
                    Request_Query = Official_api + "artist:" + "\"" + title.username + "\" " + "track:" + "\"" + title.title + "\"";
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(title.title))
                    {
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
                Thread.Sleep(2000);
                res = await request();
            }
            return res;
        }
    }
}

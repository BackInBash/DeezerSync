using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SoundCloud.Api;
using SoundCloud.Api.Entities;

namespace DeezerSync.SoundCloud
{
    public class Loader
    {
        private static readonly string SOUNDCLOUD_CLIENTID = "https://a-v2.sndcdn.com/assets/app-e56f488-d5a2fcb-3.js";
        protected static string username = Config.soundcloud_profile;
        protected static User user = null;
        protected ISoundCloudClient client = null;
        private HttpClient http = new HttpClient();

        /// <summary>
        /// Gets a SoundCloud client ID from the config. If it isn´t set try to pull it from the webapp.
        /// </summary>
        /// <returns>SoundCloud Client ID</returns>
        private async Task<string> getClientID()
        {
            if (string.IsNullOrWhiteSpace(Config.soundcloud_clientid))
            {
                Match m = Regex.Match(await http.GetStringAsync(SOUNDCLOUD_CLIENTID), ",client_id:\"[a-zA-Z_0-9]*\"");
                Match m1 = Regex.Match(m.Value, "\"[a-zA-Z_0-9]*\"");
                return Regex.Replace(m1.Value, "[(^\") + (?=\"\\n)]", "").Trim();
            } else
            {
                return Config.soundcloud_clientid;
            }
        }

        /// <summary>
        /// Initialize SoundCloud API with the provided ClientID.
        /// </summary>
        protected async Task init()
        {
            client = SoundCloudClient.CreateUnauthorized(await getClientID());
            var entity = await client.Resolve.GetEntityAsync("https://soundcloud.com/" + username);
            user = entity as User;
        }
    }
}

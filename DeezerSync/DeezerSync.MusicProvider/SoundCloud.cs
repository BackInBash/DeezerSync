using SoundCloud.Api;
using SoundCloud.Api.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DeezerSync.Models;

namespace DeezerSync.MusicProvider
{
    public class SoundCloud
    {
        private static readonly string SOUNDCLOUD_CLIENTID = "https://a-v2.sndcdn.com/assets/app-e56f488-d5a2fcb-3.js";
        private string username = string.Empty;
        private User user = null;
        private string ClientID = string.Empty;
        private ISoundCloudClient client = null;
        private HttpClient http = new HttpClient();

        /// <summary>
        /// Gets a SoundCloud client ID from the config. If it isn´t set try to pull it from the webapp.
        /// </summary>
        /// <returns>SoundCloud Client ID</returns>
        private async Task<string> getClientID()
        {
            Match m = Regex.Match(await http.GetStringAsync(SOUNDCLOUD_CLIENTID), ",client_id:\"[a-zA-Z_0-9]*\"");
            Match m1 = Regex.Match(m.Value, "\"[a-zA-Z_0-9]*\"");
            return Regex.Replace(m1.Value, "[(^\") + (?=\"\\n)]", "").Trim();
        }

        /// <summary>
        /// Initialize SoundCloud API with the provided ClientID.
        /// </summary>
        public SoundCloud(string username)
        {
            this.username = username;
            client = SoundCloudClient.CreateUnauthorized(getClientID().Result);
            var entity = client.Resolve.GetEntityAsync("https://soundcloud.com/" + username).GetAwaiter().GetResult();
            user = entity as User;
        }

        public SoundCloud(string username, string clientid)
        {
            this.username = username;
            client = SoundCloudClient.CreateUnauthorized(clientid);
            var entity = client.Resolve.GetEntityAsync("https://soundcloud.com/" + username).GetAwaiter().GetResult();
            user = entity as User;
        }

        /// <summary>
        /// Gets all playlists.
        /// </summary>
        /// <returns>The playlists.</returns>
        private async Task<IEnumerable<Playlist>> GetPlaylists()
        {
            IEnumerable<Playlist> list = await client.Users.GetPlaylistsAsync(user);
            return list;
        }

        /// <summary>
        /// Save all Playlist Data to Standard List
        /// </summary>
        /// <returns>Standard SoundCloud Playlist</returns>
        public async Task<List<StandardPlaylist>> GetStandardPlaylists()
        {
            var playlistdata = await GetPlaylists();
            List<StandardPlaylist> pl = new List<StandardPlaylist>();

            foreach (var i in playlistdata)
            {
                var trackinfo = i.Tracks;
                List<StandardTitle> track = new List<StandardTitle>();

                foreach (var a in trackinfo)
                {
                    try
                    {
                        var userinfo = a.User;
                        track.Add(new StandardTitle { username = userinfo.Username, description = a.Description, duration = a.Duration / 1000, genre = a.Genre, labelname = a.LabelName ?? string.Empty, title = a.Title, id = (long)i.Id });

                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                }

                pl.Add(new StandardPlaylist { description = i.Description ?? string.Empty, title = i.Title, provider = "soundcloud", tracks = track, id = i.Id.ToString() });
            }
            return pl;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Search.Model;
using SoundCloud.Api.Entities;

namespace Search.SoundCloud
{
    public class playlist : Loader
    {

        public playlist()
        {
            init().Wait();
        }

        /// <summary>
        /// Gets all playlists.
        /// </summary>
        /// <returns>The playlists.</returns>
        public async Task<IEnumerable<Playlist>> GetPlaylists()
        {
            IEnumerable<Playlist> list = await client.Users.GetPlaylistsAsync(user);
            return list;
        }

        /// <summary>
        /// Save all Playlist Data to Standard List
        /// </summary>
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

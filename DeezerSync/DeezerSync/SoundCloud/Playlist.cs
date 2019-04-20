using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeezerSync.Model;
using SoundCloud.Api.Entities;

namespace DeezerSync.SoundCloud
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
        public async Task SetStandardPlaylists()
        {
            var playlistdata = await GetPlaylists();

            foreach(var i in playlistdata){
                var trackinfo = i.Tracks;
                List<StandardTitle> track = new List<StandardTitle>();

                foreach(var a in trackinfo)
                {
                    try
                    {
                        var userinfo = a.User;
                        track.Add(new StandardTitle { username = userinfo.Username, description = a.Description, duration = a.Duration, genre = a.Genre, labelname = a.LabelName ?? string.Empty, title = a.Title });

                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                Program.Playlists.Add(new StandardPlaylist { description = i.Description ?? string.Empty, title = i.Title, provider = "soundcloud", tracks = track });
            }
        }
    }
}

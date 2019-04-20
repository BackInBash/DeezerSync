using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeezerSync.Deezer.API
{
    class Playlist
    {

        /// <summary>
        /// Crete a new Deezer Playlist
        /// </summary>
        /// <param name="name">Playlist Name</param>
        /// <param name="description">(optional) Playlist description</param>
        /// <returns></returns>
        public long CreatePlaylist(string name, string description = "")
        {
            Model.CreatePlaylist playlist = new Model.CreatePlaylist()
            {
                description = description,
                songs = false,
                status = 0,
                tags = new List<string>(),
                title = name
            };
            string json = JsonConvert.SerializeObject(playlist, Formatting.Indented);

            Login l = new Login();

            var result = JsonConvert.DeserializeObject<Model.CreatePlaylistResponse>(l.DeezerRequest("playlist.create", json));

            return result.results;
        }
    }
}

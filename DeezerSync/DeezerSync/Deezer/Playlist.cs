using DeezerSync.Deezer.API;
using DeezerSync.Deezer.API.Model;
using DeezerSync.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DeezerSync.Deezer
{
    class Playlist
    {
        protected Login l = null;
        public Playlist()
        {
            l = new Login();
        }

        /// <summary>
        /// Get a List of all Playlists
        /// </summary>
        /// <returns></returns>
        public List<string> GetPlaylists()
        {
            return null;
        }

        /// <summary>
        /// Return a List with all Track from a single playlist
        /// </summary>
        /// <param name="PlaylistID">ID of an existing Playlist</param>
        /// <returns></returns>
        public List<StandardTitle> GetAllTracksInPlaylist(long PlaylistID)
        {
            RequestPlaylistData playlist = new RequestPlaylistData()
            {
                 header = true,
                 lang = "de",
                 nb = 40,
                 playlist_id = PlaylistID.ToString(),
                 start = 0,
                 tab = 0,
                 tags = true
            };

            string json = JsonConvert.SerializeObject(playlist, Formatting.None);
            string jsonresult = l.DeezerRequest("deezer.pagePlaylist", json);
            Console.WriteLine(jsonresult);
            var result = (dynamic)null;

            try
            {
                result = DeezerSync.Deezer.API.Model.PlaylistDataModel.Welcome.FromJson(jsonresult);
            }
            catch (JsonSerializationException)
            {
                try
                {
                    result = JsonConvert.DeserializeObject<dynamic>(jsonresult);
                    throw new Exception("ERROR: " + result.error.VALID_TOKEN_REQUIRED);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            List<StandardTitle> titles = new List<StandardTitle>();

            foreach(var track in result.Results.Songs.Data)
            {
                titles.Add(new StandardTitle { title = track.SngTitle, description = string.Empty, duration = (int)track.Duration, genre = string.Empty, username = track.ArtName, labelname = string.Empty });
            }
            return titles;
        }

        /// <summary>
        /// Add multiple Songs to a Playlist
        /// </summary>
        /// <param name="PlaylistID">ID of an existing Playlist</param>
        /// <param name="TrackIDs">An array filled with TrackIDs</param>
        /// <returns></returns>
        public bool AddSongsToPlaylist(long PlaylistID, long[] TrackIDs)
        {
            List<List<long>> myList = new List<List<long>>();
            foreach (long l in TrackIDs)
            {
                myList.Add(new List<long> { l, 0 });
            }

            AddSongsToPlaylist songstoplaylist = new AddSongsToPlaylist()
            {
                playlist_id = PlaylistID.ToString(),
                offset = -1,
                songs = myList
            };

            string json = JsonConvert.SerializeObject(songstoplaylist, Formatting.None);

            var result = (dynamic)null;
            string jsonresult = l.DeezerRequest("playlist.addSongs", json);

            try
            {
                result = JsonConvert.DeserializeObject<CreatePlaylistResponse>(jsonresult);
            }
            catch (JsonSerializationException)
            {
                try
                {
                    result = JsonConvert.DeserializeObject<dynamic>(jsonresult);
                    throw new Exception("ERROR: " + result.error.VALID_TOKEN_REQUIRED);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return true;
        }

        /// <summary>
        /// Create a new Deezer Playlist
        /// </summary>
        /// <param name="name">Playlist Name</param>
        /// <param name="description">(optional) Playlist description</param>
        /// <returns></returns>
        public long CreatePlaylist(string name, string description = "")
        {
            CreatePlaylist playlist = new CreatePlaylist()
            {
                description = description,
                songs = false,
                status = 0,
                tags = new List<string>(),
                title = name
            };
            string json = JsonConvert.SerializeObject(playlist, Formatting.Indented);

            var result = (dynamic)null;
            string jsonresult = l.DeezerRequest("playlist.create", json);

            try
            {
                result = JsonConvert.DeserializeObject<CreatePlaylistResponse>(jsonresult);
            }
            catch (JsonSerializationException)
            {
                try
                {
                    result = JsonConvert.DeserializeObject<dynamic>(jsonresult);
                    throw new Exception("ERROR: "+result.error.VALID_TOKEN_REQUIRED);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return result.results;
        }
    }
}

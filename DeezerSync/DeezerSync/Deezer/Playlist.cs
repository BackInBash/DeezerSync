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
        protected static Login l = new Login();

        /// <summary>
        /// Get a List of all Playlists
        /// </summary>
        /// <returns></returns>
        public static List<StandardPlaylist> GetAllPlaylists()
        {
            RequestAllPlaylists playlists = new RequestAllPlaylists()
            {
                nb = 40,
                tab = "playlists",
                user_id = l.userid
            };

            string json = JsonConvert.SerializeObject(playlists, Formatting.None);
            string jsonresult = l.DeezerRequest("deezer.pageProfile", json);

            var result = (dynamic)null;

            try
            {
                result = DeezerSync.Deezer.API.Model.AllPlaylists.Request.FromJson(jsonresult);
            }
            catch (JsonSerializationException ex)
            {
                try
                {
                    result = JsonConvert.DeserializeObject<dynamic>(jsonresult);
                    throw new Exception("ERROR: " + result.error.VALID_TOKEN_REQUIRED);
                }
                catch (Exception)
                {
                    throw new Exception(ex.Message);
                }
            }

            List<StandardPlaylist> playlist = new List<StandardPlaylist>();

            foreach(var i in result.Results.Tab.Playlists.Data)
            {
                playlist.Add(new StandardPlaylist { description = string.Empty, provider = "deezer", title = i.Title, tracks = GetAllTracksInPlaylist(i.PlaylistId), id = i.PlaylistId });
            }

            return playlist;
        }

        /// <summary>
        /// Return a List with all Track from a single playlist
        /// </summary>
        /// <param name="PlaylistID">ID of an existing Playlist</param>
        /// <returns></returns>
        private static List<StandardTitle> GetAllTracksInPlaylist(string PlaylistID)
        {
            RequestPlaylistData playlist = new RequestPlaylistData()
            {
                 header = true,
                 lang = "de",
                 nb = 40,
                 playlist_id = PlaylistID,
                 start = 0,
                 tab = 0,
                 tags = true
            };

            string json = JsonConvert.SerializeObject(playlist, Formatting.None);
            string jsonresult = l.DeezerRequest("deezer.pagePlaylist", json);

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
                    throw new Exception("ERROR: " + result.Error.ToString());
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            List<StandardTitle> titles = new List<StandardTitle>();

            foreach(var track in result.Results.Songs.Data)
            {
                titles.Add(new StandardTitle { title = track.SngTitle, description = string.Empty, duration = (int)track.Duration, genre = string.Empty, username = track.ArtName, labelname = string.Empty, id = (long)track.SngId });
            }
            return titles;
        }

        /// <summary>
        /// Add multiple Songs to a Playlist
        /// </summary>
        /// <param name="PlaylistID">ID of an existing Playlist</param>
        /// <param name="TrackIDs">An array filled with TrackIDs</param>
        /// <returns></returns>
        public static bool AddSongsToPlaylist(string PlaylistID, List<long> TrackIDs)
        {

            List<AddSongsToPlaylist> plst = new List<AddSongsToPlaylist>();
            foreach (long l in TrackIDs)
            {
                List<List<long>> myList = new List<List<long>>
                {
                    new List<long> { l, 0 }
                };
                plst.Add(new AddSongsToPlaylist()
                {
                    playlist_id = PlaylistID,
                    offset = -1,
                    songs = myList
                }
                );
            }

            foreach (var i in plst)
            {
                var result = (dynamic)null;
                string jsonresult = string.Empty;
                string json = JsonConvert.SerializeObject(i, Formatting.None);

                jsonresult = l.DeezerRequest("playlist.addSongs", json);

                try
                {
                    result = JsonConvert.DeserializeObject<CreatePlaylistResponse>(jsonresult);
                }
                catch (JsonSerializationException ex)
                {
                    try
                    {
                        result = JsonConvert.DeserializeObject<dynamic>(jsonresult);
                        Console.WriteLine("ERROR: " + result.error);
                    }
                    catch (Exception)
                    {
                        throw new Exception(ex.Message);
                    }
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
        public static long CreatePlaylist(string name, string description = "")
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

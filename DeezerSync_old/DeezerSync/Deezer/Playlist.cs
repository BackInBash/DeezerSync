using DeezerSync.Deezer.API;
using DeezerSync.Deezer.API.Model;
using DeezerSync.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeezerSync.Deezer
{
    class Playlist
    {
        protected static Login l = new Login();
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Get a List of all Playlists
        /// </summary>
        /// <returns></returns>
        public static async Task<List<StandardPlaylist>> GetAllPlaylistsasync()
        {

            RequestAllPlaylists playlists = new RequestAllPlaylists()
            {
                nb = 40,
                tab = "playlists",
                user_id = l.userid
            };

            string json = JsonConvert.SerializeObject(playlists, Formatting.None);

            string jsonresult = await l.DeezerRequestasync("deezer.pageProfile", json);

            var result = (dynamic)null;

            try
            {
                result = DeezerSync.Deezer.API.Model.AllPlaylists.Request.FromJson(jsonresult);
            }
            catch (JsonSerializationException ex)
            {
                logger.Warn(ex);
                try
                {
                    result = JsonConvert.DeserializeObject<dynamic>(jsonresult);
                    logger.Error(result);
                    throw new Exception("ERROR: " + result.error.VALID_TOKEN_REQUIRED);
                }
                catch (Exception)
                {
                    throw new Exception(ex.Message);
                }
            }

            List<StandardPlaylist> playlist = new List<StandardPlaylist>();

            foreach (var i in result.Results.Tab.Playlists.Data)
            {
                playlist.Add(new StandardPlaylist { description = string.Empty, provider = "deezer", title = i.Title, tracks = await GetAllTracksInPlaylist(i.PlaylistId), id = i.PlaylistId });
            }

            return playlist;
        }

        /// <summary>
        /// Return a List with all Track from a single playlist
        /// </summary>
        /// <param name="PlaylistID">ID of an existing Playlist</param>
        /// <returns></returns>
        private static async Task<List<StandardTitle>> GetAllTracksInPlaylist(string PlaylistID)
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
            string jsonresult = await l.DeezerRequestasync("deezer.pagePlaylist", json);

            var result = (dynamic)null;

            try
            {
                result = DeezerSync.Deezer.API.Model.PlaylistDataModel.Welcome.FromJson(jsonresult);
            }
            catch (JsonSerializationException e)
            {
                logger.Warn(e);
                try
                {
                    result = JsonConvert.DeserializeObject<dynamic>(jsonresult);
                    throw new Exception("ERROR: " + result.Error.ToString());
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    throw new Exception(ex.Message);
                }
            }

            List<StandardTitle> titles = new List<StandardTitle>();

            foreach (var track in result.Results.Songs.Data)
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
        public static async Task<bool> AddSongsToPlaylistasync(string PlaylistID, List<long> TrackIDs)
        {

            List<AddSongsToPlaylist> plst = new List<AddSongsToPlaylist>();
            var task1 = Task.Run(() =>
            {
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
            });
            task1.Wait();

            var task2 = Task.Run(async () =>
            {
                foreach (var i in plst)
                {
                    var result = (dynamic)null;
                    string jsonresult = string.Empty;
                    string json = JsonConvert.SerializeObject(i, Formatting.None);

                    jsonresult = await l.DeezerRequestasync("playlist.addSongs", json);

                    try
                    {
                        result = JsonConvert.DeserializeObject<CreatePlaylistResponse>(jsonresult);
                    }
                    catch (JsonSerializationException ex)
                    {
                        logger.Error(ex);
                        try
                        {
                            result = JsonConvert.DeserializeObject<dynamic>(jsonresult);
                            logger.Warn(result.error);
                        }
                        catch (Exception)
                        {
                            throw new Exception(ex.Message);
                        }
                    }
                }
            });
            task2.Wait();
            return true;
        }

        /// <summary>
        /// Create a new Deezer Playlist
        /// </summary>
        /// <param name="name">Playlist Name</param>
        /// <param name="description">(optional) Playlist description</param>
        /// <returns></returns>
        public static async Task<long> CreatePlaylistasync(string name, string description = "")
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
            string jsonresult = await l.DeezerRequestasync("playlist.create", json);

            try
            {
                result = JsonConvert.DeserializeObject<CreatePlaylistResponse>(jsonresult);
            }
            catch (JsonSerializationException e)
            {
                logger.Warn(e);
                try
                {
                    result = JsonConvert.DeserializeObject<dynamic>(jsonresult);
                    logger.Error(result.error.VALID_TOKEN_REQUIRED);
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

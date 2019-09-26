using DeezerSync.Core.Models.API;
using DeezerSync.Models;
using DeezerSync.Models.API;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UserDataModel = DeezerSync.Models.API.UserDataModel;

namespace DeezerSync.DeezerAPI
{
    public class Private
    {
        public Private()
        {
            GetDeezerAPILogin().Wait();
        }

        public Private(string secret)
        {
            Private.secret = secret;
            GetDeezerAPILogin().Wait();
        }

        // User Agent: Chrome Version 77.0.3865.90
        private readonly string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/77.0.3865.90 Safari/537.36";
        private readonly string apiurl = "https://www.deezer.com/ajax/gw-light.php";
        private readonly string actionurl = "https://www.deezer.com/ajax/action.php";
        private readonly string api_version = "api_version=1.0";
        private readonly string api_input = "input=3";
        private readonly string api_token = "api_token=";
        private readonly string method = "method=";
        private readonly string cid = "cid=";

        internal string userid;
        private string apiKey;
        private string csrfsid;
        public static string secret = "";


        internal async Task<string> Requestasync(string Dmethod, string payload = null)
        {

            var request = (dynamic)null;
            if (string.IsNullOrEmpty(apiKey))
            {
                request = (WebRequest)WebRequest.Create(apiurl + "?" + api_version + "&" + api_token + "&" + api_input + "&" + method + Dmethod + "&" + cid + GenCid());
            }
            else
            {
                request = (WebRequest)WebRequest.Create(apiurl + "?" + api_version + "&" + apiKey + "&" + api_input + "&" + method + Dmethod + "&" + cid + GenCid());
            }

            request.Method = "POST";

            if (!string.IsNullOrEmpty(payload))
            {
                request.ContentType = "application/json; charset=utf-8";
            }
 
            request.UserAgent = this.UserAgent;
            request.Headers["User-Agent"] = this.UserAgent;
            request.Headers["Cache-Control"] = "max-age=0";
            request.Headers["accept-language"] = "en-US,en;q=0.9,en-US;q=0.8,en;q=0.7";
            request.Headers["accept-charset"] = "utf-8,ISO-8859-1;q=0.8,*;q=0.7";
            request.Headers["cookie"] = "arl=" + secret + "; sid=" + csrfsid;

            var content = string.Empty;
            request.CookieContainer = new CookieContainer();

            try
            {
                if (!string.IsNullOrEmpty(payload))
                {
                    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        streamWriter.Write(payload);
                    }
                }

                using (var response = await request.GetResponseAsync().ConfigureAwait(false))
                {
                    if (string.IsNullOrEmpty(csrfsid))
                    {
                        foreach (Cookie cook in response.Cookies)
                        {
                            if (cook.Name.Equals("sid"))
                            {
                                csrfsid = cook.Value;
                            }
                        }
                    }
                    using (var stream = response.GetResponseStream())
                    {
                        using (var sr = new StreamReader(stream))
                        {
                            content = sr.ReadToEnd();
                        }
                    }
                }
            }
            catch (WebException we)
            {
                // Wait before the next request
                Thread.Sleep(1000);
                // Converting Data
                int errorCode = (int)((HttpWebResponse)we.Response).StatusCode;
                int startsWith = 5;
                // Retry HTTP Request on HTTPError 5xx
                if (errorCode.ToString().StartsWith(startsWith.ToString()))
                {
                    throw new WebException("ERROR during HTTP Request: " + we.Message + " Error Code: " + (int)((HttpWebResponse)we.Response).StatusCode);
                }
                else
                {
                    throw new WebException("ERROR during HTTP Request: " + we.Message + " Error Code: " + (int)((HttpWebResponse)we.Response).StatusCode);
                }
            }
            return content;
        }

        private int GenCid()
        {
            Random rnd = new Random();
            return rnd.Next(100000000, 999999999);
        }

        /// <summary>
        /// Get Deezer API Creds
        /// </summary>
        /// <returns></returns>
        private async Task GetDeezerAPILogin()
        {
            string webresult = await Requestasync("deezer.getUserData");
            var welcome = (dynamic)null;
            try
            {
                welcome = UserDataModel.FromJson(webresult);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            // Check for Valid User ID
            if (welcome.Results.User.UserId > 0)
            {
                // Check for Valid API Key
                if (!string.IsNullOrEmpty(welcome.Results.CheckForm) && welcome.Results.User.UserId != 0)
                {
                    // Write API Key to Var
                    apiKey = api_token + welcome.Results.CheckForm;
                    // Write UserID to Var
                    userid = welcome.Results.User.UserId.ToString();
                }
                else
                {
                    throw new Exception("Wrong User Information");
                }
            }
            else
            {
                throw new Exception("Cannot get Deezer API Key");
            }
        }

        /// <summary>
        /// Execute Search Query on Deezer Private API
        /// </summary>
        /// <param name="query">Deezer Private Search Query</param>
        /// <returns></returns>
        public async Task<List<StandardTitle>> SearchQuery(string query)
        {
            SearchResult searchResult = new SearchResult();
            List<StandardTitle> Track = new List<StandardTitle>();
            var request = await Requestasync("deezer.pageSearch", "{\"query\":\"" + query + "\",\"start\":0,\"nb\":40,\"suggest\":true,\"artist_suggest\":true,\"top_tracks\":true}");
            searchResult = JsonConvert.DeserializeObject<SearchResult>(request);

            foreach(TrackDatum t in searchResult.Results.Track.Data)
            {
                try
                {
                    Track.Add(new StandardTitle { id = (long)t.SngId, title = t.SngTitle, duration = (int)t.Duration, genre = string.Empty, description = string.Empty, username = t.ArtName.ToString(), labelname = t.Version });
                }
                catch(Exception) { }
            }

            return Track;
        }


        /// <summary>
        /// Get a List of all Playlists
        /// </summary>
        /// <returns></returns>
        public async Task<List<StandardPlaylist>> GetAllPlaylistsasync()
        {

            RequestAllPlaylists playlists = new RequestAllPlaylists()
            {
                nb = 40,
                tab = "playlists",
                user_id = userid
            };

            string json = JsonConvert.SerializeObject(playlists, Formatting.None);

            string jsonresult = await Requestasync("deezer.pageProfile", json);

            var result = (dynamic)null;

            try
            {
                result = AllPlaylists.Request.FromJson(jsonresult);
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
        private async Task<List<StandardTitle>> GetAllTracksInPlaylist(string PlaylistID)
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
            string jsonresult = await Requestasync("deezer.pagePlaylist", json);

            var result = (dynamic)null;

            try
            {
                result = PlaylistDataModel.Welcome.FromJson(jsonresult);
            }
            catch (JsonSerializationException e)
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
        /// <param name="TrackIDs">An List filled with TrackIDs</param>
        /// <returns></returns>
        public async Task<bool> AddSongsToPlaylistasync(string PlaylistID, List<long> TrackIDs)
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

                    jsonresult = await Requestasync("playlist.addSongs", json);

                    try
                    {
                        result = JsonConvert.DeserializeObject<CreatePlaylistResponse>(jsonresult);
                    }
                    catch (JsonSerializationException ex)
                    {
                        try
                        {
                            result = JsonConvert.DeserializeObject<dynamic>(jsonresult);
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
        public async Task<long> CreatePlaylistasync(string name, string description = "")
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
            string jsonresult = await Requestasync("playlist.create", json);

            try
            {
                result = JsonConvert.DeserializeObject<CreatePlaylistResponse>(jsonresult);
            }
            catch (JsonSerializationException e)
            {
                try
                {
                    result = JsonConvert.DeserializeObject<dynamic>(jsonresult);
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

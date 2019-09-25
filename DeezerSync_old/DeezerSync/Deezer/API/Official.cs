using DeezerSync.Deezer.API.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace DeezerSync.Deezer.API
{
    class SearchRequest
    {
        public string artist;
        public string track;
    }
    class Official
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private const string Official_api = "https://api.deezer.com/search?q=";

        private string artist;
        private string track;
        private int duration;

        private static readonly HttpClient client = new HttpClient();

        public Official(string artist, string track, int duration)
        {
            this.artist = artist;
            this.track = track;
            this.duration = duration;
        }

        private bool checkDuration(long dur)
        {
            if (this.duration == (dur - 1))
            {
                return true;
            }
            if (this.duration == dur)
            {
                return true;
            }
            if (this.duration == (dur + 1))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Prepare Search Query String
        /// </summary>
        /// <param name="i">Query Stage</param>
        /// <returns>SearchRequest Object contains Track & Artist</returns>
        private SearchRequest GenerateSearchRequest(int i)
        {
            SearchRequest s = new SearchRequest();

            switch (i)
            {
                case 1:
                    // Vanilla Search for Artist & Title
                    if (this.artist.Contains("&"))
                    {
                        s.artist = Regex.Replace(this.artist, "&", "", RegexOptions.IgnoreCase).Trim();
                    }
                    else
                    {
                        s.artist = this.artist;
                    }
                    if (this.track.Contains("&"))
                    {
                        s.track = Regex.Replace(this.track, "&", "", RegexOptions.IgnoreCase).Trim();
                    }
                    else
                    {
                        s.track = this.track;
                    }
                    logger.Trace("GenerateSearchRequest: Step 1: Artist: " + artist + " Track: " + track);
                    break;

                case 2:
                    // Remove Artist in Track & Replace Label
                    if (this.artist.Contains("&"))
                    {
                        s.artist = Regex.Replace(this.artist, "&", "", RegexOptions.IgnoreCase).Trim();
                    }
                    else
                    {
                        s.artist = this.artist;
                    }

                    string tmptrack;

                    if (this.track.Contains("&"))
                    {
                        tmptrack = Regex.Replace(this.track, "&", "", RegexOptions.IgnoreCase).Trim();
                    }
                    else
                    {
                        tmptrack = this.track;
                    }

                    if (tmptrack.Contains("-"))
                    {
                        string[] split = tmptrack.Split('-', 2, StringSplitOptions.RemoveEmptyEntries);
                        if (s.artist.Contains(split[0]))
                        {
                            s.track = split[1];
                            s.track = Regex.Replace(s.track, @"\[.*\]", "", RegexOptions.IgnoreCase).Trim();
                        }
                        else
                        {
                            if (split[0].Contains("-"))
                            {
                                s.artist = split[0].Split('-', 2, StringSplitOptions.RemoveEmptyEntries)[0];
                            }
                            if (split[0].Contains("&"))
                            {
                                s.artist = split[0].Split('&', 2, StringSplitOptions.RemoveEmptyEntries)[0];
                            }
                            else
                            {
                                s.artist = split[0];
                            }
                            s.track = split[1];
                            s.track = Regex.Replace(s.track, @"\[.*\]", "", RegexOptions.IgnoreCase).Trim();
                        }
                        logger.Trace("GenerateSearchRequest: Step 2: Artist: " + artist + " Track: " + track);
                    }
                    else
                    {
                        s.track = tmptrack;
                        s.track = Regex.Replace(s.track, @"\[.*\]", "", RegexOptions.IgnoreCase).Trim();
                        logger.Trace("GenerateSearchRequest: Step 2: Artist: " + artist + " Track: " + track);
                    }
                    break;

                case 3:
                    // Remove Artist & Modify Track
                    s.artist = string.Empty;
                    string trackex = this.track;
                    if (this.track.Contains("-"))
                    {
                        trackex = this.track.Split('-', 2, StringSplitOptions.RemoveEmptyEntries)[1];
                    }
                    else
                    {
                        trackex = this.track;
                    }

                    if (Regex.Match(this.track, @"Remix", RegexOptions.IgnoreCase).Success)
                    {
                        Match m = Regex.Match(this.track, @"(\(|\[).*Remix*(\)|\])", RegexOptions.IgnoreCase);
                        if (m.Success)
                        {
                            // Split track replace remix entry with the clean remix artist
                            string regex = Regex.Replace(trackex, @"(\(|\[).*Remix*(\)|\])", m.Value, RegexOptions.IgnoreCase).Trim();

                            // Set Remix Artist as new Artist
                            s.artist = Regex.Replace(m.Value, @"[\(*\)|\[*\]]", "", RegexOptions.IgnoreCase).Trim();
                            s.artist = Regex.Replace(s.artist, @"Remix", "", RegexOptions.IgnoreCase).Trim();

                            // Remove remaining [] ()
                            s.track = Regex.Replace(regex, @"(\(.*\)|\[.*\])", "", RegexOptions.IgnoreCase).Trim();
                            s.track = Regex.Replace(s.track, @"&", "", RegexOptions.IgnoreCase).Trim();
                        }
                        else
                        {
                            s.track = Regex.Replace(this.track, @"(\(.*\)|\[.*\])", "", RegexOptions.IgnoreCase).Trim();
                            s.track = Regex.Replace(s.track, @"&", "", RegexOptions.IgnoreCase).Trim();
                        }
                    }
                    else
                    {
                        if (this.track.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[0].Contains(this.artist))
                        {
                            s.track = Regex.Replace(this.track, @"(\(.*\)|\[.*\])", "", RegexOptions.IgnoreCase).Trim();
                            s.track = Regex.Replace(s.track, @"&", "", RegexOptions.IgnoreCase).Trim();
                        }
                        else
                        {
                            // Prevent Lable in Artist
                            if (this.track.Contains("-"))
                            {
                                s.track = Regex.Replace(this.track, @"(\(.*\)|\[.*\])", "", RegexOptions.IgnoreCase).Trim();
                            }
                            else
                            {
                                s.track = this.artist + " " + Regex.Replace(this.track, @"(\(.*\)|\[.*\])", "", RegexOptions.IgnoreCase).Trim();
                            }

                            s.track = Regex.Replace(s.track, @"&", "", RegexOptions.IgnoreCase).Trim();
                        }
                    }
                    if (s.artist.Split(' ').Length > 2)
                    {
                        // Reset if multiple artists are Detected
                        s.artist = this.artist;
                    }
                    logger.Trace("GenerateSearchRequest: Step 3: Artist " + artist + " Track: " + track);
                    break;
            }
            return s;
        }
        /// <summary>
        /// Deserialize Deezer API Result with given Query informations
        /// </summary>
        /// <param name="i">Query Stage</param>
        /// <param name="artist">The Artist to search for</param>
        /// <param name="track">The track name to search for</param>
        /// <returns>A Object contains the deserialized search Query</returns>
        private async Task<ResultSearch.Search> GetSearchResult(int i, string artist, string track)
        {
            var data = (dynamic)null;

            switch (i)
            {
                case 1:
                    data = JsonConvert.DeserializeObject<ResultSearch.Search>((await Request(3, artist, track)));
                    if (data.Total == 0)
                    {
                        data = JsonConvert.DeserializeObject<ResultSearch.Search>((await Request(1, string.Join("-", artist, track))));
                    }

                    break;
                case 2:
                    data = JsonConvert.DeserializeObject<ResultSearch.Search>((await Request(3, artist, track)));
                    if (data.Total == 0)
                    {
                        data = JsonConvert.DeserializeObject<ResultSearch.Search>((await Request(3, artist, track)));
                    }

                    break;
                case 3:
                    if (string.IsNullOrEmpty(artist))
                    {
                        data = JsonConvert.DeserializeObject<ResultSearch.Search>((await Request(lvl: 1, Track: track)));
                    }
                    else
                    {
                        data = JsonConvert.DeserializeObject<ResultSearch.Search>((await Request(lvl: 3, Track: track, Artist: artist)));
                        if (data.Total == 0)
                        {
                            data = JsonConvert.DeserializeObject<ResultSearch.Search>((await Request(1, string.Join("-", artist, track))));
                        }
                    }

                    break;
            }
            return data;
        }

        /// <summary>
        /// Starting the Search for a Track
        /// </summary>
        /// <returns>Deezer TrackID</returns>
        public async Task<long> finder()
        {
            for (int i = 1; i < 4; i++)
            {
                var search = GenerateSearchRequest(i);
                string artist = search.artist;
                string track = search.track;
                for (int c = 1; c < 4; c++)
                {
                    var data = await GetSearchResult(c, artist, track);

                    logger.Debug("Search: Artist: " + artist + " Track: " + track);

                    if (data.Data != null)
                    {
                        switch (c)
                        {
                            case 1:
                                foreach (var found in data.Data)
                                {
                                    if ((found.Artist.Name.Contains(artist) && found.Title.Equals(track)) || checkDuration(found.Duration.Value))
                                    {
                                        logger.Debug("GenerateSearchRequest: Stage 1 FOUND " + data.Total + ": Artist: " + found.Artist.Name + " Track: " + found.Title + " Link: " + found.Link.AbsoluteUri + "\n");
                                        return (long)found.Id;
                                    }

                                    if ((found.Artist.Name.Contains(this.artist) && found.Title.Equals(this.track)) || checkDuration(found.Duration.Value))
                                    {
                                        logger.Trace("GenerateSearchRequest: Stage 1 FOUND " + data.Total + ": Artist: " + found.Artist.Name + " Track: " + found.Title + " Link: " + found.Link.AbsoluteUri + "\n");
                                        return (long)found.Id;
                                    }
                                }
                                break;

                            case 2:
                                foreach (var found in data.Data)
                                {
                                    if (data.Data.Count < 5 && checkDuration(found.Duration.Value))
                                    {
                                        logger.Debug("GenerateSearchRequest: Stage 2 FOUND " + data.Total + ": Artist: " + found.Artist.Name + " Track: " + found.Title + " Link: " + found.Link.AbsoluteUri + "\n");
                                        return (long)found.Id;
                                    }
                                    else
                                    {
                                        if ((found.Artist.Name.Contains(artist) && found.Title.Contains(track)) || checkDuration(found.Duration.Value))
                                        {
                                            logger.Debug("GenerateSearchRequest: Stage 2 FOUND " + data.Total + ": Artist: " + found.Artist.Name + " Track: " + found.Title + " Link: " + found.Link.AbsoluteUri + "\n");
                                            return (long)found.Id;
                                        }
                                    }

                                }
                                break;
                            case 3:
                                foreach (var found in data.Data)
                                {
                                    if ((found.Artist.Name.Contains(artist) && found.Title.Contains(track)) || checkDuration(found.Duration.Value))
                                    {
                                        logger.Debug("GenerateSearchRequest: Stage 3 FOUND " + data.Total + ": Artist: " + found.Artist.Name + " Track: " + found.Title + " Link: " + found.Link.AbsoluteUri + "\n");
                                        return (long)found.Id;
                                    }

                                    if ((found.Artist.Name.Contains(this.artist) && found.Title.Contains(this.track)) || checkDuration(found.Duration.Value))
                                    {
                                        logger.Debug("GenerateSearchRequest: Stage 3 FOUND " + data.Total + ": Artist: " + found.Artist.Name + " Track: " + found.Title + " Link: " + found.Link.AbsoluteUri + "\n");
                                        return (long)found.Id;
                                    }
                                }
                                break;
                        }
                    }
                    else
                    {
                        logger.Debug("GenerateSearchRequest: Empty Dataset\n");
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// Start Search Querys on Official API
        /// </summary>
        /// <param name="lvl">Searchlevel 1,2,3</param>
        /// <param name="track">Trackname</param>
        /// <param name="artist">Artistname</param>
        /// <param name="duration">Duration</param>
        /// <returns></returns>
        private async Task<string> Request(int lvl, string Artist = null, string Track = null, int duration = 0)
        {
            try
            {
                switch (lvl)
                {
                    case 5:
                        // Artist + Track + Duration
                        //Console.WriteLine(Official_api + "artist:" + "\"" + Artist + "\" " + "track:" + "\"" + Track + "\" " + "dur_min:" + (duration - 1).ToString() + " dur_max:" + (duration + 1).ToString());
                        return await client.GetStringAsync(Official_api + "artist:" + "\"" + Artist + "\" " + "track:" + "\"" + Track + "\" " + "dur_min:" + (duration - 1).ToString() + " dur_max:" + (duration + 1).ToString());

                    case 4:
                        // Track + Duration
                        //Console.WriteLine(Official_api + Track + " " + "dur_min:" + (duration - 1).ToString() + " dur_max:" + (duration + 1).ToString());
                        return await client.GetStringAsync(Official_api + Track + " " + "dur_min:" + (duration - 1).ToString() + " dur_max:" + (duration + 1).ToString());

                    case 3:
                        // Artist + Track
                        //Console.WriteLine(Official_api + "artist:" + "\"" + Artist + "\" " + "track:" + "\"" + Track + "\"");
                        string res = await client.GetStringAsync(Official_api + "artist:" + "\"" + Artist + "\" " + "track:" + "\"" + Track + "\"");
                        if (res.Equals("{\"error\":{\"type\":\"Exception\",\"message\":\"Quota limit exceeded\",\"code\":4}}"))
                        {
                            Thread.Sleep(2000);
                            //Console.WriteLine("Retry: "+Official_api + "artist:" + "\"" + Artist + "\" " + "track:" + "\"" + Track + "\"");
                            res = await Request(3, Artist, Track);
                        }
                        return res;

                    case 2:
                        // Artist
                        //Console.WriteLine(Official_api + "artist:" + "\"" + Artist + "\" ");
                        return await client.GetStringAsync(Official_api + "artist:" + "\"" + Artist + "\" ");

                    case 1:
                        // Track
                        //Console.WriteLine(Official_api + Track);
                        string result = await client.GetStringAsync(Official_api + Track);
                        if (result.Equals("{\"error\":{\"type\":\"Exception\",\"message\":\"Quota limit exceeded\",\"code\":4}}"))
                        {
                            Thread.Sleep(2000);
                            //Console.WriteLine("Retry: "+Official_api + Track);
                            result = await Request(1, Track);
                        }
                        return result;

                    default:
                        throw new ArgumentOutOfRangeException("Option out of scope");
                }
            }
            catch (HttpRequestException ex)
            {
                logger.Error(ex);
                return "{\"data\": [],\"total\": 0}";
            }
            catch (Exception e)
            {
                logger.Error(e);
                return "{\"data\": [],\"total\": 0}";
            }
        }
    }
}

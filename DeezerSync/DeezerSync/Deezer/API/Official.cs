using DeezerSync.Deezer.API.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DeezerSync.Deezer.API
{
    class Official
    {
        private readonly string Official_api = "https://api.deezer.com/search?q=";

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

        public long finder()
        {
            bool duration = false;
            long id = 0;

            for (int i = 1; i < 4; i++)
            {
                string artist = null;
                string track = null;

                switch (i)
                {
                    case 1:
                        // Vanilla Search for Artist & Title
                        if (this.artist.Contains("&"))
                        {
                            artist = Regex.Replace(this.artist, "&", "", RegexOptions.IgnoreCase).Trim();
                        }
                        else
                        {
                            artist = this.artist;
                        }
                        if (this.track.Contains("&"))
                        {
                            track = Regex.Replace(this.track, "&", "", RegexOptions.IgnoreCase).Trim();
                        }
                        else
                        {
                            track = this.track;
                        }
                        //Console.WriteLine("Step 1: Artist: "+artist+ " Track: "+track);
                        break;

                    case 2:
                        // Remove Artist in Track & Replace Label
                        if (this.artist.Contains("&"))
                        {
                            artist = Regex.Replace(this.artist, "&", "", RegexOptions.IgnoreCase).Trim();
                        }
                        else
                        {
                            artist = this.artist;
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
                            if (artist.Contains(split[0]))
                            {
                                track = split[1];
                            }
                            else
                            {
                                if (split[0].Contains("-"))
                                {
                                    artist = split[0].Split('-', 2, StringSplitOptions.RemoveEmptyEntries)[0];
                                }
                                if (split[0].Contains("&"))
                                {
                                    artist = split[0].Split('&', 2, StringSplitOptions.RemoveEmptyEntries)[0];
                                }
                                else
                                {
                                    artist = split[0];
                                }
                                track = split[1];
                            }
                            //Console.WriteLine("Step 2: Artist: " + artist + " Track: " + track);
                        }
                        else
                        {
                            track = tmptrack;
                            //Console.WriteLine("Step 2: Artist: "+artist+ " Track: "+track);
                        }
                        break;

                    case 3:
                        // Remove Artist & Modify Track
                        artist = string.Empty;
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

                                artist = Regex.Replace(m.Value, @"[\(*\)|\[*\]]", "", RegexOptions.IgnoreCase).Trim();
                                artist = Regex.Replace(artist, @"Remix", "", RegexOptions.IgnoreCase).Trim();
                                // Set Remix Artist as new Artist
                                this.artist = artist;

                                // Remove remaining [] ()
                                track = Regex.Replace(regex, @"(\(.*\)|\[.*\])", "", RegexOptions.IgnoreCase).Trim();
                                track = Regex.Replace(track, @"&", "", RegexOptions.IgnoreCase).Trim();
                            }
                            else
                            {
                                track = Regex.Replace(this.track, @"(\(.*\)|\[.*\])", "", RegexOptions.IgnoreCase).Trim();
                                track = Regex.Replace(track, @"&", "", RegexOptions.IgnoreCase).Trim();
                            }
                        }
                        else
                        {
                            if (this.track.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[0].Contains(this.artist))
                            {
                                track = Regex.Replace(this.track, @"(\(.*\)|\[.*\])", "", RegexOptions.IgnoreCase).Trim();
                                track = Regex.Replace(track, @"&", "", RegexOptions.IgnoreCase).Trim();
                            }
                            else
                            {
                                track = this.artist + " " + Regex.Replace(this.track, @"(\(.*\)|\[.*\])", "", RegexOptions.IgnoreCase).Trim();
                                track = Regex.Replace(track, @"&", "", RegexOptions.IgnoreCase).Trim();
                            }
                        }
                        //Console.WriteLine("Step 3: Artist "+artist+" Track: " + track);
                        break;

                }
                //Console.WriteLine("");

                var data = (dynamic)null;

                switch (i)
                {
                    case 1:
                        data = JsonConvert.DeserializeObject<ResultSearch.Search>((Request(3, artist, track).Result));
                        if (data.Total == 0)
                        {
                            data = JsonConvert.DeserializeObject<ResultSearch.Search>((Request(1, track).Result));
                        }
                        if (data.Data != null)
                        {
                            foreach (var found in data.Data)
                            {
                                if (found.Artist.Name.Contains(artist) && found.Title.Contains(track))
                                {
                                    Console.WriteLine("FOUND "+ data.Total + ": Artist: " + artist + " Track: " + track + " Link: " + found.Link.AbsoluteUri);
                                    return (long)found.Id;
                                }

                                if (found.Artist.Name.Contains(this.artist) && found.Title.Contains(this.track))
                                {
                                    Console.WriteLine("FOUND " + data.Total + ": Artist: " + artist + " Track: " + track + " Link: " + found.Link.AbsoluteUri);
                                    return (long)found.Id;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Empty Dataset");
                        }
                        break;
                    case 2:
                        data = JsonConvert.DeserializeObject<ResultSearch.Search>((Request(3, artist, track).Result));
                        if (data.Total == 0)
                        {
                            data = JsonConvert.DeserializeObject<ResultSearch.Search>((Request(3, artist, Regex.Replace(track, @"(\(.*\)|\[.*\])", "", RegexOptions.IgnoreCase).Trim()).Result));
                        }
                        if (data.Data != null)
                        {
                            foreach (var found in data.Data)
                            {
                                if (found.Artist.Name.Contains(artist) && found.Title.Contains(track))
                                {
                                    Console.WriteLine("FOUND "+ data.Total + ": Artist: " + artist + " Track: " + track + " Link: " + found.Link.AbsoluteUri);
                                    return (long)found.Id;
                                }

                                if (found.Artist.Name.Contains(this.artist) && found.Title.Contains(this.track))
                                {
                                    Console.WriteLine("FOUND " + data.Total + ": Artist: " + artist + " Track: " + track + " Link: " + found.Link.AbsoluteUri);
                                    return (long)found.Id;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Empty Dataset");
                        }
                        break;
                    case 3:
                        if (string.IsNullOrEmpty(artist))
                        {
                            data = JsonConvert.DeserializeObject<ResultSearch.Search>((Request(lvl: 1, Track: track).Result));
                        } 
                        else
                        {
                            data = JsonConvert.DeserializeObject<ResultSearch.Search>((Request(lvl: 3, Track: track, Artist: artist).Result));
                        }
                        if (data.Data != null)
                        {
                            foreach (var found in data.Data)
                            {
                                if (found.Title.Contains(track) && found.Artist.Name.Contains(track) || found.Artist.Name.Contains(this.artist))
                                {
                                    Console.WriteLine("FOUND " + data.Total + ": Artist: " + artist + " Track: " + track + " Link: " + found.Link.AbsoluteUri);
                                    return (long)found.Id;
                                }

                                if (found.Title.Contains(this.track) && found.Artist.Name.Contains(this.track) || found.Artist.Name.Contains(this.artist))
                                {
                                    Console.WriteLine("FOUND " + data.Total + ": Artist: " + artist + " Track: " + track + " Link: " + found.Link.AbsoluteUri);
                                    return (long)found.Id;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Empty Dataset");
                        }
                        break;
                }

            }
            return id;
        }

        /// <summary>
        /// Start Search Querys on Official API
        /// </summary>
        /// <param name="lvl">Searchlevel 1,2,3</param>
        /// <param name="track">Trackname</param>
        /// <param name="artist">Artistname</param>
        /// <param name="duration">Duration</param>
        /// <returns></returns>
        public async Task<string> Request(int lvl, string Artist = null, string Track = null, int duration = 0)
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
                        if (res.Equals("{\"data\":[],\"total\":0}"))
                        {
                            Console.WriteLine(Official_api + "artist:" + "\"" + Artist + "\" " + "track:" + "\"" + Track + "\"");
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
                        if (result.Equals("{\"data\":[],\"total\":0}"))
                        {
                            Console.WriteLine(Official_api + Track);
                        }
                        return result;

                    default:
                        throw new ArgumentOutOfRangeException("Option out of scope");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
                //throw new HttpRequestException(ex.Message);
                return "{\"data\": [],\"total\": 0}";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "{\"data\": [],\"total\": 0}";
            }
        }
    }
}

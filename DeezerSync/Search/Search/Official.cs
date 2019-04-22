using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Search
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

            for (int i = 0; i < 5; i++)
            {
                string artist = null;
                string track = null;

                switch (i)
                {
                    case 1:
                        // Vanilla Search for Artist & Title
                        artist = this.artist;
                        track = this.track;
                        Console.WriteLine("Step 1: Artist: "+artist+ " Track: "+track);
                        continue;
                    case 2:
                        // Remove Artist in Track & Replace Label
                        artist = this.artist;
                        if (this.track.Contains("-"))
                        {
                            string[] split = this.track.Split('-');
                            if (this.artist.Contains(split[0]))
                            {
                                track = split[1];
                            }
                            else
                            {
                                if (split[0].Contains("-"))
                                {
                                    artist = split[0].Split('-')[0];
                                }
                                if (split[0].Contains("&"))
                                {
                                    artist = split[0].Split('&')[0];
                                }
                                else
                                {
                                    artist = split[0];
                                }
                                track = split[1];
                            }
                            Console.WriteLine("Step 2: Artist: " + artist + " Track: " + track);
                        }
                        else
                        {
                            track = this.track;
                            Console.WriteLine("Step 2: Artist: "+artist+ " Track: "+track);
                        }

                        continue;

                    case 3:
                        // Remove Artist & Modify Track
                        artist = null;
                        string trackex = this.track; ;
                        if (this.track.Contains("-"))
                        {
                            trackex = this.track.Split('-')[1];
                        }
                        else
                        {
                            trackex = this.artist;
                        }
                        
                        if (this.track.Contains("Remix"))
                        {
                            Match m = Regex.Match(this.track, @"(\(|\[).*Remix*(\)|\])", RegexOptions.IgnoreCase);
                            if (m.Success)
                            {
                                // Split track replace remix entry with the clean remix artist
                                string regex = Regex.Replace(trackex, @"(\(|\[).*Remix*(\)|\])", Regex.Replace(m.Value, @"Remix", "", RegexOptions.IgnoreCase).Trim(), RegexOptions.IgnoreCase).Trim();
                                // Remove remaining [] ()
                                track = Regex.Replace(regex, @"(\(.*\)|\[.*\])", "", RegexOptions.IgnoreCase).Trim();
                            }
                            else
                            {
                                track = Regex.Replace(this.track, @"(\(.*\)|\[.*\])", "", RegexOptions.IgnoreCase).Trim();
                            }
                        }
                        else
                        {
                            track += Regex.Replace(this.track, @"(\(.*\)|\[.*\])", "", RegexOptions.IgnoreCase).Trim();
                        }
                        Console.WriteLine("Step 3: Track: " + track);
                        continue;

                    case 4:

                    case 5:

                    default:
                        break;
                }
                Console.WriteLine("");

                //var data = JsonConvert.DeserializeObject<ResultSearch.Search>((Request(i)));
            }
            return 0;
        }

        /// <summary>
        /// Start Search Querys on Official API
        /// </summary>
        /// <param name="lvl">Searchlevel 1,2,3</param>
        /// <param name="track">Trackname</param>
        /// <param name="artist">Artistname</param>
        /// <param name="duration_max">Max Duration</param>
        /// <param name="duration_min">Min Duration</param>
        /// <returns></returns>
        public async Task<string> Request(int lvl, string Artist = null, string Track = null, int duration = 0)
        {
            try
            {
                switch (lvl)
                {
                    case 5:
                        // Artist + Track + Duration
                                   Console.WriteLine(System.Uri.EscapeDataString(Official_api + "artist:" + "\"" + Artist + "\" " + "track:" + "\"" + Track + "\" " + "dur_min:" + (duration-1).ToString() + " dur_max:" + (duration + 1).ToString()));
                        return await client.GetStringAsync(System.Uri.EscapeDataString(Official_api + "artist:" + "\"" + Artist + "\" " + "track:" + "\"" + Track + "\" " + "dur_min:" + (duration - 1).ToString() + " dur_max:" + (duration + 1).ToString()));

                    case 4:
                        // Track + Duration
                                   Console.WriteLine(System.Uri.EscapeDataString(Official_api + "track:" + "\"" + Track + "\" " + "dur_min:" + (duration - 1).ToString() + " dur_max:" + (duration + 1).ToString()));
                        return await client.GetStringAsync(System.Uri.EscapeDataString(Official_api + "track:" + "\"" + Track + "\" " + "dur_min:" + (duration - 1).ToString() + " dur_max:" + (duration + 1).ToString()));

                    case 3:
                        // Artist + Track
                                   Console.WriteLine(System.Uri.EscapeDataString(Official_api + "artist:" + "\"" + Artist + "\" " + "track:" + "\"" + Track + "\""));
                        return await client.GetStringAsync(System.Uri.EscapeDataString(Official_api + "artist:" + "\"" + Artist + "\" " + "track:" + "\"" + Track + "\""));

                    case 2:
                        // Artist
                                   Console.WriteLine(System.Uri.EscapeDataString(Official_api + "artist:" + "\"" + Artist + "\" "));
                        return await client.GetStringAsync(System.Uri.EscapeDataString(Official_api + "artist:" + "\"" + Artist + "\" "));

                    case 1:
                        // Track
                                   Console.WriteLine(System.Uri.EscapeDataString(Official_api + "track:" + "\"" + Track + "\""));
                        return await client.GetStringAsync(System.Uri.EscapeDataString(Official_api + "track:" + "\"" + Track + "\""));

                    default:
                        throw new ArgumentOutOfRangeException("Option out of scope");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "{\"data\": [],\"total\": 0}";
            }
        }
    }
}


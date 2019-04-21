using DeezerSync.Deezer.API.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace DeezerSync.Deezer.API
{
    class Official
    {
        private readonly string Official_api = "https://api.deezer.com/search?q=";
        private string Artist = "artist:";
        private string Track = "track:";
        private string Duration_min = "dur_min:";
        private string Duration_max = "dur_max:";

        private string artist;
        private string track;
        private string duration_min;
        private string duration_max;

        private static readonly HttpClient client = new HttpClient();

        public Official(string artist, string track, string max_dur, string min_dur)
        {
            this.artist = artist;
            this.track = track;
            this.duration_min = min_dur;
            this.duration_max = max_dur;

            this.Artist = this.Artist + ((char)34) + artist + ((char)34);
            this.Track = this.Track + ((char)34) + track + ((char)34);
            this.Duration_max = this.Duration_max + ((long.Parse(max_dur)/60) - 1).ToString();
            this.Duration_min = this.Duration_min + ((long.Parse(min_dur)/60) - 1).ToString();
        }

        public long finder(int lvl)
        {
            var data = JsonConvert.DeserializeObject<ResultSearch.Search>((Request(lvl)));
            if (data.Data != null)
            {
                foreach (var i in data.Data)
                {
                    if (i.Title.Contains(track) || i.Artist.Name.ToString().Equals(artist))
                    {
                        Console.WriteLine(i.Link.AbsoluteUri);
                        return i.Id.Value;
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
        /// <param name="duration_max">Max Duration</param>
        /// <param name="duration_min">Min Duration</param>
        /// <returns></returns>
        public string Request(int lvl)
        {
            try
            {
                switch (lvl)
                {
                    case 3:
                        Console.WriteLine(Official_api + Artist + " " + Track + Duration_min + " " + Duration_max);
                        return client.GetStringAsync(Official_api + Artist + " " + Track + Duration_min + " " + Duration_max).Result;

                    case 2:
                        Console.WriteLine(Official_api + Artist + " " + Track);
                        return client.GetStringAsync(Official_api + Artist + " " + Track).Result;

                    case 1:
                        Console.WriteLine(Official_api + Track);
                        return client.GetStringAsync(Official_api + Track).Result;

                    default:
                        throw new ArgumentOutOfRangeException("Option out of scope");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return "{\"data\": [],\"total\": 0}";
            }
        }
    }
}

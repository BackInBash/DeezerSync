using System;
using System.Text.RegularExpressions;

namespace Search
{
    public class RemixTest
    {
        private string artist;
        private string track;
        private int duration;


        public RemixTest(string artist, string track, int duration)
        {
            this.artist = artist;
            this.track = track;
            this.duration = duration;
        }

        public void test()
        {
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
                        Console.WriteLine("Step 1: Artist: " + artist + " Track: " + track);
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
                            Console.WriteLine("Step 2: Artist: " + artist + " Track: " + track);
                        }
                        else
                        {
                            track = tmptrack;
                            Console.WriteLine("Step 2: Artist: " + artist + " Track: " + track);
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
                            trackex = this.artist;
                        }

                        if (this.track.Contains("Remix"))
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
                        Console.WriteLine("Step 3: Artist: "+artist+" Track: " + track);
                        break;

                }
            }
        }
    }
}

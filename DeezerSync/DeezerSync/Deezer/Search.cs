using DeezerSync.Deezer.API;
using DeezerSync.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DeezerSync.Deezer
{
    class Search
    {
        protected Deezer.API.Official s = null;
        protected Deezer.Playlist p = null;
        private List<StandardPlaylist> SoundCloud;
        private List<StandardPlaylist> Deezer;
        private List<long> TrackIDs = new List<long>();

        /// <summary>
        /// Initialize Object with Playlist Data
        /// </summary>
        /// <param name="SoundCloud">Playlist List from Soundcloud</param>
        /// <param name="Deezer">Playlist List from Deezer</param>
        public Search(List<StandardPlaylist> SoundCloud, List<StandardPlaylist> Deezer)
        {
            //s = new Deezer.API.Official();
            p = new Deezer.Playlist();
            this.SoundCloud = SoundCloud;
            this.Deezer = Deezer;
        }

        public void Title()
        {
            List<string> dz = new List<string>();
            List<string> sc = new List<string>();

            foreach (var d in Deezer)
            {
                dz.Add(d.title);
            }

            foreach (var s in SoundCloud)
            {
                sc.Add(s.title);
            }

            IEnumerable<string> different = sc.Except(dz);

            foreach (var diff in different)
            {
                Console.WriteLine("Create Playlist: " + diff);
                DeezerSync.Deezer.Playlist.CreatePlaylist(diff);
            }

            // Playlist loop
            foreach (var playlist in SoundCloud)
            {
                List<long> TrackIDs = new List<long>();
                Deezer = null;
                Deezer = DeezerSync.Deezer.Playlist.GetAllPlaylists();

                // Track loop
                foreach (var track in playlist.tracks)
                {
                    // Search loop
                    Deezer.API.Official o = new Deezer.API.Official(track.username, track.title, track.duration);
                    long id = o.finder();
                    if (id != 0)
                    {
                        bool NotExists = true;
                        foreach (var deezer in Deezer)
                        {
                            if (deezer.title.Equals(playlist.title))
                            {
                                foreach (var dzloop in deezer.tracks)
                                {
                                    if (dzloop.id.Equals(id))
                                    {
                                        NotExists = false;
                                    }
                                }
                            }
                        }
                        if (NotExists == true)
                        {
                            TrackIDs.ToList();
                            if (TrackIDs.Count.Equals(0))
                            {
                                TrackIDs.Add(id);
                            }
                            else
                            {
                                foreach (long l in TrackIDs.ToList())
                                {
                                    if (l != id)
                                    {
                                        NotExists = false;
                                    }
                                }
                                if(NotExists == true)
                                {
                                    TrackIDs.Add(id);
                                }
                            }
                        }
                    }
                }

                if (TrackIDs.Count != 0)
                {
                    string playlistid = null;
                    foreach (var did in Deezer)
                    {
                        if (playlist.title.Equals(did.title))
                        {
                            playlistid = did.id;
                        }
                    }
                    Playlist.AddSongsToPlaylist(playlistid, TrackIDs);
                }
                else
                {
                    Console.WriteLine("Playlist " + playlist.title + " no changes.");
                }
            }
        }
    }
}

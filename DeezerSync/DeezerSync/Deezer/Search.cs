using DeezerSync.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeezerSync.Deezer
{
    class Search
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
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
                logger.Info("Create Playlist: " + diff);
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
                        // True title dont exists in Deezer Playlist
                        bool NotExists = true;
                        foreach (var deezer in Deezer)
                        {
                            if (deezer.title.Equals(playlist.title))
                            {
                                foreach (var dzloop in deezer.tracks)
                                {
                                    if (dzloop.id.Equals(id))
                                    {
                                        // False track exists in Deezer Playlist
                                        NotExists = false;
                                        logger.Warn("Track: "+dzloop.username+" - "+dzloop.title+" already in Playlist "+ deezer.title);
                                    }
                                }
                            }
                        }
                        if (NotExists == true)
                        {
                            TrackIDs.ToList();
                            if (TrackIDs.Count.Equals(0))
                            {
                                // Add Track ID to tmp list if List is empty
                                TrackIDs.Add(id);
                            }
                            else
                            {
                                foreach (long l in TrackIDs.ToList())
                                {
                                    if (l == id)
                                    {
                                        // Track exists in tmp List
                                        NotExists = false;
                                        logger.Warn("Track: "+ track.username + " - " + track.title + " already in tmp List");
                                    }
                                }
                                if (NotExists == true)
                                {
                                    // Track dont exists in tmp List
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
                            // Get Playlist ID from name
                            playlistid = did.id;
                        }
                    }
                    Playlist.AddSongsToPlaylist(playlistid, TrackIDs);
                    logger.Info("Playlist " + playlist.title + " with " + TrackIDs.Count + " changes.");
                }
                else
                {
                    logger.Info("Playlist " + playlist.title + " no changes.");
                }
            }
        }
    }
}

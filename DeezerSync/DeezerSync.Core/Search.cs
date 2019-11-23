using DeezerSync.Models;
using DeezerSync.Models.API;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeezerSync.Log;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace DeezerSync.Core
{
    partial class DebugResult
    {
        public StandardTitle Searching;
        public List<StandardTitle> Results;
    }
    public class Search
    {
        private List<StandardPlaylist> MusicProvider;
        private List<StandardPlaylist> Deezer;

        public NLogger log;

        public Search(NLogger log)
        {
            this.log = log;
        }
        public Search(List<StandardPlaylist> MusicProvider, List<StandardPlaylist> Deezer)
        {
            this.MusicProvider = MusicProvider;
            this.Deezer = Deezer;
        }

        /// <summary>
        /// Start the Search routine
        /// </summary>
        /// <returns></returns>
        public async Task Start()
        {

            // Setup Logging

            var logger = LogManager.GetCurrentClassLogger();
            try
            {
                var config = new ConfigurationBuilder()
                   .SetBasePath(System.IO.Directory.GetCurrentDirectory()) //From NuGet Package Microsoft.Extensions.Configuration.Json
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                   .Build();

                var servicesProvider = DeezerSync.Log.Logging.BuildDi(config);
                using (servicesProvider as IDisposable)
                {
                    log = servicesProvider.GetRequiredService<NLogger>();

                    // Create Deezer Playlists
                    Prepare prepare = new Prepare();
                    await prepare.CreateMissingPlaylists(MusicProvider, Deezer);

                    // Apply Filter on Track Data
                    foreach (var playlist in MusicProvider)
                    {
                        List<long> TrackIDs = new List<long>();

                        foreach (var track in playlist.tracks)
                        {
                            log.Info("Start Searching for Track: " + track.title + " Artist: " + track.username + " Label: " + track.labelname);
                            var query = await prepare.PrepareDeezerQuery(track);
                            var result = new List<StandardTitle>();

                            for (int i=1; i<4; i++)
                            {
                                log.Debug("Search Stage "+i);
                                query.search_stage = i;
                                result = await ExecuteQuery(query);
                                if(result.Count != 0)
                                {
                                    break;
                                }
                            }
                             
                            long id = await search(result, track);

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
                            log.Info("\n");
                            Console.WriteLine();
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
                            await AddSongToPlaylist(playlistid, TrackIDs);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // NLog: catch any exception and log it.
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                LogManager.Shutdown();
            }
        }

        /// <summary>
        /// Add multiple Songs to a Deezer Playlist
        /// </summary>
        /// <param name="id">Playlist ID</param>
        /// <param name="ids">SongIDs</param>
        /// <returns></returns>
        protected async Task<bool> AddSongToPlaylist(string id, List<long> ids)
        {
            DeezerAPI.Private api = new DeezerAPI.Private();
            return await api.AddSongsToPlaylistasync(id, ids);
        }

        /// <summary>
        /// Check Search result for the right Track
        /// </summary>
        /// <param name="results">Deezer Query Results</param>
        /// <param name="Searching">The Track to search for</param>
        /// <returns></returns>
        public async Task<long> search(List<StandardTitle> results, StandardTitle Searching)
        {
#if DEBUG
            /*
            int i = 0;
            do { i++; } while (File.Exists(Directory.GetCurrentDirectory() + "\\results\\" + i + ".json"));
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\results\\");
            await File.WriteAllTextAsync(Directory.GetCurrentDirectory() + "\\results\\" + i+".json", JsonConvert.SerializeObject(new DebugResult { Searching = Searching, Results = results }, Formatting.Indented));
            */
#endif
            foreach (var result in results)
            {







                /*
                 * DeezerSync Result Search Filter
                 * Results 777 songs out of 1593
                 * False positives TRUE
                 * 
                //Artist
                if (!string.IsNullOrEmpty(result.artist))
                {
                    if ((result.artist.Contains(Searching.artist ?? Searching.username) && result.title.Equals(Searching.title)) || await checkDuration(Searching.duration, result.duration))
                    {
                        log.Info("Found Song Artist: " + result.artist + " Track: " + result.title + " https://www.deezer.com/us/track/" + result.id);
                        return result.id;
                    }

                    if ((result.artist.Contains(Searching.artist ?? Searching.username) && result.title.Contains(Searching.title)) || await checkDuration(Searching.duration, result.duration))
                    {
                        log.Info("Found Song Artist: " + result.artist + " Track: " + result.title + " https://www.deezer.com/us/track/" + result.id);
                        return result.id;
                    }

                    if ((result.artist.Contains(Searching.artist ?? Searching.username) || result.title.Contains(Searching.title)) && await checkDuration(Searching.duration, result.duration))
                    {
                        log.Info("Found Song Artist: " + result.artist + " Track: " + result.title + " https://www.deezer.com/us/track/" + result.id);
                        return result.id;
                    }

                    if (result.artist.Contains(Searching.artist ?? Searching.username) && result.title.Contains(Searching.title))
                    {
                        log.Info("Found Song Artist: " + result.artist + " Track: " + result.title + " https://www.deezer.com/us/track/" + result.id);
                        return result.id;
                    }
                }
                //Remix Artist
                if (!string.IsNullOrEmpty(Searching.remixArtist))
                {
                    if ((result.artist.Contains(Searching.remixArtist) && result.title.Equals(Searching.title)) || await checkDuration(Searching.duration, result.duration))
                    {
                        log.Info("Found Song Artist: " + result.artist + " Track: " + result.title + " https://www.deezer.com/us/track/" + result.id);
                        return result.id;
                    }

                    if ((result.artist.Contains(Searching.remixArtist) && result.title.Contains(Searching.title)) || await checkDuration(Searching.duration, result.duration))
                    {
                        log.Info("Found Song Artist: " + result.artist + " Track: " + result.title + " https://www.deezer.com/us/track/" + result.id);
                        return result.id;
                    }

                    if ((result.artist.Contains(Searching.remixArtist) || result.title.Contains(Searching.title)) && await checkDuration(Searching.duration, result.duration))
                    {
                        log.Info("Found Song Artist: " + result.artist + " Track: " + result.title + " https://www.deezer.com/us/track/" + result.id);
                        return result.id;
                    }

                    if (result.artist.Contains(Searching.remixArtist) && result.title.Contains(Searching.title))
                    {
                        log.Info("Found Song Artist: " + result.artist + " Track: " + result.title + " https://www.deezer.com/us/track/" + result.id);
                        return result.id;
                    }

                    if (result.artist.Contains(Searching.artist ?? Searching.username) && (result.title.Contains(Searching.remixArtist) && (result.title.Contains(Searching.title))))
                    {
                        log.Info("Found Song Artist: " + result.artist + " Track: " + result.title + " https://www.deezer.com/us/track/" + result.id);
                        return result.id;
                    }
                }
                //Username
                if ((result.username.Contains(Searching.username) && result.title.Equals(Searching.title)) || await checkDuration(Searching.duration, result.duration))
                {
                    log.Info("Found Song Artist: " + result.artist + " Track: " + result.title + " https://www.deezer.com/us/track/" + result.id);
                    return result.id;
                }

                if ((result.username.Contains(Searching.username) && result.title.Contains(Searching.title)) || await checkDuration(Searching.duration, result.duration))
                {
                    log.Info("Found Song Artist: " + result.username + " Track: " + result.title + " https://www.deezer.com/us/track/" + result.id);
                    return result.id;
                }

                if ((result.username.Contains(Searching.username) || result.title.Contains(Searching.title)) && await checkDuration(Searching.duration, result.duration))
                {
                    log.Info("Found Song Artist: " + result.username + " Track: " + result.title + " https://www.deezer.com/us/track/" + result.id);
                    return result.id;
                }

                if (result.username.Contains(Searching.username) && result.title.Contains(Searching.title))
                {
                    log.Info("Found Song Artist: " + result.username + " Track: " + result.title + " https://www.deezer.com/us/track/" + result.id);
                    return result.id;
                }
                */
                log.Info("Could not find Track: " + result.title + " Artist: " + result.artist ?? result.username + "Original Track: " + Searching.title + " Artist: " + Searching.artist ?? Searching.username);
            }
            return 0;
        }

        /// <summary>
        /// Execute the Deezer Search Query
        /// </summary>
        /// <param name="query">Prepared StandardTitel Object</param>
        /// <returns></returns>
        protected async Task<List<StandardTitle>> ExecuteQuery(StandardTitle query)
        {
            try
            {
                // Deezer Private API
                /*
                DeezerAPI.Private api = new DeezerAPI.Private();
                if (query.isRemix)
                {
                    return await api.SearchQuery(query.username + " " + query.title + " " + query.labelname);
                }
                return await api.SearchQuery(query.username + " " + query.title);
                */

                // Deezer Public API
                DeezerAPI.Official api = new DeezerAPI.Official(query);
                ResultSearch.Search res = await api.Search();
                log.Info("Found " + res.Data.Count + " Tracks");
                if (res.Data.Count > 0)
                {
                    List<StandardTitle> tracks = new List<StandardTitle>();
                    Prepare p = new Prepare();
                    foreach (var i in res.Data)
                    {
                        tracks.Add(await p.PrepareDeezerQuery(new StandardTitle { description = string.Empty, duration = (int)i.Duration, genre = string.Empty, id = i.Id.Value, labelname = string.Empty, search_stage = 0, title = i.Title, username = i.Artist.Name, artist = i.Artist.Name, url = i.Link.AbsoluteUri }));
                    }
                    return tracks;
                }
                return new List<StandardTitle>();
            }
            catch (Exception e)
            {
                log.Warning("Search Query Exception: "+e.Message, e);
                return new List<StandardTitle>();
            }
        }


        /// <summary>
        /// Compare the song duration to guess if it is the right one
        /// </summary>
        /// <param name="actual">Actual Song Duraion (MusicProvider)</param>
        /// <param name="found">Found Song Duraion (Deezer)</param>
        /// <returns></returns>
        public async Task<bool> checkDuration(long actual, long found)
        {
            if (actual == (found - 2))
            {
                log.Debug("Same Duration TRUE");
                return true;
            }
            if (actual == (found - 1))
            {
                log.Debug("Same Duration TRUE");
                return true;
            }
            if (actual == found)
            {
                log.Debug("Same Duration TRUE");
                return true;
            }
            if (actual == (found + 1))
            {
                log.Debug("Same Duration TRUE");
                return true;
            }
            if (actual == (found + 2))
            {
                log.Debug("Same Duration TRUE");
                return true;
            }
            log.Debug("Duration check FALSE");
            return false;
        }
    }
}

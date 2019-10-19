﻿using DeezerSync.Models;
using DeezerSync.Models.API;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeezerSync.Core
{
    /// <summary>
    /// Log Class
    /// </summary>
    public class NLogger
    {
        private readonly ILogger<NLogger> _logger;

        public NLogger(ILogger<NLogger> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Debug Log
        /// </summary>
        /// <param name="name"></param>
        public void Debug(string name)
        {
            _logger.LogDebug(20, "{Action}", name);
        }
        /// <summary>
        /// Info Log
        /// </summary>
        /// <param name="name"></param>
        public void Info(string name)
        {
            _logger.LogInformation(20, "{Action}", name);
        }
        /// <summary>
        /// Error Log
        /// </summary>
        /// <param name="name"></param>
        public void Error(string name)
        {
            _logger.LogError(20, "{Action}", name);
        }
        /// <summary>
        /// Warning Log
        /// </summary>
        /// <param name="name"></param>
        public void Warning(string name)
        {
            _logger.LogWarning(20, "{Action}", name);
        }
    }

    public class Search
    {
        private List<StandardPlaylist> MusicProvider;
        private List<StandardPlaylist> Deezer;
        public Search(List<StandardPlaylist> MusicProvider, List<StandardPlaylist> Deezer)
        {
            this.MusicProvider = MusicProvider;
            this.Deezer = Deezer;
        }

        /// <summary>
        /// Private Logger Injector
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IServiceProvider BuildDi(IConfiguration config)
        {
            return new ServiceCollection()
               .AddTransient<NLogger>()
               .AddLogging(loggingBuilder =>
               {
                   // configure Logging with NLog
                   loggingBuilder.ClearProviders();
                   loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                   loggingBuilder.AddNLog(config);
               })
               .BuildServiceProvider();
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

                var servicesProvider = BuildDi(config);
                using (servicesProvider as IDisposable)
                {
                    var log = servicesProvider.GetRequiredService<NLogger>();

                    // Create Deezer Playlists
                    Prepare prepare = new Prepare();
                    await prepare.CreateMissingPlaylists(MusicProvider, Deezer);

                    // Apply Filter on Track Data
                    foreach (var playlist in MusicProvider)
                    {
                        List<long> TrackIDs = new List<long>();

                        foreach (var track in playlist.tracks)
                        {
                            var query = await prepare.PrepareDeezerQuery(track);
                            var result = await ExecuteQuery(query);
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
        protected async Task<long> search(List<StandardTitle> results, StandardTitle Searching)
        {
            foreach (var result in results)
            {
                if ((result.username.Contains(Searching.username) && result.title.Equals(Searching.title)) || await checkDuration(Searching.duration, result.duration))
                {
                    return result.id;
                }

                if ((result.username.Contains(Searching.username) && result.title.Contains(Searching.title)) || await checkDuration(Searching.duration, result.duration))
                {
                    return result.id;
                }

                if ((result.username.Contains(Searching.username) || result.title.Contains(Searching.title)) && await checkDuration(Searching.duration, result.duration))
                {
                    return result.id;
                }
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
                /*
                DeezerAPI.Private api = new DeezerAPI.Private();
                if (query.isRemix)
                {
                    return await api.SearchQuery(query.username + " " + query.title + " " + query.labelname);
                }
                return await api.SearchQuery(query.username + " " + query.title);
                */

                DeezerAPI.Official api = new DeezerAPI.Official(query);
                ResultSearch.Search res = await api.Search();
                if (res.Data.Count > 0)
                {
                    List<StandardTitle> tracks = new List<StandardTitle>();
                    foreach (var i in res.Data)
                    {
                        tracks.Add(new StandardTitle { description = string.Empty, duration = (int)i.Duration, genre = string.Empty, id = i.Id.Value, isRemix = false, labelname = string.Empty, search_stage = 0, title = i.Title, username = i.Artist.Name });
                    }
                    return tracks;
                }
                return new List<StandardTitle>();
            }
            catch (Exception)
            {
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
                return true;
            }
            if (actual == (found - 1))
            {
                return true;
            }
            if (actual == found)
            {
                return true;
            }
            if (actual == (found + 1))
            {
                return true;
            }
            if (actual == (found + 2))
            {
                return true;
            }
            return false;
        }
    }
}

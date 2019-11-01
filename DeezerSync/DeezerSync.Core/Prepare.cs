using DeezerSync.Log;
using DeezerSync.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DeezerSync.Core
{
    public class Prepare
    {
        public NLogger log;
        public Prepare()
        {
            var config = new ConfigurationBuilder()
            .SetBasePath(System.IO.Directory.GetCurrentDirectory()) //From NuGet Package Microsoft.Extensions.Configuration.Json
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

            var servicesProvider = DeezerSync.Log.Logging.BuildDi(config);
            using (servicesProvider as IDisposable)
            {
                log = servicesProvider.GetRequiredService<NLogger>();
            }
        }

        public Prepare(NLogger log)
        {
            this.log = log;
        }

        /// <summary>
        /// Create Missing Playlists on Deezer
        /// </summary>
        /// <param name="MusicProvider">Music Provider Playlists</param>
        /// <param name="Deezer">Deezer Playlists</param>
        /// <returns></returns>
        internal async Task CreateMissingPlaylists(List<StandardPlaylist> MusicProvider, List<StandardPlaylist> Deezer)
        {
            // Initialize Objects
            List<string> dz = new List<string>();
            List<string> sc = new List<string>();
            DeezerAPI.Private api = new DeezerAPI.Private();

            // Write Playlist Names to Lists
            foreach (var d in Deezer)
            {
                dz.Add(d.title);
            }

            foreach (var s in MusicProvider)
            {
                sc.Add(s.title);
            }

            // Get differences from Lists
            IEnumerable<string> different = sc.Except(dz);

            // Create mising playlists
            foreach (var diff in different)
            {
                log.Info("Create Deezer Playlist: " + diff);
                await api.CreatePlaylistasync(diff);
            }
        }

        /// <summary>
        /// Remove special chars to improve seach results
        /// </summary>
        /// <param name="input">StandardTitel Object</param>
        /// <returns></returns>
        public async Task<StandardTitle> PrepareDeezerQuery(StandardTitle input)
        {
            // Remove unsearchable Char
            if (input.title.Contains("&"))
            {
                log.Debug("Remove '&' from Title " + input.title);
                input.title = Regex.Replace(input.title, "&", "", RegexOptions.IgnoreCase).Trim();
            }

            if (input.username.Contains("&"))
            {
                log.Debug("Remove '&' from Artist" + input.username);
                input.username = Regex.Replace(input.username, "&", "", RegexOptions.IgnoreCase);
            }

            // Remix Detection + Set Remix Artist as label
            if (Regex.Match(input.title, @"Remix", RegexOptions.IgnoreCase).Success)
            {
                log.Info(input.title + " is Remix");
                input.isRemix = true;
                Match m = Regex.Match(input.title, @"(\(|\[).*Remix*(\)|\])", RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    // Split track replace remix entry with the clean remix artist
                    string regex = Regex.Replace(input.title, @"(\(|\[).*Remix*(\)|\])", m.Value, RegexOptions.IgnoreCase).Trim();

                    // Set Remix Artist as new Artist
                    input.remixArtist = Regex.Replace(m.Value, @"[\(*\)|\[*\]]", "", RegexOptions.IgnoreCase).Trim();
                    input.remixArtist = Regex.Replace(input.remixArtist, @"Remix", "", RegexOptions.IgnoreCase).Trim();
                    log.Debug("Set Remix Artist " + input.remixArtist + " as new main Artist " + input.username + " (labelname)");

                    // Remove remaining [] ()
                    input.title = Regex.Replace(regex, @"(\(.*\)|\[.*\])", "", RegexOptions.IgnoreCase).Trim();
                    input.title = Regex.Replace(input.title, @"&", "", RegexOptions.IgnoreCase).Trim();
                    log.Debug("New Title is: " + input.title);
                }
                else
                {
                    input.title = Regex.Replace(input.title, @"(\(.*\)|\[.*\])", "", RegexOptions.IgnoreCase).Trim();
                    input.title = Regex.Replace(input.title, @"&", "", RegexOptions.IgnoreCase).Trim();
                    log.Debug("New Title is: " + input.title);
                }
            }
            else
            {
                input.title = Regex.Replace(input.title, @"(\(.*\)|\[.*\])", "", RegexOptions.IgnoreCase).Trim();
                input.title = Regex.Replace(input.title, @"&", "", RegexOptions.IgnoreCase).Trim();
                log.Debug("New Title is: " + input.title);
            }

            // Remove unsearchable chars
            if (input.title.Contains("<") && input.title.Contains(">"))
            {
                log.Debug("Remove '< >' from Title");
                input.title = Regex.Replace(input.title, @"(<.*>)", "", RegexOptions.IgnoreCase).Trim();
            }

            // Remove artist name in titel
            Match cleanTitel = Regex.Match(input.title, @"\s(-)\s", RegexOptions.IgnoreCase);
            if (input.title.Contains(input.username))
            {
                if (cleanTitel.Success)
                {
                    log.Debug("Remove same Artist from Titel");
                    string[] tmp = Regex.Split(input.title, @"\s(-)\s", RegexOptions.IgnoreCase);
                    input.artist = tmp[0].Trim();
                    input.title = tmp[2].Trim();
                }
            }
            else
            {
                if (cleanTitel.Success)
                {
                    log.Debug("Remove Artist from Title");
                    string[] tmp = Regex.Split(input.title, @"\s(-)\s", RegexOptions.IgnoreCase);
                    input.title = tmp[2].Trim();

                    // Check if contains two whitespaces can come from an previeous char removal
                    if(Regex.Match(tmp[0], @"\s\s", RegexOptions.IgnoreCase).Success)
                    {
                        string[] art = Regex.Split(tmp[0], @"\s\s", RegexOptions.IgnoreCase);
                        input.artist = art[0].Trim();
                    }
                    else
                    {
                        input.artist = tmp[0].Trim();
                    }
                }
            }
            log.Debug("Prepared Serach Query is Artist: " + input.username + " Titel: " + input.title);
            return input;
        }
    }
}
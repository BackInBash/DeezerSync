﻿using DeezerSync.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DeezerSync.Core
{
    class Prepare
    {

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
                await api.CreatePlaylistasync(diff);
            }
        }

        /// <summary>
        /// Remove special chars to improve seach results
        /// </summary>
        /// <param name="input">StandardTitel Object</param>
        /// <returns></returns>
        internal async Task<StandardTitle> PrepareDeezerQuery(StandardTitle input)
        {
            // Remove unsearchable Char
            if (input.title.Contains("&"))
            {
                input.title = Regex.Replace(input.title, "&", "", RegexOptions.IgnoreCase).Trim();
            }

            if (input.username.Contains("&"))
            {
                input.username = Regex.Replace(input.username, "&", "", RegexOptions.IgnoreCase).Trim();
            }

            // Remix Detection + Set Remix Artist as label
            if (Regex.Match(input.title, @"Remix", RegexOptions.IgnoreCase).Success)
            {
                input.isRemix = true;
                Match m = Regex.Match(input.title, @"(\(|\[).*Remix*(\)|\])", RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    // Split track replace remix entry with the clean remix artist
                    string regex = Regex.Replace(input.title, @"(\(|\[).*Remix*(\)|\])", m.Value, RegexOptions.IgnoreCase).Trim();

                    // Set Remix Artist as new Artist
                    input.labelname = Regex.Replace(m.Value, @"[\(*\)|\[*\]]", "", RegexOptions.IgnoreCase).Trim();
                    input.labelname = Regex.Replace(input.labelname, @"Remix", "", RegexOptions.IgnoreCase).Trim();

                    // Remove remaining [] ()
                    input.title = Regex.Replace(regex, @"(\(.*\)|\[.*\])", "", RegexOptions.IgnoreCase).Trim();
                    input.title = Regex.Replace(input.title, @"&", "", RegexOptions.IgnoreCase).Trim();
                }
                else
                {
                    input.title = Regex.Replace(input.title, @"(\(.*\)|\[.*\])", "", RegexOptions.IgnoreCase).Trim();
                    input.title = Regex.Replace(input.title, @"&", "", RegexOptions.IgnoreCase).Trim();
                }
            }
            else
            {
                input.title = Regex.Replace(input.title, @"(\(.*\)|\[.*\])", "", RegexOptions.IgnoreCase).Trim();
                input.title = Regex.Replace(input.title, @"&", "", RegexOptions.IgnoreCase).Trim();
            }

            // Remove unsearchable chars
            if (input.title.Contains("<") && input.title.Contains(">"))
            {
                input.title = Regex.Replace(input.title, @"(<.*>)", "", RegexOptions.IgnoreCase).Trim();
            }

            // Remove artist name in titel
            if (input.title.Contains(input.username))
            {
                if (input.title.Contains("-"))
                {
                    input.title = input.title.Split('-')[1].Trim();
                }
            }

            return input;
        }
    }
}
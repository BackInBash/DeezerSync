using DeezerSync.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DeezerSync.MusicProvider
{
    public class main
    {

        public List<StandardPlaylist> Data = new List<StandardPlaylist>();

        public main(string SoundCloudUsername, string SpotifyUserID, string SpotifyAPIKey, string SoundCloudClientID = null)
        {
            Spotify s = null;
            SoundCloud sc = null;

            // SoundCloud

            if (!string.IsNullOrEmpty(SoundCloudClientID))
            {
                sc = new SoundCloud(SoundCloudUsername, SoundCloudClientID);
            }
            else
            {
                sc = new SoundCloud(SoundCloudUsername);
            }
            Data.AddRange(getData(sc).GetAwaiter().GetResult());

            // Spotify

            s = new Spotify(SpotifyAPIKey, SpotifyUserID);
            Data.AddRange(getData(s).GetAwaiter().GetResult());

        }

        public async Task<List<StandardPlaylist>> getData(dynamic s)
        {
            return await s.GetStandardPlaylists();
        }
    }
}

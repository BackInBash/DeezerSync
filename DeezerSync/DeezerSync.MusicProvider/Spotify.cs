using DeezerSync.Models;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeezerSync.MusicProvider
{
    class Spotify
    {
        private string[] SpotifyUserIDs;
        private string secret;
        public Spotify(string secret, string userid)
        {
            SpotifyUserIDs = new string[] { userid };
            this.secret = secret;

        }

        public async Task<List<StandardPlaylist>> GetStandardPlaylists()
        {
            SpotifyWebAPI _spotify = new SpotifyWebAPI()
            {
                AccessToken = secret,
                TokenType = "Bearer"
            };

            List<StandardPlaylist> Playlists = new List<StandardPlaylist>();

            foreach (string user in SpotifyUserIDs)
            {
                Paging<SimplePlaylist> userPlaylists = _spotify.GetUserPlaylists(user, 50);
                if (userPlaylists.Error == null)
                {
                    foreach (var i in userPlaylists.Items)
                    {
                        StandardPlaylist stp = new StandardPlaylist();
                        FullPlaylist playlist = await _spotify.GetPlaylistAsync(i.Id);
                        stp.description = playlist.Description;
                        stp.id = playlist.Id;
                        stp.provider = "spotify";
                        stp.title = playlist.Name;

                        List<StandardTitle> track = new List<StandardTitle>();
                        foreach(var trck in playlist.Tracks.Items)
                        {
                            track.Add(new StandardTitle { username = trck.Track.Artists[0].Name, description = trck.Track.Album.Name, duration = trck.Track.DurationMs, genre = trck.Track.Type, title = trck.Track.Name});
                        }
                        stp.tracks = track;
                        Playlists.Add(stp);
                    }
                }
            }
            return Playlists;
        }
    }
}

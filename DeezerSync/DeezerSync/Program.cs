
namespace DeezerSync
{
    class Program
    {
        //public static List<StandardPlaylist> Playlists = new List<StandardPlaylist>();

        static void Main(string[] args)
        {
            // Read Config from File
            Config config = new Config();
            config.Read();

            // Get all Playlists and start searching
            SoundCloud.playlist pl = new SoundCloud.playlist();

            Deezer.Search s = new Deezer.Search(pl.GetStandardPlaylists().Result, Deezer.Playlist.GetAllPlaylists());
            s.Title();
        }
    }
}

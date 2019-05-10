using System;

namespace DeezerSync
{
    class Program
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            Console.WriteLine(
                " _____                               _____                     \n" +
                "|  __ \\                             / ____|\n"+                    
"| |  | |  ___   ___  ____ ___  _ __| (___   _   _  _ __    ___ \n"+
"| |  | | / _ \\ / _ \\|_  // _ \\| '__|\\___ \\ | | | || '_ \\  / __|\n"+
"| |__| ||  __/|  __/ / /|  __/| |   ____) || |_| || | | || (__ \n"+
"|_____/  \\___| \\___|/___|\\___||_|  |_____/  \\__, ||_| |_| \\___|\n"+
"                                             __/ |             \n"+
"                                            |___/");


            logger.Info("Starting DeezerSync on "+ System.Runtime.InteropServices.RuntimeInformation.OSDescription);

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

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace DeezerSync
{
    public partial class ConfigModel
    {
        public string SoundCloud_Username;
        public string SoundCloud_ClientID;
        public string Deezer_Secret;
        public string Spotify_Username;
        public string Spotify_Secret;
        public string db_ip;
        public string db_port;
    }
    class Config
    {
        public string soundcloud_profile { get; private set; }
        public string soundcloud_clientid { get; private set; }
        public string deezer_secret { get; private set; }
        public string spotify_profile { get; private set; }
        public string spotify_secret { get; private set; }
        public string db_ip { get; set; }
        public string db_port { get; set; }

        private string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "config.json");

        public Config()
        {
            Read();
        }

        public Config(string path)
        {
            this.path = path;
            Read();
        }

        private void Read()
        {
            try
            {
                string config = File.ReadAllText(path);
                var result = JsonConvert.DeserializeObject<ConfigModel>(config);

                soundcloud_profile = result.SoundCloud_Username;
                soundcloud_clientid = result.SoundCloud_ClientID;
                deezer_secret = result.Deezer_Secret;
                spotify_profile = result.Spotify_Username;
                spotify_secret = result.Spotify_Secret;
                db_ip = result.db_ip;
                db_port = result.db_port;

            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("No Config File in " + path + " found.");
            }
            catch (IOException e)
            {
                throw new IOException(e.Message);
            }
            catch (JsonException ex)
            {
                throw new JsonException(ex.Message);
            }
        }
    }
}

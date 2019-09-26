﻿using Newtonsoft.Json;
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
    }
    class Config
    {
        public string soundcloud_profile { get; private set; }
        public string soundcloud_clientid { get; private set; }
        public string deezer_secret { get; private set; }

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
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SoundCloud.Api;
using SoundCloud.Api.Entities;

namespace Search.SoundCloud
{
    public class Loader
    {
        private static string clientId = "FweeGBOOEOYJWLJN3oEyToGLKhmSz0I7";
        protected static string username = "omg-network-radio";
        protected static User user = null;
        protected ISoundCloudClient client = null;

        /// <summary>
        /// Initialize SoundCloud API with the provided ClientID.
        /// </summary>
        protected async Task init()
        {
            client = SoundCloudClient.CreateUnauthorized(clientId);
            var entity = await client.Resolve.GetEntityAsync("https://soundcloud.com/"+username);
            user = entity as User;
        }
    }
}

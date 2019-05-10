using System.Threading.Tasks;
using SoundCloud.Api;
using SoundCloud.Api.Entities;

namespace DeezerSync.SoundCloud
{
    public class Loader
    {
        private static string clientId = Config.soundcloud_clientid;
        protected static string username = Config.soundcloud_profile;
        protected static User user = null;
        protected ISoundCloudClient client = null;

        /// <summary>
        /// Initialize SoundCloud API with the provided ClientID.
        /// </summary>
        protected async Task init()
        {
            client = SoundCloudClient.CreateUnauthorized(clientId);
            var entity = await client.Resolve.GetEntityAsync("https://soundcloud.com/" + username);
            user = entity as User;
        }
    }
}

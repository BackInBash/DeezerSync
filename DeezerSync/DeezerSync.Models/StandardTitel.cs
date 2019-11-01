using System;
using System.Collections.Generic;
using System.Text;

namespace DeezerSync.Models
{
    public partial class StandardTitle
    {
        /// <summary>
        /// Description
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// Duration in Sec.
        /// </summary>
        public int duration { get; set; }
        /// <summary>
        /// Genre
        /// </summary>
        public string genre { get; set; }
        /// <summary>
        /// Track Titel
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// Username
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// Artist
        /// </summary>
        public string artist { get; set; }
        /// <summary>
        /// Remix Artist
        /// </summary>
        public string remixArtist { get; set; }
        /// <summary>
        /// Artists Label
        /// </summary>
        public string labelname { get; set; }
        /// <summary>
        /// Music Providers Track ID
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// If Track is a Remix (Different Artist)
        /// </summary>
        public bool isRemix { get; set; }
        /// <summary>
        /// Marks filter stage in queue
        /// </summary>
        public int search_stage { get; set; }

    }
}

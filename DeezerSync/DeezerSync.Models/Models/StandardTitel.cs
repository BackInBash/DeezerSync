using System;
using System.Collections.Generic;
using System.Text;

namespace DeezerSync.Core.Models
{
    public partial class StandardTitle
    {
        public string description { get; set; }             // Description of this track
        public int duration { get; set; }                   // The track duration
        public string genre { get; set; }                   // The track genre
        public string title { get; set; }                   // The track title
        public string username { get; set; }                // The username from the uploader
        public string labelname { get; set; }               // The lable of the uploader
        public long id { get; set; }                        // Track ID
        public bool isRemix { get; set; }                   // is Track a Remix
        public int search_stage { get; set; }               // set search stage

    }
}

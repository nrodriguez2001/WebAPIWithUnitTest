using System.Collections.Generic;

namespace VevoAPI.Models
{
    public class ArtistModel
    {
        public int ArtistId { get; set; }

        public string SafeName { get; set; }

        public string ArtistName { get; set; }

        public IEnumerable<Video> Videos;

    }
}
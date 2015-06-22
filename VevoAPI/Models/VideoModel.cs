namespace VevoAPI.Models
{
    public class VideoModel
    {
        public int VideoId { get; set; }

        public int ArtistId { get; set; }

        public string isrc { get; set; }

        public string ShortTitle { get; set; }

        public string VideoTitle { get; set; }
    }
}
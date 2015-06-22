namespace VevoAPI.Models
{
    public class ModelFactory
    {
        public ArtistModel Create(Artist artist)
        {
            return new ArtistModel()
                       {
                           ArtistId = artist.artist_id,
                           SafeName = artist.urlSafeName,
                           ArtistName = artist.name,
                           Videos = artist.Videos
                       };
        }

        public VideoModel Create(Video video)
        {
            return new VideoModel()
            {
                VideoId = video.video_id,
                isrc = video.isrc,
                ShortTitle = video.urlSafeTitle,
                VideoTitle = video.VideoTitle,
                ArtistId = video.artist_id
            };
        }
    }
}
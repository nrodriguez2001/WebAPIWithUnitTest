using VevoAPI.Models;
using System.Collections.Generic;
using System.Configuration;

namespace VevoAPI.Helpers
{
    public class ArtistCrud : IArtistCrud
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public  IEnumerable<Artist> Get()
        {
            var criteria = StoredProc.GetList<Artist>("udp_Artists_lst", this.connectionString);
            return criteria;
        }

        public  Artist Get(int id)
        {
            var artist = StoredProc.GetSingleRecord<Artist>("udp_Artists_sel", "@artist_id", id, this.connectionString);
            if (artist.artist_id == 0) return null;
            return artist;
        }

        public IEnumerable<Video> GetVideosByArtists(int artistId)
        {
            var videosByArtist = StoredProc.GetListVideosByArtist<Video>("udp_VideosByArtist_sel", "@artist_id", artistId, this.connectionString);
            return videosByArtist;
        }

        public Artist AddOrUpdate(Artist artist)
        {
            var retRecord = StoredProc.AddOrUpdateArtists("udp_Artists_ups", artist, this.connectionString);
            return retRecord;
        }

        public int Delete(int id)
        {
            var result = StoredProc.DeleteRecord("udp_Artists_del", "@artist_id", id, this.connectionString);
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using VevoAPI.Helpers;

namespace VevoAPI.Models
{
    public class ArtistRepository : IArtistRepository
    {
        readonly ArtistCrud artistCrud = new ArtistCrud();

        public IEnumerable<Artist> GetAll()
        {
            var artistsx = this.artistCrud.Get();
            return artistsx;
        }

        public Artist Get(int id)
        {
            var artistsx = this.artistCrud.Get(id);
            if (artistsx != null)
            {
                artistsx.Videos = this.artistCrud.GetVideosByArtists(id);
            }
            
            return artistsx;
        }

        public Artist Add(Artist item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            var rec = this.artistCrud.AddOrUpdate(item);

            return rec;
        }

        public int Remove(int id)
        {
            var result = this.artistCrud.Delete(id);
            return result;
        }

        public Artist Update(Artist item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            var record = this.Get(item.artist_id);

            if (record == null || record.artist_id == 0)
            {
                return record;
            }
            item = this.artistCrud.AddOrUpdate(item);
            return item;
          }
    }
}
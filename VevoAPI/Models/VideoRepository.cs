using System;
using System.Collections.Generic;
using VevoAPI.Helpers;

namespace VevoAPI.Models
{
    public class VideoRepository : IVideoRepository
    {
        readonly VideoCrud videoCrud = new VideoCrud();

        public IEnumerable<Video> GetAll()
        {
            var artistsx = this.videoCrud.Get();
            return artistsx;
        }

        public Video Get(int id)
        {
            var videosx = this.videoCrud.Get(id);
            return videosx;
        }

        public Video Add(Video item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            var rec = this.videoCrud.AddOrUpdate(item);

            return rec;
        }

        public int Remove(int id)
        {
            int result = this.videoCrud.Delete(id);
            return result;
        }

        public Video Update(Video item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            var record = this.Get(item.video_id);

            if (record.artist_id == 0)
            {
                return record;
            }
            item = this.videoCrud.AddOrUpdate(item);
            return item;
        }
    }
}
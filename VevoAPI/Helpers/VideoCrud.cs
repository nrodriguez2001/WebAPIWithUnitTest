using System.Collections.Generic;
using System.Configuration;
using VevoAPI.Models;

namespace VevoAPI.Helpers
{
    public class VideoCrud : IVideoCrud
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public IEnumerable<Video> Get()
        {
            var criteria = StoredProc.GetList<Video>("udp_Videos_lst", this.connectionString);
            return criteria;
        }

        public Video Get(int id)
        {
            var criteria = StoredProc.GetSingleRecord<Video>("udp_Videos_sel", "@video_id", id, this.connectionString);
            return criteria;
        }

        public Video AddOrUpdate(Video video)
        {
            var retRecord = StoredProc.AddOrUpdateVideos("udp_Videos_ups", video, this.connectionString);
            return retRecord;
        }

        public int Delete(int id)
        {
            var result = StoredProc.DeleteRecord("udp_Videos_del", "@video_id", id, this.connectionString);
            return result;
        }
    }
}

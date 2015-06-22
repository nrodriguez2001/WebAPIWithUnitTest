using System.Collections.Generic;

using VevoAPI.Models;

namespace VevoAPI.Helpers
{
    public interface IVideoCrud
    {
        IEnumerable<Video> Get();

        Video Get(int id);

        Video AddOrUpdate(Video video);

        int Delete(int id);
    }
}
using System.Collections.Generic;

namespace VevoAPI.Models
{
    public interface IVideoRepository
    {
        IEnumerable<Video> GetAll();
        Video Get(int id);
        Video Add(Video item);
        int Remove(int id);
        Video Update(Video item);
    }
}
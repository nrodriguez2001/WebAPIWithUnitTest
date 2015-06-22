using System.Collections.Generic;

namespace VevoAPI.Models
{
    public interface IArtistRepository
    {
        IEnumerable<Artist> GetAll();
        Artist Get(int id);
        Artist Add(Artist item);
        int Remove(int id);
        Artist Update(Artist item);
    }
}
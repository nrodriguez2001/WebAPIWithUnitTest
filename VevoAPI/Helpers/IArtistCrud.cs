using System.Collections.Generic;

using VevoAPI.Models;

namespace VevoAPI.Helpers
{
    public interface IArtistCrud
    {
        IEnumerable<Artist> Get();

        Artist Get(int id);

        Artist AddOrUpdate(Artist artist);

        int Delete(int id);
    }
}
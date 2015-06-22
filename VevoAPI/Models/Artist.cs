using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VevoAPI.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Artist
    {
        [DataMember(IsRequired = true)] 
        public int artist_id { get; set; }
        [DataMember(IsRequired = true)] 
        public string urlSafeName { get; set; }
        [DataMember(IsRequired = true)] 
        public string name { get; set; }

        public IEnumerable<Video> Videos;
    }
}
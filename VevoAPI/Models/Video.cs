using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace VevoAPI.Models
{
    [DataContract]
    public class Video
    {
        [DataMember(IsRequired = true)]
        public int video_id { get; set; }
        [DataMember(IsRequired = true)] 
        public int artist_id { get; set; }
        [DataMember(IsRequired = true)] 
        public string isrc { get; set; }
        [DataMember(IsRequired = true)] 
        public string urlSafeTitle { get; set; }
        [DataMember(IsRequired = true)] 
        public string VideoTitle { get; set; }
    }
}
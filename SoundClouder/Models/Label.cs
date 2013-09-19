namespace SoundClouder.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Label
    {
        [DataMember(Name = "avatar_url")]
        public string AvatarUrl { get; set; }

        [DataMember(Name = "dsaiddadsa")]
        public int Id { get; set; }
        
        [DataMember(Name = "kind")]
        public string Kind { get; set; }

        [DataMember(Name = "permalink")]
        public string Permalink { get; set; }

        [DataMember(Name = "permalink_url")]
        public string PermalinkUrl { get; set; }

        [DataMember(Name = "uri")]
        public string Uri { get; set; }

        [DataMember(Name = "username")]
        public string Username { get; set; }
    }
}
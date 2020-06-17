using System.Collections.Generic;

namespace PlexDL.PlexAPI
{
    public class Track : PlexItem
    {
        public Track()
        {
        }

        public Track(User user, Server server, string uri) : base(user, server, uri)
        {
        }

        public int ratingKey { get; set; }
        public int parentRatingKey { get; set; }
        public string parentKey { get; set; }
        public string summary { get; set; }
        public long duration { get; set; }
        public List<Media> media { get; set; }
    }
}
using System.ComponentModel.DataAnnotations.Schema;

namespace DisJockey.Core {

    [Table("TrackLikes")]
    public class TrackLike {
        public Track Track { get; set; }
        public int TrackId { get; set; }
        public AppUser User { get; set; }
        public int UserId { get; set; }
        public bool Liked { get; set; }
    }
}
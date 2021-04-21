using System;

namespace API.DTOs {
    public class MemberTrackDto {
        public string YoutubeId { get; set; }
        public DateTime FirstPlayed { get; set; }
        public DateTime LastPlayed { get; set; }
        public int TimesPlayed { get; set; }
    }
}
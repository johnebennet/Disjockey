using System;
using System.Collections.Generic;

namespace API.DTOs
{
    public class TrackUsersDto
    {
        public string YoutubeId { get; set; }
        public DateTime CreatedOn { get; set; }
        public ICollection<UserDto> Users { get; set; }
    }
}
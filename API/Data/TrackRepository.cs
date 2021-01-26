using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data {
    public class TrackRepository : ITrackRepository {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public TrackRepository(DataContext context, IMapper mapper) {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Track> GetTrackByIdAsync(int id) {
            return await _context.Tracks.FindAsync(id);
        }

        public async Task<Track> GetTrackByYoutubeIdAsync(string youtubeId) {
            return await _context.Tracks
                .Include(x => x.AppUsers)
                .Include(x => x.Likes)
                .FirstOrDefaultAsync(x => x.YoutubeId == youtubeId);
        }

        public async Task<IEnumerable<TrackUsersDto>> GetTracksAsync() {
            var userTracks = await _context.Tracks
                .Include(t => t.AppUsers).ThenInclude(ut => ut.User)
                .ProjectTo<TrackUsersDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return userTracks;
        }

        // public async Task<int> GetTrackLikes(Track track, bool liked) {
        //     // var likes = _context.TrackLikes;
            
        //     // return await likes
        //     //     .Where(x => x.Track == track && x.Liked == liked)
        //     //     .CountAsync();

        //     var track = await _context.Tracks
        //         .Include(x => x.Likes)
        //         .FirstOrDefaultAsync(x => x.)

        //     return await _context.Track
        // }

        public async Task<bool> AddTrackLike(Track track, AppUser user, bool liked) {
            // Get the TrackLike record
            var trackLike = await _context.TrackLikes.FirstOrDefaultAsync(x => x.Track == track && x.User == user);

            // If it doesn't exist, create a record
            if (trackLike == null) {
                trackLike = new TrackLikes {
                    Track = track,
                    User = user
                };

                _context.TrackLikes.Add(trackLike);
            }

            // Set the Liked field so it always writes to the DB
            trackLike.Liked = liked;            

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
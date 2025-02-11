using Microsoft.AspNetCore.Mvc;
using reeltok.api.users.DTOs.LikeVideoRequests;
using reeltok.api.users.Entities;
using reeltok.api.users.Interfaces.Services;
using reeltok.api.users.Mappers;
using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.Controllers
{
    [Route("api/likedVideos")]
    [ApiController]
    public class LikeVideoController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly ILikeVideoService _likeVideoService;

        public LikeVideoController(ILikeVideoService likeVideoService, IUsersService usersService)
        {
            _usersService = usersService;
            _likeVideoService = likeVideoService;
        }

        [HttpPost("LikeAVideo")]
        public async Task<IActionResult> LikeVideoAsync([FromBody] LikeVideoRequestDto likeVideo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (likeVideo == null)
            {
                return BadRequest("LikeVideo cannot be null");
            }

            User? existingUser = await _usersService.GetUserByIdAsync(likeVideo.UserId).ConfigureAwait(false);

            if (existingUser == null)
            {
                return BadRequest("User does not exist.");
            }

            LikedDetails likeVideoModel = likeVideo.ToLikeVideoFromCreateDTO();

            LikedVideo likedVideoEntity = new LikedVideo(likeVideoModel);

            bool isLiked = await _likeVideoService.AddToLikedVideosAsync(likedVideoEntity).ConfigureAwait(false);

            if (isLiked)
            {
                return Ok("Video liked successfully!");
            }

            return StatusCode(500, "Failed to like video.");
        }

        [HttpDelete("UnlikeAVideo")]
        public async Task<IActionResult> UnlikeVideoAsync([FromBody] LikeVideoRequestDto likeVideo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (likeVideo.UserId == Guid.Empty || likeVideo.VideoId == Guid.Empty)
            {
                return BadRequest("User Id and Liked Video Id cannot be empty");
            }

            User? existingUser = await _usersService.GetUserByIdAsync(likeVideo.UserId).ConfigureAwait(false);

            if (existingUser == null)
            {
                return BadRequest("User does not exist.");
            }

            bool isUnliked = await _likeVideoService.RemoveFromLikedVideosAsync(likeVideo.UserId, likeVideo.VideoId).ConfigureAwait(false);

            if (isUnliked)
            {
                return Ok("Video unliked successfully!");
            }

            return StatusCode(500, "Failed to unlike video.");
        }
    }
}
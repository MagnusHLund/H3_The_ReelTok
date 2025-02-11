using System.ComponentModel.DataAnnotations;
using reeltok.api.users.Entities;

namespace reeltok.api.users.DTOs.LikeVideoResponses
{
    public class GetAllLikedVideoResponseDto : BaseResponseDto
    {
        [Required]
        public List<Video> Videos { get; private set; }

        public GetAllLikedVideoResponseDto(bool success, List<Video> videos) : base(success)
        {
            Videos = videos;
        }
    }
}
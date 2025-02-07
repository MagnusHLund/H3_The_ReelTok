using System.ComponentModel.DataAnnotations;
using reeltok.api.users.Entities;

namespace reeltok.api.users.DTOs.LikeVideoResponseDTO
{
    internal class GetAllLikedVideoResponseDto : BaseResponseDto
    {
        [Required]
        internal List<Video> Videos { get; private set; }

        internal GetAllLikedVideoResponseDto(bool success, List<Video> videos) : base(success)
        {
            Videos = videos;
        }
    }
}
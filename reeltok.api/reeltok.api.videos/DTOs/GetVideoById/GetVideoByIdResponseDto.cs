
using Newtonsoft.Json;
using reeltok.api.videos.Entities;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.videos.DTOs.GetVideoById
{
    public class GetVideoByIdResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty]
        public VideoEntity Video { get; set; }

        public GetVideoByIdResponseDto(VideoEntity video, bool success = true) : base(success)
        {
            Video = video;
        }
    }
}
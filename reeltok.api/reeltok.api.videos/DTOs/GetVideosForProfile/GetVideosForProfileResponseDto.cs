using Newtonsoft.Json;
using reeltok.api.videos.Entities;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.videos.DTOs.GetVideosForProfile
{
    public class GetVideosForProfileResponseDto
    {
        [Required]
        [JsonProperty("Videos")]
        public List<BaseVideoEntity> ProfileVideos { get; set; }

        public GetVideosForProfileResponseDto(List<BaseVideoEntity> profileVideos)
        {
            ProfileVideos = profileVideos;
        }
    }
}

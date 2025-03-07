using reeltok.api.auth.DTOs;

namespace reeltok.api.recommendations.DTOs
{
    public class AlgorithmResponseDTO : BaseResponseDto
    {
        public List<Guid> VideoIds { get; set; }

        public AlgorithmResponseDTO(List<Guid> videoIds, bool success = true) : base(success)
        {
            VideoIds = videoIds;
        }
    }
}

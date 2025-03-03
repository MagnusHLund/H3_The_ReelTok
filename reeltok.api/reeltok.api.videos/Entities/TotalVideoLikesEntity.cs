namespace reeltok.api.videos.Entities
{
    public class TotalVideoLikesEntity
    {
        public Guid VideoId { get; set; }
        public uint TotalLikes { get; set; }

        public TotalVideoLikesEntity(Guid videoId, uint totalLikes)
        {
            VideoId = videoId;
            TotalLikes = totalLikes;
        }
    }
}
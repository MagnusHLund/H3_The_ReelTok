
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.users.Entities
{
    internal class Video
    {
        [Required]
        internal Guid VideoId { get; set; }
        [Required]
        internal string Title { get; set; }

        internal Video(Guid videoId, string title){
            VideoId = videoId;
            Title = title;
        }
    }
}
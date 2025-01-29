using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.videos.DTOs
{
    internal class EditVideoInfoRequestDto
    {
        internal Guid UserId { get; private set; }
        internal string Title { get; private set; }
        internal string Description { get; private set; }
        internal string Tag { get; private set; }
        internal IFormFile VideoFile { get; private set; }

        internal EditVideoInfoRequestDto(Guid userId, string title, string description, string tag, IFormFile videofile)
        {
            UserId = userId;
            Title = title;
            Description = description;
            Tag = tag;
            VideoFile = videofile;
        }
    }
}
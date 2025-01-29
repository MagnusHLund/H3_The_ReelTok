using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.recommendations.DTOs
{
    internal class EditRecommendationDto
    {
        internal string? Category { get; private set; }
        internal Guid? UserId { get; private set; }

        internal EditRecommendationDto(string category, Guid userId)
        {
            Category = category;
            UserId = userId;
        }
    }
}


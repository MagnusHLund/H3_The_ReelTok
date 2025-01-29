using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.recommendations.DTOs
{
    internal class UpdateRecommendationDto
    {
        internal string? Category { get; private set; }
        internal Guid? UserId { get; private set; }

        internal UpdateRecommendationDto(string category, Guid userId)
        {
            Category = category;
            UserId = userId;
        }
    }
}
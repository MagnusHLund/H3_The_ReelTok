using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using reeltok.api.recommendations.Data;
using reeltok.api.recommendations.Utils;
using reeltok.api.recommendations.Interfaces.Repositories;

namespace reeltok.api.recommendations.Repositories
{
    public class RecommendationsRepository : IRecommendationsRepository
    {
        private readonly RecommendationDbContext _context;

        public RecommendationsRepository(RecommendationDbContext context)
        {
            _context = context;

        }

        public async Task<List<Guid>> GetRecommendedVideosByUserAsync(Guid userId, byte amountOfVideos)
        {
            string sqlQuery = SqlUtils.GetRecommendedVideosByUser();

            List<Guid> videoIds = new List<Guid>();

            using (DbCommand command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sqlQuery;

                var userIdParam = new SqlParameter("@UserId", userId);
                var amountParam = new SqlParameter("@Amount", amountOfVideos);

                command.Parameters.Add(userIdParam);
                command.Parameters.Add(amountParam);

                await _context.Database.OpenConnectionAsync().ConfigureAwait(false);

                using (DbDataReader result = await command.ExecuteReaderAsync().ConfigureAwait(false))
                {
                    while (await result.ReadAsync().ConfigureAwait(false))
                    {
                        videoIds.Add(result.GetGuid(0));
                    }
                }
            }

            return videoIds;
        }
    }
}
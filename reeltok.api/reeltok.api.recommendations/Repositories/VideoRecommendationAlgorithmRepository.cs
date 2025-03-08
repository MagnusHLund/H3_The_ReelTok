using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using reeltok.api.recommendations.Data;
using reeltok.api.recommendations.Interfaces.Repositories;

namespace reeltok.api.recommendations.Repositories
{
    public class VideoRecommendationAlgorithmRepository : IVideoRecommendationAlgorithmRepository
    {
        private readonly RecommendationDbContext _context;

        public VideoRecommendationAlgorithmRepository(RecommendationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Guid>> GetTopVideoByUserInterestAsync(Guid userId, int amount)
        {
            string sqlQuery =
                @"DECLARE @currentTime BIGINT = DATEDIFF(SECOND, '1970-01-01', GETUTCDATE());

          WITH UserInterestCategories AS (
              SELECT cui.CategoryId
              FROM CategoryUserInterests cui
              JOIN UserInterests ui ON ui.UserInterestId = cui.UserInterestId
              WHERE ui.UserId = @UserId
          ),

          VideoScores AS (
              SELECT
                  vc.VideoId,
                  CASE
                      WHEN cvc.CategoryId IN (SELECT CategoryId FROM UserInterestCategories)
                          THEN 1.0
                      ELSE 0.3
                  END AS CategoryMultiplier,
                  1.0 /
                  ((1.0 + ISNULL(vw.TotalTimesWatched, 0))
                  * (1.0 + ABS(@currentTime - ISNULL(vw.LastWatchedTime, @currentTime)))) AS RawScore
              FROM VideoCategories vc
              LEFT JOIN CategoryVideoCategories cvc ON cvc.VideoCategoryId = vc.VideoCategoryId
              LEFT JOIN (
                  SELECT
                      VideoId,
                      SUM(TimesWatched) AS TotalTimesWatched,
                      MAX(Timestamp) AS LastWatchedTime
                  FROM VideoWatched
                  WHERE UserId = @UserId
                  GROUP BY VideoId
              ) vw ON vw.VideoId = vc.VideoId
          )

          SELECT TOP (@Amount) VideoId
          FROM VideoScores
          ORDER BY RawScore DESC;";

            try
            {
                // Execute the raw SQL query and retrieve the data
                List<Guid> videoIds = new List<Guid>();
                using (DbCommand command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = sqlQuery;
                    command.Parameters.Add(new SqlParameter("@UserId", userId));
                    command.Parameters.Add(new SqlParameter("@Amount", amount));

                    _context.Database.OpenConnection();
                    using (DbDataReader result = await command.ExecuteReaderAsync())
                    {
                        while (await result.ReadAsync())
                        {
                            videoIds.Add(result.GetGuid(0));
                        }
                    }
                }

                // Check if any videos were returned
                if (!videoIds.Any())
                {
                    return new List<Guid>();
                }

                return videoIds;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("I DON'T KNOW MAN WHAT IS IT? ASK CHATGPT NO SORRY ASK COPILOT IT HELP MUCH BETTER");
                throw;
            }
        }

    }
}

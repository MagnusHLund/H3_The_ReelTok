namespace reeltok.api.recommendations.Utils
{
    internal static class SqlUtils
    {
        internal static string GetRecommendedVideosByUser()
        {
            return @"
                DECLARE @currentTime BIGINT = DATEDIFF(SECOND, '1970-01-01', GETUTCDATE());
                DECLARE @MatchingCategoryMultiplier FLOAT = 1.0;
                DECLARE @DifferentCategoryMultiplier FLOAT = 0.3;

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
                                THEN @MatchingCategoryMultiplier
                            ELSE @DifferentCategoryMultiplier
                        END *
                        ((1.0 / (1.0 + ISNULL(vw.TotalTimeWatched, 0)))
                        * (1.0 + ABS(@currentTime - ISNULL(vw.LastWatchedTime, @currentTime)))) AS RawScore
                    FROM VideoCategories vc
                    LEFT JOIN CategoryVideoCategories cvc ON cvc.VideoCategoryId = vc.VideoCategoryId
                    LEFT JOIN (
                        SELECT
                            VideoId,
                            SUM(WatchCount) AS TotalTimeWatched,
                            MAX(LastWatchedAt) AS LastWatchedTime
                        FROM WatchedVideos
                        WHERE UserId = @UserId
                        GROUP BY VideoId
                    ) vw ON vw.VideoId = vc.VideoId
                )

                SELECT TOP (@Amount) VideoId
                FROM VideoScores
                ORDER BY RawScore DESC;
            ";
        }
    }
}

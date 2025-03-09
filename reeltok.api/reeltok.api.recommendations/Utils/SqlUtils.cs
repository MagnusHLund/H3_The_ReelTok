namespace reeltok.api.recommendations.Utils
{
    internal static class SqlUtils
    {
        internal static string GetRecommendedVideosByUser()
        {
            // TODO: Make this more readable.

            return @"
                DECLARE @currentTime BIGINT = DATEDIFF(SECOND, '1970-01-01', GETUTCDATE());

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
                        ((1.0 + ISNULL(vw.TotalTimeWatched, 0))
                        * (1.0 + ABS(@currentTime - ISNULL(vw.LastWatchedTime, @currentTime)))) AS RawScore
                    FROM VideoCategories vc
                    LEFT JOIN CategoryVideoCategories cvc ON cvc.VideoCategoryId = vc.VideoCategoryId
                    LEFT JOIN (
                        SELECT
                            VideoId,
                            SUM(TimeWatched) AS TotalTimeWatched,
                            MAX(Timestamp) AS LastWatchedTime
                        FROM VideoWatched
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
USE master;
GO
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'VideosDb')
BEGIN
    CREATE DATABASE VideosDb;
END;

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'UsersDb')
BEGIN
    CREATE DATABASE UsersDb;
END;

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'AuthDb')
BEGIN
    CREATE DATABASE AuthDb;
END;

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'CommentsDb')
BEGIN
    CREATE DATABASE CommentsDb;
END;

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'RecommendationsDb')
BEGIN
    CREATE DATABASE RecommendationsDb;
END;

GO


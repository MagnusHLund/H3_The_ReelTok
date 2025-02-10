-- Create Database
CREATE DATABASE AuthDb;
GO

-- Switch to the new database context
USE AuthDb;
GO

-- Create Login for the user
CREATE LOGIN AuthService WITH PASSWORD = 'Kode1234!';
GO

-- Create User for the login in the database
CREATE USER AuthService FOR LOGIN AuthService;
GO

-- Grant db_owner role to the user
ALTER ROLE db_owner ADD MEMBER AuthService;
GO

CREATE OR ALTER PROCEDURE dbo.sp_CreateUser
    @Name NVARCHAR(100),
    @Email NVARCHAR(150),
    @PasswordHash NVARCHAR(255),
    @Phone NVARCHAR(20) = NULL,
    @Role NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Users (Name, Email, PasswordHash, Phone, Role)
    VALUES (@Name, @Email, @PasswordHash, @Phone, @Role);

    SELECT
        Id,
        Name,
        Email,
        PasswordHash,
        Phone,
        Role
    FROM dbo.Users
    WHERE Id = SCOPE_IDENTITY();
END;
GO

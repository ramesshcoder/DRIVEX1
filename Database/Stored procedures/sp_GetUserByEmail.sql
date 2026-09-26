CREATE OR ALTER PROCEDURE dbo.sp_GetUserByEmail
    @Email NVARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        Name,
        Email,
        PasswordHash,
        Phone,
        Role
    FROM dbo.Users
    WHERE Email = @Email;
END;
GO

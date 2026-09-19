CREATE OR ALTER PROCEDURE dbo.sp_GetAllCars
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        c.Id,
        c.BrandId,
        c.CategoryId,
        b.Name AS Brand,
        cat.Name AS Category,
        c.Model,
        c.Year,
        c.PricePerDay,
        c.Transmission,
        c.FuelType,
        c.Seats,
        c.ImageUrl,
        c.IsAvailable
    FROM dbo.Cars c
    INNER JOIN dbo.Brands b ON c.BrandId = b.Id
    INNER JOIN dbo.Categories cat ON c.CategoryId = cat.Id
    ORDER BY c.Id;
END;
GO

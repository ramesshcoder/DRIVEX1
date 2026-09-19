USE Drivex;
GO

INSERT INTO dbo.Brands (Name, Description)
VALUES
    (N'Hyundai', N'Hyundai Motor Company'),
    (N'Kia', N'Kia Corporation'),
    (N'Maruti', N'Maruti Suzuki'),
    (N'Honda', N'Honda Cars'),
    (N'Mahindra', N'Mahindra & Mahindra'),
    (N'Toyota', N'Toyota Motor Corporation');

INSERT INTO dbo.Categories (Name, Description)
VALUES
    (N'SUV', N'Sport utility vehicle'),
    (N'Sedan', N'Sedan'),
    (N'Hatchback', N'Hatchback');

INSERT INTO dbo.Cars (BrandId, CategoryId, Model, Year, PricePerDay, Transmission, FuelType, Seats, ImageUrl, IsAvailable)
VALUES
    (1, 1, N'Creta', 2024, 2800.00, N'Automatic', N'Petrol', 5, N'images/creta.jpg', 1),
    (2, 1, N'Seltos', 2024, 3000.00, N'Automatic', N'Diesel', 5, N'images/seltos.avif', 1),
    (3, 3, N'Swift', 2023, 1500.00, N'Manual', N'Petrol', 5, N'images/swift.jpg', 1),
    (3, 3, N'Baleno', 2023, 1700.00, N'Automatic', N'Petrol', 5, N'images/baleno.jpg', 1),
    (4, 2, N'City', 2024, 2400.00, N'Automatic', N'Petrol', 5, N'images/city.jpg', 1),
    (1, 2, N'Verna', 2024, 2600.00, N'Automatic', N'Petrol', 5, N'images/verna.jpg', 1),
    (5, 1, N'XUV700', 2024, 3500.00, N'Automatic', N'Diesel', 7, N'images/xuv 700.jpg', 1),
    (5, 1, N'Scorpio N', 2024, 3800.00, N'Manual', N'Diesel', 7, N'images/scorpio.jpg', 1),
    (6, 1, N'Innova Crysta', 2023, 4200.00, N'Automatic', N'Diesel', 7, N'images/innova crysta.jpg', 1),
    (6, 1, N'Fortuner', 2024, 5500.00, N'Automatic', N'Diesel', 7, N'images/fortuner.jpg', 1);
GO

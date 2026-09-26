USE Drivex;
GO

IF OBJECT_ID(N'dbo.Reviews', N'U') IS NOT NULL DROP TABLE dbo.Reviews;
IF OBJECT_ID(N'dbo.Payments', N'U') IS NOT NULL DROP TABLE dbo.Payments;
IF OBJECT_ID(N'dbo.Bookings', N'U') IS NOT NULL DROP TABLE dbo.Bookings;
IF OBJECT_ID(N'dbo.Cars', N'U') IS NOT NULL DROP TABLE dbo.Cars;
IF OBJECT_ID(N'dbo.Categories', N'U') IS NOT NULL DROP TABLE dbo.Categories;
IF OBJECT_ID(N'dbo.Brands', N'U') IS NOT NULL DROP TABLE dbo.Brands;
IF OBJECT_ID(N'dbo.Locations', N'U') IS NOT NULL DROP TABLE dbo.Locations;
IF OBJECT_ID(N'dbo.Users', N'U') IS NOT NULL DROP TABLE dbo.Users;
GO

CREATE TABLE dbo.Users
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    Phone NVARCHAR(20) NULL,
    Role NVARCHAR(20) NOT NULL,
    CONSTRAINT UQ_Users_Email UNIQUE (Email)
);

CREATE TABLE dbo.Locations
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Address NVARCHAR(250) NOT NULL,
    City NVARCHAR(100) NOT NULL
);

CREATE TABLE dbo.Brands
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    Description NVARCHAR(200) NULL
);

CREATE TABLE dbo.Categories
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    Description NVARCHAR(200) NULL
);

CREATE TABLE dbo.Cars
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    BrandId INT NOT NULL,
    CategoryId INT NOT NULL,
    Model NVARCHAR(100) NOT NULL,
    Year INT NOT NULL,
    PricePerDay DECIMAL(10,2) NOT NULL,
    Transmission NVARCHAR(20) NOT NULL,
    FuelType NVARCHAR(20) NOT NULL,
    Seats INT NOT NULL,
    ImageUrl NVARCHAR(500) NULL,
    IsAvailable BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_Cars_Brands FOREIGN KEY (BrandId) REFERENCES dbo.Brands (Id),
    CONSTRAINT FK_Cars_Categories FOREIGN KEY (CategoryId) REFERENCES dbo.Categories (Id)
);

CREATE TABLE dbo.Bookings
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    CarId INT NOT NULL,
    PickupLocationId INT NOT NULL,
    ReturnLocationId INT NOT NULL,
    PickupDate DATETIME2 NOT NULL,
    ReturnDate DATETIME2 NOT NULL,
    TotalAmount DECIMAL(10,2) NOT NULL,
    Status NVARCHAR(30) NOT NULL,
    CONSTRAINT FK_Bookings_Users FOREIGN KEY (UserId) REFERENCES dbo.Users (Id),
    CONSTRAINT FK_Bookings_Cars FOREIGN KEY (CarId) REFERENCES dbo.Cars (Id),
    CONSTRAINT FK_Bookings_PickupLocation FOREIGN KEY (PickupLocationId) REFERENCES dbo.Locations (Id),
    CONSTRAINT FK_Bookings_ReturnLocation FOREIGN KEY (ReturnLocationId) REFERENCES dbo.Locations (Id)
);

CREATE TABLE dbo.Payments
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    BookingId INT NOT NULL,
    Amount DECIMAL(10,2) NOT NULL,
    PaymentDate DATETIME2 NOT NULL,
    PaymentMethod NVARCHAR(30) NOT NULL,
    PaymentStatus NVARCHAR(30) NOT NULL,
    CONSTRAINT FK_Payments_Bookings FOREIGN KEY (BookingId) REFERENCES dbo.Bookings (Id)
);

CREATE TABLE dbo.Reviews
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    CarId INT NOT NULL,
    Rating INT NOT NULL,
    Comment NVARCHAR(500) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Reviews_Users FOREIGN KEY (UserId) REFERENCES dbo.Users (Id),
    CONSTRAINT FK_Reviews_Cars FOREIGN KEY (CarId) REFERENCES dbo.Cars (Id)
);
GO

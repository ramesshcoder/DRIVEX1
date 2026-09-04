CREATE TABLE Bookings
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    CarId INT NOT NULL,
    PickupLocationId INT NOT NULL,
    ReturnLocationId INT NOT NULL,
    PickupDate DATETIME2 NOT NULL,
    ReturnDate DATETIME2 NOT NULL,
    TotalAmount DECIMAL(10,2) NOT NULL,
    Status NVARCHAR(30) NOT NULL
);
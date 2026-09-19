using Drivex.Domain.Cars;
using Drivex.Repositories.Base;
using Microsoft.Data.SqlClient;

namespace Drivex.Repositories.Cars;

public class CarRepository : BaseSqlRepository, ICarRepository
{
    public CarRepository(IConfiguration configuration)
        : base(configuration)
    {
    }

    public Task<List<Car>> GetAllCarsAsync(CancellationToken cancellationToken = default)
    {
        return QueryStoredProcedureAsync("sp_GetAllCars", MapCar, cancellationToken: cancellationToken);
    }

    public Task<Car?> GetCarByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return QueryStoredProcedureSingleAsync(
            "sp_GetCarById",
            MapCar,
            command => command.Parameters.Add(new SqlParameter("@Id", id)),
            cancellationToken);
    }

    private static Car MapCar(SqlDataReader reader)
    {
        return new Car
        {
            Id = GetRequired<int>(reader, "Id"),
            BrandId = GetRequired<int>(reader, "BrandId"),
            CategoryId = GetRequired<int>(reader, "CategoryId"),
            Brand = GetRequired<string>(reader, "Brand"),
            Category = GetRequired<string>(reader, "Category"),
            Model = GetRequired<string>(reader, "Model"),
            Year = GetRequired<int>(reader, "Year"),
            PricePerDay = GetRequired<decimal>(reader, "PricePerDay"),
            Transmission = GetRequired<string>(reader, "Transmission"),
            FuelType = GetRequired<string>(reader, "FuelType"),
            Seats = GetRequired<int>(reader, "Seats"),
            ImageUrl = GetOptional<string>(reader, "ImageUrl"),
            IsAvailable = GetRequired<bool>(reader, "IsAvailable")
        };
    }
}

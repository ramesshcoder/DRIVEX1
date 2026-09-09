using System.Data;
using Drivex.Domain.Cars;
using Drivex.Repositories.Base;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Drivex.Repositories.Cars;

public class CarRepository : BaseSqlRepository, ICarRepository
{
    public CarRepository(IConfiguration configuration)
        : base(configuration)
    {
    }

    public async Task<List<Car>> GetAllCarsAsync()
    {
        var cars = new List<Car>();

        using var connection = CreateConnection();

        using var command = new SqlCommand(
            "sp_GetAllCars",
            connection);

        command.CommandType = CommandType.StoredProcedure;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var car = new Car
            {
                Id = Convert.ToInt32(reader["Id"]),
                BrandId = Convert.ToInt32(reader["BrandId"]),
                CategoryId = Convert.ToInt32(reader["CategoryId"]),
                Model = reader["Model"].ToString() ?? string.Empty,
                Year = Convert.ToInt32(reader["Year"]),
                PricePerDay = Convert.ToDecimal(reader["PricePerDay"]),
                Transmission = reader["Transmission"].ToString() ?? string.Empty,
                FuelType = reader["FuelType"].ToString() ?? string.Empty,
                Seats = Convert.ToInt32(reader["Seats"]),
                ImageUrl = reader["ImageUrl"] == DBNull.Value
                    ? null
                    : reader["ImageUrl"].ToString(),
                IsAvailable = Convert.ToBoolean(reader["IsAvailable"])
            };

            cars.Add(car);
        }

        return cars;
    }
}
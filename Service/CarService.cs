using Drivex.DTOs.Cars;
using Drivex.Repositories.Cars;

namespace Drivex.Services.Cars;

public class CarService : ICarService
{
    private readonly ICarRepository _carRepository;

    public CarService(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public async Task<List<CarDto>> GetAllCarsAsync()
    {
        var cars = await _carRepository.GetAllCarsAsync();

        return cars.Select(car => new CarDto
        {
            Id = car.Id,
            Brand = car.BrandId.ToString(),
            Model = car.Model,
            Category = car.CategoryId.ToString(),
            Year = car.Year,
            PricePerDay = car.PricePerDay,
            Transmission = car.Transmission,
            FuelType = car.FuelType,
            Seats = car.Seats,
            ImageUrl = car.ImageUrl,
            IsAvailable = car.IsAvailable
        }).ToList();
    }
}
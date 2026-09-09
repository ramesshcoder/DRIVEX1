using Drivex.DTOs.Cars;

namespace Drivex.Services.Cars;

public interface ICarService
{
    Task<List<CarDto>> GetAllCarsAsync();
}
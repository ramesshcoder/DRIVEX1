using Drivex.Domain.Cars;

namespace Drivex.Repositories.Cars;

public interface ICarRepository
{
    Task<List<Car>> GetAllCarsAsync();
}
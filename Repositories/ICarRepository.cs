using Drivex.Domain.Cars;

namespace Drivex.Repositories.Cars;

public interface ICarRepository
{
    Task<List<Car>> GetAllCarsAsync(CancellationToken cancellationToken = default);
    Task<Car?> GetCarByIdAsync(int id, CancellationToken cancellationToken = default);
}

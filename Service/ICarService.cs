using Drivex.Domain.Cars;

namespace Drivex.Services.Cars;

public interface ICarService
{
    Task<List<Car>> GetAllCarsAsync(CancellationToken cancellationToken = default);
    Task<Car?> GetCarByIdAsync(int id, CancellationToken cancellationToken = default);
}

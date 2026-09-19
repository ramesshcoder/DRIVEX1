using AutoMapper;
using Drivex.DTOs.Cars;
using Drivex.Services.Cars;
using Microsoft.AspNetCore.Mvc;

namespace Drivex.Controllers;

[ApiController]
[Route("api/cars")]
public class CarsController : ControllerBase
{
    private readonly ICarService _carService;
    public readonly IMapper _mapster;

    public CarsController(ICarService carService,
        IMapper mapster)
    {
        _carService = carService;
        _mapster = mapster;
    }

    [HttpGet]
    public async Task<ActionResult<List<CarDto>>> GetAll(CancellationToken cancellationToken)
    {
        var cars = await _carService.GetAllCarsAsync(cancellationToken);
        return Ok(_mapster.Map<List<CarDto>>(cars));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CarDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var car = await _carService.GetCarByIdAsync(id, cancellationToken);
        if (car is null)
        {
            return NotFound();
        }

        return _mapster.Map<CarDto>(car);
    }
}

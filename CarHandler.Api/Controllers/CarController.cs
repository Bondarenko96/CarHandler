using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarHandler.Controllers;

[ApiController]
[Route("[controller]")]
public class CarController(CarService carService) : ControllerBase
{
    [HttpPost, Route("create"), ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create([FromBody] CreateCarDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var car = await carService.CreateAsync(dto, cancellationToken);
            return Ok(car);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet, Route("get"), ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Get([FromBody] GetCarDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var car = await carService.GetAsync(dto, cancellationToken);
            if (car == null)
                return NoContent();
                
            return Ok(car);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete, Route("delete/{id:int}"), ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            await carService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut, Route("update"), ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Update([FromBody] UpdateCarDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var car = await carService.UpdateAsync(dto, cancellationToken);
            return Ok(car);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpHead, Route("check/{licensePlate}"), ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult Check(string licensePlate)
    {
        var exists = carService.Check(licensePlate);
        if (exists)
            return Ok();

        return BadRequest("Такого номера нет в базе");
    }
}

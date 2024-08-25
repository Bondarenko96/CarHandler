using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeHandler.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController(EmployeeService employeeService) : ControllerBase
{
    [HttpPost, Route("create"), ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create([FromBody] CreateEmployeeDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var employee = await employeeService.CreateAsync(dto, cancellationToken);
            return Ok(employee);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet, Route("get"), ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Get([FromBody] GetEmployeeDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var employee = await employeeService.GetAsync(dto, cancellationToken);
            if (employee == null)
                return NoContent();

            return Ok(employee);
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
            await employeeService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut, Route("update"), ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Update([FromBody] UpdateEmployeeDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var employee = await employeeService.UpdateAsync(dto, cancellationToken);
            return Ok(employee);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

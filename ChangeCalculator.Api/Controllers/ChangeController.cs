using ChangeCalculator.Core.Models;
using ChangeCalculator.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChangeCalculator.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChangeController : ControllerBase
{
    private readonly IChangeCalculatorService _service;

    public ChangeController(IChangeCalculatorService service)
    {
        _service = service;
    }

    public sealed class CalculateRequest
    {
        public decimal Amount { get; set; }
    }

    /// <summary>
    /// Calculates the minimum number of notes and coins for the given amount (ZAR).
    /// </summary>
    [HttpPost("calculate")]
    [ProducesResponseType(typeof(ChangeResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public ActionResult<ChangeResult> Calculate([FromBody] CalculateRequest request)
    {
        if (request is null)
            return BadRequest(new { error = "Request body is required." });

        try
        {
            var result = _service.Calculate(request.Amount);
            return Ok(result);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}

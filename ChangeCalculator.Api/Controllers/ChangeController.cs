using ChangeCalculator.Core.Models;
using ChangeCalculator.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
        // Make it nullable so "missing" is detectable
        [BindRequired]                         // require the field to be present
        [JsonNumberHandling(JsonNumberHandling.Strict)] // reject "786.80" (string)
        [Range(0, 1_000_000_000, ErrorMessage = "Amount must be between 0 and 1,000,000,000.")]
        public decimal? Amount { get; set; }
    }

    /// Calculates the minimum number of notes and coins for the given amount (ZAR).
    [HttpPost("calculate")]
    [ProducesResponseType(typeof(ChangeResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public ActionResult<ChangeResult> Calculate([FromBody] CalculateRequest request)
    {
        // With [ApiController], invalid ModelState returns 400 automatically, but we keep a guard:
        if (!ModelState.IsValid || request is null || request.Amount is null)
            return ValidationProblem(ModelState);

        try
        {
            var result = _service.Calculate(request.Amount.Value);
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

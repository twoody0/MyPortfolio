using Microsoft.AspNetCore.Mvc;
using PortfolioBackend.Models;

namespace PortfolioBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactController : ControllerBase
{
    [HttpPost]
    public IActionResult Post([FromBody] ContactRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                success = false,
                message = "Validation failed",
                errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
            });
        }

        Console.WriteLine($"Received contact from {request.Name} <{request.Email}>: {request.Message}");
        return Ok(new { success = true, message = "Message received!" });
    }
}

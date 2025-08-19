using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Microsoft.AspNetCore.Mvc;
using PortfolioBackend.Helpers;
using PortfolioBackend.Models;
using System.Threading.Tasks;

namespace PortfolioBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactController : ControllerBase
{
    private readonly IAmazonSimpleEmailService _ses;

    public ContactController(IAmazonSimpleEmailService ses)
    {
        _ses = ses;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ContactRequest request)
    {
        if (!ModelState.IsValid)
        {
            // Return specific validation problems; do not echo raw values
            return ValidationProblem(ModelState);
        }

        // Sanitize
        var safeName = InputSanitizer.StripCrlf(InputSanitizer.SafeText(request.Name));
        var safeEmail = InputSanitizer.SafeText(request.Email);   // Address validated by [EmailAddress]
        var safeMessage = InputSanitizer.RemoveControlChars(request.Message ?? string.Empty).Trim();

        // Optional: prevent obvious header injection via email field like "a@b.com\r\nBcc:..."
        if (safeEmail.Contains("\r") || safeEmail.Contains("\n"))
        {
            return BadRequest(new { success = false, message = "Invalid email." });
        }

        string subject = $"New Contact Form Message from {safeName}";
        string body = $"Name: {safeName}\nEmail: {safeEmail}\nMessage:\n{safeMessage}";

        var sendRequest = new SendEmailRequest
        {
            Source = "noreply@tyler-woody.dev", // Must be verified in SES
            Destination = new Destination { ToAddresses = new List<string> { "tylerjameswoody@gmail.com" } },
            Message = new Message
            {
                Subject = new Content(subject),
                Body = new Body { Text = new Content(body) }
            }
        };

        try
        {
            var response = await _ses.SendEmailAsync(sendRequest);
            // Log with sanitized values only (no raw copies)
            Console.WriteLine($"Email sent. SES MessageId={response.MessageId}");
            return Ok(new { success = true, message = "Message sent successfully!" });
        }
        catch (Exception ex)
        {
            // Avoid logging PII; log minimal identifiers
            Console.WriteLine($"SES send failed: {ex.GetType().Name}");
            return StatusCode(500, new { success = false, message = "Failed to send email." });
        }
    }
}

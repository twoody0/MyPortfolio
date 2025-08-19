using Microsoft.AspNetCore.Mvc;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

namespace PortfolioBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactController : ControllerBase
{
    // DTO now includes the Turnstile token
    public record ContactRequest(string Name, string Email, string Message, string CfTurnstileToken);

    private readonly IAmazonSimpleEmailService _ses;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _config;

    public ContactController(IAmazonSimpleEmailService ses, IHttpClientFactory httpClientFactory, IConfiguration config)
    {
        _ses = ses;
        _httpClientFactory = httpClientFactory;
        _config = config;
    }

    [HttpPost]
    public async Task<IActionResult> Post(ContactRequest request)
    {
        // Step 1: Verify Turnstile
        var secretKey = _config["Turnstile:SecretKey"];
        if (string.IsNullOrWhiteSpace(secretKey))
        {
            return StatusCode(500, new { success = false, message = "Turnstile secret key not configured." });
        }

        if (string.IsNullOrWhiteSpace(request.CfTurnstileToken))
        {
            return BadRequest(new { success = false, message = "Missing verification token." });
        }

        var http = _httpClientFactory.CreateClient();
        var response = await http.PostAsync("https://challenges.cloudflare.com/turnstile/v0/siteverify",
            new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "secret", secretKey },
                { "response", request.CfTurnstileToken }
            }));

        var json = await response.Content.ReadAsStringAsync();
        var verification = JsonSerializer.Deserialize<TurnstileVerifyResponse>(json);

        if (verification == null || !verification.Success)
        {
            return BadRequest(new { success = false, message = "Verification failed." });
        }

        // Step 2: Send Email (if verified)
        string subject = $"New Contact Form Message from {request.Name}";
        string body = $"Name: {request.Name}\nEmail: {request.Email}\nMessage:\n{request.Message}";

        var sendRequest = new SendEmailRequest
        {
            Source = "noreply@tyler-woody.dev",
            Destination = new Destination
            {
                ToAddresses = new List<string> { "tylerjameswoody@gmail.com" }
            },
            Message = new Message
            {
                Subject = new Content(subject),
                Body = new Body
                {
                    Text = new Content(body)
                }
            }
        };

        try
        {
            var sesResponse = await _ses.SendEmailAsync(sendRequest);
            Console.WriteLine($"Email sent! Message ID: {sesResponse.MessageId}");
            return Ok(new { success = true, message = "Message sent successfully!" });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
            return StatusCode(500, new { success = false, message = "Failed to send email." });
        }
    }
}

// Helper class for Turnstile JSON
public class TurnstileVerifyResponse
{
    public bool Success { get; set; }
    public string[]? ErrorCodes { get; set; }
    public float? Score { get; set; }
    public string? Action { get; set; }
    public string? Cdata { get; set; }
}

using Microsoft.AspNetCore.Mvc;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using System.Threading.Tasks;

namespace PortfolioBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactController : ControllerBase
{
    public record ContactRequest(string Name, string Email, string Message);

    private readonly IAmazonSimpleEmailService _ses;

    public ContactController(IAmazonSimpleEmailService ses)
    {
        _ses = ses;
    }

    [HttpPost]
    public async Task<IActionResult> Post(ContactRequest request)
    {
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
            var response = await _ses.SendEmailAsync(sendRequest);
            Console.WriteLine($"Email sent! Message ID: {response.MessageId}");
            return Ok(new { success = true, message = "Message sent successfully!" });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
            return StatusCode(500, new { success = false, message = "Failed to send email." });
        }
    }
}

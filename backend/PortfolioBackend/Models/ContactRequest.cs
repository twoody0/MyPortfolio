using System.ComponentModel.DataAnnotations;

namespace PortfolioBackend.Models;

public class ContactRequest
{
    [Required, StringLength(80)]
    public string Name { get; set; } = "";

    [Required, EmailAddress, StringLength(254)]
    public string Email { get; set; } = "";

    [Required, StringLength(4000)]
    public string Message { get; set; } = "";
}
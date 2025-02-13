using System.ComponentModel.DataAnnotations;

namespace Resort.Web.ViewModels;

public class LoginVM
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
    public bool RememberMe { get; set; }
    public string? RedirectUrl { get; set; }
}

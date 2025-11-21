using System.ComponentModel.DataAnnotations;

public class RegisterModel
{
    [Required, Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; } = "";

    [Required]
    public string Email { get; set; } = "";

    [Required]
    public string Password { get; set; } = "";
}
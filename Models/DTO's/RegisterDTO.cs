using System.ComponentModel.DataAnnotations;

namespace Models.DTO_s;

public class RegisterDTO
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string ConfirmPassword { get; set; }
}
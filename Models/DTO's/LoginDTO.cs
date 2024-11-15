using System.ComponentModel.DataAnnotations;

namespace Models.DTO_s;

public class LoginDTO
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}
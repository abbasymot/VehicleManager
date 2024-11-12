using Microsoft.AspNetCore.Identity;

namespace Models;

public class AppUser : IdentityUser
{
    public List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
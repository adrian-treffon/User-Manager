using Microsoft.AspNetCore.Identity;

namespace Domain
{
  public class AppUser : IdentityUser<int>
  {
    public string PhotoUrl { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Profession { get; set; }
    public string Role { get; set; }

  }
}
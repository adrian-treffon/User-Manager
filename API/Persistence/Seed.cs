using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
  public class Seed
  {
    public static async Task SeedData(UserManager<AppUser> userManager)
    {

      if (!userManager.Users.Any())
      {

        var user = new AppUser
        {
          PhotoUrl = "assets/man.png",
          FirstName = "Adam",
          LastName = "Kowalski",
          Profession = "Dentysta",
          UserName = "user",
          Role = "Casual"
        };

        var admin = new AppUser
        {
          PhotoUrl = "assets/woman.png",
          FirstName = "Monika",
          LastName = "Nowak",
          Profession = "Psycholog",
          UserName = "admin",
          Role = "Admin"

        };

        await userManager.CreateAsync(user, "user");
        await userManager.CreateAsync(admin, "admin");

      }
    }
  }
}
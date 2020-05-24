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

        for (int i = 0; i < 10; i++)
        {
          var user = new AppUser
          {
            PhotoUrl = "http://localhost:5000/resources/images/1.png",
            FirstName = "Adam",
            LastName = "Kowalski",
            Profession = "Dentysta",
            UserName = $"user{i}",
            Role = Roles.Casual
          };

          var admin = new AppUser
          {
            PhotoUrl = "http://localhost:5000/resources/images/2.png",
            FirstName = "Monika",
            LastName = "Nowak",
            Profession = "Psycholog",
            UserName = $"admin{i}",
            Role = Roles.Admin

          };

          await userManager.CreateAsync(user, "user");
          await userManager.CreateAsync(admin, "admin");
        }

      }
    }
  }
}
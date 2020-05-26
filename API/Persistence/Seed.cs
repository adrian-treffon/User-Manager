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

        for (int i = 0; i < 5; i++)
        {
          var user1 = new AppUser
          {
            PhotoUrl = "http://localhost:5000/resources/images/1.png",
            FirstName = "Adam",
            LastName = "Kowalski",
            Profession = "Dentysta",
            UserName = $"user0{i}",
            Role = Roles.Casual
          };

          var user2 = new AppUser
          {
            PhotoUrl = "http://localhost:5000/resources/images/3.png",
            FirstName = "Marek",
            LastName = "Duda",
            Profession = "Murarz",
            UserName = $"user1{i}",
            Role = Roles.Casual
          };

          await userManager.CreateAsync(user1, "user");
          await userManager.CreateAsync(user2, "user");
        }

        var user = new AppUser
          {
            PhotoUrl = "http://localhost:5000/resources/images/4.png",
            FirstName = "Dorota",
            LastName = "Słoneczny",
            Profession = "Uczeń",
            UserName = "user",
            Role = Roles.Casual
          };

         var admin = new AppUser
          {
            PhotoUrl = "http://localhost:5000/resources/images/2.png",
            FirstName = "Monika",
            LastName = "Nowak",
            Profession = "Psycholog",
            UserName = "admin",
            Role = Roles.Admin
          };

          await userManager.CreateAsync(admin, "admin");
          await userManager.CreateAsync(user, "user");

      }
    }
  }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            
            if (!context.Users.Any())
            {
              var users = new List<User>
                {
                  new User{
                    PhotoUrl ="assets/man.png",
                    FirstName = "Adam",
                    LastName = "Kowalski",
                    Profession = "Dentysta",
                  },
                   new User{
                   PhotoUrl = "assets/woman.png",
                   FirstName = "Monika",
                   LastName ="Nowak",
                   Profession = "Psycholog",
                  },
                   new User{
                    PhotoUrl="assets/child.png",
                    FirstName ="Dorota",
                    LastName = "Bogacka",
                    Profession = "Uczeń",
                  },
                };

                await context.Users.AddRangeAsync(users);
                await context.SaveChangesAsync();
            }
        }
    }
}
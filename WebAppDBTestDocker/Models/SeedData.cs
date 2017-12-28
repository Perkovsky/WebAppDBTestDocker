using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace WebAppDBTestDocker.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IServiceProvider services)
        {
            ApplicationDbContext context = services.GetRequiredService<ApplicationDbContext>();

            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User
                    {
                        Name = "Semen",
                        Age = 22,
                        Phone = "(055) 555-55-55"
                    },
                    new User
                    {
                        Name = "Andrew",
                        Age = 37,
                        Phone = "(077) 777-77-77"
                    },
                    new User
                    {
                        Name = "Dima",
                        Age = 28,
                        Phone = "(088) 888-88-88"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}

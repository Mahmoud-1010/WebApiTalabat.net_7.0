using DataAccessLayer.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "Mahmoud Behidy",
                    UserName = "mahmoud.behidy",
                    Email = "mahmoud.behidy@gmail.com",
                    PhoneNumber = "01022931845",
                    Address = new Address()
                    {
                        FirstName = "Mahmoud",
                        LastName = "Behidy",
                        Country = "Egypt",
                        City = "Giza",
                        Street = "10 El Nady St.",
                        ZipCode = "23423"
                    }
                };
                await userManager.CreateAsync(user, "P@ssw0rd");
            }
        }
    }
}

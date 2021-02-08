using MessengerUI.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessengerUI.Data
{
    public class DataSeed
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public DataSeed(ApplicationDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public void SeedData()
        {
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser{
                    Name = "Daemon",
                    Surname = "Griffons",
                    UserName = "daemongriffons@gmail.com",
                    Email = "daemongriffons@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0812189106",
                    PhoneNumberConfirmed = true
                    },
                    new AppUser{
                    Name = "Simangele",
                    Surname = "Maphananga",
                    UserName = "simangeleinno@gmail.com",
                    Email = "simangeleinno@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0678722926",
                    PhoneNumberConfirmed = true
                    },
                     new AppUser{
                    Name = "Mpho",
                    Surname = "Mokhokane",
                    UserName = "mphomokhokane@gmail.com",
                    Email = "mphomokhokane@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0610748491",
                    PhoneNumberConfirmed = true
                    },
                      new AppUser{
                    Name = "Nthabiseng",
                    Surname = "Moela",
                    UserName = "nthabisengmoela1@gmail.com",
                    Email = "nthabisengmoela1@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0747747207",
                    PhoneNumberConfirmed = true
                    },

                };

                foreach(var user in users)
                {
                    userManager.CreateAsync(user, "MyPassword@123").GetAwaiter().GetResult();
                }
            }
        }
    }
}

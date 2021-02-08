using MessengerUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessengerUI.Components
{
    public class UsersListViewComponent : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public UsersListViewComponent(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var users = _userManager.Users.Select(user => new UsersIndexModel
            {
                UserId = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Image = user.ImageUrl
            }).OrderBy(z => z.Name);


            var model = new UsersViewModel
            {
                Users = new PagedList<UsersIndexModel>(users, 1, 150)
            };

            return View(model);
        }
    }
}

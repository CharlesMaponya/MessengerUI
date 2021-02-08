using MessengerUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessengerUI.Components
{
    public class ActiveUserViewComponent : ViewComponent

    {
        private readonly UserManager<AppUser> _userManager;

        public ActiveUserViewComponent(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var activeUser = await _userManager.GetUserAsync((System.Security.Claims.ClaimsPrincipal)User);

            var model = new ActiveUserModel
            {
                Name = activeUser.Name,
                Surname = activeUser.Surname,
                Image = activeUser.ImageUrl
            };
            return View(model);
        }
    }
}

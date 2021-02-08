using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using MessengerUI.Models;
using MessengerUI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MessengerUI.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailSender _emailSender;
        //private readonly UrlEncoder _urlEncoder;
        private readonly IFileManager _fileManager;
        private PhotoSettings _options;

        public ManageController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IEmailSender emailSender, IFileManager fileManager, IOptionsSnapshot<PhotoSettings> options)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _fileManager = fileManager;
            _options = options.Value;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ModelState.AddModelError("", "No such user exists!");
                return View();
            }

            var model = new UserViewModel
            {
                Name = user.Name,
                Surname = user.Surname,
                Country = user.Country,
                Email = user.Email,
                Phone = user.PhoneNumber,
                Bio = user.Bio
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Something went wrong!");
                return View(model);
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ModelState.AddModelError("", "Something went wrong!");
                return View(model);
            }
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Country = model.Country;
            user.Email = model.Email;
            user.EmailConfirmed = true;
            user.PhoneNumber = model.Phone;
            user.PhoneNumberConfirmed = true;
            user.Bio = model.Bio;

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                ModelState.AddModelError("", "Unexpected error occurred while updating your Details");
                return View(model);
            }
            return RedirectToAction(nameof(ManageController.Index), "Manage");
        }



       [HttpGet]
       public IActionResult Security()
        {
            return View();
        }

        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Security(SecurityViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Something went wrong");
                return View(model);
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ModelState.AddModelError("",$"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                return View();
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                Errors(changePasswordResult);
                return View(model);
            }

            await _signInManager.RefreshSignInAsync(user);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Socials()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ModelState.AddModelError("", "No such user exists!");
                return View();
            }

            var model = new SocialsViewModel
            {
                FbUrl = user.FbUrl,
                TwitterUrl = user.TwitterUrl,
                LinkedInUrl = user.LinkedInUrl,
                GithubUrl = user.GithubUrl
            };
            return View(model);
        }

        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Socials(SocialsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Something went wrong!");
                return View(model);
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ModelState.AddModelError("", "Something went wrong!");
                return View(model);
            }

            user.TwitterUrl = model.TwitterUrl;
            user.FbUrl = model.FbUrl;
            user.LinkedInUrl = model.LinkedInUrl;
            user.GithubUrl = model.GithubUrl;

            await _userManager.UpdateAsync(user);
            return RedirectToAction(nameof(ManageController.Socials), "Manage");
        }

        public IActionResult Avator() => View();

        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Avator(AvatorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Something went wrong!");
                return View(model);
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ModelState.AddModelError("", "Something went wrong!");
                return View(model);
            }
            if (model.Avator == null)
            {
                ModelState.AddModelError("", "An Image has to be supplied!!");
                return View(model);
            }
            else
            {
                if (model.Avator.Length > _options.MaxBytes)
                {
                    ModelState.AddModelError("", "Image File Size Exceeded");
                    return View(model);
                }

                if (!_options.IsSupported(model.Avator.FileName))
                {
                    ModelState.AddModelError("", "Invalid File Type");
                    return View(model);
                }
                else
                {
                    if (!String.IsNullOrWhiteSpace(user.ImageUrl))
                    {
                        _fileManager.RemoveImage(user.ImageUrl);
                        user.ImageUrl = _fileManager.SaveImage(model.Avator);
                        var res = await _userManager.UpdateAsync(user);
                        if (res.Succeeded)
                        {
                            return RedirectToAction(nameof(Avator));
                        }
                    }
                    user.ImageUrl = _fileManager.SaveImage(model.Avator);
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Avator));
                    }
                    Errors(result);
                    return View(model);
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);

            if(user == null)
            {
                ModelState.AddModelError("", $"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                return View();
            }
            var model = new UserViewModel
            {
                Name = user.Name,
                Surname = user.Surname,
                Country = user.Country,
                Email = user.Email,
                Phone = user.PhoneNumber,
                Bio = user.Bio,
                Image = user.ImageUrl,
                Facebook = user.FbUrl,
                Twitter = user.TwitterUrl,
                LinkedIn = user.LinkedInUrl,
                Github = user.GithubUrl
            };
            return View(model);
        }

        #region Helpers
        private void Errors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        #endregion
    }
}

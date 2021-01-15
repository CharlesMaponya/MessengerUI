using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessengerUI.Data;
using MessengerUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;

namespace MessengerUI.Controllers
{
    public class MessagesController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public MessagesController(UserManager<AppUser> userManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }


        public IActionResult Index(int? page=1)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var users = _userManager.Users.Select(user => new UsersIndexModel {
                UserId= user.Id,
                Name=user.Name,
                Surname = user.Surname,
                Image = user.ImageUrl
            });

            var model = new UsersViewModel
            {
                Users = new PagedList<UsersIndexModel>(users, pageNumber, 12)
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Conversation(string recipientId)
        {
            var CurrentUser = _userManager.GetUserId(User);

            if (recipientId == CurrentUser)
            {
                return RedirectToAction(nameof(Index));
            }
            var recipient = await _userManager.FindByIdAsync(recipientId);
            var messages = _dbContext.Texts.Where(x => (x.RecipientId == recipientId && x.SenderId == CurrentUser) || (x.RecipientId == CurrentUser && x.SenderId == recipientId)).OrderBy(x => x.MessageSent);
            var model = new UserMessagesViewModel
            {
                User = recipient,
                Texts = messages.ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Message([FromQuery] string RecipientId, [FromForm] string Message)
        {
            var currentUser = _userManager.GetUserId(User);

            var message = new Text
            {
                SenderId = currentUser,
                RecipientId = RecipientId,
                Message = Message,
                MessageSent = DateTime.Now
            };

            await _dbContext.Texts.AddAsync(message);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Conversation), new { recipientId = RecipientId });
        }
    }
}

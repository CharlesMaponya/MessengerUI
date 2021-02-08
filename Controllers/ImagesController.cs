using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessengerUI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MessengerUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IFileManager _fileManager;

        public ImagesController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        [HttpGet("{image}")]
        public IActionResult StreamPhoto(string image)
        {
            var mime = image.Substring(image.LastIndexOf('.') + 1);
            return new FileStreamResult(_fileManager.ImageStream(image), $"image/{mime}");
        }
    }
}

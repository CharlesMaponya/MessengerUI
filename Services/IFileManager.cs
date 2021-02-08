using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MessengerUI.Services
{
    public interface IFileManager
    {
        string SaveImage(IFormFile image);
        FileStream ImageStream(string image);
        bool RemoveImage(string image);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessengerUI.Services
{
    public interface IEmailSender
    {
        Task SendMail(string email, string subject, string body);
    }
}

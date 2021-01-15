using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessengerUI.Models
{
    public class UserMessagesViewModel
    {
        public List<Text> Texts { get; set; }

        public AppUser User { get; set; }

        public string Message { get; set; }
    }
}

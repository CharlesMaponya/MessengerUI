using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessengerUI.Models
{
    public class UsersIndexModel
    {
        public string UserId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Image { get; set; }

        public DateTime DateSent { get; set; }

        public string LastText { get; set; }
    }
}

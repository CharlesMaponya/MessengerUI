using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessengerUI.Models
{
    public class UsersViewModel
    {
        public string Search { get; set; }

        public PagedList<UsersIndexModel> Users { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MessengerUI.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Bio { get; set; }

        public string Country { get; set; }

        public string ImageUrl { get; set; }

        public string FbUrl { get; set; }
        public string TwitterUrl { get; set; }

        public string LinkedInUrl { get; set; }

        public string GithubUrl { get; set; }

        public virtual ICollection<Text> TextsSent { get; set; }

        public virtual ICollection<Text> TextsRecieved { get; set; }

    }
}

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
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public virtual ICollection<Text> TextsSent { get; set; }

        public virtual ICollection<Text> TextsRecieved { get; set; }

    }
}

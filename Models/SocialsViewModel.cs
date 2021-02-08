using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MessengerUI.Models
{
    public class SocialsViewModel
    {
        [Required]
        [Display(Name ="Facebook Url")]
        public string FbUrl { get; set; }

        [Required]
        [Display(Name = "Twitter Url")]
        public string TwitterUrl { get; set; }

        [Required]
        [Display(Name = "LinkedIn Url")]
        public string LinkedInUrl { get; set; }

        [Required]
        [Display(Name = "Github Url")]
        public string GithubUrl { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MessengerUI.Models
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "First Name is required!")]
        [StringLength(256, ErrorMessage = "First name must be between {2} and {0} characters long", MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Last Name is required!")]
        [StringLength(256, ErrorMessage = "First name must be between {2} and {0} characters long", MinimumLength = 3)]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Confirm Password")]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}

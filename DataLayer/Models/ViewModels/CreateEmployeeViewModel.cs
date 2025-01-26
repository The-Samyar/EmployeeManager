using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.ViewModels
{
    public class CreateEmployeeViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [DisplayName("Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DisplayName("Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password repeat is required.")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [DisplayName("Repeat Password")]
        public string PasswordRepeat { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public DateTime HiringDate { get; set; }
        [DisplayName("Position")]
        public int SelectedPosition { get; set; }
    }
}

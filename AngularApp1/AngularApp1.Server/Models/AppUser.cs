using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using AngularApp1.Server.Models;

namespace AngularApp1.Server.Models
{
    public class AppUser : IdentityUser
    {
        [Required(ErrorMessage = "Please insert your First Name"), MaxLength(100)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please insert your Last Name"), MaxLength(100)]
        public string LastName { get; set; }

        public AppUser(ApplicationUserDTO audto) 
        {
            FirstName = audto.FirstName;
            LastName = audto.LastName;
            Email = audto.Email;
        }

        public AppUser(AccountUserDTO account) 
        {
            FirstName = account.FirstName;
            LastName = account.LastName;
            PhoneNumber = account.PhoneNumber;
        }

        public AppUser() { }
    }
}

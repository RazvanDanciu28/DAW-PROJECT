﻿using AngularApp1.Server.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;


namespace AngularApp1.Server.Models
{
    public class ApplicationUserDTO
    {
        [Required(ErrorMessage = "Please insert your First Name"), MaxLength(100)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please insert your Last Name"), MaxLength(100)]
        public string LastName { get; set; }
        public string Email { get; set; }

        public ApplicationUserDTO(AppUser user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
        }
        public ApplicationUserDTO() { }
    }

    
}

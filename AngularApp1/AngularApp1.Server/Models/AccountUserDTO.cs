using System.ComponentModel.DataAnnotations;


namespace AngularApp1.Server.Models
{

    public class LoginUserDTO
    {
        [Required(ErrorMessage = "Please insert your Email Address")]
        [DataType(DataType.EmailAddress)]

        public required string Email { get; set; }
        public required string Password { get; set; }
    }
    public class AccountUserDTO : LoginUserDTO
    {
        [Required(ErrorMessage = "Please insert your First Name"), MaxLength(100)]
        public required string FirstName { get; set; }
        [Required(ErrorMessage = "Please insert your Last Name"), MaxLength(100)]
        public required string LastName { get; set; }
        public required string PhoneNumber { get; set; }
   
    }
}

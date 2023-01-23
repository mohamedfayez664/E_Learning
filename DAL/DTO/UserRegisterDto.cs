using System.ComponentModel.DataAnnotations;

namespace DAL.DTO
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "Name Not entered")]
        public string Name { get; set; }
        [Required, MinLength(8), MaxLength(50)]
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Compare("Email", ErrorMessage = "The email do not match.")]  // [Compare("Email", ErrorMessage = "الايميل غير مطابق")]
        public string EmailConfirmed { get; set; }

        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "The Password do not match.")]
        public string PasswordConfirmed { get; set; }
        public string? Adress { get; set; }
        // [EgPhone]
        [RegularExpression("(01)[0125][0-9]{8}")]
        // [RegularExpression("(01)[0125]\\9{8}")]
        public string Phone { get; set; }
    }
}

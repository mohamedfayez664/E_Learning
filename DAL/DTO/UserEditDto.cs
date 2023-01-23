using System.ComponentModel.DataAnnotations;

namespace DAL.DTO
{
    public class UserEditDto
    {
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Adress { get; set; }
        // [EgPhone]
        [RegularExpression("(01)[0125][0-9]{8}")]
        // [RegularExpression("(01)[0125]\\9{8}")]
        public string? Phone { get; set; }
    }
}

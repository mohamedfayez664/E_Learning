using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class UserRole
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public User User { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }

        public Role Role { get; set; }
        [ForeignKey("Role")]
        public int RoleId { get; set; }


    }
}

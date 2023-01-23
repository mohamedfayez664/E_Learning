using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class RoleRight
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Role Role { get; set; }
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public Right Right { get; set; }
        [ForeignKey("Right")]
        public int RightId { get; set; }

    }
}

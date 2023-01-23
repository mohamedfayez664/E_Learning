using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class UserGroup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public User User { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public StGroup StGroup { get; set; }
        [ForeignKey("StGroup")]
        public int StGroupId { get; set; }
    }
}

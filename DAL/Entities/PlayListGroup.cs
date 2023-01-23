using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class PlayListGroup
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Discussion { get; set; }
        public PlayList PlayList { get; set; }
        [ForeignKey("PlayList")]
        public int PlayListId { get; set; }
        public StGroup StGroup { get; set; }
        [ForeignKey("StGroup")]
        public int StGroupId { get; set; }
    }
}

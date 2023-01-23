using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public PlayList PlayList { get; set; }
        [ForeignKey("PlayList")]
        public int PlayListId { get; set; }

        public List<Media> Media { get; set; }

    }
}

using DAL.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Media
    {
        public int Id { get; set; }
        public MediaType? MediaType { get; set; }  /// Pdf or Word .....
        public MediaRefer? MediaRefer { get; set; }  /// Quiz or material .....
        public string Description { get; set; }
        public string Url { get; set; }
        public bool? IsPublic { get; set; } = false;
        public Lesson Lesson { get; set; }
        [ForeignKey("Lesson")]
        public int LessonId { get; set; }



    }
}

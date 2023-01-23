using DAL.Enum;
using Microsoft.AspNetCore.Http;

namespace DAL.DTO
{
    public class MediaAddTypeDto
    {
        public MediaType MediaType { get; set; }  /// Pdf or Word .....
        public MediaRefer MediaRefer { get; set; }  /// Pdf or Word .....
        public bool IsPublic { get; set; } //= false;
        public string Description { get; set; }
        public List<IFormFile> Files { get; set; }
        //   public int LessonId { get; set; }
    }
}

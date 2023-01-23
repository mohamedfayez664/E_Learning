using Microsoft.AspNetCore.Http;

namespace DAL.DTO
{
    public class CourseAccessFilesDto
    {
        public IFormFile? Image { get; set; }
        public IFormFile? Pdf { get; set; }
    }
}

using DAL.Enum;

namespace DAL.DTO
{
    public class MediaEditDataDto
    {
        public MediaType? MediaType { get; set; }  /// Pdf or Word .....
        public MediaRefer? MediaRefer { get; set; }  /// Pdf or Word .....
        public bool? IsPublic { get; set; } //= false;
        public string? Description { get; set; }
        // public List<IFormFile>? Files { get; set; }
    }
}

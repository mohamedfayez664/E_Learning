namespace DAL.DTO
{
    public class LessonGetDataDto
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public PlayListGetDto PlayList { get; set; }
        public List<string> Data { get; set; }   ////Url from Media table

    }
}

namespace DAL.Entities
{
    public class PlayList
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        //public string PublicUrl { get; set; }
        public Course Course { get; set; }
        public int CourseId { get; set; }

    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category? Parent { get; set; }
        [ForeignKey("Category")]
        public int? ParentId { get; set; }
    }
}

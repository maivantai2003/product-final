using System.ComponentModel.DataAnnotations;

namespace QuanLySanPhamBasic.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Book>? Books { get; set; }
    }
}

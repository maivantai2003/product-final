using System.ComponentModel.DataAnnotations;

namespace QuanLySanPhamBasic.Models
{
    public class Catalog
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public ICollection<BookCatalog>? BookCatalogs { get; set; }
    }
}


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLySanPhamBasic.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int TotalPrice { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey("UserId")]
        public AppUser? User { get; set; }
        public ICollection<CartDetail>? CartDetails { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLySanPhamBasic.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string OrderCode { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public int TotalAmount { get; set; }

        // Shipping information
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Notes { get; set; }
        public string PaymentMethod { get; set; }

        [ForeignKey("UserId")]
        public AppUser? User { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
} 

using Microsoft.AspNetCore.Identity;

namespace QuanLySanPhamBasic.Models
{
    public class AppUser : IdentityUser
    {
        public string? Address { get; set; }
        public ICollection<Cart>? Carts { get; set; }
    }
}

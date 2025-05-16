using System.ComponentModel.DataAnnotations;

namespace QuanLySanPhamBasic.ViewModel
{
    public class RegisterViewModel
    {
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password not match")]
        public string ConfirmPassword { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuanLySanPhamBasic.Models;

namespace QuanLySanPhamBasic.Controllers
{
    public class ChatController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public ChatController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            ViewBag.Users=_userManager.Users.ToList();
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using QuanLySanPhamBasic.Hubs;
using QuanLySanPhamBasic.Interface;
using QuanLySanPhamBasic.ViewModel;
using System.Security.Claims;

namespace QuanLySanPhamBasic.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepo _cartRepo;
        private readonly IHubContext<myHub> _hubContext;

        public CartController(ICartRepo cartRepo, IHubContext<myHub> context)
        {
            _cartRepo = cartRepo;
            _hubContext=context;
        }
        public IActionResult Index()
        {
            string userId = GetLoggedInUserId();
            var cart = _cartRepo.GetActiveCartForUser(userId);

            if (cart == null)
            {
                TempData["Message"] = "Giỏ hàng của bạn hiện tại đang TRỐNG";
            }

            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int bookId, int quantity)
        {
            string userId = GetLoggedInUserId();
            _cartRepo.AddBookToCart(userId, bookId, quantity);
            await _hubContext.Clients.All.SendAsync("render");
            return RedirectToAction("Index", "Cart");
        }

        private string GetLoggedInUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        [HttpGet]
        public IActionResult Delete(int cartId, int bookId)
        {
            var cartDetail = _cartRepo.GetCartDetailById(cartId, bookId);
            TempData["Message"] = "Bạn có chắc muốn xóa sản phẩm này không?";
            
            return View(cartDetail);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int cartId, int bookId)
        {
            _cartRepo.DeleteCartDetail(cartId, bookId);
            await _hubContext.Clients.All.SendAsync("delete");
            return RedirectToAction("Index", "Cart");
        }

        [HttpGet]
        public IActionResult DeleteAllCart(int cartId)
        {
            var cart = _cartRepo.GetCartById(cartId);
            TempData["Message"] = "Bạn có chắc muốn xóa TẤT CẢ sản phẩm này không?";
            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAllCartConfirmed(int cartId)
        {
            _cartRepo.DeleteCart(cartId);
            await _hubContext.Clients.All.SendAsync("deleteAll");
            return RedirectToAction("Index", "Cart");
        }

        // New method to handle updating cart quantities via AJAX
        [HttpPost]
        public async Task<IActionResult> UpdateQuantities([FromBody] List<CartItemUpdate> updates)
        {
            if (updates == null || !updates.Any())
            {
                return Json(new { success = false });
            }

            try
            {
                foreach (var update in updates)
                {
                    _cartRepo.UpdateCartItemQuantity(update.CartId, update.BookId, update.Quantity);
                }

                await _hubContext.Clients.All.SendAsync("cartUpdated");
                return Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        // New action for checkout
        [HttpGet]
        public IActionResult Checkout()
        {
            string userId = GetLoggedInUserId();
            var cart = _cartRepo.GetActiveCartForUser(userId);

            if (cart == null || !cart.CartDetails.Any())
            {
                TempData["Message"] = "Giỏ hàng của bạn trống, không thể thanh toán";
                return RedirectToAction("Index");
            }

            return View(cart);
        }

        [HttpPost]
        public IActionResult ProcessCheckout(CheckoutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, message = "Dữ liệu không hợp lệ" });
                }
                
                string userId = GetLoggedInUserId();
                var cart = _cartRepo.GetActiveCartForUser(userId);
                return View("Checkout", cart);
            }

            string currentUserId = GetLoggedInUserId();
            _cartRepo.CompleteOrder(currentUserId, model);

            string successMessage = "Đặt hàng thành công! Cảm ơn bạn đã mua hàng tại cửa hàng chúng tôi.";
            
            // Store in both TempData and Session to ensure it persists through redirects
            TempData["OrderSuccess"] = successMessage;
            TempData["ShowAlert"] = "true";
            
            // Store message in session
            HttpContext.Session.SetString("OrderSuccessMessage", successMessage);
            
            // Return JSON for AJAX requests
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new { success = true, message = successMessage });
            }
            
            return RedirectToAction("Index", "Books");
        }

        public IActionResult OrderConfirmation()
        {
            if (TempData["OrderSuccess"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            // Thêm nút để chuyển hướng về trang giỏ hàng
            ViewBag.ShowCartButton = true;

            return View();
        }
    }

    // Class to handle cart item update request
    public class CartItemUpdate
    {
        public int CartId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
    }
}

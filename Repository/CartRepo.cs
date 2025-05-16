using QuanLySanPhamBasic.Models;
using QuanLySanPhamBasic.Interface;
using Microsoft.EntityFrameworkCore;
using System.Net;
using QuanLySanPhamBasic.ViewModel;

namespace QuanLySanPhamBasic.Repositories
{
	public class CartRepo : ICartRepo
	{
		private readonly AppDbContext _context;

		public CartRepo(AppDbContext context)
        {
			_context = context;
		}
		public Cart GetActiveCartForUser(string userId)
		{
			var res = _context.Carts
                    .Include(c => c.CartDetails)
						.ThenInclude(cd => cd.Book)
					.FirstOrDefault(c => c.UserId == userId && c.Status == "Pending");

            return res;
		}

		public void CreateCart(string userId)
		{
			var newCart = new Cart
			{
				UserId = userId,
				Status = "Pending",
				TotalPrice = 0,
				CreatedDate = DateTime.Now,
			};

			_context.Carts.Add(newCart);
			_context.SaveChanges();
		}

		public void AddBookToCart(string userId, int bookId, int quantity)
		{
			var cart = GetActiveCartForUser(userId);
			if (cart == null)
			{
				CreateCart(userId);
				cart = GetActiveCartForUser(userId);
			}

			var book = _context.Books.Find(bookId);
			var cartDetail = cart.CartDetails!.FirstOrDefault(cd => cd.BookId == bookId);

			if (cartDetail == null)
			{
				cartDetail = new CartDetail
				{
					CartId = cart.Id,
					BookId = bookId,
					Price = book!.Price,
					Quantity = quantity
				};
				_context.CartDetails.Add(cartDetail);
			}
			else
			{
				cartDetail.Quantity += quantity;
			}

			cart.TotalPrice += book!.Price * quantity;
			_context.SaveChanges();
		}

		public void DeleteCartDetail(int cartId, int bookId)
		{
			var cartDetail = _context.CartDetails.FirstOrDefault(x => x.CartId == cartId && x.BookId == bookId);

            if (cartDetail != null)
            {
                _context.CartDetails.Remove(cartDetail);

                _context.SaveChanges();
            }
        }

        public void DeleteCart(int cartId)
        {
			var cart = _context.Carts.FirstOrDefault(x => x.Id == cartId);
            if (cart != null)
            {
                _context.Carts.Remove(cart);

                _context.SaveChanges();
            }
        }

        public CartDetail GetCartDetailById(int cartId, int bookId)
        {
            return _context.CartDetails.FirstOrDefault(x => x.CartId == cartId && x.BookId == bookId) ?? new CartDetail();
        }

        public Cart GetCartById(int cartId)
        {
            return _context.Carts.FirstOrDefault(x => x.Id == cartId) ?? new Cart();
        }

        public void UpdateCartItemQuantity(int cartId, int bookId, int quantity)
        {
            var cart = _context.Carts
                .Include(c => c.CartDetails)
                .FirstOrDefault(c => c.Id == cartId);

            if (cart == null)
                return;

            var cartDetail = cart.CartDetails?.FirstOrDefault(cd => cd.BookId == bookId);
            if (cartDetail == null)
                return;

            // Calculate the price difference based on quantity change
            var priceDifference = cartDetail.Price * (quantity - cartDetail.Quantity);
            
            // Update the quantity
            cartDetail.Quantity = quantity;
            
            // Update the cart total price
            cart.TotalPrice += priceDifference;
            
            _context.SaveChanges();
        }

        public void CompleteOrder(string userId, CheckoutViewModel model)
        {
            // Get the active cart for the user
            var cart = GetActiveCartForUser(userId);
            if (cart == null || !cart.CartDetails.Any())
                return;

            // Create a new order
            var order = new Order
            {
                UserId = userId,
                OrderCode = GenerateOrderCode(),
                OrderDate = DateTime.Now,
                Status = "Pending",
                TotalAmount = cart.TotalPrice,
                FullName = model.FullName,
                Address = model.Address,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Notes = model.Notes,
                PaymentMethod = model.PaymentMethod,
                OrderDetails = new List<OrderDetail>()
            };

            // Create order details from cart details
            foreach (var cartDetail in cart.CartDetails)
            {
                var orderDetail = new OrderDetail
                {
                    BookId = cartDetail.BookId,
                    BookTitle = cartDetail.Book.Title,
                    Price = cartDetail.Price,
                    Quantity = cartDetail.Quantity,
                };
                
                order.OrderDetails.Add(orderDetail);
            }

            // Add the order to the database
            _context.Orders.Add(order);

            // Change the cart status to "Completed"
            cart.Status = "Completed";

            _context.SaveChanges();
        }

        private string GenerateOrderCode()
        {
            // Generate a unique order code with prefix "ORD-" followed by timestamp and random 4 digits
            return "ORD-" + DateTime.Now.ToString("yyMMddHHmm") + "-" + new Random().Next(1000, 9999);
        }

        public void ClearCartAfterCheckout(string userId)
        {
            // Lấy giỏ hàng vừa hoàn thành (đã đánh dấu Completed)
            var cart = _context.Carts
                .Include(c => c.CartDetails)
                .FirstOrDefault(c => c.UserId == userId && c.Status == "Completed");

            if (cart != null && cart.CartDetails != null)
            {
                // Xóa tất cả chi tiết giỏ hàng
                _context.CartDetails.RemoveRange(cart.CartDetails);
                
                // Xóa luôn giỏ hàng cũ
                _context.Carts.Remove(cart);
                
                // Lưu thay đổi
                _context.SaveChanges();
            }

            // Tạo giỏ hàng mới cho người dùng
            var newCart = new Cart
            {
                UserId = userId,
                Status = "Pending",
                TotalPrice = 0,
                CreatedDate = DateTime.Now,
            };

            _context.Carts.Add(newCart);
            _context.SaveChanges();
        }
        
        public void RemoveCompletedCarts(string userId)
        {
            // Lấy tất cả giỏ hàng đã hoàn thành
            var completedCarts = _context.Carts
                .Include(c => c.CartDetails)
                .Where(c => c.UserId == userId && c.Status == "Completed")
                .ToList();

            foreach (var cart in completedCarts)
            {
                if (cart.CartDetails != null && cart.CartDetails.Any())
                {
                    // Xóa tất cả chi tiết giỏ hàng
                    _context.CartDetails.RemoveRange(cart.CartDetails);
                }
                
                // Xóa giỏ hàng
                _context.Carts.Remove(cart);
            }
            
            // Lưu thay đổi nếu có giỏ hàng nào được xóa
            if (completedCarts.Any())
            {
                _context.SaveChanges();
            }
        }
    }
}

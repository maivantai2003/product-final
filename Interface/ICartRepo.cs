using QuanLySanPhamBasic.Models;
using QuanLySanPhamBasic.ViewModel;

namespace QuanLySanPhamBasic.Interface
{
    public interface ICartRepo
    {
        void CreateCart(string userId);
        void DeleteCartDetail(int cartId, int bookId);
        void DeleteCart(int cartId);
        void AddBookToCart(string userId, int bookId, int quantity);
        Cart GetActiveCartForUser(string userId);
        CartDetail GetCartDetailById(int cartId, int bookId);
        Cart GetCartById(int cartId);
        void UpdateCartItemQuantity(int cartId, int bookId, int quantity);
        void CompleteOrder(string userId, CheckoutViewModel model);
        void ClearCartAfterCheckout(string userId);
        void RemoveCompletedCarts(string userId);
    }
}

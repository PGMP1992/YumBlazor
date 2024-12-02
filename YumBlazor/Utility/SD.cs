using YumBlazor.Models;

namespace YumBlazor.Utility
{
    public static class SD
    {
        public static string Role_Admin = "Admin";
        public static string Role_Customer = "Customer";
        
        public static string StatusPending = "Pending";
        public static string StatusApproved = "Approved";
        public static string StatusReadyForPickUp = "Ready For PickUp";
        public static string StatusCompleted = "Completed";
        public static string StatusCancelled = "Cancelled";

        public static List<OrderDetail> ConvertToCart(List<ShoppingCart> shoppingCarts)
        {
            List<OrderDetail> orderDetails = new List<OrderDetail>();

            foreach (var cart in shoppingCarts)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    ProductId = cart.ProductId,
                    ProductName = cart.Product.Name,
                    Count = cart.Count,
                    Price = Convert.ToDouble(cart.Product.Price)
                }; 
                orderDetails.Add(orderDetail);
            }
            return orderDetails;
        }
    }
}

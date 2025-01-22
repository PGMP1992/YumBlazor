using Microsoft.AspNetCore.Components;
using Stripe.Checkout;
using YumBlazor.Models;
using YumBlazor.Repos.Interfaces;
using YumBlazor.Utility;

namespace YumBlazor.Services
{
    public class PaymentService
    {
        private readonly NavigationManager _navigationManager;
        private readonly IOrderRepos _orderRepos;

        public PaymentService(NavigationManager navigationManager, IOrderRepos orderRepos)
        {
            _navigationManager = navigationManager;
            _orderRepos = orderRepos;
        }

        public Session CreateStripeCheckoutSession(OrderHeader orderHeader)
        {
            var lineItems = orderHeader.OrderDetails
                .Select(order => new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "sek",
                        UnitAmountDecimal = (decimal?)order.Price * 100, // sek 10.10 => 1010
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = order.ProductName,
                        }
                    },
                    Quantity = order.Count,
                }).ToList();

            var option = new Stripe.Checkout.SessionCreateOptions
            {
                SuccessUrl = $"{_navigationManager.BaseUri}order/confirmation/{{CHECKOUT_SESSION_ID}}",
                CancelUrl = $"{_navigationManager.BaseUri}cart",
                LineItems = lineItems,
                Mode = "payment",
            };

            var service = new SessionService();
            var session = service.Create(option);
            return session;
        }

        public async Task<OrderHeader> CheckPaymentStatusAndUpdateOrder(string sessionId)
        {
            OrderHeader orderHeader = await _orderRepos.GetOrderBySessionIdAsync(sessionId);
            var service = new SessionService();
            var session = service.Get(sessionId);

            if (session.PaymentStatus.ToLower() == "paid")
            {
                await _orderRepos.UpdateStatusAsync(orderHeader.Id, SD.StatusApproved, session.PaymentIntentId);
            }
            return orderHeader;
        }
    }
}

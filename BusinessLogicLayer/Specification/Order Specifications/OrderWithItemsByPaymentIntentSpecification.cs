using System;
using System.Linq.Expressions;
using DataAccessLayer.Entities.Order;

namespace BusinessLogicLayer.Specification.Order_Specifications
{
    public class OrderWithItemsByPaymentIntentSpecification : BaseSpecification<Order>
    {
        public OrderWithItemsByPaymentIntentSpecification(string paymentIntentId) 
            : base(o => o.PaymentIntentId == paymentIntentId)
        {
        }
    }
}
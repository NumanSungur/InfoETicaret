using Core.Entities.Abstract;
using System;

namespace Entities.DTO
{
    public class OrdersDto:IDTO
    {
        public int Id { get; set; }
        public int BasketId { get; set; }
        public DateTime OrderDate { get; set; }                
        public string OrderStatus { get; set; }
        public decimal TotalPrice { get; set; }
    }
}

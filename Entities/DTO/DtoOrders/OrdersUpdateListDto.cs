using Core.Entities.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace Entities.DTO
{
    public class OrdersUpdateListDto : IDTO
    {
        public int Id { get; set; }
        public int CustomersId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OdemeTuru { get; set; }
        public string CargoNumber { get; set; }
        public int BasketId { get; set; }
        public decimal CargoPrice { get; set; }
        public string OrderStatus { get; set; } //Teslim edildi/edilmedi
        public decimal TotalPrice { get; set; }
        public byte Kdv { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal CouponPrice { get; set; }
        public ICollection<OrderDetailsDto> OrderDetailsDto { get; set; }
        public ICollection<OrderNotesDto> OrderNotesDto { get; set; }
        public ICollection<OrderInformationsDto> OrderInformationsDto { get; set; }
        public CustomersUpdateDto CustomersUpdateDto { get; set; }
    }
}

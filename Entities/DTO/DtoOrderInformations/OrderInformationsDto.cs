using Core.Entities.Abstract;
using System;

namespace Entities.DTO
{
    public class OrderInformationsDto : IDTO
    {
        public int Id { get; set; }        
        public int CustomerId { get; set; }       
        public bool Sms { get; set; }       
        public bool Email { get; set; }      
        public string Message { get; set; }        
        public DateTime InfoDate { get; set; }
        public int OrdersId { get; set; }
    }
}

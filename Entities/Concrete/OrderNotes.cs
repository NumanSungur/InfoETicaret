using Core.Entities.Abstract;
using System;

namespace Entities.Concrete
{
    public class OrderNotes:IEntity
    {
        public int Id { get; set; }       
        public string Notes { get; set; }
        public DateTime NotDate { get; set; }
        public int OrdersId { get; set; }
        public virtual Orders Orders { get; set; }
    }
}

using Core.Entities.Abstract;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public class Customers:IEntity
    {
        public int Id { get; set; }
        public string NameSurname { get; set; }
        public string Phone { get; set; } //namesurname like %Ahmet%,
        public string Email { get; set; }
        public string City { get; set; } //şehir
        public string District { get; set; } //ilçe
        public string FullAddress { get; set; } //adres
        public string Password { get; set; }
        public bool Advert { get; set; } //mail ve sms olarak bilgilendirme izni
        public bool Gender { get; set; }
        public ICollection<Orders> Orders { get; set; }
    }
}

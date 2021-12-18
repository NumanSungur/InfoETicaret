using Core.Entities.Abstract;

namespace Entities.DTO
{
    public class CustomersDto :IDTO
    {
        public int Id { get; set; }       
        public string NameSurname { get; set; }        
        public string Phone { get; set; }       
        public string Email { get; set; }        
        public string City { get; set; }            
    }
}

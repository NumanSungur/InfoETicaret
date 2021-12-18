using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class UsersAdmin:IEntity
    {
        public int Id { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }        
        public bool Status { get; set; }
        public string Roles { get; set; }
    }
}

using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class AutoBasket :IEntity
    {
        public int Id { get; set; }
        public int Uretilen { get; set; }
    }
}

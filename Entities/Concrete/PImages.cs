using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class PImages:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductsId { get; set; }
        public Products Products { get; set; }
    }
}

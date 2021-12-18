using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Variants:IEntity
    {
        public int Id { get; set; }
        public string GroupName { get; set; } //renk,numara,kğ vb
        public string Name { get; set; } //kırmızı yeşil mavi 38 39 40
        public decimal Price { get; set; }
        public int ProductsId { get; set; }     
        public Products Products { get; set; }
    }
}

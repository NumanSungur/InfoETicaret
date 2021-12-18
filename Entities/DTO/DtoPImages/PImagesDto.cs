using Core.Entities.Abstract;

namespace Entities.DTO
{
    public class PImagesDto: IDTO
    {
        public int Id { get; set; }        
        public string Name { get; set; }
        public int ProductsId { get; set; }
    }
}

using Core.Entities.Abstract;

namespace Entities.DTO
{
    public class TempBasketsDto : IDTO
    {
        public int Id { get; set; }       
        public int ProductsId { get; set; }
        public int CookiesID { get; set; }
        public string MainImages { get; set; }
        public string Name { get; set; }        
        public int Piece { get; set; }        
        public decimal Price { get; set; }        
        public string VName { get; set; }        
        public int CustomersId { get; set; }
    }
}

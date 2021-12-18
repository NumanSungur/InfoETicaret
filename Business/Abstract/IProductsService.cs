using Core.Results.Abstract;
using Entities.DTO;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IProductsService
    {
        //Her zaman kullanıcıya bir tabloda bütün sütünlar gönderilmez.
        //Entities deki tablolara benzer bir yapı daha kurulur.
        //DTO => Data Transfer Object

        IDataResult<IList<ProductsDto>> GetAll();
        IDataResult<IList<ProductsDto>> KategoriyeGoreUrunGetirme(int CategoryId);
        IDataResult<IList<ProductsDto>> GetAllKampanya();
        IDataResult<ProductsUpdateDto> GetById(int Id);
        IResult Add(ProductsUpdateDto data);
        IResult Update(ProductsUpdateDto data);
        IResult Delete(int Id);
        IDataResult<int> SearchId(string Name, decimal Price, int Stock);
    }
}

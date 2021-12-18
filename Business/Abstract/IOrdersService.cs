using Core.Results.Abstract;
using Entities.DTO;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IOrdersService
    {
        IDataResult<IList<OrdersUpdateListDto>> FiveTableGetAll(int Id); //ilişkilendirilmiş 5 tablo getirecek.
        IDataResult<IList<OrdersDto>> GetAll(string Durumu);
        IDataResult<OrdersUpdateDto> GetById(int Id);
        IResult Add(OrdersUpdateDto data);
        IResult Update(OrdersUpdateDto data);
        IResult Delete(int Id);
    }
}

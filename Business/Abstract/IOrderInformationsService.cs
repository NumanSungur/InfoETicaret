using Core.Results.Abstract;
using Entities.DTO;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IOrderInformationsService
    {
        IDataResult<IList<OrderInformationsDto>> GetAll(int Id);
        OrderInformationsDto GetByIdFirst(int Id);
        IResult Add(OrderInformationsDto data);
        IResult Update(OrderInformationsDto data);
        IResult Delete(int Id);
    }
}

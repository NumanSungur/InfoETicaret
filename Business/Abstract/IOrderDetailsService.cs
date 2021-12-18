using Core.Results.Abstract;
using Entities.DTO;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IOrderDetailsService
    {
        IDataResult<IList<OrderDetailsDto>> GetAll(int Id);
        OrderDetailsDto GetByIdFirst(int Id);
        IResult Add(OrderDetailsDto data);
        IResult Update(OrderDetailsDto data);
        IResult Delete(int Id);
    }
}

using Core.Results.Abstract;
using Entities.DTO;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IOrderNotesService
    {
        IDataResult<IList<OrderNotesDto>> GetAll(int Id);
        OrderNotesDto GetByIdFirst(int Id);
        IResult Add(OrderNotesDto data);
        IResult Update(OrderNotesDto data);
        IResult Delete(int Id);
    }
}

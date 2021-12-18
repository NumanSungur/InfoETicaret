using Core.Results.Abstract;
using Entities.DTO;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IVariantsService
    {
        IDataResult<IList<VariantsDto>> GetAll(int Id);
        VariantsDto GetByIdFirst(int Id);
        IResult Add(VariantsDto data);
        IResult Update(VariantsDto data);
        IResult Delete(int id);
    }
}

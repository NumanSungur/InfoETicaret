using Core.Results.Abstract;
using Entities.DTO;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICustomersService
    {
        IDataResult<IList<CustomersDto>> GetAll();
        IDataResult<CustomersUpdateDto> GetById(int Id);
        IDataResult<CustomersDto> Login(string Email, string Phone, string Password);
        IResult Add(CustomersUpdateDto data);
        IResult Update(CustomersUpdateDto data);
        IResult Delete(int Id);
    }
}

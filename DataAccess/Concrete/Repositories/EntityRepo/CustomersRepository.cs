using Core.DataRepository.Concrete.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.Repositories.EntityRepo
{
    public class CustomersRepository : EfEntityRepository<Customers>, ICustomersRepository 
    {
        public CustomersRepository(DbContext context):base(context)
        {
                
        }
    }
}

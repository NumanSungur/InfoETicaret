using Core.DataRepository.Concrete.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.Concrete.Repositories.EntityRepo
{
     public class OrdersRepository :EfEntityRepository<Orders>,IOrdersRepository
    {
        public OrdersRepository(DbContext context):base(context)
        {
                
        }
    }
}

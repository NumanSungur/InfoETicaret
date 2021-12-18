using Core.DataRepository.Concrete.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.Repositories.EntityRepo
{
    class ProductsRepository:EfEntityRepository<Products>,IProductsRepository
    {
        public ProductsRepository(DbContext context):base(context)
        {
                
        }
    }
}

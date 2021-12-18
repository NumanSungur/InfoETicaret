using Core.DataRepository.Concrete.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.Repositories.EntityRepo
{
    public class TempBasketsRepository : EfEntityRepository<TempBaskets>, ITempBasketsRepository 
    {
        public TempBasketsRepository(DbContext context):base(context)
        {
                
        }
    }
}

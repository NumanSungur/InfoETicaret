using Core.DataRepository.Concrete.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.Repositories.EntityRepo
{
    public class UsersAdminRepository : EfEntityRepository<UsersAdmin>, IUsersAdminRepository 
    {
        // Bu kısımda Repository sınfımın Yapıcı Metot' una parametre gönderiyoruz, yani veri tabanı bağlantısı gönderiyoruz.
        public UsersAdminRepository(DbContext context):base(context)
        {
                
        }
    }
}

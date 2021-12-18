using System;

namespace DataAccess.Abstract
{
    public interface IUnitofWorks: IDisposable
    {
        IProductsRepository ProductsRepository { get;  }
        ICategoriesRepository CategoriesRepository { get; }
        IVariantRepository VariantRepository { get; }
        IPImagesRepository PImagesRepository { get; }
        ICustomersRepository CustomersRepository { get; }
        ITempBasketsRepository TempBasketsRepository { get; }
        IUsersAdminRepository UsersAdminRepository { get; }
        IOrdersRepository OrdersRepository { get; }
        IOrderDetailsRepository OrderDetailsRepository { get; }
        IOrderInformationsRepository OrderInformationsRepository { get; }
        IOrderNotesRepository OrderNotesRepository { get; }
        IAutoBasketRepository AutoBasketRepository { get; }

        void SaveChanges();

        //Stack temizleme İşlemi
        //Bütün Repolar tek bir yerden yönetim
        //Veri tabanı Kayıt onay işlemleri
        //Aynı anda 2 tabloya veri eklemesi yapılacağı zaman (Products ,ProductsImages) iki tablodan birinde ekleme hatası çıktı.Eğer herhangi bir tabloda hata çıkıyorsa bütün eklemeleri iptal eder.

        //Crud İşlemleri yapan yapılara transaction. 2 tablo için 2 transaction => 2 tablo için tek transaction
    }
}

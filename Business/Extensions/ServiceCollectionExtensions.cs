using Business.Abstract;
using Business.Concrete;
using Business.ValidationRules.FluentValidation;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts.EntityFramework;
using DataAccess.Concrete.Repositories;
using Entities.DTO;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Extensions
{
    //Bir defa oluşturulacak. Herkes için aynı olacak
    public static class ServiceCollectionExtensions
    {
        //geriye interface döndürecek
        //yapıcı metot geriye metot döndürmez
        public static IServiceCollection MyService(this IServiceCollection ServiceCollection)
        {
            //Ahmet 5 defa anasayfaya geldi, 5 defa ürün listelendi 5 instance oluşacak.
            //transient => her sınıf istenildiğinde yeni bir instance oluşturur.

            //Cevdet 5 ürürn listelemesi yaptı, Ahmet te 5 adet yapmak zorunda
            //Singleton => Bir tane oluşturur Herkes için aynıdır.

            //Hasan request attı 5 defa anasayfaya girdi 1 defa üretti 10 veri listelendi
            //Burak request attı 5 defa anasayfaya girdi 1 defa üretti 20 veri listelendi
            //Scoped =>Yeni bir request geldiğinde üretilir.

            ServiceCollection.AddDbContext<ETicaretContext>();
            ServiceCollection.AddScoped<IUnitofWorks, UnitofWorks>();            
            ServiceCollection.AddScoped<ICategoriesService, CategoriesManager>();
            ServiceCollection.AddScoped<ICustomersService, CustomersManager>();
            ServiceCollection.AddScoped<IOrderDetailsService, OrderDetailsManager>();
            ServiceCollection.AddScoped<IOrderInformationsService, OrderInformationsManager>();
            ServiceCollection.AddScoped<IOrderNotesService, OrderNotesManager>();
            ServiceCollection.AddScoped<IOrdersService, OrdersManager>();
            ServiceCollection.AddScoped<IPImagesService, PImagesManager>();
            ServiceCollection.AddScoped<IProductsService, ProductsManager>();
            ServiceCollection.AddScoped<ITempBasketsService, TempBasketsManager>();
            ServiceCollection.AddScoped<IUsersAdminService, UsersAdminManager>();
            ServiceCollection.AddScoped<IVariantsService, VariantsManager>();

            //Validation dependency tanımlamaları
            //ınstnace oluşturulan tek yer
            ServiceCollection.AddSingleton<IValidator<CategoriesDto>, CategoriesValidation>();
            ServiceCollection.AddSingleton<IValidator<CustomersUpdateDto>, CustomersValidation>();
            ServiceCollection.AddSingleton<IValidator<OrderInformationsDto>, OrdersInformationValidation>();
            ServiceCollection.AddSingleton<IValidator<OrderNotesDto>, OrdersNoteValidation>();
            ServiceCollection.AddSingleton<IValidator<OrderDetailsDto>, OrdersDetailValidation>();
            ServiceCollection.AddSingleton<IValidator<OrdersUpdateDto>, OrdersValidation>();
            ServiceCollection.AddSingleton<IValidator<PImagesDto>, PImagesValidation>();
            ServiceCollection.AddSingleton<IValidator<ProductsUpdateDto>, ProductsValidation>();
            ServiceCollection.AddSingleton<IValidator<UsersAdminDto>, UsersAdminValidation>();
            ServiceCollection.AddSingleton<IValidator<VariantsDto>, VariantsValidation>();

            return ServiceCollection;
        }
    }
}

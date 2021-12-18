using DataAccess.Abstract;
using DataAccess.Concrete.Contexts.EntityFramework;
using DataAccess.Concrete.Repositories.EntityRepo;

namespace DataAccess.Concrete.Repositories
{
    public class UnitofWorks : IUnitofWorks
    {
        private readonly ETicaretContext context;
        private  ProductsRepository products;
        private CategoriesRepository categories;
        private VariantRepository variant;
        private PImagesRepository pImages;
        private CustomersRepository customers;
        private TempBasketsRepository tempBaskets;
        private UsersAdminRepository usersAdmin;
        private OrdersRepository orders;
        private OrderDetailsRepository orderDetails;
        private OrderInformationsRepository orderInformations;
        private OrderNotesRepository orderNotes;
        private AutoBasketRepository autoBasketRepo;

        public UnitofWorks(ETicaretContext _context)
        {
            context = _context;
        }
        public IProductsRepository ProductsRepository => products ?? new ProductsRepository(context);
        public ICategoriesRepository CategoriesRepository => categories ?? new CategoriesRepository(context);
        public IVariantRepository VariantRepository => variant ?? new VariantRepository(context);
        public IPImagesRepository PImagesRepository => pImages ?? new PImagesRepository(context);
        public ICustomersRepository CustomersRepository => customers ?? new CustomersRepository(context);
        public ITempBasketsRepository TempBasketsRepository => tempBaskets ?? new TempBasketsRepository(context);
        public IUsersAdminRepository UsersAdminRepository => usersAdmin ?? new UsersAdminRepository(context);
        public IOrdersRepository OrdersRepository => orders ?? new OrdersRepository(context);
        public IOrderDetailsRepository OrderDetailsRepository => orderDetails ?? new OrderDetailsRepository(context);
        public IOrderInformationsRepository OrderInformationsRepository => orderInformations ?? new OrderInformationsRepository(context);
        public IOrderNotesRepository OrderNotesRepository => orderNotes ?? new OrderNotesRepository(context);//Ternary İf
        public IAutoBasketRepository AutoBasketRepository => autoBasketRepo ?? new AutoBasketRepository(context);
        public void Dispose()
        {
            context.Dispose(); //Ramdam siler.
        }
        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}

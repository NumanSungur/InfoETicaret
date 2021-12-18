using AutoMapper;
using Entities.Concrete;
using Entities.DTO;

namespace Business.AutoMapper.Profiles
{
    public class AllProfile:Profile
    {
        public AllProfile()
        {
            //listeleme / add / update
            //Kullanıcıya gelen entity e dönüşecek
            CreateMap<ProductsUpdateDto, Products>();  //dto to entity convert
            //Entity den gelen kullanıcınınkine çevir.
            CreateMap<Products, ProductsUpdateDto>(); //entity to dto convert
            CreateMap<Products, ProductsDto>();       //entity to dto convert

            CreateMap<PImagesDto, PImages>();           //dto to entity convert 
            CreateMap<PImages, PImagesDto>();           //entity to dto convert

            CreateMap<VariantsDto, Variants>();           //dto to entity convert 
            CreateMap<Variants, VariantsDto>();           //entity to dto convert

            CreateMap<CategoriesDto, Categories>();   //dto to entity convert 
            CreateMap<Categories, CategoriesDto>();     //entity to dto convert

            CreateMap<CustomersUpdateDto, Customers>();      //dto to entity convert       
            CreateMap<Customers, CustomersUpdateDto>();     //entity to dto convert
            CreateMap<Customers, CustomersDto>();         //entity to dto convert

            CreateMap<OrdersUpdateDto, Orders>();       //dto to entity convert  
            CreateMap<Orders, OrdersUpdateDto>();      //entity to dto convert
            CreateMap<Orders, OrdersDto>();           //entity to dto convert
            CreateMap<OrdersUpdateListDto, Orders>();       //dto to entity convert  
            CreateMap<Orders, OrdersUpdateListDto>();      //entity to dto convert

            CreateMap<OrderInformationsDto, OrderInformations>();   //dto to entity convert  
            CreateMap<OrderInformations, OrderInformationsDto>();   //entity to dto convert

            CreateMap<OrderDetailsDto, OrderDetails>();   //dto to entity convert  
            CreateMap<OrderDetails, OrderDetailsDto>();   //entity to dto convert

            CreateMap<OrderNotesDto, OrderNotes>();      //dto to entity convert  
            CreateMap<OrderNotes, OrderNotesDto>();     //entity to dto convert

            CreateMap<TempBasketsDto, TempBaskets>();     //dto to entity convert  
            CreateMap<TempBaskets, TempBasketsDto>();    //entity to dto convert

            CreateMap<UsersAdminDto, UsersAdmin>();      //dto to entity convert  
            CreateMap<UsersAdmin, UsersAdminDto>();      //entity to dto convert

            CreateMap<AutoBasketDto, AutoBasket>();     //dto to entity convert 
            CreateMap<AutoBasket, AutoBasketDto>();     //entity to dto convert
        }
    }
}

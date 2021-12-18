using AutoMapper;
using Business.Abstract;
using Core.Results.Abstract;
using Core.Results.ComplexTypes;
using Core.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    //Dto ları alıp entities yapısına çevirerek repository gönderecegiz.
    //Repository den aldığımız verileri burada istediğimiz DTO ya dönüştürme işlemi gerçeklerştirip kullanıcıya göndereceğiz.
    public class ProductsManager : IProductsService
    {
        private readonly IUnitofWorks works;
        private readonly IMapper mapper;
        public ProductsManager(IUnitofWorks _works, IMapper _mapper)
        {
            works = _works;
            mapper = _mapper;
        }
        public IResult Add(ProductsUpdateDto data)
        {
            //ProductsUpdateDto sınfı , Products sınıfına dönüştürülecek
            //Products sınıfındaki ve ProductsUpdateDto sınıfındaki property leri otomatik eşleştirip transfer eden yapımız var.AUTOMAPPER
            try
            {
                var dönüştürülen = mapper.Map<Products>(data);
                works.ProductsRepository.Add(dönüştürülen);
                works.SaveChanges();
                return new Result(ResultStatus.Success, "Kayıt Başarılı");
            }
            catch (Exception)
            {
                return new Result(ResultStatus.Error, "Kayıt Başarısız");
            }           
        }
        public IResult Delete(int Id)
        {
            try
            {
                works.ProductsRepository.Delete(works.ProductsRepository.GetByIdFirst(x => x.Id == Id));
                works.SaveChanges();
                return new Result(ResultStatus.Success, "Silme Başarılı");
            }
            catch (Exception)
            {
                return new Result(ResultStatus.Error, "Silme Başarısız");
            }
        }
        public IDataResult<IList<ProductsDto>> GetAll()
        {           
            IList<ProductsDto> products = new List<ProductsDto>();
            foreach (var item in works.ProductsRepository.GetAll())
            {
                products.Add(mapper.Map<ProductsDto>(item));
            }
            if (products.Count > 0)
            {
                return new DataResult<IList<ProductsDto>>(ResultStatus.Success,products.Count() + "Kayıt Listelendi",products);
            }
            else
            {
                return new DataResult<IList<ProductsDto>>(ResultStatus.Info, " Kayıt Bulunamadı", products);
            }
        }

        public IDataResult<IList<ProductsDto>> GetAllKampanya()
        {
            IList<ProductsDto> products = new List<ProductsDto>();
            foreach (var item in works.ProductsRepository.GetAll(x => x.Discount != 0))
            {
                products.Add(mapper.Map<ProductsDto>(item));
            }
            if (products.Count > 0)
            {
                return new DataResult<IList<ProductsDto>>(ResultStatus.Success, products.Count() + " Kayıt Listelendi.", products);
            }
            else
            {
                return new DataResult<IList<ProductsDto>>(ResultStatus.Info, "Kayıt Bulunamadı", null);
            }
        }

        public IDataResult<ProductsUpdateDto> GetById(int Id)
        {
            var data = works.ProductsRepository.GetByIdFirst(x => x.Id == Id);
            if (data != null) 
            {
                var AutoData = mapper.Map<ProductsUpdateDto>(data);
                return new DataResult<ProductsUpdateDto>(ResultStatus.Success, "1 Kayıt Getirildi.", AutoData);
            }
            else
            {
                return new DataResult<ProductsUpdateDto>(ResultStatus.Info, "Kayıt Bulunamadı.", null);
            }
        }

        public IDataResult<IList<ProductsDto>> KategoriyeGoreUrunGetirme(int CategoryId)
        {
            IList<ProductsDto> products = new List<ProductsDto>();
            foreach (var item in works.ProductsRepository.GetAll(x => x.CategoriesId == CategoryId))
            {
                products.Add(mapper.Map<ProductsDto>(item));
            }
            if (products.Count > 0)
            {
                return new DataResult<IList<ProductsDto>>(ResultStatus.Success, products.Count() + " Kayıt Listelendi.", products);
            }
            else
            {
                return new DataResult<IList<ProductsDto>>(ResultStatus.Info, "Kayıt Bulunamadı", null);
            }
        }

        public IDataResult<int> SearchId(string Name, decimal Price, int Stock)
        {
            var data = works.ProductsRepository.GetByIdFirst(x => x.Name == Name && x.Stock == Stock && x.Price == Price);
            if (data != null)
            {                
                return new DataResult<int>(ResultStatus.Success, "1 Kayıt Getirildi.", data.Id);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Info, "Kayıt Bulunamadı.", 0);
            }
        }
        public IResult Update(ProductsUpdateDto data)
        {
            try
            {
                var datam = mapper.Map<Products>(data);
                works.ProductsRepository.Update(datam);
                works.SaveChanges();
                return new Result(ResultStatus.Success, "Güncelleme Başarılı");
            }
            catch (Exception)
            {
                return new Result(ResultStatus.Error, "Güncelleme Başarısız");
            }
        }
    }
}

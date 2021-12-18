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
    public class TempBasketsManager : ITempBasketsService
    {
        private readonly IUnitofWorks works;
        private readonly IMapper mapper;
        public TempBasketsManager(IUnitofWorks _works, IMapper _mapper)
        {
            works = _works;
            mapper = _mapper;
        }
        public IResult AddUpdate(int ProductId, int CookiesID, int VaryantID)
        {         
            try
            {              
                // CookiesID(1) olan kullanıcı daha önce bu ürünü sepete eklememiş ise sepete eklenecek                
                if (works.TempBasketsRepository.GetByIdFirst(x=>x.ProductsId == ProductId && x.CookiesID == CookiesID) == null)
                {
                    var FindProduct = works.ProductsRepository.GetByIdFirst(x => x.Id == ProductId);
                    TempBasketsDto dto = new TempBasketsDto();
                    dto.ProductsId = FindProduct.Id;
                    dto.Name = FindProduct.Name;
                    dto.Piece = 1;                   
                    dto.CookiesID = CookiesID;

                    if (VaryantID == 0)
                    {
                        dto.VName = "";
                        dto.Price = FindProduct.Price;
                    }
                    else
                    {
                        var BulunanVaryant = works.VariantRepository.GetByIdFirst(x => x.Id == VaryantID);
                        dto.VName = BulunanVaryant.Name;
                        dto.Price = BulunanVaryant.Price;
                    }
                    dto.MainImages = FindProduct.MainImages;
                    works.TempBasketsRepository.Add(mapper.Map<TempBaskets>(dto));
                    works.SaveChanges();
                    return new Result(ResultStatus.Success, "Ürün Sepete Eklenmiştir.");
                }
                // Eklemiş ise adet güncellemesi gerçekleşecek.
                else
                {
                    var BulunanUrun = works.TempBasketsRepository.GetByIdFirst(x => x.ProductsId == ProductId && x.CookiesID == CookiesID);
                    BulunanUrun.Piece++;
                    works.TempBasketsRepository.Update(BulunanUrun);
                    works.SaveChanges();
                    return new Result(ResultStatus.Success, "Ürün'ün Adeti Arttırılmıştır.");
                }               
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
                works.TempBasketsRepository.Delete(works.TempBasketsRepository.GetByIdFirst(x => x.Id == Id));
                works.SaveChanges();
                return new Result(ResultStatus.Success, "Silme Başarılı");
            }
            catch (Exception)
            {
                return new Result(ResultStatus.Error, "Silme Başarısız");
            }
        }
        public IDataResult<IList<TempBasketsDto>> GetAll(int CookiesID)
        {
            IList<TempBasketsDto> data = new List<TempBasketsDto>();
            foreach (var item in works.TempBasketsRepository.GetAll(x=>x.CookiesID == CookiesID))
            {
                data.Add(mapper.Map<TempBasketsDto>(item));
            }
            if (data.Count > 0)
            {
                return new DataResult<IList<TempBasketsDto>>(ResultStatus.Success, data.Count() + "Kayıt Listelendi", data);
            }
            else
            {
                return new DataResult<IList<TempBasketsDto>>(ResultStatus.Info, " Kayıt Bulunamadı", null);
            }
        }
        public IDataResult<TempBasketsDto> GetById(int Id)
        {
            var data = works.TempBasketsRepository.GetByIdFirst(x => x.Id == Id);
            if (data != null)
            {
                return new DataResult<TempBasketsDto>(ResultStatus.Success, "1 Kayıt Getirildi.", mapper.Map<TempBasketsDto>(data));
            }
            else
            {
                return new DataResult<TempBasketsDto>(ResultStatus.Info, "Kayıt Bulunamadı.", null);
            }
        }     
        public IResult SepetArttırEksilt(int Id, bool islem)
        {
            try
            {
                var Sepetim = works.TempBasketsRepository.GetByIdFirst(x => x.Id == Id);
                if (islem) //true ise Arttırma
                {
                    Sepetim.Piece++;
                    works.TempBasketsRepository.Update(Sepetim);
                    works.SaveChanges();
                    return new Result(ResultStatus.Success, "Ürün'ün Adeti Arttırılmıştır.");
                }
                else //false ise Azaltma
                {
                    if (Sepetim.Piece > 1) // 1'dan büyük ise azaltma
                    {
                        Sepetim.Piece--;
                        works.TempBasketsRepository.Update(Sepetim);
                        works.SaveChanges();
                        return new Result(ResultStatus.Success, "Ürün'ün Adeti Azaltılmıştır.");
                    }
                    else
                    {
                        return new Result(ResultStatus.Success, "Ürün'ün Adeti Azaltılamaz.");
                    }
                }               
            }
            catch (Exception)
            {
                return new Result(ResultStatus.Error, "SepetArttırEksilt İşlemi Başarısız");
            }
        }
        public IResult AutoBasketUpdate(AutoBasketDto data)
        {
            try
            {
                works.AutoBasketRepository.Update(mapper.Map<AutoBasket>(data));
                works.SaveChanges();
                return new Result(ResultStatus.Success, "Güncelleme Başarılı");
            }
            catch (Exception)
            {
                return new Result(ResultStatus.Error, "Güncelleme Başarısız");
            }
        }
        public IDataResult<AutoBasketDto> GetByIdAuto(int Id)
        {
            var data = works.AutoBasketRepository.GetByIdFirst(x => x.Id == Id);
            if (data != null)
            {
                return new DataResult<AutoBasketDto>(ResultStatus.Success, "1 Kayıt Getirildi.", mapper.Map<AutoBasketDto>(data));
            }
            else
            {
                return new DataResult<AutoBasketDto>(ResultStatus.Info, "Kayıt Bulunamadı.", null);
            }
        }
    }
}

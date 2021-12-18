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
    public class PImagesManager : IPImagesService
    {
        //automapper ile dönüştürülen dataları repository e gönderip yapılan işlem sonucunu kullanıcıya bildiren sınıflarım
        //repository ile kullanıcı arasındaki iletişimi saglayan yapı.

        private readonly IUnitofWorks works;
        private readonly IMapper mapper;
        public PImagesManager(IUnitofWorks _works, IMapper _mapper)
        {
            works = _works;
            mapper = _mapper;
        }
        public IResult Add(PImagesDto data)
        {
            try
            {                
                works.PImagesRepository.Add(mapper.Map<PImages>(data));
                works.SaveChanges();
                //Başarılı log kaydı tutulacak
                return new Result(ResultStatus.Success, "Kayıt Başarılı");
            }
            catch (Exception)
            {
                //Başarısız log kaydı tutulacak
                return new Result(ResultStatus.Error, "Kayıt Başarısız");
            }
        }
        public IResult Delete(int id)
        {
            try
            {
                works.PImagesRepository.Delete(works.PImagesRepository.GetByIdFirst(x=>x.Id==id));
                works.SaveChanges();                
                return new Result(ResultStatus.Success, "Silme Başarılı");
            }
            catch (Exception)
            {                
                return new Result(ResultStatus.Error, "Silme Başarısız");
            }
        }
        public IDataResult<IList<PImagesDto>> GetAll(int id)
        {
            IList<PImagesDto> data = new List<PImagesDto>();
            foreach (var item in works.PImagesRepository.GetAll(x=>x.ProductsId==id))
            {
                data.Add(mapper.Map<PImagesDto>(item));
            }
            if (data.Count > 0)
            {
                return new DataResult<IList<PImagesDto>>(ResultStatus.Success, data.Count() + "Kayıt Listelendi", data);
            }
            else
            {
                return new DataResult<IList<PImagesDto>>(ResultStatus.Info, " Kayıt Bulunamadı", null);
            }
        }
        public PImagesDto GetByIdFirst(int id)
        {
            return mapper.Map<PImagesDto>(works.PImagesRepository.GetByIdFirst(x => x.Id == id));
        }
        public IResult Update(PImagesDto data)
        {
            try
            {
                works.PImagesRepository.Update(mapper.Map<PImages>(data));
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

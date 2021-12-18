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
    public class VariantsManager : IVariantsService
    {
        private readonly IUnitofWorks works;
        private readonly IMapper mapper;
        public VariantsManager(IUnitofWorks _works, IMapper _mapper)
        {
            works = _works;
            mapper = _mapper;
        }
        public IResult Add(VariantsDto data)
        {
            try
            {
                works.VariantRepository.Add(mapper.Map<Variants>(data));
                works.SaveChanges();
                return new Result(ResultStatus.Success, "Kayıt Başarılı");
            }
            catch (Exception)
            {
                return new Result(ResultStatus.Error, "Kayıt Başarısız");
            }
        }
        public IResult Delete(int id)
        {
            try
            {
                works.VariantRepository.Delete(works.VariantRepository.GetByIdFirst(x => x.Id == id));
                works.SaveChanges();
                return new Result(ResultStatus.Success, "Silme Başarılı");
            }
            catch (Exception)
            {
                return new Result(ResultStatus.Error, "Silme Başarısız");
            }
        }
        public IDataResult<IList<VariantsDto>> GetAll(int Id)
        {
            IList<VariantsDto> data = new List<VariantsDto>();
            foreach (var item in works.VariantRepository.GetAll(x => x.ProductsId == Id))
            {
                data.Add(mapper.Map<VariantsDto>(item));
            }
            if (data.Count > 0)
            {
                return new DataResult<IList<VariantsDto>>(ResultStatus.Success, data.Count() + "Kayıt Listelendi", data);
            }
            else
            {
                return new DataResult<IList<VariantsDto>>(ResultStatus.Info, " Kayıt Bulunamadı", null);
            }
        }
        public VariantsDto GetByIdFirst(int Id)
        {
            return mapper.Map<VariantsDto>(works.VariantRepository.GetByIdFirst(x => x.Id == Id));
        }
        public IResult Update(VariantsDto data)
        {
            try
            {
                works.VariantRepository.Update(mapper.Map<Variants>(data));
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

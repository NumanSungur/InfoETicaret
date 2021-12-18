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
    public class OrderInformationsManager : IOrderInformationsService
    {
        private readonly IUnitofWorks works;
        private readonly IMapper mapper;
        public OrderInformationsManager(IUnitofWorks _works, IMapper _mapper)
        {
            works = _works;
            mapper = _mapper;
        }
        public IResult Add(OrderInformationsDto data)
        {
            try
            {
                works.OrderInformationsRepository.Add(mapper.Map<OrderInformations>(data));
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
                works.OrderInformationsRepository.Delete(works.OrderInformationsRepository.GetByIdFirst(x => x.Id == Id));
                works.SaveChanges();
                return new Result(ResultStatus.Success, "Silme Başarılı");
            }
            catch (Exception)
            {
                return new Result(ResultStatus.Error, "Silme Başarısız");
            }
        }
        public IDataResult<IList<OrderInformationsDto>> GetAll(int Id)
        {
            IList<OrderInformationsDto> data = new List<OrderInformationsDto>();
            foreach (var item in works.OrderInformationsRepository.GetAll(x => x.OrdersId == Id))
            {
                data.Add(mapper.Map<OrderInformationsDto>(item));
            }
            if (data.Count > 0)
            {
                return new DataResult<IList<OrderInformationsDto>>(ResultStatus.Success, data.Count() + "Kayıt Listelendi", data);
            }
            else
            {
                return new DataResult<IList<OrderInformationsDto>>(ResultStatus.Info, " Kayıt Bulunamadı", null);
            }
        }
        public OrderInformationsDto GetByIdFirst(int Id)
        {
            return mapper.Map<OrderInformationsDto>(works.OrderInformationsRepository.GetByIdFirst(x => x.Id == Id));
        }
        public IResult Update(OrderInformationsDto data)
        {
            try
            {
                works.OrderInformationsRepository.Update(mapper.Map<OrderInformations>(data));
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

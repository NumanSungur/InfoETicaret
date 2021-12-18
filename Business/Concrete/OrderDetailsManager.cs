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
    public class OrderDetailsManager : IOrderDetailsService
    {
        private readonly IUnitofWorks works;
        private readonly IMapper mapper;
        public OrderDetailsManager(IUnitofWorks _works, IMapper _mapper)
        {
            works = _works;
            mapper = _mapper;
        }
        public IResult Add(OrderDetailsDto data)
        {
            try
            {
                var datam = mapper.Map<OrderDetails>(data);
                works.OrderDetailsRepository.Add(datam);
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
                works.OrderDetailsRepository.Delete(works.OrderDetailsRepository.GetByIdFirst(x => x.Id == Id));
                works.SaveChanges();
                return new Result(ResultStatus.Success, "Silme Başarılı");
            }
            catch (Exception)
            {
                return new Result(ResultStatus.Error, "Silme Başarısız");
            }
        }
        public IDataResult<IList<OrderDetailsDto>> GetAll(int Id)
        {
            IList<OrderDetailsDto> data = new List<OrderDetailsDto>();
            foreach (var item in works.OrderDetailsRepository.GetAll(x => x.OrdersId == Id))
            {
                data.Add(mapper.Map<OrderDetailsDto>(item));
            }
            if (data.Count > 0)
            {
                return new DataResult<IList<OrderDetailsDto>>(ResultStatus.Success, data.Count() + "Kayıt Listelendi", data);
            }
            else
            {
                return new DataResult<IList<OrderDetailsDto>>(ResultStatus.Info, " Kayıt Bulunamadı", null);
            }
        }
        public OrderDetailsDto GetByIdFirst(int Id)
        {
            return mapper.Map<OrderDetailsDto>(works.OrderDetailsRepository.GetByIdFirst(x => x.Id == Id));
        }
        public IResult Update(OrderDetailsDto data)
        {
            try
            {
                works.OrderDetailsRepository.Update(mapper.Map<OrderDetails>(data));
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

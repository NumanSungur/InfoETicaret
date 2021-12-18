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
    public class OrdersManager : IOrdersService
    {
        private readonly IUnitofWorks works;
        private readonly IMapper mapper;
        public OrdersManager(IUnitofWorks _works, IMapper _mapper)
        {
            works = _works;
            mapper = _mapper;
        }
        public IResult Add(OrdersUpdateDto data)
        {
            try
            {
                works.OrdersRepository.Add(mapper.Map<Orders>(data));
                works.SaveChanges();
                return new Result(ResultStatus.Success, "Kayıt Başarılı");
            }
            catch (Exception e)
            {
                string mesaj = e.Message;
                return new Result(ResultStatus.Error, "Kayıt Başarısız");
            }
        }
        public IResult Delete(int Id)
        {
            try
            {
                works.OrdersRepository.Delete(works.OrdersRepository.GetByIdFirst(x => x.Id == Id));
                works.SaveChanges();
                return new Result(ResultStatus.Success, "Silme Başarılı");
            }
            catch (Exception)
            {
                return new Result(ResultStatus.Error, "Silme Başarısız");
            }
        }

        public IDataResult<IList<OrdersUpdateListDto>> FiveTableGetAll(int Id)
        {
            IList<OrdersUpdateListDto> data = new List<OrdersUpdateListDto>();
            foreach (var item in works.OrdersRepository.GetAll(x=>x.Id == Id,a=>a.Customers, a => a.OrderDetails, a => a.OrderNotes, a => a.OrderInformations))
            {
                OrdersUpdateListDto dt = new OrdersUpdateListDto();
                dt = mapper.Map<OrdersUpdateListDto>(item); //order
                dt.CustomersUpdateDto = mapper.Map<CustomersUpdateDto>(item.Customers); // Customers
                dt.OrderDetailsDto = mapper.Map<IList<OrderDetailsDto>>(item.OrderDetails); //ıcollection orderdetail
                dt.OrderNotesDto = mapper.Map<IList<OrderNotesDto>>(item.OrderNotes); //ıcollection ordernotes
                dt.OrderInformationsDto = mapper.Map<IList<OrderInformationsDto>>(item.OrderInformations); //ıcollection orderınformation
                data.Add(dt);
            }
            if (data.Count > 0)
            {
                return new DataResult<IList<OrdersUpdateListDto>>(ResultStatus.Success, data.Count() + "Kayıt Listelendi", data);
            }
            else
            {
                return new DataResult<IList<OrdersUpdateListDto>>(ResultStatus.Info, " Kayıt Bulunamadı", null);
            }
        }

        public IDataResult<IList<OrdersDto>> GetAll(string Durumu)
        {
            IList<OrdersDto> data = new List<OrdersDto>();
            foreach (var item in (Durumu != null) ? works.OrdersRepository.GetAll(x => x.OrderStatus == Durumu) : works.OrdersRepository.GetAll()) 
            {
                data.Add(mapper.Map<OrdersDto>(item));
            }
            if (data.Count > 0)
            {
                return new DataResult<IList<OrdersDto>>(ResultStatus.Success, data.Count() + "Kayıt Listelendi", data);
            }
            else
            {
                return new DataResult<IList<OrdersDto>>(ResultStatus.Info, " Kayıt Bulunamadı", null);
            }
        }
        public IDataResult<OrdersUpdateDto> GetById(int Id)
        {
            var data = works.OrdersRepository.GetByIdFirst(x => x.Id == Id);
            if (data != null)
            {
                return new DataResult<OrdersUpdateDto>(ResultStatus.Success, "1 Kayıt Getirildi.", mapper.Map<OrdersUpdateDto>(data));
            }
            else
            {
                return new DataResult<OrdersUpdateDto>(ResultStatus.Info, "Kayıt Bulunamadı.", null);
            }
        }
        public IResult Update(OrdersUpdateDto data)
        {
            try
            {
                works.OrdersRepository.Add(mapper.Map<Orders>(data));
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

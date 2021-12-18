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
    public class CustomersManager : ICustomersService
    {
        private readonly IUnitofWorks works;
        private readonly IMapper mapper;
        public CustomersManager(IUnitofWorks _works, IMapper _mapper)
        {
            works = _works;
            mapper = _mapper;
        }
        public IResult Add(CustomersUpdateDto data)
        {
            try
            {
                works.CustomersRepository.Add(mapper.Map<Customers>(data));
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
                works.CustomersRepository.Delete(works.CustomersRepository.GetByIdFirst(x => x.Id == Id));
                works.SaveChanges();
                return new Result(ResultStatus.Success, "Silme Başarılı");
            }
            catch (Exception)
            {
                return new Result(ResultStatus.Error, "Silme Başarısız");
            }
        }
        public IDataResult<IList<CustomersDto>> GetAll()
        {
            IList<CustomersDto> data = new List<CustomersDto>();
            foreach (var item in works.CustomersRepository.GetAll())
            {
                data.Add(mapper.Map<CustomersDto>(item));
            }
            if (data.Count > 0)
            {
                return new DataResult<IList<CustomersDto>>(ResultStatus.Success, data.Count() + "Kayıt Listelendi", data);
            }
            else
            {
                return new DataResult<IList<CustomersDto>>(ResultStatus.Info, " Kayıt Bulunamadı", null);
            }
        }
        public IDataResult<CustomersUpdateDto> GetById(int Id)
        {
            var data = works.CustomersRepository.GetByIdFirst(x => x.Id == Id);
            if (data != null)
            {                
                return new DataResult<CustomersUpdateDto>(ResultStatus.Success, "1 Kayıt Getirildi.", mapper.Map<CustomersUpdateDto>(data));
            }
            else
            {
                return new DataResult<CustomersUpdateDto>(ResultStatus.Info, "Kayıt Bulunamadı.", null);
            }
        }
        public IDataResult<CustomersDto> Login(string Email, string Phone, string Password)
        {             
            if (Email == null) // Eposta Boş Telefon Doluysa Çalışacak.
            {
                var data = works.CustomersRepository.GetByIdFirst(x => x.Phone == Phone && x.Password == Password);
                if (data != null)
                {
                    return new DataResult<CustomersDto>(ResultStatus.Success, "1 Kayıt Getirildi.", mapper.Map<CustomersDto>(data));
                }
                else
                {
                    return new DataResult<CustomersDto>(ResultStatus.Info, "Kayıt Bulunamadı.", null);
                }
            }
            else if (Phone == null) // Telefon Boş Eposta Doluysa Çalışacak.
            {
                var data = works.CustomersRepository.GetByIdFirst(x => x.Email == Email && x.Password == Password);
                if (data != null)
                {
                    return new DataResult<CustomersDto>(ResultStatus.Success, "1 Kayıt Getirildi.", mapper.Map<CustomersDto>(data));
                }
                else
                {
                    return new DataResult<CustomersDto>(ResultStatus.Info, "Kayıt Bulunamadı.", null);
                }
            }
            else // Eposta ve Telefon Doluysa Çalışacak.
            {
                var data = works.CustomersRepository.GetByIdFirst(x => x.Email == Email && x.Phone == Phone && x.Password == Password);
                if (data != null)
                {
                    return new DataResult<CustomersDto>(ResultStatus.Success, "1 Kayıt Getirildi.", mapper.Map<CustomersDto>(data));
                }
                else
                {
                    return new DataResult<CustomersDto>(ResultStatus.Info, "Kayıt Bulunamadı.", null);
                }
            }
        }

        public IResult Update(CustomersUpdateDto data)
        {
            try
            {
                works.CustomersRepository.Update(mapper.Map<Customers>(data));
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

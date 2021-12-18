using Business.Abstract;
using Entities.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Authorize]
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService db;
        public ProductsController(IProductsService _db)
        {
            db = _db;
        }
        [HttpGet]
        public IList<ProductsDto> Get()
        {
            return db.GetAll().Data;
        }
        [HttpGet("GetById/{Id}")]
        public ProductsUpdateDto GetById(int Id)
        {
            return db.GetById(Id).Data;
        }
        [HttpGet("FiyatınaGore/{Sart}")]
        public IList<ProductsDto> Get(bool Sart)
        {
            if (Sart) //true ya da false
            {
                return db.GetAll().Data.OrderBy(x => x.Price).ToList();
            }
            else
            {
                return db.GetAll().Data.OrderByDescending(x => x.Price).ToList();
            }
        }
        [HttpPost]
        public IActionResult Post(ProductsUpdateDto data)
        {
            db.Add(data);
            return Ok("Başarılı");
        }
        [HttpPut("{Id}")]
        public IActionResult Put(int Id, ProductsUpdateDto data)
        {
            data.Id = Id;
            db.Update(data);
            return Ok("Başarılı");
        }
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            db.Delete(Id);
            return Ok("Başarılı");
        }
    }
}

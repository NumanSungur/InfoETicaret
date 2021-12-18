using Business.Abstract;
using Entities.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace UIAdmin.Controllers
{
    [Authorize(Roles = "Admin,Temsilci")]
    public class OrdersController : Controller
    {
        private readonly IOrdersService db;
        private readonly IOrderInformationsService dbInfo;
        public OrdersController(IOrdersService _db, IOrderInformationsService _dbInfo)
        {
            db = _db;
            dbInfo = _dbInfo;
        }
        public async Task<IActionResult> Index()
        {
            // var data = db.FiveTableGetAll();
            return View(db.GetAll(null).Data);
        }
        [Route("/Orders/Detail/{Id:int?}")]
        public async Task<IActionResult> Detail(int Id)
        {
            return View(db.FiveTableGetAll(Id).Data);
        }
        [HttpPost]
        [Route("/Orders/Detail/{Id:int?}")]
        public async Task<IActionResult> Detail(int Id, OrderInformationsDto data)
        {
            data.Sms = (Request.Form["Sms"] == "1" ? true : false);
            data.Email = (Request.Form["Email"] == "1" ? true : false);
            data.InfoDate = DateTime.Now;
            data.OrdersId = Id;
            data.Id = 0;
            data.CustomerId = db.FiveTableGetAll(Id).Data.FirstOrDefault().CustomersId;
            dbInfo.Add(data);

            return View(db.FiveTableGetAll(Id).Data);
        }
        public async Task<IActionResult> Teslim()
        {            
            return View(db.GetAll("Teslim Edildi").Data);
        }
        public async Task<IActionResult> İptal()
        {
            return View(db.GetAll("İptal Edildi").Data);
        }
        public async Task<IActionResult> İade()
        {
            return View(db.GetAll("İade Edildi").Data);
        }
    }
}

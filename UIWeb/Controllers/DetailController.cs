using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UIWeb.Controllers
{
    [AllowAnonymous]
    public class DetailController : Controller
    {
        private readonly IProductsService db;
        private readonly IPImagesService ımagesdb;
        private readonly IVariantsService variantsdb;
        public DetailController(IProductsService _db, IPImagesService _ımagesdb, IVariantsService _variantsdb)
        {
            db = _db;
            ımagesdb = _ımagesdb;
            variantsdb = _variantsdb;
        }
        [Route("/urun/{UrunAdi}/{Id:int}")]
        public IActionResult Index(string UrunAdi,int Id)
        {
            ViewBag.Resimler = ımagesdb.GetAll(Id).Data;
            ViewBag.Varyant = variantsdb.GetAll(Id).Data;
            if (ViewBag.Varyant != null)
            {
                ViewBag.VaryantGroup = variantsdb.GetAll(Id).Data.GroupBy(x => x.GroupName).FirstOrDefault().Key;
            }
            return View(db.GetById(Id).Data);
        }
    }
}

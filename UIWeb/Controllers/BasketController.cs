using Business.Abstract;
using Entities.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UIWeb.Controllers
{
    [AllowAnonymous]
    public class BasketController : Controller
    {
        private readonly ITempBasketsService sepet;
        private readonly IProductsService db;
        private readonly IVariantsService variantdb;
        public BasketController(ITempBasketsService _sepet, IProductsService _db, IVariantsService _variantdb)
        {
            sepet = _sepet;
            db = _db;
            variantdb = _variantdb;
        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult SepetDelete(int ID)
        {
            sepet.Delete(ID);
            return Json("");
        }
        public  JsonResult JSONStokAyari(int ID, int Durum)
        {
            return Json(sepet.SepetArttırEksilt(ID, (Durum == 0) ? false : true));
        }
        public IActionResult PartialSepetGet()
        {
            int Cookie = Convert.ToInt32(Request.Cookies["SepetId"]);
            return PartialView("Views/PartialView/_PartialViewSepet.cshtml", sepet.GetAll(Cookie).Data);
        }
        public JsonResult DetailSepetInsert(int ID, int varyant)
        {
            string Mesaj = "";
            if (Request.Cookies["SepetId"] == null)
            {
                // Yeni Cookies Üretme Ekranı.
                int Bulunan = sepet.GetByIdAuto(1).Data.Uretilen + 1;// İlk Başta Veritanındaki Cookie'yi alıp üzerine 1 ekliyor.
                CookieOptions cookie = new CookieOptions();
                cookie.Expires = DateTime.Now.AddDays(6);
                Response.Cookies.Append("SepetId", Bulunan.ToString(), cookie);

                AutoBasketDto dto = sepet.GetByIdAuto(1).Data;
                dto.Uretilen++;
                sepet.AutoBasketUpdate(dto);
                // Yeni Cookies Üretme Ekranı.

                Mesaj = sepet.AddUpdate(Convert.ToInt32(ID), Bulunan, varyant).Message;
            }
            else
            {
                int Cookie = Convert.ToInt32(Request.Cookies["SepetId"]);
                Mesaj = sepet.AddUpdate(Convert.ToInt32(ID), Cookie, varyant).Message;
            }

            return Json(Mesaj);
        }
    }
}

using Business.Abstract;
using Core.Results.ComplexTypes;
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
    public class HomeController : Controller
    {
        private readonly IProductsService service;
        private readonly ITempBasketsService sepet;
        private readonly IVariantsService variantdb;
        public HomeController(IProductsService _service, ITempBasketsService _sepet, IVariantsService _variantdb)
        {
            service = _service;
            sepet = _sepet;
            variantdb = _variantdb;
        }
        public IActionResult Index()
        {
            //Hata veya olumlu mesajları ve dataları taşıyan bir sınıf yapmamız lazımdır
            //if (service.GetAll().ResultStatus==ResultStatus.Success)
            //{
            //    var data = service.GetAll().Data;
            //    var mesaj = service.GetAll().Message;
            //}
            //else
            //{
            //    var data = service.GetAll().Message;
            //}
            return View(service.GetAll());
        }
        public JsonResult SepetEkle(string ID)
        {
            string Mesaj = "";
            if (variantdb.GetAll(int.Parse(ID)).Data != null)
            {
                Mesaj = "Bu Ürüne Ait varyasyonlar olduğu için detay sayfasından seçim yaparak ekleyebilirsiniz.";
            }
            else
            {
                if (Request.Cookies["SepetId"] == null)
                {
                    //Yeni Cookies Üretme Ekranı
                    int Bulunan = sepet.GetByIdAuto(1).Data.Uretilen + 1; // İlk Başta Veritanındaki Cookie'yi alıp üzerine 1 ekliyor.
                    CookieOptions cookie = new CookieOptions();
                    cookie.Expires = DateTime.Now.AddDays(6);
                    Response.Cookies.Append("SepetId", Bulunan.ToString(), cookie);

                    //Eger Cookies arayüz tarafından 0 olarak gönderilmişse bu demek oluyor ki o kullanıcı ilk defa sepete ürün ekliyor demektir.
                    AutoBasketDto dto = sepet.GetByIdAuto(1).Data;
                    dto.Uretilen++;
                    sepet.AutoBasketUpdate(dto);
                    //Yeni Cookies Üretme Ekranı

                    Mesaj = sepet.AddUpdate(Convert.ToInt32(ID), Bulunan, 0).Message;
                }
                else
                {
                    int Cookie = Convert.ToInt32(Request.Cookies["SepetId"]);
                    Mesaj = sepet.AddUpdate(Convert.ToInt32(ID), Cookie, 0).Message;
                }
            }
            return Json(Mesaj);
        }
    }
}

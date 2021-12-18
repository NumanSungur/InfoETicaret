using Business.Abstract;
using Core.Results.ComplexTypes;
using Entities.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace UIAdmin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductsService db;
        private readonly IPImagesService imagesdb;
        public ProductController(IProductsService _db, IPImagesService _imagesdb)
        {
            db = _db;
            imagesdb = _imagesdb;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(db.GetAll().Data);
        }
        [HttpGet,Route("/Product/Insert")]
        public async Task<IActionResult> Insert()
        {
            return View();
        }
        [HttpPost, Route("/Product/Insert")]
        public async Task<IActionResult> Insert(ProductsUpdateDto data,IFormFile Files,IList<IFormFile> MultiFiles)
        {
            var DataKategori = Request.Form["CategoriesId"];
            foreach (var item in DataKategori)
            {
                if (item != "0")
                {
                    data.CategoriesId = Convert.ToInt32(item);
                }
            }
            if (Files != null)
            {
                string NewName = Guid.NewGuid() + Path.GetExtension(Files.FileName);
                string Kayityolu = Path.Combine(URLPath.URLFile, $"wwwroot/images/Products/" + NewName);
                Files.CopyTo(new FileStream(Kayityolu, FileMode.Create));
                data.MainImages = NewName;
                if (db.Add(data).ResultStatus== ResultStatus.Success)
                {
                    //Detay resmi ekleme bölümü.ürün eklemesi başarılı ise detay resimlerini eklemeye başlasın.
                    int BulunanId = db.SearchId(data.Name, data.Price, data.Stock).Data;
                    //Detay Resmi Ekleme Zorunluluğu Yok.
                    if (MultiFiles !=null)
                    {
                        foreach (var item in MultiFiles)
                        {
                            string NewNameDetail = Guid.NewGuid() + Path.GetExtension(item.FileName);
                            string KayityoluDetail = Path.Combine(URLPath.URLFile, $"wwwroot/images/Products/detail/" + NewNameDetail);
                            item.CopyTo(new FileStream(KayityoluDetail, FileMode.Create));
                            PImagesDto pImages = new PImagesDto();
                            pImages.ProductsId = BulunanId;
                            pImages.Name = NewNameDetail;
                            imagesdb.Add(pImages);
                        }
                    }
                    ViewData["Message"] = "<div class='alert alert-success'>Bilgiler Eklendi.</div>";
                }
                else
                {
                    ViewData["Message"] = "<div class='alert alert-danger'>Bir Hata ile Karşılaşıldı.Lütfen Tekrar Deneyiniz.</div>";
                }
            }
            else
            {
                ViewData["Message"] = "<div class='alert alert-danger'>Ürün Ana Resmi Seçilmedi...</div>";
            }
            return View();
        }
        [HttpGet, Route("/Product/Update/{Id:int}")]
        public async Task<IActionResult> Update(int Id)
        {
            ViewBag.detayResim = imagesdb.GetAll(Id).Data;
            return View(db.GetById(Id).Data);
        }
        [HttpPost, Route("/Product/Update/{Id:int}")]
        public async Task<IActionResult> Update(int Id, ProductsUpdateDto data, IFormFile Files, IList<IFormFile> MultiFiles)
        {
            if (Request.Form["CategoriesId"] != "0")
            {
                var DataKategori = Request.Form["CategoriesId"];
                foreach (var item in DataKategori)
                {
                    if (item != "0")
                    {
                        data.CategoriesId = Convert.ToInt32(item);
                    }
                }
            }
            else
            {
                data.CategoriesId = Convert.ToInt32(Request.Form["KategoriGizli"]);
            }
            //Detay Resmi Kontrol
            if (MultiFiles !=null)
            {
                foreach (var item in MultiFiles)
                {
                    string NewNameDetail = Guid.NewGuid() + Path.GetExtension(item.FileName);
                    string KayityoluDetail = Path.Combine(URLPath.URLFile, $"wwwroot/images/Products/detail/" + NewNameDetail);
                    item.CopyTo(new FileStream(KayityoluDetail, FileMode.Create));
                    PImagesDto pImages = new PImagesDto();
                    pImages.ProductsId = Id;
                    pImages.Name = NewNameDetail;
                    imagesdb.Add(pImages);
                }
            }
            data.Id = Id;
            if (Files != null)
            {
                //ana resim güncellenmek isteniyorsa
                // Silinecek olan Dosyanın Konumu ve ismini belirtip siliyoruz.
                //System.IO.File.Delete(Path.Combine(URLPath.URLFile, $"wwwroot/images/Products/" + db.GetById(Id).Data.MainImages));
                string NewName = Guid.NewGuid() + Path.GetExtension(Files.FileName);
                string Kayityolu = Path.Combine(URLPath.URLFile, $"wwwroot/images/Products/" + NewName);
                Files.CopyTo(new FileStream(Kayityolu, FileMode.Create));
                data.MainImages = NewName;
                if (db.Update(data).ResultStatus == ResultStatus.Success)
                {
                    ViewData["Message"] = "<div class='alert alert-success'>Bilgiler Güncellendi.</div>";
                }
                else
                {
                    ViewData["Message"] = "<div class='alert alert-danger'>Bilgiler Güncellenemedi.</div>";
                }
            }
            else
            {
                //ana resim güncellenmek istenmiyorsa   
                if (db.Update(data).ResultStatus== ResultStatus.Success)
                {
                    ViewData["Message"] = "<div class='alert alert-success'>Bilgiler Güncellendi.</div>";
                }
                else
                {
                    ViewData["Message"] = "<div class='alert alert-danger'>Bilgiler Güncellenemedi.</div>";
                }
            }
            ViewBag.detayResim = imagesdb.GetAll(Id).Data;
            return View(db.GetById(Id).Data);
        }
        [HttpGet, Route("/Product/Delete/{Id:int}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var ProductData = db.GetById(Id).Data;
            db.Delete(Id);
            System.IO.File.Delete(Path.Combine(URLPath.URLFile, $"wwwroot/images/Products/" + ProductData.MainImages));
            if (imagesdb.GetAll(ProductData.Id).ResultStatus== ResultStatus.Success)
            {
                foreach (var item in imagesdb.GetAll(ProductData.Id).Data)
                {
                    imagesdb.Delete(item.Id);
                    System.IO.File.Delete(Path.Combine(URLPath.URLFile, $"wwwroot/images/Products/detail/" + item.Name));
                }
            }
            return Redirect("/Product");
        }
        [Route("/Product/ImagesDelete/{Id:int}")]
        public async Task<JsonResult> ImagesDelete(int Id)
        {
            var ImagesData = imagesdb.GetByIdFirst(Id);
            imagesdb.Delete(Id);
            System.IO.File.Delete(Path.Combine(URLPath.URLFile, $"wwwroot/images/Products/detail/" + ImagesData.Name));
            return Json("Resim Silindi.");
        }
    }
}

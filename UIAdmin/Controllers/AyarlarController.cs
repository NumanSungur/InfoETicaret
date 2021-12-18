using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UIAdmin.Controllers
{
    public class AyarlarController : Controller
    {
        [Authorize(Roles = "Admin,Temsilci")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin,Temsilci")]
        [HttpPost]
        public IActionResult Index(string a)
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Insert()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Update()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete()
        {
            return View();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLySinhVienThucTap.Areas.Sinhvien.Controllers
{
    public class HomeController : Controller
    {
        // GET: Sinhvien/Home
        public ActionResult Index()
        {
            ViewBag.ActivePage = "Home";
            ViewBag.TieuDe = "Trang chủ";
            return View();
        }
    }
}
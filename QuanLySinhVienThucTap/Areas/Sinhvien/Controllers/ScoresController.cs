using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLySinhVienThucTap.Areas.Sinhvien.Controllers
{
    public class ScoresController : Controller
    {
        // GET: Sinhvien/Scores
        public ActionResult Index()
        {
            ViewBag.ActivePage = "Scores";
            ViewBag.TieuDe = "Điểm thực tập";
            return View();
        }
    }
}
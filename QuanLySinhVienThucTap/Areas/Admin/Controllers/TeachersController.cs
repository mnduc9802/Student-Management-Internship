using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using OfficeOpenXml;
using PagedList;
using QuanLySinhVienThucTap.Models;

namespace QuanLySinhVienThucTap.Areas.Admin.Controllers
{
    public class TeachersController : Controller
    {
        private QLSVTTEntities db = new QLSVTTEntities();

        // GET: Admin/Teachers
        public ActionResult Index(string lastname, string firstname, string office, string magv, int? page, string ErrorMessage)
        {
            ViewBag.DeMuc = "Quản lý giảng viên";
            ViewBag.ActivePage = "Teachers";
            ViewBag.TieuDe = "Giảng viên";
            var Teacher = from s in db.Teachers select s;

            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ViewBag.ErrorMessage = ErrorMessage;
            }

            if (!String.IsNullOrEmpty(lastname))
            {
                Teacher = Teacher.Where(s => s.LastName.Contains(lastname));
            }

            if (!String.IsNullOrEmpty(firstname))
            {
                Teacher = Teacher.Where(s => s.FirstName.Contains(firstname));
            }

            if (!String.IsNullOrEmpty(office))
            {
                Teacher = Teacher.Where(s => s.Office.Contains(office));
            }

            if (!String.IsNullOrEmpty(magv))
            {
                if (magv.Length == 8 && magv.StartsWith("010263"))
                {
                    int teacherID = Convert.ToInt32(magv.Substring(6));
                    Teacher = Teacher.Where(s => s.TeacherID== teacherID);
                }
            }

            if (!Teacher.Any())
            {
                ViewBag.ErrorMessage = "Không tìm thấy kết quả phù hợp.";
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(Teacher.OrderBy(h => h.TeacherID).ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Teachers/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.DeMuc = "Quản lý giảng viên";
            ViewBag.ActivePage = "Teachers";
            ViewBag.TieuDe = "Giảng viên";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // GET: Admin/Teachers/Create
        public ActionResult Create()
        {
            ViewBag.DeMuc = "Quản lý giảng viên";
            ViewBag.ActivePage = "Teachers";
            ViewBag.TieuDe = "Giảng viên";
            return View();
        }

        // POST: Admin/Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeacherID,LastName,FirstName,Address,Gender,DateOfBirth,Email,PhoneNumber,Office")] Teacher teacher)
        {
            if (teacher.DateOfBirth > DateTime.Today)
            {
                ModelState.AddModelError("DateOfBirth", "Ngày sinh không chính xác.");
                ViewBag.DeMuc = "Quản lý giảng viên";
                ViewBag.ActivePage = "Teachers";
                ViewBag.TieuDe = "Giảng viên";
            }
            if (ModelState.IsValid)
            {
                db.Teachers.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(teacher);
        }

        // GET: Admin/Teachers/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.DeMuc = "Quản lý giảng viên";
            ViewBag.ActivePage = "Teachers";
            ViewBag.TieuDe = "Giảng viên";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Admin/Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeacherID,LastName,FirstName,Address,Gender,DateOfBirth,Email,PhoneNumber,Office")] Teacher teacher)
        {
            if (teacher.DateOfBirth > DateTime.Today)
            {
                ModelState.AddModelError("DateOfBirth", "Ngày sinh không chính xác.");
                ViewBag.DeMuc = "Quản lý giảng viên";
                ViewBag.ActivePage = "Teachers";
                ViewBag.TieuDe = "Giảng viên";
            }
            if (ModelState.IsValid)
            {
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(teacher);
        }

        // POST: Admin/Teachers/Delete/5
        [HttpPost]
        public JsonResult Delete(int ID)
        {
            int rowCount = db.Teachers.Count();
            Teacher teacher = db.Teachers.FirstOrDefault(x => x.TeacherID == ID);
            if (teacher != null)
            {
                db.Teachers.Remove(teacher);
                db.SaveChanges();
            }
            db.Database.ExecuteSqlCommand($"DBCC CHECKIDENT('dbo.Teacher', RESEED, {rowCount - 1})");
            return Json("");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public ActionResult Import(FormCollection form)
        {
            ViewBag.ControllerName = "Teachers";
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    var teacherList = new List<Teacher>();
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;
                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                        {
                            var teacher = new Teacher();
                            teacher.TeacherID = Convert.ToInt32(workSheet.Cells[rowIterator, 1].Value);
                            teacher.LastName = workSheet.Cells[rowIterator, 2].Value.ToString();
                            teacher.FirstName = workSheet.Cells[rowIterator, 3].Value.ToString();
                            teacher.Gender = workSheet.Cells[rowIterator, 4].Value.ToString();
                            string dateString = workSheet.Cells[rowIterator, 5].Value.ToString();
                            DateTime dateOfBirth;
                            if (DateTime.TryParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOfBirth))
                            {
                                teacher.DateOfBirth = dateOfBirth;
                            }
                            else
                            {
                                teacher.DateOfBirth = DateTime.MinValue;
                            }
                            teacher.Email = workSheet.Cells[rowIterator, 6].Value.ToString();
                            teacher.PhoneNumber = workSheet.Cells[rowIterator, 7].Value.ToString();
                            teacher.Office = workSheet.Cells[rowIterator, 8].Value.ToString();
                            teacher.Address = workSheet.Cells[rowIterator, 9].Value.ToString();
                            teacherList.Add(teacher);
                        }
                    }

                    foreach (var item in teacherList)
                    {
                        if (!db.Teachers.Any(s => s.Email == item.Email))
                        {
                            db.Teachers.Add(item);
                        }
                    }

                    int result = db.SaveChanges();
                    if (result > 0)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Giảng viên đã tồn tại!!!";
                        return RedirectToAction("Index", new { ErrorMessage = ViewBag.ErrorMessage });
                    }
                }
            }
            return View("Index");
        }
        [HttpPost]
        public FileResult Export()
        {
            DataTable dt = GetTeacherDataTable();

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add(dt, "Teachers"); 

                ws.Range("A1:J1").Merge();
                ws.Cell(1, 1).Value = "Danh sách giảng viên";
                ws.Cell(1, 1).Style.Font.FontSize = 16;
                ws.Cell(1, 1).Style.Font.Bold = true;
                ws.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws.Cell(2, 1).Value = "STT";
                ws.Cell(2, 2).Value = "Mã giảng viên";
                ws.Cell(2, 3).Value = "Họ đệm";
                ws.Cell(2, 4).Value = "Tên";
                ws.Cell(2, 5).Value = "Giới tính";
                ws.Cell(2, 6).Value = "Ngày sinh";
                ws.Cell(2, 7).Value = "Email";
                ws.Cell(2, 8).Value = "PhoneNumber";
                ws.Cell(2, 9).Value = "Office";
                ws.Cell(2, 10).Value = "Address";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        ws.Cell(i + 3, j + 1).Value = dt.Rows[i][j].ToString();
                    }
                }

                var headerRange = ws.Range("A2:J2");
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws.Columns().AdjustToContents();

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DanhSachGiangVien.xlsx");
                }
            }
        }
        public DataTable GetTeacherDataTable()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[10]
            {
                new DataColumn("TeacherID"),
                new DataColumn("TeacherCode"),
                new DataColumn("LastName"),
                new DataColumn("FirstName"),
                new DataColumn("Gender"),
                new DataColumn("DateOfBirth"),
                new DataColumn("Email"),
                new DataColumn("PhoneNumber"),
                new DataColumn("Office"),
                new DataColumn("Address")
            });

            var teachers = db.Teachers.ToList();

            foreach (var sv in teachers)
            {
                string magv;
                if (sv.TeacherID < 10)
                {
                    magv = "0102630" + sv.TeacherID;
                }
                else
                {
                    magv = "010263" + sv.TeacherID;
                }
                dt.Rows.Add(sv.TeacherID, magv ,sv.LastName, sv.FirstName, sv.Gender, sv.DateOfBirth, sv.Email,
                            sv.PhoneNumber, sv.Office, sv.Address);
            }

            return dt;
        }
        public ActionResult Download()
        {
            string filePath = Server.MapPath("~/Content/assets/file/GiangVien.xlsx");

            if (System.IO.File.Exists(filePath))
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=GiangVien.xlsx");

                Response.TransmitFile(filePath);
                Response.End();
            }
            else
            {
                return HttpNotFound("File not found");
            }
            return new EmptyResult();
        }
    }
}

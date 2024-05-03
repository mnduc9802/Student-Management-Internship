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
using DocumentFormat.OpenXml.Office.CustomXsn;
using DocumentFormat.OpenXml.Spreadsheet;
using OfficeOpenXml;
using PagedList;
using QuanLySinhVienThucTap.Models;

namespace QuanLySinhVienThucTap.Areas.Admin.Controllers
{
    public class InternshipsController : Controller
    {
        private QLSVTTEntities db = new QLSVTTEntities();

        // GET: Admin/Internships
        public ActionResult Index(string masv, string macb, string magv, int? page)
        {
            ViewBag.DeMuc = "Quản lý tiến độ thực tập";
            ViewBag.TieuDe = "Tiến độ thực tập";
            ViewBag.ActivePage = "Internships";
            var Internship = from s in db.Internships select s;

            if (!String.IsNullOrEmpty(masv))
            {
                Internship = Internship.Where(s => s.Student.StudentCode.Contains(masv));
            }

            if (!String.IsNullOrEmpty(macb))
            {
                if (macb.Length == 8 && macb.StartsWith("010263"))
                {
                    int emID = Convert.ToInt32(macb.Substring(6));
                    Internship = Internship.Where(s => s.EmployeeID == emID);
                }
            }

            if (!String.IsNullOrEmpty(magv))
            {
                if (magv.Length == 8 && magv.StartsWith("010263"))
                {
                    int gvID = Convert.ToInt32(magv.Substring(6));
                    Internship = Internship.Where(s => s.TeacherID == gvID);
                }
            }

            if (!Internship.Any())
            {
                ViewBag.ErrorMessage = "Không tìm thấy kết quả phù hợp.";
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(Internship.OrderBy(a => a.InternShipID).ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Internships/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.DeMuc = "Quản lý tiến độ thực tập";
            ViewBag.TieuDe = "Tiến độ thực tập";
            ViewBag.ActivePage = "Internships";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Internship internship = db.Internships.Find(id);
            if (internship == null)
            {
                return HttpNotFound();
            }
            return View(internship);
        }

        // GET: Admin/Internships/Create
        public ActionResult Create()
        {
            ViewBag.DeMuc = "Quản lý tiến độ thực tập";
            ViewBag.TieuDe = "Tiến độ thực tập";
            ViewBag.ActivePage = "Internships";
            var emList = db.Employees.Select(item => new {
                EmployeeID = item.EmployeeID,
                macb = item.EmployeeID < 10 ? "0102630" + item.EmployeeID : "010263" + item.EmployeeID
            }).ToList();
            var gvList = db.Teachers.Select(item => new {
                TeacherID = item.TeacherID,
                magv = item.TeacherID < 10 ? "0102630" + item.TeacherID : "010263" + item.TeacherID
            }).ToList();
            ViewBag.EmployeeID = new SelectList(emList, "EmployeeID", "macb");
            ViewBag.TeacherID = new SelectList(gvList, "TeacherID", "magv");
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentCode");
            return View();
        }

        // POST: Admin/Internships/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InternShipID,StudentID,EmployeeID,TeacherID,Start_Day,End_Day")] Internship internship)
        {
            if (ModelState.IsValid)
            {
                db.Internships.Add(internship);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var emList = db.Employees.Select(item => new {
                EmployeeID = item.EmployeeID,
                macb = item.EmployeeID < 10 ? "0102630" + item.EmployeeID : "010263" + item.EmployeeID
            }).ToList();
            var gvList = db.Teachers.Select(item => new {
                TeacherID = item.TeacherID,
                magv = item.TeacherID < 10 ? "0102630" + item.TeacherID : "010263" + item.TeacherID
            }).ToList();
            ViewBag.EmployeeID = new SelectList(emList, "EmployeeID", "macb", internship.EmployeeID);
            ViewBag.TeacherID = new SelectList(gvList, "TeacherID", "magv", internship.TeacherID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentCode", internship.StudentID);
            return View(internship);
        }

        // GET: Admin/Internships/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.DeMuc = "Quản lý tiến độ thực tập";
            ViewBag.TieuDe = "Tiến độ thực tập";
            ViewBag.ActivePage = "Internships";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Internship internship = db.Internships.Find(id);
            if (internship == null)
            {
                return HttpNotFound();
            }
            var emList = db.Employees.Select(item => new {
                EmployeeID = item.EmployeeID,
                macb = item.EmployeeID < 10 ? "0102630" + item.EmployeeID : "010263" + item.EmployeeID
            }).ToList();
            var gvList = db.Teachers.Select(item => new {
                TeacherID = item.TeacherID,
                magv = item.TeacherID < 10 ? "0102630" + item.TeacherID : "010263" + item.TeacherID
            }).ToList();
            ViewBag.EmployeeID = new SelectList(emList, "EmployeeID", "macb", internship.EmployeeID);
            ViewBag.TeacherID = new SelectList(gvList, "TeacherID", "magv", internship.TeacherID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentCode", internship.StudentID);
            return View(internship);
        }

        // POST: Admin/Internships/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InternShipID,StudentID,EmployeeID,TeacherID,Start_Day,End_Day")] Internship internship)
        {
            if (ModelState.IsValid)
            {
                db.Entry(internship).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var emList = db.Employees.Select(item => new {
                EmployeeID = item.EmployeeID,
                macb = item.EmployeeID < 10 ? "0102630" + item.EmployeeID : "010263" + item.EmployeeID
            }).ToList();
            var gvList = db.Teachers.Select(item => new {
                TeacherID = item.TeacherID,
                magv = item.TeacherID < 10 ? "0102630" + item.TeacherID : "010263" + item.TeacherID
            }).ToList();
            ViewBag.EmployeeID = new SelectList(emList, "EmployeeID", "macb", internship.EmployeeID);
            ViewBag.TeacherID = new SelectList(gvList, "TeacherID", "magv", internship.TeacherID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentCode", internship.StudentID);
            return View(internship);
        }

        // POST: Admin/Internships/Delete/5
        [HttpPost]
        public JsonResult Delete(int ID)
        {
            int rowCount = db.Internships.Count();
            Internship internship = db.Internships.FirstOrDefault(x => x.InternShipID == ID);
            if (internship != null)
            {
                db.Internships.Remove(internship);
                db.SaveChanges();
            }
            db.Database.ExecuteSqlCommand($"DBCC CHECKIDENT('dbo.Internship', RESEED, {rowCount - 1})");
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
            ViewBag.ControllerName = "Internships";
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    var internshipsList = new List<Internship>();
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;
                        var studentDict = db.Students.ToDictionary(f => $"{f.LastName} {f.FirstName}", x => x.StudentID);
                        var employeeDict = db.Employees.ToDictionary(f => f.Name, x => x.EmployeeID);
                        var teacherDict = db.Teachers.ToDictionary(f => $"{f.LastName} {f.FirstName}", x => x.TeacherID);
                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                        {
                            var internship = new Internship();
                            internship.InternShipID = Convert.ToInt32(workSheet.Cells[rowIterator, 1].Value);
                            var studentName = workSheet?.Cells[rowIterator, 2]?.Value?.ToString()?.Trim() ?? "Null Value";
                            if (!studentDict.ContainsKey(studentName))
                            {
                                ViewBag.StudentError = "Sinh viên không tồn tại";
                                continue;
                            }
                            internship.StudentID = studentDict[studentName];

                            var employeeName = workSheet.Cells[rowIterator, 3].Value.ToString().Trim();
                            if (!employeeDict.ContainsKey(employeeName))
                            {
                                ViewBag.EmployeeError = "Cán bộ công ty không tồn tại";
                                continue;
                            }
                            internship.EmployeeID = employeeDict[employeeName];

                            var teacherName = workSheet.Cells[rowIterator, 4].Value.ToString().Trim();
                            if (!teacherDict.ContainsKey(teacherName))
                            {
                                ViewBag.StudentError = "Giảng viên không tồn tại";
                                continue;
                            }
                            internship.TeacherID = teacherDict[teacherName];

                            string startday = workSheet.Cells[rowIterator, 5].Value.ToString();
                            DateTime start_day;
                            if (DateTime.TryParseExact(startday, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out start_day))
                            {
                                internship.Start_Day = start_day;
                            }
                            else
                            {
                                internship.Start_Day = DateTime.MinValue;
                            }

                            string endday = workSheet.Cells[rowIterator, 6].Value.ToString();
                            DateTime end_day;
                            if (DateTime.TryParseExact(endday, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out end_day))
                            {
                                internship.End_Day = end_day;
                            }
                            else
                            {
                                internship.End_Day = DateTime.MinValue;
                            }

                            internshipsList.Add(internship);
                        }
                    }

                    foreach (var item in internshipsList)
                    {
                        db.Internships.Add(item);
                    }

                    int result = db.SaveChanges();
                    if (result > 0)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View("Index");
        }

        [HttpPost]
        public FileResult Export()
        {
            DataTable dt = GetInternshipsDataTable();

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add(dt, "Internships");

                ws.Range("A1:F1").Merge();
                ws.Cell(1, 1).Value = "Danh sách Thực tập";
                ws.Cell(1, 1).Style.Font.FontSize = 16;
                ws.Cell(1, 1).Style.Font.Bold = true;
                ws.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws.Cell(2, 1).Value = "STT";
                ws.Cell(2, 2).Value = "Tên Sinh viên";
                ws.Cell(2, 3).Value = "Tên Cán bộ công ty";
                ws.Cell(2, 4).Value = "Tên Giáo viên";
                ws.Cell(2, 5).Style.NumberFormat.Format = "dd/MM/yyyy";
                ws.Cell(2, 5).Value = "Ngày bắt đầu";
                ws.Cell(2, 6).Style.NumberFormat.Format = "dd/MM/yyyy";
                ws.Cell(2, 6).Value = "Ngày kết thúc";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        ws.Cell(i + 3, j + 1).Value = dt.Rows[i][j].ToString();
                    }
                }

                var headerRange = ws.Range("A2:F2");
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws.Columns().AdjustToContents();

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DanhSachThucTap.xlsx");
                }
            }
        }
        public DataTable GetInternshipsDataTable()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[6]
            {
            new DataColumn("InternShipID"),
            new DataColumn("StudentName"),
            new DataColumn("EmployeeName"),
            new DataColumn("TeacherName"),
            new DataColumn("Start_Day"),
            new DataColumn("End_Day")
            });

            var internship = db.Internships.ToList();

            foreach (var t in internship)
            {
                string namesv = t.Student.LastName + " " + t.Student.FirstName;
                string namegv = t.Teacher.LastName + " " + t.Teacher.FirstName;
                dt.Rows.Add(t.InternShipID, namesv, t.Employee.Name, namegv, t.Start_Day, t.End_Day);
            }

            return dt;
        }
        public ActionResult Download()
        {
            string filePath = Server.MapPath("~/Content/assets/file/ThucTap.xlsx");

            if (System.IO.File.Exists(filePath))
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=ThucTap.xlsx");

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

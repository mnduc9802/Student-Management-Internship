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
using DocumentFormat.OpenXml.Wordprocessing;
using OfficeOpenXml;
using PagedList;
using QuanLySinhVienThucTap.Models;

namespace QuanLySinhVienThucTap.Areas.Admin.Controllers
{
    public class TopicsController : Controller
    {
        private QLSVTTEntities db = new QLSVTTEntities();

        // GET: Admin/Topics
        public ActionResult Index(string title,string masv, string macb, int? page, string ErrorMessage)
        {
            ViewBag.DeMuc = "Quản lý đề tài";
            ViewBag.ActivePage = "Topics";
            ViewBag.TieuDe = "Đề tài";
            var Topic = from s in db.Topics select s;

            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ViewBag.ErrorMessage = ErrorMessage;
            }

            if (!String.IsNullOrEmpty(title))
            {
                Topic = Topic.Where(s => s.Title.Contains(title));
            }

            if (!String.IsNullOrEmpty(masv))
            {
                Topic = Topic.Where(s => s.Student.StudentCode.Contains(masv));
            }

            if (!String.IsNullOrEmpty(macb))
            {
                if (macb.Length == 8 && macb.StartsWith("010263"))
                {
                    int emID = Convert.ToInt32(macb.Substring(6));
                    Topic = Topic.Where(s => s.EmployeeID == emID);
                }
            }

            if (!Topic.Any())
            {
                ViewBag.ErrorMessage = "Không tìm thấy kết quả phù hợp.";
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(Topic.OrderBy(h => h.TopicID).ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Topics/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.DeMuc = "Quản lý đề tài";
            ViewBag.ActivePage = "Topics";
            ViewBag.TieuDe = "Đề tài";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // GET: Admin/Topics/Create
        public ActionResult Create()
        {
            ViewBag.DeMuc = "Quản lý đề tài";
            ViewBag.ActivePage = "Topics";
            ViewBag.TieuDe = "Đề tài";
            var emList = db.Employees.Select(item => new {
                EmployeeID = item.EmployeeID,
                macb = item.EmployeeID < 10 ? "0102630" + item.EmployeeID : "010263" + item.EmployeeID
            }).ToList();
            ViewBag.EmployeeID = new SelectList(emList, "EmployeeID", "macb");
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentCode");
            return View();
        }

        // POST: Admin/Topics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TopicID,Title,Description,StudentID,EmployeeID")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                if (db.Topics.Any(s => s.StudentID == topic.StudentID && s.TopicID != topic.TopicID))
                {
                    ModelState.AddModelError("StudentID", "Mã sinh viên đã tồn tại.");
                    ViewBag.DeMuc = "Quản lý đề tài";
                    ViewBag.ActivePage = "Topics";
                    ViewBag.TieuDe = "Đề tài";
                }
                else
                {
                    db.Topics.Add(topic);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            var emList = db.Employees.Select(item => new {
                EmployeeID = item.EmployeeID,
                macb = item.EmployeeID < 10 ? "0102630" + item.EmployeeID : "010263" + item.EmployeeID
            }).ToList();
            ViewBag.EmployeeID = new SelectList(emList, "EmployeeID", "macb", topic.EmployeeID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentCode", topic.StudentID);
            return View(topic);
        }

        // GET: Admin/Topics/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.DeMuc = "Quản lý đề tài";
            ViewBag.ActivePage = "Topics";
            ViewBag.TieuDe = "Đề tài";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            var emList = db.Employees.Select(item => new {
                EmployeeID = item.EmployeeID,
                macb = item.EmployeeID < 10 ? "0102630" + item.EmployeeID : "010263" + item.EmployeeID
            }).ToList();
            ViewBag.EmployeeID = new SelectList(emList, "EmployeeID", "macb", topic.EmployeeID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentCode", topic.StudentID);
            return View(topic);
        }

        // POST: Admin/Topics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TopicID,Title,Description,StudentID,EmployeeID")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(topic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var emList = db.Employees.Select(item => new {
                EmployeeID = item.EmployeeID,
                macb = item.EmployeeID < 10 ? "0102630" + item.EmployeeID : "010263" + item.EmployeeID
            }).ToList();
            ViewBag.EmployeeID = new SelectList(emList, "EmployeeID", "macb", topic.EmployeeID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentCode", topic.StudentID);
            return View(topic);
        }

        // POST: Admin/Topics/Delete/5
        [HttpPost]
        public JsonResult Delete(int ID)
        {
            int rowCount = db.Topics.Count();
            Topic topic = db.Topics.FirstOrDefault(x => x.TopicID == ID);
            if (topic != null)
            {
                db.Topics.Remove(topic);
                db.SaveChanges();
            }
            db.Database.ExecuteSqlCommand($"DBCC CHECKIDENT('dbo.Topic', RESEED, {rowCount - 1})");
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
            ViewBag.ControllerName = "Topics";
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    var topicList = new List<Topic>();
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;
                        var studentDict = db.Students.ToDictionary(f => $"{f.LastName} {f.FirstName}", x => x.StudentID);
                        var employeeDict = db.Employees.ToDictionary(f => f.Name, x => x.EmployeeID);
                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                        {
                            var topic = new Topic();
                            topic.TopicID = Convert.ToInt32(workSheet.Cells[rowIterator, 1].Value);
                            topic.Title = workSheet.Cells[rowIterator, 2].Value.ToString().Trim();
                            topic.Description = workSheet.Cells[rowIterator, 3].Value.ToString().Trim();
                            var studentName = workSheet.Cells[rowIterator, 4].Value.ToString().Trim();
                            var employeeName = workSheet.Cells[rowIterator, 5].Value.ToString().Trim();

                            if (studentDict.ContainsKey(studentName)==false) 
                            {
                                ViewBag.ErrorMessage = "Sinh viên không tồn tại";
                                continue;
                            }
                            topic.StudentID = studentDict[studentName];

                            if (employeeDict.ContainsKey(employeeName) == false)
                            {
                                ViewBag.ErrorMessage = "Cán bộ công ty không tồn tại";
                                continue;
                            }
                            topic.EmployeeID = employeeDict[employeeName];

                            topicList.Add(topic);
                        }
                    }

                    foreach (var item in topicList)
                    {
                        if (!db.Topics.Any(s => s.StudentID == item.StudentID))
                        {
                            db.Topics.Add(item);
                        }
                    }

                    int result = db.SaveChanges();
                    if (result > 0)
                    {
                        return RedirectToAction("Index", new { ErrorMessage = ViewBag.ErrorMessage });
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Sinh viên làm đề tài đã tồn tại!!!";
                        return RedirectToAction("Index", new { ErrorMessage = ViewBag.ErrorMessage });
                    }
                }
            }
            return View("Index");
        }

        [HttpPost]
        public FileResult Export()
        {
            DataTable dt = GetTopicDataTable();

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add(dt, "Topics");

                ws.Range("A1:G1").Merge();
                ws.Cell(1, 1).Value = "Danh sách Đề Tài";
                ws.Cell(1, 1).Style.Font.FontSize = 16;
                ws.Cell(1, 1).Style.Font.Bold = true;
                ws.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws.Cell(2, 1).Value = "STT";
                ws.Cell(2, 2).Value = "Mã sinh viên";
                ws.Cell(2, 3).Value = "Tên sinh viên";
                ws.Cell(2, 4).Value = "Mã cán bộ nhân viên";
                ws.Cell(2, 5).Value = "Tên cán bộ nhân viên";
                ws.Cell(2, 6).Value = "Tên đề tài";
                ws.Cell(2, 7).Value = "Mô tả";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        ws.Cell(i + 3, j + 1).Value = dt.Rows[i][j].ToString();
                    }
                }

                var headerRange = ws.Range("A2:G2");
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws.Columns().AdjustToContents();

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DanhSachDeTai.xlsx");
                }
            }
        }
        public DataTable GetTopicDataTable()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[7]
            {
            new DataColumn("TopicID"),
            new DataColumn("StudentCode"),
            new DataColumn("StudentName"),
            new DataColumn("EmployeeCode"),
            new DataColumn("EmployeeName"),
            new DataColumn("Title"),
            new DataColumn("Description"),
            });

            var topics = db.Topics.ToList();

            foreach (var t in topics)
            {
                string magv;
                if (t.EmployeeID < 10)
                {
                    magv = "0102630" + t.EmployeeID;
                }
                else
                {
                    magv = "010263" + t.EmployeeID;
                }
                string namesv = t.Student.LastName + " " + t.Student.FirstName;
                dt.Rows.Add(t.TopicID, t.Student.StudentCode, namesv, magv, t.Employee.Name, t.Title, t.Description);
            }

            return dt;
        }
        public ActionResult Download()
        {
            string filePath = Server.MapPath("~/Content/assets/file/DeTai.xlsx");

            if (System.IO.File.Exists(filePath))
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=DeTai.xlsx");

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

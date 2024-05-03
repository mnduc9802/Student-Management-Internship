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
using OfficeOpenXml;
using PagedList;
using QuanLySinhVienThucTap.Models;

namespace QuanLySinhVienThucTap.Areas.Admin.Controllers
{
    public class StudentsController : Controller
    {
        private QLSVTTEntities db = new QLSVTTEntities();

        // GET: Admin/Students
        public ActionResult Index(string studentcode, string lastname, string firstname, string classroom, int? page, string ErrorMessage)
        {
            ViewBag.DeMuc = "Quản lý sinh viên thực tập";
            ViewBag.ActivePage = "Students";
            ViewBag.TieuDe = "Sinh viên";
            var Student = from s in db.Students select s;

            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ViewBag.ErrorMessage = ErrorMessage;
            }

            if (!String.IsNullOrEmpty(studentcode))
            {
                Student = Student.Where(s => s.StudentCode.Contains(studentcode));
            }

            if (!String.IsNullOrEmpty(lastname))
            {
                Student = Student.Where(s => s.LastName.Contains(lastname));
            }

            if (!String.IsNullOrEmpty(firstname))
            {
                Student = Student.Where(s => s.FirstName.Contains(firstname));
            }

            if (!String.IsNullOrEmpty(classroom))
            {
                Student = Student.Where(s => s.Classroom.Contains(classroom));
            }

            if (!Student.Any())
            {
                ViewBag.ErrorMessage = "Không tìm thấy kết quả phù hợp.";
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(Student.OrderBy(h => h.StudentID).ToPagedList(pageNumber, pageSize));
            //return View(db.Students.ToList());
        }

        // GET: Admin/Students/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.DeMuc = "Quản lý sinh viên thực tập";
            ViewBag.ActivePage = "Students";
            ViewBag.TieuDe = "Sinh viên";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Admin/Students/Create
        public ActionResult Create()
        {
            ViewBag.DeMuc = "Quản lý sinh viên thực tập";
            ViewBag.ActivePage = "Students";
            ViewBag.TieuDe = "Sinh viên";
            return View(new Student());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentID,StudentCode,LastName,FirstName,Gender,DateOfBirth,Email,PhoneNumber,Classroom,GPAScore,LetterScore,Address,Status")] Student student)
        {
            if (db.Students.Any(s => s.StudentCode == student.StudentCode))
            {
                ModelState.AddModelError("StudentCode", "Mã sinh viên đã tồn tại.");
                ViewBag.DeMuc = "Quản lý sinh viên thực tập";
                ViewBag.ActivePage = "Students";
                ViewBag.TieuDe = "Sinh viên";
            }

            if (student.DateOfBirth > DateTime.Today)
            {
                ModelState.AddModelError("DateOfBirth", "Ngày sinh không chính xác.");
                ViewBag.DeMuc = "Quản lý sinh viên thực tập";
                ViewBag.ActivePage = "Students";
                ViewBag.TieuDe = "Sinh viên";
            }

            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeMuc = "Quản lý sinh viên thực tập";
            ViewBag.ActivePage = "Students";
            ViewBag.TieuDe = "Sinh viên";

            return View(student);
        }

        // GET: Admin/Students/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.DeMuc = "Quản lý sinh viên thực tập";
            ViewBag.ActivePage = "Students";
            ViewBag.TieuDe = "Sinh viên";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Admin/Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,StudentCode,LastName,FirstName,Gender,DateOfBirth,Email,PhoneNumber,Classroom,GPAScore,LetterScore,Address,Status")] Student student)
        {
            if (student.DateOfBirth > DateTime.Today)
            {
                ModelState.AddModelError("DateOfBirth", "Ngày sinh không chính xác.");
                ViewBag.DeMuc = "Quản lý sinh viên thực tập";
                ViewBag.ActivePage = "Students";
                ViewBag.TieuDe = "Sinh viên";
            }

            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Admin/Students/Delete/5
        [HttpPost]
        public ActionResult Delete(int ID)
        {
            bool hasRelatedData = db.Topics.Any(t => t.StudentID == ID);

            if (hasRelatedData)
            {
                return RedirectToAction("Index", new { ErrorMessage = "Không thể xóa sinh viên vì tồn tại dữ liệu liên quan." });
            }
            else
            {
                Student student = db.Students.FirstOrDefault(x => x.StudentID == ID);

                if (student != null)
                {
                    db.Students.Remove(student);
                    db.SaveChanges();
                }

                int rowCount = db.Students.Count();
                db.Database.ExecuteSqlCommand($"DBCC CHECKIDENT('dbo.Student', RESEED, {rowCount - 1})");

                return RedirectToAction("Index", new { ErrorMessage = "" });
            }
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
            ViewBag.ControllerName = "Students";
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    var studentList = new List<Student>();
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;
                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                        {
                            var student = new Student();
                            student.StudentID = Convert.ToInt32(workSheet.Cells[rowIterator, 1].Value);
                            student.StudentCode = workSheet.Cells[rowIterator, 2].Value.ToString().Trim();
                            student.LastName = workSheet.Cells[rowIterator, 3].Value.ToString().Trim();
                            student.FirstName = workSheet.Cells[rowIterator, 4].Value.ToString().Trim();
                            student.Gender = workSheet.Cells[rowIterator, 5].Value.ToString().Trim();
                            string dateString = workSheet.Cells[rowIterator, 6].Value.ToString().Trim();
                            DateTime dateOfBirth;
                            if (DateTime.TryParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOfBirth))
                            {
                                student.DateOfBirth = dateOfBirth;
                            }
                            else
                            {
                                student.DateOfBirth = DateTime.MinValue;
                            }
                            student.Email = workSheet.Cells[rowIterator, 7].Value.ToString().Trim();
                            student.PhoneNumber = workSheet.Cells[rowIterator, 8].Value.ToString().Trim();
                            student.Classroom = workSheet.Cells[rowIterator, 9].Value.ToString().Trim();
                            student.GPAScore = Convert.ToDecimal(workSheet.Cells[rowIterator, 10].Value);
                            student.LetterScore = workSheet.Cells[rowIterator, 11].Value.ToString().Trim();
                            student.Address = workSheet.Cells[rowIterator, 12].Value.ToString().Trim(); ;
                            string statusString = workSheet.Cells[rowIterator, 13].Value.ToString().Trim();
                            bool isCheck;
                            if(statusString.ToLower() == "làm tại doanh nghiệp")
                            {
                                isCheck = true;
                            }
                            else if (statusString.ToLower() == "làm ngoài")
                            {
                                isCheck = false;
                            }
                            else
                            {
                                isCheck = false; 
                            }
                            student.Status = isCheck;
                            studentList.Add(student);
                        }
                    }

                    foreach (var item in studentList)
                    {
                        if (!db.Students.Any(s => s.StudentCode == item.StudentCode))
                        {
                            db.Students.Add(item);
                        }
                    }

                    int result = db.SaveChanges();
                    if (result > 0)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Sinh viên đã tồn tại!!!";
                        return RedirectToAction("Index", new { ErrorMessage = ViewBag.ErrorMessage });
                    }
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public FileResult Export()
        {
            DataTable dt = GetStudentDataTable();

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add(dt, "Students");

                ws.Range("A1:M1").Merge();
                ws.Cell(1, 1).Value = "Danh sách sinh viên";
                ws.Cell(1, 1).Style.Font.FontSize = 16;
                ws.Cell(1, 1).Style.Font.Bold = true;
                ws.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws.Cell(2, 1).Value = "STT";
                ws.Cell(2, 2).Value = "Mã sinh viên";
                ws.Cell(2, 3).Value = "Họ đệm";
                ws.Cell(2, 4).Value = "Tên";
                ws.Cell(2, 5).Value = "Giới tính";
                ws.Cell(2, 6).Value = "Ngày sinh";
                ws.Cell(2, 6).Style.NumberFormat.Format = "dd/MM/yyyy";
                ws.Cell(2, 7).Value = "Email";
                ws.Cell(2, 8).Value = "Số điện thoại";
                ws.Cell(2, 9).Value = "Lớp";
                ws.Cell(2, 10).Value = "Điểm GPA";
                ws.Cell(2, 11).Value = "Điểm chữ";
                ws.Cell(2, 12).Value = "Địa chỉ";
                ws.Cell(2, 13).Value = "Trạng thái";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        ws.Cell(i + 3, j + 1).Value = dt.Rows[i][j].ToString();
                    }
                }

                var headerRange = ws.Range("A2:M2");
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws.Columns().AdjustToContents();

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DanhSachSinhVienThucTap.xlsx");
                }
            }
        }
        public DataTable GetStudentDataTable()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[13]
            {
            new DataColumn("StudentID"),
            new DataColumn("StudentCode"),
            new DataColumn("LastName"),
            new DataColumn("FirstName"),
            new DataColumn("Gender"),
            new DataColumn("DateOfBirth"),
            new DataColumn("Email"),
            new DataColumn("PhoneNumber"),
            new DataColumn("Classroom"),
            new DataColumn("GPAScore"),
            new DataColumn("LetterScore"),
            new DataColumn("Address"),
            new DataColumn("Status")
            });

            var students = db.Students.ToList();

            foreach (var sv in students)
            {
                string status = "";
                if(sv.Status == true)
                {
                    status = "Làm tại doanh nghiệp";
                }
                else
                {
                    status = "Làm ngoài";
                }
                dt.Rows.Add(sv.StudentID, sv.StudentCode, sv.LastName, sv.FirstName, sv.Gender, sv.DateOfBirth, sv.Email,
                    sv.PhoneNumber, sv.Classroom, sv.GPAScore, sv.LetterScore, sv.Address, status);
            }

            return dt;
        }
        public ActionResult Download()
        {
            string filePath = Server.MapPath("~/Content/assets/file/Sinhvien.xlsx");

            if (System.IO.File.Exists(filePath))
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=Sinhvien.xlsx");

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

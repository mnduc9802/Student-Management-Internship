using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
    public class EmployeesController : Controller
    {
        private QLSVTTEntities db = new QLSVTTEntities();

        // GET: Admin/Employees
        public ActionResult Index(string name, string companyname, string companyaddress, string macb, int? page, string ErrorMessage)
        {
            ViewBag.DeMuc = "Quản lý cán bộ công ty";
            ViewBag.ActivePage = "Employees";
            ViewBag.TieuDe = "Cán bộ hướng dẫn";
            var Employee = from s in db.Employees select s;

            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ViewBag.ErrorMessage = ErrorMessage;
            }

            if (!String.IsNullOrEmpty(name))
            {
                Employee = Employee.Where(s => s.Name.Contains(name));
            }

            if (!String.IsNullOrEmpty(companyname))
            {
                Employee = Employee.Where(s => s.CompanyName.Contains(companyname));
            }

            if (!String.IsNullOrEmpty(companyaddress))
            {
                Employee = Employee.Where(s => s.CompanyAddress.Contains(companyaddress));
            }

            if (!String.IsNullOrEmpty(macb))
            {
                if (macb.Length == 8 && macb.StartsWith("010263"))
                {
                    int emID = Convert.ToInt32(macb.Substring(6));
                    Employee = Employee.Where(s => s.EmployeeID == emID);
                }
            }

            if (!Employee.Any())
            {
                ViewBag.ErrorMessage = "Không tìm thấy kết quả phù hợp.";
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(Employee.OrderBy(h => h.EmployeeID).ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Employees/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.DeMuc = "Quản lý cán bộ công ty";
            ViewBag.ActivePage = "Employees";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Admin/Employees/Create
        public ActionResult Create()
        {
            ViewBag.DeMuc = "Quản lý cán bộ công ty";
            ViewBag.ActivePage = "Employees";
            return View();
        }

        // POST: Admin/Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeID,Name,Email,PhoneNumber,Address,CompanyName,CompanyAddress,Note")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Admin/Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.DeMuc = "Quản lý cán bộ công ty";
            ViewBag.ActivePage = "Employees";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Admin/Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeID,Name,Email,PhoneNumber,Address,CompanyName,CompanyAddress,Note")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // POST: Admin/Employees/Delete/5
        [HttpPost]
        public JsonResult Delete(int ID)
        {
            int rowCount = db.Employees.Count();
            Employee employee = db.Employees.FirstOrDefault(x => x.EmployeeID == ID);
            if (employee != null)
            {
                db.Employees.Remove(employee);
                db.SaveChanges();
            }
            db.Database.ExecuteSqlCommand($"DBCC CHECKIDENT('dbo.Employee', RESEED, {rowCount - 1})");
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
            ViewBag.ControllerName = "Employees";
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    var employeeList = new List<Employee>();
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;
                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                        {
                            var employee = new Employee();
                            employee.EmployeeID = Convert.ToInt32(workSheet.Cells[rowIterator, 1].Value);
                            employee.Name = workSheet.Cells[rowIterator, 2].Value.ToString().Trim();
                            employee.Email = workSheet.Cells[rowIterator, 3].Value.ToString().Trim();
                            employee.PhoneNumber = workSheet.Cells[rowIterator, 4].Value.ToString().Trim();
                            employee.Address = workSheet.Cells[rowIterator, 5].Value.ToString().Trim();
                            employee.CompanyName = workSheet.Cells[rowIterator, 6].Value.ToString().Trim();
                            employee.CompanyAddress = workSheet.Cells[rowIterator, 7].Value.ToString().Trim();
                            employee.Note = workSheet.Cells[rowIterator, 8].Value.ToString().Trim();
                            employeeList.Add(employee);
                        }
                    }

                    foreach (var item in employeeList)
                    {
                        if (!db.Employees.Any(s => s.Email == item.Email))
                        {
                            db.Employees.Add(item);
                        }
                    }

                    int result = db.SaveChanges();
                    if (result > 0)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Cán bộ đã tồn tại!!!";
                        return RedirectToAction("Index", new { ErrorMessage = ViewBag.ErrorMessage });
                    }
                }
            }
            return View("Index");
        }
        [HttpPost]
        public FileResult Export()
        {
            DataTable dt = GetEmployeeDataTable();

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add(dt, "Employees");

                ws.Range("A1:I1").Merge();
                ws.Cell(1, 1).Value = "Danh sách cán bộ công ty";
                ws.Cell(1, 1).Style.Font.FontSize = 16;
                ws.Cell(1, 1).Style.Font.Bold = true;
                ws.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws.Cell(2, 1).Value = "STT";
                ws.Cell(2, 2).Value = "Mã cán bộ công ty";
                ws.Cell(2, 3).Value = "Tên cán bộ công ty";
                ws.Cell(2, 4).Value = "Email";
                ws.Cell(2, 5).Value = "Số điện thoại";
                ws.Cell(2, 6).Value = "Địa chỉ";
                ws.Cell(2, 7).Value = "Tên công ty";
                ws.Cell(2, 8).Value = "Địa chỉ công ty";
                ws.Cell(2, 9).Value = "Ghi chú";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        ws.Cell(i + 3, j + 1).Value = dt.Rows[i][j].ToString();
                    }
                }

                var headerRange = ws.Range("A2:I2");
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws.Columns().AdjustToContents();

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DanhSachCanBoCongTy.xlsx");
                }
            }
        }
        public DataTable GetEmployeeDataTable()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[9]
            {
            new DataColumn("EmployeeID"),
            new DataColumn("EmployeeCode"),
            new DataColumn("Name"),
            new DataColumn("Email"),
            new DataColumn("PhoneNumber"),
            new DataColumn("Address"),
            new DataColumn("CompanyName"),
            new DataColumn("CompanyAddress"),
            new DataColumn("Note"),
            });

            var employee = db.Employees.ToList();

            foreach (var sv in employee)
            {
                string magv;
                if (sv.EmployeeID < 10)
                {
                    magv = "0102630" + sv.EmployeeID;
                }
                else
                {
                    magv = "010263" + sv.EmployeeID;
                }
                dt.Rows.Add(sv.EmployeeID, magv , sv.Name, sv.Email, sv.PhoneNumber, sv.Address, sv.CompanyName, sv.CompanyAddress,
                    sv.Note);
            }

            return dt;
        }
        public ActionResult Download()
        {
            string filePath = Server.MapPath("~/Content/assets/file/Canbo.xlsx");

            if (System.IO.File.Exists(filePath))
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=Canbo.xlsx");

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

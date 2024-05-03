using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DocumentFormat.OpenXml.Spreadsheet;
using QuanLySinhVienThucTap.Models;

namespace QuanLySinhVienThucTap.Areas.Admin.Controllers
{
    public class AccountsController : Controller
    {
        private QLSVTTEntities db = new QLSVTTEntities();

        // GET: Admin/Accounts
        public ActionResult Index()
        {
            if (ViewBag.Message != null)
            {
                // Nếu có, truyền thông báo này vào ViewBag để hiển thị lên trang
                ViewBag.Message = "Gửi tài khoản thành công";
            }
            ViewBag.DeMuc = "Quản lý tài khoản";
            ViewBag.ActivePage = "Accounts";
            ViewBag.TieuDe = "Tài khoản";
            var accounts = db.Accounts.Include(a => a.Role);
            return View(accounts.ToList());
        }

        // GET: Admin/Accounts/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.DeMuc = "Quản lý tài khoản";
            ViewBag.ActivePage = "Accounts";
            ViewBag.TieuDe = "Tài khoản";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }
        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        // GET: Admin/Accounts/Create
        public ActionResult Create()
        {
            ViewBag.DeMuc = "Quản lý tài khoản";
            ViewBag.ActivePage = "Accounts";
            ViewBag.TieuDe = "Tài khoản";
            ViewBag.RoleID = new SelectList(db.Roles, "IDRole", "Role1");
            ViewBag.StudentCode = new SelectList(db.Students, "StudentCode", "Role1");
            return View();
        }

        // POST: Admin/Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "AccountID,Username,Password,RoleID,Status")] Account account)
        {
            if (ModelState.IsValid)
            {
                var existingUser = db.Accounts.FirstOrDefault(u => u.Username == account.Username);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Username", "Tài khoản đã tồn tại. Vui lòng chọn tên đăng nhập khác.");
                    return View(account);
                }
                account.Password = HashPassword(account.Password);
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleID = new SelectList(db.Roles, "IDRole", "Role1", account.RoleID);
            ViewBag.StudentCode = new SelectList(db.Students, "StudentCode", "Role1", account.AccountID);
            return View(account);
        }

        // GET: Admin/Accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.DeMuc = "Quản lý tài khoản";
            ViewBag.ActivePage = "Accounts";
            ViewBag.TieuDe = "Tài khoản";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleID = new SelectList(db.Roles, "IDRole", "Role1", account.RoleID);
            ViewBag.StudentCode = new SelectList(db.Students, "StudentCode", "Role1", account.AccountID);
            return View(account);
        }

        // POST: Admin/Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountID,Username,Password,RoleID,Status")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleID = new SelectList(db.Roles, "IDRole", "Role1", account.RoleID);
            ViewBag.StudentCode = new SelectList(db.Students, "StudentCode", "Role1", account.AccountID);
            return View(account);
        }

        // POST: Admin/Accounts/Delete/5
        [HttpPost]
        public JsonResult Delete(int ID)
        {
            int rowCount = db.Accounts.Count();
            Account account = db.Accounts.FirstOrDefault(x => x.AccountID == ID);
            if (account != null)
            {
                db.Accounts.Remove(account);
                db.SaveChanges();
            }
            db.Database.ExecuteSqlCommand($"DBCC CHECKIDENT('dbo.Account', RESEED, {rowCount - 1})");
            return Json("");
        }

        [HttpPost]
        public JsonResult Send(Account account)
        {
            string subject = "Thông tin tài khoản";
            string body = $"Tên đăng nhập: {account.Username}\nMật khẩu: {account.Password}";

                using (MailMessage mailMessage = new MailMessage("leduclol711@gmail.com", "leduclol711@gmail.com"))
                {
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = false;

                    using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 465))
                    {
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new NetworkCredential("leduclol711@gmail.com", "cgmogfdgldjzvgxs");

                        smtpClient.Send(mailMessage);
                    }
                }

                ViewBag.Message = "Gửi tài khoản thành công";

            return Json(" ");
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}

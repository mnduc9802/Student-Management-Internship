using QuanLySinhVienThucTap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace QuanLySinhVienThucTap.Controllers
{
    public class LoginController : Controller
    {
        private QLSVTTEntities db = new QLSVTTEntities();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Index");
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login model)
        {
            if (ModelState.IsValid) // Kiểm tra xem ModelState có hợp lệ không
            {
                if (!string.IsNullOrEmpty(model.Password)) // Kiểm tra trống mật khẩu
                {
                    string pass = HashPassword(model.Password);
                    var user = db.Accounts.FirstOrDefault(u => u.Username == model.UserName && u.Password == pass);
                    if (user != null)
                    {
                        var roleId = user.RoleID;
                        var role = db.Roles.FirstOrDefault(r => r.IDRole == roleId);
                        if (user.Status == true)
                        {
                            if (user.RoleID == 1)
                            {
                                //Admin
                                Session["Tendangnhap"] = user.Username;
                                Session["LoaiTaikhoan"] = role.Role1;
                                Session["Show"] = true;
                                Session["IsTeacher"] = true;
                                Session["IsEmployee"] = true;
                                return RedirectToAction("Index", "Home", new { area = "Admin" });
                            }
                            else if (user.RoleID == 2)
                            {
                                //Giảng viên
                                var giangvien = db.Teachers.FirstOrDefault(u => u.Email == user.Username);
                                Session["Tendangnhap"] = CapitalizeFirstLetter(giangvien.LastName + " " + giangvien.FirstName);
                                Session["LoaiTaikhoan"] = role.Role1;
                                Session["Show"] = false;
                                Session["IsTeacher"] = true;
                                Session["IsEmployee"] = false;
                                return RedirectToAction("Index", "Home", new { area = "Admin" });
                            }
                            else if (user.RoleID == 3)
                            {
                                //Cán bộ
                                var canbo = db.Employees.FirstOrDefault(u => u.Email == user.Username);
                                Session["Tendangnhap"] = CapitalizeFirstLetter(canbo.Name);
                                Session["LoaiTaikhoan"] = role.Role1;
                                Session["Show"] = false;
                                Session["IsTeacher"] = false;
                                Session["IsEmployee"] = true;
                                return RedirectToAction("Index", "Home", new { area = "Admin" });
                            }
                            else if (user.RoleID == 4)
                            {
                                var sinhvien = db.Students.FirstOrDefault(u => u.StudentCode == user.Username);
                                var topic = db.Topics.FirstOrDefault(u => u.Student.StudentCode == user.Username);
                                var score = db.Scores.FirstOrDefault(u => u.TopicID == topic.TopicID);
                                DateTime dateOfbirth = sinhvien.DateOfBirth.GetValueOrDefault(); ;
                                string formattedDate = dateOfbirth.ToString("dd/MM/yyyy");
                                //Sinh viên
                                Session["Tendangnhap"] = CapitalizeFirstLetter(sinhvien.LastName + " " + sinhvien.FirstName);
                                Session["LoaiTaikhoan"] = role.Role1;
                                Session["Lop"] = sinhvien.Classroom;
                                Session["Hodem"] = sinhvien.LastName;
                                Session["Ten"] = sinhvien.FirstName;
                                Session["Masinhvien"] = sinhvien.StudentCode;
                                Session["Diachi"] = sinhvien.Address;
                                Session["Ngaysinh"] = formattedDate;
                                Session["Email"] = sinhvien.Email;
                                Session["Sodienthoai"] = sinhvien.PhoneNumber;
                                Session["GPA"] = sinhvien.GPAScore;
                                Session["Chu"] = sinhvien.LetterScore;
                                Session["Tendetai"] = topic.Title;
                                Session["CanboHD"] = topic.Employee.Name;
                                Session["Congty"] = topic.Employee.CompanyName;
                                Session["Diem1"] = score.Score1;
                                Session["Diem2"] = score.Score2;
                                Session["Diem3"] = score.Score3;
                                Session["Diem4"] = score.Score4;
                                Session["Diem5"] = score.Score5;
                                Session["DiemTB"] = (score.Score1 + score.Score2 + score.Score3 + score.Score4 + score.Score5) / 5;
                                Session["Danhgia"] = score.Assessment;
                                return RedirectToAction("Index", "Home", new { area = "Sinhvien" });
                            }
                        }
                        else
                        {

                            ModelState.AddModelError("", "Tài khoản này hiện tại không hoạt động.");
                            return View(model);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không chính xác.");
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Vui lòng nhập mật khẩu.");
                    return View(model);
                }
            }

            return View(model);
        }
        public string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            string[] words = input.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(words[i]))
                {
                    words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
                }
            }

            return string.Join(" ", words);
        }
    }
}
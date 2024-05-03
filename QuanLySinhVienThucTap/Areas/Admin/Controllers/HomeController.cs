using QuanLySinhVienThucTap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLySinhVienThucTap.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private QLSVTTEntities db = new QLSVTTEntities();
        // GET: Admin/Home
        public ActionResult Index()
        {
            int[] conditionCounts = new int[4];
            int[] avgCounts = new int[4];
            int[] statusCounts = new int[2];
            var students = db.Students.ToList();
            var scores = db.Scores.ToList();
            foreach (var student in students)
            {
                if (student.GPAScore >= 3.8m)
                    conditionCounts[0]++;
                else if (student.GPAScore >= 3 && student.GPAScore < 3.8m)
                    conditionCounts[1]++;
                else if (student.GPAScore >= 2 && student.GPAScore < 3)
                    conditionCounts[2]++;
                else
                    conditionCounts[3]++;
            }
            foreach(var student in students)
            {
                if(student.Status == true)
                {
                    statusCounts[0]++;
                }
                else
                {
                    statusCounts[1]++;
                }
            }
            foreach (var score in scores)
            {
                double score1 = Convert.ToDouble(score.Score1.GetValueOrDefault());
                double score2 = Convert.ToDouble(score.Score2.GetValueOrDefault());
                double score3 = Convert.ToDouble(score.Score3.GetValueOrDefault());
                double score4 = Convert.ToDouble(score.Score4.GetValueOrDefault());
                double score5 = Convert.ToDouble(score.Score5.GetValueOrDefault());

                // Tính trung bình
                double scoretb = (score1 + score2 + score3 + score4 + score5) / 5;
                if (scoretb >= 8.5)
                    avgCounts[0]++;
                else if (scoretb >= 7)
                    avgCounts[1]++;
                else if (scoretb >= 5.5)
                    avgCounts[2]++;
                else
                    avgCounts[3]++;
            }
            ViewBag.ConditionCounts = conditionCounts;
            ViewBag.AvgCounts = avgCounts;
            ViewBag.StatusCounts = statusCounts;
            int stuCount = db.Students.Count();
            int teaCount = db.Teachers.Count();
            int empCount = db.Employees.Count();
            int topCount = db.Topics.Count();
            ViewBag.StudentCount = stuCount;
            ViewBag.TeacherCount = teaCount;
            ViewBag.EmployeeCount = empCount;
            ViewBag.TopicCount = topCount;
            ViewBag.TieuDe = "Trang chủ";
            ViewBag.DeMuc = "Trang chủ";
            ViewBag.ActivePage = "Home";
            return View();
        }
        
    }
}
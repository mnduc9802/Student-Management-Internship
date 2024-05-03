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
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office.CustomXsn;
using DocumentFormat.OpenXml.Spreadsheet;
using OfficeOpenXml;
using PagedList;
using QuanLySinhVienThucTap.Models;

namespace QuanLySinhVienThucTap.Areas.Admin.Controllers
{
    public class ScoresController : Controller
    {
        private QLSVTTEntities db = new QLSVTTEntities();

        // GET: Admin/Scores
        public ActionResult Index(string assessment, string nametopic,string masv, int? page, string ErrorMessage)
        {
            ViewBag.DeMuc = "Quản lý điểm";
            ViewBag.ActivePage = "Scores";
            ViewBag.TieuDe = "Điểm";
            var Score = from s in db.Scores select s;
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ViewBag.ErrorMessage = ErrorMessage;
            }

            if (!String.IsNullOrEmpty(assessment))
            {
                Score = Score.Where(s => s.Assessment.Contains(assessment));
            }

            if (!String.IsNullOrEmpty(nametopic))
            {
                Score = Score.Where(s => s.Topic.Title.Contains(nametopic));
            }
            if (!String.IsNullOrEmpty(masv))
            {
                Score = Score.Where(s => s.Topic.Student.StudentCode.Contains(masv));
            }

            if (!Score.Any())
            {
                ViewBag.ErrorMessage = "Không tìm thấy kết quả phù hợp.";
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(Score.OrderBy(a => a.ScoreID).ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Scores/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.DeMuc = "Quản lý điểm";
            ViewBag.ActivePage = "Scores";
            ViewBag.TieuDe = "Điểm";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Score score = db.Scores.Find(id);
            if (score == null)
            {
                return HttpNotFound();
            }
            return View(score);
        }

        // GET: Admin/Scores/Create
        public ActionResult Create()
        {
            ViewBag.DeMuc = "Quản lý điểm";
            ViewBag.ActivePage = "Scores";
            ViewBag.TieuDe = "Điểm";
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "Title");
            var studentList = db.Students.Select(s => new {
                StudentID = s.StudentID,
                FullName = s.LastName + " " + s.FirstName
            }).ToList();
            ViewBag.StudentName = new SelectList(studentList, "StudentName", "FullName");
            return View();
        }

        // POST: Admin/Scores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ScoreID,Score1,Score2,Score3,Score4,Score5,Assessment,TopicID")] Score score)
        {
            if (ModelState.IsValid)
            {
                db.Scores.Add(score);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var studentList = db.Students.Select(s => new {
                StudentID = s.StudentID,
                FullName = s.LastName + " " + s.FirstName
            }).ToList();
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "Title", score.TopicID);
            ViewBag.StudentName = new SelectList(studentList, "StudentName", "FullName", score.Topic.StudentID);
            return View(score);
        }

        // GET: Admin/Scores/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.DeMuc = "Quản lý điểm";
            ViewBag.ActivePage = "Scores";
            ViewBag.TieuDe = "Điểm";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Score score = db.Scores.Find(id);
            if (score == null)
            {
                return HttpNotFound();
            }
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "Title", score.TopicID);
            return View(score);
        }

        // POST: Admin/Scores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ScoreID,Score1,Score2,Score3,Score4,Score5,Assessment,TopicID")] Score score)
        {
            if (ModelState.IsValid)
            {
                db.Entry(score).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "Title", score.TopicID);
            return View(score);
        }

        // POST: Admin/Scores/Delete/5
        [HttpPost]
        public JsonResult Delete(int ID)
        {
            int rowCount = db.Scores.Count();
            Score score = db.Scores.FirstOrDefault(x => x.ScoreID == ID);
            if (score != null)
            {
                db.Scores.Remove(score);
                db.SaveChanges();
            }
            db.Database.ExecuteSqlCommand($"DBCC CHECKIDENT('dbo.Score', RESEED, {rowCount - 1})");
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
            ViewBag.ControllerName = "Scores";
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    var scoreList = new List<Score>();
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;
                        var studentDict = db.Students.ToDictionary(f => System.Tuple.Create($"{f.LastName} {f.FirstName}", f.Classroom), x => x.StudentID);
                        var topicDict = db.Topics.ToDictionary(f => f.Title, x => x.TopicID);
                        int studentNotFoundCount = 0;
                        int topicNotFoundCount = 0;
                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                        {
                            var score = new Score();
                            score.ScoreID = Convert.ToInt32(workSheet.Cells[rowIterator, 1].Value);
                            var studentName = workSheet.Cells[rowIterator, 2].Value.ToString().Trim();
                            var studentClass = workSheet.Cells[rowIterator, 3].Value.ToString().Trim();
                            score.Score1 = Convert.ToDecimal(workSheet.Cells[rowIterator, 4].Value);
                            score.Score2 = Convert.ToDecimal(workSheet.Cells[rowIterator, 5].Value);
                            score.Score3 = Convert.ToDecimal(workSheet.Cells[rowIterator, 6].Value);
                            score.Score4 = Convert.ToDecimal(workSheet.Cells[rowIterator, 7].Value);
                            score.Score5 = Convert.ToDecimal(workSheet.Cells[rowIterator, 8].Value);
                            score.Assessment = workSheet.Cells[rowIterator, 9].Value.ToString().Trim();
                            var topicTitle = workSheet.Cells[rowIterator, 10].Value.ToString().Trim();

                            if (!studentDict.ContainsKey(System.Tuple.Create(studentName, studentClass)))
                            {
                                studentNotFoundCount++;
                                continue;
                            }

                            score.StudentID = studentDict[System.Tuple.Create(studentName, studentClass)];

                            if (!topicDict.ContainsKey(topicTitle))
                            {
                                studentNotFoundCount++;
                                continue;
                            }
                            score.TopicID = topicDict[topicTitle];

                            scoreList.Add(score);
                        }
                        if (studentNotFoundCount > 0)
                        {
                            ViewBag.StudentNotFoundCount = $"Có {studentNotFoundCount} sinh viên không tồn tại trong file Excel";
                        }
                        if (topicNotFoundCount > 0)
                        {
                            ViewBag.TopicNotFoundCount = $"Có {topicNotFoundCount} đề tài không tồn tại trong file Excel";
                        }
                    }


                    foreach (var item in scoreList)
                    {
                        db.Scores.Add(item);
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
            DataTable dt = GetScoreDataTable();

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add(dt, "Scores");

                ws.Range("A1:K1").Merge();
                ws.Cell(1, 1).Value = "Danh sách Điểm";
                ws.Cell(1, 1).Style.Font.FontSize = 16;
                ws.Cell(1, 1).Style.Font.Bold = true;
                ws.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws.Cell(2, 1).Value = "STT";
                ws.Cell(2, 2).Value = "Mã sinh viên";
                ws.Cell(2, 3).Value = "Tên sinh viên";
                ws.Cell(2, 4).Value = "Lớp";
                ws.Cell(2, 5).Value = "Điểm 1";
                ws.Cell(2, 6).Value = "Điểm 2";
                ws.Cell(2, 7).Value = "Điểm 3";
                ws.Cell(2, 8).Value = "Điểm 4";
                ws.Cell(2, 9).Value = "Điểm 5";
                ws.Cell(2, 10).Value = "Đánh giá";
                ws.Cell(2, 11).Value = "Tên đề tài";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        ws.Cell(i + 3, j + 1).Value = dt.Rows[i][j].ToString();
                    }
                }

                var headerRange = ws.Range("A2:K2");
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                ws.Columns().AdjustToContents();

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DanhSachDiem.xlsx");
                }
            }
        }
        public DataTable GetScoreDataTable()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[11]
            {
            new DataColumn("ScoreID"),
            new DataColumn("StudentCode"),
            new DataColumn("StudentName"),
            new DataColumn("Classroom"),
            new DataColumn("Score1"),
            new DataColumn("Score2"),
            new DataColumn("Score3"),
            new DataColumn("Score4"),
            new DataColumn("Score5"),
            new DataColumn("Assessment"),
            new DataColumn("TopicName"),
            });

            var scores = db.Scores.ToList();

            foreach (var s in scores)
            {
                var tp = db.Topics.FirstOrDefault(a => a.TopicID == s.TopicID);
                string namesv = CapitalizeFirstLetter(tp.Student.LastName + " "+ tp.Student.FirstName);
                dt.Rows.Add(s.ScoreID, tp.Student.StudentCode ,namesv, tp.Student.Classroom ,s.Score1, s.Score2, s.Score3, s.Score4, s.Score5, s.Assessment, s.Topic.Title);
            }

            return dt;
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

        public ActionResult Download()
        {
            string filePath = Server.MapPath("~/Content/assets/file/Diem.xlsx");

            if (System.IO.File.Exists(filePath))
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=Diem.xlsx");

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

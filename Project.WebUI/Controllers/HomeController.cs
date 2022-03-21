using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.DAL.Abstract;
using Project.WebUI.Areas.Admin.Models;
using Project.WebUI.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.WebUI.Controllers
{
    [TypeFilter(typeof(UserAuth))]
    public class HomeController : Controller
    {
        private ILogicService _service;
        public HomeController(ILogicService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            var user = _service.User.FindByCondition(x => x.UserMail == HttpContext.Session.GetString("UserSession")).Include(y => y.UserExams).FirstOrDefault();
            var exams = user.UserExams.Where(x => x.ExamDate ==DateTime.Now.Date && x.IsStarted == false).ToList();
            if (exams.Count != 0)
            {
                ViewBag.ThereIsExam = 1;
            }
            else
            {
                ViewBag.ThereIsExam = 0;
            }
            return View();
        }
        [HttpPost]
        public IActionResult Exam()
        {
            var user = _service.User.FindByCondition(x => x.UserMail == HttpContext.Session.GetString("UserSession"))
                .Include(y => y.UserExams)
                .ThenInclude(y => y.Exam)
                .ThenInclude(y=>y.Text.Questions)
                .ThenInclude(x=>x.Options)
                .FirstOrDefault();
            var exams = user.UserExams.Where(x => x.ExamDate == DateTime.Now.Date && x.IsStarted == false)
                .OrderBy(x=>x.ExamDate)
                .FirstOrDefault();

            if (exams != null)
            {
                exams.IsStarted = true;
                _service.UserExam.Update(exams);
                _service.Save();
                return View(exams);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public JsonResult CalculateScore()
        {
            GenericResponse<List<string>> response = new GenericResponse<List<string>>();


            try
            {
                int ExamID = int.Parse(Request.Query["ExamID"]);
                List<string> corrects = new List<string>();
                List<string> SelectCorrects = new List<string>();
                SelectCorrects.Add("This is for count.");
                var currentExam = _service.UserExam.FindByCondition(x => x.ExamID == ExamID && x.User.UserMail== HttpContext.Session.GetString("UserSession"))
                    .Include(x=>x.Exam)
                    .ThenInclude(x=>x.Text)
                    .ThenInclude(x=>x.Questions)
                    .ThenInclude(x=>x.Options)
                    .FirstOrDefault();
                foreach (var question in currentExam.Exam.Text.Questions)
                {
                    
                    foreach (var option in question.Options)
                    {
                        if (option.IsTrue)
                        {
                            SelectCorrects.Add(option.ID.ToString());
                            if (option.ID == int.Parse(Request.Query["question_" + question.ID].ToString()))
                            {
                                corrects.Add(Request.Query["question_" + question.ID].ToString());
                            }
                        }
                       
                        

                    }
                    
                }
                string countScore = (corrects.Count * 25).ToString();
                SelectCorrects[0] = countScore;
                response.Data = SelectCorrects;
                response.HasError = false;
                response.Message = "Transaction Completed!";
            }
            catch (Exception exp)
            {

                response.HasError = true;
                response.Message = "Operation Failed!";
            }

            return Json(response);
        }
    }
}

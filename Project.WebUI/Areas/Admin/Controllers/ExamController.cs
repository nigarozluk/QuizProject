using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.DAL.Abstract;
using Project.DAL.Entity;
using Project.WebUI.Areas.Admin.Models;
using Project.WebUI.Library;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Project.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuth]
    public class ExamController : Controller
    {
        private ILogicService _service;
        public ExamController(ILogicService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            string url = "http://wired.com/most-recent/";
           

            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = client.GetAsync(url).Result)
                {
                    using (HttpContent content = response.Content)
                    {
                        string result = content.ReadAsStringAsync().Result;
                        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                        doc.LoadHtml(result);
                        HtmlNodeCollection selectedtag = doc.DocumentNode.SelectNodes("//div[contains(@class, 'archive-item-component__info')]");
                        var data = selectedtag.Take(5);
                        List<DataTitleLinkModel> list = new List<DataTitleLinkModel>();
                        foreach (var item in data)
                        {
                             list.Add(new DataTitleLinkModel()
                            {
                                Title = item.LastChild.FirstChild.InnerText,
                                Link = item.LastChild.Attributes["href"].Value,
                               
                            });
                        }
                        ViewBag.Data = list;

                    }
                }
            }


            return View();
        }
        [HttpGet]
        public IActionResult GetContent(string link,string title)
        
        {
            string text="";
            //List<string> list = new List<string>();
            string url = "http://wired.com" + link;

            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = client.GetAsync(url).Result)
                {
                    using (HttpContent content = response.Content)
                    {
                        string result = content.ReadAsStringAsync().Result;
                        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                       
                        doc.LoadHtml(result);
                        HtmlNodeCollection selectedtag = doc.DocumentNode.SelectNodes("//div[contains(@class, 'body__inner-container')]");
                        if (selectedtag!=null)
                        {
                            foreach (var item in selectedtag)
                            {
                                if (item.SelectSingleNode("ul")!=null)
                                {
                                    item.SelectSingleNode("ul").Remove();
                                }
                                if (item.SelectSingleNode("//div[contains(@class, 'paywall heading-h3')]") !=null)
                                {
                                    item.SelectSingleNode("//div[contains(@class, 'paywall heading-h3')]").Remove();
                                }
                              
                               
                                text += System.Net.WebUtility.HtmlDecode(item.InnerText);
                            }
                        }
                        
                        
                    }
                }
            }
            ViewBag.Title = title;
            ViewBag.ExamData = text;
            return View();
        }
        [HttpGet]
        public IActionResult ExamList()
        {
            ViewBag.Text = _service.Text.FindAll();
            return View();
        }
        [HttpPost]
        public IActionResult CreateExam()
        {
            try
            {
                var text = _service.Text.FindByCondition(x => x.TextTitle == Request.Form["Title"].ToString()).FirstOrDefault();
                if (text==null)
                {
                    var TextData = new Text()
                    {
                        TextTitle = (Request.Form["Title"].ToString()),
                        TextContent = (Request.Form["Text"].ToString())
                    };
                    _service.Text.Create(TextData);
                    _service.Save();

                    for (int i = 1; i < 5; i++)
                    {
                        var Question = new Question()
                        {
                            TextID = TextData.ID,
                            QuestionText = (Request.Form["Question_" + i].ToString()),
                        };
                        _service.Question.Create(Question);
                        _service.Save();
                        for (int j = 1; j < 5; j++)
                        {
                            var Option = new Option()
                            {
                                QuestionID = Question.ID,
                                OptionText = (Request.Form["Answer" + j + "_" + i].ToString()),
                                IsTrue = j.ToString() == (Request.Form["CorrectAnswer_" + i].ToString()) ? true : false
                            };
                            _service.Option.Create(Option);
                            _service.Save();
                        }

                    }
                    TempData["Message"] = "Saved.";
                }
                else
                {
                    TempData["Message"] = "Text is Already Used in a Exam!!!.";
                }
               
            }
            catch (Exception)
            {

                throw;
            }


            return RedirectToAction("Index", "Exam");
        }
        [HttpPost]
        public JsonResult GetExamList(DatatablesModel.DataTablesRequestParameter dataTablesRequestParameter)
        {
            DatatablesModel.DataTablesResponseParameter<ExamListModel> dataTablesReponseParameter = new DatatablesModel.DataTablesResponseParameter<ExamListModel>();
            var data = _service.UserExam.FindAll()
                .Include(x => x.Exam)
                .Include(x => x.User)
                 .Select(k => new ExamListModel
                 {
                     Title = k.Exam.Text.TextTitle,
                     ExamID= k.ExamID,
                     ExamDate = k.ExamDate,
                     UserMail=k.User.UserMail
                 }).ToList(); 
            data = data.Skip(dataTablesRequestParameter.start).Take(dataTablesRequestParameter.length).ToList();
            dataTablesReponseParameter.recordsTotal = data.Count();
            //List<Exam> list = data.ToList();
            dataTablesReponseParameter.draw = dataTablesRequestParameter.draw;
            dataTablesReponseParameter.recordsFiltered = dataTablesReponseParameter.recordsTotal;
            dataTablesReponseParameter.data = data;
            return Json(dataTablesReponseParameter);
        }
        [HttpPost]
        public JsonResult AddExam(int TextID,string UserMail,DateTime ExamDate)
        {
            GenericResponse<string> response = new GenericResponse<string>();
            try
            {
                    var exam = new Exam()
                    {
                        TextID = TextID,
                    };
                    _service.Exam.Create(exam);
                    _service.Save();
                    var user = _service.User.FindByCondition(x => x.UserMail == UserMail).FirstOrDefault();
                    if (user == null)
                    {
                        user = new User()
                        {
                            UserName = UserMail,
                            UserMail = UserMail,
                            UserPassword = "123456",
                        };
                        _service.User.Create(user);
                        _service.Save();
                    }
                    var userExam = new UserExam()
                    {
                        UserID = user.ID,
                        ExamID = exam.ID,
                        ExamDate = ExamDate

                    };
                    _service.UserExam.Create(userExam);
                    _service.Save();
                    response.HasError = false;
                    response.Message = "Transaction Completed!";
                

               
            }
            catch (Exception)
             {

                    response.HasError = true;
                    response.Message = "Operation Failed!";
             }

            return Json(response);
        }
        [HttpPost]
        public JsonResult RemoveExam(int examID, string userMail)
        {
            GenericResponse<string> response = new GenericResponse<string>();

         
                try
                {
                    var user = _service.User.FindByCondition(x=>x.UserMail==userMail).FirstOrDefault();
                    var userExam = _service.UserExam.FindByCondition(x => x.ExamID == examID && x.UserID == user.ID).FirstOrDefault();
                    _service.UserExam.Delete(userExam);
                    _service.Save();
                    response.HasError = false;
                    response.Message = "Transaction Completed!";
                }
                catch (Exception)
                {

                    response.HasError = true;
                    response.Message = "Operation Failed!";
                }

            return Json(response);
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalHouseFinding.Models;
using RentalHouseFinding.Common;
using RentalHouseFinding.Caching;

namespace RentalHouseFinding.Controllers.User
{ 
    public class QuestionsController : Controller
    {
        private RentalHouseFindingEntities _db = new RentalHouseFindingEntities();

        public ICacheRepository Repository { get; set; }
        public QuestionsController()
            : this(new CacheRepository())
        {
        }

        public QuestionsController(ICacheRepository repository)
        {
            this.Repository = repository;
        }

        //
        // GET: /Questions/

        [Authorize(Roles = "Admin, User")]
        public ViewResult Index()
        {
            int userId = CommonModel.GetUserIdByUsername(User.Identity.Name);
            var lstPostId = _db.Posts.Where(p => p.UserId == userId && !p.IsDeleted).Select(p => p.Id).ToList();

            var lstQuestion = _db.Questions.Where(q => lstPostId.Contains(q.PostId) && !q.IsDeleted).OrderByDescending(q=>q.CreatedDate).ToList();
            var lstSentQuestion = _db.Questions.Where(q => q.SenderId == userId && !q.IsDeleted).OrderByDescending(q=>q.CreatedDate).ToList();
            lstQuestion.AddRange(lstSentQuestion);
            lstQuestion.OrderByDescending(q => q.CreatedDate);
            return View(lstQuestion);
        }

        //
        // GET: /Questions/Details/5
        [Authorize(Roles = "Admin, User")]
        public ViewResult Details(int id)
        {
            Questions questions = _db.Questions.Single(q => q.Id == id);

            questions.IsRead = true;
            _db.ObjectStateManager.ChangeObjectState(questions, System.Data.EntityState.Modified);
            _db.SaveChanges();
            return View(questions);
        }

        [HttpPost]
        [Authorize(Roles = "User, Admin")]
        public ActionResult SendAnswer(AnswerViewModel model)
        {
            try
            {
                Answers answerToCreate = new Answers();
                answerToCreate.Content = model.ContentAnswer;
                answerToCreate.CreatedDate = DateTime.Now;
                answerToCreate.QuestionId = model.QuestionId;
                answerToCreate.IsRead = false;
                answerToCreate.IsDeleted = false;
                _db.Answers.AddObject(answerToCreate);
                _db.SaveChanges();

                if (!string.IsNullOrEmpty(answerToCreate.Question.SenderEmail))
                {
                    string emailTemplate = Repository.GetAllEmailTemplate().Where(e => e.Name.Equals(ConstantEmailTemplate.RECEIVE_ANSWER, StringComparison.CurrentCultureIgnoreCase)).Select(m => m.Template).FirstOrDefault();
                    string message = string.Format(emailTemplate, User.Identity.Name, model.ContentAnswer, answerToCreate.Question.Title, answerToCreate.Question.Content);
                    CommonModel.SendEmail(answerToCreate.Question.SenderEmail, message, "Bạn nhận được 1 câu trả lời", 0);
                }

                return View(answerToCreate);
            }
            catch
            {
                return Content("Có lỗi xảy ra", "text/html");
            }
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
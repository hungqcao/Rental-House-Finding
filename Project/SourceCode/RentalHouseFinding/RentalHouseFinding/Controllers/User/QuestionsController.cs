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
            ViewBag.Question = lstQuestion;
            ViewBag.SentQuestion = lstSentQuestion;
            return View();
        }

        //
        // GET: /Questions/Details/5
        [Authorize(Roles = "Admin, User")]
        public ViewResult Details(int id)
        {
            Questions questions = _db.Questions.Single(q => q.Id == id);

            questions.IsRead = true;
            _db.ObjectStateManager.ChangeObjectState(questions, System.Data.EntityState.Modified);

            foreach (var item in questions.Answers)
            {
                item.IsRead = true;
                _db.ObjectStateManager.ChangeObjectState(questions, System.Data.EntityState.Modified);
            }
            _db.SaveChanges();
            return View(questions);
        }

        [HttpPost]
        public ActionResult SendQuestion(QuestionViewModel model)
        {
            try
            {
                int postId = Convert.ToInt32(Session["PostID"]);
                Users user = null;

                var post = _db.Posts.Where(p => p.Id == postId).FirstOrDefault();
                if (post.UserId != null)
                {
                    user = _db.Users.Where(u => u.Id == post.UserId).FirstOrDefault();
                }

                Questions questionToCreate = new Questions();
                questionToCreate.Content = model.ContentQuestion.Trim();
                questionToCreate.Title = model.TitleQuestion.Trim();
                questionToCreate.CreatedDate = DateTime.Now;
                questionToCreate.IsDeleted = false;
                questionToCreate.IsRead = false;
                questionToCreate.PostId = postId;
                if (model.UserId != 0)
                {
                    questionToCreate.SenderId = model.UserId;
                }
                else
                {
                    questionToCreate.SenderId = null;
                }
                questionToCreate.SenderEmail = model.Email;

                _db.Questions.AddObject(questionToCreate);
                _db.SaveChanges();

                string message = string.Empty;
                if (user != null)
                {
                    string emailTemplate = Repository.GetAllEmailTemplate().Where(e => e.Name.Equals(ConstantEmailTemplate.RECEIVE_QUESTION, StringComparison.CurrentCultureIgnoreCase)).Select(m => m.Template).FirstOrDefault();
                    message = string.Format(emailTemplate, model.Email, model.TitleQuestion, model.ContentQuestion, post.Title);
                    CommonModel.SendEmail(user.Email, message, "Bạn nhận được 1 câu hỏi", 0);
                }
                else if (!string.IsNullOrEmpty(post.Contacts.Email))
                {
                    string emailTemplate = Repository.GetAllEmailTemplate().Where(e => e.Name.Equals(ConstantEmailTemplate.RECEIVE_QUESTION, StringComparison.CurrentCultureIgnoreCase)).Select(m => m.Template).FirstOrDefault();
                    message = string.Format(emailTemplate, model.Email, model.TitleQuestion, model.ContentQuestion, post.Title);
                    CommonModel.SendEmail(post.Contacts.Email, message, "Bạn nhận được 1 câu hỏi", 0);
                }

                if (user != null)
                {
                    UserLogs log = new UserLogs();
                    log.UserId = user.Id;
                    log.Message = message;
                    log.IsRead = false;
                    log.CreatedDate = DateTime.Now;
                    _db.UserLogs.AddObject(log);
                    _db.SaveChanges();
                }
                return Content("Câu hỏi đã được gửi đi", "text/html");
            }
            catch
            {
                return Content("Có lỗi xảy ra!", "text/html");
            }

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

                string message = string.Empty;
                if (!string.IsNullOrEmpty(answerToCreate.Question.SenderEmail))
                {
                    string emailTemplate = Repository.GetAllEmailTemplate().Where(e => e.Name.Equals(ConstantEmailTemplate.RECEIVE_ANSWER, StringComparison.CurrentCultureIgnoreCase)).Select(m => m.Template).FirstOrDefault();
                    message = string.Format(emailTemplate, User.Identity.Name, model.ContentAnswer, answerToCreate.Question.Title, answerToCreate.Question.Content);
                    CommonModel.SendEmail(answerToCreate.Question.SenderEmail, message, "Bạn nhận được 1 câu trả lời", 0);
                }

                if (answerToCreate.Question.SenderId.HasValue)
                {
                    UserLogs log = new UserLogs();
                    log.UserId = answerToCreate.Question.SenderId.Value;
                    log.Message = message;
                    log.IsRead = false;
                    log.CreatedDate = DateTime.Now;
                    _db.UserLogs.AddObject(log);
                    _db.SaveChanges();
                }

                Questions questions = _db.Questions.Single(q => q.Id == model.QuestionId);
                return View(questions);
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
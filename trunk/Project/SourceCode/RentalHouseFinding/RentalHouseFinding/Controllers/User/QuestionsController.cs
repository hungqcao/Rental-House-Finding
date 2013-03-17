using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalHouseFinding.Models;
using RentalHouseFinding.RHF.Common;

namespace RentalHouseFinding.Controllers.User
{ 
    public class QuestionsController : Controller
    {
        private RentalHouseFindingEntities _db = new RentalHouseFindingEntities();

        //
        // GET: /Questions/

        [Authorize(Roles = "Admin, User")]
        public ViewResult Index()
        {
            int userId = CommonModel.GetUserIdByUsername(User.Identity.Name);
            var lstPostId = _db.Posts.Where(p => p.UserId == userId && !p.IsDeleted).Select(p => p.Id).ToList();

            var lstQuestion = _db.Questions.Where(q => lstPostId.Contains(q.PostId) && !q.IsDeleted).ToList();
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
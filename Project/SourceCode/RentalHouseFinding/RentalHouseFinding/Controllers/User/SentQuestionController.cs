using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalHouseFinding.Models;
using RentalHouseFinding.RHF.Common;

namespace RentalHouseFinding.Controllers.User
{
    public class SentQuestionController : Controller
    {
        private RentalHouseFindingEntities _db = new RentalHouseFindingEntities();

        //
        // GET: /Questions/

        [Authorize(Roles = "Admin, User")]
        public ViewResult Index()
        {
            int userId = CommonModel.GetUserIdByUsername(User.Identity.Name);

            var lstQuestion = _db.Questions.Where(q => q.SenderId == userId && !q.IsDeleted).ToList();
            return View(lstQuestion);
        }

        //
        // GET: /Questions/Details/5
        [Authorize(Roles = "Admin, User")]
        public ViewResult Details(int id)
        {
            Questions questions = _db.Questions.Single(q => q.Id == id);

            foreach (var item in questions.Answers)
            {
                item.IsRead = true;
                _db.ObjectStateManager.ChangeObjectState(item, System.Data.EntityState.Modified);
            }
            if (questions.Answers != null && questions.Answers.Count != 0)
            {
                _db.SaveChanges();
            }
            return View(questions);
        }

    }
}

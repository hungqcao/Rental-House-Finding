using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalHouseFinding.Models;
using System.Data;
using RentalHouseFinding.Caching;
using RentalHouseFinding.Common;
using System.Text.RegularExpressions;

namespace RentalHouseFinding.Controllers
{
    public class SMSController : Controller
    {

        RentalHouseFindingEntities _db = new RentalHouseFindingEntities();
        private string _noInfo;

        public ICacheRepository Repository { get; set; }
        public SMSController()
            : this(new CacheRepository())
        {
        }

        public SMSController(ICacheRepository repository)
        {
            this.Repository = repository;
            this._noInfo = Repository.GetAllConfiguration().Where(c => c.Name.Equals(ConstantCommonString.NONE_INFORMATION, StringComparison.CurrentCultureIgnoreCase)).Select(c => c.Value).FirstOrDefault().ToString();
        }
        
        
        //
        // GET: /SMS/
        public ActionResult Index()
        {
            return View();
        }
        // GET: /SMS/
        public ActionResult SMSActive()
        {            
            return View();
        }
        [HttpPost]
        public ActionResult SMSActive(SMSModel model)
        {
            Payments payment = new Payments();
            if (ModelState.IsValid)
            {
                try
                {
                    //ContentSMS =  MS ABCD                
                    string code = !String.IsNullOrEmpty(model.ContentSMS) ? model.ContentSMS.Split(' ')[1].ToString() : String.Empty;
                    var postId = (from p in _db.Posts
                                  where p.Code.Equals(code, StringComparison.CurrentCultureIgnoreCase) && !p.IsDeleted && p.StatusId != 2// statusId = 2 is Pending.
                                  select p.Id).FirstOrDefault();
                    if (postId != 0)
                    {
                        payment.PhoneNumber = model.PhoneNumber;
                        payment.PostsId = postId;
                        payment.CreatedDate = DateTime.Now;
                        _db.AddToPayments(payment);
                        _db.SaveChanges();
                        //Update Post table
                        string strExpiredDate = Repository.GetAllConfiguration().Where(c => c.Name.Equals(ConstantCommonString.EXPIRED_DATE_AFTER_RENEW, StringComparison.CurrentCultureIgnoreCase)).Select(c => c.Value).FirstOrDefault().ToString();
                        int numberExpiredDate = 0;
                        int.TryParse(strExpiredDate, out numberExpiredDate);

                        var post = _db.Posts.Where(p => p.Id == postId).FirstOrDefault();

                        DateTime currentExpiredDate = post.ExpiredDate;
                        //Check current ExpriedDate.
                        if (DateTime.Compare(currentExpiredDate, DateTime.Now) <= 0)
                        {
                            // currently after SMSActive added 15days.
                            post.ExpiredDate = DateTime.Now.AddDays(numberExpiredDate);
                        }
                        else
                        {
                            post.ExpiredDate = currentExpiredDate.AddDays(numberExpiredDate);
                        }
                        post.StatusId = 1;// 1 is Active.
                        post.RenewDate = DateTime.Now;
                        _db.ObjectStateManager.ChangeObjectState(post, EntityState.Modified);
                        _db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Sai cú pháp hoặc mã tin không đúng.");
                    }
                }
                catch (Exception ex)
                {                                        
                }
            }       
            
            return View();
        }

    }
}

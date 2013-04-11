using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalHouseFinding.Models;
using System.Web.Helpers;
using System.Data.Objects;

namespace RentalHouseFinding.Controllers.Admin
{
    public class ManagePaymentController : Controller
    {
        private const int MAX_RECORD_PER_PAGE = 15;
        private readonly static RentalHouseFindingEntities _db = new RentalHouseFindingEntities();

        //
        // GET: /ManagePayment/

        public ActionResult Index(int? page, string sort, string sortdir, ManagePaymentModel model)
        {
            if (page == null)
            {
                page = 1;
            }
            IQueryable<Payments> paymentList = _db.Payments;
            var paymentViewList = paymentList.Select(p => new
            {
                Code = p.Post.Code,
                p.CreatedDate,
                p.PhoneNumber,
                PostTitle = (from post in _db.Posts
                             where (post.Id == p.PostsId)
                             select post.Title).FirstOrDefault(),
                p.PostsId
            });
            
            //Custom sort
            if (sortdir == "ASC")
            {
                if (sort == "PostTitle")
                {
                    paymentViewList = paymentViewList.OrderBy(p => p.PostTitle)
                        .Skip(MAX_RECORD_PER_PAGE * ((int)page - 1)).Take(MAX_RECORD_PER_PAGE);
                }
                else if (sort == "PhoneNumber")
                {
                    paymentViewList = paymentViewList.OrderBy(p => p.PhoneNumber)
                        .Skip(MAX_RECORD_PER_PAGE * ((int)page - 1)).Take(MAX_RECORD_PER_PAGE);
                }
                else if (sort == "CreatedDate")
                {
                    paymentViewList = paymentViewList.OrderBy(p => p.CreatedDate)
                        .Skip(MAX_RECORD_PER_PAGE * ((int)page - 1)).Take(MAX_RECORD_PER_PAGE);
                }
            }
            else
            {
                if (sort == "PostTitle")
                {
                    paymentViewList = paymentViewList.OrderByDescending(p => p.PostTitle)
                        .Skip(MAX_RECORD_PER_PAGE * ((int)page - 1)).Take(MAX_RECORD_PER_PAGE);
                }
                else if (sort == "PhoneNumber")
                {
                    paymentViewList = paymentViewList.OrderByDescending(p => p.PhoneNumber)
                        .Skip(MAX_RECORD_PER_PAGE * ((int)page - 1)).Take(MAX_RECORD_PER_PAGE);
                }
                else
                {
                    paymentViewList = paymentViewList.OrderByDescending(p => p.CreatedDate)
                        .Skip(MAX_RECORD_PER_PAGE * ((int)page - 1)).Take(MAX_RECORD_PER_PAGE);
                }
            }

            var grid = new WebGrid(ajaxUpdateContainerId: "container-grid", 
                rowsPerPage: MAX_RECORD_PER_PAGE);
            grid.Bind(paymentViewList, autoSortAndPage: false, rowCount: paymentList.Count());
            model.Grid = grid;
            ViewBag.Index = ((int)page - 1) * MAX_RECORD_PER_PAGE;
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(ManagePaymentModel model)
        {
            IQueryable<Payments> paymentList = _db.Payments;

            if (model.CreatedDateFrom != null)
            {
                paymentList = paymentList.Where(p => (EntityFunctions
                            .DiffDays(p.CreatedDate, model.CreatedDateFrom) <= 0));
            }

            if (model.CreatedDateTo != null)
            {
                paymentList = paymentList.Where(p => (EntityFunctions
                            .DiffDays(p.CreatedDate, model.CreatedDateTo) >= 0));
            }

            var paymentViewList = paymentList.Select(p => new
            {
                Code = p.Post.Code,
                p.CreatedDate,
                p.PhoneNumber,
                PostTitle = (from post in _db.Posts
                             where (post.Id == p.PostsId)
                             select post.Title).FirstOrDefault(),
                p.PostsId
            }).OrderByDescending(p => p.CreatedDate).Skip(0).Take(MAX_RECORD_PER_PAGE);

            var grid = new WebGrid(ajaxUpdateContainerId: "container-grid",
            canSort: false, rowsPerPage: MAX_RECORD_PER_PAGE);
            grid.Bind(paymentViewList, autoSortAndPage: false, rowCount: paymentList.Count());
            model.Grid = grid;
            ViewBag.Index = 0;
            return View(model);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalHouseFinding.Models;

namespace RentalHouseFinding.Controllers.Admin
{
    public class CreateFacilityTemplateController : Controller
    {
        private RentalHouseFindingEntities _db = new RentalHouseFindingEntities();
        //
        // GET: /CreateFacilityTemplate/

        public ActionResult Index(double id)
        {
            FacilityTemplates objToCreate;
            
            double numberOfColumn = Math.Pow(2, id) - 1;
            for (int i = 0; i <= numberOfColumn; i++)
            {
                string binaryString = Convert.ToString(i, 2);
                while (binaryString.Length != 11)
                {
                    binaryString = "0" + binaryString;
                }
                objToCreate = new FacilityTemplates();
                objToCreate.Column11 = binaryString[10].ToString().Equals("0") ? false : true;
                objToCreate.Column10 = binaryString[9].ToString().Equals("0") ? false : true;
                objToCreate.Column9 = binaryString[8].ToString().Equals("0") ? false : true;
                objToCreate.Column8 = binaryString[7].ToString().Equals("0") ? false : true;
                objToCreate.Column7 = binaryString[6].ToString().Equals("0") ? false : true;
                objToCreate.Column6 = binaryString[5].ToString().Equals("0") ? false : true;
                objToCreate.Column5 = binaryString[4].ToString().Equals("0") ? false : true;
                objToCreate.Column4 = binaryString[3].ToString().Equals("0") ? false : true;
                objToCreate.Column3 = binaryString[2].ToString().Equals("0") ? false : true;
                objToCreate.Column2 = binaryString[1].ToString().Equals("0") ? false : true;
                objToCreate.Column1 = binaryString[0].ToString().Equals("0") ? false : true;
                _db.FacilityTemplates.AddObject(objToCreate);
                
            }
            _db.SaveChanges();
            return View();
        }

    }
}

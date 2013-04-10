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
                objToCreate.IsStayWithOwner = binaryString[10].ToString().Equals("0") ? false : true;
                objToCreate.IsAllowCooking = binaryString[9].ToString().Equals("0") ? false : true;
                objToCreate.HasWaterHeater = binaryString[8].ToString().Equals("0") ? false : true;
                objToCreate.HasTVCable = binaryString[7].ToString().Equals("0") ? false : true;
                objToCreate.HasToilet = binaryString[6].ToString().Equals("0") ? false : true;
                objToCreate.HasSecurity = binaryString[5].ToString().Equals("0") ? false : true;
                objToCreate.HasMotorParking = binaryString[4].ToString().Equals("0") ? false : true;
                objToCreate.HasInternet = binaryString[3].ToString().Equals("0") ? false : true;
                objToCreate.HasGarage = binaryString[2].ToString().Equals("0") ? false : true;
                objToCreate.HasBed = binaryString[1].ToString().Equals("0") ? false : true;
                objToCreate.HasAirConditioner = binaryString[0].ToString().Equals("0") ? false : true;
                _db.FacilityTemplates.AddObject(objToCreate);
                
            }
            _db.SaveChanges();
            return View();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentalHouseFinding.Models;
using System.Web.Mvc;

namespace RentalHouseFinding.RHF.DAL
{
    public class FullTextSearchHelper : IDisposable
    {
        private RentalHouseFindingEntities _db = new RentalHouseFindingEntities();

        public void Dispose()
        {
            try
            {
                if (_db != null)
                {
                    _db.Dispose();
                    _db = null;
                }
            }
            catch
            {
            }
        }

        [HttpPost]
        public List<FullTextSearchPost_ResultSuggestion> GetFullTextSuggestion(int categoryId, int provinceId, int DistrictId, string keyWords, int skip, int take)
        {
            try
            {
                var listSuggestion = _db.FullTextSearchPost(categoryId, provinceId, DistrictId, keyWords).Skip(skip).Take(take);
               
                return listSuggestion.ToList();
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public List<AdvancedSearchFacility_Result> AdvanceSearch(int categoryId, 
                                                                int provinceId, 
                                                                int DistrictId, 
                                                                double areaMax,
                                                                double areaMin,
                                                                double priceMax,
                                                                double priceMin,
                                                                int hasAirConditionerScore,
                                                                int hasBedScore,
                                                                int hasGarageScore,
                                                                int hasInternetScore,
                                                                int hasMotorParkingLotScore,
                                                                int hasSecurityScore,
                                                                int hasTVCableScore,
                                                                int hasWaterHeaterScore,
                                                                int isAllowCookingScore,
                                                                int isStayWithOwnerScore,
                                                                int hasToiletScore,
                                                                int skip, 
                                                                int take)
        {
            try
            {
                var listSuggestion = _db.AdvancedSearchFacilities(categoryId, 
                                                                provinceId, 
                                                                DistrictId, 
                                                                areaMax, 
                                                                areaMin, 
                                                                priceMax, 
                                                                priceMin, 
                                                                hasAirConditionerScore,
                                                                hasBedScore,
                                                                hasGarageScore,
                                                                hasInternetScore,
                                                                hasMotorParkingLotScore,
                                                                hasSecurityScore,
                                                                hasTVCableScore,
                                                                hasWaterHeaterScore,
                                                                isAllowCookingScore,
                                                                isStayWithOwnerScore,
                                                                hasToiletScore).Skip(skip).Take(take);

                return listSuggestion.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
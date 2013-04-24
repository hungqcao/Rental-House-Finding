using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentalHouseFinding.Models;
using System.Web.Mvc;

namespace RentalHouseFinding.Common
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

        public List<FullTextSearchPostWithWeightenScore_Result> FullTextSearchPostWithWeightenScore(int categoryId,
                                                                                                    int provinceId, 
                                                                                                    int districtId, 
                                                                                                    string keyWords, 
                                                                                                    int descriptionColumnScore,
                                                                                                    int titleColumnScore,
                                                                                                    int streetColumnScore,
                                                                                                    int nearbyColumnScore,
                                                                                                    int numberAddressColumnScore,
                                                                                                    int skip, 
                                                                                                    int take,
                                                                                                    out int numberOfResult)
        {
            try
            {
                var listSuggestion = _db.FullTextSearchPostWithWeightenScore(categoryId, 
                                                                            provinceId, 
                                                                            districtId, 
                                                                            keyWords, 
                                                                            titleColumnScore, 
                                                                            descriptionColumnScore, 
                                                                            streetColumnScore, 
                                                                            nearbyColumnScore,
                                                                            numberAddressColumnScore).ToList();
                numberOfResult = listSuggestion.Count();
                var listReturn = listSuggestion.Skip(skip).Take(take);
                return listReturn.ToList();
            }
            catch (Exception ex)
            {
                numberOfResult = 0;
                return null;
            }
        }

        public List<AdvancedSearchFacility_Result> AdvanceSearch(int categoryId, 
                                                                int provinceId, 
                                                                int districtId, 
                                                                double? areaMax,
                                                                double? areaMin,
                                                                double? priceMax,
                                                                double? priceMin,
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
                                                                int take,
                                                                out int numberOfResult)
        {
            try
            {
                if (areaMax == null)
                {
                    areaMax = 0;
                }
                if (areaMin == null)
                {
                    areaMin = 0;
                }
                if (priceMax == null)
                {
                    priceMax = 0;
                }
                if (priceMin == null)
                {
                    priceMin = 0;
                }
                var listSuggestion = _db.AdvancedSearchFacilities(categoryId,
                                            provinceId,
                                            districtId,
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
                                            hasToiletScore,
                                            hasTVCableScore,
                                            hasWaterHeaterScore,
                                            isAllowCookingScore,
                                            isStayWithOwnerScore).ToList(); 
                
                numberOfResult = listSuggestion.Count();
                var listReturn = listSuggestion.Skip(skip).Take(take);
                return listReturn.ToList();
            }
            catch (Exception ex)
            {
                numberOfResult = 0;
                return null;
            }
        }

        public List<FullTextSearchPostWithWeightenScore_Result> FullTextSearchPostWithWeightenScoreTakeAll(int categoryId,
                                                                                                    int provinceId,
                                                                                                    int districtId,
                                                                                                    string keyWords,
                                                                                                    int descriptionColumnScore,
                                                                                                    int titleColumnScore,
                                                                                                    int streetColumnScore,
                                                                                                    int nearbyColumnScore,
                                                                                                    int numberAddressColumnScore)
        {
            try
            {
                var listSuggestion = _db.FullTextSearchPostWithWeightenScore(categoryId, 
                                                                            provinceId, 
                                                                            districtId, 
                                                                            keyWords, 
                                                                            titleColumnScore, 
                                                                            descriptionColumnScore, 
                                                                            streetColumnScore, 
                                                                            nearbyColumnScore, 
                                                                            numberAddressColumnScore);

                return listSuggestion.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<AdvancedSearchFacility_Result> AdvanceSearchTakeAll(int categoryId,
                                                                int provinceId,
                                                                int DistrictId,
                                                                double? areaMax,
                                                                double? areaMin,
                                                                double? priceMax,
                                                                double? priceMin,
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
                                                                int hasToiletScore)
        {
            try
            {
                if (areaMax == null)
                {
                    areaMax = 0;
                }
                if (areaMin == null)
                {
                    areaMin = 0;
                }
                if (priceMax == null)
                {
                    priceMax = 0;
                }
                if (priceMin == null)
                {
                    priceMin = 0;
                }
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
                                                                hasToiletScore,
                                                                hasTVCableScore,
                                                                hasWaterHeaterScore,
                                                                isAllowCookingScore,
                                                                isStayWithOwnerScore);

                return listSuggestion.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
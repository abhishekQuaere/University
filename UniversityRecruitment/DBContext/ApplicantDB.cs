using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityRecruitment.Models;
using UniversityRecruitment.Utilities;

namespace UniversityRecruitment.DBContext
{
    public class ApplicantDB
    {
        DapperDbContext _dapper = new DapperDbContext();
        SessionManager sm = new SessionManager();

        public List<SelectListItem> PostList()
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                List<SelectListItem> _iresult = _dapper.GetAll<SelectListItem>("Proc_GetAllPostList", dynamicParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        //public List<T> ListOfPostForApplying<T>(int PostId)
        //{
        //    try
        //    {
        //        DynamicParameters dynamicParameters = new DynamicParameters();
        //        dynamicParameters.Add("PostId", PostId, DbType.Int32);
        //        var res = _dapper.GetAll<T>("Proc_ListOfPostForApplying", dynamicParameters);
        //        return res;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        public T saveAppliedForm<T>(saveAppliedForm model)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("Id", model.UserId, DbType.Int32);
                dynamicParameters.Add("PostCode", model.postCode, DbType.Int32);
                dynamicParameters.Add("ApplyingCategory", model.Category, DbType.Int32);
                dynamicParameters.Add("ApplyingSubCategory", model.SubCategory, DbType.Int32);
                dynamicParameters.Add("Specialization", model.SpecializationOfThePost, DbType.Int32);
                dynamicParameters.Add("IpAddress", model.IpAddress, DbType.Int32);
                var res = _dapper.ExecuteGet<T>("ApplyForPost", dynamicParameters);
                return res;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public dynamic ListOfPostForApplying(string PostTypeId,long UserId)
        {
            postListPara req = new postListPara();
            ApplicantModel model = new ApplicantModel();

            using (SqlConnection objConnection = new SqlConnection(DapperDbContext.connect()))
            {
                try
                {
                    req.Id = UserId;
                    req.PostTypeId = PostTypeId;

                    var reader = objConnection.QueryMultiple("GetPostListToApply", req, commandType: System.Data.CommandType.StoredProcedure);
                    var list = reader.Read<ApplicantModel>().ToList();
                    var list1 = reader.Read<AppliedForm>().ToList();

                    model.list = list;
                    model.list1 = list1;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return model;
            }

        }

        public List<SelectListItem> BindState()
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("ProcId", 1, DbType.Int32);
            return _dapper.ExecuteGetAll<SelectListItem>("Proc_BindState_City", dynamicParameters);
        }

        public List<SelectListItem> BindCity(Int64 StateId)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("StateId", StateId, DbType.Int32);
            dynamicParameters.Add("ProcId", 9, DbType.Int32);
            return _dapper.ExecuteGetAll<SelectListItem>("Proc_BindState_City", dynamicParameters);
        }

        public T SavePersonalInfo<T>(Personalinfo model)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("Id", model.ID);
                dynamicParameters.Add("FirstName", model.FirstName);
                dynamicParameters.Add("MiddleName", model.MiddleName);
                dynamicParameters.Add("LastName", model.SurName);
                dynamicParameters.Add("BirthCity", model.City);
                dynamicParameters.Add("AlternateMobile", model.AlternateMobile);
                dynamicParameters.Add("Gender", model.Gender);
                dynamicParameters.Add("BirthState", model.State);
                dynamicParameters.Add("BirthCountry", model.Country);
                dynamicParameters.Add("FatherName", model.FatherName);
                dynamicParameters.Add("MotherName", model.MotherName);
                dynamicParameters.Add("Nationality", model.Nationality);
                dynamicParameters.Add("MaritalStatus", model.MaritalStatus);
                dynamicParameters.Add("PermanentAddress1", model.Address1);
                dynamicParameters.Add("PermanentAddress2", model.Address2);
                dynamicParameters.Add("PermanentStateId", model.StateId);
                dynamicParameters.Add("PermanentStateOther", model.otherState);
                dynamicParameters.Add("PermanentCityId", model.CityId);
                dynamicParameters.Add("PermanentCityOther", model.otherCity);
                dynamicParameters.Add("PermanentPinCode", model.Pincode);
                dynamicParameters.Add("SameMailingAddress", model.SameMailingAddress);
                dynamicParameters.Add("MailingAddress1", model.MAddress1);
                dynamicParameters.Add("MailingAddress2", model.MAddress2);
                dynamicParameters.Add("MailingStateId", model.MStateId);
                dynamicParameters.Add("MailingStateOther", model.SotherState);
                dynamicParameters.Add("MailingCityId", model.MCityId);
                dynamicParameters.Add("MailingCityOther", model.SotherCity);
                dynamicParameters.Add("MailingPinCode", model.MPincode);
                dynamicParameters.Add("HasBlindness") ;
                dynamicParameters.Add("BlindnessPercent");
                dynamicParameters.Add("BlindnessDocumentPath");
                dynamicParameters.Add("HasHearing");
                dynamicParameters.Add("HearingPercentage");
                dynamicParameters.Add("HearingDocumentPath");
                dynamicParameters.Add("HasLocomotor");
                dynamicParameters.Add("LocomotorPercentage");
                dynamicParameters.Add("LocomotorDocumentPath");
                dynamicParameters.Add("CategoryDocumentPath" ,"");
                dynamicParameters.Add("AadharDocumentPath", model.AdharImage);              

                var res = _dapper.ExecuteGet<T>("ManageApplicantPersonal", dynamicParameters);
                return res;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public T GetApplicantPersonalinfo<T>(int ID)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("Id", ID, DbType.Int32);
                return _dapper.Get<T>("Proc_GetApplicantPersonalinfo", dynamicParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
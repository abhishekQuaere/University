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

        public T saveAppliedForm<T>(saveAppliedForm model)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("Id", model.UserId, DbType.Int64);
                dynamicParameters.Add("PostCode", model.postCode, DbType.Int32);
                dynamicParameters.Add("ApplyingCategory", model.Category, DbType.String);
                dynamicParameters.Add("ApplyingSubCategory", model.SubCategory == null ? model.SubCategory = String.Empty : model.SubCategory, DbType.String);
                dynamicParameters.Add("Specialization", model.SpecializationOfThePost == null ? model.SpecializationOfThePost = String.Empty : model.SpecializationOfThePost, DbType.String);
                dynamicParameters.Add("IpAddress", model.IpAddress, DbType.String);
                var res = _dapper.ExecuteGet<T>("ApplyForPost", dynamicParameters);
                return res;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public dynamic ListOfPostForApplying(string PostTypeId, long UserId)
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

        public dynamic GetPostListForPayment(long Id)
        {
            AppliedForm model = new AppliedForm();

            using (SqlConnection objConnection = new SqlConnection(DapperDbContext.connect()))
            {
                try
                {
                    DynamicParameters parammeter = new DynamicParameters();
                    parammeter.Add("Id", Id);

                    var reader = objConnection.QueryMultiple("GetPostListForPayment", parammeter, commandType: System.Data.CommandType.StoredProcedure);
                    var list = reader.Read<AppliedForm>().ToList();
                    var list1 = reader.Read<FessPaid>().ToList();

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

        public ResearchGuidance SaveResearchGuidances(ResearchGuidance model)
        {
            var res = new ResearchGuidance();
            if (model.researchGuidances.Count() > 0)
            {
                foreach (var itm in model.researchGuidances)
                {
                    try
                    {
                        DynamicParameters dynamicParameters = new DynamicParameters();
                        dynamicParameters.Add("Id", model.Id, DbType.Int64);
                        dynamicParameters.Add("Degree", itm.Degree, DbType.String);
                        dynamicParameters.Add("Subject", itm.Subject, DbType.String);
                        dynamicParameters.Add("NoOfStudent", itm.NoOfStudents, DbType.String);
                        dynamicParameters.Add("ThesisSubmitted", itm.ThesisSubmitted, DbType.String);
                        dynamicParameters.Add("SubmissionDate", itm.SubmissionDate, DbType.String);
                        dynamicParameters.Add("DegreeAwarded", itm.DegreeAwarded, DbType.String);
                        dynamicParameters.Add("IpAddress", model.IpAddress, DbType.String);
                        dynamicParameters.Add("DocumentPath", itm.DocumentPath, DbType.String);
                        dynamicParameters.Add("AwardDate", itm.AwardDate, DbType.String);
                        res = _dapper.ExecuteGet<ResearchGuidance>("ManageApplicantResearchGuidance", dynamicParameters);
                       
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            return res;
        }

    }
}
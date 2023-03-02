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
                dynamicParameters.Add("ApplyingCategory", model.Category, DbType.String);
                dynamicParameters.Add("ApplyingSubCategory", model.SubCategory, DbType.String);
                dynamicParameters.Add("Specialization", model.SpecializationOfThePost, DbType.String);
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

    }
}
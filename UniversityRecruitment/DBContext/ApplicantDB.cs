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
                        dynamicParameters.Add("Degree", itm.Degree, DbType.Int32);
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

        public academicsDetails saveQualification(academicsDetails model)
        {

            academicsDetails obj = new academicsDetails();
            var reader = new academicsDetails();

            try
            {
                DynamicParameters perm = new DynamicParameters();
                if (model.lst.Count() > 0)
                {
                    for (int i = 0; i < model.lst.Count; i++)
                    {
                        perm.Add("@id", 1);
                        perm.Add("@Qualification", model.lst[i].qualification);
                        perm.Add("@CourseName", model.lst[i].nameOfCourse);
                        perm.Add("@Specialization", model.lst[i].specialization);
                        perm.Add("@BoardName", model.lst[i].nameofBoard);
                        perm.Add("@YearPassed", Convert.ToInt32(model.lst[i].yearPassed));
                        perm.Add("@CGPA", model.lst[i].cgpa);
                        perm.Add("@Division", model.lst[i].divison);
                        perm.Add("@PercentMarks", model.lst[i].perMarks);
                        perm.Add("@SubjectStudied", model.lst[i].subjectStudied);
                        perm.Add("@DocumentPath", model.lst[i].attachment);
                        perm.Add("@IpAddress", model.ip);
                        reader = _dapper.ExecuteGet<academicsDetails>("ManageApplicantQualification", perm);
                    }
                }

                return reader;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public academicsDetails saveugcDetails(academicsDetails model)
        {

            academicsDetails obj = new academicsDetails();
            var reader = new academicsDetails();

            try
            {
                DynamicParameters perm = new DynamicParameters();
                if (model.lst1.Count() > 0)
                {
                    for (int i = 0; i < model.lst1.Count; i++)
                    {
                        perm.Add("@id", model.UserId);
                        perm.Add("@Exam", model.lst1[i].exam);
                        perm.Add("@Subject", model.lst1[i].subject);
                        perm.Add("@RollNo", model.lst1[i].rollno);
                        perm.Add("@Year", model.lst1[i].year);
                        perm.Add("@DocumentPath", model.lst1[i].uDocument);

                        perm.Add("@IpAddress", model.ip);
                        reader = _dapper.ExecuteGet<academicsDetails>("ManageApplicantEntrance", perm);
                    }
                }

                return reader;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public LecturesModel saveLectures(LecturesModel model)
        {

            LecturesModel obj = new LecturesModel();
            var reader = new LecturesModel();

            try
            {
                DynamicParameters perm = new DynamicParameters();
                if (model.lst.Count() > 0)
                {
                    for (int i = 0; i < model.lst.Count; i++)
                    {
                        perm.Add("@id", model.UserId);
                        perm.Add("@Title", model.lst[i].titleLectures);
                        perm.Add("@LectureType", model.lst[i].type);
                        perm.Add("@Agency", model.lst[i].organizingBody);
                        perm.Add("@LectureLevel", model.lst[i].level);
                        perm.Add("@AcademicSession", model.lst[i].programDate);
                       
               
                       
                        perm.Add("@DocumentPath", model.lst[i].attachment);
                       
                        perm.Add("@IpAddress", model.ip);
                        reader = _dapper.ExecuteGet<LecturesModel>("[ManageApplicantInvitedLecture]", perm);
                    }
                }

                return reader;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }

}

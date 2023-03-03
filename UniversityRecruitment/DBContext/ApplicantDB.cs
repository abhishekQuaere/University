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
        public List<T> ListOfPostFeeDetails<T>(long Id)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("Id", Id, DbType.Int32);
                var res = _dapper.GetAll<T>("GetPaymentReceiptForMVC", dynamicParameters);
                return res;
            }
            catch (Exception ex)
            {
                throw;
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
                dynamicParameters.Add("HasBlindness");
                dynamicParameters.Add("BlindnessPercent");
                dynamicParameters.Add("BlindnessDocumentPath");
                dynamicParameters.Add("HasHearing");
                dynamicParameters.Add("HearingPercentage");
                dynamicParameters.Add("HearingDocumentPath");
                dynamicParameters.Add("HasLocomotor");
                dynamicParameters.Add("LocomotorPercentage");
                dynamicParameters.Add("LocomotorDocumentPath");
                dynamicParameters.Add("CategoryDocumentPath", "");
                dynamicParameters.Add("AadharDocumentPath", model.AdharImage);

                var res = _dapper.ExecuteGet<T>("ManageApplicantPersonal", dynamicParameters);
                return res;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public T SavePersonalDoc<T>(PersonalDoc model)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@ID", model.ID);
                dynamicParameters.Add("@Photo", model.Photo);
                dynamicParameters.Add("@Signature", model.Signature);  

                var res = _dapper.ExecuteGet<T>("Proc_UpdataImage_Signature", dynamicParameters);
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

        public Referee SaveReferee(Referee model)
        {
            var res = new Referee();
            if (model.referee.Count() > 0)
            {
                foreach (var itm in model.referee)
                {
                    try
                    {
                        DynamicParameters dynamicParameters = new DynamicParameters();
                        dynamicParameters.Add("Id", model.Id, DbType.Int64);
                        dynamicParameters.Add("Name", itm.refereeName, DbType.String);
                        dynamicParameters.Add("Address", itm.address, DbType.String);
                        dynamicParameters.Add("Email", itm.emailId, DbType.String);
                        dynamicParameters.Add("Mobile", itm.Mobile, DbType.String);
                        dynamicParameters.Add("Phone", itm.landline, DbType.String);
                        dynamicParameters.Add("Fax", itm.Fax, DbType.String);
                        dynamicParameters.Add("IpAddress", model.IpAddress, DbType.String);
                        res = _dapper.ExecuteGet<Referee>("ManageApplicantReferee", dynamicParameters);

                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            return res;
        }

        public Acceptance SaveAcceptance(Acceptance model)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("Id", model.Id, DbType.Int64);
                dynamicParameters.Add("Salary", model.Salary, DbType.Boolean);
                dynamicParameters.Add("SalaryDetail", model.SalaryDetail, DbType.String);
                dynamicParameters.Add("AcademicBreak", model.AcademicBreak, DbType.Boolean);
                dynamicParameters.Add("AcademicBreakDetail", model.AcademicBreakDetail, DbType.String);
                dynamicParameters.Add("Law", model.Law, DbType.Boolean);
                dynamicParameters.Add("LawDetail", model.LawDetail, DbType.String);
                dynamicParameters.Add("CaseFiled", model.CaseFiled, DbType.Boolean);
                dynamicParameters.Add("CaseDetail", model.CaseDetail, DbType.String);
                dynamicParameters.Add("OtherInformation", model.OtherInformation, DbType.String);
                dynamicParameters.Add("NocDocumentPath", model.NocDocumentPath, DbType.String);
                dynamicParameters.Add("EwsDocumentPath", model.EwsDocumentPath, DbType.String);
                dynamicParameters.Add("IpAddress", model.IpAddress, DbType.String);
                var res = _dapper.ExecuteGet<Acceptance>("ManageApplicantAcceptance", dynamicParameters);
                return res;
            }
            catch (Exception ex)
            {
                throw;
            }
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

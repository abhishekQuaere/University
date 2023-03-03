using UniversityRecruitment.DBContext;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityRecruitment.Models;
using System.Data.SqlClient;

namespace UniversityRecruitment.DBContext
{
    public class AccountDb
    {
        DapperDbContext _dapper = new DapperDbContext();

        public List<SelectListItem> StateList()
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("ProcId", 1, DbType.Int32);
                List<SelectListItem> _iresult = _dapper.GetAll<SelectListItem>("Proc_BindDropDowns", dynamicParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<SelectListItem> CityList(int StateId)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("ProcId", 2, DbType.Int32);
                dynamicParameters.Add("CommonId", StateId, DbType.Int32);
                List<SelectListItem> _iresult = _dapper.GetAll<SelectListItem>("Proc_BindDropDowns", dynamicParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public T RegistrationApplication<T>(Registration model)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("FirstName", model.FirstName, DbType.String);
                dynamicParameters.Add("MiddleName", model.MiddleName, DbType.String);
                dynamicParameters.Add("LastName", model.Surname, DbType.String);
                dynamicParameters.Add("FatherName", model.FatherName, DbType.String);
                dynamicParameters.Add("MotherName", model.MotherName, DbType.String);
                dynamicParameters.Add("Dob", model.DOB, DbType.String);
                dynamicParameters.Add("Aadhar", model.AddharNo, DbType.String);
                dynamicParameters.Add("Gender", model.Gender, DbType.String);
                dynamicParameters.Add("Category", model.Category, DbType.String);
                dynamicParameters.Add("PermanentAddress1", model.PermanentAddress1, DbType.String);
                dynamicParameters.Add("PermanentAddress2", model.PermanentAddress2, DbType.String);
                dynamicParameters.Add("PermanentStateId", model.PermanentStateId, DbType.String);
                dynamicParameters.Add("PermanentStateOther", model.PermanentStateOther, DbType.String);
                dynamicParameters.Add("PermanentCityId", model.PermanentCityId, DbType.String);
                dynamicParameters.Add("PermanentCityOther", model.PermanentCityOther, DbType.String);
                dynamicParameters.Add("PermanentPinCode", model.PinCode, DbType.String);
                dynamicParameters.Add("Email", model.Email, DbType.String);
                dynamicParameters.Add("Mobile", model.Mobile, DbType.String);
                dynamicParameters.Add("Password", model.Password, DbType.String);
                dynamicParameters.Add("IpAddress", model.Ip, DbType.String);
                var res = _dapper.ExecuteGet<T>("RegisterApplicant", dynamicParameters);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T CheckAuthorization<T>(Login model)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("LoginId", model.LoginId, DbType.String);
                dynamicParameters.Add("Password", model.Password, DbType.String);
                dynamicParameters.Add("IpAddress", model.IpAddress, DbType.String);
                dynamicParameters.Add("DeviceType", "M", DbType.String);
                var res = _dapper.ExecuteGet<T>("ValidateApplicantLogin", dynamicParameters);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T GetUserInformation<T>(int ApplicationId)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("ApplicationId", ApplicationId, DbType.Int32);
                var res = _dapper.Get<T>("Proc_GetUserInformation", dynamicParameters);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T InsertOtp<T>(string Otp, string Purpose, string EmailOrPhone, string MessageBody, int ApplicationId)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("Otp", Otp, DbType.String);
                dynamicParameters.Add("Purpose", Purpose, DbType.String);
                dynamicParameters.Add("EmailOrPhone", EmailOrPhone, DbType.String);
                dynamicParameters.Add("MessageBody", MessageBody, DbType.String);
                dynamicParameters.Add("ApplicationId", ApplicationId, DbType.String);
                var res = _dapper.Execute<T>("Proc_InsertOtp", dynamicParameters);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T ValidateOtp<T>(string Otp, int ApplicationId)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("Otp", Otp, DbType.String);
                dynamicParameters.Add("ApplicationId", ApplicationId, DbType.String);
                var res = _dapper.Execute<T>("Proc_ValidateOtp", dynamicParameters);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        #region Priyanshu

        public dynamic SaveDesignationDetails(Experience model)
        {

            Experience obj = new Experience();

            var res = new Experience();
            try
            {
                var perm = new DynamicParameters();
                if (model.Designationlist.Count() > 0)
                {
                    for (int i = 0; i < model.Designationlist.Count; i++)
                    {
                        perm.Add("@id", model.UserId);
                        perm.Add("@Designation", model.Designationlist[i].Designation);
                        perm.Add("@NatureOfPost", model.Designationlist[i].NatureofPost);
                        perm.Add("@Salary", model.Designationlist[i].GradeAgp);
                        perm.Add("@Employer", model.Designationlist[i].NameAddress);
                        perm.Add("@FromDate", model.Designationlist[i].DesignationPeriodFrom);
                        perm.Add("@ToDate", model.Designationlist[i].DesignationPeriodTo);
                        perm.Add("@DocumentPath", model.Designationlist[i].DesignationImage);
                        perm.Add("@IpAddress", model.Ipaddress);
                        res = _dapper.ExecuteGet<Experience>("ManageApplicantWorkExperience", perm);
                    }
                }
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }


        public dynamic saveAgencyDetails(Experience model)
        {

            Experience obj = new Experience();
            var res = new Experience();
            try
            {
                if (model.Agencylist.Count() > 0)
                {
                    var perm = new DynamicParameters();
                    for (int i = 0; i < model.Agencylist.Count; i++)
                    {
                        perm.Add("@id", model.UserId);
                        perm.Add("@Agency", model.Agencylist[i].Agency);
                        perm.Add("@HostInstitution", model.Agencylist[i].HostInstitution);
                        perm.Add("@FromDate", model.Agencylist[i].AgencyPeriodFrom);
                        perm.Add("@ToDate", model.Agencylist[i].AgencyPeriodTo);
                        perm.Add("@DocumentPath", model.Agencylist[i].AgencyImage);
                        perm.Add("@IpAddress", model.Ipaddress);
                        res = _dapper.ExecuteGet<Experience>("ManageApplicantPostDoctoral", perm);
                    }
                }
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public dynamic SaveAwardDetails(Award model)
        {
            Award obj = new Award();

            var res = new Award();
            try
            {
                var perm = new DynamicParameters();
                if (model.AwardList.Count() > 0)
                {
                    for (int i = 0; i < model.AwardList.Count; i++)
                    {
                        perm.Add("@id", model.UserId);
                        perm.Add("@AwardName", model.AwardList[i].Name);
                        perm.Add("@AwardDate", model.AwardList[i].DateOfAward);
                        perm.Add("@Organization", model.AwardList[i].Organization);
                        perm.Add("@AwardLevel", model.AwardList[i].Level);
                        perm.Add("@DocumentPath", model.AwardList[i].UploadAward); 
                        perm.Add("@IpAddress", model.Ipaddress);
                        res = _dapper.ExecuteGet<Award>("[ManageApplicantAwardFellowship]", perm);
                    }
                }
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic SaveResearchPaper(ResearchPaper model)
        {
            ResearchPaper obj = new ResearchPaper();

            var res = new ResearchPaper();
            try
            {
                var perm = new DynamicParameters();
                if (model.ResearchPaperList.Count() > 0)
                {
                    for (int i = 0; i < model.ResearchPaperList.Count; i++)
                    {
                        perm.Add("@Id", model.ID);
                        perm.Add("@PublicationName", model.ResearchPaperList[i].PublicationTitle);
                        perm.Add("@PublicationDate", model.ResearchPaperList[i].PublicationDate);
                        perm.Add("@Journal", model.ResearchPaperList[i].Journal);
                        perm.Add("@Isbn", model.ResearchPaperList[i].ISBN);
                        perm.Add("@PeerReviewed", model.ResearchPaperList[i].PeerReviewed);
                        perm.Add("@ImpactFactor", model.ResearchPaperList[i].ImpactFactor);
                        perm.Add("@NoOfCoAuthors", model.ResearchPaperList[i].NoOfAuther);
                        perm.Add("@IsMainAuthor", model.ResearchPaperList[i].IsMainAuthor);
                        perm.Add("@RefereedJournal", model.ResearchPaperList[i].ReferredJournal);
                        perm.Add("@ListedInUgc", model.ResearchPaperList[i].IsUGClist);
                        perm.Add("@UgcSerialNo", model.ResearchPaperList[i].UGCSerialNo);
                        perm.Add("@DocumentPath", model.ResearchPaperList[i].UploadAttachment);
                        perm.Add("@IpAddress", model.Ipaddress);
                        res = _dapper.ExecuteGet<ResearchPaper>("ManageApplicantResearchPublication", perm);
                    }
                }
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic SaveEditBook(Editedbooks model)
        {
            Editedbooks obj = new Editedbooks();

            var res = new Editedbooks();
            try
            {
                var perm = new DynamicParameters();
                if (model.EditedBookList.Count() > 0)
                {
                    for (int i = 0; i < model.EditedBookList.Count; i++)
                    {
                        perm.Add("@Id", model.Id);
                        perm.Add("@Category", model.EditedBookList[i].Category);
                        perm.Add("@Title", model.EditedBookList[i].Title);
                        perm.Add("@PublicationDate", model.EditedBookList[i].PublicationDate);
                        perm.Add("@Publisher", model.EditedBookList[i].PublisherName);
                        perm.Add("@PublisherType", model.EditedBookList[i].publisherType);
                        perm.Add("@Isbn", model.EditedBookList[i].ISBNNo);
                        perm.Add("@NoOfCoAuthors", model.EditedBookList[i].NoOfCoAuther);
                        perm.Add("@IsMainAuthor", model.EditedBookList[i].IsMainAuther);
                        perm.Add("@DocumentPath", model.EditedBookList[i].UploadAttachment);                       
                        perm.Add("@IpAddress", model.Ipaddress);
                        res = _dapper.ExecuteGet<Editedbooks>("ManageApplicantBookPublication", perm);
                    }
                }
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public dynamic SaveInformationDetails(Information model)
        {

            Information obj = new Information();

            var res = new Information();
            try
            {
                var perm = new DynamicParameters();
                if (model.InformationList.Count() > 0)
                {
                    for (int i = 0; i < model.InformationList.Count; i++)
                    {
                        perm.Add("@id", model.UserId);
                        perm.Add("@Detail", model.InformationList[i].Detail);
                        perm.Add("@DocumentPath", model.InformationList[i].UploadInformation); 
                        perm.Add("@IpAddress", model.Ipaddress);
                        res = _dapper.ExecuteGet<Information>("[ManageApplicantOtherInformation]", perm);
                    }
                }
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

        public dynamic SaveActivityDetails(Activities model)
        {

            Activities obj = new Activities();

            var res = new Activities();
            try
            {
                var perm = new DynamicParameters();
                if (model.AList.Count() > 0)
                {
                    for (int i = 0; i < model.AList.Count; i++)
                    {
                        perm.Add("@id", model.UserId);
                        perm.Add("@NatureofActivity", model.AList[i].ActivityId);
                        perm.Add("@Description", model.AList[i].Description);
                        perm.Add("@Institution", model.AList[i].Institution);
                        perm.Add("@AcademicYear", model.AList[i].AcademicYear);
                        perm.Add("@DocumentPath", model.AList[i].UploadActivity);
                        perm.Add("@IpAddress", model.Ipaddress);
                        res = _dapper.ExecuteGet<Activities>("[ManageApplicantActivity]", perm);
                    }
                }
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }


        public dynamic SaveMoocsDetails(Moocs model)
        {

            Moocs obj = new Moocs();

            var res = new Moocs();
            try
            {
                var perm = new DynamicParameters();
                if (model.MoocsList.Count() > 0)
                {
                    for (int i = 0; i < model.MoocsList.Count; i++)
                    {
                        perm.Add("@id", model.UserId);
                        perm.Add("@Mooc", model.MoocsList[i].LevelofMoocs);
                        perm.Add("@ShortDescription", model.MoocsList[i].Description);
                        perm.Add("@NoOfCreditCourse", model.MoocsList[i].NoOfCredit);
                        perm.Add("@NoOfModules", model.MoocsList[i].NoOfModule);
                        perm.Add("@NoOfQuadrant", 0);
                        perm.Add("@DocumentPath", model.MoocsList[i].UploadMoocs);
                        perm.Add("@IpAddress", model.Ipaddress);
                        res = _dapper.ExecuteGet<Moocs>("[ManageApplicantMooc]", perm);
                    }
                }
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }


        public dynamic SaveCurriculaDetails(DesignofNewCurricula model)
        {

            DesignofNewCurricula obj = new DesignofNewCurricula();

            var res = new DesignofNewCurricula();
            try
            {
                var perm = new DynamicParameters();
                if (model.DesignCurriculaList.Count() > 0)
                {
                    for (int i = 0; i < model.DesignCurriculaList.Count; i++)
                    {
                        perm.Add("@id", model.UserId);
                        perm.Add("@Title", model.DesignCurriculaList[i].Title);
                        perm.Add("@Level", model.DesignCurriculaList[i].Level);
                        perm.Add("@NoOfUnit", model.DesignCurriculaList[i].Courses);
                        perm.Add("@ApprovalOfAuthority", model.DesignCurriculaList[i].Authority); 
                        perm.Add("@DocumentPath", model.DesignCurriculaList[i].UploadDesignofCurricula);
                        perm.Add("@IpAddress", model.Ipaddress);
                        res = _dapper.ExecuteGet<DesignofNewCurricula>("[ManageApplicantCurricula]", perm);
                    }
                }
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }



        #endregion



    }
}
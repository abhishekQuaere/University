using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityRecruitment.DBContext;
using UniversityRecruitment.Filters;
using UniversityRecruitment.Models;
using UniversityRecruitment.Utilities;

namespace UniversityRecruitment.Controllers
{
    //[AuthorizeAdmin]
    public class ApplicantController : Controller
    {
        // GET: Applicant

        ApplicantDB apdb = new ApplicantDB();
        SessionManager sm = new SessionManager();
        Common com = new Common();
        DapperDbContext _dapper = new DapperDbContext();

        public ActionResult Index(string PostTypeId)
        {
            ApplicantModel model = new ApplicantModel();
            ViewBag.PostList = apdb.PostList();
            if (!String.IsNullOrEmpty(PostTypeId))
            {
                model = apdb.ListOfPostForApplying(PostTypeId, sm.userId);
            }
            else
            {
                model = apdb.ListOfPostForApplying("PROF", sm.userId);
            }
            return View(model);
        }

        public PartialViewResult BindPostList(string PostTypeId)
        {
            var res = new ApplicantModel();
            if (!String.IsNullOrEmpty(PostTypeId))
            {
                res = apdb.ListOfPostForApplying(PostTypeId, sm.userId);
            }
            else
            {
                res = apdb.ListOfPostForApplying("PROF", sm.userId);
            }
            return PartialView("_PostList", res);
        }

        [HttpPost]
        public JsonResult saveAppliedForm(saveAppliedForm model)
        {
            if (model != null)
            {
                model.UserId = sm.userId;
                model.IpAddress = Common.GetIPAddress();
                var result = apdb.saveAppliedForm<saveAppliedForm>(model);
                model.ResponseCode = result.ResponseCode;
                model.ResponseMessage = result.ResponseMessage;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult PersonalDetails(int? ID)
        {
            Personalinfo obj = new Personalinfo();
            obj.ID = Convert.ToInt32(ID);
            obj = apdb.GetApplicantPersonalinfo<Personalinfo>(Convert.ToInt32(ID));
            ViewBag.ddlstate = apdb.BindState();

            return View(obj);
        }
        [HttpPost]
        public JsonResult CityList(int StateId)
        {
            List<SelectListItem> ObjList = new List<SelectListItem>();
            ObjList = apdb.BindCity(StateId);
            return Json(ObjList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult PersonalDetails(Personalinfo obj)
        {
            Personalinfo model = new Personalinfo();
            ViewBag.ddlstate = apdb.BindState();
            model = apdb.SavePersonalInfo<Personalinfo>(obj);
            if (model.ResponseCode == 0)
            {
                TempData["Message"] = "Updated Succesfuly";
                return View(obj);
            }
            return View(obj);
        }

        public ActionResult UploadPhoto(int? ID)
        {
            PersonalDoc obj = new PersonalDoc();
            obj.ID = Convert.ToInt32(ID);
            obj = apdb.GetApplicantPhotoAndSign<PersonalDoc>(obj.ID);
            return View(obj);
        }
        [HttpPost]
        public ActionResult UploadPhoto(PersonalDoc obj)
        {
            PersonalDoc model = new PersonalDoc();
            model = apdb.SavePersonalDoc<PersonalDoc>(obj);
            if (model.ResponseCode == 0)
            {
                TempData["Message"] = model.message;
                return View(obj);
            }
            return View(obj);
        }
        public ActionResult AcademicDetails()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AcademicDetails(academicsDetails model)
        {
            ApplicantDB repo = new ApplicantDB();
            academicsDetails obj = new academicsDetails();
            model.ip = Common.GetIPAddress();
            model.UserId = sm.userId;
            if (model.lst != null && model.lst.Count() > 0)
            {

                obj = repo.saveQualification(model);
            }
            if (model.lst1 != null && model.lst1.Count() > 0)
            {
                obj = repo.saveugcDetails(model);
            }



            return Json(obj, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Experience()
        {
            Experience model = new Experience();
            return View(model);
        }

        public ActionResult Awards()
        {
            Award model = new Award();
            return View(model);
        }

        public ActionResult Lectures()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Lectures(LecturesModel model)
        {

            LecturesModel obj = new LecturesModel();
            ApplicantDB repo = new ApplicantDB();
            model.ip = Common.GetIPAddress();
            model.UserId = sm.userId;
            if (model.lst != null && model.lst.Count() > 0)
            {
                obj = repo.saveLectures(model);
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FeePayment()
        {
            AppliedForm model = new AppliedForm();
            model = apdb.GetPostListForPayment(sm.userId);
            return View(model);
        }

        public ActionResult FeeRecipt()
        {
            FeeDetails fd = new FeeDetails();
            fd.lst = apdb.ListOfPostFeeDetails<FeeDetails>(sm.userId);
            return View(fd);
        }

        public ActionResult ResearchDegree()
        {
            return View();
        }

        public JsonResult SaveResearchDegree(ResearchDegree model)
        {
            model.Id = sm.userId;
            model.IpAddress = Common.GetIPAddress();
            if (model.researchDegree.Count() > 0 && model.researchDegree != null)
            {
                model = apdb.saveResearchDegree(model);
            }
            else
            {
                model.ResponseMessage = "Something went wrong !";
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Activities()
        {
            Activities model = new Activities();
            // ViewBag.ActivitiesList = apdb.ActivitieList();
            return View(model);
        }

        public ActionResult Information()
        {
            Information model = new Information();
            return View(model);
        }

        public ActionResult Acceptance()
        {
            return View();
        }

        public JsonResult saveAcceptance(Referee model)
        {
            Acceptance obj = new Acceptance();
            model.Id = sm.userId;
            model.IpAddress = Common.GetIPAddress();
            obj.Salary = model.Salary;
            obj.SalaryDetail = model.SalaryDetail;
            obj.AcademicBreak = model.AcademicBreak;
            obj.AcademicBreakDetail = model.AcademicBreakDetail;
            obj.Law = model.Law;
            obj.LawDetail = model.LawDetail;
            obj.CaseFiled = model.CaseFiled;
            obj.CaseDetail = model.CaseDetail;
            obj.OtherInformation = model.OtherInformation;
            obj.NocDocumentPath = model.NocDocumentPath;
            obj.EwsDocumentPath = model.EwsDocumentPath;

            if (model.referee.Count() > 0 && model.referee != null)
            {
                model = apdb.SaveReferee(model);
                obj = apdb.SaveAcceptance(obj);
            }
            else
            {
                model.ResponseMessage = "Something went wrong !";
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ResearchGuidance()
        {
            return View();
        }

        public JsonResult SaveResearchGuidances(ResearchGuidance model)
        {
            model.Id = sm.userId;
            model.IpAddress = Common.GetIPAddress();
            if (model.researchGuidances.Count() > 0 && model.researchGuidances != null)
            {
                model = apdb.SaveResearchGuidances(model);
            }
            else
            {
                model.ResponseMessage = "Something went wrong !";
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UploadFile(HttpPostedFileBase File)
        {
            string Dirpath = "~/Content/writereaddata/ResearchGuidance/";
            string path = "";
            string filename = File.FileName;
            bool res = false;
            string msg = "";
            if (!Directory.Exists(Server.MapPath(Dirpath)))
            {
                Directory.CreateDirectory(Server.MapPath(Dirpath));
            }
            string ext = Path.GetExtension(File.FileName);
            var status = com.ValidateImagePDF_FileExtWithSize(File, 2048);
            if (status == "Valid")
            {

                filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_" + filename;
                string completepath = Path.Combine(Server.MapPath(Dirpath), filename);
                if (System.IO.File.Exists(completepath))
                {
                    System.IO.File.Delete(completepath);
                }

                File.SaveAs(completepath);
                path = Dirpath + filename;
                res = true;
            }
            else
            {
                msg = status;
            }
            return Json(new { result = res, fpath = path, mesg = msg });
        }
        public ActionResult ProjectConsultancy()
        {
            return View();
        }

        public ActionResult PaymentReceipt(string RefCode)
        {
            PersontalDetails model = new PersontalDetails();
            model = apdb.PaymentReciept(RefCode, sm.userId);
            return View(model);
        }

        public ActionResult ResearchPaper()
        {
            return View();
        }
        public JsonResult SaveResearchPaper(ResearchPaper model)
        {

            AccountDb repo = new AccountDb();
            ResearchPaper obj = new ResearchPaper();
            model.ID = Convert.ToInt32(sm.userId);
            model.Ipaddress = Common.GetIPAddress();
            obj = repo.SaveResearchPaper(model);
            return Json(obj, JsonRequestBehavior.AllowGet);

        }
        public JsonResult SaveEditedBook(Editedbooks model)
        {

            AccountDb repo = new AccountDb();
            Editedbooks obj = new Editedbooks();
            model.Id = Convert.ToInt32(sm.userId);
            model.Ipaddress = Common.GetIPAddress();
            obj = repo.SaveEditBook(model);
            return Json(obj, JsonRequestBehavior.AllowGet);

        }

        public ActionResult BookAuthored()
        {
            return View();
        }


        public ActionResult ChapterPublished()
        {
            return View();
        }

        public JsonResult SaveChapterPublished(BookPublication model)
        {
            model.Id = sm.userId;
            model.IpAddress = Common.GetIPAddress();
            if (model.bookPublications.Count() > 0 && model.bookPublications != null)
            {
                model = apdb.SaveChapterPublished(model);
            }
            else
            {
                model.ResponseMessage = "Something went wrong !";
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditedBooks()
        {
            return View();
        }

        public ActionResult ChapterTranslationWork()
        {
            return View();
        }

        public JsonResult SaveChapterTranslationWork(BookPublication model)
        {
            model.Id = sm.userId;
            model.IpAddress = Common.GetIPAddress();
            if (model.bookPublications.Count() > 0 && model.bookPublications != null)
            {
                model = apdb.SaveChapterTranslationWork(model);
            }
            else
            {
                model.ResponseMessage = "Something went wrong !";
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }


        public ActionResult BooksTranslationWork()
        {
            return View();
        }

        [HttpPost]
        public JsonResult BooksTranslationWork(bookTranslationWrokModel model)
        {
            ApplicantDB repo = new ApplicantDB();
            bookTranslationWrokModel obj = new bookTranslationWrokModel();
            if (model.lst != null && model.lst.Count() > 0)
            {
                obj = repo.saveBookTranslationWork(model);
            }

            return Json(obj, JsonRequestBehavior.AllowGet);
        }



        public ActionResult TeachingPedagogy()
        {
            return View();
        }
        [HttpPost]
        public JsonResult TeachingPedagogy(TeachingPedgogyModel model)
        {
            ApplicantDB repo = new ApplicantDB();
            TeachingPedgogyModel obj = new TeachingPedgogyModel();
            model.ip = Common.GetIPAddress();
            model.userId = sm.userId;
            if (model.lst != null && model.lst.Count() > 0)
            {
                obj = repo.saveTeachingPedgogy(model);
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }



        public ActionResult DesignofNewCurricula()
        {
            return View();
        }

        public ActionResult MOOCS()
        {
            Moocs model = new Moocs();
            return View(model);
        }


        public ActionResult EContent()
        {
            return View();
        }


        public JsonResult UploadFileAcceptance(HttpPostedFileBase File)
        {
            string Dirpath = "~/Content/writereaddata/Acceptance/";
            string path = "";
            string filename = File.FileName;
            bool res = false;
            string msg = "";
            if (!Directory.Exists(Server.MapPath(Dirpath)))
            {
                Directory.CreateDirectory(Server.MapPath(Dirpath));
            }
            string ext = Path.GetExtension(File.FileName);
            var status = com.ValidateImagePDF_FileExtWithSize(File, 2048);
            if (status == "Valid")
            {

                filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_" + filename;
                string completepath = Path.Combine(Server.MapPath(Dirpath), filename);
                if (System.IO.File.Exists(completepath))
                {
                    System.IO.File.Delete(completepath);
                }

                File.SaveAs(completepath);
                path = Dirpath + filename;
                res = true;
            }
            else
            {
                msg = status;
            }
            return Json(new { result = res, fpath = path, mesg = msg });
        }

        #region Priyanshu
        public JsonResult SaveExperienceDetails(Experience model)
        {

            AccountDb repo = new AccountDb();
            Experience obj = new Experience();
            model.UserId = sm.userId;
            model.Ipaddress = Common.GetIPAddress();
            if (model.Designationlist != null && model.Designationlist.Count() > 0)
            {
                obj = repo.SaveDesignationDetails(model);
            }
            if (model.Agencylist != null && model.Agencylist.Count() > 0)
            {
                obj = repo.saveAgencyDetails(model);
            }
            return Json(obj, JsonRequestBehavior.AllowGet);

        }


        public JsonResult SaveAwardDetails(Award model)
        {

            AccountDb repo = new AccountDb();
            Award obj = new Award();
            model.UserId = sm.userId;
            model.Ipaddress = Common.GetIPAddress();
            obj = repo.SaveAwardDetails(model);
            return Json(obj, JsonRequestBehavior.AllowGet);

        }

        public JsonResult SaveInformationDetails(Information model)
        {

            AccountDb repo = new AccountDb();
            Information obj = new Information();
            model.UserId = sm.userId;
            model.Ipaddress = Common.GetIPAddress();
            obj = repo.SaveInformationDetails(model);
            return Json(obj, JsonRequestBehavior.AllowGet);

        }

        public JsonResult SaveActivityDetails(Activities model)
        {

            AccountDb repo = new AccountDb();
            Activities obj = new Activities();
            model.UserId = sm.userId;
            model.Ipaddress = Common.GetIPAddress();
            obj = repo.SaveActivityDetails(model);
            return Json(obj, JsonRequestBehavior.AllowGet);

        }


        public JsonResult SaveMoocsDetails(Moocs model)
        {

            AccountDb repo = new AccountDb();
            Moocs obj = new Moocs();
            model.UserId = sm.userId;
            model.Ipaddress = Common.GetIPAddress();
            obj = repo.SaveMoocsDetails(model);
            return Json(obj, JsonRequestBehavior.AllowGet);

        }

        public JsonResult SaveCurriculaDetails(DesignofNewCurricula model)
        {

            AccountDb repo = new AccountDb();
            DesignofNewCurricula obj = new DesignofNewCurricula();
            model.UserId = sm.userId;
            model.Ipaddress = Common.GetIPAddress();
            obj = repo.SaveCurriculaDetails(model);
            return Json(obj, JsonRequestBehavior.AllowGet);

        }
        public JsonResult UploadExperienceFile(HttpPostedFileBase File)
        {
            string Dirpath = "~/Content/writereaddata/Experience/";
            string path = "";
            string filename = File.FileName;
            bool res = false;
            string msg = "";
            if (!Directory.Exists(Server.MapPath(Dirpath)))
            {
                Directory.CreateDirectory(Server.MapPath(Dirpath));
            }
            string ext = Path.GetExtension(File.FileName);
            var status = com.ValidateImagePDF_FileExtWithSize(File, 2048);
            if (status == "Valid")
            {

                filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_" + filename;
                string completepath = Path.Combine(Server.MapPath(Dirpath), filename);
                if (System.IO.File.Exists(completepath))
                {
                    System.IO.File.Delete(completepath);
                }

                File.SaveAs(completepath);
                path = Dirpath + filename;
                res = true;
            }
            else
            {
                msg = status;
            }
            return Json(new { result = res, fpath = path, mesg = msg });
        }


        public JsonResult UploadAwardFile(HttpPostedFileBase File)
        {
            string Dirpath = "~/Content/writereaddata/Award/";
            string path = "";
            string filename = File.FileName;
            bool res = false;
            string msg = "";
            if (!Directory.Exists(Server.MapPath(Dirpath)))
            {
                Directory.CreateDirectory(Server.MapPath(Dirpath));
            }
            string ext = Path.GetExtension(File.FileName);
            var status = com.ValidateImagePDF_FileExtWithSize(File, 2048);
            if (status == "Valid")
            {

                filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_" + filename;
                string completepath = Path.Combine(Server.MapPath(Dirpath), filename);
                if (System.IO.File.Exists(completepath))
                {
                    System.IO.File.Delete(completepath);
                }

                File.SaveAs(completepath);
                path = Dirpath + filename;
                res = true;
            }
            else
            {
                msg = status;
            }
            return Json(new { result = res, fpath = path, mesg = msg });
        }
        public JsonResult UploadResearchPaperFile(HttpPostedFileBase File)
        {
            string Dirpath = "~/Content/writereaddata/Researchpaper/";
            string path = "";
            string filename = File.FileName;
            bool res = false;
            string msg = "";
            if (!Directory.Exists(Server.MapPath(Dirpath)))
            {
                Directory.CreateDirectory(Server.MapPath(Dirpath));
            }
            string ext = Path.GetExtension(File.FileName);
            var status = com.ValidateImagePDF_FileExtWithSize(File, 2048);
            if (status == "Valid")
            {

                filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_" + filename;
                string completepath = Path.Combine(Server.MapPath(Dirpath), filename);
                if (System.IO.File.Exists(completepath))
                {
                    System.IO.File.Delete(completepath);
                }

                File.SaveAs(completepath);
                path = Dirpath + filename;
                res = true;
            }
            else
            {
                msg = status;
            }
            return Json(new { result = res, fpath = path, mesg = msg });
        }
        public JsonResult UploadEditedBookFile(HttpPostedFileBase File)
        {
            string Dirpath = "~/Content/writereaddata/EditedBook/";
            string path = "";
            string filename = File.FileName;
            bool res = false;
            string msg = "";
            if (!Directory.Exists(Server.MapPath(Dirpath)))
            {
                Directory.CreateDirectory(Server.MapPath(Dirpath));
            }
            string ext = Path.GetExtension(File.FileName);
            var status = com.ValidateImagePDF_FileExtWithSize(File, 2048);
            if (status == "Valid")
            {

                filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_" + filename;
                string completepath = Path.Combine(Server.MapPath(Dirpath), filename);
                if (System.IO.File.Exists(completepath))
                {
                    System.IO.File.Delete(completepath);
                }

                File.SaveAs(completepath);
                path = Dirpath + filename;
                res = true;
            }
            else
            {
                msg = status;
            }
            return Json(new { result = res, fpath = path, mesg = msg });
        }


        public JsonResult UploadMoocsFile(HttpPostedFileBase File)
        {
            string Dirpath = "~/Content/writereaddata/MOOCs/";
            string path = "";
            string filename = File.FileName;
            bool res = false;
            string msg = "";
            if (!Directory.Exists(Server.MapPath(Dirpath)))
            {
                Directory.CreateDirectory(Server.MapPath(Dirpath));
            }
            string ext = Path.GetExtension(File.FileName);
            var status = com.ValidateImagePDF_FileExtWithSize(File, 2048);
            if (status == "Valid")
            {

                filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_" + filename;
                string completepath = Path.Combine(Server.MapPath(Dirpath), filename);
                if (System.IO.File.Exists(completepath))
                {
                    System.IO.File.Delete(completepath);
                }

                File.SaveAs(completepath);
                path = Dirpath + filename;
                res = true;
            }
            else
            {
                msg = status;
            }
            return Json(new { result = res, fpath = path, mesg = msg });
        }

        public JsonResult UploadInformationFile(HttpPostedFileBase File)
        {
            string Dirpath = "~/Content/writereaddata/Information/";
            string path = "";
            string filename = File.FileName;
            bool res = false;
            string msg = "";
            if (!Directory.Exists(Server.MapPath(Dirpath)))
            {
                Directory.CreateDirectory(Server.MapPath(Dirpath));
            }
            string ext = Path.GetExtension(File.FileName);
            var status = com.ValidateImagePDF_FileExtWithSize(File, 2048);
            if (status == "Valid")
            {

                filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_" + filename;
                string completepath = Path.Combine(Server.MapPath(Dirpath), filename);
                if (System.IO.File.Exists(completepath))
                {
                    System.IO.File.Delete(completepath);
                }

                File.SaveAs(completepath);
                path = Dirpath + filename;
                res = true;
            }
            else
            {
                msg = status;
            }
            return Json(new { result = res, fpath = path, mesg = msg });
        }


        public JsonResult UploadAgencyFile(HttpPostedFileBase File)
        {
            string Dirpath = "~/Content/writereaddata/AgencyFile/";
            string path = "";
            string filename = File.FileName;
            bool res = false;
            string msg = "";
            if (!Directory.Exists(Server.MapPath(Dirpath)))
            {
                Directory.CreateDirectory(Server.MapPath(Dirpath));
            }
            string ext = Path.GetExtension(File.FileName);
            var status = com.ValidateImagePDF_FileExtWithSize(File, 2048);
            if (status == "Valid")
            {

                filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_" + filename;
                string completepath = Path.Combine(Server.MapPath(Dirpath), filename);
                if (System.IO.File.Exists(completepath))
                {
                    System.IO.File.Delete(completepath);
                }

                File.SaveAs(completepath);
                path = Dirpath + filename;
                res = true;
            }
            else
            {
                msg = status;
            }
            return Json(new { result = res, fpath = path, mesg = msg });
        }
        public JsonResult UploadActivityFile(HttpPostedFileBase File)
        {
            string Dirpath = "~/Content/writereaddata/Activity/";
            string path = "";
            string filename = File.FileName;
            bool res = false;
            string msg = "";
            if (!Directory.Exists(Server.MapPath(Dirpath)))
            {
                Directory.CreateDirectory(Server.MapPath(Dirpath));
            }
            string ext = Path.GetExtension(File.FileName);
            var status = com.ValidateImagePDF_FileExtWithSize(File, 2048);
            if (status == "Valid")
            {

                filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_" + filename;
                string completepath = Path.Combine(Server.MapPath(Dirpath), filename);
                if (System.IO.File.Exists(completepath))
                {
                    System.IO.File.Delete(completepath);
                }

                File.SaveAs(completepath);
                path = Dirpath + filename;
                res = true;
            }
            else
            {
                msg = status;
            }
            return Json(new { result = res, fpath = path, mesg = msg });



        }
        #endregion


        public JsonResult UploadFileForAcadMicDetails(HttpPostedFileBase File)
        {
            string Dirpath = "~/Content/writereaddata/Academic/";
            string path = "";
            string filename = File.FileName;
            bool res = false;
            string msg = "";
            if (!Directory.Exists(Server.MapPath(Dirpath)))
            {
                Directory.CreateDirectory(Server.MapPath(Dirpath));
            }
            string ext = Path.GetExtension(File.FileName);
            var status = com.ValidateImagePDF_FileExtWithSize(File, 2048);
            if (status == "Valid")
            {

                filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_" + filename;
                string completepath = Path.Combine(Server.MapPath(Dirpath), filename);
                if (System.IO.File.Exists(completepath))
                {
                    System.IO.File.Delete(completepath);
                }

                File.SaveAs(completepath);
                path = Dirpath + filename;
                res = true;
            }
            else
            {
                msg = status;
            }
            return Json(new { result = res, fpath = path, mesg = msg });



        }

        public JsonResult UploadFileForLectures(HttpPostedFileBase File)
        {
            string Dirpath = "~/Content/writereaddata/Lectures/";
            string path = "";
            string filename = File.FileName;
            bool res = false;
            string msg = "";
            if (!Directory.Exists(Server.MapPath(Dirpath)))
            {
                Directory.CreateDirectory(Server.MapPath(Dirpath));
            }
            string ext = Path.GetExtension(File.FileName);
            var status = com.ValidateImagePDF_FileExtWithSize(File, 2048);
            if (status == "Valid")
            {

                filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_" + filename;
                string completepath = Path.Combine(Server.MapPath(Dirpath), filename);
                if (System.IO.File.Exists(completepath))
                {
                    System.IO.File.Delete(completepath);
                }

                File.SaveAs(completepath);
                path = Dirpath + filename;
                res = true;
            }
            else
            {
                msg = status;
            }
            return Json(new { result = res, fpath = path, mesg = msg });



        }

        public JsonResult UploadFileResearchDegree(HttpPostedFileBase File)
        {
            string Dirpath = "~/Content/writereaddata/ResearchDegree/";
            string path = "";
            string filename = File.FileName;
            bool res = false;
            string msg = "";
            if (!Directory.Exists(Server.MapPath(Dirpath)))
            {
                Directory.CreateDirectory(Server.MapPath(Dirpath));
            }
            string ext = Path.GetExtension(File.FileName);
            var status = com.ValidateImagePDF_FileExtWithSize(File, 2048);
            if (status == "Valid")
            {

                filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_" + filename;
                string completepath = Path.Combine(Server.MapPath(Dirpath), filename);
                if (System.IO.File.Exists(completepath))
                {
                    System.IO.File.Delete(completepath);
                }

                File.SaveAs(completepath);
                path = Dirpath + filename;
                res = true;
            }
            else
            {
                msg = status;
            }
            return Json(new { result = res, fpath = path, mesg = msg });
        }

        public JsonResult UploadCurriculFile(HttpPostedFileBase File)
        {
            string Dirpath = "~/Content/writereaddata/Curricula/";
            string path = "";
            string filename = File.FileName;
            bool res = false;
            string msg = "";
            if (!Directory.Exists(Server.MapPath(Dirpath)))
            {
                Directory.CreateDirectory(Server.MapPath(Dirpath));
            }
            string ext = Path.GetExtension(File.FileName);
            var status = com.ValidateImagePDF_FileExtWithSize(File, 2048);
            if (status == "Valid")
            {

                filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_" + filename;
                string completepath = Path.Combine(Server.MapPath(Dirpath), filename);
                if (System.IO.File.Exists(completepath))
                {
                    System.IO.File.Delete(completepath);
                }

                File.SaveAs(completepath);
                path = Dirpath + filename;
                res = true;
            }
            else
            {
                msg = status;
            }
            return Json(new { result = res, fpath = path, mesg = msg });
        }

        public JsonResult UploadFileChapterPublished(HttpPostedFileBase File)
        {
            string Dirpath = "~/Content/writereaddata/ChapterPublished/";
            string path = "";
            string filename = File.FileName;
            bool res = false;
            string msg = "";
            if (!Directory.Exists(Server.MapPath(Dirpath)))
            {
                Directory.CreateDirectory(Server.MapPath(Dirpath));
            }
            string ext = Path.GetExtension(File.FileName);
            var status = com.ValidateImagePDF_FileExtWithSize(File, 2048);
            if (status == "Valid")
            {

                filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_" + filename;
                string completepath = Path.Combine(Server.MapPath(Dirpath), filename);
                if (System.IO.File.Exists(completepath))
                {
                    System.IO.File.Delete(completepath);
                }

                File.SaveAs(completepath);
                path = Dirpath + filename;
                res = true;
            }
            else
            {
                msg = status;
            }
            return Json(new { result = res, fpath = path, mesg = msg });
        }

        public JsonResult UploadFileChapterTranslationWork(HttpPostedFileBase File)
        {
            string Dirpath = "~/Content/writereaddata/ChapterTranslationWork/";
            string path = "";
            string filename = File.FileName;
            bool res = false;
            string msg = "";
            if (!Directory.Exists(Server.MapPath(Dirpath)))
            {
                Directory.CreateDirectory(Server.MapPath(Dirpath));
            }
            string ext = Path.GetExtension(File.FileName);
            var status = com.ValidateImagePDF_FileExtWithSize(File, 2048);
            if (status == "Valid")
            {

                filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_" + filename;
                string completepath = Path.Combine(Server.MapPath(Dirpath), filename);
                if (System.IO.File.Exists(completepath))
                {
                    System.IO.File.Delete(completepath);
                }

                File.SaveAs(completepath);
                path = Dirpath + filename;
                res = true;
            }
            else
            {
                msg = status;
            }
            return Json(new { result = res, fpath = path, mesg = msg });
        }
        public JsonResult UploadFileTeachingPedagogy(HttpPostedFileBase File)
        {
            string Dirpath = "~/Content/writereaddata/TeachingPedagogy/";
            string path = "";
            string filename = File.FileName;
            bool res = false;
            string msg = "";
            if (!Directory.Exists(Server.MapPath(Dirpath)))
            {
                Directory.CreateDirectory(Server.MapPath(Dirpath));
            }
            string ext = Path.GetExtension(File.FileName);
            var status = com.ValidateImagePDF_FileExtWithSize(File, 2048);
            if (status == "Valid")
            {

                filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_" + filename;
                string completepath = Path.Combine(Server.MapPath(Dirpath), filename);
                if (System.IO.File.Exists(completepath))
                {
                    System.IO.File.Delete(completepath);
                }

                File.SaveAs(completepath);
                path = Dirpath + filename;
                res = true;
            }
            else
            {
                msg = status;
            }
            return Json(new { result = res, fpath = path, mesg = msg });
        }
        public JsonResult UploadFileBookTranslationEork(HttpPostedFileBase File)
        {
            string Dirpath = "~/Content/writereaddata/BookTranslationEork/";
            string path = "";
            string filename = File.FileName;
            bool res = false;
            string msg = "";
            if (!Directory.Exists(Server.MapPath(Dirpath)))
            {
                Directory.CreateDirectory(Server.MapPath(Dirpath));
            }
            string ext = Path.GetExtension(File.FileName);
            var status = com.ValidateImagePDF_FileExtWithSize(File, 2048);
            if (status == "Valid")
            {

                filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_" + filename;
                string completepath = Path.Combine(Server.MapPath(Dirpath), filename);
                if (System.IO.File.Exists(completepath))
                {
                    System.IO.File.Delete(completepath);
                }

                File.SaveAs(completepath);
                path = Dirpath + filename;
                res = true;
            }
            else
            {
                msg = status;
            }
            return Json(new { result = res, fpath = path, mesg = msg });
        }

        [HttpPost]
        public JsonResult BookAuthored(bookAuthoredModel model)
        {
            ApplicantDB repo = new ApplicantDB();
            bookAuthoredModel obj = new bookAuthoredModel();
            model.ip = Common.GetIPAddress();
            model.userId = sm.userId;
            if (model.lst != null && model.lst.Count() > 0)
            {
                obj = repo.saveBookAuthor(model);
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}
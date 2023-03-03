﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityRecruitment.DBContext;
using UniversityRecruitment.Models;
using UniversityRecruitment.Utilities;

namespace UniversityRecruitment.Controllers
{
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
        public ActionResult PersonalDetails()
        {
            Personalinfo obj = new Personalinfo();
            return View(obj);
        }
        [HttpPost]
        public ActionResult PersonalDetails(Personalinfo obj)
        {
            return View();
        }

        public ActionResult UploadPhoto()
        {
            return View();
        }
        public ActionResult AcademicDetails()
        {
            return View();
        }

        public ActionResult Experience()
        {
            return View();
        }

        public ActionResult Awards()
        {
            return View();
        }

        public ActionResult Lectures()
        {
            return View();
        }

        public ActionResult FeePayment()
        {
            AppliedForm model = new AppliedForm();
            model = apdb.GetPostListForPayment(sm.userId);
            return View(model);
        }

        public ActionResult FeeRecipt()
        {
            return View();
        }

        public ActionResult ResearchDegree()
        {
            return View();
        }

        public ActionResult Activities()
        {
            return View();
        }

        public ActionResult Information()
        {
            return View();
        }

        public ActionResult Acceptance()
        {
            return View();
        }

        public ActionResult ResearchGuidance()
        {
            return View();
        }
        public JsonResult SaveResearchGuidances(ResearchGuidance model)
        {
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
            return Json(obj,JsonRequestBehavior.AllowGet);

        }


        public JsonResult SaveAwardDetails(Award model)
        {

            AccountDb repo = new AccountDb();
            Award obj = new Award();
            model.UserId = sm.userId;
            model.Ipaddress = Common.GetIPAddress(); 
               obj = repo.SaveAwardDetails(model);  
            return Json(obj,JsonRequestBehavior.AllowGet);

        }

        public JsonResult SaveInformationDetails(Information model)
        {

            AccountDb repo = new AccountDb();
            Information obj = new Information();
            model.UserId = sm.userId;
            model.Ipaddress = Common.GetIPAddress();
            obj = repo.SaveInformationDetails(model);
            return Json(obj,JsonRequestBehavior.AllowGet);

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

        #endregion


    }
}
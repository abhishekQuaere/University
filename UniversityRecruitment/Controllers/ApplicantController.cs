﻿using System;
using System.Collections.Generic;
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

        public ActionResult Index(string  PostTypeId)
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
                res = apdb.ListOfPostForApplying("PROF",sm.userId);
            }
            return PartialView("_PostList", res);
        }

        [HttpPost]
        public JsonResult saveAppliedForm(saveAppliedForm model)
        {
            if (model != null)
            {
                model.UserId = sm.userId;
                var result = apdb.saveAppliedForm<saveAppliedForm>(model);
                model.msg = result.msg;
            }           
            return Json(model, JsonRequestBehavior.AllowGet);
        }

       [HttpGet]
        public ActionResult PersonalDetails( int ? ID)
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
            if(model.ResponseCode == 0)
            {
                TempData["Message"] = "Updated Succesfuly" ;
                return View();
            }
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
            return View();
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



    }
}
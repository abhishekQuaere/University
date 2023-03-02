using System;
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

        public ActionResult Index()
        {
            ApplicantModel model = new ApplicantModel();
            ViewBag.PostList = apdb.PostList();
            return View(model);
        }

        public PartialViewResult BindPostList(int postId)
        {
            ApplicantModel model = new ApplicantModel();
            if (postId != 0)
            {
                model.list = apdb.ListOfPostForApplying<ApplicantModel>(postId);
            }
            else
            {
                model.list = apdb.ListOfPostForApplying<ApplicantModel>(1);
            }
            return PartialView("_PostList", model);
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

       
        public ActionResult PersonalDetails()
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

        public ActionResult Information()
        {
            return View();
        }

        public ActionResult Acceptance()
        {
            return View();
        }




    }
}
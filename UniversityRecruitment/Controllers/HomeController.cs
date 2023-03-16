using SRVTextToImage;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using UniversityRecruitment.DBContext;
using UniversityRecruitment.Models;
using UniversityRecruitment.Utilities;

namespace UniversityRecruitment.Controllers
{
    public class HomeController : Controller
    {

        AccountDb acdb = new AccountDb();
        SessionManager sm = new SessionManager();

        #region CreateResponse
        /// <summary>
        /// Creates a successfull response with redirection and default MessageStream -> MessageStream.RecordUpdatedSuccessfully .
        /// </summary>
        /// <param name="Action"></param>
        /// <param name="Controller"></param>
        //private void CreateResponse(string Action, string Controller)
        //{
        //    ViewBag.ResponseURL = Url.Action(Action, Controller);
        //    ViewBag.ResponseMessage = MessageStream.RecordUpdatedSuccessfully;
        //    ViewBag.ResponseType = ResponseType.Success;
        //}

        /// <summary>
        /// Creates a successfull response with redirection and message.
        /// </summary>
        /// <param name="Action"></param>
        /// <param name="Controller"></param>
        /// <param name="Message"></param>
        private void CreateResponse(string Action, string Controller, string Message)
        {
            ViewBag.ResponseURL = Url.Action(Action, Controller);
            ViewBag.ResponseMessage = Message;
            ViewBag.ResponseType = ResponseType.Success;
        }

        /// <summary>
        /// Creates a response with customized parameters. Keep action and controller empty / blank if redirection is not required.
        /// </summary>
        /// <param name="Action"></param>
        /// <param name="Controller"></param>
        /// <param name="Message"></param>
        /// <param name="ResponseType_success_warning_error_OR_info"></param>
        private void CreateResponse(string Action, string Controller, string Message, string ResponseType_success_warning_error_OR_info, bool UseTempData = false, string ResponseTitle = "")
        {
            if (!string.IsNullOrEmpty(Action))
            {
                ViewBag.ResponseURL = Url.Action(Action, Controller);
                if (UseTempData)
                    TempData["ResponseURL"] = Url.Action(Action, Controller);
            }

            ViewBag.ResponseMessage = Message;
            ViewBag.ResponseType = ResponseType_success_warning_error_OR_info;
            if (!string.IsNullOrEmpty(ResponseTitle)) ViewBag.ResponseTitle = ResponseTitle;

            if (UseTempData)
            {
                TempData["ResponseMessage"] = Message;
                TempData["ResponseType"] = ResponseType_success_warning_error_OR_info;
            }
        }

        private void FlushCreateResponse()
        {
            TempData.Remove("ResponseURL");
            TempData.Remove("ResponseMessage");
            TempData.Remove("ResponseType");
        }
        #endregion

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public FileResult GetCaptchaimage()
        {
            CaptchaRandomImage ci = new CaptchaRandomImage();
            this.Session["capimagetext"] = ci.GetRandomString(5).ToUpper();
            ci.GenerateImage(this.Session["capimagetext"].ToString(), 150, 40, Color.Black, Color.White);
            MemoryStream stream = new MemoryStream();
            ci.Image.Save(stream, ImageFormat.Png);
            stream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(stream, "image/png");
        }

        public ActionResult Registration()
        {
            Registration model = new Registration();
            ViewBag.StateList = acdb.StateList();
            return View(model);
        }

        [HttpPost]
        public JsonResult BindCityList(int StateId)
        {
            List<SelectListItem> ObjList = new List<SelectListItem>();
            ObjList = acdb.CityList(StateId);
            return Json(ObjList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Registration(Registration model)
        {
            Registration obj = new Registration();

            ViewBag.StateList = acdb.StateList();
            if (Convert.ToString(Session["capimagetext"]) == model.Captcha)
            {
                if (ModelState.IsValid)
                {
                    Random rnd = new Random();
                    string otp = (rnd.Next(100000, 999999)).ToString();
                    model.Ip = Common.GetIPAddress();
                    obj = acdb.RegistrationApplication<Registration>(model);
                    if (obj.ResponseCode != 0)
                    {
                        CreateResponse("", "", obj.ResponseMessage, ResponseType.Success);
                    }
                    else
                    {
                        Session["ApplicationId"] = obj.Id;
                        Session["Mobile"] = obj.Mobile;
                        Session["EmailId"] = obj.Email;
                        string Message2 = SMS.otpMessageForRegistration(obj.Name, otp);
                        string mobile = obj.Mobile;
                        string status = SMS.SendSMS(mobile, Message2, ConfigurationManager.AppSettings["TEMP-Examotp"].ToString());
                        if (status == "OK")
                        {
                            int a = acdb.InsertOtp<int>(otp, "Application Registration", "Mobile", Message2, obj.Id);
                        }
                        string response = sendEmailOTP(obj.Id, obj.Email);
                        CreateResponse("EmailAndMobileVerification", "Home", "Please verify Phone No. & Email Id through OTP.", ResponseType.Success);
                    }
                }
                else
                {
                    CreateResponse("", "", MessageStream.AllFieldsMandatory, ResponseType.Info);
                }
            }
            else
            {
                CreateResponse("", "", MessageStream.InvalidCaptcha, ResponseType.Error);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult EmailAndMobileVerification()
        {
            EmailAndMobile obj = new EmailAndMobile();
            obj.EmailId = Convert.ToString(Session["EmailId"]);
            obj.Mobile = Convert.ToString(Session["Mobile"]);
            obj.ApplicationId = Convert.ToInt32(Session["ApplicationId"]);
            return View(obj);
        }

        public ActionResult Login()
        {
            Login model = new Login();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(Login model)
        {
            Login obj = new Login();
            if (ModelState.IsValid)
            {
                model.IpAddress = Common.GetIPAddress();
                obj = acdb.CheckAuthorization<Login>(model);
                if (obj.ResponseCode == 0)
                {
                    sm.userName = obj.Name;
                    sm.EmailId = obj.Email;
                    sm.EmailVerified = obj.EmailVerified;
                    sm.MobileVerified = obj.MobileVerified;
                    sm.Mobile = obj.Mobile;
                    sm.Password = obj.Password;
                    sm.userId = obj.Id;
                    return RedirectToAction("Index", "Applicant");
                }
                else
                {
                    CreateResponse("", "", obj.ResponseMessage, ResponseType.Error);
                }
            }
            else
            {
                CreateResponse("", "", MessageStream.AllFieldsMandatory, ResponseType.Info);

            }
            return View(model);
        }

        public string sendEmailOTP(int ApplicationId, string Email)
        {
            Random rnd = new Random();
            string Name = String.Empty;
            string otp = (rnd.Next(100000, 999999)).ToString();
            if (ApplicationId != 0)
            {
                Name = acdb.GetUserInformation<String>(ApplicationId);
                sendEmail(Email, Name, otp, ApplicationId);
                return "success";
            }
            else
            {
                return "error";
            }

        }

        public void sendEmail(string email, string Name, string Otp, int ApplicationId)
        {
            string MessageBody = getMessageBody(Name, Otp);
            string subject = "New Registration";
            Mail.SendEmailMessag(email, subject, MessageBody);
            int a = acdb.InsertOtp<int>(Otp, "Application Registration", "Email", MessageBody, ApplicationId);
        }

        public String getMessageBody(string Name, string Otp)
        {
            StreamReader rd = new StreamReader(HostingEnvironment.MapPath(@"~/EmailTemplates/EmailRemplates.html"));
            string msgBody = rd.ReadToEnd();
            msgBody = msgBody.Replace("[User]", Name);
            msgBody = msgBody.Replace("[OTP]", Otp);
            return msgBody;
        }

        public JsonResult validateEmailOtp(int ApplicationId, string Otp)
        {
            string response = String.Empty;
            if (ApplicationId != 0 && !String.IsNullOrEmpty(Otp))
            {
                response = acdb.ValidateOtp<String>(Otp, ApplicationId);
                if (response == "success")
                {
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Incorrect Otp", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }           
        }
        
        public JsonResult validateMobileOtp(int ApplicationId, string Otp)
        {
            string response = String.Empty;
            if (ApplicationId != 0 && !String.IsNullOrEmpty(Otp))
            {
                response = acdb.ValidateOtp<String>(Otp, ApplicationId);
                if (response == "success")
                {
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Incorrect Otp", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }           
        }


        public ActionResult changePassword(changePasword model)
        {

            return View(model);
        }

        public JsonResult CheckPassword(string oldpassword)
        {
            changePasword model = new changePasword();
            model.userId = sm.userId;
            model.oldPassword = oldpassword;
            var res = acdb.chekPassword(model);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult changePass(string newPassword)
        {
            changePasword model = new changePasword();
            model.userId = sm.userId;
            model.newPassword = newPassword;
            var res = acdb.changePassword(model);
            return Json(res, JsonRequestBehavior.AllowGet);
        }




    }
}
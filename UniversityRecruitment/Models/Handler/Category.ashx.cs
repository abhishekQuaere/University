using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
namespace AyushastraShopping.Models.Handler
{
    /// <summary>
    /// Summary description for category
    /// </summary>
    public class category : IHttpHandler
    {

        /// <summary>
        /// Summary description for category
        /// </summary>
        public static string GalleryImgPath { get; set; }
        protected Commons Obj = new Commons();


        public void ProcessRequest(HttpContext context)
        {
            try
            {

                if (context.Request.Files.Count > 0)
                {
                    HttpFileCollection files = context.Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile file = files[i];
                        string fname;
                        string flname = "";
                        if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE" || HttpContext.Current.Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            flname = Obj.GetUniqueFileName(file.FileName);
                            fname = flname;
                        }
                        GalleryImgPath = Path.Combine(context.Server.MapPath("~/assets/FileUpload/attribute/"), fname);
                        file.SaveAs(GalleryImgPath);
                        context.Response.ContentType = "text/plain";
                        context.Response.Write("~/assets/FileUpload/attribute/" + flname);

                    }
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("Error: " + ex.Message);
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        //public void ProcessRequest(HttpContext context)
        //    {
        //        try
        //        {
        //            string fn = "";
        //            if (context.Request.Files.Count > 0)
        //            {
        //                HttpFileCollection files = context.Request.Files;
        //                string name = Path.GetFileName(context.Request.Files.AllKeys[0]);
        //                string FileText = Path.GetExtension(name);
        //                if (FileText.ToLower() == ".jpg" || FileText.ToLower() == ".png" || FileText.ToLower() == ".jpeg")
        //                {
        //                    for (int i = 0; i < files.Count; i++)
        //                    {
        //                        HttpPostedFile file = files[i];
        //                        string fname;
        //                        string flname = "";
        //                        if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE" || HttpContext.Current.Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
        //                        {
        //                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
        //                            fname = testfiles[testfiles.Length - 1];
        //                        }
        //                        else
        //                        {
        //                            flname = Obj.GetUniqueFileName(file.FileName);
        //                            fname = flname;
        //                        }
        //                        fname = Path.Combine(context.Server.MapPath("/assets/FileUpload/attribute/"), fname);
        //                        file.SaveAs(fname);
        //                        context.Response.ContentType = "text/plain";
        //                        //context.Response.Write("https://localhost:44304/assets/FileUpload/Category/" + flname);
        //                        string path = Obj.getImagePath();
        //                        context.Response.Write(path + "attribute/" + flname);
        //                    }
        //                }

        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            context.Response.Write("Error: " + ex.Message);
        //        }
        //    }

        //    public bool IsReusable
        //    {
        //        get
        //        {
        //            return false;
        //        }
        //    }
    }
    }



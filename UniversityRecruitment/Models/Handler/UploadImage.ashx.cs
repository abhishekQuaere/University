using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace AyushastraShopping.Models.Handler
{
    /// <summary>
    /// Summary description for UploadImage
    /// </summary>
    public class UploadImage : IHttpHandler
    {
        protected Commons Obj = new Commons();
        public void ProcessRequest(HttpContext context)
        {
            {
               
                if (context.Request.Files.Count > 0)
                {
                    HttpFileCollection files = context.Request.Files;
                    string name = Path.GetFileName(context.Request.Files.AllKeys[0]);
                    string FileText = Path.GetExtension(name);
                    if (FileText.ToLower() == ".png" || FileText.ToLower() == ".jpg" || FileText.ToLower() == ".jpeg")
                    {
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
                            fname = Path.Combine(context.Server.MapPath("/Content/images/users/UploadImage/"), fname);
                            file.SaveAs(fname);
                            context.Response.ContentType = "text/plain";
                            string path = Obj.getImagePath();
                            context.Response.Write(path + "/Content/images/users/UploadImage/" + flname);
                        }
                    }
                }
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
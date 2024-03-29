﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AyushastraShopping.Models.Handlers
{
    /// <summary>
    /// Summary description for DoctorProfile
    /// </summary>
    public class DoctorProfile : IHttpHandler
    {

        protected Commons Obj = new Commons();
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string fn = "";
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
                        fname = Path.Combine(context.Server.MapPath("/ReadWriteData/DoctorProfile/"), fname);
                        file.SaveAs(fname);


                        context.Response.ContentType = "text/plain";
                        string path = Obj.getDoctorFilePath();
                        context.Response.Write(path + "DoctorProfile/" + flname);

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
    }
}
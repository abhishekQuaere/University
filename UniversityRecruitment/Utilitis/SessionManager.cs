using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityRecruitment.Utilities
{
    public class SessionManager
    {
        public void RemoveSession()
        {
            try
            {
                HttpContext.Current.Session.Clear();
                HttpContext.Current.Session.Abandon();
                HttpContext.Current.Session.RemoveAll();
            }
            catch
            { }
        }

        public string userName
        {
            get
            {
                if (HttpContext.Current.Session["userName"] != null)
                {
                    return HttpContext.Current.Session["userName"].ToString();
                }
                else
                {
                    return null;
                }
            }
            set
            {
                HttpContext.Current.Session["userName"] = value;
            }
        }

        public Int64 userId
        {
            get
            {
                if (HttpContext.Current.Session["userId"] != null)
                {
                    return Convert.ToInt64(HttpContext.Current.Session["userId"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                HttpContext.Current.Session["userId"] = value;
            }
        }

        public string FirstName
        {
            get
            {
                if (HttpContext.Current.Session["FirstName"] != null)
                {
                    return Convert.ToString(HttpContext.Current.Session["FirstName"].ToString());
                }
                else
                {
                    return "";
                }
            }
            set
            {
                HttpContext.Current.Session["FirstName"] = value;
            }
        }

        public string MiddleName
        {
            get
            {
                if (HttpContext.Current.Session["MiddleName"] != null)
                {
                    return Convert.ToString(HttpContext.Current.Session["MiddleName"].ToString());
                }
                else
                {
                    return "";
                }
            }
            set
            {
                HttpContext.Current.Session["MiddleName"] = value;
            }
        }

        public string Surname
        {
            get
            {
                if (HttpContext.Current.Session["Surname"] != null)
                {
                    return Convert.ToString(HttpContext.Current.Session["Surname"].ToString());
                }
                else
                {
                    return "";
                }
            }
            set
            {
                HttpContext.Current.Session["Surname"] = value;
            }
        }

        public string FatherName
        {
            get
            {
                if (HttpContext.Current.Session["FatherName"] != null)
                {
                    return Convert.ToString(HttpContext.Current.Session["FatherName"].ToString());
                }
                else
                {
                    return "";
                }
            }
            set
            {
                HttpContext.Current.Session["FatherName"] = value;
            }
        }

        public string MotherName
        {
            get
            {
                if (HttpContext.Current.Session["MotherName"] != null)
                {
                    return Convert.ToString(HttpContext.Current.Session["MotherName"].ToString());
                }
                else
                {
                    return "";
                }
            }
            set
            {
                HttpContext.Current.Session["MotherName"] = value;
            }
        }

        public string DOB
        {
            get
            {
                if (HttpContext.Current.Session["DOB"] != null)
                {
                    return Convert.ToString(HttpContext.Current.Session["DOB"].ToString());
                }
                else
                {
                    return "";
                }
            }
            set
            {
                HttpContext.Current.Session["DOB"] = value;
            }
        }

        public string AddharNo
        {
            get
            {
                if (HttpContext.Current.Session["AddharNo"] != null)
                {
                    return Convert.ToString(HttpContext.Current.Session["AddharNo"].ToString());
                }
                else
                {
                    return "";
                }
            }
            set
            {
                HttpContext.Current.Session["AddharNo"] = value;
            }
        }

        public string Gender
        {
            get
            {
                if (HttpContext.Current.Session["Gender"] != null)
                {
                    return Convert.ToString(HttpContext.Current.Session["Gender"].ToString());
                }
                else
                {
                    return "";
                }
            }
            set
            {
                HttpContext.Current.Session["Gender"] = value;
            }
        }

        public string Category
        {
            get
            {
                if (HttpContext.Current.Session["Category"] != null)
                {
                    return Convert.ToString(HttpContext.Current.Session["Category"].ToString());
                }
                else
                {
                    return "";
                }
            }
            set
            {
                HttpContext.Current.Session["Category"] = value;
            }
        }

        public string PermanentAddress1
        {
            get
            {
                if (HttpContext.Current.Session["PermanentAddress1"] != null)
                {
                    return Convert.ToString(HttpContext.Current.Session["PermanentAddress1"].ToString());
                }
                else
                {
                    return "";
                }
            }
            set
            {
                HttpContext.Current.Session["PermanentAddress1"] = value;
            }
        }

        public Int32 PermanentStateId
        {
            get
            {
                if (HttpContext.Current.Session["PermanentStateId"] != null)
                {
                    return Convert.ToInt32(HttpContext.Current.Session["PermanentStateId"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                HttpContext.Current.Session["PermanentStateId"] = value;
            }
        }

        public Int32 PermanentCityId
        {
            get
            {
                if (HttpContext.Current.Session["PermanentCityId"] != null)
                {
                    return Convert.ToInt32(HttpContext.Current.Session["PermanentCityId"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                HttpContext.Current.Session["PermanentCityId"] = value;
            }
        }

        public string PinCode
        {
            get
            {
                if (HttpContext.Current.Session["PinCode"] != null)
                {
                    return Convert.ToString(HttpContext.Current.Session["PinCode"].ToString());
                }
                else
                {
                    return "";
                }
            }
            set
            {
                HttpContext.Current.Session["PinCode"] = value;
            }
        }

        public string EmailId
        {
            get
            {
                if (HttpContext.Current.Session["EmailId"] != null)
                {
                    return Convert.ToString(HttpContext.Current.Session["EmailId"].ToString());
                }
                else
                {
                    return "";
                }
            }
            set
            {
                HttpContext.Current.Session["EmailId"] = value;
            }
        }

        public string Mobile
        {
            get
            {
                if (HttpContext.Current.Session["Mobile"] != null)
                {
                    return Convert.ToString(HttpContext.Current.Session["Mobile"].ToString());
                }
                else
                {
                    return "";
                }
            }
            set
            {
                HttpContext.Current.Session["Mobile"] = value;
            }
        }

        public string UserExceptionSession
        {
            get
            {
                if (HttpContext.Current.Session["UserExceptionSession"] != null)
                {
                    return Convert.ToString(HttpContext.Current.Session["UserExceptionSession"].ToString());
                }
                else
                {
                    return "";
                }
            }
            set
            {
                HttpContext.Current.Session["UserExceptionSession"] = value;
            }
        }
        
        public string Password
        {
            get
            {
                if (HttpContext.Current.Session["Password"] != null)
                {
                    return Convert.ToString(HttpContext.Current.Session["Password"].ToString());
                }
                else
                {
                    return "";
                }
            }
            set
            {
                HttpContext.Current.Session["Password"] = value;
            }
        }

        public Int32 EmailVerified
        {
            get
            {
                if (HttpContext.Current.Session["EmailVerified"] != null)
                {
                    return Convert.ToInt32(HttpContext.Current.Session["EmailVerified"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                HttpContext.Current.Session["EmailVerified"] = value;
            }
        }

        public Int32 MobileVerified
        {
            get
            {
                if (HttpContext.Current.Session["MobileVerified"] != null)
                {
                    return Convert.ToInt32(HttpContext.Current.Session["MobileVerified"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                HttpContext.Current.Session["MobileVerified"] = value;
            }
        }



    }
}
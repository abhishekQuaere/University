using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;

using System.Data;
using System.IO;

using System.Net;
using System.Reflection;
using System.Text;

using System.Web.Configuration;

namespace AyushastraShopping.Models
{
    public class Commons
    {
        public string GetUniqueFileName(string filename)
        {
            try
            {
                string[] ext = new string[5];
                ext = filename.Split('.');
                return Guid.NewGuid().ToString() + "." + ext[ext.Length - 1];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<double> GetCoordinates(string address)
        {
            string message = string.Empty;
            List<double> coordinates = new List<double>();
            try
            {

                string addr = address.Trim();
                string key = "AIzaSyD8GcHnLoi9z1QiSbPkkS04uQab7pvWiBU";
                //string key = System.Configuration.ConfigurationManager.AppSettings["GoogleMapKey"];

                string requestUri = string.Format(
                    "https://maps.googleapis.com/maps/api/geocode/json?address={0}&key={1}",
                    Uri.EscapeDataString(addr),
                    Uri.EscapeDataString(key));

                JArray objects = GetRequest(requestUri);

                if (objects.Count > 0)
                {
                    var lat = objects[0]["results"][0]["geometry"]["location"]["lat"];
                    var lng = objects[0]["results"][0]["geometry"]["location"]["lng"];
                    if (!(lat == null || lng == null))
                    {
                        coordinates.Add(Convert.ToDouble(lat));
                        coordinates.Add(Convert.ToDouble(lng));
                    }
                    else
                    {
                        coordinates.Add(0);
                        coordinates.Add(0);
                    }

                }

            }
            catch (Exception ex)
            {
                message = ex.Message + "," + ex.StackTrace;

            }

            return coordinates;


        }

        private JArray GetRequest(string url)
        {
            //Sends requests and returns json array results

            JArray objects = new JArray { };
            try
            {

                WebRequest request = WebRequest.Create(url);
                // Set the credentials.
                // Get the response.
                HttpWebResponse response = null;
                try
                {
                    // This is where the HTTP GET actually occurs.
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (Exception ex)
                {
                    objects = JArray.Parse("[\"Error\"]");
                    //Write Error into Log File
                }
                // Display the status. You want to see "OK" here.
                var des = response.StatusDescription;
                // Get the stream containing content returned by the server.
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream, Encoding.UTF8);
                // Read the content. This is the Json that represents all the orders .
                string responseFromServer = reader.ReadToEnd();



                // change this to array.
                var json = "[" + responseFromServer + "]";
                // parse as array 
                objects = JArray.Parse(json);


                // Cleanup the streams and the response.
                reader.Close();
                dataStream.Close();
                response.Close();


            }
            catch (Exception ex)
            {
                objects = JArray.Parse("[\"Error\"]");
                //Write Error into Log File
            }


            return objects;
        }

        public List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
        public string getImagePath()
        {
            return WebConfigurationManager.AppSettings["FileUpload"];
        }
        public string getDoctorFilePath()
        {
            return WebConfigurationManager.AppSettings["DoctorFile"];
        }
    }
}
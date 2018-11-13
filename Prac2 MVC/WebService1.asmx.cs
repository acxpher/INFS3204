using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;


namespace Prac2mvc
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public String HelloWorld()
        {
            return "Hello World";
        }

        
        [WebMethod]
        public String[] ReadBooks(String path)
        {
            var records = new List<String>();
            String line;
            try
            {
                StreamReader sr = new StreamReader(path);
                while ((line= sr.ReadLine()) != null)
                {
                    records.Add(line);
                };
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block");
            }

            return records.ToArray();
        }

        [WebMethod]
        public void WriteBooks(String path, String[] Records)
        {

            try
            {
                FileStream fs = new FileStream(path, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                foreach (var record in Records)
                {
                    sw.WriteLine(record);
                    sw.Flush();
                }
                
                sw.Close();
                fs.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block");
            }

        }

        [WebMethod]
        public void WriteBook(String path, String record)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(record);
                sw.Flush();
                sw.Close();
                fs.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block");
            }
        }
    }
}

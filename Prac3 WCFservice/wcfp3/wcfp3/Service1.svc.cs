using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace wcfp3
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {


        public List<Book> ReadBooks(String file)
        {
            List<Book> tb = new List<Book>();
            using (StreamReader reader = new StreamReader(file))
            {
                while (reader.Peek() >= 0)
                {
                    Book b = new Book();
                    string str;
                    string[] strArray;
                    str = reader.ReadLine();
                    strArray = str.Split(',');
                    b.ID = strArray[0];
                    b.name = strArray[1];
                    b.author = strArray[2];
                    b.year = int.Parse(strArray[3]);
                    b.price = double.Parse(strArray[4].Substring(1));
                    b.stock = int.Parse(strArray[5]);
                    tb.Add(b);
                }
            }
            return tb;
        }


        public void WriteBooks(List<Book> books, string file)
        {

            try
            {
                FileStream fs = new FileStream(file, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                foreach (var book in books)
                {
                    String temp = book.ID + "," + book.name + "," + book.author + "," + book.year + ",$" + book.price.ToString() + "," + book.stock.ToString();
                    sw.WriteLine(temp);
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
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace wcfp3
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service2" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service2.svc or Service2.svc.cs at the Solution Explorer and start debugging.
    public class Service2 : IService2
    {
        public BookPurchaseResponse PurchaseBooks(BookPurchaseInfo info)
        {
            String file = @"C:\Users\freddy\Desktop\books.txt";
            Service1Reference.Service1Client pro = new Service1Reference.Service1Client();
            List<Service1Reference.Book> book = new List<Service1Reference.Book>();
            BookPurchaseResponse Response = new BookPurchaseResponse();
            int num = 0;
            Dictionary<int, string> Num = new Dictionary<int, string>();

            int key;
            int value;
            double totalPrice = 0.0;
            foreach (Service1Reference.Book p in pro.ReadBooks(file))
            {
                Num.Add(num, p.ID);
                book.Add(p);
                num++;
            }

            foreach (var item in info.items)
            {
                key = item.Key;
                value = item.Value;
                if (key > book.Count || key < 0)
                {
                    Response.result = false;
                    Response.response = "No such book";
                    return Response;
                }
                if (value > book[key].stock)
                {
                    Response.result = false;
                    Response.response = "No enough stocks";
                    return Response;
                }
                totalPrice = totalPrice + value * book[key].price;
            }

            if (totalPrice > info.budget)
            {
                Response.response = "No enough money";
                Response.result = false;
                return Response;
            }

            Response.response = "Your change is " +Convert.ToString(info.budget - totalPrice);
            Response.result = true;
            return Response;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC3.Controllers
{
    public class BookController : Controller
    {

        String file = @"C:\Users\freddy\Desktop\books.txt";

        // GET: Book
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowAll()
        {

            ServiceReference1.Service1Client pro = new ServiceReference1.Service1Client();
            ViewBag.index = 1;
            List<ServiceReference1.Book> books = pro.ReadBooks(file);
            return View(books);
        }

        //purchase book
        public ActionResult PurchaseBook() { ShowAll(); return View(); }
        [HttpPost]
        public ActionResult PurchaseBook(double budget, int[] number, int[] amount)
        {

            ServiceReference2.Service2Client pro = new ServiceReference2.Service2Client();
            ServiceReference2.BookPurchaseInfo info = new ServiceReference2.BookPurchaseInfo();
            ServiceReference2.BookPurchaseResponse res = new ServiceReference2.BookPurchaseResponse();
            Dictionary<int, int> items = new Dictionary<int, int>();
            info.budget = budget;
            for (int i = 0; i < number.Count(); i++) {
                items.Add(number[i], amount[i]);
            }            
            info.items = items;
            res.response = pro.PurchaseBooks(info.budget, info.items, out res.result);
            ViewBag.response = res.response;
            return View();
        }
        //Add more
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult MyView(int id)
        {
            ViewData["fieldCount"] = id;
            return View();
        }

        //Add book
        public ActionResult AddBook(){ShowAll();return View();}    
        [HttpPost]
        public ActionResult AddBook(String id, String name, String author, int year, double price, int stock)
        {
            int num;

            if (!(Int32.TryParse(id, out num)))
            {
                return Content("<script>alert('Please input a ID only with numbers');window.location='/Book/ShowAll';</script>");
            }
            else if (year.GetType() != typeof(int) || year > 2017)
            {
                return Content("<script>alert('Please input a valid year');window.location='/Book/ShowAll';</script>");
            }
            else if (price.GetType() != typeof(double) || price < 0)
            {
                return Content("<script>alert('Please input a valid price');window.location='/Book/ShowAll';</script>");
            }
            else if (stock.GetType() != typeof(int) || stock < 0)
            {
                return Content("<script>alert('Please input a valid stock');window.location='/Book/ShowAll';</script>");
            }

            else
            {
                ServiceReference1.Book book = new ServiceReference1.Book { ID = id, name = name, author = author, year = year, price = price, stock = stock };
                //write to txt file
                ServiceReference1.Service1Client pro = new ServiceReference1.Service1Client();
                List<ServiceReference1.Book> books = pro.ReadBooks(file);
                books.Insert(0, book);
                pro.WriteBooks(books, file);
                return Content("<script>alert('New book added');window.location='/Book/ShowAll';</script>");
            }
        }

        //Delete book
        public ActionResult DeleteBook() { ShowAll(); return View(); }
        [HttpPost]
        public ActionResult DeleteBook(String selected, String input)
        {
            ServiceReference1.Service1Client pro = new ServiceReference1.Service1Client();
            List<ServiceReference1.Book> books = pro.ReadBooks(file);
            switch (selected)
            {
                case "ID":

                    {
                        if (books.Find(b => b.ID == input) != null)
                        {
                            books.Remove(books.Find(b => b.ID == input));
                            pro.WriteBooks(books, file);
                            if (books.Find(b => b.ID == input) == null)
                            {
                                return Content("<script>alert('The following book is deleted:" + selected + ":" + input + "');window.location='/Book/ShowAll';</script>");
                            }
                            else
                            {
                                return Content("<script>alert('This book is Not deleted successfully');window.location='/Book/ShowAll';</script>");
                            }
                        }
                        else return Content("<script>alert('This book is Not found');window.location='/Book/ShowAll';</script>");
                    }
                case "Year":
                    {
                        if (books.Find(b => b.year == Convert.ToInt32(input)) != null)
                        {
                            books.Remove(books.Find(b => b.year == Convert.ToInt32(input)));
                            pro.WriteBooks(books, file);
                            if (books.Find(b => b.year == Convert.ToInt32(input)) == null)
                            {
                                return Content("<script>alert('The following book is deleted:" + selected + ":" + input + "');window.location='/Book/ShowAll';</script>");

                            }
                            else
                            {
                                return Content("<script>alert('This book is Not deleted successfully');window.location='/Book/ShowAll';</script>");
                            }
                        }
                        else return Content("<script>alert('This book is Not found');window.location='/Book/ShowAll';</script>");
                    }
                case "Num":
                    {
                        int d = Convert.ToInt32(input);
                        if (d <= books.Count&&d>0)
                        {
                            
                            if (books[d-1] != null)
                            //&& Convert.ToInt32(input)< bookList.Count)
                            {
                                books.RemoveAt(d-1);
                                pro.WriteBooks(books, file);
                                /* if (books[Convert.ToInt32(input) - 1] == null)
                                 {

                                     return Content("<script>alert('The following book is deleted:" + selected + ":" + input + "');window.location='/Book/ShowAll';</script>");

                                 }
                                 else
                                 {
                                     return Content("<script>alert('This book is Not deleted successfully');window.location='/Book/ShowAll';</script>");
                                 }
                                 */
                                return Content("<script>alert('The following book is deleted:" + selected + ":" + input + "');window.location='/Book/ShowAll';</script>");
                            }
                            else return Content("<script>alert('This book is Not found');window.location='/Book/ShowAll';</script>");
                        }
                        else return Content("<script>alert('Book Index out of range');window.location='/Book/ShowAll';</script>");

                    }
            }
            return Content("<script>alert('Please make a valid selection');window.location='/Book/ShowAll';</script>");
        }

        //Search book
        public ViewResult SearchBook() { ShowAll(); return View(); }
        [HttpPost]
        public ActionResult SearchBook(string selected, string input)
        {
            ServiceReference1.Service1Client pro = new ServiceReference1.Service1Client();
            List<ServiceReference1.Book> books = pro.ReadBooks(file);
            List<ServiceReference1.Book> result = new List<ServiceReference1.Book>();
            switch (selected)
            {
                case "ID":
                    {

                        foreach (ServiceReference1.Book item in books)
                        {
                            if (item.ID.Contains(input))
                            result.Add(item); 
                           
                        }
                        if (result.Count == 0) return Content("<script>alert('Matching result can not be found');window.location='/Book/ShowAll';</script>");
                        else return View(result);
                    }

                case "Name":
                    {
                        foreach (ServiceReference1.Book item in books)
                        {
                            if (item.name.ToLower().Contains(input.ToLower()))
                            result.Add(item); 
                          
                        }
                        if (result.Count == 0) return Content("<script>alert('Matching result can not be found');window.location='/Book/ShowAll';</script>");
                        else return View(result);
                    }


                case "Author":
                    {

                        foreach (ServiceReference1.Book item in books)
                        {
                            if (item.author.ToLower().Contains(input.ToLower()))
                            result.Add(item);                          
                        }
                        if(result.Count==0) return Content("<script>alert('Matching result can not be found');window.location='/Book/ShowAll';</script>");
                        else return View(result);
                    }



                case "Year":
                    {

                        foreach (ServiceReference1.Book item in books)
                        {
                            if (item.year == Convert.ToInt32(input))
                            result.Add(item);
                            
                        }
                        if (result.Count == 0) return Content("<script>alert('Matching result can not be found');window.location='/Book/ShowAll';</script>");
                        else return View(result);
                    }

            }
            return Content("<script>alert('Please make a valid selection');window.location='/Book/ShowAll';</script>");
        }

       
    }
}
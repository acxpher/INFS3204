using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace Prac2mvc.Models
{
    
    public class BooksController : Controller
    {
        private BookDBContext db = new BookDBContext();//initialization db
        public String path = AppDomain.CurrentDomain.BaseDirectory + @"books.txt";//define file path
        localhost.WebService1 ws = new localhost.WebService1(); //reference web service

        //function to get a list of books from server
        public List<Book> GetAllBook() {
            List<Book> bookList = new List<Book>();
            String[] records = ws.ReadBooks(path);
            int num = 0;
            foreach (String line in records)
            {
                num++;
                Book book = new Book();
                String[] temp = line.Split(',');
                book.ID = num;
                book.BookID = temp[0];
                book.Name = temp[1];
                book.Author = temp[2];
                book.Year = temp[3];
                book.Price = Convert.ToDecimal(temp[4].Substring(1));//handling the "$"
                book.Stock = Convert.ToInt16(temp[5]);
                
                bookList.Add(book);
            }

            return bookList;
        }

        [HttpGet]
        public ActionResult Index(String option, String search)
        {
            ViewBag.path = path;

            List<Book> bookList = GetAllBook();
            List<Book> rest = new List<Book>();

            //check duplicated record
            foreach (Book book in bookList) {
                var test = db.Books.Where(x => x.BookID.Equals(book.BookID)).ToList();
                if (test.Count()==0) {
                    rest.Add(book);
                }
            }
            db.Books.AddRange(rest);
            db.SaveChanges();

            //Search
            switch (option)
            {
                case "BookID":
                    return View(db.Books.Where(b => b.BookID.Contains(search)).ToList());
                case "Name":
                    return View(db.Books.Where(b => b.Name.Contains(search)).ToList());
                case "Author":
                    return View(db.Books.Where(b => b.Author.Contains(search)).ToList());
                case "Year":
                    return View(db.Books.Where(b => b.Year.Contains(search)).ToList());
                case "Price":
                    return View(db.Books.Where(b => b.Price.ToString().Contains(search)).ToList());
                default:
                    return View(db.Books.ToList());
            }
        }

        //handle the multi delete function
        [HttpPost]
        public ActionResult Index(Boolean Year, String delete)
        {
            if (Year)
            {
                List<Book> test = db.Books.Where(x => x.Year.Equals(delete)).ToList();
                db.Books.RemoveRange(test);
                db.SaveChanges();
                String[] results = BufferToWrite(db.Books.ToList()); //prepare the data
                ws.WriteBooks(path, results);//call the webservice to write txt file
                return View(db.Books.ToList());
            }
            else
            {
                return View(db.Books.ToList());
            }

        }

        // GET: Books
        public async Task<ActionResult> Index()
        {
            return View(await db.Books.ToListAsync());
        }

                
        // GET: Books/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }
        

        // GET: Books/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,BookID,Name,Author,Year,Price,Stock")] Book book)
        {
            if (ModelState.IsValid)
            {
                //prepare the String to write
                String record = book.BookID + "," + book.Name + "," + book.Author + "," + book.Year + ",$" + book.Price.ToString() + "," + book.Stock.ToString();
                //call the webservice to write txt file
                ws.WriteBook(path, record);
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        //function to prepare data to write
        public String[] BufferToWrite(List<Book> bookList)
        {
            var results = new List<String>();
            if (bookList.Count() == 0) {
                RedirectToAction("Index");
            }
            else
            {
                foreach (var book in bookList)
                {
                    String temp = book.BookID + "," + book.Name + "," + book.Author + "," + book.Year + ",$" + book.Price.ToString() + "," + book.Stock.ToString();
                    results.Add(temp);
                }
            }
            return results.ToArray();
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,BookID,Name,Author,Year,Price,Stock")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                await db.SaveChangesAsync();
                String[] results = BufferToWrite(db.Books.ToList());//prepare the array to write
                ws.WriteBooks(path, results);//call the webservice to write txt file

                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Book book = await db.Books.FindAsync(id);

            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {

            Book book = await db.Books.FindAsync(id);
            db.Books.Remove(book);
            await db.SaveChangesAsync();
            String[] results = BufferToWrite(db.Books.ToList());
            ws.WriteBooks(path, results);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

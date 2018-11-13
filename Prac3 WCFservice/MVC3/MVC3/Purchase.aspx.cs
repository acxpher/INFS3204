using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MVC3
{
    public partial class Purchase : System.Web.UI.Page
    {

        String file = @"C:\Users\freddy\Desktop\books.txt";
        List<string> boxid = new List<string>();
       // bool For_Search = false;
        int add
        {
            get { return (int)ViewState["Add"]; }
            set { ViewState["Add"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            ServiceReference1.Service1Client pro = new ServiceReference1.Service1Client();
            List<ServiceReference1.Book> books = pro.ReadBooks(file);
            //createTable(booklist);
            morebook.Style.Add("margin-right", "3em");
            if (!IsPostBack) { this.add = 0; } else { addmore(); }

        }
        //ADD MORE
        protected void addmore()
        {

            for (int i = 0; i < this.add; i++)
            {
                Label lbl = new Label();
                lbl.Text = " Book Number ";
                lbl.CssClass = "L1";
                More.Controls.Add(lbl);

                TextBox txt = new TextBox();
                txt.Style.Add("margin-right", "3em");
                txt.ID = "morebook" + i.ToString();
                txt.CssClass = "txt";
                More.Controls.Add(txt);

                Label lbl2 = new Label();
                lbl2.Text = " Amount  ";
                lbl2.CssClass = "L2";
                More.Controls.Add(lbl2);

                TextBox txt2 = new TextBox();
                txt2.ID = "moreamount" + i.ToString();
                txt2.CssClass = "txt2";
                More.Controls.Add(txt2);

                More.Controls.Add(new LiteralControl("<br/>"));
            }
        }
        protected void MORE_Click(object sender, EventArgs e)
        {
            Label lbl = new Label();
            lbl.Text = "Book Number";
            lbl.CssClass = "L1";
            More.Controls.Add(lbl);

            TextBox txt = new TextBox();
            txt.Style.Add("margin-right", "3em");
            txt.ID = "morebook" + add.ToString();
            txt.CssClass = "txt";
            More.Controls.Add(txt);

            Label lbl2 = new Label();
            lbl2.Text = "Amount";
            lbl2.CssClass = "L2";
            More.Controls.Add(lbl2);

            TextBox txt2 = new TextBox();
            txt2.ID = "moreamount" + add.ToString();
            txt2.CssClass = "txt2";
            More.Controls.Add(txt2);

            More.Controls.Add(new LiteralControl("<br/>"));
            this.add++;

        }
        //PURCHASE
        protected void P_Click(object sender, EventArgs e)
        {
            float buget = 0;
            int key = 0;
            int value = 0;
            bool done = true;
            String response = "";
            Dictionary<int, int> items = new Dictionary<int, int>();
            List<int> keyBox = new List<int>();
            List<int> valueBox = new List<int>();

            try
            {
                items.Add(Convert.ToInt32(morebook.Text), Convert.ToInt32(moreamount.Text));
                buget = Convert.ToSingle(Buget.Text);

                foreach (Control c in More.Controls)
                {
                    
                    if (c is TextBox && c.ID.StartsWith("morebook"))
                    {

                        TextBox morebooks = (TextBox)c;
                        key = Convert.ToInt32(morebooks.Text);
                        //Duplicate Keys
                        if (!keyBox.Contains(key)) { Debug.WriteLine("Key = " + key); keyBox.Add(key); }
                        else { done = false; response = "Please add up same books together"; break; }


                    }
                    //Get Value
                    if (c is TextBox && c.ID.StartsWith("moreamount"))
                    {
                        TextBox moreamounts = (TextBox)c;
                        value = Convert.ToInt32(moreamounts.Text);
                        if (done == true) { valueBox.Add(value); }

                    }
                }

                if (done == true)
                {
                    //Construct Dictionary of Book
                    for (int x = 0; x < keyBox.Count; x++)
                    {
                        items.Add(keyBox[x], valueBox[x]);
                    }
                    bool result;
                    ServiceReference2.Service2Client pro = new ServiceReference2.Service2Client();
                    ServiceReference2.BookPurchaseInfo info = new ServiceReference2.BookPurchaseInfo();
                    ServiceReference2.BookPurchaseResponse res = new ServiceReference2.BookPurchaseResponse();
                    response = pro.PurchaseBooks(buget, items, out result);


                }

                TextBox4.Text = response;


            }
            //Duplicate Keys
            catch (ArgumentException) { TextBox4.Text = "Same book in same box"; }
            //Empty Boxes
            catch (FormatException) { TextBox4.Text = "Please fill in all blanks"; }
        }
    }
}
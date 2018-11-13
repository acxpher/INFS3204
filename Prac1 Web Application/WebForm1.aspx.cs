using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            calculate();
        }
        public void calculate() {
            if (TextBox1.Text.Length > 0 && TextBox2.Text.Length > 0) {
                double result=0;
                double num1 = Convert.ToDouble(TextBox1.Text);
                double num2 = Convert.ToDouble(TextBox2.Text);

                switch (DropDownList1.SelectedValue){
                    case "+":
                        result = num1 + num2;
                        break;
                    case "-":
                        result = num1 - num2;
                        break;
                    case "*":
                        result = num1 * num2;
                        break;
                    case "/":
                        result = num1 / num2;
                        break;
             
                }
                TextBox3.Text = result.ToString();
                TextBox4.Text = Convert.ToString((int)result, 2);
            }
           

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            count();
        }
        public void count() {
            string ans = TextBox4.Text;
            int numof0 = ans.Length - ans.Replace("0", "").Length;
            int numof1 = ans.Length - ans.Replace("1", "").Length;
            TextBox5.Text = numof0.ToString();
            TextBox6.Text = numof1.ToString();
           
            





        }
    }
}
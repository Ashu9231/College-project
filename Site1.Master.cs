using MongoDB.Bson.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppl
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["username"] != null)
                {
                    if (Session["Role"].Equals("User"))
                    {
                        LinkButton6.Visible = false;//admin page
                        LinkButton9.Visible = false;//author page
                        LinkButton8.Visible = false;//publisher page
                        LinkButton7.Visible = false;//BooK Issuing page
                        LinkButton10.Visible = false;//admin inventory page
                        LinkButton11.Visible = false;//admin management page
                        LinkButton2.Visible = false;//login page
                        LinkButton3.Visible = false;//sign up page
                        LinkButton5.Visible = true;//log out page
                        LinkButton1.Visible = true;// View book page
                        LinkButton4.Text = "Hello" + Session["username"].ToString();// hello user page 
                    }
                    else if (Session["Role"].Equals("admin"))
                    {
                        LinkButton6.Visible = false;//admin page
                        LinkButton9.Visible = true;//author page
                        LinkButton8.Visible = true;//publisher page
                        LinkButton7.Visible = true;//BooK Issuing page
                        LinkButton10.Visible = true;//admin inventory page
                        LinkButton11.Visible = true;//admin management page
                        LinkButton2.Visible = false;//login page
                        LinkButton3.Visible = false;//sign up page
                        LinkButton5.Visible = true;//log out page
                        LinkButton1.Visible = true;// View book page
                        LinkButton4.Visible = true;// hello user page
                        LinkButton4.Text = "Hello Admin";
                    }
                }
                else
                {
                  LinkButton6.Visible = true;//admin page
                   LinkButton9.Visible = false;//author page
                   LinkButton8.Visible = false;//publisher page
                   LinkButton7.Visible = false;//BooK Issuing page
                   LinkButton10.Visible = false;//admin inventory page
                   LinkButton11.Visible = false;//admin management page
                   LinkButton2.Visible = true;//login page
                   LinkButton3.Visible = true;//sign up page
                   LinkButton5.Visible = false;//log out page
                   LinkButton1.Visible = false;// View book page
                   LinkButton4.Visible = false;// hello user page
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message.ToString() + "')</script>");
            }




        }
        protected void LinkButton6_Click(object sender, EventArgs e)
        {
            Response.Redirect("Adminlogin.aspx");
        }

        protected void LinkButton9_Click(object sender, EventArgs e)
        {
            Response.Redirect("Athor managment.aspx");
        }

        protected void LinkButton8_Click(object sender, EventArgs e)
        {
            Response.Redirect("PublishrManagment.aspx");
        }

        protected void LinkButton7_Click(object sender, EventArgs e)
        {
            Response.Redirect("Book Issusing.aspx");
        }

        protected void LinkButton10_Click(object sender, EventArgs e)
        {
            Response.Redirect("book Inventry page.aspx");
        }

        protected void LinkButton11_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminMemberManagment.aspx");
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("loginpage.aspx");
          
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {

            Response.Redirect("Userloginform.aspx");

        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
           
            Session["username"] = "";

            Session["Fullname"] = "";
            Session["Role"] = "";
            Response.Redirect("loginpage.aspx");

            LinkButton6.Visible = false;//admin page
            LinkButton9.Visible = false;//author page
            LinkButton8.Visible = false;//publisher page
            LinkButton7.Visible = false;//BooK Issuing page
            LinkButton10.Visible = false;//admin inventory page
            LinkButton11.Visible = false;//admin management page
            LinkButton2.Visible = false;//login page
            LinkButton3.Visible = false;//sign up page
            LinkButton5.Visible = true;//log out page
            LinkButton1.Visible = true;// View book page

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Viewbook.aspx");
           
        }
        protected void LinkButton12_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cart.aspx");
        }


        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserManagment.aspx");
        }
    }
}
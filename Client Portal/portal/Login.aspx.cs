using System;
using System.Collections.Specialized;
using Stemstudios.DataAccessLayer;

namespace Client_Portal
{
    public partial class Login : System.Web.UI.Page
    {
        private Database db = Database.Instance;

        public Login()
        {
            this.Load += new EventHandler(Login_Load);
        }

        protected void Login_Load(object sender, EventArgs e)
        {
            if (Session["siteObj"] != null)
            {
                Stemstudios.DataAccessLayer.DataObjects.Site siteObj = (Stemstudios.DataAccessLayer.DataObjects.Site)Session["siteObj"];
                Uri req = Request.Url;
                if (siteObj.IsTempPassword())
                {
                    Response.Redirect("http://" + req.Authority + "/portal/ChangePassword.aspx");
                }
                else
                {
                    Response.Redirect("http://" + req.Authority + "/portal/Home.aspx");
                }
            }
            else if (this.Request.HttpMethod.Equals("POST"))
            {
                NameValueCollection requestParams = this.Request.Params;
                if (requestParams["txtEmail"] != null && requestParams["txtEmail"] != null)
                {
                    if (requestParams["txtEmail"].Length > 0 && requestParams["txtPass"].Length > 0)
                    {
                        if (Formating.EmailAddressCheck(requestParams["txtEmail"]) && Formating.PasswordCheck(requestParams["txtPass"]))
                        {
                            try
                            {
                                String hashedPass = db.GetSHA256Hash(requestParams["txtPass"]);
                                DataSet loginRes = db.Select("*", Stemstudios.DataAccessLayer.DataObjects.Site.Table, "SiteEmail = '" + requestParams["txtEmail"] + "' AND PortalPassword = '" + hashedPass + "'");
                                if (loginRes.NumberOfRows() > 0)
                                {
                                    Stemstudios.DataAccessLayer.DataObjects.Site siteObj = new Stemstudios.DataAccessLayer.DataObjects.Site(loginRes.GetRecordDataSet(0));
                                    if (Session["siteObj"] != null)
                                    {
                                        Session.Remove("siteObj");
                                    }
                                    this.Session.Add("siteObj", siteObj);
                                    Uri req = Request.Url;
                                    if (siteObj.IsTempPassword())
                                    {
                                        Response.Redirect("http://" + req.Authority + "/portal/ChangePassword.aspx");
                                    }
                                    else
                                    {
                                        Response.Redirect("http://" + req.Authority + "/portal/DisplayRepairs.aspx");
                                    }
                                }
                                else
                                {
                                    ErrorMsg.Text = "Error the account credientials provided do not match an account in the system.";
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorMsg.Text = ex.Message;
                            }
                        }
                        else
                        {
                            ErrorMsg.Text = "Error please confirm email and password are formatted correctly.";
                        }
                    }
                    else
                    {
                        ErrorMsg.Text = "Error both email and password must be provided.";
                    }
                }
                else
                {
                    ErrorMsg.Text = "Error both email and password must be provided.";
                }
            }

        }
    }
}
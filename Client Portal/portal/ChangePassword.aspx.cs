using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using Stemstudios.DataAccessLayer;

namespace Client_Portal
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["siteObj"] == null)
            {
                Uri req = Request.Url;
                Response.Redirect("http://" + req.Authority + "/portal/Login.aspx");
            }
            else if (this.Request.HttpMethod.Equals("POST"))
            {
                Stemstudios.DataAccessLayer.DataObjects.Site siteObj = (Stemstudios.DataAccessLayer.DataObjects.Site)Session["siteObj"];
                NameValueCollection requestParams = this.Request.Params;
                String currPass = Database.Instance.GetSHA256Hash(requestParams["txtCurrentPass"]);
                if (currPass.Equals(siteObj.GetPassword()))
                {
                    if (requestParams["txtPassOne"].Length > 0 && requestParams["txtPassTwo"].Length > 0)
                    {
                        if (Formating.ItemIDCheck(requestParams["txtPassOne"]) && Formating.ItemIDCheck(requestParams["txtPassTwo"]))
                        {
                            if (requestParams["txtPassOne"].Equals(requestParams["txtPassTwo"]))
                            {
                                siteObj.SetPassword(requestParams["txtPassOne"]);
                                try
                                {
                                    siteObj.SaveObject(Database.Instance);
                                    Uri req = Request.Url;
                                    Response.Redirect("http://" + req.Authority + "/portal/DisplayRepairs.aspx");
                                }
                                catch (Exception ex)
                                {
                                    ErrorMsg.Text = "Error "+ex.Message;
                                }
                            }
                            else
                            {
                                ErrorMsg.Text = "Error passwords do not match.";
                            }
                        }
                        else
                        {
                            ErrorMsg.Text = "Error new password is not formatted properly.<br/>A password must have one lowercase and uppercase character and one digit.";
                        }
                    }
                    else
                    {
                        ErrorMsg.Text = "Error password fields can not be blank.";
                    }
                }
                else
                {
                    ErrorMsg.Text = "Error current password provided does not matched the on in the system.";
                }
            }
        }
    }
}
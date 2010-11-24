using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using Stemstudios.DataAccessLayer;
using Stemstudios.DataAccessLayer.DataObjects;

namespace Client_Portal
{
    public partial class Login : System.Web.UI.Page
    {
        private Database db = Database.Instance;

        public Login()
        {
            this.LoadComplete += new EventHandler(Login_LoadComplete);
        }

        protected void Login_LoadComplete(object sender, EventArgs e)
        {
            if (this.Request.HttpMethod.Equals("POST"))
            {
                NameValueCollection requestParams = this.Request.Params;
                if (requestParams["txtEmail"] != null && requestParams["txtEmail"] != null)
                {
                    if (requestParams["txtEmail"].Length > 0 && requestParams["txtPass"].Length > 0)
                    {
                        if (Formating.EmailAddressCheck(requestParams["txtEmail"]) && Formating.ItemIDCheck(requestParams["txtPass"]))
                        {
                            try
                            {
                                DataSet loginRes = db.Select("*", Stemstudios.DataAccessLayer.DataObjects.Site.Table, "SiteEmail = '" + requestParams["txtEmail"] + "' AND PortalPassword = '" + requestParams["txtPass"] + "'");
                                if (loginRes.NumberOfRows() > 0)
                                {
                                    Stemstudios.DataAccessLayer.DataObjects.Site siteObj = new Stemstudios.DataAccessLayer.DataObjects.Site(loginRes.GetRecordDataSet(0));
                                    ErrorMsg.Text = "Logged In!";
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
}
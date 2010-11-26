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
    public partial class ViewRepair : System.Web.UI.Page
    {
        private Database db = Database.Instance;
        protected void Page_Load(object sender, EventArgs e)
        {
            NameValueCollection requestParams = this.Request.Params;
            if (requestParams["id"] == null)
            {
                Response.StatusCode = 404;
                Response.End();
                return;
            }
            if (!Formating.TitleCheck(requestParams["id"]))
            {
                Response.StatusCode = 404;
                Response.End();
                return;
            }

            DataSet data = db.Select("*", LotRepair.Table, LotRepair.Fields.repairID.ToString() + " = '" + requestParams["id"] + "'");
            if (data.NumberOfRows() == 0)
            {
                Response.StatusCode = 404;
                Response.End();
                return;
            }
            data.Read();
            LotRepair repair = new LotRepair(data);
            this.Title = "Work Order " + repair.GetWorkOrder() + " - Ragno Electric";
            this.pageTitle.InnerText = "Work Order " + repair.GetWorkOrder();

        }
    }
}
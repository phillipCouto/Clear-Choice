using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using Stemstudios.DataAccessLayer;
using Stemstudios.DataAccessLayer.DataObjects;
using System.Text;

namespace Client_Portal
{
    public partial class ViewRepair : System.Web.UI.Page
    {
        private Database db = Database.Instance;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["siteObj"] == null)
            {
                Uri req = Request.Url;
                Response.Redirect("http://" + req.Authority + "/portal/Login.aspx");
            }

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

            try
            {
                DataSet data = db.Select("*", LotRepair.Table, LotRepair.Fields.repairID.ToString() + " = '" + requestParams["id"] + "'");
                if (data.NumberOfRows() == 0)
                {
                    Response.StatusCode = 404;
                    Response.End();
                    return;
                }
                data.Read();
                LotRepair repair = new LotRepair(data);
                data = db.Select("*", Lot.Table, Lot.Fields.lotID.ToString() + " = '" + repair.GetLotID() + "'");
                data.Read();

                Lot lot = new Lot(data.GetRecordDataSet());


                this.Title = "Work Order " + repair.GetWorkOrder() + " - Ragno Electric";
                this.pageTitle.InnerText = "Work Order " + repair.GetWorkOrder();
                this.tdLotNum.InnerText = "" + lot.GetLotNumber();
                this.tdLotAddress.InnerText = "" + lot.GetAddress();
                this.tdLotModel.InnerText = "" + lot.GetLotType();
                this.tdtLotWorkOrder.InnerText = "" + repair.GetWorkOrder();

                tdApptDate.InnerText = ((repair.GetDateOfAppointment().Equals(DateTime.MinValue)) ? "" : repair.GetDateOfAppointment().ToLongDateString());
                tdApptInspection.InnerText = ((repair.GetInspectionPassed().Equals(DateTime.MinValue)) ? "" : repair.GetInspectionPassed().ToLongDateString());
                tdApptRequested.InnerText = repair.GetRequestedBy();
                tdApptWindow.InnerText = repair.GetWindowOfAppointment();
                tdApptSourceCode.InnerText = repair.GetSourceCode();

                tdClientName.InnerText = repair.GetOwnerName();
                tdClientPhone.InnerText = repair.GetHomeNumber();
                tdClientAltPhone.InnerText = repair.GetAltNumber();
                tdClientEmail.InnerText = repair.GetEmail();

                pageNotes.InnerHtml = repair.GetNotes().Replace(Environment.NewLine,"<br/>");
                String header = "<table width=\"100%\"><tr class=\"rowHeader\"><td>Problem Area</td><td>Description</td><td>Completed Date</td><td>Time</td><td>Action Taken</td></tr>";
                StringBuilder repActionTable = new StringBuilder();
                repActionTable.Append(header);
                data = db.Select("*", LotRepairAction.Table, LotRepairAction.Fields.repairID.ToString() + " = '" + repair.GetRepairID() + "'");
                int actionCount = 0;
                while (data.Read())
                {
                    LotRepairAction action = new LotRepairAction(data.GetRecordDataSet());
                    if (actionCount % 2 == 0)
                    {
                        repActionTable.Append("<tr class=\"itemRow itemRowActions\">");
                    }
                    else
                    {
                        repActionTable.Append("<tr class=\"itemRow itemRowOdd itemRowActions\">");
                    }
                    repActionTable.Append("<td>"+action.GetProblemArea()+"</td>");
                    repActionTable.Append("<td>" + action.GetDescription() + "</td>");
                    repActionTable.Append("<td>" + ((action.GetDate().Equals(DateTime.MinValue)) ? "" : action.GetDate().ToLongDateString()) + "</td>");
                    repActionTable.Append("<td>" + action.GetTime() + "</td>");
                    repActionTable.Append("<td>" + action.GetAction() + "</td>");
                    repActionTable.Append("</tr>");

                    actionCount++;
                }
                repActionTable.Append("</table>");
                pageRepairActions.InnerHtml = repActionTable.ToString();

            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                Response.StatusDescription = "Error loading Repair objects from database.";
                Response.End();
                return;
            }

        }
    }
}
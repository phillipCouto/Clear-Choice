using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Stemstudios.DataAccessLayer;
using Stemstudios.DataAccessLayer.DataObjects;
using System.Text;

namespace Client_Portal
{
    public partial class DisplayRepairs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["siteObj"] == null)
            {
                Uri req = Request.Url;
                Response.Redirect("http://" + req.Authority + "/portal/Login.aspx");
            }
            else
            {
                Stemstudios.DataAccessLayer.DataObjects.Site siteObj = (Stemstudios.DataAccessLayer.DataObjects.Site)Session["siteObj"];
                DataSet data = Database.Instance.Select(LotRepair.Table+".*",LotRepair.Table+","+Lot.Table,"lot_repairs.lotID = lots.lotID AND lots.assocID = '"+siteObj.GetSiteID()+"'",LotRepair.Fields.DateOfAppointment.ToString()+" DESC");
                String header = "<table width=\"100%\"><tr><td>Work Order</td><td>Date</td><td>Window</td><td>Requested By</td><td>Inspection Date</td></tr>";
                StringBuilder upComing = new StringBuilder();
                StringBuilder incomplete = new StringBuilder();
                StringBuilder completed = new StringBuilder();
                upComing.Append(header);
                incomplete.Append(header);
                completed.Append(header);

                while (data.Read())
                {
                    LotRepair repair = new LotRepair(data.GetRecordDataSet());
                    if (DateTime.Now.CompareTo(repair.GetDateOfAppointment()) < 0)
                    {
                        upComing.Append("<tr><td><a class=\"item\" href=\"/portal/\">" + repair.GetWorkOrder() + "</a></td>");
                        upComing.Append("<td>" + repair.GetDateOfAppointment().ToLongDateString() + "</td>");
                        upComing.Append("<td>" + repair.GetWindowOfAppointment() + "</td>");
                        upComing.Append("<td>" + repair.GetRequestedBy() + "</td>");
                        if (!repair.GetInspectionPassed().Equals(DateTime.MinValue))
                        {
                            upComing.Append("<td>" + repair.GetInspectionPassed().ToLongDateString() + "</td>");
                        }
                        else
                        {
                            upComing.Append("<td></td></tr>");
                        }
                    }
                    else
                    {
                        DataSet repairActions = Database.Instance.Select("*", LotRepairAction.Table, LotRepairAction.Fields.repairID.ToString() + " = '" + repair.GetRepairID() + "' AND " + LotRepairAction.Fields.Date.ToString() + " IS NULL");
                        if (repairActions.NumberOfRows() > 0)
                        {
                            incomplete.Append("<tr><td><a class=\"item\" href=\"/portal/\">" + repair.GetWorkOrder() + "</a></td>");
                            incomplete.Append("<td>" + repair.GetDateOfAppointment().ToLongDateString() + "</td>");
                            incomplete.Append("<td>" + repair.GetWindowOfAppointment() + "</td>");
                            incomplete.Append("<td>" + repair.GetRequestedBy() + "</td>");
                            if (!repair.GetInspectionPassed().Equals(DateTime.MinValue))
                            {
                                incomplete.Append("<td>" + repair.GetInspectionPassed().ToLongDateString() + "</td>");
                            }
                            else
                            {
                                incomplete.Append("<td></td></tr>");
                            }
                        }
                        else
                        {
                            DataSet CompleterepairActions = Database.Instance.Select("*", LotRepairAction.Table, LotRepairAction.Fields.repairID.ToString() + " = '" + repair.GetRepairID() + "'");
                            if (CompleterepairActions.NumberOfRows() > 0)
                            {
                                completed.Append("<tr><td><a class=\"item\" href=\"/portal/\">" + repair.GetWorkOrder() + "</a></td>");
                                completed.Append("<td>" + repair.GetDateOfAppointment().ToLongDateString() + "</td>");
                                completed.Append("<td>" + repair.GetWindowOfAppointment() + "</td>");
                                completed.Append("<td>" + repair.GetRequestedBy() + "</td>");
                                if (!repair.GetInspectionPassed().Equals(DateTime.MinValue))
                                {
                                    completed.Append("<td>" + repair.GetInspectionPassed().ToLongDateString() + "</td>");
                                }
                                else
                                {
                                    completed.Append("<td></td></tr>");
                                }
                            }
                        }
                    }
                }
                if (completed.ToString().Equals(header))
                {
                    cellCompletedRepairs.InnerText = "None";
                }
                else
                {
                    completed.Append("</table>");
                    cellCompletedRepairs.InnerHtml = completed.ToString();
                }

                if (incomplete.ToString().Equals(header))
                {
                    cellIncompleteRepairs.InnerText = "None";
                }
                else
                {
                    incomplete.Append("</table>");
                    cellIncompleteRepairs.InnerHtml = incomplete.ToString();
                }

                if (upComing.ToString().Equals(header))
                {
                    cellUpcomingRepairs.InnerText = "None";
                }
                else
                {
                    upComing.Append("</table>");
                    cellUpcomingRepairs.InnerHtml = upComing.ToString();
                }                
            }
        }
    }
}
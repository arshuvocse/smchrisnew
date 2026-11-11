using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Transactions;
using System.Web;
using System.Web.Script.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.MeetingMinorsDAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using DAO.MeetingMinorsDAO;
using HELPER_FUNCTIONS.HELPERS;
using Newtonsoft.Json;

public partial class MeetingMinors_MeetingEntry : System.Web.UI.Page
{
    MiscellaneousInformationDAL AMAsterDal = new MiscellaneousInformationDAL();
    MeetingEntryDAL AMeetingEntryDal = new MeetingEntryDAL();
    private SubcommitteeSetupDAL AMAster = new SubcommitteeSetupDAL();
    MemberInfoDaL aMinors = new MemberInfoDaL();
    
    // Session-backed caches so dropdown data survives across postbacks
    // and is not re-fetched on every add/remove click.
    private DataTable CompaniesCache
    {
        get
        {
            if (Session["__MtgEntry_Companies"] == null)
                Session["__MtgEntry_Companies"] = aMinors.GetAllCompaniesForRadioButton();
            return (DataTable)Session["__MtgEntry_Companies"];
        }
    }

    private DataTable GetEmployeesForCompany(string companyId)
    {
        string key = "__MtgEntry_Emps_" + companyId;
        if (Session[key] == null)
            Session[key] = AMAsterDal.GetDDLEmpInfo(companyId);
        return (DataTable)Session[key];
    }

    // Member position options (Chairman, Member, etc.) — static reference data,
    // no need to re-query the DB on every add/remove click.
    private DataTable MemberPositionCache
    {
        get
        {
            if (Session["__MtgEntry_MemberPos"] == null)
                Session["__MtgEntry_MemberPos"] = aMinors.GetDDLMemberPostion();
            return (DataTable)Session["__MtgEntry_MemberPos"];
        }
    }

    // Client-driven "Add Employees" (MeetingGridA) / "Members List" (MeetingGridB) tables.
    // These replace the old gv_Details_Save / gv_BoardMember GridViews: row state now lives
    // in a JS array, and these helpers just hand the browser fresh JSON to hydrate/append —
    // no ViewState, no FindControl. See MeetingGridA/MeetingGridB in the .aspx for the client side.
    private void EmitGridReferenceData()
    {
        // The old ddlCompanySave RowDataBound stripped a "Select..." placeholder row that
        // CompaniesCache includes for dropdown use — do the same here since Grid A's Company
        // options are rendered as a radio group (no placeholder needed).
        DataTable companies = CompaniesCache.Clone();
        foreach (DataRow row in CompaniesCache.Rows)
        {
            if (!string.Equals(row["TextField"].ToString(), "Select...", StringComparison.OrdinalIgnoreCase))
            {
                companies.ImportRow(row);
            }
        }

        string companiesJson = JsonConvert.SerializeObject(companies);
        string positionsJson = JsonConvert.SerializeObject(MemberPositionCache);
        string script = "MeetingGridA.setReferenceData(" + companiesJson + ");MeetingGridB.setPositionOptions(" + positionsJson + ");";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "MeetingGridRefData", script, true);
    }

    private void EmitGridAJson(bool replace, DataTable rows)
    {
        string json = JsonConvert.SerializeObject(rows ?? new DataTable());
        string method = replace ? "hydrate" : "appendRows";
        // Call render() immediately after hydrate/appendRows so the DOM updates
        // whether we're in a full postback or an async (UpdatePanel) postback.
        string script = "MeetingGridA." + method + "(" + json + "); MeetingGridA.render();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "GridA_" + Guid.NewGuid().ToString("N"), script, true);
    }

    // Board-Member Position arrives from different sources as either a numeric PositionId
    // (Category/Sub-Committee queries) or an already-resolved text label (edit-mode load,
    // and what Save always writes) — resolve to text here so the client never has to care.
    private void ResolveBoardMemberPositionText(DataTable dt)
    {
        if (dt == null || dt.Rows.Count == 0 || !dt.Columns.Contains("PositionId"))
            return;

        if (dt.Columns.Contains("Position"))
            dt.Columns.Remove("Position");
        dt.Columns.Add("Position", typeof(string));

        DataTable positions = MemberPositionCache;
        foreach (DataRow row in dt.Rows)
        {
            string posId = row["PositionId"] == null ? "" : row["PositionId"].ToString();
            string label = "";
            foreach (DataRow p in positions.Rows)
            {
                if (string.Equals(p["Value"].ToString(), posId, StringComparison.OrdinalIgnoreCase))
                {
                    label = p["TextField"].ToString();
                    break;
                }
            }
            row["Position"] = label;
        }
    }

    private void EmitGridBJson(DataTable rows)
    {
        DataTable dt = rows ?? new DataTable();
        ResolveBoardMemberPositionText(dt);
        string json = JsonConvert.SerializeObject(dt);
        // Call render() immediately after hydrate so the DOM updates whether we're
        // in a full postback or an async (UpdatePanel) postback.
        string script = "MeetingGridB.hydrate(" + json + "); MeetingGridB.render();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "GridB_" + Guid.NewGuid().ToString("N"), script, true);
    }

    // A full (non-Ajax) postback wipes MeetingGridA/B's client-side JS state — the
    // browser reloads the whole page and the tbody markup starts empty again. On a
    // successful save the page navigates away, so it doesn't matter; but on a
    // validation failure the page re-renders in place and needs its grids re-hydrated
    // from the hidden JSON fields the client already posted, or Members List / Add
    // Employees appear empty even though the data is still present in the hidden fields.
    private void RestoreClientGridsAfterValidationFailure()
    {
        string gridAJson = hfGridA_Json.Value;
        if (!string.IsNullOrWhiteSpace(gridAJson) && gridAJson != "[]" && gridAJson != "null")
        {
            string scriptA = "MeetingGridA.hydrate(" + gridAJson + "); MeetingGridA.render();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "GridA_Restore_" + Guid.NewGuid().ToString("N"), scriptA, true);
        }

        string gridBJson = hfGridB_Json.Value;
        if (!string.IsNullOrWhiteSpace(gridBJson) && gridBJson != "[]" && gridBJson != "null")
        {
            string scriptB = "MeetingGridB.hydrate(" + gridBJson + "); MeetingGridB.render();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "GridB_Restore_" + Guid.NewGuid().ToString("N"), scriptB, true);
        }

        string gridDocJson = hfGridDoc_Json.Value;
        if (!string.IsNullOrWhiteSpace(gridDocJson) && gridDocJson != "[]" && gridDocJson != "null")
        {
            string scriptDoc = "MeetingGridDoc.hydrate(" + gridDocJson + ");";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "GridDoc_Restore_" + Guid.NewGuid().ToString("N"), scriptDoc, true);
        }

        RestoreLocationAndSubCommitteeDropdowns();
    }

    private void RestoreLocationAndSubCommitteeDropdowns()
    {
        try
        {
            string compLocId = !string.IsNullOrEmpty(ddlCompanyLocation.SelectedValue)
                ? ddlCompanyLocation.SelectedValue
                : (!string.IsNullOrEmpty(ddlCompany.SelectedValue)
                    ? ddlCompany.SelectedValue
                    : (Request.Form[ddlCompanyLocation.UniqueID] ?? Request.Form[ddlCompanyLocation.ClientID]));

            string officeId = !string.IsNullOrEmpty(hfSelectedOffice.Value)
                ? hfSelectedOffice.Value
                : (Request.Form[ddlOffice.UniqueID] ?? Request.Form[ddlOffice.ClientID] ?? ddlOffice.SelectedValue);

            string locationId = !string.IsNullOrEmpty(hfSelectedLocation.Value)
                ? hfSelectedLocation.Value
                : (Request.Form[ddlLocation.UniqueID] ?? Request.Form[ddlLocation.ClientID] ?? ddlLocation.SelectedValue);

            string floorId = !string.IsNullOrEmpty(hfSelectedFloor.Value)
                ? hfSelectedFloor.Value
                : (Request.Form[ddlFloor.UniqueID] ?? Request.Form[ddlFloor.ClientID] ?? ddlFloor.SelectedValue);

            string roomId = !string.IsNullOrEmpty(hfSelectedRoom.Value)
                ? hfSelectedRoom.Value
                : (Request.Form[ddlMettingRoomName.UniqueID] ?? Request.Form[ddlMettingRoomName.ClientID] ?? ddlMettingRoomName.SelectedValue);

            string categoryId = !string.IsNullOrEmpty(ddlCategory.SelectedValue)
                ? ddlCategory.SelectedValue
                : (Request.Form[ddlCategory.UniqueID] ?? Request.Form[ddlCategory.ClientID]);

            string subCommitteeId = !string.IsNullOrEmpty(hfSelectedSubCommittee.Value)
                ? hfSelectedSubCommittee.Value
                : (Request.Form[ddlSubCommittee.UniqueID] ?? Request.Form[ddlSubCommittee.ClientID] ?? ddlSubCommittee.SelectedValue);

            bool isOffice = rbLocation.Items.Count > 0 && rbLocation.Items[0].Selected;

            if (isOffice && !string.IsNullOrEmpty(compLocId))
            {
                var officeOpts = FetchOfficeOptions(compLocId);
                BindListToControl(ddlOffice, officeOpts, officeId);

                var locOpts = !string.IsNullOrEmpty(officeId) ? GetLocationsByOffice(officeId) : new List<object>();
                if (locOpts.Count > 0) BindListToControl(ddlLocation, locOpts, locationId);

                var floorOpts = !string.IsNullOrEmpty(locationId) ? GetFloorsByLocation(locationId) : new List<object>();
                if (floorOpts.Count > 0) BindListToControl(ddlFloor, floorOpts, floorId);

                var roomOpts = !string.IsNullOrEmpty(floorId) ? GetRoomsByFloor(floorId) : new List<object>();
                if (roomOpts.Count > 0) BindListToControl(ddlMettingRoomName, roomOpts, roomId);

                string officeJson = JsonConvert.SerializeObject(officeOpts);
                string locJson = JsonConvert.SerializeObject(locOpts);
                string floorJson = JsonConvert.SerializeObject(floorOpts);
                string roomJson = JsonConvert.SerializeObject(roomOpts);

                string scriptDropdowns = string.Format(
                    "renderSelectOptions(document.getElementById('{0}'), {4}, {5});" +
                    "renderSelectOptions(document.getElementById('{1}'), {6}, {7});" +
                    "renderSelectOptions(document.getElementById('{2}'), {8}, {9});" +
                    "renderSelectOptions(document.getElementById('{3}'), {10}, {11});",
                    ddlOffice.ClientID,
                    ddlLocation.ClientID,
                    ddlFloor.ClientID,
                    ddlMettingRoomName.ClientID,
                    officeJson,
                    JsonConvert.SerializeObject(officeId ?? ""),
                    locJson,
                    JsonConvert.SerializeObject(locationId ?? ""),
                    floorJson,
                    JsonConvert.SerializeObject(floorId ?? ""),
                    roomJson,
                    JsonConvert.SerializeObject(roomId ?? "")
                );
                ScriptManager.RegisterStartupScript(this, this.GetType(), "RestoreDropdowns_" + Guid.NewGuid().ToString("N"), scriptDropdowns, true);
            }

            if (!string.IsNullOrEmpty(categoryId))
            {
                var state = FetchCategorySubCommitteeState(compLocId, categoryId, id_mastetID.Value);
                if (state != null && state.SubCommitteeOptions != null && state.SubCommitteeOptions.Count > 0)
                {
                    BindListToControl(ddlSubCommittee, state.SubCommitteeOptions, subCommitteeId);
                    string subJson = JsonConvert.SerializeObject(state.SubCommitteeOptions);
                    string scriptSub = string.Format(
                        "if (document.getElementById('{0}')) document.getElementById('{0}').style.display = '{1}';" +
                        "renderSelectOptions(document.getElementById('{2}'), {3}, {4});",
                        DivSubCommitte.ClientID,
                        state.SubCommitteeVisible ? "" : "none",
                        ddlSubCommittee.ClientID,
                        subJson,
                        JsonConvert.SerializeObject(subCommitteeId ?? "")
                    );
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "RestoreSubComm_" + Guid.NewGuid().ToString("N"), scriptSub, true);
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.TraceError("Error restoring location/subcommittee dropdowns: " + ex);
        }
    }

    private void BindListToControl(DropDownList ddl, List<object> items, string selectedValue)
    {
        if (ddl == null) return;
        ddl.Items.Clear();
        if (items != null)
        {
            foreach (object item in items)
            {
                var dict = item as Dictionary<string, object>;
                if (dict != null)
                {
                    string val = dict.ContainsKey("Value") && dict["Value"] != null ? dict["Value"].ToString() : "";
                    string txt = dict.ContainsKey("TextField") && dict["TextField"] != null ? dict["TextField"].ToString() : "";
                    ddl.Items.Add(new ListItem(txt, val));
                }
            }
        }
        if (!string.IsNullOrEmpty(selectedValue) && ddl.Items.FindByValue(selectedValue) != null)
        {
            ddl.SelectedValue = selectedValue;
        }
    }

    // GetDTforDDL() (ClsCommonInternalDAL) already inserts a "Select..." placeholder row
    // (Value="-1") into every DataTable it returns, so DataBind()-ing one of those DataTables
    // already gives the control a placeholder as Items[0]. Several call sites here used to
    // insert a second placeholder unconditionally afterwards, producing two "Select..."/
    // "Select an Option" entries at the top of the list. Only the true raw-query fallback
    // paths (which don't go through GetDTforDDL) actually need one added. Call this instead
    // of Items.Insert(0, ...) directly so a placeholder is added only when one isn't already there.
    private static bool IsPlaceholderValue(string value)
    {
        return string.IsNullOrEmpty(value) || value == "-1";
    }

    private static void EnsureSelectPlaceholder(DropDownList ddl, string placeholderText)
    {
        if (ddl == null) return;
        if (ddl.Items.Count > 0 && IsPlaceholderValue(ddl.Items[0].Value)) return;
        ddl.Items.Insert(0, new ListItem(placeholderText, ""));
    }

    private static void EnsureSelectPlaceholder(List<object> rows, string placeholderText)
    {
        if (rows == null) return;
        if (rows.Count > 0)
        {
            var first = rows[0] as Dictionary<string, object>;
            if (first != null && first.ContainsKey("Value") &&
                IsPlaceholderValue(first["Value"] == null ? "" : first["Value"].ToString()))
            {
                return;
            }
        }
        rows.Insert(0, new Dictionary<string, object> { { "Value", "" }, { "TextField", placeholderText } });
    }

    protected void Page_Load(object sender, EventArgs e)
    {
         Page.Form.Attributes.Add("enctype", "multipart/form-data");
         FUDocument.Attributes["onchange"] = "UploadFile(this)";
        EmitGridReferenceData();
        if (!IsPostBack)
        {
       ButtonVisible();
            LoadInitialGrid();
            LoadDropDownList();

            if (!string.IsNullOrEmpty(Request.QueryString["MID"]))
            {

                id_mastetID.Value = (Request.QueryString["MID"]);

                // Master fields, mail fields, document grids, and Grid A/B are now loaded
                // client-side via GetMeetingMasterForEdit() (see the WebMethod near
                // AjaxSearchEmployeesForMeeting) instead of being populated here — this flag
                // tells the client's DOMContentLoaded handler to fire that AJAX call.
                // __meetingEditBootstrapPending guards loadLocationDropdownsIfNeeded() (fired by
                // MS AJAX's pageLoad() on this very first load) from racing that AJAX call and
                // overwriting the Office/Location/Floor/Room selection it's about to set — see
                // the matching guard/clear in loadLocationDropdownsIfNeeded() and the
                // GetMeetingMasterForEdit .done()/.fail() handlers.
                ScriptManager.RegisterStartupScript(this, this.GetType(), "MeetingEditBootstrap",
                    "window.__meetingEditBootstrapPending = true; var __meetingEditMID = " + JsonConvert.SerializeObject(id_mastetID.Value) + ";", true);

                // rbNotice is Visible="false" (never rendered to the DOM), so unlike every other
                // field above it can't be populated by client-side JS — it has to stay set here.
                try
                {
                    DataTable dtMaster = AMeetingEntryDal.GetMasterDataById(id_mastetID.Value);
                    if (dtMaster != null && dtMaster.Rows.Count > 0)
                    {
                        DataRow row = dtMaster.Rows[0];

                        if (dtMaster.Columns.Contains("IsNotice") && row["IsNotice"] != DBNull.Value)
                        {
                            bool isNot = Convert.ToBoolean(row["IsNotice"]);
                            rbNotice.SelectedValue = isNot ? "1" : "0";
                            if (rbEntryType.Items.Count > 1)
                            {
                                rbEntryType.Items[0].Selected = isNot;
                                rbEntryType.Items[1].Selected = !isNot;
                            }
                        }

                        if (dtMaster.Columns.Contains("Title") && row["Title"] != DBNull.Value)
                            txtTitle.Text = row["Title"].ToString();

                        if (dtMaster.Columns.Contains("MeetingNo") && row["MeetingNo"] != DBNull.Value)
                            txtMeetingNo.Text = row["MeetingNo"].ToString();

                        if (dtMaster.Columns.Contains("MeetingDate") && row["MeetingDate"] != DBNull.Value)
                        {
                            try { txtMeetingDate.Text = Convert.ToDateTime(row["MeetingDate"]).ToString("dd-MMM-yyyy hh:mm tt"); } catch { }
                        }

                        if (dtMaster.Columns.Contains("StartTime") && row["StartTime"] != DBNull.Value)
                            txtStartTime.Text = row["StartTime"].ToString();

                        if (dtMaster.Columns.Contains("EndTime") && row["EndTime"] != DBNull.Value)
                            txtEndTime.Text = row["EndTime"].ToString();

                        string compId = dtMaster.Columns.Contains("CompanyId") ? row["CompanyId"].ToString() : "";
                        if (!string.IsNullOrEmpty(compId))
                        {
                            try { ddlCompany.SelectedValue = compId; } catch { }
                            try { ddlCompanyLocation.SelectedValue = compId; } catch { }
                        }

                        string catId = dtMaster.Columns.Contains("CategoryID") ? row["CategoryID"].ToString() : "";
                        if (!string.IsNullOrEmpty(catId))
                        {
                            try { ddlCategory.SelectedValue = catId; } catch { }
                        }

                        string subCommId = dtMaster.Columns.Contains("SubCommitteeId") ? row["SubCommitteeId"].ToString() : "";
                        if (!string.IsNullOrEmpty(subCommId) && ddlSubCommittee.Items.FindByValue(subCommId) != null)
                        {
                            try { ddlSubCommittee.SelectedValue = subCommId; } catch { }
                        }

                        bool isOffice = dtMaster.Columns.Contains("IsOfficePremisis") && row["IsOfficePremisis"] != DBNull.Value && Convert.ToBoolean(row["IsOfficePremisis"]);
                        bool isOuter = dtMaster.Columns.Contains("IsOuterPremisis") && row["IsOuterPremisis"] != DBNull.Value && Convert.ToBoolean(row["IsOuterPremisis"]);
                        bool isVirtual = dtMaster.Columns.Contains("IsVirtualMeeting") && row["IsVirtualMeeting"] != DBNull.Value && Convert.ToBoolean(row["IsVirtualMeeting"]);

                        rbLocation.Items[0].Selected = isOffice;
                        rbLocation.Items[1].Selected = isOuter;
                        rbLocation.Items[2].Selected = isVirtual;

                        if (isOffice)
                        {
                            string officeId = row["OfficeId"] != DBNull.Value ? row["OfficeId"].ToString() : "";
                            string locationId = row["LocationId"] != DBNull.Value ? row["LocationId"].ToString() : "";
                            string floorId = row["FloorId"] != DBNull.Value ? row["FloorId"].ToString() : "";
                            string roomId = row["MettingRoomId"] != DBNull.Value ? row["MettingRoomId"].ToString() : "";

                            CommonDataLoadDAL commonDataLoad = new CommonDataLoadDAL();

                            DataTable dtOffice = string.IsNullOrEmpty(compId) ? commonDataLoad.GetDDLSalaryLocation() : commonDataLoad.GetDDLSalaryLocationByCompany(compId);
                            if (dtOffice != null && dtOffice.Rows.Count > 0)
                            {
                                // dtOffice already includes a "Select..." placeholder row via GetDTforDDL() -
                                // do not insert a second one (see FetchOfficeOptions).
                                ddlOffice.DataSource = dtOffice;
                                ddlOffice.DataValueField = "Value";
                                ddlOffice.DataTextField = "TextField";
                                ddlOffice.DataBind();
                                if (!string.IsNullOrEmpty(officeId) && ddlOffice.Items.FindByValue(officeId) != null)
                                    ddlOffice.SelectedValue = officeId;
                            }

                            DataTable dtLoc = !string.IsNullOrEmpty(officeId) ? commonDataLoad.GetDDLJobLocation(officeId) : commonDataLoad.GetDDLJobLocation();
                            if (dtLoc != null && dtLoc.Rows.Count > 0)
                            {
                                ddlLocation.DataSource = dtLoc;
                                ddlLocation.DataValueField = "Value";
                                ddlLocation.DataTextField = "TextField";
                                ddlLocation.DataBind();
                                EnsureSelectPlaceholder(ddlLocation, "Select an Option");
                                if (!string.IsNullOrEmpty(locationId) && ddlLocation.Items.FindByValue(locationId) != null)
                                    ddlLocation.SelectedValue = locationId;
                            }

                            DataTable dtFloor = !string.IsNullOrEmpty(locationId) ? commonDataLoad.GetDDLFloor(locationId) : new DAL.MeetingMinorsDAL.FloorDal().GetInformationById("");
                            if (dtFloor != null && dtFloor.Rows.Count > 0)
                            {
                                ddlFloor.DataSource = dtFloor;
                                ddlFloor.DataValueField = "Value";
                                ddlFloor.DataTextField = "TextField";
                                ddlFloor.DataBind();
                                EnsureSelectPlaceholder(ddlFloor, "Select an Option");
                                if (!string.IsNullOrEmpty(floorId) && ddlFloor.Items.FindByValue(floorId) != null)
                                    ddlFloor.SelectedValue = floorId;
                            }

                            DataTable dtRoom = !string.IsNullOrEmpty(floorId) ? commonDataLoad.GetDDLMeetingRoom(floorId) : null;
                            if (dtRoom == null || dtRoom.Rows.Count == 0)
                            {
                                dtRoom = new DAL.MeetingMinorsDAL.MeetingRoomDal().GetEntryformation();
                                if (dtRoom != null && dtRoom.Columns.Contains("MeetingRoomName"))
                                {
                                    if (!dtRoom.Columns.Contains("Value")) dtRoom.Columns.Add("Value", typeof(string));
                                    if (!dtRoom.Columns.Contains("TextField")) dtRoom.Columns.Add("TextField", typeof(string));
                                    foreach (DataRow r in dtRoom.Rows)
                                    {
                                        r["Value"] = r["MeetingRoomId"].ToString();
                                        r["TextField"] = r["MeetingRoomName"].ToString();
                                    }
                                }
                            }
                            if (dtRoom != null && dtRoom.Rows.Count > 0)
                            {
                                ddlMettingRoomName.DataSource = dtRoom;
                                ddlMettingRoomName.DataValueField = "Value";
                                ddlMettingRoomName.DataTextField = "TextField";
                                ddlMettingRoomName.DataBind();
                                EnsureSelectPlaceholder(ddlMettingRoomName, "Select an Option");
                                if (!string.IsNullOrEmpty(roomId) && ddlMettingRoomName.Items.FindByValue(roomId) != null)
                                    ddlMettingRoomName.SelectedValue = roomId;
                            }

                            if (!string.IsNullOrEmpty(roomId))
                            {
                                txtCapacity.Text = FetchRoomCapacityText(roomId);
                            }
                        }
                        else if (isOuter)
                        {
                            if (dtMaster.Columns.Contains("Location") && row["Location"] != DBNull.Value)
                                txtLocation.Text = row["Location"].ToString();
                            if (dtMaster.Columns.Contains("LocationDescription") && row["LocationDescription"] != DBNull.Value)
                                txtDescription.Text = row["LocationDescription"].ToString();
                        }
                        else if (isVirtual)
                        {
                            if (dtMaster.Columns.Contains("Remarks") && row["Remarks"] != DBNull.Value)
                                txtRemarks.Text = row["Remarks"].ToString();
                        }

                        if (dtMaster.Columns.Contains("MailAddress") && row["MailAddress"] != DBNull.Value)
                            txtMailAddress.Text = row["MailAddress"].ToString();
                        if (dtMaster.Columns.Contains("MailBody") && row["MailBody"] != DBNull.Value)
                            txtMailBody.Text = row["MailBody"].ToString();
                        if (dtMaster.Columns.Contains("IsSendMail") && row["IsSendMail"] != DBNull.Value)
                            chkSendMail.Checked = Convert.ToBoolean(row["IsSendMail"]);
                    }
                }
                catch (Exception)
                {
                }

                DataTable ApprovalPath = AMeetingEntryDal.GetMeetingApprovalPathById(id_mastetID.Value);
                if (ApprovalPath.Rows.Count > 0)
                {
                    ViewState["gv_Details_App"] = ApprovalPath;
                    gv_ApprovalPathDetail.DataSource = ApprovalPath;
                    gv_ApprovalPathDetail.DataBind();



                    for (int i = 0; i < gv_ApprovalPathDetail.Rows.Count; i++)
                    {
                        DropDownList ddlSequenceList = (DropDownList)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("ddlSequenceList");


                        ddlSequenceList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select One..", String.Empty));
                        for (int k = 1; k < gv_ApprovalPathDetail.Rows.Count + 1; k++)
                        {
                            ddlSequenceList.Items.Insert(k, new ListItem(k.ToString()));
                        }
                    }


                    for (int i = 0; i < ApprovalPath.Rows.Count; i++)
                    {
                        DropDownList ddlSequenceList = (DropDownList)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("ddlSequenceList");
                        HiddenField hfSeq_No = (HiddenField)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("hfSeq_No");

                        ddlSequenceList.SelectedValue = hfSeq_No.Value;


                        HiddenField hfIsMinimumApprovalPerson = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("hfIsMinimumApprovalPerson"));
                        HiddenField hfCanEdit = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("hfCanEdit"));
                        HiddenField HiNotificationEmailApp = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("HiNotificationEmailApp"));
                        HiddenField hfNotificationSMSApp = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("hfNotificationSMSApp"));


                        CheckBox chkMimimumCount = (CheckBox)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("chkMimimumCount");

                        if (hfIsMinimumApprovalPerson.Value != "")
                        {
                            chkMimimumCount.Checked = Convert.ToBoolean(hfIsMinimumApprovalPerson.Value);

                        }


                        CheckBox chkIsEdit = (CheckBox)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("chkIsEdit");

                        if (hfCanEdit.Value != "")
                        {
                            chkIsEdit.Checked = Convert.ToBoolean(hfCanEdit.Value);

                        }
                        CheckBoxList chkNotificationApp = (CheckBoxList)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("chkNotificationApp");


                        if (HiNotificationEmailApp.Value != "")
                        {
                            chkNotificationApp.Items[0].Selected = Convert.ToBoolean(HiNotificationEmailApp.Value);

                        }

                        if (hfNotificationSMSApp.Value != "")
                        {
                            chkNotificationApp.Items[1].Selected = Convert.ToBoolean(hfNotificationSMSApp.Value);

                        }


                    }
                    lblstatus.Text = "";


                }
                else
                {
                    lblstatus.Text = "No Approval Path have been  selected.";

                }
                DataTable dtAgendaa = AMeetingEntryDal.GetAgendDataById(id_mastetID.Value);
                if (dtAgendaa.Rows.Count > 0)
                {
                   
                    gv_AgendaList.DataSource = dtAgendaa;
                    gv_AgendaList.DataBind();


                    for (int i = 0; i < gv_AgendaList.Rows.Count; i++)
                    {


                        HiddenField hfImplementationStatus = ((HiddenField)gv_AgendaList.Rows[i].FindControl("hfImplementationStatus"));
                        DropDownList ddlImplementationStatus = ((DropDownList)gv_AgendaList.Rows[i].Cells[2].FindControl("ddlImplementationStatus"));
                        ddlImplementationStatus.SelectedValue = hfImplementationStatus.Value;

                    }
                }


            }
            else
            {
                // Add mode (no MID): emit empty JSON so MeetingGridA/B hydrate with a blank row
                EmitGridAJson(true, new DataTable());

            }
        }
    }
    private void LoadInitialGrid()
    {
        DataTable dtDetails = new DataTable();

        dtDetails.Columns.Add("Agenda");
        dtDetails.Columns.Add("Remarks");

        dtDetails.Columns.Add("Presentor");
        dtDetails.Columns.Add("Observation");
        dtDetails.Columns.Add("Decision");
        dtDetails.Columns.Add("ImplementationStatus");


        DataRow dr = null;
        dr = dtDetails.NewRow();

        dr["Agenda"] = "";

        dr["Presentor"] = "";
        dr["Remarks"] = "";
        dr["Observation"] = "";
        dr["Decision"] = "";
        dr["ImplementationStatus"] = "";
 

        dtDetails.Rows.Add(dr);

        gv_AgendaList.DataSource = null;
        gv_AgendaList.DataBind();
        gv_AgendaList.DataSource = dtDetails;
        gv_AgendaList.DataBind();

    }


    public void ButtonVisible()
    {
        if (Session["Status"] != null)
        {
            if (Session["Status"].ToString() == "Add")
            {
                submitButton.Visible = true;
                //lbDraft.Visible = true;
            }
            else if (Session["Status"].ToString() == "Edit")
            {
                editButton.Visible = true;

                //lbDraft.Visible = true;

            }
            else if (Session["Status"].ToString() == "Delete")
            {
                delButton.Visible = true;
            }
            Session["Status"] = null;
        }
        else
        {

            Response.Redirect("MeetingStatusList.aspx");
        }


    }

    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();

    private void LoadDropDownList()
    {
        AMAsterDal.GetCompanyListIntoDropdown(ddlCompany);
        AMAsterDal.GetCompanyListIntoDropdown(ddlCompanyLocation);
        AMAsterDal.GetCategoryListIntoDropdown(ddlCategory);
        AMAsterDal.GetCompanyListIntoDropdownAll(ddlComSearch);
        ddlCompany.SelectedIndex = 1;
        ddlComSearch.SelectedIndex = 1;

        //using (DataTable dt = _commonDataLoad.GetDDLSalaryLocation())
        //{
        //    ddlOffice.DataSource = dt;
        //    ddlOffice.DataValueField = "Value";
        //    ddlOffice.DataTextField = "TextField";
        //    ddlOffice.DataBind();
        //}

        ddlCompany_OnSelectedIndexChanged(null, null);
        ddlComSearch_OnSelectedIndexChanged(null, null);

        MemberInfoDaL aMinors = new MemberInfoDaL();


         DataTable dtss = aMinors.loadMember("");

         if (dtss.Rows.Count > 0)
        {
            gv_loadGridView.DataSource = dtss;
            gv_loadGridView.DataBind();
        }
         else
         {
             gv_loadGridView.DataSource = null;
             gv_loadGridView.DataBind();
             
         }

    }
    protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDivision.SelectedValue != "")
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#exampleModal2').modal('show')", true);

            AMAsterDal.GetDepartmentByDivList(ddlDepartment, ddlDivision.SelectedValue);

        }
        else
        {

            ddlDepartment.Items.Clear();
        }
    }

    protected void gv_DocumentUpload_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;

        if ((gv.ShowHeader == true && gv.Rows.Count > 0)
            || (gv.ShowHeaderWhenEmpty == true))
        {
            //Force GridView to use <thead> instead of <tbody> - 11/03/2013 - MCR.
            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
    private bool docVali(HiddenField docFile)
    {
        if (docFile.Value == "")
        {
            aShowMessage.ShowMessageBox("Please Upload a Document", this);

            return false;
        }
        return true;

    }

    protected void brnAddDoc_OnClick(object sender, EventArgs e)
    {
        AddDocRow(hfDocFile, hfDocFileName, HyperLink2, "Notice", "MeetingGridDoc");
    }

    protected void brnAddDocMin_OnClick(object sender, EventArgs e)
    {
        AddDocRow(hfDocFileMin, hfDocFileNameMin, HyperLink2Min, "Minutes", "MeetingGridDocMin");
    }

    private void AddDocRow(HiddenField docFile, HiddenField docFileName, HyperLink downloadLink, string docType, string clientGridVar)
    {
        if (docVali(docFile))
        {
            // OCR runs client-side against OcrHandler.ashx after the row is added, so this
            // postback no longer blocks on Tesseract - ExtractedText is filled in asynchronously.
            var docRow = new
            {
                DocumentLink = "../UploadMeetingDocument/" + docFile.Value,
                FileName = docFileName.Value,
                DocumentNote = (string)null,
                ExtractedText = "",
                DocType = docType
            };

            string json = JsonConvert.SerializeObject(docRow);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "GridDoc_Add_" + Guid.NewGuid().ToString("N"),
                clientGridVar + ".addRow(" + json + "); requestDocOcr('" + HttpUtility.JavaScriptStringEncode(docFile.Value) + "', " + clientGridVar + ");", true);

            downloadLink.NavigateUrl = "";
            docFile.Value = "";
        }
    }

    protected void btnDocUp_OnClick(object sender, EventArgs e)
    {
        UploadDoc(FUDocument, HyperLink2);
    }

    protected void btnDocUpMin_OnClick(object sender, EventArgs e)
    {
        UploadDoc(FUDocumentMin, HyperLink2Min);
    }

    private void UploadDoc(FileUpload fileUpload, HyperLink downloadLink)
    {
        if (fileUpload.HasFile)
        {
            string _fileExt = System.IO.Path.GetExtension(fileUpload.FileName);
            string AdsFile = "Meeting_Mis_DOC_" + Guid.NewGuid().ToString() + Path.GetExtension(fileUpload.FileName);
            //  fileName = guid.ToString() + imageFileUpload.FileName;
            fileUpload.SaveAs(Server.MapPath("../UploadImg/") + AdsFile);
            downloadLink.NavigateUrl = "../UploadImg/" + AdsFile;
            downloadLink.Text = "Uploaded Successfully";
        }
        else
        {
            downloadLink.NavigateUrl = "";
        //    downloadLink.Text = "No File Uploaded";
        }
    }
    private bool AddMemberValidation()
    {
        if (gv_EmpListSearch.Rows.Count == 0)
        {

            AlertMessageBoxShow("Please select at least one employee !!!");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#exampleModal2').modal('show')", true);

            return false;
        }

        int totalCount = gv_EmpListSearch.Rows.Cast<GridViewRow>().Count(r => ((CheckBox)r.FindControl("chkSelect")).Checked);

        if (totalCount == 0)
        {
            aShowMessage.ShowMessageBox("Please Select Employee", this);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#exampleModal2').modal('show')", true);



            return false;
        }






        return true;
    }


    private bool AddMemberValidationApp()
    {
        if (gv_EmpListSearchAPP.Rows.Count == 0)
        {

            AlertMessageBoxShow("Please select at least one employee !!!");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#exampleModalAPPEmpp').modal('show')", true);

            return false;
        }

        int totalCount = gv_EmpListSearchAPP.Rows.Cast<GridViewRow>().Count(r => ((CheckBox)r.FindControl("chkSelectAPP")).Checked);

        if (totalCount == 0)
        {
            aShowMessage.ShowMessageBox("Please Select Employee", this);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#exampleModalAPPEmpp').modal('show')", true);



            return false;
        }






        return true;
    }



    private bool BordmemberMemberValidationApp()
    {
        if (gv_loadGridView.Rows.Count == 0)
        {

            AlertMessageBoxShow("Please select at least one employee !!!");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#exampleModalBoardMember').modal('show')", true);

            return false;
        }

        int totalCount = gv_loadGridView.Rows.Cast<GridViewRow>().Count(r => ((CheckBox)r.FindControl("BchkSelect")).Checked);

        if (totalCount == 0)
        {
            aShowMessage.ShowMessageBox("Please Select Employee", this);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#exampleModalBoardMember').modal('show')", true);



            return false;
        }






        return true;
    }


    public bool CheckBoardMemberApp()
    {
        for (int i = 0; i < gv_loadGridView.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)gv_loadGridView.Rows[i].Cells[0].FindControl("BchkSelect");
            for (int j = 0; j < gv_ApprovalPathDetail.Rows.Count; j++)
            {
                if (chkBoxRows.Checked)
                {
                    Label SSStxt_empId = (Label)gv_ApprovalPathDetail.Rows[j].FindControl("Applbl_EmpName");

                    Label EmpDI = (Label)gv_loadGridView.Rows[i].FindControl("BM_Name");

                    if (EmpDI.Text == SSStxt_empId.Text)
                    {


                        return false;

                    }

                }

            }

        }
        return true;
    }
    public bool CheckEmpListApp()
    {
        for (int i = 0; i < gv_EmpListSearchAPP.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)gv_EmpListSearchAPP.Rows[i].Cells[0].FindControl("chkSelectAPP");
            for (int j = 0; j < gv_ApprovalPathDetail.Rows.Count; j++)
            {
                if (chkBoxRows.Checked)
                {
                    Label SSStxt_empId = (Label)gv_ApprovalPathDetail.Rows[j].FindControl("Appbl_EmpMasterCode");

                    Label EmpDI = (Label)gv_EmpListSearchAPP.Rows[i].FindControl("lbl_EmpMasterCodeAPP");

                    if (EmpDI.Text == SSStxt_empId.Text)
                    {


                        return false;

                    }

                }

            }

        }
        return true;
    }
    public bool CheckEmpList()
    {
        // Grid A ("Add Employees") no longer lives server-side — its current EmpMasterCodes
        // are handed over via hfGridA_ExistingCodes by prepareGridAExistingCodes() (client JS)
        // right before this postback fires.
        string[] existingCodes = (hfGridA_ExistingCodes.Value ?? "")
            .Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < gv_EmpListSearch.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)gv_EmpListSearch.Rows[i].Cells[0].FindControl("chkSelect");
            if (!chkBoxRows.Checked) continue;

            Label EmpDI = (Label)gv_EmpListSearch.Rows[i].FindControl("lbl_EmpMasterCode");
            foreach (var code in existingCodes)
            {
                if (string.Equals(EmpDI.Text.Trim(), code.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }
        }
        return true;
    }

    private DataTable CreateDraftMemberTable()
    {
        DataTable table = new DataTable();
        table.Columns.Add("EmpInfoId");
        table.Columns.Add("EmpMasterCode");
        table.Columns.Add("EmpName");
        table.Columns.Add("Designation");
        return table;
    }

    private void BindDraftMemberList(DataTable table)
    {
        ViewState["DraftMemberList"] = table;
        gv_DraftMemberList.DataSource = table;
        gv_DraftMemberList.DataBind();
    }

    protected void btnStageMembers_OnClick(object sender, EventArgs e)
    {
        if (!AddMemberValidation())
        {
            return;
        }

        DataTable draft = ViewState["DraftMemberList"] as DataTable ?? CreateDraftMemberTable();
        var draftCodes = new HashSet<string>(draft.AsEnumerable()
            .Select(row => row["EmpMasterCode"].ToString()), StringComparer.OrdinalIgnoreCase);
        // Grid A ("Add Employees") no longer lives server-side — its current EmpMasterCodes
        // are handed over via hfGridA_ExistingCodes by prepareGridAExistingCodes() (client JS)
        // right before this postback fires.
        var savedCodes = new HashSet<string>(
            (hfGridA_ExistingCodes.Value ?? "").Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(c => c.Trim()),
            StringComparer.OrdinalIgnoreCase);

        foreach (GridViewRow row in gv_EmpListSearch.Rows)
        {
            CheckBox selected = row.FindControl("chkSelect") as CheckBox;
            if (selected == null || !selected.Checked)
            {
                continue;
            }

            HiddenField empInfoId = row.FindControl("hfEmpInfoId") as HiddenField;
            Label employeeCode = row.FindControl("lbl_EmpMasterCode") as Label;
            Label employeeName = row.FindControl("lbl_EmpName") as Label;
            Label designation = row.FindControl("lbl_Designation") as Label;
            string code = employeeCode == null ? string.Empty : employeeCode.Text.Trim();

            if (string.IsNullOrWhiteSpace(code) || draftCodes.Contains(code) || savedCodes.Contains(code))
            {
                continue;
            }

            DataRow draftRow = draft.NewRow();
            draftRow["EmpInfoId"] = empInfoId == null ? string.Empty : empInfoId.Value;
            draftRow["EmpMasterCode"] = code;
            draftRow["EmpName"] = employeeName == null ? string.Empty : employeeName.Text;
            draftRow["Designation"] = designation == null ? string.Empty : designation.Text;
            draft.Rows.Add(draftRow);
            draftCodes.Add(code);
        }

        BindDraftMemberList(draft);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#exampleModal2').modal('show')", true);
    }

    protected void btnRemoveDraftMember_OnClick(object sender, EventArgs e)
    {
        LinkButton button = (LinkButton)sender;
        GridViewRow gridRow = (GridViewRow)button.NamingContainer;
        DataTable draft = ViewState["DraftMemberList"] as DataTable;

        if (draft != null && gridRow.RowIndex >= 0 && gridRow.RowIndex < draft.Rows.Count)
        {
            string removedCode = draft.Rows[gridRow.RowIndex]["EmpMasterCode"].ToString();
            draft.Rows.RemoveAt(gridRow.RowIndex);

            foreach (GridViewRow searchRow in gv_EmpListSearch.Rows)
            {
                Label code = searchRow.FindControl("lbl_EmpMasterCode") as Label;
                CheckBox selected = searchRow.FindControl("chkSelect") as CheckBox;
                if (code != null && selected != null &&
                    string.Equals(code.Text.Trim(), removedCode, StringComparison.OrdinalIgnoreCase))
                {
                    selected.Checked = false;
                }
            }

            BindDraftMemberList(draft);
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#exampleModal2').modal('show')", true);
    }

    protected void btnSubmitDraftMembers_OnClick(object sender, EventArgs e)
    {
        DataTable draft = ViewState["DraftMemberList"] as DataTable;
        if (draft == null || draft.Rows.Count == 0)
        {
            AlertMessageBoxShow("Please add at least one employee to the draft list.");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#exampleModal2').modal('show')", true);
            return;
        }

        gv_EmpListSearch.DataSource = draft;
        gv_EmpListSearch.DataBind();
        foreach (GridViewRow row in gv_EmpListSearch.Rows)
        {
            CheckBox selected = row.FindControl("chkSelect") as CheckBox;
            if (selected != null)
            {
                selected.Checked = true;
            }
        }

        btnAddToListEmp_OnClick(sender, e);
        ViewState["DraftMemberList"] = null;
        gv_DraftMemberList.DataSource = null;
        gv_DraftMemberList.DataBind();
    }

    protected void btnAddToListEmp_OnClick(object sender, EventArgs e)
    {
        if (AddMemberValidation())
        {
            if (CheckEmpList())
            {
                // Grid A ("Add Employees") state now lives client-side; only the newly
                // picked rows are appended (MeetingGridA.appendRows), not the whole table.
                DataTable aDataTable = new DataTable();
                aDataTable.Columns.Add("EmpInfoId");
                aDataTable.Columns.Add("EmpMasterCode");
                aDataTable.Columns.Add("EmpName");
                aDataTable.Columns.Add("Designation");
                aDataTable.Columns.Add("Type");
                aDataTable.Columns.Add("NotificationEmail");
                aDataTable.Columns.Add("NotificationSMS");
                aDataTable.Columns.Add("Position");
                aDataTable.Columns.Add("IsBoardMember");
                aDataTable.Columns.Add("BMemberSetupDetailsID");

                DataRow dataRow;

                for (int i = 0; i < gv_EmpListSearch.Rows.Count; i++)
                {
                    CheckBox chkSelect = (CheckBox)gv_EmpListSearch.Rows[i].Cells[0].FindControl("chkSelect");
                    HiddenField hfEmpInfoId = ((HiddenField)gv_EmpListSearch.Rows[i].FindControl("hfEmpInfoId"));
                    Label lbl_EmpMasterCode = (Label)gv_EmpListSearch.Rows[i].FindControl("lbl_EmpMasterCode");
                    Label lbl_EmpName = (Label)gv_EmpListSearch.Rows[i].FindControl("lbl_EmpName");
                    Label lbl_Designation = (Label)gv_EmpListSearch.Rows[i].FindControl("lbl_Designation");

                    if (chkSelect.Checked)
                    {
                        dataRow = aDataTable.NewRow();
                        dataRow["IsBoardMember"] = "0";
                        dataRow["BMemberSetupDetailsID"] = "0";
                        dataRow["EmpInfoId"] = hfEmpInfoId.Value;
                        dataRow["EmpMasterCode"] = lbl_EmpMasterCode.Text;
                        dataRow["EmpName"] = lbl_EmpName.Text;
                        dataRow["Designation"] = lbl_Designation.Text;
                        // Everyone added via this search modal is a real employee record.
                        dataRow["Type"] = "Employee";
                        dataRow["NotificationEmail"] = "";
                        dataRow["NotificationSMS"] = "";
                        dataRow["Position"] = "";

                        aDataTable.Rows.Add(dataRow);
                    }
                }

                EmitGridAJson(false, aDataTable);
            }
            else
            {
                AlertMessageBoxShow("Already Exist !!!");
            }
        }

    }
    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        var chkBoxHeader = (CheckBox)gv_EmpListSearch.HeaderRow.FindControl("chkSelectAll");

        for (int i = 0; i < gv_EmpListSearch.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)gv_EmpListSearch.Rows[i].Cells[0].FindControl("chkSelect");
            chkBoxRows.Checked = chkBoxHeader.Checked;
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#exampleModal2').modal('show')", true);

    }

    ShowMessage aShowMessage = new ShowMessage();

    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#exampleModal2').modal('show')", true);
        if (ddlCompany.SelectedIndex > 0)
        {
            LoadEMPInfo();
        }
        else
        {
            ddlCompany.Focus();
            aShowMessage.ShowMessageBox("Please Select This !!!", this);
        }
    }

    private string GenerateParameter()
    {
        string parameter = " ";

        if (ddlComSearch.SelectedIndex > 0)
        {
            parameter = parameter + "  and    com.CompanyId = '" + ddlComSearch.SelectedValue + "'";
        }

        if (ddlDivision.SelectedIndex > 0)
        {
            parameter = parameter + "  and    div.DivisionId = '" + ddlDivision.SelectedValue + "'";
        }

        if (ddlDepartment.SelectedIndex > 0)
        {
            parameter = parameter + "  and    Dpt.DepartmentId = '" + ddlDepartment.SelectedValue + "'";
        }

        if (ddlEmp.SelectedIndex > 0)
        {
            parameter = parameter + "  and   e.EmpInfoId = '" + ddlEmp.SelectedValue + "'";
        }
        return parameter;
    }

    private string GenerateParameterApp()
    {
        string parameter = " ";

        if (ddlCompany.SelectedIndex > 0)
        {
            parameter = parameter + "  and    com.CompanyId = '" + ddlCompany.SelectedValue + "'";
        }

        if (ddlDivisionAPP.SelectedIndex > 0)
        {
            parameter = parameter + "  and    div.DivisionId = '" + ddlDivisionAPP.SelectedValue + "'";
        }

        if (ddlDepartmentAPP.SelectedIndex > 0)
        {
            parameter = parameter + "  and    Dpt.DepartmentId = '" + ddlDepartmentAPP.SelectedValue + "'";
        }
        return parameter;
    }

    private void LoadEMPInfo()
    {
        DataTable jobCreationInfos = new DataTable();
        jobCreationInfos = AMAsterDal.GetEMpInfos(GenerateParameter());
        if (jobCreationInfos.Rows.Count > 0)
        {

            gv_EmpListSearch.DataSource = jobCreationInfos;
            gv_EmpListSearch.DataBind();



        }
        else
        {
            gv_EmpListSearch.DataSource = null;
            gv_EmpListSearch.DataBind();
            AlertMessageBoxShow("No Data Found!!!");

        }
    }


    private void LoadEMPInfoAappEmp()
    {
        DataTable jobCreationInfos = new DataTable();
        jobCreationInfos = AMAsterDal.GetEMpInfos(GenerateParameterApp());
        if (jobCreationInfos.Rows.Count > 0)
        {

            gv_EmpListSearchAPP.DataSource = jobCreationInfos;
            gv_EmpListSearchAPP.DataBind();



        }
        else
        {
            gv_EmpListSearchAPP.DataSource = null;
            gv_EmpListSearchAPP.DataBind();
            AlertMessageBoxShow("No Data Found!!!");

        }
    }
    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }

    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedValue != "")
        {
            using (DataTable dt = AMAsterDal.GetDDLComDivision(ddlCompany.SelectedValue))
            {
                 

                ddlDivisionAPP.DataSource = dt;
                ddlDivisionAPP.DataValueField = "Value";
                ddlDivisionAPP.DataTextField = "TextField";
                ddlDivisionAPP.DataBind();
            }
            if (Session["UserTypeId"].ToString() == "3" ||
               Session["UserTypeId"].ToString() == "4")
            {
                using (DataTable dt2 = AMAsterDal.GetDDLRoutingPath(ddlCompany.SelectedValue))
                {
                    rbRoutingPath.DataSource = dt2;
                    rbRoutingPath.DataValueField = "Value";
                    rbRoutingPath.DataTextField = "TextField";
                    rbRoutingPath.DataBind();
                }
            }
            else
            {
                using (DataTable dt2 = AMAsterDal.GetDDLRoutingPathDepartment(ddlCompany.SelectedValue, Session["DepartmentId"].ToString()))
                {
                    rbRoutingPath.DataSource = dt2;
                    rbRoutingPath.DataValueField = "Value";
                    rbRoutingPath.DataTextField = "TextField";
                    rbRoutingPath.DataBind();
                }
            }

            //    using (DataTable dt2 = AMeetingEntryDal.GetEmpInfobyID(ddlCompany.SelectedValue))
            //    {

            //        for (int i = 0; i < gv_AgendaList.Rows.Count; i++)
            //        {
            //DropDownList ddlPresentor= ((DropDownList)gv_AgendaList.Rows[i].Cells[2].FindControl("ddlPresentor"));

            //ddlPresentor.DataSource = dt2;
            //ddlPresentor.DataValueField = "EmpInfoId";
            //ddlPresentor.DataTextField = "EmpName";
            //ddlPresentor.DataBind();
            //ddlPresentor.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
            //ddlPresentor.SelectedIndex = 0;
            //        }
            //    }

            // Reload member list based on the new company selection
            DataTable jobCreationInfos = AMeetingEntryDal.GetEmpMemberInfoByCategory(ddlCompany.SelectedValue);
            EmitGridBJson(jobCreationInfos);

            PopulateMailAddressForCompany(ddlCompany.SelectedValue);

            // Preserve the Add Employees rows across this postback by re-emitting GridA
            // from the hidden field the JS serialised before the form submitted.
            string gridAJson = hfGridA_Json.Value;
            if (!string.IsNullOrWhiteSpace(gridAJson) && gridAJson != "[]" && gridAJson != "null")
            {
                string scriptA = "MeetingGridA.hydrate(" + gridAJson + "); MeetingGridA.render();";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "GridA_CompanyRestore", scriptA, true);
            }
            else
            {
                EmitGridAJson(true, new DataTable());
            }
        }
        else
        {
            //for (int i = 0; i < gv_AgendaList.Rows.Count; i++)
            //{
            //    DropDownList ddlPresentor = ((DropDownList) gv_AgendaList.Rows[i].Cells[2].FindControl("ddlPresentor"));
            //    ddlPresentor.Items.Clear();
            //}
           
             rbRoutingPath.Items.Clear();
        }
    }

    protected void btnAgenaListAdd_OnClick(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;

        DataTable aTable = new DataTable();
        aTable.Columns.Add("Agenda");
        aTable.Columns.Add("Presentor");
        aTable.Columns.Add("Remarks");
        aTable.Columns.Add("Observation");
        aTable.Columns.Add("Decision");
        aTable.Columns.Add("ImplementationStatus");


        DataRow dr;



        for (int i = 0; i < gv_AgendaList.Rows.Count; i++)
        {
            dr = aTable.NewRow();
            dr["Agenda"] =
                   ((TextBox)gv_AgendaList.Rows[i].Cells[2].FindControl("txtAgenda")).Text.Trim();


            dr["Remarks"] =
                ((TextBox)gv_AgendaList.Rows[i].Cells[2].FindControl("txtRemarks")).Text.Trim();

            dr["Presentor"] =
                ((HiddenField)gv_AgendaList.Rows[i].Cells[2].FindControl("hfPresentor")).Value.Trim();


          
            dr["Presentor"] =
             ((DropDownList)gv_AgendaList.Rows[i].Cells[2].FindControl("ddlPresentor")).SelectedValue.Trim();


            dr["Observation"] =
              ((TextBox)gv_AgendaList.Rows[i].Cells[2].FindControl("txtObservation")).Text.Trim();
            dr["Decision"] =
               ((TextBox)gv_AgendaList.Rows[i].Cells[2].FindControl("txtDecision")).Text.Trim();


            dr["ImplementationStatus"] =
               ((HiddenField)gv_AgendaList.Rows[i].Cells[2].FindControl("hfImplementationStatus")).Value.Trim();



            dr["ImplementationStatus"] =
             ((DropDownList)gv_AgendaList.Rows[i].Cells[2].FindControl("ddlImplementationStatus")).SelectedValue.Trim();


            aTable.Rows.Add(dr);

            if (rowIndex == i)
            {
                dr = aTable.NewRow();
                dr["Agenda"] = "";
                dr["Presentor"] = "";
                dr["Remarks"] = "";
                dr["Observation"] = "";
                dr["Decision"] = "";
                dr["ImplementationStatus"] = "";

                aTable.Rows.Add(dr);
            }
        }

        //Session["table"] = (DataTable)aTable;
        gv_AgendaList.DataSource = null;
        gv_AgendaList.DataBind();
        gv_AgendaList.DataSource = aTable;
        gv_AgendaList.DataBind();

        //using (DataTable dt2 = AMeetingEntryDal.GetEmpInfobyID(ddlCompany.SelectedValue))
        //{

        //    for (int i = 0; i < gv_AgendaList.Rows.Count; i++)
        //    {
        //        DropDownList ddlPresentor = ((DropDownList)gv_AgendaList.Rows[i].Cells[2].FindControl("ddlPresentor"));

        //        ddlPresentor.DataSource = dt2;
        //        ddlPresentor.DataValueField = "EmpInfoId";
        //        ddlPresentor.DataTextField = "EmpName";
        //        ddlPresentor.DataBind();
        //        ddlPresentor.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
        //        ddlPresentor.SelectedIndex = 0;



        //        ddlPresentor.SelectedValue = ((HiddenField)gv_AgendaList.Rows[i].Cells[1].FindControl("hfPresentor"))
        //                 .Value;
        //    }
        //}

        for (int i = 0; i < gv_AgendaList.Rows.Count; i++)
        {


            HiddenField hfImplementationStatus = ((HiddenField)gv_AgendaList.Rows[i].FindControl("hfImplementationStatus"));
            DropDownList ddlImplementationStatus = ((DropDownList)gv_AgendaList.Rows[i].Cells[2].FindControl("ddlImplementationStatus"));
            ddlImplementationStatus.SelectedValue = hfImplementationStatus.Value;
           
        }

    }

    protected void btnAgenaLisRemove_OnClick(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;

        DataTable aTable = new DataTable();
        aTable.Columns.Add("Agenda");
        aTable.Columns.Add("Presentor");
        aTable.Columns.Add("Remarks");
        aTable.Columns.Add("Observation");
        aTable.Columns.Add("Decision");
        aTable.Columns.Add("ImplementationStatus");


        DataRow dr;

        for (int i = 0; i < gv_AgendaList.Rows.Count; i++)
        {
            if (rowIndex != i)
            {
                dr = aTable.NewRow();
                dr["Remarks"] =
               ((TextBox)gv_AgendaList.Rows[i].Cells[2].FindControl("txtRemarks")).Text.Trim();
                dr["Agenda"] =
                  ((TextBox)gv_AgendaList.Rows[i].Cells[2].FindControl("txtAgenda")).Text.Trim();

                dr["Presentor"] =
                    ((HiddenField)gv_AgendaList.Rows[i].Cells[2].FindControl("hfPresentor")).Value.Trim();
                dr["Presentor"] =
           ((DropDownList)gv_AgendaList.Rows[i].Cells[2].FindControl("ddlPresentor")).SelectedValue.Trim();

                dr["Observation"] =
        ((TextBox)gv_AgendaList.Rows[i].Cells[2].FindControl("txtObservation")).Text.Trim();
                dr["Decision"] =
                   ((TextBox)gv_AgendaList.Rows[i].Cells[2].FindControl("txtDecision")).Text.Trim();
                dr["ImplementationStatus"] =
           ((HiddenField)gv_AgendaList.Rows[i].Cells[2].FindControl("hfImplementationStatus")).Value.Trim();



                dr["ImplementationStatus"] =
                 ((DropDownList)gv_AgendaList.Rows[i].Cells[2].FindControl("ddlImplementationStatus")).SelectedValue.Trim();


                aTable.Rows.Add(dr);
            }
        }

        if (aTable.Rows.Count > 0)
        {
            gv_AgendaList.DataSource = null;
            gv_AgendaList.DataBind();
            gv_AgendaList.DataSource = aTable;
            gv_AgendaList.DataBind();



        }

        for (int i = 0; i < gv_AgendaList.Rows.Count; i++)
        {


            HiddenField hfImplementationStatus = ((HiddenField)gv_AgendaList.Rows[i].FindControl("hfImplementationStatus"));
            DropDownList ddlImplementationStatus = ((DropDownList)gv_AgendaList.Rows[i].Cells[2].FindControl("ddlImplementationStatus"));
            ddlImplementationStatus.SelectedValue = hfImplementationStatus.Value;

        }
        //using (DataTable dt2 = AMeetingEntryDal.GetEmpInfobyID(ddlCompany.SelectedValue))
        //{

        //    for (int i = 0; i < gv_AgendaList.Rows.Count; i++)
        //    {
        //        DropDownList ddlPresentor = ((DropDownList)gv_AgendaList.Rows[i].Cells[2].FindControl("ddlPresentor"));

        //        ddlPresentor.DataSource = dt2;
        //        ddlPresentor.DataValueField = "EmpInfoId";
        //        ddlPresentor.DataTextField = "EmpName";
        //        ddlPresentor.DataBind();
        //        ddlPresentor.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
        //        ddlPresentor.SelectedIndex = 0;



        //        ddlPresentor.SelectedValue = ((HiddenField)gv_AgendaList.Rows[i].Cells[1].FindControl("hfPresentor"))
        //                 .Value;
        //    }
        //}
    }

    protected void btnOkayRoutingPath_OnClick(object sender, EventArgs e)
    {
        string grade = "";
        for (int i = 0; i < rbRoutingPath.Items.Count; i++)
        {
            if (rbRoutingPath.Items[i].Selected)
            {
                grade = rbRoutingPath.Items[i].Value;
            }
        }

        if (grade == "")
        {
            AlertMessageBoxShow("Select Routing Path!!!");
        }
        else
        {

            DataTable jobCreationInfos = new DataTable();
            jobCreationInfos = AMAsterDal.GetEMpInfoFromRoutingPath(grade);
            if (jobCreationInfos.Rows.Count > 0)
            {
                lblstatus.Text = "";
                ViewState["gv_Details_App"] = jobCreationInfos;
                gv_ApprovalPathDetail.DataSource = jobCreationInfos;
                gv_ApprovalPathDetail.DataBind();


            



                for (int i = 0; i < gv_ApprovalPathDetail.Rows.Count; i++)
                {
                    DropDownList ddlSequenceList = (DropDownList)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("ddlSequenceList");


                    ddlSequenceList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select One..", String.Empty));
                    for (int k = 1; k < gv_ApprovalPathDetail.Rows.Count + 1; k++)
                    {
                        ddlSequenceList.Items.Insert(k, new ListItem(k.ToString()));
                    }
                }


                for (int i = 0; i < jobCreationInfos.Rows.Count; i++)
                {
                    DropDownList ddlSequenceList = (DropDownList)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("ddlSequenceList");
                    HiddenField hfSeq_No = (HiddenField)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("hfSeq_No");

                    ddlSequenceList.SelectedValue = hfSeq_No.Value;

                }


            }
            else
            {
                gv_ApprovalPathDetail.DataSource = null;
                gv_ApprovalPathDetail.DataBind();
                lblstatus.Text = "No Approval Path have been  selected.";


            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#exampleModal').modal('hide')", true);

        }




    }

    protected void Appbtn_DetailsRemove_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        if (ViewState["gv_Details_App"] != null)
        {
            DataTable dt = (DataTable)ViewState["gv_Details_App"];
            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count > 0)
            {
                //Store the current data in ViewState for future reference  
                ViewState["gv_Details_App"] = dt;
                //Re bind the GridView for the updated data  
                gv_ApprovalPathDetail.DataSource = dt;
                gv_ApprovalPathDetail.DataBind();
            }
            else
            {
                ViewState["gv_Details_App"] = null;
                //Re bind the GridView for the updated data  
                gv_ApprovalPathDetail.DataSource = null;
                gv_ApprovalPathDetail.DataBind();
            }


            for (int i = 0; i < gv_ApprovalPathDetail.Rows.Count; i++)
            {
                DropDownList ddlSequenceList = (DropDownList)gv_ApprovalPathDetail.Rows[i].FindControl("ddlSequenceList");


                ddlSequenceList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select One.....", String.Empty));
                for (int k = 1; k < gv_ApprovalPathDetail.Rows.Count + 1; k++)
                {
                    ddlSequenceList.Items.Insert(k, new ListItem(k.ToString()));
                }
            }

            for (int i = 0; i < gv_ApprovalPathDetail.Rows.Count; i++)
            {
                DropDownList ddlSequenceList = (DropDownList)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("ddlSequenceList");
                HiddenField hfSeq_No = (HiddenField)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("hfSeq_No");

                ddlSequenceList.SelectedValue = hfSeq_No.Value;




                HiddenField hfIsMinimumApprovalPerson = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("hfIsMinimumApprovalPerson"));
                HiddenField hfCanEdit = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("hfCanEdit"));
                HiddenField HiNotificationEmailApp = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("HiNotificationEmailApp"));
                HiddenField hfNotificationSMSApp = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("hfNotificationSMSApp"));


                CheckBox chkMimimumCount = (CheckBox)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("chkMimimumCount");

                if (hfIsMinimumApprovalPerson.Value != "")
                {
                    chkMimimumCount.Checked = true;

                }


                CheckBox chkIsEdit = (CheckBox)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("chkIsEdit");

                if (hfCanEdit.Value != "")
                {
                    chkIsEdit.Checked = true;

                }
                CheckBoxList chkNotificationApp = (CheckBoxList)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("chkNotificationApp");


                if (HiNotificationEmailApp.Value != "")
                {
                    chkNotificationApp.Items[0].Selected = true;

                }

                if (hfNotificationSMSApp.Value != "")
                {
                    chkNotificationApp.Items[1].Selected = true;

                }

            }
        }


        if (gv_ApprovalPathDetail.Rows.Count == 0)
        {
            lblstatus.Text = "No Approval Path have been  selected.";
        }
        else
        {
            lblstatus.Text = "";
        }
    }


    protected void ddlDivisionAPP_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDivisionAPP.SelectedValue != "")
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#exampleModalAPPEmpp').modal('show')", true);

            AMAsterDal.GetDepartmentByDivList(ddlDepartmentAPP, ddlDivisionAPP.SelectedValue);

        }
        else
        {

            ddlDepartmentAPP.Items.Clear();
        }
    }

    protected void btnSearchAPP_OnClick(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#exampleModalAPPEmpp').modal('show')", true);
        if (ddlCompany.SelectedIndex > 0)
        {
            LoadEMPInfoAappEmp();
        }
        else
        {
            ddlCompany.Focus();
            aShowMessage.ShowMessageBox("Please Select This !!!", this);
        }
    }

    protected void chkSelectAllAPP_CheckedChanged(object sender, EventArgs e)
    {
        var chkBoxHeader = (CheckBox)gv_EmpListSearchAPP.HeaderRow.FindControl("chkSelectAllAPP");

        for (int i = 0; i < gv_EmpListSearchAPP.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)gv_EmpListSearchAPP.Rows[i].Cells[0].FindControl("chkSelectAPP");
            chkBoxRows.Checked = chkBoxHeader.Checked;
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#exampleModalAPPEmpp').modal('show')", true);
    }

    protected void btnAddToListEmpAPP_OnClick(object sender, EventArgs e)
    {
        if (AddMemberValidationApp())
        {
            if (CheckEmpListApp())
            {

                DataTable aDataTable = new DataTable();
                aDataTable.Columns.Add("EmpInfoId");
                aDataTable.Columns.Add("EmpMasterCode");
                aDataTable.Columns.Add("EmpName");
                aDataTable.Columns.Add("Designation");
                aDataTable.Columns.Add("Seq_No");
                aDataTable.Columns.Add("IsMinimumApprovalPerson");
                aDataTable.Columns.Add("CanEdit");
                aDataTable.Columns.Add("NotificationEmail");
                aDataTable.Columns.Add("NotificationSMS");
               



                DataRow dataRow = null;

                for (int i = 0; i < gv_EmpListSearchAPP.Rows.Count; i++)
                {
                    CheckBox chkSelect = (CheckBox)gv_EmpListSearchAPP.Rows[i].Cells[0].FindControl("chkSelectAPP");
                    HiddenField hfEmpInfoId = ((HiddenField)gv_EmpListSearchAPP.Rows[i].FindControl("hfEmpInfoIdAPP"));
                    Label lbl_EmpMasterCode = (Label)gv_EmpListSearchAPP.Rows[i].FindControl("lbl_EmpMasterCodeAPP");
                    Label lbl_EmpName = (Label)gv_EmpListSearchAPP.Rows[i].FindControl("lbl_EmpNameAPP");
                    Label lbl_Designation = (Label)gv_EmpListSearchAPP.Rows[i].FindControl("lbl_DesignationAPP");

                    if (chkSelect.Checked)
                    {
                        //  if (HasDCStoreId(Convert.ToInt32(loadGridView.DataKeys[i][0].ToString())))
                        {



                            dataRow = aDataTable.NewRow();

                            dataRow["EmpInfoId"] = hfEmpInfoId.Value;

                            dataRow["EmpMasterCode"] = lbl_EmpMasterCode.Text;
                            dataRow["EmpName"] = lbl_EmpName.Text;
                            dataRow["Designation"] = lbl_Designation.Text;

                            dataRow["Seq_No"] = "";
                            dataRow["IsMinimumApprovalPerson"] = "";
                            dataRow["CanEdit"] = "";
                            dataRow["NotificationEmail"] = "";
                            dataRow["NotificationSMS"] = "";
                          

                            aDataTable.Rows.Add(dataRow);
                        }
                    }
                }
                for (int i = 0; i < gv_ApprovalPathDetail.Rows.Count; i++)
                {


                    HiddenField ShfEmpInfoId = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("ApphfEmpInfoId"));
                    HiddenField hfSeq_No = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("hfSeq_No"));
                    Label Slbl_EmpMasterCode = (Label)gv_ApprovalPathDetail.Rows[i].FindControl("Appbl_EmpMasterCode");
                    Label Slbl_EmpName = (Label)gv_ApprovalPathDetail.Rows[i].FindControl("Applbl_EmpName");
                    Label Slbl_Designation = (Label)gv_ApprovalPathDetail.Rows[i].FindControl("Applbl_Designation");



                    HiddenField hfIsMinimumApprovalPerson = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("hfIsMinimumApprovalPerson"));
                    HiddenField hfCanEdit = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("hfCanEdit"));
                    HiddenField HiNotificationEmailApp = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("HiNotificationEmailApp"));
                    HiddenField hfNotificationSMSApp = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("hfNotificationSMSApp"));



                    dataRow = aDataTable.NewRow();
                    dataRow["EmpInfoId"] = ShfEmpInfoId.Value;

                    dataRow["EmpMasterCode"] = Slbl_EmpMasterCode.Text;
                    dataRow["EmpName"] = Slbl_EmpName.Text;
                    dataRow["Designation"] = Slbl_Designation.Text;
                    dataRow["Seq_No"] = hfSeq_No.Value;


                    dataRow["IsMinimumApprovalPerson"] = hfIsMinimumApprovalPerson.Value;
                    dataRow["CanEdit"] = hfCanEdit.Value;
                    dataRow["NotificationEmail"] = HiNotificationEmailApp.Value;
                    dataRow["NotificationSMS"] = hfNotificationSMSApp.Value;
                          






                    aDataTable.Rows.Add(dataRow);
                }
                ViewState["gv_Details_App"] = aDataTable;
                gv_ApprovalPathDetail.DataSource = aDataTable;
                gv_ApprovalPathDetail.DataBind();

                if (gv_ApprovalPathDetail.Rows.Count==0)
                {
                    lblstatus.Text = "No Approval Path have been  selected.";
                }
                else
                {
                    lblstatus.Text = "";
                }


                for (int i = 0; i < gv_ApprovalPathDetail.Rows.Count; i++)
                {
                    DropDownList ddlSequenceList = (DropDownList)gv_ApprovalPathDetail.Rows[i].FindControl("ddlSequenceList");


                    ddlSequenceList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select One.....", String.Empty));
                    for (int k = 1; k < gv_ApprovalPathDetail.Rows.Count + 1; k++)
                    {
                        ddlSequenceList.Items.Insert(k, new ListItem(k.ToString()));
                    }
                }

                for (int i = 0; i < gv_ApprovalPathDetail.Rows.Count; i++)
                {
                    DropDownList ddlSequenceList = (DropDownList)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("ddlSequenceList");
                    HiddenField hfSeq_No = (HiddenField)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("hfSeq_No");

                    ddlSequenceList.SelectedValue = hfSeq_No.Value;




                    HiddenField hfIsMinimumApprovalPerson = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("hfIsMinimumApprovalPerson"));
                    HiddenField hfCanEdit = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("hfCanEdit"));
                    HiddenField HiNotificationEmailApp = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("HiNotificationEmailApp"));
                    HiddenField hfNotificationSMSApp = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("hfNotificationSMSApp"));


                    CheckBox chkMimimumCount = (CheckBox)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("chkMimimumCount");

                    if (hfIsMinimumApprovalPerson.Value!="")
                    {
                        chkMimimumCount.Checked =true;
                         
                    }


                    CheckBox chkIsEdit = (CheckBox)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("chkIsEdit");

                    if (hfCanEdit.Value != "")
                    {
                        chkIsEdit.Checked = true;

                    }
                    CheckBoxList chkNotificationApp = (CheckBoxList)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("chkNotificationApp");


                    if (HiNotificationEmailApp.Value != "")
                    {
                        chkNotificationApp.Items[0].Selected = true;

                    }

                    if (hfNotificationSMSApp.Value != "")
                    {
                        chkNotificationApp.Items[1].Selected = true;

                    }

                }
            }
            else
            {
                AlertMessageBoxShow("Already Exist !!!");
            }
        }
        
    }

    protected void rbLocation_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbLocation.Items[0].Selected == true)
        {
            ddlOffice.Enabled = true;
            ddlLocation.Enabled = true;
            ddlFloor.Enabled = true;
            ddlMettingRoomName.Enabled = true;
            txtCapacity.ReadOnly = false;



            txtLocation.ReadOnly = true;
            txtDescription.ReadOnly = true;
            txtRemarks.ReadOnly = true;

            txtLocation.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtRemarks.Text = string.Empty;

            if (string.IsNullOrEmpty(ddlCompanyLocation.SelectedValue) && !string.IsNullOrEmpty(ddlCompany.SelectedValue))
            {
                try { ddlCompanyLocation.SelectedValue = ddlCompany.SelectedValue; } catch { }
            }

            if (ddlOffice.Items.Count <= 1)
            {
                string compLocId = !string.IsNullOrEmpty(ddlCompanyLocation.SelectedValue) ? ddlCompanyLocation.SelectedValue : ddlCompany.SelectedValue;
                DataTable dtOffices = new CommonDataLoadDAL().GetDDLSalaryLocationByCompany(compLocId);
                if (dtOffices == null || dtOffices.Rows.Count == 0)
                {
                    dtOffices = new CommonDataLoadDAL().GetDDLSalaryLocation();
                }
                if (dtOffices != null && dtOffices.Rows.Count > 0)
                {
                    // dtOffices already includes a "Select..." placeholder row via GetDTforDDL() -
                    // do not insert a second one (see FetchOfficeOptions).
                    ddlOffice.DataSource = dtOffices;
                    ddlOffice.DataValueField = "Value";
                    ddlOffice.DataTextField = "TextField";
                    ddlOffice.DataBind();
                }
            }
        }

        if (rbLocation.Items[1].Selected == true)
        {
            ddlOffice.Enabled = false;
            ddlLocation.Enabled = false;
            ddlFloor.Enabled = false;
            ddlMettingRoomName.Enabled = false;
            txtCapacity.ReadOnly = true;

            txtLocation.ReadOnly = false;
            txtDescription.ReadOnly = false;
            txtRemarks.ReadOnly = true;



            ddlOffice.SelectedValue = null;
            ddlLocation.Items.Clear();
            ddlFloor.Items.Clear();
            ddlMettingRoomName.Items.Clear();
            txtCapacity.Text = string.Empty;
            txtRemarks.Text = string.Empty;

           



        }

        if (rbLocation.Items[2].Selected == true)
        {
            ddlOffice.Enabled = false;
            ddlLocation.Enabled = false;
            ddlFloor.Enabled = false;
            ddlMettingRoomName.Enabled = false;
            txtCapacity.ReadOnly = true;

            txtLocation.ReadOnly = true;
            txtDescription.ReadOnly = true;
            txtRemarks.ReadOnly = false;

            ddlOffice.SelectedValue = null;
            ddlLocation.Items.Clear();
            ddlFloor.Items.Clear();
            ddlMettingRoomName.Items.Clear();
            txtCapacity.Text = string.Empty;
            txtLocation.Text = string.Empty;
            txtDescription.Text = string.Empty;
        }
    }

    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        SaveUpdateInfo("");
    }

    public bool Validation()
    {

       
        if (ddlCompany.SelectedIndex <= 0 || string.IsNullOrWhiteSpace(ddlCompany.SelectedValue) || ddlCompany.SelectedValue == "-1")
        {
            aShowMessage.ShowMessageBox("please fill out Company", this);
            ddlCompany.Focus();
            return false;
        }



        if (txtTitle.Text == "")
        {
            aShowMessage.ShowMessageBox("please fill out Title", this);
            txtTitle.Focus();
            return false;
        }

        int meetingNoValue;
        if (string.IsNullOrWhiteSpace(txtMeetingNo.Text) || !int.TryParse(txtMeetingNo.Text.Trim(), out meetingNoValue))
        {
            aShowMessage.ShowMessageBox("please fill out a valid Board Meeting Number", this);
            txtMeetingNo.Focus();
            return false;
        }

 

        if (txtMeetingDate.Text == "")
        {
            aShowMessage.ShowMessageBox("please fill out Meeting Date", this);
            txtMeetingDate.Focus();
            return false;
        }

        if (rbLocation.SelectedIndex < 0)
        {
            aShowMessage.ShowMessageBox("please select a Meeting Location Type", this);
            rbLocation.Focus();
            return false;
        }

        if (string.IsNullOrWhiteSpace(hfGridDoc_Json.Value) || hfGridDoc_Json.Value == "[]" || hfGridDoc_Json.Value == "null")
        {
            aShowMessage.ShowMessageBox("Please Add Document Information", this);
            return false;
        }


        for (int i = 0; i < gv_ApprovalPathDetail.Rows.Count; i++)
        {

            DropDownList ddlSequenceList =
                  (DropDownList)gv_ApprovalPathDetail.Rows[i].FindControl("ddlSequenceList");


            if (ddlSequenceList.SelectedValue == "")
            {
                aShowMessage.ShowMessageBox("please fill out Sequence", this);
                ddlSequenceList.Focus();
                return false;
            }
        }



        return true;
    }



    public bool CheckSeq()
    {

        for (int i = 0; i < gv_ApprovalPathDetail.Rows.Count; i++)
        {
            for (int j = 0; j < gv_ApprovalPathDetail.Rows.Count; j++)
            {
                if (i != j)
                {
                    DropDownList ddlSequenceList =
                 (DropDownList)gv_ApprovalPathDetail.Rows[i].FindControl("ddlSequenceList");
                    DropDownList ddlSequenceList2 =
                 (DropDownList)gv_ApprovalPathDetail.Rows[j].FindControl("ddlSequenceList");
                    if (ddlSequenceList.SelectedValue ==
                        ddlSequenceList2.SelectedValue)
                    {

                        return false;
                    }
                }
            }
        }

      
        return true;
    }
    private void SaveUpdateInfo(string acst)
    {
        if (Validation() == true)
        {
            if (CheckSeq())
            {
                string KeySearch = "";
                string MasterSearch = "";
                string DocumentSearch = "";
                string AgendaSearch = "";
                string MemberSearch = "";
                List<MiscellaneousInfoDocumentDAO> DocList =
                    JsonConvert.DeserializeObject<List<MiscellaneousInfoDocumentDAO>>(hfGridDoc_Json.Value) ?? new List<MiscellaneousInfoDocumentDAO>();

                foreach (var DocA in DocList)
                {
                    DocumentSearch = DocumentSearch + (DocA.DocumentNote ?? "").Trim() + " ";
                }


                List<MeetingInfoAgendaDAO> AgendaList = new List<MeetingInfoAgendaDAO>();

                for (int i = 0; i < gv_AgendaList.Rows.Count; i++)
                {
                    TextBox txtAgenda = (TextBox) gv_AgendaList.Rows[i].FindControl("txtAgenda");
                    TextBox txtObservation = (TextBox)gv_AgendaList.Rows[i].FindControl("txtObservation");
                    TextBox txtDecision = (TextBox)gv_AgendaList.Rows[i].FindControl("txtDecision");
                    TextBox txtRemarks = (TextBox)gv_AgendaList.Rows[i].FindControl("txtRemarks");
                    DropDownList ddlPresentor = (DropDownList)gv_AgendaList.Rows[i].FindControl("ddlPresentor");
                    DropDownList ddlImplementationStatus = (DropDownList)gv_AgendaList.Rows[i].FindControl("ddlImplementationStatus");


                    MeetingInfoAgendaDAO AgendaA = new MeetingInfoAgendaDAO();

                    AgendaA.Agenda = txtAgenda.Text.Trim().ToString();
                    AgendaA.Remarks = txtRemarks.Text.Trim().ToString();
                    AgendaA.Observation = txtObservation.Text.Trim().ToString();
                    AgendaA.Decision = txtDecision.Text.Trim().ToString();
                    AgendaSearch = AgendaSearch + txtAgenda.Text.Trim().ToString() + " ";

                    AgendaA.PresentorId =
                        Convert.ToInt32(ddlPresentor.SelectedIndex > 0 ? ddlPresentor.SelectedValue : null);

                    AgendaA.ImplementationStatus =
                        ddlImplementationStatus.SelectedValue;
                    AgendaList.Add(AgendaA);
                }

                int _RefEmpId = 0;
                int _RefSeqNo = 0;
                int _RefMinAppCount = 0;

                List<MiscellaneousInfoRoutingPathDAO> RoutingPath = new List<MiscellaneousInfoRoutingPathDAO>();

                for (int i = 0; i < gv_ApprovalPathDetail.Rows.Count; i++)
                {
                    CheckBox chkMimimumCount = (CheckBox) gv_ApprovalPathDetail.Rows[i].FindControl("chkMimimumCount");
                    DropDownList ddlSequenceList =
                        (DropDownList) gv_ApprovalPathDetail.Rows[i].FindControl("ddlSequenceList");


                    HiddenField ApphfEmpInfoId =
                        (HiddenField) gv_ApprovalPathDetail.Rows[i].FindControl("ApphfEmpInfoId");


                    CheckBox chkIsEdit = (CheckBox) gv_ApprovalPathDetail.Rows[i].FindControl("chkIsEdit");
                    CheckBoxList chkNotificationApp =
                        (CheckBoxList) gv_ApprovalPathDetail.Rows[i].FindControl("chkNotificationApp");



                    MiscellaneousInfoRoutingPathDAO Routing = new MiscellaneousInfoRoutingPathDAO();


                    Routing.IsMinimumApprovalPerson = chkMimimumCount.Checked;
                    Routing.Seq_No = Convert.ToInt32(ddlSequenceList.SelectedValue);
                    Routing.EmpInfoId = Convert.ToInt32(ApphfEmpInfoId.Value.Trim());

                    Routing.CanEdit = chkIsEdit.Checked;
                    Routing.IsEmailNotification = chkNotificationApp.Items[0].Selected;
                    Routing.IsSMSNotification = chkNotificationApp.Items[1].Selected;
                    if (chkMimimumCount.Checked)
                    {
                        _RefMinAppCount++;
                        if (Routing.Seq_No == 1)
                        {
                            _RefEmpId = Convert.ToInt32(ApphfEmpInfoId.Value.Trim());
                            _RefSeqNo = (int) Routing.Seq_No;
                        }
                    }

                    RoutingPath.Add(Routing);
                }





                // Grid A ("Add Employees") / Grid B ("Members List") rows are posted as JSON
                // from the client-side arrays (see hfGridA_Json/hfGridB_Json + handleMeetingGridsSave()
                // in the .aspx) instead of being read back from a server-side GridView.
                List<MeetingInfoDetailDAO> MeetingInfoDetailList =
                    JsonConvert.DeserializeObject<List<MeetingInfoDetailDAO>>(hfGridA_Json.Value) ?? new List<MeetingInfoDetailDAO>();

                foreach (var MeetingInfoDetail in MeetingInfoDetailList)
                {
                    MeetingInfoDetail.IsBoardMember = "0";
                    if (MeetingInfoDetail.Type != "Employee")
                    {
                        MeetingInfoDetail.EmpInfoId = null;
                    }
                    // Notification UI is hidden on this grid — always false, same as before.
                    MeetingInfoDetail.NotificationEmail = false;
                    MeetingInfoDetail.NotificationSMS = false;

                    MemberSearch = MemberSearch + (MeetingInfoDetail.EmpName ?? "").Trim() + " ";
                }

                List<MeetingInfoDetailDAO> MeetingInfoDetailList2 =
                    JsonConvert.DeserializeObject<List<MeetingInfoDetailDAO>>(hfGridB_Json.Value) ?? new List<MeetingInfoDetailDAO>();

                foreach (var MeetingInfoDetail in MeetingInfoDetailList2)
                {
                    MeetingInfoDetail.Type = "";
                    MeetingInfoDetail.EmpInfoId = null;
                    MeetingInfoDetail.EmpMasterCode = "";
                    MeetingInfoDetail.IsBoardMember = "1";
                    MeetingInfoDetail.NotificationEmail = false;
                    MeetingInfoDetail.NotificationSMS = false;

                    MemberSearch = MemberSearch + (MeetingInfoDetail.EmpName ?? "").Trim() + " ";
                }


                MeetingEntryDAO aMaster = new MeetingEntryDAO();

                aMaster.MeetingInfoID = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);

                aMaster.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                string classfic = "";
                //if (ddlClassification.SelectedValue != "")
                //{
                //    classfic = ddlCategory.SelectedItem.Text.Trim();
                //}

                MasterSearch = MasterSearch + txtTitle.Text.Trim() + " "
                               +
                               classfic + " "
                               + txtMeetingDate.Text.Trim() + " " + txtStartTime.Text.Trim() + "" +
                               " " + txtEndTime.Text.Trim();
                aMaster.Title = txtTitle.Text.Trim();

                aMaster.CategoryID = Convert.ToInt32(ddlCategory.SelectedIndex > 0 ? ddlCategory.SelectedValue : null);

                aMaster.MeetingPurpose = null;
                aMaster.MeetingNo = txtMeetingNo.Text.Trim();
                aMaster.Classification = null; // ddlClassification.SelectedIndex > 0 ? ddlClassification.SelectedValue : null;
                DateTime parsedMeetingDate;
                string[] dateFormats = new string[] { 
                    "dd-MMM-yyyy hh:mm tt", "dd-MMM-yyyy h:mm tt", "d-MMM-yyyy hh:mm tt", "d-MMM-yyyy h:mm tt",
                    "dd-MMM-yyyy HH:mm", "d-MMM-yyyy HH:mm", "dd-MMM-yyyy", "d-MMM-yyyy",
                    "yyyy-MM-dd HH:mm:ss", "yyyy-MM-dd HH:mm", "yyyy-MM-dd"
                };

                if (!string.IsNullOrWhiteSpace(txtMeetingDate.Text) &&
                    (DateTime.TryParseExact(txtMeetingDate.Text.Trim(), dateFormats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out parsedMeetingDate) ||
                     DateTime.TryParse(txtMeetingDate.Text.Trim(), out parsedMeetingDate)))
                {
                    aMaster.MeetingDate = parsedMeetingDate;

                    if (!string.IsNullOrWhiteSpace(txtStartTime.Text))
                    {
                        DateTime dtStart;
                        TimeSpan tsStart;
                        if (TimeSpan.TryParse(txtStartTime.Text.Trim(), out tsStart))
                            aMaster.StartTime = tsStart;
                        else if (DateTime.TryParse(txtStartTime.Text.Trim(), out dtStart))
                            aMaster.StartTime = dtStart.TimeOfDay;
                    }
                    else if (parsedMeetingDate.TimeOfDay != TimeSpan.Zero)
                    {
                        aMaster.StartTime = parsedMeetingDate.TimeOfDay;
                    }
                }
                else
                {
                    aMaster.MeetingDate = null;
                }

                if (!string.IsNullOrWhiteSpace(txtStartTime.Text) && aMaster.StartTime == null)
                {
                    DateTime dtStart;
                    TimeSpan tsStart;
                    if (TimeSpan.TryParse(txtStartTime.Text.Trim(), out tsStart))
                        aMaster.StartTime = tsStart;
                    else if (DateTime.TryParse(txtStartTime.Text.Trim(), out dtStart))
                        aMaster.StartTime = dtStart.TimeOfDay;
                }

                if (!string.IsNullOrWhiteSpace(txtEndTime.Text))
                {
                    DateTime dtEnd;
                    TimeSpan tsEnd;
                    if (TimeSpan.TryParse(txtEndTime.Text.Trim(), out tsEnd))
                        aMaster.EndTime = tsEnd;
                    else if (DateTime.TryParse(txtEndTime.Text.Trim(), out dtEnd))
                        aMaster.EndTime = dtEnd.TimeOfDay;
                }



                string strSubComm = !string.IsNullOrEmpty(hfSelectedSubCommittee.Value) ? hfSelectedSubCommittee.Value : ddlSubCommittee.SelectedValue;
                int subCommVal = 0;
                int.TryParse(strSubComm, out subCommVal);
                aMaster.SubCommitteeId = subCommVal > 0 ? subCommVal : (Convert.ToInt32(ddlSubCommittee.SelectedIndex > 0 ? ddlSubCommittee.SelectedValue : null));


                aMaster.IsOfficePremisis = rbLocation.Items[0].Selected;
                aMaster.IsOuterPremisis = rbLocation.Items[1].Selected;
                aMaster.IsVirtualMeeting = rbLocation.Items[2].Selected;


                if (rbLocation.Items[0].Selected == true)
                {

                    string strOffice = !string.IsNullOrEmpty(hfSelectedOffice.Value) ? hfSelectedOffice.Value : Request.Form[ddlOffice.UniqueID];
                    if (string.IsNullOrEmpty(strOffice)) strOffice = Request.Form[ddlOffice.ClientID];
                    if (string.IsNullOrEmpty(strOffice)) strOffice = ddlOffice.SelectedValue;

                    string strLoc = !string.IsNullOrEmpty(hfSelectedLocation.Value) ? hfSelectedLocation.Value : Request.Form[ddlLocation.UniqueID];
                    if (string.IsNullOrEmpty(strLoc)) strLoc = Request.Form[ddlLocation.ClientID];
                    if (string.IsNullOrEmpty(strLoc)) strLoc = ddlLocation.SelectedValue;

                    string strFloor = !string.IsNullOrEmpty(hfSelectedFloor.Value) ? hfSelectedFloor.Value : Request.Form[ddlFloor.UniqueID];
                    if (string.IsNullOrEmpty(strFloor)) strFloor = Request.Form[ddlFloor.ClientID];
                    if (string.IsNullOrEmpty(strFloor)) strFloor = ddlFloor.SelectedValue;

                    string strRoom = !string.IsNullOrEmpty(hfSelectedRoom.Value) ? hfSelectedRoom.Value : Request.Form[ddlMettingRoomName.UniqueID];
                    if (string.IsNullOrEmpty(strRoom)) strRoom = Request.Form[ddlMettingRoomName.ClientID];
                    if (string.IsNullOrEmpty(strRoom)) strRoom = ddlMettingRoomName.SelectedValue;

                    int oId = 0, lId = 0, fId = 0, rId = 0;
                    int.TryParse(strOffice, out oId);
                    int.TryParse(strLoc, out lId);
                    int.TryParse(strFloor, out fId);
                    int.TryParse(strRoom, out rId);

                    aMaster.OfficeId = oId > 0 ? oId : (int?)null;
                    aMaster.LocationId = lId > 0 ? lId : (int?)null;
                    aMaster.FloorId = fId > 0 ? fId : (int?)null;
                    aMaster.MettingRoomId = rId > 0 ? rId : (int?)null;
                    aMaster.Location = null;
                    aMaster.LocationDescription = null;

                }

                if (rbLocation.Items[1].Selected == true)
                {

                    aMaster.OfficeId = null;
                    aMaster.LocationId = null;
                    aMaster.FloorId = null;
                    aMaster.MettingRoomId = null;
                    aMaster.Location = txtLocation.Text;
                    aMaster.LocationDescription = txtDescription.Text;


                }

                if (rbLocation.Items[2].Selected == true)
                {

                    aMaster.OfficeId = null;
                    aMaster.LocationId = null;
                    aMaster.FloorId = null;
                    aMaster.MettingRoomId = null;
                    aMaster.Location = null;
                    aMaster.LocationDescription = null;
                    aMaster.Remarks = txtRemarks.Text;


                }

                KeySearch = MasterSearch + " " + MemberSearch + " " + DocumentSearch + " " + AgendaSearch;
                aMaster.KeySearch = KeySearch;

                aMaster.MailAddress = string.IsNullOrWhiteSpace(txtMailAddress.Text) ? null : txtMailAddress.Text.Trim();
                aMaster.MailBody = string.IsNullOrWhiteSpace(txtMailBody.Text) ? null : txtMailBody.Text;
                aMaster.IsSendMail = chkSendMail.Checked;




                aMaster.RefMinAppCountCheck = 0;


                 
                    if(rbEntryType.SelectedValue == "Notice")
                {
                    aMaster.IsNotice = true;
                }
                    else
                    {
                        aMaster.IsNotice = false;

                    }
              
                aMaster.Isapproved = false;

                

                if (acst == "Drafted")
                {
                    aMaster.ActionStatus = acst;
                    aMaster.RefEmpId = 0;
                    aMaster.RefSeqNo = 0;
                    aMaster.RefMinAppCount = 0;
                }
                else
                {
                    aMaster.RefEmpId = _RefEmpId;
                    aMaster.RefSeqNo = _RefSeqNo;
                    aMaster.RefMinAppCount = _RefMinAppCount;


                    if (gv_ApprovalPathDetail.Rows.Count==0)
                    {
                        aMaster.Isapproved = true;
                        aMaster.RefEmpId = 0;
                        aMaster.ActionStatus = "Approved";
                    }
                    else
                    {
                        aMaster.ActionStatus = "Initiator";
                    }


                  
                }
                bool result = false;
                int pk = 0;

                try
                {
                    using (var scope = new System.Transactions.TransactionScope())
                    {
                        pk = AMeetingEntryDal.SaveMaster(aMaster, Session["UserId"].ToString());

                        if (pk > 0)
                        {

                            result = true;
                            AMeetingEntryDal.SaveDetails(MeetingInfoDetailList, pk, 0);
                            AMeetingEntryDal.SaveDetails(MeetingInfoDetailList2, pk, 1);
                            AMeetingEntryDal.SaveRoutingPathDetails(RoutingPath, pk);
                            AMeetingEntryDal.SaveDocumentDetails(DocList, pk);
                            AMeetingEntryDal.SaveAgendaDetails(AgendaList, pk);


                            if (gv_ApprovalPathDetail.Rows.Count > 0)
                            {
                                if (Session["EmpInfoId"].ToString() != "")
                                {

                                    if (acst != "Drafted")
                                    {


                                        //del Log
                                        if (aMaster.MeetingInfoID > 0)
                                        {
                                            bool status = AMeetingEntryDal.AuditTrailLogById(pk.ToString(), "Edit");
                                        }
                                        else
                                        {
                                            bool status = AMeetingEntryDal.AuditTrailLogById(pk.ToString(), "Initial");

                                        }


                                        if (gv_ApprovalPathDetail.Rows.Count > 0)
                                        {

                                            MeetingInfoAppLogIdDAO appLogDaoa = new MeetingInfoAppLogIdDAO();

                                            appLogDaoa.ActionStatus = "Drafted";
                                            appLogDaoa.ApprovedDate = DateTime.Now;
                                            appLogDaoa.ApprovedBy = Convert.ToInt32(Session["UserId"].ToString());
                                            appLogDaoa.PreEmpInfoId = Convert.ToInt32(0);
                                            appLogDaoa.ForEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                                            appLogDaoa.MeetingInfoID = pk;

                                            appLogDaoa.Comments = "";


                                            int idd = AMeetingEntryDal.SavAppLog(appLogDaoa);


                                            //DataTable dtempdata =
                                            //    AMAsterDal.GetEmpRoutingPath(pk.ToString());
                                            MeetingInfoAppLogIdDAO appLogDao = new MeetingInfoAppLogIdDAO();
                                            {



                                                appLogDao.ActionStatus = "Initiator";
                                                appLogDao.ApprovedDate = DateTime.Now;
                                                appLogDao.ApprovedBy = Convert.ToInt32(Session["UserId"].ToString());
                                                appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                                                appLogDao.ForEmpInfoId = Convert.ToInt32(_RefEmpId);
                                                appLogDao.MeetingInfoID = pk;

                                                appLogDao.Comments = "";


                                            }
                                            ;
                                            int iddddd = AMeetingEntryDal.SavAppLog(appLogDao);
                                            //                            SenMailForApprved(appLogDao.ForEmpInfoId, " Increment Approval ", @"  <br/> Dear Sir, <br/>
                                            //An Increment is waiting for your approval. <br/><br/>
                                            // please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
                                            //");

                                        }
                                    }
                                }
                            }


                        }
                        else
                        {
                            result = false;
                        }

                        if (result)
                        {
                            scope.Complete();
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Trace.TraceError("Meeting save transaction failed: " + ex);
                    result = false;
                }

                if (result == true)
                {
                    bool isSendingMail = chkSendMail.Checked;

                    if (isSendingMail)
                    {
                        // Sent only after the whole transaction above (master, details, routing
                        // path, documents, agenda) has committed - never off unsaved/rolled-back data.
                        string mailSubject = (rbEntryType.SelectedValue == "Minutes" ? "Meeting Minutes: " : "Meeting Notice: ") + txtTitle.Text;
                        SendMailToAddress(txtMailAddress.Text, mailSubject, txtMailBody.Text, DocList);
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "ShowSaveLoadingModal",
                        "showDataSavingLoadingModal('MeetingStatusList.aspx', " + (isSendingMail ? "true" : "false") + ");",
                        true);
                }
                else
                {
                    AlertMessageBoxShow("Operation Failed");
                    RestoreClientGridsAfterValidationFailure();
                }
            }
            else
            {
                AlertMessageBoxShow("Sequence No Can not be Same");
                RestoreClientGridsAfterValidationFailure();
            }
        }
        else
        {
            RestoreClientGridsAfterValidationFailure();
        }
    }





    public static bool SenMailForApprved(int forEmpID, string mSubject, string mBody)
    {



        string ForMailAddress = "";
        using (var db = new HRIS_SMCEntities())
        {
            var GetMailAddress = (from t in db.tblEmpGeneralInfoes
                                  where t.EmpInfoId == forEmpID
                                  select t).FirstOrDefault();

            if (GetMailAddress != null)
            {
                ForMailAddress = GetMailAddress.OfficialEmail;
            }



        }

        if (ForMailAddress != "")
        {
            try
            {
                // Set TLS 1.2 (Office 365 requires this)
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                using (SmtpClient smtpClient = new SmtpClient("smtp.office365.com", 587))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.UseDefaultCredentials = false;

                    // Use Office 365 credentials
                    smtpClient.Credentials = new NetworkCredential("no-reply@smc-bd.org", "vfwzmbxprdmqhhln");

                    // Set timeout (in milliseconds)
                    smtpClient.Timeout = 20000;

                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress("no-reply@smc-bd.org");
                        mailMessage.IsBodyHtml = true;
                        mailMessage.To.Add(ForMailAddress);
                        mailMessage.Subject = mSubject;
                        mailMessage.Body =
                   "<div style='background-color: #DFF0D8; border-style: solid; border-color: #39B3D7; color: black; padding: 25px; border-radius: 15px 50px 30px 5px;'> <br/>" +
                   WebUtility.HtmlDecode(mBody)
                   +
                   "</div>";
                        mailMessage.IsBodyHtml = true;

                        smtpClient.Send(mailMessage);

                    }
                }
            }
            catch (Exception ex)
            {

                if (ex.InnerException != null)
                {

                }
            }





            System.Threading.Thread.Sleep(100);
        }


        return true;
    }

    // Company-wise member emails, same filter MemberInformationView.aspx uses (loadMember(criteria)
    // with a parameterized CompanyId), so txtMailAddress can be auto-filled per selected company
    // while still staying a free-text, comma-separated field the user can edit further.
    private string GetCompanyMemberEmails(string companyId)
    {
        int companyIdInt;
        if (!int.TryParse(companyId, out companyIdInt))
            return string.Empty;

        DataTable dt = aMinors.loadMember(new MemberInformationSearchCriteria { CompanyId = companyIdInt });
        if (dt == null || !dt.Columns.Contains("Email"))
            return string.Empty;

        var emails = dt.AsEnumerable()
            .Select(row => row["Email"] == DBNull.Value ? string.Empty : row["Email"].ToString().Trim())
            .Where(email => !string.IsNullOrWhiteSpace(email));

        return string.Join(", ", emails.Distinct(StringComparer.OrdinalIgnoreCase));
    }

    private void PopulateMailAddressForCompany(string companyId)
    {
        var addresses = new List<string>();

        string userEmail = GetOfficialEmailForCurrentUser();
        if (!string.IsNullOrWhiteSpace(userEmail))
            addresses.Add(userEmail);

        string memberEmails = GetCompanyMemberEmails(companyId);
        if (!string.IsNullOrWhiteSpace(memberEmails))
            addresses.AddRange(memberEmails.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(e => e.Trim()));

        txtMailAddress.Text = string.Join(", ", addresses.Distinct(StringComparer.OrdinalIgnoreCase));
    }

    private string GetOfficialEmailForCurrentUser()
    {
        if (Session["EmpInfoId"] == null || string.IsNullOrWhiteSpace(Session["EmpInfoId"].ToString()))
            return string.Empty;

        int empInfoId;
        if (!int.TryParse(Session["EmpInfoId"].ToString(), out empInfoId))
            return string.Empty;

        using (var db = new HRIS_SMCEntities())
        {
            var emp = (from t in db.tblEmpGeneralInfoes
                       where t.EmpInfoId == empInfoId
                       select t).FirstOrDefault();

            return emp != null ? (emp.OfficialEmail ?? string.Empty) : string.Empty;
        }
    }

    private static void SendMailToAddress(string toAddress, string mSubject, string mBody, IEnumerable<MiscellaneousInfoDocumentDAO> attachments)
    {
        if (string.IsNullOrWhiteSpace(toAddress))
            return;

        try
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

            using (SmtpClient smtpClient = new SmtpClient("smtp.office365.com", 587))
            {
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("no-reply@smc-bd.org", "vfwzmbxprdmqhhln");
                smtpClient.Timeout = 20000;

                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress("no-reply@smc-bd.org");

                    string[] recipients = toAddress.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string recipient in recipients)
                    {
                        if (!string.IsNullOrWhiteSpace(recipient))
                            mailMessage.To.Add(recipient.Trim());
                    }

                    // Preserve the body exactly as typed (including line breaks) instead of
                    // decoding it as if it already contained HTML - txtMailBody is plain text.
                    string formattedBody = WebUtility.HtmlEncode(mBody ?? string.Empty)
                        .Replace("\r\n", "<br/>")
                        .Replace("\n", "<br/>");

                    mailMessage.Subject = mSubject;
                    mailMessage.Body =
                        "<div style='background-color: #DFF0D8; border: 2px solid #39B3D7; color: black; padding: 25px; border-radius: 15px;'>" +
                        "<h3 style='color:#2E6DA4;'>" + WebUtility.HtmlEncode(mSubject) + "</h3>" +
                        formattedBody +
                        "<br/><br/><p style='color:#555;'>This is an automated notification from HRIS.</p>"  
                        ;
                    mailMessage.IsBodyHtml = true;

                    if (attachments != null)
                    {
                        foreach (var doc in attachments)
                        {
                            if (doc == null || string.IsNullOrWhiteSpace(doc.DocumentLink))
                                continue;

                            string physicalPath = HttpContext.Current.Server.MapPath(doc.DocumentLink);
                            if (!File.Exists(physicalPath))
                                continue;

                            string displayName = string.IsNullOrWhiteSpace(doc.FileName)
                                ? Path.GetFileName(physicalPath)
                                : doc.FileName;

                            mailMessage.Attachments.Add(new Attachment(physicalPath) { Name = displayName });
                        }
                    }

                    smtpClient.Send(mailMessage);
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.TraceError("Meeting entry notification mail failed: " + ex);
        }
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        SaveUpdateInfo("");
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {
        if (id_mastetID.Value != "")
        {
            // int Id = Convert.ToInt32(Session["UserId"].ToString());
            //Int32 DEl_ID = SaveChangesMasterDelete(Id);


            bool status = AMeetingEntryDal.AuditTrailLogById(id_mastetID.Value, "Delete");
            bool ss = AMeetingEntryDal.DeleteMaster(id_mastetID.Value);
            if (ss)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
            "alert",
            "alert('Operation Successful...');window.location ='MeetingStatusList.aspx';",
            true);
            }
            else
            {
                AlertMessageBoxShow("Operation Faild!!!!");
            }


        }
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("MeetingStatusList.aspx");
    }

    protected void ddlOffice_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlOffice.SelectedIndex > 0)
        {
            using (DataTable dt = _commonDataLoad.GetDDLJobLocation(ddlOffice.SelectedValue))
            {
                ddlLocation.DataSource = dt;
                ddlLocation.DataValueField = "Value";
                ddlLocation.DataTextField = "TextField";
                ddlLocation.DataBind();

            }
        }
        else
        {
            ddlLocation.Items.Clear();
            ddlFloor.Items.Clear();
            ddlMettingRoomName.Items.Clear();
            txtCapacity.Text = "";
        }
    }

    protected void ddlLocation_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocation.SelectedIndex > 0)
        {
            using (DataTable dt = _commonDataLoad.GetDDLFloor(ddlLocation.SelectedValue))
            {
                ddlFloor.DataSource = dt;
                ddlFloor.DataValueField = "Value";
                ddlFloor.DataTextField = "TextField";
                ddlFloor.DataBind();
            }
        }

        else
        {
            ddlFloor.Items.Clear();
            ddlMettingRoomName.Items.Clear();
            txtCapacity.Text = "";
        }
    }

    protected void ddlFloor_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFloor.SelectedIndex > 0)
        {
            using (DataTable dt = _commonDataLoad.GetDDLMeetingRoom(ddlFloor.SelectedValue))
            {
                ddlMettingRoomName.DataSource = dt;
                ddlMettingRoomName.DataValueField = "Value";
                ddlMettingRoomName.DataTextField = "TextField";
                ddlMettingRoomName.DataBind();
            }
        }

        else
        {
          
            ddlMettingRoomName.Items.Clear();
            txtCapacity.Text = "";
        }
    }

    protected void ddlMettingRoomName_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMettingRoomName.SelectedIndex > 0)
        {
            DataTable dtMaster = AMeetingEntryDal.GetCapacityDataByMeetingId(ddlMettingRoomName.SelectedValue);
            if (dtMaster.Rows.Count > 0)
            {
                txtCapacity.Text = dtMaster.Rows[0]["MeetingRoomCapacity"].ToString();
            }
        }
        else
        {

            
            txtCapacity.Text = "";
        }
    }

    protected void chkNotificationApp_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow) (((CheckBoxList) sender).Parent.Parent)).RowIndex;

        CheckBoxList chkNotificationApp =
            ((CheckBoxList) gv_ApprovalPathDetail.Rows[rowIndex].FindControl("chkNotificationApp"));

        HiddenField HiNotificationEmailApp =
            ((HiddenField) gv_ApprovalPathDetail.Rows[rowIndex].FindControl("HiNotificationEmailApp"));
        HiddenField hfNotificationSMSApp =
            ((HiddenField) gv_ApprovalPathDetail.Rows[rowIndex].FindControl("hfNotificationSMSApp"));

        HiNotificationEmailApp.Value = chkNotificationApp.Items[0].Selected.ToString();
        hfNotificationSMSApp.Value = chkNotificationApp.Items[1].Selected.ToString();


    }


    protected void chkIsEdit_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;

        CheckBox chkIsEdit = ((CheckBox)gv_ApprovalPathDetail.Rows[rowIndex].FindControl("chkIsEdit"));

        HiddenField hfCanEdit = ((HiddenField)gv_ApprovalPathDetail.Rows[rowIndex].FindControl("hfCanEdit"));


        hfCanEdit.Value = chkIsEdit.Checked.ToString();
        
    }

    protected void chkMimimumCount_OnCheckedChanged(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;

        CheckBox chkMimimumCount = ((CheckBox)gv_ApprovalPathDetail.Rows[rowIndex].FindControl("chkMimimumCount"));

        HiddenField hfIsMinimumApprovalPerson = ((HiddenField)gv_ApprovalPathDetail.Rows[rowIndex].FindControl("hfIsMinimumApprovalPerson"));


        hfIsMinimumApprovalPerson.Value = chkMimimumCount.Checked.ToString();
    }
    protected void ddlComSearch_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDivision.Items.Clear();
        ddlDepartment.Items.Clear();
        ddlEmp.Items.Clear();
        if (ddlComSearch.SelectedValue != "")
        {
            using (DataTable dt = AMAsterDal.GetDDLComDivision(ddlComSearch.SelectedValue))
            {
                ddlDivision.DataSource = dt;
                ddlDivision.DataValueField = "Value";
                ddlDivision.DataTextField = "TextField";
                ddlDivision.DataBind();
            }


            using (DataTable dt = AMAsterDal.GetDDLEmpInfo(ddlComSearch.SelectedValue))
            {
                ddlEmp.DataSource = dt;
                ddlEmp.DataValueField = "Value";
                ddlEmp.DataTextField = "TextField";
                ddlEmp.DataBind();
            }
        }
        else
        {
            ddlDivision.Items.Clear();
            ddlDepartment.Items.Clear();
        }

    }
    protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DivSubCommitte.Visible = false;
        ddlSubCommittee.Items.Clear();

        string effectiveCategory = ddlCategory.SelectedValue == "" ? "1" : ddlCategory.SelectedValue;

        if (effectiveCategory == "1")
        {
            DataTable jobCreationInfos = AMeetingEntryDal.GetEmpMemberInfoByCategory(ddlCompany.SelectedValue);
            if (jobCreationInfos.Rows.Count > 0)
            {
                EmitGridBJson(jobCreationInfos);
            }
            else
            {
                EmitGridBJson(new DataTable());
            }
        }
        else
        {
            EmitGridBJson(new DataTable());
        }

        if (ddlCategory.SelectedValue != "" && ddlCategory.SelectedValue != "1")
        {
            DivSubCommitte.Visible = true;

            if (id_mastetID.Value=="")
            {
                using (DataTable dt = AMeetingEntryDal.GetDDLComDivisionEntry(ddlCompany.SelectedValue, ddlCategory.SelectedValue))
                {
                    if (dt.Rows.Count>0)
                    {
                        ddlSubCommittee.DataSource = dt;
                        ddlSubCommittee.DataValueField = "Value";
                        ddlSubCommittee.DataTextField = "TextField";
                        ddlSubCommittee.DataBind();
                    }
                    else
                    {
                        DivSubCommitte.Visible = false;
                    }
                }
            }
            else
            {
                using (DataTable dt = AMeetingEntryDal.GetDDLComDivision(ddlCompany.SelectedValue, ddlCategory.SelectedValue))
                {
                    ddlSubCommittee.DataSource = dt;
                    ddlSubCommittee.DataValueField = "Value";
                    ddlSubCommittee.DataTextField = "TextField";
                    ddlSubCommittee.DataBind();
                }
            }
            EmitGridAJson(true, new DataTable());
        }
    }

    protected void ddlSubCommittee_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCategory.SelectedValue != "")
        {
            DataTable jobCreationInfos = AMeetingEntryDal.GetEmpMemberInfoBySubCOmmitte(ddlCompany.SelectedValue,
                ddlSubCommittee.SelectedValue);
            EmitGridBJson(jobCreationInfos);

            DataTable dtMember_List = AMAster.GetMemberListDataById(ddlSubCommittee.SelectedValue);
            EmitGridAJson(true, dtMember_List);
        }
    }

    protected void lbDraft_OnClick(object sender, EventArgs e)
    {
        string acStatus = "Drafted";
        SaveUpdateInfo(acStatus);
    }

    protected void AddModalBoardMember_OnClick(object sender, EventArgs e)
    {
        if (BordmemberMemberValidationApp())
        {
            if (CheckBoardMemberApp())
            {

                DataTable aDataTable = new DataTable();
                aDataTable.Columns.Add("EmpInfoId");
                aDataTable.Columns.Add("EmpMasterCode");
                aDataTable.Columns.Add("EmpName");
                aDataTable.Columns.Add("Designation");
                aDataTable.Columns.Add("Seq_No");
                aDataTable.Columns.Add("IsMinimumApprovalPerson");
                aDataTable.Columns.Add("CanEdit");
                aDataTable.Columns.Add("NotificationEmail");
                aDataTable.Columns.Add("NotificationSMS");




                DataRow dataRow = null;

                for (int i = 0; i < gv_loadGridView.Rows.Count; i++)
                {
                    CheckBox chkSelect = (CheckBox)gv_loadGridView.Rows[i].Cells[0].FindControl("BchkSelect");


                    Label BM_Name = (Label)gv_loadGridView.Rows[i].FindControl("BM_Name");
               

                    if (chkSelect.Checked)
                    {
                        //  if (HasDCStoreId(Convert.ToInt32(loadGridView.DataKeys[i][0].ToString())))
                        {



                            dataRow = aDataTable.NewRow();

                            dataRow["EmpInfoId"] ="0";

                            dataRow["EmpMasterCode"] = null;
                            dataRow["EmpName"] = BM_Name.Text;
                            dataRow["Designation"] = "";

                            dataRow["Seq_No"] = "";
                            dataRow["IsMinimumApprovalPerson"] = "";
                            dataRow["CanEdit"] = "";
                            dataRow["NotificationEmail"] = "";
                            dataRow["NotificationSMS"] = "";


                            aDataTable.Rows.Add(dataRow);
                        }
                    }
                }
                for (int i = 0; i < gv_ApprovalPathDetail.Rows.Count; i++)
                {


                    HiddenField ShfEmpInfoId = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("ApphfEmpInfoId"));
                    HiddenField hfSeq_No = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("hfSeq_No"));
                    Label Slbl_EmpMasterCode = (Label)gv_ApprovalPathDetail.Rows[i].FindControl("Appbl_EmpMasterCode");
                    Label Slbl_EmpName = (Label)gv_ApprovalPathDetail.Rows[i].FindControl("Applbl_EmpName");
                    Label Slbl_Designation = (Label)gv_ApprovalPathDetail.Rows[i].FindControl("Applbl_Designation");



                    HiddenField hfIsMinimumApprovalPerson = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("hfIsMinimumApprovalPerson"));
                    HiddenField hfCanEdit = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("hfCanEdit"));
                    HiddenField HiNotificationEmailApp = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("HiNotificationEmailApp"));
                    HiddenField hfNotificationSMSApp = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("hfNotificationSMSApp"));



                    dataRow = aDataTable.NewRow();
                    dataRow["EmpInfoId"] = ShfEmpInfoId.Value;

                    dataRow["EmpMasterCode"] = Slbl_EmpMasterCode.Text;
                    dataRow["EmpName"] = Slbl_EmpName.Text;
                    dataRow["Designation"] = Slbl_Designation.Text;
                    dataRow["Seq_No"] = hfSeq_No.Value;


                    dataRow["IsMinimumApprovalPerson"] = hfIsMinimumApprovalPerson.Value;
                    dataRow["CanEdit"] = hfCanEdit.Value;
                    dataRow["NotificationEmail"] = HiNotificationEmailApp.Value;
                    dataRow["NotificationSMS"] = hfNotificationSMSApp.Value;







                    aDataTable.Rows.Add(dataRow);
                }
                ViewState["gv_Details_App"] = aDataTable;
                gv_ApprovalPathDetail.DataSource = aDataTable;
                gv_ApprovalPathDetail.DataBind();

                if (gv_ApprovalPathDetail.Rows.Count == 0)
                {
                    lblstatus.Text = "No Approval Path have been  selected.";
                }
                else
                {
                    lblstatus.Text = "";
                }


                for (int i = 0; i < gv_ApprovalPathDetail.Rows.Count; i++)
                {
                    DropDownList ddlSequenceList = (DropDownList)gv_ApprovalPathDetail.Rows[i].FindControl("ddlSequenceList");


                    ddlSequenceList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select One.....", String.Empty));
                    for (int k = 1; k < gv_ApprovalPathDetail.Rows.Count + 1; k++)
                    {
                        ddlSequenceList.Items.Insert(k, new ListItem(k.ToString()));
                    }
                }

                for (int i = 0; i < gv_ApprovalPathDetail.Rows.Count; i++)
                {
                    DropDownList ddlSequenceList = (DropDownList)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("ddlSequenceList");
                    HiddenField hfSeq_No = (HiddenField)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("hfSeq_No");

                    ddlSequenceList.SelectedValue = hfSeq_No.Value;




                    HiddenField hfIsMinimumApprovalPerson = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("hfIsMinimumApprovalPerson"));
                    HiddenField hfCanEdit = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("hfCanEdit"));
                    HiddenField HiNotificationEmailApp = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("HiNotificationEmailApp"));
                    HiddenField hfNotificationSMSApp = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("hfNotificationSMSApp"));


                    CheckBox chkMimimumCount = (CheckBox)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("chkMimimumCount");

                    if (hfIsMinimumApprovalPerson.Value != "")
                    {
                        chkMimimumCount.Checked = true;

                    }


                    CheckBox chkIsEdit = (CheckBox)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("chkIsEdit");

                    if (hfCanEdit.Value != "")
                    {
                        chkIsEdit.Checked = true;

                    }
                    CheckBoxList chkNotificationApp = (CheckBoxList)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("chkNotificationApp");


                    if (HiNotificationEmailApp.Value != "")
                    {
                        chkNotificationApp.Items[0].Selected = true;

                    }

                    if (hfNotificationSMSApp.Value != "")
                    {
                        chkNotificationApp.Items[1].Selected = true;

                    }

                }
            }
            else
            {
                AlertMessageBoxShow("Already Exist !!!");
            }
        }
    }

    protected void chkBSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        var chkBoxHeader = (CheckBox)gv_loadGridView.HeaderRow.FindControl("chkBSelectAll");

        for (int i = 0; i < gv_loadGridView.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)gv_loadGridView.Rows[i].Cells[0].FindControl("BchkSelect");
            chkBoxRows.Checked = chkBoxHeader.Checked;
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#exampleModalBoardMember').modal('show')", true);
    }

    protected void DropDownList2_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    // Called from the Employee Name typeahead in gv_Details_Save ("Add Employees") as the
    // user types. Replaces the old approach of binding every active employee in the
    // company into each row's ddlEmployeeSave DropDownList (which put the whole roster,
    // per Employee-type row, into ViewState on every postback and was the main cause of
    // this page — including the Members List grid sharing the same ViewState — feeling
    // stuck on "loading" whenever these grids were touched).
    [System.Web.Services.WebMethod(EnableSession = true), ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<object> AjaxSearchEmployeesForMeeting(string companyId, string term)
    {
        var results = new List<object>();

        // WebMethod must be static — guard session explicitly since Page_Load auth-redirect
        // is bypassed for WebMethod requests. This endpoint returns employee PII.
        var session = System.Web.HttpContext.Current.Session;
        if (session == null || session["UserId"] == null)
        {
            return results;
        }

        if (string.IsNullOrWhiteSpace(companyId) || string.IsNullOrWhiteSpace(term) || term.Trim().Length < 2)
        {
            return results;
        }

        // Create a local DAL instance (static context — no instance fields available).
        var dal = new DAL.MeetingMinorsDAL.MeetingEntryDAL();
        DataTable dt = dal.SearchEmpForMeeting(companyId.Trim(), term.Trim());
        if (dt == null) return results;

        foreach (DataRow row in dt.Rows)
        {
            results.Add(new
            {
                EmpInfoId  = row["EmpInfoId"].ToString(),
                EmpMasterCode = row["EmpMasterCode"].ToString(),
                EmpName    = row["EmpName"].ToString(),
                Designation = row["Designation"].ToString()
            });
        }
        return results;
    }

    // ===================== Phase 1 AJAX conversion =====================
    // Static WebMethod equivalents of the room-booking cascade (ddlCompanyLocation -> ddlOffice
    // -> ddlLocation -> ddlFloor -> ddlMettingRoomName -> txtCapacity), the Category -> Sub-Committee
    // cascade, and the edit-mode (?MID=) initial data load. Called via the same raw
    // "MeetingEntry.aspx/MethodName" $.ajax pattern as AjaxSearchEmployeesForMeeting above.
    // These mirror (not replace) the OnSelectedIndexChanged handlers immediately below, which are
    // left in place — unused once AutoPostBack is removed from their controls in the markup — so
    // this conversion stays reversible.

    private static List<object> DataTableRows(DataTable dt)
    {
        var list = new List<object>();
        if (dt == null) return list;
        foreach (DataRow row in dt.Rows)
        {
            var dict = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
                dict[col.ColumnName] = row[col] == DBNull.Value ? null : row[col];
            list.Add(dict);
        }
        return list;
    }

    private static string FetchRoomCapacityText(string roomId)
    {
        if (string.IsNullOrEmpty(roomId)) return "";
        DataTable dt = new MeetingEntryDAL().GetCapacityDataByMeetingId(roomId);
        return dt.Rows.Count > 0 ? dt.Rows[0]["MeetingRoomCapacity"].ToString() : "";
    }

    // GetDDLSalaryLocationByCompany/GetDDLSalaryLocation both route through the shared
    // GetDTforDDL() helper (ClsCommonInternalDAL), which already inserts a "Select..."
    // placeholder row (Value="-1") as row 0 of every DataTable it returns. Do NOT insert
    // another one here - a prior version of this method did, which produced two placeholder
    // rows with different Value sentinels ("" here vs "-1" from GetDTforDDL). Because their
    // Values differed, the client-side renderSelectOptions() dedup (keyed on Value) treated
    // them as distinct options, so both rendered as "Select..." in the Office dropdown.
    private static List<object> FetchOfficeOptions(string companyLocationId)
    {
        DataTable dt = new CommonDataLoadDAL().GetDDLSalaryLocationByCompany(companyLocationId);
        if (dt == null || dt.Rows.Count == 0)
        {
            dt = new CommonDataLoadDAL().GetDDLSalaryLocation();
        }
        return DataTableRows(dt);
    }

    private class CategorySubCommitteeState
    {
        public List<object> GridBRows;
        public bool SubCommitteeVisible;
        public List<object> SubCommitteeOptions;
        public bool ResetGridA;
    }

    // Mirrors ddlCategory_OnSelectedIndexChanged (:2805).
    private static CategorySubCommitteeState FetchCategorySubCommitteeState(string companyId, string categoryId, string mid)
    {
        var result = new CategorySubCommitteeState { SubCommitteeOptions = new List<object>() };
        var dal = new MeetingEntryDAL();

        string effectiveCategory = string.IsNullOrEmpty(categoryId) ? "1" : categoryId;
        DataTable gridB = effectiveCategory == "1" ? dal.GetEmpMemberInfoByCategory(companyId) : new DataTable();
        result.GridBRows = DataTableRows(gridB);

        if (!string.IsNullOrEmpty(categoryId) && categoryId != "1")
        {
            result.SubCommitteeVisible = true;
            DataTable dt = string.IsNullOrEmpty(mid)
                ? dal.GetDDLComDivisionEntry(companyId, categoryId)
                : dal.GetDDLComDivision(companyId, categoryId);

            if (string.IsNullOrEmpty(mid) && dt.Rows.Count == 0)
            {
                result.SubCommitteeVisible = false;
            }
            else
            {
                result.SubCommitteeOptions = DataTableRows(dt);
            }
            result.ResetGridA = true;
        }
        return result;
    }

    private class DivisionRoutingResult
    {
        public List<object> DivisionOptions;
        public List<object> RoutingOptions;
    }

    // Mirrors the ddlDivisionAPP/rbRoutingPath side effect of ddlCompany_OnSelectedIndexChanged
    // (:1148) — needed in edit mode so the Approval-Path search modal has correct options.
    private static DivisionRoutingResult FetchDivisionAndRoutingPath(string companyId)
    {
        var result = new DivisionRoutingResult();
        var dal = new MiscellaneousInformationDAL();
        result.DivisionOptions = DataTableRows(dal.GetDDLComDivision(companyId));

        var session = HttpContext.Current.Session;
        string userTypeId = session != null && session["UserTypeId"] != null ? session["UserTypeId"].ToString() : "";
        DataTable routingDt = (userTypeId == "3" || userTypeId == "4")
            ? dal.GetDDLRoutingPath(companyId)
            : dal.GetDDLRoutingPathDepartment(companyId, session != null && session["DepartmentId"] != null ? session["DepartmentId"].ToString() : "");
        result.RoutingOptions = DataTableRows(routingDt);
        return result;
    }

    [System.Web.Services.WebMethod(EnableSession = true), ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<object> GetOfficesByCompanyLocation(string companyLocationId)
    {
        var session = HttpContext.Current.Session;
        if (session == null || session["UserId"] == null)
            return new List<object>();
        return FetchOfficeOptions(companyLocationId);
    }

    [System.Web.Services.WebMethod(EnableSession = true), ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<object> GetLocationsByOffice(string officeId)
    {
        var session = HttpContext.Current.Session;
        if (session == null || session["UserId"] == null)
            return new List<object>();
        DataTable dt = string.IsNullOrEmpty(officeId) ? null : new CommonDataLoadDAL().GetDDLJobLocation(officeId);
        if (dt == null || dt.Rows.Count == 0)
        {
            dt = new CommonDataLoadDAL().GetDDLJobLocation();
        }
        var rows = DataTableRows(dt);
        EnsureSelectPlaceholder(rows, "Select...");
        return rows;
    }

    [System.Web.Services.WebMethod(EnableSession = true), ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<object> GetFloorsByLocation(string locationId)
    {
        var session = HttpContext.Current.Session;
        if (session == null || session["UserId"] == null)
            return new List<object>();
        DataTable dt = string.IsNullOrEmpty(locationId) ? null : new CommonDataLoadDAL().GetDDLFloor(locationId);
        if (dt == null || dt.Rows.Count == 0)
        {
            dt = new DAL.MeetingMinorsDAL.FloorDal().GetInformationById("");
        }
        var rows = DataTableRows(dt);
        EnsureSelectPlaceholder(rows, "Select...");
        return rows;
    }

    [System.Web.Services.WebMethod(EnableSession = true), ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<object> GetRoomsByFloor(string floorId)
    {
        var session = HttpContext.Current.Session;
        if (session == null || session["UserId"] == null)
            return new List<object>();
        DataTable dt = string.IsNullOrEmpty(floorId) ? null : new CommonDataLoadDAL().GetDDLMeetingRoom(floorId);
        if (dt == null || dt.Rows.Count == 0)
        {
            dt = new DAL.MeetingMinorsDAL.MeetingRoomDal().GetEntryformation();
            if (dt != null && dt.Columns.Contains("MeetingRoomName"))
            {
                if (!dt.Columns.Contains("Value")) dt.Columns.Add("Value", typeof(string));
                if (!dt.Columns.Contains("TextField")) dt.Columns.Add("TextField", typeof(string));
                foreach (DataRow r in dt.Rows)
                {
                    r["Value"] = r["MeetingRoomId"].ToString();
                    r["TextField"] = r["MeetingRoomName"].ToString();
                }
            }
        }
        var rows = DataTableRows(dt);
        EnsureSelectPlaceholder(rows, "Select...");
        return rows;
    }

    [System.Web.Services.WebMethod(EnableSession = true), ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object GetRoomCapacity(string roomId)
    {
        var session = HttpContext.Current.Session;
        if (session == null || session["UserId"] == null)
            return new { capacity = "" };
        return new { capacity = FetchRoomCapacityText(roomId) };
    }

    [System.Web.Services.WebMethod(EnableSession = true), ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object GetCategorySubCommitteeState(string companyId, string categoryId, string mid)
    {
        var session = HttpContext.Current.Session;
        if (session == null || session["UserId"] == null)
            return null;
        var state = FetchCategorySubCommitteeState(companyId, categoryId, mid);
        return new
        {
            gridBRows = state.GridBRows,
            subCommitteeVisible = state.SubCommitteeVisible,
            subCommitteeOptions = state.SubCommitteeOptions,
            resetGridA = state.ResetGridA
        };
    }

    [System.Web.Services.WebMethod(EnableSession = true), ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object GetSubCommitteeMembers(string companyId, string subCommitteeId, string categoryId)
    {
        var session = HttpContext.Current.Session;
        if (session == null || session["UserId"] == null)
            return null;
        if (string.IsNullOrEmpty(categoryId))
            return new { gridBRows = new List<object>(), gridARows = new List<object>() };

        DataTable gridB = new MeetingEntryDAL().GetEmpMemberInfoBySubCOmmitte(companyId, subCommitteeId);
        DataTable gridA = new SubcommitteeSetupDAL().GetMemberListDataById(subCommitteeId);
        return new { gridBRows = DataTableRows(gridB), gridARows = DataTableRows(gridA) };
    }

    // Mirrors the master-field/doc/grid population portion of Page_Load's edit-mode block
    // (:181-345) — everything except the Approval Path / Agenda GridView binding, which stays
    // server-side (Page_Load still runs it, see the restructured block above).
    [System.Web.Services.WebMethod(EnableSession = true), ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object GetMeetingMasterForEdit(string mid)
    {
        var session = HttpContext.Current.Session;
        if (session == null || session["UserId"] == null || string.IsNullOrWhiteSpace(mid))
            return null;

        var meetingDal = new MeetingEntryDAL();
        DataTable dtMaster = meetingDal.GetMasterDataById(mid);
        if (dtMaster.Rows.Count == 0) return null;
        DataRow row = dtMaster.Rows[0];

        bool isOfficePremisis = false, isOuterPremisis = false, isVirtualMeeting = false, isNotice = true;
        try { isOfficePremisis = Convert.ToBoolean(row["IsOfficePremisis"].ToString()); } catch { }
        try { isOuterPremisis = Convert.ToBoolean(row["IsOuterPremisis"].ToString()); } catch { }
        try { isVirtualMeeting = Convert.ToBoolean(row["IsVirtualMeeting"].ToString()); } catch { }
        // Matches Page_Load's rbNotice/rbEntryType logic (:329): a NULL/unset IsNotice must
        // default to Notice (true), not Minutes - otherwise this silently flips existing Notice
        // meetings to the Minutes panel on edit and hides their already-loaded document grid.
        if (dtMaster.Columns.Contains("IsNotice") && row["IsNotice"] != DBNull.Value)
        {
            try { isNotice = Convert.ToBoolean(row["IsNotice"]); } catch { }
        }

        string companyId = row["CompanyId"].ToString();
        string categoryId = row["CategoryID"].ToString();

        string meetingDateText = "";
        try
        {
            if (row["MeetingDate"] != DBNull.Value)
                meetingDateText = Convert.ToDateTime(row["MeetingDate"]).ToString("dd-MMM-yyyy hh:mm tt");
        }
        catch { }

        List<object> officeOptions = new List<object>(), locationOptions = new List<object>(),
            floorOptions = new List<object>(), roomOptions = new List<object>();
        string capacity = "", officeId = "", locationId = "", floorId = "", roomId = "";
        if (isOfficePremisis)
        {
            officeId = row["OfficeId"] != DBNull.Value ? row["OfficeId"].ToString() : "";
            locationId = row["LocationId"] != DBNull.Value ? row["LocationId"].ToString() : "";
            floorId = row["FloorId"] != DBNull.Value ? row["FloorId"].ToString() : "";
            roomId = row["MettingRoomId"] != DBNull.Value ? row["MettingRoomId"].ToString() : "";

            var commonDataLoad = new CommonDataLoadDAL();
            officeOptions = FetchOfficeOptions(companyId);

            DataTable dtLoc = !string.IsNullOrEmpty(officeId) ? commonDataLoad.GetDDLJobLocation(officeId) : null;
            if (dtLoc == null || dtLoc.Rows.Count == 0) dtLoc = commonDataLoad.GetDDLJobLocation();
            locationOptions = DataTableRows(dtLoc);
            EnsureSelectPlaceholder(locationOptions, "Select an Option");

            DataTable dtFloor = !string.IsNullOrEmpty(locationId) ? commonDataLoad.GetDDLFloor(locationId) : null;
            if (dtFloor == null || dtFloor.Rows.Count == 0) dtFloor = new DAL.MeetingMinorsDAL.FloorDal().GetInformationById("");
            floorOptions = DataTableRows(dtFloor);
            EnsureSelectPlaceholder(floorOptions, "Select an Option");

            DataTable dtRoom = !string.IsNullOrEmpty(floorId) ? commonDataLoad.GetDDLMeetingRoom(floorId) : null;
            if (dtRoom == null || dtRoom.Rows.Count == 0)
            {
                dtRoom = new DAL.MeetingMinorsDAL.MeetingRoomDal().GetEntryformation();
                if (dtRoom != null && dtRoom.Columns.Contains("MeetingRoomName"))
                {
                    if (!dtRoom.Columns.Contains("Value")) dtRoom.Columns.Add("Value", typeof(string));
                    if (!dtRoom.Columns.Contains("TextField")) dtRoom.Columns.Add("TextField", typeof(string));
                    foreach (DataRow r in dtRoom.Rows)
                    {
                        r["Value"] = r["MeetingRoomId"].ToString();
                        r["TextField"] = r["MeetingRoomName"].ToString();
                    }
                }
            }
            roomOptions = DataTableRows(dtRoom);
            EnsureSelectPlaceholder(roomOptions, "Select an Option");

            capacity = FetchRoomCapacityText(roomId);
        }

        var subCommitteeState = FetchCategorySubCommitteeState(companyId, categoryId, mid);
        var divisionRouting = FetchDivisionAndRoutingPath(companyId);

        DataTable dtDoc = meetingDal.GetDocDataById(mid);
        DataTable dtNoticeDocs = dtDoc.Clone();
        DataTable dtMinutesDocs = dtDoc.Clone();
        foreach (DataRow r in dtDoc.Rows)
        {
            if (dtDoc.Columns.Contains("DocType") && r["DocType"].ToString() == "Minutes")
                dtMinutesDocs.ImportRow(r);
            else
                dtNoticeDocs.ImportRow(r);
        }

        DataTable dtGridA = meetingDal.GetMeetingInfoDetailByIdEmp(mid);
        DataTable dtGridB = meetingDal.GetMeetingInfoDetailByIdBoardMember(mid);

        return new
        {
            companyId,
            title = row["Title"].ToString(),
            categoryId,
            subCommitteeId = row["SubCommitteeId"].ToString(),
            meetingNo = row["MeetingNo"].ToString(),
            meetingDateText,
            startTime = row["StartTime"].ToString(),
            endTime = row["EndTime"].ToString(),
            isNotice,
            isOfficePremisis,
            isOuterPremisis,
            isVirtualMeeting,
            officeId,
            locationId,
            floorId,
            roomId,
            capacity,
            officeOptions,
            locationOptions,
            floorOptions,
            roomOptions,
            location = row["Location"].ToString(),
            locationDescription = row["LocationDescription"].ToString(),
            remarks = row["Remarks"].ToString(),
            mailAddress = dtMaster.Columns.Contains("MailAddress") && row["MailAddress"] != DBNull.Value ? row["MailAddress"].ToString() : "",
            mailBody = dtMaster.Columns.Contains("MailBody") && row["MailBody"] != DBNull.Value ? row["MailBody"].ToString() : "",
            isSendMail = dtMaster.Columns.Contains("IsSendMail") && row["IsSendMail"] != DBNull.Value && Convert.ToBoolean(row["IsSendMail"]),
            subCommitteeVisible = subCommitteeState.SubCommitteeVisible,
            subCommitteeOptions = subCommitteeState.SubCommitteeOptions,
            divisionOptions = divisionRouting.DivisionOptions,
            routingPathOptions = divisionRouting.RoutingOptions,
            noticeDocs = DataTableRows(dtNoticeDocs),
            minutesDocs = DataTableRows(dtMinutesDocs),
            gridARows = DataTableRows(dtGridA),
            gridBRows = DataTableRows(dtGridB)
        };
    }

    protected void ddlCompanyLocation_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompanyLocation.SelectedValue != "")
        {
            CommonDataLoadDAL commonDataLoad = new CommonDataLoadDAL();
            using (DataTable dt = commonDataLoad.GetDDLSalaryLocationByCompany(ddlCompanyLocation.SelectedValue))
            {
                // GetDDLSalaryLocationByCompany already includes a "Select..." placeholder
                // row via GetDTforDDL() - do not insert a second one (see FetchOfficeOptions).
                ddlOffice.DataSource = dt;
                ddlOffice.DataValueField = "Value";
                ddlOffice.DataTextField = "TextField";
                ddlOffice.DataBind();
            }
        }
    }
}

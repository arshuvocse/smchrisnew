<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IVSearchControl.ascx.cs" Inherits="Inverview_IVSearchControl" %>

<div class="col-2">
    <div class="form-group">
        <label class="control-label">Company</label><label style="color: #a52a2a">*</label>
        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" class="form-control form-control-sm" />
    </div>
</div>
<div class="col-2">
    <div class="form-group">
        <label class="control-label">Financial Year</label><label style="color: #a52a2a">*</label>
        <asp:DropDownList runat="server" ID="ddlFinYear" class="form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="ddlFinYear_OnSelectedIndexChanged"/>
    </div>
</div>
<div class="col-2">
    <div class="form-group">
        <label class="control-label">Department</label><label style="color: #a52a2a">*</label>
        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDepartment" class="form-control form-control-sm" OnSelectedIndexChanged="ddlDepartment_OnSelectedIndexChanged" />
    </div>
</div>

<div class="col-2">
    <div class="form-group">
        <label class="control-label">Circulation From Date</label>
        <asp:TextBox runat="server" ID="startDate" class="form-control form-control-sm" AutoPostBack="True"  OnTextChanged="startDate_OnTextChanged"></asp:TextBox>
        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                      Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                      TargetControlID="startDate" />
    </div>
</div>
<div class="col-2">
    <div class="form-group">
        <label class="control-label">Circulation to Date</label>
        <asp:TextBox runat="server" ID="endDate" class="form-control form-control-sm" AutoPostBack="True" OnTextChanged="endDate_OnTextChanged" ></asp:TextBox>
        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                      Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                      TargetControlID="endDate" />
    </div>
</div>

<div class="col-2">
    <div class="form-group required">
        <label class="control-label">Job Circulation</label><label style="color: #a52a2a">*</label>
         <asp:DropDownList runat="server"   ID="ddlJobCirculation" class="form-control form-control-sm"   />
        <asp:TextBox  Visible="False" runat="server" AutoPostBack="True" ID="txt_JobCirculation" class="form-control form-control-sm" OnTextChanged="txt_JobCirculation_OnTextChanged"></asp:TextBox>
        <asp:HiddenField runat="server" ID="hfJobID"/>
        <ajaxToolkit:AutoCompleteExtender
            ID="at_txt_JobCirculation"
            TargetControlID="txt_JobCirculation"
            runat="server"
            ServiceMethod="GetJobCirculationAutoPosition"
            ServicePath="~/WebService.asmx"
            MinimumPrefixLength="1"
            CompletionInterval="1000"
            EnableCaching="false"
            CompletionSetCount="1"
            FirstRowSelected="false">
        </ajaxToolkit:AutoCompleteExtender>
    </div>
</div>

<%--<div class="col-2" runat="server" >
    <div class="form-group">
                               
        <label>Job Title</label>
        <asp:TextBox runat="server" ID="txt_JobTitle" class="form-control form-control-sm"></asp:TextBox>
    </div>
</div>--%>

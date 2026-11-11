<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IVSearchControlReport.ascx.cs" Inherits="Report_Pages_IVSearchControlReport" %>
 <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
<div class="col-2">
    <div class="form-group">
        <label class="control-label">Company</label> <label style="color: #a52a2a">*</label>
        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" class="form-control form-control-sm" />
    </div>
</div>
<div class="col-2">
    <div class="form-group">
        <label class="control-label">Financial Year</label> <label style="color: #a52a2a">*</label>
        <asp:DropDownList runat="server" ID="ddlFinYear" class="form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="ddlFinYear_OnSelectedIndexChanged"/>
         <script type="text/javascript">
             function pageLoad() {
                 $('.SelectMe').chosen({ disable_search_threshold: 5, search_contains: true });





             }
</script>
    </div>
</div>
<div class="col-2">
    <div class="form-group">
        <label class="control-label">Department</label> <label style="color: #a52a2a">*</label>
        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDepartment" class="form-control form-control-sm SelectMe" OnSelectedIndexChanged="ddlDepartment_OnSelectedIndexChanged" />
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

<div class="col-2" runat="server" >
    <div class="form-group">
        <label class="control-label">Job Circulation</label> <label style="color: #a52a2a">*</label>
        <asp:DropDownList runat="server"   ID="ddlJobCirculation" class="form-control form-control-sm SelectMe"   />
        <asp:TextBox runat="server" Visible="False" AutoPostBack="True" ID="txt_JobCirculation" class="form-control form-control-sm" OnTextChanged="txt_JobCirculation_OnTextChanged"></asp:TextBox>
        <asp:HiddenField runat="server" ID="hfJobID"/>
        <ajaxToolkit:AutoCompleteExtender
            ID="at_txt_JobCirculation"
            TargetControlID="txt_JobCirculation"
            runat="server"
            ServiceMethod="GetJobCirculationAutoPosition"   CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
            ServicePath="~/WebService.asmx"
            MinimumPrefixLength="1"
            CompletionInterval="1000"
            EnableCaching="false"
            CompletionSetCount="1"
            FirstRowSelected="false"  ShowOnlyCurrentWordInCompletionListItem="true">
        </ajaxToolkit:AutoCompleteExtender>
    </div>
</div>

<%--<div class="col-2" runat="server" >
    <div class="form-group">
                               
        <label>Job Title</label>
        <asp:TextBox runat="server" ID="txt_JobTitle" class="form-control form-control-sm"></asp:TextBox>
    </div>
</div>--%>

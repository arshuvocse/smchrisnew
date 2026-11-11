<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="ExitInterviewForm.aspx.cs" Inherits="Survey_ExitInterviewForm" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
        <style type="text/css">
        /*AutoComplete flyout */
    
    </style>
    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>
                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;">  <img src="../Report_Pages/app.png"  width="20px" />  Exit Interview Form</h1>
                        </div>
                      
                                            <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="detailsViewButton" Text="Go Back" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>
                    </div>

                    <div class="card">
                        <div class="card-body">
                            
                            
                            
                             <div class="row">
                                <style>
                                    .tblTHColorChang{
                                        background-color: #EDF2F5!important;
                                        font-weight: bold;
                                        font-size: 13px;
                                    }


.title-widget {
	color: #898989;
	font-size: 20px;
	font-weight: 300;
	line-height: 1;
	position: relative;
	text-transform: uppercase;
	font-family: 'Fjalla One', sans-serif;
	margin-top: 0;
	margin-right: 0;
	margin-bottom: 25px;
	 
	padding-left: 12px;

}
  .chkChoiceStep label {
            padding-left: 5px;
            padding-right: 5px;
        }
.title-widget::before {
    background-color: #ea5644;
    content: "";
    height: 22px;
    left: 0px;
    position: absolute;
    top: -2px;
    width: 5px;
}


                                </style>
                                     <table class="table table-bordered table-striped">
                                                    <tr>
                                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Employee ID</td>
                                                        <td> <asp:Label runat="server" ID="lblEmpId"></asp:Label></td>

                                                        
                                                         <td  class="tblTHColorChang" style="width: 20%; padding: 10px;">Supervisor</td>
                                                        <td>  <asp:Label ID="ReportingLabel"  runat="server"></asp:Label></td>
                                                    </tr>
                                                    
                                                     <tr>
                                                        <td  class="tblTHColorChang" style="width: 20%; padding: 10px;">Employee Name</td>
                                                        <td> <asp:Label runat="server" ID="lblEmployeeName"></asp:Label></td>

                                                         
                                                              
                                                         <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Department</td>
                                                        <td>  <asp:Label ID="deptNameLabel"  runat="server"></asp:Label></td>
                                                    </tr>
                                                    
                                                     <tr>
                                                        <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Designation</td>
                                                        <td>   <asp:Label ID="desigNameLabel" runat="server"></asp:Label></td>

                                                     <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Office</td>
                                                        <td>  <asp:Label ID="LocationLabel"   runat="server"></asp:Label></td>

                                                    </tr>
                                                    
                                                    
                                                    
                                                    
                                                     
                                                    
                                                     <tr>
                                                        <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Date Of Joining</td>
                                                        <td>     <asp:Label ID="joiningDateLabel"  runat="server"></asp:Label></td>
  <td style="width: 20%; padding: 10px;" class="tblTHColorChang" >Place</td>
                                                        <td> <asp:Label runat="server" ID="lblPlace"></asp:Label></td>
                                                    </tr>
                                                    
                                                    
                                                 
                                                    
                                                    
                                                    </table>
                            </div>

                            

                            <fieldset class="for-panel">
                                <%--<legend>Search By</legend>--%>
                                <div class="form-row" runat="server" Visible="False">
                                    <div class="col-3" runat="server" Visible="False">
                                        <div class="form-group ">
                                            <label class="control-label">Company</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" class="form-control form-control-sm" />
                                        </div>
                                    </div>

                                    <div class="col-4">
                                        <div class="form-group ">
                                            <label class="control-label">Employee Name</label> <br />
                                            <asp:TextBox ID="txt_EmpName" runat="server" AutoPostBack="True" Enabled="False" CssClass="form-control form-control-sm" OnTextChanged="txt_EmpName_OnTextChanged"></asp:TextBox>
                                            <asp:HiddenField runat="server" ID="hfEmpInfoId" />
                                            
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                            EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                            ServiceMethod="GetExitImployeeForm" ServicePath="~/WebService.asmx" TargetControlID="txt_EmpName"
                                            UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                            ShowOnlyCurrentWordInCompletionListItem="true">
                                        </cc1:AutoCompleteExtender>

                                            <%--<ajaxToolkit:AutoCompleteExtender
                                                ID="at_txt_EmpName"
                                                TargetControlID="txt_EmpName"
                                                runat="server"
                                                ServiceMethod="GetExitImployeeForm"
                                                ServicePath="~/WebService.asmx"
                                                MinimumPrefixLength="2"
                                                CompletionInterval="1000"
                                                EnableCaching="false"
                                                CompletionSetCount="1"
                                                FirstRowSelected="True">
                                            </ajaxToolkit:AutoCompleteExtender>--%>

                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        

                            <div class="form-row" runat="server" Visible="False">

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Employee Code</label><span style="color: red">&nbsp;*</span>
                                        <asp:Label runat="server" ID="empCode" class="form-control form-control-sm" />
                                        <asp:HiddenField ID="HiddenField2" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Employee Name</label><span style="color: red">&nbsp;*</span> <br />
                                        <asp:Label runat="server" ID="empName" class="form-control form-control-sm" />
                                        <asp:HiddenField ID="HiddenField" runat="server" />

                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Date of Joining</label><span style="color: red">&nbsp;*</span>
                                        <asp:Label runat="server" ID="dtJoining" class="form-control form-control-sm" />
                                        <asp:HiddenField ID="HiddenField3" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Division</label><span style="color: red">&nbsp;*</span>
                                        <asp:Label runat="server" ID="ddlDivision" class="form-control form-control-sm" />
                                        <asp:HiddenField ID="hfDivision" runat="server" />

                                    </div>
                                </div>
                            </div>

                            <div class="form-row" runat="server" Visible="False">
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Department</label><span style="color: red">&nbsp;*</span>
                                        <asp:Label runat="server" ID="ddlDepartment" class="form-control form-control-sm" />
                                        <asp:HiddenField ID="hfDepartment" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Designation</label><span style="color: red">&nbsp;*</span>
                                        <asp:Label runat="server" ID="ddlDesignation" class="form-control form-control-sm" />
                                        <asp:HiddenField ID="hfDesignation" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group" runat="server" Visible="False">
                                        <label>Slary Grade</label><span style="color: red">&nbsp;*</span>
                                        <asp:Label runat="server" ID="ddlSalaryGrade" class="form-control form-control-sm" />
                                        <asp:HiddenField ID="hfSalaryGrade" runat="server" />
                                    </div>
                                </div>

                            </div>
                            


                            <div style="text-align: center;font-size: 14px;">
                                What was reason(s) for leaving ?
                                        <br />
                                (Please tick (√) all items that apply)
                            </div>
                            <hr />
                            <br />

                            <div class="col-12">
                                <div class="form-group">
                                    <asp:CheckBoxList runat="server" CssClass="chkChoiceStep" ID="exitQuestionRadioButtonList" RepeatDirection="Horizontal" BorderStyle="None" RepeatColumns="9">
                                    </asp:CheckBoxList>
                                </div>
                            </div>

                    
                            <div id="exitReasonGridView" style="height: auto; overflow: auto; width: auto;">
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered thead-dark" DataKeyNames="ExitServyId">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="ServayQuestion" HeaderText="Options" />
                                        
                                        <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:RadioButtonList runat="server" CssClass="chkChoiceStep" ID="rad_RatingScale" RepeatDirection="Horizontal" BorderStyle="None">
                                                <asp:ListItem Value="1" Text="Strongly Agree"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Agree"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Neutral"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="Disagree"></asp:ListItem>
                                                <asp:ListItem Value="5" Text="Strongly Disagree"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                        
                                    </Columns>
                                </asp:GridView>
                            </div>
                    
                            <div class="form-row">
                                <div class="col-6">
                                    <div class="form-group">
                                        <label> Other Opinion </label>
                                        <asp:TextBox runat="server" ID="otherOpinion" TextMode="MultiLine" Rows="3"  class="form-control" />
                                    </div>
                                </div>
                            </div>

                        <div>
                            <asp:HiddenField runat="server" ID="hdpk" />
                            <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" Text="Submit " CssClass="btn btn-sm btn-info" />
                            <asp:Button ID="cancelButton" Text="Cancel" OnClick="cancelButton_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>


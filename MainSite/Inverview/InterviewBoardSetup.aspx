<%@ Page Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="InterviewBoardSetup.aspx.cs" Inherits="Inverview_InterviewBoardSetup" %>

<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>
<%@ Register Src="~/Report_Pages/IVSearchControlReport.ascx" TagPrefix="uc1" TagName="IVSearchControl" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">
       <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=40);
            opacity: 0.4;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            width: 300px;
            border: 3px solid #0DA9D0;
        }
        .form-group.required .control-label:after { 
            color: #d00;
            content: "*";
            position: absolute;
            margin-left: 4px;
            top:4px;
            font-size: large;
        }
    </style>

    
        
    <style>
        #cpFormBody_gv_InterviewBoardMember> tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }


          #cpFormBody_gv_InterviewBoardMember  > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }
    </style>
     
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=40);
            opacity: 1;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            width: 300px;
            border: 3px solid #0DA9D0;
        }
        .form-group.required .control-label:after { 
            color: #d00;
            content: "*";
            position: absolute;
            margin-left: 4px;
            top:4px;
            font-size: large;
        }
    </style>
    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>
                   <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png" width="20px"  /> Interview Committee Info</h1>
                        </div>
                        <%--<div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="detailsViewButton" Text="List Information " CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                        </div>--%>

                        <div class="page-heading__container float-right d-none d-sm-block">
                          
                             <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                        <asp:LinkButton ID="ListViewButton"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-body">
                            <div class="form-row">
                                <%--                                <div class="col-3">
                                    <div class="form-group required">
                                        <label class="control-label">Company</label>
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" class="form-control form-control-sm" />
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group required">
                                        <label class="control-label">Job Circulation</label>
                                        <asp:TextBox runat="server" AutoPostBack="True" ID="txt_JobCirculation" class="form-control form-control-sm" OnTextChanged="txt_JobCirculation_OnTextChanged"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hdJobId" />
                                        <ajaxToolkit:AutoCompleteExtender
                                            ID="at_txt_JobCirculation"
                                            TargetControlID="txt_JobCirculation"
                                            runat="server"
                                            ServiceMethod="GetJobCirculationAuto"
                                            ServicePath="~/WebService.asmx"
                                            MinimumPrefixLength="1"
                                            CompletionInterval="1000"
                                            EnableCaching="false"
                                            CompletionSetCount="1"
                                            CompletionListCssClass="autocomplete_completionListElement"
                                            FirstRowSelected="True">
                                        </ajaxToolkit:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">

                                        <label>Job Title</label>
                                        <asp:TextBox runat="server" ID="txt_JobTitle" class="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>--%>
                                <uc1:IVSearchControl runat="server" ID="IVSearchControl" />

                                <div class="col-3" runat="server" visible="False">
                                    <div class="form-group required">
                                        <label class="control-label">Interview Phase</label>
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlInterviewPhase" OnSelectedIndexChanged="ddlInterviewPhase_OnSelectedIndexChanged" class="form-control form-control-sm">
                                            <asp:ListItem Text="-----Select" Value="-1" runat="server"></asp:ListItem>
                                            <asp:ListItem Text="1" Value="1" runat="server"></asp:ListItem>
                                            <asp:ListItem Text="2" Value="2" runat="server"></asp:ListItem>
                                            <asp:ListItem Text="3" Value="3" runat="server"></asp:ListItem>
                                            <asp:ListItem Text="4" Value="4" runat="server"></asp:ListItem>
                                            <asp:ListItem Text="5" Value="5" runat="server"></asp:ListItem>
                                        </asp:DropDownList>

                                        <%--                                        <asp:Button runat="server" ID="btn_CopyIVInfo" OnClick="btn_CopyIVInfo_OnClick" Text="Copy From Previous Phase" CssClass="btn btn-sm btn-outline-secondary" />--%>
                                    </div>
                                </div>
                                <%--<div class="col-3">
                                    <div class="form-group required">
                                        <label class="control-label">Activity</label><br />
                                        <asp:CheckBoxList RepeatLayout="Table" RepeatDirection="Horizontal" runat="server" ID="lchk_InterviewActivity" AutoPostBack="True" OnSelectedIndexChanged="lchk_InterviewActivity_OnSelectedIndexChanged">
                                        </asp:CheckBoxList>
                                        <asp:TextBox runat="server" ID="txt_ActivityOther" placeholder="Activity Other Remarks" class="form-control form-control-sm" Visible="False"></asp:TextBox>
                                    </div>
                                </div>--%>

                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Interview Venue</label>
                                        <asp:TextBox runat="server" ID="txt_InterviewVenue" class="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group required">
                                        <label class="control-label">Interview Date</label>
                                        <asp:TextBox runat="server" ID="txt_InterviewDate" class="form-control form-control-sm "></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                            TargetControlID="txt_InterviewDate" />
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Email Body</label>
                                        <asp:TextBox runat="server" ID="txt_EmailBody" class="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-1">
                                    <div class="form-group required">
                                        <label class="control-label">Start Time</label>
                                        <cc1:TimeSelector ID="txt_InterviewFromTime" runat="server" DisplaySeconds="false" DisplayButtons="false">
                                        </cc1:TimeSelector>

                                    </div>
                                </div>
                                <div class="col-1">
                                    <div class="form-group required">
                                        <label class="control-label">End Time</label>
                                        <cc1:TimeSelector ID="txt_InterviewToTime" runat="server" DisplaySeconds="false" DisplayButtons="false">
                                        </cc1:TimeSelector>

                                    </div>
                                </div>

                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Remarks</label>
                                        <asp:TextBox runat="server" ID="txt_InterviewMasterRemarks" class="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>


                            </div>
                            <br />
                            <div>
                                <asp:GridView Width="100%" ID="gv_InterviewBoardMember" runat="server" ShowFooter="true"
                                    AutoGenerateColumns="false"    CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender" 
                                    OnRowCreated="gv_InterviewBoardMember_RowCreated">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL#">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                                <asp:HiddenField runat="server" ID="hdpkd" Value='<%#Eval("BoardDetailsId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Option">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlEmpOption" runat="server"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlEmpOption_OnSelectedIndexChanged" class="form-control form-control-sm">
                                                    <asp:ListItem Value="1">SMC</asp:ListItem>
                                                    <asp:ListItem Value="2">SMC EL</asp:ListItem>
                                                    <asp:ListItem Value="3">Other</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_EmpID" runat="server" ReadOnly="true" class="form-control form-control-sm" Text='<%#Eval("EmpMasterCode") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Search Emp Name">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_EmpName" runat="server" AutoPostBack="True" class="form-control form-control-sm" OnTextChanged="txt_EmpName_OnTextChanged" Text='<%#Eval("EmpName") %>'></asp:TextBox>
                                                <asp:HiddenField runat="server" ID="hdEmpInfoId" Value='<%#Eval("EmpInfoId") %>' />
                                                <ajaxToolkit:AutoCompleteExtender
                                                    ID="at_txt_EmpName"
                                                    TargetControlID="txt_EmpName"
                                                    runat="server"
                                                    ServiceMethod="GetEmpAutoForIVBoardSetup"
                                                    ServicePath="~/WebService.asmx"
                                                    MinimumPrefixLength="1"
                                                    CompletionInterval="500"
                                                    EnableCaching="false"
                                                    CompletionListCssClass="autocomplete_completionListElement"
                                                    CompletionSetCount="1"
                                                    FirstRowSelected="True">
                                                </ajaxToolkit:AutoCompleteExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_EmpDesignation" runat="server" class="form-control form-control-sm" Text='<%#Eval("Designation") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_EmpDepartment" runat="server" class="form-control form-control-sm" Text='<%#Eval("DepartmentName") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Company">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_EmpCompany" runat="server" class="form-control form-control-sm" Text='<%#Eval("CompanyName") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Email">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_EmpEmail" runat="server" class="form-control form-control-sm" Text='<%#Eval("OfficialEmail") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Phone">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_EmpPhone" runat="server" class="form-control form-control-sm" Text='<%#Eval("OfficialMobile") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Activity">
                                            <ItemTemplate>
                                                <asp:CheckBoxList RepeatLayout="Table" RepeatDirection="Horizontal" runat="server" ID="lchk_InterviewActivity" AutoPostBack="True" OnSelectedIndexChanged="lchk_InterviewActivity_OnSelectedIndexChanged">
                                                </asp:CheckBoxList>
                                                <asp:TextBox runat="server" ID="txt_ActivityOther" placeholder="Activity Other Remarks" class="form-control form-control-sm" ReadOnly="True"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Operation">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lb_Remove" runat="server"
                                                    OnClick="lb_Remove_Click">Remove</asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:LinkButton ID="btn_AddRow" runat="server" class="btn btnMyDesignAddtoList btn-sm"
                                                    
                                                    OnClick="btn_AddRow_Click" ><i class="fa fa-plus"></i> &nbsp;Add New</asp:LinkButton>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <br/>
                            <div class="form-row">
                                <div class="col-2">
                                    <div class="form-group">
                                        <asp:LinkButton ID="LinkButton1" CssClass="btn btnMyDesignSearch btn-sm" runat="server" OnClick="LinkButton1_OnClick" Text="Set Exam Operation"></asp:LinkButton>
                                        
                                    </div>
                                </div>


                            </div>
                                        <div>
        <asp:ModalPopupExtender runat="server" TargetControlID="test" PopupControlID="panal" ID="mp1"/>
        <asp:HiddenField ID="test" runat="server"></asp:HiddenField>
        <asp:Panel ID="panal" runat="server" Style="display: none;padding: 30px;" Height="600px" Width="60%" CssClass="modalPopup">
            <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>--%>
            
               <br/>

                                  <div class="page-header text-center">
      <h1  class="elegantshd" ><asp:Label ID="lblHeader" Text="Exam Operation" runat="server"></asp:Label></h1>
    </div>
             <style>
                 .elegantshd {
  color: #131313;
  
  letter-spacing: .15em; 
    
     font-family: 'Kreon', serif;
 vertical-align:middle;  text-decoration-style: wavy;
}
             </style>
            <hr/>
                    <div class="form-row">
                        
                        <div class="col-2">
                            <div class="form-group" style="margin-top: 9px;">
                                
                                <asp:CheckBox ID="writtenCheckBox" AutoPostBack="True" OnCheckedChanged="writtenCheckBox_OnCheckedChanged" Text="&nbsp;Written:" runat="server" />
                            </div>
                        </div>
                        <div class="col-3" runat="server" Visible="False" id="written" >
                            <div class="form-group required">
                                <asp:TextBox runat="server" ID="writtenTextBox" class="form-control form-control-sm"></asp:TextBox>
                                 <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2122" runat="server" Enabled="True"
                                                                            TargetControlID="writtenTextBox" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                            </div>
                        </div>
                        <div class="col-2">
                            <div class="form-group" style="margin-top: 9px;">
                                
                                <asp:CheckBox ID="otherCheckBox" AutoPostBack="True" OnCheckedChanged="otherCheckBox_OnCheckedChanged" Text="&nbsp;Other:" runat="server" />
                            </div>
                        </div>
                        <div class="col-3" runat="server" Visible="False" id="other">
                            <div class="form-group required">
                                <asp:TextBox runat="server" ID="otherMarksTextBox" class="form-control form-control-sm"></asp:TextBox>
                                 <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                TargetControlID="otherMarksTextBox" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                            </div>
                        </div>
                       

                    </div>
            <div class="form-row">
                 <div class="col-2">
                            <div class="form-group"  style="margin-top: 9px;">
                                
                                <asp:CheckBox ID="vivaCheckBox" AutoPostBack="True" OnCheckedChanged="vivaCheckBox_OnCheckedChanged" Text="&nbsp; Viva:" runat="server" />
                            </div>
                        </div>
            </div>
                    <div class="form-row" runat="server" Visible="False" id="grid">
                        <div class="col-12">
                            <div class="form-group">
                                <asp:GridView Width="100%" ID="GridView1" runat="server" ShowFooter="true"
                                    AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark"
                                     DataKeyNames="VivaId">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL#">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Viva">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_Viva" runat="server" ReadOnly="true" class="form-control form-control-sm" Text='<%#Eval("VivaName") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Catgory">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_Catgory" runat="server" ReadOnly="true" class="form-control form-control-sm" Text='<%#Eval("Category") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Out Of">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_outof" runat="server"  class="form-control form-control-sm" Text='<%#Eval("VivaMarks") %>'  ></asp:TextBox>
                                                 <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                                            TargetControlID="txt_outof" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField >
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="txt_checkAll" OnCheckedChanged="txt_checkAll_OnCheckedChanged" AutoPostBack="True" runat="server"></asp:CheckBox>
                                                <asp:Label ID="txt_selectAll" runat="server"  ></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>

                                                <asp:CheckBox ID="txt_check" AutoPostBack="True" OnCheckedChanged="txt_check_OnCheckedChanged" Checked='<%#Eval("IsData") %>' runat="server"></asp:CheckBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        

                    </div>
                        
                        <asp:Button runat="server" ID="btnYes" OnClick="btnYes_OnClick" Text="Close " CssClass="btn btn-sm btn-info" />
                        <%--<asp:Button ID="btnNo" Text="Cancel" OnClick="btnNo_Click" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />--%>
                    
                <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
        </asp:Panel>
    </div>

                            <br />
                            <br />
                            <div>
                                <asp:HiddenField runat="server" ID="hdpk" />

                                  <link href="../UserSetup/ButtonGrups.css" rel="stylesheet" />
                                      <div class="ui-group-buttons">
                                <asp:Button ID="btn_Save" Text="Save" CssClass="btn btn-sm btn-info" Visible="False" OnClientClick="return confirm('Are you sure you want to Save ?')"  runat="server" OnClick="btn_Save_OnClick" />
                                           <div class="or or-sm" runat="server" Visible="False" id="orBTN"></div>
                                    <asp:Button ID="btnSubmit" Text=" Submit " CssClass="btn btn-sm btn-success" OnClientClick="return confirm('Are you sure you want to Submit ?')" Visible="False" runat="server" OnClick="btnSubmit_OnClick" />
                                           </div>
                                  <div class="ui-group-buttons">
                                <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-info" Visible="False" OnClientClick="return confirm('Are you sure you want to update ?')"  runat="server" OnClick="editButton_OnClick" />
                                         <div class="or or-sm" runat="server" Visible="False" id="orUp"></div>

                                    <asp:Button ID="btnUpdateforSubmit" Text="Submit" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="btnUpdateforSubmit_OnClick" OnClientClick="return confirm('Are you sure you want to Submit ?')" />
                                           </div>
                                <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger"   OnClientClick="return confirm('Are you sure you want to delete ?')" Visible="False" runat="server" OnClick="delButton_OnClick" />

                                <%--<asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" Text="Submit " CssClass="btn btn-sm btn-info" />--%>
                                <asp:Button ID="cancelButton" Text="Cancel" OnClick="cancelButton_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" Visible="False"/>
                            </div>
                            <br />
                        </div>
                    </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
</asp:Content>

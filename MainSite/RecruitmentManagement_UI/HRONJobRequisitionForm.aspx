<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="HRONJobRequisitionForm.aspx.cs" Inherits="MasterSetup_UI_HRONJobRequisitionForm" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
     <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <style type="text/css">
        /*AutoComplete flyout */
      

         #cpFormBody_KeyResponGridView > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }


          #cpFormBody_KeyResponGridView  > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }


          #cpFormBody_EducationRequirementGridView > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }


          #cpFormBody_EducationRequirementGridView  > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }




                 #cpFormBody_OtherRequirementsGridView > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }


          #cpFormBody_OtherRequirementsGridView  > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }





                   #cpFormBody_OfficeGridView > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }


          #cpFormBody_OfficeGridView  > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }



                       #cpFormBody_DirectlySupervicesGridView > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }


          #cpFormBody_DirectlySupervicesGridView  > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }
    </style>





    <style>
        fieldset.for-panel {
            background-color: #fcfcfc;
            border: 1px solid #999;
            padding: 5px 10px;
            background-color: white;
            margin-bottom: 5px;
        }

        fieldset.for-panel legend {
            background-color: #fafafa;
            border: 1px solid #ddd;
            border-radius: 1px;
            font-size: 12px;
            font-weight: bold;
            line-height: 10px;
            margin: inherit;
            padding: 7px;
            width: auto;
            margin-bottom: 0;
            color: black;
        }
    </style>
    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                  <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 1px;"><img src="../Report_Pages/app.png" width="20px"  /> Employee Requisition Form </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <%--<asp:Button ID="ViewListButton" Text="View List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="ViewListButton_OnClick" />--%>
                        <%--<asp:Button ID="reloadButton" Text="Reload" CssClass="btn btn-sm btn-outline-success" runat="server" OnClick="reloadButton_OnClick" />--%>
                    </div>

                    <div class="page-heading__container float-right d-none d-sm-block">
                        
                          <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                        <asp:LinkButton ID="ViewListButton"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="ViewListButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
                        
                    </div>

                </div>
                <!-- //END PAGE HEADING -->
                <asp:HiddenField ID="empIdHiddenField" runat="server" />

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">
                            
                            
                             <fieldset class="for-panel" runat="server"   id="DivMemo">
                                                <legend>EMPLOYEE REQUISITION FORM</legend>
                                                <div class="row">

                                <div class="col-md-12">
                                    
                                    <div class="row">

                                <div class="col-md-12">
                                    
                                        <div class="form-row">
                                            <div class="col-md-6">
                                                
                                        <div class="form-row">
                                                <div class="col-md-4"> </div>
                                                <div class="col-md-4">  
                                                    
                                                          <asp:TextBox ID="ReqCodetextBox"    CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            </div>
                                            <div class="col-md-6">
                                                
                                                  <div class="form-row">
                                                <div class="col-md-4">Date:</div>
                                                <div class="col-md-4">  
                                                    
                                                                <asp:TextBox ID="reqDateTextBox"    CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                                                    Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                    TargetControlID="reqDateTextBox" />

                                                </div>
                                            </div>
                                            </div>
                                            </div>
                                    </div>
                                                    </div>
                                 
                                 
                                       <div class="row" style="padding: 2px;">

                                <div class="col-md-12">
                                    
                                        <div class="form-row">
                                            <div class="col-md-6">
                                                
                                        <div class="form-row">
                                                <div class="col-md-4">Department:</div>
                                                <div class="col-md-8">  
                                                    
                                                                  <asp:Label ID="deptLabel"   CssClass="form-control form-control-sm"  runat="server"></asp:Label>    
                                                    <asp:HiddenField runat="server" ID="hfDepartId"/>
                                                </div>
                                            </div>
                                            </div>
                                            <div class="col-md-6">
                                                      <div class="form-row">
                                                <div class="col-md-4">Position:</div>
                                                <div class="col-md-6">  
                                                    
                                                                       <asp:RadioButtonList ID="jstRadioButtonList" RepeatDirection="Horizontal" runat="server" AutoPostBack="True" OnTextChanged="jstRadioButtonList_OnTextChanged">
                                                                <asp:ListItem> &nbsp; New  </asp:ListItem>
                                                                <asp:ListItem> &nbsp; Replacement &nbsp; </asp:ListItem>
                                                            </asp:RadioButtonList>

                                                </div>
                                            </div>
                                    

                                            </div>
                                            

                                            </div>
                                    </div>
                                                    </div>
                                           <div class="row" style="padding: 2px;" runat="server" Visible="False" id="DivReplacement">
                                                   <div class="col-md-12">
                                    
                                        <div class="form-row">
                                            
                                             <div class="col-md-4">
                                            If Existing: Last/Proposed salary Grade:
                                                 </div>
                                              <div class="col-md-2">
                                                     <asp:DropDownList ID="gradeDropDownList" CssClass="form-control form-control-sm SelectMe" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="ddlSalaryGrade_OnSelectedIndexChanged" ></asp:DropDownList>
                                                  </div>         <div class="col-md-1">
                                                                     Step
                                                  </div>
                                                       <div class="col-md-1">
                                                              <asp:DropDownList ID="ddlSalaryStep" CssClass="form-control form-control-sm SelectMe" runat="server"></asp:DropDownList>
                                                  </div> <div class="col-md-2"> Date of separation:   </div>     <div class="col-md-1">       <asp:TextBox ID="DateOfSeperationTextBox" ReadOnly="True" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                                    Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                    TargetControlID="DateOfSeperationTextBox" />
                                            </div>
                                            </div>
                                            </div>
                                                </div>
                                       <div class="row" style="padding: 2px;">

                                <div class="col-md-12">
                                    
                                        <div class="form-row">
                                            <div class="col-md-6">
                                                
                                        <div class="form-row">
                                                <div class="col-md-4">Designation:</div>
                                                <div class="col-md-8">  
                                                    
                                                         <asp:DropDownList runat="server"  ID="ddlDesignation" class="form-control form-control-sm SelectMe" />     <script type="text/javascript">
                                                                                                                                                                        function pageLoad() {
                                                                                                                                                                            $('.SelectMe').chosen({ disable_search_threshold: 5, search_contains: true });





                                                                                                                                                                        }
</script>

                                                </div>
                                            </div>
                                            </div>
                                            <div class="col-md-6">
                                                      <div class="form-row">
                                                <div class="col-md-4">Reporting to:</div>
                                                <div class="col-md-6">  
                                                    
                                                                 <asp:DropDownList runat="server" ID="ddlReportingBoss" class="form-control form-control-sm SelectMe" >
                                                    </asp:DropDownList>      

                                                </div>
                                            </div>
                                    

                                            </div>
                                            

                                            </div>
                                    </div>
                                                    </div>
                                    
                                    
                                    
                                                <div class="row" style="padding: 2px;">

                                <div class="col-md-12">
                                    
                                        <div class="form-row">
                                            <div class="col-md-6">
                                                
                                        <div class="form-row">
                                                <div class="col-md-4">Place of Posting:</div>
                                                <div class="col-md-8">  
                                                       <asp:DropDownList ID="officeDropDownList" CssClass="form-control form-control-sm SelectMe" runat="server" Visible="False" AutoPostBack="True" OnSelectedIndexChanged="officeDropDownList_OnSelectedIndexChanged"></asp:DropDownList>
                                                         <asp:DropDownList runat="server"  ID="placeDropDownList" class="form-control form-control-sm SelectMe" />

                                                </div>
                                            </div>
                                            </div>
                                            <div class="col-md-6">
                                                      <div class="form-row">
                                                <div class="col-md-4"> </div>
                                                <div class="col-md-6">  
                                                    
                                                            

                                                </div>
                                            </div>
                                    

                                            </div>
                                            

                                            </div>
                                    </div>
                                                    </div>
                                    
                                    
                                                   <div class="row" style="padding: 2px;">

                                <div class="col-md-12">
                                    
                                        <div class="form-row">
                                            <div class="col-md-6">
                                                
                                        <div class="form-row">
                                                <div class="col-md-4"> Date of Joining:</div>
                                                <div class="col-md-4">  
                                                    
                                                            <asp:TextBox ID="expDtJoinTextBox" CssClass="form-control form-control-sm"  runat="server"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="TopLeft"
                                                        Format="dd-MMM-yyyy" PopupButtonID="expDtJoinTextBox" CssClass="MyCalendar"
                                                        TargetControlID="expDtJoinTextBox" />

                                                </div>
                                            </div>
                                            </div>
                                            <div class="col-md-6">
                                                      <div class="form-row">
                                                <div class="col-md-4">Classification:</div>
                                                <div class="col-md-6">  
                                                    
                                                                <asp:RadioButtonList ID="typeOfPosRadioButtonList" OnSelectedIndexChanged="typeOfPosRadioButtonList_OnSelectedIndexChanged" runat="server" AutoPostBack="True" RepeatDirection="Horizontal">
                                                    </asp:RadioButtonList>     

                                                </div>
                                            </div>
                                    

                                            </div>
                                            

                                            </div>
                                    </div>
                                                    </div>
                                    
                                    
                                    
                                    
                                                   <div class="row" >

                                <div class="col-md-12">
                                    
                                        <div class="form-row" >
                                            <div class="col-md-6">
                                                
                                        <div class="form-row" style="padding: 2px;">
                                                <div class="col-md-5"> Specify period/project name:</div>
                                                <div class="col-md-4">  
                                                    
                                                           <asp:DropDownList ID="projectDropDownList" Enabled="True" CssClass="form-control form-control-sm SelectMe" runat="server"></asp:DropDownList>

                                                </div>
                                            </div>
                                            </div>
                                            <div class="col-md-6">
                                                      <div class="form-row">
                                                <div class="col-md-4"> </div>
                                                <div class="col-md-6">  
                                                     

                                                </div>
                                            </div>
                                    

                                            </div>
                                            

                                            </div>
                                    </div>
                                                    </div>
                                    
                                               <div class="row" >

                                <div class="col-md-12">
                                    
                                     
                                                
                                        <div class="form-row" style="padding: 2px;">
                                                <div class="col-md-3"> If New, is this budgeted:</div>
                                                <div class="col-md-2">  
                                                    
                                                        <asp:RadioButtonList ID="isBudgetedCheckBox" RepeatDirection="Horizontal"  runat="server" AutoPostBack="True" OnTextChanged="isBudgetedCheckBox_CheckedChanged">
                                                    <asp:ListItem> Yes </asp:ListItem>
                                                    <asp:ListItem> No</asp:ListItem>
                                                </asp:RadioButtonList>

                                                </div>
                                            
                                              <div class="col-md-5"> (Provide detailed justification for board approval)</div>
                                            </div>
                                            
                                    </div>
                                                    </div>
                                    
                                    <div class="row" >
                                         <div class="col-md-12">
                                    
                                           <div class="form-group">
                                                    <label>Justification: </label>
                                                    &nbsp;<label style="color: #a52a2a">*</label>
                                                    <asp:TextBox ID="JustificationTextBox" TextMode="MultiLine" Rows="1" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                        </div>
                                        </div>
                                    
                                    
                                    
                                                   <div class="row" style="padding: 2px;">

                                <div class="col-md-12">
                                    
                                        <div class="form-row">
                                            <div class="col-md-6">
                                                <label style="padding: 2px;"> Pre-requisite Qualification:</label>
                                        <div class="form-row" style="padding: 2px;">
                                                <div class="col-md-3"> Academic:</div>
                                                <div class="col-md-6">  
                                                    
                                                            <asp:DropDownList ID="EducationRequirementDropDownList" runat="server" CssClass="form-control form-control-sm SelectMe"></asp:DropDownList>

                                                </div>
                                            </div>
                                                
                                                
                                                 <div class="form-row" style="padding: 2px;">
                                                <div class="col-md-3"> Specialization:</div>
                                                <div class="col-md-6">  
                                                    
                                                            <asp:TextBox ID="DropDownList3" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                                </div>
                                            </div>
                                                
                                                
                                                
                                                 <div class="form-row" style="padding: 2px;">
                                                <div class="col-md-3"> Age Group:</div>
                                                <div class="col-md-6">  
                                                    
                                                            <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                                </div>
                                            </div>
                                                
                                                
                                                   <div class="form-row" style="padding: 2px;">
                                                <div class="col-md-3"> Experience:</div>
                                                <div class="col-md-6">  
                                                    
                                                            <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                                </div>
                                            </div>
                                                
                                                        <div class="form-row" style="padding: 2px;">
                                                <div class="col-md-3"> Skills:</div>
                                                <div class="col-md-6">  
                                                    
                                                            <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                                </div>
                                            </div>

                                            </div>
                                            <div class="col-md-6">
                                                      <div class="form-row">
                                                <div class="col-md-4">Preferable:</div>
                                                <div class="col-md-4">  
                                                     <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                               
                                                </div>
                                                          
                                                           <div class="col-md-4">Acceptable:</div>
                                            </div>
                                    

                                            </div>
                                            

                                            </div>
                                    </div>
                                                    </div>
                                    
                                        <div class="row" >
                                         <div class="col-md-12">
                                    
                                           <div class="form-group">
                                                    <label>Position Summary: </label>
                                                    &nbsp;<label style="color: #a52a2a">*</label>
                                                    <asp:TextBox ID="TextBox1" TextMode="MultiLine" Rows="1" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                        </div>
                                        </div>
                               
                                 </fieldset>
                            
                                   <div class="row" >
                                      <div class="col-md-6" runat="server">
                                        <div class="form-group">
                                            <label class="font-weight-bold">Comments &nbsp;</label><label style="font-size: 10px; color: gray; font-style: italic"> (*Applicable for Final Submit)</label>
                                            
                                         
                                            
                                            <asp:TextBox runat="server"   ID="txtComment" TextMode="MultiLine" Rows="2" class="form-control" />
                                        </div>
                                    </div>
                                 
                                  <div class="col-md-6" runat="server" Visible="False" id="DivShow">
                                      
                                                 <div style="max-height:180px; overflow: scroll">
                                                      <div class="form-group">
                                            <label class="font-weight-bold">Approval Status &nbsp;</label>
                                                          </div>
                                                <asp:GridView ID="AppLogCommentGridView" CssClass="table table-bordered text-center thead-dark" runat="server" Width="100%" AutoGenerateColumns="False">
                                                    <Columns>
                                                      
                                                        <asp:BoundField DataField="PreEmp" HeaderText="Initiator" HtmlEncode="False" />
                                                        <asp:BoundField DataField="ForEmp" HeaderText="Approval Person" HtmlEncode="False" />
                                                        <%--<asp:BoundField DataField="Version" HeaderText="Version" HtmlEncode="False" />--%>
                                                        <asp:BoundField DataField="Comments" HeaderText="Comments" HtmlEncode="False" />
                                                        <asp:BoundField DataField="ActionStatus" HeaderText="Action Status" HtmlEncode="False" />
                                                        

                                                        <asp:BoundField DataField="ApproveBy" HeaderText="Approved By" HtmlEncode="False" />
                                                        <asp:BoundField DataField="ApproveDate" HeaderText="Approved Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                     
                                                       
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                  </div>

                                       <link href="../UserSetup/ButtonGrups.css" rel="stylesheet" />
                                      <div class="ui-group-buttons">
 <asp:Button ID="submitButton" Text=" Save " CssClass="btn btn-sm btn-info"  OnClientClick="return confirm('Are you sure you want to Save ?')" Visible="False" runat="server" OnClick="Button2_OnClick" />
                                           <div class="or or-sm" runat="server" Visible="False" id="orBTN"></div>
                                    <asp:Button ID="btnSubmit" Text=" Submit " CssClass="btn btn-sm btn-success" OnClientClick="return confirm('Are you sure you want to Submit ?')" Visible="False" runat="server" OnClick="btnSubmit_OnClick" />
                                               <asp:HiddenField runat="server" ID="WhichButton" Value="0"/>
                                           </div>
                                     <div class="ui-group-buttons">
                                    <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-info" Visible="False" OnClientClick="return confirm('Are you sure you want to Update ?')" runat="server" OnClick="editButton_OnClick" />
                                           <div class="or or-sm" runat="server" Visible="False" id="orUp"></div>

                                    <asp:Button ID="btnUpdateforSubmit" Text="Submit" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="btnUpdateforSubmit_OnClick" OnClientClick="return confirm('Are you sure you want to Submit ?')" />
                                           </div>
                                   
                                    <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />

                                    <%--<asp:Button ID="Button2" Text="Save" CssClass="btn btn-info btn-sm" runat="server" OnClick="Button2_OnClick" />--%>
                                    <asp:Button ID="Button3" Text="Cancel" CssClass="btn btn-warning btn-sm" runat="server" Visible="False" />



                

                                        
                           
                                </div>
                            </div>
                            
                                   
                                </div>

                            </div>
    </div>
                            </div>
                        </div>
                    </div>
                </div>
                </div>


                </div>
                </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>


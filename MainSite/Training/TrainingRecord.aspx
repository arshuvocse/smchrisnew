<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="TrainingRecord.aspx.cs" Inherits="Training_TrainingRecord" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
 
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <style type="text/css">
        
          div#cpFormBody_ddlEmpInfo_chosen {
            width: 100%!important;
        }
            div#cpFormBody_pop_ddlGrade_chosen {
            width: 100%!important;
        }

             div#cpFormBody_pop_ddlDepartment_chosen {
            width: 100%!important;
        }
         .modalBackground {
             background-color: Black;
             filter: alpha(opacity=40);
             opacity: 0.4;
         }

        .modalPopup {
            background-color: #FFFFFF;
            width: 300px;
            border: 3px solid #0DA9D0;
        }
    </style>
    
      <style>
            
          #cpFormBody_gvTrainner  > tbody > tr > th {
              padding: 9px 0;
              color: #fff;
              background-color: #5B799E;
              /*background-color: #98A9C0;*/
          }

          #cpFormBody_gvTrainner > tbody > tr:not(th):nth-child(odd) {
              background-color: #DFDFDF;
          }


          #cpFormBody_gv_allocateEmp  > tbody > tr > th {
              padding: 9px 0;
              color: #fff;
              background-color: #5B799E;
              /*background-color: #98A9C0;*/
          }

          #cpFormBody_gv_allocateEmp > tbody > tr:not(th):nth-child(odd) {
              background-color: #DFDFDF;
          }


          #cpFormBody_gvTrainner > tbody > tr:not(th):nth-child(odd) {
              background-color: #DFDFDF;
          }


          #cpFormBody_gv_selectedEmp  > tbody > tr > th {
              padding: 9px 0;
              color: #fff;
              background-color: #07619D;
              /*background-color: #98A9C0;*/
          }

          #cpFormBody_gv_selectedEmp > tbody > tr:not(th):nth-child(odd) {
              background-color: #DFDFDF;
          }

        
      </style>
    <style type="text/css">
        /*AutoComplete flyout */
      
        </style>


    <style>
        fieldset.for-panel {
            background-color: #fcfcfc;
            border: 1px solid #999;
            padding: 15px 10px;
            background-color: white;
            margin-bottom: 12px;
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
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>
                <div class="container-fluid">
                    <div class="page-heading" >
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" />  Training Records Entry</h1>
                        </div>
                        <%--  <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="detailsViewButton" Text="Training Records" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                        </div>--%>

                        <div class="page-heading__container float-right d-none d-sm-block">
                            
                               <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                        <asp:LinkButton ID="detailsViewButton"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
                            
                            
                        </div>

                    </div>
                    <div class="card">
                        <div class="card-body">
                            <div class="form-row">
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Company</label>
                                        <span style="color: red">&nbsp;*</span>
                                        <asp:DropDownList runat="server" ID="ddlCompany" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" CssClass="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                        <span style="color: red">&nbsp;*</span>
                                        <asp:DropDownList ID="ddlFinancialYear" OnSelectedIndexChanged="ddlFinancialYear_OnSelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <fieldset class="for-panel" runat="server">
                                <legend>Training Information</legend>
                                <div class="form-row">

                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>Training Type</label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:DropDownList ID="ddlTrainingType" runat="server" class="form-control form-control-sm selectme"></asp:DropDownList>
                                                <script type="text/javascript">
                                                    function pageLoad() {
                                                        $('.selectme').chosen({ disable_search_threshold: 5, search_contains: true });

                                                    }
                                                        </script>
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Budget Head</label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:DropDownList ID="ddlBudgetHead" runat="server" class="form-control form-control-sm selectme"></asp:DropDownList>
                                        </div>
                                    </div>



                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Training Title</label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:TextBox ID="txtTrainingTitle" runat="server" class="form-control form-control-sm"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Training Details</label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:TextBox ID="txtTrainingDetails" runat="server" class="form-control form-control-sm"></asp:TextBox>

                                        </div>
                                    </div>

                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>Training Organization</label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:DropDownList ID="ddlTrainingOrg" AutoPostBack="True" OnSelectedIndexChanged="ddlTrainingOrg_OnSelectedIndexChanged" runat="server" class="form-control form-control-sm selectme"></asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Location</label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:DropDownList ID="ddlLocation" runat="server" class="form-control form-control-sm selectme"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-1.2">
                                        <br />
                                        <br />
                                        <asp:CheckBox runat="server" ID="isSmcVanue" OnCheckedChanged="isSmcVanue_OnCheckedChanged" AutoPostBack="True" />
                                        <label>Is SMC Venue</label>

                                    </div>
                                    <div class="col-md-3" runat="server" id="venueDiv" visible="False">
                                        <div class="form-group">
                                            <label>Select Venue</label>
                                            <asp:DropDownList runat="server" ID="ddlVenue" CssClass="form-control form-control-sm selectme" />
                                        </div>
                                    </div>



                                </div>
                                <hr />
                                <div class="form-row">
                                    <div class="col-md-2">
                                        <br />
                                        <div class="form-group">
                                            <asp:Button runat="server" ID="btnAddEmployee" OnClick="btnAddEmployee_OnClick" Text="Add Participants " CssClass="btn btn-sm btnMyDesignDraft" />
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Total Participant</label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:TextBox ID="txtTotalParticipant" runat="server" CssClass="form-control form-control-sm" Enabled="False"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Trainer Cost</label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:TextBox ID="txtTrainingCost" AutoPostBack="True" OnTextChanged="txtTrainingCost_OnTextChanged" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                TargetControlID="txtTrainingCost" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Food & Venue Cost</label>

                                            <asp:TextBox ID="txtLogistic" AutoPostBack="True" OnTextChanged="txtTrainingCost_OnTextChanged" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                TargetControlID="txtLogistic" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Other Cost</label>

                                            <asp:TextBox ID="txtOtherCost" AutoPostBack="True" OnTextChanged="txtTrainingCost_OnTextChanged" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                                TargetControlID="txtOtherCost" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Grand Total</label>

                                            <asp:TextBox ID="txtGrandTotal" runat="server" CssClass="form-control form-control-sm" Enabled="False"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Cost Per Participant</label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:TextBox ID="txtCostPerParticipant" runat="server" CssClass="form-control form-control-sm" Enabled="False"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-1.5">
                                        <div class="form-group">
                                            <label>Start Date</label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:TextBox ID="txtStartDate" AutoPostBack="True" OnTextChanged="txtStartDate_OnTextChanged" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                TargetControlID="txtStartDate" />
                                        </div>
                                    </div>
                                    <div class="col-md-1.5">
                                        <div class="form-group">
                                            <label>End Date</label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:TextBox ID="txtEndDate" AutoPostBack="True" OnTextChanged="txtEndDate_OnTextChanged" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                TargetControlID="txtEndDate" />
                                        </div>
                                    </div>
                                    <div class="col-md-1.5" runat="server" visible="False">
                                        <div class="form-group">
                                            <label>Start Time</label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:TextBox ID="txtStartTime" OnTextChanged="txtStartTime_OnTextChanged" TextMode="Time" onpaste="return txtStartTime_OnTextChanged" onkeypress="return txtStartTime_OnTextChanged" runat="server" AutoPostBack="True" class="form-control form-control-sm"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-md-1.5" runat="server" visible="False">
                                        <div class="form-group">
                                            <label>End Time</label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:TextBox ID="txtEndTime" OnTextChanged="txtEndTime_OnTextChanged" TextMode="Time" onpaste="return txtEndTime_OnTextChanged" onkeypress="return txtEndTime_OnTextChanged" runat="server" AutoPostBack="True" class="form-control form-control-sm"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-md-6" runat="server" visible="False">
                                        <div class="form-group">
                                            <label>Select Days</label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:CheckBoxList ID="chkDays" OnSelectedIndexChanged="chkDays_OnSelectedIndexChanged" AutoPostBack="True" RepeatDirection="Horizontal" runat="server" />
                                        </div>
                                    </div>


                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Days</label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:GridView ID="gv_daylist" runat="server" AutoGenerateColumns="false"     CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender" >

                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkDay" AutoPostBack="true" runat="server" OnCheckedChanged="chkDay_CheckedChanged"></asp:CheckBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Day">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Day" runat="server" class="form-control form-control-sm" Text='<%#Eval("Day") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                </Columns>

                                            </asp:GridView>

                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        
                                        <div class="row">
                                           
                                            <label></label>
                                           

                                                <div class="col-md-4">
                                                     <div class="form-group">
                                                      <asp:CheckBox ID="chk_Common" runat="server" Text=" Is Common" AutoPostBack="True" TextAlign="Right" OnCheckedChanged="chk_Common_OnCheckedChanged"></asp:CheckBox>
                                                         </div>
                                                </div>
                                         
                                                  




                                            <div class="col-md-4">
                                                    <label>Start Time</label>
                                                    <asp:TextBox ID="txt_deadLine" runat="server" CssClass="form-control form-control-sm" OnTextChanged="txt_deadLine_Changed" AutoPostBack="True"  TextMode="Time"></asp:TextBox>
                                               
                                            </div>
                                                 <div class="col-md-4">
                                              <label>End Time</label>
                                                    <asp:TextBox ID="txtEndTimeComm" runat="server" CssClass="form-control form-control-sm" OnTextChanged="txtEndTimeComm_Changed" AutoPostBack="True"  TextMode="Time"></asp:TextBox>
                                                   
                                                     </div>
                                                 <br/>
                                        </div>
                                       
                                           <br/>   <br/>   
                                            
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"   CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender">

                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkDay" AutoPostBack="True" OnCheckedChanged="chkDay_OnCheckedChanged" runat="server"></asp:CheckBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Date" runat="server" class="form-control form-control-sm" Text='<%#Eval("Date") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Day">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Day" runat="server" class="form-control form-control-sm" Text='<%#Eval("DayName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Start Time ">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="StartTime" TextMode="Time" runat="server" AutoPostBack="True" OnTextChanged="StartTime_OnTextChanged" class="form-control form-control-sm" Text='<%#Eval("StartTime") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="End Time ">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="EndTime" TextMode="Time" runat="server" AutoPostBack="True" OnTextChanged="EndTime_OnTextChanged" class="form-control form-control-sm" Text='<%#Eval("EndTime") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                </Columns>

                                            </asp:GridView>

                                        </div>
                                    </div>

                                
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Total Days</label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:TextBox ID="txtTotalDays" Enabled="False" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Total Hours</label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:TextBox ID="txtTotalTrainingHoures" Enabled="False" CssClass="form-control form-control-sm" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </fieldset>

                            <fieldset class="for-panel" runat="server">
                                <legend>Trainer Information</legend>
                                <div class="form-row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Trainer</label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:DropDownList ID="ddlTrainer" runat="server" class="form-control form-control-sm selectme"></asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="col-md-1">
                                        <div class="form-group" style="padding-top: 17px;">
                                         
                                          
                                             <asp:LinkButton runat="server" ID="AddTrainner"   OnClick="AddTrainner_OnClick"    CssClass="btn btnMyDesignOne   btn-sm pull-right"><span aria-hidden="true" class="fa fa-plus"></span>  &nbsp; Add </asp:LinkButton> 
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group"  style="padding-top: 17px;">
                                            
                                        
                                             
                                            <asp:CheckBox runat="server" ID="notListedCheck" AutoPostBack="true" OnCheckedChanged="notListedCheck_OnCheckedChanged" Text="Not Listed" />
                                        </div>
                                    </div>
                                    <div class="col-md-2" id="notListedNameDiv" runat="server" visible="false">
                                        <div class="form-group">
                                            <label>Name</label>
                                            <asp:TextBox ID="txt_NotListedTrainer" runat="server" CssClass="form-control  form-control-sm"></asp:TextBox>

                                        </div>
                                    </div>

                                    <div class="col-md-2" id="notListedDetailsDiv" runat="server" visible="false">
                                        <div class="form-group">
                                            <label>Details</label>
                                            <asp:TextBox ID="txt_NotListedTrainerDetails" runat="server" CssClass="form-control  form-control-sm"></asp:TextBox>

                                        </div>

                                    </div>

                                    <div class="col-md-1">
                                        <div class="form-group"  style="padding-top: 17px;">
                                           
                                            
                                               <asp:LinkButton runat="server" ID="AddNotListed"  Visible="false" OnClick="AddNotListed_OnClick"    CssClass="btn btnMyDesignOne   btn-sm pull-right"><span aria-hidden="true" class="fa fa-plus"></span>  &nbsp; Add </asp:LinkButton> 
                                          
                                        </div>
                                    </div>
                                </div>

                                <div class="form-row">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-2"></div>
                                            <div class="col-md-8">
                                                <asp:GridView  ID="gvTrainner" Width="100%"   CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender" AutoGenerateColumns="false" runat="server">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL#">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                                <%--<asp:HiddenField runat="server" ID="hdpkd" Value='<%#Eval("BoardDetailsId") %>'/>--%>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Trainner">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txt_Trainner" runat="server" class="form-control form-control-sm" Text='<%#Eval("TrainerName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Details">
                                                            <ItemTemplate>

                                                                <asp:Label Visible="false" ID="txt_trainerID" runat="server" class="form-control form-control-sm" Text='<%#Eval("TrainerId") %>'></asp:Label>
                                                                <asp:Label ID="txt_TrainnerDetails" runat="server" class="form-control form-control-sm" Text='<%#Eval("TrainerDetails") %>'></asp:Label>

                                                                <%--<asp:HiddenField runat="server" ID="trainnerId"   />--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Operation">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lb_RemoveTrainer" OnClick="lb_RemoveTrainer_OnClick" runat="server"> <img src="../Assets/img/delete.png"/></asp:LinkButton>
                                                            </ItemTemplate>


                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-md-2"></div>
                                        </div>
                                    </div>
                                </div>


                            </fieldset>


                              <div class="row">
                                      <div class="col-md-4" runat="server">
                                        <div class="form-group">
                                            <label class="font-weight-bold">Comments &nbsp;</label><label style="font-size: 10px; color: gray; font-style: italic"> (*Applicable for Final Submit)</label>
                                            
                                         
                                            
                                            <asp:TextBox runat="server"   ID="txtComment" TextMode="MultiLine" Rows="2" class="form-control" />
                                        </div>
                                    </div>
                                 
                                  <div class="col-md-8" runat="server" Visible="False" id="DivShow">
                                      
                                                 <div style="max-height:180px; overflow: scroll">
                                                      <div class="form-group">
                                            <label class="font-weight-bold">Approval Status &nbsp;</label>
                                                          </div>
                                                <asp:GridView ID="AppLogCommentGridView"  CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender" runat="server" Width="100%" AutoGenerateColumns="False">
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


                            <asp:HiddenField runat="server" ID="hdpk" />
                            
                               <link href="../UserSetup/ButtonGrups.css" rel="stylesheet" />
                                      <div class="ui-group-buttons">
                                           <asp:HiddenField runat="server" ID="WhichButton" Value="0"/>
                            <asp:Button ID="submitButton" Text="Save" CssClass="btn btn-sm btn-info" Visible="False" OnClientClick="return confirm('Are you sure you want to Save ?')" runat="server" OnClick="btn_Save_OnClick" />
                                             <div class="or or-sm" runat="server" Visible="False" id="orBTN"></div>
                                    <asp:Button ID="btnSubmit" Text=" Submit " CssClass="btn btn-sm btn-success" OnClientClick="return confirm('Are you sure you want to Submit ?')" Visible="False" runat="server" OnClick="btnSubmit_OnClick" />
                                          
                                           </div>
                             <div class="ui-group-buttons">
                            <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                   <div class="or or-sm" runat="server" Visible="False" id="orUp"></div>

                                    <asp:Button ID="btnUpdateforSubmit" Text="Submit" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="btnUpdateforSubmit_OnClick" OnClientClick="return confirm('Are you sure you want to Submit ?')" />
                                           </div>
                            <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />

                            <%--<asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" CssClass="btn btn-sm btn-info" Text="Submit"></asp:Button>--%>

                            <%--                            <asp:Button ID="cancelButton" Text="Cancel" OnClick="cancelButton_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />--%>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div>
        <ajaxToolkit:ModalPopupExtender ID="mpe_1" runat="server" TargetControlID="hnd_Test" PopupControlID="pnl_1"
            BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>

        <asp:HiddenField ID="hnd_Test" runat="server"></asp:HiddenField>
        <asp:Panel ID="pnl_1" runat="server" Style="display: none; overflow: scroll; padding: 10px" Height="650px" Width="80%" CssClass="modalPopup">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                     
                                                                                                 <div class="row">
                                                    <div class="col-md-6" style=";padding-left: 15px;">
                                                          <div class="text-left">
                                                    <h2 class="elegantshd">
                                                        <asp:Label ID="lblHeader" Text="Select Participants" runat="server"></asp:Label></h2>
                                                </div>
                                                    </div>
                                                    
                                                    <div class="col-md-6" style="padding-top: 15px;padding-right: 15px;">
                                                          <asp:LinkButton ID="LinkButton1"   OnClick="btnNo_Click" CssClass="btn btn-sm btn-danger pull-right  pull-right" runat="server"><i style="font-size: 16px" class="fa fa-times" aria-hidden="true"></i>
</asp:LinkButton>
                                                    </div>
                                                </div>
                                             
                                                                            <style>
                                                                                .elegantshd {
                                                                                    color: #131313;
  
                                                                                    letter-spacing: .15em;
                                                                                    text-shadow: 1px 1px 2px #000000;
                                                                                    font-size: 20px !important;
                                                                                    font-family: 'Kreon', serif;
                                                                                    vertical-align:middle;  text-decoration-style: wavy;
                                                                                }
                                                                            </style>
                        <asp:Label ForeColor="#8B0000" Font="Bold" runat="server" ID="m_MemberName"></asp:Label>
                                                                        </span></h1>
                    <hr/>
                    <div>

                        <div class="form-row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Company  </label>
                                    <asp:DropDownList runat="server" ID="pop_ddlCompany" OnSelectedIndexChanged="pop_ddlCompany_OnSelectedIndexChanged" class="form-control form-control-sm" AutoPostBack="True" />

                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label class="">Department </label>

                                    <asp:DropDownList runat="server" ID="pop_ddlDepartment" class="form-control form-control-sm selectme" />
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label class="">Grade </label>

                                    <asp:DropDownList runat="server" ID="pop_ddlGrade" class="form-control form-control-sm selectme" />
                                </div>
                            </div>
                            
                             <div  class="col-md-3">

                                     
                                        <div class="form-group">
                                            <label>Single Employee</label>
                                            
                                               <asp:DropDownList   runat="server"   ID="ddlEmpInfo" class="form-control form-control-sm selectme" />
                                                    
                                           

                                        </div>
                                    </div>
                            <div class="col-md-3" style="margin-top: 16px">
                                
                                 <asp:LinkButton runat="server" ID="pop_btnSearch" OnClick="pop_btnSearch_OnClick"   CssClass="btn btnMyDesignSearch   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search Information </asp:LinkButton>
                                
                            </div>

                        </div>
                         <div id="sda" style="overflow: auto; width: auto;">
                        <asp:GridView ID="gv_allocateEmp" Width="100%"  CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender" AutoGenerateColumns="false" runat="server">
                            <Columns>
                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="cbCheckAll" runat="server" Text="Operation" OnCheckedChanged="cbCheckAll_OnCheckedChanged"
                                            AutoPostBack="true" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="txt_check" AutoPostBack="true" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ID">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_empCode" runat="server" Text='<%#Eval("EmpMasterCode") %>'></asp:Label>

                                        <asp:HiddenField runat="server" ID="empInfoId" Value='<%#Eval("EmpInfoId") %>' />
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Name">


                                    <ItemTemplate>
                                        <asp:Label ID="txt_employee" runat="server"   Text='<%#Eval("EmpName") %>'></asp:Label>



                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_designation" runat="server"  Text='<%#Eval("Designation") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Department">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_dptName" runat="server"   Text='<%#Eval("DepartmentName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Grade">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_grdName" runat="server"  Text='<%#Eval("GradeName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>




                            </Columns>
                        </asp:GridView>
                             </div>
                      
                          <br />
                        
                        
                         <div class="row">
                              <div class="col-md-12">
                                  <asp:LinkButton runat="server" ID="btnAddEmpList"   OnClick="btnAddEmpList_OnClick"    CssClass="btn btnMyDesignOne   btn-sm pull-right"><span aria-hidden="true" class="fa fa-plus"></span>  &nbsp; Add to List </asp:LinkButton> 
                              </div>
                         </div>
                           
                          <br />
                        <div class="row">
                             <div class="col-md-12">
                                  <div id="sdsa" style="overflow: auto; width: auto;">
                        <asp:GridView ID="gv_selectedEmp"  CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender"  AutoGenerateColumns="false" runat="server">
                            <Columns>
                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>

                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="ID">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_empCode" runat="server"  Text='<%#Eval("EmpMasterCode") %>'></asp:Label>

                                        <asp:HiddenField runat="server" ID="empInfoId" Value='<%#Eval("EmpInfoId") %>' />
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Name">


                                    <ItemTemplate>
                                        <asp:Label ID="txt_employee" runat="server"   Text='<%#Eval("EmpName") %>'></asp:Label>



                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_designation" runat="server"  Text='<%#Eval("Designation") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Department">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_dptName" runat="server"   Text='<%#Eval("DepartmentName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Grade">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_grdName" runat="server" Text='<%#Eval("GradeName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Operation">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnselectedEmpRemove" runat="server" OnClick="btnselectedEmpRemove_OnClick" >
                                            <img src="../Assets/img/delete.png"/>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>




                            </Columns>
                        </asp:GridView>
                                </div>
                             </div>
                        </div>
                        <br />

                         <hr/>


                        <asp:Button runat="server" ID="btnEmpSubmit" OnClick="btnEmpSubmit_OnClick" Text="Submit " CssClass="btn btn-sm btn-info" />
                        <asp:Button ID="btnNo" Text="Cancel" OnClick="btnNo_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                           <br />
                           <br />
                           <br />

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
</asp:Content>


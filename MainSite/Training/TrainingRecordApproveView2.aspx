<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="TrainingRecordApproveView2.aspx.cs" Inherits="Training_TrainingRecord" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <style type="text/css">
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
                      <div class="page-heading"  >
                      <div class="page-heading__container">
                        <div class="icon"> <img src="../Assets/2076-512.png" height="28px" /></div>
                                <span>Approval Request</span>
                        <h1 class="title" style="font-size: 18px; padding-top: 1px;">Training Record Approval</h1>
                    </div>
                        <%--  <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="detailsViewButton" Text="Training Records" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                        </div>--%>

                        <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="homeButton" Visible="False" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                            <asp:Button ID="ListViewButton" Visible="False" Text="Go Back" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                            
                             <asp:Button ID="detailsViewButton" Text="<<< Back to List" CssClass="btn btn-sm text-info" runat="server" OnClick="detailsViewButton_OnClick" style="box-shadow: 0 0 3px 1px rgba(0,0,0,.35);" />
                        </div>

                    </div>
                    <div class="card">
                        <div class="card-body">
                            <div class="form-row">
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Company</label>
                                        
                                        <asp:DropDownList runat="server" ID="ddlCompany" Enabled="False" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" CssClass="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                     
                                        <asp:DropDownList ID="ddlFinancialYear"  Enabled="False"  OnSelectedIndexChanged="ddlFinancialYear_OnSelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <fieldset class="for-panel" runat="server">
                                <legend>Training Information</legend>
                                <div class="form-row">

                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>Training Type</label>
                                           
                                            <asp:DropDownList ID="ddlTrainingType" runat="server"  Enabled="False"   CssClass="form-control form-control-sm"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Budget Head</label>
                                            
                                            <asp:DropDownList ID="ddlBudgetHead"  Enabled="False"   runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                                        </div>
                                    </div>



                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Training Title</label>
                                            
                                            <asp:TextBox ID="txtTrainingTitle" ReadOnly="True" runat="server" class="form-control form-control-sm"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Training Details</label>
                                           
                                            <asp:TextBox ID="txtTrainingDetails"  ReadOnly="True"  runat="server" class="form-control form-control-sm"></asp:TextBox>

                                        </div>
                                    </div>

                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>Training Organization</label>
                                            
                                            <asp:DropDownList ID="ddlTrainingOrg"   Enabled="False"   AutoPostBack="True" OnSelectedIndexChanged="ddlTrainingOrg_OnSelectedIndexChanged" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Location</label>
                                            
                                            <asp:DropDownList ID="ddlLocation"   Enabled="False"  runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-1.2">
                                        <br />
                                        <br />
                                        <asp:CheckBox runat="server" ID="isSmcVanue"  Enabled="False"  OnCheckedChanged="isSmcVanue_OnCheckedChanged" AutoPostBack="True" />
                                        <label>Is SMC Venue</label>

                                    </div>
                                    <div class="col-md-3" runat="server" id="venueDiv" visible="False">
                                        <div class="form-group">
                                            <label>Select Venue</label>
                                            <asp:DropDownList runat="server"   Enabled="False"   ID="ddlVenue" CssClass="form-control form-control-sm" />
                                        </div>
                                    </div>



                                </div>
                                <hr />
                                <div class="form-row">
                                      <div class="col-md-2" style="display: none">
                                       <br/>
                                        <div class="form-group">
                                            <asp:Button runat="server" ID="btnAddEmployee" OnClick="btnAddEmployee_OnClick" Text="Add Participants " CssClass="btn btn-sm btn-dark" />
                                        </div>
                                    </div>
                                    <div class="col-md-2" style="display: none">
                                        <div class="form-group">
                                            <label>Total Participant</label>
                                          
                                            <asp:TextBox ID="txtTotalParticipant" runat="server" CssClass="form-control form-control-sm" Enabled="False"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Trainer Cost</label>
                                     
                                            <asp:TextBox ID="txtTrainingCost" ReadOnly="True" AutoPostBack="True" OnTextChanged="txtTrainingCost_OnTextChanged"   runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                          
                                             </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Food & Venue Cost</label>
                                           
                                            <asp:TextBox ID="txtLogistic"  ReadOnly="True"  AutoPostBack="True" OnTextChanged="txtTrainingCost_OnTextChanged"   runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        
                                              </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Other Cost</label>
                                            
                                            <asp:TextBox ID="txtOtherCost"  ReadOnly="True"  AutoPostBack="True" OnTextChanged="txtTrainingCost_OnTextChanged"   runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        
                                             </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Grand Total</label>
                                           
                                            <asp:TextBox ID="txtGrandTotal" ReadOnly="True"   runat="server" CssClass="form-control form-control-sm"  ></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Cost Per Participant</label>
                                             
                                            <asp:TextBox ID="txtCostPerParticipant"  ReadOnly="True"   runat="server" CssClass="form-control form-control-sm" Enabled="False"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-1.5">
                                        <div class="form-group">
                                            <label>Start Date</label>
                                          
                                            <asp:TextBox ID="txtStartDate" AutoPostBack="True" OnTextChanged="txtStartDate_OnTextChanged" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                TargetControlID="txtStartDate" />
                                        </div>
                                    </div>
                                    <div class="col-md-1.5">
                                        <div class="form-group">
                                            <label>End Date</label>
                                           
                                            <asp:TextBox ID="txtEndDate"  ReadOnly="True"   AutoPostBack="True" OnTextChanged="txtEndDate_OnTextChanged" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                           
                                        </div>
                                    </div>
                                    <div class="col-md-1.5" runat="server" Visible="False">
                                        <div class="form-group">
                                            <label>Start Time</label>
                                          
                                            <asp:TextBox ID="txtStartTime"  ReadOnly="True"   OnTextChanged="txtStartTime_OnTextChanged" TextMode="Time" onpaste="return txtStartTime_OnTextChanged" onkeypress="return txtStartTime_OnTextChanged" runat="server" AutoPostBack="True" class="form-control form-control-sm"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-md-1.5" runat="server" Visible="False">
                                        <div class="form-group">
                                            <label>End Time</label>
                                            
                                            <asp:TextBox ID="txtEndTime"  ReadOnly="True"   OnTextChanged="txtEndTime_OnTextChanged" TextMode="Time" onpaste="return txtEndTime_OnTextChanged" onkeypress="return txtEndTime_OnTextChanged" runat="server" AutoPostBack="True" class="form-control form-control-sm"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-md-6" runat="server" Visible="False">
                                        <div class="form-group">
                                            <label>Select Days</label>
                                            
                                            <asp:CheckBoxList ID="chkDays" OnSelectedIndexChanged="chkDays_OnSelectedIndexChanged" AutoPostBack="True" RepeatDirection="Horizontal" runat="server" />
                                        </div>
                                    </div>
                                    

                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Participant</label>
                                              <div style="overflow: scroll;  ">
                                              <asp:GridView ID="gv_selectedEmp" Width="100%" CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="false" runat="server">
                            <Columns>
                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>

                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="ID">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_empCode" runat="server"   Text='<%#Eval("EmpMasterCode") %>'></asp:Label>

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
                                        <asp:Label ID="txt_grdName" runat="server"   Text='<%#Eval("GradeName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Operation" Visible="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnselectedEmpRemove" runat="server" OnClick="btnselectedEmpRemove_OnClick" Text="Remove"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>




                            </Columns>
                        </asp:GridView>
                                            </div>
                                             
                                            <asp:GridView ID="gv_daylist" Visible="False" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">

                                            <Columns>
                                                <asp:TemplateField Visible="False">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkDay" AutoPostBack="true" Enabled="False" runat="server" OnCheckedChanged="chkDay_CheckedChanged" ></asp:CheckBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Day">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Day"  runat="server" class="form-control form-control-sm" Text='<%#Eval("Day") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                

                                            </Columns>

                                        </asp:GridView>

                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label></label>
                                     
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">

                                            <Columns>
                                                <asp:TemplateField  Visible="False">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkDay" AutoPostBack="True" OnCheckedChanged="chkDay_OnCheckedChanged" runat="server" ></asp:CheckBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Date" runat="server" class="form-control form-control-sm"  Text='<%#Eval("Date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Day">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Day" runat="server" class="form-control form-control-sm" Text='<%#Eval("DayName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Start Time ">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="StartTime" ReadOnly="True" TextMode="Time" runat="server" AutoPostBack="True" OnTextChanged="StartTime_OnTextChanged" class="form-control form-control-sm" Text='<%#Eval("StartTime") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="End Time ">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="EndTime"  ReadOnly="True" TextMode="Time" runat="server" AutoPostBack="True" OnTextChanged="EndTime_OnTextChanged" class="form-control form-control-sm" Text='<%#Eval("EndTime") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                

                                            </Columns>

                                        </asp:GridView>

                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-md-1">
                                        <div class="form-group">
                                            <label>Total Days</label>
                                             
                                            <asp:TextBox ID="txtTotalDays" Enabled="False" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-1">
                                        <div class="form-group">
                                            <label>Total Hours</label>
                                          
                                            <asp:TextBox ID="txtTotalTrainingHoures" Enabled="False" CssClass="form-control form-control-sm" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                
                                <div style="overflow: scroll;  ">
                                   <asp:GridView   ID="gvTrainner" Width="100%" CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="false" runat="server">
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

                                                        <asp:Label Visible="false" ID="txt_trainerID" runat="server"  Text='<%#Eval("TrainerId") %>'></asp:Label>
                                                        <asp:Label ID="txt_TrainnerDetails" runat="server"   Text='<%#Eval("TrainerDetails") %>'></asp:Label>

                                                        <%--<asp:HiddenField runat="server" ID="trainnerId"   />--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Operation" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lb_RemoveTrainer" OnClick="lb_RemoveTrainer_OnClick" runat="server">Remove</asp:LinkButton>
                                                    </ItemTemplate>


                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                </div>
                            </fieldset>

                            <fieldset class="for-panel" runat="server" Visible="False">
                                <legend>Trainner Information</legend>
                                <div class="form-row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Trainner</label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:DropDownList ID="ddlTrainer" runat="server" class="form-control form-control-sm"></asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="col-md-1">
                                        <div class="form-group">
                                            <label>&nbsp</label>
                                            <asp:Button runat="server" ID="AddTrainner" OnClick="AddTrainner_OnClick" CssClass="form-control btn btn-success btn-sm" Text="Add"></asp:Button>
                                        </div>
                                    </div>
                                    <div class="col-md-1.2">
                                        <div class="form-group">
                                            <br />
                                            <br />
                                            <label>Not Listed</label>
                                            <asp:CheckBox runat="server" ID="notListedCheck" AutoPostBack="true" OnCheckedChanged="notListedCheck_OnCheckedChanged" />
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
                                        <div class="form-group">
                                            <label>&nbsp</label>
                                            <asp:Button runat="server" ID="AddNotListed" Visible="false" OnClick="AddNotListed_OnClick" CssClass="form-control btn btn-success btn-sm" Text="Add"></asp:Button>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-row">
                                    <div class="col-md-12">
                                   <div class="row">
                                            <div class="col-md-2"></div>
                                        <div class="col-md-8">
                                     
                                            </div>
                                         <div class="col-md-2"></div>
                                   </div>
                                    </fieldset>

                             
                                        <div class="form-row">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                <label>Approval Status </label>
                                                &nbsp;<label style="color: #a52a2a">*</label>
                                                <asp:RadioButtonList ID="actionRadioButtonList" runat="server" RepeatDirection="Horizontal">
                                                    
                                                </asp:RadioButtonList>
                                                </div>
                                             </div>
                                            </div>
                            

                                                   <div class="row">
                                      <div class="col-md-4" runat="server">
                                        <div class="form-group">
                                            <label class="font-weight-bold">Comments &nbsp;</label>
                                            
                                         
                                            
                                            <asp:TextBox runat="server"   ID="txtComment" TextMode="MultiLine" Rows="2" class="form-control" />
                                        </div>
                                    </div>
                                 
                                  <div class="col-md-8" runat="server" Visible="False" id="DivShow">
                                      
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
                                      
                            
                             <asp:HiddenField runat="server" ID="hdpk" />
                            
                            
                             <link href="../UserSetup/ButtonGrups.css" rel="stylesheet" />
                                      <div class="ui-group-buttons">
                            
                             <asp:LinkButton runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" Text="Save " CssClass="btn btn-sm btn-success" OnClientClick="return confirm('Are you sure you want to Submit ?')" style="box-shadow: 0 0 3px 1px rgba(0,0,0,.35);" ><i class="fa fa-check" aria-hidden="true"></i> &nbsp; Submit

&nbsp; </asp:LinkButton>  
                                    <asp:LinkButton ID="Button1" Text="Submit" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="Button1_OnClick" OnClientClick="return confirm('Are you sure you want to Submit ?')" style="box-shadow: 0 0 3px 1px rgba(0,0,0,.35);" ><i class="fa fa-check" aria-hidden="true"></i> &nbsp; Submit

&nbsp; </asp:LinkButton>  
                                           <div class="or or-sm" runat="server"   id="orBTN"></div>
                                <asp:LinkButton ID="Button2a"   CssClass="btn btn-sm btn-danger"  OnClientClick="return confirm('Are you sure you want to Reject ?')"  runat="server" OnClick="Button2a_OnClick" ><i class="fa fa-times" aria-hidden="true"></i>
&nbsp; Reject </asp:LinkButton> 
                                      </div>
                                    <asp:HiddenField ID="entryempinfoIdHiddenField" runat="server" />
                                    <asp:HiddenField ID="actionstatusHiddenField" runat="server" />
                                </div>

                                
                           





                           
                        <%--<asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
                        <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />

                            <%--<asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" CssClass="btn btn-sm btn-info" Text="Submit"></asp:Button>

                            <asp:Button ID="cancelButton" Text="Cancel" OnClick="cancelButton_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />--%>
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
        <asp:Panel ID="pnl_1" runat="server" Style="display: none;overflow: scroll;padding: 10px" Height="500px" Width="90%" CssClass="modalPopup">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <h3 style="text-align: center;">Select Participants <span>
                        <asp:Label ForeColor="#8B0000" Font="Bold" runat="server" ID="m_MemberName"></asp:Label></span></h3>
                    <div>

                        <div class="form-row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Company  </label>
                                    <asp:DropDownList runat="server" ID="pop_ddlCompany" OnSelectedIndexChanged="pop_ddlCompany_OnSelectedIndexChanged" class="form-control form-control-sm" AutoPostBack="True" />

                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label class="">Department </label>

                                    <asp:DropDownList runat="server" ID="pop_ddlDepartment" class="form-control form-control-sm" />
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label class="">Grade </label>

                                    <asp:DropDownList runat="server" ID="pop_ddlGrade" class="form-control form-control-sm" />
                                </div>
                            </div>
                            <div class="col-md-3">
                                <br/>
                                
                                <asp:Button runat="server" ID="pop_btnSearch" OnClick="pop_btnSearch_OnClick" Text="Search " CssClass="btn btn-sm btn-info" />
                            </div>

                        </div>

                        <asp:GridView ID="gv_allocateEmp" Width="100%" CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="false" runat="server">
                            <Columns>
                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField>
                                      <HeaderTemplate>
                        <asp:Checkbox ID="cbCheckAll" runat="server" Text="Operation"  OnCheckedChanged="cbCheckAll_OnCheckedChanged"
                        AutoPostBack="true" />
                    </HeaderTemplate>
                                     
                                    <ItemTemplate>
                                    
                                        <asp:CheckBox ID="txt_check"    AutoPostBack="true" runat="server"></asp:CheckBox>
                                    </ItemTemplate>


                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="ID">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_empCode" runat="server" class="form-control form-control-sm" Text='<%#Eval("EmpMasterCode") %>'></asp:Label>

                                        <asp:HiddenField runat="server" ID="empInfoId" Value='<%#Eval("EmpInfoId") %>' />
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Name">


                                    <ItemTemplate>
                                        <asp:Label ID="txt_employee" runat="server" class="form-control form-control-sm" Text='<%#Eval("EmpName") %>'></asp:Label>



                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_designation" runat="server" class="form-control form-control-sm" Text='<%#Eval("Designation") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Department">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_dptName" runat="server" class="form-control form-control-sm" Text='<%#Eval("DepartmentName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Grade">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_grdName" runat="server" class="form-control form-control-sm" Text='<%#Eval("GradeName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>




                            </Columns>
                        </asp:GridView>  <br/>
                        <asp:Button runat="server" ID="btnAddEmpList" OnClick="btnAddEmpList_OnClick" Text="Add To List " CssClass="btn btn-sm btn-info" />
                      
                        <br />


                       

                        <asp:Button runat="server" ID="btnEmpSubmit" OnClick="btnEmpSubmit_OnClick" Text="Submit " CssClass="btn btn-sm btn-info" />
                        <asp:Button ID="btnNo" Text="Cancel" OnClick="btnNo_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
</asp:Content>


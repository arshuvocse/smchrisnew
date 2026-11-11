<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="TrainingRecordApproveView.aspx.cs" Inherits="Training_TrainingRecord" %>

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
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;">Training Records Approve</h1>
                        </div>
                        <%--  <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="detailsViewButton" Text="Training Records" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                        </div>--%>

                        <div class="page-heading__container float-right d-none d-sm-block">
                            <%--<asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                            <asp:Button ID="ListViewButton" Visible="True" Text="Go Back" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />--%>
                        </div>

                    </div>
                    <div class="card">
                        <div class="card-body">
                            <div class="form-row">
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Company</label>
                                        <span style="color: red">&nbsp;*</span>
                                        <asp:DropDownList runat="server" ID="ddlCompany" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" CssClass="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                        <span style="color: red">&nbsp;*</span>
                                        <asp:DropDownList ID="ddlFinancialYear" OnSelectedIndexChanged="ddlFinancialYear_OnSelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <fieldset class="for-panel" runat="server">
                                <legend>Training Type</legend>
                                <div class="form-row">

                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Training Type</label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:DropDownList ID="ddlTrainingType" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Budget Head</label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:DropDownList ID="ddlBudgetHead" runat="server" class="form-control form-control-sm"></asp:DropDownList>
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

                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Training Organization</label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:DropDownList ID="ddlTrainingOrg" AutoPostBack="True" OnSelectedIndexChanged="ddlTrainingOrg_OnSelectedIndexChanged" runat="server" class="form-control form-control-sm"></asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Location</label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:DropDownList ID="ddlLocation" runat="server" class="form-control form-control-sm"></asp:DropDownList>
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
                                            <label>Select Vanue</label>
                                            <asp:DropDownList runat="server" ID="ddlVenue" CssClass="form-control form-control-sm" />
                                        </div>
                                    </div>



                                </div>
                                <hr />
                                <div class="form-row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Total Participant</label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:TextBox ID="txtTotalParticipant" runat="server" CssClass="form-control form-control-sm" Enabled="False"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Training Cost</label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:TextBox ID="txtTrainingCost" AutoPostBack="True" OnTextChanged="txtTrainingCost_OnTextChanged" TextMode="Number" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Logistic Cost</label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:TextBox ID="txtLogistic" AutoPostBack="True" OnTextChanged="txtTrainingCost_OnTextChanged" TextMode="Number" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Other Cost</label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:TextBox ID="txtOtherCost" AutoPostBack="True" OnTextChanged="txtTrainingCost_OnTextChanged" TextMode="Number" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Grand Total</label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:TextBox ID="txtGrandTotal" TextMode="Number" runat="server" CssClass="form-control form-control-sm" Enabled="False"></asp:TextBox>
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
                                    <div class="col-md-1.5">
                                        <div class="form-group">
                                            <label>Start Time</label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:TextBox ID="txtStartTime" OnTextChanged="txtStartTime_OnTextChanged" TextMode="Time" onpaste="return txtStartTime_OnTextChanged" onkeypress="return txtStartTime_OnTextChanged" runat="server" AutoPostBack="True" class="form-control form-control-sm"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-md-1.5">
                                        <div class="form-group">
                                            <label>End Time</label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:TextBox ID="txtEndTime" OnTextChanged="txtEndTime_OnTextChanged" TextMode="Time" onpaste="return txtEndTime_OnTextChanged" onkeypress="return txtEndTime_OnTextChanged" runat="server" AutoPostBack="True" class="form-control form-control-sm"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Select Days</label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:CheckBoxList ID="chkDays" OnSelectedIndexChanged="chkDays_OnSelectedIndexChanged" AutoPostBack="True" RepeatDirection="Horizontal" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Total Days</label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:TextBox ID="txtTotalDays" Enabled="False" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Total Training Hours</label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:TextBox ID="txtTotalTrainingHoures" Enabled="False" CssClass="form-control form-control-sm" runat="server" />
                                        </div>
                                    </div>

                                </div>
                            </fieldset>

                            <fieldset class="for-panel" runat="server">
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
                                        <asp:GridView ShowFooter="true" ID="gvTrainner" Width="100%" CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="false" runat="server">
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
                                                        <asp:LinkButton ID="lb_RemoveTrainer" OnClick="lb_RemoveTrainer_OnClick" runat="server">Remove</asp:LinkButton>
                                                    </ItemTemplate>


                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>

                                <div class="form-row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Button runat="server" ID="btnAddEmployee" OnClick="btnAddEmployee_OnClick" Text="Add Participants " CssClass="btn btn-sm btn-shadow-info" />
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <div class="form-group">
                                        <label>Approval Status </label>
                                        &nbsp;<label style="color: #a52a2a">*</label>
                                        <asp:RadioButtonList ID="jobreqRadioButtonList" runat="server" RepeatDirection="Horizontal">
                                                    
                                        </asp:RadioButtonList>
                                    </div>




                            <asp:HiddenField runat="server" ID="hdpk" />
                            
                             <asp:Button ID="submitButton" Text="Save" CssClass="btn btn-sm btn-info" Visible="True" runat="server" OnClick="btn_Save_OnClick" />
                        <%--<asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="editButton_OnClick" />
                        <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="delButton_OnClick" />--%>

                            <%--<asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" CssClass="btn btn-sm btn-info" Text="Submit"></asp:Button>--%>

                            <%--<asp:Button ID="cancelButton" Text="Cancel" OnClick="cancelButton_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />--%>
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
        <asp:Panel ID="pnl_1" runat="server" Style="display: none;" Height="500px" Width="90%" CssClass="modalPopup">
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
                                <asp:TemplateField HeaderText="Operation">
                                    <ItemTemplate>

                                        <asp:CheckBox ID="txt_check" runat="server"></asp:CheckBox>
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
                        </asp:GridView>
                        <asp:Button runat="server" ID="btnAddEmpList" OnClick="btnAddEmpList_OnClick" Text="Add To List " CssClass="btn btn-sm btn-info" />
                        <asp:GridView ID="gv_selectedEmp" Width="100%" CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="false" runat="server">
                            <Columns>
                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
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
                                <asp:TemplateField HeaderText="Operation">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnselectedEmpRemove" runat="server" OnClick="btnselectedEmpRemove_OnClick" Text="Remove"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>




                            </Columns>
                        </asp:GridView>
                        <br />


                        

                        <asp:Button runat="server" ID="btnEmpSubmit" OnClick="btnEmpSubmit_OnClick" Text="Submit " CssClass="btn btn-sm btn-info" />
                        <%--<asp:Button ID="btnNo" Text="Cancel" OnClick="btnNo_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />--%>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
</asp:Content>


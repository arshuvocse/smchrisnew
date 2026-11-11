<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="EmpExperience.aspx.cs" Inherits="UserSetup_EmpExperience" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="container-fluid">
        <div class="page-heading">
            <div class="page-heading__container">
                <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" />Update Information</h1>
            </div>
            <div class="page-heading__container">
                <asp:Label runat="server" ID="lbl_Mode"></asp:Label>
                <span>
                  <label   class="btn infoN" style="font-size: 14px;">Employee ID:  &nbsp; <asp:Label runat="server" class="label w3-tag w3-green" style="font-size: 14px;" ID="empMasterCode"></asp:Label></label>
                    
                  <label   class="btn infoN" style="font-size: 14px;">Employee Name: &nbsp;  <asp:Label runat="server" class="label w3-tag w3-green" style="font-size: 14px;" ID="lblEmpName"></asp:Label></label>
                    
                   <label class="btn infoN" style="font-size: 11px;">Designation: &nbsp; 
                      <asp:Label runat="server" class="label w3-tag w3-green" Style="font-size: 9px;" ID="lblDesignation"></asp:Label></label>
                </span>
            </div>
            <div class="page-heading__container float-right d-none d-sm-block">
                
                 <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                        <asp:LinkButton ID="LinkButton1"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
                <%--<asp:Button ID="detailsViewButton" Text="Update Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                <asp:Button ID="btnEditInfo" Text="Back to List" Visible="False"  CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="btnEditInfo_OnClick" />--%>
            </div>
        </div>
        <div class="card">
            <div class="card-body">
                <Style>
                       .chkChoice label {
                padding-left: 6px;
               
            }
                </Style>
                
                
                <style>
                            .NavbarAcc {
                                color: white!important;
    background-color: #5078B3!important;
    font-family: Arial, Sans-Serif!important;
    font-size: 14px!important;
    font-weight: bold!important;
    padding: 10px!important;
    margin-top: 5px!important;
    cursor: pointer!important;
                            }
                        </style>
                        <nav class="navbar navbar-light bg-light NavbarAcc">
 <span>6. Experience</span>
</nav>
                 

                
                                <br />

                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="form-row">
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Company/Institute Name</label>
                                                    <asp:TextBox runat="server" ID="txt_ExpCompany" class="form-control form-control-sm" />
                                                </div>
                                            </div>

                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Contact person</label>
                                                    <asp:TextBox runat="server" ID="txt_ExpContactPerson" class="form-control form-control-sm" />
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Address</label>
                                                    <asp:TextBox runat="server" ID="txt_ExpAddress" class="form-control form-control-sm" />
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Nature of Business</label>
                                                    <asp:TextBox runat="server" ID="txt_ExpNatureofBusiness" class="form-control form-control-sm" />
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Job Type</label>
                                                    <asp:TextBox runat="server" ID="txt_ExpJobType" class="form-control form-control-sm" />
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Leaving Salary</label>
                                                    <asp:TextBox runat="server" ID="txt_ExpLeavingSalary" class="form-control form-control-sm" />
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                                                        Enabled="True" TargetControlID="txt_ExpLeavingSalary" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>From Date</label>
                                                    <asp:TextBox runat="server" ID="txt_ExpFromDate" class="form-control form-control-sm" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender8" runat="server"
                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                        TargetControlID="txt_ExpFromDate" />

                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>To Date</label>
                                                    <asp:TextBox runat="server" ID="txt_ExpToDate" class="form-control form-control-sm" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender9" runat="server"
                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                        TargetControlID="txt_ExpToDate" />
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group" style="padding-top: 14px;">

                                                    <asp:CheckBox runat="server" ID="chk_ExpLastJob" CssClass="chkChoice" Text="Last Job" />
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Designation</label>
                                                    <asp:TextBox runat="server" ID="txt_ExpDesignation" class="form-control form-control-sm" />
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Job Description</label>
                                                    <asp:TextBox runat="server" ID="txt_ExpJobDescription" class="form-control form-control-sm" />
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Tel No.</label>
                                                    <asp:TextBox runat="server" ID="txt_ExpTelNo" class="form-control form-control-sm" />
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server"
                                                        Enabled="True" TargetControlID="txt_ExpTelNo" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Remarks</label>
                                                    <asp:TextBox runat="server" ID="txt_ExpRemarks" class="form-control form-control-sm" />
                                                </div>
                                            </div>


                                            <div class="col-3" style="margin-top: 19px;">
                                                <div class="form-group">
                                                    <asp:Button runat="server" ID="btnAddExperience" Text="Add Experience"
                                                        CssClass="btn btn-outline-success btn-sm" OnClick="btnAddExperience_OnClick" />
                                                </div>
                                            </div>

                                        </div>





                                        <div>

                                            <div style="overflow: scroll;">
                                                <asp:GridView Width="100%" ID="gv_Experience" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL#">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                                <asp:HiddenField ID="EmpExperienceId" runat="server" Value='<%#Eval("EmpExperienceId") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Company/Institute">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ExpCompany" runat="server" Text='<%#Eval("ExpCompany") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Contact Person">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ExpContactPerson" runat="server" Text='<%#Eval("ExpContactPerson") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Address">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ExpAddress" runat="server" Text='<%#Eval("ExpAddress") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Nature of Business">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ExpNatureofBusiness" runat="server" Text='<%#Eval("ExpNatureofBusiness") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Job Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ExpJobType" runat="server" Text='<%#Eval("ExpJobType") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Leaving Salary">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ExpLeavingSalary" runat="server" Text='<%#Eval("ExpLeavingSalary") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="From Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ExpFromDate" runat="server" Text='<%#Eval("ExpFromDate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="To Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ExpToDate" runat="server" Text='<%#Eval("ExpToDate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Is Last Job">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ExpLastJob" runat="server" Text='<%#Eval("ExpLastJob") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Designation">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ExpDesignation" runat="server" Text='<%#Eval("ExpDesignation") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Job Description">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ExpJobDescription" runat="server" Text='<%#Eval("ExpJobDescription") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tel No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ExpTelNo" runat="server" Text='<%#Eval("ExpTelNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ExpRemarks" runat="server" Text='<%#Eval("ExpRemarks") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Edit">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lb_EditExperience" runat="server" OnClick="lb_EditExperience_OnClick"><img src="../Assets/img/rsz_edit.png" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Remove">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lb_RemoveExperience" runat="server" OnClick="lb_RemoveExperience_OnClick"><img src="../Assets/img/delete.png" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>



                                        <br />
                                        <div class="form-row">
                                            <div class="col-md-10">
                                                <asp:HiddenField runat="server" ID="hdpk" />


                                                <link href="ButtonGrups.css" rel="stylesheet" />
                                                <div class="ui-group-buttons">

                                                    <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" Text="  Save  " CssClass="btn btn-sm btn-info" OnClientClick="return confirm('Are you sure you want to Save ?')" />
                                                    <div class="or or-sm"></div>
                                                    <asp:Button runat="server" ID="btnNext" OnClientClick="return confirm('Are you sure you want to Save & Next ?')"  OnClick="btn_Next_OnClick" Text="   Save & Next   " CssClass="btn btn-sm btn-success" />

                                                    <div class="or or-sm"></div>
                                                    <asp:Button ID="cancelButton"  OnClientClick="return confirm('Are you sure you want to Exit ?')"  Text="  Exit  " OnClick="cancelButton_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:LinkButton CssClass="hh previous" OnClick="lbPrevious_OnClick" ID="lbPrevious" runat="server">&laquo; Previous</asp:LinkButton>
                                                <asp:LinkButton CssClass="hh next" runat="server" ID="lblNext" OnClick="lblNext_OnClick">Next &raquo;</asp:LinkButton>

                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                           
            </div>
        </div>
    </div>
</asp:Content>


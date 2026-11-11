<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="EmpTraining.aspx.cs" Inherits="UserSetup_EmpTraining" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
     <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
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
                    <label class="btn infoN" style="font-size: 13px;">Employee ID:  &nbsp;
                        <asp:Label runat="server" class="label w3-tag w3-green" Style="font-size: 13px;" ID="empMasterCode"></asp:Label></label>
                
                    
                  <label class="btn infoN" style="font-size: 13px;">Employee Name: &nbsp; 
                      <asp:Label runat="server" class="label w3-tag w3-green" Style="font-size: 13px;" ID="lblEmpName"></asp:Label></label>
                    
                     <label class="btn infoN" style="font-size: 11px;">Designation: &nbsp; 
                      <asp:Label runat="server" class="label w3-tag w3-green" Style="font-size: 9px;" ID="lblDesignation"></asp:Label></label>
                </span>
            </div>
            <div class="page-heading__container float-right d-none d-sm-block">
                
                 <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                        <asp:LinkButton ID="LinkButton1"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
             <%--   <asp:Button ID="detailsViewButton" Text="Update Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                
                <asp:Button ID="btnEditInfo" Text="Back to List" Visible="False"  CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="btnEditInfo_OnClick" />--%>
            </div>
        </div>
        <div class="card">
            <div class="card-body">
                
                
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
 <span>7. Training</span>
</nav>
                 
 
                                <br />

                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="form-row">
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Training Name</label>
                                                    <asp:TextBox runat="server" class="form-control form-control-sm" ID="txt_TrTrainingName" />
                                                </div>
                                            </div>

                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Training Type</label>
                                                    <asp:DropDownList runat="server" class="form-control form-control-sm clsSelect" ID="ddlTrTrainingType">
                                                    </asp:DropDownList>
                                                    
                                                          <script type="text/javascript">
                                                              function pageLoad() {
                                                                  $('.clsSelect').chosen({ disable_search_threshold: 5, search_contains: true });

                                                                





                                                              }
</script>
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Training Description</label>
                                                    <asp:TextBox runat="server" class="form-control form-control-sm" ID="txt_TrTrainingDescription" />
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Training Institution</label>
                                                    <asp:DropDownList runat="server" class="form-control form-control-sm clsSelect" ID="ddlTrTrainingInstitution">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Training Place</label>
                                                    <asp:TextBox runat="server" class="form-control form-control-sm" ID="txt_TrTrainingPlace" />
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Training Country</label>
                                                    <asp:DropDownList runat="server" class="form-control form-control-sm clsSelect" ID="ddlTrTrainingCountry">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Training Achievment</label>
                                                    <asp:TextBox runat="server" class="form-control form-control-sm" ID="txt_TrTrainingAchievment" />
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>From Date</label>
                                                    <asp:TextBox runat="server" class="form-control form-control-sm" ID="txt_TrFromDate" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender12" runat="server"
                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                        TargetControlID="txt_TrFromDate" />
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>To Date</label>
                                                    <asp:TextBox runat="server" class="form-control form-control-sm" ID="txt_TrToDate" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender13" runat="server"
                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                        TargetControlID="txt_TrToDate" />
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Training Days</label>
                                                    <asp:TextBox runat="server" class="form-control form-control-sm" ID="txt_TrTrainingDays" />
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server"
                                                        Enabled="True" TargetControlID="txt_TrTrainingDays" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Remarks</label>
                                                    <asp:TextBox runat="server" class="form-control form-control-sm" ID="txt_TrRemarks" />
                                                </div>
                                            </div>
                                            <div class="col-3"
                                                style="margin-top: 19px;">
                                                <div class="form-group">
                                                    <asp:Button runat="server" ID="btnAddTraining" Text="Add Training"
                                                        CssClass="btn btn-outline-success btn-sm" OnClick="btnAddTraining_OnClick" />
                                                </div>
                                            </div>

                                        </div>

                                        <div>

                                            <div style="overflow: scroll;">
                                                <asp:GridView Width="100%" ID="gv_Training" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL#">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                                <asp:HiddenField ID="EmpTrainingId" runat="server" Value='<%#Eval("EmpTrainingId") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Training Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_TrainingName" runat="server" Text='<%#Eval("TrainingName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Training Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_TrainingType" runat="server" Text='<%#Eval("TrainingTypeName") %>'></asp:Label>
                                                                <asp:HiddenField runat="server" ID="hfTrainingType" Value='<%#Eval("TrainingType") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Training Description">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_TrainingDescription" runat="server" Text='<%#Eval("TrainingDescription")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Training Institution">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_TrainingInstitution" runat="server" Text='<%#Eval("TrainingInstitutionName")%>'></asp:Label>
                                                                <asp:HiddenField runat="server" ID="hfTrainingInstitution" Value='<%#Eval("TrainingInstitution")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Training Place">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_TrainingPlace" runat="server" Text='<%#Eval("TrainingPlace")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Training Country">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_TrainingCountry" runat="server" Text='<%#Eval("TrainingCountryName")%>'></asp:Label>
                                                                <asp:HiddenField runat="server" ID="hfTrainingCountry" Value='<%#Eval("TrainingCountry")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Training Achievment">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_TrainingAchievment" runat="server" Text='<%#Eval("TrainingAchievment")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="From Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_TrFromDate" runat="server" Text='<%#Eval("TrFromDate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="To Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_TrToDate" runat="server" Text='<%#Eval("TrToDate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Training Days">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_TrainingDays" runat="server" Text='<%#Eval("TrainingDays")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_TrRemarks" runat="server" Text='<%#Eval("TrRemarks")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Edit">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lb_EditTraining" runat="server" OnClick="lb_EditTraining_OnClick"><img src="../Assets/img/rsz_edit.png" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Remove">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lb_RemoveTraining" runat="server" OnClick="lb_RemoveTraining_OnClick"><img src="../Assets/img/delete.png" /></asp:LinkButton>
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

                                                    <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" OnClientClick="return confirm('Are you sure you want to Save ?')" Text="  Save  " CssClass="btn btn-sm btn-info" />
                                                    <div class="or or-sm"></div>
                                                    <asp:Button runat="server" ID="btnNext" OnClick="btn_Next_OnClick" OnClientClick="return confirm('Are you sure you want to Save & Next ?')"  Text="   Save & Next   " CssClass="btn btn-sm btn-success" />

                                                    <div class="or or-sm"></div>
                                                    <asp:Button ID="cancelButton" Text="  Exit  "  OnClientClick="return confirm('Are you sure you want to Exit ?')"  OnClick="cancelButton_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                                                </div>
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
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


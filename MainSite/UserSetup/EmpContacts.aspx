<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="EmpContacts.aspx.cs" Inherits="UserSetup_EmpContacts" %>

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
               <%-- <asp:Button ID="detailsViewButton" Text="Update Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                   <asp:Button ID="btnEditInfo" Text="Back to List" Visible="False"  CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="btnEditInfo_OnClick" />--%>
                
                
                   <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                        <asp:LinkButton ID="LinkButton1"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
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
 <span>3. Contacts</span>
</nav>
                 
                                <br />

                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="form-row">
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Present Address</label>
                                                    <asp:TextBox runat="server" ID="txt_EmpPresentAddress" class="form-control form-control-sm" />
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Present Division</label>
                                                    <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlEmpPresentDivision" class="form-control form-control-sm" OnSelectedIndexChanged="ddlEmpPresentDivision_OnSelectedIndexChanged">
                                                        
                        
                                                    </asp:DropDownList>
                                                    
                                                                                           <script type="text/javascript">
                                                                                               function pageLoad() {
                                                                                                   $('#cpFormBody_ddlEmpPresentDivision').chosen({ disable_search_threshold: 5, search_contains: true });

                                                                                                   $('#cpFormBody_ddlEmpPresentDist').chosen({ disable_search_threshold: 5, search_contains: true });
                                                                                                   $('#cpFormBody_ddlEmpPresentThana').chosen({ disable_search_threshold: 5, search_contains: true });

                                                                                                   $('#cpFormBody_ddlEmpParmanentDivision').chosen({ disable_search_threshold: 5, search_contains: true });
                                                                                                   $('#cpFormBody_ddlEmpParmanentThana').chosen({ disable_search_threshold: 5, search_contains: true });




                                                                                               }
</script>
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Present District</label>
                                                    <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlEmpPresentDist" class="form-control form-control-sm" OnSelectedIndexChanged="ddlEmpPresentDist_OnSelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Present Thana</label>
                                                    <asp:DropDownList runat="server" ID="ddlEmpPresentThana" class="form-control form-control-sm">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Present Tel. No</label>
                                                    <asp:TextBox runat="server" ID="txt_EmpPresentTelNo" class="form-control form-control-sm" />
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Parmanent Address</label>
                                                    <asp:TextBox runat="server" ID="txt_EmpParmanentAddress" class="form-control form-control-sm" />
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Parmanent Division</label>
                                                    <asp:DropDownList AutoPostBack="True" runat="server" ID="ddlEmpParmanentDivision" class="form-control form-control-sm" OnSelectedIndexChanged="ddlEmpParmanentDivision_OnSelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Parmanent District</label>
                                                    <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlEmpParmanentDistrict" class="form-control form-control-sm" OnSelectedIndexChanged="ddlEmpParmanentDistrict_OnSelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Parmanent Thana</label>
                                                    <asp:DropDownList runat="server" ID="ddlEmpParmanentThana" class="form-control form-control-sm">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Parmanent Tel. No</label>
                                                    <asp:TextBox runat="server" ID="txt_EmpParmanentTelNo" class="form-control form-control-sm" />
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                        Enabled="True" TargetControlID="txt_EmpParmanentTelNo" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Personal Email</label>
                                                    <asp:TextBox runat="server" ID="txt_EmpPersonalEmail" class="form-control form-control-sm" />
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Official Email</label>
                                                    <asp:TextBox runat="server" ID="txt_EmpOfficialEmail" class="form-control form-control-sm" />
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Personal Mobile</label>
                                                    <asp:TextBox runat="server" ID="txt_EmpPersonalMobile" class="form-control form-control-sm" />
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                        Enabled="True" TargetControlID="txt_EmpPersonalMobile" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Official Mobile</label>
                                                    <asp:TextBox runat="server" ID="txt_EmpOfficialMobile" class="form-control form-control-sm" />

                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                        Enabled="True" TargetControlID="txt_EmpOfficialMobile" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                </div>
                                            </div>

                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Fax</label>
                                                    <asp:TextBox runat="server" ID="txt_EmpFax" class="form-control form-control-sm" />
                                                </div>
                                            </div>

                                        </div>

                                        <div class="form-row" style="margin-top: 50px;">
                                        </div>
                                        <div class="form-row">
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Emergency Contact Person</label>
                                                    <asp:TextBox runat="server" ID="txt_EmpEmergencyPerson" class="form-control form-control-sm" />
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Emergency Contact Address</label>
                                                    <asp:TextBox runat="server" ID="txt_EmpEmergencyAddress" class="form-control form-control-sm" />
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Emergency Number</label>
                                                    <asp:TextBox runat="server" ID="txt_EmpEmergencyNumber" class="form-control form-control-sm" />
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                        Enabled="True" TargetControlID="txt_EmpEmergencyNumber" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                </div>
                                            </div>


                                        </div>


                                        <br />
                                        <div class="form-row">
                                            <div class="col-md-10">
                                                <asp:HiddenField runat="server" ID="hdpk" />


                                                <link href="ButtonGrups.css" rel="stylesheet" />
                                                <div class="ui-group-buttons">

                                                    <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" Text="  Save  " OnClientClick="return confirm('Are you sure you want to Save ?')"
                                                        CssClass="btn btn-sm btn-info" />
                                                    <div class="or or-sm"></div>
                                                    <asp:Button runat="server" ID="btnNext" OnClick="btn_Next_OnClick" Text="   Save & Next   " OnClientClick="return confirm('Are you sure you want to Save & Next ?')" CssClass="btn btn-sm btn-success" />

                                                    <div class="or or-sm"></div>
                                                    <asp:Button ID="cancelButton" Text="  Exit  " OnClick="cancelButton_OnClick" OnClientClick="return confirm('Are you sure you want to Exit ?')" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
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


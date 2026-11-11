<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="EmpEmploymentInformation.aspx.cs" Inherits="UserSetup_EmpEmploymentInformation" %>

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
                    <label class="btn infoN" style="font-size: 14px;">Employee ID:  &nbsp;
                        <asp:Label runat="server" class="label w3-tag w3-green" Style="font-size: 14px;" ID="empMasterCode"></asp:Label></label>
                  
                    
                  <label class="btn infoN" style="font-size: 14px;">Employee Name: &nbsp; 
                      <asp:Label runat="server" class="label w3-tag w3-green" Style="font-size: 14px;" ID="lblEmpName"></asp:Label></label>
                    
                     <label class="btn infoN" style="font-size: 12px;">Designation: &nbsp; 
                      <asp:Label runat="server" class="label w3-tag w3-green" Style="font-size: 10px;" ID="lblDesignation"></asp:Label></label>
                </span>
            </div>
            <div class="page-heading__container float-right d-none d-sm-block">
             <%--   <asp:Button ID="detailsViewButton" Text="Update Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                 <asp:Button ID="btnEditInfo" Text="Back to List" Visible="False"  CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="btnEditInfo_OnClick" />
                --%>
                
                 <asp:LinkButton ID="LinkButton2" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                        <asp:LinkButton ID="LinkButton3"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
                <%-- <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                        <asp:LinkButton ID="LinkButton1"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>--%>
            </div>
        </div>
       
                <div class="card">
                    <div class="card-body">

                       <%-- <ajaxToolkit:Accordion ID="MyAccordion" runat="server" SelectedIndex="0" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent" FadeTransitions="false" FramesPerSecond="40" TransitionDuration="250" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                            <Panes>


                                <ajaxToolkit:AccordionPane ID="AccordionPane2" runat="server">
                                    <Header>
                                        <span class="accordionLink">2. Employment Information</span>
                                    </Header>
                                    <Content>--%>
                        
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
 <span>2. Employment Information</span>
</nav>

                                         <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
            <ContentTemplate>
                                                <br />
                                                <div class="form-row">
                                                    <div class="col-3">
                                                        <div class="form-group">
                                                            <label>Company</label>
                                                            <label style="color: #a52a2a">*</label>
                                                            <asp:DropDownList runat="server" Enabled="False" AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" CssClass="form-control form-control-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="col-3">
                                                        <div class="form-group">
                                                            <label>Division</label>
                                                            <label style="color: #a52a2a">*</label>
                                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDivision" class="form-control form-control-sm" OnSelectedIndexChanged="ddlDivision_OnSelectedIndexChanged" />
                                                            
                                                              <script type="text/javascript">
                                                                  function pageLoad() {
                                                                      $('#cpFormBody_ddlDivision').chosen({ disable_search_threshold: 5, search_contains: true });

                                                                      $('#cpFormBody_ddlDepartment').chosen({ disable_search_threshold: 5, search_contains: true });
                                                                      $('#cpFormBody_ddlWing').chosen({ disable_search_threshold: 5, search_contains: true });
                                                                   
                                                                      $('#cpFormBody_ddlDesignation').chosen({ disable_search_threshold: 5, search_contains: true });
                                                                      $('#cpFormBody_ddlSalaryLocation').chosen({ disable_search_threshold: 5, search_contains: true });
                                                                      $('#cpFormBody_ddlSection').chosen({ disable_search_threshold: 5, search_contains: true });
                                                                      $('#cpFormBody_ddlSubSection').chosen({ disable_search_threshold: 5, search_contains: true });
                                                                      $('#cpFormBody_ddlSalaryGrade').chosen({ disable_search_threshold: 5, search_contains: true });
                                                                      $('#cpFormBody_ddlSalaryStep').chosen({ disable_search_threshold: 5, search_contains: true });
                                                                      $('#cpFormBody_ddlJobLocation').chosen({ disable_search_threshold: 5, search_contains: true });



                                                                  }
</script>

                                                        </div>
                                                    </div>
                                                    <div class="col-3" runat="server" id="wing">
                                                        <div class="form-group">
                                                            <label>Wing</label>
                                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlWing" class="form-control form-control-sm" OnSelectedIndexChanged="ddlWing_OnSelectedIndexChanged" />

                                                        </div>
                                                    </div>
                                                    <div class="col-3" runat="server" id="dept">
                                                        <div class="form-group">
                                                            <label>Department</label>
                                                            
                                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDepartment" class="form-control form-control-sm" OnSelectedIndexChanged="ddlDepartment_OnSelectedIndexChanged" />

                                                        </div>
                                                    </div>
                                                    <div class="col-3" runat="server" id="sec">
                                                        <div class="form-group">
                                                            <label>Section</label>
                                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlSection" class="form-control form-control-sm" OnSelectedIndexChanged="ddlSection_OnSelectedIndexChanged" />

                                                        </div>
                                                    </div>
                                                    <div class="col-3" runat="server" id="subsec">
                                                        <div class="form-group">
                                                            <label>Sub Section</label>
                                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlSubSection" class="form-control form-control-sm" OnSelectedIndexChanged="ddlSubSection_OnSelectedIndexChanged" />

                                                        </div>
                                                    </div>
                                                    <div class="col-3">
                                                        <div class="form-group">
                                                            <label>Employee Category</label>
                                                            <label style="color: #a52a2a">*</label>
                                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlEmpCategory" class="form-control form-control-sm" OnSelectedIndexChanged="ddlEmpCategory_OnSelectedIndexChanged" />

                                                        </div>
                                                    </div>
                                                    <div class="col-3">
                                                        <div class="form-group">
                                                            <label>Salary Grade</label><label style="color: #a52a2a">*</label>
                                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlSalaryGrade" class="form-control form-control-sm" OnSelectedIndexChanged="ddlSalaryGrade_OnSelectedIndexChanged" />

                                                        </div>
                                                    </div>
                                                    <div class="col-3">
                                                        <div class="form-group">
                                                            <label>Salary Step</label><label style="color: #a52a2a">*</label>
                                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlSalaryStep" class="form-control form-control-sm" />

                                                        </div>
                                                    </div>
                                                    <div class="col-3">
                                                        <div class="form-group">
                                                            <label>Designation</label><label style="color: #a52a2a">*</label>
                                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDesignation" class="form-control form-control-sm" />

                                                        </div>
                                                    </div>
                                                    <div class="col-3">
                                                        <div class="form-group">
                                                            <label>Designation Type</label>
                                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDesignationType" class="form-control form-control-sm" />

                                                        </div>
                                                    </div>
                                                    <div class="col-3">
                                                        <div class="form-group">
                                                            <label>Office</label><label style="color: #a52a2a">*</label>
                                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlSalaryLocation" OnSelectedIndexChanged="ddlSalaryLocation_OnSelectedIndexChanged" class="form-control form-control-sm" />

                                                        </div>
                                                    </div>
                                                    <div class="col-3">
                                                        <div class="form-group">
                                                            <label>Place</label>
                                                            <asp:DropDownList runat="server"  ID="ddlJobLocation" class="form-control form-control-sm" />

                                                        </div>
                                                    </div>
                                                    
                                                    
                                                     <div class="col-3">
                                                        <div class="form-group">
                                                            <label>Floor</label>
                                                            <asp:TextBox runat="server"   ID="txtFloor" class="form-control form-control-sm" />

                                                        </div>
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
                                                            <asp:Button runat="server" OnClientClick="return confirm('Are you sure you want to Save & Next ?')"  ID="btnNext" OnClick="btn_Next_OnClick" Text="   Save & Next   " CssClass="btn btn-sm btn-success" />

                                                            <div class="or or-sm"></div>
                                                            <asp:Button ID="cancelButton" Text="  Exit  " OnClick="cancelButton_OnClick" CssClass="btn btn-sm warning"  OnClientClick="return confirm('Are you sure you want to Exit ?')"  runat="server" BackColor="#FFCC00" />
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

                                   <%-- </Content>
                                </ajaxToolkit:AccordionPane>
                            </Panes>
                        </ajaxToolkit:Accordion>--%>
                    </div>
                </div>

          

    </div>
</asp:Content>


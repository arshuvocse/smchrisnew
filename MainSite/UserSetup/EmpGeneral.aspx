<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" EnableEventValidation="false"  AutoEventWireup="true" CodeFile="EmpGeneral.aspx.cs" Inherits="UserSetup_EmpGeneral" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
     <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    


    <div class="container-fluid">
        <div class="page-heading">
            <div class="page-heading__container">
                <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" /> Employee Information</h1>
            </div>
            <div class="page-heading__container">
                <asp:Label runat="server" ID="lbl_Mode"></asp:Label>
                <span>

                    <label class="btn infoN" style="font-size: 13px;">Employee ID:  &nbsp;
                        <asp:Label runat="server" class="label w3-tag w3-green" Style="font-size: 12px;" ID="empMasterCode"></asp:Label></label>
                    
                    
                  <label class="btn infoN" style="font-size: 13px;">Employee Name: &nbsp; 
                      <asp:Label runat="server" class="label w3-tag w3-green" Style="font-size: 12px;" ID="lblEmpName"></asp:Label></label>
                    
                    
                      <label class="btn infoN" style="font-size: 12px;">Designation: &nbsp; 
                      <asp:Label runat="server" class="label w3-tag w3-green" Style="font-size: 10px;" ID="lblDesignation"></asp:Label></label>

                </span>
            </div>
            <div class="page-heading__container float-right d-none d-sm-block">
              
                   <asp:LinkButton ID="LinkButton2" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                        <asp:LinkButton ID="LinkButton3"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
              <%--  <asp:Button ID="detailsViewButton" Text="Update Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                   <asp:Button ID="btnEditInfo" Text="Back to List" Visible="False"  CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="btnEditInfo_OnClick" />--%>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                 <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaiting">
                                        <asp:Image ID="imgWait1" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>

                                  <style>
                    .imgshadow {
                 -webkit-box-shadow: 0px 10px 13px -7px #000000, 5px 2px 3px 5px rgba(0,0,0,0); 
box-shadow: 0px 10px 13px -7px #000000, 5px 2px 3px 5px rgba(0,0,0,0);  }
                     
                                                                                                 
                </style>

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
 <span>1. General Information</span>
</nav>
                        
                        <br/>
                                        
                        
                                        <div class="form-row">

                                            <div class="col-2">
                                                <div class="form-group" style="margin-top: 14px">
                                                    <asp:CheckBox runat="server" ID="chkIsPreviousEmp" CssClass="chkChoiceSigle" AutoPostBack="True" OnCheckedChanged="chkIsPreviousEmp_OnCheckedChanged" Text="Is Previous Employee"/>
                                                </div>
                                            </div>
                                            
                                                <div class="col-4">
                                                        <div class="form-group">
                                                            <label>Employee</label>
                                                            <label style="color: #a52a2a">*</label>
                                                            <asp:DropDownList Enabled="False" runat="server"  ID="ddlEmpPrevious" CssClass="form-control form-control-sm clsSelect" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpPrevious_OnSelectedIndexChanged"   />
                                                            
                                                                                      <script type="text/javascript">
                                                                                          function pageLoad() {
                                                                                              $('.clsSelect').chosen({ disable_search_threshold: 5, search_contains: true });







                                                                                          }
</script>
                                                             </div>
                                                   </div>
                                            
                                              <div class="col-2">
                                                <div class="form-group" style="margin-top: 14px">
                                                    <asp:CheckBox runat="server" ID="chkSpecialTransfer"  CssClass="chkChoiceSigle" Text="Is Special Transfer"/>
                                                </div>
                                            </div>
                                            
                                            
                                              
                                              <div class="col-2">
                                                    <label>Recruitment Type</label>
                                                <div class="form-group" >
                                                     <asp:RadioButtonList ID="rbRecruitmentType" RepeatDirection="Horizontal"  runat="server" >
                                                    <asp:ListItem> New </asp:ListItem>
                                                    <asp:ListItem> Replacement</asp:ListItem>
                                                </asp:RadioButtonList>
                                                </div>
                                            </div>

                                            </div>

                                        <div class="form-row">

                                            <div class="col-2">
                                                <div class="form-group">
                                                    <label>Company</label>
                                                    <label style="color: #a52a2a">*</label>
                                                    <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" CssClass="form-control form-control-sm" />
                                                </div>
                                            </div>
                                              <div class="col-2">
                                                        <div class="form-group">
                                                            <label>Division</label>
                                                            <label style="color: #a52a2a">*</label>
                                                            <asp:DropDownList runat="server"  ID="ddlDivision" class="form-control form-control-sm clsSelect"   />
                                                            
                                                                                       
                                                             </div>
                                                   </div>
                                            <div class="col-2">
                                                <div class="form-group">
                                                    <label>Employee Name</label>
                                                    <label style="color: #a52a2a">*</label>
                                                    <asp:TextBox runat="server" ID="txt_EmpName" class="form-control form-control-sm" />
                                                </div>
                                            </div>
                                            <div class="col-2">
                                                <div class="form-group">
                                                    <label>Gender</label>
                                                    <asp:DropDownList runat="server" ID="ddlGender" class="form-control form-control-sm">
                                                        <asp:ListItem Text="Select..." Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                                                        <asp:ListItem Text="FeMale" Value="FeMale"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-2">
                                                <div class="form-group">
                                                    <label>Blood Group</label>
                                                    <asp:DropDownList runat="server" ID="ddlBloodGroup" class="form-control form-control-sm">
                                                        <asp:ListItem Text="Unknown" Value="Unknown"></asp:ListItem>
                                                        <asp:ListItem Text="A+" Value="A+"></asp:ListItem>
                                                        <asp:ListItem Text="A-" Value="A-"></asp:ListItem>
                                                        <asp:ListItem Text="B+" Value="B+"></asp:ListItem>
                                                        <asp:ListItem Text="B-" Value="B-"></asp:ListItem>
                                                        <asp:ListItem Text="O+" Value="O+"></asp:ListItem>
                                                        <asp:ListItem Text="O-" Value="O-"></asp:ListItem>
                                                        <asp:ListItem Text="AB+" Value="AB+"></asp:ListItem>
                                                        <asp:ListItem Text="AB-" Value="AB-"></asp:ListItem>

                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-2">
                                                <div class="form-group">
                                                    <label>Tin No.</label>
                                                    <asp:TextBox runat="server" ID="txt_EmpTinNo" class="form-control form-control-sm" />
                                                </div>
                                            </div>
                                            <div class="col-2">
                                                <div class="form-group">
                                                    <label>Father's Name</label>
                                                    <asp:TextBox runat="server" ID="txt_EmpFatherName" class="form-control form-control-sm" />
                                                </div>
                                            </div>
                                            
                                               <div class="col-2">
                                                <div class="form-group">
                                                    <label>Father's DOB</label>
                                                    
                                                    <asp:TextBox runat="server" ID="txtFatherDOB" class="form-control form-control-sm" autocomplete="off" placeholder="example: 01-Jan-2019" AutoPostBack="True" OnTextChanged="txtFatherDOB_OnTextChanged" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                        TargetControlID="txtFatherDOB" />
                                                </div>
                                            </div>
                                            <div class="col-2">
                                                <div class="form-group">
                                                    <label>Father's Occupation</label>
                                                    <asp:DropDownList runat="server" ID="ddlEmpFOccupation" class="form-control form-control-sm clsSelect">
                                                    </asp:DropDownList>
                                                    
                                       
                                                </div>
                                            </div>
                                            <div class="col-2">
                                                <div class="form-group">
                                                    <label>Mother's Name</label>
                                                    <asp:TextBox runat="server" ID="txt_EmpMotherName" class="form-control form-control-sm" />
                                                </div>
                                            </div>
                                            
                                                <div class="col-2">
                                                <div class="form-group">
                                                    <label>Mother's DOB</label>
                                                   
                                                    <asp:TextBox runat="server" ID="txtMotherDOB" class="form-control form-control-sm" autocomplete="off" placeholder="example: 01-Jan-2019" AutoPostBack="True" OnTextChanged="txtMotherDOB_OnTextChanged" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server"
                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                        TargetControlID="txtMotherDOB" />
                                                </div>
                                            </div>
                                            <div class="col-2">
                                                <div class="form-group">
                                                    <label>Mother's Occupation</label>
                                                    <asp:DropDownList runat="server" ID="ddlEmpMOccupation" class="form-control form-control-sm clsSelect">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-2">
                                                <div class="form-group">
                                                    <label>Date of Birth</label>
                                                    <label style="color: #a52a2a">*</label>
                                                    <asp:TextBox runat="server" ID="txt_EmpDOB" class="form-control form-control-sm" autocomplete="off" placeholder="example: 01-Jan-2019" AutoPostBack="True" OnTextChanged="txt_EmpDOB_OnTextChanged" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                        TargetControlID="txt_EmpDOB" />
                                                </div>
                                            </div>
                                            <div class="col-2">
                                                <div class="form-group">
                                                    <label>Date of Join</label>
                                                    <label style="color: #a52a2a">*</label>
                                                    <asp:TextBox runat="server" ID="txt_EmpDOJ" class="form-control form-control-sm" autocomplete="off" AutoPostBack="True" placeholder="example: 01-Jan-2019" OnTextChanged="txt_EmpDOJ_OnTextChanged" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                        TargetControlID="txt_EmpDOJ" />
                                                </div>
                                            </div>
                                            <div class="col-2">
                                                <div class="form-group">
                                                    <label>Religion</label>
                                                    <asp:DropDownList runat="server" ID="ddlEmpReligion" class="form-control form-control-sm">
                                                        <asp:ListItem Text="Select..." Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="Islam" Value="Islam"></asp:ListItem>
                                                        <asp:ListItem Text="Hindu" Value="Hindu"></asp:ListItem>
                                                        <asp:ListItem Text="Christian" Value="Christian"></asp:ListItem>
                                                          <asp:ListItem Text="Buddhist" Value="Buddhist"></asp:ListItem>
                                                                            <asp:ListItem Text="Others" Value="Others"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-2">
                                                <div class="form-group">
                                                    <label>Marital Status</label>
                                                    <asp:DropDownList runat="server" ID="ddlEmpMaritalStatus" class="form-control form-control-sm">
                                                        <asp:ListItem Text="Select..." Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="Single" Value="Single"></asp:ListItem>
                                                        <asp:ListItem Text="Married" Value="Married"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-2">
                                                <div class="form-group">
                                                    <label>Employee Type</label>
                                                    <label style="color: #a52a2a">*</label>
                                                    <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlEmpType" class="form-control form-control-sm" OnSelectedIndexChanged="ddlEmpType_OnSelectedIndexChanged" />

                                                </div>
                                            </div>
                                            <style>
                                                  .chkChoiceSigle label {
            padding-left: 8px;
            padding-right: 7px;
        }
                                            </style>

                                            <div class="col-2"  >
                                                <div class="form-group">
                                                     <asp:CheckBox runat="server" onclick="MutExChkList(this);" ID="chkIsCompanyDirector"  AutoPostBack="True" OnCheckedChanged="chkIsCompanyDirector_OnCheckedChanged"   Text="  Company Director  " CssClass="chkChoiceSigle"   />
                                                    <br/>
                                                    <asp:CheckBox runat="server" onclick="MutExChkList(this);"  ID="chkIsProgramContractual" AutoPostBack="True" OnCheckedChanged="chkIsProgramContractual_OnCheckedChanged" Text="Doner Project" CssClass="chkChoiceSigle" Enabled="false" /><br/>
                                                     <asp:CheckBox runat="server" onclick="MutExChkList(this);" ID="FundedProjectsCheckBox1"  AutoPostBack="True" OnCheckedChanged="FundedProjectsCheckBox1_OnCheckedChanged"   Text="SMC Funded Projects" CssClass="chkChoiceSigle" Enabled="false" />
                                                    <br/>
                                                     <asp:CheckBox  AutoPostBack="True" OnCheckedChanged="chkSmcContract_OnCheckedChanged"  runat="server" onclick="MutExChkList(this);" ID="chkSmcContract"   Text="  SMC Contract  " CssClass="chkChoiceSigle" Enabled="false" />
                                                    
                                                     
                                                    
                                                </div>
                                            </div>
                                            
                                             <div class="col-md-2">

                                            <div class="form-group">
                                                <label>Select a Project </label>
                                                <%--<span style="color:red">&nbsp;*</span>--%>
                                                <asp:DropDownList ID="ProjectDropDownList" Enabled="False" CssClass="form-control form-control-sm  clsSelect"  runat="server"></asp:DropDownList>
                                                <asp:HiddenField runat="server" ID="HFproMasId"/>
                                                <asp:HiddenField runat="server" ID="HFproDtlId"/>
                                            </div>

                                        </div>
                                            
                                                                        <div class="col-2">
                                                <div class="form-group">
                                                    <label>Contract End Date</label>
                                                    <asp:TextBox runat="server" ID="txt_ContractEndDate" CssClass="form-control form-control-sm"  AutoPostBack="True" OnTextChanged="txt_ContractEndDate_OnTextChanged" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender154" runat="server"
                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                        TargetControlID="txt_ContractEndDate" />
                                                </div>
                                            </div>
                                            
                                            
                                               <div class="col-2">
                                                <div class="form-group">
                                                    <label>Contract Preiod (Month)</label>
                                                    <asp:TextBox runat="server" ID="txtContractualPreiod" ReadOnly="True" CssClass="form-control form-control-sm" />
                                                  
                                                </div>
                                            </div>

                                            <div class="col-2">
                                                <div class="form-group">
                                                    <label>Place Of Birth</label>
                                                    <asp:TextBox runat="server" ID="txt_EmpPlaceOfBirth" class="form-control form-control-sm" />
                                                </div>
                                            </div>
                                            <div class="col-2">
                                                <div class="form-group">
                                                    <label>Nationality</label>
                                                    <asp:DropDownList runat="server" ID="ddlEmpNationality" class="form-control form-control-sm clsSelect">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-2">
                                                <div class="form-group">
                                                    <label>National ID</label>
                                                    <asp:TextBox runat="server" ID="txt_EmpNationalID" class="form-control form-control-sm" />
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextB56oxExxxtenderunitsalePrice" runat="server"
                                                        Enabled="True" TargetControlID="txt_EmpNationalID" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                            <div id="iddivContract" cssclass="row" runat="server" visible="False">
                                                <div cssclass="col-2">
                                                    <div cssclass="form-group">
                                                    </div>
                                                </div>
                                            </div>









                                            <div class="col-2">
                                                <div class="form-group">
                                                    <label>Passport No.</label>
                                                  
                                                    <asp:TextBox runat="server" ID="txt_EmpPassport" class="form-control form-control-sm" />
                                                </div>
                                            </div>
                                            <div class="col-2">
                                                <div class="form-group">
                                                    <label>Expected Service length(Y)</label>
                                                    <asp:TextBox runat="server" ID="txt_EmpExpectedServiceLength" class="form-control form-control-sm" AutoPostBack="True" OnTextChanged="txt_EmpExpectedServiceLength_OnTextChanged" />
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderunitValue" runat="server"
                                                        Enabled="True" TargetControlID="txt_EmpExpectedServiceLength" FilterType="Custom" ValidChars="0123456789"></cc1:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                            <div class="col-2">
                                                <div class="form-group">
                                                    <label>Date of Retirement</label>
                                                    <asp:TextBox runat="server" ID="txt_EmpDateOfRetirement" class="form-control form-control-sm" />
                                                    <%--    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                                                                    Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                    TargetControlID="txt_EmpDateOfRetirement" />--%>
                                                </div>
                                            </div>
                                            <div class="col-2">
                                                <div class="form-group">
                                                    <label>Date of Confirmation</label>
                                                    <asp:TextBox runat="server" ID="txt_EmpDateOfConformation" class="form-control form-control-sm" autocomplete="off" AutoPostBack="True" OnTextChanged="txt_EmpDateOfConformation_OnTextChanged" placeholder="example: 01-Jan-2019" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server"
                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                        TargetControlID="txt_EmpDateOfConformation" />
                                                </div>
                                            </div>
                                            <div class="col-2">
                                                <div class="form-group">
                                                    <label>Confirmation Status</label>
                                                    <label style="color: #a52a2a">*</label>
                                                    <asp:DropDownList runat="server" ID="ddlConformationStatus" CssClass="form-control form-control-sm">
                                                        <asp:ListItem Text="Select..." Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="Yes" Value="true"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="false"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-2">
                                                <div class="form-group">
                                                    <label>Reporting Boss</label>
                                                      <asp:DropDownList runat="server" ID="ddlReportingBoss" class="form-control form-control-sm clsSelect" AutoPostBack="True" OnSelectedIndexChanged="ddlReportingBoss_OnSelectedIndexChanged">
                                                    </asp:DropDownList>
                                              <%--      <asp:TextBox runat="server" AutoPostBack="True" Visible="False" ID="txt_ReportingBoss" class="form-control form-control-sm" OnTextChanged="txt_ReportingBoss_OnTextChanged" />

                                                    <ajaxToolkit:AutoCompleteExtender
                                                        ID="at_txt_ReportingBoss"
                                                        TargetControlID="txt_ReportingBoss" DelimiterCharacters=""
                                                        EnableCaching="true" Enabled="True" CompletionSetCount="10"
                                                        runat="server"
                                                        ServiceMethod="GetEmpNameDesigIDAuto_AllCompany"
                                                        ServicePath="~/WebService.asmx"
                                                        MinimumPrefixLength="1"
                                                        CompletionInterval="500"
                                                        CompletionListCssClass="autocomplete_completionListElement"
                                                        FirstRowSelected="false">
                                                    </ajaxToolkit:AutoCompleteExtender>
                                                    <asp:HiddenField runat="server" ID="hdReportingBoss" />--%>
                                                </div>
                                            </div>
                            
                               <div class="col-2" style="margin-top: 24px;">
                                                <div class="form-group">
                                                    <label>Not Supervisor ?</label>
                                                    <asp:CheckBox  AutoPostBack="true" ID="chkIsAllEmployee"  runat="server"
                                                    OnCheckedChanged="chkIsAllEmployee_CheckedChanged" />
                                       
                                                    </div>
                                                    </div>
                            
                            <div class="col-2">
                                                <div class="form-group">
                                                    <label>Final Approver</label>  
                                                    <asp:HiddenField runat="server" ID="hfSuperMenuAppId"/>
                                                      <asp:DropDownList runat="server" ID="ddlFinalApprover" class="form-control form-control-sm clsSelect">
                                                    </asp:DropDownList>
                                              <%--      <asp:TextBox runat="server" AutoPostBack="True" Visible="False" ID="txt_ReportingBoss" class="form-control form-control-sm" OnTextChanged="txt_ReportingBoss_OnTextChanged" />

                                                    <ajaxToolkit:AutoCompleteExtender
                                                        ID="at_txt_ReportingBoss"
                                                        TargetControlID="txt_ReportingBoss" DelimiterCharacters=""
                                                        EnableCaching="true" Enabled="True" CompletionSetCount="10"
                                                        runat="server"
                                                        ServiceMethod="GetEmpNameDesigIDAuto_AllCompany"
                                                        ServicePath="~/WebService.asmx"
                                                        MinimumPrefixLength="1"
                                                        CompletionInterval="500"
                                                        CompletionListCssClass="autocomplete_completionListElement"
                                                        FirstRowSelected="false">
                                                    </ajaxToolkit:AutoCompleteExtender>
                                                    <asp:HiddenField runat="server" ID="hdReportingBoss" />--%>
                                                </div>
                                            </div>
                            
                             <div class="col-2">
                                                <div class="form-group">
                                                    <label>Leave Approver</label>  
                                                    
                                                      <asp:DropDownList runat="server" ID="ddlLeaveApproval" class="form-control form-control-sm clsSelect">
                                                    </asp:DropDownList>
                                </div>
                                            </div>

                             <div class="col-2">
                                                <div class="form-group">
                                                    <label>Leave Recommender</label>  
                                                    
                                                      <asp:DropDownList runat="server" ID="ddlleaveRecommender" class="form-control form-control-sm clsSelect">
                                                    </asp:DropDownList>
                                </div>
                                            </div>
                            
                         

                                            <div class="col-2" runat="server" Visible="False">
                                                <div class="form-group">
                                                    <label>Reporting Boss's Designation</label>
                                                    <asp:TextBox runat="server" ID="txt_ReportingBossDesig" class="form-control form-control-sm" ReadOnly="True" />
                                                </div>
                                            </div>
                                            <div class="col-2" style="margin-top: 24px;">
                                                <div class="form-group">
                                                   
                                                    <asp:CheckBox runat="server" Text=" Is Probationary" ID="chkIsProbationary" AutoPostBack="True" CssClass="chkChoiceSigle" OnCheckedChanged="chkIsProbationary_OnCheckedChanged" />
                                                </div>
                                            </div>
                                            <div class="col-2">
                                                <div class="form-group">
                                                    <label>Probationary End Date</label>
                                                    <asp:TextBox runat="server" ID="txt_ProbationaryEndDate" class="form-control form-control-sm" AutoPostBack="True" OnTextChanged="txt_ProbationaryEndDate_OnTextChanged"   />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender114" runat="server"
                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                        TargetControlID="txt_ProbationaryEndDate" />
                                                </div>
                                            </div>
                                            
                                            
                                             <div class="col-2">
                                                        <div class="form-group">
                                                            <label class="control-label">Circulation From Date</label>
                                                            <asp:TextBox runat="server" ID="startDate" class="form-control form-control-sm"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender14" runat="server"
                                                                Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                TargetControlID="startDate" />
                                                        </div>
                                                    </div>
                                                    <div class="col-2">
                                                        <div class="form-group">
                                                            <label class="control-label">Circulation to Date</label>
                                                            <asp:TextBox runat="server" ID="endDate" class="form-control form-control-sm"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender15" runat="server"
                                                                Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                TargetControlID="endDate" />
                                                        </div>
                                                    </div>

                                                    <div class="col-2">
                                                        <div class="form-group">
                                                            <label class="control-label">Job Circulation</label>
                                                            <asp:TextBox runat="server" ID="txt_JobCirculation" class="form-control form-control-sm" AutoPostBack="True" OnTextChanged="txt_JobCirculation_OnTextChanged"></asp:TextBox>
                                                            <asp:HiddenField runat="server" ID="hfJobID" />
                                                            <ajaxToolkit:AutoCompleteExtender
                                                                ID="at_txt_JobCirculation"
                                                                TargetControlID="txt_JobCirculation"
                                                                runat="server"
                                                                ServiceMethod="GetJobCirculationAutoPosition"
                                                                ServicePath="~/WebService.asmx"
                                                                MinimumPrefixLength="1"
                                                                CompletionInterval="1000"
                                                                EnableCaching="false"
                                                                CompletionSetCount="1"
                                                                FirstRowSelected="false">
                                                            </ajaxToolkit:AutoCompleteExtender>
                                                        </div>
                                                    </div>
                                            <%-- <div class="col-2">
                                                            <div class="form-group">
                                                                <label>Separation Date</label>
                                                                <asp:TextBox runat="server" ID="SeparationDateTextBox" class="form-control form-control-sm" ReadOnly="True" />
                                                               
                                                            </div>
                                                        </div>--%>

                                           
                                        </div>
                                        
                                      
                                        <br />
                                        
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
                                        
                                        
                                             <fieldset class="for-panel">
                                                            <legend>Directly Supervised Information</legend>
                                        <div class="form-row">
                                              <div class="col-3">
                                                  </div>
                                            <div class="col-4">
                                                
                                                  <div class="form-group">
                                         <label>Directly Supervised</label>
                                             <asp:DropDownList runat="server" ID="ddldirectlySuper" class="form-control form-control-sm clsSelect">
                                                    </asp:DropDownList>
                                            <%--    <asp:TextBox ID="directlySuperTextBox" AutoPostBack="True" runat="server" class="form-control form-control-sm" OnTextChanged="directlySuperTextBox_OnTextChanged"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" DelimiterCharacters=""
                                                    EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                    ServiceMethod="GetCompanyWiseEmployeeInfoOnlyForPromotion" ServicePath="~/WebService.asmx" TargetControlID="directlySuperTextBox"
                                                    UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                    ShowOnlyCurrentWordInCompletionListItem="true">
                                                </cc1:AutoCompleteExtender>

                                                <asp:HiddenField ID="directlyEmpIdHiddenField" runat="server" />--%>
                                                <asp:HiddenField ID="rptHiddenField" runat="server" />
                                         </div>
                                                <%--<asp:AutoCompleteExtender
                                                    ID="at_txt_JobCirculation"
                                                    TargetControlID="EmployeeNameTextBox"
                                                    runat="server"
                                                    ServiceMethod="GetParticipantList"
                                                    ServicePath="~/WebService.asmx"
                                                    MinimumPrefixLength="2"
                                                    CompletionInterval="10"
                                                    EnableCaching="false"
                                                    CompletionSetCount="1"
                                                    FirstRowSelected="false">
                                                </asp:AutoCompleteExtender>--%>
                                                <asp:HiddenField ID="HiddenField4" runat="server" />

                                                <%--<asp:DropDownList ID="NewReportingBodyDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"></asp:DropDownList>--%>
                                            </div>
                                            <div class="col-1" style="margin-top: 19px;">
                                                 <div class="form-group">
                                                       <asp:Button ID="Button1" Text="Add To List" CssClass="btn btn-outline-success btn-block disabled btn-sm" runat="server" OnClick="Button1_OnClick" />
                                                     </div>
                                            </div>
                                              </div>
                                            
                                        
                                                 <div class="form-row">
                                                      <div class="col-3">
                                                  </div>
                                            <div class="col-6">
                                                <asp:HiddenField ID="delEmpIdHiddenField" runat="server" />
                                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                                    CssClass="table table-bordered  text-center thead-dark" DataKeyNames="EmpInfoId">
                                                    <Columns>


                                                        <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />


                                                        <asp:TemplateField HeaderText="Delete">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="deleteImageButton" runat="server" OnClick="deleteImageButton_OnClick"
                                                                    ImageUrl="~/Assets/img/delete.png" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
   </fieldset>
                                        <br />
                                       
                                          <fieldset class="for-panel">
                                                            <legend>Image Upload</legend>
                                        <div class="form-row">
                                             <div class="col-4">
                                                <div class="form-group">
                                                    <label>Photo Upload</label>
                                                    <div>
                                                        <%--<asp:FileUpload ID="fu_Image" runat="server" />
                                                                    <asp:Button ID="btn_ImageUpload" runat="server" Text="Image Upload" OnClick="btn_ImageUpload_OnClick" />--%>
                                                        <%--<asp:Button ID="btnProcessData"
                                                                                runat="server" Text="Process Data"
                                                                                OnClick="btnProcessData_Click" />--%>
                                                        <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server"
                                                                        AssociatedUpdatePanelID="upFormBody">
                                                                        <ProgressTemplate>
                                                                            Please wait, your file is getting uploaded....
                                                                        </ProgressTemplate>
                                                                    </asp:UpdateProgress>
                                                                    <triggers>
                                                                        <asp:PostBackTrigger ControlID="btn_ImageUpload"  />--%>
                                                        <%--<asp:AsyncPostBackTrigger ControlID="btnProcessData" />--%>
                                                        <%--</triggers>

                                                                    <asp:Image ID="img" runat="server"
                                                                        Width="100" Height="100" ImageAlign="Middle" />--%>
                                                        <%--                                                                    <ajaxToolkit:AjaxFileUpload
                                                                        id="ajaxUpload1" 
                                                                        OnUploadComplete="ajaxUpload1_OnUploadComplete"
                                                                        
                                                                        runat="server"  />--%>


                                                        <%--                                                                    <br />
                                                                    <asp:Image ID="Image1" runat="server" Width="120" Height="90" BorderWidth="1" />--%>

                                                        <input type="file" name="EmpImage" accept="image/*" />
                                                        <input type="button" id="btnUpload" value="Photo Upload" />
                                                        <progress id="fileProgress" style="display: none"></progress>
                                                        <hr />
                                                        <asp:Image ID="img_emp" runat="server" CssClass="imgshadow" height="130" width="120" />
                                                        <asp:HiddenField runat="server" ID="hfempimg" />
                                                        <span id="lblMessage" style="color: Green"></span>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="col-5">
                                                <div class="form-group">
                                                    <label>Signature Upload (Dimension:243 * 44)</label>
                                                    <div>
                                                          <asp:FileUpload ID="FileUpload1" runat="server" runat="server" Visible="False" />
                                                        <input type="file" name="SignatureFile" accept="image/*"  />
                                                        <input type="button" id="btnSignatureUpload" value="Signature Upload" />
                                                        <progress id="SignaturefileProgress" style="display: none"></progress>
                                                        <hr />
                                                        <asp:Image ID="SignatureImage" runat="server" CssClass="imgshadow" height="130" width="120" />
                                                        <asp:HiddenField runat="server" ID="hfSignature" />
                                                        <span id="lblMessageSignature" style="color: Green"></span>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-4" runat="server" Visible="False">
                                                <div class="form-group">
                                                    <label>Nominee Image Upload</label>
                                                    <div>

                                                        <input type="file" name="NomineeImageUpload" accept="image/*" />
                                                        <input type="button" id="btnNomineeImageUpload" value="Nominee Image Upload" />
                                                        <progress id="NomineeImageProgress" style="display: none"></progress>
                                                      
                                                        <hr />
                                                        <asp:Image ID="img_NomineeImage" runat="server" CssClass="imgshadow" height="130" width="120" />
                                                        <asp:HiddenField runat="server" ID="hfNomineeImage" />
                                                        <span id="lblMessageNomineeImage" style="color: Green"></span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        </fieldset>
                                        <br/>
                                        <br/>
                                        
                                      
                                        <div class="form-row">
                                            <div class="col-md-10">
                                                <asp:HiddenField runat="server" ID="hdpk" />


                                                <link href="ButtonGrups.css" rel="stylesheet" />
                                                <div class="ui-group-buttons">

                                                    <asp:Button runat="server" ID="btn_Save" OnClientClick="return confirm('Are you sure you want to Save ?')" OnClick="btn_Save_OnClick" Text="  Save  " CssClass="btn btn-sm btn-info" />
                                                    <div class="or or-sm"></div>
                                                    <asp:Button runat="server" ID="btnNext" OnClientClick="return confirm('Are you sure you want to Save & Next ?')"  OnClick="btn_Next_OnClick" Text="   Save & Next   " CssClass="btn btn-sm btn-success" />

                                                    <div class="or or-sm"></div>
                                                    <asp:Button ID="cancelButton"  OnClientClick="return confirm('Are you sure you want to Exit ?')"  Text="  Exit  " OnClick="cancelButton_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                                                </div>
                                            </div>
                                            <div class="col-md-1">
                                            </div>
                                            <div class="col-md-1">

                                                <asp:LinkButton CssClass="hh next" Visible="False" runat="server" ID="lblNext" OnClick="lblNext_OnClick">Next &raquo;</asp:LinkButton>

                                            </div>
                                        </div>
                                         <br/>
                                        <br/> <br/>
                                        <br/>
                                        <br/>
                                        <br/>
                                 

                    </div>
                </div>



            </ContentTemplate>

        </asp:UpdatePanel>
    </div>

    <script type="text/javascript">


        $("body").on("click", "#btnUpload", function () {
            $.ajax({
                url: '/FileUploadHandler.ashx',
                type: 'POST',
                data: new FormData($('form')[0]),
                cache: false,
                contentType: false,
                processData: false,
                success: function (file) {
                    $("#fileProgress").hide();
                    $("#SignaturefileProgress").hide();
                    $("#NomineeImageProgress").hide();
                    //$("#lblMessage").html("<b>" + file.name + "</b> has been uploaded.");
                    $("#lblMessage").html("Image has been uploaded.");
                    //console.log(file);
                    $('#cpFormBody_img_emp').prop({ src: '/UploadImg/' + file.dbfilename });
                    $('#cpFormBody_hfempimg').val(file.dbfilename);
                 

                },
                xhr: function () {
                    var fileXhr = $.ajaxSettings.xhr();
                    if (fileXhr.upload) {
                        $("#SignaturefileProgress").hide();
                        $("#NomineeImageProgress").hide();
                        $("#fileProgress").show();
                        fileXhr.upload.addEventListener("progress", function (e) {
                            if (e.lengthComputable) {
                                $("#fileProgress").attr({
                                    value: e.loaded,
                                    max: e.total
                                });
                            }
                        }, false);
                    }
                    return fileXhr;
                }
            });
        });

        $("body").on("click", "#btnSignatureUpload", function () {
            $.ajax({
                url: '/SignatureUploadHandler.ashx',
                type: 'POST',
                data: new FormData($('form')[0]),
                cache: false,
                contentType: false,
                processData: false,
                success: function (file) {
                    $("#fileProgress").hide();
                    $("#SignaturefileProgress").hide();
                    $("#NomineeImageProgress").hide();

                    $("#lblMessageSignature").html("Signature has been uploaded.");
                    $('#cpFormBody_SignatureImage').prop({ src: '/UploadImg/' + file.dbfilename });
                    $('#cpFormBody_hfSignature').val(file.dbfilename);


                },
                xhr: function () {
                    var fileXhr = $.ajaxSettings.xhr();
                    if (fileXhr.upload) {
                        $("#fileProgress").hide();
                        $("#NomineeImageProgress").hide();
                        $("#SignaturefileProgress").show();
                        fileXhr.upload.addEventListener("SignaturefileProgress", function (e) {
                            if (e.lengthComputable) {
                                $("#SignaturefileProgress").attr({
                                    value: e.loaded,
                                    max: e.total
                                });
                            }
                        }, false);
                    }
                    return fileXhr;
                }
            });
        });

        $("body").on("click", "#btnNomineeImageUpload", function () {
            $.ajax({
                url: '/NomineeImageUploadHandler.ashx',
                type: 'POST',
                data: new FormData($('form')[0]),
                cache: false,
                contentType: false,
                processData: false,
                success: function (file) {
                    $("#fileProgress").hide();
                    $("#SignaturefileProgress").hide();
                    $("#NomineeImageProgress").hide();

                    $("#lblMessageNomineeImage").html("Nominee Image has been uploaded.");
                    $('#cpFormBody_img_NomineeImage').prop({ src: '/UploadImg/' + file.dbfilename });
                    $('#cpFormBody_hfNomineeImage').val(file.dbfilename);
                },
                xhr: function () {
                    var fileXhr = $.ajaxSettings.xhr();
                    if (fileXhr.upload) {
                        $("#fileProgress").hide();
                        $("#SignaturefileProgress").hide();

                        $("#NomineeImageProgress").show();
                        fileXhr.upload.addEventListener("NomineeImageProgress", function (e) {
                            if (e.lengthComputable) {
                                $("#NomineeImageProgress").attr({
                                    value: e.loaded,
                                    max: e.total
                                });
                            }
                        }, false);
                    }
                    return fileXhr;
                }
            });
        });
        </script>
        <script>
        function MutExChkList(chk) {
            var chkList = chk.parentNode.parentNode.parentNode;
            var chks = chkList.getElementsByTagName("input");
            for (var i = 0; i < chks.length; i++) {
                if (chks[i] != chk && chk.checked) {
                    chks[i].checked = false;
                }
            }
        }
    </script>
</asp:Content>


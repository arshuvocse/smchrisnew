<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="DashBoard.aspx.cs" Inherits="DashBoard_UI_DashBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">

    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Timer ID="dashboardLoadTimer" runat="server" Interval="700" OnTick="dashboardLoadTimer_Tick" />
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
                        <div class="icon">
                            <img src="../Report_Pages/app.png" width="20px" />
                        </div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Dashboard </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="homeButton" Visible="False" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" />
                        <asp:Button ID="detailsViewButton" Visible="False" Text="View List" CssClass="btn btn-sm btn-outline-secondary " runat="server" />
                    </div>

                </div>
                <!-- //END PAGE HEADING -->


                <link href="../UserProfile/UserProfileShadow.css" rel="stylesheet" />
                <style>
                    .imgshadow {
                        -webkit-box-shadow: 5px 5px 15px 5px #000000;
                        box-shadow: 5px 5px 15px 5px #000000;
                    }

                    .Label_Title {
                        background-color: gray;
                        text-align: left;
                        padding: 10px;
                        color: #000;
                        font-weight: bold;
                        font-size: 18px;
                        margin-left: -14px;
                        width: 100% !important;
                    }

                    .Label_Box {
                        background-color: #F5F5F5;
                        width: 100%;
                        padding: 12px;
                        color: #000;
                        font-weight: bold;
                        font-size: 13px;
                    }

                    .box {
                        background: #FFF;
                    }




                    /* mouse over link */
                    a:hover {
                        text-decoration: none;
                        font-style: italic;
                        -webkit-transition: all .3s ease-in-out;
                        -moz-transition: all .3s ease-in-out;
                        -o-transition: all .3s ease-in-out;
                        -ms-transition: all .3s ease-in-out;
                    }

                    /* se 
/*==================================================
 * Effect 1
 * ===============================================*/
                    .effect1 {
                        -webkit-box-shadow: 0 10px 6px -6px #777;
                        -moz-box-shadow: 0 10px 6px -6px #777;
                        box-shadow: 0 10px 6px -6px #777;
                    }

                    .dotRed {
                        height: 30px;
                        color: black !important;
                        display: inline-block;
                        padding-left: 12px;
                        padding-right: 12px;
                        padding-top: 9px;
                        text-align: center;
                        background-color: #C8022C;
                        border-radius: 50%;
                    }

                    .dotGreen {
                        height: 30px;
                        color: black !important;
                        display: inline-block;
                        padding-left: 12px;
                        padding-right: 12px;
                        padding-top: 9px;
                        text-align: center;
                        background-color: #9EECAE;
                        border-radius: 50%;
                    }
                </style>


                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">

                            <div class="row">

                                <div class="col-md-12">
                                    <div class="row">



                                        <div class="col-md-3">

                                            <div class="portlet light profile-sidebar-portlet bordered" style="box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);">
                                                <style>
                                                    .scenter {
                                                        display: block;
                                                        margin-left: auto;
                                                        margin-right: auto;
                                                        width: 50%;
                                                    }


                                                    .title-widget {
                                                        color: #898989;
                                                        font-size: 20px;
                                                        font-weight: 300;
                                                        line-height: 1;
                                                        position: relative;
                                                        text-transform: uppercase;
                                                        font-family: 'Fjalla One', sans-serif;
                                                        margin-top: 0;
                                                        margin-right: 0;
                                                        margin-bottom: 25px;
                                                        padding-left: 12px;
                                                    }

                                                        .title-widget::before {
                                                            background-color: #ea5644;
                                                            content: "";
                                                            height: 22px;
                                                            left: 0px;
                                                            position: absolute;
                                                            top: -2px;
                                                            width: 5px;
                                                        }
                                                </style>
                                                <div class="profile-userpic">
                                                    <asp:Image ID="UserImage" runat="server" CssClass="img-responsive scenter" alt="" />
                                                    <%--<img src="../Assets/man-icon.png" class="img-responsive scenter" alt="">--%>
                                                </div>
                                                <div class="profile-usertitle">

                                                    <div class="profile-usertitle-name">
                                                        <label class="font-weight-bold" style="color: black">ID: </label>
                                                        <asp:Label runat="server" ID="lblID" />
                                                    </div>
                                                    <div class="profile-usertitle-job">
                                                        <label class="font-weight-bold" style="color: black">Name:</label>
                                                        <asp:Label runat="server" ID="lblshortName" />
                                                    </div>
                                                    <div class="profile-usertitle-nameDES">
                                                        <label class="font-weight-bold" style="color: black">Designation:</label>
                                                        <asp:Label runat="server" ID="lblDesignation" />
                                                    </div>
                                                </div>
                                                <%--  <div class="profile-userbuttons">--%>
                                                <%--<asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>--%>
                                                <%--   <asp:Button ID="btnPrintCv" runat="server" OnClick="btnPrintCv_Click" CssClass="btn btn-info  btn-sm" Text="Print CV" />--%>
                                                <%--</ContentTemplate>
                             </asp:UpdatePanel>--%>
                                                <%--   </div>--%>
                                                <br />
                                                <%--   <div class="profile-usermenu">
                <ul class="nav">
                     <li class="active">
                        <a href="#">
                            <i class="icon-home"></i> Ticket List </a>
                    </li>
                    <li>
                        <a href="#">
                            <i class="icon-settings"></i> Support Staff </a>
                    </li>
                    <li>
                        <a href="#">
                            <i class="icon-info"></i> Configurations </a>
                    </li>
                </ul>
            </div>--%>
                                            </div>
                                        </div>



                                        <div class="col-md-3">
                                            <asp:GridView Visible="False" runat="server" AutoGenerateColumns="False" Width="100%" ID="gv_JdBoard" CssClass="table table-bordered text-center thead-dark gridDatatable" DataKeyNames="ActionStatus">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL#">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                            <asp:HiddenField ID="EmpInfoId" runat="server" Value='<%#Eval("EmpInfoId") %>' />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>





                                                </Columns>
                                            </asp:GridView>


                                            <asp:GridView Visible="False" ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                                CssClass="table table-bordered text-center thead-dark" DataKeyNames="EmployeeJobLeftId,EmployeeId,ExitDetailId,ApprovalStatus,ExitMasterId,empinfoidForMain,EmpInfoIdApproval">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                            <asp:HiddenField runat="server" ID="HFExitMasterId" Value='<%#Eval("ExitMasterId")%>' />
                                                            <asp:HiddenField runat="server" ID="hfDivisionId" Value='<%#Eval("DivisionId")%>' />

                                                            <asp:HiddenField runat="server" ID="hfApprovalStatusShow" Value='<%#Eval("ApprovalStatusShow")%>' />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />

                                                    <asp:BoundField DataField="EmpMasterCode" HeaderText="Empployee ID" />
                                                    <asp:BoundField DataField="EmpName" HeaderText="Empployee Name" />

                                                    <asp:BoundField DataField="JobLeftType" HeaderText="Job Left Type" />

                                                    <asp:BoundField DataField="JobLeftDate" HeaderText="Job Left Date" DataFormatString="{0:dd-MMM-yyyy}" />

                                                    <asp:BoundField DataField="Reason" HeaderText="Reason" />
                                                    <asp:BoundField DataField="ApprovalStatus" HeaderText="Approval Status" />
                                                    <asp:TemplateField HeaderText="Other Comments" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lblComments" runat="server" CssClass="btn btn-sm btnMyDesignScan"
                                                                CommandArgument="<%# Container.DataItemIndex %>" CommandName="appComm">Other Comments</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--<asp:BoundField DataField="EntryBy" HeaderText="Entry By" />
                                        <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="UpdateBy" HeaderText="UpdateBy" />
                                        <asp:BoundField DataField="UpdateDate" HeaderText="UpdateDate" DataFormatString="{0:dd-MMM-yyyy}" />--%>

                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="iinkButton" runat="server" CssClass="btn btn-info btn-sm"
                                                                CommandArgument="<%# Container.DataItemIndex %>" CommandName="Clearance">Go To Clearance &#8921;</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="deleteImageButton" runat="server"
                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="DeleteData"
                                                    ImageUrl="~/Assets/img/delete.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="viewImageButton" runat="server"
                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="ViewData"
                                                    ImageUrl="~/Assets/img/list-view.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                        <div class="col-md-3">
                                        </div>
                                        <div class="col-md-3 box effect1">

                                            <%--<link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css">--%>

                                            <div class="Label_Title">
                                                <img src="giphy.gif" width="42" height="32" />&nbsp;&nbsp; Approval Notification
                                            </div>
                                            
                                            <asp:HyperLink style="padding: 12px;"  Visible="False" ID="hpKPIPendingforSetup" runat="server" NavigateUrl="../Appraisal/AppraisalSelfList.aspx" Font-Bold="True" ToolTip="go to approval page" ForeColor="black">
                                                <div style="padding: 10px" class="Label_Box">

                                                    <span class="dotGreen">
                                                        <asp:Label Text="0" runat="server" ID="lblKPIPendingforSetup"></asp:Label>
                                                    </span>
                                                    <span>KPI Pending for Setup</span>


                                                </div>
                                            </asp:HyperLink>
                                           <asp:HyperLink style="padding: 12px;"  Visible="False" ID="hpBSCKPIPendingforSetup" runat="server" NavigateUrl="../Appraisal/BSCOKRAppraisalSelfList.aspx" Font-Bold="True" ToolTip="go to approval page" ForeColor="black">
                                                <div style="padding: 10px" class="Label_Box">

                                                    <span class="dotGreen">
                                                        <asp:Label Text="0" runat="server" ID="lblBSCKPIPendingforSetup"></asp:Label>
                                                    </span>
                                                    <span>BSC/OKR Pending for Setup</span>


                                                </div>
                                            </asp:HyperLink>
                                          


                                            <asp:HyperLink style="padding: 12px;"  runat="server" Visible="False" ID="hpKPIPendingforApproval" NavigateUrl="../Appraisal/AppraisalSupApprove.aspx" Font-Bold="True" ToolTip="go to approval page" ForeColor="black">
                                                <div style="padding: 10px" class="Label_Box">

                                                    <span class="dotGreen">
                                                        <asp:Label Text="0" runat="server" ID="lblKPIPending"></asp:Label>
                                                    </span>
                                                    <span>KPI Pending for Approval</span>


                                                </div>
                                            </asp:HyperLink>
                                             <asp:HyperLink style="padding: 12px;"  runat="server" Visible="False" ID="hpBSCOKRKPIPending" NavigateUrl="../Appraisal/BSCKPIApproval.aspx" Font-Bold="True" ToolTip="go to approval page" ForeColor="black">
                                                <div style="padding: 10px" class="Label_Box">

                                                    <span class="dotGreen">
                                                        <asp:Label Text="0" runat="server" ID="lblBSCOKRKPIPending"></asp:Label>
                                                    </span>
                                                    <span>BSC/OKR Pending for Approval</span>


                                                </div>
                                            </asp:HyperLink>


                                            

                                            <asp:HyperLink style="padding: 12px;"  runat="server" Visible="False" ID="hpAppOKRBSEPendingforSetup" NavigateUrl="../Appraisal/OKRBSCAppraisalDashboardSelf.aspx" Font-Bold="True" ToolTip="go to approval page" ForeColor="black">
                                                <div style="padding: 10px" class="Label_Box">

                                                    <span class="dotGreen">
                                                        <asp:Label Text="0" runat="server" ID="lblAppOKRBSEPendingforSetup"></asp:Label>
                                                    </span>
                                                    <span>OKR/BSC Appraisal Pending for Setup</span>


                                                </div>
                                            </asp:HyperLink>


                                              <asp:HyperLink style="padding: 12px;"  runat="server" Visible="False" ID="hpOKBSCAppraisalApproval" NavigateUrl="../Appraisal/OKRBSCApppraisalApprove.aspx" Font-Bold="True" ToolTip="go to approval page" ForeColor="black">
      <div style="padding: 10px" class="Label_Box">

          <span class="dotGreen">
              <asp:Label Text="0" runat="server" ID="lblOKBSCAppraisalApproval"></asp:Label>
          </span>
          <span>OKR/BSC  Appraisal Pending for Approval</span>


      </div>
  </asp:HyperLink>
                                              <asp:HyperLink style="padding: 12px;"  runat="server" Visible="False" ID="hpMOKBSCAppraisalApproval" NavigateUrl="../Appraisal/MOKRBSCApppraisalApprove.aspx" Font-Bold="True" ToolTip="go to approval page" ForeColor="black">
      <div style="padding: 10px" class="Label_Box">

          <span class="dotGreen">
              <asp:Label Text="0" runat="server" ID="lblMOKBSCAppraisalApproval"></asp:Label>
          </span>
          <span>Mid-Year OKR/BSC Pending for Approval</span>


      </div>
  </asp:HyperLink>
                                        
                                            
                                              <asp:HyperLink style="padding: 12px;"  runat="server" Visible="False" ID="hpMOKBSCAppraisalPersonal" NavigateUrl="../Appraisal/MOKRBSCAppraisalDashboardSelf.aspx" Font-Bold="True" ToolTip="go to approval page" ForeColor="black">
      <div style="padding: 10px" class="Label_Box">

          <span class="dotGreen">
              <asp:Label Text="0" runat="server" ID="lblMOKBSCAppraisalPersonal"></asp:Label>
          </span>
          <span>Mid-Year OKR/BSC Pending for Setup</span>


      </div>
  </asp:HyperLink>
                                             





                                                                                        <asp:HyperLink style="padding: 12px;"  runat="server" Visible="False" ID="hlKPIMidSupervisorMark" NavigateUrl="../Appraisal/KPIMIDApppraisalApprove.aspx" Font-Bold="True" ToolTip="go to approval page" ForeColor="black">
    <div style="padding: 10px" class="Label_Box">

        <span class="dotGreen">
            <asp:Label Text="0" runat="server" ID="lblKPIMidSupervisorMark"></asp:Label>
        </span>
        <span>KPI Mid-Year  Pending for Approval</span>


    </div>
</asp:HyperLink>
                                      
                                          
                                            <asp:HyperLink style="padding: 12px;"  runat="server" Visible="False" ID="hlKPIMidSelfMark" NavigateUrl="../Appraisal/KPIMidYearMarkSetSelf.aspx" Font-Bold="True" ToolTip="go to approval page" ForeColor="black">
    <div style="padding: 10px" class="Label_Box">

        <span class="dotGreen">
            <asp:Label Text="0" runat="server" ID="lblKPIMidSelfMark"></asp:Label>
        </span>
        <span>KPI Mid-Year Pending for Setup</span>


    </div>
</asp:HyperLink>
                                          
                                            <asp:HyperLink style="padding: 12px;"  runat="server" Visible="False" ID="hlOKRBSCMidSelfMark" NavigateUrl="../Appraisal/MOKRBSCAppraisalDashboardSelf.aspx" Font-Bold="True" ToolTip="go to approval page" ForeColor="black">
    <div style="padding: 10px" class="Label_Box">

        <span class="dotGreen">
            <asp:Label Text="0" runat="server" ID="lblOKRBSCMidSelfMark"></asp:Label>
        </span>
        <span>OKR/BSC Mid-Year Pending for Setup</span>


    </div>
</asp:HyperLink>


                                            <asp:HyperLink style="padding: 12px;"  runat="server" Visible="False" ID="hpAppPendingforSetup" NavigateUrl="../Appraisal/AppraisalDashboard.aspx" Font-Bold="True" ToolTip="go to approval page" ForeColor="black">
                                                <div style="padding: 10px" class="Label_Box">

                                                    <span class="dotGreen">
                                                        <asp:Label Text="0" runat="server" ID="lblAppPendingforSetup"></asp:Label>
                                                    </span>
                                                    <span>Appraisal Pending for Setup</span>


                                                </div>
                                            </asp:HyperLink>
                                         

                                            <asp:HyperLink style="padding: 12px;"  runat="server" Visible="False" ID="hpAppraisalPendingforApproval" NavigateUrl="../Appraisal/ApppraisalApprove.aspx" Font-Bold="True" ToolTip="go to approval page" ForeColor="black">
                                                <div style="padding: 10px" class="Label_Box">

                                                    <span class="dotGreen">
                                                        <asp:Label Text="0" runat="server" ID="lblAppPending"></asp:Label>
                                                    </span>
                                                    <span>Appraisal Pending for Approval</span>


                                                </div>
                                            </asp:HyperLink>
                                          


                                            <asp:HyperLink style="padding: 12px;"  runat="server" Visible="False" ID="hpManpowerBudgetPending" NavigateUrl="../MPBudget/MPBudgetListApproval.aspx" Font-Bold="True" ToolTip="go to approval page" ForeColor="black">
                                                <div style="padding: 10px" class="Label_Box">
                                                    <span class="dotGreen">
                                                        <asp:Label Text="0" runat="server" ID="lblManpowerBudgetPending"></asp:Label></span>
                                                    <span>Manpower Budget Pending</span>
                                            </asp:HyperLink>


                                            
                                            <asp:HyperLink style="padding: 12px;"  Visible="False" ID="hpRequisitionPen" runat="server" NavigateUrl="../RecruitmentManagement_UI/JobRequisitionFormAproval.aspx" Font-Bold="True" ForeColor="black" ToolTip="go to approval page">
                                                <div style="padding: 10px" class="Label_Box">
                                                    <span class="dotGreen">
                                                        <asp:Label Text="0" runat="server" ID="lblRequisitionPen"></asp:Label>
                                                    </span>
                                                    <span>Requisition Pending</span>


                                                </div>
                                            </asp:HyperLink>


                                          
                                            <asp:HyperLink style="padding: 12px;"  runat="server" Visible="False" ID="hpRecruitmentPending" NavigateUrl="../RecruitmentApp_UI/RecruimentApprovalView.aspx" Font-Bold="True" ForeColor="black" ToolTip="go to approval page">
                                                <div style="padding: 10px" class="Label_Box">
                                                    <span class="dotGreen">
                                                        <asp:Label Text="0" runat="server" ID="lblRecruitmentPending"></asp:Label>
                                                    </span>
                                                    <span>Recruitment Pending</span>


                                                </div>
                                            </asp:HyperLink>
                                           

                                            <asp:HyperLink style="padding: 12px;"  runat="server" Visible="False" ID="hpClearenceFormApproval" NavigateUrl="../ExitManagement_UI/ClearenceFormView.aspx" Font-Bold="True" ForeColor="black" ToolTip="go to approval page">
                                                <div style="padding: 10px" class="Label_Box">
                                                    <span class="dotGreen">
                                                        <asp:Label Text="0" runat="server" ID="lblClearenceFormApproval"></asp:Label></span>
                                                    <span>Clearence Form Pending</span>
                                                </div>
                                            </asp:HyperLink>

                                        
                                            <asp:HyperLink style="padding: 12px;"  runat="server" Visible="False" ID="hpMiscellaneousMeetingPending" NavigateUrl="../MeetingMinors/MiscellaneousInformationApprovalList.aspx" Font-Bold="True" ForeColor="black" ToolTip="go to approval page">
                                                <div style="padding: 10px" class="Label_Box">
                                                    <span class="dotGreen">
                                                        <asp:Label Text="0" runat="server" ID="lblMiscellaneousMeetingPending"></asp:Label></span>
                                                    <span>Document Approval Pending</span>
                                                </div>
                                            </asp:HyperLink>


                                        



                                            <asp:HyperLink style="padding: 12px;"  Visible="False" ID="hpEmployeeProbationPeriod" runat="server" NavigateUrl="../Survey/ProbationListApproval.aspx" Font-Bold="True" ToolTip="go to approval page" ForeColor="black">
                                                <div style="padding: 10px" class="Label_Box">

                                                    <span class="dotGreen">
                                                        <asp:Label Text="0" runat="server" ID="lblEmployeeProbationPeriod"></asp:Label>
                                                    </span>
                                                    <span>Employee Probation Period</span>


                                                </div>
                                            </asp:HyperLink>
                                       

                                            <asp:HyperLink style="padding: 12px;"  runat="server" Visible="False" ID="hpEmployeeStateChangeApproval" NavigateUrl="../ContractualEmployeeManagement_UI/ContractualEmpApprovalList.aspx" Font-Bold="True" ToolTip="go to approval page" ForeColor="black">
                                                <div style="padding: 10px" class="Label_Box">

                                                    <span class="dotGreen">
                                                        <asp:Label Text="0" runat="server" ID="lblEmployeeStateChangeApproval"></asp:Label>
                                                    </span>
                                                    <span>Employemnt Type Change Approval</span>


                                                </div>
                                            </asp:HyperLink>
 

                                            <asp:HyperLink style="padding: 12px;"  runat="server" Visible="False" ID="hpSkillWillSetup" NavigateUrl="../Skill_Will_Assessment/Skill_WillAssesmentList.aspx" Font-Bold="True" ToolTip="go to approval page" ForeColor="black">
                                                <div style="padding: 10px" class="Label_Box">

                                                    <span class="dotGreen">
                                                        <asp:Label Text="0" runat="server" ID="lblSkillWillSetup"></asp:Label>
                                                    </span>
                                                    <span>Skill Will Assessment Setup</span>


                                                </div>
                                            </asp:HyperLink>

                                          

                                            <asp:HyperLink style="padding: 12px;"  runat="server" Visible="False" ID="hpSkillWill" NavigateUrl="../Skill_Will_Assessment/SkillWill_AssesmentApprovalList.aspx" Font-Bold="True" ToolTip="go to approval page" ForeColor="black">
                                                <div style="padding: 10px" class="Label_Box">

                                                    <span class="dotGreen">
                                                        <asp:Label Text="0" runat="server" ID="lblSkillWill"></asp:Label>
                                                    </span>
                                                    <span>Skill Will Assessment Approval</span>


                                                </div>
                                            </asp:HyperLink>


                                          

                                            <asp:HyperLink  style="padding: 12px;"  runat="server" Visible="False" ID="hpExpenseReimbursementForm" NavigateUrl="../HealthCare_UI/ExpenseReimbursementFormApproval.aspx" Font-Bold="True" ToolTip="go to approval page" ForeColor="black">
                                                <div style="padding: 10px" class="Label_Box">

                                                    <span class="dotGreen">
                                                        <asp:Label Text="0" runat="server" ID="lblExpenseReimbursementForm"></asp:Label>
                                                    </span>
                                                    <span>Expense Reimbursement Form Approval</span>


                                                </div>
                                            </asp:HyperLink>
                                            
                                            
                                            
                                          

                                            <asp:HyperLink  style="padding: 12px;"  runat="server" Visible="False" ID="hpHCDoctorFeedback" NavigateUrl="../HealthCare_UI/CommitteeFeedbackPanel.aspx" Font-Bold="True" ToolTip="go to approval page" ForeColor="black">
                                                <div style="padding: 10px" class="Label_Box">

                                                    <span class="dotGreen">
                                                        <asp:Label Text="0" runat="server" ID="lblCommitteeFeedbackPanel"></asp:Label>
                                                    </span>
                                                    <span>Committee Feedback Panel</span>


                                                </div>
                                            </asp:HyperLink>

                                        </div>


                                    </div>
                                    <br />
                                </div>
                                <br />




                            </div>
                        </div>

                          <div class="row" runat="server" visible="False">
                                                                          <asp:HyperLink style="padding: 12px;"  runat="server" Visible="False" ID="hpOKBSCSetupPersonal" NavigateUrl="../Appraisal/BSCOKRAppraisalSelfList.aspx" Font-Bold="True" ToolTip="go to approval page" ForeColor="black">
    <div style="padding: 10px" class="Label_Box">

        <span class="dotGreen">
            <asp:Label Text="0" runat="server" ID="lblOKBSCSetupPersonal"></asp:Label>
        </span>
        <span>OKR/BSC Pending for Setup</span>


    </div>
</asp:HyperLink>

                                                   <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%"  ID="GridViewokrbscSelf" CssClass="table table-bordered text-center thead-dark gridDatatable" DataKeyNames="ActionStatus">
                         <Columns>
                             <asp:TemplateField HeaderText="SL#">
                                 <ItemTemplate>
                                     <%#Container.DataItemIndex+1 %>
                                     <asp:HiddenField ID="EmpInfoId" runat="server" Value='<%#Eval("EmpInfoId") %>' />
                                     <asp:HiddenField ID="FinancialYearId" runat="server" Value='<%#Eval("FinancialYearId") %>' />
                                     <asp:HiddenField ID="CurrentStatus" runat="server" Value='<%#Eval("CurrentStatus") %>' />
                                 </ItemTemplate>
                             </asp:TemplateField>
                             <asp:TemplateField HeaderText="Financial Year">
                                 <ItemTemplate>
                                     <asp:Label ID="FinancialYearDesc" runat="server" Text='<%#Eval("FinancialYearDesc") %>'></asp:Label>
                                 </ItemTemplate>
                             </asp:TemplateField>


                             <asp:TemplateField HeaderText="Deadline Date">
                                 <ItemTemplate>
                                     <asp:Label ID="lblExtensionDate" runat="server" Text='<%#Eval("ExtensionDate", "{0:dd- MMM -yyyy}") %>'></asp:Label>
                                 </ItemTemplate>
                             </asp:TemplateField>


                             <asp:TemplateField HeaderText="Employee Info">
                                 <ItemTemplate>
                                     <asp:Label ID="employee" runat="server" Text='<%#Eval("employee") %>'></asp:Label>
                                 </ItemTemplate>
                             </asp:TemplateField>
                             <asp:TemplateField HeaderText="Department">
                                 <ItemTemplate>
                                     <asp:Label ID="DepartmentName" runat="server" Text='<%#Eval("DepartmentName") %>'></asp:Label>
                                 </ItemTemplate>
                             </asp:TemplateField>

                             <asp:TemplateField HeaderText="Approval Status">
                                 <ItemTemplate>
                                     <asp:Label ID="Approval" runat="server" Text='<%#Eval("KPIActionStatus") %>'></asp:Label>
                                 </ItemTemplate>
                             </asp:TemplateField>


                             <asp:TemplateField HeaderText="Awaiting Employee">
                                 <ItemTemplate>
                                     <asp:Label ID="Awaiting" runat="server" Text='<%#Eval("PendingEmp") %>'></asp:Label>
                                 </ItemTemplate>
                             </asp:TemplateField>

                            

                              
                             
                         </Columns>
                     </asp:GridView>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label></label>
                                        <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True" RepeatDirection="Horizontal" Enabled="False">
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                        <div class="row">
                            <style>
                                .tblTHColorChang {
                                    background-color: #EDF2F5 !important;
                                    font-weight: bold;
                                    font-size: 13px;
                                }


                                .title-widget {
                                    color: #898989;
                                    font-size: 20px;
                                    font-weight: 300;
                                    line-height: 1;
                                    position: relative;
                                    text-transform: uppercase;
                                    font-family: 'Fjalla One', sans-serif;
                                    margin-top: 0;
                                    margin-right: 0;
                                    margin-bottom: 25px;
                                    padding-left: 12px;
                                }

                                    .title-widget::before {
                                        background-color: #ea5644;
                                        content: "";
                                        height: 22px;
                                        left: 0px;
                                        position: absolute;
                                        top: -2px;
                                        width: 5px;
                                    }
                            </style>

                            <asp:HiddenField runat="server" ID="ComPanyID" />
                            <div class="col-md-2" runat="server" visible="False">
                                <div class="form-group">
                                    <label>Financial Year</label>
                                    <asp:DropDownList ID="ddlFinancialYear" AutoPostBack="true" OnSelectedIndexChanged="ddlFinancialYear_OnSelectedIndexChanged" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                </div>
                                <asp:Label runat="server" ID="lblKPISelfStatus"></asp:Label>
                            </div>



                            <div class="col-md-10" runat="server" visible="False">
                                <h2 class="blue title-widget" style="color: #2196F3; text-shadow: 0 0 2px black;">KPI Information</h2>
                                <table class="table table-bordered table-striped">




                                    <tr>

                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px; text-align: center">Self
    
    
     <table class="table table-bordered table-striped">
         <tr>
             <td class="tblTHColorChang" style="width: 20%; padding: 10px;">KPI
                                                        <table style="height: 100px" class="table table-bordered table-striped">
                                                            <tr>
                                                                <td style="width: 20%; padding: 10px;"></td>
                                                            </tr>
                                                        </table>
             </td>
             <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Appraisal
                                                              
                                                              
                                                              
                                                                 <table style="height: 100px" class="table table-bordered table-striped">
                                                                     <tr>
                                                                         <td style="width: 20%; padding: 10px;">

                                                                             <asp:Label runat="server" ID="lblApprisalSelfStatus"></asp:Label>
                                                                         </td>
                                                                     </tr>
                                                                 </table>
             </td>

         </tr>
     </table>
                                        </td>








                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px; text-align: center">As Approver
                                                             
                                                                  <table class="table table-bordered table-striped">
                                                                      <tr>
                                                                          <td class="tblTHColorChang" style="width: 20%; padding: 10px;">KPI
                                                            
                                                            
                                                              <table class="table table-bordered table-striped" style="height: 100px">
                                                                  <tr>
                                                                      <td style="width: 20%; padding: 10px;">Done   
                                                                      </td>

                                                                      <td style="width: 20%; padding: 10px;">

                                                                          <asp:Label runat="server" ID="lblKpiDone"></asp:Label>
                                                                      </td>
                                                                  </tr>

                                                                  <tr>
                                                                      <td style="width: 20%; padding: 10px;">Pending   
                                                                      </td>

                                                                      <td style="width: 20%; padding: 10px;"></td>
                                                                  </tr>
                                                              </table>
                                                                          </td>
                                                                          <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Appraisal
                                                              
                                                              
                                                           <table class="table table-bordered table-striped" style="height: 100px">
                                                               <tr>
                                                                   <td style="width: 20%; padding: 10px;">Done   
                                                                   </td>

                                                                   <td style="width: 20%; padding: 10px;">

                                                                       <asp:Label runat="server" ID="lblAppDone"></asp:Label>
                                                                   </td>
                                                               </tr>

                                                               <tr>
                                                                   <td style="width: 20%; padding: 10px;">Pending  
                                                                   </td>

                                                                   <td style="width: 20%; padding: 10px;"></td>
                                                               </tr>
                                                           </table>
                                                                          </td>





                                                                      </tr>
                                                                  </table>

                                        </td>
                                    </tr>





                                </table>

                            </div>





                            <div class="col-md-10" runat="server" visible="False">
                                <h2 class="blue title-widget" style="color: #2196F3; text-shadow: 0 0 2px black;">Approval Notification </h2>
                                <table class="table table-bordered table-striped">




                                    <tr>










                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px; text-align: center">As Approver
                                                             
                                                                  <table class="table table-bordered table-striped">
                                                                      <tr>
                                                                          <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Manpower Budget
                                                            
                                                            
                                                              <table class="table table-bordered table-striped" style="height: 100px">


                                                                  <tr>
                                                                      <td style="width: 20%; padding: 10px;">Pending   
                                                                      </td>

                                                                      <td style="width: 20%; padding: 10px;"></td>
                                                                  </tr>
                                                              </table>
                                                                          </td>


                                                                          <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Requisition 
                                                            
                                                            
                                                              <table class="table table-bordered table-striped" style="height: 100px">


                                                                  <tr>
                                                                      <td style="width: 20%; padding: 10px;">Pending   
                                                                      </td>

                                                                      <td style="width: 20%; padding: 10px;"></td>
                                                                  </tr>
                                                              </table>
                                                                          </td>



                                                                      </tr>
                                                                  </table>

                                        </td>
                                    </tr>





                                </table>

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




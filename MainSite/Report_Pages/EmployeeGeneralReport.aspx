<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeGeneralReport.aspx.cs" Inherits="Report_Pages_EmployeeGeneralReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        fieldset.for-panel {
            background-color: #fcfcfc;
            border: 1px solid #999;
            padding: 15px 10px;
            background-color: white;
            margin-bottom: 12px;
        }

        .chkChoiceHeader label {
            padding-left: 10px;
            padding-right: 40px;
            font-size: 14px;
            font-weight: bold;
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

        #cpFormBody_BulletedList1 {
            font-size: 16px;
            margin-left: 2px;
        }

        #cpFormBody_BulletedList1 li {
            margin: 12px 0px;
        }

         #cpFormBody_BulletedList1 li a {
             color: #000000;
         }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">


    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                        <div class="icon">
                            <img src="../Report_Pages/app.png" width="20px" />
                        </div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Employee Information Report </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <%--<asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " Visible="False" runat="server" OnClick="HomeButton_OnClick" />

                        <asp:Button ID="detailsViewButton" Text="View List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />--%>
                    </div>

                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <fieldset class="for-panel">
                                        <legend>Report List </legend>
                                        <div class="form-group">
                                            <asp:BulletedList
                                                ID="BulletedList1"
                                                Width="500"
                                                runat="server"
                                                BulletStyle="Numbered"
                                                Font-Bold="True"
                                                DisplayMode="HyperLink" Target="_blank">
                                                <asp:ListItem  Value="NewJoinerReport.aspx">New Joiner List</asp:ListItem>
                                                <asp:ListItem Value="SeparationReport.aspx">Separation List</asp:ListItem>
                                                <%--<asp:ListItem Value="ProbationaryEmployeeReport.aspx">Probationary Employee List</asp:ListItem>--%>
                                                <asp:ListItem Value="PromotionalReport.aspx">Promotion</asp:ListItem>
                                                <%--<asp:ListItem Value="TransferInfoReport.aspx">Transfer</asp:ListItem>--%>
                                                <%--<asp:ListItem Value="ContractRenewalReport.aspx">Contract Renewal</asp:ListItem>--%>
                                                <asp:ListItem Value="ConfirmationReportList.aspx">Confirmation employee list
                                                </asp:ListItem>
                                                <%--
                                                <asp:ListItem Value="Promotion">Promotion</asp:ListItem>
                                                <asp:ListItem Value="Upgradation">Up-gradation</asp:ListItem>
                                                <asp:ListItem Value="LeaveEncashment">Leave Encashment</asp:ListItem>
                                                <asp:ListItem Value="ContractRenewal">Contract Renewal</asp:ListItem>
                                                <asp:ListItem Value="OtherDeduction">Other Deduction</asp:ListItem>
                                                <asp:ListItem Value="ActiveEmployeeList"> Active Employee List</asp:ListItem>
                                                <asp:ListItem Value="WPPFEntitlementList">WPPF Entitlement List</asp:ListItem>
                                                <asp:ListItem Value="IncentiveEntitlementList">Incentive Entitlement List</asp:ListItem>
                                                <asp:ListItem Value="FinalSettlementNoteMemo">Final Settlement Note / Memo
                                                </asp:ListItem>
                                                <asp:ListItem Value="LoanApprovalMemoNote">Loan Approval Memo/Note
                                                </asp:ListItem>
                                                <asp:ListItem Value="BillsTrainervendor">Bills (Trainer / vendor)
                                                </asp:ListItem>
                                                <asp:ListItem Value="confirmationemployeelist">Confirmation employee list
                                                </asp:ListItem>--%>
                                                <asp:ListItem Value="IncrementInformationReport.aspx">Annual / Monthly increment
                                                </asp:ListItem>
                                               <%-- <asp:ListItem Value="RedesignationrReport.aspx">Re-designation
                                                </asp:ListItem>--%>
                                                <%--<asp:ListItem Value="pickupanddropaddandcancel">Pickup and drop add and cancel
                                                </asp:ListItem>
                                                <asp:ListItem Value="lwp">Lwp
                                                </asp:ListItem>
                                                <asp:ListItem Value="reappointment">Re-appointment
                                                </asp:ListItem>
                                                <asp:ListItem Value="stopdisbusementofsalary">Stop disbusement of salary
                                                </asp:ListItem>--%>
                                            </asp:BulletedList>

                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>


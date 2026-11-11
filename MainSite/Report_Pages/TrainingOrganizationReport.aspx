<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="TrainingOrganizationReport.aspx.cs" Inherits="Report_Pages_TrainingOrganizationReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">

    <div class="content" id="content">
        <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                      <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px"
                   />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
        <!-- PAGE HEADING -->
        <div class="page-heading">
            <div class="page-heading__container">
                <div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>
                <span></span>
                <h1 class="title" style="font-size: 18px; padding-top: 9px;">Training Organization Report </h1>
            </div>

            <%--<div class="page-heading__container float-right d-none d-sm-block">
                <asp:Button ID="homeButton" Visible="False" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                <asp:Button ID="addNewButton" Visible="False" Text="Add New Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="addNewButton_OnClick" />
            </div>--%>
        </div>
        <!-- //END PAGE HEADING -->

        <div class="container-fluid">
            <div class="card">
                <div class="card-body">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaiting">
                                        <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>


                            <div class="form-row">
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Company</label> <span style="color:red;" >*</span>
                                        <asp:DropDownList runat="server" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" AutoPostBack="true" CssClass="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Organization</label>
                                        <asp:DropDownList ID="ddlOrganization"  AutoPostBack="true" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>

                                <%--<div class="col-2">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                        <asp:DropDownList ID="ddlFinancialYear" OnSelectedIndexChanged="ddlFinancialYear_OnSelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Training Title</label>
                                        <asp:DropDownList runat="server" ID="ddlTrainingRecord" AutoPostBack="true" CssClass="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>--%>

                                <div class="col-md-1">
                                    <div class="form-group">
                                        <label>&nbsp;&nbsp;</label>
                                        <asp:Button runat="server" ID="SearchButton" OnClick="SearchButton_OnClick" ToolTip="Click To Search" Text="Search" class="btn btn-outline-info btn-block disabled btn-sm" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div class="row">
                        <div class="col-md-2">
                            <%--<label>Attendance List</label>--%>
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-1">
                        </div>
                        <div class="col-md-3">
                            <asp:LinkButton ID="btnExportToExcel" runat="server" CssClass="btnexcel  pull-right" OnClick="btnExportToExcel_Click"><span aria-hidden="true" class="glyphicon glyphicon-ok"></span>Export To Excel</asp:LinkButton>
                            <%--<asp:LinkButton ID="btnExportToPDF" runat="server" Visible="False" CssClass="btnPDF  pull-right" OnClick="btnExportToPDF_Click"><span aria-hidden="true" class="glyphicon glyphicon-apple"></span>Export To PDF</asp:LinkButton>--%>
                        </div>

                    </div>

                    <style>
                        .btnexcel {
                            background-color: #4CAF50;
                            border: none;
                            color: white;
                            padding: 8px 12px;
                            text-align: center;
                            text-decoration: none;
                            display: inline-block;
                            font-size: 12px;
                            margin: 4px 2px;
                            cursor: pointer;
                        }

                        .btnPDF {
                            background-color: #008CBA;
                            border: none;
                            color: white;
                            padding: 8px 12px;
                            text-align: center;
                            text-decoration: none;
                            display: inline-block;
                            font-size: 12px;
                            margin: 4px 2px;
                            cursor: pointer;
                        }
                    </style>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div id="gridContainer1" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;">
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered text-center thead-dark">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--<asp:BoundField DataField="CompanyName" HeaderText="Company Name" />--%>
                                        <asp:BoundField DataField="TrainingOrgName" HeaderText="Organization Name" />
                                        <asp:BoundField DataField="OrgTypeName" HeaderText="Type" />
                                        <asp:BoundField DataField="Trainer" HeaderText="Trainer" />
                                        <asp:BoundField DataField="OfficeBranch" HeaderText="Office/Branch" />
                                        
                                        <asp:BoundField DataField="ContactPerson" HeaderText="Contact Person"/>
                                        <asp:BoundField DataField="ContactPersonEmail" HeaderText="Contact Person Email" />

                                        <asp:BoundField DataField="ContactPersonCell" HeaderText="Contact Person Cell" />
                                        <asp:BoundField DataField="OrgAddress" HeaderText="Address" />
                                        <asp:BoundField DataField="OrgProfile" HeaderText="Profile" />
                                        <asp:BoundField DataField="ForeignLocal" HeaderText="Foreign/Local" />
                                        <asp:BoundField DataField="VendorAudit" HeaderText="Vendor Audit" />
                                        <asp:BoundField DataField="ClientsRecommendation" HeaderText="Clients Recommendation" />
                                        <asp:BoundField DataField="LogisticsFacility" HeaderText="Logistics Facility" />
                                        
                                        <asp:BoundField DataField="HasTin" HeaderText="Has Tin" />
                                        <asp:BoundField DataField="HasVat" HeaderText="Has Vat" />
                                        <asp:BoundField DataField="HasTradeLicense" HeaderText="Has Trade License" />
                                        <asp:BoundField DataField="HasBankSolv" HeaderText="Has Bank Solv." />
                                        <asp:BoundField DataField="Others" HeaderText="Others" />

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
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
    </div>

</asp:Content>


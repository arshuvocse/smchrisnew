<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="ApplogReportView.aspx.cs" Inherits="MenuSetup_SupervisorApprovalEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    
    <style>
           #cpFormBody_loadGridView td {
            border: 1px solid #ddd;
            padding: 8px;
        }

        #cpFormBody_loadGridView tr:nth-child(even) tr:nth-child(even){background-color: white;}

 

        #cpFormBody_loadGridView th {
            padding: 10px;
            border-style: none;

            background-color: #CCCCCC!important;
            color: black!important;
            font-weight: bold;
            font-size: 13px;
        }

        
        .btnexcelcc {
            border: none;
            color: #131313;
            padding-left: 36px;
            padding-top: 8px;
            padding-bottom: 8px;
            padding-right: 36px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 12px;
            margin: 4px 2px;
            cursor: pointer;
            background: url(../Assets/excel.png);
            background-position: center;
            background-repeat: no-repeat;
            box-shadow: 0 0 3px 1px rgba(0,0,0,.35);
        }
    </style>
    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                       
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">   <img src="app.png"  width="20px" />  Approval Report </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <%--<asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                        <asp:Button ID="addNewButton" Text="Add New Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="addNewButton_OnClick" />--%>
                    </div>
                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">
                             <div class="row" >
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Company</label>
                                        <asp:DropDownList ID="companyDropDownList" class="form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="companyDropDownList_OnSelectedIndexChanged" CssClass="" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                 <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Financial Year </label>
                                        <asp:DropDownList ID="finyearDropDownList1" class="form-control form-control-sm" AutoPostBack="True"  CssClass="" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                 <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Approval Operation</label>
                                        <asp:DropDownList ID="menuDropDownList" class="form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="menuDropDownList_OnSelectedIndexChanged" CssClass="" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-1">
                                    <div class="form-group" style="margin-top: 18px;">

                                        <asp:LinkButton ID="btnFilterSearch" Text="Search" runat="server" OnClick="btnFilterSearch_OnClick" ToolTip="Click To Search" CssClass="btn btn-info   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search </asp:LinkButton>

                                    </div>

                                </div>
                            </div>
                            <br/>
                            <br/>
                            
                               <div class="form-row" style="padding-right: 10px">

                                <div class="col-md-6" style="padding-left: 2px">
                                    <label style="font-size: 18px;">Details Information</label>
                                </div>
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-2">
                                    <%--  <asp:LinkButton ID="btnExportToExcel" runat="server" CssClass="btnexcel  pull-right" Style="padding-right: 10px;" OnClick="btnExportToExcel_Click"><span aria-hidden="true" style="font-size: 14px; color: #4CAF50;" class="fa fa-file-excel-o"></span> Download to xls</asp:LinkButton>--%>

                                    <input type="button" id="btnExportDisciplinary" title="Export to Excel" class="pull-right btnexcelcc " value="" />
                                </div>


                            </div>

                            <hr />
                            <div id="gridContainer1" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;">
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered text-center thead-dark" >
                                    <Columns>
                                       
                                        <%--<asp:BoundField DataField="CompanyName" HeaderText="Company Name" />--%>
                                        <asp:BoundField DataField="Info" HeaderText="Employee Information" />
                                        <asp:BoundField DataField="FromEmp" HeaderText="From Person" />
                                        <asp:BoundField DataField="ToEmp" HeaderText="To Person" />
                                        <asp:BoundField DataField="ActionStatus" HeaderText="Approval Status" />
                                        <asp:BoundField DataField="Version" HeaderText="Version" />
                                        <asp:BoundField DataField="ApproveDate" HeaderText="Approval Date"  DataFormatString="{0:dd-MMM-yyyy}" />
                                        
                                    </Columns>
                                </asp:GridView>
                            </div>
                            
                            <%--<div class="form-group">
                                <asp:Button ID="submitButton" Text="Save" CssClass="btn btn-sm btn-info" Visible="True" runat="server" OnClick="submitButton_OnClick" />
                            </div>--%>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

     <script src="../Assets/table2excel.js"></script>
    <script type="text/javascript">
        $("body").on("click", "#btnExportDisciplinary", function () {
            $("[id*=loadGridView]").table2excel({
                filename: " Approval_Report _Info.xls"
            });
        });

    </script>
</asp:Content>


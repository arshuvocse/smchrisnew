<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="ClearanceForwardingList.aspx.cs" Inherits="ExitManagement_UI_ClearanceForwardingList" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.19/css/dataTables.bootstrap4.min.css" />
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.19/js/dataTables.bootstrap4.min.js"></script>
    <link rel="stylesheet" type="text/css" href="../Assets/assets/js/select2/select2.css" />
    <link rel="stylesheet" type="text/css" href="../Assets/assets/js/select2/select2-bootstrap.css" />
    <style type="text/css">
        .clearance-employee-select + .select2,
        .clearance-employee-select + .select2-container,
        .select2-container {
            width: 100% !important;
        }
        .select2-container--disabled .select2-choice,
        .select2-container--disabled .select2-selection,
        .select2-container.select2-container--disabled {
            background-color: #e9ecef !important;
            cursor: not-allowed !important;
            opacity: 0.85 !important;
        }
        .select2-container--disabled .select2-arrow,
        .select2-container--disabled .select2-selection__arrow {
            display: none !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">

    <style>
        .table thead th {
            background-color: #5B799E;
            color: white;
        }
        .badge-responsible {
            background-color: #20c997 !important;
            color: #ffffff !important;
            padding: 2px 8px !important;
            border-radius: 4px !important;
            font-size: 11px !important;
            font-weight: bold !important;
            display: inline-block !important;
            box-shadow: 0 1px 2px rgba(0,0,0,0.15) !important;
            text-shadow: none !important;
        }
        .badge-alternate {
            background-color: #6c757d !important;
            color: #ffffff !important;
            padding: 2px 8px !important;
            border-radius: 4px !important;
            font-size: 11px !important;
            font-weight: 500 !important;
            display: inline-block !important;
            box-shadow: 0 1px 2px rgba(0,0,0,0.1) !important;
            text-shadow: none !important;
        }
    </style>


    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>

                <div class="page-heading">
                    <div class="page-heading__container">
                        <div class="icon"><img src="../Report_Pages/app.png" width="20px" /></div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Clearance Auto Forwarding List</h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary" runat="server" OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                        <asp:LinkButton ID="addNewButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-success" runat="server" OnClick="addNewButton_OnClick"><i class="fa fa-plus"></i>&nbsp;Add New</asp:LinkButton>
                    </div>
                </div>

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">

                            <div class="form-row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Company</label>
                                        <asp:DropDownList runat="server" ID="ddlCompany" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" CssClass="form-control form-control-sm" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Employee Name</label>
                                        <asp:DropDownList runat="server" ID="ddlEmployee" AutoPostBack="True" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged" CssClass="form-control form-control-sm clearance-employee-select" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Date From</label>
                                        <asp:TextBox ID="txtDateFrom" AutoCompleteType="Disabled" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="TopLeft" Format="dd-MMM-yyyy" TargetControlID="txtDateFrom" CssClass="MyCalendar" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Date To</label>
                                        <asp:TextBox ID="txtDateTo" AutoCompleteType="Disabled" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" PopupPosition="TopLeft" Format="dd-MMM-yyyy" TargetControlID="txtDateTo" CssClass="MyCalendar" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Status</label>
                                        <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control form-control-sm">
                                            <asp:ListItem Text="All" Value="" />
                                            <asp:ListItem Text="Forwarded" Value="Forwarded" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="form-row">
                                <div class="col-md-10"></div>
                                <div class="col-md-2" style="margin-top: 5px;">
                                    <div class="form-group">
                                        <asp:LinkButton runat="server" ID="btnSearch" OnClick="btnSearch_Click" ToolTip="Click To Search" CssClass="btn btn-info btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>&nbsp;Search</asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="btnReset" OnClick="btnReset_Click" CssClass="btn btn-warning btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>&nbsp;Reset</asp:LinkButton>
                                    </div>
                                </div>
                            </div>

                            <div id="gridContainer1" style="height: auto; overflow: auto; width: auto;">
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered text-center thead-dark" DataKeyNames="ClearanceForwardingId,EmployeeId,ForwardedBy,Status"
                                    OnRowCommand="loadGridView_RowCommand" OnPreRender="loadGridView_PreRender">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CompanyName" HeaderText="Company" />
                                        <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee Code" />
                                        <asp:BoundField DataField="EmpName" HeaderText="Employee" />
                                        <asp:TemplateField HeaderText="Supervised Employee(s)">
                                            <ItemTemplate>
                                                <div style="text-align: left; min-width: 250px;">
                                                    <%# FormatSupervisedEmployeeList(Eval("SupervisedEmployeeNames")) %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ForwardedDate" HeaderText="Forward Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="ForwardedByName" HeaderText="Forwarded By" />
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <span class="badge" style='<%# Eval("Status") != null && Eval("Status").ToString() == "Forwarded" ? "background-color: #6f42c1; color: white; padding: 4px 8px; border-radius: 10px; font-size: 11px;" : "background-color: #6c757d; color: white; padding: 4px 8px; border-radius: 10px; font-size: 11px;" %>'>
                                                    <i class="fa fa-random"></i> <%# Eval("Status") != null && Eval("Status").ToString() == "Forwarded" ? "Auto Forwarded" : Eval("Status") %>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <a href='<%# "ClearanceForwardingEntry.aspx?id=" + Eval("ClearanceForwardingId") %>' title="Edit" class="btn btn-sm btn-outline-primary" style="padding: 2px 6px; display: inline-block;">
                                                    <img src="../Assets/img/rsz_edit.png" alt="Edit" style="border:0; vertical-align: middle;" />
                                                </a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="viewImageButton" runat="server"
                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="ViewData"
                                                    ImageUrl="~/Assets/img/list-view.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                            <asp:Panel ID="pnlDetails" runat="server" Visible="False" CssClass="card" Style="margin-top: 15px;">
                                <div class="card-body">
                                    <h3 style="font-size: 16px;">Clearance Forwarding Details</h3>
                                    <div class="row">
                                        <div class="col-md-3"><label>Company: </label><asp:Label ID="lblDetailCompany" runat="server" /></div>
                                        <div class="col-md-3"><label>Employee: </label><asp:Label ID="lblDetailEmployee" runat="server" /></div>
                                        <div class="col-md-3"><label>Forwarded By: </label><asp:Label ID="lblDetailForwardedBy" runat="server" /></div>
                                        <div class="col-md-3"><label>Forward Date: </label><asp:Label ID="lblDetailForwardDate" runat="server" /></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3"><label>Status: </label><asp:Label ID="lblDetailStatus" runat="server" /></div>
                                        <div class="col-md-9"><label>Remarks: </label><asp:Label ID="lblDetailRemarks" runat="server" /></div>
                                    </div>

                                    <label style="font-size: 14px; font-weight: bold; margin-top: 10px;">Directly Supervised Employee List</label>
                                    <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="False" DataKeyNames="ClearanceForwardingDetailId,SupervisedEmployeeId" OnRowCommand="gvDetail_RowCommand" CssClass="table table-bordered text-center thead-dark">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Priority / Order">
                                                <ItemTemplate>
                                                    <span class='<%# GetPriorityBadgeClass(Eval("OrderNo"), Container.DataItemIndex) %>' style='<%# GetPriorityBadgeStyle(Eval("OrderNo"), Container.DataItemIndex) %>'>
                                                        <%# GetPriorityText(Eval("OrderNo"), Container.DataItemIndex) %>
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee Code" />
                                            <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                            <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                            <asp:BoundField DataField="Status" HeaderText="Status" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" CommandName="CompleteForwarding" CommandArgument="<%# Container.DataItemIndex %>"
                                                        CssClass="btn btn-sm btn-success" Visible='<%# CanCurrentUserComplete(Eval("Status"), Eval("SupervisedEmployeeId")) %>'>Complete</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                    <asp:LinkButton ID="btnCloseDetails" runat="server" OnClick="btnCloseDetails_Click" CssClass="btn btn-sm btn-outline-secondary">Close</asp:LinkButton>
                                </div>
                            </asp:Panel>

                        </div>
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <script type="text/javascript">
        function initClearanceEmployeeSelect2() {
            var $ = window.jQuery || jQuery;
            if (!$ || !$.fn.select2) {
                setTimeout(initClearanceEmployeeSelect2, 100);
                return;
            }

            $('.clearance-employee-select').each(function () {
                var $ddl = $(this);
                if ($ddl.hasClass('select2-hidden-accessible')) {
                    $ddl.select2('destroy');
                }
                $ddl.select2({
                    width: '100%'
                });
            });
        }

        function initGridDataTable() {
            var $ = window.jQuery || jQuery;
            if (!$ || !$.fn.DataTable) {
                return;
            }

            var $grid = $('#<%=loadGridView.ClientID%>');
            if ($grid.length && $grid.find('tbody tr').length > 0 && $grid.find('thead th').length > 0) {
                if ($.fn.DataTable.isDataTable($grid)) {
                    $grid.DataTable().destroy();
                }
                $grid.DataTable({ "bInfo": true, "bFilter": true, pageLength: 10 });
            }
        }

        function pageLoad() {
            initClearanceEmployeeSelect2();
            initGridDataTable();
        }

        $(document).ready(function () {
            initClearanceEmployeeSelect2();
            initGridDataTable();
        });

        if (typeof Sys !== 'undefined' && Sys.WebForms && Sys.WebForms.PageRequestManager) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm) {
                prm.add_endRequest(function () {
                    initClearanceEmployeeSelect2();
                    initGridDataTable();
                });
            }
        }
    </script>
</asp:Content>

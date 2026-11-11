<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="ClearanceForwardingEntry.aspx.cs" Inherits="ExitManagement_UI_ClearanceForwardingEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .clearance-employee-select + .select2 {
            width: 100% !important;
        }
        .btn-move-up, .btn-move-down {
            padding: 2px 7px;
            font-size: 11px;
            line-height: 1.2;
            border-radius: 3px;
            transition: all 0.15s ease-in-out;
            cursor: pointer;
        }
        .btn-move-up:hover {
            background-color: #007bff !important;
            color: #ffffff !important;
            border-color: #007bff !important;
            transform: translateY(-2px);
        }
        .btn-move-down:hover {
            background-color: #007bff !important;
            color: #ffffff !important;
            border-color: #007bff !important;
            transform: translateY(2px);
        }
        .selected-supervised-row {
            background-color: #eaf4fd !important;
        }
        .row-highlight {
            background-color: #fff3cd !important;
            transition: background-color 0.4s ease;
        }
        .priority-badge {
            display: inline-block;
            min-width: 125px;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hfClearanceForwardingId" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfOrderedSupervisedIds" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfEditCompanyId" runat="server" />
                <asp:HiddenField ID="hfEditEmployeeId" runat="server" />

                <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>

                <div class="page-heading">
                    <div class="page-heading__container">
                        <div class="icon"></div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;"><img src="../Report_Pages/app.png" width="20px" /> <asp:Label ID="lblPageTitle" runat="server" Text="Clearance Auto Forwarding" /></h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary" runat="server" OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                        <asp:LinkButton ID="ListViewButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-success" runat="server" OnClick="ListViewButton_OnClick"><i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
                    </div>
                </div>

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">

                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Company</label>
                                        <span style="color: red">&nbsp;*</span>
                                        <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" CssClass="form-control form-control-sm" />
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Employee Name</label>
                                        <span style="color: red">&nbsp;*</span>
                                        <asp:DropDownList ID="ddlEmployee" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged" CssClass="form-control form-control-sm clearance-employee-select" />
                                    </div>
                                </div>

                                <div class="col-md-5">
                                    <div class="form-group">
                                        <label>Remarks</label>
                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control form-control-sm" TextMode="MultiLine" Rows="1"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="d-flex justify-content-between align-items-center mb-2" style="background: #f8f9fa; padding: 8px 12px; border-radius: 4px; border-left: 4px solid #007bff;">
                                        <label style="font-size: 14.5px; font-weight: bold; margin-bottom: 0; color: #2c3e50;">
                                            <i class="fa fa-users text-primary"></i> Employee List (Same Division)
                                        </label>
                                        <span class="text-muted" style="font-size: 11.5px;">
                                            <i class="fa fa-info-circle text-info"></i> The 1st employee selected becomes <strong>Responsible Person</strong>. (Use <i class="fa fa-arrow-up text-primary"></i> <i class="fa fa-arrow-down text-primary"></i> to reorder if needed).
                                        </span>
                                    </div>

                                    <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                        CssClass="table table-bordered text-center thead-dark table-hover" DataKeyNames="EmpInfoId" ClientIDMode="Static">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select" HeaderStyle-Width="60px">
                                                <HeaderTemplate>
                                                    <input type="checkbox" id="chkSelectAllSupervised" onclick="ToggleSelectAllSupervised(this)" title="Select All" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSupervisedSelect" runat="server" onclick="OnRowSelectChanged(this)" />
                                                    <asp:HiddenField ID="hfRowEmpInfoId" Value='<%# Eval("EmpInfoId") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Priority / Order" HeaderStyle-Width="155px">
                                                <ItemTemplate>
                                                    <span class="priority-label">
                                                        <span class="badge badge-light text-muted border p-1" style="font-size: 11px;">Not Selected</span>
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Sort / Reorder" HeaderStyle-Width="95px">
                                                <ItemTemplate>
                                                    <div class="btn-group btn-group-sm" role="group">
                                                        <button type="button" class="btn btn-outline-primary btn-move-up" onclick="MoveRowUp(this); return false;" title="Move Up (Higher Priority)">
                                                            <i class="fa fa-arrow-up"></i>
                                                        </button>
                                                        <button type="button" class="btn btn-outline-primary btn-move-down" onclick="MoveRowDown(this); return false;" title="Move Down (Lower Priority)">
                                                            <i class="fa fa-arrow-down"></i>
                                                        </button>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee Code" HeaderStyle-Width="110px" />
                                            <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                            <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                            <asp:BoundField DataField="DivisionName" HeaderText="Division" HeaderStyle-Width="140px" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <div class="alert alert-warning text-center m-2" style="font-size: 13px;">
                                                <i class="fa fa-exclamation-triangle"></i> No employee found in this employee's division.
                                            </div>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                            </div>

                            <br />

                            <div class="form-group">
                                <asp:Button ID="btnForward" Text="Forward" CssClass="btn btn-sm btn-info" runat="server" OnClick="btnForward_Click" />
                                <asp:Button ID="btnClear" Text="Clear" CssClass="btn btn-sm warning" runat="server" OnClick="btnClear_Click" BackColor="#FFCC00" />
                            </div>

                        </div>
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <script type="text/javascript">
        var orderedSelectedIds = [];

        function getEmpIdFromRow($row) {
            var hf = $row.find("input[id*='hfRowEmpInfoId']");
            if (hf.length && hf.val()) {
                return hf.val().trim();
            }
            return '';
        }

        function initOrderedSelection() {
            var val = $('#hfOrderedSupervisedIds').val() || '';
            if (val.trim().length > 0) {
                orderedSelectedIds = val.split(',').map(function (s) { return s.trim(); }).filter(function (s) { return s.length > 0; });
            } else {
                orderedSelectedIds = [];
                var grid = $('#<%=loadGridView.ClientID%>');
                if (grid.length) {
                    grid.find('tbody tr').each(function () {
                        var row = $(this);
                        if (row.find('th').length > 0) return;
                        var chk = row.find("input[type='checkbox'][id*='chkSupervisedSelect']");
                        if (chk.is(':checked')) {
                            var id = getEmpIdFromRow(row);
                            if (id && orderedSelectedIds.indexOf(id) === -1) {
                                orderedSelectedIds.push(id);
                            }
                        }
                    });
                }
            }
            RenderPriorityBadges();
        }

        function OnRowSelectChanged(chk) {
            var $chk = $(chk);
            var $row = $chk.closest('tr');
            var empId = getEmpIdFromRow($row);
            if (!empId) return;

            if ($chk.is(':checked')) {
                // When checked: whichever is selected first becomes Primary Person, followed by Alternate...
                if (orderedSelectedIds.indexOf(empId) === -1) {
                    orderedSelectedIds.push(empId);
                }
            } else {
                // When unchecked: remove from selection order; remaining items automatically shift
                orderedSelectedIds = orderedSelectedIds.filter(function (id) {
                    return id !== empId;
                });
            }

            RenderPriorityBadges();
        }

        function ToggleSelectAllSupervised(header) {
            var grid = $('#<%=loadGridView.ClientID%>');
            if (!grid.length) return;

            if (header.checked) {
                grid.find('tbody tr').each(function () {
                    var row = $(this);
                    if (row.find('th').length > 0) return;
                    var empId = getEmpIdFromRow(row);
                    if (empId && orderedSelectedIds.indexOf(empId) === -1) {
                        orderedSelectedIds.push(empId);
                    }
                });
            } else {
                orderedSelectedIds = [];
            }

            RenderPriorityBadges();
        }

        function MoveRowUp(btn) {
            var row = $(btn).closest('tr');
            var empId = getEmpIdFromRow(row);

            if (empId && orderedSelectedIds.indexOf(empId) > 0) {
                var idx = orderedSelectedIds.indexOf(empId);
                var temp = orderedSelectedIds[idx - 1];
                orderedSelectedIds[idx - 1] = orderedSelectedIds[idx];
                orderedSelectedIds[idx] = temp;
            }

            var prev = row.prev('tr');
            if (prev.length && prev.find('th').length === 0) {
                row.insertBefore(prev);
                row.addClass('row-highlight');
                setTimeout(function () { row.removeClass('row-highlight'); }, 400);
            }

            RenderPriorityBadges();
        }

        function MoveRowDown(btn) {
            var row = $(btn).closest('tr');
            var empId = getEmpIdFromRow(row);

            if (empId && orderedSelectedIds.indexOf(empId) >= 0 && orderedSelectedIds.indexOf(empId) < orderedSelectedIds.length - 1) {
                var idx = orderedSelectedIds.indexOf(empId);
                var temp = orderedSelectedIds[idx + 1];
                orderedSelectedIds[idx + 1] = orderedSelectedIds[idx];
                orderedSelectedIds[idx] = temp;
            }

            var next = row.next('tr');
            if (next.length) {
                row.insertAfter(next);
                row.addClass('row-highlight');
                setTimeout(function () { row.removeClass('row-highlight'); }, 400);
            }

            RenderPriorityBadges();
        }

        function RenderPriorityBadges() {
            var grid = $('#<%=loadGridView.ClientID%>');
            if (!grid.length) return;

            var priorityMap = {};
            for (var i = 0; i < orderedSelectedIds.length; i++) {
                priorityMap[orderedSelectedIds[i]] = i + 1;
            }

            grid.find('tbody tr').each(function () {
                var row = $(this);
                if (row.find('th').length > 0) return;

                var chk = row.find("input[type='checkbox'][id*='chkSupervisedSelect']");
                var badgeContainer = row.find('.priority-label');
                var empId = getEmpIdFromRow(row);

                if (empId && priorityMap[empId]) {
                    var orderIndex = priorityMap[empId];
                    chk.prop('checked', true);
                    row.addClass('selected-supervised-row');

                    var badgeHtml = '';
                    if (orderIndex === 1) {
                        badgeHtml = '<span class="badge-responsible" style="background-color: #20c997; color: #ffffff; padding: 3px 8px; border-radius: 4px; font-size: 11px; font-weight: bold; display: inline-block; box-shadow: 0 1px 2px rgba(0,0,0,0.15);"><i class="fa fa-star" style="color: #ffc107; margin-right: 3px;"></i> Responsible Person</span>';
                    } else {
                        badgeHtml = '<span class="badge-alternate" style="background-color: #6c757d; color: #ffffff; padding: 3px 8px; border-radius: 4px; font-size: 11px; font-weight: 500; display: inline-block; box-shadow: 0 1px 2px rgba(0,0,0,0.1);"><i class="fa fa-user" style="margin-right: 3px;"></i> Alternate</span>';
                    }
                    badgeContainer.html(badgeHtml);
                } else {
                    chk.prop('checked', false);
                    row.removeClass('selected-supervised-row');
                    badgeContainer.html('<span style="background-color: #f8f9fa; color: #6c757d; border: 1px solid #ced4da; padding: 2px 7px; border-radius: 4px; font-size: 11px; display: inline-block;">Not Selected</span>');
                }
            });

            $('#hfOrderedSupervisedIds').val(orderedSelectedIds.join(','));

            var totalCheckboxes = grid.find("input[type='checkbox'][id*='chkSupervisedSelect']").length;
            var checkedCount = orderedSelectedIds.length;
            var headerChk = document.getElementById('chkSelectAllSupervised');
            if (headerChk) {
                headerChk.checked = (totalCheckboxes > 0 && checkedCount === totalCheckboxes);
            }
        }

        function pageLoad() {
            initClearanceEmployeeSelect2();
            initOrderedSelection();
        }

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
    </script>
</asp:Content>

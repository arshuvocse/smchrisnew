<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="TrainingAttendanceReport.aspx.cs" Inherits="Report_Pages_TrainingAttendanceReport" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <style>
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
                
                <h1 class="title" style="font-size: 18px; padding-top: 9px;"><img src="app.png"  width="18px" />  Training Attendance Report </h1>
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
                                        <asp:DropDownList runat="server" ID="ddlCompany" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" CssClass="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Financial Year</label> <span style="color:red;" >*</span>
                                        <asp:DropDownList ID="ddlFinancialYear" OnSelectedIndexChanged="ddlFinancialYear_OnSelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Training Title</label> <span style="color:red;" >*</span>
                                        <asp:DropDownList runat="server" ID="ddlTrainingRecord" AutoPostBack="true" CssClass="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                
                                 <div class="col-2" runat="server" >
                                <div class="form-group">
                                    <label class="control-label">Training From Date</label>
                                    <asp:TextBox runat="server" ID="ContractualstartDate" class="form-control form-control-sm" AutoPostBack="True" ></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender39" runat="server"
                                                                  Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                  TargetControlID="ContractualstartDate" />
                                </div>
                            </div>
                            <div class="col-2" runat="server" >
                                <div class="form-group">
                                    <label class="control-label">Training To Date</label>
                                    <asp:TextBox runat="server" ID="ContractualendDate" class="form-control form-control-sm" AutoPostBack="True" ></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender40" runat="server"
                                                                  Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                  TargetControlID="ContractualendDate" />
                                </div>
                            </div>
                                
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <label>&nbsp;&nbsp;</label> <br />
                                        <asp:Button runat="server" ID="SearchButton" OnClick="SearchButton_OnClick" ToolTip="Click To Search" Text="Search" class="btn btn-outline-info btn-block disabled btn-sm" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div class="row" runat="server" Visible="False">
                        <div class="col-md-2">
                            <label>Attendance List</label>
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
                    
                      <br/>
                            <br/>
                            <div class="form-row" style="padding-right: 10px">

                                <div class="col-md-6" style="padding-left: 2px">
                                    <label style="font-size: 16px;">Details Information</label>
                                </div>
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-2">
                                    <%--  <asp:LinkButton ID="btnExportToExcel" runat="server" CssClass="btnexcel  pull-right" Style="padding-right: 10px;" OnClick="btnExportToExcel_Click"><span aria-hidden="true" style="font-size: 14px; color: #4CAF50;" class="fa fa-file-excel-o"></span> Download to xls</asp:LinkButton>--%>

                                    <input type="button" id="btnExportDisciplinary" title="Export to Excel" class="pull-right btnexcelcc " value="" />
                                </div>


                            </div>

                            <hr />

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
                                         <asp:BoundField DataField="TrainingTitle" HeaderText="Training Title" />
                                         <%--<asp:BoundField DataField="FinancialYearDesc" HeaderText="Financial Year" />--%>
                                         <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee ID" />
                                         <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                         <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                         <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                         <asp:BoundField DataField="ATTDate" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                         <asp:BoundField DataField="Status" HeaderText="Absent/Present" />
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
        <script src="../Assets/table2excel.js"></script>
    <script type="text/javascript">
        $("body").on("click", "#btnExportDisciplinary", function () {
            $("[id*=loadGridView]").table2excel({
                filename: "Training_Attendance_Report_List.xls"
            });
        });

    </script>
</asp:Content>


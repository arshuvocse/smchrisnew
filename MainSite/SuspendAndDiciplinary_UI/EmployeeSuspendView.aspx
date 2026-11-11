<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="EmployeeSuspendView.aspx.cs" Inherits="SuspendAndDiciplinary_UI_EmployeeSuspendView" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">

    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                        <div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Employee Suspend List </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="addNewButton" Text="Add New Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="addNewButton_OnClick" />

                        <%--<asp:Button ID="reloadButton" Text="Refresh" CssClass="btn btn-sm btn-outline-success" runat="server" OnClick="reloadButton_OnClick" />--%>
                    </div>

                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
        <!-- //END PAGE HEADING -->

        <div class="container-fluid">
            <div class="card">
                <div class="card-body">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="form-row">

                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Company</label>
                                        <asp:DropDownList runat="server" ID="ddlCompany" class="form-control form-control-sm" />
                                    </div>
                                </div>



                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Effective From Date </label>

                                        <asp:TextBox ID="EffectiveDateTextBox" AutoCompleteType="Disabled" AutoPostBack="True" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="TopLeft"
                                            Format="dd-MMM-yyyy" PopupButtonID="EffectiveDateTextBox" CssClass="MyCalendar"
                                            TargetControlID="EffectiveDateTextBox" />
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Effective To Date </label>

                                        <asp:TextBox ID="EffectToDate" AutoCompleteType="Disabled" AutoPostBack="True" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupPosition="TopLeft"
                                            Format="dd-MMM-yyyy" PopupButtonID="EffectToDate" CssClass="MyCalendar"
                                            TargetControlID="EffectToDate" />
                                    </div>
                                </div>

                                <div class="col-md-2" style="margin-top: 17px;">
                                    <div class="form-group">

                                        <asp:Button runat="server" ID="SearchButton" OnClick="SearchButton_OnClick" ToolTip="Click To Search" Text="Search" class="btn btn-outline-info disabled btn-sm" />
                                    </div>
                                </div>



                            </div>


                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div class="row">
                        <div class="col-md-12">
                            <label></label>
                        </div>


                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-1">
                        </div>
                        <div class="col-md-1">
                        </div>
                        <div class="col-md-1">
                        </div>
                        <div class="col-md-1">
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
                                </style>
                        <div class="col-md-2 ">
                            <asp:LinkButton ID="btnExportToExcel" runat="server" CssClass="btnexcel pull-right" OnClick="btnExportToExcel_Click"><span aria-hidden="true" class="glyphicon glyphicon-ok"></span>Export To Excel</asp:LinkButton>




                        </div>
                    </div>
                     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                    <div class="row" runat="server" visible="False">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label></label>
                                <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True" RepeatDirection="Horizontal" Enabled="False">
                                </asp:CheckBoxList>


                            </div>
                        </div>
                    </div>

                    <div id="gridContainer1" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: hidden;">
                        <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                            CssClass="table table-bordered text-center thead-dark" DataKeyNames="SuspendId,IsSuspand"
                            OnRowCommand="loadGridView_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="SL">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Print">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ViewReportImageButton" runat="server" class="btn btn-white btn-sm  " CommandArgument='<%#Eval("SuspendId") %>'
                                            CommandName="Preview" ImageUrl="~/Assets/report_magnify.png" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee ID" />
                                <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                <asp:BoundField DataField="DepartmentName" HeaderText="Department" />

                                <%--<asp:BoundField DataField="ActionStatus" HeaderText="Status" />--%>

                                <%-- <asp:BoundField DataField="Description" HeaderText="Description" />
                                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" />--%>

                                <asp:BoundField DataField="SuspendReasonEntry" HeaderText="Action Taken" />
                                <asp:BoundField DataField="EffectiveDate" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Effective From Date" />
                                <asp:BoundField DataField="EffectiveToDate" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Effective To Date" />
                                <asp:BoundField DataField="UserName" HeaderText="Create By" />
                                <asp:BoundField DataField="EntryDate" HeaderText="Create Date" DataFormatString="{0:dd-MMM-yyyy}" />


                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="editImageButton" runat="server"
                                            CommandArgument="<%# Container.DataItemIndex %>" CommandName="EditData"
                                            ImageUrl="~/Assets/img/rsz_edit.png" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="115px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="deleteImageButton" runat="server"
                                            CommandArgument="<%# Container.DataItemIndex %>" CommandName="DeleteData"
                                            ImageUrl="~/Assets/img/delete.png" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="View" HeaderStyle-Width="115px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="viewImageButton" runat="server"
                                            CommandArgument="<%# Container.DataItemIndex %>" CommandName="ViewData"
                                            ImageUrl="~/Assets/img/list-view.png" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--<asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDownload" runat="server"   CommandName="cmd"> Suspend Release  &gt; &gt; &gt; &gt; </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                            </Columns>
                        </asp:GridView>
                    </div>
                            
                             </ContentTemplate>
        </asp:UpdatePanel>
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
            <br />
        </div>
       
    </div>

</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="ExitInterViewFormReport.aspx.cs" Inherits="Report_Pages_ExitInterViewFormReport" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    
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
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                        <div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Exit Interview Report </h1>
                    </div>

                    <%--<div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                        <asp:Button ID="addNewButton" Text="Add New Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="addNewButton_OnClick" />
                    </div>--%>



                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">

                            <div class="row" runat="server" visible="False">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label></label>
                                        <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True" RepeatDirection="Horizontal" Enabled="False">
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Company Name </label>
                                        <span style="color: red">&nbsp;*</span>
                                        <asp:DropDownList ID="companyDropDownList" class="form-control form-control-sm" runat="server" AutoPostBack="True"></asp:DropDownList>
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
                                <div class="col-md-2">
                                    <div class="form-group" style="margin-top: 17px;">

                                        <asp:Button runat="server" ID="SearchButton" OnClick="SearchButton_OnClick" ToolTip="Click To Search" Text="Search" class="btn btn-outline-info disabled btn-sm" />
                                    </div>
                                </div>
                            </div>

                            <div id="gridContainer1" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;">
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered text-center thead-dark" DataKeyNames="EmployeeJobLeftId,IsJobLeft,ExitMasterId"
                                    OnRowCommand="loadGridView_RowCommand" OnRowCreated="loadGridView_OnRowCreated">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                          <asp:TemplateField HeaderText="Preview">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ViewReportImageButton" runat="server" class="btn btn-white btn-sm  " CommandArgument='<%#Eval("ExitMasterId") %>'
                                                CommandName="Preview" ImageUrl="~/Assets/report_magnify.png" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <%--<asp:BoundField DataField="CompanyName" HeaderText="Company Name" />--%>
                                        <asp:BoundField DataField="EmpName" HeaderText="Empployee Name" />
                                        <asp:BoundField DataField="JobLeftType" HeaderText="Job Left Type" />
                                        <asp:BoundField DataField="SubmissionDate" HeaderText="Submission Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="JobLeftDate" HeaderText="Seperation Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="Reason" HeaderText="Reason" />
                                        <asp:BoundField DataField="ExitFormStatus" HeaderText="Status" />
                                        <asp:BoundField DataField="EntryBy" HeaderText="Create By" />
                                        <asp:BoundField DataField="EntryDate" HeaderText="Create Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="UpdateBy" HeaderText="UpdateBy" />
                                        <asp:BoundField DataField="UpdateDate" HeaderText="UpdateDate" DataFormatString="{0:dd-MMM-yyyy}" />

                                      

                                    </Columns>
                                </asp:GridView>
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>


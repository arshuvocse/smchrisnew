<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="DeadlineExtendedEntryApproval.aspx.cs" Inherits="Appraisal_DeadlineExtendedEntryApproval" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">

    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>

                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;">Deadline Extension  Request Approval</h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="detailsViewButton" Text="View List" Visible="False" OnClick="detailsViewButton_OnClick" CssClass="btn btn-sm btn-outline-secondary " runat="server" />
                        </div>
                    </div>

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
                            <div class="form-row">


                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Company</label>
                                        <asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" class="form-control form-control-sm" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                        <asp:DropDownList ID="ddlFinancialYear" AutoPostBack="true" OnSelectedIndexChanged="ddlFinancialYear_OnSelectedIndexChanged" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-2" style="margin-top: 18px" runat="server" visible="False">
                                    <div class="form-group">
                                        <asp:RadioButtonList ID="rbDeptOrEmp" AutoPostBack="True" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbDeptOrEmp_SelectedIndexChanged">
                                            <asp:ListItem>Department</asp:ListItem>
                                            <asp:ListItem>Employee</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>

                                <div class="col-2" id="DptShow" runat="server">
                                    <div class="form-group">
                                        <label>Department</label>
                                        <asp:DropDownList runat="server" ID="ddlDept" CssClass="form-control form-control-sm" />
                                    </div>
                                </div>
                                
                                      <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Operation</label>
                                        <asp:DropDownList ID="OperationDropDownList" runat="server" class="form-control form-control-sm" AutoPostBack="True">
                                            <asp:ListItem Value="Nullss">Select One........</asp:ListItem>  <asp:ListItem Value="BSC/OKR">BSC/OKR</asp:ListItem>
                                            <asp:ListItem Value="Apprisal">Apprisal</asp:ListItem>
                                            <asp:ListItem Value="KPI">KPI</asp:ListItem>

                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-2" runat="server">
                                    <div class="form-group" style="margin-top: 18px;">

                                        <asp:Button ID="SearchButton" Text="Search" CssClass="btn btn-sm btn-info" runat="server" OnClick="SearchButton_OnClick" />
                                    </div>
                                </div>

                            </div>
                            <div class="row" runat="server" visible="False">

                                <div class="col-md-2" style="margin-top: 17px;">
                                    <div class="form-group">
                                        <asp:CheckBox ID="chk_Common" runat="server" Text=" Is Common" OnCheckedChanged="chk_Common_OnCheckedChanged" AutoPostBack="True" TextAlign="Right"></asp:CheckBox>




                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Extension Date: </label>
                                        <asp:TextBox ID="ExtendedDateTextBox" AutoCompleteType="Disabled" runat="server" AutoPostBack="True" OnTextChanged="ExtendedDateTextBox_TextChanged" CausesValidation="true" class="form-control form-control-sm"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="Calendar1" runat="server"
                                            TargetControlID="ExtendedDateTextBox"
                                            Format="dd MMM yyyy" CssClass="MyCalendar" PopupPosition="TopLeft" />
                                        <style>
                                                      
                                                        </style>
                                    </div>
                                </div>
                          

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Description: </label>
                                        <asp:TextBox ID="DescriptionTextBox" runat="server" TextMode="MultiLine" CausesValidation="true" class="form-control"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Remarks: </label>
                                        <asp:TextBox ID="RemarksTextBox" runat="server" CausesValidation="true" class="form-control form-control-sm"></asp:TextBox>

                                    </div>
                                </div>



                            </div>

                            <asp:GridView ID="gv_allocateEmp" Width="100%" CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="false" runat="server">
                                <Columns>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="txt_checkAll" OnCheckedChanged="txt_checkAll_OnCheckedChanged" AutoPostBack="True" runat="server"></asp:CheckBox>
                                            <asp:Label ID="txt_selectAll" runat="server"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                            <asp:CheckBox ID="txt_check" runat="server"></asp:CheckBox>
                                        </ItemTemplate>


                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            <asp:HiddenField runat="server" ID="HiddenFieldDetailsID" Value='<%#Eval("DeadlineExtensionRequestDetailsId") %>' />
                                            <asp:HiddenField runat="server" ID="txt_empInfoId" Value='<%#Eval("EmpInfoId") %>' />
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ID">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_empId" runat="server" class="form-control form-control-sm" Text='<%#Eval("EmpMasterCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_name" runat="server" class="form-control form-control-sm" Text='<%#Eval("EmpName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_designation" runat="server" class="form-control form-control-sm" Text='<%#Eval("Designation") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Department">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_dptName" runat="server" class="form-control form-control-sm" Text='<%#Eval("DepartmentName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Division" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_division" runat="server" class="form-control form-control-sm" Text='<%#Eval("DivisionName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--                                    <asp:TemplateField HeaderText="Extension Date">
                                        <ItemTemplate>
                                            <asp:TextBox ID="ExtensionDate" AutoCompleteType="Disabled" runat="server" class="form-control" Width="100%" Height="29px" Text='<%# Eval("ExtensionDate")%>' OnTextChanged="deliveryDateTextBox_TextChanged" AutoPostBack="True"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                TargetControlID="txt_Extended" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <%-- <asp:TemplateField HeaderText="Total Employee">
                                    <ItemTemplate>
                                        <asp:Label ID="TotalEmployee" runat="server" class="form-control form-control-sm" Text='<%#Eval("TotalEmployee") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Extension Date">
                                        <ItemTemplate>
                                            <asp:Label ID="ExtendedDate" runat="server"  DataFormatString="{0:dd-MMM-yyyy}" class="form-control form-control-sm" Text='<%#Eval("ExtendedDate") %>'></asp:Label>


                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <%-- <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txt_Remarks" runat="server" class="form-control form-control-sm" Text='<%#Eval("Remarks") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                </Columns>
                            </asp:GridView>

                            <div class="form-row">
                            </div>

                            <div class="form-row">

                                <div class="form-group">
                                    <asp:HiddenField runat="server" ID="hid_KpiMasrerId" />
                                    <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" Text="Approve" CssClass="btn btn-sm btn-info" />
                                    <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                    <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />
                                    <asp:Button ID="cancelButton" Text="Cancel" Visible="False" OnClick="cancelButton_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                                </div>
                                <br />
                                <br />

                            </div>

                            <br />
                            <br />
                            <br />
                            <br />
                        </div>
                    </div>

                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>


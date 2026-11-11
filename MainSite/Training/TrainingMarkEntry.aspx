<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="TrainingMarkEntry.aspx.cs" Inherits="Training_TrainingMarkEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>

                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;">Training Marks Entry</h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                            <%--<asp:Button ID="detailsViewButton" OnClick="detailsViewButton_Click" Text="Back to List " CssClass="btn btn-sm btn-outline-secondary " runat="server"  />--%>
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-body">
                            <div class="form-row">
                                 <div class="col-2">
                                    <div class="form-group">
                                        <label>Company</label>
                                        <asp:DropDownList runat="server" ID="ddlCompany" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" CssClass="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                        <asp:DropDownList ID="ddlFinancialYear" OnSelectedIndexChanged="ddlFinancialYear_OnSelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Training Title</label>
                                        <asp:DropDownList runat="server" OnSelectedIndexChanged="ddlTrainingRecord_OnSelectedIndexChanged" ID="ddlTrainingRecord" AutoPostBack="true"  CssClass="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                               
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Out Of</label>
                                        <asp:TextBox runat="server" ID="outMarksTextBox" TextMode="Number"  CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-2">
                                    <div class="form-group">
                                        <label>Remarks</label>
                                        <asp:TextBox runat="server" ID="txtRemarks"   CssClass="form-control form-control-sm"></asp:TextBox>
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
                                        <label>Total Yearly Budgeted Cost</label>
                                        <asp:TextBox runat="server" ID="txtToalYearlyBudget" ReadOnly="True"  AutoPostBack="true" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-3" runat="server" visible="False">
                                    <div class="form-group">
                                        <label>Total  Budget </label>
                                        <asp:TextBox runat="server" ID="txt_toalBudget" AutoPostBack="true" TextMode="Number" CssClass="form-control form-control-sm" Text="0"></asp:TextBox>
                                    </div>
                                </div>--%>
                            </div>
                                <asp:GridView ShowFooter="true" ID="gv_AllEmployee" Width="100%"  CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="false" DataKeyNames="TrainingRecordDetailsEmp,EmpInfoId" runat="server">
                                    <Columns>
                                         <asp:TemplateField >
                                            <HeaderTemplate>
                                                <asp:CheckBox runat="server" AutoPostBack="True" ID="chkAll" Text=""  OnCheckedChanged="chkAll_OnCheckedChanged"/>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chkSingle" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    <%--      <asp:TemplateField HeaderText="Is Present">
                                                <ItemTemplate>
                                                         <asp:CheckBox ID="lb_empCheck"   runat="server"></asp:CheckBox>
                                                        <asp:HiddenField runat="server" ID="allocationDetailsId" Value='<%#Eval("TrainingRecordMasterId") %>'/>
                                                        <asp:HiddenField runat="server" ID="EmpInfoId" Value='<%#Eval("EmpInfoId") %>'/>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                         
                                            <asp:TemplateField HeaderText="ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_employeeId" runat="server" class="form-control form-control-sm" Text='<%#Eval("EmpMasterCode") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_employeeName" runat="server" class="form-control form-control-sm" Text='<%#Eval("EmpName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_employeeDesignation" runat="server" class="form-control form-control-sm" Text='<%#Eval("Designation") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_employeeDepartment" runat="server" class="form-control form-control-sm" Text='<%#Eval("DepartmentName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Pre">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_Pre" runat="server" class="form-control form-control-sm" ></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Post">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_Post" runat="server" class="form-control form-control-sm" ></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
<%--                                         <asp:TemplateField HeaderText="Grade">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_employeeGrade" runat="server" class="form-control form-control-sm" Text='<%#Eval("GradeName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                           <%--  <asp:TemplateField HeaderText="Quater">
                                                <ItemTemplate>
                                                     
                                                    <asp:Label  ID="txt_Quater" runat="server" class="form-control form-control-sm" Text='<%#Eval("Quater") %>'></asp:Label>
                                                    
                                                     
                                                     </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        
                                        
                                           
                                    </Columns>
                                </asp:GridView>
                            
                                    
                            <asp:HiddenField runat="server" ID="hdpk"/> 
                                  <%--  <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_Click" Text="Submit " CssClass="btn btn-sm btn-info"/>--%>
                                 <asp:Button runat="server" ID="Button1"  Text="Submit " CssClass="btn btn-sm btn-info" OnClick="Button1_OnClick"/>
                                 <%--    <asp:Button ID="cancelButton" Text="Cancel" OnClick="cancelButton_Click" CssClass="btn btn-sm warning" runat="server"  BackColor="#FFCC00" />--%>
                        </div>
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>


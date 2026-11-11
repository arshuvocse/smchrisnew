<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="BudgetWiseAllocation.aspx.cs" Inherits="Training_BudgetWiseAllocation" %>

<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">

    <style type="text/css">
        /*AutoComplete flyout */
       
    </style>


    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>
                <div class="container-fluid">
                    <div class="page-heading">
                       <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"> Budget Wise Allocation</h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="detailsViewButton" Text="Allocation List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_Click" />
                        </div>

                    </div>
                    <div class="card">
                        <div class="card-body">
                            <div class="form-row">
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Company</label>
                                        <asp:DropDownList runat="server" ID="ddlCompany" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" CssClass="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                        <asp:DropDownList ID="ddlFinancialYear" AutoPostBack="true" OnSelectedIndexChanged="ddlFinancialYear_SelectedIndexChanged" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Training</label>
                                        <asp:DropDownList ID="ddlTraining" AutoPostBack="true" OnSelectedIndexChanged="ddlTraining_SelectedIndexChanged" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>




                            <div class="form-row">
                                <div class="col-md-12">
                                    <asp:GridView ID="gv_DptDetails" Width="100%" CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="false" runat="server">

                                        <Columns>
                                            <asp:TemplateField HeaderText="SL#">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                    <%--<asp:HiddenField runat="server" ID="hdpkd" Value='<%#Eval("BoardDetailsId") %>'/>--%>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quater">
                                                <ItemTemplate>
                                                     <asp:Label ID="txt_quaterId" Visible="False" runat="server" class="form-control form-control-sm" Text='<%#Eval("quaterId") %>'></asp:Label>
                                                    <asp:Label ID="txt_Quater" runat="server" class="form-control form-control-sm" Text='<%#Eval("quater") %>'></asp:Label>
                                                    <asp:HiddenField runat="server" ID="gv_budgetDetailsID" Value='<%#Eval("TrainingBudgetDetailsDptId") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Month">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_Month" runat="server" class="form-control form-control-sm" Text='<%#Eval("month") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Department">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_department" runat="server" class="form-control form-control-sm" Text='<%#Eval("department") %>'></asp:Label>
                                                    <asp:HiddenField runat="server" ID="deptId" Value='<%#Eval("departmentId") %>' />
                                                    <asp:HiddenField runat="server" ID="finYear" Value='<%#Eval("finYear") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="From Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_fromDate" runat="server" class="form-control form-control-sm" Text='<%#Eval("fromDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="To Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_toDate" runat="server" class="form-control form-control-sm" Text='<%#Eval("toDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Quantity">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_Qty" runat="server" class="form-control form-control-sm" Text='<%#Eval("quantity") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Operation">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lb_Allocate" OnClick="lb_Allocate_Click" runat="server">Allocate</asp:LinkButton>
                                                </ItemTemplate>


                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </div>

                            <div class="form-row">
                                <div class="col-md-12">
                                    <asp:GridView ID="gv_GrdDetails" Width="100%" CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="false" runat="server">

                                        <Columns>
                                            <asp:TemplateField HeaderText="SL#">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                    <%--<asp:HiddenField runat="server" ID="hdpkd" Value='<%#Eval("BoardDetailsId") %>'/>--%>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quater">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_Quater" runat="server" class="form-control form-control-sm" Text='<%#Eval("quater") %>'></asp:Label>
                                                     <asp:Label ID="txt_quaterId" Visible="False" runat="server" class="form-control form-control-sm" Text='<%#Eval("quaterId") %>'></asp:Label>
                                                    <asp:HiddenField runat="server" ID="gv_budgetDetailsID" Value='<%#Eval("TrainingBudgetDetailsGradeId") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Month">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_Month" runat="server" class="form-control form-control-sm" Text='<%#Eval("month") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Grade">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_department" runat="server" class="form-control form-control-sm" Text='<%#Eval("grade") %>'></asp:Label>
                                                    <asp:HiddenField runat="server" ID="gradeId" Value='<%#Eval("gradeId") %>' />
                                                    <asp:HiddenField runat="server" ID="finYear" Value='<%#Eval("finYear") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="From Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_fromDate" runat="server" class="form-control form-control-sm" Text='<%#Eval("fromDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="To Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_toDate" runat="server" class="form-control form-control-sm" Text='<%#Eval("toDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Quantity">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_Qty" runat="server" class="form-control form-control-sm" Text='<%#Eval("quantity") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Operation">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="grd_alloate" OnClick="grd_alloate_Click1" runat="server">Allocate</asp:LinkButton>
                                                </ItemTemplate>


                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </div>
                             <div class="form-row">
                                <div class="col-md-12">
                                    <asp:GridView ID="gv_BudEmp" Width="100%" CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="false" runat="server">

                                        <Columns>
                                            <asp:TemplateField HeaderText="SL#">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                    <%--<asp:HiddenField runat="server" ID="hdpkd" Value='<%#Eval("BoardDetailsId") %>'/>--%>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quater">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_Quater" runat="server" class="form-control form-control-sm" Text='<%#Eval("quater") %>'></asp:Label>
                                                     <asp:Label ID="txt_quaterId" Visible="False" runat="server" class="form-control form-control-sm" Text='<%#Eval("quaterId") %>'></asp:Label>
                                                    <asp:HiddenField runat="server" ID="trainingBudgetMasterId" Value='<%#Eval("TrainingBudgetMasterId") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quantity">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_Qty" runat="server" class="form-control form-control-sm" Text='<%#Eval("EmpCount") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            

                                         
                                            <asp:TemplateField HeaderText="Operation">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="emp_alloate" OnClick="emp_alloate_Click" runat="server">Allocate</asp:LinkButton>
                                                </ItemTemplate>


                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </div>
                            <div class="form-row" runat="server" id="dptAllocation" visible="false">
                                <div class="col-6">
                                    <div class="form-row">
                                        <div class="col-6">
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="lbl_DptName" runat="server"></asp:Label></label>
                                            </div>
                                        </div>
                                        <div class="col-6">
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="lbl_maxQty" Visible="false" runat="server"></asp:Label></label>
                                                <asp:Label runat="server" ID="maxQtyValue" ></asp:Label>
                                                <asp:HiddenField runat="server" ID="quater" />
                                                <asp:HiddenField runat="server" ID="quaterText" />
                                                <asp:HiddenField runat="server" ID="forSector" />
                                                <asp:HiddenField runat="server" ID="budgetDetailsID" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-row">
                                        <div class="col-12">
                                            <asp:GridView ID="gv_EmpDetails" Width="100%" CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="false" runat="server">

                                        <Columns>
                                            <asp:TemplateField HeaderText="SL#">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Operation">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="lb_empCheck"  runat="server"></asp:CheckBox>
                                                </ItemTemplate>


                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_empCode" runat="server" class="form-control form-control-sm" Text='<%#Eval("EmpMasterCode") %>'></asp:Label>
                                                    <asp:HiddenField runat="server" ID="empInfoId" Value='<%#Eval("EmpInfoId") %>' />
                                                </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Name">


                                                <ItemTemplate>
                                                    <asp:Label ID="txt_employee" runat="server" class="form-control form-control-sm" Text='<%#Eval("EmpName") %>'></asp:Label>

                                                   

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
                                            <asp:TemplateField HeaderText="Grade">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_grdName" runat="server" class="form-control form-control-sm" Text='<%#Eval("GradeName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                          

                                        </Columns>
                                    </asp:GridView>

                                            <asp:GridView ID="gv_EmpDetails2" Width="100%" CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="false" runat="server">

                                        <Columns>
                                            <asp:TemplateField HeaderText="SL#">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Operation">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="lb_empCheck"  runat="server"></asp:CheckBox>
                                                </ItemTemplate>


                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_empCode" runat="server" class="form-control form-control-sm" Text='<%#Eval("EmpMasterCode") %>'></asp:Label>
                                                    <asp:HiddenField runat="server" ID="detailsId" Value='<%#Eval("DetailsId") %>' />
                                                    <asp:HiddenField runat="server" ID="empInfoId" Value='<%#Eval("EmpInfoId") %>' />
                                                </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Name">


                                                <ItemTemplate>
                                                    <asp:Label ID="txt_employee" runat="server" class="form-control form-control-sm" Text='<%#Eval("EmpName") %>'></asp:Label>

                                                   

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
                                            <asp:TemplateField HeaderText="Grade">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_grdName" runat="server" class="form-control form-control-sm" Text='<%#Eval("GradeName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                          

                                        </Columns>
                                    </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Button ID="dpt_AddEmployee" OnClick="dpt_AddEmployee_Click" runat="server" Text="Add To List" CssClass="btn btn-success btn-sm" />
                                    </div>
                                </div>
                                <div class="col-6">

                                    <div class="form-row">
                                        <div class="col-12">
                                            <asp:GridView ID="gv_allocateEmp" Width="100%" CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="false" runat="server">

                                        <Columns>
                                            <asp:TemplateField HeaderText="SL#">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>

                                            </asp:TemplateField>


                                              <asp:TemplateField HeaderText="Operation">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_quaterId" Visible="False" runat="server" class="form-control form-control-sm" Text='<%#Eval("quaterId") %>'></asp:Label>
                                                    <asp:Label ID="txt_quater" runat="server" class="form-control form-control-sm" Text='<%#Eval("quater") %>'></asp:Label>
                                                </ItemTemplate>


                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_empCode" runat="server" class="form-control form-control-sm" Text='<%#Eval("EmpMasterCode") %>'></asp:Label>
                                                    <asp:HiddenField runat="server" ID="detailsID" Value='<%#Eval("DetailsId") %>' />
                                                    <asp:HiddenField runat="server" ID="empInfoId" Value='<%#Eval("EmpInfoId") %>' />
                                                </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Name">


                                                <ItemTemplate>
                                                    <asp:Label ID="txt_employee" runat="server" class="form-control form-control-sm" Text='<%#Eval("EmpName") %>'></asp:Label>

                                                   

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
                                            <asp:TemplateField HeaderText="Grade">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_grdName" runat="server" class="form-control form-control-sm" Text='<%#Eval("GradeName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Remove">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="removeEmp" runat="server" OnClick="removeEmp_Click" >Remove</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                          

                                        </Columns>
                                    </asp:GridView>
                                        </div>
                                    </div>
                                </div>


                                
                               
                            </div>

                           

                          <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_Click" CssClass="btn btn-sm btn-info" Text="Submit"></asp:Button>
                            
                                     <asp:Button ID="cancelButton" Text="Cancel" OnClick="cancelButton_Click" CssClass="btn btn-sm warning" runat="server"  BackColor="#FFCC00" />

                        </div>
                         
                    </div>
                   

                </div>
                <asp:HiddenField runat="server" ID="hdpk" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>




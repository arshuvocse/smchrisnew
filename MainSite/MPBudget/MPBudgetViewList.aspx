<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MainMasterPage.master" CodeFile="MPBudgetViewList.aspx.cs" Inherits="MPBudget_MPBudgetViewList" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .form-group.required .control-label:after {
            color: #d00;
            content: "*";
            position: absolute;
            margin-left: 4px;
            top: 4px;
            font-size: large;
        }
    </style>
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <style>
        fieldset.for-panel {
            background-color: #fcfcfc;
            border: 1px solid #999;
            padding: 15px 10px;
            background-color: white;
            margin-bottom: 12px;
        }

            fieldset.for-panel legend {
                background-color: #fafafa;
                border: 1px solid #ddd;
                border-radius: 1px;
                font-size: 12px;
                font-weight: bold;
                line-height: 10px;
                margin: inherit;
                padding: 7px;
                width: auto;
                margin-bottom: 0;
                color: black;
            }
    </style>
    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>

                <div class="page-heading">
                    <div class="page-heading__container">
                        <h1 class="title" style="font-size: 18px; padding-top: 0px;">Manpower Budget </h1>
                    </div>

                    <%--<div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="detailsViewButton" Text="List Information " CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                        </div>--%>

                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                        <asp:Button ID="detailsViewButton" Visible="True" Text="Go Back" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>

                </div>
                <div class="container-fluid">
                    <div class="card">
                        <%-- <div class="page-heading">--%>
                        <div class="card-body">
                            <div class="form-row">
                                <div class="col-2">
                                    <div class="form-group required">
                                        <label class="control-label">Company</label>
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlCompany" Enabled="False" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" CssClass="form-control form-control-sm" />
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="form-group required">
                                        <label class="control-label">Department</label>
                                        <asp:DropDownList  Enabled="False"  runat="server" AutoPostBack="True" ID="ddlDepartment" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlDepartment_OnSelectedIndexChanged" />
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group required">
                                        <label class="control-label">Financial Year</label>
                                        <asp:DropDownList runat="server"  Enabled="False"   ID="ddlFinYear" CssClass="form-control form-control-sm" />
                                    </div>
                                </div>
                                <div class="col-2" runat="server" Visible="False">
                                    <div class="form-group required">
                                        <label class="control-label">Employee Type</label>
                                        <asp:RadioButtonList runat="server" AutoPostBack="True" ID="radEmpType" RepeatDirection="Horizontal" OnSelectedIndexChanged="radEmpType_OnSelectedIndexChanged" />
                                    </div>
                                </div>

                                <div class="col-2" runat="server" Visible="False">
                                    <div class="form-group">
                                        <label class="control-label">Project Name</label>
                                        <asp:DropDownList runat="server" ID="ddlProjectName" CssClass="form-control form-control-sm" Enabled="False" />
                                    </div>
                                </div>

                            </div>

                            <fieldset class="for-panel" runat="server" Visible="False">
                                <legend>Existing Employee </legend>
                                <div class="form-row">
                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>Employee Category</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlEmpCategoryEx" class="form-control form-control-sm" OnSelectedIndexChanged="ddlEmpCategoryEx_OnSelectedIndexChanged" />
                                        </div>
                                    </div>
                                    <div class="col-2">
                                        <div class="form-group required">
                                            <label class="control-label">Salary Grade</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlGradeEx" class="form-control form-control-sm" OnSelectedIndexChanged="ddlGradeEx_OnSelectedIndexChanged" />
                                        </div>
                                    </div>
                                    <%--<div class="col-2">
                                    <div class="form-group">
                                        <label>Designation</label>
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDesignationEx" class="form-control form-control-sm" OnSelectedIndexChanged="ddlDesignationEx_OnSelectedIndexChanged" />
                                    </div>
                                </div>--%>



                                    <%--                                <div class="col-2 ">
                                    <div class="form-group">
                                        <label>Step's Total Employee</label>
                                        <asp:Label runat="server" ID="lblExStepTotalEmp"  readonly="true" class="form-control form-control-sm" />
                                    </div>
                                </div>--%>
                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>Grade's Total Employee</label>
                                            <asp:Label runat="server" ID="lblExGradeTotalEmp" readonly="true" class="form-control form-control-sm" />
                                        </div>
                                    </div>
                                    <%--                                <div class="col-3 ">
                                    <div class="form-group">
                                        <label>Grade's Existing Total Salary</label>
                                        <asp:Label runat="server" ID="lblExGradeTotalSal" readonly="true" class="form-control form-control-sm" />
                                    </div>
                                </div>--%>

                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>Existing Salary(Min)</label>
                                            <asp:Label runat="server" ID="lblExGradeMinSal" readonly="true" class="form-control form-control-sm" />
                                        </div>
                                    </div>
                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>Existing Salary(Max)</label>
                                            <asp:Label runat="server" ID="lblExGradeMaxSal" readonly="true" class="form-control form-control-sm" />
                                        </div>
                                    </div>

                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>Step</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlStepEx" class="form-control form-control-sm" OnSelectedIndexChanged="ddlStepEx_OnSelectedIndexChanged" />
                                        </div>
                                    </div>
                                </div>
                            </fieldset>

                            <%--<fieldset class="for-panel">
                                                <legend>For New Designation</legend>
                            <div class="form-row">
                                
                                <div class="col-1">
                                    <div class="form-group">
                                        <label>New Designation</label>
                                        <asp:CheckBox runat="server" AutoPostBack="True" ID="chk_NewDesignation" OnCheckedChanged="chk_NewDesignation_OnCheckedChanged" />
                                    </div>
                                </div>
                                 <div class="col-3">
                                    <div class="form-group">
                                        <label>Employee Category</label>
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlEmpCategoryNew" class="form-control form-control-sm" OnSelectedIndexChanged="ddlEmpCategoryNew_OnSelectedIndexChanged" />
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Salary Grade</label>
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlGradeNew" class="form-control form-control-sm" OnSelectedIndexChanged="ddlGradeNew_OnSelectedIndexChanged" />
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label> Designation</label>
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDesignationNew" class="form-control form-control-sm" OnSelectedIndexChanged="ddlDesignationEx_OnSelectedIndexChanged" />
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group">
                                        <label> Step</label>
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlStepNew" class="form-control form-control-sm" OnSelectedIndexChanged="ddlStepEx_OnSelectedIndexChanged" />
                                    </div>
                                </div>
                            </div>
                                     </fieldset>--%>

                            <fieldset class="for-panel">
                                <legend>Requisition Details</legend>
                                <div class="form-row" runat="server" Visible="False">


                                    <div class="col-2 ">
                                        <div class="form-group required">
                                            <label class="control-label">Job Title</label>
                                            <asp:TextBox runat="server" ID="txt_Designation" class="form-control form-control-sm" />
                                        </div>
                                    </div>

                                    <div class="col-2 ">
                                        <div class="form-group required">
                                            <label class="control-label">No. Of Employee</label>
                                            <asp:TextBox runat="server" ID="txt_EmployeeRequisition" class="form-control form-control-sm" />
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderconvRate" runat="server"
                                                Enabled="True" TargetControlID="txt_EmployeeRequisition" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                        </div>
                                    </div>

                                    <div class="col-2 ">
                                        <div class="form-group required">
                                            <label class="control-label">Salary Range From</label>
                                            <asp:TextBox runat="server" ID="txt_ReqApproxSalary" AutoPostBack="True" OnTextChanged="txt_ReqApproxSalary_OnTextChanged" class="form-control form-control-sm" />
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                Enabled="True" TargetControlID="txt_ReqApproxSalary" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                        </div>
                                    </div>

                                    <div class="col-2 ">
                                        <div class="form-group required">
                                            <label class="control-label">Salary Range To</label>
                                            <asp:TextBox runat="server" ID="lbl_ReqTotalSalary" AutoPostBack="True" OnTextChanged="lbl_ReqTotalSalary_OnTextChanged" class="form-control form-control-sm" />
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                Enabled="True" TargetControlID="lbl_ReqTotalSalary" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                        </div>
                                    </div>

                                    <div class="col-2 ">
                                        <div class="form-group required">
                                            <label class="control-label">Quarter</label>
                                            <asp:DropDownList CssClass="form-control form-control-sm" runat="server" ID="ddlQuarter">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-2 ">
                                        <div class="form-group">
                                            <label>Job Summary</label>
                                            <asp:TextBox runat="server" ID="txtRemarks" class="form-control form-control-sm" />
                                        </div>
                                    </div>


                                </div>
                                <div class="form-row">
                                    <div class="col-2 "></div>
                                    <div class="col-2 "></div>
                                    <div class="col-2 "  runat="server" Visible="False">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button ID="btnAdd" Text="Add to list" OnClick="btnAdd_OnClick" CssClass="btn btn-outline-success btn-block disabled btn-sm" runat="server" />

                                        </div>
                                        <div class="col-2 "></div>
                                        <div class="col-2 "></div>
                                    </div>
                                </div>
                                <div>
                                    <asp:GridView Width="100%" ID="gv_MP" runat="server"  AutoGenerateColumns="False" CssClass="table table-bordered  text-center thead-dark">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL#">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                    <asp:HiddenField runat="server" ID="hdpkd" Value='<%#Eval("MPBudgetDetailsId") %>' />
                                                    <%--<asp:HiddenField runat="server" ID="hdFilterType" Value='<%#Eval("FilterType") %>' />--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesignation" runat="server" Text='<%#Eval("Designation") %>'></asp:Label>
                                                    <%--<asp:HiddenField runat="server" ID="hdDesignation" Value='<%#Eval("DesignationId") %>' />--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmpCategoryName" runat="server" Text='<%#Eval("EmpCategoryName") %>'></asp:Label>
                                                    <asp:HiddenField runat="server" ID="hdEmpCategoryId" Value='<%#Eval("EmpCategoryId") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Grade">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSalaryGrade" runat="server" Text='<%#Eval("GradeName") %>'></asp:Label>
                                                    <asp:HiddenField runat="server" ID="hdSalaryGrade" Value='<%#Eval("SalaryGradeId") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Requisition">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmployeeReq" runat="server" Text='<%#Eval("EmployeeRequisition") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Employee Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmployeeType" runat="server" Text='<%#Eval("EmployeeType") %>'></asp:Label>
                                                    <asp:HiddenField runat="server" ID="hdEmployeeType" Value='<%#Eval("EmployeeTypeId") %>' />
                                                    <br />
                                                    <span>( </span>
                                                    <asp:Label ID="lblProject" runat="server" Text='<%#Eval("Project") %>'></asp:Label>
                                                    <asp:HiddenField runat="server" ID="hdProject" Value='<%#Eval("ProjectId") %>' />
                                                    <span>)</span>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Salary Range From">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReqSalPerEmp" runat="server" Text='<%#Eval("ReqApproxSalary") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Salary Range To">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReqTotalSalary" runat="server" Text='<%#Eval("ReqTotalSalary") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quarter">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuarter" runat="server" Text='<%#Eval("QuarterName") %>'></asp:Label>
                                                    <asp:HiddenField runat="server" ID="hdQuarter" Value='<%#Eval("QuarterId") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%--                                        <asp:TemplateField HeaderText="Step">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSalaryStep" runat="server" Text='<%#Eval("SalaryStepName") %>'></asp:Label>
                                                <asp:HiddenField runat="server" ID="hdSalaryStep" Value='<%#Eval("SalaryStepId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Job Summary">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtDtlRemarks" ReadOnly="True"   runat="server" Text='<%#Eval("DtlRemarks") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Edit" runat="server" Visible="False">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lb_Edit" runat="server" OnClick="lb_Edit_Click">Edit</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remove"  runat="server" Visible="False">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lb_Remove" runat="server" OnClick="lb_Remove_Click">Remove</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </fieldset>

                            <br />
                            <br />
                            <div>
                                <asp:HiddenField runat="server" ID="hdpk" />
                                <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" Visible="False" Text="Save " CssClass="btn btn-sm btn-info" />
                                <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />
                                <asp:Button ID="cancelButton" Text="Cancel" Visible="False" OnClick="cancelButton_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                            </div>
                        </div>
                    </div>
                </div>




              
              <%--  <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0"
                                DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaiting">
                                        <asp:Image ID="imgWait" runat="server" ImageAlign="Middle" ImageUrl="../Assets/images/loading-icon-big.gif"
                                            Height="100%" Width="100%" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>--%>



            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

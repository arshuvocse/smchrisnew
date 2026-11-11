<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MainMasterPage.master" CodeFile="MPBudgetEntry.aspx.cs" Inherits="MPBudget_MPBudgetEntry" %>

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

         .SelectchkChoice label {
            padding-left: 6px;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cpFormBody" runat="Server">
    
    
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
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
        

                <div class="page-heading">
                    <div class="page-heading__container">
                        <h1 class="title" style="font-size: 18px; padding-top: 0px;"> <img src="../Report_Pages/app.png" width="20px"  />  Manpower Budget </h1>
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
                        <asp:UpdatePanel  runat="server">
            <ContentTemplate>
                        <%-- <div class="page-heading">--%>
                        <div class="card-body">
                            <div class="form-row">
                                <div class="col-2">
                                    <div class="form-group required">
                                        <label class="control-label">Company</label>
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" class="form-control form-control-sm" />
                                        
                                        
                                                <script type="text/javascript">
                                          function pageLoad() {
                                              $('.Selectme').chosen({ disable_search_threshold: 5, search_contains: true });

                         $('#<%=ddlDepartment.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });
                     }
               </script>
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group required">
                                        <label class="control-label">Department</label>
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDepartment" class="form-control form-control-sm Selectme" OnSelectedIndexChanged="ddlDepartment_OnSelectedIndexChanged" />
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group required">
                                        <label class="control-label">Financial Year</label>
                                        <asp:DropDownList runat="server" ID="ddlFinYear" class="form-control form-control-sm" />
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group  ">
                                        <label class="control-label">Employee Type</label>
                                        <asp:RadioButtonList runat="server" AutoPostBack="True" ID="radEmpType" RepeatDirection="Horizontal" OnSelectedIndexChanged="radEmpType_OnSelectedIndexChanged" />
                                    </div>
                                </div>

                                <div class="col-2">
                                    <div class="form-group">
                                        <label class="control-label">Project Name</label>
                                        <asp:DropDownList runat="server" ID="ddlProjectName" CssClass="form-control form-control-sm Selectme" Enabled="False" />
                                    </div>
                                </div>

                            </div>

                            <fieldset class="for-panel">
                                <legend>Existing Employee </legend>
                                <div class="form-row">
                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>Employee Category</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlEmpCategoryEx" class="form-control form-control-sm" OnSelectedIndexChanged="ddlEmpCategoryEx_OnSelectedIndexChanged" />
                                        </div>
                                    </div>
                                    <div class="col-2">
                                        <div class="form-group">
                                            <label class="control-label">Salary Grade</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlGradeEx" class="form-control form-control-sm Selectme" OnSelectedIndexChanged="ddlGradeEx_OnSelectedIndexChanged" />
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
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlStepEx" class="form-control form-control-sm Selectme" OnSelectedIndexChanged="ddlStepEx_OnSelectedIndexChanged" />
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
                                <legend>Requisition</legend>
                                <div class="form-row">

                                      <div class="col-2">
                                        <div class="form-group">
                                            <label>Employee Category</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="dlEmpCategoryEx2" class="form-control form-control-sm Selectme" OnSelectedIndexChanged="dlEmpCategoryEx2_OnSelectedIndexChanged" />
                                        </div>
                                    </div>
                                    <div class="col-2">
                                        <div class="form-group required">
                                            <label class="control-label">Salary Grade</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlGradeEx2" class="form-control form-control-sm Selectme" OnSelectedIndexChanged="ddlGradeEx2_OnSelectedIndexChanged" />
                                        </div>
                                    </div>
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
                                    <div class="col-10 ">
                                        <div class="form-group">
                                            <label>Job Summary</label>
                                            <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="3" class="form-control" />
                                        </div>
                                    </div>


                                </div>
                                <div class="form-row">
                                    <div class="col-2 "></div>
                                    <div class="col-2 "></div>
                                    <div class="col-2 ">
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
                                                   
                                                    
                                                    <asp:Label ID="lblProject" runat="server" Text='<%#Eval("Project") %>'></asp:Label>
                                                    <asp:HiddenField runat="server" ID="hdProject" Value='<%#Eval("ProjectId") %>' />
                                                    
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
                                                    <asp:TextBox ID="txtDtlRemarks" ReadOnly="True"  CssClass="form-control" TextMode="MultiLine" runat="server" Text='<%#Eval("DtlRemarks") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lb_Edit" runat="server"  class="btn btn-danger btn-sm" OnClick="lb_Edit_Click"><i class="fa fa-pencil" aria-hidden="true"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remove">
                                                <ItemTemplate>
                                                    <asp:LinkButton    class="btn btn-success btn-sm "   ID="lb_Remove" runat="server" OnClick="lb_Remove_Click"><i class="fa fa-trash" aria-hidden="true"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </fieldset>
                            <br />
                              <div class="form-row">
                                <div class="col-12 ">
                                    <asp:CheckBox ID="manualUpdateCheckBox" CssClass="SelectchkChoice" Text=" Manual Approve" Checked="True" runat="server" />  
                                </div>
                            </div>
                            <br />

                             <div class="row">
                                  <div class="col-md-6" runat="server">
                                        <div class="form-group">
                                            <label class="font-weight-bold">Comments &nbsp;</label><label style="font-size: 10px; color: gray; font-style: italic"> (*Applicable for Final Submit)</label>
                                            
                                         
                                            
                                            <asp:TextBox runat="server"   ID="txtComment" TextMode="MultiLine" Rows="2" class="form-control" />
                                        </div>
                                    </div>
                                 
                                  <div class="col-md-6" runat="server" Visible="False" id="DivShow">
                                      
                                                 <div style="max-height:180px; overflow: scroll">
                                                      <div class="form-group">
                                            <label class="font-weight-bold">Approval Status &nbsp;</label>
                                                          </div>
                                                <asp:GridView ID="AppLogCommentGridView" CssClass="table table-bordered text-center thead-dark" runat="server" Width="100%" AutoGenerateColumns="False">
                                                    <Columns>
                                                      
                                                        <asp:BoundField DataField="PreEmp" HeaderText="Initiator" HtmlEncode="False" />
                                                        <asp:BoundField DataField="ForEmp" HeaderText="Approval Person" HtmlEncode="False" />
                                                        <%--<asp:BoundField DataField="Version" HeaderText="Version" HtmlEncode="False" />--%>
                                                        <asp:BoundField DataField="Comments" HeaderText="Comments" HtmlEncode="False" />
                                                        <asp:BoundField DataField="ActionStatus" HeaderText="Action Status" HtmlEncode="False" />
                                                        

                                                        <asp:BoundField DataField="ApproveBy" HeaderText="Approved By" HtmlEncode="False" />
                                                        <asp:BoundField DataField="ApproveDate" HeaderText="Approved Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                     
                                                       
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                             </div>

                            <br />
                            <br />
                            <div class="row">
                                <asp:HiddenField runat="server" ID="hdpk" />
                                   <div class="col-md-6">
                                   <link href="../UserSetup/ButtonGrups.css" rel="stylesheet" />
                                                <div class="ui-group-buttons">
                                                    
                                <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" Visible="False" Text="Save " CssClass="btn btn-sm btn-info" />
                                                      <div class="or or-sm" runat="server" id="SavSub" Visible="False"></div>
                                                       <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_OnClick" Visible="False"  Text="   Submit   " OnClientClick="return confirm('Are you sure you want to Submit ?')" CssClass="btn btn-sm btn-success" />
                                                    </div>
                                <asp:HiddenField runat="server" ID="WhichButton" Value="0"/>
                                 <div class="ui-group-buttons">
                                <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                     <div class="or or-sm" runat="server" id="UpSub" Visible="False"></div>
                                     <asp:Button runat="server" ID="btnUpSub" OnClick="btnUpSub_OnClick" Visible="False"  Text="   Submit   " OnClientClick="return confirm('Are you sure you want to Submit ?')" CssClass="btn btn-sm btn-success" />
                                      </div>
                                <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />
                                <asp:Button ID="cancelButton" Text="Cancel" Visible="False" OnClick="cancelButton_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                                </div>
                                 
                            </div>
                        </div>
                
                 </ContentTemplate>
        </asp:UpdatePanel>
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



           
    </div>
</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MainMasterPage.master" CodeFile="MPBudgetApproveView.aspx.cs" Inherits="MPBudget_MPBudgetEntry" %>

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
                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            
                            <div class="pull-left">
                                <div class="icon"> <img src="../Assets/2076-512.png" height="28px" /></div>
                                <span>Approval Request</span>
                                <h1 class="title" style="font-size: 18px; padding-top: 0px;">Manpower Budget Approval</h1>
                            </div>




                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">

                        <asp:HyperLink ID="BacktoList" Text="<<< Back to List" OnClick="BacktoList_OnClick" style="box-shadow: 0 0 3px 1px rgba(0,0,0,.35);" NavigateUrl="MPBudgetListApproval.aspx" CssClass="btn btn-sm text-info" runat="server"   />


                            <style>
                                

                                
.title-widget {
	color: #898989;
	font-size: 20px;
	font-weight: 300;
	line-height: 1;
	position: relative;
	text-transform: uppercase;
	font-family: 'Fjalla One', sans-serif;
	margin-top: 0;
	margin-right: 0;
	margin-bottom: 25px;
	 
	padding-left: 12px;

}


.title-widget::before {
    background-color: #ea5644;
    content: "";
    height: 22px;
    left: 0px;
    position: absolute;
    top: -2px;
    width: 5px;
}

                                 .radiostyle {
                                     height: auto;
                                 }

                                .radiostyle label {
                                    margin-left: 3px !important;
                                    margin-right: 10px !important;
                                }
                            </style>

                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                        <%--<asp:Button ID="ViewListButton" Text="View List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="ViewListButton_OnClick" />--%>
                        <%--<asp:Button ID="reloadButton" Text="Reload" CssClass="btn btn-sm btn-outline-success" runat="server" OnClick="reloadButton_OnClick" />--%>
                    </div>

                    <div class="page-heading__container float-right d-none d-sm-block">
             
                    </div>
                        <%--     <asp:Button ID="detailsViewButton" Text="List Information " CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />--%>
                    </div>
                    <div class="page-heading">

                        <div class="row">
                            <div class="col-md-12">
                                <div class="card-body">
                                    
                                    <div class="card"  style="padding: 14px; " >
                                           <div class="row" style="padding-top:20px;">
                                               
                                                   <div class="col-md-4">
                                                       </div>
                                          
                                                <div class="col-md-4">
                                              
                                        <div class="form-group">
                                Approval Status &nbsp; <span style="color: #a52a2a">*</span> &nbsp;
                               
                               
                                    <asp:RadioButtonList ID="actionRadioButtonList" CssClass="radiostyle" RepeatLayout="Flow" runat="server" RepeatDirection="Horizontal">
                                        
                                    </asp:RadioButtonList>
                               
                                &nbsp; </div>
                                             
                                    
                                        
                                        <div class="form-group">
                                            <label class="font-weight-bold">Comments</label>
                                            
                                         
                                            
                                            <asp:TextBox runat="server"   ID="txtComment" TextMode="MultiLine" Rows="2" class="form-control" />
                                        </div>
                                    </div>
                                           
                                            
                                         
                                            
                                            
                                          </div>
                                        
                                        <div class="row"  >
                                            
                                                   <div class="col-md-4">
                                                       </div>
                                      <div class="col-md-4">
                                             <link href="../UserSetup/ButtonGrups.css" rel="stylesheet" />
                                      <div class="ui-group-buttons">
                                                   <asp:LinkButton runat="server" OnClientClick="return confirm('Are you sure you want to Submit ?')" style="box-shadow: 0 0 3px 1px rgba(0,0,0,.35);" ID="btn_Save" OnClick="btn_Save_OnClick"  CssClass="btn btn-sm btn-success"><i class="fa fa-check" aria-hidden="true"></i>

&nbsp; Submit </asp:LinkButton> 
                                         
                                           <asp:LinkButton ID="Button1"   OnClientClick="return confirm('Are you sure you want to Submit ?')" style="box-shadow: 0 0 3px 1px rgba(0,0,0,.35);" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="Button1_OnClick"><i class="fa fa-check" aria-hidden="true"></i> &nbsp; Submit

&nbsp; </asp:LinkButton>  
                                          
                                             <div class="or or-sm" runat="server"   id="orBTN"></div>
                                          <asp:LinkButton ID="cancelButton"  OnClientClick="return confirm('Are you sure you want to Reject ?')" style="box-shadow: 0 0 3px 1px rgba(0,0,0,.35);"  OnClick="Button2a_OnClick" CssClass="btn btn-sm btn-danger " Visible="True" runat="server" ><i class="fa fa-times" aria-hidden="true"></i>
&nbsp; Reject </asp:LinkButton> 
                                             </div>
                                   
                                
                                    <asp:HiddenField ID="entryempinfoIdHiddenField" runat="server" />
                                    <asp:HiddenField ID="actionstatusHiddenField" runat="server" />
                                      </div>
                                </div>
                                    </div>
                                    <div class="card" style="padding: 14px; " >
                                        
                                         
                                <div class="row">
                                   <div class="col-md-12">
                                       
                                         <div class="row">
                                          <div class="col-md-2">
                                                  <label class="font-weight-bold"> Company Name:</label>
                                          </div>
                                             
                                             <div class="col-md-5">
                                                   <label class="font-weight-bold"> Department:</label>
                                                 </div>
                                               <div class="col-md-2">
                                                    <label class="font-weight-bold"> Financial Year:</label>
                                                 </div>
                                             
                                               <div class="col-md-3">
                                                  <label class="font-weight-bold"> Project Name:</label>
                                          </div>
                                        
                                          <div class="col-md-2"  runat="server" Visible="False">
                                                    <label class="font-weight-bold"> Employee Type:</label>
                                                 </div>
                                        </div>
                                          
                                        
                                      
                                 <div class="row">
                                     <div class="col-md-2">
                                          <hr/>
                                       <asp:Label runat="server" ID="lblCompany"></asp:Label>
                                         </div>
                                       <div class="col-md-5">
                                            <hr/>
                                            <asp:Label runat="server" ID="lblDept"></asp:Label>
                                                 </div>
                                       <div class="col-md-2">
                                            <hr/>
                                           <asp:Label runat="server" ID="lblFinancialYear"></asp:Label>
                                                 </div>
                                     
                                      
                                      <div class="col-md-3">
                                           <hr/>
                                       <asp:Label runat="server" ID="lblProjectName"></asp:Label>
                                         </div>
                                            
                                              <div class="col-md-2" runat="server" Visible="False">
                                            <hr/>
                                             <asp:Label runat="server" ID="lblEmpType"></asp:Label>
                                                 </div>
                                     
                                 </div>
                                          </div>
                                       
                                         <br/>
                                    <div class="row">
                                        
                                        <div class="row">
                                     <div class="col-md-3">
                                        
                                            </div>
                                   
                                       </div>
                                      <%--  <label class="font-weight-bold"> Department:</label>
                                   <asp:Label runat="server" ID="Label3"></asp:Label>
                                       
                                        <label class="font-weight-bold"> Department:</label>
                                   <asp:Label runat="server" ID="Label4"></asp:Label>
                                       
                                        <label class="font-weight-bold"> Department:</label>
                                   <asp:Label runat="server" ID="Label5"></asp:Label>--%>
                                   </div>
                                    </div>
                                         
                                       
                                        
                                       
                                </div>  <br/>
                                        
                                 <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;"> Manpower Budget Information</h2>
                            <hr/>
                                      <asp:GridView Width="100%" ID="gv_MP" runat="server"  AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">
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
                                        <%--<asp:TemplateField HeaderText="New Designation">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="newDesigDropDownList" CssClass="form-control" runat="server"></asp:DropDownList>--%>
                                                <%--<asp:HiddenField runat="server" ID="hdDesignation" Value='<%#Eval("DesignationId") %>' />--%>
                                            <%--</ItemTemplate>
                                        </asp:TemplateField>--%>
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
                                                <asp:Label ID="txtDtlRemarks"  runat="server" Text='<%#Eval("DtlRemarks") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <%-- <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lb_Edit" runat="server" OnClick="lb_Edit_Click">Edit</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remove">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lb_Remove" runat="server" OnClick="lb_Remove_Click">Remove</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                                    <br/>
                                       <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;"> Approval Status List</h2>
                            <hr/>
                               <div class="col-md-12">
                                                 <div style="max-height: 400px; overflow: scroll">
                                                <asp:GridView ID="AppLogCommentGridView" CssClass="table table-bordered text-center thead-dark" runat="server" Width="100%" AutoGenerateColumns="False">
                                                    <Columns>
                                                      
                                                             <asp:BoundField DataField="PreEmp" HeaderText="Approval Person" HtmlEncode="False" />
                                                      <asp:BoundField DataField="Comments" HeaderText="Comments" HtmlEncode="False" />
                                                    <asp:BoundField DataField="ForEmp" HeaderText="Approval Person" HtmlEncode="False" />
                                                    <%--<asp:BoundField DataField="Version" HeaderText="Version" HtmlEncode="False" />--%>
                                                  
                                                <%--    <asp:BoundField DataField="ActionStatus" HeaderText="Action Status" HtmlEncode="False" />--%>


                                              <%--      <asp:BoundField DataField="ApproveBy" HeaderText="Approved By" HtmlEncode="False" />--%>
                                                    <asp:BoundField DataField="ApproveDate" HeaderText="Approved Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                     
                                                     
                                                       
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                          
                        </div>
                     <div class="row" style="margin-top: -60px;"  runat="server" Visible="False" >
                          <div class="col-md-12">
                                <div class="card-body" style="padding-right: 10px;">
                             <div class="card" style="padding: 10px" runat="server" Visible="False">
                               
                                <div class="form-row">


                                    <div class="col-md-4 ">
                                        <div class="form-group ">
                                            <label class="control-label font-weight-bold">Designation:</label> <br/>
                                            <hr/>
                                             <asp:Label runat="server" ID="lblDesignation"></asp:Label>
                                            
                                           
                                            <asp:TextBox runat="server" Visible="False" ID="txt_Designation" class="form-control form-control-sm" />
                                        </div>
                                    </div>
                                    <div class="col-2 "  runat="server" Visible="False">
                                        <div class="form-group ">
                                            <label class="control-label">New Designation</label>
                                            <br/>
                                            <hr/>
                                             <asp:Label runat="server" ID="lblNewDesignation"></asp:Label>
                                            
                                            <asp:DropDownList ID="newDesignDropDownList"  Visible="False" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-4 ">
                                        <div class="form-group ">
                                            <label class="control-label font-weight-bold">Employee Requisition</label>
                                             <br/>
                                            <hr/>
                                             <asp:Label runat="server" ID="lblEmployeeRequisition"></asp:Label>
                                             
                                            <asp:TextBox runat="server"  Visible="False" ID="txt_EmployeeRequisition" class="form-control form-control-sm" />
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderconvRate" runat="server"
                                                Enabled="True" TargetControlID="txt_EmployeeRequisition" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                        </div>
                                    </div>

                                    <div class="col-md-4 ">
                                        <div class="form-group ">
                                            <label class="control-label font-weight-bold">Salary Range From</label> <br/>
                                            <hr/>
                                             <asp:Label runat="server" ID="lblSAlarRngFrom"></asp:Label>
                                            

                                            <asp:TextBox runat="server" Visible="False" ID="txt_ReqApproxSalary" class="form-control form-control-sm" />
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                Enabled="True" TargetControlID="txt_ReqApproxSalary" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                        </div>
                                    </div>

                                  
                                    <div class="col-2 " runat="server" visible="False">
                                        <div class="form-group" >
                                            <br />
                                            <asp:Button ID="btnAdd" Text="Add to list" OnClick="btnAdd_OnClick" CssClass="btn btn-sm activity-success" runat="server" BackColor="#FFCC00" />

                                        </div>
                                    </div>

                                </div>
                                 <div class="form-row">
                                       <div class="col-md-4">
                                        <div class="form-group ">
                                            <label class="control-label font-weight-bold">Salary Range To</label>
                                            <br/>
                                            <hr/>
                                             <asp:Label runat="server" ID="lblSalaryRangeTo"></asp:Label>
                                            
                                            <asp:TextBox runat="server" Visible="False" ID="lbl_ReqTotalSalary" class="form-control form-control-sm" />
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                Enabled="True" TargetControlID="lbl_ReqTotalSalary" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group ">
                                            <label class="control-label font-weight-bold">Quarter</label>
                                            
                                             <br/>
                                            <hr/>
                                             <asp:Label runat="server" ID="lblQuarter"></asp:Label>
                                             
                                            <asp:DropDownList CssClass="form-control form-control-sm" Visible="False" runat="server" ID="ddlQuarter">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4" runat="server">
                                        <div class="form-group">
                                            <label class="font-weight-bold">Job Summary</label>
                                            
                                             <br/>
                                            <hr/>
                                             <asp:Label runat="server" ID="lblJobSummary"></asp:Label>
                                            
                                            <asp:TextBox runat="server" Visible="False" ID="txtRemarks" class="form-control form-control-sm" />
                                        </div>
                                    </div>
                                 </div>
                           
                          </div>
                                    <div class="card" style="padding: 10px;background-color: white"  >
                                    
                                   
                                        </div>
                                        </div>

                                        </div>
                                   

                           <br/>
                          
                                    
                                   
                               
                        </div>
                          </div>
                     </div>
                        <div class="card-body">
                            <div class="form-row" runat="server" Visible="False">
                                <div class="col-2">
                                    <div class="form-group ">
                                        <label class="control-label">Company</label>
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" class="form-control form-control-sm" />
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group ">
                                        <label class="control-label">Department</label>
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDepartment" class="form-control form-control-sm" OnSelectedIndexChanged="ddlDepartment_OnSelectedIndexChanged" />
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group ">
                                        <label class="control-label">Financial Year</label>
                                        <asp:DropDownList runat="server" ID="ddlFinYear" class="form-control form-control-sm" />
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group ">
                                        <label class="control-label">Employee Type</label>
                                        <asp:RadioButtonList runat="server" AutoPostBack="True" ID="radEmpType" RepeatDirection="Horizontal" OnSelectedIndexChanged="radEmpType_OnSelectedIndexChanged" class="form-control form-control-sm" />
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group ">
                                        <label class="control-label">Project Name</label>
                                        <asp:DropDownList runat="server" ID="ddlProjectName" CssClass="form-control form-control-sm" Enabled="False" />
                                    </div>
                                </div>

                            </div>

                            <fieldset class="for-panel" runat="server" visible="False">
                                <legend>Existing Employee </legend>
                                <div class="form-row">
                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>Employee Category</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlEmpCategoryEx" class="form-control form-control-sm" OnSelectedIndexChanged="ddlEmpCategoryEx_OnSelectedIndexChanged" />
                                        </div>
                                    </div>
                                    <div class="col-2">
                                        <div class="form-group ">
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
                                            <label>Existing Salary(Max)</label>
                                            <asp:Label runat="server" ID="lblExGradeMaxSal" readonly="true" class="form-control form-control-sm" />
                                        </div>
                                    </div>
                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>Existing Salary(Min)</label>
                                            <asp:Label runat="server" ID="lblExGradeMinSal" readonly="true" class="form-control form-control-sm" />
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

                         
                            <div>
                             
                            </div>
                            <br />
                            <br />
                            <div class="row">
                                <div class="col-md-5">
                                </div>

                            </div>
                            <div>
                                <asp:HiddenField runat="server" ID="hdpk" />
                                <asp:HiddenField runat="server" ID="hddpk" />
                              
                            </div>
                        </div>
                    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

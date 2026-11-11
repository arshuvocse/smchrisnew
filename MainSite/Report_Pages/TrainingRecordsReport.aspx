<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="TrainingRecordsReport.aspx.cs" Inherits="Report_Pages_TrainingRecordsReport" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    
    
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
                        <div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;"> Training Records Report </h1>
                    </div>

                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                        <asp:Button ID="addNewButton" Text="Add New Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="addNewButton_OnClick" />
                    </div>
                    


                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">
                            
                             <div class="row" runat="server" Visible="False">
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
                                        <label>Report Name </label>
                                        <span style="color: red">&nbsp;*</span>
                                       <asp:DropDownList ID="reportDropDownList" AutoPostBack="True" runat="server" class="form-control form-control-sm" OnSelectedIndexChanged="reportDropDownList_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Select One</asp:ListItem>
                                        <asp:ListItem Value="Inquiry">Training Records List </asp:ListItem>
                                    </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Company Name </label>
                                        <span style="color: red">&nbsp;*</span>
                                        <asp:DropDownList ID="companyDropDownList" class="form-control form-control-sm"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="companyDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                
                                  <div class="col-md-2" runat="server"  >
                                    <div class="form-group">
                                        <label>Financial Year </label>
                                        <span style="color: red">&nbsp;*</span>
                                        <asp:DropDownList ID="FinancialYearDropDownList" class="form-control form-control-sm"  runat="server" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                </div>
                                    <div  class="col-md-2">
                                            <div class="form-group">
                                            <asp:Label runat="server" ID="Label4">Start From Date </asp:Label> 
                                           
                                            <asp:TextBox ID="EffectiveDateTextBox" AutoCompleteType="Disabled"   AutoPostBack="True" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="TopLeft"
                                                Format="dd-MMM-yyyy" PopupButtonID="EffectiveDateTextBox" CssClass="MyCalendar"
                                                TargetControlID="EffectiveDateTextBox" />
                                        </div>
                                        </div>
                                
                                    <div  class="col-md-2">
                                            <div class="form-group">
                                            <asp:Label runat="server" ID="Label1">Start To Date </asp:Label>  
                                           
                                            <asp:TextBox ID="EffectToDate" AutoCompleteType="Disabled"   AutoPostBack="True" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupPosition="TopLeft"
                                                Format="dd-MMM-yyyy" PopupButtonID="EffectToDate" CssClass="MyCalendar"
                                                TargetControlID="EffectToDate" />
                                        </div>
                                        </div>
                                
                                <div class="col-md-2"  runat="server" Visible="False">

                                                <div class="form-group">
                                                    <label>
                                                        <asp:CheckBox ID="manCheckBox" Text="Select/UnSelect All" runat="server" AutoPostBack="True" OnCheckedChanged="manCheckBox_OnCheckedChanged" />
                                                    </label>
                                                    <div style="max-height: 200px; overflow: scroll">
                                                        <asp:CheckBoxList ID="managementCheckBoxList" runat="server"></asp:CheckBoxList>
                                                    </div>


                                                </div>
                                            </div>
                                 <div class="col-md-2">
                                    <div class="form-group" style="margin-top: 17px;">
                                        
                                         <asp:Button runat="server" ID="SearchButton" OnClick="SearchButton_OnClick" ToolTip="Click To Search"  Text="Search"    class="btn btn-outline-info disabled btn-sm"  /> 
                                        </div>
                                     </div>
                            </div>
                               <div class="row">
                                
                                   <div class="col-md-2">
                                       
                                       <label> Separation List</label>
                                       </div>
                                   
                                   
                                   <div class="col-md-2">
                                       
                                       
                                       </div>
                                   <div class="col-md-2">
                                       
                                       
                                       </div>
                                   <div class="col-md-2">
                                       
                                       
                                       </div>
                                     <div class="col-md-2">
                                         <asp:LinkButton ID="btnExportToExcel" runat="server" CssClass="btnexcel" OnClick="btnExportToExcel_Click" ><span aria-hidden="true" class="glyphicon glyphicon-ok"></span>Export To Excel</asp:LinkButton>
                                       
                                       </div>
                                     
                                   <div class="col-md-2">
                                      <asp:LinkButton ID="btnExportToPDF" runat="server" CssClass="btnexcel"  OnClick="btnExportToPDF_Click" ><span aria-hidden="true" class="glyphicon glyphicon-apple"></span>Export To PDF</asp:LinkButton>
        
  </div>
                                       
                                       </div>
                            
                                 <style>
                                    .btnexcel {
                                        background-color: #4CAF50;
                                        border: none;
                                        color: white;
                                        padding: 10px 16px;
                                        text-align: center;
                                        text-decoration: none;
                                        display: inline-block;
                                        font-size: 12px;
                                        margin: 4px 2px;
                                        cursor: pointer;
                                    }
                                </style>
                                   </div>
                            <div id="gridContainer1" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;">
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered text-center thead-dark" DataKeyNames="EmployeeJobLeftId,IsJobLeft"
                                    OnRowCommand="loadGridView_RowCommand" OnRowCreated="loadGridView_OnRowCreated">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:BoundField DataField="EmpMasterCode" HeaderText="Empployee ID" />
                                         <asp:BoundField DataField="EmpName" HeaderText="Empployee Name" />
                                         <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                         <asp:BoundField DataField="GradeName" HeaderText="Grade" />
                                        <asp:BoundField DataField="DateOfJoin" HeaderText="Date Of Join" DataFormatString="{0:dd-MMM-yyyy}" />
                                      
                                      
                                        <asp:BoundField DataField="JobLeftType" HeaderText="Type of Separation" />
                                        
                                        <asp:BoundField DataField="SubmissionDate" HeaderText="Effective Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="JobLeftDate" HeaderText="Date of Seperation" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="LengthServicewithSMC" HeaderText="Length of Service with SMC"  />
 
                                      
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                </div>
         <%--   </ContentTemplate>
        </asp:UpdatePanel>--%>
    <%--</div>--%>
</asp:Content>


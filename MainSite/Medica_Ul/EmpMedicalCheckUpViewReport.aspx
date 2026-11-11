<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="EmpMedicalCheckUpViewReport.aspx.cs" Inherits="Medica_Ul_EmpMedicalCheckUpViewReport" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
     <div class="content" id="content">
        <style>
         .btnexcel {
            background-color: #4CAF50;
            border: none;
            color: white;
            text-shadow: 1px 1px 1px #000000;
            padding: 8px 12px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 12px;
            margin: 4px 2px;
            cursor: pointer;
                                           
        }
    </style>
    <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">--%>
        <%--    <ContentTemplate>--%>
             <div class="page-heading">
                    <div class="page-heading__container">
                        <div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Medical Check-Up List Report</h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                         <asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " Visible="False" runat="server" OnClick="HomeButton_OnClick" />
                        <asp:Button ID="addNewButton" Text="Add New Information" Visible="False"  CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="addNewButton_OnClick" />
                    </div>

                </div>
         <div class="container-fluid" >
                    <div class="card" >
                        <div class="card-body" >
                             <div class="row" runat="server" Visible="False">
                                      
                                    <div class="col-md-3" runat="server" Visible="False">
                                        <div class="form-group">
                                            <label> </label>
                                             <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True"  RepeatDirection="Horizontal" Enabled="False" >
                                        </asp:CheckBoxList>
                                            

                                        </div>
                                    </div>
                                </div>
                            
                            <div class="row" runat="server" Visible="False">
                                 <div class="col-md-3">
                                
                                     <asp:DropDownList ID="company" runat="server" EnableViewState="true"  class="form-control form-control-sm" AutoPostBack="True"></asp:DropDownList>
                                     </div>
                            </div>
                            
                             <div class="form-row">
                                    <div class="col-2">
                                        <div class="form-group ">
                                            <label class="control-label">Company</label> <label style="color: #a52a2a">*</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged"  class="form-control form-control-sm" />
                                        </div>
                                    </div>
                                    
                                     <div class="col-2">
                                    <div class="form-group">
                                        <label>Check-up From Date</label>   <label style="color: #a52a2a">*</label>
                                        <asp:TextBox runat="server" ID="CheckuptxtDate" class="form-control form-control-sm" />
                                          <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                                                    Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                    TargetControlID="CheckuptxtDate" />
                                    </div>
                                         </div>
                                 
                                 
                                  <div class="col-2">
                                    <div class="form-group">
                                        <label>Check-up To Date</label>   <label style="color: #a52a2a">*</label>
                                        <asp:TextBox runat="server" ID="CheckuptxtToDate" class="form-control form-control-sm" />
                                          <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                    Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                    TargetControlID="CheckuptxtToDate" />
                                    </div>
                                         </div>
                                 
                                 
                                   <div class="col-2">
                                    <div class="form-group">
                                        <label>Status</label>   <label style="color: #a52a2a">*</label>
                                           <asp:DropDownList runat="server" AutoPostBack="True" ID="CheckupddlStatus"   class="form-control form-control-sm" >
                                               <asp:ListItem Value="0">Select One</asp:ListItem>
                                               <asp:ListItem Value="1">Completed</asp:ListItem>
                                               <asp:ListItem Value="2">Not Completed</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                         </div>

                                  
                                 </div>
                            <div class="row">
                                <div class="col-2">
                                        <div class="form-group">
                                            <label>Division</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDivision" class="form-control form-control-sm" OnSelectedIndexChanged="ddlDivision_OnSelectedIndexChanged" />

                                        </div>
                                    </div>
                                    <div class="col-2" runat="server" id="wing" visible="False">
                                        <div class="form-group">
                                            <label>Wing</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlWing" class="form-control form-control-sm" OnSelectedIndexChanged="ddlWing_OnSelectedIndexChanged" />

                                        </div>
                                    </div>
                                    <div class="col-2" runat="server" id="dept">
                                        <div class="form-group">
                                            <label>Department</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDepartment" class="form-control form-control-sm" OnSelectedIndexChanged="ddlDepartment_OnSelectedIndexChanged" />

                                        </div>
                                    </div>
                                    <div class="col-2" runat="server" id="sec" visible="False">
                                        <div class="form-group">
                                            <label>Section</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlSection" class="form-control form-control-sm" OnSelectedIndexChanged="ddlSection_OnSelectedIndexChanged" />

                                        </div>
                                    </div>
                                    <div class="col-2" runat="server" id="subsec" visible="False">
                                        <div class="form-group">
                                            <label>Sub Section</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlSubSection" class="form-control form-control-sm" OnSelectedIndexChanged="ddlSubSection_OnSelectedIndexChanged" />

                                        </div>
                                    </div>

                                    <div class="col-md-2">

                                        <div class="form-group">
                                            <label>Employee ID: </label>
                                            <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="True" placeholder=" Employee ID" CssClass="form-control form-control-sm"
                                                OnTextChanged="EmployeeDropDownList2_SelectedIndexChanged"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters=""
                                                    EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                    ServiceMethod="GetCompanyWiseEmployeeInfoForIDANdNae" ServicePath="~/WebService.asmx" TargetControlID="txtSearch"
                                                    UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                    ShowOnlyCurrentWordInCompletionListItem="true">
                                                </cc1:AutoCompleteExtender>

                                        </div>
                                    </div>


                                    <div class="col-md-2">

                                        <div class="form-group">
                                            <label>Employee Name: </label>
                                            <asp:TextBox ID="NameTextBox" runat="server" AutoPostBack="True" placeholder=" Employee Name" CssClass="form-control form-control-sm" OnTextChanged="EmployeeDropDownList_SelectedIndexChanged"></asp:TextBox>
                                            
                                               <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                    EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                    ServiceMethod="GetCompanyWiseEmployeeInfoForIDANdNae" ServicePath="~/WebService.asmx" TargetControlID="NameTextBox"
                                                    UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                    ShowOnlyCurrentWordInCompletionListItem="true">
                                                </cc1:AutoCompleteExtender>

                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Designation</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDesignation" class="form-control form-control-sm" />

                                        </div>
                                    </div>
                                
                                
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Office</label>
                                            <asp:DropDownList runat="server" ID="ddlSalaryLocation" class="form-control form-control-sm" />

                                        </div>
                                    </div>
                                
                                 <div class="col-md-2">
                                    <div class="form-group" style="margin-top: 17px;">

                                        <asp:Button runat="server" ID="SearchButton" OnClick="SearchButton_OnClick" ToolTip="Click To Search" Text="Search" class="btn btn-info btn-sm" />
                                    </div>
                                </div>
                            </div>
                            
                            
                                 <div class="row" style="padding: 5px;">
                        <div class="col-md-10"></div>
                        <div class="col-md-2">
                        
                            <asp:LinkButton ID="btnExportToExcel" runat="server"  CssClass="btnexcel  pull-right"  OnClick="btnExportToExcel_Click" ><i class="fa fa-download"></i> Export To Excel</asp:LinkButton>

                        </div>
                    </div>

                            <div id="gridContainer1" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;" >
                                
                           
                                <asp:GridView ID="CheckupGridView1" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-sm table-bordered text-center thead-dark" >
                                    <Columns>

                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                            <asp:BoundField DataField="Date" HeaderText="Check-up Date"  DataFormatString="{0:dd-MMM-yyyy}"/>

                                        <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee ID" />
                                        <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                        <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                        
                                      
                                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                       
                                       
                                          
                                      
                                    </Columns>
                                </asp:GridView>
                            </div>

                            </div>
                        </div>
                    </div>
                </div>
         <%--   </ContentTemplate>--%>
        <%--    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnExportToExcel"  />
    </Triggers>--%>
    <%--    </asp:UpdatePanel>--%>
    </div>
    
 
</asp:Content>


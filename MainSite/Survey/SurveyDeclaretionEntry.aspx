<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="SurveyDeclaretionEntry.aspx.cs" Inherits="Survey_SurveyDeclaretionEntry" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    
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
    <style>
              #cpFormBody_GVQuestion  > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }

       #cpFormBody_GVQuestion > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }

          #cpFormBody_EmpSaveGridView1  > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #33B5E5;
            /*background-color: #98A9C0;*/
        }

       #cpFormBody_EmpSaveGridView1 > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
           padding: 18px;
        }



           #cpFormBody_GVQuestion  > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }

       #cpFormBody_EmpInfoGridView > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }

        #cpFormBody_EmpInfoGridView  > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #0094ff;
            /*background-color: #98A9C0;*/
        }


    </style>
     <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                          <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px"
                   />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                        
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;"><img src="../Report_Pages/app.png"  width="20px" />  Survey Declaration Information </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                          <asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="HomeButton_OnClick" />
                        <asp:Button ID="addNewButton" Text="Go Back" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="addNewButton_OnClick" />
                    </div>

                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">
                            <asp:HiddenField runat="server" ID="VacancyIdHiddenField"/>
                               <div class="form-row">
                                  
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Company</label>     <label style="color: #a52a2a">*</label>
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" class="form-control form-control-sm" />
                                    </div>
                                </div>

                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                         <label style="color: #a52a2a">*</label>
                                        <asp:DropDownList runat="server" ID="ddlFinYear" CssClass="form-control form-control-sm" />
                                    </div>
                                </div>
                                
                            
                                   
                                   <div class="col-md-2">
                                        <div class="form-group">
                                        <label>Survey Name </label>
                                        <label style="color: #a52a2a">*</label>
                                         
                                       
                                        <asp:TextBox ID="SurveyNameTextBox" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                    </div> 
                                       </div>
                                       
                                            <div  class="col-md-2">
                                            <div class="form-group">
                                            <label>Survey Start Date </label>     <label style="color: #a52a2a">*</label>
                                           
                                            <asp:TextBox ID="EffectiveDateTextBox" AutoCompleteType="Disabled"   AutoPostBack="True" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="TopLeft"
                                                Format="dd-MMM-yyyy" PopupButtonID="EffectiveDateTextBox" CssClass="MyCalendar"
                                                TargetControlID="EffectiveDateTextBox" />
                                        </div>
                                        </div>
                                  
                                
                                    <div  class="col-md-2">
                                            <div class="form-group">
                                            <label>Survey End Date </label>      <label style="color: #a52a2a">*</label>
                                           
                                            <asp:TextBox ID="EffectToDate" AutoCompleteType="Disabled"   AutoPostBack="True" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupPosition="TopLeft"
                                                Format="dd-MMM-yyyy" PopupButtonID="EffectToDate" CssClass="MyCalendar"
                                                TargetControlID="EffectToDate" />
                                        </div>
                                        </div>
                                      <div  class="col-md-2">
                                       <div class="checkbox">

                                            <label class="btn btn-default">
                                                <asp:CheckBox ID="isActiveCheckBox" CssClass="checkbox margin-right" runat="server" />
                                                <label>Is Active </label>
                                            </label>

                                        </div>
                                          </div>
                                
                                <div class="col-md-2" style="margin-top: 12px;" runat="server" Visible="False">
                                    <div class="form-group" >

                                        <asp:Button runat="server" ID="SearchButton" OnClick="SearchButton_OnClick" ToolTip="Click To Search" Text="Search" class="btn btn-outline-info disabled btn-sm" />
                                    </div>
                                </div>
                               


                            </div>
                            
                                <div class="page-header text-center">
      <h1  class="elegantshd" >Select Question</h1>
    </div>
                            <div class="row">
                                      
                                               
    
             <style>
                 .elegantshd {
                     color: #131313;
  
                     letter-spacing: .15em;
                     text-shadow: 2px 2px 4px #000000;
                     text-decoration: underline;
                     font-family: 'Kreon', serif;
                     vertical-align:middle;  text-decoration-style: wavy;
                 }
             </style> 
                                   <div class="col-md-12">
                                        <div id="gridContainer13" style="height: 300px; overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;">
                                <asp:GridView ID="GVQuestion" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered text-center thead-dark" DataKeyNames="SurveyQuestionId"
                                    OnRowCommand="loadGridView_RowCommand" OnRowCreated="loadGridView_OnRowCreated">
                                    <Columns>
                                        
                                          <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkSelectAll" runat="server" CssClass="form-control-sm" AutoPostBack="True" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" CssClass="form-control-sm" AutoPostBack="True" runat="server" />
                                                  <asp:HiddenField runat="server" ID="txt_empInfoId" Value='<%#Eval("SurveyQuestionId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="QuestionTitle" HeaderText="Question Title" />
                                        <asp:BoundField DataField="SurveyQuestionGroup" HeaderText="Survey Question Group Name" />
                                        <asp:BoundField DataField="SurveyQuestionType" HeaderText="Survey Question Type" />
                                
                                    </Columns>
                                </asp:GridView>
                            </div>
                                   </div>
                            </div>
                                  <div class="row">
                                    <div class="col-md-3" runat="server" Visible="False">
                                        <div class="form-group">
                                            <label> </label>
                                             <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True"  RepeatDirection="Horizontal" Enabled="False" >
                                        </asp:CheckBoxList>
                                            

                                        </div>
                                    </div>
                                </div>
                            <br/>
                                            <div class="page-header text-center">
      <h1  class="elegantshd" >Select Participant</h1>
    </div>
                              <fieldset class="for-panel">
                                <legend>Search Employee </legend>
                            <div class="form-row">
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Employee Category</label>
                                        <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control form-control-sm" />
                                    </div>
                                </div>

                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Department</label>
                                        <asp:DropDownList runat="server" ID="ddlDept" CssClass="form-control form-control-sm" />
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group" style="margin-top: 18px;">

                                        <asp:Button ID="Button1" Text="Search" CssClass="btn btn-outline-success disabled btn-sm" runat="server" OnClick="Button1_OnClick" />
                                    </div>
                                </div>

                            </div>
                                <div id="gridContainer1" style="height: 300px; overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;" >
                                
                                <asp:GridView ID="EmpInfoGridView" runat="server" AutoGenerateColumns="False"
                                   CssClass="table table-bordered text-center thead-dark" DataKeyNames="EmpInfoId"
                                    OnRowCommand="loadGridView_RowCommand"  Font-Size="12px"      >
                                    <Columns>
                                        
                                         <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="EmpchkSelectAll" runat="server" CssClass="form-control-sm" AutoPostBack="True" OnCheckedChanged="EmpchkSelectAll_CheckedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="EmpchkSelect" CssClass="form-control-sm" AutoPostBack="True" runat="server" />
                                                  <asp:HiddenField runat="server" ID="txt_empId" Value='<%#Eval("EmpInfoId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    
                                        <asp:BoundField DataField="EmpMasterCode" HeaderText="ID" />
                                        <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="DepartmentName"  HeaderText="Department" />
                                       
                                        <asp:BoundField DataField="SalaryLocation"  HeaderText="Office" />
                                        <asp:BoundField DataField="EmpType"  HeaderText="Employee Type" />
                                      


                                       
                                    </Columns>
                                    
                                </asp:GridView>

                              
                            </div>
                                  
                                  <div class="row" style="padding: 10px;">
                                       <div class="col-md-10">
                                           </div>
                                      
                                       <div class="col-md-2">
                                            <div class="form-group">
                                            <asp:Button ID="textButton" Text="Add To List" OnClick="textButton_OnClick" CssClass="btn btn-outline-success btn-block disabled btn-sm" runat="server" />
                                        </div>
                                           </div>
                                  </div>
                                  
                                </fieldset>
                            
                             <div id="gridContaindder1" style="height: 300px; overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;" >
                                
                                <asp:GridView ID="EmpSaveGridView1" runat="server" AutoGenerateColumns="False"
                                   CssClass="table table-bordered text-center thead-dark" DataKeyNames="EmpInfoId"
                                    OnRowCommand="loadGridView_RowCommand"  Font-Size="12px"  PageIndex="1"    >
                                    <Columns>
                                        
                                         
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    
                                        <asp:BoundField DataField="EmpMasterCode" HeaderText="ID" />
                                        <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="DepartmentName"  HeaderText="Department" />
                                       
                                        <asp:BoundField DataField="SalaryLocation"  HeaderText="Office" />
                                        <asp:BoundField DataField="EmpType"  HeaderText="Employee Type" />
                                      


                                       
                                    </Columns>
                                    
                                </asp:GridView>
                                 </div>

                        
                          <div class="row">
                               <div class="col-md-12">
                                     <div class="form-group">
                                        <asp:Button ID="submitButton" Text="Save" CssClass="btn btn-sm btn-info"   Visible="False" runat="server" OnClick="submitButton_Click" />
                                           <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                        <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />
                                     <%--   <asp:Button ID="cancelButton" Text="Back to List" CssClass="btn btn-sm warning" runat="server" Visible="False" OnClick="cancelButton_OnClick" BackColor="#FFCC00" />--%>
                                       <%-- &nbsp; &nbsp;  &nbsp;<asp:HyperLink ID="hyperlink" NavigateUrl="~/ContractualEmployeeManagement_UI/ContractualEmpList.aspx" CssClass="btn btn-sm text-info" Text="&lt; &lt; &lt; &lt; Back to List" runat="server" />--%>
                                    </div>
                               </div>
                           </div>
                    </div>

                            </div>
                            
                         
                        </div>
                    </div>
                
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>


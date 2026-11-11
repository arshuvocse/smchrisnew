<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeJobLeftApproveView.aspx.cs" Inherits="ExitManagement_UI_EmployeeJobLeftApproveView" %>
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
                <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px"
                   />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                        <div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;"> Separation Approval List </h1>
                    </div>

                    <div class="page-heading__container float-right d-none d-sm-block">
                          <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                 
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
                            <div class="row" runat="server" Visible="False">

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Company Name </label>
                                        <span style="color: red">&nbsp;*</span>
                                        <asp:DropDownList ID="companyDropDownList" class="form-control form-control-sm"  runat="server" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                </div>
                                    <div  class="col-md-2">
                                            <div class="form-group">
                                            <label runat="server" ID="Label4">Effective From Date </label> 
                                           
                                            <asp:TextBox ID="EffectiveDateTextBox" AutoCompleteType="Disabled"   AutoPostBack="True" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="TopLeft"
                                                Format="dd-MMM-yyyy" PopupButtonID="EffectiveDateTextBox" CssClass="MyCalendar"
                                                TargetControlID="EffectiveDateTextBox" />
                                        </div>
                                        </div>
                                
                                    <div  class="col-md-2">
                                            <div class="form-group">
                                            <label runat="server" ID="Label1">Effective To Date </label>  
                                           
                                            <asp:TextBox ID="EffectToDate" AutoCompleteType="Disabled"   AutoPostBack="True" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupPosition="TopLeft"
                                                Format="dd-MMM-yyyy" PopupButtonID="EffectToDate" CssClass="MyCalendar"
                                                TargetControlID="EffectToDate" />
                                        </div>
                                        </div>
                                 <div class="col-md-2">
                                    <div class="form-group" style="margin-top: 18px;">
                                        
                                         <asp:Button runat="server" ID="SearchButton" OnClick="SearchButton_OnClick" ToolTip="Click To Search"  Text="Search"    class="btn btn-outline-info disabled btn-sm"  /> 
                                        </div>
                                     </div>
                            </div>

                            <div id="gridContainer1" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;">
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                   CssClass="AddToListCssTable"
                                     OnPreRender="gv_DocumentUpload_PreRender" DataKeyNames="EmployeeJobLeftId, EmployeeId,IsJobLeft,EmployeeJobLeftAppLogId"
                                    OnRowCommand="loadGridView_RowCommand" OnRowCreated="loadGridView_OnRowCreated">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                           <%--<asp:TemplateField HeaderText="Print Letter">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="VisitingLetterImageButton" runat="server" class="btn btn-white btn-sm  " CommandArgument='<%#Eval("EmployeeJobLeftId") %>' 
                                                        CommandName="PrintLetter" ImageUrl="~/Assets/file.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        <%--<asp:BoundField DataField="CompanyName" HeaderText="Company Name" />--%>

                                        <asp:BoundField DataField="EmpName" HeaderText="Empployee Name" />
                                      
                                        <asp:BoundField DataField="JobLeftType" HeaderText="Job Left Type" />
                                        
                                        <asp:BoundField DataField="SubmissionDate" HeaderText="Submission Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="JobLeftDate" HeaderText="Seperation Date" DataFormatString="{0:dd-MMM-yyyy}" />

                                        <asp:BoundField DataField="Reason" HeaderText="Reason" />
                                        <asp:BoundField DataField="ExitFormStatus" HeaderText="Status" />
                                        
                                       
                                        <%--<asp:BoundField DataField="EntryBy" HeaderText="Create By" />
                                        <asp:BoundField DataField="EntryDate" HeaderText="Create Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="UpdateBy" HeaderText="UpdateBy" />
                                        <asp:BoundField DataField="UpdateDate" HeaderText="UpdateDate" DataFormatString="{0:dd-MMM-yyyy}" />--%>

                                        <asp:TemplateField HeaderText="Approve">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="editImageButton" runat="server"
                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="ViewData"
                                                   ImageUrl="~/Assets/img/completed-task.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--<asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="deleteImageButton" runat="server"
                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="DeleteData"
                                                    ImageUrl="~/Assets/img/delete.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="viewImageButton" runat="server"
                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="ViewData"
                                                    ImageUrl="~/Assets/img/list-view.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>


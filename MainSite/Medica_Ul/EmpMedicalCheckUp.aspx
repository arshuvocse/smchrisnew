<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="EmpMedicalCheckUp.aspx.cs" Inherits="Medica_Ul_EmpMedicalCheckUp" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
    <style>
        .checkboxlist_nowrap {
            display: inline;
        }
    </style>
    
    
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">


    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>
                      <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px"
                   />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
                
                     <div class="page-heading">
                    <div class="page-heading__container">
                        <div class="icon"></div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Medical Check-up Information </h1>
                    </div>

                    <%-- <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="detailsViewButton" Text="View List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>--%>

                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                        <asp:Button ID="ListViewButton" Visible="True" Text="Go Back" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>

                </div>
                <div class="container-fluid">
                    

                    <div class="card">
                        <div class="card-body">
                            
                                <%--<legend>Search By</legend>--%>
                                 <asp:HiddenField runat="server" ID="m_hdpkd"></asp:HiddenField>
                                <div class="form-row">
                                    <div class="col-2">
                                        <div class="form-group ">
                                            <label class="control-label">Company</label> <label style="color: #a52a2a">*</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" class="form-control form-control-sm" />
                                        </div>
                                    </div>
                                    
                                     <div class="col-2">
                                    <div class="form-group">
                                        <label>Check-up Date</label>  <label style="color: #a52a2a">*</label>
                                        <asp:TextBox runat="server" ID="txtDate" class="form-control form-control-sm" />
                                          <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                                                    Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                    TargetControlID="txtDate" />
                                    </div>
                                </div>
                                     <div class="col-2">
                                    <div class="form-group">
                                        <label>Comments</label> 
                                        <asp:TextBox runat="server" ID="txtComments" class="form-control form-control-sm" />
                                           
                                    
                               
                                   </div>
                                         </div>
                                 
                                </div>
                          
                    
                            
                             <fieldset class="for-panel">
                                <legend>Employee Information</legend>
                            <div class="form-row">
                                
                                
                                        
                                             <div class="col-3">
                                        <div class="form-group ">
                                            <label class="control-label">Search Employee Name</label> <label style="color: #a52a2a">*</label>
                                            <br />
                                            <asp:TextBox ID="txt_EmpName" runat="server" AutoPostBack="True" Enabled="False" CssClass="form-control form-control-sm" OnTextChanged="txt_EmpName_OnTextChanged"></asp:TextBox>
                                            <asp:HiddenField runat="server" ID="hfEmpInfoId" />
                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                ServiceMethod="GetExitImployee" ServicePath="~/WebService.asmx" TargetControlID="txt_EmpName"
                                                UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                ShowOnlyCurrentWordInCompletionListItem="true">
                                            </cc1:AutoCompleteExtender>
                                              <asp:HiddenField ID="repEmpIdHiddenField" runat="server" />
                                        <%--    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                ServiceMethod="GetExitImployee" ServicePath="~/WebService.asmx" TargetControlID="txt_EmpName"
                                                UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                ShowOnlyCurrentWordInCompletionListItem="true">
                                            </cc1:AutoCompleteExtender>--%>
                                        </div>
                                    </div>
                                    
                                
                                            
                                            <div class="col-md-9">
                                                <div class="row">
                                                     <div class="col-2">
                                    <div class="form-group">
                                        <label>Employee ID</label> 
                                        <asp:Label runat="server" ID="empCode" class="form-control form-control-sm" />
                                       
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Employee Name</label> 
                                        <br />
                                        <asp:Label runat="server" ID="empName" class="form-control form-control-sm" />
                                       

                                    </div>
                                </div>

                                

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Department</label> 
                                        <asp:Label runat="server" ID="ddlDivision" class="form-control form-control-sm" />
                                    
                                    </div>
                                </div>
                                
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Designation</label> 
                                        <asp:Label runat="server" ID="ddlDesignation" class="form-control form-control-sm" />
                                        
                                    </div>
                                </div>
                                                </div>
                                            </div>

                               

                            </div>
                                 </fieldset>
 
                            
                            
                             <div class="form-row">
                               

                                
                              

                             
                            </div>
                            
                             <div class="form-row">
                                   <div class="col-5">
                                       </div>
                                 <div class="col-2">
                                    <div class="form-group">
                                        
                                          <asp:Button ID="textButton" Text="Add To List" OnClick="textButton_OnClick" CssClass="btn btn-outline-success btn-block disabled btn-sm" runat="server" />
                                        </div>
                                    </div>
                                 </div>

                            <div id="exitReasonGridViewa" style="height: auto;">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-sm table-bordered text-center thead-dark" DataKeyNames="EmpInfoId, CompanyId">
                                    <Columns>

                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        

                                        <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee ID" />
                                        <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="Date" HeaderText="Date" Visible="False" />
                                        <asp:BoundField DataField="Comments" HeaderText="Comments" Visible="False" />
                                       
                                          <asp:TemplateField HeaderText="Remarks">
                                                            <ItemTemplate >
                                                                   <asp:TextBox ID="txtRemarks" CssClass="form-control form-control-sm" Value='<%# Eval("Remarks")%>'  runat="server"></asp:TextBox>
                                                                  
                                                                </ItemTemplate>
                                                        </asp:TemplateField>
                                        
                                           <asp:TemplateField HeaderText="Delete">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="deleteImageButtonDirectlySupervices" runat="server" OnClick="deleteImageButtonDirectlySupervices_OnClick"
                                                                    ImageUrl="~/Assets/img/delete.png" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                        
                                    </Columns>
                                </asp:GridView>
                            </div>

                        
                            <div>
                                <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" Text="Submit " Visible="False" CssClass="btn btn-sm btn-info" />
                                         <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                    <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />
                                       
                                <asp:Button ID="cancelButton" Text="Cancel" OnClick="cancelButton_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
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
                            <br/>
                            <br/>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>


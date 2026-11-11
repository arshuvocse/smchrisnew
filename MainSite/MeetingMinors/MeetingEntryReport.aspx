<%@ Page Title="Board Minutes Entry Report" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" EnableEventValidation="false"  AutoEventWireup="true" CodeFile="MeetingEntryReport.aspx.cs" Inherits="MeetingMinors_MeetingEntryReport" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
      
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
      <style>
        fieldset.for-panel {
          
            padding: 10px 8px;
            background-color: white;
            margin-bottom: 12px;
        }

            fieldset.for-panel legend {
               
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

  
  .chkChoice label {
            padding-left: 4px;
            padding-right: 4px;
        }

  
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
          </style>
    
     <div class="content" id="content">
                 <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                             
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" /> Board Minutes Entry Report </h1>
                        </div>
                         <div class="page-heading__container float-right d-none d-sm-block">
                         
             
                <%--<asp:Button ID="reloadButton" Text="Reload" CssClass="btn btn-sm btn-outline-success" runat="server" OnClick="reloadButton_OnClick" />--%>
            </div>
                    </div>
                     
<asp:UpdatePanel runat="server">
    <ContentTemplate>
         <asp:UpdateProgress ID="UpdateProgress4" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait11" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
        <div class="card">
   <div class="card-body">
       
          <div class="row">
  <div class="col-md-2"></div>
                    <div class="col-md-6">
                        <div class="form-group ">
                                          
                          <div class="row">
                               <div class="col-md-2"></div>
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Company:</label></div>
                            <div class="col-md-6">  <asp:DropDownList runat="server"  ID="ddlCompany" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged"  class="form-control form-control-sm" /></div>
                          </div>
                            
                            <div style="padding-top: 5px;"></div>
                             <div class="row">
                                  <div class="col-md-2"></div>
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">  From Date:</label></div>
                            <div class="col-md-6">  <asp:TextBox runat="server"   TextMode="Date"    ID="txtCreatedDate"  class="form-control form-control-sm" /></div>
                          </div>
                          <%--  <div style="padding-top: 5px;"></div>--%>
                               <div style="padding-top: 5px;"></div>
                             <div class="row">
                                  <div class="col-md-2"></div>
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">  To Date:</label></div>
                            <div class="col-md-6">  <asp:TextBox runat="server"   TextMode="Date"    ID="txtToDate"  class="form-control form-control-sm" /></div>
                          </div>
                            
                             
                                <div class="row" runat="server" Visible="False">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Title:</label></div>
                            <div class="col-md-6">  <asp:TextBox runat="server"    ID="txtTitle"  class="form-control form-control-sm" /></div>
                          </div>
                         <%--   <div style="padding-top: 5px;"></div>--%>

                             <div class="row" runat="server" Visible="False">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Propuse:</label></div>
                            <div class="col-md-6">  <asp:TextBox runat="server"      ID="txtPropuse"  class="form-control form-control-sm" /></div>
                          </div>
                            
                           <%--  <div style="padding-top: 5px;"></div>--%>
                            
                             <asp:DropDownList  runat="server"    ID="ddlCreatedBy" style="display: none" class="form-control form-control-sm" />
                            </div>
                        </div>
             
                 
             
             
              <div class="col-md-12" runat="server" Visible="False">
                        <div class="form-group ">
                                          
                          
                                 <div class="row">
                                      
                               
                                    <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Key Search:</label></div>
                            <div class="col-md-6">
                                
                                  <asp:TextBox runat="server"    ID="txtKeySearch"  class="form-control form-control-sm" />
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                        EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                        ServiceMethod="getMeetingKeySearch" ServicePath="~/WebService.asmx" TargetControlID="txtKeySearch"
                                                        UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement222"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                        ShowOnlyCurrentWordInCompletionListItem="true">
                                                    </cc1:AutoCompleteExtender>
                                  <asp:DropDownList runat="server"    ID="ddlKeySearch"  Visible="False" class="form-control form-control-sm" />
                            </div>
                               </div>
                            </div>
                                       </div>
             </div>
       
       
            


                                    <div class="row">
                                        <div class="col-md-5">
                                            <%--<asp:Button runat="server" ID="Button51" OnClick="btn_Save_OnClick" Text="Search " CssClass="btn btn-outline-info btn-block disabled btn-sm" />--%>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:LinkButton runat="server" ID="btn_Search" OnClick="btn_Search_OnClick"   CssClass="btn btn-info   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search </asp:LinkButton>

                                            <asp:LinkButton runat="server" ID="lbReset" OnClick="lbReset_OnClick" CssClass="btn btn-warning   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset </asp:LinkButton>
                                        </div>
                                        <div class="col-md-4">
                                            <%--<asp:Button runat="server" ID="Button51" OnClick="btn_Save_OnClick" Text="Search " CssClass="btn btn-outline-info btn-block disabled btn-sm" />--%>
                                        </div>
                                    </div>
       
        <br />
                   <div class="form-row" style="padding-right: 10px">

                                <div class="col-md-6" style="padding-left: 2px">
                                    <label style="font-size: 18px;">Details Information</label>
                                </div>
                                <div class="col-md-2">
                                </div>
                               
                              
                                     <div class="col-md-2"  style="margin-top: 22px; padding: 5px;">
                                       
                                               
                                        
                                      
                                     </div>
                                <div class="col-md-2">
                                    <%--  <asp:LinkButton ID="btnExportToExcel" runat="server" CssClass="btnexcel  pull-right" Style="padding-right: 10px;" OnClick="btnExportToExcel_Click"><span aria-hidden="true" style="font-size: 14px; color: #4CAF50;" class="fa fa-file-excel-o"></span> Download to xls</asp:LinkButton>--%>
                                        <asp:LinkButton ID="btnExportToExcel" runat="server"  CssClass="btnexcel  pull-right" OnClick="btnExportToExcel_Click" ><i class="fa fa-file-excel-o"></i> Export To Excel</asp:LinkButton>
                                    <%--<input type="button" id="btnExportEmpDetails" title="Export to Excel" class="pull-right btnexcelcc " value="" />--%>
                                </div>


                            </div>
          
       <style>
           .table   thead th {
               background-color: #5B799E;
               color: white;
                font-size: 13px !important;
                                            font-family: "Times New Roman", Times, serif !important;
                                            font-style: italic !important;
                                            font-weight: bold!important;
           }


          
                                              
                                              .dt-button.buttons-print,
                                               .dt-button.buttons-excel.buttons-html5,
                                               .dt-button.buttons-pdf.buttons-html5 {
                                                   background-color: white!important;
                                                    color:#880e4f !important;
                                                   border: none!important;
                                                  
                                                   padding: 5px 18px!important;
                                                   text-align: center!important;
                                                   text-decoration: none!important;
                                                   display: inline-block!important;
                                                   font-size: 16px!important;
                                                   margin: 2px 1px!important;
                                                   cursor: pointer!important;
                                                   -webkit-transition-duration: 0.4s!important;
                                                   transition-duration: 0.4s!important;
                                                   box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2), 0 3px 10px 0 rgba(0,0,0,0.19)!important;
                                               }


                                              .dt-buttons {
                                                  align-content: center;
                                                  text-align: right;
                                                  margin-top: -50px;
                                              }
                                              .dt-button.buttons-excel.buttons-html5:hover,
                                              .dt-button.buttons-pdf.buttons-html5:hover {
                                                  box-shadow: 0 12px 16px 0 rgba(0,0,0,0.24),0 17px 50px 0 rgba(0,0,0,0.19)!important;
                                                  color:white!important;
              background-color: #880e4f !important;
                                              }
                                         
       </style>

            <asp:GridView Width="100%" ShowHeader="True" ID="gv_ViewList" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                               

                                                 
                                                
                                                
                                                  <asp:BoundField DataField="MeetingNo" HeaderText="Meeting No."   /> 
                                                  <asp:BoundField DataField="MeetingYear" HeaderText="Year"   /> 
                                                  <asp:BoundField DataField="MeetingDate" HeaderText="Date"   /> 
                                                  <asp:BoundField DataField="AgendaNo" HeaderText="Agenda No."   /> 
                                                  <asp:BoundField DataField="Notice" HeaderText="Notice"   /> 
                                                  <asp:BoundField DataField="Agenda" HeaderText="Detail of Agenda"   /> 
                                                  <asp:BoundField DataField="Observation" HeaderText="Observation"   /> 
                                                  <asp:BoundField DataField="Decision" HeaderText="Decision"   /> 
                                                  <asp:BoundField DataField="ImplementationStatus" HeaderText="Implementation Status"   /> 
                                                
                                                 
                                            </Columns>
                                        </asp:GridView>
     
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
    
     <Triggers>
                       <asp:PostBackTrigger ControlID="btnExportToExcel" />  
        
    </Triggers>
   
</asp:UpdatePanel>
                               </div>
        
        </div>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master"  EnableEventValidation="false"  AutoEventWireup="true" CodeFile="BoardMeetingAuditTrail.aspx.cs" Inherits="MeetingMinors_BoardMeetingAuditTrail" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    
         <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
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

 .autocomplete_completionListElementsss {
            margin: 0px !important;
            background-color: White !important;
            color: windowtext !important;
            border: buttonshadow !important;
            border-width: 1px !important;
            border-style: solid !important;
            cursor: 'default' !important;
            overflow: auto!important;
            font-family: Calibri !important;
            font-size: 12px !important;
            text-align: left !important;
            list-style-type: none !important;
            margin-left: 0px !important;
            padding-left: 0px !important;
            max-height: 200px !important;
            width: 600px !important;

            overflow: auto!important;
            box-shadow: 0 0 3px 1px rgba(0,0,0,.35)!important;
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
                             
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" />  Audit Trail  </h1>
                        </div>
                         <div class="page-heading__container float-right d-none d-sm-block">
                             <asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="vcchomeButton_OnClick" />
                <%--<asp:Button ID="AddNewButton" Text="Add New" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="AddNewButton_OnClick"  />--%>
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
               
                    <div class="col-md-6">
                        <div class="form-group ">
                                          
                          <div class="row">
                               <div class="col-md-2"></div>
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Company:</label></div>
                            <div class="col-md-6">  <asp:DropDownList runat="server"  ID="ddlCompany"  class="form-control form-control-sm" /></div>
                          </div>
                            
                            <div style="padding-top: 5px;"></div>
                             <div class="row" runat="server">
                                  <div class="col-md-2"></div>
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right"> Modify From Date:</label></div>
                            <div class="col-md-6">  <asp:TextBox runat="server"   TextMode="Date"    ID="txtCreatedDate"  class="form-control form-control-sm" /></div>
                          </div>
                          <%--  <div style="padding-top: 5px;"></div>--%>
                            
                            
                             
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
                            
                             
                            </div>
                        </div>
             
                    <div class="col-md-6">
                        <div class="form-group ">
                                          
                          
                                 <div class="row">
                                     
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Operation:</label></div>
                            <div class="col-md-6">
                                    <asp:DropDownList runat="server"    ID="ddlOperation" AutoPostBack="True" OnSelectedIndexChanged="ddlOperation_OnSelectedIndexChanged"    class="form-control form-control-sm" >
                                      <asp:ListItem Value="0">Please Select One...</asp:ListItem>
                                      <%--<asp:ListItem  Value="Board-Meeting">Board-Meeting</asp:ListItem>--%>
                                      <asp:ListItem Value="Document">Document</asp:ListItem>
                                </asp:DropDownList>
                                
                                  <asp:DropDownList runat="server" Visible="False"   ID="ddlCreatedBy"  class="form-control form-control-sm" />
                                
                                  <script type="text/javascript">
                                      function pageLoad() {
                                          $('#<%=ddlCreatedBy.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });
                                          $('#<%=ddlKeySearch.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });

                                      }
               </script>
                            </div>
                          </div>
                          
                            
                            
                             <div style="padding-top: 5px;"></div>
                             <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Modify To Date:</label></div>
                            <div class="col-md-6">  <asp:TextBox runat="server"   TextMode="Date"    ID="txtToDate"  class="form-control form-control-sm" /></div>
                          </div>
                           
                            </div>
                        </div>
             
             
              <div class="col-md-12" runat="server"  >
                        <div class="form-group ">
                                          
                          
                                 <div class="row">
                                      
                               
                                    <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Key Search:</label></div>
                            <div class="col-md-6">
                                <asp:HiddenField runat="server" ID="hfKeySearchId"/>
                                  <asp:TextBox runat="server"    ID="txtKeySearch"  AutoPostBack="True" OnTextChanged="txtKeySearch_OnTextChanged" class="form-control form-control-sm" />
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                        EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                        ServiceMethod="getMiscellaneousKeySearch_Audit" ServicePath="~/WebService.asmx" TargetControlID="txtKeySearch"
                                                        UseContextKey="True" CompletionListCssClass="autocomplete_completionListElementsss"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                        ShowOnlyCurrentWordInCompletionListItem="true">
                                                    </cc1:AutoCompleteExtender>
                                  <asp:DropDownList runat="server"    ID="ddlKeySearch"  Visible="False" class="form-control form-control-sm" />
                            </div>
                               </div>
                            </div>
                                       </div>
              
              
                   <div class="col-md-12" runat="server" Visible="False">
                        <div class="form-group ">
                                          
                          
                                 <div class="row">
                                      
                               
                                    <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Operation:</label></div>
                            <div class="col-md-6">
                                
                                <%--  <asp:TextBox runat="server"    ID="TextBox1"  class="form-control form-control-sm" />
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters=""
                                                        EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                        ServiceMethod="getMeetingKeySearch" ServicePath="~/WebService.asmx" TargetControlID="txtKeySearch"
                                                        UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                        ShowOnlyCurrentWordInCompletionListItem="true">
                                                    </cc1:AutoCompleteExtender>--%>
                              
                            </div>
                               </div>
                            </div>
                                       </div>
             </div>
       
       
            

                                    <div class="row">
                                        <div class="col-md-4">
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
       
       
         
                                <div class="row">
                                     <div class="col-md-8">
                                         
                                     </div>
                                    
                                      <style>.ssss {
                                                                                                                     font-size: 13px;
                                                                                                                     font-weight: bold;
                                                                          
                                               
                                                                                                                 }</style>
                              
                                     <div class="col-md-2"  style="margin-top: 22px; padding: 5px;">
                                       
                                            
                                         
                                             
                               
                                        
                                      
                                     </div>
                             
                               
       
                                       <div class="col-md-2" style="margin-top: 17px;">
                                         <asp:LinkButton ID="btnExportToExcel" runat="server"  CssClass="btnexcel  pull-right" OnClick="btnExportToExcel_Click" ><i class="fa fa-file-excel-o"></i> Export To Excel</asp:LinkButton>
                                         </div>
                                </div>
                                <%-- <div id="tbl_container" style="width:100%" class="">

                    </div>--%>
                                  <br/>
            <asp:GridView Width="100%" ShowHeader="True" ID="gv_ViewList" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Company">
                                                    <ItemTemplate>
                                                         
                                                        <asp:Label ID="lbl_ShortName"   runat="server" Text='<%#Eval("ShortName")%>'></asp:Label>
                                                        <asp:HiddenField runat="server" ID="hfMeetingInfoID" Value='<%#Eval("MeetingInfoID")%>' />
                                                              <asp:HiddenField runat="server" ID="hfActionStatus" Value='<%#Eval("ActionStatus")%>' />

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                      
                                                    <asp:TemplateField HeaderText="Title">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Title" runat="server" Text='<%#Eval("Title") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 
                                                <asp:TemplateField HeaderText="Meeting Category">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_MeetingCategory" runat="server" Text='<%#Eval("MeetingCategory") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                              <asp:TemplateField HeaderText="Meeting Note">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Purpose" runat="server" Text='<%#Eval("MeetingPurpose") %>'></asp:Label>
                                                    </ItemTemplate>
                                                  
                                                      
                                                  
                                                </asp:TemplateField>
                                                
                                                 <asp:BoundField DataField="MeetingDate" HeaderText="Meeting Date" DataFormatString="{0:dd-MMM-yyyy}" /> 
                                                
                                                <%-- <asp:TemplateField HeaderText="Modification By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_CreateByd" runat="server" Text='<%#Eval("DeleteBy") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                       <asp:BoundField DataField="DeleteDate" HeaderText="Modification Date" DataFormatString="{0:dd-MMM-yyyy}" /> --%>
                                                
                                                
                                                
                                                 <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Statusw" runat="server" CssClass='<%#Eval("StatusStyle") %>' Text='<%#Eval("StatusMode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <%--  <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Status" runat="server" CssClass='<%#Eval("StatusStyle") %>' Text='<%#Eval("Status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                --%>
                                     
                                                    
                                                
<%--                                                  <asp:TemplateField HeaderText="Updated By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_UpdateBy" runat="server" Text='<%#Eval("UpdateBy") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                
                                                  <asp:BoundField DataField="UpdateDate" HeaderText="Updated Date" DataFormatString="{0:dd-MMM-yyyy}" /> --%>


                                            
                                                <asp:TemplateField HeaderText="View">
                                                    <ItemTemplate>
                                                        
                                                      <%--  <asp:LinkButton runat="server" ID="btnEdit" OnClick="btnEdit_OnClick"   CssClass="btn btn-sm btn-warning"><i class="fa fa-pencil-square-o"></i> </asp:LinkButton>
                                                      <asp:LinkButton runat="server" ID="btnRemove" OnClick="btnRemove_OnClick"   CssClass="btn btn-sm btn-danger"><i class="fa fa-minus-circle"></i> </asp:LinkButton>--%>
                                                        
                                                        <asp:LinkButton runat="server" ID="btnViewMeeting" OnClick="btnViewMeeting_OnClick"   CssClass="btn btn-sm btn-success"><i class="fa   fa-eye"></i> </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
       
       
       
       
       
         <asp:GridView Width="100%" ShowHeader="True" ID="GridViewDocument" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Company">
                                                    <ItemTemplate>
                                                         
                                                        <asp:Label ID="lbl_ShortName"   runat="server" Text='<%#Eval("ShortName")%>'></asp:Label>
                                                        <asp:HiddenField runat="server" ID="hfMiscellaneousInfoId" Value='<%#Eval("MiscellaneousInfoId")%>' />
                                                              <asp:HiddenField runat="server" ID="hfActionStatusDoc" Value='<%#Eval("ActionStatus")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                 
                                                <asp:TemplateField HeaderText="Title">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_sssssssss" runat="server" Text='<%#Eval("Title") %>'></asp:Label>
                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                              <asp:TemplateField HeaderText="Purpose">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Purpose" runat="server" Text='<%#Eval("Purpose") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Statusw" runat="server" CssClass='<%#Eval("StatusStyle") %>' Text='<%#Eval("StatusMode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    
                                                   <%--<asp:TemplateField HeaderText="Modification By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_CreateBy" runat="server" Text='<%#Eval("DeleteBy") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                
                                                  <asp:BoundField DataField="DeleteDate" HeaderText="Modification Date" DataFormatString="{0:dd-MMM-yyyy}" /> 
                                                
                                                   <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Statusw" runat="server" CssClass='<%#Eval("StatusStyle") %>' Text='<%#Eval("Status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                
                                          <%--        <asp:TemplateField HeaderText="Updated By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_UpdateBy" runat="server" Text='<%#Eval("UpdateBy") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                
                                                  <asp:BoundField DataField="UpdateDate" HeaderText="Updated Date" DataFormatString="{0:dd-MMM-yyyy}" /> --%>


                                                <asp:TemplateField HeaderText="View">
                                                    <ItemTemplate>
                                                        
                                                      <%--  <asp:LinkButton runat="server" ID="btnEdit" OnClick="btnEdit_OnClick"   CssClass="btn btn-sm btn-warning"><i class="fa fa-pencil-square-o"></i> </asp:LinkButton>
                                                      <asp:LinkButton runat="server" ID="btnRemove" OnClick="btnRemove_OnClick"   CssClass="btn btn-sm btn-danger"><i class="fa fa-minus-circle"></i> </asp:LinkButton>--%>
                                                        
                                                        <asp:LinkButton runat="server" ID="btnViewDoc" OnClick="btnViewDoc_OnClick"   CssClass="btn btn-sm btn-success"><i class="fa   fa-eye"></i> </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
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
                 
                 <asp:PostBackTrigger ControlID="btnExportToExcel"/>
             </Triggers>
</asp:UpdatePanel>
                               </div>
        
        </div>
</asp:Content>


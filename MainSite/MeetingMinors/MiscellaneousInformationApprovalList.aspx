<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="MiscellaneousInformationApprovalList.aspx.cs" Inherits="MeetingMinors_MiscellaneousInformationApprovalList" %>

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
                             
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" /> Document Approval List </h1>
                        </div>
                         <div class="page-heading__container float-right d-none d-sm-block">
                         
                <asp:Button ID="AddNewButton" Text="Add New" Visible="False" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="AddNewButton_OnClick"  />
                                <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="vcchomeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                                
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
       
         <div class="row" runat="server" Visible="False">
               
                    <div class="col-md-6">
                        <div class="form-group ">
                                          
                          <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Company:</label></div>
                            <div class="col-md-6">  <asp:DropDownList runat="server"  ID="ddlCompany" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged"  class="form-control form-control-sm" /></div>
                          </div>
                            <div style="padding-top: 5px;"></div>
                            
                            
                             
                                <div class="row" runat="server" Visible="False">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Subject:</label></div>
                            <div class="col-md-6">  <asp:TextBox runat="server"    ID="txtTitle"  class="form-control form-control-sm" /></div>
                          </div>
                            <div style="padding-top: 5px;"></div>

                             <div class="row" runat="server" Visible="False">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Propuse:</label></div>
                            <div class="col-md-6">  <asp:TextBox runat="server"      ID="txtPropuse"  class="form-control form-control-sm" /></div>
                          </div>
                            
                             <div style="padding-top: 5px;"></div>
                            
                              <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Key Search:</label></div>
                            <div class="col-md-9">  <asp:DropDownList runat="server"    ID="ddlKeySearch"  class="form-control form-control-sm" /></div>
                          </div>
                            </div>
                        </div>
             
                    <div class="col-md-6">
                        <div class="form-group ">
                                          
                          
                                 <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Created By:</label></div>
                            <div class="col-md-6">  <asp:DropDownList runat="server"    ID="ddlCreatedBy"  class="form-control form-control-sm" />
                                
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
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Created from Date:</label></div>
                            <div class="col-md-6">  <asp:TextBox runat="server"   TextMode="Date"    ID="txtCreatedDate"  class="form-control form-control-sm" /></div>
                          </div>
                            
                             <div style="padding-top: 5px;"></div>
                             <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Created to Date:</label></div>
                            <div class="col-md-6">  <asp:TextBox runat="server"   TextMode="Date"    ID="txtToDate"  class="form-control form-control-sm" /></div>
                          </div>
                           
                            </div>
                        </div>
             </div>
       
            

                                    <div class="row" runat="server" Visible="False">
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
       
        
                     <style>
        .pagination .page-item.active .page-link  {
            background-color: #007BFF !important;
            border-style: none !important;
            /* W3C */
        }

        
    </style>
   
    
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
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Company">
                                                    <ItemTemplate>
                                                         
                                                        <asp:Label ID="lbl_ShortName"   runat="server" Text='<%#Eval("ShortName")%>'></asp:Label>
                                                        <asp:HiddenField runat="server" ID="hfMiscellaneousInfoId" Value='<%#Eval("MiscellaneousInfoId")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                 
                                                <asp:TemplateField HeaderText="Subject">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Title" runat="server" Text='<%#Eval("Title") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                              <asp:TemplateField HeaderText="Purpose" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Purpose" runat="server" Text='<%#Eval("Purpose") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                   <asp:TemplateField HeaderText="Initiated By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_CreateBy" runat="server" Text='<%#Eval("CreateBy") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                
                                                  <asp:BoundField DataField="CreateDate" HeaderText="Initiated Date" DataFormatString="{0:dd-MMM-yyyy}" /> 
                                                
                                                
                                                
                                                 <%-- <asp:TemplateField HeaderText="Updated By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_UpdateBy" runat="server" Text='<%#Eval("UpdateBy") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                
                                                  <asp:BoundField DataField="UpdateDate" HeaderText="Updated Date" DataFormatString="{0:dd-MMM-yyyy}" /> --%>


                                                <asp:TemplateField HeaderText="Actions">
                                                    <ItemTemplate>
                                                        
                                                        <asp:LinkButton runat="server" ID="btnEdit" OnClick="btnEdit_OnClick"   CssClass="btn btn-sm btn-success">&nbsp; Go to Approve&nbsp;&#8921; </asp:LinkButton>
                                                    <%--  <asp:LinkButton runat="server" ID="btnRemove" OnClick="btnRemove_OnClick"   CssClass="btn btn-sm btn-danger"><i class="fa fa-minus-circle"></i> </asp:LinkButton>
                                                        
                                                        <asp:LinkButton runat="server" ID="btnView" OnClick="btnView_OnClick"   CssClass="btn btn-sm btn-success"><i class="fa   fa-eye"></i> </asp:LinkButton>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
     
           <br/> 
      
              </div>
        
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
                               </div>
        
        </div>
</asp:Content>


<%@ Page Title="" Language="C#" ValidateRequest="false" MasterPageFile="~/MasterPages/MainMasterPage.master" enableEventValidation="false" AutoEventWireup="true" CodeFile="MiscellaneousInformation.aspx.cs" Inherits="MeetingMinors_MiscellaneousInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
        
    
         <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
    
  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/fancybox/3.5.7/jquery.fancybox.css" integrity="sha512-nNlU0WK2QfKsuEmdcTwkeh+lhGs6uyOxuUs+n+0oXSYDok5qy0EI0lt01ZynHq6+p/tbgpZ7P+yUb+r71wqdXg==" crossorigin="anonymous" referrerpolicy="no-referrer" />

<script src="https://cdnjs.cloudflare.com/ajax/libs/fancybox/3.5.7/jquery.fancybox.js" integrity="sha512-j7/1CJweOskkQiS5RD9W8zhEG9D9vpgByNGxPIqkO5KrXrwyDAroM9aQ9w8J7oRqwxGyz429hPVk/zR6IOMtSA==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>

    <style>
      .shrinkToFit {
          width: 100% !important;
          height: 100% !important;
      }
    </style>
    
      <script>
          $(document).ready(function () {
              $(".fancybox").fancybox({
                 
                  'width':1000, // or whatever
                  'height': 700,
                  'type': 'iframe',
                  'autoSize': false
              });

             
          });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    
    <style>
        div#cpFormBody_ddlDivision_chosen {
            width: 100%!important;
        }

        div#cpFormBody_ddlDepartment_chosen {
            width: 100%!important;
        }

        div#cpFormBody_ddlEmp_chosen {
            width: 100%!important;
        }
            
        .Label_Title {
            background-color: #C7C7C7;
            width: 100%;
            text-align: center;
            margin: 0px;
            padding: 5px;
            text-align: center;
            color: #000;
            margin-right: 5%;
            font-weight: bold;
            font-size: 13px;
        }
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
    
    

                           

<div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
   <div class="modal-dialog  modal-lg" role="document">
     <asp:UpdatePanel runat="server">
         <ContentTemplate>
              <div class="modal-content">
         <div class="modal-header">
            
               
                  
                  <h3 class="modal-title" id="exampleModalLabel"  style="color:#2196F3; text-shadow:  0 0 1px black;">Routing Path</h3>
                  
                   
               
           
            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
         </div>
         <div class="modal-body">
                 <div class="row">
                            <div class="col-2">
                                <div class="form-group">
                                     
                                      
                                </div>
                               
                            </div>
               
               
                            <div class="col-md-12">
                                                               
                                                                <div class="Label_Title">Routing Path List</div>
                                                              
                               <br/>
                                <div class="form-group">
                                               <div style="overflow: scroll; height: 230px">
                                    <asp:RadioButtonList ID="rbRoutingPath" CssClass="chkChoice"   repeatColumns="1" RepeatDirection="Horizontal" runat="server"/>
                                                   </div>

                               
                              
                                
                              </div>


                            </div>
                            
                         

                        </div>
             
              
         </div>
         <div class="modal-footer"><asp:LinkButton  runat="server" class="btn btn-success" OnClick="btnOkayRoutingPath_OnClick" ID="btnOkayRoutingPath"  >Okay</asp:LinkButton><button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button> </div>
      </div>
         </ContentTemplate>
     </asp:UpdatePanel>
   </div>
</div>
    
    <div class="modal fade" id="exampleModal2" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
   <div class="modal-dialog  modal-lg" style=" width: 90% !important; " role="document">
  <asp:UpdatePanel runat="server">
      <ContentTemplate>
              <div class="modal-content">
         <div class="modal-header">
            
               
                  
                  <h3 class="modal-title" id="exampleModalLabel2"  style="color:#2196F3; text-shadow:  0 0 1px black;">Add Employee </h3>
                  
                   
               
           
            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
         </div>
         <div class="modal-body">
                 <div class="row">
                            <div class="col-md-12">
                               
           <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;">Search Employee</h2>
                                
                                 <div class="row" runat="server" Visible="False">
                                         <div class="col-md-3" style="padding-top: 8px">  <label class="control-label" style="font-weight: bold">Company:</label></div>
                              <div class="col-md-6">
                                  
                                   <asp:DropDownList runat="server"   ID="ddlComSearch" AutoPostBack="True" OnSelectedIndexChanged="ddlComSearch_OnSelectedIndexChanged"  class="form-control form-control-sm" />
                                  
                                 
                                  </div>
                                     </div>
                                
          <div class="row" runat="server" Visible="False">
               
                            
               <div class="col-md-3" style="padding-top: 8px" >  <label  style="font-weight: bold" class="control-label">Division:</label></div>
                                <div class="col-md-6">  <asp:DropDownList runat="server"   ID="ddlDivision"   OnSelectedIndexChanged="ddlDivision_OnSelectedIndexChanged" AutoPostBack="True"  class="form-control form-control-sm SelectMe33" ></asp:DropDownList> 
                                    
                                      <%--<script type="text/javascript">
                                          function pageLoad() {
                                              $('#<%=ddlDivision.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });

                         $('#<%=ddlDepartment.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });
                     }
               </script>--%>

                                </div>
              </div>
                                     <br/>
          <div class="row" runat="server" Visible="False">
              <div class="col-md-3" style="padding-top: 8px">  <label   style="font-weight: bold" class="control-label ">Department:</label></div>
                                <div class="col-md-6">  <asp:DropDownList runat="server"   ID="ddlDepartment"  class="form-control form-control-sm SelectMe33" ></asp:DropDownList></div>
           
                 
                            </div>
                                
                                
                                <div class="row">
                                         <div class="col-md-3" style="padding-top: 8px">  <label class="control-label" style="font-weight: bold" >Employee Name:</label></div>
                              <div class="col-md-6">
                                  
                                   <asp:DropDownList runat="server"   ID="ddlEmp"  style="width: 300px !important"   class="form-control form-control-sm SelectMe33" />
                                  </div>
                                    
                                
                                    
                                      
                                     </div>
                                
                                    <br/>
                                <div class="row">
                                     <div class="col-md-3"></div>
                                         <div class="col-md-2">
                       <asp:LinkButton runat="server" ID="LinkButton1"   CssClass="btn btn-success   btn-sm" OnClick="btnSearch_OnClick"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search </asp:LinkButton>
              </div>
                                         </div>
       <br/>
       
                                
                              <div class="row">
                                  <div class="col-md-12">
                                        <asp:GridView Width="100%" ShowHeader="True" ID="gv_EmpListSearch" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkSelectAll" runat="server" CssClass="form-control-sm" AutoPostBack="True" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" CssClass="form-control-sm" AutoPostBack="True" OnCheckedChanged="chkSelect_OnCheckedChanged"  runat="server" />
                                                
                                                 <asp:HiddenField runat="server" ID="hfEmpInfoId" Value='<%#Eval("EmpInfoId")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                 
                                                <asp:TemplateField HeaderText="Employee ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_EmpMasterCode" runat="server" Text='<%#Eval("EmpMasterCode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                             
                                                 <asp:TemplateField HeaderText="Employee Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_EmpName" runat="server" Text='<%#Eval("EmpName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                 <asp:TemplateField HeaderText="Designation">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Designation" runat="server" Text='<%#Eval("Designation") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                  <asp:TemplateField HeaderText="Division" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_DivisionName" runat="server" Text='<%#Eval("DivisionName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                 <asp:TemplateField HeaderText="Department"  Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_DepartmentName" runat="server" Text='<%#Eval("DepartmentName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                
                                            </Columns>
                                        </asp:GridView>
                                  </div>
                              </div>
                                                        
      
       
              <br/>
       <div class="row">
           <div class="col-4"></div>
           <div class="col-4"></div>
           <div class="col-4">
               <asp:LinkButton runat="server" CssClass="btn btnMyDesignAddtoList btn-sm" ID="btnAddToListEmp" OnClick="btnAddToListEmp_OnClick"><span aria-hidden="true" class="fa fa-plus"></span>  &nbsp;Add To List</asp:LinkButton>
           </div>
       </div>
                            </div>
               
               
                   
                            
                         

                        </div>
             
              
         </div>
         <div class="modal-footer"> <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button> </div>
      </div>
      </ContentTemplate>
  </asp:UpdatePanel>
   </div>
</div>
     <div class="content" id="content">
                 <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                             
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" /> Document Information   </h1>
                        </div>
                                            <div class="page-heading__container float-right d-none d-sm-block">
                                                <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                        <asp:LinkButton ID="detailsViewButton"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
                    </div>
                    </div>
                     
<div class="card">
   <div class="card-body">
       
       <asp:UpdatePanel runat="server">
    <ContentTemplate>
        
         <asp:UpdateProgress ID="UpdateProgress4" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait11" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
       <asp:HiddenField runat="server" ID="id_mastetID"/>
         <div class="row">
             
                    <div class="col-md-12">
                        
                        
                        
                                        <div class="form-row">

                                            <div class="col-2">
                                                <div class="form-group">
                                                    <label>Company</label>
                                                    <label style="color: #a52a2a">*</label>
                                                   <asp:DropDownList runat="server"   ID="ddlCompany" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged"  class="form-control form-control-sm" />
                                                    
                                                            <script type="text/javascript">
                                                                function pageLoad() {

                                                                    $('.SelectMe33').chosen({ disable_search_threshold: 5, search_contains: true });

                                                                    $('.SelectMe').chosen({ disable_search_threshold: 5, search_contains: true });
                                                                }
</script>
                                                </div>
                                            </div>
                                            
                                                 <div class="col-5">
                                                <div class="form-group">
                                                    <label>Subject</label>
                                                    <label style="color: #a52a2a">*</label>
                                                 <asp:TextBox runat="server"    ID="txtTitle"  class="form-control" TextMode="MultiLine" Rows="2" />
                                                </div>
                                            </div>
                                            
                                            
                                            
                                                 <div class="col-5">
                                                <div class="form-group">
                                                    <label>Purpose</label>
                                      
                                              <asp:TextBox runat="server"   TextMode="MultiLine" Rows="2"    ID="txtPurpose"  class="form-control  " />
                                                </div>
                                            </div>
                                            </div>
                        <div class="form-group " runat="server" Visible="False">
                                   
                        
                            <div class="row">
                                <div class="col-md-2" style="padding-top: 8px">  <label class="control-label pull-right">Company:<span   style="color:red; " title="please fill out this field"> * </span></label></div>
                                <div class="col-md-2"> 
                                    
                                       
                                </div>
                                <div class="col-md-2" style="padding-top: 8px">  <label class="control-label pull-right">Subject:<span   style="color:red; " title="please fill out this field"> * </span></label></div>
                                <div class="col-md-2">  </div>
                               <div class="col-md-2" style="padding-top: 8px">  <label class="control-label pull-right">Purpose: </label></div>
                                <div class="col-md-2">  </div>
                            </div>
                              <div style="padding-top: 5px;"></div>
                          <%--  <div style="padding-top: 5px;"></div>--%>
                            <div class="row">
                                
                            </div>
                              <div style="padding-top: 5px;"></div>
                            <div class="row">
                                
                            </div>
                            
                       


                            </div>
                        </div>
             </div>
       
       
      
                                             
           <fieldset class="for-panel">
                                <legend>Document </legend>
                                        <div class="row">
                                           


    
  
                                            <asp:HiddenField runat="server" ID="hfDocFileName"/>
                                            <asp:HiddenField runat="server" ID="hfDocFile"/>
                                            <div class="col-4">
                                                <div class="form-group">
                                                    <label>Document Upload<span   style="color:red; " title="please fill out this field"> * </span> <span style="color: gray!important">Supported Files are:[jpg, png,xlsx,pdf,txt,doc,docx]</span></label>
                                                    <div>
   <input type="file" name="postedFile" id="upImage" onchange="showpreview(this)" class="form-control form-control-sm" />
  <asp:FileUpload ID="FUDocument" Visible="False" CssClass="form-control form-control-sm" runat="server"   />
                                                         <br/>
                                                        <input type="button"  class="btn btn-sm  btn-info" id="btnUpload" value="Upload Document" />
                                                          <asp:LinkButton  runat="server" Visible="False" OnClick="btnDocUp_OnClick" ID="btnDocUp"  CssClass="btn btn-sm  btn-info">
                                                          
                                                      
              &nbsp;    <span class="btn-label"><i class="fa fa-upload"></i></span>  &nbsp;   &nbsp;Upload Document
                                                          </asp:LinkButton>
                                                        <br/>
                                                          <progress id="fileProgress" style="display: none"></progress>
                                                            <br/>
                                             <span id="lblMessage" style="color: Green"></span>
                                         <asp:Label runat="server" ID="lblMsg" style="color: Green"></asp:Label>
                                                        <asp:HyperLink Visible="False" ID="HyperLink2" runat="server"
    
     Target="_blank" ToolTip="Click to Show Document"></asp:HyperLink> 
                                                           
                                                        
                                                       
                                                    </div>
                                                </div>
                                            </div>
                                            
                                            
                                            <div class="col-4">
                                             <div class="form-group">
                                                    <label>Short Description<span   style="color:red; " title="please fill out this field"> * </span></label>
                                                    <div>

                                                     <asp:TextBox runat="server"   ID="txtSummaryNote"  TextMode="MultiLine" Rows="2"    class="form-control" />
                                                         
                                                    </div>
                                                </div>
                                                
                                                  <div class="form-group">
                                                       <asp:LinkButton runat="server" ID="brnAddDoc"   OnClick="brnAddDoc_OnClick"    CssClass="btn btnMyDesignAddtoList   btn-sm pull-right"><span aria-hidden="true" class="fa fa-plus"></span>  &nbsp; Add to List </asp:LinkButton> 
                                                      </div>
                                            </div>

                                        </div>
               <br/>
                                                <div class="row">
                                                    <div class="col-md-8">
                                                        <%--https://docs.google.com/gview?url=--%>
                                                    
                                                          <asp:GridView Width="100%" ShowHeader="True" ID="gv_DocumentUpload" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Document">
                                                    

                                                    <ItemTemplate>
                                                         <a class="btn btn-sm btnMyDesignSearch"     Target="_blank"   href="<%# Eval("DocumentLinkPreview") %>">Preview</a>
                                                       
                                                         

                                                          <asp:HyperLink ID="HLDocumentLink"   Visible="False" Target="_blank"  runat="server" NavigateUrl='<%# Eval("DocumentLink") %>'  CssClass="btn btn-sm btnMyDesignSearch"  Text='Preview' >
        </asp:HyperLink>
                                                        
                                                  <%--      <iframe width="500" src="https://docs.google.com/gview?url=http://localhost:30407/UploadMeetingDocument/0e5683f4f4434501b34695c4f0b38400.doc&embedded=true" > </iframe>
                                                        
                                                        <a href="<%# Eval("DocumentLink") %>" class="fancybox"  > ssss </a>--%>
                                                        
                                                <%--        <a   Target="_blank"    href="<%# Eval("DocumentLink")%>">Download</a>--%>
                                                        <asp:Label ID="lbl_DocumentLink" Visible="False" runat="server" Text='<%#Eval("DocumentLink")%>'></asp:Label>
                                                        <asp:HiddenField runat="server" ID="hfFileName" Value='<%#Eval("FileName")%>' />
                                                        <asp:HiddenField runat="server" ID="hfDocumentLinkPreview" Value='<%#Eval("DocumentLinkPreview")%>' />
                                                        <asp:HiddenField runat="server" ID="hfDocumentLink" Value='<%#Eval("DocumentLink")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="File Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_FileName" runat="server" Text='<%#Eval("FileName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 
                                                <asp:TemplateField HeaderText="Short Description	">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_DocumentNote" runat="server" Text='<%#Eval("DocumentNote") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                             

                                                <asp:TemplateField HeaderText="Remove">
                                                    <ItemTemplate>
                                                      <asp:LinkButton runat="server" ID="btnDocRemove" OnClick="btnDocRemove_OnClick"   CssClass="btn btn-sm btn-danger"><i class="fa fa-minus-circle"></i> </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                                        
                                                
                                                    </div>
                                                    </div>
                                               </fieldset>
       
   
        
        
           <div class="row" style="padding-top:10px" runat="server" Visible="False">
                 <div class="col-md-12">
                      <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;">Employee List</h2>
                      <asp:GridView Width="100%" ShowHeader="True" ID="gv_Member" runat="server" AutoGenerateColumns="false" CssClass="blueTableNew"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                           
                                                 <asp:TemplateField HeaderText="Type">
                                                    <ItemTemplate>
                                                     <asp:RadioButtonList runat="server"  ID="rbType" CssClass="chkChoice" AutoPostBack="True" OnSelectedIndexChanged="rbType_OnSelectedIndexChanged" RepeatDirection="Horizontal">
                                                               
                
                 <asp:ListItem>Employee</asp:ListItem>
                    <asp:ListItem>Guest</asp:ListItem>
                                                           </asp:RadioButtonList>
                                                        
                                                        <asp:HiddenField runat="server" ID="hfType" Value='<%#Eval("Type")%>' />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 
                                                <asp:TemplateField HeaderText="Employee ID">
                                                    <ItemTemplate>
                                                        <asp:TextBox ReadOnly="True" ID="txt_EmpMasterCode" CssClass="form-control form-control-sm" runat="server" Text='<%#Eval("EmpMasterCode") %>'></asp:TextBox>
                                                        
                                                        <asp:HiddenField runat="server" ID="MemEmpInfoId" Value='<%#Eval("EmpInfoId")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                             
                                                 <asp:TemplateField HeaderText="Employee Name">
                                                    <ItemTemplate>
                                                        
                                                          <asp:DropDownList Width="45%" ID="ddlEmpName" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpName_OnSelectedIndexChanged" Visible="False"  CssClass="form-control form-control-sm SelectMe"  runat="server"  ></asp:DropDownList>

                                                        <asp:TextBox ID="txt_EmpName" Visible="False"  CssClass="form-control form-control-sm"  runat="server" Text='<%#Eval("EmpName") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                 <asp:TemplateField HeaderText="Designation">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_Designation"  CssClass="form-control form-control-sm"  runat="server" Text='<%#Eval("Designation") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                            
                                                <asp:TemplateField HeaderText="Actions">
                                                    <ItemTemplate>
                                                        
                                                        <asp:LinkButton runat="server" ID="btn_gv_MemberAdd"   OnClick="btn_gv_MemberAdd_OnClick" CssClass="btn btn-sm btn-success"><i class="fa fa-plus"></i> </asp:LinkButton> 
                                                      <asp:LinkButton runat="server" ID="btn_gv_Member" OnClick="btn_gv_Member_OnClick"   CssClass="btn btn-sm btn-danger"><i class="fa fa-minus-circle"></i> </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                
                                       
                                                
                                                
                                                
                                            </Columns>
                                        </asp:GridView>
                     </div>
            </div>
      <br/>
        
        
     
         <div class="row" style="padding-top: 25px">
                   <br/>
        <br/>
        <br/>
                            <div class="col-md-3">
                                <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;">Approval Path Setup</h2>
                         </div>
          
           
             
             </div>
        <div class="row">
              <div class="col-md-2">
                                <div class="form-group">
                                     
                                     <asp:LinkButton runat="server" ID="LinkButtaon2"  data-toggle="modal" data-target="#exampleModal"  CssClass="btn btn-sm btn-success">Choose Approval Path  </asp:LinkButton>
                                </div>
                               
                            </div>
             
                <div class="col-md-2">
                                <div class="form-group">
                                     
                                     <asp:LinkButton runat="server" ID="LinkButton3"  data-toggle="modal" data-target="#exampleModal2"  CssClass="btn btn-sm btnMyDesignOne">Create Approval Path </asp:LinkButton>
                                </div>
                               
                            </div>
        </div>
       
      
       
        <div class="row">

           
           <div class="col-md-12">
                    
                           <asp:GridView Width="100%" ShowHeader="True" ID="gv_Details_Save" runat="server" AutoGenerateColumns="false" CssClass="blueTableNew"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                            <asp:HiddenField runat="server"  ID="hfIsMinimumApprovalPerson" Value='<%#Eval("IsMinimumApprovalPerson")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                           <asp:TemplateField HeaderText="Mandatory Approver" Visible="False">
                                                    <ItemTemplate>
                                                <asp:CheckBox ID="chkMimimumCount" AutoPostBack="True" OnCheckedChanged="chkMimimumCount_OnCheckedChanged" Checked="True" CssClass="form-control-sm"   runat="server" />
                                              
                                                 
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Approval Sequence">
                                                    <ItemTemplate>
                                                      <asp:DropDownList runat="server" ID="ddlSequenceList" class="form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="ddlSequenceList_OnSelectedIndexChanged" >
                    
                                                           </asp:DropDownList>
                                                        <asp:HiddenField runat="server" ID="ShfEmpInfoId" Value='<%#Eval("EmpInfoId")%>' />
                                                        <asp:HiddenField runat="server" ID="hfSeq_No" Value='<%#Eval("Seq_No")%>' />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 
                                                <asp:TemplateField HeaderText="Employee ID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Slbl_EmpMasterCode" runat="server" Text='<%#Eval("EmpMasterCode") %>'></asp:Label>
                                                        
                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                             
                                                 <asp:TemplateField HeaderText="Employee Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Slbl_EmpName" runat="server" Text='<%#Eval("EmpName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                 <asp:TemplateField HeaderText="Designation">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Slbl_Designation" runat="server" Text='<%#Eval("Designation") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Division">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Slbl_DivisionName" runat="server" Text='<%#Eval("DivisionName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                 <asp:TemplateField HeaderText="Department">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Slbl_DepartmentName" runat="server" Text='<%#Eval("DepartmentName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                 
                                                
                                               <asp:TemplateField HeaderText="Is Edit">
                                                    <ItemTemplate>
                                                <asp:CheckBox ID="chkIsEdit" CssClass="form-control-sm"  AutoPostBack="True" OnCheckedChanged="chkIsEdit_OnSelectedIndexChanged"    runat="server" />
                                                
                                                   <asp:HiddenField runat="server" ID="hfCanEdit" Value='<%#Eval("CanEdit")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                              
                                                
                                                
                                           <asp:TemplateField HeaderText="Notification">
                                                    <ItemTemplate>
                                                
                                                
                                                 
                                                   <asp:HiddenField runat="server" ID="HiNotificationEmail" Value='<%#Eval("NotificationEmail")%>' />
                                                        <asp:HiddenField runat="server" ID="hfNotificationSMS" Value='<%#Eval("NotificationSMS")%>' />

                                          
                                                <asp:CheckBoxList runat="server" ID="chkNotification" AutoPostBack="True" OnSelectedIndexChanged="chkNotificationApp_OnSelectedIndexChanged" CssClass="chkChoice" RepeatDirection="Horizontal">
                                                               
                
                 <asp:ListItem>Email</asp:ListItem>
                    <asp:ListItem>SMS</asp:ListItem>
                                                           </asp:CheckBoxList>
                                                 
                                                 
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                                
                                                  <asp:TemplateField HeaderText="Remove">
                                                    <ItemTemplate>
                                                      <asp:LinkButton runat="server" ID="btn_DetailsRemove" OnClick="btn_DetailsRemove_OnClick"   CssClass="btn btn-sm btn-danger"><i class="fa fa-minus-circle"></i> </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
               
       
       <br/>
               </div>
       
       </div>
     
       <div class="row">
          
         
           <div class="col-md-12">
               <div class="form-row">
                  
                   <div class="col-md-8">
                       
                         <asp:Label runat="server" ID="lblstatus" Text="No Approval Path have been  selected." ForeColor="Red" Font-Size="17px"></asp:Label>
                       <br/>
                       <br/>
                        <link href="../UserSetup/ButtonGrups.css" rel="stylesheet" />
                                      <div class="ui-group-buttons">
                                          
                                           <asp:LinkButton ID="lbDraft"  OnClick="lbDraft_OnClick" Visible="False"  OnClientClick="return confirm('Are you sure you want to Draft ?')" CssClass="btn btn-sm btn-success"  runat="server"> 
 &nbsp;Draft</asp:LinkButton>
                                             <div class="or or-sm" runat="server"   id="orBTN"></div>

                       <asp:LinkButton ID="submitButton"  OnClick="btnSave_OnClick" Visible="False"  OnClientClick="return confirm('Are you sure you want to Submit ?')" CssClass="btn btn-sm btn-info"  runat="server"> 
 &nbsp;Submit</asp:LinkButton>
                       
                       
                        <asp:LinkButton ID="editButton" OnClientClick="return confirm('Are you sure you want to Update ?')"  CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="editButton_OnClick"> &nbsp; Update & Submit</asp:LinkButton>
                                    <asp:LinkButton ID="delButton" OnClientClick="return confirm('Are you sure you want to Delete ?')" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" ><i class="fa fa-trash-o" aria-hidden="true"></i>&nbsp; Delete</asp:LinkButton>
                                       </div>
                   </div>
                  
               </div>
               <div class="form-group">
               </div>
           </div>

           <div class="col-md-4">
           </div>
       </div>
       
       
       
         </ContentTemplate>
           
          
           
           <Triggers>  
  
         <asp:PostBackTrigger ControlID="btnDocUp" />  
  
</Triggers> 
</asp:UpdatePanel>

       
      
              </div>
        
        </div>
                               </div>
        
        </div>
  

   <script type="text/javascript">
       $("body").on("click", "#btnUpload", function () {
           if ($("#upImage").val() != '') {
           $.ajax({
               url: '/HandlerDoc.ashx',
               type: 'POST',
               data: new FormData($('form')[0]),
               cache: false,
               contentType: false,
               processData: false,
               success: function (file) {
                   $("#cpFormBody_hfDocFile").val('');
                   $("#cpFormBody_hfDocFileName").val('');
                   $("#fileProgress").hide();
                   $("#lblMessage").html("<b>" + file.name + "</b> has been uploaded.");
                   $("#cpFormBody_hfDocFile").val(file.dbfilename);
                   $("#cpFormBody_hfDocFileName").val(file.name);
                 
               },
               xhr: function () {
                   var fileXhr = $.ajaxSettings.xhr();
                   if (fileXhr.upload) {
                       $("progress").show();
                       fileXhr.upload.addEventListener("progress", function (e) {
                           if (e.lengthComputable) {
                               $("#fileProgress").attr({
                                   value: e.loaded,
                                   max: e.total
                               });
                           }
                       }, false);
                   }
                   return fileXhr;
               }
           });
           } else {
               alert("Please Upload a Document!!!");
           }
       });
    </script>
    
    
    
    
      <script type="text/javascript">
          function showpreview(input) {


              debugger;
              //$('#ContentPlaceHolder1_imageFileUpload').val($(this).val().toLowerCase());
              var validExtensions = [
                  'jpg', 'png', 'JPG', 'PNG',  'XLSX', 'xlsx', 'PDF', 'pdf', 'TXT', 'txt', 'DOC', 'doc', 'DOCX', 'docx']; //array of valid extensions
              var fileName = input.files[0].name;
              var fileNameExt = fileName.substr(fileName.lastIndexOf('.') + 1);
              if ($.inArray(fileNameExt, validExtensions) == -1) {
                  input.type = '';
                  input.type = 'file';

                  alert("Only these file types are accepted :  jpg, png,xlsx,pdf,txt,doc,docx");
              }
              else {

                  var picsize = (input.files[0].size);
                  if (picsize > 5000000) {
                      input.type = '';
                      input.type = 'file';

                      alert("File Size is not accepted");

                  } else {

                      if (input.files && input.files[0]) {


                          var filerdr = new FileReader();
                          filerdr.onload = function (e) {

                          }
                          filerdr.readAsDataURL(input.files[0]);
                      }

                  }


              }

              //if (input.files && input.files[0]) {

              //    var reader = new FileReader();
              //    reader.onload = function (e) {
              //        $('#imgpreview').css('visibility', 'visible');
              //        $('#imgpreview').attr('src', e.target.result);
              //    }
              //    reader.readAsDataURL(input.files[0]);
              //}

          }

        </script>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="ExecutiveOfficeDocUpload.aspx.cs" Inherits="HealthCare_UI_ExecutiveOfficeDocUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
    
    

                           


     <div class="content" id="content">
                 <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                             
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" /> Executive Office Document Upload   </h1>
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
                                                   <asp:DropDownList runat="server"   ID="ddlCompany"  class="form-control form-control-sm" />
                                                    
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
                                                    <label>Category</label>
                                                    <label style="color: #a52a2a">*</label>
                                               
                                                    <asp:DropDownList runat="server"   ID="ddlcategory"  class="form-control form-control-sm" />
                                                </div>
                                            </div>
                                            
                                            
                                            
                                                 <div class="col-5">
                                                <div class="form-group">
                                                    <label>Remarks</label>
                                      
                                              <asp:TextBox runat="server"   TextMode="MultiLine" Rows="2"    ID="txtPurpose"  class="form-control  " />
                                                </div>
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
       
   
        
        
      <br/>
        
        
               <div class="row">
          
         
           <div class="col-md-12">
               <div class="form-row">
                  
                   <div class="col-md-8">
                       

      
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


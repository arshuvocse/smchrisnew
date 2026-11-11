<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="EmpExitEntry.aspx.cs" Inherits="Survey_EmpExitEntry" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
      <style type="text/css">
        /*AutoComplete flyout */
      
    </style>
    <style>
        .checkboxlist_nowrap {
            display: inline;
        }
        .cls-SelectJq {
            
        }

           fieldset.for-panel {
               background-color: #fcfcfc;
               border: 1px solid #999;
               padding: 15px 10px;
               background-color: white;
               margin-bottom: 12px;
           }

           .buttonFinish {
               display: none !important;
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
    
         <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
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
                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                             
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" /> Clearance Form Setup</h1>
                        </div>
                                            <div class="page-heading__container float-right d-none d-sm-block">
                                                   <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                        <asp:LinkButton ID="detailsViewButton"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
                  
 
                    </div>
                    </div>

                    <div class="card">
                        <div class="card-body">
                            <fieldset class="for-panel">
                                <%--<legend>Search By</legend>--%>
                                <div class="form-row">
                                    <div class="col-3">
                                        <div class="form-group ">
                                            <label class="control-label">Company</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" class="form-control form-control-sm" />
                                        </div>
                                    </div>

                                    <div class="col-4">
                                        <div class="form-group ">
                                            <label class="control-label">Search Employee Name</label>

                                              <asp:DropDownList   runat="server"   ID="ddlEmpInfo" AutoPostBack="True" OnTextChanged="ddlEmpInfo_OnTextChanged" class="form-control form-control-sm" />
                                                    <script type="text/javascript">
                                                        function pageLoad() {
                                                            try {
                                                                $('#<%=ddlEmpInfo.ClientID%>').chosen('destroy');
                                                            } catch (e) { }
                                                            $('#<%=ddlEmpInfo.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true, width: '100%' });

                                                            try {
                                                                $('.cls-SelectJq').chosen('destroy');
                                                            } catch (e) { }
                                                            $('.cls-SelectJq').chosen({ disable_search_threshold: 5, search_contains: true, width: '100%' });
                                                            $('.cls-SelectJq').trigger('chosen:updated');
                                                        }
                                                    </script>
                                            <asp:TextBox Visible="False" ID="txt_EmpName" runat="server" AutoPostBack="True" Enabled="False" CssClass="form-control form-control-sm" OnTextChanged="txt_EmpName_OnTextChanged"></asp:TextBox>
                                            <asp:HiddenField runat="server" ID="hfEmpInfoId" />
                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                ServiceMethod="GetExitImployee" ServicePath="~/WebService.asmx" TargetControlID="txt_EmpName"
                                                UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                ShowOnlyCurrentWordInCompletionListItem="true">
                                            </cc1:AutoCompleteExtender>
                                        <%--    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                ServiceMethod="GetExitImployee" ServicePath="~/WebService.asmx" TargetControlID="txt_EmpName"
                                                UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                ShowOnlyCurrentWordInCompletionListItem="true">
                                            </cc1:AutoCompleteExtender>--%>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                    

                            <div class="form-row" runat="server" Visible="False">

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Employee Code</label><span style="color: red">&nbsp;*</span>
                                        <asp:Label runat="server" ID="empCode" class="form-control form-control-sm" />
                                        <asp:HiddenField ID="HiddenField2" runat="server" />
                                        <asp:HiddenField ID="deptHiddenField" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Employee Name</label><span style="color: red">&nbsp;*</span>
                                        <br />
                                        <asp:Label runat="server" ID="empName" class="form-control form-control-sm" />
                                        <asp:HiddenField ID="HiddenField" runat="server" />

                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Date of Joining</label><span style="color: red">&nbsp;*</span>
                                        <asp:Label runat="server" ID="dtJoining" class="form-control form-control-sm" />
                                        <asp:HiddenField ID="HiddenField3" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Division</label><span style="color: red">&nbsp;*</span>
                                        <asp:Label runat="server" ID="ddlDivision" class="form-control form-control-sm" />
                                        <asp:HiddenField ID="hfDivision" runat="server" />

                                    </div>
                                </div>
                            </div>

                            <div class="form-row" runat="server" Visible="False">
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Designation</label><span style="color: red">&nbsp;*</span>
                                        <asp:Label runat="server" ID="ddlDesignation" class="form-control form-control-sm" />
                                        <asp:HiddenField ID="hfDesignation" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3" style="display: none">
                                    <div class="form-group">
                                        <label>Slary Grade</label><span style="color: red">&nbsp;*</span>
                                        <asp:Label runat="server" ID="ddlSalaryGrade" class="form-control form-control-sm" />
                                        <asp:HiddenField ID="hfSalaryGrade" runat="server" />
                                    </div>
                                </div>
                                
                                <div class="col-6">
                                    <div class="form-group">
                                        <label> Description </label>
                                        <asp:TextBox runat="server" ID="descriptionTextbox" TextMode="MultiLine" Rows="2"  class="form-control" />
                                    </div>
                                </div>
                            </div>
                            
                            
                            
                                <div class="row">
                                <style>
                                    .tblTHColorChang{
                                        background-color: #EDF2F5!important;
                                        font-weight: bold;
                                        font-size: 13px;
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
  .chkChoiceStep label {
            padding-left: 5px;
            padding-right: 5px;
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
                                     
                                     <table class="table table-bordered table-striped">
                                                    <tr>
                                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Employee ID</td>
                                                        <td> <asp:Label runat="server" ID="lblEmpId"></asp:Label></td>

                                                        
                                                         <td  class="tblTHColorChang" style="width: 20%; padding: 10px;">Supervisor</td>
                                                        <td>  <asp:Label ID="ReportingLabel"  runat="server"></asp:Label></td>
                                                    </tr>
                                                    
                                                     <tr>
                                                        <td  class="tblTHColorChang" style="width: 20%; padding: 10px;">Employee Name</td>
                                                        <td> <asp:Label runat="server" ID="lblEmployeeName"></asp:Label></td>

                                                         
                                                              
                                                         <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Department</td>
                                                        <td>  <asp:Label ID="deptNameLabel"  runat="server"></asp:Label></td>
                                                    </tr>
                                                    
                                                     <tr>
                                                        <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Designation</td>
                                                        <td>   <asp:Label ID="desigNameLabel" runat="server"></asp:Label></td>

                                                     <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Office</td>
                                                        <td>  <asp:Label ID="LocationLabel"   runat="server"></asp:Label></td>

                                                    </tr>
                                                    
                                                    
                                                    
                                                    
                                                     
                                                    
                                                     <tr>
                                                        <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Date Of Joining</td>
                                                        <td>     <asp:Label ID="joiningDateLabel"  runat="server"></asp:Label></td>
  <td style="width: 20%; padding: 10px;" class="tblTHColorChang" >Place</td>
                                                        <td> <asp:Label runat="server" ID="lblPlace"></asp:Label></td>
                                                    </tr>
                                                    
                                                    
                                                 
                                                    
                                                    
                                                    </table>
                            </div>
                            

                            <div id="exitReasonGridViewa" style="height: auto;">
                                <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;"> Supervisor Information</h2>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                      CssClass="AddToListCssTable" OnPreRender="gv_DocumentUpload_PreRender" DataKeyNames="DepartmentId,EmpInfoId">
                                    <Columns>

                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        

                                        <asp:BoundField DataField="EmpName" HeaderText="Employee Info" />

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                              
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkEmpSelect" Checked="True"  CssClass="form-control-sm"  runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                            <div id="exitReasonGridView" style="height: auto;">
                                
                                 <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;"> Department List</h2>
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                     CssClass="AddToListCssTable" OnPreRender="gv_DocumentUpload_PreRender" DataKeyNames="DepartmentId,SetInfo">
                                    <Columns>

                                        <asp:TemplateField HeaderText="SL">
                                            <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkSelectAll" runat="server" CssClass="form-control-sm" AutoPostBack="True" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" OnCheckedChanged="chkSelect_OnCheckedChanged" CssClass="form-control-sm" AutoPostBack="True" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="DepartmentName" HeaderText="Department">
                                            <HeaderStyle Width="25%" />
                                            <ItemStyle Width="25%" />
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="Employee Name">
                                            <HeaderStyle Width="65%" />
                                            <ItemStyle Width="65%" />
                                            <ItemTemplate>
                                                
                                                    <asp:ListBox   runat="server"   ID="ddlEmpInfoList"   SelectionMode="Multiple"   Enabled="False"  CssClass="form-control form-control-sm cls-SelectJq" data-placeholder="Choose Employee(s)..." />
                                                <asp:TextBox Visible="False" ID="employeeTextBox" runat="server" AutoPostBack="True" OnTextChanged="employeeTextBox_OnTextChanged" CssClass="form-control-sm" Width="100%" ReadOnly="True"></asp:TextBox>
                                                <asp:HiddenField ID="hdfEmpInfoId" runat="server" />
                                                <cc1:AutoCompleteExtender ID="empAutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                    EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                    ServiceMethod="GetExitImployeeDept" ServicePath="~/WebService.asmx" TargetControlID="employeeTextBox"
                                                    UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                    ShowOnlyCurrentWordInCompletionListItem="true">
                                                </cc1:AutoCompleteExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            
                            <DIV class="row">
                                <div class="col-md-2"></div>
                                <div class="col-md-8">
                                          <fieldset class="for-panel">
                                <legend>Document </legend>
                                        <div class="row">
                                           


    
  
                                            <asp:HiddenField runat="server" ID="hfDocFileName"/>
                                            <asp:HiddenField runat="server" ID="hfDocFile"/>
                                            <div class="col-6">
                                                <div class="form-group">
                                                    <label>Document Upload<span   style="color:red; " title="please fill out this field"> * </span></label>
                                                    <div>
   <input type="file" name="postedFile"  onchange="showpreview(this);" class="form-control form-control-sm" />
  <asp:FileUpload ID="FUDocument" Visible="False" CssClass="form-control form-control-sm" runat="server"   />
                                                         <br/>
                                                        <input type="button"  class="btn btn-sm  btn-info" id="btnUpload" value="Upload Document" />
                                                          <asp:LinkButton  runat="server" Visible="False"   ID="btnDocUp"  CssClass="btn btn-sm  btn-info">
                                                          
                                                      
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
                                            
                                            
                                            <div class="col-6">
                                             <div class="form-group">
                                                    <label>Summary Note </label>
                                                    <div>

                                                     <asp:TextBox runat="server"   ID="txtSummaryNote"  TextMode="MultiLine" Rows="2"    class="form-control" />
                                                         
                                                    </div>
                                                </div>
                                                
                                                  <div class="form-group">
                                                       <asp:LinkButton runat="server" ID="brnAddDoc"   OnClick="brnAddDoc_OnClick"    CssClass="btn btn-success   btn-sm pull-right"><span aria-hidden="true" class="fa fa-plus"></span>  &nbsp; Add to List </asp:LinkButton> 
                                                      </div>
                                            </div>

                                        </div>
               <br/>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        
                                                        
                                                          <asp:GridView Width="100%" ShowHeader="True" ID="gv_DocumentUpload" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Document">
                                                    <ItemTemplate>
                                                          <asp:HyperLink ID="HLDocumentLink"   Target="_blank"  runat="server" NavigateUrl='<%# Eval("DocumentLink") %>'  Text='Download' >
        </asp:HyperLink>
                                                        
                                                <%--        <a   Target="_blank"    href="<%# Eval("DocumentLink")%>">Download</a>--%>
                                                        <asp:Label ID="lbl_DocumentLink" Visible="False" runat="server" Text='<%#Eval("DocumentLink")%>'></asp:Label>
                                                        <asp:HiddenField runat="server" ID="hfFileName" Value='<%#Eval("FileName")%>' />
                                                        <asp:HiddenField runat="server" ID="hfDocumentLink" Value='<%#Eval("DocumentLink")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                 
                                                <asp:TemplateField HeaderText="Summary Note	">
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
                                </div>
                            </DIV>

                     
                            <div>
                                <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" Text="Submit " CssClass="btn btn-sm btn-info" />
                                <asp:Button ID="cancelButton" Text="Cancel" OnClick="cancelButton_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    
      <script type="text/javascript">
          $("body").on("click", "#btnUpload", function () {
              debugger;
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
          });


          function showpreview(input) {


              debugger;
              //$('#ContentPlaceHolder1_imageFileUpload').val($(this).val().toLowerCase());
              var validExtensions = ['txt', 'jpg', 'png', 'gif', 'doc', 'docx', 'TXT', 'JPG', 'PNG', 'GIF', 'DOC', 'DOCX', 'pdf', 'PDF', 'ZIP', 'zip', 'rar', 'RAR', 'xlsx', 'XLSX', 'xls', 'XLS'];
              var fileName = input.files[0].name;
              var fileNameExt = fileName.substr(fileName.lastIndexOf('.') + 1);
              if ($.inArray(fileNameExt, validExtensions) == -1) {
                  input.type = '';
                  input.type = 'file';

                  alert("Only these file types are accepted : " + validExtensions.join(', '));
              }
              else {

                  var picsize = (input.files[0].size);
                  if (picsize > 200000000) {
                      input.type = ''
                      input.type = 'file'

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


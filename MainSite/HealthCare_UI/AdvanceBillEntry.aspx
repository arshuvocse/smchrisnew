<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="AdvanceBillEntry.aspx.cs" Inherits="HealthCare_UI_AdvanceBillEntry" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
    
    

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

   .tblTHColorChang{
            background-color: #EDF2F5!important;
            font-weight: bold;
            font-size: 13px;
     }



.title-widget {
	color: #898989!important;
	font-size: 20px!important;
	font-weight: bold!important;
	 
	position: relative!important;
	 
	font-family: 'Fjalla One', sans-serif!important;
	margin-top: 0!important;
	margin-right: 0!important;
	 margin-bottom: 10px!important;
	 
	 
	padding-left: 12px!important;

}

.title-widget::before {
    background-color: #D75A4A;
    content: "";
    height: 22px;
    left: 0px;
    position: absolute;
    top: -2px;
    width: 5px;
}



 </style>

      <div class="content" id="content">
          
              <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>
                
                
                 <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>

                 <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                             
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" /> Advance Bill Information   </h1>
                        </div>

                           <div class="page-heading__container float-right d-none d-sm-block">
              
                               <asp:LinkButton ID="HomeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="HomeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                        <asp:LinkButton ID="LinkButton1"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
              <%--  <asp:Button ID="detailsViewButton" Text="Update Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                   <asp:Button ID="btnEditInfo" Text="Back to List" Visible="False"  CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="btnEditInfo_OnClick" />--%>
            </div>
                    </div>
                    </div>
                     
<div class="card">
   <div class="card-body">
       
       
       
        
          <div class="row">
               <div class="col-md-12">

                     <div class="form-group ">   
                                 
                          <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Company:  <span class="star">*</span></label></div>
                                <div class="col-md-4">
                                      <asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" class="form-control form-control-sm" AutoPostBack="True"></asp:DropDownList> 
                                    <asp:HiddenField ID="hfMasterId" runat="server" />
                                </div>
                            </div>
                          
                                         <div style="padding-top: 5px;"></div>
                          <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Financial Year:  <span class="star">*</span></label></div>
                                <div class="col-md-4"> <asp:DropDownList ID="ddlFinancialYear" runat="server"  class="form-control form-control-sm" ></asp:DropDownList> </div>
                            </div>
                         
                         <div style="padding-top: 5px;"></div>
                         <div class="row">
                             <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Employee Name:  <span class="star">*</span></label></div>
                             <div class="col-md-4"> <asp:DropDownList ID="ddlEmployee" AutoPostBack="True" OnSelectedIndexChanged="ddlEmployee_OnSelectedIndexChanged" runat="server"  class="form-control form-control-sm selectme" ></asp:DropDownList>  <script type="text/javascript">
                                                                                                                                                                                                                                                               function pageLoad() {
                                                                                                                                                                                                                                                                   $('.selectme').chosen({ disable_search_threshold: 5, search_contains: true });

                                                                                                                                                                                                                                                               }
                                                     </script> </div>
                         </div>
                         

                            
                            <div class="row" runat="server" Visible="False">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Requisition NO:  <span class="star">*</span></label></div>
                                <div class="col-md-4">
                                      <asp:DropDownList ID="ddlRequisitionNo" runat="server" OnSelectedIndexChanged="ddlRequisitionNo_OnSelectedIndexChanged"  AutoPostBack="True"  class="form-control form-control-sm selectme" ></asp:DropDownList> 
                                    
                                       
                                                    
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                </div>
                            </div>
                                        <div style="padding-top: 5px;"></div>
                                        
                           
                         
                    
                     <style>

        .star{
            color:red;
        }

         .chkChoiceHeader label {
                        padding-right: 4px;
                        padding-left: 2px;
                    font-size: 14px;
                    font-weight: bold;
                }

    </style>                       
                           
                       </div>
                   </div>         
         </div>


         <br/>
       <br/>
  
        <div class="form-row">
 <%--     <div class="col-md-"></div>--%>
              <div class="col-md-3">
                                <div class="form-group">
                                    <label  class="title-widget"> Employee Information </label> 
                                </div>
                               
                            </div>

       </div>
       
       
       
        <div class="row" style="padding-left:12px">
                                 <table class="table table-bordered table-striped" >
                                                    <tr>
                                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Employee ID</td>
                                                        <td> <asp:Label runat="server" ID="lblEmpId"></asp:Label>
                                                            <asp:HiddenField runat="server" ID="hfEmpID"/>
                                                            <asp:HiddenField runat="server" ID="hfReimbursmentFormId"/>
                                                        </td>

                                                        
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
                                                        <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Cell No</td>
                                                        <td>     <asp:Label ID="OfficailMobile"  runat="server"></asp:Label></td>
  <td style="width: 20%; padding: 10px;" class="tblTHColorChang" >Place</td>
                                                        <td> <asp:Label runat="server" ID="lblPlace"></asp:Label></td>
                                                    </tr>
                                                    
                                                    
                                                 
                                                    
                                                    
                                                    </table>
                            
                            </div>
       
      
       
        
       
             <div id="gridContainer2" runat="server" Visible="False" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;">
                 
           
                                 <asp:GridView ID="GridView1" OnPreRender="gv_DocumentUpload_PreRender"
                                    CssClass="AddToListCssTable"  runat="server" ShowFooter="True"  AutoGenerateColumns="False"    DataKeyNames="Amount"
                                    >
                                    <Columns>
                                     
          

                                         <asp:BoundField DataField="HeadOfExpense" HeaderText="Particulars"/>                                                                          
                                        <asp:BoundField DataField="Dates" HeaderText="Date(s)" /> 
                                           <asp:BoundField DataField="SINoOfEncloseVoucher" HeaderText="SI. No of Enclosed Voucher"/>
                                 
 
                                        
                                        
             <asp:TemplateField HeaderText="Amount (BDT)">
                     <ItemTemplate>
                          <asp:TextBox ID="Amount" ReadOnly="True" runat="server" Text='<%# (Eval("Amount")=="" || Eval("Amount")==null) ? "0" : Eval("Amount")%>'   CssClass="form-control form-control-sm"></asp:TextBox>
                         
                <ajaxToolkit:FilteredTextBoxExtender ID="sssss" runat="server"
                                                    TargetControlID="Amount"
                                                    FilterType="Custom, Numbers"
                                                    ValidChars="." />
                     </ItemTemplate>
                  <FooterStyle HorizontalAlign="left" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalMark" CssClass="form-control  form-control-sm " runat="server" />
                                        </FooterTemplate>
             </asp:TemplateField>
                                                                         
                                   
                                    </Columns>
                                </asp:GridView>
                            </div>
       
       
      
     
       
          <div class="row">
               <div class="col-md-12">

                     <div class="form-group ">   
                                 
                         
                        
                         
                          <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right"> Type: <span class="star">*</span></label></div>
                                <div class="col-md-4" style="padding-top:8px">
                                    
                                    
                                  <asp:RadioButtonList ID="inlineRadio" RepeatDirection="Horizontal"   CssClass="chkChoiceHeader"  runat="server">
                                        <asp:ListItem Selected="True" Value="OPD">OPD</asp:ListItem>
                                        <asp:ListItem Value="IPD">IPD</asp:ListItem>
                                        <asp:ListItem Value="Special">Special</asp:ListItem>
                               
                                    </asp:RadioButtonList>
</div>
                                      
                                </div>
                            </div>

                         
                          <div class="form-group ">   
                          <div class="row">
                               
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label  pull-right">Amount:  <span class="star">*</span></label></div>
                                <div class="col-md-4">  <asp:TextBox runat="server"  ID="txtAmount"  class="form-control form-control-sm" /> 
                                    
                                      <ajaxToolkit:FilteredTextBoxExtender ID="aafresssqQtyTextBox" runat="server"
                                                    TargetControlID="txtAmount"
                                                    FilterType="Custom, Numbers"
                                                    ValidChars="." />
                                </div>
                                </div>
                            </div>
                           
                    <div class="form-group ">   
                         <div class="row">
                          
                             <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Payment To:   <span class="star">*</span></label></div>
                             <div class="col-md-4">  <asp:TextBox runat="server"   ID="CarryPerson"  class="form-control form-control-sm" /> 
                                    
                               
                             </div>
                             </div>
                         </div>
                         
                    
                          <div class="form-group ">   
                         <div class="row">
                              
                             
                             <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Purpose:  <span class="star">*</span></label>  </div>
                             <div class="col-md-4">  <asp:TextBox runat="server"   ID="Remarks" TextMode="MultiLine"  class="form-control form-control-sm" /> 
                                    
                               
                             </div>
                             </div>
                         </div>
                           
                       </div>
                   </div>         
         </div>
       
       <br/>
   
   
        <fieldset class="for-panel">
            <legend>Approval Menu Upload</legend>
                                        <div class="form-row">
                                             <div class="col-4">
                                                <div class="form-group">
                                                   
                                                </div>
                                            </div>

                                            <div class="col-5">
                                                <div class="form-group">
                                                    <%--<label>Signature Upload (Dimension:243 * 44)</label>--%>
                                                    <div>
                                                        <asp:FileUpload ID="FileUpload1" runat="server" Visible="False" />
                                                        <input type="file" name="SignatureFile" accept="*"  />
                                                        <input type="button" id="btnSignatureUpload" value="Appro. Memu Upload" />
                                                        <progress id="SignaturefileProgress" style="display: none"></progress>
                                                        <hr />
                                                   <%--     <asp:Image ID="SignatureImage" runat="server" CssClass="imgshadow" height="130" width="120" />--%>
                                                        <asp:HiddenField runat="server" ID="hfSignature" />
                                                        <span id="lblMessageSignature" style="color: Green"></span>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-4" runat="server" Visible="False">
                                               
                                            </div>
                                        </div>
                                        </fieldset>
       

       




       <br/>
  

        <div class="form-row">
               <div class="col-md-5">
       </div>
                <div class="col-md-3">
                                        <div class="form-group" style="margin-top: 17px;">
                                            <asp:LinkButton runat="server" ID="SearchButton"  OnClick="save_onclick"  CssClass="btn btnMyDesignSearch   btn-sm"><span aria-hidden="true" class="fa fa-check-square"></span>  &nbsp; Save Information </asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="btnReset"  CssClass="btn btnMyDesignReset   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset Information</asp:LinkButton>
                                        </div> 
       </div>

        </div>
   </div>
        
        </div>
            </ContentTemplate>
              </asp:UpdatePanel>
                               </div>
        
    
    
    


  <script type="text/javascript">
      $("body").on("click", "#btnSignatureUpload", function () {
          $.ajax({
              url: '/SignatureUploadHandler.ashx',
              type: 'POST',
              data: new FormData($('form')[0]),
              cache: false,
              contentType: false,
              processData: false,
              success: function (file) {
                  $("#fileProgress").hide();
                  $("#SignaturefileProgress").hide();
                  $("#NomineeImageProgress").hide();

                  $("#lblMessageSignature").html("Approval Memo has been uploaded.");
                  $('#cpFormBody_SignatureImage').prop({ src: '/UploadImg/' + file.dbfilename });
                  $('#cpFormBody_hfSignature').val(file.dbfilename);


              },
              xhr: function () {
                  var fileXhr = $.ajaxSettings.xhr();
                  if (fileXhr.upload) {
                      $("#fileProgress").hide();
                      $("#NomineeImageProgress").hide();
                      $("#SignaturefileProgress").show();
                      fileXhr.upload.addEventListener("SignaturefileProgress", function (e) {
                          if (e.lengthComputable) {
                              $("#SignaturefileProgress").attr({
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
  </script>

        <script>
            function MutExChkList(chk) {
                var chkList = chk.parentNode.parentNode.parentNode;
                var chks = chkList.getElementsByTagName("input");
                for (var i = 0; i < chks.length; i++) {
                    if (chks[i] != chk && chk.checked) {
                        chks[i].checked = false;
                    }
                }
            }
    </script>
   

</asp:Content>


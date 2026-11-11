<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="BillSettlement.aspx.cs" Inherits="HealthCare_UI_BillSettlement" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
	 
	 
	padding-left: 12px;

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
                             
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" /> Bill    </h1>
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
                                                            <asp:HiddenField runat="server" ID="hfCompanyId"/>
                                                            <asp:HiddenField runat="server" ID="hfFinancialYearId"/>
                                                          
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
       
       
       
       
       
       
          <br/>
       <br/>
       
        <div class="form-row" >
              <div class="col-md-3">
                                
                    <label  class="title-widget">Claim Information </label> 
                                           
             </div>
  </div>  
      

<hr />
       
             <div id="gridContainer2" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;">
                 
           
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
       
       
       
       

       

 <hr />
        <fieldset class="for-panel">
                                <legend>Document </legend>
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
                                                         <a class="btn btn-sm btnMyDesignSearch"     Target="_blank"   href="<%# Eval("DocumentLinkPreview") %>">Preview</a>
                                                       
                                                         

                                                          <asp:HyperLink ID="HLDocumentLink"   Visible="False" Target="_blank"  runat="server" NavigateUrl='<%# Eval("DocumentLink") %>'  CssClass="btn btn-sm btnMyDesignSearch"  Text='Preview' >
        </asp:HyperLink>
                                                        
                                         
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

                                              
                                            </Columns>
                                        </asp:GridView>
                                            
                                            </div>
                                            </div>
               </fieldset>
       
       

       <br />
             <script type="text/javascript">
                 function pageLoad() {
                     $('.selectme').chosen({ disable_search_threshold: 5, search_contains: true });

                 }
                                                        </script>
       
       
       
       
         <br/>
  
  <div class="form-row">

              <div class="col-md-3">
                           
                                   <label class="title-widget">Bank Account Details </label>  
                                </div>
      
    
                           
                           

       </div>
       
        <hr />  
       
       

       <div class="row" style="padding-left:12px">
                                 <table class="table table-bordered table-striped" >
                                                    <tr>
                                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Account Name</td>
                                                        <td>  
                                                             <asp:Label runat="server"  ID="txtBankName" class="form-control form-control-sm" />
                                                        </td>

                                                        
                                                         <td  class="tblTHColorChang" style="width: 20%; padding: 10px;">Account No</td>
                                                        <td>       <asp:Label runat="server"  ID="txtBankAccountNo" class="form-control form-control-sm" />
                                                         </td>
                                                    </tr>
                                                    
                                                     <tr>
                                                        <td  class="tblTHColorChang" style="width: 20%; padding: 10px;">Bank Name</td>
                                                        <td>
                                                            
                                                            <asp:Label runat="server"  ID="txtBranchName" class="form-control form-control-sm" />  
                                                        </td>

                                                         
                                                              
                                                         <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Branch And Routing No</td>
                                                        <td>   
                                                            
                                                              <asp:Label runat="server"  ID="txtRoutingNo" class="form-control form-control-sm" />  
                                                        </td>
                                                    </tr>
                                                    
                                                    
                                                                                     
                                                                                     
                                                    
                                                    </table>
                            
                            </div>
       
          
        <div class="form-row">
 <%--     <div class="col-md-"></div>--%>
              <div class="col-md-3">
                                <div class="form-group">
                                    <label  class="title-widget"> Settlement Information </label> 
                                </div>
                               
                            </div>

       </div>
       
       
 
  <div class="row">
               <div class="col-md-6">

                     <div class="form-group ">   
                                         
<%--                            <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Claim No:</label></div>
                                <div class="col-md-6">  <asp:TextBox runat="server"   ID="DropDownList4"   class="form-control form-control-sm" /></div>
                            </div>--%>
                            <div style="padding-top: 5px;"></div>
                             <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right"> Requisition No:</label></div>
                                <div class="col-md-6">  <asp:Label runat="server"  CssClass="form-control form-control-sm" ID="lblRequisitionNo"></asp:Label> </div>
                            </div>
                           <div style="padding-top: 5px;"></div>
                            <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Settlement Date:<span   style="color:red; " title="please fill out this field"> * </span></label></div>
                                <div class="col-md-6">
                  
                                                   
                                         <asp:TextBox ID="SettlementDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                           <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupPosition="TopLeft"
                                                Format="dd-MMM-yyyy" PopupButtonID="ImageButtosn1" CssClass="MyCalendar"
                                                TargetControlID="SettlementDate" />                                   

                                </div>
                            </div>
                          <div style="padding-top: 5px;"></div>
                            <div class="row" runat="server" Visible="False">
                                <div class="col-md-4" style="padding-top:8px;" >  <label class="control-label pull-right"> Recommended For Payment:</label></div>
                                <div class="col-md-6" > 
                                     <div class="form-check form-check-inline" > 
                                     <input class="form-check-input" type="radio" name="inlineRadioOptions" checked="True" id="inlineRadio1" runat="server" value="option1">
                                      <label class="form-check-label" for="inlineRadio1" style="padding-top:8px;">Yes</label>
                                     </div>
                                     <div class="form-check form-check-inline" > 
                                     <input class="form-check-input" type="radio" name="inlineRadioOptions" id="inlineRadio2" runat="server" value="option2">
                                      <label class="form-check-label" for="inlineRadio1" style="padding-top:8px;">No</label>
                                     </div>


                                </div>
                            </div>
                         <div style="padding-top: 5px;"></div>
                            <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Type:</label></div>
                                <div class="col-md-6" style="padding-top:8px;">  
                                    <asp:CheckBox ID="OPD" Text="OPD" Enabled="False"  OnCheckedChanged="OPDonCheckedChanged" AutoPostBack="True" runat="server"/>
                                    <asp:CheckBox ID="IPD" Text="IPD" Enabled="False"  OnCheckedChanged="IPDonCheckedChanged" AutoPostBack="True" runat="server"/>
                                    <asp:CheckBox ID="Speical"   Text="Speical" Enabled="False" OnCheckedChanged="OtheronCheckedChanged" AutoPostBack="True" runat="server"/>
                                </div>
                            </div>
         

                          <div style="padding-top: 5px;"></div>
                          <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">OPD/IPD Balance:</label></div>
                                <div class="col-md-6">  <asp:TextBox runat="server"  Visible="False" ID="OPDIPDBalance"   class="form-control form-control-sm" />
                                     <asp:TextBox runat="server"  ID="RemainBalance"   class="form-control form-control-sm" />
                                </div>
                          </div>
                             
                     <div runat="server" Visible="False" ID="OT">
                       
                           <div style="padding-top: 5px;"></div>

                              <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Other Balance:</label></div>
                                <div class="col-md-6">  <asp:TextBox runat="server"  ID="OtherBalance"   class="form-control form-control-sm" /></div>
                            </div>
                       </div>
                         
                           
                           <div style="padding-top: 5px;"></div>
                             <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Advance Balance:</label></div>
                                <div class="col-md-6">  <asp:Label runat="server"  ID="lblAdvanceBal"   class="form-control form-control-sm" /></div>
                            </div>
                         
                         <%-- <div style="padding-top: 5px;"></div>
                             <div class="row" >
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Remain Balance:</label></div>
                                <div class="col-md-6"> </div>
                            </div>--%>
                       
                         
                              <div style="padding-top: 5px;"></div>
                             <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right"><asp:CheckBox runat="server" Text="Is Special Allowance" AutoPostBack="True" OnCheckedChanged="isExtra_OnCheckedChanged" ID="isExtra"/></label></div>
                                <div class="col-md-6">  <asp:TextBox runat="server"  ID="txtExtraAmnt" AutoPostBack="True" OnTextChanged="PayableAmount_OnTextChanged" ReadOnly="True"  class="form-control form-control-sm" />  <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                    TargetControlID="txtExtraAmnt"
                                                    FilterType="Custom, Numbers"
                                                    ValidChars="." /></div>
                            </div>
                         
                             <div style="padding-top: 5px;"></div>
                             <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Payable Amount:</label></div>
                                <div class="col-md-6">  <asp:TextBox runat="server"  ID="PayableAmount" AutoPostBack="True" OnTextChanged="PayableAmount_OnTextChanged"  class="form-control form-control-sm" /></div>
                                 <ajaxToolkit:FilteredTextBoxExtender ID="sssjjss" runat="server"
                                                    TargetControlID="PayableAmount"
                                                    FilterType="Custom, Numbers"
                                                    ValidChars="." />
                                 <asp:HiddenField runat="server" ID="hfTotalRemainingBlnc"/>
                            </div>
                           

                             <div style="padding-top: 5px;"></div>
                             <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Payment Type:</label></div>
                                <div class="col-md-6" style="padding-top:8px;">  
                                    <asp:CheckBox ID="Cash" Text="Cash" OnCheckedChanged="Cash_OnCheckedChanged" AutoPostBack="True" runat="server"/>
                                    <asp:CheckBox ID="Check" Text="Cheque" OnCheckedChanged="Check_OnCheckedChanged" AutoPostBack="True" runat="server"/>
                                </div>
                            </div>
                         
                         
                         
                         <div runat="server"  id="divCash" Visible="False">
                             <div style="padding-top: 5px;"></div>
                             
                             <div class="row" runat="server" >
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Cash Date:</label></div>
                                <div class="col-md-6">
                                    
                                   <asp:TextBox ID="txtCashDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                    <cc1:CalendarExtender ID="CalendarExtender4" runat="server" PopupPosition="TopLeft"
                                                Format="dd-MMM-yyyy" PopupButtonID="txtCashDate" CssClass="MyCalendar"
                                                TargetControlID="txtCashDate" />

                         
                                </div>
                            </div>
                             </div>
                         
                         <div runat="server"  id="divCheck"  Visible="False">
                             <div style="padding-top: 5px;"></div>
                             
                             <div class="row" runat="server" >
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">cheque Date:</label></div>
                                <div class="col-md-6">
                                    
                                   <asp:TextBox ID="CheckDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="TopLeft"
                                                Format="dd-MMM-yyyy" PopupButtonID="CheckDate" CssClass="MyCalendar"
                                                TargetControlID="CheckDate" />

                         
                                </div>
                            </div>
                               <div style="padding-top: 5px;"></div>
                             
                             
                              <div class="row" runat="server" Visible="False">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Bank Name:</label></div>
                                <div class="col-md-6">
                                    
                                  <asp:DropDownList runat="server" ID="ddlBankName" class="form-control form-control-sm selectme"> 
                                       
                                    </asp:DropDownList>
                                    
                                     
                     <style>


        .star{
            color:red;
        }

    </style>   
                                                   
                         
                                </div>
                            </div>
                             
                             
                              
                            </div>
                           <div style="padding-top: 5px;"></div>
                        
                            <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Remarks: &nbsp; </label></div>
                                <div class="col-md-6">  <asp:TextBox runat="server"   ID="txtMainRemarks"  TextMode="MultiLine" Rows="2"   class="form-control" /></div>
                                    
                          

                                </div>
                       </div>
                   </div>
      
        <div class="col-md-6">

                     <div class="form-group ">   
                                         
<%--                            <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Claim No:</label></div>
                                <div class="col-md-6">  <asp:TextBox runat="server"   ID="DropDownList4"   class="form-control form-control-sm" /></div>
                            </div>--%>
                            <div style="padding-top: 5px;"></div>
                             <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">  </label></div>
                                <div class="col-md-6">  <asp:Label runat="server" Visible="False"  CssClass="form-control form-control-sm" ID="Label1"></asp:Label> </div>
                            </div>
                           <div style="padding-top: 5px;"></div>
                            <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right"> </label></div>
                                <div class="col-md-6">
                  
                                                                         

                                </div>
                            </div>
                          <div style="padding-top: 5px;"></div>
                            <div class="row" runat="server" Visible="False">
                                <div class="col-md-4" style="padding-top:8px;" >  <label class="control-label pull-right">  </label></div>
                                <div class="col-md-6" > 
                                     


                                </div>
                            </div>
                         <div style="padding-top: 5px;"></div>
                            <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right"> </label></div>
                                <div class="col-md-6" style="padding-top:8px;">  
                                     
                                </div>
                            </div>
         
                         
                             
                    
                          <div style="padding-top: 5px;"></div>
                          <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right"> </label></div>
                                <div class="col-md-6"> 
                                </div>
                          </div>
                             
                     <div runat="server" Visible="False" ID="Div1">
                       
                           <div style="padding-top: 5px;"></div>

                              <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right"> :</label></div>
                                <div class="col-md-6">  </div>
                            </div>
                       </div>
                         
                           
                           <div style="padding-top: 5px;"></div>
                             <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right"> </label></div>
                                <div class="col-md-6">   </div>
                            </div>
                         
                         <%-- <div style="padding-top: 5px;"></div>
                             <div class="row" >
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Remain Balance:</label></div>
                                <div class="col-md-6"> </div>
                            </div>--%>
                       
                         
                              <div style="padding-top: 5px;"></div>
                             <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right"> </label></div>
                                <div class="col-md-6">  </div>
                            </div>
                         
                             <div style="padding-top: 5px;"></div>
                             <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right"> </label></div>
                                <div class="col-md-6">    </div>
                            </div>
                           

                             <div style="padding-top: 5px;"></div>
                             <div class="row" runat="server" Visible="False" id="divAdd">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">
Carry Person </label></div>
                                <div class="col-md-2"  style="padding-top: 8px">
                                    <asp:Label runat="server" ID="lblCarry"    ></asp:Label>  
                                </div>
                                 
                                   <div class="col-md-2" style="padding-top: 8px">  <label class="control-label pull-right">
Remarks </label></div>
                                <div class="col-md-2"  style="padding-top: 8px">
                                    <asp:Label runat="server" ID="lblRemarks"    ></asp:Label>  
                                </div>
                            </div>
                         
                         
                         
                         <div runat="server"  id="div2" Visible="False">
                             <div style="padding-top: 5px;"></div>
                             
                             <div class="row" runat="server" >
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">  </ </label></div>
                                <div class="col-md-6">
                                    
                                 

                         
                                </div>
                            </div>
                             </div>
                         
                      
                           
                       </div>
                   </div>

                  <div class="col-md-6">
                       
                        </div>
             </div>

         <hr />

    
       
       
       
        <fieldset class="for-panel" runat="server" Visible="False">
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
                     
                        <Triggers>  
  
         <asp:PostBackTrigger ControlID="btnDocUp" />  
  
</Triggers> 
                     
                     

        </asp:UpdatePanel>
                               </div>
        
    
    
    
    <script type="text/javascript">
        $("body").on("click", "#btnUpload", function () {
            if ($("#upImage").val() != '') {
                $.ajax({
                    url: '/HandlerHealthDoc.ashx',
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
                'jpg', 'png', 'JPG', 'PNG', 'XLSX', 'xlsx', 'PDF', 'pdf', 'TXT', 'txt', 'DOC', 'doc', 'DOCX', 'docx']; //array of valid extensions
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


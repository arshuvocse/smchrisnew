<%@ Page Title="" Language="C#" Debug="true" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="ExpenseReimbursementFormForHRPayment.aspx.cs" Inherits="HealthCare_UI_ExpenseReimbursementFormForHRPayment" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
    
      

  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/fancybox/3.5.7/jquery.fancybox.css" integrity="sha512-nNlU0WK2QfKsuEmdcTwkeh+lhGs6uyOxuUs+n+0oXSYDok5qy0EI0lt01ZynHq6+p/tbgpZ7P+yUb+r71wqdXg==" crossorigin="anonymous" referrerpolicy="no-referrer" />

<script src="https://cdnjs.cloudflare.com/ajax/libs/fancybox/3.5.7/jquery.fancybox.js" integrity="sha512-j7/1CJweOskkQiS5RD9W8zhEG9D9vpgByNGxPIqkO5KrXrwyDAroM9aQ9w8J7oRqwxGyz429hPVk/zR6IOMtSA==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    
    
    
     <style>
         .form-group_2 {
             margin-bottom: 2px!important;
         }
           .SelectchkChoice label {
            padding-left: 6px;
            font-weight: bold;
        }
          .shrinkToFit {
              width: 100% !important;
              height: 100% !important;
          }
     </style>
    
      <script>
          $(document).ready(function () {
              $(".fancybox").fancybox({

                  'width': 1000, // or whatever
                  'height': 700,
                  'type': 'iframe',
                  'autoSize': false
              });


          });

    </script>
    
    <style>
        #cpFormBody_inlineRadio_0,
        #cpFormBody_inlineRadio_1,
        #cpFormBody_inlineRadio_2
        {
         
            margin-right: 5px !important;
            margin-left: 5px !important;
        }
    </style>
    

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
         color: #898989;
         font-size: 18px;
         font-weight: bold;
	 
         position: relative;
	 
         font-family: 'Fjalla One', sans-serif;
         margin-top: 0;
         margin-right: 0;
         /*margin-bottom: 10px;*/
	 
	 
         padding-left: 10px;

     }

     .title-widget::before {
         background-color: #D75A4A;
         content: "";
         height: 20px;
         left: 0px;
         position: absolute;
         top: -2px;
         width: 5px;
     }


 </style>
    
    
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

            .attachment-criteria-item {
                transition: opacity .2s ease;
            }

            .attachment-criteria-disabled {
                opacity: .45;
            }

  
      
        </style>
 
         

      <div class="content" id="content">
          

     <div class="modal fade" id="exampleModal2" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
   <div class="modal-dialog  modal-lg" style=" width: 90% !important; " role="document">
  <asp:UpdatePanel runat="server">
      <ContentTemplate>
              <div class="modal-content">
         <div class="modal-header">
            

                  <h3 class="modal-title" id="exampleModalLabel2"  style="color:#2196F3; text-shadow:  0 0 1px black;">Add Family Member </h3>

             <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
         </div>
         <div class="modal-body">
                 <div class="row">
                            <div class="col-md-12">

                              <div class="row">
                                  <div class="col-md-12">
                                        <asp:GridView Width="100%" ShowHeader="True" ID="gv_EmpListSearch" DataKeyNames="EmpInfoId,AGE" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                    
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" CssClass="form-control-sm" AutoPostBack="True" OnCheckedChanged="chkSelect_OnCheckedChanged"  runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                 
                                                <asp:TemplateField HeaderText="Family Member">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Family" runat="server" Text='<%#Eval("Family") %>'></asp:Label>
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
                             
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" /> Healthcare Expense Reimbursement Form   </h1>
                        </div>
                  <div class="page-heading__container float-right d-none d-sm-block">
                      <asp:LinkButton ID="HomeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="HomeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                    <asp:LinkButton ID="LinkButton3"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
                    </div>
                    </div>
                     
<div class="card">
   <div class="card-body">
          
      
            
 <%--        <div class="row">
           
            
            <div class="col-md-12">
                                    
                       
 <div class="form-check form-check-inline" style="padding-left:600px">
   <asp:RadioButton  CssClass="form-check-input" ID="inlineRadio1" value="option1" name="inlineRadioOptions" AutoPostBack="True" OnCheckedChanged="IPD_Click"  runat="server"/>  
  <label class="form-check-label" for="inlineRadio1">IPD</label>
</div>

<div class="form-check form-check-inline">

     <asp:RadioButton  CssClass="form-check-input" ID="inlineRadio2" value="option2" name="inlineRadioOptions" AutoPostBack="True" OnCheckedChanged="OPD_Click"  runat="server"/> 
  <label class="form-check-label" for="inlineRadio2">OPD</label>
</div>

                                </div>
           
                   

       </div>--%>
         <style>

             .star{
                 color:red;
             }

              .chkChoiceHeader label {
             
                    padding-right: 4px;
                    font-size: 14px;
                       padding-left: 2px;
                    font-weight: bold;
                }

         </style>

       
   
   
    
   <div class="form-row" >
       <div class="col-md-3">
                                
           <label class="title-widget">Employment  Information</label> 
                                           
       </div>
       
         <div class="col-md-9">
             <a href="../DownloadsForm/User%20Manual%20Healthcare.pdf"  target="_blank" class=" pull-right" title="Click to download Health Care User Manual">Health Care User Manual</a>
             </div>
   </div>     
   <hr />


       <div class="row">
               <div class="col-md-12">

                     <div class="form-group ">   
                                 
                         <div class="row">
                             <div class="col-md-6">
                                 
                                  <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Company: <span class="star">*</span></label></div>
                                <div class="col-md-8">
                                      <asp:DropDownList ID="ddlCompany" runat="server" Enabled="False" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" CssClass="form-control form-control-sm" AutoPostBack="True"></asp:DropDownList> 
                                    <asp:HiddenField ID="hfMode" runat="server" />
                                    <asp:HiddenField ID="hfMasterId" runat="server" />
                                    <asp:HiddenField ID="HFReimbursmentId" runat="server" />
                                    <asp:HiddenField runat="server" ID="HFEntryBy"/>
                                </div>
                            </div>
                          
                           <div style="padding-top: 5px;"></div>
                          <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Financial Year:  <span class="star">*</span></label></div>
                                <div class="col-md-8"> <asp:DropDownList ID="ddlFinancialYear" AutoPostBack="True" OnSelectedIndexChanged="ddlFinancialYear_OnSelectedIndexChanged" runat="server"  CssClass="form-control form-control-sm" ></asp:DropDownList> </div>
                            </div>
                         
                          <div style="padding-top: 5px;"></div>
                          <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Employee Name:</label></div>
                                <div class="col-md-8">
                                             <asp:DropDownList   runat="server"  AutoPostBack="True" OnSelectedIndexChanged="ddlForwordEmp_OnSelectedIndexChanged" Enabled="False" ID="ddlForwordEmp" CssClass="form-control form-control-sm selectme" />
                                                   
                                                     <script type="text/javascript">
                                                         function pageLoad() {
                                                             $('.selectme').chosen({ disable_search_threshold: 5, search_contains: true });

                                                         }
                                                     </script>
                                </div>
                            </div>
                             </div>
                             <div class="col-md-6">
                                 

                                 
                          <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right"> Type:  <span class="star">*</span></label></div>
                                <div class="col-md-8" style="padding-top:8px">

                                    <asp:RadioButtonList ID="inlineRadio" RepeatDirection="Horizontal" AutoPostBack="True" CssClass="chkChoiceHeader" OnSelectedIndexChanged="inlineRadio_OnSelectedIndexChanged"  runat="server">
                                        <asp:ListItem Value="OPD">OPD</asp:ListItem>
                                        <asp:ListItem Value="IPD">IPD</asp:ListItem>
                                        <asp:ListItem Value="Special">Special</asp:ListItem>
                               
                                    </asp:RadioButtonList>
                                    

                                      
                                </div>
                            </div>

                         
                           <div style="padding-top: 5px;"></div>
                          <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Total Balance:</label></div>
                                <div class="col-md-2">  <asp:TextBox runat="server" ReadOnly="True"   ID="txtTotalBalance"  class="form-control form-control-sm" /></div>
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Total Availed Amount:</label></div>
                                <div class="col-md-2">  <asp:TextBox runat="server" ReadOnly="True"   ID="txtAvailedAmount"  class="form-control form-control-sm" /></div>
                            
                            </div>
                         
                           <div style="padding-top: 5px;"></div>
                          <div class="row">
                                 <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Remaining Balance:</label></div>
                                <div class="col-md-8">  <asp:TextBox runat="server" ReadOnly="True"   ID="RemainingBalance"  class="form-control form-control-sm" /></div>
                              </div>
                         
                         
                         

                         <div style="padding-top: 5px;"></div>
                         <div class="row">
                             <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Application Submit Date:<span class="star">*</span></label></div>
                             <div class="col-md-8">  <asp:TextBox runat="server"  autocomplete="off"  ID="SubmitDate" disabled="disabled"  class="form-control form-control-sm" />
                                 <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="TopLeft"
                                                       Format="dd-MMM-yyyy" PopupButtonID="ImageButtosn1" CssClass="MyCalendar"
                                                       TargetControlID="SubmitDate" />
                             </div>
                         </div>
                         
                         
                         <div style="padding-top: 5px;"></div>
                         <div class="row">
                             <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Illness Description:<span class="star">*</span></label></div>
                             <div class="col-md-8">  <asp:TextBox runat="server"  autocomplete="off"   ID="SelfDate"  class="form-control" />
                           
                             </div>
                         </div>

                                          <div style="padding-top: 5px;"></div>
<div class="row" runat="server" id="divHospital" visible="false">
    <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Hospital Name: </label></div>
    <div class="col-md-8">
        <asp:TextBox runat="server" ID="txtHospitalName" autocomplete="off" CssClass="form-control form-control-sm" placeholder="Type hospital name" />
        <cc1:AutoCompleteExtender
            ID="aceHospitalName"
            runat="server"
            EnableCaching="true"
            MinimumPrefixLength="1"
            CompletionSetCount="10"
            ServiceMethod="GetHospitalNameAuto"
            ServicePath="~/WebService.asmx"
            TargetControlID="txtHospitalName"
            CompletionListCssClass="autocomplete_completionListElement" />
        <asp:DropDownList runat="server" ID="ddlHospitalName" CssClass="form-control form-control-sm" Style="display:none;" />
    </div>
</div>
                         <div style="padding-top: 5px;"></div>
                         <div class="row" runat="server" id="divHospitalAdmissionDischargeDate" visible="false">
                             <div class="col-md-4" style="padding-top: 8px">
                                 <label class="control-label pull-right">Hospital Admission Date:</label>
                             </div>
                             <div class="col-md-8">
                                 <asp:TextBox
                                     runat="server"
                                     autocomplete="off"
                                     ID="txtHospitalAdmissionDate"
                                     class="form-control form-control-sm" />
                                 <cc1:CalendarExtender ID="CalendarExtenderHospitalAdmissionDate" runat="server" PopupPosition="TopLeft"
                                                       Format="dd-MMM-yyyy" PopupButtonID="txtHospitalAdmissionDate" CssClass="MyCalendar"
                                                       TargetControlID="txtHospitalAdmissionDate" />
                             </div>
                         </div>
                         <div style="padding-top: 5px;"></div>
                         <div class="row" runat="server" id="divHospitalDischargeDate" visible="false">
                             <div class="col-md-4" style="padding-top: 8px">
                                 <label class="control-label pull-right">Hospital Discharge Date:</label>
                             </div>
                             <div class="col-md-8">
                                 <asp:TextBox
                                     runat="server"
                                     autocomplete="off"
                                     ID="txtHospitalDischargeDate"
                                     class="form-control form-control-sm" />
                                 <cc1:CalendarExtender ID="CalendarExtenderHospitalDischargeDate" runat="server" PopupPosition="TopLeft"
                                                       Format="dd-MMM-yyyy" PopupButtonID="txtHospitalDischargeDate" CssClass="MyCalendar"
                                                       TargetControlID="txtHospitalDischargeDate" />
                             </div>
                         </div>
                             </div>
                         </div>
                                 
                           
                         


                     </div>
                   </div>         
         </div>


          
       <br/>
       <div class="form-row" >
              <div class="col-md-3">
                                
                                    <label class="title-widget">Employee Information</label> 
                                           
                            </div>
       </div>     
             <hr />
              
     <%--         <div class="row">
               <div class="col-md-12">

                     <div class="form-group ">   
                                                        
                            <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Employee Information:</label></div>
                                <div class="col-md-4">
                                    
                                        <asp:DropDownList   runat="server"  AutoPostBack="True" OnSelectedIndexChanged="ddlForwordEmp_OnSelectedIndexChanged"  ID="ddlForwordEmp" CssClass="form-control form-control-sm selectme" />
                                                   
                                                     <script type="text/javascript">
                                                         function pageLoad() {
                                                             $('.selectme').chosen({ disable_search_threshold: 5, search_contains: true });

                                                         }
                                                     </script>
                                </div>
                            </div>
                          
                       
                         
                                                                     
                       </div>
                   </div>         
         </div>--%>

      
       

       

    <%--     <div class="row">
                                   <div class="col-md-4">
                                                <div class="form-group ">
                                                    <label>Employee Name </label>
                                                    
                                                                <asp:DropDownList   runat="server"  AutoPostBack="True" OnSelectedIndexChanged="ddlForwordEmp_OnSelectedIndexChanged"  ID="ddlForwordEmp" CssClass="form-control form-control-sm selectme" />
                                                   
                                                     <script type="text/javascript">
                                                         function pageLoad() {
                                                             $('.selectme').chosen({ disable_search_threshold: 5, search_contains: true });

                                                         }
                                                     </script>
                                                   
                                                      
                                            </div>
                                                
                                                
                                                
                                            </div> 
         
                            </div>--%>



       <div class="row" style="padding-left:12px">
                                 <table class="table table-bordered table-striped" >
                                                    <tr>
                                                        <td class="tblTHColorChang" style="width: 15%; padding: 10px;">Employee ID</td>
                                                        <td> <asp:Label runat="server" ID="lblEmpId"></asp:Label>
                                                            <asp:HiddenField runat="server" ID="hfEmpID"/>
                                                        </td>
                                                           <td  class="tblTHColorChang" style="width: 15%; padding: 10px;">Employee Name</td>
                                                        <td> <asp:Label runat="server" ID="lblEmployeeName"></asp:Label></td>
                                                        
                                                         <td  class="tblTHColorChang" style="width: 15%; padding: 10px;">Supervisor</td>
                                                        <td>  <asp:Label ID="ReportingLabel"  runat="server"></asp:Label></td>
                                                    </tr>
                                                     
                                                    
                                                     <tr>
                                                        <td class="tblTHColorChang"  style="width: 15%; padding: 10px;">Designation</td>
                                                        <td>   <asp:Label ID="desigNameLabel" runat="server"></asp:Label></td>
                                                              <td class="tblTHColorChang"  style="width: 15%; padding: 10px;">Department</td>
                                                        <td>  <asp:Label ID="deptNameLabel"  runat="server"></asp:Label></td>
                                                     <td class="tblTHColorChang"  style="width: 15%; padding: 10px;">Office</td>
                                                        <td>  <asp:Label ID="LocationLabel"   runat="server"></asp:Label></td>

                                                    </tr>
                                                    
                                                    
                                                                                               
                                                                                                    
                                                     <tr>
                                                        <td class="tblTHColorChang"  style="width: 15%; padding: 10px;">Cell No<span class="star">*</span></td>
                                                        <td>          <asp:TextBox runat="server"  ID="txtOfficailMobile" class="form-control form-control-sm" /></td>
  <td style="width: 15%; padding: 10px;" class="tblTHColorChang" >Place</td>
                                                        <td> <asp:Label runat="server" ID="lblPlace"></asp:Label></td>
                                                    </tr>
                                                    
                                                    
                                                 
                                                    
                                                    
                                                    </table>
                            
                            </div>








<%--  <div class="row">
               <div class="col-md-6">

                     <div class="form-group ">   
                                         
                            <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Name:</label></div>
                                <div class="col-md-6">  <asp:TextBox runat="server"   ID="DropDownList4"  class="form-control form-control-sm" /></div>
                            </div>
                            <div style="padding-top: 5px;"></div>
                            <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Designation:</label></div>
                                <div class="col-md-6">  <asp:TextBox runat="server"   ID="TextBox1"  class="form-control form-control-sm" /></div>
                            </div>
                           <div style="padding-top: 5px;"></div>
                            <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Job Location:</label></div>
                                <div class="col-md-6">  <asp:TextBox runat="server"  ID="TextBox2"  class="form-control form-control-sm" /></div>
                            </div>
                           
                       </div>
                   </div>

                  <div class="col-md-6">
                        <div class="form-group ">
                                               
                            <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right"> Id No:</label></div>
                                <div class="col-md-6">  <asp:TextBox runat="server"   ID="ddCompany"  class="form-control form-control-sm" /></div>
                            </div>
                            <div style="padding-top: 5px;"></div>
                            <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right"> Department:</label></div>
                                <div class="col-md-6">  <asp:TextBox runat="server"    ID="DropDownList1"  class="form-control form-control-sm" /></div>
                            </div>
                              <div style="padding-top: 5px;"></div>
                            <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right"> Cell No:</label></div>
                                <div class="col-md-6">  <asp:TextBox runat="server"  ID="DropDownList2"  class="form-control form-control-sm" /></div>
                            </div>
                          
                            </div>
                        </div>
             </div>--%>



       <br/>
  
  <div class="form-row">

              <div class="col-md-3">
                           
                                   <label class="title-widget">Bank Account Details </label>  
                                </div>
      
      <div class="col-md-2" runat="server" Visible="False">  <asp:LinkButton ID="btnBankDtls"    CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="btnBankDtls_OnClick" > <i class="fa fa-plus"></i>&nbsp; </asp:LinkButton>  <asp:LinkButton ID="btnReload"    CssClass="btn btn-sm btn-outline-info " runat="server" OnClick="btnReload_OnClick" > <i class="fa fa-refresh"></i>&nbsp; </asp:LinkButton></div>
                           
                           

       </div>
          
            <hr />  
       
       
       <div class="row" style="padding-left:12px">
                                 <table class="table table-bordered table-striped" >
                                                    <tr>
                                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Account Name <span class="star">*</span></td>
                                                        <td>  
                                                             <asp:TextBox runat="server"  ID="txtBankName" class="form-control form-control-sm" />
                                                        </td>

                                                        
                                                         <td  class="tblTHColorChang" style="width: 20%; padding: 10px;">Account No <span class="star">*</span></td>
                                                        <td>       <asp:TextBox runat="server"  ID="txtBankAccountNo" class="form-control form-control-sm" />
                                                         </td>
                                                    </tr>
                                                    
                                                     <tr>
                                                        <td  class="tblTHColorChang" style="width: 20%; padding: 10px;">Bank & Branch Name <span class="star">*</span></td>
                                                        <td>
                                                            
                                                            <asp:TextBox runat="server"  ID="txtBranchName" class="form-control form-control-sm" />  
                                                        </td>

                                                         
                                                              
                                                         <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Routing No <span class="star">*</span></td>
                                                        <td>   
                                                            
                                                              <asp:TextBox runat="server"  ID="txtRoutingNo" class="form-control form-control-sm" />  
                                                        </td>
                                                    </tr>
                                                    
                                                    
                                                                                     
                                                                                     
                                                    
                                                    </table>
                            
                            </div>
       
       
       


<%--  <div class="row">
               <div class="col-md-6">

                     <div class="form-group ">                                                    
                            <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Account Name:</label></div>
                                <div class="col-md-6">  <asp:TextBox runat="server"   ID="DropDownList3"  class="form-control form-control-sm" /></div>
                            </div>
                            <div style="padding-top: 5px;"></div>
                            <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Branch Name:</label></div>
                                <div class="col-md-6">  <asp:TextBox runat="server"  ID="TextBox3"  class="form-control form-control-sm" /></div>
                            </div>
                            
                           
                       </div>
                   </div>

                  <div class="col-md-6">
                        <div class="form-group ">
                                               
                            <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right"> Account No:</label></div>
                                <div class="col-md-6">  <asp:TextBox runat="server"   ID="DropDownList5"  class="form-control form-control-sm" /></div>
                            </div>
                            <div style="padding-top: 5px;"></div>
                            <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right"> Branch And Routing No:</label></div>
                                <div class="col-md-6">  <asp:TextBox runat="server"  ID="TextBox5"  class="form-control form-control-sm" /></div>
                            </div>
                                                   
                            </div>
                        </div>
             </div>--%>
       


       <br/>


        <div class="form-row">

              <div class="col-md-3">
                               
                                    <label class="title-widget">Patient Information </label> 
                                                   
                      </div>
            <div class="col-md-2">
               <div class="form-group">
                                     
              <asp:LinkButton runat="server" ID="LinkButton1"  data-toggle="modal" data-target="#exampleModal2"  CssClass="btn btn-sm btnMyDesignOne">Add Patient </asp:LinkButton>
          </div>
          </div>
       </div>
       
   <hr />
       
  <div class="row">
               <div class="col-md-4">

                     <div class="form-group ">                                                    
                            <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Name of Patient: <span class="star">*</span></label></div>
                                <div class="col-md-6">  <asp:TextBox runat="server"  ID="NameofPatient"  class="form-control form-control-sm" /></div>
                            </div>
                           
                                              
                       </div>
                   </div>  
           <div class="col-md-4">
                <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Age: </label></div>
                                <div class="col-md-6">  <asp:TextBox runat="server"    ID="Age"  class="form-control form-control-sm" />
                                    
                                      <ajaxToolkit:FilteredTextBoxExtender ID="freqQtyTextBox" runat="server"
                                                    TargetControlID="Age"
                                                    FilterType="Custom, Numbers"
                                                    ValidChars="." />
                                </div>
                            </div>
               </div>
      
      <div class="col-md-4">
          <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Relationship: <span class="star">*</span></label></div>
                                <div class="col-md-6">  <asp:TextBox runat="server" ReadOnly="True" ID="Relationship"  class="form-control form-control-sm" /></div>
                            </div>   
       
      </div>
              
             </div>
       
       
       <br/>
       <br/>
       
      <div class="form-row" >
              <div class="col-md-5">
                                
                                    <label class="title-widget">Brief Description of illness </label> 
                                           
                            </div>
       </div>   

 <hr />
                                           


       
      
       
  <div id="gridContainer1" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: hidden;">     
                            
  <asp:GridView ID="loadGridView" runat="server" ShowFooter="true" AutoGenerateColumns="false" DataKeyNames="YesNo1,Date,YesNo"  OnPreRender="gv_DocumentUpload_PreRender"
                                    CssClass="table table-striped table-bordered">
    <Columns>
       
        
       <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
             </asp:TemplateField>
        

        <asp:TemplateField HeaderText="Description">
            <ItemTemplate>
                <asp:Label ID="Description" runat="server"  Text='<%# Eval("Description") %>'  ></asp:Label>
                  <asp:HiddenField runat="server" ID="hfReibCheckOppId" Value='<%#Eval("ReibCheckOppId")%>' />
            </ItemTemplate>
        </asp:TemplateField>

            
         
        <asp:TemplateField HeaderText="Yes" >
            <ItemTemplate>    
              <asp:CheckBox ID="Yes"  AutoPostBack="True" OnCheckedChanged="btnRemove_OnClick"  runat="server" />
            </ItemTemplate>  
        </asp:TemplateField>
        
             
         <asp:TemplateField HeaderText="No" >
            <ItemTemplate>       
                 <asp:CheckBox ID="No"  Checked="True" AutoPostBack="True" OnCheckedChanged="NoCheck_OnChanged"  runat="server"  />
            </ItemTemplate>  
        </asp:TemplateField>
        
     
            <asp:TemplateField HeaderText="Date"  >
            <ItemTemplate>
                <asp:TextBox ID="Date" runat="server"  Enabled="False" Text='<%# Eval("Descriptiondate")%>'  CssClass="form-control form-control-sm"></asp:TextBox>
                     <cc1:CalendarExtender ID="CalendarExtender" runat="server" PopupPosition="TopLeft"
                                                Format="dd-MMM-yyyy" PopupButtonID="ImageButtosn1" CssClass="MyCalendar"
                                                TargetControlID="Date" />
            </ItemTemplate>  
        </asp:TemplateField>
        
        

    </Columns>
</asp:GridView>

                            </div>

       
     
  <div class="form-row">
              <div class="col-md-12">
                            
                                    <label class="title-widget">Enclosures(Tick)mark the appropriate Box. Put serial number on every enclosed document </label>   <span class="star">*</span>
                              
                                
                            </div>

       </div>
        <hr />
   <div class="form-row">
                                    
                            <div class="col-md-4">
                                
                                <div class="form-group_2">
                                             
                                    <asp:CheckBox ID="Prescription" CssClass="SelectchkChoice" style=" margin: .4rem;  font: 1rem 'Fira Sans', sans-serif;"  Text="1. PhotocCopy of the Prescription" runat="server"/>
                                    
                                    <style>

                                        </style>
                                </div>

                                <div class="form-group_2">

                                    <asp:CheckBox ID="BillofConsaltation" CssClass="SelectchkChoice"   style=" margin: .4rem;  font: 1rem 'Fira Sans', sans-serif;"  Text="2. Original Bill of Consaltation" runat="server"/>

                                </div>
                                
                                <div class="form-group_2">

                                    <asp:CheckBox ID="BillofMedicine" CssClass="SelectchkChoice"  style=" margin: .4rem;  font: 1rem 'Fira Sans', sans-serif;" Text="3. Original Bill of Medicine" runat="server"/>

                                </div>

                              
                                
                              
                                
                            </div>
               
               
                            <div class="col-md-4">
                                 <div class="form-group_2">

                                    <asp:CheckBox ID="BillofHospitalization" CssClass="SelectchkChoice"  style=" margin: .4rem;  font: 1rem 'Fira Sans', sans-serif;" Text="4. Original Bill of Hospitalization" runat="server"/>

                                </div>
                                <div class="form-group_2">
                                             
                                    <asp:CheckBox ID="BillofInvestigation" CssClass="SelectchkChoice"   style=" margin: .4rem;  font: 1rem 'Fira Sans', sans-serif;"  Text="5. Original Bill of Investigation" runat="server"/>

                                </div>

                                <div class="form-group_2">

                                    <asp:CheckBox ID="PhotoCopyofInvestigation"  CssClass="SelectchkChoice" style=" margin: .4rem;  font: 1rem 'Fira Sans', sans-serif;" Text="6. PhotoCopy of Investigation" runat="server"/>

                                </div>
                                
                              
                                
                               



                            </div>
                            
                         
                            <div class="col-md-4">
                                  <div class="form-group_2">

                                    <asp:CheckBox ID="BillForCharges" CssClass="SelectchkChoice"  style=" margin: .4rem;  font: 1rem 'Fira Sans', sans-serif;"  Text="7. Original Bill For Charges" runat="server"/>

                                </div>
                                
                                
                                  <div class="form-group_2">

                                    <asp:CheckBox ID="Other"  CssClass="SelectchkChoice"  style=" margin: .4rem;  font: 1rem 'Fira Sans', sans-serif;" Text="8. Other" runat="server"/>

                                </div>
                                
                                   <div class="form-group_2">

                                    <asp:CheckBox ID="SpecialDocumentUpload" CssClass="SelectchkChoice"  style=" margin: .4rem;  font: 1rem 'Fira Sans', sans-serif;" Text="9. Special Allowance Approval Documents" runat="server"/>

                                </div>

                              
                                </div>

                        </div>

       <br/>
       
  <div class="form-row" >
              <div class="col-md-3">
                                
                    <label  class="title-widget">Claim Information </label> 
                                           
             </div>
  </div>  
      

<hr />
       
       
       



               <div id="gridContainer2" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;">
                                <asp:GridView ID="GridView1" runat="server" ShowFooter="True"  AutoGenerateColumns="False"    DataKeyNames="Amount,IsIncrement"  CssClass="table table-striped table-bordered"   OnPreRender="gv_DocumentUpload_PreRender">
                                    <Columns>
                                     
          



                 <asp:TemplateField HeaderText="Particulars  <span class='star'>*</span>">
                            <ItemTemplate>
                    <%--        <asp:TextBox ID="HeadOfExpense" Enabled="False" runat="server"  Text='<%# (Eval("HeadOfExpense")=="" || Eval("HeadOfExpense")==null) ? "0" : Eval("HeadOfExpense")%>'  CssClass="form-control form-control-sm"></asp:TextBox>--%>
                                
                                <asp:Label ID="HeadOfExpense"  runat="server"  Text='<%# (Eval("HeadOfExpense")=="" || Eval("HeadOfExpense")==null) ? "0" : Eval("HeadOfExpense")%>' ></asp:Label>
                                 <asp:HiddenField runat="server" ID="hfHeadOfExpenseId" Value='<%#Eval("OIPDHeadOfExpenseId")%>' />
                           </ItemTemplate>
                         </asp:TemplateField>
                                        
                                        
                                        
                                        <asp:TemplateField HeaderText="Max Limit(BDT)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="Amounttt" ReadOnly="True" runat="server" Text='<%# (Eval("Amount")=="" || Eval("Amount")==null) ? "0" : Eval("Amount")%>' OnTextChanged="Amount_OnTextChanged" AutoPostBack="True"   CssClass="form-control form-control-sm"></asp:TextBox>
                         
                                                <ajaxToolkit:FilteredTextBoxExtender ID="ssss1s" runat="server"
                                                                                     TargetControlID="Amount"
                                                                                     FilterType="Custom, Numbers"
                                                                                     ValidChars="." />
                                            </ItemTemplate>
                           
                                        </asp:TemplateField>
                                        
                                        
                                        
                                        
         <asp:TemplateField HeaderText="Document Date  <span class='star'>*</span>" >
            <ItemTemplate>
         
                
                
                     <asp:TextBox ID="txtDate" autocomplete="off" AutoPostBack="True"  OnTextChanged="Date_OnTextChanged" runat="server" class="form-control form-control-sm" Text='<%# (Eval("Dates")=="" || Eval("Dates")==null) ? "0" : Eval("Dates")%>'  CausesValidation="true"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender" runat="server" PopupPosition="TopLeft"
                                                Format="dd-MMM-yyyy" PopupButtonID="txtDate" CssClass="MyCalendar"
                                                TargetControlID="txtDate" />

            </ItemTemplate>  
        </asp:TemplateField>
                                        
                                        
                                        
                                        
                                        
                                        <asp:TemplateField HeaderText="SI. No of Enclosed Voucher(Ref. No)  <span class='star'>*</span>">
                                            <ItemTemplate>
                                                <asp:TextBox ID="Voucher" autocomplete="off" runat="server" AutoPostBack="True" OnTextChanged="Voucher_OnTextChanged" Text='<%# (Eval("SINoOfEncloseVoucher")=="" || Eval("SINoOfEncloseVoucher")==null) ? "0" : Eval("SINoOfEncloseVoucher")%>'  CssClass="form-control form-control-sm"></asp:TextBox>
                                            </ItemTemplate>
                                         <%--   <FooterStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:Label ID="lbdflTotalMark" Text="Total:" CssClass="form-control  form-control-sm " runat="server" />
                                            </FooterTemplate>--%>
                                        </asp:TemplateField>


                                   <asp:TemplateField HeaderText="Number of Days  <span class='star'>*</span>" >
                                            <ItemTemplate>

                                                <asp:TextBox ID="NumberOfdays" autocomplete="off" runat="server" AutoPostBack="True" OnTextChanged="txtRent_OnTextChanged" class="form-control form-control-sm" Text='<%# (Eval("NoOfDays")=="" || Eval("NoOfDays")==null) ? "0" : Eval("NoOfDays")%>'  CausesValidation="true"></asp:TextBox>

                                            </ItemTemplate>  
                                        </asp:TemplateField>
                                        
                                        
                                        <asp:TemplateField HeaderText="Amount (BDT) <span class='star'>*</span>" >
                                            <ItemTemplate>

                                                <asp:TextBox ID="txtRent" autocomplete="off" Text='<%# (Eval("Rent")=="" || Eval("Rent")==null) ? "0" : Eval("Rent")%>'  runat="server" class="form-control form-control-sm" AutoPostBack="True" OnTextChanged="txtRent_OnTextChanged" ReadOnly='<%# (Eval("YourConditionField").ToString().Equals("Yes")) %>'  CausesValidation="true"></asp:TextBox>
                                                
                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtRentFiltered" runat="server"
                                                                                     TargetControlID="txtRent"
                                                                                     FilterType="Custom, Numbers"
                                                                                     ValidChars="." />

                                            </ItemTemplate>  
                                        </asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="No. of Children" >
                                            <ItemTemplate>

                                                <asp:TextBox ID="txtChildrenNo" autocomplete="off" Text='<%# (Eval("ChildrenNo")=="" || Eval("ChildrenNo")==null) ? "0" : Eval("ChildrenNo")%>' ReadOnly='<%# (Eval("YourConditionField").ToString().Equals("Yes")) %>'  runat="server" class="form-control form-control-sm" AutoPostBack="True" OnTextChanged="txtChildrenNo_OnTextChanged"  CausesValidation="true"></asp:TextBox>
                                                
                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtRentFiljjtered" runat="server"
                                                                                     TargetControlID="txtChildrenNo"
                                                                                     FilterType="Custom, Numbers"
                                                                                     ValidChars="." />

                                            </ItemTemplate>  
                                        </asp:TemplateField>

                                        
          
                                        
          
                                        

             <asp:TemplateField HeaderText="Total Amount (BDT)">
                     <ItemTemplate>
                          <asp:TextBox ID="Amount" runat="server"  ReadOnly="True" Text='<%# (Eval("Amount_new")=="" || Eval("Amount_new")==null) ? "0" : Eval("Amount_new")%>' OnTextChanged="Amount_OnTextChanged" AutoPostBack="True"   CssClass="form-control form-control-sm"></asp:TextBox>
                         
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
                                        
                                        
                                          <asp:TemplateField HeaderText="Remarks">
                     <ItemTemplate>
                                      <asp:TextBox ID="txt_RemaksforHR"   runat="server"   class="form-control"  TextMode="MultiLine" Rows="2" Text='<%#Eval("RemaksforHR") %>'  ></asp:TextBox>
                     </ItemTemplate>
                 
             </asp:TemplateField>
                                       

                                        <asp:TemplateField HeaderText="Check Point" Visible="False">
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server"  ID="valueCheck"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        

                                    </Columns>
                                </asp:GridView>
                   
                   
                    <asp:GridView ID="gv_OPD" runat="server" ShowFooter="True"  AutoGenerateColumns="False"    DataKeyNames="Amount,IsIncrement"  CssClass="table table-striped table-bordered"   OnPreRender="gv_DocumentUpload_PreRender">
                                    <Columns>
                                     
          



                 <asp:TemplateField HeaderText="Particulars">
                            <ItemTemplate>
                    <%--        <asp:TextBox ID="HeadOfExpense" Enabled="False" runat="server"  Text='<%# (Eval("HeadOfExpense")=="" || Eval("HeadOfExpense")==null) ? "0" : Eval("HeadOfExpense")%>'  CssClass="form-control form-control-sm"></asp:TextBox>--%>
                                
                                <asp:Label ID="HeadOfExpense"  runat="server"  Text='<%# (Eval("HeadOfExpense")=="" || Eval("HeadOfExpense")==null) ? "0" : Eval("HeadOfExpense")%>' ></asp:Label>
                                 <asp:HiddenField runat="server" ID="hfHeadOfExpenseId" Value='<%#Eval("OIPDHeadOfExpenseId")%>' />
                           </ItemTemplate>
                         </asp:TemplateField>
                                        
                                        
                                        
                                        <asp:TemplateField HeaderText="Max Limit(BDT)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="Amounttt" ReadOnly="True" runat="server" Text='<%# (Eval("Amount")=="" || Eval("Amount")==null) ? "0" : Eval("Amount")%>' OnTextChanged="Amount_OnTextChanged" AutoPostBack="True"   CssClass="form-control form-control-sm"></asp:TextBox>
                         
                                                <ajaxToolkit:FilteredTextBoxExtender ID="ssss1s" runat="server"
                                                                                     TargetControlID="Amount"
                                                                                     FilterType="Custom, Numbers"
                                                                                     ValidChars="." />
                                            </ItemTemplate>
                           
                                        </asp:TemplateField>
                                        
                                        
                                        
                                        
         <asp:TemplateField HeaderText="Document Date   <span class='star'>*</span>" >
            <ItemTemplate>
         
                
                
                     <asp:TextBox ID="txtDate" autocomplete="off" AutoPostBack="True"  OnTextChanged="OPDDate_OnTextChanged" runat="server" class="form-control form-control-sm" Text='<%# (Eval("Dates")=="" || Eval("Dates")==null) ? "0" : Eval("Dates")%>'  CausesValidation="true"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender" runat="server" PopupPosition="TopLeft"
                                                Format="dd-MMM-yyyy" PopupButtonID="txtDate" CssClass="MyCalendar"
                                                TargetControlID="txtDate" />

            </ItemTemplate>  
        </asp:TemplateField>
                                        
                                        
                                        
                                        
                                        
                                        <asp:TemplateField HeaderText="SI. No of Enclosed Voucher(Ref. No)   <span class='star'>*</span>">
                                            <ItemTemplate>
                                                <asp:TextBox ID="Voucher" autocomplete="off" runat="server" Text='<%# (Eval("SINoOfEncloseVoucher")=="" || Eval("SINoOfEncloseVoucher")==null) ? "0" : Eval("SINoOfEncloseVoucher")%>'  CssClass="form-control form-control-sm"></asp:TextBox>
                                            </ItemTemplate>
                                         <%--   <FooterStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:Label ID="lbdflTotalMark" Text="Total:" CssClass="form-control  form-control-sm " runat="server" />
                                            </FooterTemplate>--%>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Number of Days   <span class='star'>*</span>" Visible="False">
                                            <ItemTemplate>

                                                <asp:TextBox ID="NumberOfdays" autocomplete="off" runat="server" AutoPostBack="True" OnTextChanged="NumberOfdays_OnTextChanged" class="form-control form-control-sm" Text='<%# (Eval("NoOfDays")=="" || Eval("NoOfDays")==null) ? "0" : Eval("NoOfDays")%>'  CausesValidation="true"></asp:TextBox>

                                            </ItemTemplate>  
                                        </asp:TemplateField>
                                        
                                        
                                        <asp:TemplateField HeaderText="Amount  (BDT)  <span class='star'>*</span>" >
                                            <ItemTemplate>

                                                <asp:TextBox ID="txtRent" Text='<%# (Eval("Rent")=="" || Eval("Rent")==null) ? "0" : Eval("Rent")%>'  autocomplete="off" runat="server" class="form-control form-control-sm" AutoPostBack="True" OnTextChanged="txtRentOPD_OnTextChanged"  CausesValidation="true"></asp:TextBox>
                                                
                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtRentFiltered" runat="server"
                                                                                     TargetControlID="txtRent"
                                                                                     FilterType="Custom, Numbers"
                                                                                     ValidChars="." />

                                            </ItemTemplate>  
                                            
                                             <FooterStyle HorizontalAlign="left" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblRentTotalMark" CssClass="form-control  form-control-sm " runat="server" />
                                        </FooterTemplate>
                                        </asp:TemplateField>

                                        
          
                                        

             <asp:TemplateField HeaderText="Amount (BDT) " Visible="False">
                     <ItemTemplate>
                          <asp:TextBox ID="Amount" runat="server"  ReadOnly="True" Text='<%# (Eval("Amount")=="" || Eval("Amount")==null) ? "0" : Eval("Amount")%>' OnTextChanged="Amount_OnTextChanged" AutoPostBack="True"   CssClass="form-control form-control-sm"></asp:TextBox>
                         
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
                                           <asp:TemplateField HeaderText="Remarks">
                     <ItemTemplate>
                                      <asp:TextBox ID="txt_RemaksforHR"   runat="server"   class="form-control"  TextMode="MultiLine" Rows="2" Text='<%#Eval("RemaksforHR") %>'  ></asp:TextBox>
                     </ItemTemplate>
                 
             </asp:TemplateField>
                                       

                                        <asp:TemplateField HeaderText="Check Point" Visible="False">
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server"   ID="valueCheck"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        

                                    </Columns>
                                </asp:GridView>
                            </div>
       
            
       <br/>
       <br/>

     <%--    <div class="form-row" >
              <div class="col-md-12">
                                
                    <label style="" class="title-widget"> Medical Support Committee </label> 
                                           
             </div>
  </div>  --%>
       
       <hr />
       
       
        <div class="row" style="padding-top:10px" runat="server" Visible="False">
                 <div class="col-md-12">
                     
                      <asp:GridView Width="100%" ShowHeader="True" ID="gv_Member" runat="server" AutoGenerateColumns="false" CssClass="blueTableNew"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                           
                                                 <asp:TemplateField HeaderText="Type" Visible="False">
                                                    <ItemTemplate>
                                                     <asp:RadioButtonList runat="server" ID="rbType" CssClass="chkChoice" AutoPostBack="True" OnSelectedIndexChanged="rbType_OnSelectedIndexChanged"  RepeatDirection="Horizontal">
                                                               
                
                 <asp:ListItem Selected="True">Employee</asp:ListItem>
                    <%--<asp:ListItem>Guest</asp:ListItem>--%>
                                                           </asp:RadioButtonList>
                                                        
                                                        <asp:HiddenField runat="server" ID="hfType" Value='<%#Eval("Type")%>' />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 
                                                <asp:TemplateField HeaderText="Employee ID" >
                                                    <ItemTemplate>
                                                        <asp:TextBox ReadOnly="True" ID="txt_EmpMasterCode" CssClass="form-control form-control-sm " runat="server" Text='<%#Eval("EmpMasterCode") %>'></asp:TextBox>
                                                        
                                                        <asp:HiddenField runat="server" ID="MemEmpInfoId" Value='<%#Eval("EmpInfoId")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                             
                                                 <asp:TemplateField HeaderText="Employee Name">
                                                    <ItemTemplate>
                                                        
                                                          <asp:DropDownList Width="80%" ID="ddlEmpName"   CssClass="form-control form-control-sm selectme" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpName_OnSelectedIndexChanged"  runat="server"  ></asp:DropDownList>

                                                        <asp:TextBox ID="txt_EmpName" Visible="False" CssClass="form-control form-control-sm"  runat="server" Text='<%#Eval("EmpName") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                 <asp:TemplateField HeaderText="Designation">
                                                    <ItemTemplate>
                                                        <asp:TextBox ReadOnly="True" ID="txt_Designation"  CssClass="form-control form-control-sm"  runat="server" Text='<%#Eval("Designation") %>'></asp:TextBox>
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

       <hr/>
             
        <fieldset class="for-panel">
                                <legend>Document </legend>
                                        <div class="row">
                                           

                                            <asp:HiddenField runat="server" ID="hfDocFileName"/>
                                            <asp:HiddenField runat="server" ID="hfDocFile"/>
                                            <div class="col-4">
                                                <div class="form-group">
                                                    <label>Document Upload<span   style="color:red; " title="please fill out this field"> * </span> <span style="color: gray!important">Supported Files are:[jpg, jpeg, png,xlsx,pdf,txt,doc,docx]</span></label>
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
                                            

                                            
                                                              <div class="col-md-2">
    
    <div id="attachmentPrescriptionContainer" runat="server" class="form-group_2 attachment-criteria-item">
                 
        <asp:RadioButton ID="chkPrescriptions" GroupName="AttachmentCriteria" AutoPostBack="True" OnCheckedChanged="AttachmentCriteria_OnCheckedChanged" CssClass="SelectchkChoice" style=" margin: .4rem;  font: 1rem 'Fira Sans', sans-serif;"  Text="Prescriptions" runat="server"/>

    </div>

    <div id="attachmentBillsContainer" runat="server" class="form-group_2 attachment-criteria-item">

        <asp:RadioButton ID="chkBills" GroupName="AttachmentCriteria" AutoPostBack="True" OnCheckedChanged="AttachmentCriteria_OnCheckedChanged" CssClass="SelectchkChoice"  style=" margin: .4rem;  font: 1rem 'Fira Sans', sans-serif;" Text="Bills" runat="server"/>

    </div>

    <div id="attachmentReportsContainer" runat="server" class="form-group_2 attachment-criteria-item">

        <asp:RadioButton ID="chkReports" GroupName="AttachmentCriteria" AutoPostBack="True" OnCheckedChanged="AttachmentCriteria_OnCheckedChanged" CssClass="SelectchkChoice" style=" margin: .4rem;  font: 1rem 'Fira Sans', sans-serif;"  Text="Reports" runat="server"/>

    </div>
                     
                                                                                                                                                                                                           
</div>
                
                
  <div class="col-md-2">
    
    <div id="attachmentDischargeContainer" runat="server" class="form-group_2 attachment-criteria-item">
                 
        <asp:RadioButton ID="chkDischargePaper" GroupName="AttachmentCriteria" AutoPostBack="True" OnCheckedChanged="AttachmentCriteria_OnCheckedChanged" CssClass="SelectchkChoice"   style=" margin: .4rem;  font: 1rem 'Fira Sans', sans-serif;"  Text="Discharge Paper" runat="server"/>

    </div>

    
    <div id="attachmentOtherContainer" runat="server" class="form-group_2 attachment-criteria-item">

        <asp:RadioButton ID="ckOther" GroupName="AttachmentCriteria" CssClass="SelectchkChoice" AutoPostBack="True" OnCheckedChanged="AttachmentCriteria_OnCheckedChanged"  style=" margin: .4rem;  font: 1rem 'Fira Sans', sans-serif;" Text="Other Documents" runat="server"/>

    </div>
                     
                                                                                                                                                                                                                     
    
</div>
                                            
                                            
                                            <div class="col-4">
                                            <div class="form-group" runat="server" ID="Description_" Visible="False">
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
                                                   
                                                    
                                                          <asp:GridView Width="100%" ShowHeader="True" ID="gv_DocumentUpload" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Document">
                                                    

                                                    <ItemTemplate>
                                                         <a class="btn btn-sm btnMyDesignSearch"     Target="_blank"   href="<%# Eval("DocumentLink") %>">Preview</a>
                                                        

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
                                 
                                                <asp:TemplateField HeaderText="Short Description">
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
       

       <br />
  

       
       


        <div class="form-row" runat="server" ID="button">
               <div class="col-md-5">
       </div>
                <div class="col-md-3">
                                        <div class="form-group" style="margin-top: 17px;">
                                             <asp:LinkButton runat="server" ID="drrft"  OnClick="drrft_OnClick"   CssClass="btn btnMyDesignAddtoList   btn-sm"><span aria-hidden="true" class="fa fa-check-square"></span>  &nbsp; Draft </asp:LinkButton>
                                             <asp:LinkButton runat="server" ID="SearchButton"  OnClick="Save_OnClick"   CssClass="btn btnMyDesignSearch   btn-sm"><span aria-hidden="true" class="fa fa-check-square"></span>  &nbsp; Submit </asp:LinkButton>

                                            <asp:LinkButton runat="server" ID="btnReset"  CssClass="btn btnMyDesignReset   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset </asp:LinkButton>
                                          
                                        </div> 
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
            var validExtensions = [
                'jpg', 'jpeg', 'png', 'xlsx', 'pdf', 'txt', 'doc', 'docx'
            ];

            if (input.files && input.files.length > 0) {
                var file = input.files[0];
                var fileName = file.name;
                var fileExtension = fileName.substring(fileName.lastIndexOf('.') + 1).toLowerCase();

                if ($.inArray(fileExtension, validExtensions) === -1) {
                    alert("Only these file types are accepted: jpg, jpeg, png, xlsx, pdf, txt, doc, docx");
                    input.value = ""; // Clear file input
                    return;
                }

                var fileSize = file.size; // in bytes

                if (fileSize > 5 * 1024 * 1024) { // 5 MB
                    alert("File size must be 5MB or less.");
                    input.value = ""; // Clear file input
                    return;
                }

                // Optional: preview logic (e.g., for images)
                var reader = new FileReader();
                reader.onload = function (e) {
                    // You can preview image here using e.target.result
                    // Example:
                    // $('#imgPreview').attr('src', e.target.result);
                };
                reader.readAsDataURL(file);
            }
        }

    </script>
    
    
</asp:Content>


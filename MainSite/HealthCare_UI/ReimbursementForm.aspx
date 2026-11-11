<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="ReimbursementForm.aspx.cs" Inherits="HealthCare_UI_ReimbursementForm" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
    
    
    
     <style>
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
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server"> <style>
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
         font-size: 20px;
         font-weight: bold;
	 
         position: relative;
	 
         font-family: 'Fjalla One', sans-serif;
         margin-top: 0;
         margin-right: 0;
         margin-bottom: 10px;
	 
	 
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

         </style>

       
   
   
   
   <br/>
   <div class="form-row" >
       <div class="col-md-3">
                                
           <label class="title-widget">Employment  Information</label> 
                                           
       </div>
   </div>     
   <hr />


       <div class="row">
               <div class="col-md-12">

                     <div class="form-group ">   
                                 

                                 
                            <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Company: <span class="star">*</span></label></div>
                                <div class="col-md-4">
                                      <asp:DropDownList ID="ddlCompany" runat="server" Enabled="False" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" CssClass="form-control form-control-sm" AutoPostBack="True"></asp:DropDownList> 
                                    <asp:HiddenField ID="hfMasterId" runat="server" />
                                    <asp:HiddenField ID="HFReimbursmentId" runat="server" />
                                    <asp:HiddenField runat="server" ID="HFEntryBy"/>
                                </div>
                            </div>
                          
                           <div style="padding-top: 5px;"></div>
                          <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Financial Year:  <span class="star">*</span></label></div>
                                <div class="col-md-4"> <asp:DropDownList ID="ddlFinancialYear" AutoPostBack="True" OnSelectedIndexChanged="ddlFinancialYear_OnSelectedIndexChanged" runat="server"  CssClass="form-control form-control-sm" ></asp:DropDownList> </div>
                            </div>
                         
                          <div style="padding-top: 5px;"></div>
                          <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Employee Name:</label></div>
                                <div class="col-md-4">
                                             <asp:DropDownList   runat="server"  AutoPostBack="True" OnSelectedIndexChanged="ddlForwordEmp_OnSelectedIndexChanged" Enabled="False" ID="ddlForwordEmp" CssClass="form-control form-control-sm selectme" />
                                                   
                                                     <script type="text/javascript">
                                                         function pageLoad() {
                                                             $('.selectme').chosen({ disable_search_threshold: 5, search_contains: true });

                                                         }
                                                     </script>
                                </div>
                            </div>
                         
                          <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right"> Type:  <span class="star">*</span></label></div>
                                <div class="col-md-4" style="padding-top:8px">

                                    <asp:RadioButtonList ID="inlineRadio" RepeatDirection="Horizontal" AutoPostBack="True" CssClass="chkChoiceHeader" OnSelectedIndexChanged="inlineRadio_OnSelectedIndexChanged"  runat="server">
                                        <asp:ListItem Value="OPD">OPD</asp:ListItem>
                                        <asp:ListItem Value="IPD">IPD</asp:ListItem>
                                        <asp:ListItem Value="Special">Special</asp:ListItem>
                               
                                    </asp:RadioButtonList>
                                    

                                      
                                </div>
                            </div>

                           <div style="padding-top: 5px;"></div>
                          <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Remaining Balance:</label></div>
                                <div class="col-md-4">  <asp:TextBox runat="server" ReadOnly="True"   ID="RemainingBalance"  class="form-control form-control-sm" /></div>
                            </div>
                         
                         

                         <div style="padding-top: 5px;"></div>
                         <div class="row">
                             <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Application Submit Date:<span class="star">*</span></label></div>
                             <div class="col-md-4">  <asp:TextBox runat="server"  autocomplete="off"  ID="SubmitDate" disabled="disabled"  class="form-control form-control-sm" />
                                 <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="TopLeft"
                                                       Format="dd-MMM-yyyy" PopupButtonID="ImageButtosn1" CssClass="MyCalendar"
                                                       TargetControlID="SubmitDate" />
                             </div>
                         </div>
                         
                         
                         <div style="padding-top: 5px;"></div>
                         <div class="row">
                             <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Illness Description:<span class="star">*</span></label></div>
                             <div class="col-md-4">  <asp:TextBox runat="server"  autocomplete="off"   ID="SelfDate"  class="form-control" />
                           
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
                                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Employee ID</td>
                                                        <td> <asp:Label runat="server" ID="lblEmpId"></asp:Label>
                                                            <asp:HiddenField runat="server" ID="hfEmpID"/>
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
                                                         <td class="tblTHColorChang"  style="width: 15%; padding: 10px;">Cell No<span class="star">*</span></td>
    <td>     
                 <asp:TextBox runat="server"  ID="txtOfficailMobile" class="form-control form-control-sm" />
    </td>

                                                          <td style="width: 15%; padding: 10px;" class="tblTHColorChang" >Place</td>
                                                       <td> <asp:Label runat="server" ID="lblPlace"></asp:Label></td>
                                                   </tr>
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
                           

       </div>
          
            <hr />  
       
       
       <div class="row" style="padding-left:12px">
                                 <table class="table table-bordered table-striped" >
                                                    <tr>
                                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Account Name</td>
                                                        <td> <asp:Label runat="server" ID="lblBankName"></asp:Label></td>

                                                        
                                                         <td  class="tblTHColorChang" style="width: 20%; padding: 10px;">Account No</td>
                                                        <td>  <asp:Label ID="lblBankAccountNo"  runat="server"></asp:Label></td>
                                                    </tr>
                                                    
                                                     <tr>
                                                        <td  class="tblTHColorChang" style="width: 20%; padding: 10px;">Branch Name</td>
                                                        <td> <asp:Label runat="server" ID="lblBranchName"></asp:Label></td>

                                                         
                                                              
                                                         <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Branch And Routing No</td>
                                                        <td>  <asp:Label ID="lblRoutingNo"  runat="server"></asp:Label></td>
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
       </div>
       
   <hr />
       
  <div class="row">
               <div class="col-md-6">

                     <div class="form-group ">                                                    
                            <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Name of Patient: <span class="star">*</span></label></div>
                                <div class="col-md-6">  <asp:TextBox runat="server"  ID="NameofPatient"  class="form-control form-control-sm" /></div>
                            </div>
                            <div style="padding-top: 5px;"></div>
                            <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Age: </label></div>
                                <div class="col-md-6">  <asp:TextBox runat="server"    ID="Age"  class="form-control form-control-sm" />
                                    
                                      <ajaxToolkit:FilteredTextBoxExtender ID="freqQtyTextBox" runat="server"
                                                    TargetControlID="Age"
                                                    FilterType="Custom, Numbers"
                                                    ValidChars="." />
                                </div>
                            </div>
                           <div style="padding-top: 5px;"></div>
                            <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Relationship: <span class="star">*</span></label></div>
                                <div class="col-md-6">  <asp:TextBox runat="server" ReadOnly="True" ID="Relationship"  class="form-control form-control-sm" /></div>
                            </div>                     
                       </div>
                   </div>       
      
      <div class="col-md-6">
          
          <div class="form-group">
                                     
              <asp:LinkButton runat="server" ID="LinkButton1"  data-toggle="modal" data-target="#exampleModal2"  CssClass="btn btn-sm btnMyDesignOne">Add Patient </asp:LinkButton>
          </div>
      </div>
              
             </div>
       
       
       <br/>
       <br/>
       
      <div class="form-row" >
              <div class="col-md-3">
                                
                                    <label class="title-widget">Brief Description of illness </label> 
                                           
                            </div>
       </div>   

 <hr />
                                           


       
      
       
  <div id="gridContainer1" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: hidden;">     
                            
  <asp:GridView ID="loadGridView" runat="server" ShowFooter="true" AutoGenerateColumns="false" DataKeyNames="YesNo1,Date,YesNo"  OnPreRender="gv_DocumentUpload_PreRender"
                                    CssClass="AddToListCssTable">
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
              <asp:CheckBox ID="chkYes"  AutoPostBack="True" OnCheckedChanged="btnRemove_OnClick"  runat="server" />
            </ItemTemplate>  
        </asp:TemplateField>
        
             
         <asp:TemplateField HeaderText="No" >
            <ItemTemplate>       
                 <asp:CheckBox ID="chkNo"  Checked="True" AutoPostBack="True" OnCheckedChanged="NoCheck_OnChanged"  runat="server"  />
            </ItemTemplate>  
        </asp:TemplateField>
        
     
            <asp:TemplateField HeaderText="Date" >
            <ItemTemplate>
                <asp:TextBox ID="txtDate" runat="server"  Enabled="False" Text='<%# Eval("Descriptiondate")%>'  CssClass="form-control form-control-sm"></asp:TextBox>
                     <cc1:CalendarExtender ID="CalendarExtender" runat="server" PopupPosition="TopLeft"
                                                Format="dd-MMM-yyyy" PopupButtonID="ImageButtosn1" CssClass="MyCalendar"
                                                TargetControlID="txtDate" />
            </ItemTemplate>  
        </asp:TemplateField>
        
        

    </Columns>
</asp:GridView>

                            </div>

       
       <br/>
       
                     <br/>
  <div class="form-row">
              <div class="col-md-12">
                            
                                    <label class="title-widget">Enclosures(Tick)mark the appropriate Box. Put serial number on every enclosed document </label>  <span class="star-mark"></span>
                              
                                
                            </div>

       </div>
        <hr />
   <div class="form-row">
                                    
                            <div class="col-md-3">
                                
                                <div class="form-group">
                                             
                                    <asp:CheckBox ID="Prescription" style=" margin: .4rem;  font: 1rem 'Fira Sans', sans-serif;"  Text="PhotocCopy of the Prescription" runat="server"/>
                                    
                                    <style>

                                    </style>
                                </div>

                                <div class="form-group">

                                    <asp:CheckBox ID="BillofConsaltation"  style=" margin: .4rem;  font: 1rem 'Fira Sans', sans-serif;"  Text="Original Bill of Consaltation" runat="server"/>

                                </div>
                                
                                <div class="form-group">

                                    <asp:CheckBox ID="BillofMedicine" style=" margin: .4rem;  font: 1rem 'Fira Sans', sans-serif;" Text="Original Bill of Medicine" runat="server"/>

                                </div>

                                <div class="form-group">

                                    <asp:CheckBox ID="BillofHospitalization" style=" margin: .4rem;  font: 1rem 'Fira Sans', sans-serif;" Text="Original Bill of Hospitalization" runat="server"/>

                                </div>
                                
                                <div class="form-group">

                                    <asp:CheckBox ID="SpecialDocumentUpload" style=" margin: .4rem;  font: 1rem 'Fira Sans', sans-serif;" Text="Special Allowance Approval Documents" runat="server"/>

                                </div>

                                
                            </div>
               
               
                            <div class="col-md-2">
                               
                                <div class="form-group">
                                             
                                    <asp:CheckBox ID="BillofInvestigation"  style=" margin: .4rem;  font: 1rem 'Fira Sans', sans-serif;"  Text="Original Bill of Investigation" runat="server"/>

                                </div>

                                <div class="form-group">

                                    <asp:CheckBox ID="PhotoCopyofInvestigation" style=" margin: .4rem;  font: 1rem 'Fira Sans', sans-serif;" Text="PhotoCopy of Investigation" runat="server"/>

                                </div>
                                
                                <div class="form-group">

                                    <asp:CheckBox ID="BillForCharges" style=" margin: .4rem;  font: 1rem 'Fira Sans', sans-serif;" CssClass="chkChoiceStep" Text="Original Bill For Charges" runat="server"/>

                                </div>

                                <div class="form-group">

                                    <asp:CheckBox ID="Other" style=" margin: .4rem;  font: 1rem 'Fira Sans', sans-serif;" Text="Other" runat="server"/>

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
                                <asp:GridView ID="GridView1" runat="server" ShowFooter="True"  AutoGenerateColumns="False"    DataKeyNames="Amount,IsIncrement" CssClass="blueTableNew"   OnPreRender="gv_DocumentUpload_PreRender">
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
                                        
                                        
                                        
                                        
         <asp:TemplateField HeaderText="Document Date" >
            <ItemTemplate>
         
                
                
                     <asp:TextBox ID="Date" autocomplete="off" AutoPostBack="True"  OnTextChanged="Date_OnTextChanged" runat="server" class="form-control form-control-sm" Text='<%# (Eval("Dates")=="" || Eval("Dates")==null) ? "0" : Eval("Dates")%>'  CausesValidation="true"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender" runat="server" PopupPosition="TopLeft"
                                                Format="dd-MMM-yyyy" PopupButtonID="ImageButtosn1" CssClass="MyCalendar"
                                                TargetControlID="Date" />

            </ItemTemplate>  
        </asp:TemplateField>
                                        
                                        
                                        
                                        
                                        
                                        <asp:TemplateField HeaderText="SI. No of Enclosed Voucher(Ref. No)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="Voucher" autocomplete="off" runat="server" Text='<%# (Eval("SINoOfEncloseVoucher")=="" || Eval("SINoOfEncloseVoucher")==null) ? "0" : Eval("SINoOfEncloseVoucher")%>'  CssClass="form-control form-control-sm"></asp:TextBox>
                                            </ItemTemplate>
                                         <%--   <FooterStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:Label ID="lbdflTotalMark" Text="Total:" CssClass="form-control  form-control-sm " runat="server" />
                                            </FooterTemplate>--%>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Number of Days" >
                                            <ItemTemplate>

                                                <asp:TextBox ID="NumberOfdays" autocomplete="off" runat="server" AutoPostBack="True" OnTextChanged="NumberOfdays_OnTextChanged" class="form-control form-control-sm" Text='<%# (Eval("NoOfDays")=="" || Eval("NoOfDays")==null) ? "0" : Eval("NoOfDays")%>'  CausesValidation="true"></asp:TextBox>

                                            </ItemTemplate>  
                                        </asp:TemplateField>
                                        
                                        
                                        <asp:TemplateField HeaderText="Rent" >
                                            <ItemTemplate>

                                                <asp:TextBox ID="txtRent" autocomplete="off" runat="server" class="form-control form-control-sm" AutoPostBack="True" OnTextChanged="txtRent_OnTextChanged"  CausesValidation="true"></asp:TextBox>
                                                
                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtRentFiltered" runat="server"
                                                                                     TargetControlID="txtRent"
                                                                                     FilterType="Custom, Numbers"
                                                                                     ValidChars="." />

                                            </ItemTemplate>  
                                        </asp:TemplateField>

                                        
          
                                        

             <asp:TemplateField HeaderText="Amount (BDT)">
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
                                       

                                        <asp:TemplateField HeaderText="Check Point">
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" AutoPostBack="True"  OnCheckedChanged="valueCheck_OnCheckedChanged" ID="valueCheck"/>
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


            debugger;
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
    
    
    
    <script type="text/javascript">
        function CheckOtherIsCheckedByGVID(spanChk) {
            var IsChecked = spanChk.checked;
            if (IsChecked) {
                spanChk.checked = true;

            } else {
                spanChk.checked = false;
            }
        }
    </script>  
    
    

</asp:Content>



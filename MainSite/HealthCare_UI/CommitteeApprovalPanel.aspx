<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="CommitteeApprovalPanel.aspx.cs" Inherits="HealthCare_UI_CommitteeApprovalPanel" %>
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
                             
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" /> Committee Approval Panel </h1>
                        </div>
                         <div class="page-heading__container float-right d-none d-sm-block">
                         
                              <asp:LinkButton ID="LinkButton3"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="AddNewButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
          
                             

                <%--<asp:Button ID="reloadButton" Text="Reload" CssClass="btn btn-sm btn-outline-success" runat="server" OnClick="reloadButton_OnClick" />--%>
            </div>
                    </div>
                     
<div class="card">
   <div class="card-body">
       <asp:UpdatePanel ID="UpdatePanel1" runat="server"> 
           <ContentTemplate>

               <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                   <ProgressTemplate>
                       <div class="divWaiting">
                           <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                       </div>
                   </ProgressTemplate>
               </asp:UpdateProgress>




    <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />

<%--       <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.19/css/dataTables.bootstrap4.min.css" />
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.19/js/dataTables.bootstrap4.min.js"></script>
 <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/buttons/1.5.6/js/dataTables.buttons.min.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/buttons/1.5.6/js/buttons.flash.min.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/buttons/1.5.6/js/buttons.html5.min.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/buttons/1.5.6/js/buttons.print.min.js"></script>--%>

   <%--   <script>

          $(document).ready(function () {
  


    
              var table = $('#cpFormBody_gv_ViewList').DataTable(
                   {
                       "bInfo": true,
                       "bFilter": true,
                       lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "All"]],
                       pageLength: 10,
                       dom: 'lBfrtip',

                       buttons: [


                       {
                           extend: 'excel',
                           footer: true,
                           text: '<i class="fa fa-file-excel-o" > Excel </i>',
                           titleAttr: 'Export to Excel',
                           filename: 'Expense Reimbursement Form Approval List',
                           title: 'SMC',
                           messageTop: 'Expense Reimbursement Form Approval List',
                           exportOptions: {
                               columns: [1, 2, 3, 4, 5, 6]
                           }
                       }
                       ]
                   }
                  );

              var prm = Sys.WebForms.PageRequestManager.getInstance();
              if (prm != null) {
                  prm.add_endRequest(function (sender, e) {
                      if (sender._postBackSettings.panelsToUpdate != null) {
                          table = $('#cpFormBody_gv_ViewList').DataTable(
                          {
                              "bInfo": true,
                              "bFilter": true,
                              lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "All"]],
                              pageLength: 10,
                              dom: 'lBfrtip',

                              buttons: [


                              {
                                  extend: 'excel',
                                  footer: true,
                                  text: '<i class="fa fa-file-excel-o" > Excel </i>',
                                  titleAttr: 'Export to Excel'
                           ,
                                  filename: 'Miscellaneous Information List',
                                  title: 'SMC',
                                  messageTop: 'Miscellaneous Information List',
                                  exportOptions: {
                                      columns: [1, 2, 3, 4, 5, 6]
                                  }
                              }
                              ]


                          }
                          );
                      }
                  });
              };


              table.columns().every(function () {
                  var that = this;

                  $('input', this.footer()).on('keyup change', function () {
                      if (that.search() !== this.value) {
                          that
                              .search(this.value)
                              .draw();
                      }
                  });
              });
          });

      </script>
    --%>
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

           
           .chkChoiceHeader label {
               /*padding-left: 60px !important;*/
               padding-left: 3px ;
           
               font-size: 13px;
               font-weight: bold;
           }

           .tblTHColorChang{
               background-color: #EDF2F5!important;
               font-weight: bold;
               font-size: 13px;
           }
                         .star{
                 color:red;
             }                  
       </style>
           
        <%--   <style>
               .V1 {
                   padding-right: 600px;
               }
           </style>--%>

                    <div  runat="server" ID="ActionBtn">

           <div class="row" >

              
             
                           <%--CssClass="chkChoiceHeader"--%>
               
                  <div class="col-md-2"  >
                     
                    <asp:RadioButtonList CssClass="chkChoiceHeader"  ID="actionRadioButtonList" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="actionRadioButtonList_OnSelectedIndexChanged">
                       <asp:ListItem Selected="True"  Value="Approved">Approved</asp:ListItem>
                     <%--   <asp:ListItem  Value="Review">Return</asp:ListItem>--%>
                    </asp:RadioButtonList>
                           </div>
               
                  <div class="col-md-2"  >
                        <label style="font-weight: bold">Committee  feedback (If Any)</label> &nbsp;
                      </div>
                  <div class="col-md-8"  >
                              <div class="form-group">
                           <asp:TextBox runat="server" ID="txtComm" class="form-control" TextMode="MultiLine" Rows="1"></asp:TextBox>
                         </div>
                       <div class="form-group">
                        <asp:LinkButton runat="server" ID="btnsave"  OnClick="submitButton_Click"   CssClass="btn btnMyDesignSearch   btn-sm"><span aria-hidden="true" class="fa fa-check-square"></span>  &nbsp; Submit Information </asp:LinkButton>

                                </div> 
                                </div> 
                             
                      </div>
                        

                       </div>
               
               
               
           <div class="row" runat="server" Visible="False"  >

                   <div class="col-md-4">
                   </div>
                   <div class="col-md-3" style="padding-left: 70px">

                       <div class="form-group">
                           <label>Meeting No </label> &nbsp;<label style="color: #a52a2a">*</label>
                           <asp:DropDownList ID="ddlMeeting"  runat="server" class="form-control form-control-sm"></asp:DropDownList>
                       </div>
                       
                    <%--   <div class="form-group">
                           <label>Meeting Date </label> &nbsp;<label style="color: #a52a2a">*</label>
                           <asp:TextBox runat="server"  ID="MeetingDate" class="form-control form-control-sm"></asp:TextBox>
                           <cc1:CalendarExtender ID="CalendarExtender" runat="server" PopupPosition="TopLeft"
                                                 Format="dd-MMM-yyyy" PopupButtonID="ImageButtosn1" CssClass="MyCalendar"
                                                 TargetControlID="MeetingDate" />
                       </div>--%>
                       
                       
                       
                       
                           <div class="form-group" style="margin-left: 50px;">
                               <asp:LinkButton runat="server" ID="SearchButton" OnClick="SearchButton_OnClick"   CssClass="btn btnMyDesignSearch   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search Information </asp:LinkButton>
                                      
                          
                       


                   </div>
               </div>
                   </div>

           <br/>


               <asp:HiddenField runat="server" ID="hfTopsheetGeneMasId" />
               <asp:HiddenField runat="server" ID="id_mastetID" />


           

                <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gv_JdBoard" OnRowCommand="gv_JdBoard_OnRowCommand" CssClass="AddToListCssTable"  DataKeyNames="ReimbursFromMasterId" OnPreRender="gv_DocumentUpload_PreRender">
                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            <asp:HiddenField ID="hfeimbursFromMasterId" runat="server" Value='<%#Eval("ReimbursFromMasterId") %>' />
                                            <asp:HiddenField ID="hfCompanyId" runat="server" Value='<%#Eval("CompanyId") %>' />
                                            <asp:HiddenField ID="HFEmpInfoId" runat="server" Value='<%#Eval("EmpInfoId") %>' />
                                            <asp:HiddenField ID="HFApplicationType" runat="server" Value='<%#Eval("Type") %>' />
                                            <asp:HiddenField ID="HFSalaryLocaion" runat="server" Value='<%#Eval("SalaryLoationId") %>' />
                                            <asp:HiddenField ID="HFReimbursFromMasterLogId" runat="server" Value='<%#Eval("ReimbursementSelfAppLogId") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    
                                    <asp:TemplateField HeaderText="Company">
                                        <ItemTemplate>
                                            <asp:Label ID="CompanyName" runat="server" Text='<%#Eval("ShortName3") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Employee ID">
                                        <ItemTemplate>
                                            <asp:Label ID="employfee" runat="server" Text='<%#Eval("EmpMasterCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                      <asp:TemplateField HeaderText="Employee Name">
                                        <ItemTemplate>
                                            <asp:Label ID="emeeployee" runat="server" Text='<%#Eval("EmpName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    

                                    <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="emploeeyee" runat="server" Text='<%#Eval("Designation") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    
                                    <asp:TemplateField HeaderText="Division">
                                        <ItemTemplate>
                                            <asp:Label ID="Division" runat="server"  Text='<%#Eval("DivisionName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Department">
                                        <ItemTemplate>
                                            <asp:Label ID="DepartmentName" runat="server"  Text='<%#Eval("DepartmentName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Application Type ">
                                        <ItemTemplate>
                                            <asp:Label ID="ApplicationType" runat="server"  Text='<%#Eval("Type") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                     <asp:BoundField DataField="Type" HeaderText="Application Type" />  
                                           <asp:BoundField DataField="PatientName" HeaderText="Patient Name" />  

                                                  <asp:BoundField DataField="Relationship" HeaderText="Relation" />  

                                                  <asp:BoundField DataField="Amount" HeaderText="Amount" />  
                                        <asp:BoundField DataField="SubmitDate" HeaderText="Aplication Date" />
                                    <asp:BoundField DataField="SelfDate" HeaderText="Illness Description" /> 
                                    

                                    <asp:TemplateField HeaderText="Comments">
                                           <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtcomments" CssClass="form-control" TextMode="MultiLine" Rows="2" ></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="30%"></ItemStyle>
                                    </asp:TemplateField>
 
                                    
                                      <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkSelectAll" runat="server" CssClass="form-control-sm" AutoPostBack="True" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                               <asp:CheckBox Checked="True" runat="server" ID="MeCheck"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    
                                    
                                      <asp:TemplateField HeaderText="View">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="btn_vieww" OnClick="btn_view_OnClick" CssClass="btn btn-sm btn-    success" ><i class="fa fa-eye" aria-hidden="true"></i>
                                          </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                </Columns>
                            </asp:GridView>
 
               
        
      </ContentTemplate>
          </asp:UpdatePanel>
              </div>
        
        </div>
                               </div>
        
        </div>
</asp:Content>


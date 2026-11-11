<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="YearlyIncrementProcessView.aspx.cs" Inherits="Increment_UI_YearlyIncrementProcessView" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
    
         <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">

    <div class="content" id="content">
        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
                
                  <%--   <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" 
                   />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                        <div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Print Increment Letter </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="HomeButton_OnClick" />
                        
                       <%-- <asp:Button ID="addNewButton" Text="Add New" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="addNewButton_OnClick" />--%>
                        <asp:Button ID="reloadButton" Text="Reload" CssClass="btn btn-sm btn-outline-success" runat="server" OnClick="reloadButton_OnClick" Visible="False" />
                    </div>

                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">

                            <%-- <div class="row" runat="server" Visible="False">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label></label>
                                        <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True" RepeatDirection="Horizontal" Enabled="False">
                                        </asp:CheckBoxList>


                                    </div>
                                </div>
                            </div>--%>


                           
                                <div class="form-row">
                                
                                  <div  class="col-md-3">
                                     </div>
                                  
                                 <div  class="col-md-2">
                                    <div class="form-group">
                                        <label>Company</label> 
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" class="form-control form-control-sm" />
                                    </div>
                                </div>
   
                                      <div  class="col-md-2">
                                                <div class="form-group">
                                                    <label>Division</label>
                                                    <asp:DropDownList runat="server" ID="ddlDivision" AutoPostBack="True"  class="form-control form-control-sm Selerr" OnSelectedIndexChanged="ddlDivision_OnSelectedIndexChanged"  />
                                                    
                                                                                     <script type="text/javascript">
                                                        function pageLoad() {
                                                            $('.Selerr').chosen({ disable_search_threshold: 5, search_contains: true });
                                                        }
</script>
                                                </div>
                                            </div>
                                 <div class="col-md-2" runat="server" id="wing">
                                                        <div class="form-group">
                                                            <label>Wing</label>
                                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlWing" class="form-control form-control-sm Selerr" OnSelectedIndexChanged="ddlWing_OnSelectedIndexChanged" />

                                                        </div>
                                                    </div>
                                
                                           <div class="col-md-2" runat="server" id="dept">
                                                        <div class="form-group">
                                                            <label>Department</label>
                                                            
                                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDepartment" class="form-control form-control-sm Selerr" OnSelectedIndexChanged="ddlDepartment_OnSelectedIndexChanged" />

                                                        </div>
                                                    </div>

   

                                    </div>
                              <div class="form-row">
                                
                                  <div  class="col-md-3">
                                     </div>

                                 <div  class="col-md-2">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                     
                                        <asp:DropDownList runat="server" ID="ddlFinYear" CssClass="form-control form-control-sm Selerr" />
                                    </div>
                                </div>
                                
                                  <div  class="col-md-2">
                                            <div class="form-group">
                                            <label>Effective From Date </label> 
                                           
                                            <asp:TextBox ID="EffectiveDateTextBox" AutoCompleteType="Disabled"   AutoPostBack="True" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="TopLeft"
                                                Format="dd-MMM-yyyy" PopupButtonID="EffectiveDateTextBox" CssClass="MyCalendar"
                                                TargetControlID="EffectiveDateTextBox" />
                                        </div>
                                        </div>
                                
                                    <div  class="col-md-2">
                                            <div class="form-group">
                                            <label>Effective To Date </label>  
                                           
                                            <asp:TextBox ID="EffectToDate" AutoCompleteType="Disabled"   AutoPostBack="True" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupPosition="TopLeft"
                                                Format="dd-MMM-yyyy" PopupButtonID="EffectToDate" CssClass="MyCalendar"
                                                TargetControlID="EffectToDate" />
                                        </div>
                                        </div>
                                  </div>
                            
                            
                             <div class="form-row">

                                    <div class="col-4">
                                     </div>
                                  <div  class="col-md-4">

                                        <div class="form-group">
                                            <label>Single Employee</label>
                                               <asp:DropDownList   runat="server"   ID="ddlEmpInfo" class="form-control form-control-sm Selerr" />

                                        </div>
                                    </div>
                                 </div>
                                    <div class="form-row">

                                    <div class="col-5">
                                     </div>
                                <div class="col-md-2" style="margin-top:5px;">
                                    <div class="form-group" >

                                      
                                           <asp:LinkButton runat="server" ID="LinkButton1" OnClick="SearchButton_OnClick"  ToolTip="Click To Search"    class="btn btn-info btn-sm"    ><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp;Search</asp:LinkButton>
                                        
                                             <asp:LinkButton runat="server" ID="appraisalResetButton" OnClick="appraisalResetButton_OnClick" CssClass="btn btn-warning   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset </asp:LinkButton>
                                    </div>
                                </div>
                               


                            </div>


                              <style>
        .pagination .page-item.active .page-link  {
            background-color: #007BFF !important;
            border-style: none !important;
            /* W3C */
        }

        
    </style>
  <%--  <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <script src="https://code.jquery.com/jquery-3.3.1.js"></script>
       <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.19/css/dataTables.bootstrap4.min.css" />
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.19/js/dataTables.bootstrap4.min.js"></script>
 <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/buttons/1.5.6/js/dataTables.buttons.min.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/buttons/1.5.6/js/buttons.flash.min.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/buttons/1.5.6/js/buttons.html5.min.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/buttons/1.5.6/js/buttons.print.min.js"></script>

      <script>

          $(document).ready(function () {
              // Setup - add a text input to each footer cell


              // DataTable
              var table = $('#cpFormBody_loadGridView').DataTable(
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
                           filename: 'Increment List',
                           title: 'SMC',
                           messageTop: 'Increment List',
                           exportOptions: {
                               columns: [0, 2, 3, 4, 5, 6, 7, 8, 9, 10]
                           }
                       }
                       ]
                   }
                  );

              var prm = Sys.WebForms.PageRequestManager.getInstance();
              if (prm != null) {
                  prm.add_endRequest(function (sender, e) {
                      if (sender._postBackSettings.panelsToUpdate != null) {
                          table = $('#cpFormBody_loadGridView').DataTable(
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
                                  filename: 'Increment List',
                                  title: 'SMC',
                                  messageTop: 'Increment List',
                                  exportOptions: {
                                      columns: [0, 2, 3, 4, 5, 6, 7, 8, 9, 10]
                                  }
                              }
                              ]


                          }
                          );
                      }
                  });
              };

              // Apply the search
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

      </script>--%>
    
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


                           
                            <div id="gridContainer1" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: hidden;">
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered text-center thead-dark" ShowFooter="False" DataKeyNames="IncrementId, EmployeeId"   PageIndex="0" 
                                    OnRowCommand="loadGridView_RowCommand"   >
                                    <Columns>
                                        
                                         <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkSelectAll" runat="server" CssClass="form-control-sm" AutoPostBack="True" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                
                                                <asp:HiddenField runat="server" ID="HFEmployeeId" Value='<%#Eval("EmployeeId") %>'/>
                                                <asp:HiddenField runat="server" ID="HFIncrementId"  Value='<%#Eval("IncrementId") %>'/>
                                                <asp:CheckBox ID="chkSelect" CssClass="form-control-sm" AutoPostBack="True" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                          <%-- <asp:TemplateField HeaderText="Print Letter">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ViewReportImageButton" runat="server" class="btn btn-white btn-sm  " CommandArgument='<%#Eval("IncrementId") %>'
                                                        CommandName="Preview" ImageUrl="~/Assets/file.png" />  
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:BoundField DataField="EmployeeCode" HeaderText="ID" />
                                        <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                        <asp:BoundField DataField="DepartmentName" HeaderText="Deptartment" />
                                        <asp:BoundField DataField="PreviousStep" HeaderText="Previous Step" />
                                        <asp:BoundField DataField="IncrementalStep" HeaderText="Incremental Step" />
                                        <asp:BoundField DataField="EffectiveDate" HeaderText="Effective Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                      <%--  <asp:BoundField DataField="ActionStatus2" HeaderText="Approval Status" />
                                        
                                        <asp:BoundField DataField="AwEmpName" HeaderText="Awaiting Employee" />--%>
                               <%--         <asp:TemplateField HeaderText="Rollback"  >
                                            <ItemTemplate>
                                                <asp:ImageButton ID="deleteImageButton" runat="server"
                                                  CommandArgument='<%#Eval("IncrementId") %>' CommandName="DeleteData"
                                                    ImageUrl="~/Assets/img/delete.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                        

                                        <%--<asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDownload" runat="server"   CommandName="cmd"> Suspend Release  &gt; &gt; &gt; &gt; </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                    </Columns>    
                                </asp:GridView>
                            </div> 
                            <br/>
                            <br/>
                            
                            
                              <div class="form-row">
                                <div class="col-3 ">
                                    <div class="form-group">
                                        
                                        <asp:Button ID="submitButton" Text="Process" OnClick="submitButton_OnClick"   CssClass="btn btn-sm btn-success" runat="server" />
                                        
                                        
                                        <asp:Button class=" btn btn-sm btn-success " Text="Print on PDF" runat="server" ForeColor="black" ID="btnPrint" BackColor="#54C3A7" OnClick="btnPrint_Click"  />  
                                        
                                        
                                          <asp:Button class=" btn btn-sm btn-secondary " Text="Print on DOC" runat="server"  ID="btnDoc"   OnClick="btnDoc_OnClick"  />  
                                        
                                    </div>
                                </div>
                            </div>
                                  
                                 <div runat="server" Visible="False">
                                    <asp:HiddenField runat="server" ID="MasterIdHiddenField" />
                            <asp:HiddenField runat="server" ID="IncrementIdHiddenField" />
                            <asp:HiddenField runat="server" ID="ComName" />
                            <asp:HiddenField runat="server" ID="ComId" />
                            <asp:HiddenField runat="server" ID="EmpIdHiddenfield" />

                            <div class="row">

                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="col-md-6">
                                              <asp:TextBox ID="lblLabelInfo" CssClass="form-control form-control-sm col-md-4" runat="server" Text=""></asp:TextBox>

                                        </div>
                                          
                                        <div class="col-md-6"> <asp:Label ID="lblDate" CssClass="form-control form-control-sm col-md-4 pull-right" runat="server" Text=""></asp:Label></div>
                                    </div>
                                    <br/>
                                    <fieldset class="for-panel">
                                        <legend>Employee Information</legend>
                                        <div class="row">

                                            <div class="col-md-6">


                                                <div class="form-row">
                                                    <div class="col-md-3" style="padding: 10px;">
                                                        <label>Employee ID: </label>
                                                    </div>

                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblEmployeeCode" CssClass="form-control form-control-sm" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>


                                                <div class="form-row">
                                                    <div class="col-md-3" style="padding: 10px;">

                                                        <label>Employee Name: </label>
                                                    </div>

                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblEmp" CssClass="form-control form-control-sm" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>


                                                <div class="form-row">
                                                    <div class="col-md-3" style="padding: 10px;">

                                                        <label>Designation: </label>
                                                    </div>

                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblDesignation" CssClass="form-control form-control-sm" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                
                                                   <div class="form-row">
                                                    <div class="col-md-3" style="padding: 10px;">

                                                        <label>Company: </label>
                                                    </div>

                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblCompany" CssClass="form-control form-control-sm" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="form-row">
                                                    <div class="col-md-3" style="padding: 10px;">
                                                        <label>Department: </label>
                                                    </div>

                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblDepartment" CssClass="form-control form-control-sm" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>



                                                <div class="form-row">
                                                    <div class="col-md-3" style="padding: 10px;">
                                                        <label>Place of Posting: </label>
                                                    </div>

                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblOffice" CssClass="form-control form-control-sm" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">

                                                    <label>Previous Step: </label>



                                                    <asp:Label ID="txtPreSalStep" CssClass="form-control form-control-sm" runat="server" Text=""></asp:Label>
                                                </div>


                                                <div class="form-group">

                                                    <label>Incremental Step: </label>



                                                    <asp:Label ID="txtIncrementalStep" CssClass="form-control form-control-sm" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-1">
                                            </div>

                                        </div>
                                    </fieldset>
                                </div>


                            </div>

                            <div class="row">

                                <div class="col-md-6">
                                    
                                      
                                    <div class="form-row">
                                        
                                        <div class="col-md-3" style="padding: 10px;">
                                            <label>Subject: </label>
                                        </div>

                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtSubject" CssClass="form-control form-control-sm" runat="server" Text="Annual Increment"></asp:TextBox>
                                        </div>
                                    </div>

                                      
                                    <div class="form-row">
                                        
                                        <div class="col-md-3" style="padding: 10px;">
                                            <label>Salutation: </label>
                                        </div>

                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtSalutation" CssClass="form-control form-control-sm" runat="server" Text=""></asp:TextBox>
                                        </div>
                                    </div>

                                      <div class="form-row">
                                        
                                        <div class="col-md-3" style="padding: 10px;">
                                            <label>Congratulation: </label>
                                        </div>

                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtCongra" CssClass="form-control form-control-sm" runat="server" Text="Congratulation !!!"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-row">
                                        <div class="col-md-3" style="padding: 10px;">
                                            <label>Body of the letter: </label>
                                        </div>

                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtBodyofletter" CssClass="form-control" TextMode="MultiLine" placeholder="Write first paragraph" runat="server" Text="" Font-Size="12px" Rows="4"></asp:TextBox>
                                        </div>
                                    </div>

                                    <br/>
                                    
                                     <div class="form-row" runat="server" Visible="False">
                                            <div class="col-4">
                                                <div class="form-group">
                                                    <label>Particulars</label>
                                                    <asp:TextBox runat="server" ID="txtPName" CssClass="form-control form-control-sm" />
                                                </div>
                                            </div>
                                         
                                         
                                            <div class="col-4">
                                                <div class="form-group">
                                                    <label>Salary Break-Up</label>
                                                    <asp:TextBox runat="server" ID="txtPAmount" CssClass="form-control form-control-sm" />
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                                    Enabled="True" TargetControlID="txtPAmount" FilterType="Custom"  ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                         
                                          <div class="col-2" style="margin-top: 18px;">
                                                <div class="form-group">
                                                      <asp:Button ID="EducationRequirementImageButton" CssClass="btn btn-outline-success btn-block disabled btn-sm"  runat="server"  Text="Add To List"     />
                                                    </div>
                                              </div>
                                         </div>
                                    <div class="form-row">
                                   <div class="col-md-12">
                                       <asp:HiddenField runat="server" ID="GradeCode"/>
                                       <asp:HiddenField runat="server" ID="HFSalaryGradeId"/>
                                       <asp:HiddenField runat="server" ID="HFIncrementalStepId"/>
                                          <asp:HiddenField runat="server" ID="empCatId" />
                                           <div style="max-height: 200px; overflow: scroll">
                                                    <asp:GridView ID="KeyResponGridView" runat="server" Width="100%" CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="False">
                                                            <Columns>
                                                                                                                                 <asp:TemplateField HeaderText="Particulars">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Particulars" runat="server" Text='<%#Eval("ParticularsName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                                                                                                     

                                                    </asp:TemplateField>
                                                                
                                                                   <asp:TemplateField HeaderText=" From October 1, 2021 Salary breakdown">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_SalaryBreakUpPre"     CssClass="form-control form-control-sm"  runat="server" Text='<%#Eval("SalaryBreakUpPre") %>'></asp:Label>
                                                            
                                                        
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Salary Breakdown">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_SalaryBreakUp" runat="server" Text='<%#Eval("SalaryBreakUp") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                           
                                                            </Columns>
                                                        </asp:GridView>
                                            </div>

                                         
                                   </div>
                                    </div>

                                </div>
                                  <div class="col-md-6">
                                      <div class="form-row">
                                        
                                        <div class="col-md-3" style="padding: 10px;">
                                        
                                            <label>Complimentary Close: </label>
                                          </div>

                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtComplimentaryClose"  CssClass="form-control" TextMode="MultiLine" placeholder="Write first paragraph" runat="server" Text="" Font-Size="12px" Rows="4"></asp:TextBox>
                                        </div>
                                    </div>
                                      <br/>
                                          <div class="form-row">
                                        
                                        <div class="col-md-3" style="padding: 10px;">
                                              
                                            </div>
                                           
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtSincerely" CssClass="form-control form-control-sm" runat="server" Text="Yours Sincerely,"></asp:TextBox>
                                            </div>
                                           </div>
                                      <br/>
                                      
                                   
                                      <div class="form-row">
                                        
                                        <div class="col-md-3" style="padding: 10px;">
                                             <label>Signature Person: </label>
                                            </div>

                                        <div class="col-md-9">
                                            
                                                 <asp:TextBox ID="EmployeeNameTextBox" AutoPostBack="True" runat="server" class="form-control form-control-sm"  ></asp:TextBox>
                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                                        EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                                        ServiceMethod="GetCompanyWiseEmployeeInfoActiveInactiveAll" ServicePath="~/WebService.asmx" TargetControlID="EmployeeNameTextBox"
                                                                        UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                                        ShowOnlyCurrentWordInCompletionListItem="true">
                                                                    </cc1:AutoCompleteExtender>

                                                                    <asp:HiddenField ID="repEmpIdHiddenField" runat="server" />
                                            <asp:TextBox ID="txtName" Font-Size="12px" Rows="3" Visible="False" TextMode="MultiLine" CssClass="form-control" runat="server" Text=""></asp:TextBox>
                                           </div>
                                         </div>
                                      
                                   <br/>
                                      <div class="form-row">
                                        
                                        <div class="col-md-3" style="padding: 10px;">
                                        
                                            <label>Copy To: </label>
                                             </div>

                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtCopyTO" CssClass="form-control" TextMode="MultiLine" placeholder="Write first paragraph" runat="server" Text="Chief/HoD
 CFO
Personal File" Font-Size="12px" Rows="4"></asp:TextBox>
                                        </div>
                                    </div>
                                      </div>

                            </div>
                          </div> 
                        </div>              
                              
                         
                         
                              
                              
                              
                                                                                                                                        
                                                 
                            
                            
                            
                            
                            
                            
                            
                            
                            
                            
                            
                            
                            
                            
                            
                            
                            
                            
                            
                            
                            
                            
                            
                            
                            
                            
                            
                              
                          <br/>
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
           <%-- </ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>

</asp:Content>


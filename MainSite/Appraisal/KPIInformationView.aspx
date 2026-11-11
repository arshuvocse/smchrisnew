<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="KPIInformationView.aspx.cs" Inherits="Appraisal_KPIInformationView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

  
       <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
   <style>
    .draft-btn {
        padding: 6px 12px;
        border-radius: 4px;
        font-weight: 500;
        text-transform: uppercase;
        font-size: 0.75rem;
        letter-spacing: 0.5px;
        transition: all 0.3s ease;
        border: 1px solid transparent;
        margin: 2px;
        box-shadow: 0 1px 3px rgba(0, 0, 0, 0.15); /* Default shadow */
    }

    .kpi-btn {
        background-color: #778899;
        color: black;
    }

    .kpi-btn:hover {
        background-color: #2980b9;
        color: black;
        box-shadow: 0 4px 8px rgba(0, 0, 0, 0.25); /* Stronger shadow on hover */
        transform: translateY(-1px);
    }

    .appraisal-btn {
        background-color: #FFF0F5;
        color: black;
    }

    .appraisal-btn:hover {
        background-color: #ee82eef5;
        box-shadow: 0 4px 8px rgba(0, 0, 0, 0.25); /* Stronger shadow on hover */
        transform: translateY(-1px);
        color: black;
    }

    .draft-btn:active {
        transform: translateY(0);
        box-shadow: 0 1px 2px rgba(0, 0, 0, 0.1); /* Softer shadow when pressed */
    }
</style>

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
        <style>
        .button12 {
  background-color: #04AA6D; /* Green */
  border: none;
  color: white;
  padding: 15px 32px;
  text-align: center;
  text-decoration: none;
  display: inline-block;
  font-size: 16px;
  margin: 4px 2px;
  cursor: pointer;
  -webkit-transition-duration: 0.4s; /* Safari */
  transition-duration: 0.4s;
}

.button12 {
  box-shadow: 0 8px 16px 0 rgba(0,0,0,0.2), 0 6px 20px 0 rgba(0,0,0,0.19);
}

.button12:hover {
      color: lightgrey;
  box-shadow: 0 12px 16px 0 rgba(0,0,0,0.24),0 17px 50px 0 rgba(0,0,0,0.19);
}
    </style>
    <div class="content">
        <asp:UpdatePanel runat="server" ID="upFormBody">
            <ContentTemplate>
                
                
                     <asp:UpdateProgress ID="UpdateProgress2" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWaeeeewit" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>  
                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png" width="20px"  />KPI Information</h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block" runat="server" >
                            
                           <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                           
                        </div>

                    </div>

                    <div class="card">
                        <div class="card-body">
                            <div class="row" runat="server" visible="False">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label></label>
                                        <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True" RepeatDirection="Horizontal" Enabled="False">
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                             <div class="form-row">
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <label>Company</label>
                                        <asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" class="form-control form-control-sm" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                </div>
                                 
                                   <div class="col-md-1">
                                    <div class="form-group">
                                        <label>Fiscal Year</label>
                                        <asp:DropDownList ID="ddlFinancialYear" AutoPostBack="true" OnSelectedIndexChanged="ddlFinancialYear_OnSelectedIndexChanged" runat="server" class="form-control form-control-sm selectme"></asp:DropDownList>
                                        
                                        
                                           <script type="text/javascript">
                                               function pageLoad() {
                                                   $('.selectme').chosen({ disable_search_threshold: 5, search_contains: true });

                                               }
                                                        </script>
                                    </div>
                                </div>
                                 
                                 
                                   <div class="col-2">
                                    <div class="form-group">
                                        <label>Division</label>
                                        <asp:DropDownList runat="server" ID="ddlDivision" AutoPostBack="True" OnSelectedIndexChanged="ddlDivision_OnSelectedIndexChanged" CssClass="form-control form-control-sm selectme" />
                                        
                                           
                                    </div>
                                </div>
                                 
                                   <div class="col-2">
                                    <div class="form-group">
                                        <label>Department</label>
                                        <asp:DropDownList runat="server" ID="ddlDept" CssClass="form-control form-control-sm selectme" />
                                        
                                           
                                    </div>
                                </div>

                                                             <div class="col-md-1">
<div class="form-group">
    <label for="ddl_options">Operation</label>      
    <asp:DropDownList ID="ddl_options" runat="server" CssClass="form-control form-control-sm"  >

        
        <asp:ListItem Text="Select One.." Value="0" />
        <asp:ListItem Text="KPI" Value="KPI" />
        <asp:ListItem Text="OKR" Value="OKR" />
        <asp:ListItem Text="BSC" Value="BSC" />
    </asp:DropDownList>
    </div>
    </div>
                                                                            <div class="col-md-1" style="display:none" >
    <div class="form-group">
        <label for="ddl_opdddtions">Agree or Disagree</label>      
        <asp:DropDownList ID="ddlDisagree" runat="server" CssClass="form-control form-control-sm">
            <asp:ListItem Text="All" Value="0" />
            <asp:ListItem Text="Agree" Value="Agree" />
            <asp:ListItem Text="Disagree" Value="Disagree" /> 
        </asp:DropDownList>
    </div>
</div>

                                     <div class="col-md-1">
                                    <div class="form-group" style="margin-top: 13px;">
  <asp:LinkButton runat="server" ID="SearchButton" OnClick="SearchButton_OnClick"   CssClass="btn btnMyDesignSearch   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search </asp:LinkButton>
                                      
                                    </div>
                                </div>
                                 
                                   <div class="col-md-2">
                                    <div class="form-group" style="margin-top: 18px;">
                                     <asp:LinkButton ID="btnExportToExcel" runat="server"  CssClass="btnexcel  pull-right" OnClick="btnExportToExcel_Click" ><i class="fa fa-file-excel-o"></i> Export To Excel</asp:LinkButton>
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

                                              .btn-KPI {
                                                      color: #fff !important;
    background-color: #3f51b5 !important;font-weight: bold !important;
                                                font-size: 15px!important;
}
                                           
                                              
                                                  .btn-Appraisal {
                                                     color: #000 !important;
background-color: #00ffff !important;font-weight: bold !important;
                                                font-size: 15px!important;
}   
                                            .btn-CancelApp {
                                                     color: white !important;
background-color: #E24F3B !important;
                                                font-weight: bold !important;
                                                font-size: 15px!important;
}   
       </style>
                            
                            <asp:CheckBox runat="server" ID="isKPI" Text="KPI" Visible="False" />
                                            <asp:HiddenField ID="hfMainAppraisalMasterId" runat="server"  />
                                            <asp:HiddenField ID="hfMainId" runat="server"  />
                                            <asp:HiddenField ID="hfMasterId" runat="server"  />
                            
                             <div id="gridContainer1" style=" overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;">

                              <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gv_JdBoard" OnRowCommand="gv_JdBoard_OnRowCommand" CssClass="AddToListCssTable"      OnPreRender="gv_DocumentUpload_PreRender" OnRowDataBound="gv_JdBoard_RowDataBound">
                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            <asp:HiddenField ID="hfAppraisalMasterId" runat="server" Value='<%#Eval("AppraisalMasterId") %>' />
                                            <asp:HiddenField ID="hfAppraisalSelfMasterId" runat="server" Value='<%#Eval("AppraisalSelfMasterId") %>' />
                                            <asp:HiddenField ID="EmpInfoId" runat="server" Value='<%#Eval("EmpInfoId") %>' />
                                            <asp:HiddenField ID="FinancialYearId" runat="server" Value='<%#Eval("FinancialYearId") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                       <asp:TemplateField HeaderText="Operation">
       <ItemTemplate>
           <asp:Label ID="OptionInfo" runat="server"   Text='<%#Eval("OptionInfo") %>'></asp:Label>
       </ItemTemplate>
   </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Financial Year">
                                        <ItemTemplate>
                                            <asp:Label ID="FinancialYearDesc" runat="server"   Text='<%#Eval("FinancialYearDesc") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Employee ID">
                                        <ItemTemplate>
                                            <asp:Label ID="employfee" runat="server" Text='<%#Eval("EmpMasterCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                      <asp:TemplateField HeaderText="Employee Name">
                                        <ItemTemplate>
                                            <asp:Label ID="emeeployee" runat="server" Text='<%#Eval("EmpllName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="emploeeyee" runat="server" Text='<%#Eval("Designation") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                   <%--   <asp:TemplateField HeaderText="Employee Info">
                                        <ItemTemplate>
                                            <asp:Label ID="employee" runat="server" Text='<%#Eval("employee") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    
                                    
                                    
                                    
                                    
                                    <asp:TemplateField HeaderText="Division">
                                        <ItemTemplate>
                                            <asp:Label ID="Division" runat="server"   Text='<%#Eval("DivisionName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Department">
                                        <ItemTemplate>
                                            <asp:Label ID="DepartmentName" runat="server"   Text='<%#Eval("DepartmentName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--  <asp:TemplateField HeaderText="Approval">
                                        <ItemTemplate>
                                            <asp:Label ID="Approval" runat="server" class="form-control form-control-sm" Text='<%#Eval("ActionStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                    <%--  <asp:TemplateField HeaderText="Company">
                                        <ItemTemplate>
                                            <asp:Label ID="CompanyName" runat="server" class="form-control form-control-sm" Text='<%#Eval("CompanyName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>


                                    <asp:TemplateField HeaderText="Set KPI" Visible="False">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="btn_edit" OnClick="btn_edit_OnClick" Text="Set KPI"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--   <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                      <asp:LinkButton runat="server" ID="btn_Remove" Text="Remove"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="KPI/OKR/BSC Approval Status">
                                            <HeaderTemplate>
        <asp:Label   ID="HTActionStatus" runat="server" />
    </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="ActionStatus" runat="server"   Text='<%#Eval("ActionStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                         <asp:TemplateField HeaderText=" KPI/OKR/BSC Awaiting Employee">
                                                                                     <HeaderTemplate>
    <asp:Label   ID="HTAwaitingActionStatus" runat="server" />
</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Awaiting" runat="server"   Text='<%#Eval("PendingEmp") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mid-Year Approval Status">
                                           
                                        <ItemTemplate>
                                            <asp:Label ID="MidActionStatus" runat="server"   Text='<%#Eval("MidActionStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                         <asp:TemplateField HeaderText=" Mid-Year Awaiting Employee">
                                                                                     
                                        <ItemTemplate>
                                            <asp:Label ID="MidAwaiting" runat="server"   Text='<%#Eval("MidPendingEmp") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Appraisal Approval Status">
                                        <ItemTemplate>
                                            <asp:Label ID="Awaitingf" runat="server" Text='<%#Eval("ActionStatusAppraisal") %>'></asp:Label>
                                            <br />
                                             <asp:Label ID="StatusLabel" runat="server"   style="font-weight:bold;margin-top:5px"
            Text='<%# 
                Eval("ActionStatusAppraisal").ToString() == "Approved" && 
                (Convert.ToBoolean(Eval("IsAgreed")) || Convert.ToBoolean(Eval("IsDisagreed"))) 
                ? (Convert.ToBoolean(Eval("IsAgreed")) ? "Agreed" : (Convert.ToBoolean(Eval("IsDisagreed")) ? "Disagreed" : "")) 
                : "" %>'>
        </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Appraisal Awaiting Employee">
                                        <ItemTemplate>
                                            <asp:Label ID="Awaitingh" runat="server"   Text='<%#Eval("PendingEmpApp") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    
                                    
                                    <asp:TemplateField HeaderText="Forwarding Person (This option will be disabled after approval)">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="btn_KPI"  OnClick="btn_KPI_OnClick" CssClass="btn btn-xs btn-KPI" Text='<%# String.Format("{0} Forwarding", Eval("OptionInfo")) %>'  > 
                                            </asp:LinkButton>
                                            <div style="padding-top: 3px!important"></div>
                                            &nbsp;
                                            &nbsp;
                                             <asp:LinkButton runat="server" ID="btnApprisal" OnClick="btnApprisal_OnClick" CssClass="btn btn-xs btn-Appraisal" >Appraisal Forwarding
                                            </asp:LinkButton>                                                                                  
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Remove Appraisal (Available after appraisal approval.)">
                                           <ItemTemplate>
                                          <asp:LinkButton runat="server" ID="lbApprisalCancel" OnClick="btnApprisalCancel_OnClick" CssClass="btn btn-xs btn-CancelApp" ToolTip="Click to Cancel Appraisal Approval" >Cancel
                                            </asp:LinkButton>
                                      </ItemTemplate>
                                    
                                    </asp:TemplateField>

                                    
                                    
                                    <asp:TemplateField HeaderText="Back to Draft Status">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="btn_ChangeMarks"   Visible='<%# Eval("ActionStatusAppraisal") != null && Eval("ActionStatusAppraisal").ToString() == "Approved" %>'  OnClientClick="return confirmChangeMarks('changeMark');" OnClick="btn_ChangeMarks_Click" CssClass="btn btn-sm button12"  >Change Mark 
                                            </asp:LinkButton>
                                                                          
                                                                          
                                            
                                    
      <asp:LinkButton runat="server" ID="btnDraftKPI" 
            OnClientClick="return confirmChangeMarks('kpi');" 
            OnClick="btnDraftKPI_Click" 
            CssClass="btn btn-sm draft-btn kpi-btn"  
            Text="Make the KPI Draft">
        </asp:LinkButton> 
        
        <asp:LinkButton runat="server" ID="btnDraftAppraisal" 
            OnClientClick="return confirmChangeMarks('appraisal');" 
            OnClick="btnDraftAppraisal_Click" 
            CssClass="btn btn-sm draft-btn appraisal-btn"  
            Text="Make the Appraisal Draft">
        </asp:LinkButton>
    </ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="Mid Year Print">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="btn_midYearPrint" OnClick="btn_midYearPrint_OnClick" CssClass="btn btn-sm btn-info"><i class="fa fa-print" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

<asp:TemplateField HeaderText="Appraisal Print">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="btn_print" OnClick="btn_print_OnClick" CssClass="btn btn-sm btn-info" ><i class="fa fa-print" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="View">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="btn_view" OnClick="btn_eview_OnClick" CssClass="btn btn-sm btn-success" ><i class="fa fa-eye" aria-hidden="true"></i>
 </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="PDF Generate">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkSelectAll" runat="server" CssClass="pdfSelectAllCheckBox" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelectRow" runat="server" CssClass="pdfRowCheckBox" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            </div>
                            <div class="form-group" style="margin-top: 10px;">
                                <button type="button" id="btnGenerateSelectedPdf" class="btn btn-sm btn-KPI"><i class="fa fa-file-pdf-o" aria-hidden="true"></i>&nbsp; Generate PDF (Selected)</button>
                            </div>

                            <div id="pdfProgressOverlay" style="display:none; position:fixed; top:0; left:0; width:100%; height:100%; background:rgba(0,0,0,0.55); z-index:99999; text-align:center;">
                                <div style="background:#fff; max-width:380px; margin:15% auto 0 auto; padding:25px 20px; border-radius:6px; box-shadow:0 2px 10px rgba(0,0,0,0.3);">
                                    <div class="fa fa-spinner fa-spin" style="font-size:32px; color:#337ab7;"></div>
                                    <h4 style="margin:15px 0 5px 0;">Generating PDF...</h4>
                                    <div id="pdfProgressCurrentName" style="font-size:12px; color:#777; min-height:16px; margin-bottom:10px;"></div>
                                    <div style="background:#eee; border-radius:10px; height:16px; overflow:hidden;">
                                        <div id="pdfProgressBar" style="background:#5cb85c; height:100%; width:0%; transition:width 0.3s;"></div>
                                    </div>
                                    <div id="pdfProgressText" style="margin-top:10px; font-weight:bold;">0 of 0 done</div>
                                </div>
                            </div>

                            <script type="text/javascript">
                                $(document).on('change', '.pdfSelectAllCheckBox input[type="checkbox"], input[id*="chkSelectAll"]', function () {
                                    var isChecked = $(this).is(':checked');
                                    $('.pdfRowCheckBox input[type="checkbox"], input[id*="chkSelectRow"]').prop('checked', isChecked);
                                });

                                $(document).on('change', '.pdfRowCheckBox input[type="checkbox"], input[id*="chkSelectRow"]', function () {
                                    var total = $('.pdfRowCheckBox input[type="checkbox"], input[id*="chkSelectRow"]').length;
                                    var checked = $('.pdfRowCheckBox input[type="checkbox"], input[id*="chkSelectRow"]:checked').length;
                                    $('.pdfSelectAllCheckBox input[type="checkbox"], input[id*="chkSelectAll"]').prop('checked', total > 0 && total === checked);
                                });

                                function showPdfProgress(done, total, currentName) {
                                    var pct = total > 0 ? Math.round((done / total) * 100) : 0;
                                    $('#pdfProgressBar').css('width', pct + '%');
                                    $('#pdfProgressText').text(done + ' of ' + total + ' done');
                                    $('#pdfProgressCurrentName').text(currentName ? ('Processing: ' + currentName) : '');
                                }

                                function pollPdfJobStatus(jobId) {
                                    var poller = setInterval(function () {
                                        $.ajax({
                                            url: 'SelectedPdfHandler.ashx?action=status&jobId=' + jobId,
                                            type: 'GET',
                                            dataType: 'json'
                                        }).done(function (status) {
                                            showPdfProgress(status.done, status.total, status.currentName);

                                            if (status.completed) {
                                                clearInterval(poller);

                                                if (status.error) {
                                                    $('#pdfProgressOverlay').hide();
                                                    alert('PDF generation failed: ' + status.error);
                                                    return;
                                                }

                                                if (status.failed > 0 && status.itemErrors && status.itemErrors.length > 0) {
                                                    var msg = status.failed + ' of ' + status.total + ' record(s) could not be added to the PDF:\n\n' + status.itemErrors.join('\n');
                                                    if (status.done - status.failed <= 0) {
                                                        $('#pdfProgressOverlay').hide();
                                                        alert(msg);
                                                        return;
                                                    }
                                                    alert(msg);
                                                }

                                                $('#pdfProgressText').text('Download starting...');
                                                window.location = 'SelectedPdfHandler.ashx?action=download&jobId=' + jobId;
                                                setTimeout(function () {
                                                    $('#pdfProgressOverlay').hide();
                                                }, 1200);
                                            }
                                        }).fail(function () {
                                            clearInterval(poller);
                                            $('#pdfProgressOverlay').hide();
                                            alert('Failed to check PDF generation status.');
                                        });
                                    }, 1000);
                                }

                                $(document).on('click', '#btnGenerateSelectedPdf', function () {
                                    var items = [];
                                    $('.pdfRowCheckBox input[type="checkbox"]:checked, input[id*="chkSelectRow"]:checked').each(function () {
                                        var $row = $(this).closest('tr');
                                        items.push({
                                            EmpInfoId: $row.data('empinfoid'),
                                            FinancialYearId: $row.data('financialyearid'),
                                            OptionInfo: $row.data('optioninfo'),
                                            FinancialYearDesc: $row.data('financialyeardesc'),
                                            EmpCode: $row.data('empcode'),
                                            EmpName: $row.data('empname')
                                        });
                                    });

                                    if (items.length === 0) {
                                        alert('Please select at least one row to generate PDF.');
                                        return;
                                    }

                                    var invalidItems = items.filter(function (it) { return !it.EmpInfoId || !it.FinancialYearId; });
                                    if (invalidItems.length > 0) {
                                        console.error('Selected rows missing EmpInfoId/FinancialYearId values:', invalidItems);
                                        alert('Some selected rows are missing required data and were excluded. Check browser console for details.');
                                        items = items.filter(function (it) { return it.EmpInfoId && it.FinancialYearId; });
                                    }

                                    if (items.length === 0) {
                                        return;
                                    }

                                    if (!confirm('Are you sure you want to generate PDF for ' + items.length + ' selected record(s)?')) {
                                        return;
                                    }

                                    showPdfProgress(0, items.length, null);
                                    $('#pdfProgressOverlay').show();

                                    $.ajax({
                                        url: 'SelectedPdfHandler.ashx?action=start',
                                        type: 'POST',
                                        contentType: 'application/json',
                                        data: JSON.stringify(items),
                                        dataType: 'json'
                                    }).done(function (result) {
                                        if (!result.jobId) {
                                            $('#pdfProgressOverlay').hide();
                                            alert(result.error || 'Failed to start PDF generation.');
                                            return;
                                        }
                                        pollPdfJobStatus(result.jobId);
                                    }).fail(function () {
                                        $('#pdfProgressOverlay').hide();
                                        alert('Failed to start PDF generation.');
                                    });
                                });
                            </script>
                            
                            
                                           <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gv_Excel"  CssClass="AddToListCssTable" >
                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    
                                            <asp:BoundField DataField="FinancialYearDesc" HeaderText="Financial Year" />
                                                 

                                      <asp:BoundField DataField="OptionInfo" HeaderText="Operation" />
                                            <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee ID" />
                                            <asp:BoundField DataField="EmpllName" HeaderText="Employee Name" />
                                            <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                            <asp:BoundField DataField="DivisionName" HeaderText="Division" />
                                            <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                    
                                            <asp:BoundField DataField="PendingEmp" HeaderText="KPI Awaiting Employee" />
                                            <asp:BoundField DataField="ActionStatus" HeaderText="KPI Approval Status" />

                                                                        <asp:TemplateField HeaderText="Mid-Year Approval Status">
       
    <ItemTemplate>
        <asp:Label ID="MidActionStatusexs" runat="server"   Text='<%#Eval("MidActionStatus") %>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>
     <asp:TemplateField HeaderText=" Mid-Year Awaiting Employee">
                                                 
    <ItemTemplate>
        <asp:Label ID="MidAwaitingexs" runat="server"   Text='<%#Eval("MidPendingEmp") %>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>

                                            <asp:BoundField DataField="PendingEmpApp" HeaderText="Appraisal Awaiting Employee" />
                                            <asp:BoundField DataField="ActionStatusAppraisal" HeaderText="Appraisal Approval Status" />
                                     <asp:TemplateField HeaderText="Agreed">
    <ItemTemplate>
        <asp:Label ID="lblIsAgreed" runat="server" 
            Text='<%# (Convert.ToBoolean(Eval("IsAgreed")) || Convert.ToBoolean(Eval("IsDisagreed"))) ? (Convert.ToBoolean(Eval("IsAgreed")) ? "Yes" : "") : "" %>'>
        </asp:Label>
    </ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="Disagreed">
    <ItemTemplate>
        <asp:Label ID="lblIsDisagreed" runat="server" 
            Text='<%# (Convert.ToBoolean(Eval("IsAgreed")) || Convert.ToBoolean(Eval("IsDisagreed"))) ? (Convert.ToBoolean(Eval("IsDisagreed")) ? "Yes" : "") : "" %>'>
        </asp:Label>
    </ItemTemplate>
</asp:TemplateField>
                                     

                                    
                                    
                                            <asp:BoundField DataField="FunctionalMarkMain" HeaderText="Functional: Total Mark" />
                                            <asp:BoundField DataField="FunctionalMarks" HeaderText="Functional: Total Out of 75" />
                                            <asp:BoundField DataField="BehaviouralMarks" HeaderText="Behavioural Mark" />
                                            <asp:BoundField DataField="TotalMainMarks" HeaderText="Appraisal Score (Functional Total + Behavioural Mark)" />
                                            <asp:BoundField DataField="TotalMarks" HeaderText="Appraisal Score (Functional Total Out of 75 + Behavioural Mark)" />
                                           
                                            <asp:BoundField DataField="PromotionwithIncrement" HeaderText="Recommendation" />
                                            <asp:BoundField DataField="DateOfJoin" HeaderText="Date of Joining" />
                                            <asp:BoundField DataField="GradeCode" HeaderText="Grade" />



                                    
                                      
                                     
                                </Columns>
                            </asp:GridView>
                            
                                           <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gv_ExcelKPI"  CssClass="AddToListCssTable" >
                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    
                                            <asp:BoundField DataField="FinancialYearDesc" HeaderText="Financial Year" />
                                                 

                                      <asp:BoundField DataField="OptionInfo" HeaderText="Operation" />
                                            <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee ID" />
                                            <asp:BoundField DataField="EmpllName" HeaderText="Employee Name" />
                                            <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                            <asp:BoundField DataField="DivisionName" HeaderText="Division" />
                                            <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                    
                                            <asp:BoundField DataField="PendingEmp" HeaderText="KPI Awaiting Employee" />
                                            <asp:BoundField DataField="ActionStatus" HeaderText="KPI Approval Status" />

                                                                                                            <asp:TemplateField HeaderText="Mid-Year Approval Status">
       
    <ItemTemplate>
        <asp:Label ID="MidActionStatusex" runat="server"   Text='<%#Eval("MidActionStatus") %>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>
     <asp:TemplateField HeaderText=" Mid-Year Awaiting Employee">
                                                 
    <ItemTemplate>
        <asp:Label ID="MidAwaitingex" runat="server"   Text='<%#Eval("MidPendingEmp") %>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>

                                            <asp:BoundField DataField="PendingEmpApp" HeaderText="Appraisal Awaiting Employee" />
                                            <asp:BoundField DataField="ActionStatusAppraisal" HeaderText="Appraisal Approval Status" />

                                                                   

                                     <asp:TemplateField HeaderText="Agreed">
    <ItemTemplate>
        <asp:Label ID="lblIsAgreedKPI" runat="server" 
            Text='<%# (Convert.ToBoolean(Eval("IsAgreed")) || Convert.ToBoolean(Eval("IsDisagreed"))) ? (Convert.ToBoolean(Eval("IsAgreed")) ? "Yes" : "") : "" %>'>
        </asp:Label>
    </ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="Disagreed">
    <ItemTemplate>
        <asp:Label ID="lblIsDisagreedKPI" runat="server" 
            Text='<%# (Convert.ToBoolean(Eval("IsAgreed")) || Convert.ToBoolean(Eval("IsDisagreed"))) ? (Convert.ToBoolean(Eval("IsDisagreed")) ? "Yes" : "") : "" %>'>
        </asp:Label>
    </ItemTemplate>
</asp:TemplateField>


                                    
                                    <asp:BoundField DataField="FunctionalMarks" HeaderText="Functional Mark" />
                                    
                                            <asp:BoundField DataField="BehaviouralMarks" HeaderText="Behavioural Mark" />
                                        
                                            <asp:BoundField DataField="TotalMarks" HeaderText="Appraisal Score (Functional Mark + Behavioural Mark)" />
                                           
                                            <asp:BoundField DataField="PromotionwithIncrement" HeaderText="Recommendation" />
                                            <asp:BoundField DataField="DateOfJoin" HeaderText="Date of Joining" />
                                            <asp:BoundField DataField="GradeCode" HeaderText="Grade" />



                                    
                                      
                                     
                                </Columns>
                            </asp:GridView>
                            <script type="text/javascript">
                                function confirmChangeMarks(buttonType) {
                                    var message = "";

                                    if (buttonType === "kpi") {
                                        message = "Are you sure you want to save as DRAFT for KPI? This action cannot be undone. Appraisal & Mid Year data will also be deleted.";
                                    }
                                    else if (buttonType === "appraisal") {
                                        message = "Are you sure you want to save as DRAFT for Appraisal? This action cannot be undone.";
                                    }
                                    else if (buttonType === "changeMark") {
                                        message = "Are you sure you want to Change?";
                                    }
                                    else {
                                        message = "Are you sure you want to save as DRAFT? This action cannot be undone.";
                                    }

                                    return confirm(message);
                                }
                            </script>

                            
                              <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>  <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>  <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                        </div>
                          <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>  <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>  <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    </div>
                </div>
            </ContentTemplate>
                 <Triggers>
                       <asp:PostBackTrigger ControlID="btnExportToExcel" />

    </Triggers>
        </asp:UpdatePanel>
        
        
        <div>
        <ajaxToolkit:ModalPopupExtender ID="mpFunctionalSup" runat="server" TargetControlID="FunctionalSup_Test" PopupControlID="pnlFunctionalSup"
            BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>

        <asp:HiddenField ID="FunctionalSup_Test" runat="server"></asp:HiddenField>
        <asp:Panel ID="pnlFunctionalSup" runat="server" Style="display: none; overflow: scroll; padding: 10px" Height="400px" Width="60%" CssClass="modalPopup">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                        <asp:UpdateProgress ID="UpdateProgress5" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWawwwwwwwit" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                                                                                                 <div class="row">
                                                    <div class="col-md-6" style="padding-left: 15px;padding-top: 12px;">
                                                          <div class="text-left">
                                                   <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" /> <span runat="server" id="lblHeader"></span></h1>
                                                </div>
                                                    </div>
                                                    
                                                    <div class="col-md-6" style="padding-top: 15px;padding-right: 15px;">
                                                          <asp:LinkButton ID="btnFunctionalSupClose"   OnClick="btnFunctionalSupClose_Click" CssClass="btn btn-xs btn-danger pull-right  pull-right" runat="server"><i style="font-size: 16px" class="fa fa-times" aria-hidden="true"></i>
</asp:LinkButton>
                                                    </div>
                                                </div>
                                             
                                             <hr/>
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

.title-widget::before {
    background-color: #ea5644;
    content: "";
    height: 22px;
    left: 0px;
    position: absolute;
    top: -2px;
    width: 5px;
}
 div#cpFormBody_ddlEmpInfo_chosen {
              width: 100%!important
          }

  div#cpFormBody_ddlForwordEmp_chosen {
              width: 100%!important
          }
                                </style>
                                     

                                    <div class="col-md-12">
                                          
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group ">
                                                    <label>Present Approval Person </label>
                                                    
                                                                <asp:DropDownList   runat="server" style="width: 320px !important" Enabled="False"   ID="ddlEmpInfo" CssClass="form-control form-control-sm selectme" />
                                                   <%-- <script type="text/javascript">
                                                        function pageLoad() {
                                                            $('#<%=ddlEmpInfo.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });
                                                            $('#<%=ddlForwordEmp.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });
                                                        }
                                                        </script>--%>
                                            </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group ">
                                                    <label>Forward Approval Person </label>
                                                    
                                                                <asp:DropDownList   runat="server"   ID="ddlForwordEmp" class="form-control form-control-sm selectme" />
                                                   
                                                    <asp:HiddenField ID="HFOperationInfo" runat="server" ></asp:HiddenField>
                                                    
                                                      <br/>
                                                      <br/>
                                                      <br/>
                                                    
                                                     <asp:Button runat="server" ID="btnAppraisalFuncSUPSave" OnClick="btnAppraisalFuncSUPSave_OnClick" Text="Submit "  style="font-size: 20px!important;" CssClass="btn btn-info" />  
                                            </div>
                                                
                                                
                                                <br/>
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
                    
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
                 </div>
        
        
        
        
        
          <div>
        <ajaxToolkit:ModalPopupExtender ID="MPAppraisalApproval" runat="server" TargetControlID="hfAppraisalApproval" PopupControlID="pnAppraisalApproval"
            BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>

        <asp:HiddenField ID="hfAppraisalApproval" runat="server"></asp:HiddenField>
        <asp:Panel ID="pnAppraisalApproval" runat="server" Style="display: none; overflow: scroll; padding: 10px" Height="400px" Width="60%" CssClass="modalPopup">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                        
                                                                                                 <div class="row">
                                                    <div class="col-md-6" style="padding-left: 15px;padding-top: 12px;">
                                                          <div class="text-left">
                                                   <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" /> Cancel Appraisal for <asp:Label  style="font-size: 18px;font-weight:bold; padding-top: 0px;" ID="cnPoplblHeadeInfo" runat="server" ></asp:Label> Approval</h1> <asp:CheckBox runat="server" Checked="True" Enabled="False" style="color: black;font-size: 15px; display:none" ID="chkCancelApprove"   />
                                                </div>
                                                    </div>
                                                    
                                                    <div class="col-md-6" style="padding-top: 15px;padding-right: 15px;">
                                                          <asp:LinkButton ID="btnApprasalResetClose"   OnClick="btnApprasalResetClose_Click" CssClass="btn btn-xs btn-danger pull-right  pull-right" runat="server"><i style="font-size: 16px" class="fa fa-times" aria-hidden="true"></i>
</asp:LinkButton>
                                                    </div>
                                                </div>
                                             
                                             <hr/>
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

.title-widget::before {
    background-color: #ea5644;
    content: "";
    height: 22px;
    left: 0px;
    position: absolute;
    top: -2px;
    width: 5px;
}
 div#cpFormBody_ddlEmpInfo_chosen {
              width: 100%!important
          }

  div#cpFormBody_ddlForwordEmp_chosen {
              width: 100%!important
          }


  
  div#cpFormBody_ddlEmpforCancelApproveAppraisal_chosen {
              width: 100%!important
          }
                                </style>
                                     

                                    <div class="col-md-12" style="display:none">
                                          
                                        <div style="overflow: scroll">
                                                  <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gv_Versions"         CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender" >

                                                      <Columns>

                                                          <asp:TemplateField HeaderText="SL#">
                                                              <ItemTemplate>
                                                                  <%#Container.DataItemIndex+1 %>
                                                              </ItemTemplate>
                                                          </asp:TemplateField>

                                                          <asp:TemplateField HeaderText="Immediate Approved Person ">
                                                              <ItemTemplate>
                                                                  <asp:Label runat="server" ID="SkillsdfsdafaInfo" Text='<%#Eval("empImmediateName") %>'></asp:Label>
                                                              </ItemTemplate>
                                                          </asp:TemplateField>

                                                           <asp:TemplateField HeaderText="Last Approved Person ">
                                                              <ItemTemplate>
                                                                  <asp:Label runat="server" ID="lSsfkillInfo" Text='<%#Eval("empLastApprovName") %>'></asp:Label>
                                                              </ItemTemplate>
                                                          </asp:TemplateField>


                                                          



                                                      </Columns>
                                                  </asp:GridView>
                                                  </div>

                                        </div>
                                     
                                     <br/>
                                     <br/>
                                      <div class="col-md-12">
                                           <br/>
                               
                                           <div class="row">
                                               
                                            <div class="col-md-12">
                                               <div class="text-center">
                                                <asp:Label ID="lblpop_forCancelInfo" runat="server" ></asp:Label>
                                                   </div>
                                                <asp:HiddenField ID="hfEmpInfoId_Cnl" runat="server" ></asp:HiddenField>
                                                <asp:HiddenField ID="hfOptionType_Cnl" runat="server" ></asp:HiddenField>

                                                <div class="form-group " style="display:none">
                                                    <label>Forward to Employee </label>
                                                    
                                                                <asp:DropDownList   runat="server" style="width: 320px !important"     ID="ddlEmpforCancelApproveAppraisal" CssClass="form-control form-control-sm selectme" />
                                                 
                                            </div>
                                                
                                                 <br/>
                                                      <br/>
                                                    
                                                  <div class="text-center">
    <asp:Button runat="server" ID="lbCancelAppraisalSubmit" 
        OnClick="lbCancelAppraisalSubmit_OnClick" 
        Text="Submit"  
        style="font-size: 20px!important;" 
        CssClass="btn btn-info" />  
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
                    <br/>  <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>  <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>  <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                                        </div>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
                 </div>
    </div>
    
              
</asp:Content>



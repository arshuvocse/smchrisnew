<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="ContractualEmpApprovalList.aspx.cs" Inherits="ContractualEmployeeManagement_UI_ContractualEmpApprovalList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
       <div class="content" id="content">
           
           
              

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                      <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px"
                   />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                        <div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;"> Employee State Change Approval List </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                             <asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="HomeButton_OnClick" />
                        <asp:Button ID="addNewButton"   Text="Back to List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="addNewButton_OnClick" />
                    </div>
                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        
                             <div class="row" runat="server" Visible="False">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label></label>
                                        <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True"         RepeatDirection="Horizontal" Enabled="False">
                                        </asp:CheckBoxList>
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
<%--    <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
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
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/buttons/1.5.6/js/buttons.print.min.js"></script>--%>

   <%--   <script>

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
                           filename: 'Employee State Change Approval List',
                           title: 'SMC',
                           messageTop: 'Employee State Change Approval List',
                           exportOptions: {
                               columns: [0, 2, 3, 4, 5, 6, 7]
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
                                  filename: 'Employee State Change Approval List',
                                  title: 'SMC',
                                  messageTop: 'Employee State Change Approval List',
                                  exportOptions: {
                                      columns: [0, 2, 3, 4, 5, 6, 7]
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
                        <div class="card-body">
                              
                          <div class="row" style="padding-top: 10px!important;">
                              <div class="col-md-12" Visible="False" runat="server" id="btnForWordDiv">
                                   <asp:Button ID="btnForrr" Text="Forward to Other" CssClass="btn btn-sm btn-success pull-right"  runat="server" OnClick="btnForrr_OnClick" />                          <asp:Label runat="server" ID="lblstatus" Text="No Approval Path have been  selected." ForeColor="Red" Font-Size="17px"></asp:Label>
                              </div>
                          </div>
                            <div id="gridContainer1" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;">
                                
                              
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered text-center thead-dark" DataKeyNames="ContractualEmpManageId,ContractualEmpManageAppLogId"
                                    OnRowCommand="loadGridView_RowCommand" OnRowCreated="loadGridView_OnRowCreated">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                     <%--   <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />--%>
                                        
                                          <asp:TemplateField>
                                            <HeaderTemplate>
                                              
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" CssClass="form-control-sm"  Visible="False"   runat="server" />
                                                

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee ID" />
                                        <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                        <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                        <asp:BoundField DataField="DateOfJoin" HeaderText="Joining Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="StateChangeReq" HeaderText="State Change Request" />
                                        <asp:BoundField DataField="EffectiveDate" HeaderText="Effective Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="Remarks" HeaderText=" Initiator Remarks" />

                                        <asp:TemplateField HeaderText="Approve">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="linkImageButton" runat="server"  CssClass="btn btn-sm btn-info"
                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="Approve"
                                                    Text="Approve &gt; &gt;" />
                                                
                                                 <asp:HiddenField runat="server" ID="hfContractualEmpManageId" Value='<%#Eval("ContractualEmpManageId")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                     <%--   <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="deleteImageButton" runat="server"
                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="DeleteData"
                                                    ImageUrl="~/Assets/img/delete.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="View" >
                                            <ItemTemplate>
                                                <asp:ImageButton ID="viewImageButton" runat="server"
                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="ViewData"
                                                    ImageUrl="~/Assets/img/list-view.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
           
              <div>
        <ajaxToolkit:ModalPopupExtender ID="MPBehavioral" runat="server" TargetControlID="Behavioral_Test" PopupControlID="pnl_Behavioral"
            BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>

        <asp:HiddenField ID="Behavioral_Test" runat="server"></asp:HiddenField>
        <asp:Panel ID="pnl_Behavioral" runat="server" Style="display: none; overflow: scroll; padding: 10px" Height="600px" Width="90%" CssClass="modalPopup">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                     
                                         <asp:UpdateProgress ID="UpdateProgress2" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWaeeeewit" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>                                                           <div class="row">
                                                    <div class="col-md-6" style="padding-left: 15px;padding-top: 12px;">
                                                          <div class="text-left">
                                                   <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" /> Forward to Other Employee</h1>
                                                </div>
                                                    </div>
                                                    
                                                    <div class="col-md-6" style="padding-top: 15px;padding-right: 15px;">
                                                          <asp:LinkButton ID="btnBehavioralClose"   OnClick="btnBehavioralClose_OnClick" CssClass="btn btn-xs btn-danger pull-right  pull-right" runat="server"><i style="font-size: 16px" class="fa fa-times" aria-hidden="true"></i>
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


   div#cpFormBody_ddlDivision_chosen {
    width: 270px!important;
}

    div#cpFormBody_ddlDivision_chosen {
    width: 270px!important;
}


                                    div#cpFormBody_ddlWing_chosen {
                                        width: 270px !important;
                                    }





                                      div#cpFormBody_ddlDepartment_chosen {
    width: 270px!important;
}


                                        div#cpFormBody_ddlSection_chosen {
    width: 270px!important;
}



                                          div#cpFormBody_ddlSubSection_chosen {
    width: 270px!important;
}



                                            div#cpFormBody_ddlEmpCategory_chosen {
    width: 270px!important;
}



                                                   div#cpFormBody_ddlSalaryGrade_chosen {
    width: 270px!important;
}



                                                          div#cpFormBody_ddlSalaryStep_chosen {
    width: 270px!important;
}


                                                          
                                                          div#cpFormBody_ddlDesignation_chosen {
    width: 270px!important;
}


                                                                  
                                                          div#cpFormBody_ddlEmpInfo_chosen {
    width: 270px!important;
}

                                                          


                                                                                               div#cpFormBody_ddlSalaryLocation_chosen {
    width: 270px!important;
}



                                                                                                                                    div#cpFormBody_ddlJobLocation_chosen {
    width: 270px!important;
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
                                             .SelectchkChoiceDsss label {
           font-size: 16px!important;color: darkred !important;
                                                 padding: 2px;
        }
                                </style>
                                     

                                     
                                                    </div>
                    
                    <div class="row">
                        <div class="col-md-4">
                              <div class="form-group">
                                            <label>Employee Name: </label> &nbsp;<label style="color: #a52a2a">*</label>
                            <asp:DropDownList   runat="server"    ID="ddlEmpInfo" class="form-control form-control-sm selectMee" />
                                  
                                           <script type="text/javascript">
                                               function pageLoad() {
                                                  
                                                   $('.selectMee').chosen({ disable_search_threshold: 5, search_contains: true });

                                               }
</script>
                             </div>
                        </div>
                          <div class="col-md-1" style="padding-top: 18px;">
                                                                            <asp:Button ID="Button1" Text="Add To List" CssClass="btn btn-sm btn-info" runat="server" OnClick="Button1_OnClick" />
                                                                    </div>
                       
                    </div>
                    
                           <div class="row">
                                  <div class="col-md-12">
                    
                           <asp:GridView Width="100%" ShowHeader="True" ID="gv_Details_Save" runat="server" AutoGenerateColumns="false" CssClass="blueTableNew"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         
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
                                 
                                               

                                             
                                                 <asp:TemplateField HeaderText="Employee Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Slbl_EmpName" runat="server" Text='<%#Eval("EmpName") %>'></asp:Label>
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
                        <div class="col-md-4">
                              <div class="form-group">
                                  
                                     <asp:Button ID="submitButton" Text="Submit" CssClass="btn btn-sm btn-info"  runat="server" OnClick="submitButton_OnClick" />
                                  </div>
                                  </div>
                                  </div>
                    
                     <br />
                           <br />
                        </div>
                
                    </div>
                </div>
                        </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
    </div>
   
                                             
</asp:Content>


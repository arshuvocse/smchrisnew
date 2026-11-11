<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="ExecutiveOfficeDocUploadView.aspx.cs" Inherits="HealthCare_UI_ExecutiveOfficeDocumentUpload" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/fancybox/3.5.7/jquery.fancybox.css" integrity="sha512-nNlU0WK2QfKsuEmdcTwkeh+lhGs6uyOxuUs+n+0oXSYDok5qy0EI0lt01ZynHq6+p/tbgpZ7P+yUb+r71wqdXg==" crossorigin="anonymous" referrerpolicy="no-referrer" />

    <script src="https://cdnjs.cloudflare.com/ajax/libs/fancybox/3.5.7/jquery.fancybox.js" integrity="sha512-j7/1CJweOskkQiS5RD9W8zhEG9D9vpgByNGxPIqkO5KrXrwyDAroM9aQ9w8J7oRqwxGyz429hPVk/zR6IOMtSA==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>

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
                             
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" /> Executive Office Document Upload list </h1>
                        </div>
                         <div class="page-heading__container float-right d-none d-sm-block">
                             
                              <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                       
                        
                         <asp:LinkButton ID="AddNewButton"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="AddNewButton_OnClick" > <i class="fa fa-plus"></i>&nbsp;Add New</asp:LinkButton>
                       
                
                <%--<asp:Button ID="reloadButton" Text="Reload" CssClass="btn btn-sm btn-outline-success" runat="server" OnClick="reloadButton_OnClick" />--%>
            </div>
                    </div>
                     
<asp:UpdatePanel runat="server">
    <ContentTemplate>
         <asp:UpdateProgress ID="UpdateProgress4" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait11" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
        <div class="card">
   <div class="card-body">


        <br />
       
                     <style>
        .pagination .page-item.active .page-link  {
            background-color: #007BFF !important;
            border-style: none !important;
            /* W3C */
        }

        
    </style>
    <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />

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
                          titleAttr: 'Export to Excel'
                   ,
                          filename: 'Executive Office Document',
                          title: 'SMC',
                          messageTop: 'Executive Office Document',
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
                                 titleAttr: 'Export to Excel',
                                 filename: 'Executive Office Document',
                                 title: 'SMC',
                                 filename: 'Executive Office Document',
                                 messageTop: '',
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

         });

      </script> 
    
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
       
       
                    <div class="col-md-12">
                        
                        
                        
                                        <div class="form-row">

                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Company</label>
                                                    <label style="color: #a52a2a">*</label>
                                                   <asp:DropDownList runat="server"   ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged"  class="form-control form-control-sm" />
                                                    
                                                            <script type="text/javascript">
                                                                function pageLoad() {

                                                                    $('.SelectMe33').chosen({ disable_search_threshold: 5, search_contains: true });

                                                                    $('.SelectMe').chosen({ disable_search_threshold: 5, search_contains: true });
                                                                }
</script>
                                                </div>
                                            </div>
                                            
                                                 <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Category</label>
                                                   
                                                <%-- <asp:TextBox runat="server"    ID="txtTitle"  class="form-control" TextMode="MultiLine" Rows="2" />--%>
                                                    <asp:DropDownList runat="server" ID="ddlcategory" AutoPostBack="True" OnTextChanged="ddlcategory_OnTextChanged" CssClass="form-control form-control-sm selectMe"/>
                                                    
                                                       <script type="text/javascript">
                                                           function pageLoad() {
                                                               $('.selectMe').chosen({ disable_search_threshold: 5, search_contains: true });


                                                           }
               </script>
                                                </div>
                                            </div>
                                            
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>SubCategory</label>

                                                    <asp:DropDownList runat="server" ID="ddlSubCategory" AutoPostBack="True"
                                                         OnSelectedIndexChanged="ddlSubCategory_OnSelectedIndexChanged"  CssClass="form-control form-control-sm selectMe"/>

                                                  <%--  <asp:TextBox runat="server"   TextMode="MultiLine" Rows="2"    ID="txtSubcategory"  class="form-control  " />--%>
                                                </div>
                                            </div>
 
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Document from Entry Date</label>
                                      
                                              <asp:TextBox runat="server"  CausesValidation="true" AutoPostBack="True" OnTextChanged="EfectiveDate_OnTextChanged" AutoCompleteType="Disabled" class="form-control form-control-sm" ID="EfectiveDate"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupPosition="TopLeft"
                                                Format="dd-MMM-yyyy" PopupButtonID="EfectiveDate" CssClass="MyCalendar"
                                                TargetControlID="EfectiveDate" />
                                                </div>
                                            </div>
                                            
                                              <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Document To Entry Date</label>
                                      
                                              <asp:TextBox runat="server" AutoPostBack="True" OnTextChanged="txtToDate_OnTextChanged"  CausesValidation="true" AutoCompleteType="Disabled" class="form-control form-control-sm" ID="txtToDate"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="TopLeft"
                                                Format="dd-MMM-yyyy" PopupButtonID="txtToDate" CssClass="MyCalendar"
                                                TargetControlID="txtToDate" />
                                                </div>
                                            </div>
                                             <div class="col-md-4">
                                                 </div>
                                               <div class="col-md-4" style="padding-top: 17px">
                                            <asp:LinkButton runat="server" ID="btn_Search" OnClick="btn_Search_OnClick"   CssClass="btn btnMyDesignSearch   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search Information </asp:LinkButton>

                                            <asp:LinkButton runat="server" ID="lbReset" OnClick="lbReset_OnClick" CssClass="btn btnMyDesignReset   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset Information</asp:LinkButton>
                                        </div>
                                            </div>
                        <div class="form-group " runat="server" Visible="False">
                                   
                        
                            <div class="row">
                                <div class="col-md-2" style="padding-top: 8px">  <label class="control-label pull-right">Company:<span   style="color:red; " title="please fill out this field"> * </span></label></div>
                                <div class="col-md-2"> 
                                    
                                       
                                </div>
                                <div class="col-md-2" style="padding-top: 8px">  <label class="control-label pull-right">Subject:<span   style="color:red; " title="please fill out this field"> * </span></label></div>
                                <div class="col-md-2">  </div>
                               <div class="col-md-2" style="padding-top: 8px">  <label class="control-label pull-right">Purpose: </label></div>
                                <div class="col-md-2">  </div>
                            </div>
                              <div style="padding-top: 5px;"></div>
                          <%--  <div style="padding-top: 5px;"></div>--%>
                            <div class="row">
                                
                            </div>
                              <div style="padding-top: 5px;"></div>
                            <div class="row">
                                
                            </div>
                            
                       


                            </div>
                        </div>

            <asp:GridView Width="100%" ShowHeader="True" ID="gv_ViewList" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Company">
                                                    <ItemTemplate>
                                                         
                                                        <asp:Label ID="lbl_ShortName"   runat="server" Text='<%#Eval("ShortName")%>'></asp:Label>
                                       
                                                              <asp:HiddenField runat="server" ID="hfisEditBtn" Value='<%#Eval("isEditBtn")%>' />
                                                        <asp:HiddenField runat="server" ID="HfMasterId" Value='<%#Eval("ExeOffiDocUpId")%>' />
                                                          <asp:HiddenField runat="server" ID="hfActionStatus" Value='<%#Eval("ActionStatus")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                 
                                                <asp:TemplateField HeaderText="Category">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Title" runat="server" Text='<%#Eval("ExeOfficeDocCategory") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                
                                                <asp:BoundField DataField="ExeOfficeDocSubCate" HeaderText="Sub Category" />


                                              <asp:TemplateField HeaderText="Remark" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Purpose" runat="server" Text='<%#Eval("Remarks") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <%--  <asp:BoundField DataField="ActionStatus" HeaderText="Approval Status" />--%>
                                        
                                
                                                   <asp:TemplateField HeaderText="Entry By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_CreateBy" runat="server" Text='<%#Eval("CreateBy") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                
                                                  <asp:BoundField DataField="CreateDate" HeaderText="Entry Date" DataFormatString="{0:dd-MMM-yyyy}" /> 
                                                
                                                
                                                
                                          <%--        <asp:TemplateField HeaderText="Updated By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_UpdateBy" runat="server" Text='<%#Eval("UpdateBy") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                
                                                  <asp:BoundField DataField="UpdateDate" HeaderText="Updated Date" DataFormatString="{0:dd-MMM-yyyy}" /> --%>

                                               <%-- Visible='<%#Eval("isEditBtn") %>'--%>
                                                <asp:TemplateField HeaderText="Actions">
                                                    <ItemTemplate>
                                                       
                                                        <asp:LinkButton runat="server" ID="btnEdit"    OnClick="btnEdit_OnClick"   CssClass="btn btn-sm btnMyDesignReset"><i class="fa fa-pencil-square-o"></i> </asp:LinkButton>
                                                      <asp:LinkButton runat="server" ID="btnRemove" OnClick="btnRemove_OnClick"   CssClass="btn btn-sm btn-danger"><i class="fa fa-minus-circle"></i> </asp:LinkButton>
                                                        
                                                         <asp:LinkButton runat="server" ID="btnTransfer" OnClick="btnTransfer_OnClick"   CssClass="btn btn-sm btn-info">Transfer </asp:LinkButton>
                                                        
                                                        <asp:LinkButton runat="server" ID="btnView" OnClick="btnView_OnClick"   CssClass="btn btn-sm btnMyDesignAddtoList"><i class="fa   fa-eye"></i> </asp:LinkButton>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
       <asp:HiddenField runat="server" ID="id_mastetID"/>

     
           <br/> 
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
           <br/> 

        
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
                               </div>
        
        </div>
    
    
    
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
                                                   <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" /> Transfer</h1>
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



                                                          div#cpFormBody_ddlNewSubCat_chosen {
    width: 270px!important;
}


                                                          
                                                          div#cpFormBody_ddlNewCat_chosen {
    width: 270px!important;
}


                                                                  
                                                          div#cpFormBody_ddlPreCat_chosen {
    width: 270px!important;
}

                                                          


                                                                                               div#cpFormBody_ddlPreSubCat_chosen {
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
                    <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Previous Category</label>
                                                    <label style="color: #a52a2a">*</label>
                                                <%-- <asp:TextBox runat="server"    ID="txtTitle"  class="form-control" TextMode="MultiLine" Rows="2" />--%>
                                                    <asp:DropDownList runat="server" Enabled="False"    ID="ddlPreCat" AutoPostBack="True" OnTextChanged="ddlPreCat_OnTextChanged" CssClass="form-control form-control-sm selectMe"/>
                                                    
                                                       <script type="text/javascript">
                                                           function pageLoad() {
                                                               $('.selectMe').chosen({ disable_search_threshold: 5, search_contains: true });


                                                           }
               </script>
                                                </div>
                        
                          <div class="form-group">
                                                    <label>Previous Sub-Category</label>

                                                    <asp:DropDownList runat="server" ID="ddlPreSubCat" Enabled="False"  CssClass="form-control form-control-sm selectMe"/>

                                                  <%--  <asp:TextBox runat="server"   TextMode="MultiLine" Rows="2"    ID="txtSubcategory"  class="form-control  " />--%>
                                                </div>
                                            </div>
                                            
                                            <div class="col-md-3">
                                              <div class="form-group">
                                                    <label>Transfer Category</label>
                                                    <label style="color: #a52a2a">*</label>
                                                <%-- <asp:TextBox runat="server"    ID="txtTitle"  class="form-control" TextMode="MultiLine" Rows="2" />--%>
                                                    <asp:DropDownList runat="server" ID="ddlNewCat" AutoPostBack="True" OnTextChanged="ddlNewCat_OnTextChanged" CssClass="form-control form-control-sm selectMe"/>
                                                    
                                                    
                                                </div>
                        
                          <div class="form-group">
                                                    <label>Transfer Sub-Category</label>

                                                    <asp:DropDownList runat="server" ID="ddlNewSubCat"  CssClass="form-control form-control-sm selectMe"/>

                                                  <%--  <asp:TextBox runat="server"   TextMode="MultiLine" Rows="2"    ID="txtSubcategory"  class="form-control  " />--%>
                                                </div>
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
</asp:Content>


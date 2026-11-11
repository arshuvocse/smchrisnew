<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" EnableEventValidation="true" AutoEventWireup="true" CodeFile="BillSettlementView.aspx.cs" Inherits="HealthCare_UI_BillSettlementView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    
    
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
           }
       </style>
    
     <style>
        .dt-button.buttons-print,
        .dt-button.buttons-excel.buttons-html5,
        .dt-button.buttons-pdf.buttons-html5 {
            
            background-color: #4CAF50;
  border: none;
  color: white;
  padding: 5px 18px;
  text-align: center;
  text-decoration: none;
  display: inline-block;
  font-size: 16px;
  margin: 2px 1px;
  cursor: pointer;
  -webkit-transition-duration: 0.4s; 
  transition-duration: 0.4s;
  box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2), 0 3px 10px 0 rgba(0,0,0,0.19);
        }

 
.dt-buttons {
    align-content: center;
    text-align: center;
}
.dt-button.buttons-pdf.buttons-html5:hover {
  box-shadow: 0 12px 16px 0 rgba(0,0,0,0.24),0 17px 50px 0 rgba(0,0,0,0.19);
}
</style>
    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                  <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                        <div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Bill Settlement List </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                 
                         <asp:LinkButton ID="HomeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="HomeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                         <asp:LinkButton ID="btn_New"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="addNewButton_OnClick" > <i class="fa fa-plus"></i>&nbsp;Add New</asp:LinkButton>
                    </div>

                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card" >
                        <div class="card-body" >
                             
                            <div id="gridContainer1" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;">
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False" OnPreRender="gv_DocumentUpload_PreRender"
                                    CssClass="AddToListCssTable" DataKeyNames="BillSettlmentId"   OnRowCommand="loadGridView_RowCommand"
                                   >
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Print">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ViewReportImageButton" runat="server" class="btn btn-white btn-sm " CommandArgument="<%# Container.DataItemIndex %>"
                                                            CommandName="ViewReport" ImageUrl="~/Assets/report_magnify.png" />
                                                    </ItemTemplate>
                                                </asp:TemplateField> 

                                        <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee ID" />
                                        <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="ClaimNo" HeaderText="Claim No" />   
                                        <asp:BoundField DataField="SettlementDate" HeaderText="Settlement Date" DataFormatString="{0:dd-MMM-yyyy}" />                   
                                        <asp:BoundField DataField="RecommendedPayment" HeaderText="Recommended Payment" Visible="False" />  
                                        <asp:BoundField DataField="PayableFrom" HeaderText="Payable From" />                                          
                                        <asp:BoundField DataField="PaymentType" HeaderText="Payment Type" />
                                      <%--  <asp:BoundField DataField="CheckDate" HeaderText="Check Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                            --%>
                                            <asp:TemplateField HeaderText="Delete" Visible="False">
                                                    <ItemTemplate>
                                                         
                                                        
                                                        <asp:LinkButton runat="server" ID="btnDelete"  CommandArgument="<%# Container.DataItemIndex %>"
                                                            CommandName="Delete"   CssClass="btn btn-sm btn-danger"><i class="fa fa-trash"></i> </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                              
                                   
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>


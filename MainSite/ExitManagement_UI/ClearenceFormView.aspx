<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="ClearenceFormView.aspx.cs" Inherits="ExitManagement_UI_EmployeeJobLeftEntryView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    
    <style>
         .btnMyDesignScan {
                        background-color: #2EAF5A !important;
                        color: white !important;
                        margin: 2px 1px !important;
                        cursor: pointer !important;
                        -webkit-transition-duration: 0.4s !important;
                        transition-duration: 0.4s !important;
                        box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2), 0 3px 10px 0 rgba(0,0,0,0.19) !important;
                    }
    </style>
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
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;"> Clearance Form </h1>
                    </div>

                    <div class="page-heading__container float-right d-none d-sm-block">
                    <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                          <%--  <asp:Button ID="addNewButton" Text="Add New Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="addNewButton_OnClick" />--%>
                    </div>
                    


                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">
                            
                             <div class="row" runat="server" Visible="False">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label></label>
                                        <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True" RepeatDirection="Horizontal" Enabled="False">
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                             <div class="row" >
                                  <div class="col-md-3">
                                    <div class="form-group">
                               <asp:LinkButton ID="bllHistory" runat="server"  OnClick="bllHistory_OnClick" CssClass="btn btnMyDesignScan"
                                                   
                                                     >View  Approval History</asp:LinkButton>  
                                 </div>
                                 </div>

                            <br/>
                                 </div>
                             <div style="margin-bottom: 10px; text-align: left; padding-left: 5px;">
                                 <strong>Assign List Status: </strong> 
                                 <span style="padding: 4px 8px; border-radius: 12px; font-size: 11px; font-weight: bold; background-color: #28a745; color: white; margin-right: 5px;">Done</span>
                                 <span style="padding: 4px 8px; border-radius: 12px; font-size: 11px; font-weight: bold; background-color: #fd7e14; color: white; margin-right: 5px;">Pending</span>
                                 <span style="padding: 4px 8px; border-radius: 12px; font-size: 11px; font-weight: bold; background-color: #6f42c1; color: white;"><i class="fa fa-random"></i> Auto Forward</span>
                             </div>
                             <div id="gridContasiner1" class="table-responsive" style="height: auto; width: 100%; overflow: auto;">
                                 <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                      CssClass="AddToListCssTable"
                                      OnPreRender="gv_DocumentUpload_PreRender"  DataKeyNames="EmployeeJobLeftId,EmployeeId,ExitDetailId,ApprovalStatusShow,ExitMasterId,DivisionId,CompanyName,empinfoidForMain,EmpInfoIdApproval"
                                     OnRowCommand="loadGridView_RowCommand" OnRowCreated="loadGridView_OnRowCreated">
                                     <Columns>
                                         <asp:TemplateField HeaderText="SL">
                                             <ItemTemplate>
                                                 <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                 <asp:HiddenField runat="server" ID="HFExitMasterId" Value='<%#Eval("ExitMasterId")%>' /> <asp:HiddenField runat="server" ID="hfApprovalStatusShow" Value='<%#Eval("ApprovalStatusShow")%>' />
                                                 <asp:HiddenField runat="server" ID="hfDivisionId" Value='<%#Eval("DivisionId")%>' />
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                         
                                          <asp:TemplateField HeaderText="View Details" Visible="False">
                                             <ItemTemplate>
                                                 <asp:ImageButton ID="ViewReportImageButton1" runat="server" class="btn btn-white btn-sm  " CommandArgument='<%#Eval("EmployeeId") %>'
                                                     CommandName="Clearence" ImageUrl="~/Assets/report_magnify.png" />
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />

                                         <asp:BoundField DataField="EmpMasterCode" HeaderText="Empployee ID" />
                                         <asp:BoundField DataField="EmpName" HeaderText="Empployee Name" />
                                         <asp:BoundField DataField="DesignationName" HeaderText="Designation" />
  <asp:BoundField DataField="SalaryLocation" HeaderText="Office" />
                                         <asp:BoundField DataField="JobLeftType" HeaderText="Job Left Type" />
                                         
                                         <asp:BoundField DataField="JobLeftDate" HeaderText="Job Left Date" DataFormatString="{0:dd-MMM-yyyy}" />

                                         <asp:BoundField DataField="Reason" HeaderText="Reason" Visible="False" />
                                         <asp:BoundField DataField="ApprovalStatusShow" HeaderText="Approval Status" Visible="False" />
                                         <asp:TemplateField HeaderText="Assign List Status">
                                             <ItemTemplate>
                                                 <div style="display: flex; flex-wrap: wrap; gap: 4px; min-width: 250px;">
                                                     <asp:Literal ID="litAssignListStatus" runat="server" Text='<%# GetAssignListStatusColored(Eval("EmpNameStatusforAssaignList")) %>'></asp:Literal>
                                                 </div>
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Other Comments" Visible="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblComments" runat="server"  CssClass="btn btn-sm btnMyDesignScan"
                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="appComm"
                                                     >Other Comments</asp:LinkButton>  
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                        <asp:BoundField DataField="PendingDaysInfo" HeaderText="Pending Days" />
                                        <asp:TemplateField HeaderText="Forwarded Status">
                                            <ItemTemplate>
                                                <asp:Literal ID="litForward" runat="server" Text='<%# GetForwardStatusBadge(Eval("Forward")) %>'></asp:Literal>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                     <%--   <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="UpdateBy" HeaderText="UpdateBy" />
                                        <asp:BoundField DataField="UpdateDate" HeaderText="UpdateDate" DataFormatString="{0:dd-MMM-yyyy}" />--%>

                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="iinkButton" runat="server"  CssClass="btn btn-info btn-sm"
                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="Clearance"
                                                     >Go To Clearance &#8921;</asp:LinkButton>  
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        
                                            <asp:BoundField DataField="EmpEntryBy" HeaderText="Initiate By" />
                                        
                                        <asp:BoundField DataField="EntryDate" HeaderText="Initiate Date" DataFormatString="{0:dd-MMM-yyyy}" />

                                       <%-- <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="deleteImageButton" runat="server"
                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="DeleteData"
                                                    ImageUrl="~/Assets/img/delete.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="View">
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
          
          
          
          
          
               <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="HiddenField1" PopupControlID="Panel1"
            BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>

        <asp:HiddenField ID="HiddenField1" runat="server"></asp:HiddenField>
        <asp:Panel ID="Panel1" runat="server" Style="display: none;  padding: 10px" Height="600px" Width="80%" CssClass="modalPopup">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                       <asp:UpdateProgress ID="UpdateProgress2" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWaieeeeeeeeweasdast" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                     
                                                                                                 <div class="row">
                                                    <div class="col-md-6" style="padding-left: 15px;padding-top: 12px;">
                                                          <div class="text-left">
                                                   <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" />  Approval History List</h1>
                                                </div>
                                                    </div>
                                                    
                                                    <div class="col-md-6" style="padding-top: 15px;padding-right: 15px;">
                                                          <asp:LinkButton ID="LinkButton2"   OnClick="LinkButton2_OnClick" CssClass="btn btn-xs btn-danger pull-right  pull-right" runat="server"><i style="font-size: 16px" class="fa fa-times" aria-hidden="true"></i>
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


                                </style>
                                     

                                    <div class="col-md-12">
                                        
                                          <div id="gridContassiner1" style="height: 450px; overflow: auto; width: auto;">
                                         
                                                 <asp:GridView ID="gv_History"  EmptyDataText="There are no data records to display." runat="server" AutoGenerateColumns="False"
                                     CssClass="AddToListCssTable"
                                     OnPreRender="gv_DocumentUpload_PreRender"  DataKeyNames="EmployeeJobLeftId,EmployeeId,ExitDetailId,ApprovalStatusShow,ExitMasterId"
                                    OnRowCommand="gv_History_RowCommand"  >
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="View Details" >
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ViewReportImageButton1" runat="server" class="btn btn-white btn-sm  " CommandArgument='<%#Eval("EmployeeId") %>'
                                                    CommandName="Clearence" ImageUrl="~/Assets/report_magnify.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />

                                        <asp:BoundField DataField="EmpMasterCode" HeaderText="Empployee ID" />
                                        <asp:BoundField DataField="EmpName" HeaderText="Empployee Name" />
                                        <asp:BoundField DataField="DesignationName" HeaderText="Designation" />
                                        <asp:BoundField DataField="SalaryLocation" HeaderText="Office" />
                                      
                                        <asp:BoundField DataField="JobLeftType" HeaderText="Job Left Type" />
                                        
                                        <asp:BoundField DataField="JobLeftDate" HeaderText="Job Left Date" DataFormatString="{0:dd-MMM-yyyy}" />

                                        <asp:BoundField DataField="Reason" HeaderText="Reason" />
                                        <asp:BoundField DataField="ApprovalStatusShow" HeaderText="Approval Status" />

                                        <asp:TemplateField HeaderText="Approved / Forwarded By">
                                            <ItemTemplate>
                                                <asp:Literal ID="litApprovedForwardInfo" runat="server" Text='<%# GetApprovedForwardInfo(Eval("ApprovedBy"), Eval("ForwardedTo")) %>'></asp:Literal>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="EmpEntryBy" HeaderText="Initiate By" />
                                        
                                        <asp:BoundField DataField="EntryDate" HeaderText="Initiate Date" DataFormatString="{0:dd-MMM-yyyy}" />

                                </Columns>
                            </asp:GridView>
                                              </div>
                                     </div>
                            </div>
                               
                  
                        </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>

          
               <ajaxToolkit:ModalPopupExtender ID="mpe_1" runat="server" TargetControlID="hnd_Test" PopupControlID="pnl_1"
            BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>

        <asp:HiddenField ID="hnd_Test" runat="server"></asp:HiddenField>
        <asp:Panel ID="pnl_1" runat="server" Style="display: none;  padding: 10px" Height="600px" Width="60%" CssClass="modalPopup">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                       <asp:UpdateProgress ID="UpdateProgress1" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWaieseeeeeeewet" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                     
                                                                                                 <div class="row">
                                                    <div class="col-md-6" style="padding-left: 15px;padding-top: 12px;">
                                                          <div class="text-left">
                                                   <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" /> Other Comments List</h1>
                                                </div>
                                                    </div>
                                                    
                                                    <div class="col-md-6" style="padding-top: 15px;padding-right: 15px;">
                                                          <asp:LinkButton ID="LinkButton1"   OnClick="btnNo_Click" CssClass="btn btn-xs btn-danger pull-right  pull-right" runat="server"><i style="font-size: 16px" class="fa fa-times" aria-hidden="true"></i>
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


                                </style>
                                     

                                    <div class="col-md-12">
                                        
                                          <div id="gridContainer1" style="height: auto; overflow: auto; width: auto;">
                                         
                                                  <asp:GridView runat="server"    AutoGenerateColumns="False" Width="100%" ID="gv_AppraisalFunc" CssClass="table table-bordered text-center thead-dark gridDatatable"   >

                                <Columns>

                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                 
                                    <asp:TemplateField HeaderText="Employee ID">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="txtKpi"     Text='<%#Eval("EmpMasterCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Employee Name">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="txtWeight"  Text='<%#Eval("EmpName") %>'></asp:Label>
                                            
                                        </ItemTemplate>
                                      
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Role ">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="txtWeightPer"  Text='<%#Eval("Rolev") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    
                                        <%-- <asp:TemplateField HeaderText="Status ">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="txtssWeightPer"  Text='<%#Eval("Status") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>--%>
                                    
                                        <asp:BoundField DataField="Resource" HeaderText="Clearance Comments" />

                                        <asp:BoundField DataField="MainRemarks" HeaderText="Further Comments (Optional)" />

                                    
                                </Columns>
                            </asp:GridView>
                                              </div>
                                     </div>
                            </div>
                               
                  
                        </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
</asp:Content>


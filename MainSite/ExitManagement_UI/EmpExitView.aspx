<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="EmpExitView.aspx.cs" Inherits="ExitManagement_UI_EmpExitView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .exit-filter-select + .select2 {
            width: 100% !important;
        }

        .approval-status-list {
            display: flex;
            flex-wrap: wrap;
            gap: 6px;
            min-width: 180px;
        }

        .approval-status-badge {
            display: inline-block;
            padding: 5px 8px;
            border-radius: 4px;
            color: #fff;
            font-size: 12px;
            font-weight: 600;
            line-height: 1.35;
            white-space: normal;
        }

        .approval-status-done {
            background-color: #20a947;
        }

        .approval-status-pending {
            background-color: #e25555;
        }

        .approval-status-not-reached {
            background-color: #f2aa45;
            color: #111;
        }

        .approval-status-legend {
            display: flex;
            flex-wrap: wrap;
            gap: 16px;
            margin-bottom: 8px;
            font-weight: 600;
        }

        .approval-status-legend-item {
            display: inline-flex;
            align-items: center;
            gap: 6px;
        }

        .approval-status-legend-color {
            width: 17px;
            height: 17px;
            border-radius: 3px;
        }

        .mail-notification-note {
            display: flex;
            align-items: center;
            gap: 12px;
            margin-bottom: 12px;
            padding: 12px 18px;
            border: 2px solid #123ee8;
            border-radius: 6px;
            background-color: #fff;
            color: #111;
            font-size: 16px;
            line-height: 1.4;
            box-shadow: 0 0 4px rgba(18, 62, 232, .25);
        }

        .mail-notification-note-container {
            display: flex;
            justify-content: flex-end;
            width: 100%;
        }

        .mail-notification-note i,
        .mail-notification-note strong {
            color: #123ee8;
        }

        .mail-notification-note i {
            flex: 0 0 auto;
            font-size: 30px;
        }
    </style>
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
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;"> Clearance Form Setup List </h1>
                    </div>

                    <div class="page-heading__container float-right d-none d-sm-block">
                      <%--  <asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                        <asp:Button ID="addNewButton" Text="Add New Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="addNewButton_OnClick" />
             --%>
                           <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                       
                        
                         <asp:LinkButton ID="addNewButton"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="addNewButton_OnClick" > <i class="fa fa-plus"></i>&nbsp;Add New</asp:LinkButton>

                        
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
                            <div class="row">
                              <div class="col-md-2">
                                             
                                            <div class="form-group">
                                                <label>Company Name: </label>  &nbsp;<label style="color: #a52a2a">*</label>
                                                <asp:DropDownList ID="companyDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm exit-filter-select" OnSelectedIndexChanged="companyDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:TextBox ID="JobCodetextBox" Visible="False" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                                <asp:HiddenField ID="jobIdHiddenField" runat="server" />
                                            </div>
                                        </div>
                                
                            </div>
                            <div class="form-row align-items-end">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>From Date</label>
                                        <asp:TextBox ID="fromDateTextBox" runat="server" TextMode="Date" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>To Date</label>
                                        <asp:TextBox ID="toDateTextBox" runat="server" TextMode="Date" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>


                                  <div class="col-md-2">
      <div class="form-group">
          <label>Division</label>
          <asp:DropDownList ID="divisionDropDownList" runat="server" AutoPostBack="True"
              CssClass="form-control form-control-sm exit-filter-select" OnSelectedIndexChanged="divisionDropDownList_SelectedIndexChanged"></asp:DropDownList>
      </div>
  </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Department</label>
                                        <asp:DropDownList ID="departmentDropDownList" runat="server" CssClass="form-control form-control-sm exit-filter-select"></asp:DropDownList>
                                    </div>
                                </div>
                              
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Approval Status</label>
                                        <asp:DropDownList ID="approvalStatusDropDownList" runat="server" CssClass="form-control form-control-sm exit-filter-select">
                                            <asp:ListItem Text="-- All Status --" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Completed" Value="Completed"></asp:ListItem>
                                            <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Employee Name</label>
                                        <asp:DropDownList ID="ddlEmpInfo" runat="server" CssClass="form-control form-control-sm exit-filter-select"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Notification</label>
                                        <asp:DropDownList ID="notificationDropDownList" runat="server" CssClass="form-control form-control-sm exit-filter-select">
                                            <asp:ListItem Text="-- All --" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Running" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Not Running" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:LinkButton ID="searchButton" runat="server" CssClass="btn btn-sm btnMyDesignSearch"
                                            OnClick="searchButton_OnClick"><i class="fa fa-search"></i>&nbsp; Search</asp:LinkButton>
                                        <asp:LinkButton ID="resetButton" runat="server" CssClass="btn btn-sm btnMyDesignReset"
                                            OnClick="resetButton_OnClick"><i class="fa fa-retweet"></i>&nbsp; Reset</asp:LinkButton>
                                        <asp:LinkButton ID="exportToExcelButton" runat="server" CssClass="btn btn-sm btn-success"
                                            OnClick="exportToExcelButton_OnClick"><i class="fa fa-file-excel-o"></i>&nbsp; Export To Excel</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="mail-notification-note-container">
                                <div class="mail-notification-note">
                                    <i class="fa fa-info-circle" aria-hidden="true"></i>
                                    <span><strong>Note:</strong> To send or run a mail notification, please tick the checkbox <strong>to the right of the employee name.</strong></span>
                                </div>
                            </div>
                            <div class="approval-status-legend" style="align-items: center;">
                                <span class="approval-status-legend-item">
                                    <span class="approval-status-legend-color approval-status-done"></span>Done
                                </span>
                                <span class="approval-status-legend-item">
                                    <span class="approval-status-legend-color approval-status-pending"></span>Pending
                                </span>
                                <span class="approval-status-legend-item">
                                    <span class="approval-status-legend-color approval-status-not-reached"></span>Not Yet Reached
                                </span>
                                <span class="approval-status-legend-item">
                                    <span style="background-color: #6f42c1; color: #fff; padding: 2px 7px; border-radius: 3px; font-size: 11px; font-weight: bold;"><i class="fa fa-random"></i> Auto forward</span>
                                </span>
                            </div>
                            <div id="gridContasdiner1" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;">
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False" CssClass="AddToListCssTable"
                                     OnPreRender="gv_DocumentUpload_PreRender" DataKeyNames="ExitMasterId,EmployeeId"
                                    OnRowCommand="loadGridView_RowCommand" OnRowCreated="loadGridView_OnRowCreated">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField DataField="CompanyName" HeaderText="Company Name" />--%>
                                        
                                             <asp:TemplateField HeaderText="Clearence Form Print">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ViewReportImageButton1" runat="server" class="btn btn-white btn-sm  " CommandArgument='<%#Eval("EmployeeId") %>'
                                                    CommandName="Clearence" ImageUrl="~/Assets/report_magnify.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Attachment">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="iinkBsssutton" runat="server" 
                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="Attachment"
                                                     >Download</asp:LinkButton>  
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="EmpMasterCode" HeaderText="Empployee ID" />
                                        <asp:BoundField DataField="EmpName" HeaderText="Empployee Name" />
                                        <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                      <asp:BoundField DataField="SalaryLocation" HeaderText="Office" />
                                       
                                        
                                     
                                        <asp:TemplateField HeaderText="Employee Approval Status">
                                            <ItemTemplate>
                                                <asp:Literal ID="employeeApprovalStatusLiteral" runat="server"
                                                    Text='<%# FormatEmployeeApprovalStatus(Eval("ExitMasterId"), Eval("EmployeeApprovalStatus")) %>'
                                                    Mode="PassThrough"></asp:Literal>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="TotalDays" HeaderText="Total Days" DataFormatString="{0} Days" />

                                         <asp:BoundField DataField="JobLeftDate" HeaderText="Separation  Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                           <asp:TemplateField Visible="false" HeaderText="Notification">
       <ItemTemplate>
            <asp:CheckBox ID="isRunningCheckBox" runat="server"
                AutoPostBack="True"
                Text="Active"
                Checked='<%# IsExitRunning(Eval("IsRunning")) %>'
                Enabled='<%# !IsApprovalCompleted(Eval("ApprovalStatus")) %>'
                onclick="if (!confirmRunningStatusChange(this)) return false;"
                OnCheckedChanged="isRunningCheckBox_OnCheckedChanged" />
            <asp:Panel ID="declineCommentPanel" runat="server"
                Visible='<%# !IsExitRunning(Eval("IsRunning")) %>'
                Style="margin-top: 5px;">
                <asp:Label ID="declineCommentLabel" runat="server"
                    ForeColor="#C0392B"
                    Font-Size="11px"
                    Text='<%# FormatDeclineComment(Eval("DeclineComment")) %>'>
                </asp:Label>
            </asp:Panel>
        </ItemTemplate>
   </asp:TemplateField>
   <asp:BoundField DataField="EntryBy" HeaderText="initiator by" />

                                        <asp:BoundField DataField="EntryDate" HeaderText="initiator  Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                       
                                        
                          <%--               <asp:BoundField DataField="UpdateBy" HeaderText="UpdateBy" />
                                        <asp:BoundField DataField="UpdateDate" HeaderText="UpdateDate" DataFormatString="{0:dd-MMM-yyyy}" />----%>
                                        
                                        
                                        <asp:TemplateField HeaderText="Action" Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="iinkButton" runat="server"  CssClass='<%#Eval("AppCount") %>'
                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="appSta"
                                                     >Approval Status</asp:LinkButton>  
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="deleteImageButton" runat="server" OnClientClick="return confirm('Are you sure you want to Delete ?')"
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
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="exportToExcelButton" />
            </Triggers>
        </asp:UpdatePanel>
          
          
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
                            <asp:Image ID="imgWaieeeeeeeewet" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                     
                                                                                                 <div class="row">
                                                    <div class="col-md-6" style="padding-left: 15px;padding-top: 12px;">
                                                          <div class="text-left">
                                                   <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" /> Approval Status List</h1>
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
                                    
                                         <asp:TemplateField HeaderText="Status ">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="txtssWeightPer"  Text='<%#Eval("Status") %>'></asp:Label>
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
        </asp:Panel>
          
          
          
          
          
          
          
          
          
          
              <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="HiddenField1" PopupControlID="Panel1"
            BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>

        <asp:HiddenField ID="HiddenField1" runat="server"></asp:HiddenField>
        <asp:Panel ID="Panel1" runat="server" Style="display: none;  padding: 10px" Height="600px" Width="60%" CssClass="modalPopup">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                       <asp:UpdateProgress ID="UpdateProgress2" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWaieeeeessseeewet" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                     
                                                                                                 <div class="row">
                                                    <div class="col-md-6" style="padding-left: 15px;padding-top: 12px;">
                                                          <div class="text-left">
                                                   <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" /> Any Other Attachment List</h1>
                                                </div>
                                                    </div>
                                                    
                                                    <div class="col-md-6" style="padding-top: 15px;padding-right: 15px;">
                                                          <asp:LinkButton ID="btnDocClose"   OnClick="btnDocClose_OnClick" CssClass="btn btn-xs btn-danger pull-right  pull-right" runat="server"><i style="font-size: 16px" class="fa fa-times" aria-hidden="true"></i>
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
                                        
                                          <div id="gridConsstainer1" style="height: auto; overflow: auto; width: auto;">
                                         
                                            <asp:GridView Width="100%" ShowHeader="True" ID="gv_Doc" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Document">
                                                    <ItemTemplate>
                                                          <asp:HyperLink ID="HLDocumentLink"   Target="_blank"  runat="server" NavigateUrl='<%# Eval("DocumentLink") %>'  Text='Download'>
        </asp:HyperLink>
                                                        <asp:Label ID="lbl_DocumentLink" Visible="False" runat="server" Text='<%#Eval("DocumentLink")%>'></asp:Label>
                                                        <asp:HiddenField runat="server" ID="hfFileName" Value='<%#Eval("FileName")%>' />

                                                        <asp:HiddenField runat="server" ID="hfDocumentLink" Value='<%#Eval("DocumentLink")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                 
                                                <asp:TemplateField HeaderText="Summary Note	">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_DocumentNote" runat="server" Text='<%#Eval("DocumentNote") %>'></asp:Label>
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
        </asp:Panel>

        <ajaxToolkit:ModalPopupExtender ID="isRunningModalPopupExtender" runat="server"
            TargetControlID="isRunningModalTarget" PopupControlID="isRunningPanel"
            BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>

        <asp:HiddenField ID="isRunningModalTarget" runat="server"></asp:HiddenField>
        <asp:Panel ID="isRunningPanel" runat="server" Style="display: none; padding: 18px;"
            Width="450px" CssClass="modalPopup">
            <asp:UpdatePanel ID="isRunningUpdatePanel" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-10">
                            <h1 class="title" style="font-size: 18px; padding-top: 0;">
                                <img src="../Report_Pages/app.png" width="20px" />
                                Update Running Status
                            </h1>
                        </div>
                        <div class="col-md-2 text-right">
                            <asp:LinkButton ID="isRunningCloseButton" runat="server"
                                CssClass="btn btn-xs btn-danger" CausesValidation="False"
                                OnClick="isRunningCancelButton_OnClick">
                                <i class="fa fa-times" aria-hidden="true"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <hr />
                    <div class="form-group">
                        <label>Decline Comment</label>
                        <label style="color: #a52a2a">*</label>
                        <asp:TextBox ID="declineCommentTextBox" runat="server"
                            CssClass="form-control form-control-sm"
                            TextMode="MultiLine" Rows="4" MaxLength="1000"></asp:TextBox>
                        <asp:Label ID="isRunningValidationLabel" runat="server"
                            ForeColor="Red"></asp:Label>
                    </div>
                    <div class="text-right">
                        <asp:LinkButton ID="isRunningSubmitButton" runat="server"
                            CssClass="btn btn-sm btn-success"
                            OnClick="isRunningSubmitButton_OnClick">
                            <i class="fa fa-check"></i>&nbsp; Submit
                        </asp:LinkButton>
                        <asp:LinkButton ID="isRunningCancelButton" runat="server"
                            CssClass="btn btn-sm btn-secondary" CausesValidation="False"
                            OnClick="isRunningCancelButton_OnClick">
                            Cancel
                        </asp:LinkButton>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>

    <script type="text/javascript">
        function confirmRunningStatusChange(checkBox) {
            if (checkBox.checked) {
                return confirm('Are you sure you want to update Running status?');
            }

            return true;
        }

        function updateApprovalStatus(chk) {
            var masterId = $(chk).attr('data-masterid');
            var empCode = $(chk).attr('data-empcode');
            var isChecked = chk.checked;
            var labelText = $(chk).parent().text().trim();
            var approvalStatus = labelText.indexOf('[Supervisor]') !== -1 ? 'as Supervisor' : 'as Department';

            var confirmMsg = "Are you sure you want to " + (isChecked ? "enable" : "disable") + " notification for Employee Code: " + empCode + "?";
            if (!confirm(confirmMsg)) {
                chk.checked = !isChecked;
                return;
            }

            $.ajax({
                type: "POST",
                url: "EmpExitView.aspx/UpdateApproval",
                data: JSON.stringify({ masterId: masterId, empCode: empCode, approvalStatus: approvalStatus, isDone: isChecked }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d === "Success") {
                        // Click the search button to refresh the grid under UpdatePanel
                        $("[id$='searchButton']")[0].click();
                    } else {
                        alert("Error updating status: " + response.d);
                        chk.checked = !isChecked;
                    }
                },
                error: function (xhr, status, error) {
                    alert("An error occurred: " + error);
                    chk.checked = !isChecked;
                }
            });
        }

        function pageLoad() {
            $('.exit-filter-select').each(function () {
                var $ddl = $(this);

                if ($ddl.hasClass('select2-hidden-accessible')) {
                    $ddl.select2('destroy');
                }

                $ddl.select2({
                    width: '100%'
                });
            });
        }
    </script>
</asp:Content>


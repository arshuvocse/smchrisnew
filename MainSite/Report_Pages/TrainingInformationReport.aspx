<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="TrainingInformationReport.aspx.cs" Inherits="Report_Pages_TrainingInformationReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
            .btnexcelcc {
            border: none;
            color: #131313;
            padding-left: 36px;
            padding-top: 8px;
            padding-bottom: 8px;
            padding-right: 36px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 12px;
            margin: 4px 2px;
            cursor: pointer;
            background: url(../Assets/excel.png);
            background-position: center;
            background-repeat: no-repeat;
            box-shadow: 0 0 3px 1px rgba(0,0,0,.35);
        }
         .btnexcel {
             background-color: #4CAF50;
             border: none;
             color: white;
             padding: 8px 12px;
             text-align: center;
             text-decoration: none;
             display: inline-block;
             font-size: 12px;
             margin: 4px 2px;
             cursor: pointer;
         }

        .btnPDF {
            background-color: #008CBA;
            border: none;
            color: white;
            padding: 8px 12px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 12px;
            margin: 4px 2px;
            cursor: pointer;
        }


   

/*#cpFormBody_loadGridView footer {
  padding: 10px !important;
    border-style: none !important;

  background-color: #CCE5FF !important;
  color: black !important;
    font-weight: bold !important;
    font-size: 13px !important;
}*/
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">

    <div class="content" id="content">
        <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                      <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
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
                
                <h1 class="title" style="font-size: 18px; padding-top: 9px;"><img src="app.png"  width="18px" /> Training Information Report </h1>
            </div>

            <%--<div class="page-heading__container float-right d-none d-sm-block">
                <asp:Button ID="homeButton" Visible="False" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                <asp:Button ID="addNewButton" Visible="False" Text="Add New Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="addNewButton_OnClick" />
            </div>--%>
        </div>
        <!-- //END PAGE HEADING -->

        <div class="container-fluid">
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


                            <div class="form-row">
                            
                                 <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Report Name </label>
                                        <span style="color: red">&nbsp;*</span>
                                        <asp:DropDownList ID="reportDropDownList" AutoPostBack="True" runat="server" class="form-control form-control-sm" OnSelectedIndexChanged="reportDropDownList_SelectedIndexChanged">
                                           <asp:ListItem Value="0">Select One</asp:ListItem>
                                            <asp:ListItem  Value="Summary">Summary Report </asp:ListItem>
                                            <asp:ListItem Value="Details">Details Report </asp:ListItem>
                                         
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                
                                    <div class="col-2">
                                    <div class="form-group">
                                        <label>Company</label> <span style="color:red;" >*</span>
                                        <asp:DropDownList runat="server" ID="ddlCompany" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" CssClass="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Financial Year</label> <span style="color:red;" >*</span>
                                        <asp:DropDownList ID="ddlFinancialYear" OnSelectedIndexChanged="ddlFinancialYear_OnSelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Training Title</label>
                                        <asp:DropDownList runat="server" ID="ddlTrainingRecord" AutoPostBack="true" CssClass="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                
                                  <div class="col-2" runat="server" >
                                <div class="form-group">
                                    <label class="control-label">Training End  Date From</label>
                                    <asp:TextBox runat="server" ID="ContractualstartDate" class="form-control form-control-sm" AutoPostBack="True" ></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender39" runat="server"
                                                                  Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                  TargetControlID="ContractualstartDate" />
                                </div>
                            </div>
                            <div class="col-2" runat="server" >
                                <div class="form-group">
                                    <label class="control-label">Training End  Date To</label>
                                    <asp:TextBox runat="server" ID="ContractualendDate" class="form-control form-control-sm" AutoPostBack="True" ></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender40" runat="server"
                                                                  Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                  TargetControlID="ContractualendDate" />
                                </div>
                            </div>

                                <div class="col-md-1">
                                    <div class="form-group">
                                        <label>&nbsp;&nbsp;</label>
                                        <asp:Button runat="server" ID="SearchButton" OnClick="SearchButton_OnClick" ToolTip="Click To Search" Text="Search" class="btn btn-outline-info btn-block disabled btn-sm" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div class="row" runat="server" Visible="False">
                        <div class="col-md-2">
                            <label>Training Information </label>
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-1">
                        </div>
                        <div class="col-md-3">
                            <asp:LinkButton ID="btnExportToExcel" runat="server" CssClass="btnexcel  pull-right" OnClick="btnExportToExcel_Click"><span aria-hidden="true" class="glyphicon glyphicon-ok"></span>Export To Excel</asp:LinkButton>
                            <%--<asp:LinkButton ID="btnExportToPDF" runat="server" Visible="False" CssClass="btnPDF  pull-right" OnClick="btnExportToPDF_Click"><span aria-hidden="true" class="glyphicon glyphicon-apple"></span>Export To PDF</asp:LinkButton>--%>
                        </div>

                    </div>
                    
                      <br/>
                            <br/>
                            <div class="form-row" style="padding-right: 10px">

                                <div class="col-md-6" style="padding-left: 2px">
                                    <label style="font-size: 16px;">Details Information</label>
                                </div>
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-2">
                                    <%--  <asp:LinkButton ID="btnExportToExcel" runat="server" CssClass="btnexcel  pull-right" Style="padding-right: 10px;" OnClick="btnExportToExcel_Click"><span aria-hidden="true" style="font-size: 14px; color: #4CAF50;" class="fa fa-file-excel-o"></span> Download to xls</asp:LinkButton>--%>
                                       <asp:LinkButton ID="LinkButton1" runat="server"  CssClass="btnexcel  pull-right" OnClick="btnExportToExcel2_Click" ><i class="fa fa-file-excel-o"></i> Export To Excel</asp:LinkButton>
                                    <%--<input type="button"  id="btnExportDisciplinary" title="Export to Excel" class="pull-right btnexcelcc " value="" />--%>
                                </div>


                            </div>

                            <hr />


                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div id="gridContainer1" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;">
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                    CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="TrainingType" HeaderText="Training Type" />
                                        <asp:BoundField DataField="TrainingTitle" HeaderText="Training Title" />
                                         <asp:BoundField DataField="TrainingOrgName" HeaderText="Training Organization" />
                                        
                                        <asp:BoundField DataField="TotalParticipant" HeaderText="Total Participant" />
                                      
                                        <asp:BoundField DataField="TotalHoure" HeaderText="Total Hours" />
                                        
                                       
                                        <asp:BoundField DataField="TrainingCost" HeaderText="Trainer Cost" />
                                        <asp:BoundField DataField="LogisticCost" HeaderText="Food & Venue Cost" />
                                        <asp:BoundField DataField="OtherCost" HeaderText="Other Cost" />
                                        <asp:BoundField DataField="GrandTotal" HeaderText="Grand Total" />
                                    </Columns>
                                 
                                </asp:GridView>
                                
                                
                                
                                  <asp:GridView ID="gv_participant" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                  CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="TrainingTitle" HeaderText="Training/Workshop/
Seminar "
 />
                                        <asp:BoundField DataField="TrainingDate" HeaderText="Date" />
                                         <asp:BoundField DataField="EmpMasterCode" HeaderText="ID" />
                                        
                                        <asp:BoundField DataField="EmpName" HeaderText="Participant Name" />
                                      
                                        <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                        
                                       
                                        <asp:BoundField DataField="DivisionName" HeaderText="Division" />
                                        <asp:BoundField DataField="Durration" HeaderText="Duration" />
                                        <asp:BoundField DataField="TotalTime" HeaderText="Time" />
                                        <asp:BoundField DataField="TrainingOrgName" HeaderText="Institute/Organization
" />
                                        
                                          <asp:BoundField DataField="TrainingPlace" HeaderText="Venue" />
                                          <asp:BoundField DataField="CostPerParticipant" HeaderText="Cost per person in Tk.
" />
                                          <asp:BoundField DataField="ManHour" HeaderText="Man Hour
" />
                                    </Columns>
                                 
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </div>
     <script src="../Assets/table2excel.js"></script>
    <script type="text/javascript">
        $("body").on("click", "#btnExportDisciplinary", function () {
            $("[id*=loadGridView]").table2excel({
                filename: "Training_Information_Report_List.xls"
            });
        });

    </script>
</asp:Content>


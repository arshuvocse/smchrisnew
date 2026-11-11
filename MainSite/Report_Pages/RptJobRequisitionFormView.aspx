<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="RptJobRequisitionFormView.aspx.cs" Inherits="MasterSetup_UI_RptJobRequisitionFormView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">
        <style>
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

            #cpFormBody_loadGridView > tbody > tr > th {
                padding: 9px 0;
                color: #fff;
                background-color: #5B799E;
                /*background-color: #98A9C0;*/
            }
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
        </style>
      
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;"> <img src="app.png"  width="18px" /> Recruitment Completion  List Report </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="HomeButton" Visible="False" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="HomeButton_OnClick" />
                        <asp:Button ID="addNewButton"  Visible="False"  Text="Add New Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="addNewButton_OnClick" />
                        <%--<asp:Button ID="reloadButton" Text="Reload" CssClass="btn btn-sm btn-outline-success" runat="server" OnClick="reloadButton_OnClick" />--%>
                    </div>

                </div>
                <!-- //END PAGE HEADING -->



                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">

             
    <!-- Modal Popup -->
    <script type="text/javascript">
        function ShowPopup() {
           
            $("#exampleModal").modal("show");
        }
    </script>
                            <style>
                                .w3-tag{background-color:#FF9800;color:#fff;padding: 4px;border-radius:10%}
                                .w3-green,.w3-hover-green:hover{color:#fff!important;background-color:#4CAF50}
                              
                            </style>
                    

                             <div class="modal fade" ID="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" >
                                <div class="modal-dialog" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h3 class="modal-title" id="exampleModalLabel" style="color:#FF9800;">All Recruitment Process</h3>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                        <div class="modal-body">
                                         
                                            
                                             <div class="col-md-12">
                                              <h5>Job Circulation: <asp:Label runat="server" ID="PJobCirculation"  class="label w3-tag w3-green" style="font-size: 12px;"  ><label   runat="server" id="lblJobCirculation"></label></asp:Label></h5>
                                            </div>
                                            
                                             <div class="col-md-12">
                                              <h5>Interview Candidate Information: <asp:Label runat="server" ID="PewCandidateInformation"  class="label w3-tag w3-green" style="font-size: 12px;" ><label runat="server" id="lblInterviewCandidateInformation"></label></asp:Label></h5>
                                            </div>
                                            
                                             <div class="col-md-12">
                                              <h5>Interview Board Information: <asp:Label runat="server" ID="PInterviewBoardInformation" class="label  w3-tag w3-green" style="font-size: 12px;"><label runat="server" id="lblInterviewBoardInformation"></label></asp:Label></h5>
                                            </div>
                                            
                                              <div class="col-md-12">
                                              <h5>Interview Candidate Invitation: <asp:Label runat="server" ID="PInterviewCandidateInvitation" class="label  w3-tag w3-green" style="font-size: 12px;" ><label runat="server" id="lblInterviewCandidateInvitation"></label></asp:Label></h5>
                                            </div>
                                          <div class="col-md-12">
                                              <h5> Candidate Attandance: <asp:Label runat="server" ID="PCandidateAttandance" class="label  w3-tag w3-green" style="font-size: 12px;" ><label runat="server" id="lblCandidateAttandance"></label></asp:Label></h5>
                                            </div>
                                            
                                            
                                               <div class="col-md-12">
                                              <h5> Interview Board Member Marks Entry: <asp:Label runat="server" ID="PMarksEntry"  class="label  w3-tag w3-green" style="font-size: 12px;" ><label runat="server" id="lblMarksEntry"></label></asp:Label></h5>
                                            </div>
                                            
                                            
                                              <div class="col-md-12">
                                              <h5> Employee Information: <asp:Label runat="server" ID="PEmployeeInformation" CssClass="label  w3-tag w3-green" style="font-size: 12px;" ><asp:Label runat="server" id="lblEmployeeInformation"></asp:Label></asp:Label></h5>
                                            </div>
                                            <div class="col-md-12">
                                              <h5> Recruitment Process Current Status: <asp:Label runat="server" ID="Label1" CssClass="label  w3-tag w3-green" style="font-size: 12px;" ><asp:Label runat="server" id="lblLast"></asp:Label></asp:Label></h5>
                                            </div>
                                             
                                        </div>
                                        <div class="modal-footer">
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row" runat="server" visible="False">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label></label>
                                        <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True" RepeatDirection="Horizontal" Enabled="False">
                                        </asp:CheckBoxList>

                                        <asp:CheckBoxList runat="server" ID="lchk_Dpt" AutoPostBack="True" RepeatDirection="Horizontal" Enabled="False">
                                        </asp:CheckBoxList>

                                    </div>
                                </div>
                            </div>

                              <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>

                            <div class="row">


                                
                                    <div class="col-md-2">

                                    <div class="form-group">
                                        <label>Company Name: </label>
                                        <asp:DropDownList ID="companyDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="companyDropDownList_SelectedIndexChanged"></asp:DropDownList>

                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                        <asp:DropDownList ID="financialYearDropDownList" class="form-control form-control-sm" runat="server" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Department </label>

                                        <asp:DropDownList ID="deptDropDownList" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                
                                     <div class="col-2" runat="server" >
                                <div class="form-group">
                                    <label class="control-label">Requisition From Date</label>
                                    <asp:TextBox runat="server" ID="ContractualstartDate" class="form-control form-control-sm" AutoPostBack="True" ></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender39" runat="server"
                                                                  Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                  TargetControlID="ContractualstartDate" />
                                </div>
                            </div>
                            <div class="col-2" runat="server" >
                                <div class="form-group">
                                    <label class="control-label">Requisition To Date</label>
                                    <asp:TextBox runat="server" ID="ContractualendDate" class="form-control form-control-sm" AutoPostBack="True" ></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender40" runat="server"
                                                                  Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                  TargetControlID="ContractualendDate" />
                                </div>
                            </div>

                                     <div class="col-md-2">
                                    <div class="form-group" style="margin-top: 17px;">

                                        <asp:Button runat="server" ID="SearchButton" OnClick="SearchButton_OnClick" ToolTip="Click To Search" Text="Search" class="btn btn-info btn-sm" />
                                    </div>
                                </div>


                               



                               

                            </div>
                 </ContentTemplate>
                 <%--  <Triggers>
            <asp:PostBackTrigger ControlID="btnStatus" />
        </Triggers>   --%>
        </asp:UpdatePanel>
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

                                    <input type="button" id="btnExportDisciplinary" title="Export to Excel" class="pull-right btnexcelcc " value="" />
                                </div>


                            </div>

                            <hr />
                            
                              <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWaidst" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                           <div class="row">
                               <div class="col-md-12">
                                    <div id="gridContainer1">
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered text-center thead-dark" 
                                    OnRowCommand="loadGridView_RowCommand" Font-Size="11px" DataKeyNames="JobReqId">
                                    <Columns>

                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                       
                                        
                                      
                                        <asp:BoundField DataField="JobReqFormDate" HeaderText="Requisition Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                          <asp:BoundField DataField="jobTitle" HeaderText="Job Title" />
                                          <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                          <asp:BoundField DataField="EmpCategoryName" HeaderText="Employee Type" />
                                        <asp:BoundField DataField="Nos" HeaderText="Total Vacancy" />
                                        <asp:BoundField DataField="TotalJoin" HeaderText="Total Joining" />
                                      
                                      
                                      
                                        
                                     
                                      

                               
                                         

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
                 </ContentTemplate>
                 <%--  <Triggers>
            <asp:PostBackTrigger ControlID="btnStatus" />
        </Triggers>   --%>
        </asp:UpdatePanel>
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
           
    </div>
    
     <script src="../Assets/table2excel.js"></script>
    <script type="text/javascript">
        $("body").on("click", "#btnExportDisciplinary", function () {
            $("[id*=loadGridView]").table2excel({
                filename: "Recruitment_Completion_List_Report.xls"
            });
        });

    </script>
</asp:Content>


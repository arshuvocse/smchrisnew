<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="MOKRBSCApppraisalApprove.aspx.cs" Inherits="Appraisal_MOKRBSCApppraisalApprove" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .resizable-textbox {
            resize: both;
            overflow: auto;
            min-width: 120px;
            min-height: 30px;
        }
        .status-red {
            background-color: #ffcccc !important;
            color: #cc0000 !important;
            font-weight: bold;
        }

        .status-green {
            background-color: #ccffcc !important;
            color: #006600 !important;
            font-weight: bold;
        }

        .status-yellow {
            background-color: #ffffcc !important;
            color: #996600 !important;
            font-weight: bold;
        }
    </style>
    <script type="text/javascript">
        function normalizeMidYearOptions(dropdown) {
            for (let i = 0; i < dropdown.options.length; i++) {
                const opt = dropdown.options[i];
                if (opt.value === 'Ongoing') {
                    opt.text = 'On Track';
                }
                if (opt.value === 'Not Completed') {
                    opt.text = 'Behind';
                }
            }
        }

        function setMidYearStatusColor(dropdown) {
            const v = (dropdown.value || '').trim();
            const t = (dropdown.options[dropdown.selectedIndex]?.text || '').trim();

            dropdown.classList.remove('status-red', 'status-green', 'status-yellow');

            if (v === 'Not Started' || v === 'Not Completed' || t === 'Not Started' || t === 'Behind' || t === 'Not Completed') {
                dropdown.classList.add('status-red');
                return;
            }

            if (v === 'Ongoing' || v === 'Completed' || t === 'On Track' || t === 'Ongoing' || t === 'Completed') {
                dropdown.classList.add('status-green');
                return;
            }

            if (v === 'Not Relevant' || t === 'Not Relevant') {
                dropdown.classList.add('status-yellow');
                return;
            }
        }

        function initializeMidYearColors() {
            const dropdowns = document.querySelectorAll('select[id*="txtMidStatus"]');

            dropdowns.forEach(function (ddl) {
                normalizeMidYearOptions(ddl);
                setMidYearStatusColor(ddl);

                if (ddl.getAttribute('data-colorbound') === '1') return;
                ddl.setAttribute('data-colorbound', '1');

                ddl.onchange = function () {
                    normalizeMidYearOptions(this);
                    setMidYearStatusColor(this);
                };
            });
        }

        document.addEventListener('DOMContentLoaded', initializeMidYearColors);

        if (typeof Sys !== 'undefined' && Sys.WebForms) {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                initializeMidYearColors();
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    


    <div class="content">
        <style>
            .MyBtnInfoCss {
                background-color: #880e4f !important;
                color: white !important;
                border: none !important;
                padding: 6px 18px !important;
                text-align: center !important;
                text-decoration: none !important;
                display: inline-block !important;
                font-size: 16px !important;
                cursor: pointer !important;
                -webkit-transition-duration: 0.4s !important;
                transition-duration: 0.4s !important;
                box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2), 0 3px 10px 0 rgba(0,0,0,0.19) !important;
            }
        </style>
        <div class="container-fluid">
            <div class="page-heading">
                <div class="page-heading__container">
                    <h1 class="title" style="font-size: 18px; padding-top: 0px;">
                        <img src="../Report_Pages/app.png" width="20px" />
                        BSC/OKR Mid-Year  Approval</h1>
                </div>
                <div class="page-heading__container float-right d-none d-sm-block">
                    <asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="vcchomeButton_OnClick" />
                </div>
            </div>

            <div class="card">
                <div class="card-body">



                    <script type="text/javascript">
                        function ShowPopup() {

                            $("#exampleModal").modal("show");
                        }
                    </script>
                    <style>
                        .chkChoiceHeader label {
                            padding-left: 2px;
                            padding-right: 10px;
                            font-size: 13px;
                        }

                        .w3-tag {
                            background-color: #FF9800;
                            color: #fff;
                            padding: 4px;
                            border-radius: 10%;
                        }

                        .w3-green, .w3-hover-green:hover {
                            color: #fff !important;
                            background-color: #4CAF50;
                        }
                    </style>


                    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h3 class="modal-title" id="exampleModalLabel" style="color: #FF9800;">Previous Comments</h3>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">


                                    <div class="col-md-12">
                                        <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" ID="GridView1" CssClass="AddToListCssTable" OnPreRender="gv_DocumentUpload_PreRender">

                                            <Columns>

                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                        <%--<asp:HiddenField ID="id_empId" runat="server" Value='<%#Eval("EmpInfoId") %>' />
                                                        <asp:HiddenField ID="selfMaster" runat="server" Value='<%#Eval("AppraisalSelfMasterId") %>' />
                                                        <asp:HiddenField ID="id_appraisalMaster" runat="server" Value='<%#Eval("AppraisalMasterId") %>' />
                                                        --%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_EmpName" runat="server" class="form-control form-control-sm" Text='<%#Eval("EmpName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Comment">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_comment" runat="server" class="form-control form-control-sm" Text='<%#Eval("DateOfJoin") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>






                                            </Columns>
                                        </asp:GridView>
                                    </div>



                                </div>
                                <div class="modal-footer">
                                </div>
                            </div>
                        </div>
                    </div>


                    <asp:UpdatePanel runat="server" ID="upFormBody">
                        <ContentTemplate>
                            <asp:UpdateProgress ID="UpdateProgress10" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaiting">
                                        <asp:Image ID="imgWait1" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>

                            <div class="form-row" runat="server" visible="False">
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Company</label>
                                        <asp:DropDownList runat="server" ID="ddlCompany" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" CssClass="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-3" style="display: none">
                                    <div class="form-group">
                                        <label>Division</label>
                                        <asp:DropDownList runat="server" ID="ddlDivision" AutoPostBack="true" OnSelectedIndexChanged="ddlDivision_OnSelectedIndexChanged" CssClass="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-3" style="display: none">
                                    <div class="form-group">
                                        <label>Wing</label>
                                        <asp:DropDownList runat="server" ID="ddlWing" AutoPostBack="true" OnSelectedIndexChanged="ddlWing_OnSelectedIndexChanged" CssClass="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-3" style="display: none">
                                    <div class="form-group">
                                        <label>Department</label>
                                        <asp:DropDownList runat="server" ID="ddlDepartment" OnSelectedIndexChanged="ddlDepartment_OnSelectedIndexChanged" AutoPostBack="true" CssClass="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-3" style="display: none">
                                    <div class="form-group">
                                        <label>Section</label>
                                        <asp:DropDownList runat="server" ID="ddlSection" OnSelectedIndexChanged="ddlSection_OnSelectedIndexChanged" AutoPostBack="true" CssClass="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-3" style="display: none">
                                    <div class="form-group">
                                        <label>Sub Section</label>
                                        <asp:DropDownList runat="server" ID="ddlSubsection" AutoPostBack="true" CssClass="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-1">
                                    <div class="form-group">
                                        <br />
                                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-sm btn-info" OnClick="btnSearch_OnClick" Text="Search" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-row" runat="server" visible="False">


                                <div class="col-md-2" runat="server">
                                    <div class="form-group">
                                        <label>Financial Year &nbsp;<span style="color: #a52a2a">*</span></label>

                                        <asp:DropDownList ID="FinancialYearDropDownList" class="form-control form-control-sm" runat="server" OnSelectedIndexChanged="FinancialYearDropDownList_OnSelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="form-row" runat="server" visible="False">
                                <div class="col-md-9">
                                </div>

                                <div class="col-md-1" style="padding-top: 2px">
                                    <label style="font-weight: bold">Select Option:&nbsp;<span style="color: #a52a2a">*</span></label>
                                </div>
                                <div class="col-md-2 pull-right">
                                    <div class="form-group">

                                        <asp:RadioButtonList CssClass="chkChoiceHeader" runat="server" ID="radOp" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="radOp_OnSelectedIndexChanged">
                                            <asp:ListItem Text="Own" Value="Own" Selected="True" />
                                            <asp:ListItem Text="Supervisor" Value="Supervisor" />
                                        </asp:RadioButtonList>
                                    </div>

                                </div>
                            </div>

                            <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" ID="AppraisalOwn" CssClass="AddToListCssTable" OnPreRender="gv_DocumentUpload_PreRender">

                                <Columns>

                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            <asp:HiddenField ID="id_empId" runat="server" Value='<%#Eval("EmpInfoId") %>' />
                                            <asp:HiddenField ID="selfMaster" runat="server" Value='<%#Eval("AppraisalSelfMasterId") %>' />
                                            <asp:HiddenField ID="id_appraisalMaster" runat="server" Value='<%#Eval("AppraisalMasterId") %>' />

                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Employee ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_EmpMasterCode" runat="server" Text='<%#Eval("EmpMasterCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Employee Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_EmpName" runat="server" Text='<%#Eval("EmpName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date of Joining">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_DateOfJoin" runat="server" Text='<%#Eval("DateOfJoin") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Department">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_DepartmentName" runat="server" Text='<%#Eval("DepartmentName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Designation" runat="server" Text='<%#Eval("Designation") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Is Mid Year Status">
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="IsMidYearStatus" />
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Functional">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" OnClick="PartAOwn_OnClick" ID="PartAOwn" Text='<%#Eval("PartA") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Behavioral">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="PartBOwn" OnClick="PartBOwn_OnClick" Text='<%#Eval("PartB") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Training">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="training" OnClick="TrainingOwn_OnClick" Text='<%#Eval("training") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <%--          <asp:TemplateField HeaderText="View Comments">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server"  Id="ViewComments" CssClass="btn btn-success btn-sm"  OnClick="ViewComments_OnClick" >View Comments</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>






                                    <%--  <asp:TemplateField HeaderText="Comments" >
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_comments" runat="server" class="form-control form-control-sm" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick"  Text=" Submit " CssClass="btn btn-sm btn-info"/>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                    <%--     <asp:TemplateField HeaderText="View Details">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" CssClass="btn btn-info btn-sm"  ToolTip="Click To View Details" BorderStyle="None" OnClick="ShowPopup" ID="btnStatus">View Details</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>      --%>
                                </Columns>
                            </asp:GridView>

                            <asp:HiddenField ID="hfFinancialYearId" runat="server" />

                            <div id="gridCnnontainer1" style="overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;">

                                <asp:GridView runat="server" AutoGenerateColumns="False" OnRowDataBound="gv_AppraisalBoard_OnRowDataBound" Width="100%" ID="gv_AppraisalBoard" CssClass="AddToListCssTable" OnPreRender="gv_DocumentUpload_PreRender" DataKeyNames="AppraisalMasterAppLogId,CompleteStatus">

                                    <Columns>

                                        <asp:TemplateField HeaderText="SL#">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                                <asp:HiddenField ID="id_empId" runat="server" Value='<%#Eval("EmpInfoId") %>' />
                                                <asp:HiddenField ID="selfMaster" runat="server" Value='<%#Eval("AppraisalSelfMasterId") %>' />
                                                <asp:HiddenField ID="id_appraisalMaster" runat="server" Value='<%#Eval("AppraisalMasterId") %>' />
                                                <asp:HiddenField ID="FinancialYearId" runat="server" Value='<%#Eval("FinancialYearId") %>' />
                                                <asp:HiddenField ID="idReport" runat="server" Value='<%#Eval("ReportingEmpId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Employee ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_EmpMasterCode" runat="server" Text='<%#Eval("EmpMasterCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_EmpName" runat="server" Text='<%#Eval("EmpName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date of Joining">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_DateOfJoin" runat="server" Text='<%#Eval("DateOfJoin") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_DepartmentName" runat="server" Text='<%#Eval("DepartmentName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Designation" runat="server" Text='<%#Eval("Designation") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Disciplinary Action">
                                            <ItemTemplate>
                                                <asp:Label ID="lsdfdsbl_Designation" runat="server" Text='<%#Eval("DiciplinaryCount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Functional">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" OnClick="PartA_OnClick" ID="PartA" Text='<%#Eval("PartA") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Behavioral">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" OnClick="PartB_OnClick" ID="PartB" Text='<%#Eval("PartB") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Over All Status" Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" OnClick="PartC_OnClick" ID="PartC" Text='<%#Eval("PartC") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>





                                        <asp:TemplateField HeaderText="Training" Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" OnClick="Training_OnClick" ID="Training" Text='<%#Eval("training") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="View Comments" runat="server" Visible="False">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="ViewComments2" CssClass="btn btn-success btn-sm" OnClick="ViewComments2_OnClick">View Comments</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Opetation">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="btn_View" CssClass="MyBtnInfoCss" OnClick="btn_View_OnClick">Go to Approve</asp:LinkButton>
                                                <asp:Label ID="lblExpireStatus" runat="server" CssClass="alert-warning" ToolTip="Please Inform HR to Extend Deadline"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--      <asp:TemplateField HeaderText="Action" runat="server" Visible="False">
                                    <ItemTemplate>
                                        <asp:RadioButtonList ID="actionRadioButtonList" runat="server" RepeatDirection="Horizontal">
                                                    
                                            
                                                    
                                        </asp:RadioButtonList>
                                    </ItemTemplate>
                                </asp:TemplateField>



                                                    
                                <asp:TemplateField HeaderText="Comments" runat="server" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_comments" runat="server" class="form-control form-control-sm" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>       
                                                    

                                <asp:TemplateField HeaderText="Operation" runat="server" Visible="False">
                                    <ItemTemplate>
                                        <asp:Button runat="server" ID="btn_Save1" OnClick="btn_Save1_OnClick"  Text="Submit " CssClass="btn btn-sm btn-info"/>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                        <%--     <asp:TemplateField HeaderText="View Comments">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" CssClass="btn btn-info btn-xs"  ToolTip="Click To View Comments" BorderStyle="None" OnClick="ShowPopup1" ID="btnStatus">Status</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>   --%>




                                        <%--                                <asp:TemplateField HeaderText="Functional">
                                    <ItemTemplate>
                                        
                                        <asp:LinkButton ID="lb_Edit" runat="server">Edit</asp:LinkButton>|
                                        <asp:LinkButton ID="lb_delete"  runat="server">Remove</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>



                            </div>



                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <%--Functional--%>

                    <div>
                        <ajaxToolkit:ModalPopupExtender ID="mpe_1" runat="server" TargetControlID="hnd_Test" PopupControlID="pnl_1"
                            BackgroundCssClass="modalBackground">
                        </ajaxToolkit:ModalPopupExtender>

                        <asp:HiddenField ID="hnd_Test" runat="server"></asp:HiddenField>
                        <asp:Panel ID="pnl_1" runat="server" Style="display: none; overflow: scroll; padding: 10px" Height="680px" Width="90%" CssClass="modalPopup">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:UpdateProgress ID="UpdateProgress11" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                        <ProgressTemplate>
                                            <div class="divWaiting">
                                                <asp:Image ID="imgWait11" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>


                                    <div class="row">
                                        <div class="col-md-6" style="padding-left: 15px; padding-top: 12px;">
                                            <div class="text-left">
                                                <h1 class="title" style="font-size: 18px; padding-top: 0px;">
                                                    <img src="../Report_Pages/app.png" width="20px" />
                                                    Appraisal Functional Part</h1>
                                            </div>
                                        </div>

                                        <div class="col-md-6" style="padding-top: 15px; padding-right: 15px;">
                                            <asp:LinkButton ID="LinkButton1" OnClick="btnNo_Click" Style="font-size: 20px!important;" CssClass="btn btn-danger pull-right  pull-right" runat="server">Close
                                            </asp:LinkButton>


                                            <asp:Button runat="server" ID="btnFunctionalSave" OnClick="btnFunctionalSave_OnClick" Text="Save " Style="font-size: 20px!important;" CssClass="btn btn-info  pull-right  " />
                                        </div>
                                    </div>

                                    <hr />
                                    <div class="row">
                                        <style>
                                            .tblTHColorChang {
                                                background-color: #EDF2F5 !important;
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
                                            <h2 class="blue title-widget" style="color: #2196F3; text-shadow: 0 0 2px black;">Employee Details</h2>
                                            <table class="table table-bordered table-striped">
                                                <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Employee ID</td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblEmpId"></asp:Label></td>


                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Supervisor</td>
                                                    <td>
                                                        <asp:Label ID="ReportingLabel" runat="server"></asp:Label></td>
                                                </tr>

                                                <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Employee Name</td>
                                                    <td>
                                                        <asp:Label ID="empName" runat="server"></asp:Label></td>



                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Department</td>
                                                    <td>
                                                        <asp:Label ID="deptNameLabel" runat="server"></asp:Label></td>
                                                </tr>

                                                <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Designation</td>
                                                    <td>
                                                        <asp:Label ID="desigNameLabel" runat="server"></asp:Label></td>

                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Office</td>
                                                    <td>
                                                        <asp:Label ID="LocationLabel" runat="server"></asp:Label></td>

                                                </tr>






                                                <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Date Of Joining</td>
                                                    <td>
                                                        <asp:Label ID="joiningDateLabel" runat="server"></asp:Label></td>
                                                    <td style="width: 20%; padding: 10px;" class="tblTHColorChang">Place</td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblPlace"></asp:Label></td>
                                                </tr>





                                            </table>
                                        </div>


                                        <div class="col-md-12">
                                            <div id="gridContainer1" style="height: auto; overflow: auto; width: auto;">

                                                <asp:GridView runat="server" ShowFooter="true" AutoGenerateColumns="False" Width="100%" ID="gv_AppraisalFunc" CssClass="AddToListCssTable" OnPreRender="gv_DocumentUpload_PreRender" DataKeyNames="MidYearStatus,AppraisalSelfFucAreaId">

                                                    <Columns>

                                                        <asp:TemplateField HeaderText="SL#">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Key Performance Indicator">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="txtKpi" Text='<%#Eval("KpiInfo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Weight (Number)">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="txtWeight" Text='<%#Eval("KpiWeight") %>'></asp:Label>

                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Label CssClass="form-control  form-control-sm" ID="lblTotalWeight" runat="server" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Weight (%) ">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="txtWeightPer" Text='<%#Eval("KpiWeightPer") %>'></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Target (Number)">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="txtTarget" Text='<%#Eval("Target") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Target (%)" runat="server" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="txtTargetPer" AutoPostBack="True" ReadOnly="True" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("TargetPer") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Dead Line">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="txtDeadLine" Text='<%#Eval("Deadline") %>'></asp:Label>
                                                                <%--         <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                Format="dd-MMM-yyyy" CssClass="custom"
                                                TargetControlID="txtDeadLine" />--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Mid Year Status">
                                                            <ItemTemplate>
                                                                <%--<asp:TextBox runat="server" ID="txtMidStatus" CssClass="form-control  form-control-sm" Text='<%#Eval("MidYearStatus") %>'></asp:TextBox>--%>
                                                                <asp:DropDownList runat="server" ID="txtMidStatus" CssClass="form-control  form-control-sm">
                                                                    <asp:ListItem Value="0">Select One..</asp:ListItem>
                                                                    <asp:ListItem Value="Not Started">Not Started</asp:ListItem>
                                                                    <asp:ListItem Value="Ongoing">On Track</asp:ListItem>
                                                                    <asp:ListItem Value="Completed">Completed</asp:ListItem>
                                                                    <asp:ListItem Value="Not Completed">Behind</asp:ListItem>
                                                                    <asp:ListItem Value="Not Relevant">Not Relevant</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Supervisor-Mark" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="txtResult" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("ResultYearEnd") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Self-Mark">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="txtselfMark" AutoPostBack="True" OnTextChanged="txtselfMark_OnTextChanged" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("SelfMark") %>'></asp:TextBox>

                                                                <%-- <asp:Label runat="server" ID="txtselfMark" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("SelfMark") %>'></asp:Label>--%>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTotalMark" CssClass="form-control  form-control-sm" runat="server" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Comment">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ReadOnly="True" ID="txtComment" CssClass="form-control" TextMode="MultiLine" Rows="2" Text='<%#Eval("CommentsFunc") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Result" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="txtMark" AutoPostBack="True" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("SupervisorMark") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTotalMarkSupervisor" CssClass="orm-control  form-control-sm " runat="server" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>








                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>




                                    <hr />


                                    <asp:HiddenField runat="server" ID="hfconfirmstatus" />
                                    <asp:HiddenField runat="server" ID="id_mastetID" />
                                    <asp:HiddenField runat="server" ID="id_selfID" />
                                    <asp:HiddenField runat="server" ID="hfempCode" />
                                    <asp:HiddenField runat="server" ID="id_Empid" />


                                    <div class="col-md-12">



                                        <div class="form-row">
                                            <div class="col-md-5"></div>
                                            <div class="col-md-5">

                                                <%--   <asp:Button ID="btnFunctionalCancel" Text="Close" OnClick="btnFunctionalCancel_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />--%>
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


                    <%--Behavioral--%>



                    <div>
                        <ajaxToolkit:ModalPopupExtender ID="MPBehavioral" runat="server" TargetControlID="Behavioral_Test" PopupControlID="pnl_Behavioral"
                            BackgroundCssClass="modalBackground">
                        </ajaxToolkit:ModalPopupExtender>

                        <asp:HiddenField ID="Behavioral_Test" runat="server"></asp:HiddenField>
                        <asp:Panel ID="pnl_Behavioral" runat="server" Style="display: none; overflow: scroll; padding: 10px" Height="680px" Width="90%" CssClass="modalPopup">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>

                                    <asp:UpdateProgress ID="UpdateProgress12" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                        <ProgressTemplate>
                                            <div class="divWaiting">
                                                <asp:Image ID="imgWait2" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>


                                    <div class="row">
                                        <div class="col-md-6" style="padding-left: 15px; padding-top: 12px;">
                                            <div class="text-left">
                                                <h1 class="title" style="font-size: 18px; padding-top: 0px;">
                                                    <img src="../Report_Pages/app.png" width="20px" />
                                                    Appraisal Behavioral Part</h1>
                                            </div>
                                        </div>

                                        <div class="col-md-6" style="padding-top: 15px; padding-right: 15px;">
                                            <asp:LinkButton ID="btnBehavioralClose" Style="font-size: 20px!important;" OnClick="btnBehavioralClose_Click" CssClass="btn btn-xs btn-danger pull-right  pull-right" runat="server">
                                                Close
                                                
                                                
                                                 <asp:Button runat="server" ID="btnBehave" OnClick="btnBehave_OnClick" Text="Save " Style="font-size: 20px!important;" CssClass="btn btn-info   pull-right" />
                                            </asp:LinkButton>
                                        </div>
                                    </div>

                                    <hr />
                                    <div class="row">
                                        <style>
                                            .tblTHColorChang {
                                                background-color: #EDF2F5 !important;
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
                                            <h2 class="blue title-widget" style="color: #2196F3; text-shadow: 0 0 2px black;">Employee Details</h2>
                                            <table class="table table-bordered table-striped">
                                                <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Employee ID</td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblEmpIdBehavioral"></asp:Label></td>


                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Supervisor</td>
                                                    <td>
                                                        <asp:Label ID="ReportingLabelBehavioral" runat="server"></asp:Label></td>
                                                </tr>

                                                <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Employee Name</td>
                                                    <td>
                                                        <asp:Label ID="empNameBehavioral" runat="server"></asp:Label></td>



                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Department</td>
                                                    <td>
                                                        <asp:Label ID="deptNameLabelBehavioral" runat="server"></asp:Label></td>
                                                </tr>

                                                <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Designation</td>
                                                    <td>
                                                        <asp:Label ID="desigNameLabelBehavioral" runat="server"></asp:Label></td>

                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Office</td>
                                                    <td>
                                                        <asp:Label ID="LocationLabelBehavioral" runat="server"></asp:Label></td>

                                                </tr>






                                                <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Date Of Joining</td>
                                                    <td>
                                                        <asp:Label ID="joiningDateLabelBehavioral" runat="server"></asp:Label></td>
                                                    <td style="width: 20%; padding: 10px;" class="tblTHColorChang">Place</td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblPlaceBehavioral"></asp:Label></td>
                                                </tr>





                                            </table>
                                        </div>


                                        <div class="col-md-12">
                                            <div id="gridContainerbeh" style="height: auto; overflow: auto; width: auto;">

                                                <asp:GridView runat="server" ShowFooter="true" AutoGenerateColumns="False" Width="100%" ID="gv_AppraisalPartB" CssClass="AddToListCssTable" OnPreRender="gv_DocumentUpload_PreRender">

                                                    <Columns>

                                                        <asp:TemplateField HeaderText="SL#">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Competencies / Skills">
                                                            <ItemTemplate>
                                                                <asp:TextBox CssClass="form-control" TextMode="MultiLine" Rows="2" runat="server" ID="SkillInfo" Text='<%#Eval("SkillInfo") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Supporting Example">
                                                            <ItemTemplate>
                                                                <asp:TextBox CssClass="form-control" TextMode="MultiLine" Rows="2" runat="server" ID="SupportingEmp" Text='<%#Eval("SupportingEmp") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Weight (Number)">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="Weight" Text='<%#Eval("Score") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />

                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Weight (Number)">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="SetScore" Text='<%#Eval("SetScore") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Label ID="ddllblTotalSetScore" CssClass="form-control  form-control-sm " runat="server" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Score">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="SelfScore" AutoPostBack="True" OnTextChanged="SelfScore_OnTextChanged" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("SelfScore") %>'></asp:TextBox>

                                                                                                                                    <cc1:FilteredTextBoxExtender ID="Fil2ssss12sEasdasdassxtender2" runat="server" Enabled="True"
TargetControlID="SelfScore" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>

                                                            </ItemTemplate>

                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTotalMarkSelf" CssClass="form-control  form-control-sm " runat="server" />
                                                            </FooterTemplate>


                                                        </asp:TemplateField>




                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>




                                    <hr />

                                    <div class="col-md-12">



                                        <div class="form-row">
                                            <div class="col-md-5"></div>
                                            <div class="col-md-5">
                                            </div>
                                        </div>
                                    </div>
                                    <%--   <asp:Button ID="btnFunctionalCancel" Text="Close" OnClick="btnFunctionalCancel_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />--%>
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


                    <%--Training--%>




                    <div>
                        <ajaxToolkit:ModalPopupExtender ID="MPTraining" runat="server" TargetControlID="Training_Test" PopupControlID="pnl_Training"
                            BackgroundCssClass="modalBackground">
                        </ajaxToolkit:ModalPopupExtender>

                        <asp:HiddenField ID="Training_Test" runat="server"></asp:HiddenField>
                        <asp:Panel ID="pnl_Training" runat="server" Style="display: none; overflow: scroll; padding: 10px" Height="680px" Width="90%" CssClass="modalPopup">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                        <ProgressTemplate>
                                            <div class="divWaiting">
                                                <asp:Image ID="img22Wait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>

                                    <div class="row">
                                        <div class="col-md-6" style="padding-left: 15px; padding-top: 12px;">
                                            <div class="text-left">
                                                <h1 class="title" style="font-size: 18px; padding-top: 0px;">
                                                    <img src="../Report_Pages/app.png" width="20px" />
                                                    Appraisal Training Part</h1>
                                            </div>
                                        </div>

                                        <div class="col-md-6" style="padding-top: 15px; padding-right: 15px;">
                                            <asp:LinkButton ID="btnTrainingClose" OnClick="btnTrainingClose_Click" CssClass="btn btn-danger pull-right  pull-right" runat="server" Style="font-size: 20px!important;">Close
                                            </asp:LinkButton>


                                            <asp:Button runat="server" ID="btnTrainSave" OnClick="btnTrainSave_OnClick" Text="Save " Style="font-size: 20px!important;" CssClass="btn btn-info   pull-right" />
                                        </div>
                                    </div>

                                    <hr />
                                    <div class="row">
                                        <style>
                                            .tblTHColorChang {
                                                background-color: #EDF2F5 !important;
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
                                            <h2 class="blue title-widget" style="color: #2196F3; text-shadow: 0 0 2px black;">Employee Details</h2>
                                            <table class="table table-bordered table-striped">
                                                <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Employee ID</td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblEmpIdTraining"></asp:Label></td>


                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Supervisor</td>
                                                    <td>
                                                        <asp:Label ID="ReportingLabelTraining" runat="server"></asp:Label></td>
                                                </tr>

                                                <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Employee Name</td>
                                                    <td>
                                                        <asp:Label ID="empNameTraining" runat="server"></asp:Label></td>



                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Department</td>
                                                    <td>
                                                        <asp:Label ID="deptNameLabelTraining" runat="server"></asp:Label></td>
                                                </tr>

                                                <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Designation</td>
                                                    <td>
                                                        <asp:Label ID="desigNameLabelTraining" runat="server"></asp:Label></td>

                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Office</td>
                                                    <td>
                                                        <asp:Label ID="LocationLabelTraining" runat="server"></asp:Label></td>

                                                </tr>






                                                <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Date Of Joining</td>
                                                    <td>
                                                        <asp:Label ID="joiningDateLabelTraining" runat="server"></asp:Label></td>
                                                    <td style="width: 20%; padding: 10px;" class="tblTHColorChang">Place</td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblPlaceTraining"></asp:Label></td>
                                                </tr>





                                            </table>
                                        </div>


                                        <div class="col-md-12">
                                            <div id="gridContainerTraining" style="height: auto; overflow: auto; width: auto;">

                                                <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gv_AppraisalTraining" CssClass="AddToListCssTable" OnPreRender="gv_DocumentUpload_PreRender">

                                                    <Columns>

                                                        <asp:TemplateField HeaderText="SL#">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Training Needs">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="TrainingNeeds" CssClass="form-control" Rows="2" TextMode="MultiLine" Text='<%#Eval("TrainingNeeds") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Quater">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="QuaterDropDownList1" AutoPostBack="true" runat="server" CssClass="form-control form-control-sm">
                                                                    <asp:ListItem Text="1st Quarter" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="2nd Quarter" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="3rd Quarter" Value="3"></asp:ListItem>
                                                                    <asp:ListItem Text="4th Quarter" Value="4"></asp:ListItem>

                                                                </asp:DropDownList>
                                                                <%--<asp:TextBox runat="server" ID="TrainingStart"  CssClass="form-control  form-control-sm"  Text='<%#Eval("TrainingStart") %>' ></asp:TextBox>--%>
                                                                <%--      <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                        TargetControlID="TrainingStart" />--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <%--    <asp:TemplateField HeaderText="End" runat="server" Visible="False">
                                    <ItemTemplate>
                                       <asp:TextBox runat="server" ID="TrainingEnd" CssClass="form-control  form-control-sm"   Text='<%#Eval("TrainingEnd") %>' ></asp:TextBox>
                                     <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                        TargetControlID="TrainingEnd" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                                        <asp:TemplateField HeaderText="Add">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btn_Add" OnClick="btn_Add_OnClick" CssClass="btn btn-info btn-sm" runat="server"><i class="fa fa-plus" aria-hidden="true"></i></asp:LinkButton>
                                                            </ItemTemplate>


                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete">
                                                            <ItemTemplate>


                                                                <asp:LinkButton ID="lb_Remove" CssClass="btn btn-danger btn-sm" OnClick="lb_Remove_OnClick" runat="server"><i class="fa fa-trash" aria-hidden="true"></i></asp:LinkButton>
                                                            </ItemTemplate>


                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>




                                    <hr />

                                    <div class="col-md-12">



                                        <div class="form-row">
                                            <div class="col-md-5"></div>
                                            <div class="col-md-5">
                                            </div>
                                        </div>
                                    </div>
                                    <%--   <asp:Button ID="btnFunctionalCancel" Text="Close" OnClick="btnFunctionalCancel_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />--%>
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



                    <%--Comments--%>


                    <div>
                        <ajaxToolkit:ModalPopupExtender ID="mpComm" runat="server" TargetControlID="Comm_Test" PopupControlID="pnl_Comm"
                            BackgroundCssClass="modalBackground">
                        </ajaxToolkit:ModalPopupExtender>

                        <asp:HiddenField ID="Comm_Test" runat="server"></asp:HiddenField>
                        <asp:Panel ID="pnl_Comm" runat="server" Style="display: none; overflow: scroll; padding: 10px" Height="680px" Width="90%" CssClass="modalPopup">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <asp:UpdateProgress ID="UpdateProgress4" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                        <ProgressTemplate>
                                            <div class="divWaiting">
                                                <asp:Image ID="jasgdajh" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>

                                    <div class="row">
                                        <div class="col-md-6" style="padding-left: 15px; padding-top: 12px;">
                                            <div class="text-left">
                                                <h1 class="title" style="font-size: 18px; padding-top: 0px;">
                                                    <img src="../Report_Pages/app.png" width="20px" />
                                                    Appraisal Comments</h1>
                                            </div>
                                        </div>

                                        <div class="col-md-6" style="padding-top: 15px; padding-right: 15px;">
                                            <asp:LinkButton ID="LinkButton2" Style="font-size: 20px!important;" OnClick="btncommClose_Click" CssClass="btn btn-xs btn-danger pull-right  pull-right" runat="server">Close
                                            </asp:LinkButton>
                                        </div>
                                    </div>

                                    <hr />
                                    <div class="row">
                                        <style>
                                            .tblTHColorChang {
                                                background-color: #EDF2F5 !important;
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
                                        </style>

                                    </div>




                                    <hr />



                                    <%--<asp:Button runat="server" ID="Button1" OnClick="btnTrainSave_OnClick" Text="Submit " CssClass="btn btn-sm btn-info" />--%>
                                    <%--   <asp:Button ID="btnFunctionalCancel" Text="Close" OnClick="btnFunctionalCancel_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />--%>
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





                    <%--Functional--%>

                    <div>
                        <ajaxToolkit:ModalPopupExtender ID="mpFunctionalSup" runat="server" TargetControlID="FunctionalSup_Test" PopupControlID="pnlFunctionalSup"
                            BackgroundCssClass="modalBackground">
                        </ajaxToolkit:ModalPopupExtender>

                        <asp:HiddenField ID="FunctionalSup_Test" runat="server"></asp:HiddenField>
                        <asp:Panel ID="pnlFunctionalSup" runat="server" Style="display: none; overflow: scroll; padding: 10px" Height="680px" Width="90%" CssClass="modalPopup">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <asp:UpdateProgress ID="UpdateProgress5" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                        <ProgressTemplate>
                                            <div class="divWaiting">
                                                <asp:Image ID="imgW21421ait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>

                                    <div class="row">
                                        <div class="col-md-6" style="padding-left: 15px; padding-top: 12px;">
                                            <div class="text-left">
                                                <h1 class="title" style="font-size: 18px; padding-top: 0px;">
                                                    <img src="../Report_Pages/app.png" width="20px" />
                                                    Appraisal Functional Part</h1>
                                            </div>
                                        </div>

                                        <div class="col-md-6" style="padding-top: 15px; padding-right: 15px;">
                                            <asp:LinkButton ID="btnFunctionalSupClose" Style="font-size: 20px!important;" OnClick="btnFunctionalSupClose_Click" CssClass="btn btn-danger pull-right  pull-right" runat="server">Close
                                            </asp:LinkButton>


                                            <asp:Button runat="server" ID="btnAppraisalFuncSUPSave" OnClick="btnAppraisalFuncSUPSave_OnClick" Text="Save " Style="font-size: 20px!important;" CssClass="btn btn-info   pull-right" />
                                        </div>
                                    </div>

                                    <hr />
                                    <div class="row">
                                        <style>
                                            .tblTHColorChang {
                                                background-color: #EDF2F5 !important;
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
                                            <h2 class="blue title-widget" style="color: #2196F3; text-shadow: 0 0 2px black;">Employee Details</h2>
                                            <table class="table table-bordered table-striped">
                                                <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Employee ID</td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblEmpIdFuncSUP"></asp:Label></td>


                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Supervisor</td>
                                                    <td>
                                                        <asp:Label ID="ReportingLabelFuncSUP" runat="server"></asp:Label></td>
                                                </tr>

                                                <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Employee Name</td>
                                                    <td>
                                                        <asp:Label ID="empNameFuncSUP" runat="server"></asp:Label></td>



                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Department</td>
                                                    <td>
                                                        <asp:Label ID="deptNameLabelFuncSUP" runat="server"></asp:Label></td>
                                                </tr>

                                                <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Designation</td>
                                                    <td>
                                                        <asp:Label ID="desigNameLabelFuncSUP" runat="server"></asp:Label></td>

                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Office</td>
                                                    <td>
                                                        <asp:Label ID="LocationLabelFuncSUP" runat="server"></asp:Label></td>

                                                </tr>






                                                <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Date Of Joining</td>
                                                    <td>
                                                        <asp:Label ID="joiningDateLabelFuncSUP" runat="server"></asp:Label></td>
                                                    <td style="width: 20%; padding: 10px;" class="tblTHColorChang">Place</td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblPlaceFuncSUP"></asp:Label></td>
                                                </tr>





                                            </table>
                                        </div>


                                        <div class="col-md-12">
                                            <p class="alert alert-success" style="display:none">
                                                Note: "If you want to add/remove the KPI points then please untick the following active ones and then add new points"
                                            </p>
                                            <div id="gridContainer1SUP" style="height: auto; overflow: auto; width: auto;">

                                                <asp:GridView runat="server" ShowFooter="true" AutoGenerateColumns="False" Width="100%" ID="gv_AppraisalFuncSUP" CssClass="AddToListCssTable" OnPreRender="gv_DocumentUpload_PreRender" DataKeyNames="MidYearStatus,AppraisalSelfFucAreaId,Dimension">

                                                    <Columns>

                                                        <asp:TemplateField HeaderText="SL#">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="BSC/OKR Dimension">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Width="100px" ID="txtDimensionStr" Text='<%#Eval("DimensionStr") %>'></asp:Label>
                                                                <asp:HiddenField runat="server" ID="hdnDimensionStr" Value='<%#Eval("DimensionStrHdn") %>'></asp:HiddenField>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Objective Goal">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Width="120px" ID="txtObjectiveGoal" Text='<%#Eval("ObjectiveGoal") %>'></asp:Label>
                                                                <asp:HiddenField runat="server" ID="hdnObjectiveGoal" Value='<%#Eval("ObjectiveGoalHdn") %>'></asp:HiddenField>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="KPI/Measure">
                                                            <ItemTemplate>
                                                                <asp:Label Width="220px" runat="server" ID="txtKPIMeasure" Text='<%#Eval("KPIMeasure") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Weightage  %">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="txtWeight" AutoPostBack="True" ReadOnly="True" Width="80px" OnTextChanged="txtWeightSUP_OnTextChanged" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("KpiWeight") %>'></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                                    TargetControlID="txtWeight" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTotalWeight" CssClass="form-control  form-control-sm " runat="server" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Maximum Mark (20%)Increase">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Width="80px" ID="lblAutoCalculation" Text='<%#Eval("AutoCalculation") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Initiatives">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="txtInitiatives" Width="220px" Text='<%#Eval("Initiatives") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Weightage (%) " Visible="false">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="txtWeightPer" ReadOnly="True" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("KpiWeightPer") %>'></asp:TextBox>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Target (Number)" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="txtTarget" ReadOnly="True" CssClass="form-control  form-control-sm" AutoPostBack="True" OnTextChanged="txtTarget_OnTextChanged" TextMode="Number" Text='<%#Eval("Target") %>'></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2122" runat="server" Enabled="True"
                                                                    TargetControlID="txtTarget" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Label ID="lbltarget" CssClass="form-control  form-control-sm " runat="server" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Target (%)" runat="server" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="txtTargetPer" ReadOnly="True" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("TargetPer") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Deadline">
                                                            <ItemTemplate>
                                                                <asp:TextBox Width="100px" runat="server" ID="txtDeadLine" CssClass="form-control  form-control-sm" Enabled="False" Text='<%#Eval("Deadline") %>'></asp:TextBox>
                                                                <asp:CalendarExtender ID="CalenduuuarExtender1" runat="server" PopupPosition="TopLeft"
                                                                    Format="dd-MMM-yyyy" PopupButtonID="txtDeadLine" CssClass="MyCalendar"
                                                                    TargetControlID="txtDeadLine" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Mid-Year Status">
                                                            <ItemTemplate>

                                                                <asp:DropDownList Enabled="False" Width="120px" runat="server" ID="txtMidStatus" CssClass="form-control  form-control-sm">
                                                                    <asp:ListItem Value="0">Select One..</asp:ListItem>
                                                                    <asp:ListItem Value="Not Started">Not Started</asp:ListItem>
                                                                    <asp:ListItem Value="Ongoing">On Track</asp:ListItem>
                                                                    <asp:ListItem Value="Completed">Completed</asp:ListItem>
                                                                    <asp:ListItem Value="Not Completed">Behind</asp:ListItem>
                                                                    <asp:ListItem Value="Not Relevant">Not Relevant</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <%--   <asp:TextBox runat="server" ReadOnly="True" ID="txtMidStatus" CssClass="form-control  form-control-sm" Text='<%#Eval("MidYearStatus") %>'></asp:TextBox>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Results (Year End)" Visible="false" runat="server">
                                                            <ItemTemplate>
                                                                <asp:TextBox Width="100px" runat="server" ID="txtResult" CssClass="form-control  form-control-sm" Text='<%#Eval("ResultYearEnd") %>'></asp:TextBox>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Self Assesment">
                                                            <ItemTemplate>
                                                                <asp:TextBox CssClass="form-control resizable-textbox" TextMode="MultiLine" Rows="2" runat="server" ID="txtCommentsFunc" ReadOnly="True" Text='<%#Eval("CommentsFunc") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Supervisor Assesment">
                                                            <ItemTemplate>
                                                                <asp:TextBox CssClass="form-control resizable-textbox" TextMode="MultiLine" Rows="2" runat="server" ID="txtSupervisorComment" Text='<%#Eval("SupervisorComments") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Mid-Year<br/>Self-Mark">
                                                            <ItemTemplate>
                                                                <asp:TextBox Width="100px" runat="server" ReadOnly="True" ID="txtselfMark" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("SelfMark") %>'></asp:TextBox>

                                                                <br />
                                                                <asp:Label runat="server" Font-Bold="true" ForeColor="#cc6600" ID="lblSelfPer"></asp:Label>

                                                                <%-- <asp:Label runat="server" ID="txtselfMark" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("SelfMark") %>'></asp:Label>--%>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblselfMark" CssClass="form-control  form-control-sm " runat="server" />
                                                                <br />
                                                                <asp:Label runat="server" Font-Bold="true" ForeColor="#cc6600" Style="font-size: 16px;" ID="lblTotalSelfPer"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="lblTotalSelfPerT" Font-Bold="true" ForeColor="#cc0000" Style="font-size: 16px;" runat="server" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Mid-Year<br/>Supervisor<br/>Mark">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" Width="100px" ID="txtMark" AutoPostBack="True" OnTextChanged="SupervisorMark_OnTextChanged" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("SupervisorMark") %>'></asp:TextBox>


                                                                <cc1:FilteredTextBoxExtender ID="FilteresfsafsadTextBoxExtenderqty" runat="server"
                                                                    Enabled="True" TargetControlID="txtMark" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                                <br />
                                                                <asp:Label runat="server" Font-Bold="true" ForeColor="#cc6600" ID="lblAutoCalculationPer" Text='<%#Eval("AutoCalculationPerSup") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTotalMark" CssClass="form-control  form-control-sm " runat="server" />

                                                                <br />
                                                                <asp:Label runat="server" Style="font-size: 16px;" Font-Bold="true" ForeColor="#cc6600" ID="lblSupervisorMarkPer"></asp:Label>
                                                                <br />

                                                                <asp:Label ID="lblSupervisorMarkPerT" Font-Bold="true" ForeColor="#cc0000" Style="font-size: 16px;" runat="server" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Is Active" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="isActiveCheckBox" runat="server" Checked="true" EnableTheming="True" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Operation" Visible="false">
                                                            <ItemTemplate>

                                                                <asp:LinkButton Visible="False" ID="btnFunction" CssClass="btn btn-info btn-sm" OnClick="btnFunction_OnClick" runat="server"><i class="fa fa-plus" aria-hidden="true"></i>
                                                                </asp:LinkButton>

                                                            </ItemTemplate>


                                                        </asp:TemplateField>


                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>




                                    <hr />


                                    <div class="col-md-12">



                                        <div class="form-row">
                                            <div class="col-md-5"></div>
                                            <div class="col-md-5">

                                                <%--   <asp:Button ID="btnFunctionalCancel" Text="Close" OnClick="btnFunctionalCancel_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />--%>
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






                    <%--BSup--%>

                    <div>
                        <ajaxToolkit:ModalPopupExtender ID="MPBSup" runat="server" TargetControlID="BSup_Test" PopupControlID="pnlBSup"
                            BackgroundCssClass="modalBackground">
                        </ajaxToolkit:ModalPopupExtender>

                        <asp:HiddenField ID="BSup_Test" runat="server"></asp:HiddenField>
                        <asp:Panel ID="pnlBSup" runat="server" Style="display: none; overflow: scroll; padding: 10px" Height="680px" Width="90%" CssClass="modalPopup">
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                    <asp:UpdateProgress ID="UpdateProgress6" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                        <ProgressTemplate>
                                            <div class="divWaiting">
                                                <asp:Image ID="i2323mgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>

                                    <div class="row">
                                        <div class="col-md-6" style="padding-left: 15px; padding-top: 12px;">
                                            <div class="text-left">
                                                <h1 class="title" style="font-size: 18px; padding-top: 0px;">
                                                    <img src="../Report_Pages/app.png" width="20px" />
                                                    Appraisal Behavioral Part
                                                </h1>
                                            </div>
                                        </div>

                                        <div class="col-md-6" style="padding-top: 15px; padding-right: 15px;">
                                            <asp:LinkButton ID="btnMPBSup" OnClick="btnMPBSupClose_Click" Style="font-size: 20px!important;" CssClass="btn btn-danger pull-right  pull-right" runat="server">Close
                                            </asp:LinkButton>


                                            <asp:Button runat="server" ID="btnPartBSUPSave" OnClick="btnPartBSUPSave_OnClick" Text="Save " Style="font-size: 20px!important;" CssClass="btn btn-info   pull-right" />
                                        </div>
                                    </div>

                                    <hr />
                                    <div class="row">
                                        <style>
                                            .tblTHColorChang {
                                                background-color: #EDF2F5 !important;
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
                                            <h2 class="blue title-widget" style="color: #2196F3; text-shadow: 0 0 2px black;">Employee Details</h2>
                                            <table class="table table-bordered table-striped">
                                                <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Employee ID</td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblEmpIdBSup"></asp:Label></td>


                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Supervisor</td>
                                                    <td>
                                                        <asp:Label ID="ReportingLabelBSup" runat="server"></asp:Label></td>
                                                </tr>

                                                <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Employee Name</td>
                                                    <td>
                                                        <asp:Label ID="empNameBSup" runat="server"></asp:Label></td>



                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Department</td>
                                                    <td>
                                                        <asp:Label ID="deptNameLabelBSup" runat="server"></asp:Label></td>
                                                </tr>

                                                <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Designation</td>
                                                    <td>
                                                        <asp:Label ID="desigNameLabelBSup" runat="server"></asp:Label></td>

                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Office</td>
                                                    <td>
                                                        <asp:Label ID="LocationLabelBSup" runat="server"></asp:Label></td>

                                                </tr>






                                                <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Date Of Joining</td>
                                                    <td>
                                                        <asp:Label ID="joiningDateLabelBSup" runat="server"></asp:Label></td>
                                                    <td style="width: 20%; padding: 10px;" class="tblTHColorChang">Place</td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblPlaceBSup"></asp:Label></td>
                                                </tr>





                                            </table>
                                        </div>


                                        <div class="col-md-12">
                                            <div id="gridContainerBSUP" style="height: auto; overflow: auto; width: auto;">

                                                <asp:GridView runat="server" ShowFooter="true" AutoGenerateColumns="False" Width="100%" ID="gv_AppraisalPartBSUP" DataKeyNames="BSCAppraisalSelfBehaveId"  CssClass="AddToListCssTable" OnPreRender="gv_DocumentUpload_PreRender">

                                                    <Columns>

                                                        <asp:TemplateField HeaderText="SL#">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Competencies / Skills">
                                                            <ItemTemplate>
                                                                <asp:TextBox CssClass="form-control" TextMode="MultiLine" Rows="2" runat="server" ID="SkillInfo" Text='<%#Eval("SkillInfo") %>'></asp:TextBox>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Supporting Example">
                                                            <ItemTemplate>
                                                                <asp:TextBox CssClass="form-control" TextMode="MultiLine" Rows="2" runat="server" ID="SupportingEmp" Text='<%#Eval("SupportingEmp") %>'></asp:TextBox>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Weight (Number)">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="Weight" AutoPostBack="True" ReadOnly="True" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("Score") %>'></asp:TextBox>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderText="Weight (Number)">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="SetScore" Text='<%#Eval("SetScore") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Label ID="ddllblTotalSetScore" CssClass="form-control  form-control-sm " runat="server" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Mid-Year<br/>Self Score">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ReadOnly="True" ID="SelfScore" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("SelfScore") %>'></asp:TextBox>

                                                                                                                                    <cc1:FilteredTextBoxExtender ID="Fil2sasdsasss12sadasdsEsxtender2" runat="server" Enabled="True"
TargetControlID="SelfScore" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>

                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblselfscrore" CssClass="form-control  form-control-sm " runat="server" />
                                                            </FooterTemplate>



                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Mid-Year<br/>Supervisor<br/>Score">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" AutoPostBack="True" OnTextChanged="SupervisorScore_OnTextChanged" ID="SupervisorScore" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("SupervisorScore") %>'></asp:TextBox>


                                                                                                                                                                                                    <cc1:FilteredTextBoxExtender ID="Fil21SupervisorScore001" runat="server" Enabled="True"
TargetControlID="SupervisorScore" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                            </ItemTemplate>

                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTotalMark" CssClass="form-control  form-control-sm " runat="server" />
                                                            </FooterTemplate>


                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Comments" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:TextBox CssClass="form-control" TextMode="MultiLine" Rows="2" runat="server" ID="txtComments" Text='<%#Eval("Comments") %>'></asp:TextBox>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>



                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>




                                    <hr />



                                    <div class="col-md-12">



                                        <div class="form-row">
                                            <div class="col-md-5"></div>
                                            <div class="col-md-5">
                                            </div>
                                        </div>
                                    </div>
                                    <%--   <asp:Button ID="btnFunctionalCancel" Text="Close" OnClick="btnFunctionalCancel_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />--%>
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


                    <%--fINAL Status--%>

                    <div>
                        <ajaxToolkit:ModalPopupExtender ID="MPFinalStatus" runat="server" TargetControlID="FinalStatus_Test" PopupControlID="PNLFinalStatus_Test"
                            BackgroundCssClass="modalBackground">
                        </ajaxToolkit:ModalPopupExtender>

                        <asp:HiddenField ID="FinalStatus_Test" runat="server"></asp:HiddenField>
                        <asp:Panel ID="PNLFinalStatus_Test" runat="server" Style="display: none; overflow: scroll; padding: 10px" Height="680px" Width="90%" CssClass="modalPopup">
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                    <asp:UpdateProgress ID="UpdateProgress7" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                        <ProgressTemplate>
                                            <div class="divWaiting">
                                                <asp:Image ID="gfd2323imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>

                                    <div class="row">
                                        <div class="col-md-6" style="padding-left: 15px; padding-top: 12px;">
                                            <div class="text-left">
                                                <h1 class="title" style="font-size: 18px; padding-top: 0px;">
                                                    <img src="../Report_Pages/app.png" width="20px" />
                                                    Appraisal Final Status
                                                </h1>
                                            </div>
                                        </div>

                                        <div class="col-md-6" style="padding-top: 15px; padding-right: 15px;">
                                            <asp:LinkButton ID="btnFinalStatusClose" OnClick="btnFinalStatusClose_Click" Style="font-size: 20px!important;" CssClass="btn  btn-danger pull-right  pull-right" runat="server">Close
                                            </asp:LinkButton>

                                            <asp:Button runat="server" ID="btnFinalStatusSave" OnClick="btnFinalStatusSave_OnClick" Text="Save " Style="font-size: 20px!important;" CssClass="btn btn-info   pull-right" />
                                        </div>
                                    </div>

                                    <hr />
                                    <div class="row">
                                        <style>
                                            .tblTHColorChang {
                                                background-color: #EDF2F5 !important;
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
                                            <h2 class="blue title-widget" style="color: #2196F3; text-shadow: 0 0 2px black;">Employee Details</h2>
                                            <table class="table table-bordered table-striped">
                                                <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Employee ID</td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblEmpIdFinalStatus"></asp:Label></td>


                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Supervisor</td>
                                                    <td>
                                                        <asp:Label ID="ReportingLabelFinalStatus" runat="server"></asp:Label></td>
                                                </tr>

                                                <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Employee Name</td>
                                                    <td>
                                                        <asp:Label ID="empNameFinalStatus" runat="server"></asp:Label></td>



                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Department</td>
                                                    <td>
                                                        <asp:Label ID="deptNameLabelFinalStatus" runat="server"></asp:Label></td>
                                                </tr>

                                                <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Designation</td>
                                                    <td>
                                                        <asp:Label ID="desigNameLabelFinalStatus" runat="server"></asp:Label></td>

                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Office</td>
                                                    <td>
                                                        <asp:Label ID="LocationLabelFinalStatus" runat="server"></asp:Label></td>

                                                </tr>






                                                <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Date Of Joining</td>
                                                    <td>
                                                        <asp:Label ID="joiningDateLabelFinalStatus" runat="server"></asp:Label></td>
                                                    <td style="width: 20%; padding: 10px;" class="tblTHColorChang">Place</td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblPlaceFinalStatus"></asp:Label></td>
                                                </tr>


                                                <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Grade</td>
                                                    <td>
                                                        <asp:Label ID="lblGrade" runat="server"></asp:Label></td>
                                                    <td style="width: 20%; padding: 10px;" class="tblTHColorChang"></td>
                                                    <td>
                                                        <asp:Label runat="server" ID="Label2"></asp:Label></td>
                                                </tr>


                                                <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Step</td>
                                                    <td>
                                                        <asp:Label ID="lblStep" runat="server"></asp:Label></td>
                                                    <td style="width: 20%; padding: 10px;" class="tblTHColorChang"></td>
                                                    <td>
                                                        <asp:Label runat="server" ID="Label4"></asp:Label></td>
                                                </tr>








                                            </table>
                                        </div>
                                        
                                        
                                           <div class="col-md-12">

                                            <table class="table table-bordered table-striped">
                                                <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Overall Evaluation</td>


                                                    <td class="tblTHColorChang_New" style="width: 10%; padding: 10px;">Poor (<=60)</td>

                                                    


                                                    <td class="tblTHColorChang_New" style="width: 10%; padding: 10px;">Average (61-75)</td>
                                                   


                                                    <td class="tblTHColorChang_New" style="width: 10%; padding: 10px;">Good (76-80)</td>
                                                   


                                                    <td class="tblTHColorChang_New" style="width: 10%; padding: 10px;">Excellent (81-90)</td>
                                                    <td class="tblTHColorChang_New" style="width: 10%; padding: 10px;">Outstanding (>=91)</td>
                                                  


                                                </tr>
                                            </table>

                                        </div>

                                        <div class="col-md-12">

                                            <table class="table table-bordered table-striped">
                                                <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Total Marks Obtained (Out Of 100)</td>


                                                    <td class="tblTHColorChang" style="width: 10%; padding: 10px;">A:Functional</td>

                                                    <td>
                                                        <asp:Label runat="server" ID="partAScore"></asp:Label></td>


                                                    <td class="tblTHColorChang" style="width: 10%; padding: 10px;">B:Behavioral</td>
                                                    <td>
                                                        <asp:Label ID="partBScore" runat="server"></asp:Label></td>


                                                    <td class="tblTHColorChang" style="width: 10%; padding: 10px;">Total</td>
                                                    <td>
                                                        <asp:Label ID="totalScore" runat="server"></asp:Label></td>


                                                    <td class="tblTHColorChang" style="width: 10%; padding: 10px;">Over All Status</td>
                                                    <td>
                                                        <asp:Label ID="lblStatus" runat="server"></asp:Label></td>


                                                </tr>
                                            </table>

                                        </div>
                                        <div class="col-md-12">



                                            <div class="form-row">
                                                <div class="col-md-3">
                                                    <asp:RadioButtonList runat="server" ID="recommend" AutoPostBack="True" OnSelectedIndexChanged="recommend_SelectedIndexChanged">

                                                        <asp:ListItem Text="Promotion with Increment" Value="6" />
                                                        <asp:ListItem Text="Special Increment" Value="2" />
                                                        <asp:ListItem Text="Promotion" Value="3" />
                                                        <asp:ListItem Text="Performance Improvement Plan" Value="4" />
                                                        <%--<asp:ListItem Text="Promotion with Additional Increment" Value="5" />--%>

                                                        <asp:ListItem Text="General Increment" Selected="True" Value="1" />
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="col-md-1" id="Divsteps" runat="server" visible="False">
                                                    <div class="form-group">
                                                        <label>Steps</label>
                                                        <asp:TextBox ID="txtStep" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteresfsafsadTextBoxExtenderqty" runat="server"
                                                            Enabled="True" TargetControlID="txtStep" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>


                                                <div class="col-md-1" id="Div1Other" runat="server" visible="False">
                                                    <div class="form-group">

                                                        <label>Special Increment</label>
                                                        <asp:TextBox ID="txtnote" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-md-4">
                                                    <div class="form-group">

                                                        <label>Justification Promotion & Increment:</label>
                                                        <asp:TextBox ID="txtJustification" CssClass="form-control" TextMode="MultiLine" Rows="2" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div id="gridContainser1" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: hidden; display:none">
                                                        <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                                            CssClass="AddToListCssTable" OnPreRender="gv_DocumentUpload_PreRender">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="SL">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>






                                                                <asp:BoundField DataField="SuspendReasonEntry" HeaderText="Action Taken" />
                                                                <asp:BoundField DataField="EffectiveDate" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Effective Date" />


                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>


                                        <br />
                                        <asp:HyperLink ID="SuspendHistory" runat="server" Visible="False" Target="_blank" NavigateUrl="~/SuspendAndDiciplinary_UI/History.aspx">Suspend History</asp:HyperLink>

                                        <br />

                                        <hr />

                                        <div class="col-md-12">

                                            <div class="form-row">
                                                <asp:HiddenField runat="server" ID="hfDocFileName" />
                                                <asp:HiddenField runat="server" ID="hfDocFile" />
                                                <div class="col-md-4"></div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>
                                                            Document Upload    &nbsp; &nbsp;<asp:HyperLink ID="HyperLink1" CssClass=" btn-sm btn btn-warning" Target="_blank" runat="server" NavigateUrl="../DownloadsForm/RECOMMENDATION_FOR_PROMOTION.doc"><i class=" fa fa-arrow-down"></i> &nbsp; Download RECOMMENDATION Form
                                                            </asp:HyperLink></label>
                                                        <div>
                                                            <input type="file" name="postedFile" id="upImage" onchange="showpreview(this)" class="form-control form-control-sm" />
                                                            <asp:FileUpload ID="FUDocument" Visible="False" CssClass="form-control form-control-sm" runat="server" />
                                                            <br />
                                                            <input type="button" class="btn btn-sm  btn-success" id="btnUpload" value="Upload Document" />
                                                            &nbsp; 
                                                                   <asp:HyperLink ID="HLDocumentLink" CssClass=" btn-sm btn btn-outline-dark" Target="_blank" runat="server" Text='Preview'>
                                                                   </asp:HyperLink>
                                                            <asp:Label runat="server" ID="lbFileName"></asp:Label>
                                                            <asp:LinkButton runat="server" Visible="False" ID="btnDocUp" CssClass="btn btn-sm  btn-info">
                                                            
                                                      
              &nbsp;    <span class="btn-label"><i class="fa fa-upload"></i></span>  &nbsp;   &nbsp;Upload Document
                                                            </asp:LinkButton>
                                                            <br />
                                                            <progress id="fileProgress" style="display: none"></progress>
                                                            <br />
                                                            <span id="lblMessage" style="color: Green"></span>
                                                            <asp:Label runat="server" ID="lblMsg" Style="color: Green"></asp:Label>
                                                            <asp:HyperLink Visible="False" ID="HyperLink2" runat="server"
                                                                Target="_blank" ToolTip="Click to Show Document"></asp:HyperLink>



                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-row">
                                                <div class="col-md-5"></div>
                                                <div class="col-md-5"></div>
                                            </div>
                                        </div>
                                        <%--   <asp:Button ID="btnFunctionalCancel" Text="Close" OnClick="btnFunctionalCancel_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />--%>
                                        <br />
                                        <br />
                                    </div>


                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>









                    <%--Training--%>




                    <div>
                        <ajaxToolkit:ModalPopupExtender ID="MPTrainingSUP" runat="server" TargetControlID="TrainingSUP_Test" PopupControlID="pnl_TrainingSUP"
                            BackgroundCssClass="modalBackground">
                        </ajaxToolkit:ModalPopupExtender>

                        <asp:HiddenField ID="TrainingSUP_Test" runat="server"></asp:HiddenField>
                        <asp:Panel ID="pnl_TrainingSUP" runat="server" Style="display: none; overflow: scroll; padding: 10px" Height="680px" Width="90%" CssClass="modalPopup">
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <asp:UpdateProgress ID="UpdateProgress8" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                        <ProgressTemplate>
                                            <div class="divWaiting">
                                                <asp:Image ID="ggg232222imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>

                                    <div class="row">
                                        <div class="col-md-6" style="padding-left: 15px; padding-top: 12px;">
                                            <div class="text-left">
                                                <h1 class="title" style="font-size: 18px; padding-top: 0px;">
                                                    <img src="../Report_Pages/app.png" width="20px" />
                                                    Appraisal Training Part</h1>
                                            </div>
                                        </div>

                                        <div class="col-md-6" style="padding-top: 15px; padding-right: 15px;">
                                            <asp:LinkButton ID="btnTrainingSUPClose" Style="font-size: 20px!important;" OnClick="btnTrainingSUPClose_Click" CssClass="btn btn-danger pull-right  pull-right" runat="server">Close
                                            </asp:LinkButton>

                                            <asp:Button runat="server" ID="btnTrainSaveSUP" OnClick="btnTrainSaveSUP_OnClick" Text="Save " Style="font-size: 20px!important;" CssClass="btn btn-info   pull-right" />
                                        </div>
                                    </div>

                                    <hr />
                                    <div class="row">

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
                                        </style>
                                        <style>
                                            .tblTHColorChang {
                                                background-color: #EDF2F5 !important;
                                                font-weight: bold;
                                                font-size: 13px;
                                            }

                                             .tblTHColorChang_New {
                                               
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
                                            <h2 class="blue title-widget" style="color: #2196F3; text-shadow: 0 0 2px black;">Employee Details</h2>
                                            <table class="table table-bordered table-striped">
                                                <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Employee ID</td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblEmpIdTrainingSUP"></asp:Label></td>


                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Supervisor</td>
                                                    <td>
                                                        <asp:Label ID="ReportingLabelTrainingSUP" runat="server"></asp:Label></td>
                                                </tr>

                                                <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Employee Name</td>
                                                    <td>
                                                        <asp:Label ID="empNameTrainingSUP" runat="server"></asp:Label></td>



                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Department</td>
                                                    <td>
                                                        <asp:Label ID="deptNameLabelTrainingSUP" runat="server"></asp:Label></td>
                                                </tr>

                                                <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Designation</td>
                                                    <td>
                                                        <asp:Label ID="desigNameLabelTrainingSUP" runat="server"></asp:Label></td>

                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Office</td>
                                                    <td>
                                                        <asp:Label ID="LocationLabelTrainingSUP" runat="server"></asp:Label></td>

                                                </tr>






                                                <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Date Of Joining</td>
                                                    <td>
                                                        <asp:Label ID="joiningDateLabelTrainingSUP" runat="server"></asp:Label></td>
                                                    <td style="width: 20%; padding: 10px;" class="tblTHColorChang">Place</td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblPlaceTrainingSUP"></asp:Label></td>
                                                </tr>





                                            </table>
                                        </div>


                                        <div class="col-md-12">
                                            <div id="gridContainerTrainingSUP" style="height: auto; overflow: auto; width: auto;">

                                                <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gv_AppraisalTrainingSUP" CssClass="AddToListCssTable" OnPreRender="gv_DocumentUpload_PreRender">

                                                    <Columns>

                                                        <asp:TemplateField HeaderText="SL#">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Training Needs">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="TrainingNeeds" CssClass="form-control" Rows="2" TextMode="MultiLine" Text='<%#Eval("TrainingNeeds") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Quater">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="QuaterDropDownList1" AutoPostBack="true" runat="server" CssClass="form-control form-control-sm">
                                                                    <asp:ListItem Text="1st Quarter" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="2nd Quarter" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="3rd Quarter" Value="3"></asp:ListItem>
                                                                    <asp:ListItem Text="4th Quarter" Value="4"></asp:ListItem>

                                                                </asp:DropDownList>
                                                                <%--<asp:TextBox runat="server" ID="TrainingStart"  CssClass="form-control  form-control-sm"  Text='<%#Eval("TrainingStart") %>' ></asp:TextBox>--%>
                                                                <%--      <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                        TargetControlID="TrainingStart" />--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <%--    <asp:TemplateField HeaderText="End" runat="server" Visible="False">
                                    <ItemTemplate>
                                       <asp:TextBox runat="server" ID="TrainingEnd" CssClass="form-control  form-control-sm"   Text='<%#Eval("TrainingEnd") %>' ></asp:TextBox>
                                     <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                        TargetControlID="TrainingEnd" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                                        <asp:TemplateField HeaderText="Add">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btn_Add" OnClick="btn_Add_OnClick" CssClass="btn btn-info btn-sm" runat="server"><i class="fa fa-plus" aria-hidden="true"></i></asp:LinkButton>
                                                            </ItemTemplate>


                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete">
                                                            <ItemTemplate>


                                                                <asp:LinkButton ID="lb_Remove" CssClass="btn btn-danger btn-sm" OnClick="lb_Remove_OnClick" runat="server"><i class="fa fa-trash" aria-hidden="true"></i></asp:LinkButton>
                                                            </ItemTemplate>


                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>




                                    <hr />


                                    <div class="col-md-12">



                                        <div class="form-row">
                                            <div class="col-md-5"></div>
                                            <div class="col-md-5">
                                            </div>
                                        </div>
                                    </div>
                                    <%--   <asp:Button ID="btnFunctionalCancel" Text="Close" OnClick="btnFunctionalCancel_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />--%>
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






                    <div>
                        <ajaxToolkit:ModalPopupExtender ID="mpApprove" runat="server" TargetControlID="Approve_Test" PopupControlID="pnl_Approve"
                            BackgroundCssClass="modalBackground">
                        </ajaxToolkit:ModalPopupExtender>

                        <asp:HiddenField ID="Approve_Test" runat="server"></asp:HiddenField>
                        <asp:Panel ID="pnl_Approve" runat="server" Style="display: none; overflow: scroll; padding: 10px" Height="680px" Width="90%" CssClass="modalPopup">
                            <asp:UpdatePanel ID="UpdatePsdasdanel9" runat="server">
                                <ContentTemplate>
                                    <asp:UpdateProgress ID="UpdateProasdasdgress9" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                        <ProgressTemplate>
                                            <div class="divWaiting">
                                                <asp:Image ID="im33333gWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>

                                    <div class="row">
                                        <div class="col-md-6" style="padding-left: 15px; padding-top: 12px;">
                                            <div class="text-left">
                                                <h1 class="title" style="font-size: 18px; padding-top: 0px;">
                                                    <img src="../Report_Pages/app.png" width="20px" />
                                                    Appraisal Approve</h1>
                                            </div>
                                        </div>

                                        <div class="col-md-6" style="padding-top: 15px; padding-right: 15px;">
                                            <asp:LinkButton ID="btnApproveClose" OnClick="btnApproveClose_Click" Style="font-size: 20px!important" CssClass="btn btn-danger pull-right  pull-right" runat="server">Close
                                            </asp:LinkButton>
                                        </div>
                                    </div>

                                    <hr />
                                    <div class="form-row">
                                        <div class="col-md-4">
                                        </div>
                                        <div class="col-md-1.5">
                                            <label style="font-weight: bold">Approval Status:&nbsp;<span style="color: #a52a2a">*</span></label>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">

                                                <asp:RadioButtonList ID="actionRadioButtonList" runat="server" RepeatDirection="Horizontal">
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>

                                        <div class="col-md-3" style="display:none">
                                            <label style="font-weight: bold">
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  &nbsp;&nbsp;&nbsp;&nbsp;Previous Year's Score
                                            </label>
                                        </div>
                                    </div>

                                    <div class="form-row">
                                        <div class="col-md-4">
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label style="font-weight: bold">Comments</label>
                                                <asp:TextBox runat="server" ID="commentsTextBox" CssClass="form-control" ReadOnly="False" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-4" style="display:none">


                                            <div class="form-group">
                                                <label>Financial Year &nbsp; </label>

                                                <asp:DropDownList ID="ddlFiny" class="form-control form-control-sm col-md-8" runat="server" OnSelectedIndexChanged="ddlFiny_OnSelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                            </div>

                                            <div class="form-group">
                                                <label>Score &nbsp; </label>

                                                <asp:Label ID="lblScore" class="form-control form-control-sm col-md-8" runat="server"></asp:Label>
                                            </div>

                                            <div class="form-group">

                                                <asp:CheckBox runat="server" Visible="False" ID="SSGradeCheck" CssClass="chkChoiceHeader" AutoPostBack="True" OnCheckedChanged="SSGradeCheck_OnCheckedChanged" Text=" Select All / Unselect All" />
                                                <br />
                                                <asp:CheckBoxList ID="cblHeader" Visible="False" RepeatDirection="Vertical" RepeatColumns="3" CssClass="chkChoiceHeader" runat="server">


                                                    <asp:ListItem Value="BI">Basic Information</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="AI">Academic information</asp:ListItem>
                                                    <asp:ListItem Value="TWI">Training/WorkShop Information</asp:ListItem>
                                                    <asp:ListItem Value="FD">Family Description</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="Exp">Experience</asp:ListItem>
                                                    <asp:ListItem Value="NI">Nominees Information</asp:ListItem>
                                                    <asp:ListItem Value="PA">Performance & Achivement</asp:ListItem>

                                                    <asp:ListItem Selected="True" Value="DI">Diciplinary Information</asp:ListItem>

                                                    <asp:ListItem Value="PI">Promotional Information </asp:ListItem>
                                                    <%--(Promotion/Upgradation/Re-appointment)--%>
                                                    <asp:ListItem Selected="True" Value="threeParam">Promotion History of Employee/ Upgradation History of Employee/ History of Special Increment </asp:ListItem>
                                                    <asp:ListItem Value="TI">Re-Designation/ Re-appointment/ Posting/Transfer Information</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="KPI">Appraisal</asp:ListItem>


                                                </asp:CheckBoxList>
                                            </div>

                                            <div class="form-group">
                                                <asp:LinkButton runat="server" ID="btnViewrpt" OnClick="btnViewrpt_OnClick" AutoPostBack="True" CssClass="btn btn-success   btn-sm"><span aria-hidden="true" class="fa fa-print"></span>  &nbsp; View Report </asp:LinkButton>

                                            </div>
                                        </div>
                                    </div>


                                    <div class="form-row">
                                        <div class="col-md-5">
                                        </div>
                                        <div class="col-4 ">
                                            <div class="form-group">
                                                <link href="../UserSetup/ButtonGrups.css" rel="stylesheet" />
                                                <div class="ui-group-buttons">
                                                    <asp:LinkButton runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" CssClass="btn btn-info" OnClientClick="return confirm('Are you sure you want to Submit ?')" Style="box-shadow: 0 0 3px 1px rgba(0,0,0,.35);"> <i class="fa fa-check" aria-hidden="true"></i> &nbsp; Submit

&nbsp; </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%--  </div>--%>






                                    <div class="col-md-12">
                                        <div class="form-row" runat="server">
                                            <div class="col-md-12">
                                                <fieldset class="for-panel">
                                                    <legend>Approval Status List</legend>
                                                    <div style="overflow: scroll">
                                                        <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gv_Versions" CssClass="AddToListCssTable" OnPreRender="gv_DocumentUpload_PreRender">

                                                            <Columns>

                                                                <asp:TemplateField HeaderText="SL#">
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex+1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Employee ">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" ID="SkillInfo" Text='<%#Eval("Employee") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>




                                                                <asp:TemplateField HeaderText="Comments">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" ID="Version" Text='<%#Eval("PreviousVersion") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Approval Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" ID="Date" Text='<%#Eval("EntryDate") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>



                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </fieldset>
                                            </div>

                                        </div>
                                    </div>




                                    <%--   <asp:Button ID="btnFunctionalCancel" Text="Close" OnClick="btnFunctionalCancel_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />--%>
                          
                        </div>
                
                    </div>
                </div>
                        </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <script type="text/javascript">
        $("body").on("click", "#btnUpload", function () {
            if ($("#upImage").val() != '') {
                $.ajax({
                    url: '/HandlerDocAppFinalStatus.ashx',
                    type: 'POST',
                    data: new FormData($('form')[0]),
                    cache: false,
                    contentType: false,
                    processData: false,
                    success: function (file) {
                        $("#cpFormBody_hfDocFile").val('');
                        $("#cpFormBody_hfDocFileName").val('');
                        $("#fileProgress").hide();
                        $("#lblMessage").html("<b>" + file.name + "</b> has been uploaded.");
                        $("#cpFormBody_hfDocFile").val(file.dbfilename);
                        $("#cpFormBody_hfDocFileName").val(file.name);



                    },
                    xhr: function () {
                        var fileXhr = $.ajaxSettings.xhr();
                        if (fileXhr.upload) {
                            $("progress").show();
                            fileXhr.upload.addEventListener("progress", function (e) {
                                if (e.lengthComputable) {
                                    $("#fileProgress").attr({
                                        value: e.loaded,
                                        max: e.total
                                    });
                                }
                            }, false);
                        }
                        return fileXhr;
                    }
                });
            } else {
                alert("Please Upload a Document!!!");
            }
        });
    </script>
    <script type="text/javascript">


        function showpreview(input) {


            debugger;
            //$('#ContentPlaceHolder1_imageFileUpload').val($(this).val().toLowerCase());
            var validExtensions = [
                'PDF', 'pdf'];
            var fileName = input.files[0].name;
            var fileNameExt = fileName.substr(fileName.lastIndexOf('.') + 1);
            if ($.inArray(fileNameExt, validExtensions) == -1) {
                input.type = '';
                input.type = 'file';

                alert("Only these file types are accepted : pdf");
            }
            else {

                var picsize = (input.files[0].size);
                if (picsize > 5000000) {
                    input.type = '';
                    input.type = 'file';

                    alert("File Size is not accepted");

                } else {

                    if (input.files && input.files[0]) {


                        var filerdr = new FileReader();
                        filerdr.onload = function (e) {

                        }
                        filerdr.readAsDataURL(input.files[0]);
                    }

                }


            }


        }

    </script>
</asp:Content>


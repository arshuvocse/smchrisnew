<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="KPIMidYearMarkSetSelf.aspx.cs" Inherits="Appraisal_KPIMidYearMarkSetSelf" %>

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

        /* Keep dropdown options readable (avoid inheriting selected color). */
        select.status-red option,
        select.status-green option,
        select.status-yellow option {
            background-color: #ffffff !important;
            color: #000000 !important;
            font-weight: normal;
        }
    </style>

    <script type="text/javascript">
        function isNumeric(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            var inputValue = evt.target.value;

            // Allow control keys: backspace, delete, left arrow, right arrow
            if (charCode == 8 || charCode == 46 || charCode == 37 || charCode == 39) {
                return true;
            }

            // Allow numeric digits (0-9)
            if (charCode >= 48 && charCode <= 57) {
                return true;
            }

            // Allow one decimal point
            if (charCode == 46 && inputValue.indexOf('.') == -1) {
                return true;
            }

            // Disallow any other input
            return false;
        }

        function setMidYearStatusColor(dropdown) {
            const v = (dropdown.value || '').trim();  // Value
            const t = (dropdown.options[dropdown.selectedIndex]?.text || '').trim(); // Text

            dropdown.classList.remove('status-red', 'status-green', 'status-yellow');

            // ✅ RED
            if (v === 'Not Started' || v === 'Not Completed' || t === 'Not Started' || t === 'Behind' || t === 'Not Completed') {
                dropdown.classList.add('status-red');
                return;
            }

            // ✅ GREEN
            if (v === 'Ongoing' || v === 'Completed' || t === 'On Track' || t === 'Ongoing' || t === 'Completed') {
                dropdown.classList.add('status-green');
                return;
            }

            // ✅ YELLOW
            if (v === 'Not Relevant' || t === 'Not Relevant') {
                dropdown.classList.add('status-yellow');
                return;
            }
        }

        function initializeMidYearColors() {
            const dropdowns = document.querySelectorAll('select[id*=\"txtMidStatus\"]');

            dropdowns.forEach(function (ddl) {

                // ✅ Initial color set
                setMidYearStatusColor(ddl);

                // ✅ Prevent duplicate binding (important for UpdatePanel)
                if (ddl.getAttribute('data-colorbound') === '1') return;
                ddl.setAttribute('data-colorbound', '1');

                // ✅ Bind change handler (don't override AutoPostBack)
                if (ddl.addEventListener) {
                    ddl.addEventListener('change', function () {
                        setMidYearStatusColor(this);
                    });
                } else {
                    var existingOnChange = ddl.onchange;
                    ddl.onchange = function () {
                        setMidYearStatusColor(this);
                        if (existingOnChange) return existingOnChange.apply(this, arguments);
                    };
                }
            });
        }

        // ✅ First load
        document.addEventListener('DOMContentLoaded', initializeMidYearColors);

        // ✅ After UpdatePanel postback
        if (typeof Sys !== 'undefined' && Sys.WebForms) {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                initializeMidYearColors();
            });
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">

    <div class="content">



        <div class="container-fluid">
            <div class="page-heading">
                <div class="page-heading__container">
                    <h1 class="title" style="font-size: 18px; padding-top: 0px;">
                        <img src="../Report_Pages/app.png" width="20px" />
                        KPI Mid-Year Marking</h1>
                </div>
                <%-- <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="detailsViewButton" Text="" CssClass="btn btn-sm btn-outline-secondary " runat="server"  />
                        </div>--%>
                <div class="page-heading__container float-right d-none d-sm-block">
                    <asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />

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
                                        <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" ID="GridView1" CssClass="table table-bordered text-center thead-dark gridDatatable">

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
                            <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaiting">
                                        <asp:Image ID="eeeeeeeeeeeeeeee" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
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
                            <div class="form-row">


                                <div class="col-md-2" runat="server">
                                    <div class="form-group">
                                        <label>Financial Year &nbsp;<span style="color: #a52a2a">*</span></label>

                                        <asp:DropDownList ID="FinancialYearDropDownList" class="form-control form-control-sm" runat="server" OnSelectedIndexChanged="FinancialYearDropDownList_OnSelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="form-row" style="display:none">
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

                            <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" ID="AppraisalOwn" CssClass="table table-bordered text-center thead-dark gridDatatable" DataKeyNames="ActionStatusdd">

                                <Columns>



                                    <asp:TemplateField HeaderText="Employee ID">
                                        <ItemTemplate>

                                            <asp:HiddenField ID="id_empId" runat="server" Value='<%#Eval("EmpInfoId") %>' />
                                            <asp:HiddenField ID="selfMaster" runat="server" Value='<%#Eval("AppraisalSelfMasterId") %>' />
                                            <asp:HiddenField ID="id_appraisalMaster" runat="server" Value='<%#Eval("AppraisalMasterId") %>' />
                                            <asp:Label ID="lbl_EmpMasterCode" runat="server" Text='<%#Eval("EmpMasterCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Employee Info">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_EmpName" runat="server" Text='<%#Eval("EmpName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--  <asp:TemplateField HeaderText="Date of Joining">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_DateOfJoin" runat="server"   Text='<%#Eval("DateOfJoin") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Department">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_DepartmentName" runat="server"   Text='<%#Eval("DepartmentName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Designation" runat="server" Text='<%#Eval("Designation") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Is Mid Year Status" Visible="False">
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

                                    <asp:TemplateField HeaderText="Training" Visible="false">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="training" OnClick="TrainingOwn_OnClick" Text='<%#Eval("training") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Over All Status">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" OnClick="PartC_OnClick" ID="PartC"><i class="fa fa-eye" aria-hidden="true"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--              <asp:TemplateField HeaderText="View Comments">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server"  Id="ViewComments" CssClass="btn btn-success btn-sm"  OnClick="ViewComments_OnClick" >View Comments</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Approval Status">
                                        <ItemTemplate>
                                            <asp:Label ID="Approval" runat="server" Text='<%#Eval("ActionStatusAppraisal") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Awaiting Employee">
                                        <ItemTemplate>
                                            <asp:Label ID="Awaiting" runat="server" Text='<%#Eval("PendingEmpApp") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Print" Visible="true">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="btn_print" OnClick="btn_print_OnClick" CssClass="btn btn-sm btn-info"><i class="fa fa-print" aria-hidden="true"></i> Print
                                            </asp:LinkButton>
                                               <asp:Label ID="lblExpireStatus" runat="server" CssClass="alert-warning"  ToolTip="Please Inform HR to Extend Deadline"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Comments">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txt_comments" runat="server" TextMode="MultiLine" Width="100%" Rows="1" class="form-control"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" Text=" Submit " CssClass="btn btn-sm btn-info" />
                                            
                                             
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--     <asp:TemplateField HeaderText="View Details">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" CssClass="btn btn-info btn-sm"  ToolTip="Click To View Details" BorderStyle="None" OnClick="ShowPopup" ID="btnStatus">View Details</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>      --%>
                                </Columns>
                            </asp:GridView>

                            <asp:GridView runat="server" AutoGenerateColumns="False" OnRowDataBound="gv_AppraisalBoard_OnRowDataBound" Width="100%" ID="gv_AppraisalBoard" CssClass="table table-bordered text-center thead-dark gridDatatable" DataKeyNames="AppraisalMasterAppLogId,CompleteStatus">

                                <Columns>
                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            <asp:HiddenField ID="id_empId" runat="server" Value='<%#Eval("EmpInfoId") %>' />
                                            <asp:HiddenField ID="selfMaster" runat="server" Value='<%#Eval("AppraisalSelfMasterId") %>' />
                                            <asp:HiddenField ID="id_appraisalMaster" runat="server" Value='<%#Eval("AppraisalMasterId") %>' />
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
                                    <asp:TemplateField HeaderText=" ">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" OnClick="PartCSupp_OnClick" ID="PartC" Text="View Details"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>





                                    <asp:TemplateField HeaderText="Training">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" OnClick="Training_OnClick" ID="Training" Text='<%#Eval("training") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <%--       <asp:TemplateField HeaderText="View Comments">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server"  Id="ViewComments2" CssClass="btn btn-success btn-sm"  OnClick="ViewComments2_OnClick" >View Comments</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>


                                    <%--       <asp:TemplateField HeaderText="Comments" >
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_comments" runat="server" class="form-control form-control-sm" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Action" runat="server" Visible="False">
                                        <ItemTemplate>
                                            <asp:RadioButtonList ID="actionRadioButtonList" runat="server" RepeatDirection="Horizontal">
                                            </asp:RadioButtonList>
                                        </ItemTemplate>
                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText="Comments" runat="server" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txt_commentss" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Operation" runat="server" Visible="False">
                                        <ItemTemplate>
                                            <asp:Button runat="server" ID="btn_Save1" OnClick="btn_Save1_OnClick" Text="Save " CssClass="btn btn-sm btn-info" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                        <ProgressTemplate>
                                            <div class="divWaiting">
                                                <asp:Image ID="imgWaieeeeeeeewet" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
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
                                            &nbsp;   &nbsp;  &nbsp;&nbsp;
                                                           <asp:LinkButton ID="LinkButton1" OnClick="btnNo_Click" CssClass="btn btn-danger pull-right" Style="font-size: 20px!important;" runat="server"> Close

                                                           </asp:LinkButton>
                                            &nbsp;&nbsp;  &nbsp;&nbsp;
                                                          <asp:LinkButton runat="server" ID="btnFunctionalSave" OnClick="btnFunctionalSave_OnClick" Style="font-size: 20px!important;" CssClass="btn btn-info  pull-right ">Save</asp:LinkButton>
                                            &nbsp;&nbsp; &nbsp;&nbsp;
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

                                                <asp:GridView runat="server" ShowFooter="true" AutoGenerateColumns="False" Width="100%" ID="gv_AppraisalFunc" CssClass="table table-bordered text-center thead-dark gridDatatable" OnRowDataBound="gv_AppraisalFunc_RowDataBound" DataKeyNames="MidYearStatus,AppraisalSelfFucAreaId">

                                                    <Columns>

                                                        <asp:TemplateField HeaderText="SL#">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Key Performance Indicator">
                                                            <ItemTemplate>
                                                                <asp:TextBox ReadOnly="True" Width="530px" runat="server" ID="txtKpi" class="form-control " Rows="2" TextMode="MultiLine" Text='<%#Eval("KpiInfo") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Weight (Number)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ReadOnly="True" runat="server" ID="txtWeight" AutoPostBack="True" OnTextChanged="txtWeight_OnTextChanged" CssClass="form-control  form-control-sm" Text='<%#Eval("KpiWeight") %>'></asp:TextBox>

                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                                    TargetControlID="txtWeight" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Label CssClass="form-control  form-control-sm" ID="lblTotalWeight" runat="server" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Weight (%) ">
                                                            <ItemTemplate>
                                                                <asp:TextBox ReadOnly="True" runat="server" class="form-control form-control-sm" TextMode="Number" ID="txtWeightPer" Text='<%#Eval("KpiWeightPer") %>'></asp:TextBox>

                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxEsxtender2" runat="server" Enabled="True"
                                                                    TargetControlID="txtWeightPer" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                            </ItemTemplate>



                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Mid-Year Status">
                                                            <ItemTemplate>
                                                                <%--<asp:TextBox runat="server" ID="txtMidStatus" CssClass="form-control  form-control-sm" Text='<%#Eval("MidYearStatus") %>'></asp:TextBox>--%>
                                                                <asp:DropDownList Width="110px" runat="server" ID="txtMidStatus" CssClass="form-control  form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="txtMidStatus_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Select One..</asp:ListItem>
                                                                    <asp:ListItem Value="Not Started">Not Started</asp:ListItem>
                                                                    <asp:ListItem Value="Ongoing">On Track</asp:ListItem>
                                                                    <asp:ListItem Value="Completed">Completed</asp:ListItem>
                                                                    <asp:ListItem Value="Not Completed">Behind</asp:ListItem>
                                                                    <asp:ListItem Value="Not Relevant">Not Relevant</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Target (Number)" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:TextBox ReadOnly="True" class="form-control form-control-sm" runat="server" ID="txtTarget" Text='<%#Eval("Target") %>'></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxsssEsxtender2" runat="server" Enabled="True"
                                                                    TargetControlID="txtTarget" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Target (%)" runat="server" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="txtTargetPer" AutoPostBack="True" ReadOnly="True" CssClass="form-control  form-control-sm" Text='<%#Eval("TargetPer") %>'></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxsssssEsxtender2" runat="server" Enabled="True"
                                                                    TargetControlID="txtTargetPer" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Deadline">
                                                            <ItemTemplate>
                                                                <asp:TextBox Enabled="False" CssClass="form-control form-control-sm" runat="server" ID="txtDeadLine" Text='<%#Eval("Deadline") %>'></asp:TextBox>
                                                                <asp:CalendarExtender ID="CalenduuuarExtender1" runat="server" PopupPosition="TopLeft"
                                                                    Format="dd-MMM-yyyy" PopupButtonID="txtDeadLine" CssClass="MyCalendar"
                                                                    TargetControlID="txtDeadLine" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Mid-Year Status" Visible="false">
                                                            <ItemTemplate>
                                                                <%--<asp:TextBox runat="server" ID="txtMidStatus" CssClass="form-control  form-control-sm" Text='<%#Eval("MidYearStatus") %>'></asp:TextBox>--%>
                                                                <%--     <asp:DropDownList   runat="server" ID="txtMidStatus" CssClass="form-control  form-control-sm">
                                                                    <asp:ListItem Value="Not Started">Not Started</asp:ListItem>
                                                                    <asp:ListItem Value="In Progress">In Progress</asp:ListItem>
                                                                    <asp:ListItem Value="Completed">Completed</asp:ListItem>
                                                                </asp:DropDownList>--%>


                                                                <asp:LinkButton runat="server" OnClick="lbMidStatus_OnClick" CssClass="btn btn-info" Text="Update" ID="lbMidStatus"></asp:LinkButton>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Results
(Year End)"
                                                            Visible="false">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="txtResultYearEnd" CssClass="form-control  form-control-sm" Text='<%#Eval("ResultYearEnd") %>'></asp:TextBox>

                                                            </ItemTemplate>


                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Supervisor-Mark" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="txtResult" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("ResultYearEnd") %>'></asp:TextBox>


                                                                <cc1:FilteredTextBoxExtender ID="FilssteredTextBoxsssssEsxtender2" runat="server" Enabled="True"
                                                                    TargetControlID="txtResult" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Mid-Year Marking">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="txtselfMark" AutoPostBack="True" OnTextChanged="txtselfMark_OnTextChanged" TextMode="Number" onkeypress="return isNumeric(event);" CssClass="form-control  form-control-sm" Text='<%#Eval("SelfMark") %>'></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilsssssteredTextBoxsssssEsxtender2" runat="server" Enabled="True"
                                                                    TargetControlID="txtselfMark" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                                <%-- <asp:Label runat="server" ID="txtselfMark" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("SelfMark") %>'></asp:Label>--%>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTotalMark" CssClass="form-control  form-control-sm" runat="server" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Self Assessment">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="txtComment" CssClass="form-control  form-control-sm resizable-textbox" TextMode="MultiLine" Rows="2" Text='<%#Eval("Comments") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Result" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="txtMark" AutoPostBack="True" CssClass="form-control  form-control-sm" Text='<%#Eval("SupervisorMark") %>'></asp:TextBox>


                                                                <cc1:FilteredTextBoxExtender ID="FilssssstsseredTextBoxsssssEsxtender2" runat="server" Enabled="True"
                                                                    TargetControlID="txtMark" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTotalMarkSupervisor" CssClass="form-control  form-control-sm " runat="server" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>


                                                        <%--    <asp:TemplateField HeaderText="Is Active" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="isActiveCheckBox" OnCheckedChanged="isActiveCheckBox_OnCheckedChanged" runat="server" Checked='<%#Eval("IsActive") %>' AutoPostBack="True" EnableTheming="True" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Operation" Visible="False">
                                                            <ItemTemplate>

                                                                <asp:LinkButton Visible="False" ID="btnFunction" CssClass="btn btn-info btn-sm" OnClick="btnFunction_OnClick" runat="server"><i class="fa fa-plus" aria-hidden="true"></i>
                                                                </asp:LinkButton>
                                                                <asp:LinkButton Visible="False" ID="lb_FunctioRemove" CssClass="btn btn-danger btn-sm" OnClick="lb_FunctioRemove_OnClick" runat="server"><i class="fa fa-trash" aria-hidden="true"></i>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>


                                                        </asp:TemplateField>--%>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>




                                    <hr />


                                    <asp:HiddenField runat="server" ID="id_mastetID" />
                                    <asp:HiddenField runat="server" ID="id_selfID" />
                                    <asp:HiddenField runat="server" ID="id_Empid" />
                                    <div class="form-row">
                                        <div class="col-md-5"></div>
                                        <div class="col-md-5">
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


                    <%--Behavioral--%>



                    <div>
                        <ajaxToolkit:ModalPopupExtender ID="MPBehavioral" runat="server" TargetControlID="Behavioral_Test" PopupControlID="pnl_Behavioral"
                            BackgroundCssClass="modalBackground">
                        </ajaxToolkit:ModalPopupExtender>

                        <asp:HiddenField ID="Behavioral_Test" runat="server"></asp:HiddenField>
                        <asp:Panel ID="pnl_Behavioral" runat="server" Style="display: none; overflow: scroll; padding: 10px" Height="600px" Width="80%" CssClass="modalPopup">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>

                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                        <ProgressTemplate>
                                            <div class="divWaiting">
                                                <asp:Image ID="imgWaeeeewit" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
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
                                            <asp:LinkButton ID="btnBehavioralClose" Style="font-size: 20px!important;" OnClick="btnBehavioralClose_Click" CssClass="btn btn-danger pull-right  pull-right" runat="server">Close
                                            </asp:LinkButton>


                                            <asp:Button runat="server" ID="btnBehave" OnClick="btnBehave_OnClick" Text="Save " Style="font-size: 20px!important;" CssClass="btn btn-info  pull-right" />
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

    <asp:GridView runat="server" ShowFooter="true" AutoGenerateColumns="False" Width="100%" ID="gv_AppraisalPartB" CssClass="table table-bordered text-center thead-dark gridDatatable">

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
                    <asp:TextBox runat="server" ID="SupportingEmp" CssClass="form-control" TextMode="MultiLine" Rows="2" Text='<%#Eval("SupportingEmp") %>'></asp:TextBox>
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
            <asp:TemplateField HeaderText="Mid-Year Mark">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="SelfScore" AutoPostBack="True" OnTextChanged="SelfScore_OnTextChanged" CssClass="form-control  form-control-sm" TextMode="Number" onkeypress="return isNumeric(event);" Text='<%#Eval("SelfScore") %>'></asp:TextBox>

                    <cc1:FilteredTextBoxExtender ID="sadFisasasare001" runat="server" Enabled="True"
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


                                    <div class="form-row">
                                        <div class="col-md-5"></div>
                                        <div class="col-md-5">
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
                                                <asp:Image ID="imgqqWaitdd" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
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
                                            <asp:LinkButton ID="btnTrainingClose" OnClick="btnTrainingClose_Click" CssClass="btn  btn-danger pull-right  pull-right" Style="font-size: 20px!important;" runat="server">Close
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

                                                <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gv_AppraisalTraining" CssClass="table table-bordered text-center thead-dark gridDatatable">

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


                                    <div class="form-row">
                                        <div class="col-md-5"></div>
                                        <div class="col-md-5">
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
                        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="hfDoc" PopupControlID="pnl_doc"
                            BackgroundCssClass="modalBackground">
                        </ajaxToolkit:ModalPopupExtender>

                        <asp:HiddenField ID="hfDoc" runat="server"></asp:HiddenField>
                        <asp:Panel ID="pnl_doc" runat="server" Style="display: none; overflow: scroll; padding: 10px" Height="650px" Width="90%" CssClass="modalPopup">
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                <ContentTemplate>
                                    <asp:UpdateProgress ID="UpdateProgress9" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                        <ProgressTemplate>
                                            <div class="divWaiting">
                                                <asp:Image ID="imgqqWaitasdasd" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                    <div class="row">
                                        <div class="col-md-6" style="padding-left: 15px; padding-top: 12px;">
                                            <div class="text-left">
                                                <h1 class="title" style="font-size: 18px; padding-top: 0px;">
                                                    <img src="../Report_Pages/app.png" width="20px" />
                                                    Document Upload</h1>
                                            </div>
                                        </div>

                                        <div class="col-md-6" style="padding-top: 15px; padding-right: 15px;">
                                            <asp:LinkButton ID="btnDocClose" Style="font-size: 20px!important;" OnClick="btnDocClose_OnClick" CssClass="btn btn-danger pull-right  pull-right" runat="server"> Close
                                            </asp:LinkButton>


                                            <asp:Button runat="server" ID="btnSaveDOcment" OnClick="btnSaveDOcment_OnClick" Text="Save " Style="font-size: 20px!important;" CssClass="btn btn-info   pull-right" />
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


                                            <div class="row">



                                                <div class="col-2">
                                                </div>

                                                <asp:HiddenField runat="server" ID="hfDocFileName" />
                                                <asp:HiddenField runat="server" ID="hfDocFile" />
                                                <div class="col-4">
                                                    <div class="form-group">
                                                        <label>Document Upload<span style="color: red;" title="please fill out this field"> * </span></label>
                                                        <div>
                                                            <input type="file" name="postedFile" id="upImage" onchange="showpreview(this)" class="form-control form-control-sm" />
                                                            <asp:FileUpload ID="FUDocument" Visible="False" CssClass="form-control form-control-sm" runat="server" />
                                                            <br />
                                                            <input type="button" class="btn btn-sm  btn-info" id="btnUpload" value="Upload Document" />
                                                            <asp:LinkButton runat="server" Visible="False" OnClick="btnDocUp_OnClick" ID="btnDocUp" CssClass="btn btn-sm  btn-info">
                                                          
                                                      
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


                                                <div class="col-4">
                                                    <div class="form-group">
                                                        <label>Summary Note<span style="color: red;" title="please fill out this field"> * </span></label>
                                                        <div>

                                                            <asp:TextBox runat="server" ID="txtSummaryNote" TextMode="MultiLine" Rows="2" class="form-control" />

                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <asp:LinkButton runat="server" ID="brnAddDoc" OnClick="brnAddDoc_OnClick" CssClass="btn btn-success   btn-sm pull-right"><span aria-hidden="true" class="fa fa-plus"></span>  &nbsp; Add to List </asp:LinkButton>
                                                    </div>
                                                </div>

                                            </div>
                                            <br />




                                            <div class="row">
                                                <div class="col-2">
                                                </div>
                                                <div class="col-md-8">


                                                    <asp:GridView Width="100%" ShowHeader="True" ID="gv_DocumentUpload" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable" OnPreRender="gv_DocumentUpload_PreRender">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SL#">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex + 1%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Document">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="HLDocumentLink" Target="_blank" runat="server" NavigateUrl='<%# Eval("DocumentLink") %>' Text='Preview'>
                                                                    </asp:HyperLink>

                                                                    <%--        <a   Target="_blank"    href="<%# Eval("DocumentLink")%>">Download</a>--%>
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



                                                            <asp:TemplateField HeaderText="Remove">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="btnDocRemove" OnClick="btnDocRemove_OnClick" CssClass="btn btn-sm btn-danger"><i class="fa fa-minus-circle"></i> </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>


                                                </div>
                                            </div>

                                        </div>
                                    </div>




                                    <hr />


                                    <div class="form-row">
                                        <div class="col-md-5"></div>
                                        <div class="col-md-5">
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
                                <Triggers>

                                    <asp:PostBackTrigger ControlID="btnDocUp" />

                                </Triggers>
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
                                                <asp:Image ID="imgWwqrqwait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
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
                                            <asp:LinkButton ID="LinkButton2" OnClick="btncommClose_Click" CssClass="btn btn-danger pull-right  pull-right" runat="server">Close
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



                                        <div class="col-md-12">

                                            <div class="row" runat="server" visible="False">

                                                <div class="col-md-3" runat="server">
                                                    <div class="form-group">
                                                        <label>Comments</label>
                                                        <asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="form-row" runat="server">
                                                <div class="col-md-12">
                                                    <fieldset class="for-panel">
                                                        <legend>Approval Status List</legend>
                                                        <div style="overflow: scroll">
                                                            <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gv_Versions" CssClass="table table-bordered text-center thead-dark gridDatatable">

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
                                                    </fieldset>
                                                </div>

                                            </div>
                                        </div>
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
                        <asp:Panel ID="pnlFunctionalSup" runat="server" Style="display: none; overflow: scroll; padding: 10px" Height="600px" Width="80%" CssClass="modalPopup">
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
                                        <div class="col-md-6" style="padding-left: 15px; padding-top: 12px;">
                                            <div class="text-left">
                                                <h1 class="title" style="font-size: 18px; padding-top: 0px;">
                                                    <img src="../Report_Pages/app.png" width="20px" />
                                                    Appraisal Functional Part</h1>
                                            </div>
                                        </div>

                                        <div class="col-md-6" style="padding-top: 15px; padding-right: 15px;">
                                            <asp:LinkButton ID="btnFunctionalSupClose" Style="font-size: 20px!important;" OnClick="btnFunctionalSupClose_Click" CssClass="btn  btn-danger pull-right  pull-right" runat="server">Close
                                            </asp:LinkButton>


                                            <asp:Button runat="server" ID="btnAppraisalFuncSUPSave" OnClick="btnAppraisalFuncSUPSave_OnClick" Text="Save " Style="font-size: 20px!important;" CssClass="btn btn-info" Visible="False" />
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
                                            <div id="gridContainer1SUP" style="height: auto; overflow: auto; width: auto;">

                                                <asp:GridView runat="server" ShowFooter="true" AutoGenerateColumns="False" Width="100%" ID="gv_AppraisalFuncSUP" CssClass="table table-bordered text-center thead-dark gridDatatable" DataKeyNames="AppraisalSelfFucAreaId">

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
                                                                <asp:TextBox runat="server" ID="txtWeight" AutoPostBack="True" ReadOnly="True" OnTextChanged="txtWeightSUP_OnTextChanged" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("KpiWeight") %>'></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                                    TargetControlID="txtWeight" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTotalWeight" CssClass="form-control  form-control-sm " runat="server" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Weight (%) ">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="txtWeightPer" ReadOnly="True" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("KpiWeightPer") %>'></asp:TextBox>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Target (Number)">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="txtTarget" ReadOnly="True" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("Target") %>'></asp:TextBox>
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

                                                        <asp:TemplateField HeaderText="Dead Line">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="txtDeadLine" CssClass="form-control  form-control-sm" ReadOnly="True" Text='<%#Eval("Deadline") %>'></asp:TextBox>
                                                                <%--         <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                Format="dd-MMM-yyyy" CssClass="custom"
                                                TargetControlID="txtDeadLine" />--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Mid Year Status">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ReadOnly="True" ID="txtMidStatus" CssClass="form-control  form-control-sm" Text='<%#Eval("MidYearStatus") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Result End Status" runat="server" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="txtResult" CssClass="form-control  form-control-sm" TextMode="Number" AutoPostBack="True" OnTextChanged="txtResult_OnTextChanged" Text='<%#Eval("ResultYearEnd") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblresultend" CssClass="form-control  form-control-sm " runat="server" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Self-Mark">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ReadOnly="True" ID="txtselfMark" CssClass="form-control  form-control-sm" TextMode="Number" onkeypress="return isNumeric(event);"  Text='<%#Eval("SelfMark") %>'></asp:TextBox>

                                                                <%-- <asp:Label runat="server" ID="txtselfMark" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("SelfMark") %>'></asp:Label>--%>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblselfMark" CssClass="form-control  form-control-sm " runat="server" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Supervisor Mark">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="txtMark" AutoPostBack="True" OnTextChanged="SupervisorMark_OnTextChanged" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("SupervisorMark") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTotalMark" CssClass="form-control  form-control-sm " runat="server" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>









                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>




                                    <hr />




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
                                                <asp:Image ID="imgqwqwWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
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


                                            <asp:Button runat="server" ID="btnPartBSUPSave" OnClick="btnPartBSUPSave_OnClick" Text="Save " Style="font-size: 20px!important;" CssClass="btn btn-info  pull-right" Visible="False" />
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

                                                <asp:GridView runat="server" ShowFooter="true" AutoGenerateColumns="False" Width="100%" ID="gv_AppraisalPartBSUP" CssClass="table table-bordered text-center thead-dark gridDatatable">

                                                    <Columns>

                                                        <asp:TemplateField HeaderText="SL#">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Competencies / Skills">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="SkillInfo" Text='<%#Eval("SkillInfo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Supporting Example">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="SupportingEmp" Text='<%#Eval("SupportingEmp") %>'></asp:Label>
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

                                                        <asp:TemplateField HeaderText="Self Score">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ReadOnly="True" ID="SelfScore" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("SelfScore") %>'></asp:TextBox>

                                                                                                                                                                                                                                                                                                                                    <cc1:FilteredTextBoxExtender ID="Fisasasare001" runat="server" Enabled="True"
TargetControlID="SelfScore" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>

                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblselfscrore" CssClass="form-control  form-control-sm " runat="server" />
                                                            </FooterTemplate>



                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Supervisor Score">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" AutoPostBack="True" OnTextChanged="SupervisorScore_OnTextChanged" ID="SupervisorScore" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("SupervisorScore") %>'></asp:TextBox>

                                                                                                                                                                                                                                                                    <cc1:FilteredTextBoxExtender ID="Fisadl21SuperasdvisorsadsaScore001" runat="server" Enabled="True"
TargetControlID="SupervisorScore" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                            </ItemTemplate>

                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTotalMark" CssClass="form-control  form-control-sm " runat="server" />
                                                            </FooterTemplate>


                                                        </asp:TemplateField>





                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>




                                    <hr />




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
                                                <asp:Image ID="imgWa121it" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
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
                                            <asp:LinkButton ID="btnFinalStatusClose" OnClick="btnFinalStatusClose_Click" CssClass="btn btn-xs btn-danger pull-right  pull-right" runat="server">Close
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
                                                <div class="col-md-4">
                                                    <asp:RadioButtonList runat="server" ID="recommend" AutoPostBack="True" OnSelectedIndexChanged="recommend_SelectedIndexChanged">

                                                        <asp:ListItem Text="Promotion with Increment" Value="6" />
                                                        <asp:ListItem Text="Special Increment" Value="2" />
                                                        <asp:ListItem Text="Promotion" Value="3" />
                                                        <asp:ListItem Text="Performance Improvement Plan" Value="4" />
                                                        <%--<asp:ListItem Text="Promotion with Additional Increment" Value="5" />--%>

                                                        <asp:ListItem Text="General Increment" Selected="True" Value="1" />
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="col-md-2" id="Divsteps" runat="server" visible="False">
                                                    <div class="form-group">
                                                        <label>Steps</label>
                                                        <asp:TextBox ID="txtStep" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteresfsafsadTextBoxExtenderqty" runat="server"
                                                            Enabled="True" TargetControlID="txtStep" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>


                                                <div class="col-md-2" id="Div1Other" runat="server" visible="False">
                                                    <div class="form-group">

                                                        <label>Note</label>
                                                        <asp:TextBox ID="txtnote" CssClass="form-control" TextMode="MultiLine" Rows="2" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-md-6">
                                                    <div class="form-group">

                                                        <label>Justification:</label>
                                                        <asp:TextBox ID="txtJustification" CssClass="form-control" TextMode="MultiLine" Rows="2" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>


                                    <br />
                                    <asp:HyperLink ID="SuspendHistory" runat="server" Visible="False" Target="_blank" NavigateUrl="~/SuspendAndDiciplinary_UI/History.aspx">Suspend History</asp:HyperLink>

                                    <br />

                                    <hr />




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
                                                <asp:Image ID="img213Wait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
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
                                            <asp:LinkButton ID="btnTrainingSUPClose" OnClick="btnTrainingSUPClose_Click" CssClass="btn   btn-danger pull-right  pull-right" runat="server"><i style="font-size: 20px"   >Close</i>
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

                                                <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gv_AppraisalTrainingSUP" CssClass="table table-bordered text-center thead-dark gridDatatable">

                                                    <Columns>

                                                        <asp:TemplateField HeaderText="SL#">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Training Needs">
                                                            <ItemTemplate>
                                                                <asp:TextBox ReadOnly="True" runat="server" ID="TrainingNeeds" CssClass="form-control" Rows="2" TextMode="MultiLine" Text='<%#Eval("TrainingNeeds") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Quater">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="QuaterDropDownList1" Enabled="False" AutoPostBack="true" runat="server" CssClass="form-control form-control-sm">
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

                                                        <%--       <asp:TemplateField HeaderText="Add">
                                        <ItemTemplate>
                                             <asp:LinkButton ID="btn_Add" OnClick="btn_Add_OnClick"  CssClass="btn btn-info btn-sm"  runat="server"><i class="fa fa-plus" aria-hidden="true"></i></asp:LinkButton>
                                              </ItemTemplate>
                                    
                                  
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        
                                      
                                        <asp:LinkButton ID="lb_Remove"   CssClass="btn btn-danger btn-sm"   OnClick="lb_Remove_OnClick"  runat="server"><i class="fa fa-trash" aria-hidden="true"></i></asp:LinkButton>
                                    </ItemTemplate>
                                    
                                  
                                </asp:TemplateField>--%>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>




                                    <hr />



                                    <asp:Button runat="server" ID="btnTrainSaveSUP" OnClick="btnTrainSaveSUP_OnClick" Text="Save " Style="font-size: 20px!important;" CssClass="btn btn-info" Visible="False" />
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

                </div>
            </div>
        </div>
    </div>


    <script type="text/javascript">
        $("body").on("click", "#btnUpload", function () {
            if ($("#upImage").val() != '') {
                $.ajax({
                    url: '/HandlerDocApp.ashx',
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
            var validExtensions = ['txt', 'jpg', 'png', 'gif', 'doc', 'docx', 'TXT', 'JPG', 'PNG', 'GIF', 'DOC', 'DOCX', 'pdf', 'PDF', 'ZIP', 'zip', 'rar', 'RAR', 'xlsx', 'XLSX', 'xls', 'XLS'];
            var fileName = input.files[0].name;
            var fileNameExt = fileName.substr(fileName.lastIndexOf('.') + 1);
            if ($.inArray(fileNameExt, validExtensions) == -1) {
                input.type = '';
                input.type = 'file';

                alert("Only these file types are accepted : 'txt', 'jpg', 'png', 'gif', 'doc', 'docx','rar' , 'xls', 'xlsx' ");
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



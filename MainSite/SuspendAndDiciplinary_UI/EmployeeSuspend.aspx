<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeSuspend.aspx.cs" Inherits="SuspendAndDiciplinary_UI_EmployeeSuspend" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../Assets/css/calender.css" rel="stylesheet" />
    <link href="../Assets/css/customCalender.css" rel="stylesheet" />
    <script type="text/javascript">
        function ShowPopup() {
            $("#formExampleModal").click();
        }


        $('#saveButton').click(function () {
            var title = $('#employeeTytle').val();

            if (title == "") {
                alert('Employe Type is required !!!');
                return false;
            }

            var data = { type: title };
            var stringData = JSON.stringify(data);

            $.ajax({
                type: "POST",
                url: "EmployeeSuspend.aspx/SaveEmployeeType",
                data: stringData,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
            });
        });
    </script>


    <style type="text/css">
        /*AutoComplete flyout */
       
    </style>


    <style type="text/css">
        .form-group.required .control-label:after {
            color: #d00;
            content: "*";
            position: absolute;
            margin-left: 4px;
            top: 4px;
            font-size: large;
        }
    </style>


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

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <!-- PAGE HEADING -->

                <div class="page-heading">
                    <div class="page-heading__container">
                        <%--<div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>--%>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Employee Suspend Information </h1>
                    </div>

                    <%--<div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="detailsViewButton" Text="View Details Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                        <asp:Button ID="Button1" Text="Get History" CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="Button1_OnClick" />
                        <%-- <asp:Button ID="reportViewButton" Text="Report" CssClass="btn btn-sm btn-outline-success" runat="server" OnClick="rptImageButton_Click" />--%>
                    <%--  </div>--%>

                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                        <asp:Button ID="detailsViewButton" Visible="True" Text="Go Back" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>

                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">

                    <div class="card">
                        <div class="card-body">

                            <asp:Label ID="MessageLabel" runat="server" Text=""></asp:Label>


                            <div class="form-row">
                                <div class="col-md-6">
                                    <div class="form-row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Company Name: </label>
                                                <span style="color: red">&nbsp;*</span>
                                                <asp:DropDownList ID="companyDropDownList" class="form-control form-control-sm" OnSelectedIndexChanged="companyDropDownList_OnSelectedIndexChanged" runat="server" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Financial Year </label>
                                                <span style="color: red">&nbsp;*</span>
                                                <asp:DropDownList ID="FinancialYearDropDownList" class="form-control form-control-sm" OnSelectedIndexChanged="FinancialYearDropDownList_OnSelectedIndexChanged" runat="server" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </div>



                                        <div class="col-4">
                                            <div class="form-group">
                                                <label>Search Employe:</label>
                                                <span style="color: red">&nbsp;*</span>
                                                <asp:TextBox ID="EmployeeDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnTextChanged="EmployeeDropDownList_SelectedIndexChanged"></asp:TextBox>
                                                <%--<asp:TextBox ID="EmployeeNameTextBox" AutoPostBack="True" runat="server" class="form-control form-control-sm" OnTextChanged="EmployeeNameTextBox_OnTextChanged"></asp:TextBox>--%>
                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                    EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                    ServiceMethod="GetCompanyWiseEmployeeInfoActive" ServicePath="~/WebService.asmx" TargetControlID="EmployeeDropDownList"
                                                    UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                    ShowOnlyCurrentWordInCompletionListItem="true">
                                                </cc1:AutoCompleteExtender>
                                                <asp:HiddenField ID="jobIdHiddenField" runat="server" />
                                            </div>
                                        </div>


                                        <asp:HiddenField runat="server" ID="HFDivID" />
                                        <asp:HiddenField runat="server" ID="HFDivWingId" />
                                        <asp:HiddenField runat="server" ID="HFSecID" />
                                        <asp:HiddenField runat="server" ID="HFSubSecID" />
                                        <asp:HiddenField runat="server" ID="HFSalLocID" />
                                        <asp:HiddenField runat="server" ID="HFJobLocID" />

                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-row">

                                        <div class="col-4">
                                            <div class="form-group">
                                                <label>Employee Code:</label>
                                                <asp:Label ID="empCodeLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>

                                            </div>
                                        </div>

                                        <div class="col-4">
                                            <div class="foSrm-group">
                                                <label>Department Name :</label>
                                                <asp:Label ID="deptNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                                <asp:HiddenField ID="deptIdHiddenField" runat="server" />
                                            </div>
                                        </div>

                                        <div class="col-4">
                                            <div class="form-group">
                                                <label>Employee Type:</label>
                                                <asp:Label ID="employeeType" runat="server" class="form-control form-control-sm"></asp:Label>
                                                <asp:HiddenField ID="empTypeHiddenField" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-row">
                                        <div class="col-4">
                                            <div class="form-group">
                                                <label>Employee Name:</label>
                                                <asp:Label ID="empNameTexBox" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                                <asp:HiddenField ID="EmpInfoIdHiddenField" runat="server" />
                                                <asp:HiddenField ID="suspendHiddenField" runat="server" />
                                            </div>
                                        </div>

                                        <div class="col-4">
                                            <div class="form-group">
                                                <label>Designation Name :</label>
                                                <asp:Label ID="desigNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                                <asp:HiddenField ID="desigIdHiddenField" runat="server" />
                                            </div>
                                        </div>

                                        <div class="col-4">
                                            <div class="form-group">
                                                <label>Joining Date :</label>
                                                <asp:Label ID="joiningDateLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                            </div>
                                        </div>

                                    </div>
                                </div>


                            </div>






                            <div class="form-row">

                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Effective From Date :</label>
                                        <span style="color: red">&nbsp;*</span>
                                        <div class="input-group date pull-left" id="daterangepicker1">
                                            <asp:TextBox ID="effectDateTexBox" runat="server" class="form-control form-control-sm" OnTextChanged="effectDateTexBox_Changed" AutoCompleteType="Disabled" AutoPostBack="True" CausesValidation="true"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender" runat="server" PopupPosition="TopLeft"
                                                Format="dd-MMM-yyyy" PopupButtonID="ImageBuzxzttosn1" CssClass="MyCalendar"
                                                TargetControlID="effectDateTexBox" />
                                        </div>
                                    </div>
                                </div>
                                
                                 <div class="col-2">
                                    <div class="form-group">
                                        <label>Effective To Date :</label>
                                        <span style="color: red">&nbsp;*</span>
                                        <div class="input-group date pull-left" id="daterangepicker1">
                                            <asp:TextBox ID="effectToDateTexBox" runat="server" class="form-control form-control-sm" OnTextChanged="effectDateTexBoxTo_Changed" AutoCompleteType="Disabled" AutoPostBack="True" CausesValidation="true"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="TopLeft"
                                                Format="dd-MMM-yyyy" PopupButtonID="ImageBuzxzttosn1" CssClass="MyCalendar"
                                                TargetControlID="effectToDateTexBox" />
                                        </div>
                                    </div>
                                </div>

                                <div class="col-2">
                                    <div class="form-group">
                                        <label><span style="font-size: 11px; font-weight: bold;">Action Type: </span></label>
                                        <span style="color: red">&nbsp;*</span>
                                        <asp:DropDownList ID="actionTypeDropDownList" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Description </label>
                                        <asp:TextBox ID="descriptionTexBox" runat="server" CssClass="form-control resize" Rows="1" Columns="20" TextMode="MultiLine" class="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Remarks </label>
                                        <asp:TextBox ID="remarksTextBox" runat="server" Rows="1" Columns="20" TextMode="MultiLine" class="form-control resize"></asp:TextBox>
                                    </div>
                                </div>

                            </div>











                            <div class="form-row">
                                <div class="col-1" style="display: none">
                                    <div class="form-group">
                                        <label style="color: #fff">Type:</label>
                                        <asp:Button ID="popupButton" Text="Pop up" class="btn btn-light" data-toggle="modal" data-target="#formExampleModal" CssClass="btn btn-sm btn-primary" runat="server" OnClick="popupButton_Click" />
                                    </div>
                                </div>

                                <div class="col-1" style="display: none">
                                    <div class="form-group">
                                        <label style="color: #fff">Type:</label>
                                        <asp:Button ID="refreshButton" Text="Save" CssClass="btn btn-sm btn-dark" runat="server" OnClick="refreshButton_Click"></asp:Button>
                                    </div>
                                </div>

                                <%--   <div class="modal fade" id="formExampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                    <div class="modal-dialog" role="document">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <h5 class="modal-title" id="exampleModalLabel">Employee type </h5>
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                            </div>
                                            <div class="modal-body">
                                                <form>

                                                    <div class="form-row">
                                                        <asp:Label ID="msgLabel" runat="server" Text="Label"></asp:Label>
                                                    </div>

                                                    <div class="form-row">
                                                        <div class="col-12">
                                                            <div class="form-group">
                                                                <label>Employee Type </label>
                                                                <input type="text" class="form-control" id="employeeTytle">
                                                            </div>
                                                        </div>
                                                    </div>


                                                </form>
                                            </div>
                                            <div class="modal-footer">
                                                <button type="button" id="saveButton" class="btn btn-primary"> Save </button></div>
                                            </div>
                                        </div>
                                    </div>--%>
                            </div>

                            <br />
                            
                            
                            <div class="form-row">
                                <div class="col-12 ">
                                    <asp:CheckBox ID="manualUpdateCheckBox" runat="server" /> <span>&nbsp; Manually Update to Employee Information</span>
                                </div>
                            </div>
                            
                            <br />
                            <div class="form-row">
                                <div class="col-6">
                                    <div class="form-group">

                                        <asp:Button ID="submithButton" Text="Save" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="submitButton_Click" />
                                        <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                        <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />
                                        <%--<asp:Button ID="submithButton" Text="Save" CssClass="btn btn-sm btn-info" runat="server" OnClick="submitButton_Click" />--%>
                                        <asp:Button ID="cancelButton" Text="Cancel" CssClass="btn btn-sm warning" Visible="False" runat="server" OnClick="cancelButton_OnClick" BackColor="#FFCC00" />
                                    </div>
                                </div>
                            </div>

                        </div>


                    </div>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>









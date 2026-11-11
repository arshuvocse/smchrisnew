<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeCauseShow.aspx.cs" Inherits="SuspendAndDiciplinary_UI_EmployeeCauseShow" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <!-- PAGE HEADING -->

                <div class="page-heading">
                    <div class="page-heading__container">
                        <div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Employee Show Cause Entry </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <%--<asp:Button ID="detailsViewButton" Text="View Details Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="departmentListImageButton_Click" />--%>
                        <%-- <asp:Button ID="reportViewButton" Text="Report" CssClass="btn btn-sm btn-outline-success" runat="server" OnClick="rptImageButton_Click" />--%>
                    </div>
               
                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">

                    <div class="card">
                        <div class="card-body">
                            <div class="form-row">
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Effective Date :</label>
                                        <div class="input-group date pull-left" id="daterangepicker1">
                                            <asp:TextBox ID="effectDateTexBox" runat="server" class="form-control form-control-sm" CausesValidation="true"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                Format="dd-MMM-yyyy" PopupButtonID="ImageButton1" CssClass="custom"
                                                TargetControlID="effectDateTexBox" />
                                            <div class="input-group-addon" style="border: 1px solid #cccccc">
                                                <span>
                                                    <asp:ImageButton ID="ImageButton1" runat="server"
                                                        AlternateText="Click to show calendar"
                                                        ImageUrl="../Assets/Calendar_scheduleHS.png" TabIndex="4" />
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                
                                  <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Company Name </label>
                                                        <asp:DropDownList ID="companyDropDownList" class="form-control form-control-sm" OnSelectedIndexChanged="companyDropDownList_OnSelectedIndexChanged" runat="server" AutoPostBack="True"></asp:DropDownList>
                                                    </div>
                                                      </div>
                                <div class="col-3">
                                          <div class="form-group">
                                     <label>Employee Name :</label>
                                        <asp:DropDownList ID="EmployeeDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="EmployeeDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:HiddenField ID="jobIdHiddenField" runat="server" />
                                    </div>
                                </div>

                           <%--     <div class="col-1">
                                    <div class="form-group">
                                        <label style="color: white">Search </label>
                                        <br />
                                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-sm btn-info" OnClick="searchdddddButton_Click" Text="Search" />
                                      <%--<asp:Button ID="searchButton" runat="server" CssClass="btn btn-sm btn-info" OnClick="searchdddddButton_Click" Text="Search" />
                                    </div>
                                </div>--%>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Employee Name :</label>
                                        <asp:TextBox ID="empNameTexBox" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                        <asp:HiddenField ID="EmpInfoIdHiddenField" runat="server" />
                                        <asp:HiddenField ID="suspendHiddenField" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="card">
                        <div class="card-body">
                            <asp:Label ID="MessageLabel" runat="server" Text=""></asp:Label>
                            <div class="form-row">
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Company Name :</label>
                                        <asp:Label ID="comNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="comIdHiddenField" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Division Name :</label>
                                        <asp:Label ID="divisionNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="divitionIdHiddenField" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Division Wing Name :</label>
                                        <asp:Label ID="divWingNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="divWingIdHiddenField" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Department Name :</label>
                                        <asp:Label ID="deptNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="deptIdHiddenField" runat="server" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-row">
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Section Name :</label>
                                        <asp:Label ID="secNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="secIdHiddenField" runat="server" />
                                    </div>
                                </div>


                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Sub Section Name :</label>
                                        <asp:Label ID="subSectionLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="subSectionHiddenField" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Designation Name :</label>
                                        <asp:Label ID="desigNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="desigIdHiddenField" runat="server" />
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Grade :</label>
                                        <asp:Label ID="empGradeLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="empGradeIdHiddenField" runat="server" />
                                    </div>
                                </div>

                            </div>


                            <div class="form-row">

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Joining Date :</label>
                                        <asp:Label ID="joiningDateLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                    </div>
                                </div>

                              


                            </div>

                            <div class="form-row">
                                
                                  <div class="col-3">
                                    <div class="form-group">
                                        <label>Description </label>
                                        <asp:TextBox ID="descriptionTexBox" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Remarks </label>
                                        <asp:TextBox ID="remarksTextBox" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                         

                                <div class="col-3" style="display: none" >
                                    <div class="form-group">
                                        <label>Type:</label>
                                        <asp:DropDownList ID="typeDropDownList" runat="server" class="form-control form-control-sm"></asp:DropDownList>                                        
                                        <asp:HiddenField ID="empTypeHiddenField" runat="server" />
                                    </div>
                                </div>

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
                            </div>

                            <br />
                            <div class="form-row">
                                <div class="col-6">
                                    <div class="form-group">
                                        <asp:Button ID="submithButton" Text="Save" CssClass="btn btn-sm btn-info" runat="server" OnClick="submitButton_Click" />
                                        <asp:Button ID="cancelButton" Text="Cancel" CssClass="btn btn-sm warning" runat="server" OnClick="cancelButton_OnClick" BackColor="#FFCC00" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>









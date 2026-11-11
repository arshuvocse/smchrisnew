<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="DiciplinaryAction.aspx.cs" Inherits="SuspendAndDiciplinary_UI_DiciplinaryAction" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
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
             .Label_Title {
            background-color: #C7C7C7;
            width: 100%;
            text-align: center;
            margin: 0px;
            padding: 3px;
            text-align: center;
            color: #000;
            margin-right: 5%;
            font-weight: bold;
            font-size: 13px;
        }
        /*AutoComplete flyout */
         .chkChoice label {
             padding-left: 10px;
             padding-right: 30px;
         }

        .chkChoiceStep label {
            padding-left: 8px;
            padding-right: 10px;
        }
        
        .SelectchkChoice label {
            padding-left: 6px;
            font-weight: bold;
        }

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <!-- PAGE HEADING -->
                       <asp:UpdateProgress ID="UpdateProgress1" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                        <ProgressTemplate>
                            <div class="divWaiting">
                                <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                <div class="page-heading">
                    <div class="page-heading__container">
                       <%-- <div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>--%>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Disciplinary Action Information </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                           <asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="HomeButton_OnClick" />
                         <asp:Button ID="detailsViewButton" Text="Go Back" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                        <%--<asp:Button ID="Button1" Text="Get History" CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="Button1_OnClick" />--%>
                        <%-- <asp:Button ID="reportViewButton" Text="Report" CssClass="btn btn-sm btn-outline-success" runat="server" OnClick="rptImageButton_Click" />--%>
                    </div>

                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">

                    <div class="card">
                        <div class="card-body">

                            <div class="form-row">
                                <div class="col-md-6">
                                    <div class="form-row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Company Name: </label><span style="color:red">&nbsp;*</span>
                                                <asp:DropDownList ID="companyDropDownList" class="form-control form-control-sm" OnSelectedIndexChanged="companyDropDownList_OnSelectedIndexChanged" runat="server" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </div>
                                        
                                          <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Financial Year </label> <span style="color:red">&nbsp;*</span>
                                                <asp:DropDownList ID="FinancialYearDropDownList" class="form-control form-control-sm" OnSelectedIndexChanged="FinancialYearDropDownList_OnSelectedIndexChanged" runat="server" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </div>


                                        <div class="col-10">
                                            <div class="form-group">
                                                <label>Search Employe:</label><span style="color:red">&nbsp;*</span>
                                             <asp:DropDownList runat="server" ID="ddlEmpInfo" class="form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpInfo_OnSelectedIndexChanged" />
                                                      <script type="text/javascript">
                                                          function pageLoad() {
                                                              $('#<%=ddlEmpInfo.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });
                                                        }
</script>
                                                <asp:HiddenField ID="jobIdHiddenField" runat="server" />
                                            </div>
                                        </div>
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
                                            <div class="form-group">
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
                                <%--AutoPostBack="True" OnTextChanged="ddddd"--%> 
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Effective Date :</label><span style="color:red">&nbsp;*</span>
                                        <div class="input-group date pull-left" id="daterangepicker1">
                                            <asp:TextBox ID="effectDateTexBox" runat="server" class="form-control form-control-sm" CausesValidation="true"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender" runat="server" PopupPosition="TopLeft"
                                                Format="dd-MMM-yyyy" PopupButtonID="ImageButtosn1" CssClass="MyCalendar"
                                                TargetControlID="effectDateTexBox" />
                                        </div>
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
                              <div class="row">
                              <div class="col-8">
                                    <div class="form-group" runat="server" Visible="False">
                                        <label><span style="font-size: 11px; font-weight: bold;">Action Type: </span></label><span style="color:red">&nbsp;*</span>
                                        <asp:DropDownList ID="actionTypeDropDownList" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                    </div>
                                    
                                       <div class="row">
                                                            <div class="col-md-12">
                                                               
                                                                <div class="Label_Title">Action Type List</div>
                                                              <br/>
                                                                <div class="form-group">
                                                                    <div style="overflow: scroll; height: 230px">
                                                                         <asp:CheckBox runat="server" ID="SSGradeCheck" CssClass="SelectchkChoice" AutoPostBack="True" OnCheckedChanged="SSGradeCheck_OnCheckedChanged" Text=" Select All / Unselect All" />
                                                                        <br />
                                                                        <br />
                                                                        <asp:CheckBoxList ID="gradeCheckBoxList"  CssClass="chkChoice" RepeatColumns="1" RepeatDirection="Horizontal" runat="server"></asp:CheckBoxList>
                                                                    </div>
                                                                </div>
                                                            </div>
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

                                    <asp:HiddenField runat="server" ID="HFDivID" />
                                                    <asp:HiddenField runat="server" ID="HFDivWingId" />
                                                    <asp:HiddenField runat="server" ID="HFSecID" />
                                                    <asp:HiddenField runat="server" ID="HFSubSecID" />
                                                    <asp:HiddenField runat="server" ID="HFSalLocID" />
                                                    <asp:HiddenField runat="server" ID="HFJobLocID" />
                            </div>
                            <br />
                            <div class="form-row">
                                <div class="col-6">
                                    <div class="form-group">
                                        <asp:Button ID="submithButton" Text="Save" CssClass="btn btn-sm btn-info" runat="server" OnClick="submitButton_Click" Visible="False" />
                                          <asp:Button runat="server" ID="btn_Del" OnClick="btn_Del_OnClick" Text="Delete "  CssClass="btn btn-sm btn-danger" Visible="False" />
                               <asp:Button runat="server" ID="btn_Edit" OnClick="btn_Edit_OnClick" Text="Update " CssClass="btn btn-sm btn-success" Visible="False" />
                                        <asp:Button ID="cancelButton" Text="Cancel" CssClass="btn btn-sm warning" runat="server" Visible="False" OnClick="cancelButton_OnClick" BackColor="#FFCC00" />
                                    </div>
                                </div>
                            </div>
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
                        <br/>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>









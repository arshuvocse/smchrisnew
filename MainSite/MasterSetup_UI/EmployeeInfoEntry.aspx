<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MainMasterPage.master" CodeFile="EmployeeInfoEntry.aspx.cs" Inherits="UserSetup_EmployeeInfoEntry" %>

<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .form-group.required .control-label:after {
            color: #d00;
            content: "*";
            position: absolute;
            margin-left: 4px;
            top: 4px;
            font-size: large;
        }

        body {
            font-family: Verdana;
            font-size: 14px;
        }
        /* Accordion */
        .accordionHeader {
            border: 1px solid #2F4F4F;
            color: white;
            background-color: #2E4d7B;
            font-family: Arial, Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            padding: 5px;
            margin-top: 5px;
            cursor: pointer;
        }

        #master_content .accordionHeader a {
            color: #FFFFFF;
            background: none;
            text-decoration: none;
        }

            #master_content .accordionHeader a:hover {
                background: none;
                text-decoration: underline;
            }

        .accordionHeaderSelected {
            border: 1px solid #2F4F4F;
            color: white;
            background-color: #5078B3;
            font-family: Arial, Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            padding: 5px;
            margin-top: 5px;
            cursor: pointer;
        }




        .ajax__fileupload {
            padding: 4px;
            border: #D3D3D3 1px solid;
            overflow: auto;
        }

        .ajax__fileupload_selectFileContainer {
            height: 24px;
            line-height: 24px;
        }

        .ajax__fileupload_selectFileButton {
            display: block;
            height: 24px;
            line-height: 24px;
            width: 80px;
            text-align: center;
            background-color: #212121;
            color: #D0D0D0;
            cursor: pointer;
            margin-right: 4px;
            font-size: 13px;
        }

            .ajax__fileupload_selectFileButton:hover {
                background-color: #000000;
                color: #ffffff;
            }

        .ajax__fileupload_topFileStatus {
            color: rgb(127, 126, 126);
        }

        .ajax__fileupload_ProgressBarHolder {
            margin-right: 70px;
            _margin-right: 0;
        }

        .ajax__fileupload_uploadbutton {
            width: 60px;
            text-align: center;
            cursor: pointer;
            color: white;
            font-weight: bold;
            background-color: #000099;
        }

        .ajax_fileupload_cancelbutton {
            width: 60px;
            text-align: center;
            cursor: pointer;
            color: white;
            font-weight: bold;
            background-color: #990033;
        }

        .ajax__fileupload_dropzone {
            border-style: dotted;
            border-width: 1px;
            line-height: 50px;
            text-align: center;
            _text-align: left; /* IE Only */
            margin-bottom: 2px;
        }

        .ajax__fileupload_queueContainer {
            border: #A9A9A9 1px solid;
            border-width: 1px;
            margin-top: 2px;
            padding: 4px;
            clear: both;
        }

        .ajax__fileupload_progressBar {
            padding-left: 4px;
            background-color: #CCFFCC;
        }

        .ajax__fileupload_footer {
            margin-top: 2px;
            line-height: 20px;
            height: 20px;
        }

        .ajax__fileupload_fileItemInfo {
            line-height: 20px;
            height: 20px;
            margin-bottom: 2px;
            overflow: hidden;
        }

            .ajax__fileupload_fileItemInfo .filename {
                font-weight: bold;
            }

            .ajax__fileupload_fileItemInfo .uploadstatus {
                font-style: italic;
            }

            .ajax__fileupload_fileItemInfo .removeButton {
                cursor: pointer;
                background-color: #900;
                color: white;
                width: 55px;
                height: 20px;
                line-height: 20px;
                text-align: center;
                display: block;
                float: left;
            }

            .ajax__fileupload_fileItemInfo .uploadedState {
                color: #060;
                background-color: #fff;
            }

            .ajax__fileupload_fileItemInfo .uploadingState {
                color: #FF9900;
                background-color: #fff;
            }

            .ajax__fileupload_fileItemInfo .pendingState {
                color: #009;
                background-color: #fff;
            }

            .ajax__fileupload_fileItemInfo .errorState {
                color: #ffffff;
                background-color: #ff0000;
            }

            .ajax__fileupload_fileItemInfo .cancelledState {
                color: #900;
                background-color: #fff;
            }

        /*Check*/
        .ajax__fileupload_selectFileContainer {
            display: inline-block;
            overflow: hidden;
            position: relative;
            width: 80px;
            /*float: left;*/
        }

            .ajax__fileupload_selectFileContainer input {
                border: medium none;
                cursor: pointer;
                height: 40px;
                margin: 0;
                opacity: 0;
                position: absolute;
                right: 0;
                top: 0;
            }



        .ajax__fileupload_fileItemInfo {
            position: relative;
            z-index: 0;
        }

            .ajax__fileupload_fileItemInfo div {
                display: inline-block;
            }

                .ajax__fileupload_fileItemInfo div.removeButton {
                    position: absolute;
                    top: 0;
                    right: 0;
                }
    </style>
      <style>
        #cpFormBody_ExtraCurriculamGridView > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }

        #cpFormBody_ExtraCurriculamGridView > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }


        #cpFormBody_OtherTalentsGridView > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }

        #cpFormBody_OtherTalentsGridView > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }




        #cpFormBody_AchievementsGridView > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }

        #cpFormBody_AchievementsGridView > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }



        #cpFormBody_HobbyGridView > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }

        #cpFormBody_HobbyGridView > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }




        #cpFormBody_gv_Nominee > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }

        #cpFormBody_gv_Nominee > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }



        #cpFormBody_gv_Reference > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }

        #cpFormBody_gv_Reference > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }



        #cpFormBody_gv_Training > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }

        #cpFormBody_gv_Training > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }



        #cpFormBody_gv_Experience > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }

        #cpFormBody_gv_Experience > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }



        #cpFormBody_gv_Education > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }

        #cpFormBody_gv_Education > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }


        #cpFormBody_gv_Children > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }

        #cpFormBody_gv_Children > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }
    </style>
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server" UpdateMode="Conditional">
            <%--        <Triggers>
            <asp:PostBackTrigger ControlID="<%= btn_ImageUpload.ClientID %>" />
        </Triggers>--%>
            <ContentTemplate>
                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;">Employee Information Entry</h1>
                        </div>
                        <div class="page-heading__container">
                            <asp:Label runat="server" ID="lbl_Mode"></asp:Label>
                            <span>
                                <span class="title" style="font-size: 14px; padding-top: 0px;">Employee Code : </span>
                                <asp:Label runat="server" ID="empMasterCode"></asp:Label>
                            </span>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="detailsViewButton" Text="List Information " CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-body">

                            <ajaxToolkit:Accordion ID="MyAccordion" runat="server" SelectedIndex="0" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent" FadeTransitions="false" FramesPerSecond="40" TransitionDuration="250" AutoSize="None" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                <Panes>


                                    <ajaxToolkit:AccordionPane ID="AccordionPane2" runat="server">
                                        <Header>
                                            <span class="accordionLink">1. Employment Information</span>
                                        </Header>
                                        <Content>
                                            <div class="card">
                                                <div class="card-body">
                                                    <div class="form-row">
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Company</label>
                                                                <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Division</label>
                                                                <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDivision" class="form-control form-control-sm" />

                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Wing</label>
                                                                <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlWing" class="form-control form-control-sm" />

                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Department</label>
                                                                <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDepartment" class="form-control form-control-sm" />

                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Section</label>
                                                                <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlSection" class="form-control form-control-sm" />

                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Sub Section</label>
                                                                <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlSubSection" class="form-control form-control-sm" />

                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Employee Category</label>
                                                                <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlEmpCategory" class="form-control form-control-sm" OnSelectedIndexChanged="ddlEmpCategory_OnSelectedIndexChanged" />

                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Salary Grade</label>
                                                                <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlSalaryGrade" class="form-control form-control-sm" OnSelectedIndexChanged="ddlSalaryGrade_OnSelectedIndexChanged" />

                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Salary Step</label>
                                                                <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlSalaryStep" class="form-control form-control-sm" />

                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Designation</label>
                                                                <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDesignation" class="form-control form-control-sm" />

                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Designation Type</label>
                                                                <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDesignationType" class="form-control form-control-sm" />

                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Job Location</label>
                                                                <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlJobLocation" class="form-control form-control-sm" />

                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Salary Location</label>
                                                                <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlSalaryLocation" class="form-control form-control-sm" />

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </Content>
                                    </ajaxToolkit:AccordionPane>
                                    <ajaxToolkit:AccordionPane ID="AccordionPane1" runat="server">
                                        <Header>
                                            <%--//--- Heading -----%>
                                            <span class="accordionLink">2. General Information</span>
                                        </Header>
                                        <%--//--- Content -----%>
                                        <Content>
                                            <div class="card">
                                                <div class="card-body">
                                                    <div class="form-row">
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Employee Name</label>
                                                                <asp:TextBox runat="server" ID="txt_EmpName" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Gender</label>
                                                                <asp:DropDownList runat="server" ID="ddlGender" class="form-control form-control-sm">
                                                                    <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                                                                    <asp:ListItem Text="FeMale" Value="FeMale"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Blood Group</label>
                                                                <asp:DropDownList runat="server" ID="ddlBloodGroup" class="form-control form-control-sm">
                                                                    <asp:ListItem Text="Unknown" Value="Unknown"></asp:ListItem>
                                                                    <asp:ListItem Text="A+" Value="A+"></asp:ListItem>
                                                                    <asp:ListItem Text="A-" Value="A-"></asp:ListItem>
                                                                    <asp:ListItem Text="B+" Value="B+"></asp:ListItem>
                                                                    <asp:ListItem Text="B-" Value="B-"></asp:ListItem>
                                                                    <asp:ListItem Text="O+" Value="O+"></asp:ListItem>
                                                                    <asp:ListItem Text="O-" Value="O-"></asp:ListItem>
                                                                    <asp:ListItem Text="AB+" Value="AB+"></asp:ListItem>
                                                                    <asp:ListItem Text="AB-" Value="AB-"></asp:ListItem>

                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Tin No.</label>
                                                                <asp:TextBox runat="server" ID="txt_EmpTinNo" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Father's Name</label>
                                                                <asp:TextBox runat="server" ID="txt_EmpFatherName" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Father's Occupation</label>
                                                                <asp:DropDownList runat="server" ID="ddlEmpFOccupation" class="form-control form-control-sm">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Mother's Name</label>
                                                                <asp:TextBox runat="server" ID="txt_EmpMotherName" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Mother's Occupation</label>
                                                                <asp:DropDownList runat="server" ID="ddlEmpMOccupation" class="form-control form-control-sm">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Date of Birth</label>
                                                                <asp:TextBox runat="server" ID="txt_EmpDOB" class="form-control form-control-sm" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                                    Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                    TargetControlID="txt_EmpDOB" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Date of Join</label>
                                                                <asp:TextBox runat="server" ID="txt_EmpDOJ" class="form-control form-control-sm" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                                    Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                    TargetControlID="txt_EmpDOJ" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Religion</label>
                                                                <asp:DropDownList runat="server" ID="ddlEmpReligion" class="form-control form-control-sm">
                                                                    <asp:ListItem Text="Islam" Value="Islam"></asp:ListItem>
                                                                    <asp:ListItem Text="Hindu" Value="Hindu"></asp:ListItem>
                                                                    <asp:ListItem Text="Christian" Value="Christian"></asp:ListItem>
                                                                      <asp:ListItem Text="Buddhist" Value="Buddhist"></asp:ListItem>
                                                                            <asp:ListItem Text="Others" Value="Others"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Marital Status</label>
                                                                <asp:DropDownList runat="server" ID="ddlEmpMaritalStatus" class="form-control form-control-sm">
                                                                    <asp:ListItem Text="Single" Value="Single"></asp:ListItem>
                                                                    <asp:ListItem Text="Married" Value="Married"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Employee Type</label>
                                                                <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlEmpType" class="form-control form-control-sm" OnSelectedIndexChanged="ddlEmpType_OnSelectedIndexChanged" />

                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Place Of Birth</label>
                                                                <asp:TextBox runat="server" ID="txt_EmpPlaceOfBirth" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Nationality</label>
                                                                <asp:DropDownList runat="server" ID="ddlEmpNationality" class="form-control form-control-sm">
                                                                    <asp:ListItem Text="Bangladeshi" Value="Bangladeshi"></asp:ListItem>
                                                                    <asp:ListItem Text="Foreign" Value="Foreign"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>National ID</label>
                                                                <asp:TextBox runat="server" ID="txt_EmpNationalID" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div id="iddivContract" cssclass="row" runat="server" visible="False">
                                                            <div cssclass="col-3">
                                                                <div cssclass="form-group">
                                                                    <label>Contract End Date</label>
                                                                    <asp:TextBox runat="server" ID="txt_ContractEndDate" CssClass="form-control form-control-sm" />
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender154" runat="server"
                                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                        TargetControlID="txt_ContractEndDate" />
                                                                </div>
                                                            </div>

                                                            <div cssclass="col-3">
                                                                <div cssclass="form-group">
                                                                    <label>Contract Project</label>
                                                                    <asp:CheckBoxList RepeatDirection="Horizontal" runat="server" ID="cbl_ContractProject" CssClass="form-control form-control-sm" />
                                                                </div>
                                                            </div>
                                                            <div cssclass="col-3">
                                                                <div cssclass="form-group">
                                                                    <label>Salary From Project</label>
                                                                    <asp:DropDownList runat="server" ID="ddlSalFromProject" CssClass="form-control form-control-sm" />
                                                                </div>
                                                            </div>
                                                            <div cssclass="col-3">
                                                            </div>
                                                        </div>

                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Passport No.</label>
                                                                <asp:TextBox runat="server" ID="txt_EmpPassport" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Expected Service length(Y)</label>
                                                                <asp:TextBox runat="server" ID="txt_EmpExpectedServiceLength" class="form-control form-control-sm" AutoPostBack="True" OnTextChanged="txt_EmpExpectedServiceLength_OnTextChanged" TextMode="Number" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Date of Retirement</label>
                                                                <asp:TextBox runat="server" ID="txt_EmpDateOfRetirement" class="form-control form-control-sm" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                                                                    Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                    TargetControlID="txt_EmpDateOfRetirement" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Date of Conformation</label>
                                                                <asp:TextBox runat="server" ID="txt_EmpDateOfConformation" class="form-control form-control-sm" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server"
                                                                    Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                    TargetControlID="txt_EmpDateOfConformation" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Conformation Status</label>
                                                                <asp:TextBox runat="server" ID="txt_ConformationStatus" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Reporting Boss</label>
                                                                <asp:TextBox runat="server" AutoPostBack="True" ID="txt_ReportingBoss" class="form-control form-control-sm" OnTextChanged="txt_ReportingBoss_OnTextChanged" />

                                                                <ajaxToolkit:AutoCompleteExtender
                                                                    ID="at_txt_ReportingBoss"
                                                                    TargetControlID="txt_ReportingBoss"
                                                                    runat="server"
                                                                    ServiceMethod="GetEmpNameDesigIDAuto_AllCompany"
                                                                    ServicePath="~/WebService.asmx"
                                                                    MinimumPrefixLength="1"
                                                                    CompletionInterval="500"
                                                                    EnableCaching="false"
                                                                    CompletionSetCount="1"
                                                                    FirstRowSelected="false">
                                                                </ajaxToolkit:AutoCompleteExtender>
                                                                <asp:HiddenField runat="server" ID="hdReportingBoss" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Reporting Boss's Designation</label>
                                                                <asp:TextBox runat="server" ID="txt_ReportingBossDesig" class="form-control form-control-sm" ReadOnly="True" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Is Probationary</label>
                                                                <asp:CheckBox runat="server" ID="chkIsProbationary" AutoPostBack="True" OnCheckedChanged="chkIsProbationary_OnCheckedChanged" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Probationary End Date</label>
                                                                <asp:TextBox runat="server" ID="txt_ProbationaryEndDate" class="form-control form-control-sm" ReadOnly="True" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender114" runat="server"
                                                                    Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                    TargetControlID="txt_ProbationaryEndDate" />
                                                            </div>
                                                        </div>
                                                        <div class="col-6">
                                                            <div class="form-group">
                                                                <label>Image Upload</label>
                                                                <div>
                                                                    <%--<asp:FileUpload ID="fu_Image" runat="server" />
                                                                    <asp:Button ID="btn_ImageUpload" runat="server" Text="Image Upload" OnClick="btn_ImageUpload_OnClick" />--%>
                                                                    <%--<asp:Button ID="btnProcessData"
                                                                                runat="server" Text="Process Data"
                                                                                OnClick="btnProcessData_Click" />--%>
                                                                    <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server"
                                                                        AssociatedUpdatePanelID="upFormBody">
                                                                        <ProgressTemplate>
                                                                            Please wait, your file is getting uploaded....
                                                                        </ProgressTemplate>
                                                                    </asp:UpdateProgress>
                                                                    <triggers>
                                                                        <asp:PostBackTrigger ControlID="btn_ImageUpload"  />--%>
                                                                    <%--<asp:AsyncPostBackTrigger ControlID="btnProcessData" />--%>
                                                                    <%--</triggers>

                                                                    <asp:Image ID="img" runat="server"
                                                                        Width="100" Height="100" ImageAlign="Middle" />--%>
                                                                    <%--                                                                    <ajaxToolkit:AjaxFileUpload
                                                                        id="ajaxUpload1" 
                                                                        OnUploadComplete="ajaxUpload1_OnUploadComplete"
                                                                        
                                                                        runat="server"  />--%>


                                                                    <%--                                                                    <br />
                                                                    <asp:Image ID="Image1" runat="server" Width="120" Height="90" BorderWidth="1" />--%>

                                                                    <input type="file" name="postedFile" />
                                                                    <input type="button" id="btnUpload" value="Upload" />
                                                                    <progress id="fileProgress" style="display: none"></progress>
                                                                    <hr />
                                                                    <asp:Image ID="img_emp" runat="server" Width="120" Height="90" BorderWidth="1" />
                                                                    <asp:HiddenField runat="server" ID="hfempimg" />
                                                                    <span id="lblMessage" style="color: Green"></span>
                                                                </div>
                                                            </div>
                                                        </div>


                                                        <%--                                                    <div class="col-6">
                                                        <div class="form-group">
                                                            <label>Signature Upload</label>
                                                            <div>
                                                                    
                                                                <input type="file" name="SignatureFile" />
                                                                <input type="button" id="btnSignatureUpload" value="Signature Upload" />
                                                                <progress id="SignaturefileProgress" style="display: none"></progress>
                                                                <hr />
                                                                <asp:Image ID="SignatureImage" runat="server" Width="120" Height="90" BorderWidth="1" />
                                                                <asp:HiddenField runat="server" ID="hfSignature"/>
                                                                <span id="lblMessageSignature" style="color: Green"></span>
                                                            </div>
                                                        </div>
                                                    </div>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </Content>
                                    </ajaxToolkit:AccordionPane>
                                    <ajaxToolkit:AccordionPane ID="AccordionPane3" runat="server">
                                        <%--//--- Heading -----%>
                                        <Header>
                                            <span class="accordionLink">3. Contacts</span>
                                        </Header>
                                        <%--//--- Content -----%>
                                        <Content>
                                            <div class="card">
                                                <div class="card-body">
                                                    <div class="form-row">
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Present Address</label>
                                                                <asp:TextBox runat="server" ID="txt_EmpPresentAddress" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Present Division</label>
                                                                <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlEmpPresentDivision" class="form-control form-control-sm" OnSelectedIndexChanged="ddlEmpPresentDivision_OnSelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Present District</label>
                                                                <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlEmpPresentDist" class="form-control form-control-sm" OnSelectedIndexChanged="ddlEmpPresentDist_OnSelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Present Thana</label>
                                                                <asp:DropDownList runat="server" ID="ddlEmpPresentThana" class="form-control form-control-sm">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>

                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Present Tel. No</label>
                                                                <asp:TextBox runat="server" ID="txt_EmpPresentTelNo" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Parmanent Address</label>
                                                                <asp:TextBox runat="server" ID="txt_EmpParmanentAddress" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Parmanent Division</label>
                                                                <asp:DropDownList AutoPostBack="True" runat="server" ID="ddlEmpParmanentDivision" class="form-control form-control-sm" OnSelectedIndexChanged="ddlEmpParmanentDivision_OnSelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Parmanent District</label>
                                                                <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlEmpParmanentDistrict" class="form-control form-control-sm" OnSelectedIndexChanged="ddlEmpParmanentDistrict_OnSelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Parmanent Thana</label>
                                                                <asp:DropDownList runat="server" ID="ddlEmpParmanentThana" class="form-control form-control-sm">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>

                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Parmanent Tel. No</label>
                                                                <asp:TextBox runat="server" ID="txt_EmpParmanentTelNo" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Personal Email</label>
                                                                <asp:TextBox runat="server" ID="txt_EmpPersonalEmail" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Official Email</label>
                                                                <asp:TextBox runat="server" ID="txt_EmpOfficialEmail" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Personal Mobile</label>
                                                                <asp:TextBox runat="server" ID="txt_EmpPersonalMobile" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Official Mobile</label>
                                                                <asp:TextBox runat="server" ID="txt_EmpOfficialMobile" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>

                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Fax</label>
                                                                <asp:TextBox runat="server" ID="txt_EmpFax" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card">
                                                <div class="card-body">
                                                    <div class="form-row">
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Emergency Contact Person</label>
                                                                <asp:TextBox runat="server" ID="txt_EmpEmergencyPerson" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Emergency Contact Address</label>
                                                                <asp:TextBox runat="server" ID="txt_EmpEmergencyAddress" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Emergency Number</label>
                                                                <asp:TextBox runat="server" ID="txt_EmpEmergencyNumber" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>


                                                    </div>
                                                </div>
                                            </div>
                                        </Content>
                                    </ajaxToolkit:AccordionPane>
                                    <ajaxToolkit:AccordionPane ID="AccordionPane4" runat="server">
                                        <%--//--- Heading -----%>
                                        <Header>
                                            <span class="accordionLink">4. Family Information</span>
                                        </Header>
                                        <%--//--- Content -----%>
                                        <Content>
                                            <div class="card">
                                                <div class="card-body">
                                                    <div class="form-row">
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Spouse Name</label>
                                                                <asp:TextBox runat="server" ID="txt_EmpSpouseName" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Spouse's Max Education</label>
                                                                <asp:DropDownList runat="server" ID="ddlEmpSpouseMaxEdu" class="form-control form-control-sm">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Spouse's Occupation</label>
                                                                <asp:DropDownList runat="server" ID="ddlEmpSpouseOccupation" class="form-control form-control-sm">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Spouse's DOB</label>
                                                                <asp:TextBox runat="server" ID="txt_EmpSpouseDOB" class="form-control form-control-sm" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server"
                                                                    Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                    TargetControlID="txt_EmpSpouseDOB" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Marriage Date</label>
                                                                <asp:TextBox runat="server" ID="txt_EmpMarriageDate" class="form-control form-control-sm" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server"
                                                                    Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                    TargetControlID="txt_EmpMarriageDate" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card">
                                                <div class="card-body">
                                                    <div class="form-row">

                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Children Name</label>
                                                                <asp:TextBox runat="server" ID="txt_EmpChildrenName" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Children Gender</label>
                                                                <asp:DropDownList runat="server" ID="ddlEmpChildrenGender" class="form-control form-control-sm">
                                                                    <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                                                                    <asp:ListItem Text="FeMale" Value="FeMale"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Children Occupation</label>
                                                                <asp:DropDownList runat="server" ID="ddlEmpChildrenOccupation" class="form-control form-control-sm">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Children DOB</label>
                                                                <asp:TextBox runat="server" ID="txt_EmpChildrenDOB" class="form-control form-control-sm" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server"
                                                                    Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                    TargetControlID="txt_EmpChildrenDOB" />

                                                            </div>
                                                        </div>

                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Children Marital Status</label>
                                                                <asp:DropDownList runat="server" ID="ddlChildrenMaritalStatus" class="form-control form-control-sm">
                                                                    <asp:ListItem Text="Single" Value="Single"></asp:ListItem>
                                                                    <asp:ListItem Text="Married" Value="Married"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <asp:Button runat="server" ID="btnAddChildren" Text="Add Children" CssClass="btn btn-sm" OnClick="btnAddChildren_OnClick" />
                                                            </div>
                                                        </div>
                                                        <div>
                                                             <div style="overflow: scroll;width: 100%">
                                                            <asp:GridView Width="100%" ID="gv_Children" runat="server" ShowFooter="true" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="SL#">
                                                                        <ItemTemplate>
                                                                            <%#Container.DataItemIndex+1 %>
                                                                            <asp:HiddenField ID="EmpChildrenId" runat="server" Value='<%#Eval("EmpChildrenId") %>' />

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Children Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_ChildrenName" runat="server"   Text='<%#Eval("ChildrenName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Children Gender">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_ChildrenGender" runat="server"   Text='<%#Eval("ChildrenGender") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Children Occupation">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_ChildrenOccupation" runat="server"  Text='<%#Eval("ChildrenOccupationName") %>'></asp:Label>
                                                                            <asp:HiddenField runat="server" ID="hfChildrenOccupation" Value='<%#Eval("ChildrenOccupation") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Children DOB">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_ChildrenDOB" runat="server"   Text='<%#Eval("ChildrenDOB") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Children MaritalStatus">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_ChildrenMaritalStatus" runat="server"   Text='<%#Eval("ChildrenMaritalStatus") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="Remove">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lb_RemoveChildren" runat="server" OnClick="lb_RemoveChildren_OnClick">Remove</asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                                  </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>


                                        </Content>
                                    </ajaxToolkit:AccordionPane>
                                    <ajaxToolkit:AccordionPane ID="AccordionPane5" runat="server">
                                        <%--//--- Heading -----%>
                                        <Header>
                                            <span class="accordionLink">5. Education</span>
                                        </Header>
                                        <%--//--- Content -----%>
                                        <Content>
                                            <div class="card">
                                                <div class="card-body">
                                                    <div class="form-row">
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Name of Education</label>
                                                                <asp:DropDownList runat="server" ID="ddlEducationName" class="form-control form-control-sm">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>

                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Board/University</label>
                                                                <asp:DropDownList runat="server" ID="ddlBoardUniversity" class="form-control form-control-sm">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Subject/Group</label>
                                                                <asp:DropDownList runat="server" ID="ddlSubjectGroup" class="form-control form-control-sm">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Educational Institute</label>
                                                                <asp:DropDownList runat="server" ID="ddlEducationalInstitute" class="form-control form-control-sm">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Field of specialization</label>
                                                                <asp:DropDownList runat="server" ID="ddlSpecialization" class="form-control form-control-sm">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Passing Year</label>
                                                                <asp:TextBox runat="server" ID="txt_PassingYear" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Result</label>
                                                                <asp:TextBox runat="server" ID="txt_Result" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>CGPA/Total Marks</label>
                                                                <asp:TextBox runat="server" ID="txt_CGPAMarks" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>

                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <asp:CheckBox runat="server" ID="chk_EduIsLastLevel" Text="Is Last Level" />
                                                            </div>
                                                        </div>

                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <asp:Button runat="server" ID="btnAddEducation" CssClass="btn btn-sm" Text="Add Education" OnClick="btnAddEducation_OnClick" />
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                            <div>
                                                 <div style="overflow: scroll;">
                                                <asp:GridView Width="100%" ID="gv_Education" runat="server" ShowFooter="true" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL#">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                                <asp:HiddenField ID="EmpEducationId" runat="server" Value='<%#Eval("EmpEducationId") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Education Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_EducationName" runat="server"   Text='<%#Eval("EducationName") %>'></asp:Label>
                                                                <asp:HiddenField ID="EducationNameId" runat="server" Value='<%#Eval("EducationNameId") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Board/University">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_BoardUniversity" runat="server"   Text='<%#Eval("BoardUniversity") %>'></asp:Label>
                                                                <asp:HiddenField ID="BoardUniversityId" runat="server" Value='<%#Eval("BoardUniversityId") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Subject/Group">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_SubjectGroup" runat="server"   Text='<%#Eval("SubjectGroup") %>'></asp:Label>
                                                                <asp:HiddenField ID="SubjectGroupId" runat="server" Value='<%#Eval("SubjectGroupId") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Educational Institute">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_EducationalInstitute" runat="server"   Text='<%#Eval("EducationalInstitute") %>'></asp:Label>
                                                                <asp:HiddenField ID="EducationalInstituteId" runat="server" Value='<%#Eval("EducationalInstituteId") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Field Of Specialization">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_FieldOfSpecialization" runat="server"  Text='<%#Eval("FieldOfSpecialization") %>'></asp:Label>
                                                                <asp:HiddenField ID="FieldOfSpecializationId" runat="server" Value='<%#Eval("FieldOfSpecializationId") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="PassingYear">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_PassingYear" runat="server"   Text='<%#Eval("PassingYear") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Result">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Result" runat="server"   Text='<%#Eval("Result") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="CGPA Or TotalMarks">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_CgpaOrTotalMarks" runat="server"   Text='<%#Eval("CgpaOrTotalMarks") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Is LastLevel">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_EduIsLastLevel" runat="server"   Text='<%#Eval("EduIsLastLevel") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Remove">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lb_RemoveEducation" runat="server" OnClick="lb_RemoveEducation_OnClick">Remove</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                     </div>
                                            </div>

                                        </Content>
                                    </ajaxToolkit:AccordionPane>

                                    <ajaxToolkit:AccordionPane ID="AccordionPane6" runat="server">
                                        <%--//--- Heading -----%>
                                        <Header>
                                            <span class="accordionLink">6. Experience</span>
                                        </Header>
                                        <%--//--- Content -----%>
                                        <Content>
                                            <div class="card">
                                                <div class="card-body">
                                                    <div class="form-row">
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Company/Institute Name</label>
                                                                <asp:TextBox runat="server" ID="txt_ExpCompany" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>

                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Contact person</label>
                                                                <asp:TextBox runat="server" ID="txt_ExpContactPerson" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Address</label>
                                                                <asp:TextBox runat="server" ID="txt_ExpAddress" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Nature of Business</label>
                                                                <asp:TextBox runat="server" ID="txt_ExpNatureofBusiness" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Job Type</label>
                                                                <asp:TextBox runat="server" ID="txt_ExpJobType" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Leaving Salary</label>
                                                                <asp:TextBox runat="server" ID="txt_ExpLeavingSalary" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>From Date</label>
                                                                <asp:TextBox runat="server" ID="txt_ExpFromDate" class="form-control form-control-sm" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender8" runat="server"
                                                                    Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                    TargetControlID="txt_ExpFromDate" />

                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>To Date</label>
                                                                <asp:TextBox runat="server" ID="txt_ExpToDate" class="form-control form-control-sm" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender9" runat="server"
                                                                    Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                    TargetControlID="txt_ExpToDate" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">

                                                                <asp:CheckBox runat="server" ID="chk_ExpLastJob" Text="Last Job" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Designation</label>
                                                                <asp:TextBox runat="server" ID="txt_ExpDesignation" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Job Description</label>
                                                                <asp:TextBox runat="server" ID="txt_ExpJobDescription" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Tel No.</label>
                                                                <asp:TextBox runat="server" ID="txt_ExpTelNo" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Remarks</label>
                                                                <asp:TextBox runat="server" ID="txt_ExpRemarks" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>


                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <asp:Button runat="server" ID="btnAddExperience" Text="Add Experience" CssClass="btn btn-sm" OnClick="btnAddExperience_OnClick" />
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>




                                            <div>
                                                
                                                 <div style="overflow: scroll;">
                                                <asp:GridView Width="100%" ID="gv_Experience" runat="server" ShowFooter="true" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL#">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                                <asp:HiddenField ID="EmpExperienceId" runat="server" Value='<%#Eval("EmpExperienceId") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Company/Institute">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ExpCompany" runat="server"  Text='<%#Eval("ExpCompany") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Contact Person">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ExpContactPerson" runat="server" Text='<%#Eval("ExpContactPerson") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Address">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ExpAddress" runat="server"  Text='<%#Eval("ExpAddress") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Nature of Business">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ExpNatureofBusiness" runat="server"  Text='<%#Eval("ExpNatureofBusiness") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Job Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ExpJobType" runat="server"  Text='<%#Eval("ExpJobType") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Leaving Salary">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ExpLeavingSalary" runat="server"  Text='<%#Eval("ExpLeavingSalary") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="From Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ExpFromDate" runat="server"  Text='<%#Eval("ExpFromDate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="To Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ExpToDate" runat="server"  Text='<%#Eval("ExpToDate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Is Last Job">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ExpLastJob" runat="server"  Text='<%#Eval("ExpLastJob") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Designation">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ExpDesignation" runat="server"  Text='<%#Eval("ExpDesignation") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Job Description">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ExpJobDescription" runat="server"  Text='<%#Eval("ExpJobDescription") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tel No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ExpTelNo" runat="server"  Text='<%#Eval("ExpTelNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ExpRemarks" runat="server"  Text='<%#Eval("ExpRemarks") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderText="Remove">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lb_RemoveExperience" runat="server" OnClick="lb_RemoveExperience_OnClick">Remove</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                     </div>
                                            </div>

                                        </Content>
                                    </ajaxToolkit:AccordionPane>
                                    <ajaxToolkit:AccordionPane ID="AccordionPane7" runat="server">
                                        <%--//--- Heading -----%>
                                        <Header>
                                            <span class="accordionLink">7. Training</span>
                                        </Header>
                                        <%--//--- Content -----%>
                                        <Content>
                                            <div class="card">
                                                <div class="card-body">
                                                    <div class="form-row">
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Training Name</label>
                                                                <asp:TextBox runat="server" class="form-control form-control-sm" ID="txt_TrTrainingName"   />
                                                            </div>
                                                        </div>

                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Training Type</label>
                                                                <asp:DropDownList runat="server" class="form-control form-control-sm" ID="ddlTrTrainingType"  >
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Training Description</label>
                                                                <asp:TextBox runat="server" class="form-control form-control-sm"  ID="txt_TrTrainingDescription"  />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Training Institution</label>
                                                                <asp:DropDownList runat="server" class="form-control form-control-sm"  ID="ddlTrTrainingInstitution"  >
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Training Place</label>
                                                                <asp:TextBox runat="server" class="form-control form-control-sm"  ID="txt_TrTrainingPlace"   />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Training Country</label>
                                                                <asp:DropDownList runat="server" class="form-control form-control-sm"  ID="ddlTrTrainingCountry"  >
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Training Achievment</label>
                                                                <asp:TextBox runat="server" class="form-control form-control-sm"  ID="txt_TrTrainingAchievment"   />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>From Date</label>
                                                                <asp:TextBox runat="server" class="form-control form-control-sm"  ID="txt_TrFromDate" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender12" runat="server"
                                                                    Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                    TargetControlID="txt_TrFromDate" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>To Date</label>
                                                                <asp:TextBox runat="server"  class="form-control form-control-sm" ID="txt_TrToDate"   />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender13" runat="server"
                                                                    Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                    TargetControlID="txt_TrToDate" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Training Days</label>
                                                                <asp:TextBox runat="server" class="form-control form-control-sm"  ID="txt_TrTrainingDays"   />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Remarks</label>
                                                                <asp:TextBox runat="server" class="form-control form-control-sm"  ID="txt_TrRemarks"   />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <asp:Button runat="server" ID="btnAddTraining" Text="Add Training" CssClass="btn btn-sm" OnClick="btnAddTraining_OnClick" />
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                            <div>
                                                
                                                 <div style="overflow: scroll;">
                                                <asp:GridView Width="100%" ID="gv_Training" runat="server" ShowFooter="true" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL#">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                                <asp:HiddenField ID="EmpTrainingId" runat="server" Value='<%#Eval("EmpTrainingId") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Training Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_TrainingName" runat="server"   Text='<%#Eval("TrainingName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Training Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_TrainingType" runat="server"   Text='<%#Eval("TrainingTypeName") %>'></asp:Label>
                                                                <asp:HiddenField runat="server" ID="hfTrainingType" Value='<%#Eval("TrainingType") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Training Description">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_TrainingDescription" runat="server"   Text='<%#Eval("TrainingDescription")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Training Institution">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_TrainingInstitution" runat="server"  Text='<%#Eval("TrainingInstitutionName")%>'></asp:Label>
                                                                <asp:HiddenField runat="server" ID="hfTrainingInstitution" Value='<%#Eval("TrainingInstitution")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Training Place">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_TrainingPlace" runat="server"   Text='<%#Eval("TrainingPlace")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Training Country">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_TrainingCountry" runat="server"   Text='<%#Eval("TrainingCountryName")%>'></asp:Label>
                                                                <asp:HiddenField runat="server" ID="hfTrainingCountry" Value='<%#Eval("TrainingCountry")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Training Achievment">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_TrainingAchievment" runat="server"   Text='<%#Eval("TrainingAchievment")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="From Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_TrFromDate" runat="server"   Text='<%#Eval("TrFromDate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="To Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_TrToDate" runat="server"   Text='<%#Eval("TrToDate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Training Days">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_TrainingDays" runat="server"   Text='<%#Eval("TrainingDays")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_TrRemarks" runat="server"  Text='<%#Eval("TrRemarks")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>




                                                        <asp:TemplateField HeaderText="Remove">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lb_RemoveTraining" runat="server" OnClick="lb_RemoveTraining_OnClick">Remove</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                     </div>
                                            </div>

                                        </Content>
                                    </ajaxToolkit:AccordionPane>

                                    <ajaxToolkit:AccordionPane ID="AccordionPane8" runat="server">
                                        <%--//--- Heading -----%>
                                        <Header>
                                            <span class="accordionLink">8. Reference</span>
                                        </Header>
                                        <%--//--- Content -----%>
                                        <Content>
                                            <div class="card">
                                                <div class="card-body">
                                                    <div class="form-row">
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Reference Name</label>
                                                                <asp:TextBox runat="server" ID="txt_RefReferenceName" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>

                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Occupation</label>
                                                                <asp:DropDownList runat="server" ID="ddlRefOccupation" class="form-control form-control-sm">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Address</label>
                                                                <asp:TextBox runat="server" ID="txt_RefAddress" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Mobile</label>
                                                                <asp:TextBox runat="server" ID="txt_RefMobile" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <asp:Button runat="server" ID="btnAddReference" Text="Add Reference" CssClass="btn btn-sm" OnClick="btnAddReference_OnClick" />
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                            <div>
                                                
                                                 <div style="overflow: scroll;">
                                                <asp:GridView Width="100%" ID="gv_Reference" runat="server" ShowFooter="true" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL#">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex + 1%>
                                                                <asp:HiddenField ID="EmpReferenceId" runat="server" Value='<%#Eval("EmpReferenceId")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Reference Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ReferenceName" runat="server"   Text='<%#Eval("ReferenceName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ref Occupation">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_RefOccupation" runat="server"   Text='<%#Eval("RefOccupationName")%>'></asp:Label>
                                                                <asp:HiddenField runat="server" ID="hfRefOccupation" Value='<%#Eval("RefOccupation")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ref Address">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_RefAddress" runat="server"  Text='<%#Eval("RefAddress")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ref Mobile">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_RefMobile" runat="server"   Text='<%#Eval("RefMobile")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderText="Remove">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lb_RemoveReference" runat="server" OnClick="lb_RemoveReference_OnClick">Remove</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                     </div>
                                            </div>

                                        </Content>
                                    </ajaxToolkit:AccordionPane>

                                    <ajaxToolkit:AccordionPane ID="AccordionPane9" runat="server">
                                        <%--//--- Heading -----%>
                                        <Header>
                                            <span class="accordionLink">9. Nominee</span>
                                        </Header>
                                        <%--//--- Content -----%>
                                        <Content>
                                            <div class="card">
                                                <div class="card-body">
                                                    <div class="form-row">
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Nomination Purpose</label>
                                                                <asp:DropDownList runat="server" ID="ddlNomNominationPurpose" class="form-control form-control-sm">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>

                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Nominee Name</label>
                                                                <asp:TextBox runat="server" ID="txt_NomNomineeName" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Nominee Occupation</label>
                                                                <asp:DropDownList runat="server" ID="ddlNomNomineeOccupation" class="form-control form-control-sm">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Date of Nomination</label>
                                                                <asp:TextBox runat="server" ID="txt_NomDateOfNomination" class="form-control form-control-sm" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender10" runat="server"
                                                                    Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                    TargetControlID="txt_NomDateOfNomination" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Percentage</label>
                                                                <asp:TextBox runat="server" ID="txt_NomNominationPercentage" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Date of Birth</label>
                                                                <asp:TextBox runat="server" ID="txt_NomNomineeDOB" class="form-control form-control-sm" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender11" runat="server"
                                                                    Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                    TargetControlID="txt_NomNomineeDOB" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Relation</label>
                                                                <asp:DropDownList runat="server" ID="ddlNomNomineeRelation" class="form-control form-control-sm">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Phone No.</label>
                                                                <asp:TextBox runat="server" ID="txt_NomNomineeTelephone" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Address</label>
                                                                <asp:TextBox runat="server" ID="txt_NomNomineeAddress" class="form-control form-control-sm" />
                                                            </div>
                                                        </div>


                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <asp:Button runat="server" ID="btnAddNominee" Text="Add Nominee" CssClass="btn btn-sm" OnClick="btnAddNominee_OnClick" />
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                            <div>
                                                  <div style="overflow: scroll;">
                                                <asp:GridView Width="100%" ID="gv_Nominee" runat="server" ShowFooter="true" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL#">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex + 1%>
                                                                <asp:HiddenField ID="EmpNomineeId" runat="server" Value='<%#Eval("EmpNomineeId")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Nomination Purpose">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_NominationPurpose" runat="server"   Text='<%#Eval("NominationPurposeName")%>'></asp:Label>
                                                                <asp:HiddenField runat="server" ID="hfNominationPurpose" Value='<%#Eval("NominationPurpose")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Nominee Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_NomineeName" runat="server"   Text='<%#Eval("NomineeName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date Of Nomination">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_DateOfNomination" runat="server"   Text='<%#Eval("DateOfNomination")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Nomination Percentage">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_NominationPercentage" runat="server"   Text='<%#Eval("NominationPercentage")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Nominee DOB">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_NomineeDOB" runat="server" DataFormatString="{0:dd-MMM-yyyy}" Text='<%#Eval("NomineeDOB")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Nominee Relation">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_NomineeRelation" runat="server"   Text='<%#Eval("NomineeRelationName")%>'></asp:Label>
                                                                <asp:HiddenField runat="server" ID="hfNomineeRelation" Value='<%#Eval("NomineeRelation")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Nominee Occupation">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_NomineeOccupation" runat="server"   Text='<%#Eval("NomineeOccupationName")%>'></asp:Label>
                                                                <asp:HiddenField runat="server" ID="hfNomineeOccupation" Value='<%#Eval("NomineeOccupation")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Nominee Telephone">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_NomineeTelephone" runat="server"   Text='<%#Eval("NomineeTelephone")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Nominee Address">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_NomineeAddress" runat="server"  Text='<%#Eval("NomineeAddress") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderText="Remove">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lb_RemoveNominee" runat="server" OnClick="lb_RemoveNominee_OnClick">Remove</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                      </div>
                                            </div>

                                        </Content>
                                    </ajaxToolkit:AccordionPane>

                                    <ajaxToolkit:AccordionPane ID="AccordionPane10" runat="server">
                                        <%--//--- Heading -----%>
                                        <Header>
                                            <span class="accordionLink">10. Others</span>
                                        </Header>
                                        <%--//--- Content -----%>
                                        <Content>
                                            <div class="card">
                                                <div class="card-body">
                                                    <div class="form-row">
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Extra Curriculam Activities</label>
                                                                <asp:CheckBoxList runat="server" ID="chkExtraCurriculam" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Other Talents</label>
                                                                <asp:CheckBoxList runat="server" ID="chkOtherTalents" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Achievements</label>
                                                                <asp:CheckBoxList runat="server" ID="chkAchievements" />
                                                            </div>
                                                        </div>
                                                        <div class="col-3">
                                                            <div class="form-group">
                                                                <label>Hobby</label>
                                                                <asp:CheckBoxList runat="server" ID="chkHobby" />
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>

                                        </Content>
                                    </ajaxToolkit:AccordionPane>
                                </Panes>
                            </ajaxToolkit:Accordion>
                            <br />
                            <div>
                                <asp:HiddenField runat="server" ID="hdpk" />
                                <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" Text="Submit " CssClass="btn btn-sm btn-info" />
                                <asp:Button ID="cancelButton" Text="Cancel" OnClick="cancelButton_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                            </div>
                        </div>
                    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript">
        $("body").on("click", "#btnUpload", function () {
            $.ajax({
                url: '/FileUploadHandler.ashx',
                type: 'POST',
                data: new FormData($('form')[0]),
                cache: false,
                contentType: false,
                processData: false,
                success: function (file) {
                    $("#fileProgress").hide();
                    //$("#lblMessage").html("<b>" + file.name + "</b> has been uploaded.");
                    $("#lblMessage").html("Image has been uploaded.");
                    //console.log(file);
                    $('#cpFormBody_img_emp').prop({ src: '/UploadImg/' + file.dbfilename });
                    $('#cpFormBody_hfempimg').val(file.dbfilename);
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
        });

        //$("body").on("click", "#btnSignatureUpload", function () {
        //    $.ajax({
        //        url: '/FileUploadHandler.ashx',
        //        type: 'POST',
        //        data: new FormData($('form')[0]),
        //        cache: false,
        //        contentType: false,
        //        processData: false,
        //        success: function (file) {
        //            $("#SignaturefileProgress").hide();
        //            $("#lblMessageSignature").html("Signature has been uploaded.");
        //            //console.log(file);
        //            $('#cpFormBody_SignatureImage').prop({ src: '/UploadImg/' + file.dbfilename });
        //            $('#cpFormBody_hfSignature').val(file.dbfilename);
        //        },
        //        xhr: function () {
        //            var fileXhr = $.ajaxSettings.xhr();
        //            if (fileXhr.upload) {
        //                $("SignaturefileProgress").show();
        //                fileXhr.upload.addEventListener("SignaturefileProgress", function (e) {
        //                    if (e.lengthComputable) {
        //                        $("#SignaturefileProgress").attr({
        //                            value: e.loaded,
        //                            max: e.total
        //                        });
        //                    }
        //                }, false);
        //            }
        //            return fileXhr;
        //        }
        //    });
        //});
    </script>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="MeetingStatusList.aspx.cs" Inherits="MeetingMinors_MeetingStatusList" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
    
    <!-- Flatpickr for Date Selection -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css">
    <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    <style>
        .btn-add-new {
            background-color: #3b597b !important;
            color: #ffffff !important;
            border: none !important;
            padding: 8px 22px !important;
            font-size: 14px !important;
            font-weight: 700 !important;
            border-radius: 6px !important;
            letter-spacing: 0.5px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.15);
            transition: all 0.2s ease;
            cursor: pointer;
        }
        .btn-add-new:hover {
            background-color: #2c435e !important;
            color: #ffffff !important;
        }

        .form-label-custom {
            font-size: 13px !important;
            font-weight: 700 !important;
            color: #2c3e50 !important;
            margin-bottom: 4px;
        }

        .custom-input {
            border-radius: 5px !important;
            border: 1px solid #cbd5e1 !important;
            height: 38px !important;
            font-size: 13.5px !important;
            padding: 6px 12px !important;
        }
        .custom-input:focus {
            border-color: #3b597b !important;
            box-shadow: 0 0 0 3px rgba(59, 89, 123, 0.15) !important;
        }

        .chkChoiceCustom label {
            font-size: 13px !important;
            font-weight: 500 !important;
            color: #333 !important;
            margin-left: 5px;
            cursor: pointer;
        }
        .chkChoiceCustom input[type="checkbox"] {
            transform: scale(1.1);
            cursor: pointer;
        }

        /* Key Search Card Design */
        .key-search-card {
            background: #ffffff;
            border: 1px solid #dcdfe6;
            border-radius: 12px;
            padding: 16px 20px;
            box-shadow: 0 2px 8px rgba(0,0,0,0.04);
            margin-top: 10px;
            margin-bottom: 20px;
        }

        .key-search-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 12px;
        }

        .key-search-title {
            font-size: 13px;
            font-weight: 700;
            color: #2c3e50;
            letter-spacing: 0.5px;
        }

        .btn-deep-scan {
            background-color: #eaf2f8;
            color: #2980b9;
            border: 1px solid #b3d4ec;
            font-size: 12px;
            font-weight: 600;
            padding: 4px 12px;
            border-radius: 20px;
            display: inline-flex;
            align-items: center;
        }

        .key-search-input-box {
            position: relative;
            display: flex;
            align-items: center;
        }

        .key-search-input-box .key-icon {
            position: absolute;
            left: 12px;
            color: #95a5a6;
            font-size: 14px;
            pointer-events: none;
        }

        .key-search-input-box .key-textbox {
            padding-left: 38px !important;
            padding-right: 70px !important;
            height: 40px !important;
            border-radius: 8px !important;
            border: 1px solid #cbd5e1 !important;
            font-size: 13.5px !important;
        }

        .key-search-input-box .pdf-badge {
            position: absolute;
            right: 12px;
            background-color: #fdf2f2;
            color: #e74c3c;
            border: 1px solid #f9d5d5;
            font-size: 11px;
            font-weight: 700;
            padding: 2px 8px;
            border-radius: 4px;
            pointer-events: none;
        }

        /* Buttons */
        .btn-search-custom {
            background-color: #2980b9 !important;
            color: #ffffff !important;
            border: none !important;
            padding: 8px 24px !important;
            font-size: 13.5px !important;
            font-weight: 600 !important;
            border-radius: 5px !important;
            box-shadow: 0 2px 4px rgba(41, 128, 185, 0.2);
        }
        .btn-search-custom:hover {
            background-color: #1f6694 !important;
            color: #ffffff !important;
        }

        .btn-reset-custom {
            background-color: #f39c12 !important;
            color: #ffffff !important;
            border: none !important;
            padding: 8px 24px !important;
            font-size: 13.5px !important;
            font-weight: 600 !important;
            border-radius: 5px !important;
            box-shadow: 0 2px 4px rgba(243, 156, 18, 0.2);
        }
        .btn-reset-custom:hover {
            background-color: #d68910 !important;
            color: #ffffff !important;
        }

        /* GridView Styling */
        .AddToListCssTable {
            width: 100% !important;
            border-collapse: separate !important;
            border-spacing: 0 !important;
            border: 1px solid #cbd5e1 !important;
            border-radius: 6px !important;
            overflow: hidden !important;
            margin-top: 15px;
        }
        .AddToListCssTable th {
            background: linear-gradient(180deg, #38678f 0%, #294f71 100%) !important;
            color: #ffffff !important;
            font-size: 13.5px !important;
            font-weight: 700 !important;
            padding: 10px 14px !important;
            text-align: left !important;
            border: none !important;
            border-right: 1px solid #1f3d58 !important;
        }
        .AddToListCssTable th:last-child {
            border-right: none !important;
        }
        .AddToListCssTable td {
            padding: 9px 14px !important;
            font-size: 13px !important;
            color: #333333 !important;
            border-bottom: 1px solid #e2e8f0 !important;
            vertical-align: middle !important;
            background-color: #ffffff;
        }
        .AddToListCssTable tr:hover td {
            background-color: #f8fafc !important;
        }
    </style>

    <div class="content" id="content">
        <div class="container-fluid">
            <div class="page-heading mb-3">
                <div class="page-heading__container">
                    <h1 class="title" style="font-size: 18px; font-weight: 600; color: #333; display: flex; align-items: center; gap: 8px;">
                        <img src="../Report_Pages/app.png" width="22px" height="22px" alt="" />
                        Minutes/Notice
                    </h1>
                </div>
                <div class="page-heading__container float-right">
                    <asp:Button ID="AddNewButton" Text="ADD NEW" CssClass="btn-add-new" runat="server" OnClick="AddNewButton_OnClick" />
                </div>
            </div>

            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <asp:UpdateProgress ID="UpdateProgress4" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                        <ProgressTemplate>
                            <div class="divWaiting">
                                <asp:Image ID="imgWait11" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>

                    <div class="card" style="border: 1px solid #e2e8f0; border-radius: 8px;">
                        <div class="card-body" style="padding: 24px;">

                            <!-- Hidden controls to ensure code-behind compatibility -->
                            <div style="display: none;">
                                <asp:RadioButtonList runat="server" ID="rblSearchSection" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="All" Selected="True">All</asp:ListItem>
                                    <asp:ListItem Value="Meeting">Meeting</asp:ListItem>
                                    <asp:ListItem Value="Agenda">Agenda</asp:ListItem>
                                    <asp:ListItem Value="Minutes">Minutes</asp:ListItem>
                                    <asp:ListItem Value="Approval">Approval</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:DropDownList runat="server" ID="ddlCreatedBy" />
                                <asp:DropDownList runat="server" ID="ddlClassificationSearch" />
                                <asp:DropDownList runat="server" ID="ddlMeetingStatus" />
                                <asp:DropDownList runat="server" ID="ddlApprovalStatus" />
                                <asp:DropDownList runat="server" ID="ddlImplementationStatus" />
                                <asp:ListBox runat="server" ID="ddlMemberEmployeeId" SelectionMode="Multiple" />
                                <asp:TextBox runat="server" ID="txtCreatedDate" />
                                <asp:TextBox runat="server" ID="txtToDate" />
                            </div>

                            <!-- Row 1: Company & Meeting Category -->
                            <div class="row mb-3">
                                <div class="col-md-6">
                                    <label class="form-label-custom">Company</label>
                                    <asp:DropDownList runat="server" ID="ddlCompany" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" CssClass="form-control form-control-sm custom-input" />
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label-custom">Meeting Category</label>
                                    <asp:DropDownList runat="server" ID="ddlCategorySearch" CssClass="form-control form-control-sm custom-input" />
                                </div>
                            </div>

                            <!-- Row 2: Meeting Title & Meeting Number -->
                            <div class="row mb-3">
                                <div class="col-md-6">
                                    <label class="form-label-custom">Meeting Title</label>
                                    <asp:TextBox runat="server" ID="txtTitle" CssClass="form-control form-control-sm custom-input" />
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label-custom">Meeting Number</label>
                                    <asp:TextBox runat="server" ID="txtMeetingNo" CssClass="form-control form-control-sm custom-input" />
                                </div>
                            </div>

                            <!-- Row 3: Meeting Date (From & To) & Entry Type -->
                            <div class="row mb-3">
                                <div class="col-md-8">
                                    <label class="form-label-custom mb-1">Meeting Date:</label>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label class="small text-muted mb-1" style="font-weight: 500;">From</label>
                                            <asp:TextBox runat="server" ID="txtMeetingDateFrom" placeholder="dd-mm-yyyy" CssClass="form-control form-control-sm custom-input flatpickr-date" />
                                        </div>
                                        <div class="col-md-6">
                                            <label class="small text-muted mb-1" style="font-weight: 500;">To</label>
                                            <asp:TextBox runat="server" ID="txtMeetingDateTo" placeholder="dd-mm-yyyy" CssClass="form-control form-control-sm custom-input flatpickr-date" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <label class="form-label-custom mb-1">Entry Type</label>
                                    <div class="mt-2">
                                        <div class="custom-control custom-checkbox mb-1">
                                            <asp:CheckBox runat="server" ID="chkEntryTypeNotice" Text="Notice" CssClass="chkChoiceCustom" />
                                        </div>
                                        <div class="custom-control custom-checkbox">
                                            <asp:CheckBox runat="server" ID="chkEntryTypeMinutes" Text="Minutes" CssClass="chkChoiceCustom" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <!-- KEY SEARCH Section -->
                            <div class="row mb-3">
                                <div class="col-md-10 offset-md-1">
                                    <div class="key-search-card">
                                        <div class="key-search-header">
                                            <span class="key-search-title">
                                                <i class="fa fa-search mr-1"></i> KEY SEARCH
                                            </span>
                                            <button type="button" class="btn-deep-scan">
                                                <i class="fa fa-pencil mr-1"></i> Attachment Deep Scan
                                            </button>
                                        </div>
                                        <div class="key-search-input-box">
                                            <i class="fa fa-keyboard-o key-icon"></i>
                                            <asp:TextBox runat="server" ID="txtKeySearch" CssClass="form-control key-textbox" placeholder="Search text inside PDF" />
                                            <span class="pdf-badge">
                                                <i class="fa fa-file-pdf-o mr-1"></i>PDF
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <!-- Action Buttons: Search & Reset -->
                            <div class="row justify-content-center mb-4">
                                <div class="col-auto d-flex gap-2">
                                    <asp:LinkButton runat="server" ID="btn_Search" OnClick="btn_Search_OnClick" CssClass="btn btn-search-custom">
                                        <i class="fa fa-search mr-1"></i> Search
                                    </asp:LinkButton>
                                    &nbsp;
                                    <asp:LinkButton runat="server" ID="lbReset" OnClick="lbReset_OnClick" CssClass="btn btn-reset-custom">
                                        <i class="fa fa-refresh mr-1"></i> Reset
                                    </asp:LinkButton>
                                </div>
                            </div>

                            <script type="text/javascript">
                                function pageLoad() {
                                    $('.flatpickr-date').flatpickr({
                                        dateFormat: "d-m-Y",
                                        altInput: true,
                                        altFormat: "d-m-Y"
                                    });
                                }
                            </script>

                            <!-- GridView Data Table -->
                            <asp:GridView Width="100%" ShowHeader="True" ID="gv_ViewList" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable" OnPreRender="gv_DocumentUpload_PreRender">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex + 1%>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Company">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_ShortName" runat="server" Text='<%#Eval("ShortName")%>'></asp:Label>
                                            <asp:HiddenField runat="server" ID="hfMeetingInfoID" Value='<%#Eval("MeetingInfoID")%>' />
                                            <asp:HiddenField runat="server" ID="hfActionStatus" Value='<%#Eval("ActionStatus")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Category">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_MeetingCategory" runat="server" Text='<%#Eval("MeetingCategory") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Title">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Title" runat="server" Text='<%#Eval("Title") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="MeetingNo" HeaderText="Meeting Number" />

                                    <asp:BoundField DataField="MeetingDate" HeaderText="Meeting Date" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}" />

                                    <asp:TemplateField HeaderText="Entry Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_EntryType" runat="server" Text='<%# (Eval("IsNotice") != DBNull.Value && Convert.ToBoolean(Eval("IsNotice"))) ? "Notice" : "Minutes" %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Actions">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="btnEdit" OnClick="btnEdit_OnClick" CssClass="btn btn-sm btn-warning"  ><i class="fa fa-pencil-square-o"></i> </asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="btnRemove" OnClick="btnRemove_OnClick" CssClass="btn btn-sm btn-danger"><i class="fa fa-minus-circle"></i> </asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="btnView" OnClick="btnView_OnClick" CssClass="btn btn-sm btn-success"><i class="fa fa-eye"></i> </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeInformationReport.aspx.cs" Inherits="Report_UI_EmployeeInformationReport" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
    
    <style type="text/css">
        /*AutoComplete flyout */
      
    </style>
    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=40);
            opacity: 0.4;
        }

        .modalPopup {
            background-color: #FFFFFF;
            width: 300px;
            border: 3px solid #0DA9D0;
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


    <style>
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
            text-decoration: none;
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



        .chkChoice label {
            padding-left: 10px;
            padding-right: 30px;
        }

        .chkChoiceStep label {
            padding-left: 10px;
            padding-right: 15px;
        }

        .chkChoiceDesignation label {
            padding-left: 10px;
            padding-right: 8px;
        }

        .chkChoiceHeader label {
            padding-left: 10px;
            padding-right: 40px;
            font-size: 13px;
        }


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


        .AspNet-TreeView {
            width: 200px;
            border-top: solid 1px #DDD;
        }

            .AspNet-TreeView ul {
                list-style: none;
            }

        .AspNet-TreeView-Leaf {
            border-bottom: solid 1px #DDD;
            background: url(../../images/structure/node-dot.gif) 8px 9px no-repeat;
        }

        .AspNet-TreeView-Root {
            border-bottom: solid 1px #DDD;
        }

            .AspNet-TreeView-Root a {
                display: block;
                width: 170px;
                margin-left: 20px;
                padding: 5px 5px 5px 5px;
            }

        .AspNet-TreeView-Selected {
            background: #F6F6F6 url(../Assets/arrow-right.png) 8px 9px no-repeat;
        }

        .AspNet-TreeView-Expand {
            display: block;
            float: left;
            margin: 9px 0px 0px 8px;
            padding: 6px 4px 5px 4px;
            height: 0px !important;
            background: url(../Assets/plus-sign.png) 0px 0px no-repeat;
            cursor: pointer;
        }

        .AspNet-TreeView-Collapse {
            display: block;
            float: left;
            margin: 9px 0px 0px 8px;
            padding: 6px 4px 5px 4px;
            height: 0px !important;
            background: url(../Assets/minus-sign.png) 0px 0px no-repeat;
            cursor: pointer;
        }

        .AspNet-TreeView-Show li {
            border-top: solid 1px #DDD;
            background-position: 28px 9px;
        }

        .AspNet-TreeView-Hide {
            display: none;
        }

        .AspNet-TreeView ul li ul li {
            text-indent: 20px;
            border-bottom: none;
            font-size: 11px;
        }

        .treeNode {
            color: black;
            padding-left: 10px;
            padding-right: 10px;
            padding-top: 10px;
        }

        .rootNode {
            width: 100%;
            padding-left: 10px;
            padding-right: 10px;
            padding-top: 10px;
        }

        .leafNode {
            padding-left: 10px;
            padding-right: 10px;
            background-color: #eeeeee;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">


    <div class="content" id="content" style="background-color: white">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>

                <div class="container-fluid" style="background-color: white">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;">Employee Information Report</h1>
                        </div>
                        <%--<div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="detailsViewButton" Text="List Information " CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>--%>
                    </div>

                    <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                        <ProgressTemplate>
                            <div class="divWaiting">
                                <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>

                    <div class="card">
                        <div class="card-body" style="background-color: white">

                            <div class="row">
                                <div class="col-md-12">
                                    <fieldset class="for-panel">
                                        <legend>Report Header</legend>

                                        <div class="row">


                                            <div class="col-md-12">
                                                <asp:CheckBoxList ID="cblHeader" RepeatDirection="Vertical" OnSelectedIndexChanged="cblHeader_OnSelectedIndexChanged" RepeatColumns="6" CssClass="chkChoiceHeader" runat="server">
                                                    <asp:ListItem>Company Name</asp:ListItem>
                                                    <asp:ListItem>Employee ID</asp:ListItem>
                                                    <asp:ListItem>Employee Old ID</asp:ListItem>
                                                    <asp:ListItem>Employee Name</asp:ListItem>
                                                    <asp:ListItem>Designation</asp:ListItem>
                                                    <asp:ListItem>Department</asp:ListItem>
                                                    <asp:ListItem>Division</asp:ListItem>
                                                    <asp:ListItem>Wing</asp:ListItem>
                                                    <asp:ListItem>Section</asp:ListItem>
                                                    <asp:ListItem>Sub-Section</asp:ListItem>
                                                    <asp:ListItem>Category</asp:ListItem>
                                                    <asp:ListItem>Grade</asp:ListItem>
                                                    <asp:ListItem>Step</asp:ListItem>
                                                    <asp:ListItem>Office</asp:ListItem>
                                                    <asp:ListItem>Place</asp:ListItem>
                                                    <asp:ListItem>Date of Birth</asp:ListItem>
                                                    <asp:ListItem>Nationality</asp:ListItem>
                                                    <asp:ListItem>Passport</asp:ListItem>



                                                    <asp:ListItem>Blood Group</asp:ListItem>
                                                    <asp:ListItem>Gender</asp:ListItem>
                                                    <asp:ListItem>Religion</asp:ListItem>
                                                    <asp:ListItem>Place of Birth</asp:ListItem>
                                                    <asp:ListItem>Present Address</asp:ListItem>
                                                    <asp:ListItem>Permanent Address</asp:ListItem>
                                                    <asp:ListItem>Present Division</asp:ListItem>
                                                    <asp:ListItem>Permanent Division</asp:ListItem>

                                                    <asp:ListItem>Present District</asp:ListItem>
                                                    <asp:ListItem>Permanent District</asp:ListItem>

                                                    <asp:ListItem>Present Thana</asp:ListItem>
                                                    <asp:ListItem>Permanent Thana</asp:ListItem>
                                                    <asp:ListItem>Date of Joining</asp:ListItem>
                                                    <asp:ListItem>Confirmation Date</asp:ListItem>
                                                    <asp:ListItem>Service Length</asp:ListItem>
                                                    <asp:ListItem>Date of Retirement</asp:ListItem>
                                                    <asp:ListItem>Probition End Date</asp:ListItem>
                                                    <asp:ListItem>Contractual  End Date</asp:ListItem>



                                                </asp:CheckBoxList>

                                            </div>
                                        </div>
                                </div>
                            </div>

                            <div class="form-row">
                                <div class="col-md-12">
                                    <fieldset class="for-panel">
                                        <legend>Filtering Criteria </legend>
                                        <div class="row" runat="server" visible="false">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label></label>
                                                    <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True" RepeatDirection="Horizontal" Enabled="False">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">

                                            <div class="col-2">
                                                <div class="form-group ">
                                                    <label class="control-label">Company </label>
                                                    <asp:DropDownList runat="server" ID="ddlCompany" class="form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" />
                                                </div>
                                            </div>

                                            <div class="col-3" runat="server" Visible="False">
                                                <div class="form-group">
                                                    <label>Employee No </label>
                                                    <asp:TextBox runat="server" ID="empNoTextBox" class="form-control form-control-sm" />

                                                </div>
                                            </div>

                                            <div class="col-3">
                                                <div class="form-group ">
                                                    <label>Employee Name </label>
                                                    <asp:TextBox ID="SearchEmployeeNameTextBoxTextBox" AutoPostBack="True" runat="server" class="form-control form-control-sm" OnTextChanged="SearchEmployeeNameTextBoxTextBox_OnTextChanged"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                        EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                        ServiceMethod="GetCompanyWiseEmployeeInfoOnlyForPromotion" ServicePath="~/WebService.asmx" TargetControlID="SearchEmployeeNameTextBoxTextBox"
                                                        UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                        ShowOnlyCurrentWordInCompletionListItem="true">
                                                    </cc1:AutoCompleteExtender>
                                                    <asp:HiddenField ID="repEmpIdHiddenField" runat="server" />

                                                </div>
                                            </div>

                                        </div>
                                      

                                        <div class="row" runat="server" visible="False">



                                            <div class="col-md-6">
                                                <label>&nbsp;&nbsp;&nbsp; </label>
                                                <asp:TextBox ID="gradeTextBox" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                            </div>



                                            <div class="col-md-1">
                                                <br />
                                                <label style="margin-top: 2px;">&nbsp;&nbsp;</label>
                                                <asp:Button ID="Button4" runat="server" CssClass=" btn btn-sm btn-outline-info" OnClick="Button4_OnClick" Text=" . . . " />
                                            </div>

                                            <div class="col-md-1">
                                                <br />
                                                <label>&nbsp;&nbsp; </label>
                                                <asp:Button ID="Button5" runat="server" CssClass="btn btn-sm btn-outline-success" OnClick="Button5_OnClick" Text="Clear" />
                                            </div>


                                        </div>

                                          <div class="row">
                                            <div class="col-md-4">


                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <div class="form-group">
                                                                <label>Heirarchical Position </label>

                                                                <asp:DropDownList ID="hierchicalDropDownList" class="form-control form-control-sm" runat="server">
                                                                    <asp:ListItem> In </asp:ListItem>
                                                                    <asp:ListItem> Not In </asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="Label_Title">Heirarchical Position List</div>
                                                        <div class="form-group">
                                                            <div style="overflow: scroll; height: 150px">

                                                                <br />
                                                                <asp:TreeView ID="heirerchicalTreeView" NodeStyle-CssClass="treeNode"
                                                                    RootNodeStyle-CssClass="rootNode"
                                                                    LeafNodeStyle-CssClass="leafNode"
                                                                    ExpandImageUrl="../Assets/plus-sign.png" CollapseImageUrl="../Assets/minus-sign.png" runat="server" ShowCheckBoxes="All" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                            
                                             <div class="col-md-3">


                                                        <div class="row">
                                                            <div class="col-md-8">
                                                                <div class="form-group">
                                                                    <label>Religion </label>

                                                                    <asp:DropDownList ID="religionDropDownList1" class="form-control form-control-sm" runat="server">
                                                                        <asp:ListItem> In </asp:ListItem>
                                                                        <asp:ListItem> Not In </asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="Label_Title">Religion List</div>
                                                                <div class="form-group">
                                                                    <div style="overflow: scroll; height: 150px">

                                                                        <br />
                                                                        <asp:CheckBoxList ID="religionCheckBoxList" CssClass="chkChoice" RepeatColumns="1" RepeatDirection="Horizontal" runat="server">
                                                                            <%--<asp:ListItem Text="Select..." Value="-1"></asp:ListItem>--%>
                                                                            <asp:ListItem Text="Islam" Value="Islam"></asp:ListItem>
                                                                            <asp:ListItem Text="Hindu" Value="Hindu"></asp:ListItem>
                                                                            <asp:ListItem Text="Christian" Value="Christian"></asp:ListItem>
                                                                              <asp:ListItem Text="Buddhist" Value="Buddhist"></asp:ListItem>
                                                                            <asp:ListItem Text="Others" Value="Others"></asp:ListItem>
                                                                        </asp:CheckBoxList>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>

                                                    <div class="col-md-3">


                                                        <div class="row">
                                                            <div class="col-md-8">
                                                                <div class="form-group">
                                                                    <label>Blood Group </label>

                                                                    <asp:DropDownList ID="bloodGroupDropDownList" class="form-control form-control-sm" runat="server">
                                                                        <asp:ListItem> In </asp:ListItem>
                                                                        <asp:ListItem> Not In </asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="Label_Title">Blood Group List</div>
                                                                <div class="form-group">
                                                                    <div style="overflow: scroll; height: 150px">

                                                                        <br />
                                                                        <asp:CheckBoxList ID="bloodgroupCheckBoxList" RepeatColumns="2" RepeatDirection="Horizontal" CssClass="chkChoice" runat="server">
                                                                            <asp:ListItem Text="Unknown" Value="Unknown"></asp:ListItem>
                                                                            <asp:ListItem Text="A+" Value="A+"></asp:ListItem>
                                                                            <asp:ListItem Text="A-" Value="A-"></asp:ListItem>
                                                                            <asp:ListItem Text="B+" Value="B+"></asp:ListItem>
                                                                            <asp:ListItem Text="B-" Value="B-"></asp:ListItem>
                                                                            <asp:ListItem Text="O+" Value="O+"></asp:ListItem>
                                                                            <asp:ListItem Text="O-" Value="O-"></asp:ListItem>
                                                                            <asp:ListItem Text="AB+" Value="AB+"></asp:ListItem>
                                                                            <asp:ListItem Text="AB-" Value="AB-"></asp:ListItem>
                                                                        </asp:CheckBoxList>


                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>



                                                    <div class="col-md-2">


                                                        <div class="row">
                                                            <div class="col-md-8">
                                                                <div class="form-group">
                                                                    <label>Gender </label>

                                                                    <asp:DropDownList ID="genderDropDownList" class="form-control form-control-sm" runat="server">
                                                                        <asp:ListItem> In </asp:ListItem>
                                                                        <asp:ListItem> Not In </asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="Label_Title">Gender List</div>
                                                                <div class="form-group">
                                                                    <div style="overflow: scroll; height: 150px">

                                                                        <br />
                                                                        <asp:CheckBoxList ID="genderCheckBoxList" CssClass="chkChoice" runat="server">
                                                                            <%--<asp:ListItem Text="Select..." Value="-1"></asp:ListItem>--%>
                                                                            <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                                                                            <asp:ListItem Text="Female" Value="FeMale"></asp:ListItem>
                                                                        </asp:CheckBoxList>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">


                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>Grade </label>

                                                            <asp:DropDownList ID="gradeDropDownList" class="form-control form-control-sm" runat="server">
                                                                <asp:ListItem> In </asp:ListItem>
                                                                <asp:ListItem> Not In </asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="Label_Title">Grade List</div>
                                                        <div class="form-group">
                                                            <div style="overflow: scroll; height: 150px">

                                                                <br />
                                                                <asp:CheckBoxList ID="gradeCheckBoxList" AutoPostBack="True" OnSelectedIndexChanged="gradeCheckBoxList_OnSelectedIndexChanged" CssClass="chkChoice" RepeatColumns="5" RepeatDirection="Horizontal" runat="server"></asp:CheckBoxList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="col-md-6">


                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>Step </label>

                                                            <asp:DropDownList ID="stepDropDownList" class="form-control form-control-sm" runat="server">
                                                                <asp:ListItem> In </asp:ListItem>
                                                                <asp:ListItem> Not In </asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="Label_Title">Step List</div>
                                                        <div class="form-group">
                                                            <div style="overflow: scroll; height: 150px">

                                                                <br />
                                                                <asp:CheckBoxList ID="stepCheckBoxList" CssClass="chkChoiceStep" RepeatColumns="4" RepeatDirection="Horizontal" runat="server"></asp:CheckBoxList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>


                                        <div class="row" runat="server" visible="False">
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                </div>
                                            </div>


                                            <div class="col-md-6">
                                                <label>&nbsp;&nbsp;&nbsp; </label>

                                                <asp:TextBox ID="stepTextBox" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                            </div>



                                            <div class="col-md-1">
                                                <br />
                                                <label>&nbsp;&nbsp; </label>
                                                <asp:Button ID="Button6" runat="server" CssClass=" btn btn-sm btn-outline-info" OnClick="Button6_OnClick" Text=" . . . " />
                                            </div>
                                            <div class="col-md-1">
                                                <br />
                                                <label>&nbsp;&nbsp; </label>
                                                <asp:Button ID="Button7" runat="server" OnClick="Button7_OnClick" CssClass="btn btn-sm btn-outline-success" Text="Clear" />
                                            </div>
                                        </div>


                                        <div class="row" runat="server" visible="False">
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Heirarchical Position </label>

                                                    <asp:DropDownList ID="DropDownList3" class="form-control form-control-sm" runat="server">
                                                        <asp:ListItem> In </asp:ListItem>
                                                        <asp:ListItem> Not In </asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-md-2">
                                                <label>Division: </label>
                                                <asp:DropDownList ID="NewDivisionDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="NewDivisionDropDownList_OnSelectedIndexChanged"></asp:DropDownList>
                                            </div>

                                            <div class="col-md-2" runat="server" id="wing">
                                                <label>Wing: </label>
                                                <asp:DropDownList ID="NewWingDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="NewWingDropDownList_OnSelectedIndexChanged"></asp:DropDownList>
                                            </div>

                                            <div class="col-md-2" runat="server" id="dept">
                                                <label>Department: </label>

                                                <asp:DropDownList ID="NewDepartmentDropDownList1" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="NewDepartmentDropDownList1_OnSelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-2" runat="server" id="sec">
                                                <label>Section: </label>
                                                <asp:DropDownList ID="NewSectionDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="NewSectionDropDownList_OnSelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-2" runat="server" id="subsec">
                                                <label>Sub-Section: </label>
                                                <asp:DropDownList ID="NewSubSectionDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="NewSubSectionDropDownList_OnSelectedIndexChanged"></asp:DropDownList>
                                            </div>


                                        </div>

                                      

                                        <div class="row" runat="server" visible="False">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-6">
                                                <label>&nbsp;&nbsp;&nbsp; </label>

                                                <asp:TextBox ID="hierchicalSelectedTextBox" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                            </div>



                                            <div class="col-md-1">
                                                <br />
                                                <label>&nbsp;&nbsp; </label>
                                                <asp:Button ID="heirchichalButton" runat="server" CssClass=" btn btn-sm btn-outline-info" OnClick="heirchichalButton_OnClick" Text=" . . . " />
                                            </div>
                                            <div class="col-md-1">
                                                <br />
                                                <label>&nbsp;&nbsp; </label>
                                                <asp:Button ID="Button1" runat="server" OnClick="Button1_OnClick" CssClass="btn btn-sm btn-outline-success" Text="Clear" />
                                            </div>

                                        </div>
                                        
                                        
                                            <div class="row">
                                                    <div class="col-md-6">


                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <label>Permanent Dist </label>

                                                                    <asp:DropDownList ID="permDistDropDownList" class="form-control form-control-sm" runat="server">
                                                                        <asp:ListItem> In </asp:ListItem>
                                                                        <asp:ListItem> Not In </asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="Label_Title">District List</div>
                                                                <div class="form-group">
                                                                    <div style="overflow: scroll; height: 150px">

                                                                        <br />
                                                                        <asp:CheckBoxList ID="permDistCheckBoxList" AutoPostBack="True" OnSelectedIndexChanged="permDistCheckBoxList_OnSelectedIndexChanged" RepeatColumns="4" RepeatDirection="Horizontal" CssClass="chkChoiceStep" runat="server">
                                                                        </asp:CheckBoxList>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>


                                                    <div class="col-md-6">


                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <label>Permanent Thana </label>

                                                                    <asp:DropDownList ID="permThanaDropDownList" class="form-control form-control-sm" runat="server">
                                                                        <asp:ListItem> In </asp:ListItem>
                                                                        <asp:ListItem> Not In </asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="Label_Title">Thana List</div>
                                                                <div class="form-group">
                                                                    <div style="overflow: scroll; height: 150px">

                                                                        <br />
                                                                        <asp:CheckBoxList ID="permThanaCheckBoxList1" RepeatColumns="3" RepeatDirection="Horizontal" CssClass="chkChoiceStep" runat="server">
                                                                        </asp:CheckBoxList>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>


                                        <div class="row">
                                            <div class="col-md-6">


                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>Present Office </label>

                                                            <asp:DropDownList ID="presentOfcDropDownList4" class="form-control form-control-sm" runat="server">
                                                                <asp:ListItem> In </asp:ListItem>
                                                                <asp:ListItem> Not In </asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="Label_Title">Office List</div>
                                                        <div class="form-group">
                                                            <div style="overflow: scroll; height: 150px">

                                                                <br />

                                                                <asp:CheckBoxList ID="salLocCheckBoxList" AutoPostBack="True" OnSelectedIndexChanged="gradeCheckBoxList_OnSelectedIndexChanged" CssClass="chkChoiceStep" RepeatColumns="3" RepeatDirection="Horizontal" runat="server"></asp:CheckBoxList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>


                                            <div class="col-md-6">


                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>Designation </label>

                                                            <asp:DropDownList ID="desigDropDownList" class="form-control form-control-sm" runat="server">
                                                                <asp:ListItem> In </asp:ListItem>
                                                                <asp:ListItem> Not In </asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="Label_Title">Designation List</div>
                                                        <div class="form-group">
                                                            <div style="overflow: scroll; height: 150px">

                                                                <br />

                                                                <asp:CheckBoxList ID="desigCheckBoxList" CssClass="chkChoiceDesignation" RepeatColumns="2" RepeatDirection="Horizontal" runat="server"></asp:CheckBoxList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="row" runat="server" visible="False">

                                            <div class="col-md-2">
                                                <div class="form-group">
                                                </div>
                                            </div>


                                            <div class="col-md-6">
                                                <label>&nbsp;&nbsp;&nbsp; </label>

                                                <asp:TextBox ID="presentOfcTextBox" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                            </div>



                                            <div class="col-md-1">
                                                <br />
                                                <label>&nbsp;&nbsp; </label>
                                                <asp:Button ID="Button12" runat="server" CssClass=" btn btn-sm btn-outline-info" OnClick="Button12_OnClick" Text=" . . . " />
                                            </div>
                                            <div class="col-md-1">
                                                <br />
                                                <label>&nbsp;&nbsp; </label>
                                                <asp:Button ID="Button13" runat="server" OnClick="Button13_OnClick" CssClass="btn btn-sm btn-outline-success" Text="Clear" />
                                            </div>






                                        </div>
                                        <div class="row" runat="server" visible="False">

                                            <div class="col-md-2">
                                                <div class="form-group">
                                                </div>
                                            </div>


                                            <div class="col-md-6">
                                                <label>&nbsp;&nbsp;&nbsp; </label>

                                                <asp:TextBox ID="desigTextBox" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                            </div>



                                            <div class="col-md-1">
                                                <br />
                                                <label>&nbsp;&nbsp; </label>
                                                <asp:Button ID="Button16" runat="server" CssClass=" btn btn-sm btn-outline-info" OnClick="Button16_OnClick" Text=" . . . " />
                                            </div>

                                            <div class="col-md-1">
                                                <br />
                                                <label>&nbsp;&nbsp; </label>
                                                <asp:Button ID="Button17" runat="server" OnClick="Button17_OnClick" CssClass="btn btn-sm btn-outline-success" Text="Clear" />
                                            </div>
                                        </div>
                                        
                                        
                                        
                                        <div class="row">

                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Employeement Status </label>

                                                    <asp:DropDownList ID="empStatusDropDownList" AutoPostBack="True" class="form-control form-control-sm" OnSelectedIndexChanged="empStatusDropDownList_OnSelectedIndexChanged" runat="server">
                                                        <asp:ListItem Value="0">Select From List</asp:ListItem>
                                                        <asp:ListItem>Active</asp:ListItem>
                                                        <asp:ListItem>Inactive</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-2" runat="server" visible="False" id="jobleft">
                                                <div class="form-group">
                                                    <label>Inactive Type(Job Left) </label>

                                                    <asp:DropDownList ID="jobleftTypeDropDownList" class="form-control form-control-sm" runat="server">
                                                        <%--<asp:ListItem>Job Left By Employee</asp:ListItem>
                                                        <asp:ListItem>Retirement</asp:ListItem>
                                                        <asp:ListItem>Dismiss</asp:ListItem>
                                                        <asp:ListItem>Un Attendent</asp:ListItem>
                                                        <asp:ListItem>Expired</asp:ListItem>
                                                        <asp:ListItem>Resign</asp:ListItem>
                                                        <asp:ListItem>Terminated</asp:ListItem>
                                                        <asp:ListItem>Expiry of Contracts</asp:ListItem>
                                                        <asp:ListItem>Cautionary Letter</asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-2" runat="server" visible="False" id="suspend">
                                                <div class="form-group">
                                                    <label>Inactive Type(Suspend Left) </label>

                                                    <asp:DropDownList ID="suspendDropDownList" class="form-control form-control-sm" runat="server">

                                                        <%--<asp:ListItem>Cautionary Letter</asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <%-- <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Employment Type </label>

                                                    <asp:DropDownList ID="emptypeDropDownList" class="form-control form-control-sm" runat="server">
                                                        <asp:ListItem Value="2">Contractual</asp:ListItem>
                                                        <asp:ListItem Value="1">Permanent</asp:ListItem>
                                                        <asp:ListItem Value="3">Programme Contractual</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>--%>
                                        </div>
                                        
                                          <div class="row">
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Employment Type </label>

                                                    <asp:DropDownList ID="employementDropDownList" class="form-control form-control-sm" runat="server">
                                                        <asp:ListItem Value="=">In</asp:ListItem>
                                                        <asp:ListItem Value="<>">Not In</asp:ListItem>

                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label></label>

                                                    <asp:DropDownList ID="emptypeDropDownList" class="form-control form-control-sm" runat="server">
                                                        <asp:ListItem Value="0">Select From List</asp:ListItem>
                                                        <asp:ListItem Value="2">Contractual</asp:ListItem>
                                                        <asp:ListItem Value="1">Permanent</asp:ListItem>
                                                        <asp:ListItem Value="3">Programme Contractual</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-md-1">
                                                <br />
                                                <label>&nbsp;&nbsp; </label>
                                                <asp:Button ID="Button42" runat="server" CssClass="btn btn-sm btn-outline-success" OnClick="Button42_OnClick" Text="Clear" />
                                            </div>

                                        </div>

                                        <div class="row">
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Joining Date </label>
                                                    <%--<label style="color: #a52a2a">* </label>--%>
                                                    <asp:DropDownList ID="joiningDateDropDownList" AutoPostBack="True" runat="server" class="form-control form-control-sm" OnSelectedIndexChanged="joiningDateDropDownList_OnSelectedIndexChanged">
                                                        <asp:ListItem Value="1"> No Filter </asp:ListItem>
                                                        <asp:ListItem Value="2"> = </asp:ListItem>
                                                        <asp:ListItem Value="3"> < </asp:ListItem>
                                                        <asp:ListItem Value="4"> > </asp:ListItem>
                                                        <asp:ListItem Value="5"> Between </asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-md-2" runat="server" id="joiningDatesingle" visible="False">
                                                <div class="form-group">
                                                    <label>Date: </label>
                                                    <asp:TextBox ID="joiningDtTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar" PopupButtonID="joiningDtTextBox"
                                                        TargetControlID="joiningDtTextBox" />
                                                </div>
                                            </div>


                                            <div runat="server" id="joiningdateRange" visible="False" class="col-md-4">

                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>From </label>
                                                            <asp:TextBox ID="joiningDtFrTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                                Format="dd-MMM-yyyy" CssClass="MyCalendar" PopupButtonID="joiningDtFrTextBox"
                                                                TargetControlID="joiningDtFrTextBox" />

                                                        </div>
                                                    </div>

                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>To </label>
                                                            <asp:TextBox ID="joiningDtToTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server"
                                                                Format="dd-MMM-yyyy" CssClass="MyCalendar" PopupButtonID="joiningDtToTextBox"
                                                                TargetControlID="joiningDtToTextBox" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-1">
                                                <br />
                                                <label>&nbsp;&nbsp; </label>
                                                <asp:Button ID="Button43" runat="server" CssClass="btn btn-sm btn-outline-success" OnClick="Button43_OnClick" Text="Clear" />
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Retirement Date </label>
                                                    <%--<label style="color: #a52a2a">* </label>--%>
                                                    <asp:DropDownList ID="retirementDropDownList" AutoPostBack="True" runat="server" class="form-control form-control-sm" OnSelectedIndexChanged="retirementDropDownList_OnSelectedIndexChanged">
                                                        <asp:ListItem Value="1"> No Filter </asp:ListItem>
                                                        <asp:ListItem Value="2"> = </asp:ListItem>
                                                        <asp:ListItem Value="3"> < </asp:ListItem>
                                                        <asp:ListItem Value="4"> > </asp:ListItem>
                                                        <asp:ListItem Value="5"> Between </asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-md-2" runat="server" id="retirement" visible="False">
                                                <div class="form-group">
                                                    <label>Date: </label>
                                                    <asp:TextBox ID="retTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar" PopupButtonID="retTextBox"
                                                        TargetControlID="retTextBox" />
                                                </div>
                                            </div>


                                            <div runat="server" id="retirementbt" visible="False" class="col-md-4">

                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>From </label>
                                                            <asp:TextBox ID="retfromTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server"
                                                                Format="dd-MMM-yyyy" CssClass="MyCalendar" PopupButtonID="retfromTextBox"
                                                                TargetControlID="retfromTextBox" />

                                                        </div>
                                                    </div>

                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>To </label>
                                                            <asp:TextBox ID="retToTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender27" runat="server"
                                                                Format="dd-MMM-yyyy" CssClass="MyCalendar" PopupButtonID="retToTextBox"
                                                                TargetControlID="retToTextBox" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="col-md-1">
                                                <br />
                                                <label>&nbsp;&nbsp; </label>
                                                <asp:Button ID="Button44" runat="server" CssClass="btn btn-sm btn-outline-success" OnClick="Button44_OnClick" Text="Clear" />
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Age </label>
                                                    <%--<label style="color: #a52a2a">* </label>--%>
                                                    <asp:DropDownList ID="ageDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ageDropDownList_OnSelectedIndexChanged" class="form-control form-control-sm">
                                                        <asp:ListItem Value="1"> No Filter </asp:ListItem>
                                                        <asp:ListItem Value="2"> = </asp:ListItem>
                                                        <asp:ListItem Value="3"> < </asp:ListItem>
                                                        <asp:ListItem Value="4"> > </asp:ListItem>
                                                        <asp:ListItem Value="5"> Between </asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-md-2" runat="server" id="agesingle" visible="False">
                                                <div class="form-group">
                                                    <label>No of Years: </label>
                                                    <asp:TextBox ID="ageTextBox" TextMode="Number" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                </div>
                                            </div>


                                            <div runat="server" id="agerange" visible="False" class="col-md-4">

                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>Minimum </label>
                                                            <asp:TextBox ID="ageMinTextBox" CssClass="form-control form-control-sm" TextMode="Number" runat="server" CausesValidation="True"></asp:TextBox>

                                                        </div>
                                                    </div>

                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>Maximum </label>
                                                            <asp:TextBox ID="ageMaxTextBox" TextMode="Number" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-1">
                                                <br />
                                                <label>&nbsp;&nbsp; </label>
                                                <asp:Button ID="Button45" runat="server" CssClass="btn btn-sm btn-outline-success" OnClick="Button45_OnClick" Text="Clear" />
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Service Length </label>
                                                    <%--<label style="color: #a52a2a">* </label>--%>
                                                    <asp:DropDownList ID="serviceLengthDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="serviceLengthDropDownList_OnSelectedIndexChanged" class="form-control form-control-sm">
                                                        <asp:ListItem Value="1"> No Filter </asp:ListItem>
                                                        <asp:ListItem Value="2"> = </asp:ListItem>
                                                        <asp:ListItem Value="3"> < </asp:ListItem>
                                                        <asp:ListItem Value="4"> > </asp:ListItem>
                                                        <asp:ListItem Value="5"> Between </asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-md-2" runat="server" id="serviceL" visible="False">
                                                <div class="form-group">
                                                    <label>No of Years: </label>
                                                    <asp:TextBox ID="serviceLengthTextBox" TextMode="Number" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                </div>
                                            </div>


                                            <div runat="server" id="serviceLBt" visible="False" class="col-md-4">

                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>Minimum </label>
                                                            <asp:TextBox ID="serviceLengthFrTextBox" CssClass="form-control form-control-sm" TextMode="Number" runat="server" CausesValidation="True"></asp:TextBox>

                                                        </div>
                                                    </div>

                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>Maximum </label>
                                                            <asp:TextBox ID="serviceLengthToTextBox" TextMode="Number" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="col-md-1">
                                                <br />
                                                <label>&nbsp;&nbsp; </label>
                                                <asp:Button ID="Button46" runat="server" CssClass="btn btn-sm btn-outline-success" OnClick="Button46_OnClick" Text="Clear" />
                                            </div>
                                        </div>



                                        <div class="row">
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Confirmation Date </label>
                                                    <%--<label style="color: #a52a2a">* </label>--%>
                                                    <asp:DropDownList ID="endDateDropDownList" runat="server" AutoPostBack="True" class="form-control form-control-sm" OnSelectedIndexChanged="endDateDropDownList_OnSelectedIndexChanged">
                                                        <asp:ListItem Value="1"> No Filter </asp:ListItem>
                                                        <asp:ListItem Value="2"> = </asp:ListItem>
                                                        <asp:ListItem Value="3"> < </asp:ListItem>
                                                        <asp:ListItem Value="4"> > </asp:ListItem>
                                                        <asp:ListItem Value="5"> Between </asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-md-2" runat="server" id="singleEndDate" visible="False">
                                                <div class="form-group">
                                                    <label>Date: </label>
                                                    <asp:TextBox ID="endDateTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server"
                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                        TargetControlID="endDateTextBox" />
                                                </div>
                                            </div>


                                            <div runat="server" id="endDateRange" visible="False" class="col-md-4">

                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>From Date: </label>
                                                            <asp:TextBox ID="endFromDateTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server"
                                                                Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                TargetControlID="endFromDateTextBox" />
                                                        </div>
                                                    </div>

                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>To Date: </label>
                                                            <asp:TextBox ID="endToDateTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender8" runat="server"
                                                                Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                TargetControlID="endToDateTextBox" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-1">
                                                <br />
                                                <label>&nbsp;&nbsp; </label>
                                                <asp:Button ID="Button47" runat="server" CssClass="btn btn-sm btn-outline-success" OnClick="Button47_OnClick" Text="Clear" />
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Date Of Birth </label>
                                                    <label style="color: #a52a2a">* </label>
                                                    <asp:DropDownList ID="dobDropDownList" runat="server" AutoPostBack="True" class="form-control form-control-sm" OnSelectedIndexChanged="dobDropDownList_OnSelectedIndexChanged">
                                                        <asp:ListItem Value="1"> No Filter </asp:ListItem>
                                                        <asp:ListItem Value="2"> = </asp:ListItem>
                                                        <asp:ListItem Value="3"> < </asp:ListItem>
                                                        <asp:ListItem Value="4"> > </asp:ListItem>
                                                        <asp:ListItem Value="5"> Between </asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-md-2" runat="server" id="dobsingle" visible="False">
                                                <div class="form-group">
                                                    <label>Date: </label>
                                                    <asp:TextBox ID="dobTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender9" runat="server"
                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                        TargetControlID="dobTextBox" />
                                                </div>
                                            </div>


                                            <div runat="server" id="dobrange" visible="False" class="col-md-4">

                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>From Date: </label>
                                                            <asp:TextBox ID="dobfromTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender10" runat="server"
                                                                Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                TargetControlID="dobfromTextBox" />
                                                        </div>
                                                    </div>

                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>To Date: </label>
                                                            <asp:TextBox ID="dobtoTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender11" runat="server"
                                                                Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                TargetControlID="dobtoTextBox" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-1">
                                                <br />
                                                <label>&nbsp;&nbsp; </label>
                                                <asp:Button ID="Button48" runat="server" CssClass="btn btn-sm btn-outline-success" OnClick="Button48_OnClick" Text="Clear" />
                                            </div>

                                        </div>


                                        <div class="row">

                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Confirmation Status </label>

                                                    <asp:DropDownList ID="confirmStDropDownList" class="form-control form-control-sm" runat="server">
                                                        <asp:ListItem> None </asp:ListItem>
                                                        <asp:ListItem Value="1"> Confirmed</asp:ListItem>
                                                        <asp:ListItem Value="0"> Not Confirmed</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>


                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Type of Position </label>

                                                    <asp:DropDownList ID="typeOfPosDropDownList" class="form-control form-control-sm" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>


                                            <div class="col-md-1">
                                                <br />
                                                <label>&nbsp;&nbsp; </label>
                                                <asp:Button ID="Button49" runat="server" CssClass="btn btn-sm btn-outline-success" OnClick="Button49_OnClick" Text="Clear" />
                                            </div>

                                        </div>


                                        <div class="row">
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Turnover Date </label>
                                                    <%--<label style="color: #a52a2a">* </label>--%>
                                                    <asp:DropDownList ID="turnOverdtDropDownList" AutoPostBack="True" OnSelectedIndexChanged="turnOverdtDropDownList_OnSelectedIndexChanged" runat="server" class="form-control form-control-sm">
                                                        <asp:ListItem Value="1"> No Filter </asp:ListItem>
                                                        <asp:ListItem Value="2"> = </asp:ListItem>
                                                        <asp:ListItem Value="3"> < </asp:ListItem>
                                                        <asp:ListItem Value="4"> > </asp:ListItem>
                                                        <asp:ListItem Value="5"> Between </asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-md-2" runat="server" id="turnDtSingle" visible="False">
                                                <div class="form-group">
                                                    <label>Date: </label>
                                                    <asp:TextBox ID="turnOverDateTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender12" runat="server"
                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                        TargetControlID="turnOverDateTextBox" />
                                                </div>
                                            </div>


                                            <div runat="server" id="turnDtRange" visible="False" class="col-md-4">

                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>From Date: </label>
                                                            <asp:TextBox ID="turnOverDateFrTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender13" runat="server"
                                                                Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                TargetControlID="turnOverDateFrTextBox" />
                                                        </div>
                                                    </div>

                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>To Date: </label>
                                                            <asp:TextBox ID="turnOverDatetoTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender14" runat="server"
                                                                Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                TargetControlID="turnOverDatetoTextBox" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-1">
                                                <br />
                                                <label>&nbsp;&nbsp; </label>
                                                <asp:Button ID="Button50" runat="server" CssClass="btn btn-sm btn-outline-success" OnClick="Button50_OnClick" Text="Clear" />
                                            </div>

                                        </div>



                                        <div class="row" runat="server" visible="false">
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Last Promotion </label>
                                                    <%--<label style="color: #a52a2a">* </label>--%>
                                                    <asp:DropDownList ID="DropDownList11" runat="server" class="form-control form-control-sm">
                                                        <asp:ListItem Value="1"> No Filter </asp:ListItem>
                                                        <asp:ListItem Value="2"> = </asp:ListItem>
                                                        <asp:ListItem Value="3"> < </asp:ListItem>
                                                        <asp:ListItem Value="4"> > </asp:ListItem>
                                                        <asp:ListItem Value="5"> Between </asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-md-2" runat="server" id="Div7" visible="False">
                                                <div class="form-group">
                                                    <label>Date: </label>
                                                    <asp:TextBox ID="TextBox17" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender15" runat="server"
                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                        TargetControlID="TextBox17" />
                                                </div>
                                            </div>


                                            <div runat="server" id="Div8" visible="False" class="col-md-4">

                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>From Date: </label>
                                                            <asp:TextBox ID="TextBox18" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender16" runat="server"
                                                                Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                TargetControlID="TextBox18" />
                                                        </div>
                                                    </div>

                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>To Date: </label>
                                                            <asp:TextBox ID="TextBox19" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender17" runat="server"
                                                                Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                TargetControlID="TextBox19" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>



                                    <div class="form-row" runat="server" Visible="False">
                                        <div class="col-md-12">
                                            <fieldset class="for-panel">
                                                <legend>Other Information </legend>

                                                <div class="row">
                                                   
                                                </div>


                                            

                                                <div class="row" runat="server" visible="False">


                                                    <div class="col-md-2">
                                                        <div class="form-group">
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label>&nbsp;&nbsp;&nbsp; </label>

                                                        <asp:TextBox ID="religionTextBox" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                    </div>



                                                    <div class="col-md-1">
                                                        <br />
                                                        <label>&nbsp;&nbsp; </label>
                                                        <asp:Button ID="Button24" runat="server" CssClass=" btn btn-sm btn-outline-info" OnClick="Button24_OnClick" Text=" . . . " />
                                                    </div>
                                                    <div class="col-md-1">
                                                        <br />
                                                        <label>&nbsp;&nbsp; </label>
                                                        <asp:Button ID="Button25" runat="server" OnClick="Button25_OnClick" CssClass="btn btn-sm btn-outline-success" Text="Clear" />
                                                    </div>
                                                </div>
                                                <div class="row" runat="server" visible="False">

                                                    <div class="col-md-2">
                                                        <div class="form-group">
                                                            <label>Gender </label>


                                                        </div>
                                                    </div>

                                                    <div class="col-md-6">
                                                        <label>&nbsp;&nbsp;&nbsp; </label>

                                                        <asp:TextBox ID="genderTextBox" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                    </div>



                                                    <div class="col-md-1">
                                                        <br />
                                                        <label>&nbsp;&nbsp; </label>
                                                        <asp:Button ID="Button26" runat="server" OnClick="Button26_OnClick" CssClass=" btn btn-sm btn-outline-info" Text=" . . . " />
                                                    </div>
                                                    <div class="col-md-1">
                                                        <br />
                                                        <label>&nbsp;&nbsp; </label>
                                                        <asp:Button ID="Button27" runat="server" OnClick="Button27_OnClick" CssClass="btn btn-sm btn-outline-success" Text="Clear" />
                                                    </div>
                                                </div>
                                                <div class="row" runat="server" visible="False">

                                                    <div class="col-md-2">
                                                        <div class="form-group">
                                                        </div>
                                                    </div>


                                                    <div class="col-md-6">
                                                        <label>&nbsp;&nbsp;&nbsp; </label>

                                                        <asp:TextBox ID="bloodgroupTextBox" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                    </div>



                                                    <div class="col-md-1">
                                                        <br />
                                                        <label>&nbsp;&nbsp; </label>
                                                        <asp:Button ID="Button28" runat="server" OnClick="Button28_OnClick" CssClass=" btn btn-sm btn-outline-info" Text=" . . . " />
                                                    </div>
                                                    <div class="col-md-1">
                                                        <br />
                                                        <label>&nbsp;&nbsp; </label>
                                                        <asp:Button ID="Button29" runat="server" OnClick="Button29_OnClick" CssClass="btn btn-sm btn-outline-success" Text="Clear" />
                                                    </div>
                                                </div>



                                                <div class="row" runat="server" visible="False">


                                                    <div class="col-md-2">
                                                        <div class="form-group">
                                                        </div>
                                                    </div>


                                                    <div class="col-md-6">
                                                        <label>&nbsp;&nbsp;&nbsp; </label>

                                                        <asp:TextBox ID="permDistTextBox" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                    </div>



                                                    <div class="col-md-1">
                                                        <br />
                                                        <label>&nbsp;&nbsp; </label>
                                                        <asp:Button ID="Button30" runat="server" OnClick="Button30_OnClick" CssClass=" btn btn-sm btn-outline-info" Text=" . . . " />
                                                    </div>
                                                    <div class="col-md-1">
                                                        <br />
                                                        <label>&nbsp;&nbsp; </label>
                                                        <asp:Button ID="Button31" runat="server" OnClick="Button31_OnClick" CssClass="btn btn-sm btn-outline-success" Text="Clear" />
                                                    </div>

                                                </div>
                                                <div class="row" runat="server" visible="False">
                                                    <div class="col-md-2">
                                                        <div class="form-group">
                                                        </div>
                                                    </div>


                                                    <div class="col-md-6">
                                                        <label>&nbsp;&nbsp;&nbsp; </label>
                                                        <asp:TextBox ID="permThanaTextBox" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                    </div>



                                                    <div class="col-md-1">
                                                        <br />
                                                        <label>&nbsp;&nbsp; </label>
                                                        <asp:Button ID="Button32" runat="server" OnClick="Button32_OnClick" CssClass=" btn btn-sm btn-outline-info" Text=" . . . " />
                                                    </div>
                                                    <div class="col-md-1">
                                                        <br />
                                                        <label>&nbsp;&nbsp; </label>
                                                        <asp:Button ID="Button33" runat="server" OnClick="Button33_OnClick" CssClass="btn btn-sm btn-outline-success" Text="Clear" />
                                                    </div>


                                                </div>
                                                <div class="row" runat="server" visible="False">
                                                    <div class="col-md-2">
                                                        <div class="form-group">
                                                            <label>Nomination Purpose </label>

                                                            <asp:DropDownList ID="nominationPurDropDownList" class="form-control form-control-sm" runat="server">
                                                                <asp:ListItem> In </asp:ListItem>
                                                                <asp:ListItem> Not In </asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>


                                                    <div class="col-md-6">
                                                        <label>&nbsp;&nbsp;&nbsp; </label>

                                                        <asp:TextBox ID="nominationTextBox" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                    </div>



                                                    <div class="col-md-1">
                                                        <br />
                                                        <label>&nbsp;&nbsp; </label>
                                                        <asp:Button ID="Button34" runat="server" OnClick="Button34_OnClick" CssClass=" btn btn-sm btn-outline-info" Text=" . . . " />
                                                    </div>
                                                    <div class="col-md-1">
                                                        <br />
                                                        <label>&nbsp;&nbsp; </label>
                                                        <asp:Button ID="Button35" runat="server" OnClick="Button35_OnClick" CssClass="btn btn-sm btn-outline-success" Text="Clear" />
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </div>

                                    </div>


                                    <div class="form-row" runat="server" visible="False">
                                        <div class="col-md-12">

                                            <fieldset class="for-panel">
                                                <legend>Education Information </legend>
                                                <div class="row">


                                                    <div class="col-md-2" runat="server" visible="False">
                                                        <div class="form-group">
                                                            <label>Education Lavel </label>

                                                            <asp:DropDownList ID="DropDownList18" class="form-control form-control-sm" runat="server">
                                                                <asp:ListItem> In </asp:ListItem>
                                                                <asp:ListItem> Not In </asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>


                                                    <div class="col-md-2" runat="server" visible="False">
                                                        <div class="form-group">
                                                            <label style="color: #fff;">Date: </label>
                                                            <asp:TextBox ID="TextBox26" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender24" runat="server"
                                                                Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                TargetControlID="TextBox26" />
                                                        </div>
                                                    </div>


                                                    <div class="col-md-2">
                                                        <div class="form-group">
                                                            <label>Education Name </label>
                                                            <asp:DropDownList ID="ddlEducationIn" class="form-control form-control-sm" runat="server">
                                                                <asp:ListItem> In </asp:ListItem>
                                                                <asp:ListItem> Not In </asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>


                                                    <div class="col-md-2">
                                                        <div class="form-group">
                                                            <label style="color: #fff;">Education (Name): </label>
                                                            <asp:DropDownList ID="ddlEducation" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:DropDownList>
                                                        </div>
                                                    </div>



                                                    <div class="col-md-2">
                                                        <div class="form-group">
                                                            <label>Subject Group </label>

                                                            <asp:DropDownList ID="ddlSubjectGroupIn" class="form-control form-control-sm" runat="server">
                                                                <asp:ListItem> In </asp:ListItem>
                                                                <asp:ListItem> Not In </asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>


                                                    <div class="col-md-2">
                                                        <div class="form-group">
                                                            <label style="color: #fff;">Date: </label>
                                                            <asp:DropDownList ID="ddlSubject" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                            </fieldset>

                                        </div>
                                    </div>



                                    <div class="form-row" runat="server" visible="False">
                                        <div class="col-md-12">

                                            <fieldset class="for-panel">
                                                <legend>Diciplinary Action </legend>
                                                <div class="row">


                                                    <div class="col-md-2">
                                                        <div class="form-group">
                                                            <label>Diciplinary Action </label>
                                                            <asp:DropDownList ID="ddlAction" class="form-control form-control-sm" runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>


                                                    <div class="col-md-2">
                                                        <div class="form-group">
                                                            <label>Action Date </label>
                                                            <%--<label style="color: #a52a2a">* </label>--%>
                                                            <asp:DropDownList ID="actionDateDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="actionDateDropDownList_OnSelectedIndexChanged" class="form-control form-control-sm">
                                                                <asp:ListItem Value="1"> No Filter </asp:ListItem>
                                                                <asp:ListItem Value="2"> = </asp:ListItem>
                                                                <asp:ListItem Value="3"> < </asp:ListItem>
                                                                <asp:ListItem Value="4"> > </asp:ListItem>
                                                                <asp:ListItem Value="5"> Between </asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-2" runat="server" id="diciplinaryDTSingle" visible="False">
                                                        <div class="form-group">
                                                            <label>Date: </label>
                                                            <asp:TextBox ID="actionDateTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender18" runat="server"
                                                                Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                TargetControlID="TextBox17" />
                                                        </div>
                                                    </div>


                                                    <div runat="server" id="diciplinaryDTRange" visible="False" class="col-md-4">

                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>From Date: </label>
                                                                    <asp:TextBox ID="actionFRDateTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender19" runat="server"
                                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                        TargetControlID="TextBox18" />
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>To Date: </label>
                                                                    <asp:TextBox ID="actionToDateTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender20" runat="server"
                                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                        TargetControlID="TextBox19" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </fieldset>

                                        </div>
                                    </div>


                                    <div class="form-row" runat="server" visible="false">
                                        <div class="col-md-12">

                                            <fieldset class="for-panel">
                                                <legend>Training Information </legend>
                                                <div class="row">


                                                    <div class="col-md-2">
                                                        <div class="form-group">
                                                            <label>Country </label>

                                                            <asp:DropDownList ID="ddlCountryIn" class="form-control form-control-sm" runat="server">
                                                                <asp:ListItem> In </asp:ListItem>
                                                                <asp:ListItem> Not In </asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-2">
                                                        <div class="form-group">
                                                            <label>Training Country </label>
                                                            <asp:DropDownList ID="ddlCountry" class="form-control form-control-sm" runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>


                                                    <div class="col-md-2">
                                                        <div class="form-group">
                                                            <label>Training Date (Start) </label>
                                                            <%--<label style="color: #a52a2a">* </label>--%>
                                                            <asp:DropDownList ID="ddlTrainigStart" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTrainigStart_OnSelectedIndexChanged" class="form-control form-control-sm">
                                                                <asp:ListItem Value="1"> No Filter </asp:ListItem>
                                                                <asp:ListItem Value="2"> = </asp:ListItem>
                                                                <asp:ListItem Value="3"> < </asp:ListItem>
                                                                <asp:ListItem Value="4"> > </asp:ListItem>
                                                                <asp:ListItem Value="5"> Between </asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-2" runat="server" id="trainingSingleDt" visible="False">
                                                        <div class="form-group">
                                                            <label>Start Date: </label>
                                                            <asp:TextBox ID="trainingStartTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender21" runat="server"
                                                                Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                TargetControlID="TextBox17" />
                                                        </div>
                                                    </div>


                                                    <div runat="server" id="trainingRangeDt" visible="False" class="col-md-4">

                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>From Date: </label>
                                                                    <asp:TextBox ID="trainingStartFRTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender22" runat="server"
                                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                        TargetControlID="TextBox18" />
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>To Date: </label>
                                                                    <asp:TextBox ID="trainingToTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender23" runat="server"
                                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                        TargetControlID="TextBox19" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>



                                                <div class="row">

                                                    <div class="col-md-2">
                                                        <div class="form-group">
                                                            <label>Action Date (End) </label>
                                                            <%--<label style="color: #a52a2a">* </label>--%>
                                                            <asp:DropDownList ID="ddlTrainingEnd" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTrainingEnd_OnSelectedIndexChanged" class="form-control form-control-sm">
                                                                <asp:ListItem Value="1"> No Filter </asp:ListItem>
                                                                <asp:ListItem Value="2"> = </asp:ListItem>
                                                                <asp:ListItem Value="3"> < </asp:ListItem>
                                                                <asp:ListItem Value="4"> > </asp:ListItem>
                                                                <asp:ListItem Value="5"> Between </asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-2" runat="server" id="trainingEndSingleDt" visible="False">
                                                        <div class="form-group">
                                                            <label>End Date: </label>
                                                            <asp:TextBox ID="trainingEndTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender25" runat="server"
                                                                Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                TargetControlID="TextBox17" />
                                                        </div>
                                                    </div>


                                                    <div runat="server" id="trainingEndRangeDt" visible="False" class="col-md-4">

                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>From Date: </label>
                                                                    <asp:TextBox ID="trainingEndFRTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender26" runat="server"
                                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                        TargetControlID="TextBox18" />
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>To Date: </label>
                                                                    <asp:TextBox ID="trainingEndToTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender28" runat="server"
                                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                        TargetControlID="TextBox19" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>




                                            </fieldset>

                                        </div>
                                    </div>


                                </div>

                            </div>

                            <div>
                                <%--<asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" Text="Submit " CssClass="btn btn-sm btn-info" />
                                <asp:Button ID="cancelButton" Text="Cancel" OnClick="cancelButton_OnClick" Visible="False" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />--%>
                            </div>

                            <br />
                            <br />

                            <div class="row">
                                <div class="col-md-4">
                                    <%--<asp:Button runat="server" ID="Button51" OnClick="btn_Save_OnClick" Text="Search " CssClass="btn btn-outline-info btn-block disabled btn-sm" />--%>
                                </div>
                                <div class="col-md-4">
                                    <asp:LinkButton runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" CssClass="btn btn-info   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search </asp:LinkButton>

                                    <asp:LinkButton runat="server" ID="lbReset" OnClick="lbReset_OnClick" CssClass="btn btn-warning   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset </asp:LinkButton>
                                </div>
                                <div class="col-md-4">
                                    <%--<asp:Button runat="server" ID="Button51" OnClick="btn_Save_OnClick" Text="Search " CssClass="btn btn-outline-info btn-block disabled btn-sm" />--%>
                                </div>
                            </div>

                            <br />
                            <br />
                        

                        </div>

                    </div>
                </div>
            </ContentTemplate>
           
        </asp:UpdatePanel>

          <div class="form-row" style="padding-right: 10px">
              
               <div class="col-md-2" style="padding-left:20px">
               <label style="font-size: 18px;">  Details Information</label>
            </div>
               <div class="col-md-8">
                
            </div>
            <div class="col-md-2">
                <asp:LinkButton ID="btnExportToExcel" runat="server" CssClass="btnexcel  pull-right" style="padding-right: 10px;" OnClick="btnExportToExcel_Click"><span aria-hidden="true" class="fa fa-download"></span>Download to xls</asp:LinkButton>
            </div>
              

        </div>
        
        
      <div style="padding: 10px">
            <asp:UpdatePanel runat="server" ID="ssas">
            <ContentTemplate>
                   <hr/>
                            
                          
                                 <div style="overflow: scroll; height: 500px;width: 100%">
                                                    <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                                        CssClass="table table-bordered  text-center thead-light" DataKeyNames="EmpInfoId" PageIndex="0"   >
                                                       
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SL">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:BoundField DataField="ShortName" HeaderText="Company Name" />
                                                            <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee New ID" />
                                                            <asp:BoundField DataField="SMCOldCode" HeaderText="Employee Old ID" />

                                                            <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                                            <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                                            <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                                            <asp:BoundField DataField="Division" HeaderText="Division" />
                                                            <asp:BoundField DataField="Wing" HeaderText="Wing" />
                                                            <asp:BoundField DataField="Section" HeaderText="Section" />
                                                            <asp:BoundField DataField="SubSection" HeaderText="Sub-Section" />
                                                            <asp:BoundField DataField="Category" HeaderText="Category" />
                                                            <asp:BoundField DataField="Grade" HeaderText="Grade" />
                                                            <asp:BoundField DataField="Step" HeaderText="Step" />
                                                            <asp:BoundField DataField="Office" HeaderText="Office" />
                                                            <asp:BoundField DataField="Place" HeaderText="Place" />

                                                            <asp:BoundField DataField="DateofBirth" HeaderText="Date Of Birth" DataFormatString="{0:dd-MMM-yyyy}" />
                                                            <asp:BoundField DataField="Nationality" HeaderText="Nationality" />
                                                            <asp:BoundField DataField="Passport" HeaderText="Passport" />
                                                            <asp:BoundField DataField="BloodGroup" HeaderText="Blood Group" />
                                                            <asp:BoundField DataField="Gender" HeaderText="Gender" />
                                                            <asp:BoundField DataField="Religion" HeaderText="Religion" />
                                                            <asp:BoundField DataField="PlaceofBirth" HeaderText="Place of Birth" />
                                                            <asp:BoundField DataField="PresentAddress" HeaderText="Present Address" />
                                                            <asp:BoundField DataField="PermanentAddress" HeaderText="Permanent Address" />




                                                            <asp:BoundField DataField="PresentDivision" HeaderText="Present Division" />
                                                            <asp:BoundField DataField="PermanentDivision" HeaderText="Permanent Division" />

                                                            <asp:BoundField DataField="PresentDistrict" HeaderText="Present District" />
                                                            <asp:BoundField DataField="PermanentDistrict" HeaderText="Permanent District" />


                                                            <asp:BoundField DataField="PresentThana" HeaderText="Present Thana" />
                                                            <asp:BoundField DataField="PermanentThana" HeaderText="Permanent Thana" />



                                                            <asp:BoundField DataField="DateOfJoin" HeaderText="Joining Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                            <asp:BoundField DataField="DateOfConformation" HeaderText="Confirmation Date" DataFormatString="{0:dd-MMM-yyyy}" />


                                                            <asp:BoundField DataField="ServiceLength" HeaderText="Service Length" />

                                                            <asp:BoundField DataField="DateofRetirement" HeaderText="Date of Retirement" DataFormatString="{0:dd-MMM-yyyy}" />

                                                            <asp:BoundField DataField="ProbitionEndDate" HeaderText="Probation End Date" DataFormatString="{0:dd-MMM-yyyy}" />


                                                            <asp:BoundField DataField="ContractualEndDate" HeaderText="Contractual End Date" DataFormatString="{0:dd-MMM-yyyy}" />




                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
      </div>
       

    </div>
    <div>
        <ajaxToolkit:ModalPopupExtender ID="mpe_1" runat="server" TargetControlID="hnd_Test" PopupControlID="pnl_1"
            BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>

        <asp:HiddenField ID="hnd_Test" runat="server"></asp:HiddenField>
        <asp:Panel ID="pnl_1" runat="server" Style="display: none; overflow: scroll; padding: 10px" Height="500px" Width="400px" CssClass="modalPopup">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <h3 style="text-align: center;"><span>
                        <asp:Label ForeColor="#8B0000" Font="Bold" runat="server" ID="m_MemberName"></asp:Label></span></h3>
                    <div>

                        <div class="form-row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <%--<label>Company  </label>--%>
                                    <asp:Label Text="" runat="server" />
                                    <span style="font-size: 20px; color: #E9AD25;">Heirerchical Tree: </span>
                                    <hr />


                                </div>
                            </div>

                        </div>


                        <asp:Button runat="server" ID="btnEmpSubmit" Text="Submit " OnClick="btnEmpSubmit_OnClick" CssClass="btn btn-sm btn-info" />
                        <asp:Button ID="btnNo" Text="Cancel" OnClick="btnNo_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
    <div>
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="HiddenField1" PopupControlID="Panel1"
            BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>

        <asp:HiddenField ID="HiddenField1" runat="server"></asp:HiddenField>
        <asp:Panel ID="Panel1" runat="server" Style="display: none; overflow: scroll; padding: 10px" Height="500px" Width="400px" CssClass="modalPopup">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <h3 style="text-align: center;"><span>
                        <asp:Label ForeColor="#8B0000" Font="Bold" runat="server" ID="Label1"></asp:Label></span></h3>
                    <div>

                        <div class="form-row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label Text="" runat="server" />
                                    <span style="font-size: 20px; color: #E9AD25;">Grade List: </span>
                                    <hr />

                                </div>
                            </div>

                        </div>


                        <asp:Button runat="server" ID="Button2" Text="Submit " OnClick="Button2_OnClick" CssClass="btn btn-sm btn-info" />
                        <asp:Button ID="Button3" Text="Cancel" OnClick="Button3_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
    <div>
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="HiddenField2" PopupControlID="Panel2"
            BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>

        <asp:HiddenField ID="HiddenField2" runat="server"></asp:HiddenField>
        <asp:Panel ID="Panel2" runat="server" Style="display: none; overflow: scroll; padding: 10px" Height="500px" Width="400px" CssClass="modalPopup">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <h3 style="text-align: center;"><span>
                        <asp:Label ForeColor="#8B0000" Font="Bold" runat="server" ID="Label2"></asp:Label></span></h3>
                    <div>

                        <div class="form-row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label Text="" runat="server" />
                                    <span style="font-size: 20px; color: #E9AD25;">Step List: </span>
                                    <hr />



                                </div>
                            </div>

                        </div>


                        <asp:Button runat="server" ID="Button8" Text="Submit " OnClick="Button8_OnClick" CssClass="btn btn-sm btn-info" />
                        <asp:Button ID="Button9" Text="Cancel" OnClick="Button3_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
    <div>
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="HiddenField3" PopupControlID="Panel3"
            BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>

        <asp:HiddenField ID="HiddenField3" runat="server"></asp:HiddenField>
        <asp:Panel ID="Panel3" runat="server" Style="display: none; overflow: scroll; padding: 10px" Height="500px" Width="400px" CssClass="modalPopup">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <h3 style="text-align: center;"><span>
                        <asp:Label ForeColor="#8B0000" Font="Bold" runat="server" ID="Label3"></asp:Label></span></h3>
                    <div>

                        <div class="form-row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label Text="" runat="server" />
                                    <span style="font-size: 20px; color: #E9AD25;">Office List: </span>
                                    <hr />

                                </div>
                            </div>

                        </div>


                        <asp:Button runat="server" ID="Button10" Text="Submit " OnClick="Button10_OnClick" CssClass="btn btn-sm btn-info" />
                        <asp:Button ID="Button11" Text="Cancel" OnClick="Button11_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
    <div>
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="HiddenField4" PopupControlID="Panel4"
            BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>

        <asp:HiddenField ID="HiddenField4" runat="server"></asp:HiddenField>
        <asp:Panel ID="Panel4" runat="server" Style="display: none; overflow: scroll; padding: 10px" Height="500px" Width="400px" CssClass="modalPopup">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <h3 style="text-align: center;"><span>
                        <asp:Label ForeColor="#8B0000" Font="Bold" runat="server" ID="Label4"></asp:Label></span></h3>
                    <div>

                        <div class="form-row">
                            <div class="col-md-12">
                                <div class="form-group">

                                    <asp:Label Text="" runat="server" />
                                    <span style="font-size: 20px; color: #E9AD25;">Designation List: </span>
                                    <hr />


                                </div>
                            </div>

                        </div>


                        <asp:Button runat="server" ID="Button14" Text="Submit " OnClick="Button14_OnClick" CssClass="btn btn-sm btn-info" />
                        <asp:Button ID="Button15" Text="Cancel" OnClick="Button15_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
    <div>
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender5" runat="server" TargetControlID="HiddenField5" PopupControlID="Panel5"
            BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>

        <asp:HiddenField ID="HiddenField5" runat="server"></asp:HiddenField>
        <asp:Panel ID="Panel5" runat="server" Style="display: none; overflow: scroll; padding: 10px" Height="500px" Width="400px" CssClass="modalPopup">
            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                <ContentTemplate>
                    <h3 style="text-align: center;"><span>
                        <asp:Label ForeColor="#8B0000" Font="Bold" runat="server" ID="Label5"></asp:Label></span></h3>
                    <div>

                        <div class="form-row">
                            <div class="col-md-6">
                                <div class="form-group">

                                    <asp:Label Text="" runat="server" />
                                    <span style="font-size: 20px; color: #E9AD25;">Religion List: </span>
                                    <hr />



                                </div>
                            </div>

                        </div>


                        <asp:Button runat="server" ID="Button18" Text="Submit " OnClick="Button18_OnClick" CssClass="btn btn-sm btn-info" />
                        <asp:Button ID="Button19" Text="Cancel" OnClick="Button19_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
    <div>
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender6" runat="server" TargetControlID="HiddenField6" PopupControlID="Panel6"
            BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>

        <asp:HiddenField ID="HiddenField6" runat="server"></asp:HiddenField>
        <asp:Panel ID="Panel6" runat="server" Style="display: none; overflow: scroll; padding: 10px" Height="500px" Width="400px" CssClass="modalPopup">
            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                <ContentTemplate>
                    <h3 style="text-align: center;"><span>
                        <asp:Label ForeColor="#8B0000" Font="Bold" runat="server" ID="Label6"></asp:Label></span></h3>
                    <div>

                        <div class="form-row">
                            <div class="col-md-6">
                                <div class="form-group">

                                    <asp:Label Text="" runat="server" />
                                    <span style="font-size: 20px; color: #E9AD25;">Gender List: </span>
                                    <hr />




                                </div>
                            </div>

                        </div>


                        <asp:Button runat="server" ID="Button20" Text="Submit " OnClick="Button20_OnClick" CssClass="btn btn-sm btn-info" />
                        <asp:Button ID="Button21" Text="Cancel" OnClick="Button21_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
    <div>
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender7" runat="server" TargetControlID="HiddenField7" PopupControlID="Panel7"
            BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>

        <asp:HiddenField ID="HiddenField7" runat="server"></asp:HiddenField>
        <asp:Panel ID="Panel7" runat="server" Style="display: none; overflow: scroll; padding: 10px" Height="500px" Width="400px" CssClass="modalPopup">
            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                <ContentTemplate>
                    <h3 style="text-align: center;"><span>
                        <asp:Label ForeColor="#8B0000" Font="Bold" runat="server" ID="Label7"></asp:Label></span></h3>
                    <div>

                        <div class="form-row">
                            <div class="col-md-6">
                                <div class="form-group">

                                    <asp:Label Text="" runat="server" />
                                    <span style="font-size: 20px; color: #E9AD25;">Blood Group List: </span>
                                    <hr />




                                </div>
                            </div>

                        </div>


                        <asp:Button runat="server" ID="Button22" Text="Submit " OnClick="Button22_OnClick" CssClass="btn btn-sm btn-info" />
                        <asp:Button ID="Button23" Text="Cancel" OnClick="Button23_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
    <div>
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender8" runat="server" TargetControlID="HiddenField8" PopupControlID="Panel8"
            BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>

        <asp:HiddenField ID="HiddenField8" runat="server"></asp:HiddenField>
        <asp:Panel ID="Panel8" runat="server" Style="display: none; overflow: scroll; padding: 10px" Height="500px" Width="400px" CssClass="modalPopup">
            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                <ContentTemplate>
                    <h3 style="text-align: center;"><span>
                        <asp:Label ForeColor="#8B0000" Font="Bold" runat="server" ID="Label8"></asp:Label></span></h3>
                    <div>

                        <div class="form-row">
                            <div class="col-md-6">
                                <div class="form-group">

                                    <asp:Label Text="" runat="server" />
                                    <span style="font-size: 20px; color: #E9AD25;">District List: </span>
                                    <hr />



                                </div>
                            </div>

                        </div>


                        <asp:Button runat="server" ID="Button36" Text="Submit " OnClick="Button36_OnClick" CssClass="btn btn-sm btn-info" />
                        <asp:Button ID="Button37" Text="Cancel" OnClick="Button37_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
    <div>
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender9" runat="server" TargetControlID="HiddenField9" PopupControlID="Panel9"
            BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>

        <asp:HiddenField ID="HiddenField9" runat="server"></asp:HiddenField>
        <asp:Panel ID="Panel9" runat="server" Style="display: none; overflow: scroll; padding: 10px" Height="500px" Width="400px" CssClass="modalPopup">
            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                <ContentTemplate>
                    <h3 style="text-align: center;"><span>
                        <asp:Label ForeColor="#8B0000" Font="Bold" runat="server" ID="Label9"></asp:Label></span></h3>
                    <div>

                        <div class="form-row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label Text="" runat="server" />
                                    <span style="font-size: 20px; color: #E9AD25;">Thana List: </span>
                                    <hr />

                                </div>
                            </div>

                        </div>


                        <asp:Button runat="server" ID="Button38" Text="Submit " OnClick="Button38_OnClick" CssClass="btn btn-sm btn-info" />
                        <asp:Button ID="Button39" Text="Cancel" OnClick="Button39_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
        <div>
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender10" runat="server" TargetControlID="HiddenField10" PopupControlID="Panel10"
                BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>

            <asp:HiddenField ID="HiddenField10" runat="server"></asp:HiddenField>
            <asp:Panel ID="Panel10" runat="server" Style="display: none; overflow: scroll; padding: 10px" Height="500px" Width="400px" CssClass="modalPopup">
                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                    <ContentTemplate>
                        <h3 style="text-align: center;"><span>
                            <asp:Label ForeColor="#8B0000" Font="Bold" runat="server" ID="Label10"></asp:Label></span></h3>
                        <div>

                            <div class="form-row">
                                <div class="col-md-12">
                                    <div class="form-group">

                                        <asp:Label Text="" runat="server" />
                                        <span style="font-size: 20px; color: #E9AD25;">Nomination Purposes: </span>
                                        <hr />
                                        <asp:CheckBoxList ID="nominationCheckBoxList" CssClass="chkChoice" runat="server">
                                        </asp:CheckBoxList>


                                    </div>
                                </div>

                            </div>


                            <asp:Button runat="server" ID="Button40" Text="Submit " OnClick="Button40_OnClick" CssClass="btn btn-sm btn-info" />
                            <asp:Button ID="Button41" Text="Cancel" OnClick="Button41_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </div>
    </div>
</asp:Content>


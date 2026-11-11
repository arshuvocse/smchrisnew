<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="SeparationReport.aspx.cs" Inherits="Report_Pages_SeparationReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        fieldset.for-panel {
            background-color: #fcfcfc;
            border: 1px solid #999;
            padding: 15px 10px;
            background-color: white;
            margin-bottom: 12px;
        }

        .chkChoiceHeader label {
            padding-left: 10px;
            padding-right: 40px;
            font-size: 14px;
            font-weight: bold;
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

    <div class="container-fluid" style="background-color: white">
        <div class="page-heading">
            <div class="page-heading__container">
                <h1 class="title" style="font-size: 18px; padding-top: 0px;">
                    <img src="app.png" width="20px" />
                    Separation List</h1>
            </div>
            <%--<div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="detailsViewButton" Text="List Information " CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>--%>
        </div>

        <%--    <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                        <ProgressTemplate>
                            <div class="divWaiting">
                                <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>--%>
        <asp:UpdatePanel runat="server" ID="ssas">
            <ContentTemplate>
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



                                <div class="row">



                                    <div class="col-md-8">
                                        <br />

                                        <style>
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
                                                FONT-SIZE: 12px;
                                                font-weight: bold;
                                            }

                                            .rootNode {
                                                width: 100%;
                                                padding-left: 10px;
                                                padding-right: 10px;
                                                padding-top: 10px;
                                                FONT-SIZE: 12px;
                                                font-weight: bold;
                                            }

                                            .leafNode {
                                                padding-left: 10px;
                                                padding-right: 10px;
                                                background-color: #eeeeee;
                                            }

                                            .Label_Title {
                                                background-color: #C7C7C7;
                                                width: 100%;
                                                text-align: center;
                                                margin: 0px;
                                                padding: 7px;
                                                text-align: center;
                                                color: #000;
                                                margin-right: 5%;
                                                font-weight: bold;
                                                font-size: 13px;
                                            }


                                            #cpFormBody_gv_NewJoinerList td {
                                                border: 1px solid #ddd;
                                                padding: 8px;
                                            }

                                            #cpFormBody_gv_NewJoinerList tr:hover {
                                                background-color: #F5F5DC !important;
                                            }

                                            #cpFormBody_gv_NewJoinerList tr:nth-child(even) {
                                                background-color: #f2f2f2 !important;
                                            }

                                            #cpFormBody_gv_NewJoinerList > tbody > tr:not(th):nth-child(odd) {
                                                background-color: #DFDFDF !important;
                                            }



                                            #cpFormBody_gv_NewJoinerList th {
                                                padding: 10px;
                                                border-style: none;
                                                background-color: #CCCCCC;
                                                color: black;
                                                font-weight: bold;
                                                font-size: 13px;
                                            }

                                            #cpFormBody_gv_NewJoinerList > tbody > tr:not(th):nth-child(odd) {
                                                background-color: #DFDFDF !important;
                                            }

                                            /*jjjjjjjjjjjjj*/


                                          

                                            /*kkkkkkkk*/
                                            #cpFormBody_gv_redesignation td {
                                                border: 1px solid #ddd;
                                                padding: 8px;
                                            }

                                            #cpFormBody_gv_redesignation tr:hover {
                                                background-color: #F5F5DC !important;
                                            }

                                            #cpFormBody_gv_redesignation tr:nth-child(even) {
                                                background-color: #f2f2f2 !important;
                                            }

                                            #cpFormBody_gv_redesignation > tbody > tr:not(th):nth-child(odd) {
                                                background-color: #DFDFDF !important;
                                            }



                                            #cpFormBody_gv_redesignation th {
                                                padding: 10px;
                                                border-style: none;
                                                background-color: #CCCCCC;
                                                color: black;
                                                font-weight: bold;
                                                font-size: 13px;
                                            }

                                            #cpFormBody_gv_redesignation > tbody > tr:not(th):nth-child(odd) {
                                                background-color: #DFDFDF !important;
                                            }
                                        </style>

                                        <asp:Panel runat="server" ID="pNewJoinerList">
                                            
                                            <fieldset class="for-panel">
                                                <legend>Filtering Criteria </legend>
                                                <div class="row">
                                                    <div class="col-md-3">
                                                        <div class="form-group ">
                                                            <label class="control-label">Company </label>
                                                            <asp:DropDownList runat="server" ID="ddlCompany" class="form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" />
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group ">
                                                            <div class="form-group">
                                                                <label>Effective From Date</label>
                                                                <asp:TextBox ID="SeparationFromDt" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server"
                                                                    Format="dd-MMM-yyyy" CssClass="MyCalendar" PopupButtonID="SeparationFromDt"
                                                                    TargetControlID="SeparationFromDt" />

                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group ">
                                                            <label>Effective To Date </label>
                                                            <asp:TextBox ID="SeparationToDT" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server"
                                                                Format="dd-MMM-yyyy" CssClass="MyCalendar" PopupButtonID="SeparationToDT"
                                                                TargetControlID="SeparationToDT" />
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group ">
                                                            <label>&nbsp; </label>
                                                            <br />
                                                            <asp:LinkButton runat="server" ID="LinkButton1" OnClick="btn_Save_OnClick" CssClass="btn btn-info   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search </asp:LinkButton>
                                                            <asp:LinkButton runat="server" ID="LinkButton2" OnClick="lbReset_OnClick" CssClass="btn btn-warning   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </fieldset>
                                            
                                            
                                            


                                            <fieldset class="for-panel" runat="server" Visible="false">
                                                <legend>Filtering Criteria </legend>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <div class="row">

                                                            

                                                            <div class="col-md-12" runat="server" Visible="False">
                                                                <div class="form-group">
                                                                    <asp:RadioButtonList ID="SperationManualAuto" RepeatDirection="Horizontal" RepeatColumns="1" CssClass="chkChoiceHeader" runat="server">
                                                                        <asp:ListItem Value="Manual">Manual Updated</asp:ListItem>
                                                                        <asp:ListItem Value="Final">Final Approved</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-8">
                                                        <div class="Label_Title">Heirarchical Position List</div>
                                                        <div class="form-group">
                                                            <div style="overflow: scroll; height: 200px">

                                                                <br />
                                                                <asp:TreeView ID="TreeViewSeparationList" NodeStyle-CssClass="treeNode" ShowExpandCollapse="True"
                                                                    RootNodeStyle-CssClass="rootNode"
                                                                    LeafNodeStyle-CssClass="leafNode"
                                                                    ExpandImageUrl="../Assets/plus-sign.png" CollapseImageUrl="../Assets/minus-sign.png" runat="server" ShowCheckBoxes="All" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </asp:Panel>

                                       
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:LinkButton runat="server"  Visible="False" ID="LinkButton3" OnClick="btn_Approve_OnClick" CssClass="btn btn-success btn-block"><span aria-hidden="true" class="fa fa-9x fa-check-circle"></span> Confirm Seperation </asp:LinkButton>
                                    </div>
                                    <div class="col-md-8">
                                    </div>
                                    <div class="col-md-2">
                                        <asp:LinkButton ID="btnExportToExcel" runat="server" CssClass="btnexcel pull-right" OnClick="btnExportToExcel_Click"><i class="fa fa-file-excel-o"></i> &nbsp; Export to excel</asp:LinkButton>
                                    </div>
                                </div>
                                <hr />


                                <div style="overflow: scroll; height: 400px; width: 100%">
                                    <asp:GridView ID="gv_SeparationList" runat="server" AutoGenerateColumns="False"
                                      CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender" DataKeyNames="" >
                                        
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL">
                                                <ItemTemplate>
                                                    <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField DataField="Zid" HeaderText="Zid" />--%>
                                            <asp:BoundField DataField="ShortName" HeaderText="Company" />
                                            <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee ID" />
                                            <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                            <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                            
                                            <asp:BoundField DataField="DivisionName" HeaderText="Division" />
                                            <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                            <asp:BoundField DataField="ProjectName" HeaderText="Project Name" /> 
                                           

                                            <asp:BoundField DataField="JobLeftDate" HeaderText="Separation Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField DataField="JobLeftType" HeaderText="Separation Type"  />
                                             <asp:BoundField DataField="Reason" HeaderText="Reason" />
                                         <%--   <asp:TemplateField Visible="False">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                         <%--   <asp:TemplateField HeaderText="Status"  Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="statusLabel" runat="server" />
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                        

                                       <%-- <Columns>
                                            <asp:TemplateField HeaderText="SL">
                                                <ItemTemplate>
                                                    <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ShortName" HeaderText="Company Name" />
                                            <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee ID" />
                                            <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                            <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                            <asp:BoundField DataField="DateOfJoin" HeaderText="Date of Join" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField DataField="JobLeftType" HeaderText="Type of Separation" />
                                            <asp:BoundField DataField="JobLeftDate" HeaderText="Job Left Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        </Columns>--%>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExportToExcel" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

</asp:Content>


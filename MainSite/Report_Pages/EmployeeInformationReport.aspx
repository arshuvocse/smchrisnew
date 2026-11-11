<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeInformationReport.aspx.cs" Inherits="Report_UI_EmployeeInformationReport" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
         <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />

    <style type="text/css">
        
         .divWaitingforfun {
            background-color: #262626;
            position: fixed;
            left: 0px;
            top: 0px;
            z-index: 2147483647;
            width: 100%;
            height: 2215px;
            opacity: 0.6;
            overflow: hidden;
            text-align: center;
        }
         .noTop {
             margin-top: 0;
         }

        .tall .nav-tabs li,
        .tall .scrtabs-tab-container {
            height: 125px;
        }

        .tall .nav-tabs li .scrtabs-tab-scroll-arrow,
        .tall .scrtabs-tab-container .scrtabs-tab-scroll-arrow {
            height: 123px;
            padding: 0;
            margin: 0;
            border-top: 1px solid #ddd;
        }

        .tall .nav-tabs li .scrtabs-tab-scroll-arrow .glyphicon,
        .tall .scrtabs-tab-container .scrtabs-tab-scroll-arrow .glyphicon {
            margin-top: 50px;
        }

        .tall .tab-content {
            height: calc(100vh - (125px + 4em));
            overflow-y: auto;
            overflow-x: hidden;
        }

        .tab-content {
            margin: 0.7em 0;
        }

        .tab-content h2 {
            color: #80cc28;
            margin-bottom: 30px;
        }

        .tab-content h3 {
            color: #434951;
            font-weight: 600;
        }

        .nav-tabs > li.active {
            cursor: default;
            background: #fff;
            color: #434951;
            border: 1px solid #ddd;
            border-top: 5px solid #80cc28;
            border-bottom-color: transparent;
        }

        .nav-tabs > li {
            
            padding: 0.1em 0.1em 0.1em;
            border: 1px solid #fff;
            border-top: 5px solid transparent;
            border-radius: 0;
            border-bottom-color: #ddd;
            margin: 0;
            min-width: 120px;
            font-size: 15px;
            max-width: 200px;
            white-space: normal;
            background: #f2f2f2;
            transition: border-top ease-out 0.3s, background ease-out 0.3s;
        }

        .nav-tabs > li a {
            color: #a8adb4;
            transition: color ease-out 0.3s;
        }

        .nav-tabs > li:hover {
            border-top: 5px solid #a8adb4;
        }

        .nav-tabs > li:hover a {
            color: #6d747e;
        }

        .nav > li > a {
            display: block;
            position: relative;
            width: 100%;
            height: 100%;
            line-height: 1.2em;
            margin: 0;
        }

        .nav-tabs input {
            position: relative;
        }
        /*AutoComplete flyout */
       

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

        .autocomplete_highlightedListItem {
            background-color: yellow;
            color: black;
            padding: 1px;
        }



        .autocomplete_listItem {
            background-color: white;
            color: blue;
            padding: 0px;
        }
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
            background-color: white;
            border: none;
            color: black;
            padding: 8px 12px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 12px;
            margin: 4px 2px;
            cursor: pointer;
            text-decoration: none;
            box-shadow: 0 0 3px 1px rgba(0,0,0,.35);
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


        .SelectchkChoice label {
            padding-left: 6px;
            font-weight: bold;
        }

        
        .SelectchkChoiceDsss label {
            padding-left:1px;
        }
        .chkChoice label {
            padding-left: 10px;
            padding-right: 30px;
        }

        .chkChoiceStep label {
            padding-left: 8px;
            padding-right: 10px;
        }

        .chkChoiceDesignation label {
            padding-left: 8px;
            padding-right: 7px;
        }

        .chkChoiceHeader label {
            padding-left: 4px;
            padding-right: 20px;
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
        #cpFormBody_loadGridView td  {
            border: 1px solid #ddd !important;
            padding: 8px !important;
        }

        #cpFormBody_loadGridView tr:nth-child(even){background-color: white !important;}

 

        #cpFormBody_loadGridView th {
            padding: 10px !important;
            border-style: none !important;

            background-color: #CCCCCC !important;
            color: black !important;
            font-weight: bold !important;
            font-size: 13px !important;
            text-align: center;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css">
    <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>--%>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>

    <div class="content" id="content" style="background-color: white">


        <div class="container-fluid" style="background-color: white">
            <div class="page-heading">
                <div class="page-heading__container">
                    <h1 class="title" style="font-size: 18px; padding-top: 0px;">
                        <img src="app.png"  width="20px" />  Employee Information Report</h1>
                </div>
                <%--<div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="detailsViewButton" Text="List Information " CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>--%>
            </div>

            <%--    <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                        <ProgressTemplate>
                            <div class="divWaitingforfun">
                                <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>--%>

            <div class="card">
                <div class="card-body" style="background-color: white">


                    <div class="row">
                        
                        <div class="col-md-12">
                            
                            
                             <asp:UpdatePanel runat="server" ID="UpdatePanel44">
                                    <ContentTemplate>
                                        
                                        <asp:UpdateProgress ID="progrzzess" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                        <ProgressTemplate>
                            <div class="divWaitingforfun">
                                <asp:Image ID="imgWasaait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                            <fieldset class="for-panel">
                                
                                 
                                <legend>Report Header   <asp:CheckBox runat="server" ID="chkRpt" AutoPostBack="True" OnCheckedChanged="chkRpt_OnCheckedChanged"  /></legend>

                                <div class="row">


                                    <div class="col-md-12">
                                        <asp:CheckBoxList ID="cblHeader" RepeatDirection="Vertical" RepeatColumns="6" CssClass="chkChoiceHeader" runat="server">
                                            <asp:ListItem Selected="True">Company Name</asp:ListItem>
                                            <asp:ListItem Selected="True">Employee ID</asp:ListItem>
                                            <asp:ListItem>Employee Old ID</asp:ListItem>
                                            <asp:ListItem Selected="True">Employee Name</asp:ListItem>
                                            <asp:ListItem Selected="True">Designation</asp:ListItem>
                                            <asp:ListItem Selected="True">Department</asp:ListItem>
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
                                            <asp:ListItem>National Id No</asp:ListItem>
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
                                            <asp:ListItem>SMC Service Length</asp:ListItem>
                                            <asp:ListItem>Carrier Service Length</asp:ListItem>

                                            <asp:ListItem>Date of Retirement</asp:ListItem>
                                            <asp:ListItem>Probition End Date</asp:ListItem>
                                            <asp:ListItem>Contractual  End Date</asp:ListItem>
                                            <asp:ListItem>Supervisor</asp:ListItem>
                                            <asp:ListItem>Salary Amount</asp:ListItem>
                                            <asp:ListItem>Contract Period</asp:ListItem>
                                            <asp:ListItem>Parents Name</asp:ListItem>
                                            <asp:ListItem>Personal Mobile</asp:ListItem>
                                            <asp:ListItem>Emergency Number</asp:ListItem>
                                            <asp:ListItem>Personal Email</asp:ListItem>
                                            <asp:ListItem>Official Email</asp:ListItem>
                                            <asp:ListItem>Age</asp:ListItem>
                                            <asp:ListItem>Type of Position</asp:ListItem>
                                            <asp:ListItem>Employment Type</asp:ListItem>
                                            <asp:ListItem>Retirement Date </asp:ListItem>
                                            <asp:ListItem>Tin No. </asp:ListItem>
                                            <asp:ListItem>Nominee</asp:ListItem>
                                            <asp:ListItem>Separation Date</asp:ListItem>
                                            
                                            
                                            <asp:ListItem>Recruitment Type (New)</asp:ListItem>

                                            <asp:ListItem>Recruitment Type (Replacement)</asp:ListItem>
                                            <asp:ListItem>Reappointment</asp:ListItem>
                                            
                                            
                                            <asp:ListItem>Marital Status</asp:ListItem>
                                            <asp:ListItem>Marriage Date</asp:ListItem>
                                            <asp:ListItem>Emergency Contact Person Name</asp:ListItem>


                                            <asp:ListItem>Present Telephone Number</asp:ListItem>
                                            <asp:ListItem>Permanent Telephone Number</asp:ListItem>
                                            <asp:ListItem>Emergency Contact Person's Address</asp:ListItem>



                                           <%-- <asp:ListItem>Last Promotion Date</asp:ListItem>
                                            <asp:ListItem>Last Upgradation Date</asp:ListItem>
                                            <asp:ListItem>Last Special Increment Date</asp:ListItem>--%>

                                         
                                               
                                        </asp:CheckBoxList>

                                    </div>
                                </div>
                                        
                                        
                                        

                            </fieldset>
                                        
                                        </ContentTemplate>
                                      </asp:UpdatePanel>
                        </div>
                                        
                                        
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <fieldset class="for-panel">
                                <legend>Common Filtering Criteria
</legend>

                                <asp:UpdatePanel runat="server" ID="UpdsatePanel1">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-md-4">
                                                     <div class="row">
                                                          <div class="col-8">
                                                <div class="form-group ">
                                                    <label class="control-label">Company </label>
                                                    <asp:DropDownList runat="server" ID="ddlCompany" class="form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" />
                                                </div>
                                            </div>
</div>
                                                
                                                 <div class="row">
                                            <div class="col-12">
                                                <div class="form-group ">
                                                    <label>Employee Name </label>
                                                    
                                                                <asp:DropDownList AutoPostBack="True" OnSelectedIndexChanged="ddlEmpInfo_OnSelectedIndexChanged" runat="server"   ID="ddlEmpInfo" class="form-control form-control-sm" />
                                                    <script type="text/javascript">
                                                        function pageLoad() {
                                                            $('#<%=ddlEmpInfo.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });
                                                        }
</script>

                                                    <asp:TextBox Visible="False" ID="SearchEmployeeNameTextBoxTextBox" AutoPostBack="True" runat="server" class="form-control form-control-sm" OnTextChanged="SearchEmployeeNameTextBoxTextBox_OnTextChanged"></asp:TextBox>
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

                                            </div>
                                            
                                              <div class="col-md-5">


                                                        <div class="row" runat="server" Visible="False">
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
                                                                    <div style="overflow: scroll; height: 300px">

                                                                        <br />
                                                                        <asp:TreeView ID="heirerchicalTreeView" NodeStyle-CssClass="treeNode" ShowExpandCollapse="True"
                                                                            RootNodeStyle-CssClass="rootNode"
                                                                            LeafNodeStyle-CssClass="leafNode"
                                                                            ExpandImageUrl="../Assets/plus-sign.png" CollapseImageUrl="../Assets/minus-sign.png" runat="server" ShowCheckBoxes="All" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                             <div class="col-md-2">
                                                        <div class="form-group">
                                                            <label>Employment Status </label>

                                                            <asp:DropDownList ID="empStatusDropDownList" AutoPostBack="True" OnSelectedIndexChanged="empStatusDropDownList_OnSelectedIndexChanged" class="form-control form-control-sm" runat="server">
                                                                <asp:ListItem Value="0">All</asp:ListItem>
                                                                <asp:ListItem Selected="True" Value="Yes">Active</asp:ListItem>
                                                                <asp:ListItem Value="No">Inactive</asp:ListItem>
                                                                <%--<asp:ListItem Value="Special">Special Transfer</asp:ListItem>--%>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                      
                                            </div>
                                            
                                            
                                            
                                                  <div class="row">
                                                    <div class="col-md-6">


                                                        <div class="row" runat="server" Visible="False">
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
                                                            <div class="col-md-12" runat="server" visible="false">
                                                               
                                                                <div class="Label_Title">Grade List</div>
                                                              
                                                                <div class="form-group">
                                                                    <div style="overflow: scroll; height: 230px">
                                                                         <asp:CheckBox runat="server" ID="SSGradeCheck" CssClass="SelectchkChoice" AutoPostBack="True" OnCheckedChanged="SSGradeCheck_OnCheckedChanged" Text=" Select All / Unselect All" />
                                                                        <br />
                                                                        <asp:CheckBoxList ID="gradeCheckBoxList" AutoPostBack="True" OnSelectedIndexChanged="gradeCheckBoxList_OnSelectedIndexChanged" CssClass="chkChoice" RepeatColumns="5" RepeatDirection="Horizontal" runat="server"></asp:CheckBoxList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>

                                                    <div class="col-md-6">


                                                        <div class="row" runat="server" Visible="False">
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
                                                            <div class="col-md-12" runat="server" visible="false">
                                                               
                                                                <div class="Label_Title">Step List</div>
                                                                <div class="form-group">
                                                                    <div style="overflow: scroll; height: 230px">
                                                                         <asp:CheckBox runat="server" ID="SSstepCheckBox" CssClass="SelectchkChoice" AutoPostBack="True" OnCheckedChanged="SSstepCheckBox_OnCheckedChanged" Text=" Select All / Unselect All" />

                                                                        <br />
                                                                        <asp:CheckBoxList ID="stepCheckBoxList" CssClass="chkChoiceStep" RepeatColumns="3" RepeatDirection="Horizontal" runat="server"></asp:CheckBoxList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                         
                                        
                                              <div class="row">
                                                    <div class="col-md-6">


                                                        <div class="row" runat="server" Visible="False">
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
                                                                         <asp:CheckBox runat="server" ID="SSOfficeCK" CssClass="SelectchkChoice" AutoPostBack="True" OnCheckedChanged="SSOfficeCK_OnCheckedChanged" Text=" Select All / Unselect All" />
                                                                        <br />

                                                                        <asp:CheckBoxList ID="salLocCheckBoxList"  AutoPostBack="True" OnSelectedIndexChanged="chkPlace_OnSelectedIndexChanged"   CssClass="chkChoiceStep" RepeatColumns="2" RepeatDirection="Horizontal" runat="server"></asp:CheckBoxList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>

                                                     <div class="col-md-6">


                                                        <div class="row" runat="server" Visible="False">
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <label>Place  </label>

                                                                    <asp:DropDownList ID="PlaceDropDownList" class="form-control form-control-sm" runat="server">
                                                                        <asp:ListItem> In </asp:ListItem>
                                                                        <asp:ListItem> Not In </asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-12">
                                                               
                                                                <div class="Label_Title">Place List</div>
                                                                <div class="form-group">
                                                                    <div style="overflow: scroll; height: 150px">
                                                                         <asp:CheckBox runat="server" ID="SSchkPlace" CssClass="SelectchkChoice" AutoPostBack="True" OnCheckedChanged="SSchkPlace_OnCheckedChanged_OnCheckedChanged" Text=" Select All / Unselect All" />
                                                                        <br />

                                                                        <asp:CheckBoxList ID="chkPlace" CssClass="chkChoiceStep" RepeatColumns="2" RepeatDirection="Horizontal" runat="server"></asp:CheckBoxList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                   
                                                </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </fieldset>
                        </div>
                    </div>




                    <ul class="nav nav-tabs" role="tablist">
                        <li role="presentation" class="active"><a href="#GeneralInformation" aria-controls="GeneralInformation" role="tab" data-toggle="tab">Employee Details</a></li>
                        <li role="presentation" ><a href="#EmploymentInformation" aria-controls="EmploymentInformation" role="tab" data-toggle="tab">Education</a></li>
                          <li role="presentation" ><a href="#Experience" aria-controls="Experience" role="tab" data-toggle="tab">Experience</a></li>
                          <li role="presentation" ><a href="#Family" aria-controls="Family" role="tab" data-toggle="tab">Family</a></li>
                        <li role="presentation" ><a href="#Training" aria-controls="Training" role="tab" data-toggle="tab">Training</a></li>

                        <li role="presentation" ><a href="#DiciplinaryActionTab" aria-controls="DiciplinaryActionTab" role="tab" data-toggle="tab">Disciplinary Action</a></li>

                        <li role="presentation" ><a href="#Promotion" aria-controls="Promotion" role="tab" data-toggle="tab">Promotion</a></li>
                        <li role="presentation" ><a href="#Separation" aria-controls="Separation" role="tab" data-toggle="tab">Separation</a></li>
                        <li role="presentation" ><a href="#Increment" aria-controls="Increment" role="tab" data-toggle="tab">Increment</a></li>
                        <li role="presentation" ><a href="#EmpBirthDay" aria-controls="EmpBirthDay" role="tab" data-toggle="tab">BirthDay</a></li>
                        <li role="presentation" ><a href="#EmpMedicalCheckUp" aria-controls="EmpMedicalCheckUp" role="tab" data-toggle="tab">Medical Check-Up</a></li>
                        <li role="presentation" ><a href="#Contractual" aria-controls="Contractual" role="tab" data-toggle="tab">Contractual</a></li>
                        <li role="presentation" ><a href="#Project" aria-controls="Project" role="tab" data-toggle="tab">Project</a></li>
                        <li role="presentation" ><a href="#KPI" aria-controls="KPI" role="tab" data-toggle="tab">KPI</a></li>
                        <li role="presentation" ><a href="#Appraisal" aria-controls="KPI" role="tab" data-toggle="tab">Performance Appraisal</a></li>
                        <li role="presentation" ><a href="#Survey" aria-controls="KPI" role="tab" data-toggle="tab">Survey</a></li>
                        
                        
                        
                          <li role="presentation" ><a href="#Recruitment" aria-controls="Recruitment" role="tab" data-toggle="tab">Recruitment</a></li>
                      
                    </ul>


                    <div class="tab-content">

                        <div role="tabpanel" class="tab-pane active" id="GeneralInformation">

                            <asp:UpdatePanel ID="upFormBody" runat="server">
                                
                                <ContentTemplate>
 <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaitingforfun">
                                        <asp:Image ID="imgWait1" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>

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



                                                    <div class="col-3" runat="server" visible="False">
                                                        <div class="form-group">
                                                            <label>Employee No </label>
                                                            <asp:TextBox runat="server" ID="empNoTextBox" class="form-control form-control-sm" />

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
                                                   <div class="col-md-5">


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
                                                                         <asp:CheckBox runat="server" ID="SSDesignationCK" CssClass="SelectchkChoiceDsss" AutoPostBack="True" OnCheckedChanged="SSDesignationCK_OnCheckedChanged" Text=" Select All / Unselect All" />
                                                                        <br />

                                                                        <asp:CheckBoxList ID="desigCheckBoxList" CssClass="chkChoiceDesignation" RepeatColumns="2" RepeatDirection="Horizontal" runat="server"></asp:CheckBoxList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>

                                                    <div class="col-md-2">


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
                                                    <div class="col-md-1" style="padding: 18px;">

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
                                                                         <asp:CheckBox runat="server" ID="SSDistrictCk" CssClass="SelectchkChoice" AutoPostBack="True" OnCheckedChanged="SSDistrictCk_OnCheckedChanged" Text=" Select All / Unselect All" />
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
                                                                        
                                                                <asp:CheckBox runat="server" ID="SSThanaCK" CssClass="SelectchkChoice" AutoPostBack="True" OnCheckedChanged="SSThanaCK_OnCheckedChanged" Text=" Select All / Unselect All" />
                                                                        <br />
                                                                        <asp:CheckBoxList ID="permThanaCheckBoxList1" RepeatColumns="3" RepeatDirection="Horizontal" CssClass="chkChoiceStep" runat="server">
                                                                        </asp:CheckBoxList>

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

                                                   
                                                    <div class="col-md-2" runat="server" visible="False" id="jobleft" style="display: none">
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
                                                    <div class="col-md-2" runat="server" visible="False" id="suspend" style="display: none">
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
                                                    <div class="col-md-2" style="padding-top: 24px;">
                                                        <div class="form-group">


                                                            <asp:DropDownList ID="emptypeDropDownList" class="form-control form-control-sm" runat="server">
                                                                <asp:ListItem Value="0">Select From List</asp:ListItem>
                                                                <asp:ListItem Value="2">Contractual</asp:ListItem>
                                                                <asp:ListItem Value="1">Permanent</asp:ListItem>
                                                                <asp:ListItem Value="3">Programme Contractual</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-1" style="padding-top: 24px;">

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

                                                    <div class="col-md-1" style="padding-top: 24px;">

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


                                                    <div class="col-md-1" style="padding-top: 24px;">

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

                                                    <div class="col-md-1" style="padding-top: 24px;">

                                                        <asp:Button ID="Button45" runat="server" CssClass="btn btn-sm btn-outline-success" OnClick="Button45_OnClick" Text="Clear" />
                                                    </div>

                                                </div>
                                                <div class="row">
                                                    <div class="col-md-2">
                                                        <div class="form-group">
                                                            <label>SMC Service Length </label>
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


                                                    <div class="col-md-1" style="padding-top: 24px;">

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

                                                    <div class="col-md-1" style="padding-top: 24px;">
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

                                                    <div class="col-md-1" style="padding-top: 24px;">
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


                                                    <div class="col-md-1" style="padding-top: 24px;">
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

                                                    <div class="col-md-1" style="padding-top: 24px;">
                                                        <asp:Button ID="Button50" runat="server" CssClass="btn btn-sm btn-outline-success" OnClick="Button50_OnClick" Text="Clear" />
                                                    </div>

                                                </div>


                                                   <div class="row">
                                                    <div class="col-md-2">
                                                        <div class="form-group">
                                                            <label>Salary Range </label>
                                                            <%--<label style="color: #a52a2a">* </label>--%>
                                                            <asp:DropDownList ID="SalaryRangeDropDownList" AutoPostBack="True" OnSelectedIndexChanged="SalaryRangeDropDownList_OnSelectedIndexChanged" runat="server" class="form-control form-control-sm">
                                                                <asp:ListItem Value="1"> No Filter </asp:ListItem>
                                                                <asp:ListItem Value="2"> = </asp:ListItem>
                                                                <asp:ListItem Value="3"> < </asp:ListItem>
                                                                <asp:ListItem Value="4"> > </asp:ListItem>
                                                                <asp:ListItem Value="5"> Between </asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-2" runat="server" id="SalDtSingle" visible="False">
                                                        <div class="form-group">
                                                            <label>Salary Range: </label>
                                                            <asp:TextBox ID="txtSalaryRange" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                                    TargetControlID="txtSalaryRange" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                        </div>
                                                    </div>


                                                    <div runat="server" id="SalDtRange" visible="False" class="col-md-4">

                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>Salary Range From: </label>
                                                                    <asp:TextBox ID="txtSalaryRangeFrom" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                                     <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" runat="server" Enabled="True"
                                                                    TargetControlID="txtSalaryRangeFrom" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>Salary Range To: </label>
                                                                    <asp:TextBox ID="txtSalaryRangeTo" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                                      <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                                    TargetControlID="txtSalaryRangeTo" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-1" style="padding-top: 24px;">
                                                        <asp:Button ID="Button51" runat="server" CssClass="btn btn-sm btn-outline-success" OnClick="asadassdButton50_OnClick" Text="Clear" />
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



                                            <div class="form-row" runat="server" visible="False">
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
                                                        <legend>Filtering Criteria </legend>
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

                                </ContentTemplate>

                            </asp:UpdatePanel>

                            <div class="form-row" style="padding-right: 10px">

                                <div class="col-md-6" style="padding-left: 2px">
                                    <label style="font-size: 18px;">Details Information</label>
                                </div>
                                <div class="col-md-2">
                                </div>
                                  <style>.ssss {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  font-size: 13px;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  font-weight: bold;
                                                                          
                                               
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              }</style>
                              
                                     <div class="col-md-2"  style="margin-top: 22px; padding: 5px;">
                                       
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel41">
                                    <ContentTemplate>
                                              <asp:Label ID="lblCount" runat="server" CssClass="ssss pull-right"   Text="Total : 0" ></asp:Label>
                                             
                               </ContentTemplate>
                                                    </asp:UpdatePanel>
                                        
                                      
                                     </div>
                                <div class="col-md-2">
                                    <%--  <asp:LinkButton ID="btnExportToExcel" runat="server" CssClass="btnexcel  pull-right" Style="padding-right: 10px;" OnClick="btnExportToExcel_Click"><span aria-hidden="true" style="font-size: 14px; color: #4CAF50;" class="fa fa-file-excel-o"></span> Download to xls</asp:LinkButton>--%>
                                        <asp:LinkButton ID="btnExportToExcel" runat="server"  CssClass="btnexcel  pull-right" OnClick="btnExportToExcel_Click" ><i class="fa fa-file-excel-o"></i> Export To Excel</asp:LinkButton>
                                    <%--<input type="button" id="btnExportEmpDetails" title="Export to Excel" class="pull-right btnexcelcc " value="" />--%>
                                </div>


                            </div>

                            <hr />
                            <div style="padding: 10px">
                                <asp:UpdatePanel runat="server" ID="ssas">
                                    <ContentTemplate>

                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaitingforfun">
                                        <asp:Image ID="imgWait2" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>

                                        <div style="overflow: scroll; height: 500px; width: 100%">
                                            <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                                CssClass="table table-bordered  text-center thead-light" DataKeyNames="EmpInfoId" PageIndex="0">

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

                                                    <asp:BoundField DataField="DOB" HeaderText="Date Of Birth" DataFormatString="{0:dd-MMM-yyyy}" />
                                                    <asp:BoundField DataField="Nationality" HeaderText="Nationality" />
                                                    <asp:BoundField DataField="NationalIdNo" HeaderText="National Id No." />
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



                                                    <asp:BoundField DataField="Doj" HeaderText="Joining Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                    <asp:BoundField DataField="DateOfConformation" HeaderText="Confirmation Date" DataFormatString="{0:dd-MMM-yyyy}" />


                                                    <asp:BoundField DataField="ServiceLength" HeaderText="SMC Service Length" />
                                                     <asp:BoundField DataField="EmpExperiece" HeaderText="Carrier Service Length"  />

                                                    <asp:BoundField DataField="DateofRetirement" HeaderText="Date of Retirement" DataFormatString="{0:dd-MMM-yyyy}" />

                                                    <asp:BoundField DataField="ProbitionEndDate" HeaderText="Probation End Date" DataFormatString="{0:dd-MMM-yyyy}" />


                                                    <asp:BoundField DataField="ContractualEndDate" HeaderText="Contractual End Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                    <asp:BoundField DataField="Supervisor" HeaderText="Supervisor" />
                                                    <asp:BoundField DataField="GrossAmount" HeaderText="Salary Amount" />
                                                     <asp:BoundField DataField="ContractPeriod" HeaderText="Contract Period"  />
                                                     <asp:BoundField DataField="FatherName" HeaderText="Father's Name"  />
                                                     <asp:BoundField DataField="MotherName" HeaderText="Mother's Name"  />


                                                     <asp:BoundField DataField="PersonalMobile" HeaderText="Personal Mobile"  />
                                                     <asp:BoundField DataField="EmergencyContactNumber" HeaderText="Emergency Number"  />
                                                    
                                                             <asp:BoundField DataField="PersonalEmail" HeaderText="Personal Email"  />
                                                     <asp:BoundField DataField="OfficialEmail" HeaderText="Official Email"  />
                                                     <asp:BoundField DataField="EmpAge" HeaderText="Age"  />
                                                     <asp:BoundField DataField="empType" HeaderText="Type of Position"  />
                                                     <asp:BoundField DataField="Category" HeaderText="Employment Type"  />
                                                        <asp:BoundField DataField="DateOfRetirement" HeaderText="Retirement Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                     <asp:BoundField DataField="TinNo" HeaderText="Tin No."  />
                                                     <asp:BoundField DataField="NomineeName" HeaderText="Nominee"  />
                                                    
                                                      <asp:BoundField DataField="JobLeftDate" HeaderText="Separation date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                     
                                                     <asp:BoundField DataField="RecruitmentTypeNew" HeaderText="Recruitment Type (New)"  />
                                                     <asp:BoundField DataField="RecruitmentTypeReplacement" HeaderText="Recruitment Type (Replacement)"  />
                                                    <asp:BoundField DataField="reappointment" HeaderText="Reappointment"  />
                                                    
                                                    <asp:BoundField DataField="MaritalStatus" HeaderText="Marital Status"  />
                                                    <asp:BoundField DataField="MarriageDate" HeaderText="Marriage Date"  />
                                                    <asp:BoundField DataField="EmergencyContactPerson" HeaderText="Emergency Contact Person Name"  />
                                                                                           
                                                    
                                                       <asp:BoundField DataField="PresentTelNo" HeaderText="Present Telephone Number"  />
<asp:BoundField DataField="PermanantTelNo" HeaderText="Permanent Telephone Number"  />
<asp:BoundField DataField="EmergencyContactAddress" HeaderText="Emergency Contact Person's Address"  />
                                                  
                                                    
                                                  
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                        </div>

                        <div role="tabpanel" class="tab-pane" id="EmploymentInformation">

                            <div class="row">
                                <div class="col-md-12">
                                    <fieldset class="for-panel">
                                        <legend>Education Information </legend>
                                        <div class="row">
                                            <div class="col-md-6">




                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="Label_Title">Name of Education List</div>
                                                        <div class="form-group">
                                                            <div style="overflow: scroll; height: 150px">

                                                                <br />

                                                                <asp:CheckBoxList ID="cblNameEducation" CssClass="chkChoiceStep" RepeatColumns="2" RepeatDirection="Horizontal" runat="server"></asp:CheckBoxList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>


                                            <div class="col-md-6">



                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="Label_Title">Subject/Group List</div>
                                                        <div class="form-group">
                                                            <div style="overflow: scroll; height: 150px">

                                                                <br />

                                                                <asp:CheckBoxList ID="cblSubjectGroup" CssClass="chkChoiceDesignation" RepeatColumns="2" RepeatDirection="Horizontal" runat="server"></asp:CheckBoxList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        
                                           <div class="row">
                                            <div class="col-md-3">
                                                <asp:CheckBox CssClass="chkChoiceDesignation" ID="IsEduLastLabel" Text="Is Education Last Level" runat="server" />
                                                </div>
                                               </div>

                                    </fieldset>
                                </div>
                            </div>

                            <br />
                            <br />

                            <asp:UpdatePanel runat="server" ID="ssss">
                                <ContentTemplate>
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaitingforfun ">
                                        <asp:Image ID="imgWait3" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <%--<asp:Button runat="server" ID="Button51" OnClick="btn_Save_OnClick" Text="Search " CssClass="btn btn-outline-info btn-block disabled btn-sm" />--%>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:LinkButton runat="server" ID="lbEducationSearch" OnClick="lbEducationSearch_OnClick" CssClass="btn btn-info   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search </asp:LinkButton>

                                            <asp:LinkButton runat="server" ID="lbEducationReset" OnClick="lbEducationReset_OnClick" CssClass="btn btn-warning   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset </asp:LinkButton>
                                        </div>
                                        <div class="col-md-4">
                                            <%--<asp:Button runat="server" ID="Button51" OnClick="btn_Save_OnClick" Text="Search " CssClass="btn btn-outline-info btn-block disabled btn-sm" />--%>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <br />
                            <br />
                            <div class="form-row" style="padding-right: 10px">

                                <div class="col-md-6" style="padding-left: 2px">
                                    <label style="font-size: 18px;">Details Information</label>
                                </div>
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-2">
                                    <%-- <asp:LinkButton ID="lblExportToExcelEducation" runat="server" CssClass="btnexcel  pull-right" style="padding-right: 10px;" OnClick="lblExportToExcelEducation_Click"><span aria-hidden="true" style="font-size: 14px; color: #4CAF50;" class="fa fa-file-excel-o"></span> Download to xls</asp:LinkButton>--%>

                                    <input type="button" id="btnExportEducation" title="Export to Excel" class="pull-right btnexcelcc " value="" />
                                </div>


                            </div>

                            <hr />
                            <div style="padding: 10px">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel12">
                                    <ContentTemplate>

                                        <asp:UpdateProgress ID="UpdateProgress3" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaitingforfun ">
                                        <asp:Image ID="imgWait4" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>

                                        <div style="overflow: scroll; height: 500px; width: 100%">
                                            <asp:GridView ID="gv_Education" runat="server" AutoGenerateColumns="False"
                                                CssClass="table table-bordered  text-center thead-light" PageIndex="0">

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
                                                    <asp:BoundField DataField="NationalIdNo" HeaderText="National Id No." />
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
                                                   

                                                    <asp:BoundField DataField="ServiceLength" HeaderText="SMC Service Length" />

                                                    <asp:BoundField DataField="DateofRetirement" HeaderText="Date of Retirement" DataFormatString="{0:dd-MMM-yyyy}" />

                                                    <asp:BoundField DataField="ProbitionEndDate" HeaderText="Probation End Date" DataFormatString="{0:dd-MMM-yyyy}" />


                                                    <asp:BoundField DataField="ContractualEndDate" HeaderText="Contractual End Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                      <asp:BoundField DataField="Supervisor" HeaderText="Supervisor" />
                                                    <asp:BoundField DataField="GrossAmount" HeaderText="Salary Amount" />
                                                     <asp:BoundField DataField="ContractPeriod" HeaderText="Contract Period"  />
                                                      <asp:BoundField DataField="FatherName" HeaderText="Father's Name"  />
                                                     <asp:BoundField DataField="MotherName" HeaderText="Mother's Name"  />

                                                    <asp:BoundField DataField="EducationName" HeaderText="Education Name" />
                                                    <asp:BoundField DataField="BoardUniversity" HeaderText="Board/University" />
                                                    <asp:BoundField DataField="SubjectGroup" HeaderText="Subject/Group" />
                                                    <asp:BoundField DataField="EducationalInstitute" HeaderText="Educational Institute" />

                                                    <asp:BoundField DataField="FieldOfSpecialization" HeaderText="Field Of Specialization" />
                                                    <asp:BoundField DataField="PassingYear" HeaderText="Passing Year" />
                                                    <asp:BoundField DataField="Result" HeaderText="Result" />
                                                    <asp:BoundField DataField="CgpaOrTotalMarks" HeaderText="CGPA Or Total Marks" />
                                                    <asp:BoundField DataField="EduIsLastLevel" HeaderText="Is Last Level" />
                                                    <asp:BoundField DataField="IsProfessionalEdu" HeaderText="Is Professional" />



                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                        </div>


                        <div role="tabpanel" class="tab-pane" id="DiciplinaryActionTab">

                            <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                <ContentTemplate>
                                    <asp:UpdateProgress ID="UpdateProgress4" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaitingforfun">
                                        <asp:Image ID="imgWait5" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>

                                    <div class="row">
                                        <div class="col-md-12">
                                            <fieldset class="for-panel">
                                                <legend> Filtering Criteria </legend>
                                                <div class="row">

                                                    <div class="col-md-2" runat="server">
                                                        <div class="form-group">
                                                            <label>Report Name</label>
                                                            <span style="color: red">&nbsp;*</span>
                                                            <asp:DropDownList ID="reportDropDownList" AutoPostBack="True" runat="server" class="form-control form-control-sm" OnSelectedIndexChanged="reportDropDownList_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Select One</asp:ListItem>
                                                                <asp:ListItem Value="Suspend">Employee Suspend List</asp:ListItem>
                                                                <asp:ListItem Value="Diciplinary">Disciplinary Action List</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>



                                                    <div class="col-md-2" runat="server">
                                                        <div class="form-group">
                                                            <label>Financial Year </label>

                                                            <asp:DropDownList ID="FinancialYearDropDownList" class="form-control form-control-sm" runat="server" AutoPostBack="True"></asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="col-2" runat="server" visible="False">
                                                        <div class="form-group">
                                                            <label>Increment Type</label>
                                                             
                                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlIncrementType" CssClass="form-control form-control-sm" />
                                                        </div>
                                                    </div>

                                                    <div class="col-2">
                                                        <div class="form-group">
                                                            <label><span style="font-size: 11px; font-weight: bold;">Action Type: </span></label>
                                                            <asp:DropDownList ID="actionTypeDropDownList" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-2">
                                                        <div class="form-group">
                                                            <label runat="server" id="Label11">Effective From Date </label>

                                                            <asp:TextBox ID="EffectiveDateTextBox" AutoCompleteType="Disabled" AutoPostBack="True" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                                            <asp:CalendarExtender ID="CalendarExtender29" runat="server" PopupPosition="TopLeft"
                                                                Format="dd-MMM-yyyy" PopupButtonID="EffectiveDateTextBox" CssClass="MyCalendar"
                                                                TargetControlID="EffectiveDateTextBox" />
                                                        </div>
                                                    </div>

                                                    <div class="col-md-2">
                                                        <div class="form-group">
                                                            <label runat="server" id="Label12">Effective To Date </label>

                                                            <asp:TextBox ID="EffectToDate" AutoCompleteType="Disabled" AutoPostBack="True" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                                            <asp:CalendarExtender ID="CalendarExtender30" runat="server" PopupPosition="TopLeft"
                                                                Format="dd-MMM-yyyy" PopupButtonID="EffectToDate" CssClass="MyCalendar"
                                                                TargetControlID="EffectToDate" />
                                                        </div>
                                                    </div>






                                                </div>



                                            </fieldset>
                                            <br />
                                            <br />

                                            <div class="row">
                                                <div class="col-md-3">
                                                </div>
                                                <div class="col-md-1">
                                                </div>
                                                <div class="col-md-4">

                                                    <div class="form-group" style="margin-top: 17px;">


                                                        <asp:LinkButton runat="server" ID="SearchButtonDisciplinaryAction" OnClick="SearchButtonDisciplinaryAction_OnClick" ToolTip="Click To Search" CssClass="btn btn-info   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search </asp:LinkButton>

                                                        <asp:LinkButton runat="server" ID="ResetSearchButtonDisciplinaryAction" OnClick="ResetSearchButtonDisciplinaryAction_OnClick" CssClass="btn btn-warning   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>


                            <div class="form-row" style="padding-right: 10px">

                                <div class="col-md-6" style="padding-left: 2px">
                                    <label style="font-size: 18px;">Details Information</label>
                                </div>
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-2">
                                    <%--  <asp:LinkButton ID="btnExportToExcel" runat="server" CssClass="btnexcel  pull-right" Style="padding-right: 10px;" OnClick="btnExportToExcel_Click"><span aria-hidden="true" style="font-size: 14px; color: #4CAF50;" class="fa fa-file-excel-o"></span> Download to xls</asp:LinkButton>--%>

                                    <input type="button" id="btnExportDisciplinary" title="Export to Excel" class="pull-right btnexcelcc " value="" />
                                </div>


                            </div>

                            <hr />
                            <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                <ContentTemplate>
                                    <asp:UpdateProgress ID="UpdateProgress5" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaitingforfun">
                                        <asp:Image ID="imgWait6" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                                    <div id="gridContainer1" style="overflow: scroll; height: 500px; width: 100%">
                                        <asp:GridView ID="SusPendGridView" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-bordered text-center thead-dark">
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
                                                <asp:BoundField DataField="NationalIdNo" HeaderText="National Id No." />
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
                                               

                                                <asp:BoundField DataField="ServiceLength" HeaderText="SMC Service Length" />

                                                <asp:BoundField DataField="DateofRetirement" HeaderText="Date of Retirement" DataFormatString="{0:dd-MMM-yyyy}" />

                                                <asp:BoundField DataField="ProbitionEndDate" HeaderText="Probation End Date" DataFormatString="{0:dd-MMM-yyyy}" />


                                                <asp:BoundField DataField="ContractualEndDate" HeaderText="Contractual End Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                   <asp:BoundField DataField="Supervisor" HeaderText="Supervisor" />
                                                    <asp:BoundField DataField="GrossAmount" HeaderText="Salary Amount" />
                                                     <asp:BoundField DataField="ContractPeriod" HeaderText="Contract Period"  />
                                                   <asp:BoundField DataField="FatherName" HeaderText="Father's Name"  />
                                                     <asp:BoundField DataField="MotherName" HeaderText="Mother's Name"  />
                                                <asp:BoundField DataField="SuspendReasonEntry" HeaderText="Action Taken" />
                                                <asp:BoundField DataField="EffectiveDate" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Effective From Date" />
                                                <asp:BoundField DataField="EffectiveToDate" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Effective To Date" />



                                            </Columns>
                                        </asp:GridView>



                                        <asp:GridView ID="DisplinaryGridView" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-bordered text-center thead-dark"
                                            AllowSorting="True">
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
                                                <asp:BoundField DataField="NationalIdNo" HeaderText="National Id No." />
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


                                                <asp:BoundField DataField="ServiceLength" HeaderText="SMC Service Length" />

                                                <asp:BoundField DataField="DateofRetirement" HeaderText="Date of Retirement" DataFormatString="{0:dd-MMM-yyyy}" />

                                                <asp:BoundField DataField="ProbitionEndDate" HeaderText="Probation End Date" DataFormatString="{0:dd-MMM-yyyy}" />


                                                <asp:BoundField DataField="ContractualEndDate" HeaderText="Contractual End Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                 <asp:BoundField DataField="Supervisor" HeaderText="Supervisor" />
                                                    <asp:BoundField DataField="GrossAmount" HeaderText="Salary Amount" />
                                                     <asp:BoundField DataField="ContractPeriod" HeaderText="Contract Period"  />
                                                     <asp:BoundField DataField="FatherName" HeaderText="Father's Name"  />
                                                     <asp:BoundField DataField="MotherName" HeaderText="Mother's Name"  />
                                                <asp:BoundField DataField="SuspendReasonEntry" HeaderText="Action Taken" />
                                                <asp:BoundField DataField="EffectiveDate" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Effective Date" />





                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        
                          <div role="tabpanel" class="tab-pane" id="Promotion">

                            <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                <ContentTemplate>

                                    <asp:UpdateProgress ID="UpdateProgress6" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaitingforfun">
                                        <asp:Image ID="imgWait7" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <fieldset class="for-panel">
                                                <legend>Filtering Criteria  </legend>
                                                
                                                
                                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Report type</label>   <label style="color: #a52a2a">*</label>
                                           <asp:DropDownList runat="server" AutoPostBack="True" ID="PromotionReportType"  OnSelectedIndexChanged="PromotionReportType_OnSelectedIndexChanged"  class="form-control form-control-sm" >
                                               <asp:ListItem Value="0">Select One</asp:ListItem>
                                               <asp:ListItem Value="1">Promotion</asp:ListItem>
                                               <asp:ListItem Value="2">No Promotion</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                         </div>

                                              <div class="row" runat="server" id="divPromotion" Visible="False">
                                                  
                                                    <div class="col-md-12">

                                  <fieldset class="for-panel">
                                                <legend>Promotion Filtering Criteria  </legend>
                                <div class="col-md-2" runat="server">
                                    <div class="form-group">
                                        <label>Financial Year </label>

                                        <asp:DropDownList ID="PromotionFinancialYearDropDownList" class="form-control form-control-sm" runat="server" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label runat="server" id="Label13">Effective From Date </label>

                                        <asp:TextBox ID="PromotionEffectiveDateTextBox" AutoCompleteType="Disabled" AutoPostBack="True" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                        <asp:CalendarExtender ID="CalendarExtender31" runat="server" PopupPosition="TopLeft"
                                            Format="dd-MMM-yyyy" PopupButtonID="PromotionEffectiveDateTextBox" CssClass="MyCalendar"
                                            TargetControlID="PromotionEffectiveDateTextBox" />
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label runat="server" id="Label14">Effective To Date </label>

                                        <asp:TextBox ID="PromotionEffectToDate" AutoCompleteType="Disabled" AutoPostBack="True" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                        <asp:CalendarExtender ID="CalendarExtender32" runat="server" PopupPosition="TopLeft"
                                            Format="dd-MMM-yyyy" PopupButtonID="PromotionEffectToDate" CssClass="MyCalendar"
                                            TargetControlID="PromotionEffectToDate" />
                                    </div>
                                </div>

                                <div class="col-md-2" runat="server">
                                    <div class="form-group">
                                        <label runat="server" id="Label90">Promotion Type </label>

                                        <asp:DropDownList ID="PromotionTypeDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                      </fieldset>
                                                        </div>
                                                    

</div>
                                                
                                                 <div class="row" id="divNoPromotion" runat="server" Visible="False">

                                <div class="col-md-12">
                                      <fieldset class="for-panel">
                                                <legend>No Promotion Filtering Criteria  </legend>
                                      
                                        <div class="row">
                                                    <div class="col-md-2">
                                                        <div class="form-group">
                                                            <label>Effective Date </label>
                                                            <%--<label style="color: #a52a2a">* </label>--%>
                                                            <asp:DropDownList ID="NoPromoEffectiveDropDownList" AutoPostBack="True" OnSelectedIndexChanged="NoPromoEffectiveDropDownList_OnSelectedIndexChanged" runat="server" class="form-control form-control-sm">
                                                                <asp:ListItem Value="1"> No Filter </asp:ListItem>
                                                                <asp:ListItem Value="2"> = </asp:ListItem>
                                                                <asp:ListItem Value="3"> < </asp:ListItem>
                                                                <asp:ListItem Value="4"> > </asp:ListItem>
                                                                <asp:ListItem Value="5"> Between </asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-2" runat="server" id="noPromoSingle" visible="False">
                                                        <div class="form-group">
                                                            <label>Date: </label>
                                                            <asp:TextBox ID="NoPromotxtEffectiveSingle" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender41" runat="server"
                                                                Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                TargetControlID="NoPromotxtEffectiveSingle" />
                                                        </div>
                                                    </div>


                                                    <div runat="server" id="NoPromoRange" visible="False" class="col-md-4">

                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>From Date: </label>
                                                                    <asp:TextBox ID="NoPromotxtEffectiveFromDate" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender42" runat="server"
                                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                        TargetControlID="NoPromotxtEffectiveFromDate" />
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>To Date: </label>
                                                                    <asp:TextBox ID="NoPromotxtEffectiveToDate" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender43" runat="server"
                                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                        TargetControlID="NoPromotxtEffectiveToDate" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-1" style="padding-top: 24px;">
                                                        <asp:Button ID="Button52" runat="server" CssClass="btn btn-sm btn-outline-success" OnClick="Button5sssss_OnClick" Text="Clear" />
                                                    </div>

                                                </div>

                                                </fieldset>
                                </div>
                                                </div>
                                                

                                                </fieldset>
                                            
                                            
                                              <br />
                                            <br />

                                            <div class="row">
                                                <div class="col-md-3">
                                                </div>
                                                <div class="col-md-1">
                                                </div>
                                                <div class="col-md-4">

                                                    <div class="form-group" style="margin-top: 17px;">


                                                        <asp:LinkButton runat="server" ID="lblPromotionSearch" OnClick="lblPromotionSearch_OnClick" ToolTip="Click To Search" CssClass="btn btn-info   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search </asp:LinkButton>

                                                        <asp:LinkButton runat="server" ID="lblPromotionReset" OnClick="lblPromotionReset_OnClick" CssClass="btn btn-warning   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                              
                              
                              
                                   <div class="form-row" style="padding-right: 10px">

                                <div class="col-md-6" style="padding-left: 2px">
                                    <label style="font-size: 18px;">Promotion List</label>
                                </div>
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-2">
                                    <%--  <asp:LinkButton ID="btnExportToExcel" runat="server" CssClass="btnexcel  pull-right" Style="padding-right: 10px;" OnClick="btnExportToExcel_Click"><span aria-hidden="true" style="font-size: 14px; color: #4CAF50;" class="fa fa-file-excel-o"></span> Download to xls</asp:LinkButton>--%>

                                    <input type="button" id="btnExportPromotion" title="Export to Excel" class="pull-right btnexcelcc " value="" />
                                </div>


                            </div>

                            <hr />
                              
                                  <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                        <ContentTemplate>
                            <asp:UpdateProgress ID="UpdateProgress7" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaitingforfun">
                                        <asp:Image ID="imgWait8" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            
                            
                              <div style="overflow: scroll; height: 500px; width: 100%">
                                            <asp:GridView ID="NoPromoGridView" runat="server" AutoGenerateColumns="False"
                                                CssClass="table table-bordered  text-center thead-light"   PageIndex="0">

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
                                                    <asp:BoundField DataField="NationalIdNo" HeaderText="National Id No." />
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


                                                    <asp:BoundField DataField="ServiceLength" HeaderText="SMC Service Length" />

                                                    <asp:BoundField DataField="DateofRetirement" HeaderText="Date of Retirement" DataFormatString="{0:dd-MMM-yyyy}" />

                                                    <asp:BoundField DataField="ProbitionEndDate" HeaderText="Probation End Date" DataFormatString="{0:dd-MMM-yyyy}" />


                                                    <asp:BoundField DataField="ContractualEndDate" HeaderText="Contractual End Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                  <asp:BoundField DataField="Supervisor" HeaderText="Supervisor" />
                                                    <asp:BoundField DataField="GrossAmount" HeaderText="Salary Amount" />
                                                     <asp:BoundField DataField="ContractPeriod" HeaderText="Contract Period"  />
                                                     <asp:BoundField DataField="FatherName" HeaderText="Father's Name"  />
                                                     <asp:BoundField DataField="MotherName" HeaderText="Mother's Name"  />
                                                  




                                                </Columns>
                                            </asp:GridView>
                                       
                                <asp:GridView ID="PromotionloadGridView" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered text-center thead-dark" 
                                   >
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
                                                            <asp:BoundField DataField="NationalIdNo" HeaderText="National Id No." />
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


                                                            <asp:BoundField DataField="ServiceLength" HeaderText="SMC Service Length" />

                                                            <asp:BoundField DataField="DateofRetirement" HeaderText="Date of Retirement" DataFormatString="{0:dd-MMM-yyyy}" />

                                                            <asp:BoundField DataField="ProbitionEndDate" HeaderText="Probation End Date" DataFormatString="{0:dd-MMM-yyyy}" />


                                                            <asp:BoundField DataField="ContractualEndDate" HeaderText="Contractual End Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                       <asp:BoundField DataField="Supervisor" HeaderText="Supervisor" />
                                                    <asp:BoundField DataField="GrossAmount" HeaderText="Salary Amount" />
                                                     <asp:BoundField DataField="ContractPeriod" HeaderText="Contract Period"  />
                                                  <asp:BoundField DataField="FatherName" HeaderText="Father's Name"  />
                                                     <asp:BoundField DataField="MotherName" HeaderText="Mother's Name"  />
                                        <asp:BoundField DataField="PromotionTypeName" HeaderText="Type of Action" />

                                        <asp:BoundField DataField="Effectivedate" Visible="False" HeaderText="Effective Date" DataFormatString="{0:dd-MMM-yyyy}" />



                                     <%--   <asp:BoundField DataField="Designation" HeaderText="Position" />--%>
                                        <asp:BoundField DataField="GradeName" HeaderText="Grade" />
                                        <asp:BoundField DataField="SalaryStepName" HeaderText="Step" />
                                        <asp:BoundField DataField="GrossAmount" HeaderText="Gross Monthly Salary" />
                                        <asp:BoundField DataField="LengthServicewithSMC" HeaderText="Total Years with SMC" />
                                        
                                        
                                        
                                        
                                        <asp:BoundField DataField="LastPromotion" HeaderText="Last Promotion/Upgradation/Reappointment Date" />
                                        <asp:BoundField DataField="TotalLengthfromlastpromotion" HeaderText="Total Length from last promotion/upgradation/reappointment" />
                                        <asp:BoundField DataField="diciplinaryEffectivedate" HeaderText="Disciplinary Last Issue Date" DataFormatString="{0:dd-MMM-yyyy}" />





                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                              </div>
                        
                         <div role="tabpanel" class="tab-pane" id="Separation">

                            <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                <ContentTemplate>

                                    <asp:UpdateProgress ID="UpdateProgress8" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaitingforfun">
                                        <asp:Image ID="imgWait9" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <fieldset class="for-panel">
                                                <legend>Filtering Criteria  </legend>
                                                
                                                <div class="row">
                                                     <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Separation From Date </label>

                                        <asp:TextBox ID="SeparationEffectiveDateTextBox" AutoCompleteType="Disabled" AutoPostBack="True" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                        <asp:CalendarExtender ID="CalendarExtender33" runat="server" PopupPosition="TopLeft"
                                            Format="dd-MMM-yyyy" PopupButtonID="SeparationEffectiveDateTextBox" CssClass="MyCalendar"
                                            TargetControlID="SeparationEffectiveDateTextBox" />
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Separation To Date </label>

                                        <asp:TextBox ID="SeparationEffectToDate" AutoCompleteType="Disabled" AutoPostBack="True" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                        <asp:CalendarExtender ID="CalendarExtender34" runat="server" PopupPosition="TopLeft"
                                            Format="dd-MMM-yyyy" PopupButtonID="SeparationEffectToDate" CssClass="MyCalendar"
                                            TargetControlID="SeparationEffectToDate" />
                                    </div>
                                </div>

                                <div class="col-md-5">
                                    <div class="Label_Title">Separation Type List</div>
                                    <br />
                                    <div class="form-group">
                                        <label>
                                         
                                        </label>
                                        <div style="max-height: 100px; overflow: scroll">
                                               <asp:CheckBox ID="SeparationmanCheckBox" Text="Select All / Unselect All" runat="server" CssClass="SelectchkChoice" AutoPostBack="True" OnCheckedChanged="SeparationmanCheckBox_OnCheckedChanged" />
                                            <br />
                                            <asp:CheckBoxList ID="SeparationmanagementCheckBoxList" CssClass="chkChoice" RepeatDirection="Vertical" RepeatColumns="2" runat="server"></asp:CheckBoxList>
                                        </div>


                                    </div>
                                </div>
                                                </div>
                                                </fieldset>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                             
                             <br/>
                             <br/>
                             
                              <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                <ContentTemplate>
                                    <asp:UpdateProgress ID="UpdateProgress9" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaitingforfun">
                                        <asp:Image ID="imgWait10" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                               <div class="row">
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-1">
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group" style="margin-top: 17px;">

                                        <asp:LinkButton runat="server" ID="SeparationmanSearchButton" OnClick="eparationmanSearchButton_OnClick" ToolTip="Click To Search"   CssClass="btn btn-info   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search </asp:LinkButton>
                                        
                                          <asp:LinkButton runat="server" ID="SeparationmanlbReset" OnClick="eparationmanlbReset_OnClick" CssClass="btn btn-warning   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                                    </ContentTemplate>
                                  </asp:UpdatePanel>
                             
                             
                            <div class="form-row" style="padding-right: 10px">

                                <div class="col-md-6" style="padding-left: 20px">
                                    <label style="font-size: 18px;">Separation List</label>
                                </div>
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-2">
                                    <%--  <asp:LinkButton ID="btnExportToExcel" runat="server" CssClass="btnexcel  pull-right" Style="padding-right: 10px;" OnClick="btnExportToExcel_Click"><span aria-hidden="true" style="font-size: 14px; color: #4CAF50;" class="fa fa-file-excel-o"></span> Download to xls</asp:LinkButton>--%>

                                    <input type="button" id="btnExportSeparation" title="Export to Excel" class="pull-right btnexcelcc " value="" />
                                </div>


                            </div>

                             <hr/>
                             
                             <asp:UpdatePanel runat="server" ID="sdasda">
                                 <ContentTemplate>
                                     <asp:UpdateProgress ID="UpdateProgress10" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaitingforfun">
                                        <asp:Image ID="imgWait11" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                                       <div id="gridContainedr1" style="overflow: scroll; height: 500px;width: 100%">
                            <asp:GridView ID="SeparationloadGridView" runat="server" AutoGenerateColumns="False"
                                CssClass="table table-bordered text-center thead-dark" 
                                >
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
                                       <asp:BoundField DataField="NationalIdNo" HeaderText="National Id No." />
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
                                                               <asp:BoundField DataField="ServiceLength" HeaderText="SMC Service Length" />

                                                            <asp:BoundField DataField="DateofRetirement" HeaderText="Date of Retirement" DataFormatString="{0:dd-MMM-yyyy}" />

                                                            <asp:BoundField DataField="ProbitionEndDate" HeaderText="Probation End Date" DataFormatString="{0:dd-MMM-yyyy}" />


                                                            <asp:BoundField DataField="ContractualEndDate" HeaderText="Contractual End Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                      <asp:BoundField DataField="Supervisor" HeaderText="Supervisor" />
                                                    <asp:BoundField DataField="GrossAmount" HeaderText="Salary Amount" />
                                                     <asp:BoundField DataField="ContractPeriod" HeaderText="Contract Period"  />
                                                <asp:BoundField DataField="FatherName" HeaderText="Father's Name"  />
                                                     <asp:BoundField DataField="MotherName" HeaderText="Mother's Name"  />


                                    <asp:BoundField DataField="JobLeftType" HeaderText="Type of Separation" />
                                    <%--<asp:BoundField DataField="SubmissionDate" HeaderText="Submission Date" DataFormatString="{0:dd-MMM-yyyy}" />--%>

                                    <asp:BoundField DataField="SubmissionDate" HeaderText="Submission Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                    <asp:BoundField DataField="JobLeftDate" HeaderText="Date of Seperation" DataFormatString="{0:dd-MMM-yyyy}" />
                                    <asp:BoundField DataField="LengthServicewithSMC" Visible="False" HeaderText="Length of Service with SMC" />


                                </Columns>
                            </asp:GridView>



                        
                        </div>
                                 </ContentTemplate>
                             </asp:UpdatePanel>
                             </div>
                        
                              <div role="tabpanel" class="tab-pane" id="Increment">

                            <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                <ContentTemplate>

                                    <asp:UpdateProgress ID="UpdateProgress11" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaitingforfun">
                                        <asp:Image ID="imgWait12" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <fieldset class="for-panel">
                                                <legend>Filtering Criteria  </legend>
                                              <div class="row">
                                                      <div class="col-md-2" runat="server">
                                    <div class="form-group">
                                        <label>Financial Year </label>

                                        <asp:DropDownList ID="IncrementFinancialYearDropDownList" class="form-control form-control-sm" runat="server" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Increment Type</label>
                                        
                                        <asp:DropDownList runat="server"   ID="IncrementddlIncrementType" CssClass="form-control form-control-sm" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label runat="server" id="Label15">Effective From Date </label>

                                        <asp:TextBox ID="IncrementEffectiveDateTextBox" AutoCompleteType="Disabled" AutoPostBack="True" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                        <asp:CalendarExtender ID="CalendarExtender35" runat="server" PopupPosition="TopLeft"
                                            Format="dd-MMM-yyyy" PopupButtonID="IncrementEffectiveDateTextBox" CssClass="MyCalendar"
                                            TargetControlID="IncrementEffectiveDateTextBox" />
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label runat="server" id="Label16">Effective To Date </label>

                                        <asp:TextBox ID="IncrementEffectToDate" AutoCompleteType="Disabled" AutoPostBack="True" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                        <asp:CalendarExtender ID="CalendarExtender36" runat="server" PopupPosition="TopLeft"
                                            Format="dd-MMM-yyyy" PopupButtonID="IncrementEffectToDate" CssClass="MyCalendar"
                                            TargetControlID="IncrementEffectToDate" />
                                    </div>
                                </div>

                                <div class="col-md-2" runat="server" visible="False">
                                    <div class="form-group">
                                        <label runat="server" id="Label17">Promotion Type </label>

                                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                                  </div>
                                                </fieldset>
                                            
                                            <br/>
                                            <br/>
                                            
                                             <div class="row">
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-1">
                                </div>
                                <div class="col-md-4">

                                    <div class="form-group" style="margin-top: 17px;">


                                        <asp:LinkButton runat="server" ID="IncrementSearchButton" OnClick="IncrementSearchButton_OnClick" ToolTip="Click To Search" CssClass="btn btn-info   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search </asp:LinkButton>

                                        <asp:LinkButton runat="server" ID="IncrementlbReset" OnClick="IncrementlbReset_OnClick" CssClass="btn btn-warning   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                  
                                  
                                      <div class="form-row" style="padding-right: 10px">

                                <div class="col-md-6" style="padding-left: 2px">
                                    <label style="font-size: 18px;">Increment List</label>
                                </div>
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-2">
                                    <%--  <asp:LinkButton ID="btnExportToExcel" runat="server" CssClass="btnexcel  pull-right" Style="padding-right: 10px;" OnClick="btnExportToExcel_Click"><span aria-hidden="true" style="font-size: 14px; color: #4CAF50;" class="fa fa-file-excel-o"></span> Download to xls</asp:LinkButton>--%>

                                    <input type="button" id="btnExportIncrement" title="Export to Excel" class="pull-right btnexcelcc " value="" />
                                </div>


                            </div>
                                  
                                  <hr/>
                                  
                                   <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                        <ContentTemplate>
                            <div id="gridContainser1" style="overflow: scroll; height: 500px; width: 100%">
                                <asp:GridView ID="IncrementloadGridView" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered text-center thead-dark"
                                     >
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
                                        <asp:BoundField DataField="NationalIdNo" HeaderText="National Id No." />
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


                                        <asp:BoundField DataField="ServiceLength" HeaderText="SMC Service Length" />

                                        <asp:BoundField DataField="DateofRetirement" HeaderText="Date of Retirement" DataFormatString="{0:dd-MMM-yyyy}" />

                                        <asp:BoundField DataField="ProbitionEndDate" HeaderText="Probation End Date" DataFormatString="{0:dd-MMM-yyyy}" />


                                        <asp:BoundField DataField="ContractualEndDate" HeaderText="Contractual End Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                     <asp:BoundField DataField="Supervisor" HeaderText="Supervisor" />
                                                    <asp:BoundField DataField="GrossAmount" HeaderText="Salary Amount" />
                                                     <asp:BoundField DataField="ContractPeriod" HeaderText="Contract Period"  />
                                                     <asp:BoundField DataField="FatherName" HeaderText="Father's Name"  />
                                                     <asp:BoundField DataField="MotherName" HeaderText="Mother's Name"  />
                                        <asp:BoundField DataField="PreviousStep" HeaderText="Previous Step" />
                                        <asp:BoundField DataField="IncrementalStep" HeaderText="Incremental Step" />
                                        <asp:BoundField DataField="EffectiveDate" HeaderText="Efeective Date" DataFormatString="{0:dd-MMM-yyyy}" />

                                        <asp:BoundField DataField="IncrementTType" HeaderText="Increment Type" />




                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                                  </div>
                        
                          <div role="tabpanel" class="tab-pane" id="EmpBirthDay">

                            <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                <ContentTemplate>

                                    <asp:UpdateProgress ID="UpdateProgress12" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaitingforfun">
                                        <asp:Image ID="imgWait120" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <fieldset class="for-panel">
                                                <legend>Filtering Criteria  </legend>
                                              <div class="row">
                                                  
                                                         <div class="col-2">
                                        <div class="form-group">
                                            <label>Month</label>
                                            <label style="color: #a52a2a">*</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="MonthDropDownList"   class="form-control form-control-sm" >
                                                <asp:ListItem Value="0">Please Select a Month</asp:ListItem>
                                                <asp:ListItem Value="January"></asp:ListItem>
                                                <asp:ListItem Value="February"></asp:ListItem>
                                                <asp:ListItem Value="March"></asp:ListItem>
                                                <asp:ListItem Value="April"></asp:ListItem>
                                                <asp:ListItem Value="May"></asp:ListItem>
                                                <asp:ListItem Value="June"></asp:ListItem>
                                                <asp:ListItem Value="July"></asp:ListItem>
                                                <asp:ListItem Value="August"></asp:ListItem>
                                                <asp:ListItem>September</asp:ListItem>
                                                <asp:ListItem Value="October"></asp:ListItem>
                                                <asp:ListItem Value="November"></asp:ListItem>
                                                <asp:ListItem Value="December"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                                  </div>
                                                </fieldset>
                                            
                                                     <br/>
                                            <br/>
                                            
                                             <div class="row">
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-1">
                                </div>
                                <div class="col-md-4">

                                    <div class="form-group" style="margin-top: 17px;">


                                        <asp:LinkButton runat="server" ID="EmpBirthDaySearchButton" OnClick="EmpBirthDaySearchButton_OnClick" ToolTip="Click To Search" CssClass="btn btn-info   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search </asp:LinkButton>

                                        <asp:LinkButton runat="server" ID="EmpBirthDaybtnReset" OnClick="EmpBirthDaybtnReset_OnClick" CssClass="btn btn-warning   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                              
                              
                              
                                   <div class="form-row" style="padding-right: 10px">

                                <div class="col-md-6" style="padding-left: 20px">
                                    <label style="font-size: 18px;">Month Wise Birth Day Employee Information List</label>
                                </div>
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-2">
                                    <%--  <asp:LinkButton ID="btnExportToExcel" runat="server" CssClass="btnexcel  pull-right" Style="padding-right: 10px;" OnClick="btnExportToExcel_Click"><span aria-hidden="true" style="font-size: 14px; color: #4CAF50;" class="fa fa-file-excel-o"></span> Download to xls</asp:LinkButton>--%>

                                    <input type="button" id="btnExportEmpBirthDay" title="Export to Excel" class="pull-right btnexcelcc " value="" />
                                </div>


                            </div>
                                  
                                  <hr/>
                              
                                    <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                        <ContentTemplate>
                             <asp:UpdateProgress ID="UpdateProgress13" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaitingforfun">
                                        <asp:Image ID="imgWait12s0" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <div id="gridContainsaser1" style="overflow: scroll; height: 500px; width: 100%">
                                
                                
                                    <asp:GridView ID="EmpBirthDayloadGridView" runat="server" AutoGenerateColumns="False"
                                        CssClass="table table-bordered text-center thead-dark" 
                                       >
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL">
                                                <ItemTemplate>
                                                    <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                       
                                            <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee ID" />
                                            <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                            <asp:BoundField DataField="DivisionName" HeaderText="Division" />

                                            <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                            <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                              <asp:BoundField DataField="Office" HeaderText="Office" />
                                        <asp:BoundField DataField="Place" HeaderText="Place" />
                                            <asp:BoundField DataField="DateOfBirth" HeaderText="Date of Birth"  DataFormatString="{0:dd-MMM-yyyy}"/>
                                           
                                              <asp:BoundField DataField="Supervisor" HeaderText="Supervisor" />

                                              <asp:BoundField DataField="PersonalMobile" HeaderText="Personal Mobile" />
                                              <asp:BoundField DataField="PersonalEmail" HeaderText="Personal Email" />
                                              <asp:BoundField DataField="OfficialEmail" HeaderText="Official Email" />

                                           
                                        </Columns>
                                        
                                    </asp:GridView>
                              </div>
                                                  
                                      
                        </ContentTemplate>
                    </asp:UpdatePanel>                 
                    </div>
                        
                        
                        
                        <div role="tabpanel" class="tab-pane" id="EmpMedicalCheckUp">

                            <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                <ContentTemplate>

                                    <asp:UpdateProgress ID="UpdateProgress14" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaitingforfun">
                                        <asp:Image ID="imsdgWdsait120" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                                    <div class="row">
                                        
                                         <div class="col-md-12">
                                            <fieldset class="for-panel">
                                                <legend>Filtering Criteria  </legend>
                                              <div class="row">
                                         
                                    
                                     <div class="col-2">
                                    <div class="form-group">
                                        <label>Check-up From Date</label>   <label style="color: #a52a2a">*</label>
                                        <asp:TextBox runat="server" ID="CheckuptxtDate" class="form-control form-control-sm" />
                                          <ajaxToolkit:CalendarExtender ID="CalendarExtender37" runat="server"
                                                    Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                    TargetControlID="CheckuptxtDate" />
                                    </div>
                                         </div>
                                 
                                 
                                  <div class="col-2">
                                    <div class="form-group">
                                        <label>Check-up To Date</label>   <label style="color: #a52a2a">*</label>
                                        <asp:TextBox runat="server" ID="CheckuptxtToDate" class="form-control form-control-sm" />
                                          <ajaxToolkit:CalendarExtender ID="CalendarExtender38" runat="server"
                                                    Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                    TargetControlID="CheckuptxtToDate" />
                                    </div>
                                         </div>
                                 
                                 
                                   <div class="col-2">
                                    <div class="form-group">
                                        <label>Status</label>   <label style="color: #a52a2a">*</label>
                                           <asp:DropDownList runat="server" AutoPostBack="True" ID="CheckupddlStatus"   class="form-control form-control-sm" >
                                               <asp:ListItem Value="0">Select One</asp:ListItem>
                                               <asp:ListItem Value="1">Completed</asp:ListItem>
                                               <asp:ListItem Value="2">Not Completed</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                         </div>
                                                  </div>
                                                </fieldset>
                                             </div>
                                             </div>
                                    
                                           <br/>
                                            <br/>
                                            
                                             <div class="row">
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-1">
                                </div>
                                <div class="col-md-4">

                                    <div class="form-group" style="margin-top: 17px;">


                                        <asp:LinkButton runat="server" ID="CheckupSearchButton" OnClick="CheckupSearchButton_OnClick" ToolTip="Click To Search" CssClass="btn btn-info   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search </asp:LinkButton>

                                        <asp:LinkButton runat="server" ID="CheckupResetButton" OnClick="CheckupResetButton_OnClick" CssClass="btn btn-warning   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            
                            
                            
                                 <div class="form-row" style="padding-right: 10px">

                                <div class="col-md-6" style="padding-left: 20px">
                                    <label style="font-size: 18px;">Medical Check-up List</label>
                                </div>
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-2">
                                    <%--  <asp:LinkButton ID="btnExportToExcel" runat="server" CssClass="btnexcel  pull-right" Style="padding-right: 10px;" OnClick="btnExportToExcel_Click"><span aria-hidden="true" style="font-size: 14px; color: #4CAF50;" class="fa fa-file-excel-o"></span> Download to xls</asp:LinkButton>--%>

                                    <input type="button" id="btnExportCheckup" title="Export to Excel" class="pull-right btnexcelcc " value="" />
                                </div>


                            </div>
                                  
                                  <hr/>
                                  
                                   <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                        <ContentTemplate>
                             <asp:UpdateProgress ID="UpdateProgress15" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaitingforfun">
                                        <asp:Image ID="imgWdsait120" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <div id="gridaContainser1" style="overflow: scroll; height: 500px; width: 100%">
                                <asp:GridView ID="CheckupGridView1" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered text-center thead-dark"
                                     >
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
                                        <asp:BoundField DataField="NationalIdNo" HeaderText="National Id No." />
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


                                        <asp:BoundField DataField="ServiceLength" HeaderText="SMC Service Length" />

                                        <asp:BoundField DataField="DateofRetirement" HeaderText="Date of Retirement" DataFormatString="{0:dd-MMM-yyyy}" />

                                        <asp:BoundField DataField="ProbitionEndDate" HeaderText="Probation End Date" DataFormatString="{0:dd-MMM-yyyy}" />


                                        <asp:BoundField DataField="ContractualEndDate" HeaderText="Contractual End Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        
                                     <asp:BoundField DataField="Supervisor" HeaderText="Supervisor" />
                                                    <asp:BoundField DataField="GrossAmount" HeaderText="Salary Amount" />
                                                     <asp:BoundField DataField="ContractPeriod" HeaderText="Contract Period"  />
                                                     <asp:BoundField DataField="FatherName" HeaderText="Father's Name"  />
                                                     <asp:BoundField DataField="MotherName" HeaderText="Mother's Name"  />
                                         <asp:BoundField DataField="MedicalCheckUpDate" HeaderText="Check-up Date"  DataFormatString="{0:dd-MMM-yyyy}"/>
                                         <asp:BoundField DataField="MedicalCheckUpComments" HeaderText="Comment" />
                                         <asp:BoundField DataField="MedicalCheckUpRemarks" HeaderText="Remarks" />

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                            </div>
                        
                        
                           <div role="tabpanel" class="tab-pane" id="Experience">
                               
                               
                                  <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                                <ContentTemplate>

                                    <asp:UpdateProgress ID="UpdateProgress18" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaitingforfun">
                                        <asp:Image ID="imsdgWdsaitasa120" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                               
                               
                                 <br/>
                                            <br/>
                                            
                                             <div class="row">
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-1">
                                </div>
                                <div class="col-md-4">

                                    <div class="form-group" style="margin-top: 17px;">


                                        <asp:LinkButton runat="server" ID="ExperienceSearchButton" OnClick="ExperienceSearchButton_OnClick" ToolTip="Click To Search" CssClass="btn btn-info   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search </asp:LinkButton>

                                        <asp:LinkButton runat="server" ID="ExperienceResetButton" OnClick="ExperienceResetButton_OnClick" CssClass="btn btn-warning   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                               
                                <div class="form-row" style="padding-right: 10px">

                                <div class="col-md-6" style="padding-left: 20px">
                                    <label style="font-size: 18px;">Experience List</label>
                                </div>
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-2">
                                    <%--  <asp:LinkButton ID="btnExportToExcel" runat="server" CssClass="btnexcel  pull-right" Style="padding-right: 10px;" OnClick="btnExportToExcel_Click"><span aria-hidden="true" style="font-size: 14px; color: #4CAF50;" class="fa fa-file-excel-o"></span> Download to xls</asp:LinkButton>--%>

                                    <input type="button" id="btnExportExperience" title="Export to Excel" class="pull-right btnexcelcc " value="" />
                                </div>


                            </div>
                                  
                                  <hr/>

                            <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                                
                                <ContentTemplate>
 <asp:UpdateProgress ID="UpdateProgress16" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaitingforfun">
                                        <asp:Image ID="imgWsszxzadsdsit1" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
        </asp:UpdateProgress>
        <div id="gridaCoasantazxinser1" style="overflow: scroll; height: 500px; width: 100%">
                                <asp:GridView ID="gv_Experience" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered text-center thead-dark"  >
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
                                        <asp:BoundField DataField="NationalIdNo" HeaderText="National Id No." />
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


                                        <asp:BoundField DataField="ServiceLength" HeaderText="SMC Service Length" />

                                        <asp:BoundField DataField="DateofRetirement" HeaderText="Date of Retirement" DataFormatString="{0:dd-MMM-yyyy}" />

                                        <asp:BoundField DataField="ProbitionEndDate" HeaderText="Probation End Date" DataFormatString="{0:dd-MMM-yyyy}" />


                                        <asp:BoundField DataField="ContractualEndDate" HeaderText="Contractual End Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="Supervisor" HeaderText="Supervisor" />
                                                    <asp:BoundField DataField="GrossAmount" HeaderText="Salary Amount" />
                                                     <asp:BoundField DataField="ContractPeriod" HeaderText="Contract Period"  />
                                                    <asp:BoundField DataField="FatherName" HeaderText="Father's Name"  />
                                                     <asp:BoundField DataField="MotherName" HeaderText="Mother's Name"  />
                                        
                                        
                                        <asp:BoundField DataField="ExpCompany" HeaderText="Company/Institute" />
                                        <asp:BoundField DataField="ExpContactPerson" HeaderText="Contact Person" />
                                        <asp:BoundField DataField="ExpAddress" HeaderText="Address" />
                                        <asp:BoundField DataField="ExpNatureofBusiness" HeaderText="Nature of Business" />
                                        <asp:BoundField DataField="ExpJobType" HeaderText="Job Type" />
                                        <asp:BoundField DataField="ExpLeavingSalary" HeaderText="Leaving Salary" />
                                        <asp:BoundField DataField="ExpFromDate" HeaderText="From Date" />
                                        <asp:BoundField DataField="ExpToDate" HeaderText="To Date" />
                                        <asp:BoundField DataField="TotalExperience" HeaderText="Total Experience" />

                                        <asp:BoundField DataField="ExpLastJob" HeaderText="Is Last Job" />
                                        <asp:BoundField DataField="ExpDesignation" HeaderText="Designation" />
                                        <asp:BoundField DataField="ExpJobDescription" HeaderText="Job Description" />
                                        <asp:BoundField DataField="ExpTelNo" HeaderText="Tel. No." />
                                        <asp:BoundField DataField="ExpRemarks" HeaderText="Remarks" />
                                        
                                         

                                    </Columns>
                                </asp:GridView>
                            </div>

                         
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                               </div>
                        
                        
                          <div role="tabpanel" class="tab-pane" id="Training">
                              
                              
                              
                            <asp:UpdatePanel ID="UpdatePanel40" runat="server">
                                
                                <ContentTemplate>
 <asp:UpdateProgress ID="UpdateProgress31" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaitingforfun">
                                        <asp:Image ID="sadsaimgsaasWssadsdsit1" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
         </asp:UpdateProgress>
                              
                                    
                                    <div class="row">
                                        
                                         <div class="col-md-12">
                                            <fieldset class="for-panel">
                                                <legend>Filtering Criteria  </legend>    
                              <div class="row">
                                              <div class="col-md-2">
                                                        <div class="form-group">
                                                            <label>Training Date (Start) </label>
                                                            <label style="color: #a52a2a">* </label>
                                                            <asp:DropDownList ID="TrainDropDownList" runat="server" AutoPostBack="True" class="form-control form-control-sm" OnSelectedIndexChanged="TrainDropDownList_OnSelectedIndexChanged">
                                                                <asp:ListItem Value="1"> No Filter </asp:ListItem>
                                                                <asp:ListItem Value="2"> = </asp:ListItem>
                                                                <asp:ListItem Value="3"> < </asp:ListItem>
                                                                <asp:ListItem Value="4"> > </asp:ListItem>
                                                                <asp:ListItem Value="5"> Between </asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-2" runat="server" id="Trainsingle" visible="False">
                                                        <div class="form-group">
                                                            <label>Date: </label>
                                                            <asp:TextBox ID="TrainTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender44" runat="server"
                                                                Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                TargetControlID="TrainTextBox" />
                                                        </div>
                                                    </div>


                                                    <div runat="server" id="Trainrange" visible="False" class="col-md-4">

                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>From Date: </label>
                                                                    <asp:TextBox ID="TrainfromTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender45" runat="server"
                                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                        TargetControlID="TrainfromTextBox" />
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>To Date: </label>
                                                                    <asp:TextBox ID="TraintoTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender46" runat="server"
                                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                        TargetControlID="TraintoTextBox" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-1" style="padding-top: 24px;">
                                                        <asp:Button ID="Button53" runat="server" CssClass="btn btn-sm btn-outline-success" OnClick="asdsaButton48_OnClick" Text="Clear" />
                                                    </div>
                                  
                              </div>
                                                
                                                 
                                     <div class="row">
                                              <div class="col-md-2">
                                                        <div class="form-group">
                                                            <label>Training Date (End) </label>
                                                            <label style="color: #a52a2a">* </label>
                                                            <asp:DropDownList ID="TrainEndDropDownList" runat="server" AutoPostBack="True" class="form-control form-control-sm" OnSelectedIndexChanged="TrainEndDropDownList_OnSelectedIndexChanged">
                                                                <asp:ListItem Value="1"> No Filter </asp:ListItem>
                                                                <asp:ListItem Value="2"> = </asp:ListItem>
                                                                <asp:ListItem Value="3"> < </asp:ListItem>
                                                                <asp:ListItem Value="4"> > </asp:ListItem>
                                                                <asp:ListItem Value="5"> Between </asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-2" runat="server" id="TrainEndSingleDiv" visible="False">
                                                        <div class="form-group">
                                                            <label>Date: </label>
                                                            <asp:TextBox ID="txtTrainEndDate" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender47" runat="server"
                                                                Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                TargetControlID="txtTrainEndDate" />
                                                        </div>
                                                    </div>


                                                    <div runat="server" id="TrainEndRanDiv" visible="False" class="col-md-4">

                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>From Date: </label>
                                                                    <asp:TextBox ID="txtTrainEndStartDate" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender48" runat="server"
                                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                        TargetControlID="txtTrainEndStartDate" />
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>To Date: </label>
                                                                    <asp:TextBox ID="txtTrainEndEndDate" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender49" runat="server"
                                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                        TargetControlID="txtTrainEndEndDate" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-1" style="padding-top: 24px;">
                                                        <asp:Button ID="Button54" runat="server" CssClass="btn btn-sm btn-outline-success" OnClick="asdsasaaButton48_OnClick" Text="Clear" />
                                                    </div>
                                  
                              </div>
                                    
                                    
                                           <div class="row">
                                                    <div class="col-md-2">
                                                        <div class="form-group">
                                                            <label>Training Days </label>
                                                            <%--<label style="color: #a52a2a">* </label>--%>
                                                            <asp:DropDownList ID="TrDaysDropDownList" AutoPostBack="True" OnSelectedIndexChanged="TrDaysDropDownList_OnSelectedIndexChanged" runat="server" class="form-control form-control-sm">
                                                                <asp:ListItem Value="1"> No Filter </asp:ListItem>
                                                                <asp:ListItem Value="2"> = </asp:ListItem>
                                                                <asp:ListItem Value="3"> < </asp:ListItem>
                                                                <asp:ListItem Value="4"> > </asp:ListItem>
                                                                <asp:ListItem Value="5"> Between </asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-2" runat="server" id="TrDaySingleDiv" visible="False">
                                                        <div class="form-group">
                                                            <label>Training Days: </label>
                                                            <asp:TextBox ID="txtTrDaysDateSingle" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                                                    TargetControlID="txtTrDaysDateSingle" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                        </div>
                                                    </div>


                                                    <div runat="server" id="TrDayRangeDiv" visible="False" class="col-md-4">

                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>Training Days From: </label>
                                                                    <asp:TextBox ID="txtTrDaysDateStart" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                                     <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                                                    TargetControlID="txtTrDaysDateStart" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>Training Days To: </label>
                                                                    <asp:TextBox ID="txtTrDaysDateEnd" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                                      <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                                                    TargetControlID="txtTrDaysDateEnd" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-1" style="padding-top: 24px;">
                                                        <asp:Button ID="Button55" runat="server" CssClass="btn btn-sm btn-outline-success" OnClick="asadassasdsadButton50_OnClick" Text="Clear" />
                                                    </div>

                                                </div>
                                    
                                    <div class="row">
                                                    <div class="col-md-2">
                                                        <div class="form-group">
                                                            <label>Training Fees </label>
                                                            <%--<label style="color: #a52a2a">* </label>--%>
                                                            <asp:DropDownList ID="trFeesDropDownList" AutoPostBack="True" OnSelectedIndexChanged="trFeesDropDownList_OnSelectedIndexChanged" runat="server" class="form-control form-control-sm">
                                                                <asp:ListItem Value="1"> No Filter </asp:ListItem>
                                                                <asp:ListItem Value="2"> = </asp:ListItem>
                                                                <asp:ListItem Value="3"> < </asp:ListItem>
                                                                <asp:ListItem Value="4"> > </asp:ListItem>
                                                                <asp:ListItem Value="5"> Between </asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-2" runat="server" id="trFeesSingleDiv" visible="False">
                                                        <div class="form-group">
                                                            <label>Training Fees: </label>
                                                            <asp:TextBox ID="txttrFeesSingleDate" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True"
                                                                    TargetControlID="txttrFeesSingleDate" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                        </div>
                                                    </div>


                                                    <div runat="server" id="trFeesReangeDiv" visible="False" class="col-md-4">

                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>Training Fees From: </label>
                                                                    <asp:TextBox ID="txttrFeesStartDate" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                                     <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True"
                                                                    TargetControlID="txttrFeesStartDate" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>Training Fees To: </label>
                                                                    <asp:TextBox ID="txttrFeesEndDate" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                                      <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" Enabled="True"
                                                                    TargetControlID="txttrFeesEndDate" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-1" style="padding-top: 24px;">
                                                        <asp:Button ID="Button56" runat="server" CssClass="btn btn-sm btn-outline-success" OnClick="asaasdasdassdButton50_OnClick" Text="Clear" />
                                                    </div>

                                                </div>
                                    
                                    
                                    
                                         <div class="row" >
                                                    <div class="col-md-2">
                                                        <div class="form-group">
                                                            <label>Score </label>
                                                            <%--<label style="color: #a52a2a">* </label>--%>
                                                            <asp:DropDownList ID="ScoreDropDownList" AutoPostBack="True" OnSelectedIndexChanged="ScoreDropDownList_OnSelectedIndexChanged" runat="server" class="form-control form-control-sm">
                                                                <asp:ListItem Value="1"> No Filter </asp:ListItem>
                                                                <asp:ListItem Value="2"> = </asp:ListItem>
                                                                <asp:ListItem Value="3"> < </asp:ListItem>
                                                                <asp:ListItem Value="4"> > </asp:ListItem>
                                                                <asp:ListItem Value="5"> Between </asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-2" runat="server" id="ScoreSingleDiv" visible="False">
                                                        <div class="form-group">
                                                            <label>Training Score: </label>
                                                            <asp:TextBox ID="txtScore" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                             
                                                        </div>
                                                    </div>


                                                    <div runat="server" id="ScoreRangeDiv" visible="False" class="col-md-4">

                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>Training Score From: </label>
                                                                    <asp:TextBox ID="txtScoreStart" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                                     <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" Enabled="True"
                                                                    TargetControlID="txtScoreStart" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>Training Score To: </label>
                                                                    <asp:TextBox ID="txtScoreEnd" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                                      <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" Enabled="True"
                                                                    TargetControlID="txtScoreEnd" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-1" style="padding-top: 24px;">
                                                        <asp:Button ID="Button57" runat="server" CssClass="btn btn-sm btn-outline-success" OnClick="asaasdasdassdButton50_OnClick" Text="Clear" />
                                                    </div>

                                                </div>
                                    
                                    
                                    </fieldset>
                                             </div>
                                        </div>
                                    
                                   
                                                 <div class="row">
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-1">
                                </div>
                                <div class="col-md-4">

                                    <div class="form-group" style="margin-top: 17px;">


                                        <asp:LinkButton runat="server" ID="lbTrainingSearch" OnClick="lbTrainingSearch_OnClick" ToolTip="Click To Search" CssClass="btn btn-info   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search </asp:LinkButton>

                                        <asp:LinkButton runat="server" ID="lbTrainingReset" OnClick="lbTrainingReset_OnClick" CssClass="btn btn-warning   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                                    
                                         </ContentTemplate>
                                </asp:UpdatePanel>

                              <br/>
                                 <div class="form-row" style="padding-right: 10px">

                                <div class="col-md-6" style="padding-left: 2px">
                                    <label style="font-size: 18px;">Training List</label>
                                </div>
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-2">
                                    <%--  <asp:LinkButton ID="btnExportToExcel" runat="server" CssClass="btnexcel  pull-right" Style="padding-right: 10px;" OnClick="btnExportToExcel_Click"><span aria-hidden="true" style="font-size: 14px; color: #4CAF50;" class="fa fa-file-excel-o"></span> Download to xls</asp:LinkButton>--%>

                                    <input type="button" id="btnExportTraining" title="Export to Excel" class="pull-right btnexcelcc " value="" />
                                </div>


                            </div>
                                  
                                  <hr/>

                            <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                                
                                <ContentTemplate>
 <asp:UpdateProgress ID="UpdateProgress17" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaitingforfun">
                                        <asp:Image ID="imgsaasWssadsdsit1" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
         </asp:UpdateProgress>
     
      <div id="asgasridaCoasantazxasinser1" style="overflow: scroll; height: 500px; width: 100%">
                                        <asp:GridView Width="100%" ID="gv_Training" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">
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
                                        <asp:BoundField DataField="NationalIdNo" HeaderText="National Id No." />
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
                                       <asp:BoundField DataField="ServiceLength" HeaderText="SMC Service Length" />
                                         <asp:BoundField DataField="DateofRetirement" HeaderText="Date of Retirement" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="ProbitionEndDate" HeaderText="Probation End Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                          <asp:BoundField DataField="ContractualEndDate" HeaderText="Contractual End Date" DataFormatString="{0:dd-MMM-yyyy}" />

                                <asp:BoundField DataField="Supervisor" HeaderText="Supervisor" />
                                                    <asp:BoundField DataField="GrossAmount" HeaderText="Salary Amount" />
                                                     <asp:BoundField DataField="ContractPeriod" HeaderText="Contract Period"  />
                                                  <asp:BoundField DataField="FatherName" HeaderText="Father's Name"  />
                                                     <asp:BoundField DataField="MotherName" HeaderText="Mother's Name"  />
                                                
                                       <asp:BoundField DataField="TrainingName" HeaderText="Training Name" />
                                       <asp:BoundField DataField="TrainingTypeName" HeaderText="Training Type" />
                                       <asp:BoundField DataField="Supervisor" HeaderText="Supervisor" />
                                       <asp:BoundField DataField="TrainingDescription" HeaderText="Training Description" />
                                       <asp:BoundField DataField="TrainingInstitutionName" HeaderText="Training Institution" />
                                       <asp:BoundField DataField="TrainingPlace" HeaderText="Training Place" />
                                       <asp:BoundField DataField="TrainingCountryName" HeaderText="Training Country" />
                                       <asp:BoundField DataField="TrainingAchievment" HeaderText="Training Achievment" />
                                       <asp:BoundField DataField="TrFromDate" HeaderText="From Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                       <asp:BoundField DataField="TrToDate" HeaderText="To Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                       <asp:BoundField DataField="TrainingDays" HeaderText="Training Days" />
                                       <asp:BoundField DataField="GrandTotal" HeaderText="Training Fees" />
                                       <asp:BoundField DataField="TrRemarks" HeaderText="Remarks" />
                                       




                                            </Columns>
                                        </asp:GridView>
                                    </div>
                        
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                               </div>
                        
                        
                            <div role="tabpanel" class="tab-pane" id="Contractual">
                                
                                  

                            <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                                <ContentTemplate>

                                    <asp:UpdateProgress ID="UpdateProgress19" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaitingforfun">
                                        <asp:Image ID="imsdgWdasdsait120" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                                    <div class="row">
                                        
                                         <div class="col-md-12">
                                            <fieldset class="for-panel">
                                                <legend>Filtering Criteria  </legend>
                                              <div class="row">
                                                  
                                                      <div class="col-2" runat="server" >
                                <div class="form-group">
                                    <label class="control-label">Effective From Date</label>
                                    <asp:TextBox runat="server" ID="ContractualstartDate" class="form-control form-control-sm" AutoPostBack="True" ></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender39" runat="server"
                                                                  Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                  TargetControlID="ContractualstartDate" />
                                </div>
                            </div>
                            <div class="col-2" runat="server" >
                                <div class="form-group">
                                    <label class="control-label">Effective To Date</label>
                                    <asp:TextBox runat="server" ID="ContractualendDate" class="form-control form-control-sm" AutoPostBack="True" ></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender40" runat="server"
                                                                  Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                  TargetControlID="ContractualendDate" />
                                </div>
                            </div>
                                                  
                                                  
                                                    <div class="col-3" runat="server" >
                                <div class="form-group">
                                    <label class="control-label">Type</label>&nbsp;<label style="color: #a52a2a">*</label>
                                    <asp:DropDownList ID="ContractualddlType" CssClass="form-control form-control-sm" runat="server">
                                        <asp:ListItem Selected="True" Value="0">Select One</asp:ListItem>
                                        
                                        <asp:ListItem Value="1">Extension</asp:ListItem>
                                        <asp:ListItem Value="2">Renewal</asp:ListItem>
                                        <asp:ListItem Value="3">Permanent to Contractual </asp:ListItem>
                                        <asp:ListItem Value="4">Contractual to Permanent </asp:ListItem>
                                        <asp:ListItem Value="5">Project Change </asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                                                     </div>
                                                </fieldset>
                                             </div>
                                             </div>
                                    
                                           <br/>
                                            <br/>
                                    
                                      <div class="row">
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-1">
                                </div>
                                <div class="col-md-4">

                                    <div class="form-group" style="margin-top: 17px;">


                                        <asp:LinkButton runat="server" ID="ContractualSearchButton" OnClick="ContractualSearchButton_OnClick" ToolTip="Click To Search" CssClass="btn btn-info   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search </asp:LinkButton>

                                        <asp:LinkButton runat="server" ID="ContractualResetButton" OnClick="ContractualResetButton_OnClick" CssClass="btn btn-warning   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                                    
                                     </ContentTemplate>
                                </asp:UpdatePanel>
                                
                                
                                
                                
                                      <div class="form-row" style="padding-right: 10px">

                                <div class="col-md-6" style="padding-left: 20px">
                                    <label style="font-size: 18px;">Contractual List</label>
                                </div>
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-2">
                                    <%--  <asp:LinkButton ID="btnExportToExcel" runat="server" CssClass="btnexcel  pull-right" Style="padding-right: 10px;" OnClick="btnExportToExcel_Click"><span aria-hidden="true" style="font-size: 14px; color: #4CAF50;" class="fa fa-file-excel-o"></span> Download to xls</asp:LinkButton>--%>

                                    <input type="button" id="btnExportContractual" title="Export to Excel" class="pull-right btnexcelcc " value="" />
                                </div>


                            </div>
                                  <hr/>
                                  <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                                
                                <ContentTemplate>
 <asp:UpdateProgress ID="UpdateProgress20" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaitingforfun">
                                        <asp:Image ID="imgsaasasdsaWssadsdsit1" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
         </asp:UpdateProgress>
     
     <div id="gridaCoasaasantaassainser1" style="overflow: scroll; height: 500px; width: 100%">
                                <asp:GridView ID="ContractualGridView" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered text-center thead-dark"
                                     >
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
                                        <asp:BoundField DataField="NationalIdNo" HeaderText="National Id No." />
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
                                       <asp:BoundField DataField="ServiceLength" HeaderText="SMC Service Length" />
                                         <asp:BoundField DataField="DateofRetirement" HeaderText="Date of Retirement" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="ProbitionEndDate" HeaderText="Probation End Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                          <asp:BoundField DataField="ContractualEndDate" HeaderText="Contractual End Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                         <asp:BoundField DataField="Supervisor" HeaderText="Supervisor" />
                                         <asp:BoundField DataField="ContractPeriod" HeaderText="Contract Period"  />
                                        <asp:BoundField DataField="ExtensionFromDate" HeaderText="Extension Start Date" DataFormatString="{0:dd-MMM-yyyy}" />  <%--39-43--%>
                                        <asp:BoundField DataField="ExtensionToDate" HeaderText="Extension End Date" DataFormatString="{0:dd-MMM-yyyy}" /> 
                                        <asp:BoundField DataField="RenewStartDate" HeaderText="Renewal Start Date" DataFormatString="{0:dd-MMM-yyyy}" /> 
                                  <asp:BoundField DataField="RenewToDate" HeaderText="Renewal End Date" DataFormatString="{0:dd-MMM-yyyy}" /> 

                                         
                                           <asp:BoundField DataField="PermanentToContractualEffectiveDate" HeaderText="Permanent to Contractual Start Date" DataFormatString="{0:dd-MMM-yyyy}" /> 
                                  <asp:BoundField DataField="RenewToDate" HeaderText="Permanent to Contractual End Date" DataFormatString="{0:dd-MMM-yyyy}" /> 

                                         <asp:BoundField DataField="TransferProject" HeaderText="Transferred Project" />
                                         

                                    </Columns>
                                </asp:GridView>
                            </div>
                        
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                


                                </div>
                        
                        
                          <div role="tabpanel" class="tab-pane" id="Project">
                                
                                  

                            <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                                <ContentTemplate>

                                    <asp:UpdateProgress ID="UpdateProgress21" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaitingforfun">
                                        <asp:Image ID="imsdgWasdsadasdsait120" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                                    <div class="row">
                                        
                                         <div class="col-md-12">
                                            <fieldset class="for-panel">
                                                <legend>Filtering Criteria  </legend>
                                              <div class="row">
                                                   <div class="col-md-2">

                                            <div class="form-group">
                                                <label>Select a Project </label>
                                                <%--<span style="color:red">&nbsp;*</span>--%>
                                                <asp:DropDownList ID="ProjectDropDownList" class="form-control form-control-sm"  runat="server"></asp:DropDownList>
                                            </div>

                                        </div>
                                                      </div>
                                                </fieldset>
                                             </div>
                                             </div>
                                    
                                           <br/>
                                            <br/>
                                    
                                      <div class="row">
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-1">
                                </div>
                                <div class="col-md-4">

                                    <div class="form-group" style="margin-top: 17px;">


                                        <asp:LinkButton runat="server" ID="ProjectSearchButton" OnClick="ProjectSearchButton_OnClick" ToolTip="Click To Search" CssClass="btn btn-info   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search </asp:LinkButton>

                                        <asp:LinkButton runat="server" ID="ProjectResetButton" OnClick="ProjectResetButton_OnClick" CssClass="btn btn-warning   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                                    
                                     </ContentTemplate>
                                </asp:UpdatePanel>
                              
                              
                              
                              
                                <div class="form-row" style="padding-right: 10px">

                                <div class="col-md-6" style="padding-left: 20px">
                                    <label style="font-size: 18px;">Project List</label>
                                </div>
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-2">
                                    <%--  <asp:LinkButton ID="btnExportToExcel" runat="server" CssClass="btnexcel  pull-right" Style="padding-right: 10px;" OnClick="btnExportToExcel_Click"><span aria-hidden="true" style="font-size: 14px; color: #4CAF50;" class="fa fa-file-excel-o"></span> Download to xls</asp:LinkButton>--%>

                                    <input type="button" id="btnExportProject" title="Export to Excel" class="pull-right btnexcelcc " value="" />
                                </div>


                            </div>
                                  <hr/>
                                  <asp:UpdatePanel ID="UpdatePanel31" runat="server">
                                
                                <ContentTemplate>
 <asp:UpdateProgress ID="UpdateProgress22" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaitingforfun">
                                        <asp:Image ID="imgsaasasdsaWssadasdsdsit1" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
         </asp:UpdateProgress>
     
     <div id="gridaCoasaasantaasaasassainser1" style="overflow: scroll; height: 500px; width: 100%">
                                <asp:GridView ID="ProjectGridView" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered text-center thead-dark"
                                     >
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
                                        <asp:BoundField DataField="NationalIdNo" HeaderText="National Id No." />
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
                                       <asp:BoundField DataField="ServiceLength" HeaderText="SMC Service Length" />
                                         <asp:BoundField DataField="DateofRetirement" HeaderText="Date of Retirement" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="ProbitionEndDate" HeaderText="Probation End Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                          <asp:BoundField DataField="ContractualEndDate" HeaderText="Contractual End Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                     
                                          <asp:BoundField DataField="Supervisor" HeaderText="Supervisor" />
                                         

                                    </Columns>
                                </asp:GridView>
                            </div>
                        
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                                  
                                            

                </div>
                        
                        
                        
                         <div role="tabpanel" class="tab-pane" id="KPI">
                                
                                  

                            <asp:UpdatePanel ID="UpdatePanel32" runat="server">
                                <ContentTemplate>

                                    <asp:UpdateProgress ID="UpdateProgress23" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaitingforfun">
                                        <asp:Image ID="imsdgWaasdsdsadasdsait120" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                                    <div class="row">
                                        
                                         <div class="col-md-12">
                                            <fieldset class="for-panel">
                                                <legend>Filtering Criteria  </legend>
                                              <div class="row">
                                                  
                                                   <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Financial Year</label> &nbsp;<label style="color: #a52a2a">*</label>
                                        <asp:DropDownList ID="KPIddlFinancialYear"  runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                                  
                                                  
                                                  <div class="col-2">
                                    <div class="form-group">
                                        <label>Status</label>   <label style="color: #a52a2a">*</label>
                                           <asp:DropDownList runat="server" AutoPostBack="True" ID="KPIStatusDropDownList"   class="form-control form-control-sm" >
                                               <asp:ListItem Value="0">Select One</asp:ListItem>
                                               <asp:ListItem Value="1">Completed</asp:ListItem>
                                               <asp:ListItem Value="2">Not Completed</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                         </div>
                                                       </div>
                                                </fieldset>
                                             </div>
                                             </div>
                                    
                                           <br/>
                                            <br/>
                                    
                                      <div class="row">
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-1">
                                </div>
                                <div class="col-md-4">

                                    <div class="form-group" style="margin-top: 17px;">


                                        <asp:LinkButton runat="server" ID="KPISearchButton" OnClick="KPISearchButton_OnClick" ToolTip="Click To Search" CssClass="btn btn-info   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search </asp:LinkButton>

                                        <asp:LinkButton runat="server" ID="KPIResetButton" OnClick="KPIResetButton_OnClick" CssClass="btn btn-warning   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                                    
                                     </ContentTemplate>
                                </asp:UpdatePanel>
                              
                              
                              
                              
                                <div class="form-row" style="padding-right: 10px">

                                <div class="col-md-6" style="padding-left: 20px">
                                    <label style="font-size: 18px;">KPI List</label>
                                </div>
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-2">
                                    <%--  <asp:LinkButton ID="btnExportToExcel" runat="server" CssClass="btnexcel  pull-right" Style="padding-right: 10px;" OnClick="btnExportToExcel_Click"><span aria-hidden="true" style="font-size: 14px; color: #4CAF50;" class="fa fa-file-excel-o"></span> Download to xls</asp:LinkButton>--%>

                                    <input type="button" id="btnExportKPI" title="Export to Excel" class="pull-right btnexcelcc " value="" />
                                </div>


                            </div>
                             
                             
                               <hr/>
                                  <asp:UpdatePanel ID="UpdatePanel33" runat="server">
                                
                                <ContentTemplate>
 <asp:UpdateProgress ID="UpdateProgress24" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaitingforfun">
                                        <asp:Image ID="aimgsaasasdsaWssadasdsdsit1" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
         </asp:UpdateProgress>
     
     <div id="agridaCoasaasantaasaasassainser1" style="overflow: scroll; height: 500px; width: 100%">
                                <asp:GridView ID="gv_kpiSetup" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered text-center thead-dark"
                                     >
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
                                        <asp:BoundField DataField="NationalIdNo" HeaderText="National Id No." />
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
                                       <asp:BoundField DataField="ServiceLength" HeaderText="SMC Service Length" />
                                         <asp:BoundField DataField="DateofRetirement" HeaderText="Date of Retirement" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="ProbitionEndDate" HeaderText="Probation End Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                          <asp:BoundField DataField="ContractualEndDate" HeaderText="Contractual End Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                       <asp:BoundField DataField="Supervisor" HeaderText="Supervisor" />
                                       <asp:BoundField DataField="FinancialYearDesc" HeaderText="Financial Year" />
                                       <asp:BoundField DataField="Subject" HeaderText="Subject" />
                                        
                                          <asp:BoundField DataField="DeclarationDate" HeaderText="Declaration Date" DataFormatString="{0:dd-MMM-yyyy}" />


                                         

                                    </Columns>
                                </asp:GridView>
                            </div>
                        
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                             
                             
                             

                                                 </div>
                        
                         <div role="tabpanel" class="tab-pane" id="Family">
                             
                             
                              
                            
                            <asp:UpdatePanel ID="UpdatePanel34" runat="server">
                                <ContentTemplate>
                                    <asp:UpdateProgress ID="UpdateProgress25" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaitingforfun">
                                        <asp:Image ID="imgdfgdfsssssssssssWait5" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                                   
                                        <div class="row">
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-1">
                                </div>
                                <div class="col-md-4">

                                    <div class="form-group" style="margin-top: 17px;">


                                        <asp:LinkButton runat="server" ID="FamilySearchButton" OnClick="FamilySearchButton_OnClick" ToolTip="Click To Search" CssClass="btn btn-info   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search </asp:LinkButton>

                                        <asp:LinkButton runat="server" ID="FamilyResetButton" OnClick="FamilyResetButton_OnClick" CssClass="btn btn-warning   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                    
                                     <br/>
                                <div class="form-row" style="padding-right: 10px">

                                <div class="col-md-6" style="padding-left: 2px">
                                    <label style="font-size: 18px;">Family Information</label>
                                </div>
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-2" style="display: none">
                                    <%--  <asp:LinkButton ID="btnExportToExcel" runat="server" CssClass="btnexcel  pull-right" Style="padding-right: 10px;" OnClick="btnExportToExcel_Click"><span aria-hidden="true" style="font-size: 14px; color: #4CAF50;" class="fa fa-file-excel-o"></span> Download to xls</asp:LinkButton>--%>

                                    <input type="button" id="btnExportFamily" title="Export to Excel" class="pull-right btnexcelcc " value="" />
                                </div>


                            </div>
                               <hr/>
                             
                                <asp:UpdatePanel ID="UpdatePanel35" runat="server">
                                <ContentTemplate>
                                    <asp:UpdateProgress ID="UpdateProgress26" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaitingforfun">
                                        <asp:Image ID="imgdfgdfsssasdassssasssssWait5" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                                     <div class="form-row">
                                            <div class="col-9">
                                                <div style="overflow: scroll; width: 100%">
                                                    <asp:GridView ID="gv_Spouse" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">
                                                        <Columns>
                                                            <asp:BoundField DataField="SName" HeaderText="Spouse Name" />
                                                            <asp:BoundField DataField="SpouseMaxEducationName" HeaderText="Spouse's Max Education" />
                                                            <asp:BoundField DataField="SpouseOccupationName" HeaderText="Spouse's Occupation" />
                                                            <asp:BoundField DataField="SDob" HeaderText="Spouse's DOB" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Marriage Date</label>
                                                    <asp:Label runat="server" ID="lblMarriageDate" class="form-control form-control-sm" />
                                                  
                                                </div>
                                            </div>
                                        </div>
                                    
                                          <div style="overflow: scroll; width: 100%">
                                                <asp:GridView  ID="gv_Children" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL#">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                                <asp:HiddenField ID="EmpChildrenId" runat="server" Value='<%#Eval("EmpChildrenId") %>' />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Children Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ChildrenName" runat="server" Text='<%#Eval("ChildrenName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Children Gender">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ChildrenGender" runat="server" Text='<%#Eval("ChildrenGender") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Children Occupation">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ChildrenOccupation" runat="server" Text='<%#Eval("ChildrenOccupationName") %>'></asp:Label>
                                                               
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Children DOB">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ChildrenDOB" runat="server" Text='<%#Eval("ChildrenDOB") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Children MaritalStatus">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ChildrenMaritalStatus" runat="server" Text='<%#Eval("ChildrenMaritalStatus") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                       


                                                      
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                           
                             </div>
                        
                        
                          <div role="tabpanel" class="tab-pane" id="Appraisal">
                                
                                  

                            <asp:UpdatePanel ID="UpdatePanel36" runat="server">
                                <ContentTemplate>

                                    <asp:UpdateProgress ID="UpdateProgress27" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaitingforfun">
                                        <asp:Image ID="imsdgWaasdsdsadasdsasdsaait120" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                                    <div class="row">
                                        
                                         <div class="col-md-12">
                                            <fieldset class="for-panel">
                                                <legend>Filtering Criteria  </legend>
                                              <div class="row">
                                                  
                                                   <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Financial Year</label> &nbsp;<label style="color: #a52a2a">*</label>
                                        <asp:DropDownList ID="appraisalFinYearDropDownList"  runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                                  
                                                  
                                                  <div class="col-2">
                                    <div class="form-group">
                                        <label>Status</label>   <label style="color: #a52a2a">*</label>
                                           <asp:DropDownList runat="server" ID="appraisalStatusDropDownList"    class="form-control form-control-sm" >
                                               <asp:ListItem Value="0">Select One</asp:ListItem>
                                               <asp:ListItem Value="1">Completed</asp:ListItem>
                                               <asp:ListItem Value="2">Not Completed</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                         </div>
                                                  
                                                  <div class="col-8" runat="server"  id="finList">
                                                      <div runat="server"  visible="false">
                                                      <div class="Label_Title">Select Financial Year for Appraisal  </div>
                                                              
                                                                <div class="form-group">
                                                                    <div style="overflow: scroll; height: 230px">
                                                                         <asp:CheckBox runat="server" ID="chkAllfin" CssClass="SelectchkChoice" AutoPostBack="True" OnCheckedChanged="chkAllfin_OnCheckedChanged" Text=" Select All / Unselect All" />
                                                                        <br />
                                                                        <asp:CheckBoxList ID="chkfin"   CssClass="chkChoice" RepeatColumns="5" RepeatDirection="Horizontal" runat="server"></asp:CheckBoxList>
                                                                    </div>
                                                      </div>
                                                       </div>
                                                      </div>
                                                </fieldset>
                                             </div>
                                             </div>
                                    
                                           <br/>
                                            <br/>
                                    
                                      <div class="row">
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-1">
                                </div>
                                <div class="col-md-4">

                                    <div class="form-group" style="margin-top: 17px;">


                                        <asp:LinkButton runat="server" ID="appraisalSearchButton" OnClick="appraisalSearchButton_OnClick" ToolTip="Click To Search" CssClass="btn btn-info   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search </asp:LinkButton>

                                        <asp:LinkButton runat="server" ID="appraisalResetButton" OnClick="appraisalResetButton_OnClick" CssClass="btn btn-warning   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                                    
                                     </ContentTemplate>
                                </asp:UpdatePanel>
                              
                              
                              
                              
                                <div class="form-row" style="padding-right: 10px">

                                <div class="col-md-6" style="padding-left: 2px">
                                    <label style="font-size: 18px;">Performance Appraisal List</label>
                                </div>
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-2">
                                    <%--  <asp:LinkButton ID="btnExportToExcel" runat="server" CssClass="btnexcel  pull-right" Style="padding-right: 10px;" OnClick="btnExportToExcel_Click"><span aria-hidden="true" style="font-size: 14px; color: #4CAF50;" class="fa fa-file-excel-o"></span> Download to xls</asp:LinkButton>--%>

                                    <input type="button" id="btnExportAppri" title="Export to Excel" class="pull-right btnexcelcc " value="" />
                                </div>


                            </div>
                             
                             
                               <hr/>
                                  <asp:UpdatePanel ID="UpdatePanel37" runat="server">
                                
                                <ContentTemplate>
 <asp:UpdateProgress ID="UpdateProgress28" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaitingforfun">
                                        <asp:Image ID="aimgsaasaasdsasdsaWssadasdsdsit1" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
         </asp:UpdateProgress>
     
     <div id="agridaCoasaasantaasaasassadsainser1" style="overflow: scroll; height: 500px; width: 100%">
                               <%-- <asp:GridView ID="gv_appraisal" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered text-center thead-dark"
                                     >
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
                                        <asp:BoundField DataField="NationalIdNo" HeaderText="National Id No." />
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
                                       <asp:BoundField DataField="ServiceLength" HeaderText="SMC Service Length" />
                                         <asp:BoundField DataField="DateofRetirement" HeaderText="Date of Retirement" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="ProbitionEndDate" HeaderText="Probation End Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                          <asp:BoundField DataField="ContractualEndDate" HeaderText="Contractual End Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                       <asp:BoundField DataField="Supervisor" HeaderText="Supervisor" />
                                       <asp:BoundField DataField="FinancialYearDesc" HeaderText="Financial Year" />
                                       <asp:BoundField DataField="Subject" HeaderText="Subject" />
                                        
                                          <asp:BoundField DataField="DeclarationDate" HeaderText="Declaration Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                       <asp:BoundField DataField="SupervisorMark" HeaderText="Assessment Score (last 3 years)
" />

                                        
                                         

                                    </Columns>
                                </asp:GridView>--%>
         
         
         
         
         
         
          <asp:GridView ID="gvApprisalFinal" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered text-center thead-dark"
                                     >
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
                                                    <asp:BoundField DataField="NationalIdNo" HeaderText="National Id No." />
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
                                          <asp:BoundField DataField="ServiceLength" HeaderText="SMC Service Length" />
                                                     <asp:BoundField DataField="EmpExperiece" HeaderText="Carrier Service Length"  /> 
                    <asp:BoundField DataField="DateofRetirement" HeaderText="Date of Retirement" DataFormatString="{0:dd-MMM-yyyy}" /> 
             <asp:BoundField DataField="ProbitionEndDate" HeaderText="Probation End Date" DataFormatString="{0:dd-MMM-yyyy}" /> 
 <asp:BoundField DataField="ContractualEndDate" HeaderText="Contractual End Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                    <asp:BoundField DataField="Supervisor" HeaderText="Supervisor" />
                                                    <asp:BoundField DataField="GrossAmount" HeaderText="Salary Amount" />
                                                     <asp:BoundField DataField="ContractPeriod" HeaderText="Contract Period"  />
                                                     <asp:BoundField DataField="FatherName" HeaderText="Father's Name"  />
                                                     <asp:BoundField DataField="MotherName" HeaderText="Mother's Name"  />
                                         <asp:BoundField DataField="PersonalMobile" HeaderText="Personal Mobile"  />
                                                     <asp:BoundField DataField="EmergencyContactNumber" HeaderText="Emergency Number"  /> 
                                         <asp:BoundField DataField="PersonalEmail" HeaderText="Personal Email"  />
                                                     <asp:BoundField DataField="OfficialEmail" HeaderText="Official Email"  />
                                                     <asp:BoundField DataField="EmpAge" HeaderText="Age"  />
                                                     <asp:BoundField DataField="empType" HeaderText="Type of Position"  />
                                                     <asp:BoundField DataField="EmpCategoryName" HeaderText="Employment Type"  />
    <asp:BoundField DataField="DateOfRetirement" HeaderText="Retirement Date" DataFormatString="{0:dd-MMM-yyyy}" /> 
                                         <asp:BoundField DataField="TinNo" HeaderText="Tin No."  />
                                       <asp:BoundField DataField="FinancialYearDesc" HeaderText="Financial Year" />
                                       <asp:BoundField DataField="Subject" HeaderText="Subject" />  
                                          <asp:BoundField DataField="DeclarationDate" HeaderText="Declaration Date" DataFormatString="{0:dd-MMM-yyyy}" />
         <%--<asp:BoundField DataField="SupervisorMark" HeaderText="Assessment Score (last 3 years)" />--%> 
                                            <asp:BoundField DataField="SupervisorMarks" HeaderText="Functional" />
                                        <asp:BoundField DataField="SupervisorScore" HeaderText="Behavioral" />
                                        <asp:BoundField DataField="TotalMarks" HeaderText="Total" />
                                        <asp:BoundField DataField="FinalStatus" HeaderText="Final Result" />
                                        <asp:BoundField DataField="" HeaderText="Remarks" />
                                        <asp:BoundField DataField="Promotion" HeaderText="Promotion" />
                                        <asp:BoundField DataField="PromotionwithIncrement" HeaderText="Promotion with Increment" />
                                        <asp:BoundField DataField="SI" HeaderText="Special Increment" />
                                        <asp:BoundField DataField="Pip" HeaderText="PIP" />
                                        <asp:BoundField DataField="DegreeName" HeaderText="Training"   HtmlEncode="False" />
                                        <asp:BoundField DataField="LastPromotion" HeaderText="Last Promotion/Up-gradation" />
                                        <asp:BoundField DataField="DiciplinaryCout" HeaderText="Discip. Action in last two years" /> 
                                         <asp:BoundField DataField="HisScore14_15" HeaderText="2014-15" />
                                        <asp:BoundField DataField="HisScore15_16" HeaderText="2015-16" />
                                        <asp:BoundField DataField="HisScore16_17" HeaderText="2016-17" />
                                        <asp:BoundField DataField="HisScore17_18" HeaderText="2017-18" />
                                        <asp:BoundField DataField="HisScore18_19" HeaderText="2018-19" />
                                           
                                        <asp:BoundField DataField="HisScore19_20" HeaderText="2019-20" />
                                        <asp:BoundField DataField="TotalMarks" HeaderText="2020-21" />
                                      
                                        <asp:BoundField DataField="HisScore21_22" HeaderText="2021-22" />

                                       
                                        
                                         

                                    </Columns>
                                </asp:GridView>
                            </div>
                        
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                             
                             
                             

                                                 </div>
                        
                         <div role="tabpanel" class="tab-pane" id="Survey">
                                
                                  

                            <asp:UpdatePanel ID="UpdatePanel38" runat="server">
                                <ContentTemplate>

                                    <asp:UpdateProgress ID="UpdateProgress29" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaitingforfun">
                                        <asp:Image ID="imssadgWaasdsasddsadasdsasdsaait120" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                                    <div class="row">
                                        
                                         <div class="col-md-12">
                                            <fieldset class="for-panel">
                                                <legend>Filtering Criteria  </legend>
                                              <div class="row">
                                                  
                                                   <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Financial Year</label> &nbsp;<label style="color: #a52a2a">*</label>
                                        <asp:DropDownList ID="surveyFinYearDropDownList"  runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                                  
                                                  
                                                            <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Survey Name</label> &nbsp;<label style="color: #a52a2a">*</label>
                                        <asp:DropDownList ID="SurveyNameDropDownList"  runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                                  
                                                  
                                                  <div class="col-2">
                                    <div class="form-group">
                                        <label>Status</label>   <label style="color: #a52a2a">*</label>
                                           <asp:DropDownList runat="server" AutoPostBack="True" ID="surveyStatusDropDownList"   class="form-control form-control-sm" >
                                               <asp:ListItem Value="0">Select One</asp:ListItem>
                                               <asp:ListItem Value="1">Completed</asp:ListItem>
                                               <asp:ListItem Value="2">Not Completed</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                         </div>
                                                       </div>
                                                </fieldset>
                                             </div>
                                             </div>
                                    
                                           <br/>
                                            <br/>
                                    
                                      <div class="row">
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-1">
                                </div>
                                <div class="col-md-4">

                                    <div class="form-group" style="margin-top: 17px;">


                                        <asp:LinkButton runat="server" ID="surveySearchButton" OnClick="surveySearchButton_OnClick" ToolTip="Click To Search" CssClass="btn btn-info   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search </asp:LinkButton>

                                        <asp:LinkButton runat="server" ID="surveyResetButton" OnClick="surveyResetButton_OnClick" CssClass="btn btn-warning   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                                    
                                     </ContentTemplate>
                                </asp:UpdatePanel>
                              
                              
                              
                              
                                <div class="form-row" style="padding-right: 10px">

                                <div class="col-md-6" style="padding-left: 2px">
                                    <label style="font-size: 18px;">Survey List</label>
                                </div>
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-2">
                                    <%--  <asp:LinkButton ID="btnExportToExcel" runat="server" CssClass="btnexcel  pull-right" Style="padding-right: 10px;" OnClick="btnExportToExcel_Click"><span aria-hidden="true" style="font-size: 14px; color: #4CAF50;" class="fa fa-file-excel-o"></span> Download to xls</asp:LinkButton>--%>

                                    <input type="button" id="btnExportSurvey" title="Export to Excel" class="pull-right btnexcelcc " value="" />
                                </div>


                            </div>
                             
                             
                               <hr/>
                                  <asp:UpdatePanel ID="UpdatePanel39" runat="server">
                                
                                <ContentTemplate>
 <asp:UpdateProgress ID="UpdateProgress30" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaitingforfun">
                                        <asp:Image ID="aimgsaasaasdsaasdsdsaWssadasdsdsit1" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
         </asp:UpdateProgress>
     
     <div id="agridaCoasaasantaasaaasdsassadsainser1" style="overflow: scroll; height: 500px; width: 100%">
                                <asp:GridView ID="gv_serveyGridView" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered text-center thead-dark"
                                     >
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
                                        <asp:BoundField DataField="NationalIdNo" HeaderText="National Id No." />
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
                                       <asp:BoundField DataField="ServiceLength" HeaderText="SMC Service Length" />
                                         <asp:BoundField DataField="DateofRetirement" HeaderText="Date of Retirement" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="ProbitionEndDate" HeaderText="Probation End Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                          <asp:BoundField DataField="ContractualEndDate" HeaderText="Contractual End Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                       <asp:BoundField DataField="Supervisor" HeaderText="Supervisor" />
                                      
                                        
                                          


                                         

                                    </Columns>
                                </asp:GridView>
                            </div>
                        
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                             
                             
                             

                                                 </div>
                        
                        
                        
                           <div role="tabpanel" class="tab-pane" id="Recruitment">
                                
                                  

                            <asp:UpdatePanel ID="UpdatePanel42" runat="server">
                                <ContentTemplate>

                                    <asp:UpdateProgress ID="UpdateProgress32" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaitingforfun">
                                        <asp:Image ID="imsdgWdasdsnnnnnnnait120" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                                    <div class="row">
                                        
                                         <div class="col-md-12">
                                            <fieldset class="for-panel">
                                                <legend>Filtering Criteria  </legend>
                                              <div class="row">
                                                  
                                                      <div class="col-2" runat="server" >
                                <div class="form-group">
                                    <label class="control-label">From Date</label>
                                    <asp:TextBox runat="server" ID="txtRecruitmentfrmDate" class="form-control form-control-sm"   ></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender50" runat="server"
                                                                  Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                  TargetControlID="txtRecruitmentfrmDate" />
                                </div>
                            </div>
                            <div class="col-2" runat="server" >
                                <div class="form-group">
                                    <label class="control-label">To Date</label>
                                    <asp:TextBox runat="server" ID="txtRecruitmentToDate" class="form-control form-control-sm"  ></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender51" runat="server"
                                                                  Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                  TargetControlID="txtRecruitmentToDate" />
                                </div>
                            </div>
                                                  
                                                  
                                                    <div class="col-3" runat="server" >
                                <div class="form-group">
                                    <label class="control-label">Recruitment Type</label>&nbsp; 
                                    <asp:DropDownList ID="ddlRecruitmentType" CssClass="form-control form-control-sm" runat="server">
                                        <asp:ListItem Selected="True" Value="0">Select One</asp:ListItem>
                                        
                                        <asp:ListItem Value="New">New</asp:ListItem>
                                        <asp:ListItem Value="Replacement">Replacement</asp:ListItem>
                                        <asp:ListItem Value="Reappointment">Reappointment </asp:ListItem>
                                       
                                    </asp:DropDownList>
                                </div>
                            </div>
                                                     </div>
                                                </fieldset>
                                             </div>
                                             </div>
                                    
                                           <br/>
                                            <br/>
                                    
                                      <div class="row">
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-1">
                                </div>
                                <div class="col-md-4">

                                    <div class="form-group" style="margin-top: 17px;">


                                        <asp:LinkButton runat="server" ID="lbRequirmentSearch" OnClick="lbRequirmentSearch_OnClick" ToolTip="Click To Search" CssClass="btn btn-info   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search </asp:LinkButton>

                                        <asp:LinkButton runat="server" ID="LinkButton2" OnClick="ContractualResetButton_OnClick" CssClass="btn btn-warning   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                                    
                                     </ContentTemplate>
                                </asp:UpdatePanel>
                                
                                
                                
                                
                                      <div class="form-row" style="padding-right: 10px">

                                <div class="col-md-6" style="padding-left: 20px">
                                    <label style="font-size: 18px;">Recruitment List</label>
                                </div>
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-2">
                                    <%--  <asp:LinkButton ID="btnExportToExcel" runat="server" CssClass="btnexcel  pull-right" Style="padding-right: 10px;" OnClick="btnExportToExcel_Click"><span aria-hidden="true" style="font-size: 14px; color: #4CAF50;" class="fa fa-file-excel-o"></span> Download to xls</asp:LinkButton>--%>

                                    <input type="button" id="btnExportRecruitment" title="Export to Excel" class="pull-right btnexcelcc " value="" />
                                </div>


                            </div>
                                  <hr/>
                                  <asp:UpdatePanel ID="UpdatePanel43" runat="server">
                                
                                <ContentTemplate>
 <asp:UpdateProgress ID="UpdateProgress33" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaitingforfun">
                                        <asp:Image ID="imgsaasnnmbmasdsaWssadsdsit1" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
         </asp:UpdateProgress>
     
     <div id="gridaCoasaasantaassaghinser1" style="overflow: scroll; height: 500px; width: 100%">
                                <asp:GridView ID="gv_Recruitment" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered text-center thead-dark"
                                     >
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                    
                                        <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee ID" />
                                     
                                        <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                            <asp:BoundField DataField="Division" HeaderText="Division" />
                                         <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                         <asp:BoundField DataField="RecruitmentType" HeaderText="Recruitment Type" />
                                      
                                         <asp:BoundField DataField="proEffectivedate" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                      
                                         

                                    </Columns>
                                </asp:GridView>
                            </div>
                        
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                


                                </div>
                        
                        
                        
            </div>

        </div>

    </div>

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
    <%--ExportEducation--%>
    <script src="../Assets/table2excel.js"></script>
    <script type="text/javascript">
        $("body").on("click", "#btnExportEducation", function () {
            $("[id*=gv_Education]").table2excel({
                filename: "Education_Employee_List_Info.xls"
            });
        });

    </script>
    <script type="text/javascript">
        $("body").on("click", "#btnExportEmpDetails", function () {
            $("[id*=loadGridView]").table2excel({
                filename: "Employee_List_Info.xls"
            });
        });

    </script>


    <script type="text/javascript">
        $("body").on("click", "#btnExportDisciplinary", function () {
            $("[id*=SusPendGridView]").table2excel({
                filename: "Employee_Suspend_List_Info.xls"
            });
        });

    </script>
    
    
    
    <script type="text/javascript">
        $("body").on("click", "#btnExportDisciplinary", function () {
            $("[id*=DisplinaryGridView]").table2excel({
                filename: "Employee_Disciplinary_List_Info.xls"
            });
        });

    </script>
    
    
    
    <script type="text/javascript">
        $("body").on("click", "#btnExportPromotion", function () {
            $("[id*=PromotionloadGridView]").table2excel({
                filename: "Employee_Promotion_List_Info.xls"
            });
        });

    </script>
    
     <script type="text/javascript">
         $("body").on("click", "#btnExportPromotion", function () {
             $("[id*=NoPromoGridView]").table2excel({
                 filename: "Employee_No_Promotion_List_Info.xls"
             });
         });

    </script>
    
    
    
     <script type="text/javascript">
         $("body").on("click", "#btnExportSeparation", function () {
             $("[id*=SeparationloadGridView]").table2excel({
                 filename: "Employee_Separation_List_Info.xls"
             });
         });

    </script>
    
    
      <script type="text/javascript">
          $("body").on("click", "#btnExportIncrement", function () {
              $("[id*=IncrementloadGridView]").table2excel({
                  filename: "Employee_Increment_List_Info.xls"
              });
          });

    </script>
    
    
    
      <script type="text/javascript">
          $("body").on("click", "#btnExportEmpBirthDay", function () {
              $("[id*=EmpBirthDayloadGridView]").table2excel({
                  filename: "Employee_BirthDay_List_Info.xls"
              });
          });

    </script>
    
     
      <script type="text/javascript">
          $("body").on("click", "#btnExportCheckup", function () {
              $("[id*=CheckupGridView1]").table2excel({
                  filename: "Medical_Check-Up_List_Info.xls"
              });
          });

    </script>
    
    
      
      <script type="text/javascript">
          $("body").on("click", "#btnExportExperience", function () {
              $("[id*=gv_Experience]").table2excel({
                  filename: "Employee_Experience_List_Info.xls"
              });
          });

    </script>
    
    
      
      <script type="text/javascript">
          $("body").on("click", "#btnExportTraining", function () {
              $("[id*=gv_Training]").table2excel({
                  filename: "Employee_Training_List_Info.xls"
              });
          });

    </script>
    
    
    
       <script type="text/javascript">
           $("body").on("click", "#btnExportContractual", function () {
               $("[id*=ContractualGridView]").table2excel({
                   filename: "Employee_Contractual_List_Info.xls"
               });
           });

    </script>
        
        
             <script type="text/javascript">
                 $("body").on("click", "#btnExportKPI", function () {
                     $("[id*=gv_kpiSetup]").table2excel({
                         filename: "Employee_kpi_Setup_List_Info.xls"
                     });
                 });

                   </script>
    
     <script type="text/javascript">
         $("body").on("click", "#btnExportProject", function () {
             $("[id*=ProjectGridView]").table2excel({
                 filename: "Employee_Project_List.xls"
             });
         });

                   </script>
    
    
    
    
     <script type="text/javascript">
         $("body").on("click", "#btnExportRecruitment", function () {
             $("[id*=gv_Recruitment]").table2excel({
                 filename: " Recruitment_List.xls"
             });
         });

                   </script>
    
    
    

                 <script type="text/javascript">
                     $("body").on("click", "#btnExportAppri", function () {
                         $("[id*=gv_appraisal]").table2excel({
                      filename: "Performance Appraisal List.xls"
                  });
                         $("[id*=gvApprisalFinal]").table2excel({
                             filename: "Performance Appraisal List.xls"
                         });
              });

    </script>
        
        
    
</asp:Content>


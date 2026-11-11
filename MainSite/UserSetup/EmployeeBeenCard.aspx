<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MainMasterPage.master" CodeFile="EmployeeBeenCard.aspx.cs" Inherits="UserSetup_EmployeeBeenCard" EnableEventValidation="false" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>


<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">
    
      <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cpFormBody" runat="Server">


    <style type="text/css">
        /*AutoComplete flyout */
      
    </style>


    <div class="content" id="content">

        <style>
      

        </style>
        <div class="container-fluid">
            <div class="page-heading">
                <div class="page-heading__container">
                    <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" />&nbsp;Employee History Report </h1>
                </div>
                <div class="page-heading__container float-right d-none d-sm-block">

                    <asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="HomeButton_OnClick" />

                    <asp:Button ID="btnEditInfo" Visible="False" Text="Update Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="btnEditInfo_OnClick" />
                    <asp:Button ID="btn_New" Text="Create New"  Visible="False"  CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="btn_New_OnClick" />

                </div>
            </div>
            <div class="container-fluid">
                <div class="card">
                    <asp:UpdatePanel runat="server" ID="uppa">
                        <ContentTemplate>
                            <%--  <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="uppa">
                                <ProgressTemplate>
                                    <div class="modal">
                                        <div class="center">
                                            <img alt="" src="/Assets/loader.gif" />
                                        </div>
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>--%>
                            <div class="card-body">


                                <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                    <ProgressTemplate>
                                        <div class="divWaiting">
                                            <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                                <div class="form-row">
                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>Company</label>
                                            <label style="color: #a52a2a">*</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" class="form-control form-control-sm" />
                                        </div>
                                    </div>
                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>Division</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDivision" class="form-control form-control-sm clsSelect" OnSelectedIndexChanged="ddlDivision_OnSelectedIndexChanged" />
                                              <script type="text/javascript">
                                                  function pageLoad() {
                                                      $('.clsSelect').chosen({ disable_search_threshold: 5, search_contains: true });







                                                  }
</script>
                                        </div>
                                    </div>
                                    <div class="col-2" runat="server" id="wing" visible="False">
                                        <div class="form-group">
                                            <label>Wing</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlWing" class="form-control form-control-sm clsSelect" OnSelectedIndexChanged="ddlWing_OnSelectedIndexChanged" />

                                        </div>
                                    </div>
                                    <div class="col-2" runat="server" id="dept">
                                        <div class="form-group">
                                            <label>Department</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDepartment" class="form-control form-control-sm clsSelect" OnSelectedIndexChanged="ddlDepartment_OnSelectedIndexChanged" />

                                        </div>
                                    </div>
                                    <div class="col-2" runat="server" id="sec" visible="False">
                                        <div class="form-group">
                                            <label>Section</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlSection" class="form-control form-control-sm clsSelect" OnSelectedIndexChanged="ddlSection_OnSelectedIndexChanged" />

                                        </div>
                                    </div>
                                    <div class="col-2" runat="server" id="subsec" visible="False">
                                        <div class="form-group">
                                            <label>Sub Section</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlSubSection" class="form-control form-control-sm" OnSelectedIndexChanged="ddlSubSection_OnSelectedIndexChanged" />

                                        </div>
                                    </div>

                                    <div class="col-md-2" runat="server" Visible="False">

                                        <div class="form-group">
                                            <label>Employee ID: </label>
                                            <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="True" placeholder=" Employee ID" CssClass="form-control form-control-sm"
                                                OnTextChanged="EmployeeDropDownList2_SelectedIndexChanged"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters=""
                                                EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                ServiceMethod="GetCompanyWiseEmployeeInfoForIDANdNae" ServicePath="~/WebService.asmx" TargetControlID="txtSearch"
                                                UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                ShowOnlyCurrentWordInCompletionListItem="true">
                                            </cc1:AutoCompleteExtender>

                                        </div>
                                    </div>


                                    <div class="col-md-4">

                                        <div class="form-group">
                                            <label>Employee Name: </label>
                                            
                                                                <asp:DropDownList  runat="server"   ID="ddlEmpInfo" class="form-control form-control-sm clsSelect" />
                                            

                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Designation</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDesignation" class="form-control form-control-sm clsSelect" />

                                        </div>
                                    </div>
                                </div>

                                <div class="row">




                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Office</label>
                                            <asp:DropDownList runat="server" ID="ddlSalaryLocation" class="form-control form-control-sm clsSelect" />

                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Confirmation Status</label>
                                            <asp:DropDownList runat="server" ID="ddlConformationStatus" CssClass="form-control form-control-sm">
                                                <asp:ListItem Text="Select..." Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                            <div class="col-md-2">
                                                        <div class="form-group">
                                                            <label>Employment Status </label>

                                                            <asp:DropDownList ID="empStatusDropDownList" AutoPostBack="True" OnSelectedIndexChanged="empStatusDropDownList_OnSelectedIndexChanged" class="form-control form-control-sm" runat="server">
                                                                <asp:ListItem Value="0">All</asp:ListItem>
                                                                <asp:ListItem Selected="True" Value="Yes">Active</asp:ListItem>
                                                                <asp:ListItem Value="No">Inactive</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
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
                                        }
                                    </style>
                                    <div class="col-md-4">
                                        <div class="form-group" style="margin-top: 17px;">
                                              <asp:LinkButton runat="server" ID="SearchButton" OnClick="SearchButton_OnClick"   CssClass="btn btnMyDesignSearch   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search Information </asp:LinkButton>

                                            <asp:LinkButton runat="server" ID="btnReset" OnClick="btnReset_OnClick" CssClass="btn btnMyDesignReset   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset Information</asp:LinkButton>
                                           
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                </div>



                            </div>
                            <div class="row" runat="server" visible="False">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label></label>
                                        <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True" RepeatDirection="Horizontal" Enabled="False">
                                        </asp:CheckBoxList>


                                    </div>
                                </div>
                            </div>

                            <div class="row" runat="server" Visible="False">
                                <div class="col-md-8">
                                </div>

                                <style>
                                    .ssss {
                                        font-size: 13px;
                                        font-weight: bold;
                                    }
                                </style>

                                <div class="col-md-2" style="margin-top: 22px; padding: 5px;">


                                    <asp:Label ID="lblCount" runat="server" CssClass="ssss pull-right" Text="Total : 0"></asp:Label>




                                </div>



                                <div class="col-md-2" style="margin-top: 17px;">
                                    <asp:LinkButton ID="btnExportToExcel" runat="server" CssClass="btnexcel  pull-right" OnClick="btnExportToExcel_Click"><i class="fa fa-download"></i> Export To Excel</asp:LinkButton>
                                </div>
                            </div>
                            <%-- <div id="tbl_container" style="width:100%" class="">

                    </div>--%>

                            <div id="gridContainer1" style="height: auto; overflow: auto; width: auto;">
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                       CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender"   DataKeyNames="EmpInfoId"
                                    OnRowCommand="loadGridView_RowCommand" Font-Size="12px" AllowPaging="True" PageIndex="0" OnPageIndexChanging="loadGridView_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="History">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ViewReportImageButton" runat="server" class="btn btn-white btn-sm  " CommandArgument='<%#Eval("EmpInfoId") %>'
                                                    CommandName="ViewReport" ImageUrl="~/Assets/report_magnify.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--<asp:TemplateField HeaderText="NOC Letter">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="VisitingLetterImageButton" runat="server" class="btn btn-white btn-sm  " CommandArgument='<%#Eval("EmpInfoId") %>'
                                                    CommandName="VisitingLetter" ImageUrl="~/Assets/file.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                        <asp:BoundField DataField="EmpMasterCode" HeaderText="ID" />
                                        <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="DivisionName" HeaderText="Division" />

                                        <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                        <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                        <asp:BoundField DataField="SalaryLocation" HeaderText="Office" />
                                        <asp:BoundField DataField="EmpType" HeaderText="Employee Type" />
                                        <asp:BoundField DataField="EmployeeStatus" HeaderText="Employee Status" />



                                       <%-- <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="deleteImageButton" runat="server"
                                                    CommandArgument='<%#Eval("EmpInfoId") %>' CommandName="DeleteData"
                                                    ImageUrl="~/Assets/img/delete.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="viewImageButton" runat="server"
                                                    CommandArgument='<%#Eval("EmpInfoId") %>' CommandName="ViewData"
                                                    ImageUrl="~/Assets/img/list-view.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                </asp:GridView>
                            </div>

                        </ContentTemplate>

                        <Triggers>

                            <asp:PostBackTrigger ControlID="btnExportToExcel" />
                        </Triggers>
                    </asp:UpdatePanel>
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
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
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
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>
            </div> <br/>
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
                    <br/>
                    <br/>
                    <br/>
                    <br/>
                    <br/>

        </div>
    </div>

    <style>
        .GridPager a,
        .GridPager span {
            display: inline-block;
            padding: 3px 14px;
            margin-right: 8px;
            border-radius: 3px;
            height: 20px;
            border: solid 1px #c0c0c0;
            background: #e9e9e9;
            box-shadow: inset 0px 1px 0px rgba(255,255,255, .8), 0px 1px 3px rgba(0,0,0, .1);
            font-size: 14px;
            font-weight: bold;
            text-decoration: none;
            color: #717171;
            text-shadow: 0px 1px 0px rgba(255,255,255, 1);
        }

        .GridPager a {
            background-color: #f5f5f5;
            color: #969696;
            border: 1px solid #969696;
        }

        .GridPager span {
            background: #616161;
            box-shadow: inset 0px 0px 8px rgba(0,0,0, .5), 0px 1px 0px rgba(255,255,255, .8);
            color: #f0f0f0;
            text-shadow: 0px 0px 3px rgba(0,0,0, .5);
            border: 1px solid #3AC0F2;
        }
    </style>
</asp:Content>

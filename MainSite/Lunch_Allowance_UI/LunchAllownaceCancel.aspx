<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="LunchAllownaceCancel.aspx.cs" Inherits="Lunch_Allowance_UI_LunchAllownaceCancel" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>


    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">

                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">
                            <img src="../Report_Pages/app.png" width="20px" />
                            Lunch Cancel Information  </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:LinkButton ID="HomeButton" Style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>

                        <asp:Button ID="detailsViewButton" Text="Go Back" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>

                </div>
                <asp:HiddenField ID="hdpk" runat="server" />
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">

                            <%--<form>--%>


                            <div class="row">


                                <div class="col-md-2">

                                    <div class="form-group">
                                        <label>Cancel Date </label>
                                        <span style="color: red">&nbsp;*</span>


                                        <asp:TextBox ID="effectiveDateTextBox" AutoCompleteType="Disabled" runat="server" CausesValidation="true"
                                            class="form-control form-control-sm"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Format="dd-MMM-yyyy" CssClass="MyCalendar" PopupPosition="TopLeft"
                                            TargetControlID="effectiveDateTextBox" />
                                    </div>
                                </div>


                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Cancel To Date </label>
                                        <asp:TextBox ID="txtCancelToDate" runat="server" AutoCompleteType="Disabled" CausesValidation="true" placeholder=" Cancel Date " CssClass="form-control form-control-sm"></asp:TextBox>

                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                            Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                            TargetControlID="txtCancelToDate" />




                                        <%--              
                                        <asp:TextBox ID="effectiveDateTextBox" AutoCompleteType="Disabled" runat="server" AutoPostBack="True" OnTextChanged="effectiveDateTextBox_OnTextChanged"  CausesValidation="true" 
                                                     class="form-control form-control-sm"></asp:TextBox>
                                         
                                          
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                                      Format="dd-MMM-yyyy" CssClass="MyCalendar" PopupPosition="TopRight"  
                                                                      TargetControlID="effectiveDateTextBox" />--%>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                </div>
                            </div>
                            <div class="row">

                                <div class="col-md-2">

                                    <div class="form-group">
                                        <label>Company Name </label>
                                        <span style="color: red">&nbsp;*</span>
                                        <asp:DropDownList ID="companyDropDownList" class="form-control form-control-sm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="companyDropDownList_OnSelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>


                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Division</label>
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDivision" class="form-control form-control-sm SelectMe" OnSelectedIndexChanged="ddlDivision_OnSelectedIndexChanged" />

                                        <script type="text/javascript">
                                            function pageLoad() {
                                                $('.SelectMe').chosen({ disable_search_threshold: 5, search_contains: true });





                                            }
                                        </script>

                                    </div>
                                </div>
                                <div class="col-2" runat="server" id="wing" visible="False">
                                    <div class="form-group">
                                        <label>Wing</label>
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlWing" class="form-control form-control-sm" OnSelectedIndexChanged="ddlWing_OnSelectedIndexChanged" />

                                    </div>
                                </div>
                                <div class="col-2" runat="server" id="dept">
                                    <div class="form-group">
                                        <label>Department</label>
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDepartment" class="form-control form-control-sm SelectMe" OnSelectedIndexChanged="ddlDepartment_OnSelectedIndexChanged" />

                                    </div>
                                </div>
                                <div class="col-2" runat="server" id="sec" visible="False">
                                    <div class="form-group">
                                        <label>Section</label>
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlSection" class="form-control form-control-sm" OnSelectedIndexChanged="ddlSection_OnSelectedIndexChanged" />

                                    </div>
                                </div>
                                <div class="col-2" runat="server" id="subsec" visible="False">
                                    <div class="form-group">
                                        <label>Sub Section</label>
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlSubSection" class="form-control form-control-sm" OnSelectedIndexChanged="ddlSubSection_OnSelectedIndexChanged" />

                                    </div>
                                </div>

                                <div class="col-md-4">

                                    <div class="form-group">
                                        <label>Employee Name </label>
                                        <asp:DropDownList runat="server" ID="ddlEmpInfo" class="form-control form-control-sm SelectMe" />

                                    </div>
                                </div>

                                <div class="col-md-2" runat="server" visible="False">

                                    <div class="form-group">
                                        <label>Employee ID </label>
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


                                <div class="col-md-2" runat="server" visible="False">

                                    <div class="form-group">
                                        <label>Employee Name </label>
                                        <asp:TextBox ID="NameTextBox" runat="server" AutoPostBack="True" placeholder=" Employee Name" CssClass="form-control form-control-sm" OnTextChanged="EmployeeDropDownList_SelectedIndexChanged"></asp:TextBox>

                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                            EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                            ServiceMethod="GetCompanyWiseEmployeeInfoForIDANdNae" ServicePath="~/WebService.asmx" TargetControlID="NameTextBox"
                                            UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                            ShowOnlyCurrentWordInCompletionListItem="true">
                                        </cc1:AutoCompleteExtender>

                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Designation</label>
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDesignation" class="form-control form-control-sm SelectMe" />

                                    </div>
                                </div>

                                <div class="col-md-2" runat="server" visible="False">
                                    <div class="form-group">
                                        <label>Office</label>
                                        <asp:DropDownList runat="server" ID="ddlSalaryLocation" class="form-control form-control-sm SelectMe" />

                                    </div>
                                </div>

                                <div class="col-md-2" runat="server" visible="False">
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
                                        <label>Active Status</label>
                                        <asp:DropDownList runat="server" ID="ActiveStatusDropDownList" CssClass="form-control form-control-sm">
                                            <asp:ListItem Text="Select..." Value="-1"></asp:ListItem>
                                            <asp:ListItem Selected="True" Text="Yes" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                
                                
                                           <div class="col-2">
                                                        <div class="form-group">
                                                            <label>Employee Category</label>
                                                          
                                                            <asp:DropDownList runat="server"   ID="ddlEmpCategory" class="form-control form-control-sm"   />

                                                        </div>
                                                    </div>
                                

                                <div class="col-md-3">
                                    <div class="form-group" style="margin-top: 18px;">

                                        <asp:LinkButton runat="server" ID="Button1" OnClick="Button1_OnClick" ToolTip="Click To Search" Width="80" Text="Search" class="btn btn-info btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search </asp:LinkButton>

                                        &nbsp;&nbsp;

                                            <asp:LinkButton runat="server" ID="btnReset" OnClick="btnReset_OnClick" ToolTip="Click To Reset" Text="Reset" Width="80" CssClass="btn btn-warning   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset </asp:LinkButton>

                                        <%--<asp:Button runat="server" ID="Button1" Text=" Search  " OnClick="Button11_OnClick" />--%>
                                    </div>
                                </div>


                            </div>


                            <div class="row">

                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-bordered text-center thead-dark table-hover table-striped" DataKeyNames="EmpInfoId, GuestInternInformationId" ShowFooter="True">
                                            <Columns>

                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkSelectAll" runat="server" CssClass="form-control-sm" AutoPostBack="True" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" CssClass="form-control-sm" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SL">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                        <asp:HiddenField runat="server" ID="hfActionStatus" Value='<%#Eval("ActionStatus")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee ID" />
                                                <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                                <asp:BoundField DataField="DivisionName" HeaderText="Division" />

                                                <asp:BoundField DataField="DepartmentName" HeaderText="Department" />

                                                <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                                
                                                
                                                  <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        
                                                        <asp:Label runat="server" ID="hffromDate" Text='<%#Eval("fromDate")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Comments">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtComments" runat="server" placeholder="Comments" TextMode="MultiLine" Rows="2" Columns="2" CssClass="form-control" Text='<%#Eval("Remarks")%>'></asp:TextBox>

                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            </Columns>
                                        </asp:GridView>


                                    </div>
                                </div>

                            </div>

                            <div class="row">

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Button ID="submitButton" Text=" Submit " CssClass="btn btn-sm btn-info" runat="server" OnClick="submitButton_OnClick" />


                                    </div>
                                </div>
                                <div class="col-md-3">
                                </div>

                                <div class="col-md-4">
                                </div>
                            </div>

                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />

                            <%--</form>--%>
                        </div>


                    </div>
                </div>
                </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>


</asp:Content>


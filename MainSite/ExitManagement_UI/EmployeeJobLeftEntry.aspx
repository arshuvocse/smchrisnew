<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeJobLeftEntry.aspx.cs" Inherits="ExitManagement_UI_EmployeeJobLeftEntry" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    

    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">

    <style type="text/css">
        /*AutoComplete flyout */
     
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

    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                        <div class="icon"></div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;"><img src="../Report_Pages/app.png"  width="20px" /> Employee Separation </h1>
                    </div>

                    <%-- <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="detailsViewButton" Text="View List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>--%>

                    <div class="page-heading__container float-right d-none d-sm-block">
                        
                            <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                        <asp:LinkButton ID="ListViewButton"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
                  
                    
                    </div>

                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">

                            <div class="row">

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Company Name </label>
                                        <span style="color: red">&nbsp;*</span>
                                        <asp:DropDownList ID="companyDropDownList" class="form-control form-control-sm" OnSelectedIndexChanged="companyDropDownList_OnSelectedIndexChanged" runat="server" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Job Left Type: </label>
                                        <span style="color: red">&nbsp;*</span>
                                        <asp:DropDownList ID="JobLeftTypeDropDownList" OnSelectedIndexChanged="JobLeftTypeDropDownList_OnSelectedIndexChanged" AutoPostBack="True"  class="form-control form-control-sm" runat="server"></asp:DropDownList>
                                             <asp:CheckBox ID="chkIsSubmissionDate" Visible="False" Text="Is Submission Date" CssClass="checkbox margin-right" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-6">

                                    <asp:HiddenField ID="EmployeeJobLeftIdHiddenField" runat="server" />
                                    <asp:HiddenField ID="typeHiddenField" runat="server" />
                                    <asp:HiddenField ID="isprobHiddenField" runat="server" />
                                    <asp:HiddenField ID="cateHiddenField" runat="server" />
                                    <asp:HiddenField ID="gradeHiddenField" runat="server" />
                                    <div class="form-group">
                                        <label>Search Employee </label>
                                        <span style="color: red">&nbsp;*</span>
                                        
                                         <asp:DropDownList   runat="server"   ID="ddlEmpInfo" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpInfo_OnSelectedIndexChanged" class="form-control form-control-sm" />
                                                    <script type="text/javascript">
                                                        function pageLoad() {
                                                            $('#<%=ddlEmpInfo.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });
                                                            $('.SelectMe22').chosen({ disable_search_threshold: 5, search_contains: true });

                                                        }
                                                        </script>
                                                        <%--<asp:DropDownList ID="EmployeeDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" ></asp:DropDownList>--%>
                                        <asp:TextBox ID="SearchEmployeeNameTextBoxTextBox" Visible="False" AutoPostBack="True" runat="server" Enabled="False" CssClass="form-control form-control-sm" OnTextChanged="SearchEmployeeNameTextBoxTextBox_OnTextChanged"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                            EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                            ServiceMethod="GetCompanyWiseEmployeeInfoOnlyForPromotion" ServicePath="~/WebService.asmx" TargetControlID="SearchEmployeeNameTextBoxTextBox"
                                            UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                            ShowOnlyCurrentWordInCompletionListItem="true">
                                        </cc1:AutoCompleteExtender>

                                        <asp:HiddenField ID="repEmpIdHiddenField" runat="server" />

                                        <%--<asp:AutoCompleteExtender
                                                            ID="at_txt_JobCirculation"
                                                            TargetControlID="EmployeeNameTextBox"
                                                            runat="server"
                                                            ServiceMethod="GetParticipantList"
                                                            ServicePath="~/WebService.asmx"
                                                            MinimumPrefixLength="2"
                                                            CompletionInterval="10"
                                                            EnableCaching="false"
                                                            CompletionSetCount="1"
                                                            FirstRowSelected="false">
                                                        </asp:AutoCompleteExtender>--%>
                                        <asp:HiddenField ID="jobIdHiddenField" runat="server" />
                                    </div>
                                    <%-- <div class="form-group">
                                        <label>Search Employee: </label>
                                        <asp:DropDownList ID="EmployeeDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="EmployeeDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:HiddenField ID="jobIdHiddenField" runat="server" />
                                    </div>--%>
                                </div>



                            </div>



                            <fieldset class="for-panel">




                                <div class="row">

                                    <div class="col-md-3">

                                        <div class="form-group">
                                            <label>Employee Code: </label>
                                            <asp:Label ID="lblEmployeeCode" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Designation: </label>
                                            <asp:Label ID="lblDesignation" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-3" runat="server" visible="False">

                                        <div class="form-group">

                                            <label>Company Name: </label>
                                            <asp:Label ID="lblComName" runat="server" Text=""></asp:Label>

                                        </div>
                                    </div>



                                    <div class="col-md-3" runat="server">

                                        <div class="form-group" runat="server">
                                            <label>Employement Type: </label>
                                            <asp:Label ID="lblSubSection" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-2" runat="server" visible="False">


                                        <div class="form-group" runat="server" visible="False">
                                            <label>Salary Grade: </label>
                                            <asp:Label ID="lblSalaryGrade" runat="server" Text=""></asp:Label>
                                        </div>

                                    </div>

                                    <div class="col-md-2" runat="server" visible="False">
                                        <div class="form-group">

                                            <label>Division: </label>
                                            <asp:Label ID="lblDivision" runat="server" Text=""></asp:Label>

                                        </div>
                                    </div>

                                    <div class="col-md-2" runat="server" visible="False">
                                        <div class="form-group">
                                            <label>Wing: </label>
                                            <asp:Label ID="lblWing" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>




                                    <div class="form-group" runat="server" visible="False">
                                        <label>Section: </label>
                                        <asp:Label ID="lblSection" runat="server" Text=""></asp:Label>
                                    </div>



                                </div>
                                <div class="row">
                                    <div class="col-md-3" runat="server">
                                        <div class="form-group">

                                            <label>Employee Name: </label>
                                            <asp:Label ID="lblEmp" runat="server" Text=""></asp:Label>

                                        </div>
                                    </div>
                                    <div class="col-md-3" runat="server">
                                        <div class="form-group">
                                            <label>Department: </label>
                                            <asp:Label ID="lblDepartment" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-3" runat="server">

                                        <div class="form-group">
                                            <label>Joining Date: </label>
                                            <asp:Label ID="lblJdate" runat="server" Text=""></asp:Label>
                                        </div>

                                    </div>

                                </div>



                            </fieldset>

                            <div class="row">


                                <div class="col-md-6">


                                    <label style="font-size: 17px;font-weight: bold;">Benefit Information</label>
                                    <div class="form-group" style="max-height: 200px; overflow: scroll">
                                        <%--  <asp:CheckBoxList ID="CheckBoxList1" CssClass="form-control" runat="server">
                                            <asp:ListItem>Maternity Leave</asp:ListItem>
                                            <asp:ListItem>Advance Sick Leave</asp:ListItem>
                                            <asp:ListItem>Special Sick Leave</asp:ListItem>
                                            <asp:ListItem>Leave Encashment</asp:ListItem>
                                            <asp:ListItem>Car Loan</asp:ListItem>
                                            <asp:ListItem>Bike Loan</asp:ListItem>
                                            <asp:ListItem>PF / Gratuity Loan</asp:ListItem>
                                        </asp:CheckBoxList>--%>
                                        <%--<asp:GridView ID="loadGridView" runat="server">
                                            <Columns>
                                                <asp:CheckBoxField></asp:CheckBoxField>
                                                <asp:BoundField></asp:BoundField>
                                                <asp:TemplateField></asp:TemplateField>
                                                <asp:TemplateField></asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>--%>
                                        <asp:GridView ID="itemGridView" runat="server" AutoGenerateColumns="False" Height="60px"
                                            DataKeyNames="BenefitMasterId"   CssClass="AddToListCssTable" OnPreRender="gv_DocumentUpload_PreRender"   CellPadding="4" ForeColor="#333333" GridLines="None">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Select">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="isValueCheckBox" runat="server" AutoPostBack="True" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Benefit Name">
                                                    <ItemTemplate>


                                                        <asp:TextBox ID="itemTextBox" runat="server" CssClass="form-control form-control-sm" ReadOnly="True" Text='<%# Eval("Benefit")%>'></asp:TextBox>

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="rcvQtyTextBox" runat="server" class="form-control form-control-sm" Text='<%# Eval("Amount")%>' Width="100%"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderqty" runat="server"
                                                            Enabled="True" TargetControlID="rcvQtyTextBox" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                    </ItemTemplate>
                                                </asp:TemplateField>








                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>

                                                        <asp:RadioButtonList ID="RadioButtonList1" RepeatDirection="Horizontal" runat="server">
                                                            <asp:ListItem>Addition</asp:ListItem>
                                                            <asp:ListItem>Deduction</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            </Columns>
                                            <EditRowStyle BackColor="#7C6F57" />
                                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#E3EAEB" />
                                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                            <SortedAscendingHeaderStyle BackColor="#246B61" />
                                            <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                            <SortedDescendingHeaderStyle BackColor="#15524A" />
                                        </asp:GridView>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Submission Date: </label>
                                        

                                        <asp:TextBox ID="SubmissionDateTextBox" AutoCompleteType="Disabled" AutoPostBack="True" runat="server" OnTextChanged="SubmissionDateTextBox_TextChanged" CausesValidation="true"
                                            class="form-control form-control-sm"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Format="dd-MMM-yyyy" CssClass="MyCalendar" PopupPosition="TopLeft"
                                            TargetControlID="SubmissionDateTextBox" />
                                    </div>
                                </div>


                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Separation Date: </label>
                                        <span style="color: red">&nbsp;*</span>
                                        <asp:TextBox ID="JobLeftDateTextBox" AutoCompleteType="Disabled" runat="server" AutoPostBack="True" OnTextChanged="JobLeftDateTextBox_TextChanged" CausesValidation="true" class="form-control form-control-sm"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                            Format="dd-MMM-yyyy" CssClass="MyCalendar" PopupPosition="TopLeft"
                                            TargetControlID="JobLeftDateTextBox" />
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Duration Days: </label>

                                        <asp:TextBox ID="DurationDateTextBox" AutoCompleteType="Disabled" Enabled="False" runat="server" CausesValidation="true" CssClass="form-control form-control-sm"></asp:TextBox>

                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Reason: </label>
                                        <asp:TextBox ID="ReasonTextBox" Rows="2" TextMode="MultiLine" runat="server" class="form-control"></asp:TextBox>

                                    </div>
                                </div>
                                
                                 <div class="col-md-7">
                                     
                                      <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                                                    CssClass="table table-bordered  text-center thead-dark" DataKeyNames="EmpInfoId,PrevEmpReportingBodyId">
                                                                    <Columns>


                                                                        <asp:BoundField DataField="EmpName" HeaderText="Directly Supervised Employee List" />
                                                                        
                                                                            <asp:TemplateField HeaderText="Is Board Member">
                                            <ItemTemplate>
                                                                        
                                                                         <asp:CheckBox runat="server" ID="chkIsB_ReportingBody" AutoPostBack="True" OnCheckedChanged="chkIsB_ReportingBody_OnCheckedChanged"  />
                                                                          </ItemTemplate>
                                        </asp:TemplateField>

                                                                         <asp:TemplateField HeaderText="Superised selection">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlEmpInfoList" runat="server" CssClass="form-control form-control-sm SelectMe22"    Width="320px"
                                                    >
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                                                         

                                                                    </Columns>
                                                                </asp:GridView>
                                     </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">


                                        <asp:CheckBoxList ID="ClearanceFormCheckBoxList" runat="server" RepeatDirection="Horizontal">

                                            <asp:ListItem> Exit Interview </asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                            
                            <div class="form-row">
                                <div class="col-12 ">
                                    <asp:CheckBox ID="manualUpdateCheckBox" Checked="True" runat="server" /> <span>&nbsp; Manually Update to Employee Information</span>
                                </div>
                            </div>
                            
                            <br />

                            <div class="form-group">
                                <asp:Button ID="submitButton" Text="Submit" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="submitButton_Click" />
                                <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />
                                <%--<asp:Button ID="submitButton" Text="Save" CssClass="btn btn-sm btn-info" runat="server" OnClick="submitButton_Click" />--%>
                                <asp:Button ID="cancelButton" Text="Cancel" CssClass="btn btn-sm warning" Visible="False" runat="server" OnClick="cancelButton_OnClick" BackColor="#FFCC00" />
                            </div>



                        </div>


                    </div>
                </div>

                <div class="row">

                    <div class="col-md-3">
                        <div class="form-group">
                        </div>
                    </div>
                    <div class="col-md-3">
                    </div>

                    <div class="col-md-4">
                    </div>
                </div>
                </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>



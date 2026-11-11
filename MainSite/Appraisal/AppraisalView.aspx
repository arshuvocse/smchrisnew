<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="AppraisalView.aspx.cs" Inherits="Appraisal_AppraisalView" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">

    <div class="content">
        <asp:UpdatePanel runat="server" ID="upFormBody">
            <ContentTemplate>
                <div class="container-fluid">

                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;">Employee KPI Information</h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="detailsViewButton" Text="Appraisal List" OnClick="detailsViewButton_OnClick" CssClass="btn btn-sm btn-outline-secondary " runat="server" />
                        </div>

                    </div>
                    <div class="card">
                        <div class="card-body">

                            <div class="form-row">
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Employee</label>
                                        <asp:TextBox runat="server" OnTextChanged="txt_employee_OnTextChanged" Enabled="False" AutoPostBack="True" CssClass="form-control form-control-sm" ID="txt_employee"></asp:TextBox>

                                        <%--<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>--%>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                            EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                            ServiceMethod="GetEployeeAutoComp2" ServicePath="~/Training/WebService.asmx" TargetControlID="txt_employee"
                                            UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                            ShowOnlyCurrentWordInCompletionListItem="true">
                                        </cc1:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                        <asp:DropDownList ID="ddlFinancialYear" AutoPostBack="true" runat="server" class="form-control form-control-sm" readonly="true"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="col-3" runat="server" Visible="False">
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

                                <div class="col-3"  runat="server" Visible="False">
                                    <div class="form-group">
                                        <label>Wing Name :</label>
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
                            <div class="form-row" >
                                <div class="col-3"  runat="server" Visible="False">
                                    <div class="form-group">
                                        <label>Section Name :</label>
                                        <asp:Label ID="secNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="secIdHiddenField" runat="server" />
                                    </div>
                                </div>


                                <div class="col-3"  runat="server" Visible="False">
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

                                <div class="col-3"  runat="server" Visible="False">
                                    <div class="form-group">
                                        <label>Employee Type:</label>
                                        <asp:Label ID="employeeType" runat="server" class="form-control form-control-sm"></asp:Label>
                                        <asp:HiddenField ID="empTypeHiddenField" runat="server" />
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Joining Date :</label>
                                        <asp:Label ID="joiningDateLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                    </div>
                                </div>

                            </div>

                            <asp:GridView runat="server" ShowFooter="true" AutoGenerateColumns="False" Width="100%" ID="gv_AppraisalFunc" CssClass="table table-bordered text-center thead-dark gridDatatable">

                                <Columns>

                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Key Performance Indicator">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="txtKpi" CssClass="form-control  form-control-sm" TextMode="MultiLine" Text='<%#Eval("KpiInfo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Weight">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="txtWeight" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("KpiWeight") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalWeight" CssClass="orm-control  form-control-sm " runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Target">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="txtTarget" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("Target") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Dead Line">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ReadOnly="True" ID="txtDeadLine" CssClass="form-control  form-control-sm" Text='<%#Eval("Deadline") %>'></asp:TextBox>


                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                Format="dd-MMM-yyyy" CssClass="custom"
                                                TargetControlID="txtDeadLine" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Mid Year Status" runat="server" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="txtMidStatus" CssClass="form-control  form-control-sm" Text='<%#Eval("MidYearStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Mark" runat="server" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="txtMark" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("SelfMark") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalMark" CssClass="orm-control  form-control-sm " runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>









                                </Columns>
                            </asp:GridView>


                            <asp:GridView runat="server" ShowFooter="true" AutoGenerateColumns="False" Width="100%" ID="gv_AppraisalPartB" CssClass="table table-bordered text-center thead-dark gridDatatable">

                                <Columns>

                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Competencies / Skills">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="SkillInfo" CssClass="form-control  form-control-sm" TextMode="MultiLine" Text='<%#Eval("SkillInfo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Supporting Example">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="SupportingEmp" CssClass="form-control  form-control-sm" TextMode="MultiLine" Text='<%#Eval("SupportingEmp") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Weight" >
                                        <ItemTemplate>
                                            <asp:Label runat="server" AutoPostBack="True" ID="Score" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("Score") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalScore" CssClass="orm-control  form-control-sm " runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>



                                 
                                </Columns>
                            </asp:GridView>
                            <div class="row">
                                <div class="col-9">
                                    <label>Approval Status </label>
                                    &nbsp;<label style="color: #a52a2a">*</label>
                                    <asp:RadioButtonList ID="actionRadioButtonList" runat="server" RepeatDirection="Horizontal">
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                              <div class="row">
                            <div class="col-1">
                                <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" CssClass="btn btn-sm btn-info" Text="Submit"></asp:Button>
                            </div>
                        </div>
                        </div>
                      
                        <asp:HiddenField runat="server" ID="id_mastetID" />
                        <asp:HiddenField runat="server" ID="id_Empid" />


                        <%--<asp:Button runat="server" ID="btn_Review" OnClick="btn_Review_OnClick" CssClass="btn btn-sm btn-info" Text="Review"></asp:Button>--%>
                    </div>
                </div>
                </div>
            </ContentTemplate>

        </asp:UpdatePanel>
    </div>
</asp:Content>


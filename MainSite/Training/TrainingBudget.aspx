<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="TrainingBudget.aspx.cs" Inherits="Training_TrainingBudget" %>

<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <style type="text/css">
        /*AutoComplete flyout */
      
    </style>



    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>
                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;">Training Budget</h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="detailsViewButton" Text="Budget List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_Click" />
                        </div>

                    </div>
                    <div class="card">
                        <div class="card-body">
                            <div class="form-row">
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Company</label>
                                        <asp:DropDownList runat="server" ID="ddlCompany" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" CssClass="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                        <asp:DropDownList ID="ddlFinancialYear" OnSelectedIndexChanged="ddlFinancialYear_SelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Training Title</label>
                                        <asp:TextBox ID="txt_TrainingTitle" TextMode="MultiLine" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Expected Result</label>
                                        <asp:TextBox runat="server" ID="txt_ExpectedOutcome" TextMode="MultiLine" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>


                            </div>
                            <div class="form-row form-group">
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Target Participants</label>
                                        <asp:RadioButtonList runat="server" ID="chk_Perticioants" RepeatDirection="Horizontal" OnSelectedIndexChanged="chk_Perticioants_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="Department">Department </asp:ListItem>
                                            <asp:ListItem Value="Grade">Grade </asp:ListItem>
                                            <asp:ListItem Value="Employee">Employee </asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="col-md-4" runat="server" visible="false" id="div_Deprtment">
                                    <div class="form-group">
                                        <label>Select Department</label>
                                        <div style="max-height: 200px; overflow: scroll">
                                            <asp:CheckBoxList ID="chk_Department" AutoPostBack="true" OnSelectedIndexChanged="chk_Department_SelectedIndexChanged" runat="server" CssClass="form-control"></asp:CheckBoxList>
                                        </div>

                                    </div>


                                </div>
                                <div class="col-md-4" runat="server" visible="false" id="div_Grade">
                                    <div class="form-group">
                                        <label>Select Grade</label>
                                        <div style="max-height: 200px; overflow: scroll">
                                            <asp:CheckBoxList ID="chk_Grade" runat="server" AutoPostBack="true" OnSelectedIndexChanged="chk_Department_SelectedIndexChanged"  CssClass="form-control"></asp:CheckBoxList>
                                        </div>

                                    </div>
                                </div>

                                <div class="col-md-4" runat="server" visible="false" id="div_Employee">
                                    <div class="form-group">
                                        <label>Select Employee</label>

                                        <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="txt_employee"></asp:TextBox>

                                        <%--<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>--%>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                            EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                            ServiceMethod="GetEployeeAutoComp" ServicePath="~/Training/WebService.asmx" TargetControlID="txt_employee"
                                            UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                            ShowOnlyCurrentWordInCompletionListItem="true">
                                        </cc1:AutoCompleteExtender>

                                        <%-- <ajaxToolkit:AutoCompleteExtender
                                            ID="at_txt_JobCirculation"
                                            TargetControlID="txt_employee"
                                            runat="server"
                                            ServiceMethod="GetEmployeeAutoComp"
                                            ServicePath="~/Training/WebService.asmx"
                                            MinimumPrefixLength="2"
                                            CompletionInterval="10"
                                            EnableCaching="false"
                                            CompletionSetCount="1"
                                            FirstRowSelected="false">
                                        </ajaxToolkit:AutoCompleteExtender>--%>
                                    </div>
                                </div>
                            </div>

                            <div class="form-row">
                                <div class="col-2">
                                    <label>Select Quater</label>
                                    <asp:DropDownList ID="ddlQuater" AutoPostBack="true" OnSelectedIndexChanged="ddlQuater_SelectedIndexChanged" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                </div>

                                <div class="col-2">
                                    <label>Select Month</label>
                                    <asp:DropDownList ID="ddlMonth" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                </div>


                                <div class="col-2">
                                    <label>From Date</label>
                                    <asp:TextBox ID="txt_fromDate" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                        Format="dd-MMM-yyyy" CssClass="custom"
                                        TargetControlID="txt_fromDate" />
                                </div>
                                <div class="col-2">
                                    <label>To Date</label>
                                    <asp:TextBox ID="txt_toDate" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                        Format="dd-MMM-yyyy" CssClass="custom"
                                        TargetControlID="txt_toDate" />
                                </div>
                                <div class="col-2">
                                    <label>Quantity</label>
                                    <asp:TextBox ID="txt_qty" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                </div>
                                <div class="col-2">
                                    <label></label>
                                    </br>
                                     <asp:Button ID="addToList" OnClick="addToList_Click" Text="Add to List" runat="server" class="btn btn-sm btn-info"></asp:Button>
                                </div>
                            </div>
                            </br>
                            <div class="form-row">
                                <div class="col-md-12">
                                    <asp:GridView ID="gv_DptDetails" Width="100%" CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="false" runat="server">

                                        <Columns>
                                            <asp:TemplateField HeaderText="SL#">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                    <%--<asp:HiddenField runat="server" ID="hdpkd" Value='<%#Eval("BoardDetailsId") %>'/>--%>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quater">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_quaterId" Visible="False" runat="server" class="form-control form-control-sm" Text='<%#Eval("quaterId") %>'></asp:Label>
                                                    <asp:Label ID="txt_Quater" runat="server" class="form-control form-control-sm" Text='<%#Eval("quater") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Month">
                                                <ItemTemplate>
                                                     <asp:Label ID="txt_monthId" Visible="False" runat="server" class="form-control form-control-sm" Text='<%#Eval("monthId") %>'></asp:Label>
                                                    <asp:Label ID="txt_Month" runat="server" class="form-control form-control-sm" Text='<%#Eval("month") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Department">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_department" runat="server" class="form-control form-control-sm" Text='<%#Eval("department") %>'></asp:Label>
                                                    <asp:HiddenField runat="server" ID="deptId" Value='<%#Eval("departmentId") %>' />
                                                    <asp:HiddenField runat="server" ID="finYear" Value='<%#Eval("finYear") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="From Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_fromDate" runat="server" class="form-control form-control-sm" Text='<%#Eval("fromDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="To Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_toDate" runat="server" class="form-control form-control-sm" Text='<%#Eval("toDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Quantity">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_Qty" OnTextChanged="txt_Qty_TextChanged" AutoPostBack="true"  TextMode="Number" runat="server" CssClass="form-control form-control-sm text-right" Text='<%#Eval("quantity") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Operation">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lb_Remove" OnClick="lb_Remove_Click" runat="server">Remove</asp:LinkButton>
                                                </ItemTemplate>


                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </div>

                            <div class="form-row">
                                <div class="col-md-12">
                                    <asp:GridView ID="gv_GradeDetails" Width="100%" CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="false" runat="server">

                                        <Columns>
                                            <asp:TemplateField HeaderText="SL#">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                    <%--<asp:HiddenField runat="server" ID="hdpkd" Value='<%#Eval("BoardDetailsId") %>'/>--%>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Quater">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_quaterId" Visible="False" runat="server" class="form-control form-control-sm" Text='<%#Eval("quaterId") %>'></asp:Label>
                                                    <asp:Label ID="txt_Quater" runat="server" class="form-control form-control-sm" Text='<%#Eval("quater") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Month">
                                                <ItemTemplate>
                                                     <asp:Label ID="txt_monthId" Visible="False" runat="server" class="form-control form-control-sm" Text='<%#Eval("monthId") %>'></asp:Label>
                                                    <asp:Label ID="txt_Month" runat="server" class="form-control form-control-sm" Text='<%#Eval("month") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Grade">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_department" runat="server" class="form-control form-control-sm" Text='<%#Eval("grade") %>'></asp:Label>
                                                    <asp:HiddenField runat="server" ID="deptId" Value='<%#Eval("gradeId") %>' />
                                                    <asp:HiddenField runat="server" ID="finYear" Value='<%#Eval("finYear") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="From Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_fromDate" runat="server" class="form-control form-control-sm" Text='<%#Eval("fromDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="To Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_toDate" runat="server" class="form-control form-control-sm" Text='<%#Eval("toDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Quantity">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_Qty" OnTextChanged="txt_Qty_TextChanged" AutoPostBack="true"  TextMode="Number" runat="server" CssClass="form-control form-control-sm text-right" Text='<%#Eval("quantity") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Operation">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lb_RemoveGrd" OnClick="lb_RemoveGrd_Click" runat="server">Remove</asp:LinkButton>
                                                </ItemTemplate>


                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </div>


                            


                            <div class="form-row">
                                <div class="col-md-12">
                                    <asp:GridView ID="gv_EmpDetails" Width="100%" CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="false" runat="server">

                                        <Columns>
                                            <asp:TemplateField HeaderText="SL#">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Quater">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_quaterId" Visible="False" runat="server" class="form-control form-control-sm" Text='<%#Eval("quaterId") %>'></asp:Label>
                                                    <asp:Label ID="txt_Quater" runat="server" class="form-control form-control-sm" Text='<%#Eval("quater") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Month">
                                                <ItemTemplate>
                                                     <asp:Label ID="txt_monthId" Visible="False" runat="server" class="form-control form-control-sm" Text='<%#Eval("monthId") %>'></asp:Label>
                                                    <asp:Label ID="txt_Month" runat="server" class="form-control form-control-sm" Text='<%#Eval("month") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_employee" runat="server" class="form-control form-control-sm" Text='<%#Eval("employee") %>'></asp:Label>

                                                    <asp:HiddenField runat="server" ID="deptId" Value='<%#Eval("employeeId") %>' />
                                                    <asp:HiddenField runat="server" ID="finYear" Value='<%#Eval("finYear") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="From Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_fromDate" runat="server" class="form-control form-control-sm" Text='<%#Eval("fromDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="To Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_toDate" runat="server" class="form-control form-control-sm" Text='<%#Eval("toDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Operation">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lb_RemoveEmp" OnClick="lb_RemoveEmp_Click" runat="server">Remove</asp:LinkButton>
                                                </ItemTemplate>


                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </div>

                            <div class="form-row">
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Total Participant</label>
                                        <asp:TextBox ID="txt_totalQty" TextMode="Number"  runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Duration (HH.mm)</label>
                                        <asp:TextBox runat="server" ID="txt_Duration" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Select</label>
                                        <asp:RadioButtonList runat="server" ID="radExIn" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="External">External </asp:ListItem>
                                            <asp:ListItem Value="Internal">Internal </asp:ListItem>

                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Select</label>
                                        <asp:RadioButtonList runat="server" ID="rad_fLocal" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="Foeign">Foeign </asp:ListItem>
                                            <asp:ListItem Value="Local">Local </asp:ListItem>

                                        </asp:RadioButtonList>
                                    </div>
                                </div>

                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Cost/Participent</label>
                                        <asp:TextBox runat="server" ID="txt_CostPer" OnTextChanged="txt_CostPer_TextChanged" AutoPostBack="true" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Budget</label>
                                        <asp:TextBox runat="server" ID="txt_budget" ReadOnly="true" TextMode="Number" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>



                            </div>
                            <div class="form-row">
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Referance</label>
                                        <asp:TextBox runat="server" ID="txt_ref" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Remarks</label>
                                        <asp:TextBox runat="server" ID="txt_remarks" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <br />
                                        
                                    </div>
                                </div>
                            </div>
                            <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_Click" CssClass="btn btn-sm btn-info" Text="Submit"></asp:Button>
                            
                                     <asp:Button ID="cancelButton" Text="Cancel" OnClick="cancelButton_Click" CssClass="btn btn-sm warning" runat="server"  BackColor="#FFCC00" />
                        </div>

                    </div>

                </div>
                <asp:HiddenField runat="server" ID="hdpk" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>


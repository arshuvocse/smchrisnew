<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="TrainingRequisition.aspx.cs" Inherits="Training_TrainingRequisition" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
     <style type="text/css">
        /*AutoComplete flyout */
      
    </style>

    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>
                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;">Training Requisition</h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="detailsViewButton" Text="Requisition List" CssClass="btn btn-sm btn-outline-secondary " OnClick="detailsViewButton_Click" runat="server" />
                        </div>

                    </div>
                    <div class="card">
                        <div class="card-body">
                            <div class="form-row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Company</label>
                                        <asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" class="form-control form-control-sm" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                        <asp:DropDownList ID="ddlFinancialYear" AutoPostBack="true" OnSelectedIndexChanged="ddlFinancialYear_SelectedIndexChanged" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Quater</label>
                                      
                                        <asp:DropDownList ID="ddlQuater" AutoPostBack="true"  CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                    </div>

                                </div>
                                <div class="col-md-3">

                                      <div class="form-group">
                                        <label>Requisition By</label>
                                        

                                        <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="txt_Reqemployee"></asp:TextBox>

                                        <%--<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>--%>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                            EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                            ServiceMethod="GetEployeeAutoComp" ServicePath="~/Training/WebService.asmx" TargetControlID="txt_Reqemployee"
                                            UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                            ShowOnlyCurrentWordInCompletionListItem="true">
                                        </cc1:AutoCompleteExtender>
                                        
                                    </div>
                                </div>





                            </div>
                            <div class="form-row">
                                <div class="col-4">
                                    <div class="form-group">
                                        <label> Training Topic</label>
                                        <asp:TextBox  runat="server" ID="txt_trainingTopic" TextMode="MultiLine" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-3">
                                    <div class="form-group">
                                        <label> Target Employee</label>
                                        <asp:TextBox  runat="server" ID="txt_employee"  CssClass="form-control form-control-sm"></asp:TextBox>
                                         <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters=""
                                            EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                            ServiceMethod="GetEployeeAutoComp" ServicePath="~/Training/WebService.asmx" TargetControlID="txt_employee"
                                            UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                            ShowOnlyCurrentWordInCompletionListItem="true">
                                        </cc1:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-1">
                                    <asp:Button  runat="server" CssClass="btn btn-success btn-xs" Text="Add To List"  ID="addEmployeeToList" OnClick="addEmployeeToList_Click"/>
                                </div>
                            </div>

                            <asp:GridView ID="gv_EmpDetails" Width="100%" CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="false" runat="server">

                                        <Columns>
                                            <asp:TemplateField HeaderText="SL#">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quater">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_Quater" runat="server" class="form-control form-control-sm" Text='<%#Eval("quater") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="Employee">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_employee" runat="server" class="form-control form-control-sm" Text='<%#Eval("employee") %>'></asp:Label>

                                                    <asp:HiddenField runat="server" ID="deptId" Value='<%#Eval("employeeId") %>' />
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                          


                                            <asp:TemplateField HeaderText="Operation">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lb_RemoveEmp" OnClick="lb_RemoveEmp_Click" runat="server">Remove</asp:LinkButton>
                                                </ItemTemplate>


                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                            <div>
                                <asp:HiddenField runat="server" ID="hdpk" />
                                <asp:Button runat="server" ID="btn_Save" Text="Submit " OnClick="btn_Save_Click" CssClass="btn btn-sm btn-info" />
                                <asp:Button ID="cancelButton" Text="Cancel" OnClick="cancelButton_Click" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="TrainingRequisition2.aspx.cs" Inherits="Training_TrainingRequisition2" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
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
                            <asp:Button ID="detailsViewButton" Text="Requisition List" CssClass="btn btn-sm btn-outline-secondary "  runat="server" />
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
                                      
                                        <asp:DropDownList ID="ddlQuater" AutoPostBack="true" OnSelectedIndexChanged="ddlQuater_OnSelectedIndexChanged"  CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                    </div>

                                </div>
                                 <div class="col-3">
                                    <label>Select Month</label>
                                    <asp:DropDownList ID="ddlMonth" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                </div>
                                <div class="col-md-3">

                                      <div class="form-group">
                                        <label>Requisition By</label>
                                        

                                        <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="txt_Reqemployee"></asp:TextBox>

                                        <%--<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>--%>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                            EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                            ServiceMethod="GetEployeeAutoComp" ServicePath="~/WebService.asmx" TargetControlID="txt_Reqemployee"
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
                                 <div class="col-4">
                                    <div class="form-group">
                                        <label> Exected Result</label>
                                        <asp:TextBox  runat="server" ID="txt_result" TextMode="MultiLine" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-4">
                                    <div class="form-group">
                                       <asp:Button runat="server" ID="addList" OnClick="addList_OnClick" Text="Add" CssClass="btn btn-sm btn-info"/>
                                    </div>
                                </div>
                           
                               
                            </div>

                            <asp:GridView ID="gv_training" Width="100%" CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="false" runat="server">

                                        <Columns>
                                            <asp:TemplateField HeaderText="SL#">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Training Title">
                                                <ItemTemplate>
                                                    <asp:Label ID="TrainingTitle" runat="server" class="form-control form-control-sm" Text='<%#Eval("TrainingTitle") %>'></asp:Label>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Expected Result">
                                                <ItemTemplate>
                                                    <asp:Label ID="ExpectedResult" runat="server" class="form-control form-control-sm" Text='<%#Eval("ExpectedResult") %>'></asp:Label>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Quater">
                                                <ItemTemplate>
                                                    <asp:Label ID="Quater"  runat="server" class="form-control form-control-sm" Text='<%#Eval("Quater") %>'></asp:Label>
                                                    <asp:Label ID="QuaterId" Visible="False" runat="server" class="form-control form-control-sm" Text='<%#Eval("QuaterId") %>'></asp:Label>
                                                  
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Month">
                                                <ItemTemplate>
                                                     <asp:Label ID="Month"  runat="server" class="form-control form-control-sm" Text='<%#Eval("Month") %>'></asp:Label>
                                                    <asp:Label ID="MonthId" Visible="False" runat="server" class="form-control form-control-sm" Text='<%#Eval("MonthId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           
                                         
                                          


                                            <asp:TemplateField HeaderText="Operation">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lb_RemoveEmp" OnClick="lb_RemoveEmp_OnClick" runat="server">Remove</asp:LinkButton>
                                                </ItemTemplate>


                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                            <div>
                                <asp:HiddenField runat="server" ID="hdpk" />
                                <asp:Button runat="server" ID="btn_Save" Text="Submit " OnClick="btn_Save_OnClick" CssClass="btn btn-sm btn-info" />
                                <asp:Button ID="cancelButton" Text="Cancel" OnClick="cancelButton_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>


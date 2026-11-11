<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" EnableViewState="true" MaintainScrollPositionOnPostback="true"  AutoEventWireup="true" CodeFile="JobRequisitionFormAproval.aspx.cs" Inherits="MasterSetup_UI_JobRequisitionFormView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                         
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;"> <img src="../Report_Pages/app.png" width="20px"  /> Job Requisition Approval </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                          <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="HomeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                           
                   
                        <%--<asp:Button ID="addNewButton" Text="Add New Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="addNewButton_OnClick" />--%>
                        <%--<asp:Button ID="reloadButton" Text="Reload" CssClass="btn btn-sm btn-outline-success" runat="server" OnClick="reloadButton_OnClick" />--%>
                    </div>

                </div>
                
    
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">

                            <%-- <div class="row">
                                 <div class="col-md-4">
                                    </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Approval Status </label>
                                                &nbsp;<label style="color: #a52a2a">*</label>
                                                <asp:RadioButtonList ID="jobreqRadioButtonList" runat="server" Visible="False">
                                                    
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>

                                
                                 </div>
                             <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Button ID="Button2" Text="Save" Visible="False" CssClass="btn btn-info btn-sm" runat="server" OnClick="Button2_OnClick" />
                                            </div>
                                        </div>--%>
                            <div id="gridContainer1" style="height: 430px; overflow: auto; width: auto">
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                     CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender" DataKeyNames="JobReqId,JobReqFormAppLogId"
                                    OnRowCommand="loadGridView_RowCommand" Font-Size="11px">
                                    <Columns>

                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--<asp:BoundField DataField="ReqCode" HeaderText="Requisition Code" />--%>
                                        <asp:BoundField DataField="CompanyName" HeaderText="Company" />
                                        <asp:BoundField DataField="FinancialYearDesc" HeaderText="Financial Year" />
                                        <asp:BoundField DataField="JobTitle" HeaderText="Designation" />
                                        <asp:BoundField DataField="ReqDate" HeaderText="Requisition Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="Nos" HeaderText="Total Vacency" />
                                        <%--<asp:TemplateField >
                                            <HeaderTemplate>
                                                <asp:CheckBox runat="server" AutoPostBack="True" ID="chkAll" OnCheckedChanged="chkAll_OnCheckedChanged"/>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chkSingle" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <%--              <asp:BoundField DataField="GradeName" HeaderText="Grade Name" />--%>

                                        <asp:TemplateField HeaderText="Go To Approval">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="editImageButton" CssClass="btn  btn-sm btnMyDesignSearch" runat="server"
                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="EditData"
                                                    >Go to Approve &nbsp;>></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                       <%-- <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="115px">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="deleteImageButton" runat="server"
                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="DeleteData"
                                                    ImageUrl="~/Assets/img/delete.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                    </Columns>
                                </asp:GridView>
                            </div>
                            
                            
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>


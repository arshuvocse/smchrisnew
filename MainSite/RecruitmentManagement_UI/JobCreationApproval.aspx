<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="JobCreationApproval.aspx.cs" Inherits="RecruitmentManagement_UI_JobCreationView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                    <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                        <div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>
                        <span></span>
                        <h1 class="title" style="font-size: 16px; padding-top: 1px;">Job Creation Approval </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                <asp:Button ID="AddNewButton" Text="Add New" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="AddNewButton_OnClick"  />
                <%--<asp:Button ID="reloadButton" Text="Reload" CssClass="btn btn-sm btn-outline-success" runat="server" OnClick="reloadButton_OnClick" />--%>
            </div>

                </div>
             

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-4">
                                    </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Approval Status </label>
                                                &nbsp;<label style="color: #a52a2a">*</label>
                                                <asp:RadioButtonList ID="jobreqRadioButtonList" runat="server">
                                                    <asp:ListItem>Approved</asp:ListItem>
                                                    <asp:ListItem>Verified</asp:ListItem>
                                                    <asp:ListItem>Reviewed</asp:ListItem>
                                                    <asp:ListItem>Canceled</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                 <div class="col-md-4">
                                            <div class="form-group">
                                               
                                            </div>
                                        </div>
                                 </div>
                             <div class="row">
                                
                                 <div class="col-md-12">
                            <div id="gridContainer1" style="height: auto; overflow: auto; width: auto;">
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered  text-center thead-dark" DataKeyNames="JobID"
                                    OnRowCommand="loadGridView_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    <asp:BoundField DataField="JobCode" HeaderText="Job No" />
                                    <asp:BoundField DataField="CompanyName" HeaderText="Company" />
                                    <asp:BoundField DataField="ReqCode" HeaderText="Requisition Code" />
                                    <asp:BoundField DataField="JobContext" HeaderText="Job Context" />     
                                    <asp:BoundField DataField="CompensationandOtherBenefits" HeaderText="Other Benifits" />
                                    <asp:BoundField DataField="CirculationStartDate" DataFormatString="{0:dd-MMM-yyyy}" 
                                        HeaderText="Circulation Date" />
                                  
                                    <asp:BoundField DataField="EntryBy" HeaderText="Entry By" />
                                    <asp:BoundField DataField="EntryDate" DataFormatString="{0:dd-MMM-yyyy}" 
                                        HeaderText="Entry Date" />
                                        
                                    <asp:BoundField DataField="Updateby" HeaderText="Update by" />
                                    <asp:BoundField DataField="UpdateDate" DataFormatString="{0:dd-MMM-yyyy}" 
                                        HeaderText="Update Date" />
                                         <asp:TemplateField >
                                            <HeaderTemplate>
                                                <asp:CheckBox runat="server" AutoPostBack="True" ID="chkAll" OnCheckedChanged="chkAll_OnCheckedChanged"/>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chkSingle" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       <%-- <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="editImageButton" runat="server"
                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="EditData"
                                                    ImageUrl="~/Assets/img/rsz_edit.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                             <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="115px">
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
                             <div class="row">
                                
                                 <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:Button ID="Button1" Text="Submit" CssClass="btn btn-info btn-sm" runat="server" OnClick="Button2_OnClick" />
                                            </div>
                                        </div>
                                 </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="SuspendReleaseList.aspx.cs" Inherits="SuspendAndDiciplinary_UI_SuspendReleaseList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    
    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                        <%--<div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>--%>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Suspend Release List </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                       <%-- <asp:Button ID="addNewButton" Text="Add New Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="addNewButton_OnClick" />
                        <asp:Button ID="reloadButton" Text="Refresh" CssClass="btn btn-sm btn-outline-success" runat="server" OnClick="reloadButton_OnClick" />--%>
                    </div>
                
                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">
                            
                             <div class="row" runat="server" Visible="False">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label></label>
                                        <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True" RepeatDirection="Horizontal" Enabled="False">
                                        </asp:CheckBoxList>


                                    </div>
                                </div>
                            </div>

                            <div id="gridContainer1" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: hidden;">
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered text-center thead-dark" DataKeyNames="SuspendId"
                                    OnRowCommand="loadGridView_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee Code" />
                                        <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                        <%--<asp:BoundField DataField="Designation" HeaderText="Designation Name" />
                                        <asp:BoundField DataField="DepartmentName" HeaderText="Dept Name" />--%>
                                        <asp:BoundField DataField="EffectiveDate" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Effective Date" />
                                        <%--<asp:BoundField DataField="ActionStatus" HeaderText="Status" />--%>
                                        
                                        <asp:BoundField DataField="Description" HeaderText="Description" />
                                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                        
                                        <asp:BoundField DataField="SuspendReasonEntry" HeaderText="Action Taken" />

                                          <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDownload" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="cmd"> Suspend Release  &gt; &gt; &gt; &gt; </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
          
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


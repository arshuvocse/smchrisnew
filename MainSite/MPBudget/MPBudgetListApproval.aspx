<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MainMasterPage.master" CodeFile="MPBudgetListApproval.aspx.cs" Inherits="MPBudget_MPBudgetList" %>


<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">


        <div class="container-fluid">
            <div class="page-heading">
                <div class="page-heading__container">
                    <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png" width="18px" /> Manpower Budget Approval</h1>
                </div>
                <div class="page-heading__container float-right d-none d-sm-block">
                  <%--  <asp:Button ID="btn_New" Text="Create New" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="btn_New_OnClick" />--%>
                      <asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="vcchomeButton_OnClick" />

                </div>
            </div>
            <div class="card">
                <div class="card-body">
                 
                    <div class="form-row" runat="server" Visible="False">
                         
                                       <div class="col-md-3">
                                        <div class="form-group">
                                            <label> </label>
                                             <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True"  RepeatDirection="Horizontal" Enabled="False" >
                                        </asp:CheckBoxList>
                                            

                                        </div>
                                    </div>
                                
                        <div class="col-3">
                            <div class="form-group">
                                <label>Company</label>
                                <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" class="form-control form-control-sm" />
                            </div>
                        </div>
                        <div class="col-3">
                            <div class="form-group">
                                <label>Department</label>
                                <asp:DropDownList runat="server" ID="ddlDepartment" class="form-control form-control-sm" />
                            </div>
                        </div>
                        <div class="col-3">
                            <div class="form-group">
                                <label>Financial Year</label>
                                <asp:DropDownList runat="server" ID="ddlFinYear" class="form-control form-control-sm" />
                            </div>
                        </div>
                        <div class="col-2 ">
                            <div class="form-group">
                                <br />
                                <asp:Button ID="btnFilterSearch" Text="Search" OnClick="btnFilterSearch_OnClick" CssClass="btn btn-sm activity-success" runat="server" BackColor="#FFCC00" />
                                <%--<input type="button" class="btn btn-sm activity-success" style="background-color: #FFCC00;" id="btnFilterSearch" value="Filter"></input>--%>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div id="gridContainer1" style="height: auto; overflow: auto; width: auto;">
                        <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                            CssClass="table table-bordered  text-center thead-dark" DataKeyNames="MPBudgetMasterId,MPBudgetMasterAppLogId"
                            OnRowCommand="loadGridView_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="SL">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="ShortName" HeaderText="Company" />
                                    <asp:BoundField  DataField="BudgetCode" HeaderText="Budget Code" />
                                <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                <asp:BoundField DataField="FinancialYearDesc" HeaderText="Financial Year" />
                            
                                <%--<asp:BoundField DataField="Designation" HeaderText="Designation" />--%>

                                <asp:TemplateField HeaderText="Approval">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="editImageButton" runat="server"
                                            CommandArgument="<%# Container.DataItemIndex %>" CommandName="EditData"
                                            ImageUrl="~/Assets/2076-512.png" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--<asp:TemplateField HeaderText="Delete" HeaderStyle-Width="115px">
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
    </div>
    <%--<script src="../Assets/MaterialDT/jquery-3.3.1.js"></script>--%>
    <%-- <script src="../Assets/MaterialDT/jquery.dataTables.min.js"></script>
    <script src="../Assets/MaterialDT/dataTables.material.min.js"></script>
    <script type="text/javascript">
        $.getScript('../AppJs/MPBudgetList.js', function (data, textStatus, jqxhr) {
            console.log('Script File Load Status=' + jqxhr.status);
        });
    </script>--%>
</asp:Content>


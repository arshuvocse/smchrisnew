<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MainMasterPage.master" CodeFile="ProbationListApproval.aspx.cs" Inherits="Survey_ProbationList" %>


<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>
                <!-- PAGE HEADING -->
                <div class="page-heading" style="background-color: #F0FFFF;font-style: italic">
                    <div class="page-heading__container">
                        <div class="icon"></div>
                        <span></span>
                        <h1 class="title" style="font-size: 16px; padding-top: 1px;">Employee Probation Period Approval List </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block" runat="server" >
                         <asp:Button ID="homeButton"  Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="vcchomeButton_OnClick" />
                        <asp:Button ID="btn_New" Text="Create New" CssClass="btn btn-sm btn-outline-secondary " Visible="False" runat="server" OnClick="btn_New_OnClick" />
                        <%--<asp:Button ID="reloadButton" Text="Reload" CssClass="btn btn-sm btn-outline-success" runat="server" OnClick="reloadButton_OnClick" />--%>
                    </div>
                </div>

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">
                            <div class="row" runat="server" visible="false">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label></label>
                                        <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True" RepeatDirection="Horizontal" Enabled="False">
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>

                            <div class="form-row" runat="server" Visible="False">
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Company</label>
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" class="form-control form-control-sm" />
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Department</label>
                                        <asp:DropDownList runat="server" ID="ddlDepartment" class="form-control form-control-sm" />
                                    </div>
                                </div>
                            <div class="col-2">
                                <div class="form-group">
                                    <label class="control-label"> From Date</label>
                                    <asp:TextBox runat="server" ID="startDate" class="form-control form-control-sm" AutoPostBack="True" ></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                                  Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                  TargetControlID="startDate" />
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="form-group">
                                    <label class="control-label"> To Date</label>
                                    <asp:TextBox runat="server" ID="endDate" class="form-control form-control-sm" AutoPostBack="True" ></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                                  Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                  TargetControlID="endDate" />
                                </div>
                            </div>
                                <div class="col-1 ">
                                    <div class="form-group" style="margin-top: 16px;">
                                        <asp:Button ID="btnFilterSearch" Text="Search" OnClick="btnFilterSearch_OnClick" CssClass="btn btn-outline-info btn-block disabled btn-sm" runat="server" />
                                    </div>
                                </div>
                            </div>

                            <%--<br/><br/><br/><br/><br/>
                            
                            <br/><br/><br/><br/><br/>
                            <br/><br/><br/><br/><br/>--%>
                            <div id="gridContainer1" style="height: auto; overflow: auto; width: auto;">
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered text-center thead-dark" DataKeyNames="EmpInfoId,ProbationEvaluationMasterId,ProbationEvaluationAppLogId"
                                    OnRowCommand="loadGridView_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                       <%-- <asp:BoundField DataField="ShortName" HeaderText="Company" />--%>
                                         <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee Code" />
                              <asp:BoundField DataField="ProbationEvaluationMasterId" HeaderText="Employee Code" Visible="False"/>
          <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                         <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                        <asp:BoundField DataField="DepartmentName" HeaderText="Department Name" />
                                       
                                        <asp:BoundField DataField="ProbationEndDate" HeaderText="Probation End Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="DateOfJoin" HeaderText="Joining Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="Request" HeaderText="Request for"  />
                                              <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDownload" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="Evaluate">Go for Approve  &gt; &gt; </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                               <%--         <asp:BoundField DataField="FinancialYearDesc" HeaderText="Financial Year" />--%>
                                        <%--<asp:BoundField DataField="Designation" Visible="False" HeaderText="Budget Code" />
                                        <asp:BoundField DataField="Designation" HeaderText="Designation" />--%>

                                      <%--  <asp:TemplateField HeaderText="Edit">
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
                                            <HeaderStyle Width="115px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="viewImageButton" runat="server"
                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="ViewData"
                                                    ImageUrl="~/Assets/img/list-view.png" />
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


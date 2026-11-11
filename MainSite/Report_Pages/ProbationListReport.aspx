<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MainMasterPage.master" EnableEventValidation="false"   CodeFile="ProbationListReport.aspx.cs" Inherits="Report_Pages_ProbationListReport" %>


<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">
        <%--<asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>--%>
                <!-- PAGE HEADING -->
                <div class="page-heading" style="background-color: #F0FFFF;font-style: italic">
                    <div class="page-heading__container">
                        <div class="icon"></div>
                        <span></span>
                        <h1 class="title" style="font-size: 16px; padding-top: 1px;">Employee Probation Period Report Information </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                         <asp:Button ID="homeButton" Visible="False" Text="Home" CssClass="btn btn-sm btn-outline-secondary "  runat="server" OnClick="vcchomeButton_OnClick" />
                        <asp:Button ID="btn_New" Text="Create New" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="btn_New_OnClick" Visible="False" />
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

                            <div class="form-row">
                                
                                    <div class="col-2">
                                    <div class="form-group">
                                        <label>Company</label>
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" class="form-control form-control-sm" />
                                    </div>
                                </div>
                                <div class="col-2" runat="server" Visible="False">
                                    <div class="form-group">
                                        <label>Department</label>
                                        <asp:DropDownList runat="server" ID="ddlDepartment" class="form-control form-control-sm" />
                                    </div>
                                </div>
                            <div class="col-2" Visible="False" runat="server">
                                <div class="form-group">
                                    <label class="control-label"> From Date</label>
                                    <asp:TextBox runat="server" ID="startDate" class="form-control form-control-sm" AutoPostBack="True" ></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                                  Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                  TargetControlID="startDate" />
                                </div>
                            </div>
                            <div class="col-2" Visible="False" runat="server">
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
                               
                                   <div class="col-4 pull-right" <%-- style="margin-right: 100px;" --%>>
                                    <div class="form-group" style="margin-top: 10px;">
                                        <asp:Button ID="btnExport" Visible="False" Text="Export to Excel" OnClick="btnExport_OnClick" CssClass="btnexcel" runat="server" />
                                    </div>
                                       
                                         <style>
                                    .btnexcel {
                                        background-color: #4CAF50;
                                        border: none;
                                        color: white;
                                        padding: 10px 16px;
                                        text-align: center;
                                        text-decoration: none;
                                        display: inline-block;
                                        font-size: 12px;
                                        margin: 4px 2px;
                                        cursor: pointer;
                                    }
                                </style>
                                </div>
                               
                            </div>

                            <%--<br/><br/><br/><br/><br/>
                            
                            <br/><br/><br/><br/><br/>
                            <br/><br/><br/><br/><br/>--%>
                            <div id="gridContainer1" style="height: auto; overflow: auto; width: auto;">
                               <asp:UpdatePanel ID="UpdatePanel1"  runat="server">

                                    <ContentTemplate>
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered  text-center thead-light" DataKeyNames="ProbationEvaluationMasterId"
                                    OnRowCommand="loadGridView_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                
                                                  <asp:HiddenField ID="ProbationEvaluationMasterId" runat="server" Value='<%#Eval("ProbationEvaluationMasterId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Print">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" OnClick="btn_edit_OnClick" ID="btn_edit">
                                               <img src="../Assets/report_magnify.png" />
                                            </asp:LinkButton>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:BoundField DataField="ShortName" HeaderText="Company" />
                                         <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee ID" />
                                        <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                        <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                        <asp:BoundField DataField="ProbationEndDate" HeaderText="Probation End Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="DateOfJoin" HeaderText="Joining Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="ActionStatus2" HeaderText="Approval Status" />
                                        
                                        <asp:BoundField DataField="AwEmpName" HeaderText="Awaiting Employee" />
                                           <%--   <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDownload" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="Evaluate"> Evaluate  &gt; &gt; &gt; &gt; </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
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
                                         </ContentTemplate>

                                </asp:UpdatePanel>
                                    
                            </div>
                        </div>
                    </div>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </div>
         <%--   </ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="DeadlineApproval.aspx.cs" Inherits="Appraisal_DeadlineApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">

    <div class="content">
        <asp:UpdatePanel runat="server" ID="upFormBody">
            <ContentTemplate>
                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png" width="20px"  />  Deadline Extension Approval</h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block" runat="server" visible="false">
                            <asp:Button ID="detailsViewButton" runat="server" Visible="false" Text="Add New" OnClick="detailsViewButton_OnClick" CssClass="btn btn-sm btn-outline-secondary" />
                        </div>

                    </div>

                    <div class="card">
                        <div class="card-body">
                            <div class="row" runat="server" visible="False">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label></label>
                                        <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True" RepeatDirection="Horizontal" Enabled="False">
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                             <div class="form-row">
                                 
                                   <div class="col-md-3">
                                       </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Company</label>
                                        <asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" class="form-control form-control-sm" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                </div>
                                 
                                   <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                        <asp:DropDownList ID="ddlFinancialYear" AutoPostBack="true" OnSelectedIndexChanged="ddlFinancialYear_OnSelectedIndexChanged" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                    <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Operation</label>
                                        <asp:DropDownList ID="OperationDropDownList" runat="server"  class="form-control form-control-sm" AutoPostBack="True">
                                             <asp:ListItem>Select One........</asp:ListItem>
                                              <asp:ListItem Value="BSC/OKR">BSC/OKR</asp:ListItem>
                                                <asp:ListItem Value="Apprisal">Apprisal</asp:ListItem>
                                            <asp:ListItem Value="BSC/OKRApprisal">BSC/OKR  Apprisal</asp:ListItem>
                                                <asp:ListItem Value="KPI">KPI</asp:ListItem>

                                        </asp:DropDownList>
                                    </div>
                                </div>
                                 
                                   <div class="col-2" runat="server" Visible="False">
                                    <div class="form-group">
                                        <label>Department</label>
                                        <asp:DropDownList runat="server" ID="ddlDept" CssClass="form-control form-control-sm" />
                                    </div>
                                </div>
                                 
                               
                                 
                                 </div>

                            
                                <div class="form-row">
                                 
                                   <div class="col-md-5">
                                       </div>

      <div class="col-2">
                                    <div class="form-group" >

                                        <asp:LinkButton ID="SearchButton" Text="Search"   runat="server" OnClick="SearchButton_OnClick"  ToolTip="Click To Search"    class="btn btn-info btn-sm"    ><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp;Search</asp:LinkButton>



    <asp:LinkButton runat="server" ID="appraisalResetButton" OnClick="appraisalResetButton_OnClick" CssClass="btn btn-warning   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset </asp:LinkButton>                                    </div>
                                </div>
</div>



                            <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gv_JdBoard" CssClass="table table-bordered text-center thead-dark gridDatatable" DataKeyNames="DeadlineExtensionRequestDetailsId,EmpInfoId,CompanyId,FinYearId,Operation">
                                <Columns>
                                    
                                  <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="txt_checkAll" OnCheckedChanged="txt_checkAll_OnCheckedChanged" AutoPostBack="True" runat="server"></asp:CheckBox>
                                                <asp:Label ID="txt_selectAll" runat="server"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>

                                                <asp:CheckBox ID="txt_check" runat="server"></asp:CheckBox>
                                            </ItemTemplate>


                                        </asp:TemplateField>
                                     
                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            <asp:HiddenField ID="EmpInfoId" runat="server" Value='<%#Eval("EmpInfoId") %>' />
                                            <asp:HiddenField ID="hfFinancialYearDesc" runat="server" Value='<%#Eval("FinancialYearDesc") %>' />
                                            <asp:HiddenField ID="FinancialYearId" runat="server" Value='<%#Eval("FinancialYearId") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Financial Year"  >
                                        <ItemTemplate>
                                            <asp:Label ID="FinancialYearDesc" runat="server"   Text='<%#Eval("FinancialYearDesc") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Operation"  >
                                        <ItemTemplate>
                                            <asp:Label ID="Operation" runat="server"   Text='<%#Eval("Operation") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Employee Info"  >
                                        <ItemTemplate>
                                            <asp:Label ID="employee" runat="server"   Text='<%#Eval("employee") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Department">
                                        <ItemTemplate>
                                            <asp:Label ID="DepartmentName" runat="server"   Text='<%#Eval("DepartmentName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                      <asp:TemplateField HeaderText="Extension Date">
                                        <ItemTemplate>
                                            <asp:Label ID="ExtensionDate" runat="server"    Text='<%#Eval("ExtensionDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                      <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="Description" runat="server"   Text='<%#Eval("Description") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:Label ID="Remarks" runat="server"  Text='<%#Eval("Remarks") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approve Extended Date">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txt_appextdate" runat="server" class="form-control form-control-sm" ></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                TargetControlID="txt_appextdate" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--  <asp:TemplateField HeaderText="Approval">
                                        <ItemTemplate>
                                            <asp:Label ID="Approval" runat="server" class="form-control form-control-sm" Text='<%#Eval("ActionStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                    <%--  <asp:TemplateField HeaderText="Company">
                                        <ItemTemplate>
                                            <asp:Label ID="CompanyName" runat="server" class="form-control form-control-sm" Text='<%#Eval("CompanyName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>


                                    <asp:TemplateField HeaderText="Set KPI" Visible="False">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="btn_edit" OnClick="btn_edit_OnClick" Text="Set KPI"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--   <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                      <asp:LinkButton runat="server" ID="btn_Remove" Text="Remove"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderText="View" Visible="False">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="btn_view" OnClick="btn_eview_OnClick" Text="View"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            
                            
                            <div class="row">
                              
                                <div class="col-md-5">
                                      <div class="form-group">
                                          
                                             <link href="../UserSetup/ButtonGrups.css" rel="stylesheet" />
                                      <div class="ui-group-buttons">
                                   <asp:LinkButton runat="server" CssClass="btn btn-sm btn-info"  ID="btn_Save" OnClick="btn_Save_OnClick"  OnClientClick="return confirm('Are you sure you want to Submit ?')" style="box-shadow: 0 0 3px 1px rgba(0,0,0,.35);"> <i class="fa fa-check" aria-hidden="true"></i> &nbsp; Approve</asp:LinkButton>
                                            <div class="or or-sm" runat="server"   id="orBTN"></div>
                                <asp:LinkButton runat="server" ID="Button1" OnClick="Button1_OnClick"   OnClientClick="return confirm('Are you sure you want to Reject ?')"  CssClass="btn btn-sm btn-danger" ><i class="fa fa-times" aria-hidden="true"></i>
&nbsp; Reject </asp:LinkButton> 
                         </div>
                                    </div>
                                </div>

                                       </div>
                            <div class="form-row">
                                <div class="form-group"></div>
                                   
                            </div>
                            <div class="form-row">
                                <div class="form-group"></div>
                                   
                            </div>
                            <div class="form-row">
                                <div class="form-group"></div>
                                   
                            </div>
                            <div class="form-row">
                                <div class="form-group"></div>
                                   
                            </div>
                            <div class="form-row">
                                <div class="form-group"></div>
                                   
                            </div>
                            <div class="form-row">
                                <div class="form-group"></div>
                                   
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>



<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="AppraisalFunctional.aspx.cs" Inherits="Appraisal_AppraisalFunctional" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">

    <div class="content">

        <asp:UpdatePanel runat="server" ID="upFormBody">
            <ContentTemplate>
                <div class="container-fluid">

                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;">Appraisal Functional Part</h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="detailsViewButton" OnClick="detailsViewButton_OnClick" Text="Appraisal List" CssClass="btn btn-sm btn-outline-secondary " runat="server" />
                        </div>

                    </div>
                    <div class="card">
                        <div class="card-body">

                            <div class="form-row" runat="server" Visible="False">
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                        <asp:DropDownList ID="ddlFinancialYear" AutoPostBack="true" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                             <div class="form-row">
                                <div class="col-2">
                                    <div class="form-group">
                                         <label>Employee Name :</label>
                                        <asp:Label ID="empName" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-2" runat="server" visible="false">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                        <asp:DropDownList ID="DropDownList1" AutoPostBack="true" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-row">
                               

                                <div class="col-2" runat="server" visible="false">
                                    <div class="form-group">
                                        <label>Division Name :</label>
                                        <asp:Label ID="divisionNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="divitionIdHiddenField" runat="server" />
                                    </div>
                                </div>

                                
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Designation Name :</label>
                                        <asp:Label ID="desigNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="desigIdHiddenField" runat="server" />
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Department Name :</label>
                                        <asp:Label ID="deptNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="deptIdHiddenField" runat="server" />
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Joining Date :</label>
                                        <asp:Label ID="joiningDateLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                    </div>
                                </div>
                                
                                    <div class="col-2">
                                    <div class="form-group">
                                        <label>Job Location :</label>
                                        <asp:Label ID="LocationLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                    </div>
                                </div>
                                
                                    <div class="col-2">
                                    <div class="form-group">
                                        <label>Supervisor :</label>
                                        <asp:Label ID="ReportingLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="col-3">
                                    <div class="form-group" runat="server" Visible="False">
                                        <label>Wing Name :</label>
                                        <asp:Label ID="divWingNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="divWingIdHiddenField" runat="server" />
                                    </div>
                                </div>
                                 <div class="col-3" runat="server" Visible="False">
                                    <div class="form-group">
                                        <label>Company Name :</label>
                                        <asp:Label ID="comNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="comIdHiddenField" runat="server" />
                                    </div>
                                </div>
                                <div class="col-3" runat="server" Visible="False">
                                    <div class="form-group">
                                        <label>Section Name :</label>
                                        <asp:Label ID="secNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="secIdHiddenField" runat="server" />
                                    </div>
                                </div>


                                <div class="col-3">
                                    <div class="form-group" runat="server" Visible="False">
                                        <label>Sub Section Name :</label>
                                        <asp:Label ID="subSectionLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="subSectionHiddenField" runat="server" />
                                    </div>
                                </div>

                                

                                <div class="col-3">
                                    <div class="form-group" runat="server" Visible="False">
                                        <label>Employee Type:</label>
                                        <asp:Label ID="employeeType" runat="server" class="form-control form-control-sm"></asp:Label>
                                        <asp:HiddenField ID="empTypeHiddenField" runat="server" />
                                    </div>
                                </div>
                                

                            </div>
                            <%--<div class="form-row">
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Employee Name :</label>
                                        <asp:Label ID="empName" CssClass="form-control form-control-sm" runat="server"></asp:Label>

                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Company Name :</label>
                                        <asp:Label ID="comNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="comIdHiddenField" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Division Name :</label>
                                        <asp:Label ID="divisionNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="divitionIdHiddenField" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Wing Name :</label>
                                        <asp:Label ID="divWingNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="divWingIdHiddenField" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Department Name :</label>
                                        <asp:Label ID="deptNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="deptIdHiddenField" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Section Name :</label>
                                        <asp:Label ID="secNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="secIdHiddenField" runat="server" />
                                    </div>
                                </div>


                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Sub Section Name :</label>
                                        <asp:Label ID="subSectionLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="subSectionHiddenField" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Designation Name :</label>
                                        <asp:Label ID="desigNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="desigIdHiddenField" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Employee Type:</label>
                                        <asp:Label ID="employeeType" runat="server" class="form-control form-control-sm"></asp:Label>
                                        <asp:HiddenField ID="empTypeHiddenField" runat="server" />
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Joining Date :</label>
                                        <asp:Label ID="joiningDateLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>--%>


                            <asp:GridView runat="server" OnRowDataBound="gv_AppraisalFunc_OnRowDataBound" ShowFooter="true" AutoGenerateColumns="False" Width="100%" ID="gv_AppraisalFunc" CssClass="table table-bordered text-center thead-dark gridDatatable" DataKeyNames="AppraisalSelfFucAreaId">

                                <Columns>

                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Key Performance Indicator">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtKpi" CssClass="form-control  form-control-sm" ReadOnly="True" TextMode="MultiLine" Text='<%#Eval("KpiInfo") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Weight (Number)">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtWeight" AutoPostBack="True"  ReadOnly="True" OnTextChanged="txtWeight_OnTextChanged" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("KpiWeight") %>'></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                TargetControlID="txtWeight" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalWeight" CssClass="form-control  form-control-sm " runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Weight (%) ">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtWeightPer" AutoPostBack="true" ReadOnly="True" OnTextChanged="txtWeightPer_OnTextChanged" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("KpiWeightPer") %>'></asp:TextBox>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Target (Number)">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtTarget" AutoPostBack="True"  ReadOnly="True" OnTextChanged="txtTarget_OnTextChanged" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("Target") %>'></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2122" runat="server" Enabled="True"
                                                TargetControlID="txtTarget" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lbltarget" CssClass="orm-control  form-control-sm " runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Target (%)" runat="server" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtTargetPer" AutoPostBack="True"  ReadOnly="True"  OnTextChanged="txtTargetPer_OnTextChanged" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("TargetPer") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Dead Line">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtDeadLine" CssClass="form-control  form-control-sm" ReadOnly="True" Text='<%#Eval("Deadline") %>'></asp:TextBox>
                                   <%--         <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                Format="dd-MMM-yyyy" CssClass="custom"
                                                TargetControlID="txtDeadLine" />--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Mid Year Status">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ReadOnly="True" ID="txtMidStatus" CssClass="form-control  form-control-sm" Text='<%#Eval("MidYearStatus") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Result End Status">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtResult" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("ResultYearEnd") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblresultend" CssClass="form-control  form-control-sm " runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Self-Mark">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ReadOnly="True" ID="txtselfMark" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("SelfMark") %>'></asp:TextBox>
                                            
                                           <%-- <asp:Label runat="server" ID="txtselfMark" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("SelfMark") %>'></asp:Label>--%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblselfMark" CssClass="form-control  form-control-sm " runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SupervisorMark">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtMark" AutoPostBack="True" OnTextChanged="txtMark_OnTextChanged" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("SupervisorMark") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalMark" CssClass="fa-2xorm-control  form-control-sm " runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>







                                    <asp:TemplateField HeaderText="Operation" runat="server" Visible="false">
                                        <ItemTemplate>

                                            <asp:LinkButton ID="btn_Add" OnClick="btn_Add_OnClick" runat="server">Add</asp:LinkButton>|
                                        <asp:LinkButton ID="lb_Remove" OnClick="lb_Remove_OnClick" runat="server">Remove</asp:LinkButton>
                                        </ItemTemplate>


                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>

                            <asp:HiddenField runat="server" ID="id_mastetID" />
                            <asp:HiddenField runat="server" ID="id_selfID" />
                            <asp:HiddenField runat="server" ID="id_Empid" />
                            <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" CssClass="btn btn-sm btn-info" Text="Submit"></asp:Button>
                            <%--<asp:Button runat="server" ID="btn_Review" OnClick="btn_Review_OnClick" CssClass="btn btn-sm btn-info" Text="Review"></asp:Button>--%>

                            <%--<asp:Button ID="cancelButton" Text="Cancel" OnClick="cancelButton_Click" CssClass="btn btn-sm warning" runat="server"  BackColor="#FFCC00" />--%>
                        </div>
                    </div>
                </div>
            </ContentTemplate>

        </asp:UpdatePanel>

    </div>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="AppraisalFinal.aspx.cs" Inherits="Appraisal_AppraisalFinal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">

    <div class="content">

        <asp:UpdatePanel runat="server" ID="upFormBody">
            <ContentTemplate>
                <div class="container-fluid">

                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;">Appraisal Final Status</h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="detailsViewButton" Text="Appraisal List" OnClick="detailsViewButton_OnClick" CssClass="btn btn-sm btn-outline-secondary " runat="server" />
                        </div>

                    </div>
                    <div class="card">
                        <div class="card-body">
                            <div class="form-row" runat="server" visible="False">
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
                                    <div class="form-group" runat="server" visible="False">
                                        <label>Wing Name :</label>
                                        <asp:Label ID="divWingNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="divWingIdHiddenField" runat="server" />
                                    </div>
                                </div>
                                <div class="col-3" runat="server" visible="False">
                                    <div class="form-group">
                                        <label>Company Name :</label>
                                        <asp:Label ID="comNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="comIdHiddenField" runat="server" />
                                    </div>
                                </div>
                                <div class="col-3" runat="server" visible="False">
                                    <div class="form-group">
                                        <label>Section Name :</label>
                                        <asp:Label ID="secNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="secIdHiddenField" runat="server" />
                                    </div>
                                </div>


                                <div class="col-3">
                                    <div class="form-group" runat="server" visible="False">
                                        <label>Sub Section Name :</label>
                                        <asp:Label ID="subSectionLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="subSectionHiddenField" runat="server" />
                                    </div>
                                </div>



                                <div class="col-3">
                                    <div class="form-group" runat="server" visible="False">
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
                                        <label> Wing Name :</label>
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

                            <div class="form-row">
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Total Marks Obtained (Out Of 100)</label>

                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>A:Functional</label>
                                        <asp:Label runat="server" ID="partAScore"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>B:Behavioral</label>
                                        <asp:Label runat="server" ID="partBScore"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Total</label>
                                        <asp:Label runat="server" ID="totalScore"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="col-3">

                                    <div class="form-group">
                                        <label>Over All Status:</label>
                                        <asp:Label runat="server" ID="lblStatus"></asp:Label>
                                    </div>

                                </div>
                            </div>
                            <div class="form-row">
                                <asp:RadioButtonList runat="server" ID="recommend" AutoPostBack="True" OnSelectedIndexChanged="recommend_SelectedIndexChanged">

                                    <asp:ListItem Text="General Increment" Value="1" />
                                    <asp:ListItem Text="Special Increment" Value="2" />
                                    <asp:ListItem Text="Promotion" Value="3" />
                                    <asp:ListItem Text="Performance Improvement Plan" Value="4" />
                                    <asp:ListItem Text="Promotion with Additional Increment" Value="5"/>
                                         <asp:ListItem Text="Other" Value="6" />
                                </asp:RadioButtonList>
                            </div>
                            <div id="Divsteps" class="form-row" runat="server" visible="False">
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Steps</label>
                                        <asp:TextBox ID="txtStep"  CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                              <div id="Div1Other" class="form-row" runat="server" visible="False">
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Note</label>
                                   <asp:TextBox ID="txtnote"  CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <asp:HiddenField runat="server" ID="id_mastetID" />

                            <br />
                            <asp:HyperLink ID="SuspendHistory" runat="server" Target="_blank"  NavigateUrl="~/SuspendAndDiciplinary_UI/History.aspx">Suspend History</asp:HyperLink>

                            <br />  <br />
                            <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" CssClass="btn btn-sm btn-info" Text="Submit"></asp:Button>

                            <%-- <asp:Button ID="cancelButton" Text="Cancel"  CssClass="btn btn-sm warning" runat="server"  BackColor="#FFCC00" />--%>
                        </div>
                    </div>
                </div>
            </ContentTemplate>

        </asp:UpdatePanel>

    </div>
</asp:Content>


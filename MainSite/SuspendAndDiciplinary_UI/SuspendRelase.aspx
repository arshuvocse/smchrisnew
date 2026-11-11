<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="SuspendRelase.aspx.cs" Inherits="SuspendAndDiciplinary_UI_SuspendRelase" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">

    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <!-- PAGE HEADING -->

                <div class="page-heading">
                    <div class="page-heading__container">
                        <%--<div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>--%>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Employee Suspend Entry </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <%--<asp:Button ID="detailsViewButton" Text="View Details Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />--%>
                        <%-- <asp:Button ID="reportViewButton" Text="Report" CssClass="btn btn-sm btn-outline-success" runat="server" OnClick="rptImageButton_Click" />--%>
                    </div>

                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                        <%--<asp:Button ID="ListViewButton" Visible="True" Text="Go Back" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />--%>
                    </div>

                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">

                    <div class="card">
                        <div class="card-body">


                            <div class="form-row">
                                <div class="col-md-6">
                                    <div class="form-row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Company Name: </label>
                                                <asp:DropDownList ID="companyDropDownList" ReadOnly="True" class="form-control form-control-sm" runat="server" AutoPostBack="True"></asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-6">
                                            <div class="form-group">
                                                <label>Release Date: </label>
                                                <div class="input-group date pull-left" id="daterangepicker1">
                                                    <asp:TextBox ID="releaseDateTextBox"  class="form-control form-control-sm" AutoCompleteType="Disabled" runat="server"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="TopLeft"
                                                        Format="dd-MMM-yyyy" PopupButtonID="releaseDateTextBox" CssClass="MyCalendar"
                                                        TargetControlID="releaseDateTextBox" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-row">

                                        <div class="col-6">
                                            <div class="form-group">
                                                <label><span style="font-size: 11px; font-weight: bold;">Action Type: </span></label>
                                                <asp:DropDownList ID="actionTypeDropDownList" ReadOnly="True" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-row">

                                        <div class="col-4">
                                            <div class="form-group">
                                                <label>Employee Code:</label>
                                                <asp:Label ID="empCodeLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>

                                            </div>
                                        </div>



                                        <div class="col-4">
                                            <div class="form-group">
                                                <label>Department Name :</label>
                                                <asp:Label ID="deptNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                                <asp:HiddenField ID="deptIdHiddenField" runat="server" />
                                            </div>
                                        </div>

                                        <div class="col-4">
                                            <div class="form-group">
                                                <label>Employee Type:</label>
                                                <asp:Label ID="employeeType" runat="server" class="form-control form-control-sm"></asp:Label>
                                                <asp:HiddenField ID="empTypeHiddenField" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-row">
                                        <div class="col-4">
                                            <div class="form-group">
                                                <label>Employee Name:</label>
                                                <asp:Label ID="empNameTexBox" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                                <asp:HiddenField ID="EmpInfoIdHiddenField" runat="server" />
                                                <asp:HiddenField ID="suspendHiddenField" runat="server" />
                                            </div>
                                        </div>

                                        <div class="col-4">
                                            <div class="form-group">
                                                <label>Designation Name :</label>
                                                <asp:Label ID="desigNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                                <asp:HiddenField ID="desigIdHiddenField" runat="server" />
                                            </div>
                                        </div>

                                        <div class="col-4">
                                            <div class="form-group">
                                                <label>Joining Date :</label>
                                                <asp:Label ID="joiningDateLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                            </div>
                                        </div>

                                    </div>
                                </div>


                            </div>






                            <div class="form-row">

                                <div class="col-3" runat="server" Visible="False">
                                    <div class="form-group">
                                        <label>Effective Date :</label>
                                        <div class="input-group date pull-left" id="daterangepicker1">
                                            <asp:TextBox ID="effectDateTexBox" runat="server" class="form-control form-control-sm" CausesValidation="true"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender" runat="server" PopupPosition="TopLeft"
                                                Format="dd-MMM-yyyy" PopupButtonID="ImageButtosn1" CssClass="MyCalendar"
                                                TargetControlID="effectDateTexBox" />
                                        </div>
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label> Explanation </label>
                                        
                                        <asp:TextBox ID="explainTextBox" runat="server" class="form-control form-control-sm" ></asp:TextBox>
                                            
                                        
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Description </label>
                                        <asp:TextBox ID="descriptionTexBox" ReadOnly="True" runat="server" CssClass="form-control resize" Rows="1" Columns="20" TextMode="MultiLine" class="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Remarks </label>
                                        <asp:TextBox ID="remarksTextBox" ReadOnly="True" runat="server" Rows="1" Columns="20" TextMode="MultiLine" class="form-control resize"></asp:TextBox>
                                    </div>
                                </div>

                            </div>


                            <%--<div class="form-row">
                                <div class="col-3">
                                    <div class="form-group">
                                        <label> Effective Date: </label>
                                        <div class="input-group date pull-left" id="daterangepicker1">
                                            <asp:Label ID="effectDateTexBox" runat="server" class="form-control form-control-sm"  CausesValidation="true"></asp:Label>
                                           
                                        </div>
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label> Employee Name: </label>
                                        <asp:Label ID="empNameTexBox" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="EmpInfoIdHiddenField" runat="server" />
                                        <asp:HiddenField ID="suspendHiddenField" runat="server" />
                                    </div>
                                </div>
                            </div>
                   
                            <asp:Label ID="MessageLabel" runat="server" Text=""></asp:Label>
                            <div class="form-row">
                                <div class="col-3">
                                    <div class="form-group">
                                        <label> Company Name: </label>
                                        <asp:Label ID="comNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="comIdHiddenField" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label> Division Name: </label>
                                        <asp:Label ID="divisionNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="divitionIdHiddenField" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Division Wing Name:</label>
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
                            </div>

                            <div class="form-row">
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

                            </div>


                            <div class="form-row">

                                <div class="col-3" runat="server" visible="False">
                                    <div class="form-group">
                                        <label>Grade:</label>
                                        <asp:Label ID="empGradeLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="empGradeIdHiddenField" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Joining Date:</label>
                                        <asp:Label ID="joiningDateLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                    </div>
                                </div>


                            

                                <div class="col-3" style="display: none">
                                    <div class="form-group">
                                        <label>Remarks: </label>
                                        <asp:TextBox ID="remarksTextBox" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                            <div class="form-row">
                                <div class="col-12">
                                    <div class="form-group">
                                        <label><span style="font-size: 11px; font-weight: bold;">Type: </span></label>
                                        <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem> &nbsp; &nbsp;Suspension Letter &nbsp; &nbsp;</asp:ListItem>
                                            <asp:ListItem> &nbsp; &nbsp;With Pay &nbsp; &nbsp;</asp:ListItem>
                                            <asp:ListItem> &nbsp; &nbsp;Without Pay &nbsp; &nbsp;</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                </div>

                            </div>
                            <div class="form-row">
                                <div class="col-3">
                                    <div class="form-group">
                                        <label> Release Date: </label>
                                        <div class="input-group date pull-left" id="daterangepicker1">
                                            <asp:TextBox ID="releaseDateTextBox" class="form-control form-control-sm" AutoCompleteType="Disabled" runat="server"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="TopLeft"
                                                Format="dd-MMM-yyyy" PopupButtonID="releaseDateTextBox" CssClass="MyCalendar"
                                                TargetControlID="releaseDateTextBox" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label> Explanation </label>
                                        
                                        <asp:TextBox ID="explainTextBox" runat="server" class="form-control form-control-sm" ></asp:TextBox>
                                            
                                        
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label> Remarks </label>
                                        
                                        <asp:TextBox ID="relremarksTextBox" runat="server" class="form-control form-control-sm" ></asp:TextBox>
                                            
                                        
                                    </div>
                                </div>
                                
                                <div class="col-3">
                                    
                                    <div class="form-group">
                                        <label>Description: </label>
                                        <asp:TextBox ID="descriptionTexBox" CssClass="form-control resize" Rows="1" Columns="20" TextMode="MultiLine" runat="server"  ></asp:TextBox>
                                    </div>
                                 
                            </div>--%>

                            <br />
                            <div class="form-row">
                                <div class="col-12">
                                    <div class="form-group">
                                        <div class="form-row">
                                            <div class="col-6">
                                                <asp:Button ID="submithButton" Text="Submit" CssClass="btn btn-sm btn-info" runat="server" OnClick="submitButton_Click" />
                                            </div>
                                            <%--<div class="col-6">
                                                <asp:Button ID="backToList" Text="Back to list" CssClass="btn btn-sm warning" runat="server" OnClick="backToList_OnClick" BackColor="#FFCC00" />
                                            </div>--%>


                                        </div>
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


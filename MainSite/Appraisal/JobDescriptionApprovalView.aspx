<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="JobDescriptionApprovalView.aspx.cs" Inherits="Appraisal_JobDescription" %>

<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <style type="text/css">
        
    </style>
    <div class="content">

        <asp:UpdatePanel runat="server" ID="upFormBody">
            <ContentTemplate>
                <div class="container-fluid">

                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;">Job Description</h1>
                        </div>
                        <%--<div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="detailsViewButton" Text="Job Description List"   OnClick="detailsViewButton_OnClick" CssClass="btn btn-sm btn-outline-secondary " runat="server"  />
                        </div>--%>

                        <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                            <asp:Button ID="detailsViewButton" Visible="True" Text="Go Back" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                        </div>


                    </div>
                    <div class="card">
                        <div class="card-body">
                            
                            
                                <div class="form-row">
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-1.5">
                                    <label style="font-weight: bold">Approval Status:&nbsp;<span style="color: #a52a2a">*</span></label>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">

                                        <asp:RadioButtonList ID="actionRadioButtonList" runat="server" RepeatDirection="Horizontal">
                                        </asp:RadioButtonList>
                                    </div>
                                </div>

                            </div>

                            <div class="form-row">
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label style="font-weight: bold">Comments</label>
                                        <asp:TextBox runat="server" ID="commentsTextBox" CssClass="form-control" ReadOnly="False" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>


                            <div class="form-row">
                                <div class="col-md-5">
                                </div>
                                <div class="col-4 ">
                                    <div class="form-group">
                                        <link href="../UserSetup/ButtonGrups.css" rel="stylesheet" />
                                        <div class="ui-group-buttons">
                                            <asp:LinkButton runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" CssClass="btn btn-sm btn-info" OnClientClick="return confirm('Are you sure you want to Submit ?')" Style="box-shadow: 0 0 3px 1px rgba(0,0,0,.35);"> <i class="fa fa-check" aria-hidden="true"></i> &nbsp; Submit

&nbsp; </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="row">
                                <style>
                                    .tblTHColorChang {
                                        background-color: #EDF2F5 !important;
                                        font-weight: bold;
                                        font-size: 13px;
                                    }


                                    .title-widget {
                                        color: #898989;
                                        font-size: 20px;
                                        font-weight: 300;
                                        line-height: 1;
                                        position: relative;
                                        text-transform: uppercase;
                                        font-family: 'Fjalla One', sans-serif;
                                        margin-top: 0;
                                        margin-right: 0;
                                        margin-bottom: 25px;
                                        padding-left: 12px;
                                    }

                                        .title-widget::before {
                                            background-color: #ea5644;
                                            content: "";
                                            height: 22px;
                                            left: 0px;
                                            position: absolute;
                                            top: -2px;
                                            width: 5px;
                                        }
                                </style>
                                <table class="table table-bordered table-striped">
                                    <tr>
                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Employee ID</td>
                                        <td>
                                            <asp:Label runat="server" ID="lblEmpId"></asp:Label></td>


                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Supervisor</td>
                                        <td>
                                            <asp:Label ID="ReportingLabel" runat="server"></asp:Label></td>
                                    </tr>

                                    <tr>
                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Employee Name</td>
                                        <td>
                                            <asp:Label runat="server" ID="lblEmployeeName"></asp:Label></td>



                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Department</td>
                                        <td>
                                            <asp:Label ID="deptNameLabel" runat="server"></asp:Label></td>
                                    </tr>

                                    <tr>
                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Designation</td>
                                        <td>
                                            <asp:Label ID="desigNameLabel" runat="server"></asp:Label></td>

                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Office</td>
                                        <td>
                                            <asp:Label ID="LocationLabel" runat="server"></asp:Label></td>

                                    </tr>






                                    <tr>
                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Date Of Joining</td>
                                        <td>
                                            <asp:Label ID="joiningDateLabel" runat="server"></asp:Label></td>
                                        <td style="width: 20%; padding: 10px;" class="tblTHColorChang">Place</td>
                                        <td>
                                            <asp:Label runat="server" ID="lblPlace"></asp:Label></td>
                                    </tr>





                                </table>
                            </div>

                            <div class="form-row" runat="server" Visible="False">
                                <div class="col-3" runat="server" Visible="False">
                                    <div class="form-group">
                                        <label>Company</label>
                                        <asp:DropDownList runat="server" ID="ddlCompany" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" CssClass="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Name</label>
                                        <asp:TextBox runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" ID="txt_employee" OnTextChanged="txt_employee_OnTextChanged" ReadOnly="True"></asp:TextBox>

                                        <%--<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>--%>
                                  <%--      <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                            EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                            ServiceMethod="GetEployeeAutoComp" ServicePath="~/Training/WebService.asmx" TargetControlID="txt_employee"
                                            UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                            ShowOnlyCurrentWordInCompletionListItem="true">
                                        </cc1:AutoCompleteExtender>--%>
                                    </div>
                                </div>
                                    <div class="col-3" >
                                    <div class="form-group">
                                        <label>Division Name :</label>
                                        <asp:Label ID="divisionNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="divitionIdHiddenField" runat="server" />
                                    </div>
                                </div>
                                     <div class="col-3">
                                    <div class="form-group">
                                        <label>Department Name :</label>
                                       
                                        <asp:HiddenField ID="deptIdHiddenField" runat="server" />
                                    </div>
                                </div>
                         
                            </div>
                            <div class="form-row" runat="server" Visible="False">
                                <div class="col-3" runat="server" Visible="False">
                                    <div class="form-group">
                                        <label>Company Name :</label>
                                        <asp:Label ID="comNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="comIdHiddenField" runat="server" />
                                    </div>
                                </div>

                            

                                <div class="col-3"   runat="server" Visible="False">
                                    <div class="form-group">
                                        <label>Wing Name :</label>
                                        <asp:Label ID="divWingNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="divWingIdHiddenField" runat="server" />
                                    </div>
                                </div>

                           
                            </div>
                            <div class="form-row" runat="server" Visible="False">
                                <div class="col-3" runat="server" Visible="False">
                                    <div class="form-group">
                                        <label>Section Name :</label>
                                        <asp:Label ID="secNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="secIdHiddenField" runat="server" />
                                    </div>
                                </div>


                                <div class="col-3" runat="server" Visible="False">
                                    <div class="form-group">
                                        <label>Sub Section Name :</label>
                                        <asp:Label ID="subSectionLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="subSectionHiddenField" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Designation Name :</label>
                                        
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
                                       
                                    </div>
                                </div>

                            </div>
                            <div class="form-row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Job Summary</label>
                                        <asp:TextBox runat="server" ID="txtJobSummary" CssClass="form-control" ReadOnly="True" Text="Job Summary" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <asp:GridView runat="server"  AutoGenerateColumns="False" Width="100%" ID="gv_JdDetails" CssClass="table table-bordered text-center thead-dark gridDatatable">

                                <Columns>

                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Active">
                                           
                                            <ItemTemplate >
                                                <asp:CheckBox ID="chkSelect" CssClass="form-control-sm" AutoPostBack="True" runat="server" Enabled="False" Checked="True"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Job Description">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtJdDetails" CssClass="form-control  form-control-sm" TextMode="MultiLine" Text='<%#Eval("JdDetailsInfo") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>








<%--                                    <asp:TemplateField HeaderText="Operation">
                                        <ItemTemplate>

                                            <asp:LinkButton ID="btn_Add" OnClick="btn_Add_OnClick" runat="server">Add</asp:LinkButton>|
                                        <asp:LinkButton ID="lb_Remove" OnClick="lb_Remove_OnClick" runat="server">Remove</asp:LinkButton>
                                        </ItemTemplate>


                                    </asp:TemplateField>--%>

                                </Columns>
                            </asp:GridView>
                           
                            <div class="form-group">
                                        
                                    </div>
                            <asp:HiddenField runat="server" ID="masterId" />
                            <asp:HiddenField runat="server" ID="empInfoId" />


                            
                            <%--<asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="editButton_OnClick" />
                            <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="delButton_OnClick" />--%>

                            <%--<asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" CssClass="btn btn-sm btn-info" Text="Submit"></asp:Button>--%>

                            <%--<asp:Button ID="cancelButton" Text="Cancel" OnClick="cancelButton_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" Visible="False"/>--%>
                        </div>
                    </div>
                </div>
            </ContentTemplate>

        </asp:UpdatePanel>

    </div>
</asp:Content>


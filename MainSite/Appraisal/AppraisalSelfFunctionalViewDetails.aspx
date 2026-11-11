<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="AppraisalSelfFunctionalViewDetails.aspx.cs" Inherits="Appraisal_AppraisalSelfFunctionalViewDetails" %>

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
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;">
                                <img src="../Report_Pages/app.png" width="20px" />
                                KPI Setup</h1>
                        </div>

                        <%-- <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="detailsViewButton" Text="Appraisal List" OnClick="detailsViewButton_OnClick" CssClass="btn btn-sm btn-outline-secondary " runat="server"  />
                        </div>--%>

                        <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                            <asp:Button ID="detailsViewButton" Visible="True" Text="&#8920; Back To List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                        </div>

                    </div>
                    <div class="card">
                        <div class="card-body">


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
                                            <asp:Label ID="lblOffice" runat="server"></asp:Label></td>

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

                            <div runat="server" visible="False">
                                <div class="form-row">
                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>Name</label>
                                            <asp:TextBox runat="server" ReadOnly="True" OnTextChanged="txt_employee_OnTextChanged" AutoPostBack="True" CssClass="form-control form-control-sm" ID="txt_employee"></asp:TextBox>

                                            <%--<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>--%>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                ServiceMethod="GetEployeeAutoComp2" ServicePath="~/WebService.asmx" TargetControlID="txt_employee"
                                                UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                ShowOnlyCurrentWordInCompletionListItem="true">
                                            </cc1:AutoCompleteExtender>
                                        </div>
                                    </div>
                                    <div class="col-2" runat="server" visible="false">
                                        <div class="form-group">
                                            <label>Financial Year</label>
                                            <asp:DropDownList ID="ddlFinancialYear" AutoPostBack="true" runat="server" class="form-control form-control-sm"></asp:DropDownList>
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


                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Designation Name :</label>

                                            <asp:HiddenField ID="desigIdHiddenField" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>Department Name :</label>

                                            <asp:HiddenField ID="deptIdHiddenField" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>Joining Date :</label>

                                        </div>
                                    </div>

                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>Job Location :</label>

                                        </div>
                                    </div>

                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>Supervisor :</label>

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
                            </div>


                            <h2 class="blue title-widget" style="color: #2196F3; text-shadow: 0 0 2px black;">A.Functional Area (75 Marks)</h2>
                            <hr />

                            <asp:GridView runat="server" ShowFooter="true" AutoGenerateColumns="False" Width="100%" ID="gv_AppraisalFunc" CssClass="table table-bordered text-center thead-dark gridDatatable">

                                <Columns>

                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Key Performance Indicator">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="txtKpi" Text='<%#Eval("KpiInfo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Weight (Number)">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtWeight" ReadOnly="True" OnTextChanged="txtWeight_OnTextChanged" AutoPostBack="True" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("KpiWeight") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalWeight" CssClass="form-control  form-control-sm " runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Weight (%)">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtWeightPer" ReadOnly="True" AutoPostBack="True" OnTextChanged="txtWeightPer_OnTextChanged" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("KpiWeightPer") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <%--  <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalWeight" CssClass="orm-control  form-control-sm " runat="server" />
                                        </FooterTemplate>--%>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Target (Number)">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" AutoPostBack="True" ReadOnly="True" OnTextChanged="txtTarget_OnTextChanged" ID="txtTarget" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("Target") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Target (%) " Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtTargetPer" AutoPostBack="True" OnTextChanged="txtTargetPer_OnTextChanged" CssClass="form-control   form-control-sm" TextMode="Number" Text='<%#Eval("TargetPer") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Dead Line">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="txtDeadLine" Text='<%#Eval("Deadline") %>'></asp:Label>



                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Mid Year Status" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtMidStatus" CssClass="form-control  form-control-sm" Text='<%#Eval("MidYearStatus") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Self-Mark" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtMark" AutoPostBack="True" OnTextChanged="txtMark_OnTextChanged" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("SelfMark") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalMark" CssClass="form-control  form-control-sm " runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Is Active">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="isActiveCheckBox" OnCheckedChanged="isActiveCheckBox_OnCheckedChanged" runat="server" Checked='<%#Eval("IsActive") %>' AutoPostBack="True" Enabled="False" EnableTheming="True" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Add" Visible="False">
                                        <ItemTemplate>

                                            <asp:LinkButton ID="btn_Add" CssClass="btn btn-info btn-sm" OnClick="btn_Add_OnClick" runat="server"><i class="fa fa-plus" aria-hidden="true"></i>
                                            </asp:LinkButton>

                                        </ItemTemplate>


                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Delete" Visible="False">
                                        <ItemTemplate>

                                            <asp:LinkButton ID="lb_Remove" CssClass="btn btn-danger btn-sm" OnClick="lb_Remove_OnClick" runat="server"><i class="fa fa-trash" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>


                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                            <h2 class="blue title-widget" style="color: #2196F3; text-shadow: 0 0 2px black;">B.Behavioral Area (25 Marks) </h2>
                            <hr />
                            <asp:GridView runat="server" ShowFooter="true" AutoGenerateColumns="False" Width="100%" ID="gv_AppraisalPartB" CssClass="table table-bordered text-center thead-dark gridDatatable">

                                <Columns>

                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Competencies / Skills">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="SkillInfo" Text='<%#Eval("SkillInfo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Supporting Example">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="SupportingEmp" Text='<%#Eval("SupportingEmp") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Weight (Number)">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ReadOnly="True" ID="Score" CssClass="form-control  form-control-sm" OnTextChanged="Score_OnTextChanged" TextMode="Number" Text='<%#Eval("Score") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalScore" CssClass="form-control  form-control-sm " runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Expected Number">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ReadOnly="True" ID="SetScore" CssClass="form-control  form-control-sm" Text='<%#Eval("SetScore") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="ddllblTotalWeight" CssClass="form-control  form-control-sm " runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Operation" Visible="False">
                                        <ItemTemplate>

                                            <asp:LinkButton ID="btn_Add_B" OnClick="btn_Add_B_OnClick" runat="server">Add</asp:LinkButton>|
                                        <asp:LinkButton ID="lb_Remove_b" OnClick="lb_Remove_b_OnClick" runat="server">Remove</asp:LinkButton>
                                        </ItemTemplate>


                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>

                            <div runat="server" visible="False">
                                Previous Approver Comments :
                            <hr>
                                <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gv_Versions" CssClass="table table-bordered text-center thead-dark gridDatatable">

                                    <Columns>

                                        <asp:TemplateField HeaderText="SL#">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Employee ">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="SkillInfo" CssClass="form-control  form-control-sm" Text='<%#Eval("Employee") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="SupportingEmp" CssClass="form-control  form-control-sm" Text='<%#Eval("ApproveStatus") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Remarks" CssClass="form-control  form-control-sm" TextMode="MultiLine" Text='<%#Eval("Remarks") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Version">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Version" CssClass="form-control  form-control-sm" Text='<%#Eval("PreviousVersion") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="Date" CssClass="form-control  form-control-sm" Text='<%#Eval("EntryDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                    </Columns>
                                </asp:GridView>

                            </div>


                            <div class="form-row" runat="server" visible="False">

                                <div class="col-4">
                                    <div class="form-group" runat="server">
                                        <label>Comments :</label>
                                        <label style="font-size: 10px; color: gray; font-style: italic">(*Applicable for Final Submit)</label>
                                        <asp:TextBox runat="server" ID="txt_Comments" CssClass="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>


                                <div class="col-8" runat="server" visible="False">
                                    <div class="form-group" runat="server">

                                        <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" ID="GridView1" CssClass="table table-bordered text-center thead-dark gridDatatable">

                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Employee ">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="EmpName" CssClass="form-control  form-control-sm" Text='<%#Eval("EmpName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Comments">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="Comments" CssClass="form-control  form-control-sm" Text='<%#Eval("Comments") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>

                                    </div>
                                </div>


                            </div>

                            <asp:HiddenField runat="server" ID="id_mastetID" />
                            <asp:HiddenField runat="server" ID="id_Empid" />


                            <link href="../UserSetup/ButtonGrups.css" rel="stylesheet" />
                            <div class="ui-group-buttons">
                                <asp:LinkButton runat="server" ID="btn_Save" Style="box-shadow: 0 0 3px 1px rgba(0,0,0,.35);" OnClick="btn_Save_OnClick" CssClass="btn btn-sm btn-info" Text="Save"></asp:LinkButton>
                                <div class="or or-sm" runat="server" id="orBTN"></div>
                                <asp:LinkButton ID="submitButton" Style="box-shadow: 0 0 3px 1px rgba(0,0,0,.35);" Text="Save/Draft" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="btn_Save_OnClick" />
                                <asp:LinkButton ID="submitVerifyButton" Style="box-shadow: 0 0 3px 1px rgba(0,0,0,.35);" Text="Submit" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="submitVerifyButton_OnClick" />
                                <asp:LinkButton ID="editButton" Style="box-shadow: 0 0 3px 1px rgba(0,0,0,.35);" Text="Update" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                <asp:LinkButton ID="delButton" Style="box-shadow: 0 0 3px 1px rgba(0,0,0,.35);" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" />
                            </div>
                            <%--<asp:Button ID="cancelButton" Text="Cancel" OnClick="cancelButton_Click" CssClass="btn btn-sm warning" runat="server"  BackColor="#FFCC00" />--%>
                        </div>
                    </div>
                </div>
            </ContentTemplate>

        </asp:UpdatePanel>

    </div>
</asp:Content>

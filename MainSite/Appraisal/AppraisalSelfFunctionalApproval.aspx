<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="AppraisalSelfFunctionalApproval.aspx.cs" Inherits="Appraisal_AppraisalSelfFunctionalApproval" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <style type="text/css">
        /*AutoComplete flyout */
    </style>
    <div class="content">

        <asp:UpdatePanel runat="server" ID="upFormBody">
            <ContentTemplate>
                <div class="container-fluid">

                    <style>
                        .chkChoiceHeader label {
                            padding-left: 4px;
                            padding-right: 10px;
                            font-size: 13px;
                        }

                              .checkbox label,
         .checkbox-inline label {
             text-align: left;
             padding-left: 0.3em;
                 font-weight: bold;
         }
                        #cpFormBody_gv_Versions td {
                            border: 1px solid #ddd !important;
                            padding: 8px;
                        }

                        fieldset.for-panel {
                            background-color: #fcfcfc;
                            border: 1px solid #999;
                            padding: 15px 10px;
                            background-color: white;
                            margin-bottom: 12px;
                        }

                            fieldset.for-panel legend {
                                background-color: #fafafa;
                                border: 1px solid #ddd;
                                border-radius: 1px;
                                font-size: 12px;
                                font-weight: bold;
                                line-height: 10px;
                                margin: inherit;
                                padding: 7px;
                                width: auto;
                                margin-bottom: 0;
                                color: black;
                            }
                    </style>
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;">
                                <img src="../Report_Pages/app.png" width="20px" />
                                KPI Approval</h1>
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
                        
                          <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                        <ContentTemplate>
                            <asp:UpdateProgress ID="UpdateProgress10" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaiting">
                                        <asp:Image ID="imgWait1" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        <div class="card-body">


                            <div class="form-row">
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-1.5">
                                    <label style="font-weight: bold">Approval Status:&nbsp;<span style="color: #a52a2a">*</span></label>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">

                                        <asp:RadioButtonList ID="actionRadioButtonList" runat="server" CssClass="checkbox" RepeatDirection="Horizontal">
                                        </asp:RadioButtonList>
                                    </div>
                                </div>

                            </div>
                             <asp:HiddenField runat="server" ID="HFCompanyId"/>
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
                                              <asp:Button runat="server" ID="Button1" OnClientClick="return confirm('Are you sure you want to Save ?')" OnClick="btn_Draft_OnClick" Text="  Save  " CssClass="btn btn-sm btn-success" />
                                                    <div class="or or-sm"></div>
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


                            <div class="form-row" runat="server" visible="False">
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
                            <div class="form-row" runat="server" visible="False">


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

                                <div class="col-2" runat="server" visible="false">
                                    <div class="form-group">
                                        <label>Supervisor :</label>

                                    </div>
                                </div>
                            </div>
                            <div class="form-row" runat="server" visible="False">
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
                            <h2 class="blue title-widget" style="color: #2196F3; text-shadow: 0 0 2px black;">A.Functional Area (75 Marks)</h2>
                            <hr />
                            <asp:CheckBox runat="server" ID="chkFunc" AutoPostBack="True"   style="font-weight: bold;  color: black; font-size: 22px; background: yellow"  OnCheckedChanged="chkFunc_OnCheckedChanged" Text=" Change Functional Area"/>
                            <br />
                            <asp:GridView runat="server" ShowFooter="true" AutoGenerateColumns="False" Width="100%" ID="gv_AppraisalFunc" CssClass="table table-bordered text-center thead-dark gridDatatable">

                                <Columns>

                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Key Performance Indicator">
                                        <ItemTemplate>
                                            <asp:TextBox TextMode="MultiLine" Rows="4" ReadOnly="True" Width="600px"  CssClass="form-control"   runat="server" ID="txtKpi"    Text='<%#Eval("KpiInfo") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Weight">
                                        <ItemTemplate>
                                            <asp:TextBox  runat="server" ID="txtWeight"  ReadOnly="True"   OnTextChanged="txtWeight_OnTextChanged" AutoPostBack="True" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("KpiWeight") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalWeight" CssClass="form-control  form-control-sm " runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Weight %">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtWeightPer"  ReadOnly="True"   AutoPostBack="True" OnTextChanged="txtWeightPer_OnTextChanged" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("KpiWeightPer") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <%--  <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalWeight" CssClass="form-control  form-control-sm " runat="server" />
                                        </FooterTemplate>--%>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Target">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" AutoPostBack="True"  ReadOnly="True"   OnTextChanged="txtTarget_OnTextChanged" ID="txtTarget" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("Target") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Target % " Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox   runat="server" ID="txtTargetPer" AutoPostBack="True" OnTextChanged="txtTargetPer_OnTextChanged" CssClass="form-control   form-control-sm" TextMode="Number" Text='<%#Eval("TargetPer") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Deadline">
                                        <ItemTemplate>
                                            <asp:TextBox   runat="server" ID="txtDeadLine"  ReadOnly="True"  Enabled="False"  CssClass="form-control  form-control-sm" Text='<%#Eval("Deadline") %>'></asp:TextBox>


                                                 <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                TargetControlID="txtDeadLine" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Mid Year Status" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ReadOnly="True" runat="server" ID="txtMidStatus" CssClass="form-control  form-control-sm" Text='<%#Eval("MidYearStatus") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Self-Mark" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ReadOnly="True" runat="server" ID="txtMark" AutoPostBack="True" OnTextChanged="txtMark_OnTextChanged" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("SelfMark") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalMark" CssClass="form-control  form-control-sm " runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Operation" >
                                        <ItemTemplate>

                                             <asp:LinkButton ID="btn_Add" CssClass="btn btn-info btn-sm" Enabled="False" OnClick="btn_Add_OnClick" runat="server"><i class="fa fa-plus" aria-hidden="true"></i>
</asp:LinkButton>
                                               <asp:LinkButton ID="lb_Remove"  CssClass="btn btn-danger btn-sm" Enabled="False"   OnClick="lb_Remove_OnClick" runat="server"><i class="fa fa-trash" aria-hidden="true"></i>
</asp:LinkButton>
                                        </ItemTemplate>


                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>

                            <h2 class="blue title-widget" style="color: #2196F3; text-shadow: 0 0 2px black;">B.Behavioral Area (25 Marks) </h2>
                            <hr />
                            
                            <asp:CheckBox runat="server" ID="chkBehavioral" AutoPostBack="True"  style="font-weight: bold;  color: black; font-size: 22px; background: yellow"  OnCheckedChanged="chkBehavioral_OnCheckedChanged" Text=" Change Behavioral Area"/>
                            <br/>
                            <asp:GridView runat="server" ShowFooter="true" AutoGenerateColumns="False" Width="100%" ID="gv_AppraisalPartB" CssClass="table table-bordered text-center thead-dark gridDatatable">

                                <Columns>

                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Competencies / Skills">
                                        <ItemTemplate>
                                            <asp:TextBox TextMode="MultiLine" Rows="3" ReadOnly="True" runat="server" CssClass="form-control"  ID="SkillInfo"  Text='<%#Eval("SkillInfo") %>'></asp:TextBox>
                                              <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                        EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                        ServiceMethod="GetKPIBehaviour" ServicePath="~/WebService.asmx" TargetControlID="SkillInfo"
                                                        UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                        ShowOnlyCurrentWordInCompletionListItem="true">
                                                    </asp:AutoCompleteExtender>
                                            
                                            <asp:HiddenField runat="server" ID="hfSkillType"  Value='<%#Eval("SkillType") %>'/>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Supporting Example">
                                        <ItemTemplate>
                                            <asp:TextBox  TextMode="MultiLine"  ReadOnly="True"  Rows="3" CssClass="form-control"  runat="server" ID="SupportingEmp"   Text='<%#Eval("SupportingEmp") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Weight (Number)">
                                        <ItemTemplate>
                                            <asp:TextBox  runat="server" ReadOnly="True" AutoPostBack="True" ID="Score" CssClass="form-control  form-control-sm" OnTextChanged="Score_OnTextChanged" TextMode="Number" Text='<%#Eval("Score") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalScore" CssClass="form-control  form-control-sm " runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Expected Number">
                                        <ItemTemplate>
                                             <asp:DropDownList Enabled="False" runat="server" ID="ddlWeight" OnSelectedIndexChanged="ddlWeight_OnTextChanged" AutoPostBack="True" CssClass="form-control  form-control-sm" >
                                 <asp:ListItem  Value="">Select One</asp:ListItem>
                                 <asp:ListItem  Value="0">0</asp:ListItem>

                                 <asp:ListItem  Value="1">1</asp:ListItem>
                                 <asp:ListItem  Value="2">2</asp:ListItem>
                                 <asp:ListItem  Value="3">3</asp:ListItem>
                                 <asp:ListItem  Value="4">4</asp:ListItem>
                                 <asp:ListItem  Value="5">5</asp:ListItem>
                             </asp:DropDownList>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="ddllblTotalWeight" CssClass="form-control  form-control-sm " runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Operation" Visible="False">
                                        <ItemTemplate>

                                            <asp:LinkButton ID="btn_Add_B" OnClick="btn_Add_B_OnClick" runat="server">Add</asp:LinkButton>
                                            <asp:LinkButton Visible="False" ID="lb_Remove_b" OnClick="lb_Remove_b_OnClick" runat="server">Remove</asp:LinkButton>
                                        </ItemTemplate>


                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>








                            <asp:HiddenField runat="server" ID="id_mastetID" />
                            <asp:HiddenField runat="server" ID="id_Empid" />

                            <div class="col-md-3" runat="server" visible="false">
                                <div class="form-group">
                                    <label>Comments</label>
                                    <asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>


                            <div class="form-row" runat="server">
                                <div class="col-md-12">
                                    <fieldset class="for-panel">
                                        <legend>Approval Status List</legend>
                                        <div style="overflow: scroll">
                                            <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gv_Versions" CssClass="table table-bordered text-center thead-dark gridDatatable">

                                                <Columns>

                                                    <asp:TemplateField HeaderText="SL#">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Employee ">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="SkillInfo" Text='<%#Eval("Employee") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--   <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="SupportingEmp"  Text='<%#Eval("ApproveStatus") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>


                                                    <%--  <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="Remarks"  TextMode="MultiLine" Text='<%#Eval("Remarks") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>


                                                    <asp:TemplateField HeaderText="Comments">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="Version" Text='<%#Eval("PreviousVersion") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Approval Date">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="Date" Text='<%#Eval("EntryDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Approval Status">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="Datssse" Text='<%#Eval("ApproveStatus") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                </Columns>
                                            </asp:GridView>
                                    </fieldset>
                                </div>

                            </div>

                            <%--<asp:Button runat="server" ID="btn_Return" OnClick="btn_Return_OnClick" CssClass="btn btn-sm btn-dark" Text="Return"></asp:Button>

                            <asp:Button ID="submitButton" Text="Save" CssClass="btn btn-sm btn-info" Visible="False" runat="server"  />
                            <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-info" Visible="False" runat="server"  />--%>
                            <%--<asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="delButton_OnClick" />--%>

                            <%--<asp:Button ID="cancelButton" Text="Cancel" OnClick="cancelButton_Click" CssClass="btn btn-sm warning" runat="server"  BackColor="#FFCC00" />--%>
                        </div>
                            
                                </ContentTemplate>
                    </asp:UpdatePanel>
                    </div>
                </div>
            </ContentTemplate>

        </asp:UpdatePanel>

    </div>
</asp:Content>

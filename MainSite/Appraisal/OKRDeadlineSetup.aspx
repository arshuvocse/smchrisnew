<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="OKRDeadlineSetup.aspx.cs" Inherits="Appraisal_OKRDeadlineSetup" %>

<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">



    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>


    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
</asp:Content>
<asp:Content ID="trainingSetupContent" ContentPlaceHolderID="cpFormBody" runat="Server">

    <style>
        #cpFormBody_gv_allocateEmp > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }

        #cpFormBody_gv_allocateEmp td {
            padding: 8px;
        }



        #cpFormBody_SaveGridView td {
            padding: 8px;
        }

        #cpFormBody_gv_allocateEmp > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }
    </style>
    <style>
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




        #cpFormBody_SaveGridView > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #33B5E5;
            /*background-color: #98A9C0;*/
        }

        #cpFormBody_SaveGridView > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
            padding: 18px;
        }
    </style>
    <link href="../Assets/MyPerfectCalender.css" rel="stylesheet" />
    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>
                <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;">
                                <img src="../Report_Pages/app.png" width="20px" />
                                BSC/OKR Declaration and Deadline  Setup</h1>
                        </div>
                        <%--<div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="detailsViewButton" Text="KPI Deadline Setup List" OnClick="detailsViewButton_OnClick" CssClass="btn btn-sm btn-outline-secondary " runat="server" />
                        </div>--%>

                        <div class="page-heading__container float-right d-none d-sm-block">

                            <span class="alert alert-success">
                                <asp:Label runat="server" ID="lastDate"></asp:Label></span>

                            <asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                            <asp:Button ID="detailsViewButton" Visible="True" Text="Go Back" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                        </div>


                    </div>

                    <div class="card">
                        <div class="card-body">
                            <div class="form-row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Company</label>
                                        &nbsp;<label style="color: #a52a2a">*</label>
                                        <asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" CssClass="form-control form-control-sm" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                        &nbsp;<label style="color: #a52a2a">*</label>
                                        <asp:DropDownList ID="ddlFinancialYear" AutoPostBack="true" OnSelectedIndexChanged="ddlFinancialYear_OnSelectedIndexChanged" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Declaration Date</label>
                                        &nbsp;<label style="color: #a52a2a">*</label>
                                        <asp:TextBox ID="DeclarationTextBox" autocomplete="off" runat="server" TextMode="Date" CssClass="form-control form-control-sm" AutoPostBack="True" OnTextChanged="DeclarationTextBox_OnTextChanged"></asp:TextBox>
                                      <%--  <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                            Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                            TargetControlID="DeclarationTextBox" />--%>

                                    </div>

                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Subject</label>
                                        &nbsp;<label style="color: #a52a2a">*</label>
                                        <asp:TextBox ID="subjectTextBox" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                        <asp:HiddenField runat="server" ID="HfUpdateDate" />
                                    </div>
                                </div>
                                 <div class="col-md-2">
     <div class="form-group">
         <label for="ddl_options">Operation</label>     &nbsp;<label style="color: #a52a2a">*</label>
         <asp:DropDownList ID="ddl_options" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddl_options_SelectedIndexChanged">

             
             <asp:ListItem Text="Select One.." Value="0" />
             <asp:ListItem Text="OKR" Value="OKR" />
             <asp:ListItem Text="BSC" Value="BSC" />
         </asp:DropDownList>
                          <script type="text/javascript">
                              function pageLoad() {
                                  $('#<%=ddlDept.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });

                              }
                          </script>
     </div>
 </div>

                            </div>
                            <div class="form-row">


                                <div class="col-md-1">
                                    <div class="form-group" style="margin-top: 20px;">
                                        <asp:CheckBox ID="chk_Common" runat="server" Text=" Common" TextAlign="right" AutoPostBack="True" OnCheckedChanged="chk_Common_OnCheckedChanged"></asp:CheckBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Dead Line</label>
                                        <asp:TextBox ID="txt_deadLine" autocomplete="off" runat="server" CssClass="form-control form-control-sm" TextMode="Date" AutoPostBack="True"   OnTextChanged="txt_deadLine_OnTextChanged"></asp:TextBox>
                                       <%-- <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Format="MM/dd/yyyy" CssClass="MyCalendar"
                                            TargetControlID="txt_deadLine" />--%>

                                    </div>
                                </div>
                               
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Message</label>
                                        <asp:TextBox ID="txt_commonRemarks" runat="server" AutoPostBack="True" TextMode="MultiLine" Rows="2" OnTextChanged="txt_commonRemarks_OnTextChanged" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <fieldset class="for-panel">
                                <legend>Search Employee </legend>
                                <div class="form-row">

                                    <div class="col-2" style="display:none">
                                        <div class="form-group" style="padding-top: 18px!important">

                                            <asp:CheckBox runat="server" Text="New Joiner List " ID="chkNewJoinerList" />
                                        </div>
                                    </div>
                                    <div class="col-2"  style="display:none">
                                        <div class="form-group">
                                            <label>Employee Category</label>
                                            <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control form-control-sm" />
                                        </div>
                                    </div>

                                    <div class="col-2"  style="display:none">
                                        <div class="form-group">
                                            <label>Department</label>
                                            <asp:DropDownList runat="server" ID="ddlDept" CssClass="form-control form-control-sm" />

                                          
                                        </div>
                                    </div>
                                    <div class="col-2"  style="display:none">
                                        <div class="form-group" style="margin-top: 18px;">

                                            <asp:Button ID="Button1" Text="Search" CssClass="btn btn-outline-success disabled btn-sm" runat="server" OnClick="Button1_OnClick" />
                                        </div>
                                    </div>

                                </div>






                                <asp:GridView ID="gv_allocateEmp" Width="100%" CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="false" runat="server">
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
                                                <asp:HiddenField runat="server" ID="txt_empInfoId" Value='<%#Eval("EmpInfoId") %>' />
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee ID">
                                            <ItemTemplate>
                                                <asp:Label ID="txt_empId" runat="server" Text='<%#Eval("EmpMasterCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="txt_name" runat="server" Text='<%#Eval("EmpName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="txt_designation" runat="server" Text='<%#Eval("Designation") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="txt_dptName" runat="server" Text='<%#Eval("DepartmentName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Division" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="txt_division" runat="server" Text='<%#Eval("DivisionName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dead Line">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_DeadLine" runat="server" class="form-control form-control-sm" AutoPostBack="True" TextMode="Date" OnTextChanged="txt_DeadLine_ssOnTextChanged" Text='<%#Eval("DeadLine") %>'></asp:TextBox>
 
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Operation">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_Option" ReadOnly="true" runat="server" class="form-control form-control-sm" Text='<%#Eval("OptionInfo") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Message">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_Remarks" runat="server" class="form-control form-control-sm" Text='<%#Eval("Remarks") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                                <div class="row" style="padding: 10px;">
                                    <div class="col-md-10">
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Button ID="textButton" Text="Add To List" OnClick="textButton_OnClick" CssClass="btn btn-outline-success btn-block disabled btn-sm" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </fieldset>



                            <asp:GridView ID="SaveGridView" Width="100%" CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="false" runat="server">
                                <Columns>


                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            <asp:HiddenField runat="server" ID="txt_empInfoId" Value='<%#Eval("EmpInfoId") %>' />

                                            <%--     <asp:HiddenField runat="server" ID="HFFinancialYearId" Value='<%#Eval("FinancialYearId") %>' />--%>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Employee ID">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_empId" runat="server" Text='<%#Eval("EmpMasterCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_name" runat="server" Text='<%#Eval("EmpName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_designation" runat="server" Text='<%#Eval("Designation") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Department">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_dptName" runat="server" Text='<%#Eval("DepartmentName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Division" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_division" runat="server" Text='<%#Eval("DivisionName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dead Line">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_DeadLine" runat="server" Text='<%# Eval("DeadLine", "{0:dd-MMM-yyyy}") %>'></asp:Label>


                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Operation">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_OptionInfo" runat="server" Text='<%#Eval("OptionInfo") %>'></asp:Label>


                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Message">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_Remarks" runat="server" Text='<%#Eval("Remarks") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Delete" runat="server">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="deleteImageButtonDirectlySupervices" runat="server" OnClick="deleteImageButtonDirectlySupervices_OnClick"     OnClientClick="return confirm('Are you sure you want to delete this User?');"
                                                ImageUrl="~/Assets/img/delete.png" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>




                            <div class="form-row">
                                <div class="col-2">
                                    <div class="form-group">
                                        <asp:Button ID="btn_Save" Text="Save" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="btn_Save_OnClick" />
                                        <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                        <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />

                                        <%--<asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" Text="Submit " CssClass="btn btn-sm btn-info" />--%>
                                        <asp:Button ID="cancelButton" Visible="False" Text="Cancel" OnClick="cancelButton_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />

                                    </div>
                                </div>
                            </div>
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                        </div>
                    </div>

                    <asp:HiddenField runat="server" ID="hid_KpiMasrerId" />



                </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>


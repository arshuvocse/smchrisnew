<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="BSCOKRSelf.aspx.cs" Inherits="Appraisal_AppraisalSelfFunctional" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <style type="text/css">
        
        .SelectchkChoice label {
            padding-left: 3px; 
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
    <div class="content">

        <asp:UpdatePanel runat="server" ID="upFormBody">
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
                                BSC Setup</h1>
                        </div>

                        <%-- <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="detailsViewButton" Text="Appraisal List" OnClick="detailsViewButton_OnClick" CssClass="btn btn-sm btn-outline-secondary " runat="server"  />
                        </div>--%>

                        <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:LinkButton ID="homeButton" Style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                            <asp:LinkButton ID="detailsViewButton" Style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick"> <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
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
                                            <asp:HiddenField runat="server" ID="HFCompanyId" />
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


                            <h2 class="blue title-widget" style="color: #2196F3; text-shadow: 0 0 2px black;">A. Functional Area (100% weightage will be converted to 75 Marks)</h2>
                            <hr />

                            <asp:GridView runat="server" ShowFooter="true" AutoGenerateColumns="False" Width="100%" ID="gv_AppraisalFunc" CssClass="table table-bordered text-center thead-dark gridDatatable">

                                <Columns>

                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="BSC Dimension">
                                        <ItemTemplate>
                                             <asp:DropDownList runat="server" ID="ddlBSCDimension"  CssClass="form-control  form-control-sm">
     <asp:ListItem Value="">Select One</asp:ListItem>
     
     <asp:ListItem Value="1">Finansial </asp:ListItem>
     <asp:ListItem Value="2">Internal Process</asp:ListItem>
     <asp:ListItem Value="3">Customer</asp:ListItem>
     <asp:ListItem Value="4">Learnning</asp:ListItem> 
 </asp:DropDownList>
                                        </ItemTemplate>


                                    </asp:TemplateField> <asp:TemplateField HeaderText="Objective / Goal">
                                        <ItemTemplate>
                                              <asp:TextBox runat="server"  TextMode="MultiLine" ID="txtObjectiveGoal" CssClass="form-control" Rows="6" Text='<%#Eval("ObjectiveGoal") %>'></asp:TextBox>
                                        </ItemTemplate>


                                    </asp:TemplateField>

                                  <asp:TemplateField HeaderText=" ">
            <ItemTemplate>
                <asp:Panel ID="pnlObjectiveGoal" runat="server">
                   
                     
                    <asp:GridView ID="gvNestedTable" runat="server" AutoGenerateColumns="False" CssClass="table table-sm table-bordered nested-table">
                        <Columns>
                            <asp:TemplateField HeaderText="KPI / Measure">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtKPI" CssClass="form-control form-control-sm" Text='<%#Eval("KPI") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Weightage (%)">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtWeightage" CssClass="form-control form-control-sm" Text='<%#Eval("Weightage") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Initiatives">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtInitiatives" CssClass="form-control form-control-sm" Text='<%#Eval("Initiatives") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField> <asp:TemplateField HeaderText="Deadline">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtDeadline" TextMode="Date" CssClass="form-control form-control-sm" Text='<%#Eval("Deadline") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Add">
    <ItemTemplate>
        <asp:LinkButton ID="btn_AddrowNested" CssClass="btn btn-info btn-sm" CommandArgument='<%# Container.DataItemIndex %>' OnClick="btn_AddrowNested_Click" runat="server">
            <i class="fa fa-plus" aria-hidden="true"></i>  
        </asp:LinkButton>
    </ItemTemplate>
</asp:TemplateField>
                             <asp:TemplateField HeaderText="Remove">
    <ItemTemplate>
        <asp:LinkButton ID="btn_RmvNested" CssClass="btn btn-danger btn-sm" CommandArgument='<%# Container.DataItemIndex %>' OnClick="btn_RmvNested_Click" runat="server">
            <i class="fa fa-trash" aria-hidden="true"></i> 
        </asp:LinkButton>
    </ItemTemplate>
</asp:TemplateField>

                               
                        </Columns>
                    </asp:GridView>
 
                 <asp:LinkButton ID="btn_AddNestedRow" CssClass="btn btn-info btn-sm mt-2" OnClick="btn_AddNestedRow_OnClick" runat="server">
    <i class="fa fa-plus" aria-hidden="true"></i>   Add Sub 
</asp:LinkButton>

                </asp:Panel>
            </ItemTemplate>
        </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Add">
                                        <ItemTemplate>

                                            <asp:LinkButton ID="btn_Add" CssClass="btn btn-info btn-sm" OnClick="btn_Add_OnClick" runat="server"><i class="fa fa-plus" aria-hidden="true"></i>
                                            </asp:LinkButton>

                                        </ItemTemplate>


                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Remove">
                                        <ItemTemplate>

                                            <asp:LinkButton ID="lb_Remove" CssClass="btn btn-danger btn-sm" OnClick="lb_Remove_OnClick" runat="server"><i class="fa fa-trash" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>


                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                            <h2 class="blue title-widget" style="color: #2196F3; text-shadow: 0 0 2px black;">B.Behavioral Area (25 Marks) </h2>
                            <hr />
                            

                            <asp:GridView runat="server" ShowFooter="true" AutoGenerateColumns="False" Width="100%" ID="gv_AppraisalPartB" CssClass="table table-bordered text-left thead-dark gridDatatable">

                                <Columns>

                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    
                                            <asp:TemplateField HeaderText="" Visible="False">
                                        <ItemTemplate>
                                             <asp:RadioButtonList runat="server"  ID="rbType" CssClass="SelectchkChoice"   RepeatDirection="Vertical" RepeatLayout="Flow">
                                         <asp:ListItem>Personal</asp:ListItem>
    <asp:ListItem>Team</asp:ListItem>
    <asp:ListItem>Result Focus</asp:ListItem>
    <asp:ListItem>Interpersonal Skill</asp:ListItem>
    <asp:ListItem>Leadership</asp:ListItem> 
    <asp:ListItem>Others</asp:ListItem> 
                                    </asp:RadioButtonList>
                                            
                                               </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                    
                                            <asp:TemplateField HeaderText="Competencies / Skills">
                                        <ItemTemplate>
                                             <asp:DropDownList runat="server"  Visible="False" ID="ddlSkill" Width="300px"  CssClass="form-control  form-control-sm ccccccccccc">
                                               
                                                 </asp:DropDownList>
                            
                                            <asp:TextBox runat="server" ID="SkillInfo" CssClass="form-control" Rows="2" TextMode="MultiLine" Text='<%#Eval("SkillInfo") %>'></asp:TextBox>


                                         <%--   <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                ServiceMethod="GetKPIBehaviourByType" ServicePath="~/WebService.asmx" TargetControlID="SkillInfo"
                                                UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                ShowOnlyCurrentWordInCompletionListItem="true">
                                            </asp:AutoCompleteExtender>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Supporting Example" >
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="SupportingEmp" CssClass="form-control" Rows="2" TextMode="MultiLine" Text='<%#Eval("SupportingEmp") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Score"  >
                                        <ItemTemplate>
                                            <asp:TextBox runat="server"  ID="Score" CssClass="form-control  form-control-sm"   TextMode="Number" Text='<%#Eval("Score") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalScore" CssClass="form-control  form-control-sm " runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Expected Number"  Visible="False">
                                        <ItemTemplate>
                                            <asp:DropDownList runat="server" ID="ddlWeight" OnSelectedIndexChanged="ddlWeight_OnTextChanged" AutoPostBack="True" CssClass="form-control  form-control-sm">
                                                <asp:ListItem Value="">Select One</asp:ListItem>
                                                <asp:ListItem Value="0">0</asp:ListItem>

                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                <asp:ListItem Value="5">5</asp:ListItem>
                                            </asp:DropDownList>
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
                              <script type="text/javascript">
                                                        function pageLoad() {
                                                            $('.ccccccccccc').chosen({ disable_search_threshold: 5, search_contains: true });
                                                        }
</script>
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


                            <div class="form-row">

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


                            <div class="form-row" runat="server" id="ApprovalComments" >
                                <div class="col-md-12" runat="server" visible="false">
                                    <fieldset class="for-panel">
                                        <legend>Approval Status List</legend>
                                        <div style="overflow: scroll">
                                            <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" ID="GridView2" CssClass="table table-bordered text-center thead-dark gridDatatable">

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



                                                </Columns>
                                            </asp:GridView>
                                    </fieldset>
                                </div>

                            </div>

                            <asp:HiddenField runat="server" ID="id_mastetID" />
                            <asp:HiddenField runat="server" ID="id_Empid" />

                            <div class="form-row" runat="server" visible="false">
                                <div class="col-md-12">
                                    <link href="../UserSetup/ButtonGrups.css" rel="stylesheet" />
                                    <div class="ui-group-buttons">
                                        <asp:LinkButton runat="server" ID="btn_Save" Style="box-shadow: 0 0 3px 1px rgba(0,0,0,.35);" Visible="False" OnClick="btn_Save_OnClick" CssClass="btn btn-sm btn-info" Text="Save"></asp:LinkButton>

                                        <asp:LinkButton ID="editButton" Style="box-shadow: 0 0 3px 1px rgba(0,0,0,.35);" Text="Update" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                        <div class="or or-sm" runat="server" id="orBTN"></div>
                                        <asp:LinkButton ID="submitButton" Style="box-shadow: 0 0 3px 1px rgba(0,0,0,.35);" Text="Save/Draft" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="btn_Save_OnClick" />
                                        <asp:LinkButton ID="submitVerifyButton" Style="box-shadow: 0 0 3px 1px rgba(0,0,0,.35);" Text="Submit" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="submitVerifyButton_OnClick" />

                                        <asp:LinkButton ID="delButton" Style="box-shadow: 0 0 3px 1px rgba(0,0,0,.35);" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <%--<asp:Button ID="cancelButton" Text="Cancel" OnClick="cancelButton_Click" CssClass="btn btn-sm warning" runat="server"  BackColor="#FFCC00" />--%>
                        </div>
                    </div>
                </div>
            </ContentTemplate>

        </asp:UpdatePanel>

    </div>
</asp:Content>

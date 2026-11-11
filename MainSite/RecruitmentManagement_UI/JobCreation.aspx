<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="JobCreation.aspx.cs" Inherits="RecruitmentManagement_UI_JobCreation" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">

    <style>
        .resize {
            resize: none;
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
    </style>
    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                       
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;"><img src="../Report_Pages/app.png" width="20px"  /> Create Job Circulation</h1>
                    </div>
                   <%-- <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="detailsViewButton" Text="View List" Visible="True" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>--%>
                    
                    <div class="page-heading__container float-right d-none d-sm-block">
                        
                          <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                        <asp:LinkButton ID="detailsViewButton"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
                       
                    </div>
                    

                    <%-- <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="ListViewButton" Visible="True" Text="View List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="ListViewButton_Click"  />
                    </div>--%>
                </div>
                <!-- //END PAGE HEADING -->
                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">

                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="col-md-2">
                                            <asp:HiddenField ID="JobCreationIdHiddenField" runat="server" />
                                            <div class="form-group">
                                                <label>Company Name: </label>  &nbsp;<label style="color: #a52a2a">*</label>
                                                <asp:DropDownList ID="companyDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="companyDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:TextBox ID="JobCodetextBox" Visible="False" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                                <asp:HiddenField ID="jobIdHiddenField" runat="server" />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Employee Requisition: </label>  &nbsp;<label style="color: #a52a2a">*</label>
                                                <asp:DropDownList ID="RequisitionDropDownList" runat="server" CssClass="form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="RequisitionDropDownList_SelectedIndexChanged"></asp:DropDownList>

                                            </div>
                                        </div>
                                    </div>
                                    <fieldset class="for-panel">
                                        <legend> Employee Requisition Information</legend>
                                        <div class="row">


                                            <div class="col-md-3">


                                                <div class="form-group">
                                                    <label>Job Title:  </label>

                                                    <asp:Label ID="lblJobTitle" CssClass="label label-default" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Grade:  </label>
                                                    <asp:Label ID="lblGradeTitile" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Total Vacancy:  </label>
                                                    <asp:Label ID="lblTotalVacency" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Employee Type:   </label>
                                                    <asp:Label ID="lblEmpType" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-md-3" style="display: none;">
                                                <div class="form-group">
                                                    <label>Division:  </label>
                                                    <asp:Label ID="lbldivision" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-md-3" style="display: none;">
                                                <div class="form-group">
                                                    <label>Wing:  </label>
                                                    <asp:Label ID="lbldivWing" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Department:  </label>
                                                    <asp:Label ID="lblDepartment" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-md-3" runat="server" visible="False">


                                                <div class="form-group">
                                                    <label>Place of Posting:  </label>
                                                    <asp:Label ID="lblplaceOfPosting" runat="server" />


                                                </div>
                                            </div>
                                            <div class="col-md-3" runat="server" visible="False">
                                                <div class="form-group">
                                                    <label>Reporting to:  </label>
                                                    <asp:Label ID="lblReportTo" runat="server" />


                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Expected Date of joining:  </label>
                                                    <asp:Label ID="lblExpDtJoin" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                    <fieldset class="for-panel" runat="server" visible="False">
                                        <legend>Education Experiences</legend>
                                        <div class="row">

                                            <div class="col-md-3">

                                                <div class="form-group">
                                                    <label>Experience:  </label>

                                                    <asp:Label ID="lblExperience" CssClass="label label-default" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Skill:  </label>


                                                    <asp:Label ID="lblSkill" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-md-3">

                                                <div class="form-group">
                                                    <label>Age:  </label>
                                                    <asp:Label ID="lblAge" runat="server" />

                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Others:   </label>

                                                    <asp:Label ID="lblOthers" runat="server" />

                                                </div>
                                            </div>

                                        </div>
                                    </fieldset>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <fieldset class="for-panel" runat="server" visible="False">
                                                <legend>Location Information</legend>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <label>Job Location  </label>
                                                            &nbsp;<label style="color: #a52a2a">*</label>
                                                            <div class="row">
                                                                <div class="col-md-9">
                                                                    <asp:DropDownList ID="KeyjobLocationDropDownList" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                                                                </div>

                                                                <div class="col-md-2">

                                                                    <asp:ImageButton ID="jobLocationButton" runat="server" Text="ADD" OnClick="jobLocationButton_OnClick" CssClass="btn btn-outline-info btn-xs" ImageUrl="../Assets/img/add.png" />
                                                                </div>
                                                                <div class="col-md-1">
                                                                </div>
                                                            </div>

                                                        </div>


                                                        <div class="form-group">
                                                            <asp:GridView ID="jobLocationGridView" runat="server" CssClass="table table-bordered text-center thead-dark" Width="100%" AutoGenerateColumns="False" DataKeyNames="JobLocationID">
                                                                <Columns>
                                                                    <asp:BoundField DataField="Location" HeaderText="Job Location" ItemStyle-Width="100%"
                                                                        HtmlEncode="False">
                                                                        <ItemStyle Width="100%" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="Delete">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="locationDeleteImageButton" runat="server" OnClick="locationDeleteImageButton_OnClick"
                                                                                ImageUrl="~/Assets/img/delete.png" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                </Columns>
                                                                <%--<HeaderStyle BackColor="Maroon" ForeColor="White" Height="30px" />--%>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </div>
                                        <div class="col-md-12">
                                            <fieldset class="for-panel">
                                                <legend>Other Information</legend>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="row">
                                                            <div class="col-md-7">
                                                                <div class="form-group">
                                                                    <label>Job Context: </label>   
                                                                    <br />
                                                                    <asp:TextBox ID="jobContextTextBox" Columns="100" TextMode="MultiLine" Rows="1" runat="server" CssClass="form-control "></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-3" runat="server" Visible="False">
                                                                <div class="form-group">
                                                                    <label>Other Benefit: </label>
                                                                    <asp:TextBox ID="otherBenefitTextBox" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <div class="form-group">
                                                                    <label>Salary: </label>
                                                                    <br />
                                                                    <asp:CheckBox ID="IsSalary" CssClass="checkbox margin-right" Text="Negotiable" runat="server" />

                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <div class="form-group">
                                                            <label>Circulation Start Date: </label>  &nbsp;<label style="color: #a52a2a">*</label>
                                                            <asp:TextBox ID="circulationStartDateTextBox" CssClass="form-control form-control-sm" runat="server" OnTextChanged="circulationStartDateTextBox_TextChanged" CausesValidation="true"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server"
                                                                Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                TargetControlID="circulationStartDateTextBox" />

                                                        </div>
                                                    </div>
                                                    <div class="col-md-2">

                                                        <div class="form-group">
                                                            <label>Circulation End Date: </label>  &nbsp;<label style="color: #a52a2a">*</label>
                                                            <asp:TextBox ID="circulationEndDateTextBox" CssClass="form-control form-control-sm" runat="server" OnTextChanged="circulationEndDateTextBox_TextChanged" CausesValidation="true"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                                Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                TargetControlID="circulationEndDateTextBox" />

                                                        </div>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <div class="form-group">
                                                            <label>Probable Interview Date: </label>  
                                                            <asp:TextBox ID="probableInterviewDateTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="true" OnTextChanged="probableInterviewDateTextBox_TextChanged"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                                Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                TargetControlID="probableInterviewDateTextBox" />

                                                        </div>
                                                    </div>

                                                    <div class="form-group" style="display: none">
                                                        <label>Probable Interview Date:: </label>  &nbsp;<label style="color: #a52a2a">*</label>
                                                        <asp:TextBox ID="probableRecruitmentDateTextBox" runat="server" CausesValidation="true" Enabled="False"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                                                            Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                            TargetControlID="probableRecruitmentDateTextBox" />
                                                    </div>



                                                    <div class="col-md-3">


                                                        <div class="form-group">
                                                            <label>Remarks: </label>
                                                            <asp:TextBox ID="remarksTextBox" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>

                                                        </div>
                                                    </div>


                                                    <div class="form-group" style="display: none">
                                                        <label>Position: </label>
                                                        <asp:TextBox ID="positionTextBox" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group" style="display: none">
                                                        <label>Vacancy: </label>
                                                        <asp:TextBox ID="vacancyTextBox" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </fieldset>


                                            <fieldset class="for-panel col-md-5">
                                                <legend>Prefered Way To Circulate The Vacancy</legend>  &nbsp;<label style="color: #a52a2a">*</label>
                                                <div class="form-group">
                                                    <asp:CheckBoxList ID="CheckBoxList1" runat="server"></asp:CheckBoxList>
                                                </div>
                                            </fieldset>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <link href="../UserSetup/ButtonGrups.css" rel="stylesheet" />
                                      <div class="ui-group-buttons">
                                 <asp:Button ID="Button1" Text="Save" CssClass="btn btn-sm btn-info" OnClientClick="return confirm('Are you sure you want to Save ?')" Visible="False" runat="server" OnClick="Button2_OnClick" />
                                         <div class="or or-sm" runat="server" Visible="False" id="orBTN"></div>
                                    <asp:Button ID="submitButton" Text="Submit" CssClass="btn btn-sm btn-success" Visible="False" OnClientClick="return confirm('Are you sure you want to Submit ?')" runat="server" OnClick="saveButton_OnClick" />
                                             </div>
                                         <div class="ui-group-buttons">
                                    <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClientClick="return confirm('Are you sure you want to Update ?')" OnClick="editButton_OnClick" />
                                               <div class="or or-sm" runat="server" Visible="False" id="orUp"></div>
                                         <asp:Button ID="btnUpdateforSubmit" Text="Submit" OnClientClick="return confirm('Are you sure you want to Submit ?')" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="btnUpdateforSubmit_OnClick" />
                                             
                                              </div>
                                    <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger"  OnClientClick="return confirm('Are you sure you want to Delete ?')" Visible="False" runat="server" OnClick="delButton_OnClick" />
                                        

                                        <%--<asp:Button ID="saveButton" runat="server" CssClass="btn btn-sm btn-info" Text="Save" OnClick="saveButton_OnClick" />--%>
                                        <asp:Button ID="cancelButton" Text="Cancel" Visible="False" CssClass="btn btn-sm warning" OnClick="cancelButton_OnClick"  runat="server" BackColor="#FFCC00" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row" style="display: none">
                            <div class="col-md-1">
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Educational Requirements: </label>
                                    <asp:TextBox ID="educationalRequirementsTextBox" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>Job Responsibility: </label>
                                    <asp:TextBox ID="jobResponsibilityTextBox" runat="server" CssClass="form-control resize" Rows="1" Columns="20" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Employeement Type: </label>

                                    <asp:CheckBoxList ID="employmentTypeCheckBoxList" runat="server">
                                        <asp:ListItem Value="parmanent" Text="Parmanent"></asp:ListItem>
                                        <asp:ListItem Value="contractual" Text="Contractual"></asp:ListItem>
                                        <asp:ListItem Value="trainee" Text="Trainee"></asp:ListItem>
                                        <asp:ListItem Value="casual" Text="Casual"></asp:ListItem>
                                    </asp:CheckBoxList>

                                </div>

                                <div class="form-group pull-left">
                                    <label>* </label>
                                    <asp:Button ID="eduReqButton" runat="server" Text="ADD" OnClick="eduReqButton_OnClick" />
                                </div>
                            </div>

                            <div class="col-md-4">
                            </div>
                        </div>

                        <div class="row" style="display: none">
                            <div class="col-md-1">
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:GridView ID="educationalRequirementsGridView" runat="server" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="EducationRequirements" HeaderText="Degree Title" ItemStyle-Width="100%"
                                                HtmlEncode="False"></asp:BoundField>
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="deleteImageButton" runat="server" OnClick="deleteImageButton_OnClick"
                                                        ImageUrl="~/Assets/img/delete.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle BackColor="Maroon" Font-Bold="True" ForeColor="White" Height="30px" />
                                    </asp:GridView>
                                </div>

                            </div>


                            <div class="col-md-4">
                            </div>
                        </div>



                        <div class="row" style="display: none">
                            <div class="col-md-1">
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Job Location: </label>

                                </div>

                                <div class="form-group">
                                    <label>Experience Requirements: </label>
                                    <asp:TextBox ID="experienceRequirementsTextBox" runat="server" CssClass="form-control resize" Rows="1" Columns="20" TextMode="MultiLine"></asp:TextBox>
                                </div>


                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Additional Requirements: </label>
                                    <asp:TextBox ID="additionalRequirementsTextBox" runat="server" TextMode="MultiLine" CssClass="form-control resize" Rows="1" Columns="20"></asp:TextBox>
                                </div>

                                <div class="form-group">
                                    <label>* </label>

                                </div>


                            </div>

                            <div class="col-md-4">
                            </div>
                        </div>
                        <div class="row" style="display: none">
                            <div class="col-md-1">
                            </div>
                            <div class="col-md-6">

                                <div class="form-group">
                                </div>
                            </div>


                            <div class="col-md-4">
                            </div>
                        </div>

                        <div class="row" style="display: none">
                            <div class="col-md-1">
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Degree: </label>
                                    <asp:TextBox ID="degree" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>* </label>
                                    <asp:Button ID="addDegreeButton" runat="server" Text="ADD" OnClick="addDegreeButton_OnClick" />
                                </div>
                            </div>
                            <div class="col-md-3">
                            </div>
                            <div class="col-md-4">
                            </div>
                        </div>



                        <div class="row" style="display: none">
                            <div class="col-md-1">
                            </div>
                            <div class="col-md-3">

                                <div class="form-group">
                                    <label>Employeement Type: </label>
                                    <asp:CheckBoxList ID="declarationSourceCheckBoxList" runat="server">
                                        <asp:ListItem Value="newspaper" Text="Newspaper"></asp:ListItem>
                                        <asp:ListItem Value="tv" Text="TV"></asp:ListItem>
                                        <asp:ListItem Value="other" Text="Other"></asp:ListItem>
                                    </asp:CheckBoxList>
                                </div>

                                <div class="form-group">
                                    <label>If(Other): </label>
                                    <asp:TextBox ID="otherdeclarationSourceTextBox" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                </div>


                            </div>
                            <div class="col-md-2">

                                <div class="form-group">
                                    <label>Salary: </label>

                                    <asp:TextBox ID="salaryTextBox" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
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


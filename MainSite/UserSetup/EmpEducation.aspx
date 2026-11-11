<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="EmpEducation.aspx.cs" Inherits="UserSetup_EmpEducation" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register Assembly="EditableDropDownList" Namespace="EditableControls" TagPrefix="editable" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <style>
        #cpFormBody_gv_Education > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }

        #cpFormBody_gv_Education > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }

          .chkChoice label {
            padding-left: 10px;
            padding-right: 30px;
        }
    </style>
    <div class="container-fluid">
        <div class="page-heading">
            <div class="page-heading__container">
                <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" />Update Information </h1>
            </div>
            <div class="page-heading__container">
                <asp:Label runat="server" ID="lbl_Mode"></asp:Label>
                <span>
                    <label class="btn infoN" style="font-size: 13px;">Employee ID:  &nbsp;
                        <asp:Label runat="server" class="label w3-tag w3-green" Style="font-size: 13px;" ID="empMasterCode"></asp:Label></label>
                   
                    
                  <label class="btn infoN" style="font-size: 13px;">Employee Name: &nbsp; 
                      <asp:Label runat="server" class="label w3-tag w3-green" Style="font-size: 13px;" ID="lblEmpName"></asp:Label></label>
                    
                      <label class="btn infoN" style="font-size: 11px;">Designation: &nbsp; 
                      <asp:Label runat="server" class="label w3-tag w3-green" Style="font-size: 9px;" ID="lblDesignation"></asp:Label></label>
                </span>
            </div>
            <div class="page-heading__container float-right d-none d-sm-block">
                
                   <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                        <asp:LinkButton ID="LinkButton1"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
              <%--  <asp:Button ID="detailsViewButton" Text="Update Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                  <asp:Button ID="btnEditInfo" Text="Back to List" Visible="False"  CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="btnEditInfo_OnClick" />--%>
            </div>
        </div>



        <div class="card">
            <div class="card-body">

                <style>
                            .NavbarAcc {
                                color: white!important;
    background-color: #5078B3!important;
    font-family: Arial, Sans-Serif!important;
    font-size: 14px!important;
    font-weight: bold!important;
    padding: 10px!important;
    margin-top: 5px!important;
    cursor: pointer!important;
                            }
                        </style>
                        <nav class="navbar navbar-light bg-light NavbarAcc">
 <span>5. Education</span>
</nav>
                 
              
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <br />
                                         

                                        <div class="form-row">
                                            
                                            
                                              <div class="col-3" runat="server" Visible="False">
                                                <div class="form-group">
                                                    <label>Name of Education (Search) </label>
                                                    <asp:TextBox runat="server" ID="txtEducationName" class="form-control form-control-sm" AutoPostBack="True" OnTextChanged="txtEducationName_OnTextChanged">  
                                                    </asp:TextBox>
                                                     <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters=""
                                                    EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                    ServiceMethod="GetNameofEducationWS" ServicePath="~/WebService.asmx" TargetControlID="txtEducationName"
                                                    UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                    ShowOnlyCurrentWordInCompletionListItem="true">
                                                </cc1:AutoCompleteExtender>
                                           
                                                </div>
                                            </div>


                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Name of Education</label>
                                                    <asp:DropDownList runat="server" ID="ddlEducationName" class="form-control form-control-sm clsSelect">
                                                    </asp:DropDownList>
                                                  <script type="text/javascript">
                                                      function pageLoad() {
                                                          $('.clsSelect').chosen({ disable_search_threshold: 5, search_contains: true });







                                                      }
</script>
                                                </div>
                                            </div>
                                            
                                            
                                                 
                                              <div class="col-3"  runat="server" Visible="False">
                                                <div class="form-group">
                                                    <label>Board/University (Search) </label>
                                                    <asp:TextBox runat="server" ID="txtBoardUniversity" class="form-control form-control-sm" AutoPostBack="True" OnTextChanged="BoardUniversity_OnTextChanged">  
                                                    </asp:TextBox>
                                                     <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                    EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                    ServiceMethod="GetBoardUniversityWS" ServicePath="~/WebService.asmx" TargetControlID="txtBoardUniversity"
                                                    UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                    ShowOnlyCurrentWordInCompletionListItem="true">
                                                </cc1:AutoCompleteExtender>
                                           
                                                </div>
                                            </div>

                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Board/University</label>
                                                    <asp:DropDownList runat="server" ID="ddlBoardUniversity" class="form-control form-control-sm clsSelect">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Subject/Group</label>
                                                    <asp:DropDownList runat="server" ID="ddlSubjectGroup" class="form-control form-control-sm clsSelect" >
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            
                                            
                                                    
                                              <div class="col-3" runat="server" Visible="False">
                                                <div class="form-group">
                                                    <label>Educational Institute (Search) </label>
                                                    <asp:TextBox runat="server" ID="txtEducationalInstitute" class="form-control form-control-sm" AutoPostBack="True" OnTextChanged="txtEducationalInstitute_OnTextChanged">  
                                                    </asp:TextBox>
                                                     <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters=""
                                                    EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                    ServiceMethod="GetEducationalInstituteWS" ServicePath="~/WebService.asmx" TargetControlID="txtEducationalInstitute"
                                                    UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                    ShowOnlyCurrentWordInCompletionListItem="true">
                                                </cc1:AutoCompleteExtender>
                                           
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Educational Institute</label>
                                                    <asp:DropDownList runat="server" ID="ddlEducationalInstitute" class="form-control form-control-sm clsSelect">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Field of specialization</label>
                                                    <asp:DropDownList runat="server" ID="ddlSpecialization" class="form-control form-control-sm clsSelect">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Passing Year</label>
                                                    <asp:TextBox runat="server" ID="txt_PassingYear" class="form-control form-control-sm" />
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                        Enabled="True" TargetControlID="txt_PassingYear" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Result</label>
                                                    <asp:DropDownList runat="server" ID="txt_Result" class="form-control form-control-sm clsSelect" />

                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>CGPA/Total Marks</label>
                                                    <asp:TextBox runat="server" ID="txt_CGPAMarks" class="form-control form-control-sm" />

                                                </div>
                                            </div>

                                            <div class="col-3" style="padding-top: 25px;">
                                                <div class="form-group">
                                                    <asp:CheckBox runat="server" CssClass="chkChoice"  ID="chk_EduIsLastLevel" Text="Is Last Level" />
                                                </div>
                                            </div>

                                            <div class="col-3" style="padding-top: 25px;">
                                                <div class="form-group">
                                                    <asp:CheckBox runat="server" CssClass="chkChoice" ID="chk_IsProfessionalEdu" Text="Is Professional Education" />
                                                </div>
                                            </div>

                                            <div class="col-3" style="margin-top: 25px;">
                                                <div class="form-group">
                                                    <asp:Button runat="server" ID="btnAddEducation" CssClass="btn btn-outline-success btn-sm" Text="Add Education" OnClick="btnAddEducation_OnClick" />
                                                </div>
                                            </div>

                                        </div>
                                        <br/>
                                        <br/>

                                        <div>
                                            <div style="overflow: scroll;">
                                                <asp:GridView Width="100%" ID="gv_Education" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL#">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                                <asp:HiddenField ID="EmpEducationId" runat="server" Value='<%#Eval("EmpEducationId") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Education Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_EducationName" runat="server" Text='<%#Eval("EducationName") %>'></asp:Label>
                                                                <asp:HiddenField ID="EducationNameId" runat="server" Value='<%#Eval("EducationNameId") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Board/University">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_BoardUniversity" runat="server" Text='<%#Eval("BoardUniversity") %>'></asp:Label>
                                                                <asp:HiddenField ID="BoardUniversityId" runat="server" Value='<%#Eval("BoardUniversityId") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Subject/Group">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_SubjectGroup" runat="server" Text='<%#Eval("SubjectGroup") %>'></asp:Label>
                                                                <asp:HiddenField ID="SubjectGroupId" runat="server" Value='<%#Eval("SubjectGroupId") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Educational Institute">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_EducationalInstitute" runat="server" Text='<%#Eval("EducationalInstitute") %>'></asp:Label>
                                                                <asp:HiddenField ID="EducationalInstituteId" runat="server" Value='<%#Eval("EducationalInstituteId") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Field Of Specialization">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_FieldOfSpecialization" runat="server" Text='<%#Eval("FieldOfSpecialization") %>'></asp:Label>
                                                                <asp:HiddenField ID="FieldOfSpecializationId" runat="server" Value='<%#Eval("FieldOfSpecializationId") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="PassingYear">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_PassingYear" runat="server" Text='<%#Eval("PassingYear") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Result">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Result" runat="server" Text='<%#Eval("Result") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="CGPA/TotalMarks">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_CgpaOrTotalMarks" runat="server" Text='<%#Eval("CgpaOrTotalMarks") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Last Level">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_EduIsLastLevel" runat="server" Text='<%#Eval("EduIsLastLevel") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Professional Education">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_IsProfessionalEdu" runat="server" Text='<%#Eval("IsProfessionalEdu") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Edit">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lb_EditEducation" runat="server" OnClick="lb_EditEducation_OnClick"><img src="../Assets/img/rsz_edit.png" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Remove">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lb_RemoveEducation" runat="server" OnClick="lb_RemoveEducation_OnClick"><img src="../Assets/img/delete.png" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>

                                        <br />
                                        <div class="form-row">
                                            <div class="col-md-10">
                                                <asp:HiddenField runat="server" ID="hdpk" />


                                                <%--  <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                                    <ContentTemplate>--%>


                                                <%--<link href="//netdna.bootstrapcdn.com/bootstrap/3.0.0/css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css">--%>
                                                <link href="ButtonGrups.css" rel="stylesheet" />
                                                <div class="ui-group-buttons">

                                                    <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" OnClientClick="return confirm('Are you sure you want to Save ?')" Text="  Save  " CssClass="btn btn-sm btn-info" />
                                                    <div class="or or-sm"></div>
                                                    <asp:Button runat="server" ID="btnNext" OnClick="btn_Next_OnClick" Text="   Save & Next   " OnClientClick="return confirm('Are you sure you want to Save & Next ?')" CssClass="btn btn-sm btn-success" />

                                                    <div class="or or-sm"></div>
                                                    <asp:Button ID="cancelButton" Text="  Exit  " OnClick="cancelButton_OnClick" OnClientClick="return confirm('Are you sure you want to Exit ?')" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                                                </div>
                                            </div>

                                            <div class="col-md-2">
                                                <asp:LinkButton CssClass="hh previous" OnClick="lbPrevious_OnClick" ID="lbPrevious" runat="server">&laquo; Previous</asp:LinkButton>
                                                <asp:LinkButton CssClass="hh next" runat="server" ID="lblNext" OnClick="lblNext_OnClick">Next &raquo;</asp:LinkButton>

                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                 
            </div>
        </div>

    </div>
</asp:Content>


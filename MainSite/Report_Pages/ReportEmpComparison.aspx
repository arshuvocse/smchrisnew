<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="ReportEmpComparison.aspx.cs" Inherits="Report_Pages_ReportEmpComparison" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">


    <style type="text/css">
        /*AutoComplete flyout */
     
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
              .tblTHColorChang {
                                                background-color: #EDF2F5 !important;
                                                font-weight: bold;
                                                font-size: 15px;
                                            }

                   #cpFormBody_GVExistingProject  > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }

       #cpFormBody_GVExistingProject > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }
       .lblDes {
           margin-bottom: 5px;
           padding-top: 4px;
font-weight: 500;
font-size: 14px;
       }
       
 
.table > tbody > tr > td {
    font-size: 13px;
}
    </style>


    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                      <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px"
                   />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                        
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">  <img src="app.png"  width="18px" /> Employee Comparison Report </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                          <asp:Button ID="homeButton" Visible="False" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                        <asp:Button ID="detailsViewButton" Visible="False" Text="View List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>

                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body" style="background-color: white">
                          
                            <div class="row">
                                <div class="col-md-12">
                                      <table class="table table-bordered table-striped" id="tblInfo">
                                               

                                          
                                            <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Company Name</td>
                                                    <td>
                                                 <asp:Label ID="lblComName" runat="server" Text=""></asp:Label></td>


                                                  
                                                    <td>
                                                      <asp:Label ID="lblComName2" runat="server" Text=""></asp:Label></td>
                                                </tr>
                                          
                                          
                                                <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Employee ID</td>
                                                    <td>
                                                 <asp:Label ID="lblEmployeeCode" runat="server" Text=""></asp:Label></td>


                                                  
                                                    <td>
                                                      <asp:Label ID="lblEmployeeCode2" runat="server" Text=""></asp:Label></td>
                                                </tr>
                                          
                                                
                            
                                          
                                                <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Employee Name</td>
                                                    <td>
                                                 <asp:Label ID="lblEmp" runat="server" Text=""></asp:Label></td>


                                                  
                                                    <td>
                                                      <asp:Label ID="lblEmp2" runat="server" Text=""></asp:Label></td>
                                                </tr>
                                          
                                       
                                          
                                          
                                                <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Designation</td>
                                                    <td>
                                                 <asp:Label ID="lblDesignation" runat="server" Text=""></asp:Label></td>


                                                  
                                                    <td>
                                                      <asp:Label ID="lblDesignation2" runat="server" Text=""></asp:Label></td>
                                                </tr>
                                          
                           
                                        
                                                <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Joining Date</td>
                                                    <td>
                                                 <asp:Label ID="lblJdate" runat="server" Text=""></asp:Label></td>


                                                  
                                                    <td>
                                                      <asp:Label ID="lblJdate2" runat="server" Text=""></asp:Label></td>
                                                </tr>
                                          
                                   
                                          
                                             <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Reporting Boss</td>
                                                    <td>
                                                 <asp:Label ID="lblRepBoss" runat="server" Text=""></asp:Label></td>


                                                  
                                                    <td>
                                                      <asp:Label ID="lblRepBoss2" runat="server" Text=""></asp:Label></td>
                                                </tr>
                                          
                                           
                            
                           
                                                    <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Division</td>
                                                    <td>
                                                 <asp:Label ID="lblDivision" runat="server" Text=""></asp:Label></td>


                                                  
                                                    <td>
                                                      <asp:Label ID="lblDivision2" runat="server" Text=""></asp:Label></td>
                                                </tr>
                                          
                                          
                                          
                                                    <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Department</td>
                                                    <td>
                                                 <asp:Label ID="lblDepartment" runat="server" Text=""></asp:Label></td>


                                                  
                                                    <td>
                                                      <asp:Label ID="lblDepartment2" runat="server" Text=""></asp:Label></td>
                                                </tr>
                                          
                                          
                                          
                                                    <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Section</td>
                                                    <td>
                                                 <asp:Label ID="lblSection" runat="server" Text=""></asp:Label></td>


                                                  
                                                    <td>
                                                      <asp:Label ID="lblSection2" runat="server" Text=""></asp:Label></td>
                                                </tr>
                                          
                                          
                                          
                                                    <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Employee Category</td>
                                                    <td>
                                                 <asp:Label ID="lblCat" runat="server" Text=""></asp:Label></td>


                                                  
                                                    <td>
                                                      <asp:Label ID="lblCat2" runat="server" Text=""></asp:Label></td>
                                                </tr>
                                          
                                          
                                          
                                                    <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Salary Grade</td>
                                                    <td>
                                                 <asp:Label ID="lblSalaryGrade" runat="server" Text=""></asp:Label></td>


                                                  
                                                    <td>
                                                      <asp:Label ID="lblSalaryGrade2" runat="server" Text=""></asp:Label></td>
                                                </tr>
                                          
                                          
                                              <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Salary Step</td>
                                                    <td>
                                                 <asp:Label ID="lblSalaryStep" runat="server" Text=""></asp:Label></td>


                                                  
                                                    <td>
                                                      <asp:Label ID="lblSalaryStep2" runat="server" Text=""></asp:Label></td>
                                                </tr>
                                          
                                          
                                              <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;"> present salary</td>
                                                    <td>
                                                 <asp:Label ID="lblGrossSalary" runat="server" Text=""></asp:Label></td>


                                                  
                                                    <td>
                                                      <asp:Label ID="lblGrossSalary2" runat="server" Text=""></asp:Label></td>
                                                </tr>
                                          
                                          
                                              <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Office</td>
                                                    <td>
                                                 <asp:Label ID="lblOffice" runat="server" Text=""></asp:Label></td>


                                                  
                                                    <td>
                                                      <asp:Label ID="lblOffice2" runat="server" Text=""></asp:Label></td>
                                                </tr>
                                          
                                          
                                              <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Place</td>
                                                    <td>
                                                 <asp:Label ID="lblPlace" runat="server" Text=""></asp:Label></td>


                                                  
                                                    <td>
                                                      <asp:Label ID="lblPlace2" runat="server" Text=""></asp:Label></td>
                                                </tr>
                                          
                                              <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Total Year of Experience</td>
                                                    <td>
                                                 <asp:Label ID="lblExp" runat="server" Text=""></asp:Label></td>


                                                  
                                                    <td>
                                                      <asp:Label ID="lblExp2" runat="server" Text=""></asp:Label></td>
                                                </tr>
                                          
                                             <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">last Organization</td>
                                                    <td>
                                                 <asp:Label ID="lblOrg" runat="server" Text=""></asp:Label></td>


                                                  
                                                    <td>
                                                      <asp:Label ID="lblOrg2" runat="server" Text=""></asp:Label></td>
                                                </tr>
                                          
                                              <tr>
                                                  
                                             <tr>
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;"> Last Promotion/ Upgradation Date</td>
                                                    <td>
                                                 <asp:Label ID="lblLastPromDate" runat="server" Text=""></asp:Label></td>


                                                  
                                                    <td>
                                                      <asp:Label ID="lblLastPromDate2" runat="server" Text=""></asp:Label></td>
                                                </tr>
                                          
                                              <tr>
                                                  
                                                
                                                    <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Education</td>
                                                    <td>
                                               
                                                    <div style="overflow: scroll; width: 550px!important">
                                        <asp:GridView Width="100%" ID="gv_Education" runat="server" ShowFooter="true" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#" Visible="False">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                        <asp:HiddenField ID="EmpEducationId" runat="server" Value='<%#Eval("EmpEducationId") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Education Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_EducationName" runat="server" Text='<%#Eval("EducationName") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Board/University">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_BoardUniversity" runat="server" Text='<%#Eval("BoardUniversity") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Subject/Group">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_SubjectGroup" runat="server" Text='<%#Eval("SubjectGroup") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Educational Institute">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_EducationalInstitute" runat="server" Text='<%#Eval("EducationalInstitute") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Field Of Specialization">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_FieldOfSpecialization" runat="server" Text='<%#Eval("FieldOfSpecialization") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Passing Year">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_PassingYear" runat="server" Text='<%#Eval("PassingYear") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Result">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Result" runat="server" Text='<%#Eval("Result") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="CGPA Or Total Marks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_CgpaOrTotalMarks" runat="server" Text='<%#Eval("CgpaOrTotalMarks") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Is Last Level">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_EduIsLastLevel" runat="server" Text='<%#Eval("EduIsLastLevel") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                                    </td>


                                                  
                                                    <td>
                                                  
                                                    <div style="overflow: scroll;width: 550px!important">
                                        <asp:GridView Width="100%" ID="gv_Education2" runat="server" ShowFooter="true" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#" Visible="False">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                        <asp:HiddenField ID="EmpEducationId" runat="server" Value='<%#Eval("EmpEducationId") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Education Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_EducationName" runat="server" Text='<%#Eval("EducationName") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Board/University">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_BoardUniversity" runat="server" Text='<%#Eval("BoardUniversity") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Subject/Group">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_SubjectGroup" runat="server" Text='<%#Eval("SubjectGroup") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Educational Institute">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_EducationalInstitute" runat="server" Text='<%#Eval("EducationalInstitute") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Field Of Specialization">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_FieldOfSpecialization" runat="server" Text='<%#Eval("FieldOfSpecialization") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Passing Year">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_PassingYear" runat="server" Text='<%#Eval("PassingYear") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Result">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Result" runat="server" Text='<%#Eval("Result") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="CGPA Or Total Marks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_CgpaOrTotalMarks" runat="server" Text='<%#Eval("CgpaOrTotalMarks") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Is Last Level">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_EduIsLastLevel" runat="server" Text='<%#Eval("EduIsLastLevel") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                                    </td>
                                                </tr>
                                          
                                </div>
                            </div>
                                    <div class="row">
                                       <div class="col-md-2" style="padding-top: 85px;">
                                           <label style="font-size: 20px;">  </label>
                                       </div>
                                        <div class="col-md-4">
                                            <div class="row">
                                                 <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Company Name </label> &nbsp;<label style="color: #a52a2a">*</label>
                                            <asp:DropDownList ID="companyDropDownList" class="form-control form-control-sm" OnSelectedIndexChanged="companyDropDownList_OnSelectedIndexChanged" runat="server" AutoPostBack="True"></asp:DropDownList>
                                            
                                                    <script type="text/javascript">
                                                        function pageLoad() {
                                                            $('.selectme').chosen({ disable_search_threshold: 5, search_contains: true });

                                                        }
                                                        </script>
                                        </div>
                                            
                                       
                                    </div>
                                   
                                            </div>
                                            
                                            <div class="row">
                                                 <div class="col-md-10">
                                        <div class="form-group">
                                            <label>Search Employee: </label> &nbsp;<label style="color: #a52a2a">*</label>
                                            <asp:DropDownList   runat="server"   ID="ddlEmp1" AutoPostBack="True" OnTextChanged="ddlEmp1_OnTextChanged" class="form-control form-control-sm selectme" />

                                       
                                        </div>
                                    </div>
                                            </div>
                                        </div>
                                           <div class="col-md-4">
                                            <div class="row" style="padding-top:58px;">
                                            
                                   
                                            </div>
                                            
                                            <div class="row">
                                                 <div class="col-md-10">
                                        <div class="form-group">
                                            <label>Search Employee: </label> &nbsp;<label style="color: #a52a2a">*</label>
                                            
                                               <asp:DropDownList   runat="server"   ID="ddlEmp2" AutoPostBack="True" OnTextChanged="ddlEmp2_OnTextChanged" class="form-control form-control-sm selectme" />
                                        <%--    <asp:TextBox ID="SearchEmployeeNameTextBoxTextBox2" AutoPostBack="True" runat="server" class="form-control form-control-sm" OnTextChanged="SearchEmployeeNameTextBoxTextBox2_OnTextChanged"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters=""
                                                EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                ServiceMethod="GetCompanyWiseEmployeeInfoForIDANdNae" ServicePath="~/WebService.asmx" TargetControlID="SearchEmployeeNameTextBoxTextBox2"
                                                UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                ShowOnlyCurrentWordInCompletionListItem="true">
                                            </cc1:AutoCompleteExtender>

                                            <asp:HiddenField ID="repEmpIdHiddenField2" runat="server" />--%>

                                            <%--<asp:AutoCompleteExtender
                                                            ID="at_txt_JobCirculation"
                                                            TargetControlID="EmployeeNameTextBox"
                                                            runat="server"
                                                            ServiceMethod="GetParticipantList"
                                                            ServicePath="~/WebService.asmx"
                                                            MinimumPrefixLength="2"
                                                            CompletionInterval="10"
                                                            EnableCaching="false"
                                                            CompletionSetCount="1"
                                                            FirstRowSelected="false">
                                                        </asp:AutoCompleteExtender>--%>
                                            <asp:HiddenField ID="HiddenField2" runat="server" />
                                        </div>
                                    </div>
                                            </div>
                                        </div>

                                    </div>
                                
                            
                            
                            <div class="row lblDes" >
                                
                              
                            </div>
                            
                          
                            
                             
                            
                            
                  
                                         
                                </div>
                                 
                               
                      
                            
          </div>
                        </div>
                     
     </div>
          </ContentTemplate>
                                           </asp:UpdatePanel>
        </div>
</asp:Content>


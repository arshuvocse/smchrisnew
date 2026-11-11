<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MainMasterPage.master"  CodeFile="EmployeeInfoListUpdate.aspx.cs" Inherits="UserSetup_EmployeeInfoListUpdate" EnableEventValidation="false" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>


<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">

      <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cpFormBody" runat="Server">
    
    
       <style type="text/css">
         .chkChoice label {
            padding-left: 4px; 
        }
    </style>
    
       <style>
           .table   thead th {
               background-color: #5B799E;
               color: white;
                font-size: 13px !important;
                                            font-family: "Times New Roman", Times, serif !important;
                                            font-style: italic !important;
                                            font-weight: bold!important;
           }


          
                                              
                                              .dt-button.buttons-print,
                                               .dt-button.buttons-excel.buttons-html5,
                                               .dt-button.buttons-pdf.buttons-html5 {
                                                   background-color: white!important;
                                                    color:#880e4f !important;
                                                   border: none!important;
                                                  
                                                   padding: 5px 18px!important;
                                                   text-align: center!important;
                                                   text-decoration: none!important;
                                                   display: inline-block!important;
                                                   font-size: 16px!important;
                                                   margin: 2px 1px!important;
                                                   cursor: pointer!important;
                                                   -webkit-transition-duration: 0.4s!important;
                                                   transition-duration: 0.4s!important;
                                                   box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2), 0 3px 10px 0 rgba(0,0,0,0.19)!important;
                                               }


                                              .dt-buttons {
                                                  align-content: center;
                                                  text-align: right;
                                                  margin-top: -50px;
                                              }
                                              .dt-button.buttons-excel.buttons-html5:hover,
                                              .dt-button.buttons-pdf.buttons-html5:hover {
                                                  box-shadow: 0 12px 16px 0 rgba(0,0,0,0.24),0 17px 50px 0 rgba(0,0,0,0.19)!important;
                                                  color:white!important;
              background-color: #880e4f !important;
                                              }
                                         
       </style>


    <div class="content" id="content">

        <style>
      

        </style>
        <div class="container-fluid">
            <div class="page-heading">
                <div class="page-heading__container">
                    <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" />&nbsp;Update Employee Information</h1>
                </div>
                <div class="page-heading__container float-right d-none d-sm-block">
                  
                    <%--<asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="HomeButton_OnClick" />--%>
                    
                     <%-- <asp:Button ID="btnEditInfo" Text="View List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="btnEditInfo_OnClick" />
                    <asp:Button ID="btn_New" Text="Create New" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="btn_New_OnClick" />--%>
                    
                       <asp:LinkButton ID="mmm" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="mmm_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                    
                     <asp:LinkButton ID="btnEditInfo"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-info " runat="server" OnClick="btnEditInfo_OnClick" > <i class="fa fa-list"></i>&nbsp;View List</asp:LinkButton>
                    
                    
                     <asp:LinkButton ID="btn_New"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="btn_New_OnClick" > <i class="fa fa-plus"></i>&nbsp;Add New</asp:LinkButton>
                    
                   <%--   <asp:Button ID="btnEditInfo" Text="View List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="btnEditInfo_OnClick" />
                    <asp:Button ID="btn_New" Text="Create New" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="btn_New_OnClick" />--%>


                </div>
            </div>
            <div class="container-fluid">
                <div class="card">
                   <%-- <asp:UpdatePanel runat="server" ID="uppa">
                        <ContentTemplate>--%>
                          <%--  <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="uppa">
                                <ProgressTemplate>
                                    <div class="modal">
                                        <div class="center">
                                            <img alt="" src="/Assets/loader.gif" />
                                        </div>
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>--%>
                            <div class="card-body">
                                <asp:UpdatePanel runat="server" ID="uppa">
                        <ContentTemplate>
                            
                             <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                                <div class="form-row">
                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>Company</label>
                                            <label style="color: #a52a2a">*</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" class="form-control form-control-sm" />
                                        </div>
                                    </div>
                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>Division</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDivision" class="form-control form-control-sm" OnSelectedIndexChanged="ddlDivision_OnSelectedIndexChanged" />
                                                     <script type="text/javascript">
                                                         function pageLoad() {
                                                             $('#cpFormBody_ddlDivision').chosen({ disable_search_threshold: 5, search_contains: true });

                                                             $('#cpFormBody_ddlDepartment').chosen({ disable_search_threshold: 5, search_contains: true });
                                                             $('#cpFormBody_ddlWing').chosen({ disable_search_threshold: 5, search_contains: true });
                                                             $('#cpFormBody_ddlEmpInfo').chosen({ disable_search_threshold: 5, search_contains: true });
                                                             $('#cpFormBody_ddlDesignation').chosen({ disable_search_threshold: 5, search_contains: true });
                                                             $('#cpFormBody_ddlSalaryLocation').chosen({ disable_search_threshold: 5, search_contains: true });



                                                         }
</script>

                                        </div>
                                    </div>
                                    <div class="col-2" runat="server" id="wing" visible="False">
                                        <div class="form-group">
                                            <label>Wing</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlWing" class="form-control form-control-sm" OnSelectedIndexChanged="ddlWing_OnSelectedIndexChanged" />

                                        </div>
                                    </div>
                                    <div class="col-2" runat="server" id="dept">
                                        <div class="form-group">
                                            <label>Department</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDepartment" class="form-control form-control-sm" OnSelectedIndexChanged="ddlDepartment_OnSelectedIndexChanged" />

                                        </div>
                                    </div>
                                    <div class="col-2" runat="server" id="sec" visible="False">
                                        <div class="form-group">
                                            <label>Section</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlSection" class="form-control form-control-sm" OnSelectedIndexChanged="ddlSection_OnSelectedIndexChanged" />

                                        </div>
                                    </div>
                                    <div class="col-2" runat="server" id="subsec" visible="False">
                                        <div class="form-group">
                                            <label>Sub Section</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlSubSection" class="form-control form-control-sm" OnSelectedIndexChanged="ddlSubSection_OnSelectedIndexChanged" />

                                        </div>
                                    </div>

                                    <div class="col-md-2">

                                        <div class="form-group">
                                            <label>Employee ID: </label>
                                            <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="True" placeholder=" Employee ID" CssClass="form-control form-control-sm"
                                                OnTextChanged="EmployeeDropDownList2_SelectedIndexChanged"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters=""
                                                    EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                    ServiceMethod="GetCompanyWiseEmployeeInfoForIDANdNae" ServicePath="~/WebService.asmx" TargetControlID="txtSearch"
                                                    UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                    ShowOnlyCurrentWordInCompletionListItem="true">
                                                </cc1:AutoCompleteExtender>

                                        </div>
                                    </div>


                                    <div class="col-md-2">

                                        <div class="form-group">
                                            <label>Employee Name: </label>
                                            
                                               <asp:DropDownList runat="server" ID="ddlEmpInfo" class="form-control form-control-sm" />
                                            <asp:TextBox ID="NameTextBox" runat="server" AutoPostBack="True" placeholder=" Employee Name" Visible="False" CssClass="form-control form-control-sm" OnTextChanged="EmployeeDropDownList_SelectedIndexChanged"></asp:TextBox>
                                            
                                               <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                    EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                    ServiceMethod="GetCompanyWiseEmployeeInfoForIDANdNae" ServicePath="~/WebService.asmx" TargetControlID="NameTextBox"
                                                    UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                    ShowOnlyCurrentWordInCompletionListItem="true">
                                                </cc1:AutoCompleteExtender>

                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Designation</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDesignation" class="form-control form-control-sm" />

                                        </div>
                                    </div>
                                </div>
                             </ContentTemplate>
                    </asp:UpdatePanel>
                             
                                           <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                        <ContentTemplate>
                                   <div class="row">


                           
                           
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Office</label>
                                            <asp:DropDownList runat="server" ID="ddlSalaryLocation" class="form-control form-control-sm" />

                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Confirmation Status</label>
                                            <asp:DropDownList runat="server" ID="ddlConformationStatus" CssClass="form-control form-control-sm">
                                                <asp:ListItem Text="Select..." Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                       
                                        <div class="col-md-2">
                                        <div class="form-group">
                                               <asp:CheckBox runat="server" ID="SSGradeCheck" CssClass="chkChoice"  Text="Special Transfer" />
                                            </div>
                                            </div>
                                      <style>
                                    .btnexcel {
                                         background-color: #4CAF50;
                                        border: none;
                                        color: white;
                                        padding: 8px 12px;
                                        text-align: center;
                                        text-decoration: none;
                                        display: inline-block;
                                        font-size: 12px;
                                        margin: 4px 2px;
                                        cursor: pointer;
                                    }
                                          </style>
                                    <div class="col-md-4">
                                        <div class="form-group" style="margin-top: 17px;">
                                            <asp:LinkButton runat="server" ID="SearchButton" OnClick="SearchButton_OnClick"   CssClass="btn btnMyDesignSearch   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search Information </asp:LinkButton>

                                            <asp:LinkButton runat="server" ID="btnReset" OnClick="btnReset_OnClick" CssClass="btn btnMyDesignReset   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset Information</asp:LinkButton>
                                          
                                        </div> </div>
                                         </div>
                                     <div class="col-md-2">
                                         </div>
                             
                                  

                                </div>  </ContentTemplate>
                    </asp:UpdatePanel>
                                <div class="row" runat="server" visible="False">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label></label>
                                            <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True" RepeatDirection="Horizontal" Enabled="False">
                                            </asp:CheckBoxList>


                                        </div>
                                    </div>
                                </div>
                                
                                <div class="row " runat="server"  >
                                     <div class="col-md-8">
                                         
                                     </div>
                                     <div class="col-md-2">
                                            <style>.ssss {
                                                                                                                     font-size: 13px;
                                                                                                                     font-weight: bold;
                                                                                                                     padding: 10px;
                                               
                                                                                                                 }</style>
                                     </div>
                                       <div class="col-md-2" runat="server" 
                                          >
                                         <asp:LinkButton ID="btnExportToExcel" runat="server" Visible="False" CssClass="btnexcel  pull-right" OnClick="btnExportToExcel_Click" ><span aria-hidden="true" class="glyphicon glyphicon-ok"></span>Export To Excel</asp:LinkButton>
                                               <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                        <ContentTemplate> 
                                         
                                            
                                              <asp:Label ID="lblCount" runat="server" CssClass="ssss pull-right"   Text="Total : 0" ></asp:Label>
                                             
                                 </ContentTemplate>

                        </asp:UpdatePanel>
                                         </div>
                                </div>
                                <%-- <div id="tbl_container" style="width:100%" class="">

                    </div>--%>
                                  <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                        <ContentTemplate>
                            
                            <div class="row" style="padding: 5px">


                           
                           
                                    <div class="col-md-12">
                                        <div class="form-group">

                                <div id="gridContainer1" style="height: auto; overflow: auto; width: auto;">
                                    <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                        CssClass="AddToListCssTable" DataKeyNames="EmpInfoId"
                                        OnRowCommand="loadGridView_RowCommand" Font-Size="12px" AllowPaging="True" AllowSorting="True"  OnPageIndexChanging="loadGridView_PageIndexChanging"  OnPreRender="gv_DocumentUpload_PreRender">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL" >
                                                <ItemTemplate>
                                                    <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CV"  Visible="False">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ViewReportImageButton" runat="server" class="btn btn-white btn-sm  " CommandArgument='<%#Eval("EmpInfoId") %>'
                                                        CommandName="ViewReport" ImageUrl="~/Assets/report_magnify.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee ID" />
                                            <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                            <asp:BoundField DataField="DivisionName" HeaderText="Division" />

                                            <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                            <asp:BoundField DataField="Designation" HeaderText="Designation"  Visible="False"/>
                                            <asp:BoundField DataField="SalaryLocation" HeaderText="Office"  Visible="False"/>
                                            <asp:BoundField DataField="EmpType" HeaderText="Employee Type" Visible="False"/>
                                            <asp:BoundField DataField="EmployeeStatus" HeaderText="Employee Status"  Visible="False"/>



                                            <asp:TemplateField HeaderText="General" >
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="editGeneralInformation" runat="server" CssClass="btn btn-sm btnMyDesignEdit" 
                                                        CommandArgument='<%#Eval("EmpInfoId") %>'   CommandName="GeneralInformation" ToolTip="Edit Employee General Information"
                                                       ><i class="fa fa-pencil-square-o" style="font-size: 17px!important; font-weight: bold" aria-hidden="true"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Employment" >
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="editEmploymentInformation" runat="server"  CssClass="btn btn-sm btnMyDesignEdit" ToolTip="Edit Employment Information"
                                                        CommandArgument='<%#Eval("EmpInfoId") %>' CommandName="EmploymentInformation" 
                                                         ><i class="fa fa-pencil-square-o" style="font-size: 17px!important; font-weight: bold" aria-hidden="true"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Contacts" >
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="editContacts" runat="server" ToolTip="Edit Employment Contacts Information" CssClass="btn btn-sm btnMyDesignEdit" 
                                                        CommandArgument='<%#Eval("EmpInfoId") %>' CommandName="Contacts"
                                                        ><i class="fa fa-pencil-square-o" style="font-size: 17px!important; font-weight: bold" aria-hidden="true"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Family">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="editFamilyInformation" runat="server" ToolTip="Edit Employment Family Information" CssClass="btn btn-sm btnMyDesignEdit" 
                                                        CommandArgument='<%#Eval("EmpInfoId") %>' CommandName="FamilyInformation"
                                                        ><i class="fa fa-pencil-square-o" style="font-size: 17px!important; font-weight: bold" aria-hidden="true"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Education">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="editEducation" runat="server"  ToolTip="Edit Employment Education Information" CssClass="btn btn-sm btnMyDesignEdit" 
                                                        CommandArgument='<%#Eval("EmpInfoId") %>' CommandName="Education"
                                                      ><i class="fa fa-pencil-square-o" style="font-size: 17px!important; font-weight: bold" aria-hidden="true"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Experience" >
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="editExperience" runat="server"
                                                        CommandArgument='<%#Eval("EmpInfoId") %>' CommandName="Experience"  ToolTip="Edit Employment Experience Information" CssClass="btn btn-sm btnMyDesignEdit" 
                                                      ><i class="fa fa-pencil-square-o" style="font-size: 17px!important; font-weight: bold" aria-hidden="true"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Training" >
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="editTraining" runat="server"
                                                        CommandArgument='<%#Eval("EmpInfoId") %>' CommandName="Training" ToolTip="Edit Employment Training Information" CssClass="btn btn-sm btnMyDesignEdit" 
                                                        ><i class="fa fa-pencil-square-o" style="font-size: 17px!important; font-weight: bold" aria-hidden="true"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Reference" >
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="editReference" runat="server"
                                                        CommandArgument='<%#Eval("EmpInfoId") %>' CommandName="Reference"  ToolTip="Edit Employment Reference Information" CssClass="btn btn-sm btnMyDesignEdit" 
                                                        ><i class="fa fa-pencil-square-o" style="font-size: 17px!important; font-weight: bold" aria-hidden="true"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Nominee" >
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="editNominee" runat="server"
                                                        CommandArgument='<%#Eval("EmpInfoId") %>' CommandName="Nominee"   ToolTip="Edit Employment Nominee Information" CssClass="btn btn-sm btnMyDesignEdit" 
                                                     ><i class="fa fa-pencil-square-o" style="font-size: 17px!important; font-weight: bold" aria-hidden="true"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Others">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="editOthers" runat="server"
                                                        CommandArgument='<%#Eval("EmpInfoId") %>' CommandName="Others"    ToolTip="Edit Employment Others Information" CssClass="btn btn-sm btnMyDesignEdit" 
                                                      ><i class="fa fa-pencil-square-o" style="font-size: 17px!important; font-weight: bold" aria-hidden="true"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                                             <asp:TemplateField HeaderText="Salary">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="editSalary" runat="server"
                                                        CommandArgument='<%#Eval("EmpInfoId") %>' CommandName="Salary"    ToolTip="Edit Employment Others Information" CssClass="btn btn-sm btnMyDesignEdit" 
                                                      ><i class="fa fa-pencil-square-o" style="font-size: 17px!important; font-weight: bold" aria-hidden="true"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            
                                              <asp:TemplateField HeaderText="DEPARTED">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="editDEPARTED" runat="server"
                                                                    CommandArgument='<%#Eval("EmpInfoId") %>' CommandName="DEPARTED"    ToolTip="Edit DEPARTED Information" CssClass="btn btn-sm btnMyDesignEdit" 
                                                    ><i class="fa fa-pencil-square-o" style="font-size: 17px!important; font-weight: bold" aria-hidden="true"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                          <%--  <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="deleteImageButton" runat="server"
                                                        CommandArgument='<%#Eval("EmpInfoId") %>' CommandName="DeleteData"
                                                        ImageUrl="~/Assets/img/delete.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="View">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="viewImageButton" runat="server"
                                                        CommandArgument='<%#Eval("EmpInfoId") %>' CommandName="ViewData"
                                                        ImageUrl="~/Assets/img/list-view.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                           <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                    </asp:GridView>
                                </div>
                                            </div>
                                        </div>
                                </div>
 </ContentTemplate>
                    </asp:UpdatePanel>
                    
                    </div>
                            </div>
                       
                </div>
            <br/>
            <br/>
            <br/>
            <br/>
            <br/>
            <br/>
            <br/>
            <br/>
            <br/>
            <br/>
            <br/>
            <br/>
            <br/>
            <br/>
            <br/>
            <br/>
            <br/>
            <br/>
            <br/>
            <br/>
            <br/>
            <br/>
            <br/>
            <br/>
            <br/>
            <br/>
            <br/>
            <br/>
            <br/>
            <br/>
            <br/>
            <br/>
            <br/>
            <br/>
            <br/>
            </div>
        </div>
   <style>.GridPager a,
.GridPager span {
    display: inline-block;
    padding: 3px 14px;
    margin-right: 8px;
    border-radius: 3px;
                                                                                                                                                                                                                                                                     height: 20px;
    border: solid 1px #c0c0c0;
    background: #e9e9e9;
    box-shadow: inset 0px 1px 0px rgba(255,255,255, .8), 0px 1px 3px rgba(0,0,0, .1);
    font-size: 14px;
    font-weight: bold;
    text-decoration: none;
    color: #717171;
    text-shadow: 0px 1px 0px rgba(255,255,255, 1);
}

.GridPager a {

    background-color: #f5f5f5;
    color: #969696;
    border: 1px solid #969696;
}

.GridPager span {

    background: #616161;
    box-shadow: inset 0px 0px 8px rgba(0,0,0, .5), 0px 1px 0px rgba(255,255,255, .8);
    color: #f0f0f0;
    text-shadow: 0px 0px 3px rgba(0,0,0, .5);
    border: 1px solid #3AC0F2;
}

    </style>
</asp:Content>

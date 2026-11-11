<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MainMasterPage.master" CodeFile="EmployeeInfoList.aspx.cs" Inherits="UserSetup_EmployeeInfoList" EnableEventValidation="false" %>
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
    

    <div class="content" id="content">

        <style>
      

        </style>
        <div class="container-fluid">
            <div class="page-heading">
                <div class="page-heading__container">
                    <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" />&nbsp;Employee Information List</h1>
                </div>
                <div class="page-heading__container float-right d-none d-sm-block">
                  
                <%--    <asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="HomeButton_OnClick" />
                    
                      <asp:Button ID="btnEditInfo" Text="Update Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="btnEditInfo_OnClick" />
                    <asp:Button ID="btn_New" Text="Create New" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="btn_New_OnClick" />--%>
                    
                    
                     <asp:LinkButton ID="HomeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="HomeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                    <div style="display:none">
                           <asp:LinkButton ID="btnEditInfo"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-info " runat="server" OnClick="btnEditInfo_OnClick" > <i class="fa fa-pencil"></i>&nbsp;Update Information</asp:LinkButton>
  
  
   <asp:LinkButton ID="btn_New"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="btn_New_OnClick" > <i class="fa fa-plus"></i>&nbsp;Add New</asp:LinkButton>
            
                    </div>
                      </div>
            </div>
            <div class="container-fluid">
                <div class="card">
                    <asp:UpdatePanel runat="server" ID="uppa">
                        <ContentTemplate>
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
                                      <%--            <script type="text/javascript">
                                                      function pageLoad() {
                                                          $('#<%=ddlWing.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });
                                                      }
</script>--%>
                                        </div>
                                    </div>
                                    <div class="col-2" runat="server" id="dept">
                                        <div class="form-group">
                                            <label>Department</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDepartment" class="form-control form-control-sm" OnSelectedIndexChanged="ddlDepartment_OnSelectedIndexChanged" />
                                  <%--                       <script type="text/javascript">
                                                             function pageLoad() {
                                                                 $('#<%=ddlDepartment.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });
                                                      }
</script>--%>
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
                                            <asp:TextBox ID="NameTextBox" Visible="False" runat="server" AutoPostBack="True" placeholder=" Employee Name" CssClass="form-control form-control-sm" OnTextChanged="EmployeeDropDownList_SelectedIndexChanged"></asp:TextBox>
                                            
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
                                            <asp:DropDownList runat="server"   ID="ddlDesignation" class="form-control form-control-sm" />

                                        </div>
                                    </div>
                                </div>
                      
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
                                            <label>Active Status</label>
                                            <asp:DropDownList runat="server" ID="ActiveStatusDropDownList" CssClass="form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="ActiveStatusDropDownList_OnSelectedIndexChanged">
                                                <asp:ListItem Text="All" Value="-1"></asp:ListItem>
                                                <asp:ListItem  Text="Active" Selected="True" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                       
                                        <div class="col-md-2">
                                        <div class="form-group">
                                               <asp:CheckBox runat="server" ID="SSGradeCheck" CssClass="chkChoice"  Text="Special Transfer" />
                                            </div>
                                            </div>
                                      <style>
                                 
                                          </style>
                                    <div class="col-md-4">
                                        <div class="form-group" style="margin-top: 17px;">
                                            
                                             <asp:LinkButton runat="server" ID="SearchButton" OnClick="SearchButton_OnClick"   CssClass="btn btnMyDesignSearch   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search Information </asp:LinkButton>

                                            <asp:LinkButton runat="server" ID="btnReset" OnClick="btnReset_OnClick" CssClass="btn btnMyDesignReset   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset Information</asp:LinkButton>

                                          
                                        </div> </div>
                                         </div>
                                     <div class="col-md-2">
                                         </div>
                             
                                  

                                </div>  
                                <div class="row" runat="server" visible="False">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label></label>
                                            <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True" RepeatDirection="Horizontal" Enabled="False">
                                            </asp:CheckBoxList>


                                        </div>
                                    </div>
                                </div>
                                
                                <div class="row">
                                     <div class="col-md-8">
                                         
                                     </div>
                                    
                                      <style>.ssss {
                                                                                                                     font-size: 13px;
                                                                                                                     font-weight: bold;
                                                                          
                                               
                                                                                                                 }</style>
                              
                                     <div class="col-md-2"  style="margin-top: 22px; padding: 5px;">
                                       
                                            
                                              <asp:Label ID="lblCount" runat="server" CssClass="ssss pull-right"   Text="Total : 0" ></asp:Label>
                                             
                               
                                        
                                      
                                     </div>
                             
                               
       
                                       <div class="col-md-2" style="margin-top: 17px;">
                                         <asp:LinkButton ID="btnExportToExcel" runat="server"  CssClass="btnexcel  pull-right" OnClick="btnExportToExcel_Click" ><i class="fa fa-file-excel-o"></i> Export To Excel</asp:LinkButton>
                                         </div>
                                </div>
                                <%-- <div id="tbl_container" style="width:100%" class="">

                    </div>--%>
                                  <br/>
                                <div id="gridContainer1" style="height: auto; overflow: auto; width: auto;">
                                    <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                          CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender"   DataKeyNames="EmpInfoId"
                                        OnRowCommand="loadGridView_RowCommand" Font-Size="12px" AllowPaging="True" PageIndex="0"  OnPageIndexChanging="loadGridView_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL">
                                                <ItemTemplate>
                                                    <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                       <asp:HiddenField ID="JdMasterId" runat="server" Value='<%#Eval("EmpInfoId") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CV">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ViewReportImageButton" runat="server" class="btn btn-white btn-sm  " CommandArgument='<%#Eval("EmpInfoId") %>'
                                                        CommandName="ViewReport" ImageUrl="~/Assets/report_magnify.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                                <asp:TemplateField HeaderText="NOC Letter">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="VisitingLetterImageButton" runat="server" class="btn btn-white btn-sm  " CommandArgument='<%#Eval("EmpInfoId") %>' 
                                                        CommandName="VisitingLetter" ImageUrl="~/Assets/file.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            
                                             <asp:TemplateField HeaderText="JD">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" OnClick="btn_edit_OnClick" ID="btn_edit">
                                               <img src="../Assets/report_magnify.png" height="18px" />
                                            </asp:LinkButton>

                                        </ItemTemplate>
                                    </asp:TemplateField>


                                            <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee ID" />
                                            <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                            <asp:BoundField DataField="DivisionName" HeaderText="Division" />

                                            <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                            <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                            <asp:BoundField DataField="SalaryLocation" HeaderText="Office" />
                                            <asp:BoundField DataField="EmpType" HeaderText="Employee Type" />
                                            <asp:BoundField DataField="EmployeeStatus" HeaderText="Employee Status" />
                                            <asp:TemplateField HeaderText="Update Mail">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnUpdateMail" runat="server"
                                                        CommandArgument='<%#Eval("EmpInfoId") %>' CommandName="UpdateMail"
                                                        CssClass="btn btn-sm btn-outline-info">Update Mail</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                 <div style="display:none">
                                                        <asp:ImageButton ID="deleteImageButton" runat="server"
       CommandArgument='<%#Eval("EmpInfoId") %>' CommandName="DeleteData"
       ImageUrl="~/Assets/img/delete.png" />
                                                 </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="View">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="viewImageButton" runat="server"
                                                        CommandArgument='<%#Eval("EmpInfoId") %>' CommandName="ViewData"
                                                        ImageUrl="~/Assets/img/list-view.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                    </asp:GridView>
                                </div>

                                <div class="modal fade" id="updateMailModal" tabindex="-1" role="dialog" aria-hidden="true">
                                    <div class="modal-dialog" role="document">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <h5 class="modal-title">Update Mail</h5>
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                    <span aria-hidden="true">&times;</span>
                                                </button>
                                            </div>
                                            <div class="modal-body">
                                                <asp:HiddenField ID="hfUpdateMailEmpInfoId" runat="server" />
                                                <div class="form-group">
                                                    <label>Employee</label>
                                                    <asp:TextBox ID="txtUpdateMailEmployee" runat="server" ReadOnly="True" CssClass="form-control form-control-sm" />
                                                </div>
                                                <div class="form-group">
                                                    <label>Personal Email</label>
                                                    <asp:TextBox ID="txtUpdatePersonalEmail" runat="server" CssClass="form-control form-control-sm" />
                                                </div>
                                                <div class="form-group">
                                                    <label>Official Email</label>
                                                    <asp:TextBox ID="txtUpdateOfficialEmail" runat="server" CssClass="form-control form-control-sm" />
                                                </div>
                                            </div>
                                            <div class="modal-footer">
                                                <asp:LinkButton ID="btnUpdateMailSave" runat="server" CssClass="btn btn-sm btn-primary" OnClick="btnUpdateMailSave_OnClick">
                                                    <i class="fa fa-save"></i>&nbsp;Save
                                                </asp:LinkButton>
                                                <button type="button" class="btn btn-sm btn-secondary" data-dismiss="modal">Close</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
 
                    </ContentTemplate>
                        
                        <Triggers>
                 
                 <asp:PostBackTrigger ControlID="btnExportToExcel"/>
             </Triggers>
                    </asp:UpdatePanel>
                    
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

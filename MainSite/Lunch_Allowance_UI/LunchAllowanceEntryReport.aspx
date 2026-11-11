<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MainMasterPage.master" CodeFile="LunchAllowanceEntryReport.aspx.cs" Inherits="Lunch_Allowance_UI_LunchAllowanceEntryReport" EnableEventValidation="false" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>


<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">
       <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
   
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cpFormBody" runat="Server">
    
    <style>
         .chkChoice label {
                padding-left: 10px;
                padding-right: 10px;
            }
    </style>
    
    <div class="content" id="content">

        <style>
      

            </style>
        <div class="container-fluid">
            <div class="page-heading">
                <div class="page-heading__container">
                    <h1 class="title" style="font-size: 18px; padding-top: 0px;"> <img src="../Report_Pages/app.png"  width="20px" /> Employee Lunch  History</h1>
                </div>
                <div class="page-heading__container float-right d-none d-sm-block" runat="server" >
                  
             
                        <asp:LinkButton ID="HomeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                        <asp:LinkButton ID="btn_New"  style="font-style: normal!important; font-weight: bold" Visible="False"  CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="btn_New_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
                   
                </div>
            </div>
            <div class="container-fluid">
                
                <asp:UpdatePanel runat="server" ID="asdasdas">
                    <ContentTemplate>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="divWaiting">
                                        <asp:Image ID="imgWait2" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                <div class="card">
                        <div class="card-body">
                  
                       
                        
                            
                            
                        
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
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDivision" class="form-control form-control-sm SelectMe" OnSelectedIndexChanged="ddlDivision_OnSelectedIndexChanged" />
                                                 <script type="text/javascript">
                                                function pageLoad() {
                                                    $('.SelectMe').chosen({ disable_search_threshold: 5, search_contains: true });
 


                                            

                                          }
               </script>
                                        </div>
                                    </div>
                                    <div class="col-2" runat="server" id="wing" visible="False">
                                        <div class="form-group">
                                            <label>Wing</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlWing" class="form-control form-control-sm SelectMe" OnSelectedIndexChanged="ddlWing_OnSelectedIndexChanged" />

                                        </div>
                                    </div>
                                    <div class="col-2" runat="server" id="dept">
                                        <div class="form-group">
                                            <label>Department</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDepartment" class="form-control form-control-sm SelectMe" OnSelectedIndexChanged="ddlDepartment_OnSelectedIndexChanged" />

                                        </div>
                                    </div>
                                    <div class="col-2" runat="server" id="sec" visible="False">
                                        <div class="form-group">
                                            <label>Section</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlSection" class="form-control form-control-sm SelectMe" OnSelectedIndexChanged="ddlSection_OnSelectedIndexChanged" />

                                        </div>
                                    </div>
                                    <div class="col-2" runat="server" id="subsec" visible="False">
                                        <div class="form-group">
                                            <label>Sub Section</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlSubSection" class="form-control form-control-sm" OnSelectedIndexChanged="ddlSubSection_OnSelectedIndexChanged" />

                                        </div>
                                    </div>

                                    <div class="col-md-2" runat="server" Visible="False">

                                        <div class="form-group">
                                            <label>Employee ID </label>
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


                                    <div class="col-md-4">

                                        <div class="form-group">
                                            <label>Employee Name </label>
                                               <asp:DropDownList   runat="server"   ID="ddlEmpInfo"   class="form-control form-control-sm SelectMe" />

                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Designation</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDesignation" class="form-control form-control-sm SelectMe" />

                                        </div>
                                    </div>
                                </div>
                      
                                   <div class="row">


                            <div class="col-2">
                                                <div class="form-group">
                                                    <label>Employee Category</label>
                                                    <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control form-control-sm" />
                                                </div>
                                            </div>
                           
                                    <div class="col-md-2" runat="server" Visible="False">
                                        <div class="form-group">
                                            <label>Office</label>
                                            <asp:DropDownList runat="server" ID="ddlSalaryLocation" class="form-control form-control-sm SelectMe" />

                                        </div>
                                    </div>

                                    <div class="col-md-2" runat="server" Visible="False">
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
                                            <asp:DropDownList runat="server" ID="ActiveStatusDropDownList" CssClass="form-control form-control-sm">
                                                <asp:ListItem Text="Select..." Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Yes" Selected="True" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
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
                                    <div class="col-md-3">
                                        <div class="form-group" style="margin-top: 17px;">

                                            <asp:LinkButton runat="server" ID="SearchButton" OnClick="SearchButton_OnClick" ToolTip="Click To Search" Width="80" Text="Search" class="btn btn-info btn-sm" ><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search </asp:LinkButton>

                                   &nbsp;&nbsp;

                                            <asp:LinkButton runat="server" ID="btnReset" OnClick="btnReset_OnClick" ToolTip="Click To Reset" Text="Reset"   Width="80" CssClass="btn btn-warning   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset </asp:LinkButton>
                                        </div> </div>
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
                                
                                <div class="row" runat="server" Visible="False"> 
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
                                     
                                         </div>
                                </div>
                                <%-- <div id="tbl_container" style="width:100%" class="">

                    </div>--%>
                            <br/>
                            <br/>
                                  
                                    <div class="form-row" style="padding-right: 10px">

                                <div class="col-md-6" style="padding-left: 2px">
                                    <label style="font-size: 18px;">Details Information</label>
                                </div>
                                        
                                        
                                        
                                            <div class="col-md-2" runat="server" Visible="False">
                                    <div class="form-group" style="margin-top: 20px;">
                                        <asp:CheckBox ID="chk_Common" runat="server" CssClass="chkChoice" Text=" Is Common" TextAlign="right" AutoPostBack="True" OnCheckedChanged="chk_Common_OnCheckedChanged"></asp:CheckBox>
                                    </div>
                                </div>
                                <div class="col-md-2" runat="server" Visible="False">
                                    <div class="form-group">
                                        <label>From Date</label>
                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control form-control-sm"   AutoPostBack="True" OnTextChanged="txtFromDate_OnTextChanged" Enabled="False"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                            TargetControlID="txtFromDate" />

                                    </div>
                                </div>
                                        
                                         <div class="col-md-2" runat="server" Visible="False">
                                    <div class="form-group">
                                        <label>To Date</label>
                                        <asp:TextBox ID="txtToDate" runat="server"  Enabled="False" CssClass="form-control form-control-sm"   AutoPostBack="True" OnTextChanged="txtToDate_OnTextChanged"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                            Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                            TargetControlID="txtToDate" />

                                    </div>
                                </div>
                                        
                                        

                                <div class="col-md-4">
                                </div>
                                <div class="col-md-2">
                                        <asp:LinkButton ID="btnExportToExcel" runat="server"  CssClass="btnexcel  pull-right" OnClick="btnExportToExcel_Click" ><i class="fa fa-download"></i> Export To Excel</asp:LinkButton>

                                   
                                </div>


                            </div>

                            <hr />
                                  <asp:HiddenField ID="hdpk" runat="server" />
                                   <div style="overflow: scroll; height: 500px; width: 100%">
          
                    <asp:GridView ID="gv_EmpInfoLoad" Width="100%"        CssClass="table table-bordered text-center thead-dark table-hover table-striped"   AutoGenerateColumns="false" runat="server">
                                      
                                        <Columns>
                                            
                                             
                                             <asp:TemplateField Visible="False">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="txt_checkAll" OnCheckedChanged="txt_checkAll_OnCheckedChanged" AutoPostBack="True" runat="server"></asp:CheckBox>
                                        <asp:Label ID="txt_selectAll" runat="server"  ></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>

                                        <asp:CheckBox ID="txt_check" runat="server"></asp:CheckBox>
                                    </ItemTemplate>


                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                        
                                        <asp:HiddenField runat="server" ID="txtEmployeeIdForCheck" Value='<%#Eval("EmployeeIdForCheck") %>' />
                                        <asp:HiddenField runat="server" ID="txt_empInfoId" Value='<%#Eval("EmpInfoId") %>' />
                                    </ItemTemplate>

                                </asp:TemplateField>
                                            

                                            <asp:TemplateField HeaderText="Employee ID">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_empId" runat="server"  Text='<%#Eval("EmpMasterCode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_name" runat="server"  Text='<%#Eval("EmpName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                            
                                               <asp:TemplateField HeaderText="Division">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_Division" runat="server"  Text='<%#Eval("DivisionName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                              <asp:TemplateField HeaderText="Department">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_dptName" runat="server"  Text='<%#Eval("DepartmentName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_designation" runat="server"   Text='<%#Eval("Designation") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                              
                                            
                                              <asp:TemplateField HeaderText="Food Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_Rate" runat="server"  Text='<%#Eval("Rate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                             
                                                    <asp:TemplateField HeaderText="Food Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_AllowStatus" runat="server"  Text='<%#Eval("AllowStatus") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                           

                                <asp:TemplateField HeaderText="From Date">
                                    <ItemTemplate>
                                        
                                          <asp:Label ID="txt_AllowFromDate" runat="server"  Text='<%#Eval("AllowFromDate") %>'></asp:Label>
                                  
                                    </ItemTemplate>
                                </asp:TemplateField>
                                            
                                            
                                            <asp:TemplateField HeaderText="To Date">
                                    <ItemTemplate>
                                        

                                           <asp:Label ID="txt_AllowToDate" runat="server"  Text='<%#Eval("AllowToDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                              
                                              
                                               
                                            <asp:TemplateField HeaderText="Inactive Date">
                                    <ItemTemplate>
                                        
                                           <asp:Label ID="txt_InactiveDate" runat="server"  Text='<%#Eval("InactiveDate") %>'></asp:Label>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>


                                            
                                        </Columns>
                                        
                                    </asp:GridView> 
                                       </div>
                            
                            
                            
                               <div class="form-row">
                                   <div class="col-2">
                            <div class="form-group">
                                <asp:LinkButton ID="btn_Save"  Visible="False" CssClass="btn btn-sm btn-info"   runat="server" OnClick="btn_Save_OnClick">&nbsp; Submit</asp:LinkButton>
                              <%--  <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />--%>

                                <%--<asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" Text="Submit " CssClass="btn btn-sm btn-info" />--%>
                                <%--<asp:Button ID="cancelButton" Visible="False" Text="Cancel" OnClick="cancelButton_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />--%>

                            </div>
                        </div>
                            </div>
                   
                    </div>
                            
                
                </div>
                        
                        </ContentTemplate>
                     <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnExportToExcel"  />
    </Triggers>
                    </asp:UpdatePanel>
            </div>
    </div>
         
  
        </div>
</asp:Content>

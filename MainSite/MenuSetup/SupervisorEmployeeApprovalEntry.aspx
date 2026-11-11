<%@ Page Title="" Language="C#"  EnableEventValidation="false"  MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="SupervisorEmployeeApprovalEntry.aspx.cs" Inherits="MenuSetup_SupervisorApprovalEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <style>
        

        
    </style>
    
     <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    
    
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
                        <div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;"> Supervisor Approval Setup </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <%--<asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                        <asp:Button ID="addNewButton" Text="Add New Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="addNewButton_OnClick" />--%>
                    </div>
                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">
                             <div class="row" >
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Company</label> &nbsp;<label style="color: #a52a2a">*</label>
                                        <asp:DropDownList ID="companyDropDownList" class="form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="companyDropDownList_OnSelectedIndexChanged" CssClass="" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                 <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Division</label> &nbsp;<label style="color: #a52a2a">*</label>
                                        <asp:DropDownList ID="divDropDownList" class="form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="divDropDownList_OnSelectedIndexChanged" CssClass="" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                 <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Department</label> 
                                        <asp:DropDownList ID="deptDropDownList" class="form-control form-control-sm" AutoPostBack="True"  CssClass="" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                            <div class="col-2">
                                        <div class="form-group">
                                            <label>Employee Type</label>  &nbsp; 
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlEmpCategoryEx" class="form-control form-control-sm" OnSelectedIndexChanged="ddlEmpCategoryEx_OnSelectedIndexChanged" />
                                        </div>
                                    </div>
                                     <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Grade </label>
                                                    &nbsp; 
                                                    <asp:DropDownList ID="gradeDropDownList" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                 
                                 
                                   <div class="col-md-2">

                                        <div class="form-group">
                                            <label>Employee Name: </label>
                                            
                                              <asp:DropDownList runat="server" ID="ddlEmpInfo" class="form-control form-control-sm" />
                                            <script type="text/javascript">
                                                function pageLoad() {
                                                    
                                                    $('#cpFormBody_ddlEmpInfo').chosen({ disable_search_threshold: 5, search_contains: true });
                                                
                                                    $('#cpFormBody_divDropDownList').chosen({ disable_search_threshold: 5, search_contains: true });

                                                    $('#cpFormBody_gradeDropDownList').chosen({ disable_search_threshold: 5, search_contains: true });
                                                    $('#cpFormBody_deptDropDownList').chosen({ disable_search_threshold: 5, search_contains: true });
                                                    $('.selecttome1').chosen({ disable_search_threshold: 5, search_contains: true });
                                                }
</script>
                                            </div>
                                       </div>
                                   <div class="col-md-5">
                                       </div>
                                  <div class="col-md-1">
                                    <div class="form-group" style="margin-top: 16px;">
                                       
                                        <asp:Button ID="Button1" Text="Search" CssClass="btn btn-outline-info btn-block disabled btn-sm" Visible="True" runat="server" OnClick="Button1_OnClick" />
                                    </div>
                                </div>
                                 
                                    <div class="col-md-6">
                                    <div class="form-group" style="margin-top: 16px;">
                                         <asp:LinkButton ID="btnExportToExcel" runat="server"  CssClass="btnexcel  pull-right" OnClick="btnExportToExcel_Click" ><i class="fa fa-file-excel-o"></i> Export To Excel</asp:LinkButton>
                                        
                                    </div>
                                </div>
                                 <div class="col-md-2" runat="server" Visible="False">
                                    <div class="form-group">
                                        <label>Menu Name</label>
                                        <asp:DropDownList ID="menuDropDownList" class="form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="menuDropDownList_OnSelectedIndexChanged" CssClass="" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div id="gridContainer1" style="">
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                   CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender"  DataKeyNames="SuperMenuAppId,IsChecked,EmpInfoId,DepartmentId,FromEmpInfoId">
                                    <Columns>
                                       <asp:TemplateField >
                                            <HeaderTemplate>
                                                <asp:CheckBox runat="server" AutoPostBack="True" ID="chkAll" Text="Select" OnCheckedChanged="chkAll_OnCheckedChanged"/>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chkSingle"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField DataField="CompanyName" HeaderText="Company Name" />--%>
                                        <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee Code" />
                                        <asp:BoundField DataField="EmpName" HeaderText="Name" />
                                        <asp:BoundField DataField="DivisionName" HeaderText="Division" />
                                        <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                        <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                        <asp:TemplateField HeaderText="Final Approver">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="employeeDropDownList" Width="400px" class="form-control form-control-sm selecttome1" runat="server"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                            <asp:TemplateField HeaderText="No supervisor chain will be maintained">
                                            <ItemTemplate>
                                                <asp:CheckBox Checked='<%# Convert.ToBoolean(Eval("IsAllEmployee")) %>' AutoPostBack="true" ID="chkIsAllEmployee"  runat="server"
                                                    OnCheckedChanged="chkIsAllEmployee_CheckedChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        
                                            <asp:TemplateField HeaderText="Approval Path">
                                            <ItemTemplate>
                                                      
                                                <asp:Label ID="lblEmpAppPath"   runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                                
                                
                                        <div style="display: none">
                                              <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                   CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender"   >
                                    <Columns>
                                     
                                        <%--<asp:BoundField DataField="CompanyName" HeaderText="Company Name" />--%>
                                        <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee Code" />
                                        <asp:BoundField DataField="EmpName" HeaderText="Name" />
                                        <asp:BoundField DataField="DivisionName" HeaderText="Division" />
                                        <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                        <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                        <asp:BoundField DataField="FinalApprover" HeaderText="Final Approver" />
                                 
                                        <%--<asp:BoundField DataField="lblEmpAppPath" HeaderText="Approval Path" />--%>
                                           

                                    </Columns>
                                </asp:GridView>
                                        </div>
                            </div>
                            <div class="form-group"  Visible="false" runat="server" >
                                <br/>
                                <asp:Button ID="submitButton" Text="Save" CssClass="btn btn-sm btn-info" Visible="True" runat="server" OnClick="submitButton_OnClick" />
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
                        </div>
                    </div>
                </div>
            </ContentTemplate>
               <Triggers>
                 
                 <asp:PostBackTrigger ControlID="btnExportToExcel"/>
             </Triggers>
        </asp:UpdatePanel>
    </div>
    <script src="../Assets/table2excel.js"></script>
    <script type="text/javascript">
        $("body").on("click", "#btnExport", function () {
            $("[id*=loadGridView]").table2excel({
                filename: "Employee_Join_left_Data_Info.xls"
            });
        });








    </script>

</asp:Content>


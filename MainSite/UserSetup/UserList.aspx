<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MainMasterPage.master" CodeFile="UserList.aspx.cs" Inherits="UserSetup_UserList" %>


<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">
     <style>
        .pagination .page-item.active .page-link  {
            background-color: #007BFF !important;
            border-style: none !important;
            /* W3C */
        }

        
    </style>
   
     <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />

       
    
    
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
 
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">
        

        <div class="container-fluid">
            <div class="page-heading">
                <div class="page-heading__container">
                    <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" />&nbsp;User List</h1>
                </div>
                <div class="page-heading__container float-right d-none d-sm-block">
                    
                          <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                       
                        
                         <asp:LinkButton ID="btn_New"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="addNewButton_OnClick" > <i class="fa fa-plus"></i>&nbsp;Add New</asp:LinkButton>
                        
                </div>
            </div>
            <div class="card">
                <div class="card-body">
                  <%--      
                    <div id="tbl_container" style="width:100%" class="">

                    </div>--%>
                     <asp:UpdatePanel ID="UpdatePanel1" runat="server">

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
            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlCompany"  OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"  class="form-control form-control-sm" />

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
                        <div class="col-md-4">

    <div class="form-group">
        <label>Employee Name: </label>
        
          <asp:DropDownList runat="server" ID="ddlEmpInfo" class="form-control form-control-sm" />
        </div>
    </div>      <div class="col-md-2">
    <div class="form-group">
        <label>Employee Status</label>
        <asp:DropDownList runat="server" ID="ActiveStatusDropDownList" CssClass="form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="ActiveStatusDropDownList_OnSelectedIndexChanged">
            <asp:ListItem Text="All" Value="-1"></asp:ListItem>
            <asp:ListItem  Text="Active" Selected="True" Value="1"></asp:ListItem>
            <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
        </asp:DropDownList>
    </div>
</div>

                           <div class="col-md-2">
    <div class="form-group">
        <label>User Status</label>
        <asp:DropDownList runat="server" ID="ddlUserStatus" CssClass="form-control form-control-sm" >
            <asp:ListItem Text="All" Value="-1"></asp:ListItem>
            <asp:ListItem  Text="Active" Selected="True" Value="1"></asp:ListItem>
            <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
        </asp:DropDownList>
    </div>
</div>
                           <div class="col-md-2">
       <div class="form-group" style="margin-top: 17px;">
           
            <asp:LinkButton runat="server" ID="SearchButton" OnClick="SearchButton_OnClick"   CssClass="btn btnMyDesignSearch   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search  </asp:LinkButton></div>
                        </div>
                        </div>
                    
                        <div class="col-md-12">
                            <div id="gridContainer1" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;">
                               
                                        <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                             DataKeyNames="UserId"       CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender"  
                                            OnRowCommand="loadGridView_RowCommand"   >
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="UserName" HeaderText="User Name" />
                                                <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />

                                                <asp:BoundField DataField="UserType" HeaderText="User Type" />
                                                <asp:BoundField DataField="ContactNo" HeaderText="Contact No" />
                                                <asp:BoundField DataField="Email" HeaderText="Email ID" />
                                                <asp:BoundField DataField="IsActive" HeaderText="Active Status" />
                                                <asp:BoundField DataField="Remarks" HeaderText="Remarks" />



                                               
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="editImageButton" runat="server"
                                                            CommandArgument='<%#Eval("UserId") %>' CommandName="EditData"
                                                            ImageUrl="~/Assets/img/rsz_edit.png" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            <%--    <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="deleteImageButton" runat="server"
                                                            CommandArgument='<%#Eval("UserId") %>' CommandName="DeleteData"
                                                            ImageUrl="~/Assets/img/delete.png" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderText="View">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="viewImageButton" runat="server"
                                                             CommandArgument='<%#Eval("UserId") %>' CommandName="ViewData"
                                                            ImageUrl="~/Assets/img/list-view.png" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               
                                            </Columns>
                                          
                                        </asp:GridView>
                               
                            </div>
                        </div>
                        
                             </ContentTemplate>

 </asp:UpdatePanel>  
                </div>
            </div>
            
        </div>

        
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br /> 
    </div>
    <%--<script src="../Assets/MaterialDT/jquery.dataTables.min.js"></script>
    <script src="../Assets/MaterialDT/dataTables.material.min.js"></script>
    <script type="text/javascript">
        $.getScript('../AppJs/UserList.js', function (data, textStatus, jqxhr) {
            console.log('Script File Load Status=' + jqxhr.status);
        });
    </script>--%>

</asp:Content>
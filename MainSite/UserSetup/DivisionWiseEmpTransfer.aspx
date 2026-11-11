<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="DivisionWiseEmpTransfer.aspx.cs" Inherits="UserSetup_DivisionWiseEmpTransfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
        
      <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" /> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    
    
    
    <style>
        
.title-widget {
	color: #898989;
	font-size: 20px;
	font-weight: bold;
	 
	position: relative;
	 
	font-family: 'Fjalla One', sans-serif;
	margin-top: 0;
	margin-right: 0;
	 margin-bottom: 10px;
	 
	 
	padding-left: 12px;

}

.title-widget::before {
    background-color: #D75A4A;
    content: "";
    height: 22px;
    left: 0px;
    position: absolute;
    top: -2px;
    width: 5px;
}
    </style>

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
                             
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" /> Division Wise Employee Transfer  </h1>
                        </div>
                  <div class="page-heading__container float-right d-none d-sm-block">
                      <asp:LinkButton ID="HomeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="HomeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
<%--                    <asp:LinkButton ID="LinkButton3"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>--%>
                    </div>
                    </div>
                     
<div class="card">
   <div class="card-body">
          
      
            

         <style>

             .star{
                 color:red;
             }
              .radioChoice label {
            padding-left: 5px;
            padding-right: 30px;
                  font-size: 16px;
                  font-weight: bold;
        }

         </style>
       
        <div class="form-row" >
              <div class="col-md-12" style="text-align:center">
                  <asp:RadioButtonList runat="server" ID="rbType" CssClass="radioChoice" AutoPostBack="True" OnSelectedIndexChanged="rbType_OnSelectedIndexChanged" RepeatDirection="Horizontal" RepeatLayout="Flow">
                      <asp:ListItem Selected="True" Value="0">Employee Shift &amp; Auto Hierarchy Generate</asp:ListItem>
                      <asp:ListItem Value="1">Only Employee Transfer</asp:ListItem>
                  </asp:RadioButtonList>
                  </div>
                  </div>
       <br/>
       <br/>
       <div class="form-row" >
              <div class="col-md-6">
                                
                    <label style="" class="title-widget"> From </label> 
                                           
             </div>
            <div class="col-md-6">
                                
                    <label style="" class="title-widget"> To </label> 
                                           
             </div>
  </div>  
       
       <div class="row">

             <div class="col-md-6">

                     <div class="form-group ">   
                                 
                                       
                            <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Company:  </label></div>
                                <div class="col-md-6">
                                  <asp:DropDownList runat="server"  AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" CssClass="form-control form-control-sm" />
                                </div>
                            </div>
                          
                           <div style="padding-top: 5px;"></div>
                          <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Division: </label></div>
                                <div class="col-md-6">
                                     <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDivision" class="form-control form-control-sm" OnSelectedIndexChanged="ddlDivision_OnSelectedIndexChanged" />
                                      <script type="text/javascript">
                                          function pageLoad() {
                                              $('#cpFormBody_ddlDivision').chosen({ disable_search_threshold: 5, search_contains: true });
                                              $('#cpFormBody_ddlDepartment').chosen({ disable_search_threshold: 5, search_contains: true });
                                              $('#cpFormBody_ddlWing').chosen({ disable_search_threshold: 5, search_contains: true });
                                              $('#cpFormBody_ddlDesignation').chosen({ disable_search_threshold: 5, search_contains: true });
                                              $('#cpFormBody_ddlSalaryLocation').chosen({ disable_search_threshold: 5, search_contains: true });
                                              $('#cpFormBody_ddlSection').chosen({ disable_search_threshold: 5, search_contains: true });
                                              $('#cpFormBody_ddlSubSection').chosen({ disable_search_threshold: 5, search_contains: true });
                                              $('#cpFormBody_ddlSalaryGrade').chosen({ disable_search_threshold: 5, search_contains: true });
                                              $('#cpFormBody_ddlSalaryStep').chosen({ disable_search_threshold: 5, search_contains: true });
                                              $('#cpFormBody_ddlJobLocation').chosen({ disable_search_threshold: 5, search_contains: true });


                                              $('.select_me2').chosen({ disable_search_threshold: 5, search_contains: true });
                                          }
                                         </script>
                                </div>
                            </div>
                         
                          <div style="padding-top: 5px;"></div>
                          <div class="row"  runat="server" id="wing">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Wing:</label></div>
                                <div class="col-md-6">
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlWing" class="form-control form-control-sm" OnSelectedIndexChanged="ddlWing_OnSelectedIndexChanged" />
                                </div>
                            </div>

                       
                          <div style="padding-top: 5px;"></div>
                          <div class="row" runat="server" id="dept">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Department: </label></div>
                                <div class="col-md-6" >
                                    
                                    <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDepartment" class="form-control form-control-sm" OnSelectedIndexChanged="ddlDepartment_OnSelectedIndexChanged" />
                                      
                                </div>
                            </div>

                           <div style="padding-top: 5px;"></div>
                          <div class="row" runat="server" id="sec">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Section:</label></div>
                                <div class="col-md-6">
                                  <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlSection" class="form-control form-control-sm" OnSelectedIndexChanged="ddlSection_OnSelectedIndexChanged" />

                                </div>
                            </div>
                         
                          <div style="padding-top: 5px;"></div>
                          <div class="row" runat="server" id="subsec">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Sub Section:</label></div>
                                <div class="col-md-6">
                                <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlSubSection" class="form-control form-control-sm" OnSelectedIndexChanged="ddlSubSection_OnSelectedIndexChanged" />

                                </div>
                            </div>

                           
                       </div>
                   </div>      

             <div class="col-md-6">

                     <div class="form-group ">   
                                                               
              
                          <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Division: </label></div>
                                <div class="col-md-6">
                                     <asp:DropDownList runat="server"  ID="ddlTransferDivision" AutoPostBack="True" OnSelectedIndexChanged="ddlTransferDivision_OnSelectedIndexChanged" class="form-control form-control-sm select_me2" />
                                       
                                </div>
                            </div>
                         
                               <div runat="server" Visible="False" id="divTO">
                                   <div style="padding-top: 5px;"></div>
                          <div class="row"  runat="server" id="Div1">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Wing:</label></div>
                                <div class="col-md-6">
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlTransWing" class="form-control form-control-sm  select_me2" OnSelectedIndexChanged="ddlTransWing_OnSelectedIndexChanged" />
                                </div>
                            </div>

                       
                          <div style="padding-top: 5px;"></div>
                          <div class="row" runat="server" id="Div2">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Department: </label></div>
                                <div class="col-md-6" >
                                    
                                    <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlTransDepartment" class="form-control form-control-sm  select_me2" OnSelectedIndexChanged="ddlTransDepartment_OnSelectedIndexChanged" />
                                      
                                </div>
                            </div>

                           <div style="padding-top: 5px;"></div>
                          <div class="row" runat="server" id="Div3">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Section:</label></div>
                                <div class="col-md-6">
                                  <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlTransSection" class="form-control form-control-sm  select_me2" OnSelectedIndexChanged="ddlTransSection_OnSelectedIndexChanged" />

                                </div>
                            </div>
                         
                          <div style="padding-top: 5px;"></div>
                          <div class="row" runat="server" id="Div4">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Sub Section:</label></div>
                                <div class="col-md-6">
                                <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlTransSubSection" class="form-control form-control-sm  select_me2"   />

                                </div>
                            </div>
                               </div>

                       </div>
                   </div>    
              
       </div>
       
       
       

       <div class="row">
           <div class="col-md-2"></div>
                 <div class="col-md-6">
                                        <div class="form-group" style="margin-top: 17px;">
                                            
                                             <asp:LinkButton runat="server" ID="SearchButton" OnClick="SearchButton_OnClick"   CssClass="btn btnMyDesignSearch   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search Information </asp:LinkButton>

<%--                                            <asp:LinkButton runat="server" ID="btnReset" OnClick="btnReset_OnClick" CssClass="btn btnMyDesignReset   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset Information</asp:LinkButton>--%>

                                          
                                        </div> </div>
       </div>

       <br/>
       
       <div id="gridContainer1" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: hidden;">     
                            
  <asp:GridView ID="loadGridView" runat="server" ShowFooter="true" AutoGenerateColumns="false"  CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender" 
     DataKeyNames="EmpInfoId" OnRowCreated="loadGridView_RowCreated">
    <Columns>
       <%-- <asp:BoundField DataField="RowNumber" HeaderText="SL" />--%>
        
       <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
             </asp:TemplateField>
        
          <asp:TemplateField HeaderText="" >
              <HeaderTemplate>
            <asp:CheckBox ID="CheckAll" runat="server" AutoPostBack="true"  OnCheckedChanged="CheckUncheckAll" />
        </HeaderTemplate>
              

            <ItemTemplate>
                <asp:CheckBox runat="server" ID="Checked"  />
            </ItemTemplate>  
        </asp:TemplateField>
        


        <asp:TemplateField HeaderText="Epmployee ID">
            <ItemTemplate>
                
                   <asp:Label ID="lbl_EpmployeeId" runat="server" Text='<%#Eval("EmpMasterCode") %>'></asp:Label>
      
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Employee Name">
            <ItemTemplate>
                <asp:Label ID="lbl_EmployeeName" runat="server" Text='<%#Eval("EmpName") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="Company">
            <ItemTemplate>
                  <asp:Label ID="lbl_Company" runat="server" Text='<%#Eval("CompanyName") %>'></asp:Label>
            </ItemTemplate>  
        </asp:TemplateField>
        
             <asp:TemplateField HeaderText="Division">
            <ItemTemplate>
                  <asp:Label ID="lbl_Division" runat="server" Text='<%#Eval("DivisionName") %>'></asp:Label>
            </ItemTemplate>  
        </asp:TemplateField>
        
          <asp:TemplateField HeaderText="Wing">
            <ItemTemplate>
                  <asp:Label ID="lbl_Wing" runat="server" Text='<%#Eval("DivisionWingName") %>'></asp:Label>
            </ItemTemplate>  
        </asp:TemplateField>
        
         <asp:TemplateField HeaderText="Department">
            <ItemTemplate>
                  <asp:Label ID="lbl_CreateBy" runat="server" Text='<%#Eval("DepartmentName") %>'></asp:Label>
            </ItemTemplate>  
        </asp:TemplateField>
        
         <asp:TemplateField HeaderText="Section">
            <ItemTemplate>
                  <asp:Label ID="lbl_Section" runat="server" Text='<%#Eval("SectionName") %>'></asp:Label>
            </ItemTemplate>  
        </asp:TemplateField>

             <asp:TemplateField HeaderText="SubSection">
            <ItemTemplate>
                  <asp:Label ID="lbl_SubSection" runat="server" Text='<%#Eval("SubSectionName") %>'></asp:Label>
            </ItemTemplate>  
        </asp:TemplateField>
        
     
    </Columns>
</asp:GridView>

                            </div>
       
       <br/>
       
         <div class="row">
           <div class="col-md-2"></div>
                 <div class="col-md-6">
                                        <div class="form-group" style="margin-top: 17px;">
                                            
                                             <asp:LinkButton runat="server" ID="LinkButton1" OnClick="SaveButton_OnClick"   CssClass="btn btnMyDesignSearch   btn-sm"><span aria-hidden="true" class="fa fa-check-square"></span>  &nbsp; Submit  </asp:LinkButton>

                                            <asp:LinkButton runat="server" ID="btnReset" OnClick="btnReset_OnClick" CssClass="btn btnMyDesignReset   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset </asp:LinkButton>

                                          
                                        </div> </div>
       </div>
   


        
       
       
      
       


       

       

       














       
       
       


       






       
                                   


       
      
       

       
       
       

       

       
       

       

       

       
       

        


       


    
       
  

             

       


  

       




      
              </div>
        
        </div>
                               </div>
        </ContentTemplate>
                     
                     
                    
        </asp:UpdatePanel>
        </div>
</asp:Content>


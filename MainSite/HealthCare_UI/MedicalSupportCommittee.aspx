<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="MedicalSupportCommittee.aspx.cs" Inherits="HealthCare_UI_MedicalSupportCommittee" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
    <style>
        fieldset.for-panel {
          
            padding: 10px 8px;
            background-color: white;
            margin-bottom: 12px;
        }

        fieldset.for-panel legend {
               
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


        .chkChoice label {
            padding-left: 4px;
            padding-right: 4px;
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
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    
    
    <div class="content" id="content">
    
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
     <asp:UpdateProgress ID="UpdateProgress4" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait11" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
 
        <div class="content" id="content">
                 <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                             
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" /> Medical Support Committee  </h1>
                        </div><div class="page-heading__container float-right d-none d-sm-block">
                                    <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                        <asp:LinkButton ID="detailsViewButton"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
                    </div>
                    </div>
                     
<div class="card">
   <div class="card-body">
       
         <div class="row">
               <div class="col-md-3">
                   </div>
                    <div class="col-md-6">
                        <div class="form-group ">
                          
                          <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Company:<span   style="color:red; " title="please fill out this field"> * </span></label></div>
                            <div class="col-md-6">  <asp:DropDownList runat="server" Enabled="True"  ID="ddlCompany" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" class="form-control form-control-sm" />
                                
                                            <script type="text/javascript">
                                                function pageLoad() {
                                                    $('#<%=ddlDivision.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });

                                                    $('#<%=ddlDepartment.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });
                                                    $('#<%=ddlDivisionSearch.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });
                                                    $('#<%=ddlDepartmentSearch.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });


                                            

                                          }
               </script>

                            </div>
                          </div>
                           <div style="padding-top: 5px;"></div>
                                <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Routing Path Name:<span   style="color:red;" title="please fill out this field"> * </span></label></div>
                            <div class="col-md-6">  <asp:TextBox runat="server"    ID="TxtRoutingPathName"  class="form-control form-control-sm" /></div>
                                   
                               
                                 
                          </div>
                             <div style="padding-top: 5px;"></div>
                            <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Division:<span   style="color:red; " title="please fill out this field">*  </span></label></div>
                                <div class="col-md-6">  <asp:DropDownList runat="server"  Enabled="True"  ID="ddlDivision"  AutoPostBack="True" OnSelectedIndexChanged="ddlDivision_OnSelectedIndexChanged"  class="form-control form-control-sm" /></div>
                                <asp:HiddenField runat="server" ID="hfDivision"/>
                                <asp:HiddenField runat="server" ID="hfcreateby"/>
                          

                                </div>
                            
                             <div style="padding-top: 5px;"></div>
                            <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Department:<span   style="color:red; " title="please fill out this field"> * </span></label></div>
                                <div class="col-md-6">  <asp:DropDownList runat="server"  ID="ddlDepartment"  class="form-control form-control-sm" Enabled="True" /></div>
                                <asp:HiddenField runat="server" ID="hfDept"/>
                                <asp:HiddenField runat="server" ID="PathId"/>
                   
                            </div>
                            
                            </div>
                        </div>
             </div>
       
           <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;">Search Employee</h2>
                            <hr/>
          <div class="row">
                                <div class="col-md-1" style="padding-top: 8px">  <label class="control-label">Division:</label></div>
                                <div class="col-md-2">  <asp:DropDownList runat="server"   ID="ddlDivisionSearch"  AutoPostBack="True" OnSelectedIndexChanged="ddlDivisionSearch_OnSelectedIndexChanged" class="form-control form-control-sm" /></div>
              
              <div class="col-md-1" style="padding-top: 8px">  <label class="control-label ">Department:</label></div>
                                <div class="col-md-2">  <asp:DropDownList runat="server"   ID="ddlDepartmentSearch"  class="form-control form-control-sm" /></div>
              <div class="col-md-1">
                       <asp:LinkButton runat="server" ID="LinkButton1" OnClick="Button1_OnClick"   CssClass="btn btn-success   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search </asp:LinkButton>
              </div>
              
            
                            </div>
       <br/>
       
       <div id="gridContainer2" style="height: 200px; overflow: auto; width: auto; overflow-y: scroll; overflow-x: hidden;">
           
           <asp:GridView Width="100%" ShowHeader="True" ID="gv_allocateEmp" runat="server" AutoGenerateColumns="false" CssClass="blueTableNew"   OnPreRender="gv_DocumentUpload_PreRender">

         <%--    <asp:GridView ID="gv_allocateEmp" Width="100%" Height="50%" CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="false" runat="server">--%>
                            <Columns>
                                <asp:TemplateField>
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
                                        <asp:HiddenField runat="server" ID="txt_empInfoId" Value='<%#Eval("EmpInfoId") %>' />
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee ID">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_empId" runat="server"  Text='<%#Eval("EmpMasterCode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_name" runat="server"  Text='<%#Eval("EmpName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_designation" runat="server"   Text='<%#Eval("Designation") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                
                                <asp:TemplateField HeaderText="Position">
                                    <ItemTemplate>
                               <%--         <asp:HiddenField runat="server" ID="hfPosition" Value='<%#Eval("Position")%>' />--%>

                                        <asp:RadioButtonList runat="server"    ID="chkPosition" AutoPostBack="False" OnSelectedIndexChanged="chkPosition_OnSelectedIndexChanged"  CssClass="chkChoice" RepeatDirection="Horizontal">

                                            <asp:ListItem>Doctor</asp:ListItem>
                                            <asp:ListItem>Convenor</asp:ListItem>
                                            <asp:ListItem>Secretary</asp:ListItem>
                                            <asp:ListItem>Other</asp:ListItem>
                                        </asp:RadioButtonList>
                                                
                                                 
                                    </ItemTemplate>
                                </asp:TemplateField>


                           
                            </Columns>
                        </asp:GridView>
                                
       </div>

       <br/>
       
       <br/>
       <div class="row">
           <div class="col-4"></div>
           <div class="col-4"></div>
           <div class="col-4">
               <asp:LinkButton runat="server" CssClass="btn btnMyDesignAddtoList btn-sm pull-right" OnClick="textButton_OnClick" ><span aria-hidden="true" class="fa fa-plus"></span>  &nbsp;Add To List</asp:LinkButton>
           </div>
       </div>
       <br/>
       
       <div id="gridContainer1" style="height: 200px; overflow: auto; width: auto; overflow-y: scroll; overflow-x: hidden;">


                <asp:GridView ID="gv_SaveGridView" Width="100%" CssClass="AddToListCssTable" OnPreRender="gv_DocumentUpload_PreRender" AutoGenerateColumns="false" runat="server">
                            <Columns>
                                
                                    <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                       
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approval Sequence">
                                    <ItemTemplate>
                                     <asp:DropDownList ID="SequenceId" runat="server" AutoPostBack="False" OnSelectedIndexChanged="SequenceId_OnSelectedIndexChanged"  class="form-control form-control-sm"  >
                                      
                                        </asp:DropDownList>
                                        <asp:HiddenField runat="server" ID="HF_SequenceId" Value='<%#Eval("SequenceId") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee ID">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_empId" runat="server"  Text='<%#Eval("EmpMasterCode") %>'></asp:Label>
                                        <asp:HiddenField runat="server" ID="txt_empInfoId" Value='<%#Eval("EmpInfoId") %>' />
                                     
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_name" runat="server"  Text='<%#Eval("EmpName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_designation" runat="server"   Text='<%#Eval("Designation") %>'></asp:Label>
                                        
                                    </ItemTemplate>
                                  
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Position">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_Position" runat="server"   Text='<%#Eval("Position") %>'></asp:Label>
                                        
                                    </ItemTemplate>
                                  
                                </asp:TemplateField>
                                
                                    <asp:TemplateField HeaderText="Remove">
                                                            <ItemTemplate>
                                                                <asp:LinkButton CssClass="btn btn-sm btn-danger" ID="deleteImageButtonDirectlySupervices" runat="server" OnClick="deleteImageButtonDirectlySupervices_OnClick"> <i class="fa fa-minus-circle"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                                
       
       </div>
       

       <br/>
   

       <div class="row">
                                
           <div class="col-md-3">
               <div class="form-group">
                   <asp:LinkButton ID="btn_Save"  CssClass="btn btn-sm  btnMyDesignSave" Visible="False" runat="server"   OnClick="Btn_Save"><i class="fa fa-check "></i>&nbsp; Submit Information</asp:LinkButton>
                   <asp:LinkButton ID="editButton"   CssClass="btn btn-sm  btnMyDesignSave" Visible="False" runat="server" OnClick="editButton_OnClick" ><i class="fa fa-check "></i>&nbsp; Update Information</asp:LinkButton>
                   <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm  btnMyDesignReset" Visible="False" runat="server" OnClick="delButton_OnClick" />
                   <asp:LinkButton ID="cancelButton"   CssClass="btn btn-sm  btnMyDesignReset" runat="server" OnClick="cancelButton_OnClick"   ><i class="fa fa-refresh"></i>&nbsp; Reset Information</asp:LinkButton>
                                        
               </div>
           </div>
           <div class="col-md-3">
           </div>

           <div class="col-md-4">
               <div >
                   <asp:TextBox runat="server"  Visible="False" ID="hiddencreateDate"  class="form-control form-control-sm" />
        
               </div>
            
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


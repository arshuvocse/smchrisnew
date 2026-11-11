<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="SubcommitteeSetup.aspx.cs" Inherits="MeetingMinors_SubcommitteeSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    
       <div class="content" id="content">

                 <div class="container-fluid">
                     
                     <asp:UpdatePanel runat="server">
      <ContentTemplate>
          
           <asp:UpdateProgress ID="UpdateProgress4" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait11" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" /> Sub-Committee  Setup </h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="detailsViewButton" Text="List Information " CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>
                    </div>
                     
<div class="card">
   <div class="card-body">
       <style>
           
        .Label_Title {
            background-color: #C7C7C7;
            width: 100%;
            text-align: center;
            margin: 0px;
            padding: 6px;
            text-align: center;
            color: #000;
            margin-right: 5%;
            font-weight: bold;
            font-size: 13px;
        }
        .SelectchkChoice label {
            padding-left: 6px;
            font-weight: bold;
        }

         .chkChoice label {
            padding-left: 2px;
            padding-right: 5px;
        }
       </style>
        <div class="row" runat="server" >
               <div class="col-md-3">
                   </div>
                    <div class="col-md-6">
                        <div class="form-group ">
                                   
                             
                            
                          <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Company:<span   style="color:red; " title="please fill out this field"> * </span></label></div>
                            <div class="col-md-6"> <asp:DropDownList runat="server"   ID="ddlCompany"   AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged"   class="form-control form-control-sm" /> </div>
                          </div>
                               <div style="padding-top: 5px;"></div>
                            
                            <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Meeting Category:<span   style="color:red; " title="please fill out this field"> * </span></label></div>
                            <div class="col-md-6">  <asp:DropDownList runat="server"   ID="ddlCategory"    class="form-control form-control-sm SelectMe33" >
                               
                                </asp:DropDownList>
                                          <script type="text/javascript">
                                               function pageLoad() {

                                                   $('.SelectMe33').chosen({ disable_search_threshold: 5, search_contains: true });


                                               }
</script>
                                </div>
                          </div>
                            <div style="padding-top: 5px;"></div>    
                            <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Sub-Committee Name: <span   style="color:red; " title="please fill out this field"> * </span></label></div>
                                <div class="col-md-6">  <asp:TextBox runat="server"     ID="txtSubCommittee"  class="form-control form-control-sm" /></div>
                            </div>
                              <div style="padding-top: 5px;"></div>    
                              <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Remarks: </label></div>
                                <div class="col-md-6">  <asp:TextBox runat="server"    ID="txtRemarks"  Rows="3" class="form-control" /></div>
                            </div>
                            
                              <div style="padding-top: 5px;"></div>    
                              <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  </div>
                                <div class="col-md-6"><asp:CheckBox runat="server" ID="isActive" Text="Is Active" Checked="True"/>  </div>
                            </div>

                            </div>
                        </div>
             </div>
       
          <div class="row">
         
                                                            <div class="col-md-12">
                                                               
                                                                <div class="Label_Title">Member List</div>
                                                              
                                                                <div class="form-group">
                                                                    <div style="overflow: scroll; height: 300px">
                                                                        <br/>
                                                                         <asp:CheckBox runat="server" ID="SSGradeCheck" Visible="False" CssClass="SelectchkChoice" AutoPostBack="True" OnCheckedChanged="SSGradeCheck_OnCheckedChanged" Text=" Select All / Unselect All" />
                                                                        <br />
                                                                        <asp:CheckBoxList ID="gradeCheckBoxList"  Visible="False"   CssClass="chkChoice" RepeatColumns="1" RepeatDirection="Horizontal" runat="server"></asp:CheckBoxList>
                                                                        
                                                                                <asp:GridView Margin="0,5,0,0" ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable"    OnPreRender="gv_DocumentUpload_PreRender">
                                                    <Columns>
                                                        
                                                         <asp:TemplateField HeaderText="SL#">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                         <asp:TemplateField HeaderText="Select / Unselect">
                                                            <ItemTemplate>
                                                               <asp:CheckBox runat="server" ID="chkSelect"/>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                           <asp:TemplateField HeaderText="Member Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_NameList" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                                  <asp:HiddenField runat="server" ID="hfMemberSetupDetailsID" Value='<%#Eval("MemberSetupDetailsID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                          <asp:TemplateField HeaderText="Present Status">
                                                            <ItemTemplate>
                                                             <asp:DropDownList  Width="350px"  runat="server"   ID="ddlMemberType"   class="form-control form-control-sm SelectMe33" /> 
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                         <asp:TemplateField HeaderText="Position">
                                                            <ItemTemplate>
                                                                
                                                                   <asp:DropDownList runat="server"  Width="350px"    ID="ddlPosition"   class="form-control form-control-sm SelectMe33" /> 
                                                              
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                        </Columns>
                                                                             </asp:GridView>
                                                                        
                                                                        
                                                                        
                                                                       
                                                                    </div>
                                                                </div>
                                                                <br/>
                                                                
                                                                <div class="Label_Title">Add Member</div>
                                                              
                                                                <div class="form-group">
                                                                    <div style="overflow: scroll; height: 300px">
                                                                                  <asp:GridView Width="100%" ShowHeader="True" ID="gv_Member" runat="server" AutoGenerateColumns="false" CssClass="blueTableNew"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                           
                                                 <asp:TemplateField HeaderText="Type">
                                                    <ItemTemplate>
                                                     <asp:RadioButtonList runat="server"  ID="rbType" CssClass="chkChoice" AutoPostBack="True" OnSelectedIndexChanged="rbType_OnSelectedIndexChanged" RepeatDirection="Horizontal">
                                                               
                
                 <asp:ListItem>Employee</asp:ListItem>
                    <asp:ListItem>Guest</asp:ListItem>
                                                           </asp:RadioButtonList>
                                                        
                                                        <asp:HiddenField runat="server" ID="hfType" Value='<%#Eval("Type")%>' />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 
                                                <asp:TemplateField HeaderText="Employee ID">
                                                    <ItemTemplate>
                                                        <asp:TextBox ReadOnly="True" ID="txt_EmpMasterCode" CssClass="form-control form-control-sm" runat="server" Text='<%#Eval("EmpMasterCode") %>'></asp:TextBox>
                                                        
                                                        <asp:HiddenField runat="server" ID="MemEmpInfoId" Value='<%#Eval("EmpInfoId")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                             
                                                 <asp:TemplateField HeaderText="Employee Name">
                                                    <ItemTemplate>
                                                        
                                                          <asp:DropDownList Width="50%" ID="ddlEmpName" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpName_OnSelectedIndexChanged" Visible="False"  CssClass="form-control form-control-sm SelectMe33"  runat="server"  ></asp:DropDownList>

                                                        <asp:TextBox ID="txt_EmpName" Visible="False"  CssClass="form-control form-control-sm"  runat="server" Text='<%#Eval("EmpName") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                 <asp:TemplateField HeaderText="Designation">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_Designation"  CssClass="form-control form-control-sm"  runat="server" Text='<%#Eval("Designation") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                            
                                                <asp:TemplateField HeaderText="Actions">
                                                    <ItemTemplate>
                                                        
                                                        <asp:LinkButton runat="server" ID="btn_gv_MemberAdd"   OnClick="btn_gv_MemberAdd_OnClick" CssClass="btn btn-sm btn-success"><i class="fa fa-plus"></i> </asp:LinkButton> 
                                                      <asp:LinkButton runat="server" ID="btn_gv_Member" OnClick="btn_gv_Member_OnClick"   CssClass="btn btn-sm btn-danger"><i class="fa fa-minus-circle"></i> </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                
                                       
                                                
                                                
                                                
                                            </Columns>
                                        </asp:GridView>
                                                                           </div>
                                                                </div>
                                                            </div>
                                                        </div>
              
              
               <div class="row">
                                <div class="col-md-3">
                   </div>
       <div class="col-md-3">
           <div class="form-group">
               <asp:HiddenField runat="server" ID="id_mastetID"/>
               <asp:Button ID="Button2" Text="Submit" CssClass="btn btn-sm btn-info" Visible="False"   runat="server" OnClick="btn_Save_OnClick" />
               <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
               <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />
              
                                        
           </div>
       </div>
       <div class="col-md-3">
       </div>

       <div class="col-md-4">
       </div>
   </div>

             </div>
       </div>
    </div>
          </ContentTemplate>
                         </asp:UpdatePanel>
                     </div>
           </div>
</asp:Content>


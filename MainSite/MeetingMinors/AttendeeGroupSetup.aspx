<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="AttendeeGroupSetup.aspx.cs" Inherits="MeetingMinors_AttendeeGroupSetup" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
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
            padding-left: 10px;
            padding-right: 30px;
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

                 <div class="container-fluid">
                     
                     <asp:UpdatePanel runat="server">
      <ContentTemplate>
            <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" /> Board Member Information </h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="detailsViewButton" Text="List Information " CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>
                    </div>
                     
<div class="card">
   <div class="card-body">
       
         <div class="row" runat="server">
               <div class="col-md-3">
                   </div>
                    <div class="col-md-6">
                        <div class="form-group ">
                                   
                             
                            
                          <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Company:<span   style="color:red; " title="please fill out this field"> * </span></label></div>
                            <div class="col-md-6">  <asp:DropDownList runat="server"   ID="ddlCompany"     class="form-control form-control-sm" /></div>
                          </div>
                               <div style="padding-top: 5px;"></div>
                            
                            <div class="row" runat="server" Visible="False">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Board Member Name:<span   style="color:red; " title="please fill out this field"> * </span></label></div>
                            <div class="col-md-6">
                                <asp:TextBox runat="server"   ID="txtBoardMemberName"  class="form-control  form-control-sm" />
                                 <%-- <asp:DropDownList runat="server"   ID="ddlCategory"    class="form-control form-control-sm" >
                               
                                </asp:DropDownList>--%>
                                </div>
                          </div>
                               <div style="padding-top: 5px;"></div>    
                              <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  </div>
                                <div class="col-md-6"><asp:CheckBox runat="server" ID="isActive" Text="Is Active" Checked="True"/>  </div>
                            </div>

                             <div style="padding-top: 5px;"></div>    
                            <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Effective From Date :<span   style="color:red; " title="please fill out this field"> * </span> </label></div>
                                <div class="col-md-6">  <asp:TextBox runat="server"     ID="txtStartDate"  class="form-control  form-control-sm"  />
                                    
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                        TargetControlID="txtStartDate" />
                                </div>
                            </div>
                            
                            
                             <div style="padding-top: 5px;"></div>    
                            <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Effective To Date:<span   style="color:red; " title="please fill out this field"> * </span> </label></div>
                                <div class="col-md-6">  <asp:TextBox runat="server"   ID="txtEndDate"   class="form-control  form-control-sm"   />
                                    
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server"
                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                        TargetControlID="txtEndDate" />
                                </div>
                            </div>

                            <div style="padding-top: 5px;"></div>    
                            <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Remarks: </label></div>
                                <div class="col-md-6">  <asp:TextBox runat="server"  Rows="3" TextMode="MultiLine"  ID="TxtDescription"  class="form-control" />
                                    
                                      
                                </div>
                            </div>
                            

                            </div>
                        </div>
             </div>
       
       
          
        <div class="row" runat="server" Visible="False">

           <div class="col-md-6">
                <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Company:<span   style="color:red; " title="please fill out this field"> * </span></label></div>
                            <div class="col-md-6">  </div>

                           
                          </div>
                <div style="padding-top: 5px;"></div>
                <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Member Type:<span   style="color:red; " title="please fill out this field"> * </span></label></div>
                            <div class="col-md-6"> <asp:DropDownList runat="server"   ID="ddlMemberType"  class="form-control form-control-sm" />  <asp:TextBox runat="server"   ID="txtMemberType" Visible="False"  class="form-control form-control-sm" /></div>

                           <asp:HiddenField ID="MemberMasterdHiddenField" runat="server"/>
                          </div>
                <div style="padding-top: 5px;"></div>
               

               <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Name:<span   style="color:red; " title="please fill out this field"> * </span></label></div>
                            <div class="col-md-6">  <asp:TextBox runat="server"   ID="TxtName"  class="form-control form-control-sm" /></div>
                          </div>
               
                  <div style="padding-top: 5px;"></div>
                    <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Address:</label></div>
                            <div class="col-md-6">  <asp:TextBox runat="server"   ID="TxtAddress"  class="form-control form-control-sm" /></div>
                          </div>
                  <div style="padding-top: 5px;"></div>
                 <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Phone No.:</label></div>
                            <div class="col-md-6">  <asp:TextBox runat="server"   ID="TxtPhone"  class="form-control form-control-sm" /></div>
                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtenderconvRate" runat="server"
                                                    Enabled="True" TargetControlID="TxtPhone" FilterType="Custom" ValidChars="+-0123456789 "></asp:FilteredTextBoxExtender>
                     
                          </div>
                  <div style="padding-top: 5px;"></div>
                 <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Email:</label></div>
                            <div class="col-md-6">  <asp:TextBox runat="server"  TextMode="Email"  ID="TxtEmail"  class="form-control form-control-sm" /></div>
                          </div>
           </div>
            
            
             <div class="col-md-6">
                     <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Joining Date:</label></div>
                            <div class="col-md-6">  <asp:TextBox   runat="server"   ID="txtJoiningDate"  class="form-control form-control-sm" />
                                   <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                        TargetControlID="txtJoiningDate" />
                            </div>
                          </div>
                    <div style="padding-top: 5px;"></div>
                    <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Membership Date:</label></div>
                            <div class="col-md-6">  <asp:TextBox   runat="server"   ID="TxtMembershipDate"  class="form-control form-control-sm" />
                                   <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                        TargetControlID="TxtMembershipDate" />
                            </div>
                          </div>
               
                  <div style="padding-top: 5px;"></div>
                    <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Note:</label></div>
                            <div class="col-md-6">  <asp:TextBox runat="server"  Rows="3" TextMode="MultiLine"  ID="TxtNote"  class="form-control" /></div>
                          </div>
                  <div style="padding-top: 20px;"></div>
                 
                  <div class="row">
                                <div class="col-md-3"> </div>
                            <div class="col-md-6">   <asp:LinkButton runat="server" ID="btn_Save"  OnClick="btnAddtolist_OnClick"  CssClass="btn btn-success   btn-sm pull-right"><span aria-hidden="true" class="fa fa-plus"></span>  &nbsp; Add to List </asp:LinkButton> </div>
                          </div>
               
           </div>
            </div>

       
       <div class="row">

           <div class="col-md-4">
           </div>
           <div class="col-md-4">
               <div class="form-row">
                   <div class="col-md-2"></div>
                   <div class="col-md-6">
                   
                   </div>
                  
               </div>
               <div class="form-group">
               </div>
           </div>

           <div class="col-md-4">
           </div>
       </div>
       

       <br/>

<%--       <table class="blueTable">
           <tr>
    <th>SL#</th>
    <th>Name</th>
    <th>Address</th> 
    <th>Phone No.</th>
    <th>Email</th>
    <th>Membership Date</th>
    <th>Note</th>
    <th>Actions</th>
     

  </tr>
  
  
  <tr>
    <td>1</td>
    <td>  </td>
    <td>  </td>
    <td>  </td>
    <td>  </td>
    <td>  </td>
    <td>  </td>
    <td> <asp:LinkButton runat="server" ID="LinkButton1"   CssClass="btn btn-sm btn-warning"><span aria-hidden="true" class="fa fa-edit"></span>  </asp:LinkButton>  <asp:LinkButton runat="server" ID="LinkButton2"   CssClass="btn btn-sm btn-danger"><i class="fa fa-minus-circle"></i> </asp:LinkButton></td>
     

  </tr>
           
 
       </table>--%>
       
       
            <div style="overflow: scroll; width: 100%" runat="server" Visible="False">
                                                <asp:GridView Margin="0,5,0,0" ID="gv_Children" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable"    OnPreRender="gv_DocumentUpload_PreRender">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL#">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                           <asp:TemplateField HeaderText="Company">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Company" runat="server" Text='<%#Eval("Company") %>'></asp:Label>
                                                                  <asp:HiddenField runat="server" ID="hfCompanyId" Value='<%#Eval("CompanyId") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Member Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_MemberType" runat="server" Text='<%#Eval("MemberType") %>'></asp:Label>
                                                                  <asp:HiddenField runat="server" ID="hfMeetingMemberTypeId" Value='<%#Eval("MeetingMemberTypeId") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Name" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Address">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Address" runat="server" Text='<%#Eval("Address") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Email">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Email" runat="server" Text='<%#Eval("Email") %>'></asp:Label>
                                                                <%--<asp:HiddenField runat="server" ID="hfChildrenOccupation" Value='<%#Eval("ChildrenOccupation") %>' />--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Phone">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Phone" runat="server" Text='<%#Eval("MobileNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                          <asp:TemplateField HeaderText="Joining Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_JoiningDate" runat="server" Text='<%#Eval("JoiningDate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Membership Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Date" runat="server" Text='<%#Eval("MembershipDate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Note">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Note" runat="server" Text='<%#Eval("Note") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Actions">
                                                            <ItemTemplate>
                                                                
                                                                               
                                                        <asp:LinkButton runat="server" ID="btnEdit" OnClick="lb_EditMember_OnClick"   CssClass="btn btn-sm btn-warning"><i class="fa fa-pencil-square-o"></i> </asp:LinkButton>
                                                      <asp:LinkButton runat="server" ID="btnRemove" OnClick="lb_RemoveMember_OnClick"   CssClass="btn btn-sm btn-danger"><i class="fa fa-minus-circle"></i> </asp:LinkButton>
                                                                 
                                                             <%--   <asp:LinkButton runat="server" ID="LinkButton1" OnClick="lb_EditMember_OnClick"  CssClass="btn btn-sm btn-warning"><span aria-hidden="true" class="fa fa-edit"></span>  </asp:LinkButton>--%>
                                                                
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                     
                                                    </Columns>
                                                </asp:GridView>
                
                

                                            </div>

       
          <br/> 
      
       
   
        <div class="row">
            
                                                            <div class="col-md-12">
                                                               
                                                                <div class="Label_Title">Member List</div>
                                                              
                                                                <div class="form-group">
                                                                    <div style="overflow: scroll; height: 300px">
                                                                        <br/>
                                                                         <asp:CheckBox runat="server" ID="SSGradeCheck" Visible="False" CssClass="SelectchkChoice" AutoPostBack="True" OnCheckedChanged="SSGradeCheck_OnCheckedChanged" Text=" Select All / Unselect All" />
                                                                        <br />
                                                                        <asp:CheckBoxList ID="gradeCheckBoxList"  Visible="False" CssClass="chkChoice" RepeatColumns="1" RepeatDirection="Horizontal" runat="server"></asp:CheckBoxList>
                                                                        
                                                                         <asp:GridView Margin="0,5,0,0" ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable"    OnPreRender="gv_DocumentUpload_PreRender">
                                                    <Columns>
                                                        
                                                         <asp:TemplateField HeaderText="SL#">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                         <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkSelectAll" runat="server" CssClass="form-control-sm" AutoPostBack="True" OnCheckedChanged="chkSelectAll_OnCheckedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" CssClass="form-control-sm"   runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                                           <asp:TemplateField HeaderText="Member Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_NameList" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                                  <asp:HiddenField runat="server" ID="hfMemberSetupDetailsID" Value='<%#Eval("MemberSetupDetailsID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                          <asp:TemplateField HeaderText="Member Type">
                                                            <ItemTemplate>
                                                              <asp:DropDownList runat="server"   ID="ddlMemberTypeList"  class="form-control form-control-sm">
                                    
                                    
                                    
                                 </asp:DropDownList>
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
           <div class="form-group">
               <asp:Button ID="Button2" Text="Submit" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="btn_Save_OnClick" />
               <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
               <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />
               <asp:Button ID="cancelButton" Text="Cancel" CssClass="btn btn-sm warning" runat="server" OnClick="cancelButton_OnClick" Visible="False" BackColor="#FFCC00" />
                                        
           </div>
       </div>
       <div class="col-md-3">
       </div>

       <div class="col-md-4">
       </div>
   </div>
       
       

              </div>
        
        </div>
            </ContentTemplate>
  </asp:UpdatePanel>
                               </div>
        
        </div>
    

</asp:Content>


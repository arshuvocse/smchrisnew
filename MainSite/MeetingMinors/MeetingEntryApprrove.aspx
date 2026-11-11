<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="MeetingEntryApprrove.aspx.cs" Inherits="MeetingMinors_MeetingEntryApprrove" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
      <style>
           .Label_Title {
            background-color: #C7C7C7;
            width: 100%;
            text-align: center;
            margin: 0px;
            padding: 5px;
            text-align: center;
            color: #000;
            margin-right: 5%;
            font-weight: bold;
            font-size: 13px;
        }
           fieldset.for-panel {
               background-color: #fcfcfc;
               border: 1px solid #999;
               padding: 15px 10px;
               background-color: white;
               margin-bottom: 12px;
           }

           .buttonFinish {
               display: none !important;
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

       
          .chkChoice label {
              padding-left: 4px;
              padding-right: 4px;
          }
          .modal-dialog2 {
              max-width: 90% !important;
                margin-left: 14%;
margin-top: 1.75rem;
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
    
      <div class="modal fade" id="exampleModal2" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
   <div class="modal-dialog  modal-lg" style=" width: 90% !important; " role="document">
  <asp:UpdatePanel runat="server">
      <ContentTemplate>
            <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
              <div class="modal-content">
         <div class="modal-header">
            
               
                  
                  <h3 class="modal-title" id="exampleModalLabel2"  style="color:#2196F3; text-shadow:  0 0 1px black;">Add More Member</h3>
                  
                   
               
           
            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
         </div>
         <div class="modal-body">
                 <div class="row">
                            <div class="col-12">
                               
           <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;">Search Member</h2>
                           
          <div class="row">
               
                            
               <div class="col-md-1" style="padding-top: 8px">  <label class="control-label">Division:</label></div>
                                <div class="col-md-4">  <asp:DropDownList runat="server"   ID="ddlDivision"   OnSelectedIndexChanged="ddlDivision_OnSelectedIndexChanged" AutoPostBack="True"  class="form-control form-control-sm" ></asp:DropDownList> 
                                    
                                      <%--<script type="text/javascript">
                                          function pageLoad() {
                                              $('#<%=ddlDivision.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });

                         $('#<%=ddlDepartment.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });
                     }
               </script>--%>

                                </div>
             
              <div class="col-md-1" style="padding-top: 8px">  <label class="control-label ">Department:</label></div>
                                <div class="col-md-4">  <asp:DropDownList runat="server"   ID="ddlDepartment"  class="form-control form-control-sm" ></asp:DropDownList></div>
              <div class="col-md-2">
                       <asp:LinkButton runat="server" ID="btnSearch"   CssClass="btn btn-success   btn-sm" OnClick="btnSearch_OnClick"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search </asp:LinkButton>
              </div>
                 
                            </div>
       <br/>
                                
                              <div class="row">
                                  <div class="col-md-12">
                                        <asp:GridView Width="100%" ShowHeader="True" ID="gv_EmpListSearch" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkSelectAll" runat="server" CssClass="form-control-sm" AutoPostBack="True" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" CssClass="form-control-sm"   runat="server" />
                                                
                                                 <asp:HiddenField runat="server" ID="hfEmpInfoId" Value='<%#Eval("EmpInfoId")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                 
                                                <asp:TemplateField HeaderText="Employee ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_EmpMasterCode" runat="server" Text='<%#Eval("EmpMasterCode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                             
                                                 <asp:TemplateField HeaderText="Employee Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_EmpName" runat="server" Text='<%#Eval("EmpName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                 <asp:TemplateField HeaderText="Designation">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Designation" runat="server" Text='<%#Eval("Designation") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                            </Columns>
                                        </asp:GridView>
                                  </div>
                              </div>
                                                        
      
       
              <br/>
       <div class="row">
           <div class="col-4"></div>
           <div class="col-4"></div>
           <div class="col-4">
               <asp:Button runat="server" CssClass="btn btn-outline-success btn-block disabled btn-sm" ID="btnAddToListEmp" OnClick="btnAddToListEmp_OnClick" Text="Add To List"/>
           </div>
       </div>
                            </div>
               
               
                   
                            
                         

                        </div>
             
              
         </div>
         <div class="modal-footer"> <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button> </div>
      </div>
      </ContentTemplate>
  </asp:UpdatePanel>
   </div>
</div>
    
       <div class="modal fade" id="exampleModalAPPEmpp" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
   <div class="modal-dialog  modal-lg" style=" width: 90% !important; " role="document">
  <asp:UpdatePanel runat="server">
      <ContentTemplate>
              <div class="modal-content">
         <div class="modal-header">
            
               
                  
                  <h3 class="modal-title" id="exampleModalLabelApp"  style="color:#2196F3; text-shadow:  0 0 1px black;">Add More Member</h3>
                  
                   
               
           
            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
         </div>
         <div class="modal-body">
                 <div class="row">
                            <div class="col-12">
                               
           <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;">Search Member</h2>
                           
          <div class="row">
               
                            
               <div class="col-md-1" style="padding-top: 8px">  <label class="control-label">Division:</label></div>
                                <div class="col-md-4">  <asp:DropDownList runat="server"   ID="ddlDivisionAPP"   OnSelectedIndexChanged="ddlDivisionAPP_OnSelectedIndexChanged" AutoPostBack="True"  class="form-control form-control-sm" ></asp:DropDownList> 
                                    
                                      <%--<script type="text/javascript">
                                          function pageLoad() {
                                              $('#<%=ddlDivision.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });

                         $('#<%=ddlDepartment.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });
                     }
               </script>--%>

                                </div>
             
              <div class="col-md-1" style="padding-top: 8px">  <label class="control-label ">Department:</label></div>
                                <div class="col-md-4">  <asp:DropDownList runat="server"   ID="ddlDepartmentAPP"  class="form-control form-control-sm" ></asp:DropDownList></div>
              <div class="col-md-2">
                       <asp:LinkButton runat="server" ID="btnSearchAPP"   CssClass="btn btn-success   btn-sm" OnClick="btnSearchAPP_OnClick"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search </asp:LinkButton>
              </div>
                 
                            </div>
       <br/>
                                
                              <div class="row">
                                  <div class="col-md-12">
                                        <asp:GridView Width="100%" ShowHeader="True" ID="gv_EmpListSearchAPP" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkSelectAllAPP" runat="server" CssClass="form-control-sm" AutoPostBack="True" OnCheckedChanged="chkSelectAllAPP_CheckedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelectAPP" CssClass="form-control-sm"   runat="server" />
                                                
                                                 <asp:HiddenField runat="server" ID="hfEmpInfoIdAPP" Value='<%#Eval("EmpInfoId")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                 
                                                <asp:TemplateField HeaderText="Employee ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_EmpMasterCodeAPP" runat="server" Text='<%#Eval("EmpMasterCode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                             
                                                 <asp:TemplateField HeaderText="Employee Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_EmpNameAPP" runat="server" Text='<%#Eval("EmpName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                 <asp:TemplateField HeaderText="Designation">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_DesignationAPP" runat="server" Text='<%#Eval("Designation") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                            </Columns>
                                        </asp:GridView>
                                  </div>
                              </div>
                                                        
      
       
              <br/>
       <div class="row">
           <div class="col-4"></div>
           <div class="col-4"></div>
           <div class="col-4">
               <asp:Button runat="server" CssClass="btn btn-outline-success btn-block disabled btn-sm" ID="btnAddToListEmpAPP" OnClick="btnAddToListEmpAPP_OnClick" Text="Add To List"/>
           </div>
       </div>
                            </div>
               
               
                   
                            
                         

                        </div>
             
              
         </div>
         <div class="modal-footer"> <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button> </div>
      </div>
      </ContentTemplate>
  </asp:UpdatePanel>
   </div>
</div>
    
<div class="modal fade" id="exampleModalAPP" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
   <div class="modal-dialog  modal-lg" role="document">
       <asp:UpdatePanel runat="server">
         <ContentTemplate>
              <div class="modal-content">
         <div class="modal-header">
            
               
                  
                  <h3 class="modal-title" id="exampleModalLabels"  style="color:#2196F3; text-shadow:  0 0 1px black;">Routing Path</h3>
                  
                   
               
           
            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
         </div>
         <div class="modal-body">
                 <div class="row">
                            <div class="col-2">
                                <div class="form-group">
                                     
                                      
                                </div>
                               
                            </div>
               
               
                            <div class="col-md-12">
                                                               
                                                                <div class="Label_Title">Routing Path List</div>
                                                              
                               <br/>
                                <div class="form-group">
                                               <div style="overflow: scroll; height: 230px">
                                    <asp:RadioButtonList ID="rbRoutingPath" CssClass="chkChoice"   repeatColumns="5" RepeatDirection="Horizontal" runat="server"/>
                                                   </div>

                               
                              
                                
                              </div>


                            </div>
                            
                         

                        </div>
             
              
         </div>
         <div class="modal-footer"><asp:LinkButton  runat="server" class="btn btn-success" OnClick="btnOkayRoutingPath_OnClick" ID="btnOkayRoutingPath"  >Okay</asp:LinkButton><button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button> </div>
      </div>
         </ContentTemplate>
     </asp:UpdatePanel>
   </div>
</div>
     
    
    <div class="content" id="content">
                 <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                             
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" /> Meeting Approval </h1>
                        </div>
                           <div class="page-heading__container float-right d-none d-sm-block">
                                   <span class="alert alert-info"><span style="font-weight: bold">Next Approver:</span>  <asp:Label ID="lblNextApp" runat="server"></asp:Label></span>
                        <asp:Button ID="detailsViewButton" Text="List Information " Visible="False" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>
                    </div>

               

<div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" style="width: 90%" aria-labelledby="exampleModalLabel" aria-hidden="true">
   <div class="modal-dialog modal-dialog2 " role="document">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
              <div class="modal-content">
         <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">
               
                  
                         <asp:RadioButtonList runat="server" AutoPostBack="True" ID="rbLocation" OnSelectedIndexChanged="rbLocation_OnSelectedIndexChanged" CssClass="chkChoice" RepeatDirection="Horizontal">
                                                               
                
                 <asp:ListItem Value="Office">Office Premisis</asp:ListItem>
                    <asp:ListItem Value="Outer">Outer Premisis</asp:ListItem>
                    <asp:ListItem Value="Virtual">Virtual Meeting</asp:ListItem>
                                                           </asp:RadioButtonList>
                  
                   
               
            </h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
         </div>
         <div class="modal-body">
             <div class="row">
                 <div class="col-md-4">
                       <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;">Office Premisis</h2>
                       <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Office
:</label></div>
                            <div class="col-md-8">  <asp:DropDownList runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOffice_OnSelectedIndexChanged"  ID="ddlOffice" CssClass="form-control form-control-sm" /></div>
                          </div>
                     
                      <div style="padding-top: 5px;"></div>
                           <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Location

:</label></div>
                            <div class="col-md-8">  <asp:DropDownList runat="server"   ID="ddlLocation" AutoPostBack="True" OnSelectedIndexChanged="ddlLocation_OnSelectedIndexChanged" CssClass="form-control form-control-sm" /></div>
                          </div>
                     
                      <div style="padding-top: 5px;"></div>
                           <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Floor

:</label></div>
                            <div class="col-md-8">  <asp:DropDownList runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFloor_OnSelectedIndexChanged"   ID="ddlFloor"  CssClass="form-control form-control-sm" /></div>
                          </div>
                     
                      <div style="padding-top: 5px;"></div>
                     
                           <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Meeting Room:</label></div>
                            <div class="col-md-8">  <asp:DropDownList runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMettingRoomName_OnSelectedIndexChanged"  ID="ddlMettingRoomName"  CssClass="form-control form-control-sm" /></div>
                          </div>
                     
                      <div style="padding-top: 5px;"></div>
                     
                           <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Capacity


:</label></div>
                            <div class="col-md-8">  <asp:TextBox runat="server"   ID="txtCapacity"  CssClass="form-control form-control-sm" /></div>
                          </div>
                 </div>
                 <div class="col-md-4">
                     
                        <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;">Outer Premisis</h2>
                           <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Location   



:</label></div>
                            <div class="col-md-8">  <asp:TextBox runat="server"   ID="txtLocation"  CssClass="form-control form-control-sm" /></div>
                          </div>
                     
                       <div style="padding-top: 5px;"></div>
                     
                     
                           <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Description



:</label></div>
                            <div class="col-md-8">  <asp:TextBox runat="server"   ID="txtDescription"  CssClass="form-control form-control-sm" /></div>
                          </div>
                 </div>
                 
                   <div class="col-md-4">
                     
                        <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;">Virtual Meeting</h2>
                           <div class="row">
                                <div class="col-md-4" style="padding-top: 8px">  <label class="control-label pull-right">Remarks   



:</label></div>
                            <div class="col-md-8">  <asp:TextBox runat="server"   ID="txtRemarks"  CssClass="form-control" TextMode="MultiLine" Rows="3" /></div>
                          </div>
                     
                       
                 </div>
             </div>
             
             
              
         </div>
         <div class="modal-footer"><button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button> </div>
      </div>
        </ContentTemplate>
    </asp:UpdatePanel>
   </div>
</div>

     

<div class="card">
   <div class="card-body">
      
       
        <asp:HiddenField runat="server" ID="hfRefMinAppCount"/>
       <asp:HiddenField runat="server" ID="hfRefMinAppCountCheck"/>
       <asp:HiddenField runat="server" ID="hfSeqNo"/>
       
         <div class="row">
                                     <div class="col-md-4">
                               </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label  style="font-weight: bold">Comments</label>
                                        <asp:TextBox runat="server" ID="commentsTextBox" CssClass="form-control"   TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                     </div>       
                            
                            
                             <div class="row">
                                   <div class="col-md-5">
                               </div>
                                <div class="col-4 ">
                                    <div class="form-group">
                                          <link href="../UserSetup/ButtonGrups.css" rel="stylesheet" />
                                      <div class="ui-group-buttons">
                                 
                              
                                        
                                    <asp:LinkButton ID="btnReturn" Text="Cancel"  CssClass="btn btn-sm btn-danger"  OnClientClick="return confirm('Are you sure you want to Return ?')" runat="server" OnClick="btnReturn_OnClick" ><i class="fa fa-undo" aria-hidden="true"></i>
&nbsp; Return </asp:LinkButton> 
                                          
                                              <div class="or or-sm" runat="server"   id="orBTN"></div>
                                          
                                                <asp:LinkButton ID="btnApprove" Text="Submit" CssClass="btn btn-sm btn-info"   runat="server" OnClick="btnApprove_OnClick" OnClientClick="return confirm('Are you sure you want to Approve ?')" style="box-shadow: 0 0 3px 1px rgba(0,0,0,.35);"  ><i class="fa fa-check" aria-hidden="true"></i> &nbsp; Approve

&nbsp; </asp:LinkButton>  
                                     <%--<asp:Button ID="Button2" Text="Back to List" CssClass="btn btn-sm text-info" runat="server" OnClick="detailsViewButton_OnClick" />--%>
                                    <asp:HiddenField ID="entryempinfoIdHiddenField" runat="server" />
                                    <asp:HiddenField ID="actionstatusHiddenField" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
       <div class="row">
              <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;">Meeting Information</h2>
           <div class="col-md-12">
               
                 <table class="table table-bordered table-striped">
                         
                           
                                                    <tr>
                                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Company</td>
                                                        <td> <asp:Label runat="server" ID="lblCompany"></asp:Label></td>

                                                         <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Classification </td>
                                                        <td> <asp:Label runat="server" ID="lblClassification"></asp:Label></td>

                                                        
                                                    </tr>
                                                    
                                                     <tr>
                                                        <td  class="tblTHColorChang" style="width: 20%; padding: 10px;">
Title</td>
                                                        <td> <asp:Label runat="server" ID="lblTitle"></asp:Label></td>

                                                         
                                                                   <td  class="tblTHColorChang" style="width: 20%; padding: 10px;">
Date</td>
                                                        <td> <asp:Label runat="server" ID="lblDate"></asp:Label></td>
                                                     
                                                    </tr>
                     
                     
                         <tr>
                                                        <td  class="tblTHColorChang" style="width: 20%; padding: 10px;">
Meeting Category</td>
                                                        <td> <asp:Label runat="server" ID="lblMeetingCategory"></asp:Label></td>

                                                         
                                                                   <td  class="tblTHColorChang" style="width: 20%; padding: 10px;">
Start Time</td>
                                                        <td> <asp:Label runat="server" ID="lblStartTime"></asp:Label></td>
                                                     
                                                    </tr>
                     
                        <tr>
                                                        <td  class="tblTHColorChang" style="width: 20%; padding: 10px;">
Sub-Committee Name</td>
                                                        <td> <asp:Label runat="server" ID="lblSubCommitteeName"></asp:Label></td>

                                                         
                                                                       <td  class="tblTHColorChang" style="width: 20%; padding: 10px;">
End Time</td>
                                                        <td> <asp:Label runat="server" ID="lblEndTime"></asp:Label></td>
                                                     
                                                    </tr>
                     
                     
                     
                     
                         <tr>
                                                        <td  class="tblTHColorChang" style="width: 20%; padding: 10px;">
Meeting Note</td>
                                                        <td> <asp:Label runat="server" ID="lblMeetingNote"></asp:Label></td>

                                                         
                                                             
                                                     
                                                    </tr>
                     
                     
                      <tr>
                                                        <td  class="tblTHColorChang" style="width: 20%; padding: 10px;">
Location</td>
                                                        <td style="width: 20%; padding: 10px;" colspan="3">
                                                            
                                                            
                                                            <div runat="server" ID="divOfficePremisis" Visible="False">
                                                                
                                                                
                 <table class="table table-bordered table-striped">
                             <tr>
                                                        <td class="tblTHColorChang" colspan="2" style="width: 20%; padding: 10px;">Office Premisis</td>
                                 </tr>
                           
                                                    <tr>
                                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Office</td>
                                                        <td> <asp:Label runat="server" ID="lblOffice"></asp:Label></td>

                                                         

                                                        
                                                    </tr>
                     
                     
                        <tr>
                                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Location</td>
                                                        <td> <asp:Label runat="server" ID="lblLocation2"></asp:Label></td>

                                                         

                                                        
                                                    </tr>
                     
                        <tr>
                                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Floor</td>
                                                        <td> <asp:Label runat="server" ID="lblFloor"></asp:Label></td>

                                                         

                                                        
                                                    </tr>
                     
                     
                        <tr>
                                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Meeting Room</td>
                                                        <td> <asp:Label runat="server" ID="lblMeetingRoom"></asp:Label></td>

                                                         

                                                        
                                                    </tr>
                     
                      <tr>
                                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Capacity</td>
                                                        <td> <asp:Label runat="server" ID="lblCapacity"></asp:Label></td>

                                                         

                                                        
                                                    </tr>
                     </table>

                                                            </div>
                                                             
                                                                                                        <div runat="server" ID="divOuterPremisis" Visible="False">
                                                                
                                                                
                 <table class="table table-bordered table-striped">
                             <tr>
                                                        <td class="tblTHColorChang" colspan="2" style="width: 20%; padding: 10px;">Outer Premisis</td>
                                 </tr>
                           
                                                    <tr>
                                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Location</td>
                                                        <td> <asp:Label runat="server" ID="lblOuterLocation"></asp:Label></td>

                                                         

                                                        
                                                    </tr>
                     
                     
                        <tr>
                                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Description</td>
                                                        <td> <asp:Label runat="server" ID="lblOuterDescription"></asp:Label></td>

                                                         

                                                        
                                                    </tr>
                     
           
                     
                     
                    
                     </table>

                                                            </div>
                                                            
                                                            
                                                                   <div runat="server" ID="divVirtualMeeting"  Visible="False">
                                                                
                                                                
                 <table class="table table-bordered table-striped">
                             <tr>
                                                        <td class="tblTHColorChang" colspan="2" style="width: 20%; padding: 10px;">Virtual Meeting</td>
                                 </tr>
                           
                                                    <tr>
                                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Remarks</td>
                                                        <td> <asp:Label runat="server" ID="lblRemarks"></asp:Label></td>

                                                         

                                                        
                                                    </tr>
                     
                     
                   
                     
           
                     
                     
                    
                     </table>

                                                            </div>
                                                            
                                                            

                                                        </td>

                                                         
                                                        
                                                     
                                                    </tr>
                     </table>
           </div>
       </div>
       <div class="row">
                 <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;">Member Information</h2>
           <div class="col-md-12">
                <asp:GridView Width="100%" ShowHeader="True" ID="gv_Details_Save" runat="server" AutoGenerateColumns="false" CssClass="blueTableNew"   OnPreRender="gv_DocumentUpload_PreRender">
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
                                                        
                                                        <asp:HiddenField runat="server" ID="ShfEmpInfoId" Value='<%#Eval("EmpInfoId")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                             
                                                 <asp:TemplateField HeaderText="Employee Name">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_EmpName" ReadOnly="True"  CssClass="form-control form-control-sm"  runat="server" Text='<%#Eval("EmpName") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                 <asp:TemplateField HeaderText="Designation">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_Designation"  ReadOnly="True"   CssClass="form-control form-control-sm"  runat="server" Text='<%#Eval("Designation") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                               <asp:TemplateField HeaderText="Notification" Visible="False">
                                                    <ItemTemplate>
                                                        
                                                        <asp:HiddenField runat="server" ID="HiNotificationEmail" Value='<%#Eval("NotificationEmail")%>' />
                                                        <asp:HiddenField runat="server" ID="hfNotificationSMS" Value='<%#Eval("NotificationSMS")%>' />


                                                 <asp:CheckBoxList runat="server" ID="chkNotification" AutoPostBack="True" OnSelectedIndexChanged="chkNotification_OnSelectedIndexChanged" CssClass="chkChoice" RepeatDirection="Horizontal">
                                                               
                
                 <asp:ListItem>Email</asp:ListItem>
                    <asp:ListItem>SMS</asp:ListItem>
                                                           </asp:CheckBoxList>
                                                
                                                 
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                                
                                               
                                                    <asp:TemplateField HeaderText="Position">
                                                    <ItemTemplate>
                                                        <asp:HiddenField runat="server" ID="hfPosition" Value='<%#Eval("Position")%>' />
                                                        
                                          <asp:RadioButtonList runat="server"  ID="chkPosition" AutoPostBack="True" OnSelectedIndexChanged="chkPosition_OnSelectedIndexChanged"  CssClass="chkChoice" RepeatDirection="Horizontal">
                                                               
                
                 <asp:ListItem>Member</asp:ListItem>
                    <asp:ListItem>Convenor</asp:ListItem>
                    <asp:ListItem>Secretary</asp:ListItem>
                                                           </asp:RadioButtonList>
                                                
                                                 
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Actions" runat="server" Visible="False">
                                                    <ItemTemplate>
                                                        
                                                        <asp:LinkButton runat="server" ID="btn_DetailsAdd"   OnClick="btn_DetailsAdd_OnClick" CssClass="btn btn-sm btn-success"><i class="fa fa-plus"></i> </asp:LinkButton> 
                                                      <asp:LinkButton runat="server" ID="btn_DetailsRemove" OnClick="btn_DetailsRemove_OnClick"   CssClass="btn btn-sm btn-danger"><i class="fa fa-minus-circle"></i> </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                
                                       
                                                
                                                
                                                
                                            </Columns>
                                        </asp:GridView>
           </div>
       </div>
       <br/>
       <br/>
         <div class="row">
              <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;">Agenda Information</h2>
           <div class="col-md-12">
                 
                <asp:GridView ID="gv_AgendaList" runat="server" CssClass="blueTableNew" AutoGenerateColumns="False"
                                             GridLines="None"    OnPreRender="gv_DocumentUpload_PreRender">
                                            
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Agenda">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAgenda" ReadOnly="True" runat="server"  Text='<%#Eval("Agenda") %>'
                                                           class="form-control form-control-sm"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                  <asp:TemplateField HeaderText="Remarks">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtRemarks"  ReadOnly="True"  runat="server"  Text='<%#Eval("Remarks") %>'
                                                           class="form-control form-control-sm"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Presentor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlPresentor" runat="server"  
                                                             class="form-control form-control-sm SelectMe"></asp:DropDownList>
                                                       <asp:HiddenField  runat="server" ID="hfPresentor" Value='<%#Eval("Presentor") %>'/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Actions" runat="server" Visible="False">
                                                    <ItemTemplate>
                                                       <asp:LinkButton runat="server" ID="btnAgenaListAdd"   OnClick="btnAgenaListAdd_OnClick" CssClass="btn btn-sm btn-success"><i class="fa fa-plus"></i> </asp:LinkButton>  <asp:LinkButton runat="server" ID="btnAgenaLisRemove"   CssClass="btn btn-sm btn-danger" OnClick="btnAgenaLisRemove_OnClick"><i class="fa fa-minus-circle"></i> </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                      
                                            
                                        </asp:GridView>
               </div>
             </div>
         <br/>
       <br/>
         <div class="row">
              <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;">Mintues Information</h2>
           <div class="col-md-12">
                <fieldset class="for-panel"  runat="server" Visible="False" ID="divDoc">
                <legend>Document </legend>
                                        <div class="row">
                                           


    
                                            <asp:HiddenField runat="server" ID="hfDocFileName"/>
  
                                            <asp:HiddenField runat="server" ID="hfDocFile"/>
                                            <div class="col-4">
                                                <div class="form-group">
                                                    <label>Document Upload<span   style="color:red; " title="please fill out this field"> * </span></label>
                                                    <div>
   <input type="file" name="postedFile" class="form-control form-control-sm" />
  <asp:FileUpload ID="FUDocument" Visible="False" CssClass="form-control form-control-sm" runat="server"   />
                                                         <br/>
                                                        <input type="button"  class="btn btn-sm  btn-info" id="btnUpload" value="Upload Document" />
                                                          <asp:LinkButton  runat="server" Visible="False" OnClick="btnDocUp_OnClick" ID="btnDocUp"  CssClass="btn btn-sm  btn-info">
                                                          
                                                      
              &nbsp;    <span class="btn-label"><i class="fa fa-upload"></i></span>  &nbsp;   &nbsp;Upload Document
                                                          </asp:LinkButton>
                                                        <br />
                                                          <progress id="fileProgress" style="display: none"></progress>
                                                        <br />

                                             <span id="lblMessage" style="color: Green"></span>
                                                        <asp:HyperLink ID="HyperLink2" Visible="False" runat="server"
    
     Target="_blank" ToolTip="Click to Show Document"></asp:HyperLink> 
                                                           
                                                        
                                                       
                                                    </div>
                                                </div>
                                            </div>
                                            
                                            
                                            <div class="col-4">
                                             <div class="form-group">
                                                    <label>Summary Note<span   style="color:red; " title="please fill out this field"> * </span></label>
                                                    <div>

                                                     <asp:TextBox runat="server"   ID="txtSummaryNote"  TextMode="MultiLine" Rows="2"    class="form-control" />
                                                         
                                                    </div>
                                                </div>
                                                
                                                  <div class="form-group">
                                                       <asp:LinkButton runat="server" ID="brnAddDoc"   OnClick="brnAddDoc_OnClick"    CssClass="btn btn-success   btn-sm pull-right"><span aria-hidden="true" class="fa fa-plus"></span>  &nbsp; Add to List </asp:LinkButton> 
                                                      </div>
                                            </div>

                                        </div>
               <br/>
                                                <div class="row">
                                                    <div class="col-md-8">
                                                        
                                                        
                                                        
                                                        
                                                
                                                    </div>
                                                    </div>
                                               </fieldset>
       
                 <asp:GridView Width="100%" ShowHeader="True" ID="gv_DocumentUpload" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Document">
                                                    <ItemTemplate>
                                                          <asp:HyperLink ID="HLDocumentLink"   Target="_blank"  runat="server" NavigateUrl='<%# Eval("DocumentLink") %>'  Text='Download'>
        </asp:HyperLink>
                                                        <asp:Label ID="lbl_DocumentLink" Visible="False" runat="server" Text='<%#Eval("DocumentLink")%>'></asp:Label>
                                                        <asp:HiddenField runat="server" ID="hfFileName" Value='<%#Eval("FileName")%>' />

                                                        <asp:HiddenField runat="server" ID="hfDocumentLink" Value='<%#Eval("DocumentLink")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                 
                                                <asp:TemplateField HeaderText="Summary Note	">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_DocumentNote" runat="server" Text='<%#Eval("DocumentNote") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                             

                                                <asp:TemplateField HeaderText="Remove" runat="server" Visible="False">
                                                    <ItemTemplate>
                                                      <asp:LinkButton runat="server" ID="btnDocRemove" OnClick="btnDocRemove_OnClick"   CssClass="btn btn-sm btn-danger"><i class="fa fa-minus-circle"></i> </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
               
               
               <br/>
               <br/>
                
                                           
                                                
               </div>
             
                <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;">Approval Status List </h2>   
             <div class="col-md-12">
                       
                                                            
                                                                  <asp:GridView Width="100%" ShowHeader="True" ID="gv_ApprovalList" runat="server" AutoGenerateColumns="false" CssClass="blueTableNew"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:BoundField DataField="PreEmp" HeaderText="Initiator" HtmlEncode="False" />
                                                
                                                      <asp:BoundField DataField="Comments" HeaderText="Comments" HtmlEncode="False" />

                                                    <asp:BoundField DataField="ForEmp" HeaderText="Approval Person" HtmlEncode="False" />
                                                
                                                    <asp:BoundField DataField="ActionStatus" HeaderText="Action Status" HtmlEncode="False" />

                                                    <%--<asp:BoundField DataField="Version" HeaderText="Version" HtmlEncode="False" />--%>
                                                  


                                                    <asp:BoundField DataField="ApproveBy" HeaderText="Approved By" HtmlEncode="False" />
                                                    <asp:BoundField DataField="ApprovedDate" HeaderText="Approved Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                     
                                               
                                                
                                                
                                                
                                              
                                            </Columns>
                                        </asp:GridView>
             </div>
             </div>
             
       

       <div class="row"  runat="server" Visible="False">
            <div class="col-md-12" >
             <form action="javascript:alert('Submited!');" method="post">
         <div class="wizard show-submit">
            <ul>
               <li><a href="#step-7"><span class="stepNumber">1</span> <span class="stepDesc">Meeting Entry<br></span></a></li>
               <li><a href="#step-8"><span class="stepNumber">2</span> <span class="stepDesc">Agenda Entry<br></span></a></li>
               <li><a href="#step-9"><span class="stepNumber">3</span> <span class="stepDesc">Mintues Entry<br></span></a></li>
               <li><a href="#step-10"><span class="stepNumber">4</span> <span class="stepDesc">Approval Setup<br></span></a></li>
            </ul>
            <div id="step-7">
              <asp:UpdatePanel runat="server">
                  <ContentTemplate>
                       <div class="form-group">
                  <div class="row">
                    <div class="col-md-6">
                        <div class="form-group ">
                                          
                          <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Company:<span   style="color:red; " title="please fill out this field"> * </span></label></div>
                            <div class="col-md-6">  <asp:DropDownList runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged"   ID="ddlCompany"  class="form-control form-control-sm" />
                                
                              <script type="text/javascript">
                                  function pageLoad() {

                                      //$('.SelectMe').chosen({ disable_search_threshold: 5, search_contains: true });


                                  }
</script>

                            </div>
                          </div>
                            <div style="padding-top: 5px;"></div>
                               <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Title:<span   style="color:red; " title="please fill out this field"> * </span></label></div>
                            <div class="col-md-6">  <asp:TextBox runat="server"   ID="txtTitle"  TextMode="MultiLine" Rows="2" class="form-control " /></div>
                          </div>
                            
                            <div style="padding-top: 5px;"></div>
                            
                            <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Meeting Category:<span   style="color:red; " title="please fill out this field"> * </span></label></div>
                            <div class="col-md-6">  <asp:DropDownList runat="server"   ID="ddlCategory"  AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_OnSelectedIndexChanged" class="form-control form-control-sm" >
                               
                                </asp:DropDownList>
                                </div>
                          </div>
                            
                            
                              <div style="padding-top: 5px;"></div>
                               <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Meeting Note
:<span   style="color:red; " title="please fill out this field"> * </span></label></div>
                            <div class="col-md-6">  <asp:TextBox runat="server"   ID="txtMeetingpurpose"  class="form-control" TextMode="MultiLine" Rows="3" /></div>
                          </div>
                                            <fieldset class="for-panel" runat="server" Visible="False">
                                                            <legend>Organizer
</legend>
                                                            <div class="row">
                                                                     <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right"> <asp:RadioButton runat="server" CssClass="chkChoice"  Text="By Deparment" ID="rbDeptBy"/></label></div>
                            <div class="col-md-6">   <asp:DropDownList runat="server"   ID="ddlDept"  class="form-control form-control-sm" /></div>

                                                                </div>
                                                
                                                
                                                 <div class="row">
                                                                     <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right"> <asp:RadioButton runat="server"  Text="By Employee" CssClass="chkChoice" ID="RadioButton1"/></label></div>
                            <div class="col-md-6">   <asp:DropDownList runat="server"   ID="DropDownList1"  class="form-control form-control-sm" /></div>

                                                                </div>
                                                
                                                
                                                
                                                 <div class="row">
                                                                     <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right"> <asp:RadioButton runat="server" CssClass="chkChoice"   Text="Other" ID="RadioButton2"/></label></div>
                            <div class="col-md-6">   <asp:TextBox runat="server"   ID="txtOther"  class="form-control form-control-sm" /></div>

                                                                </div>
                                                </fieldset>
                            
                            
                           
                                        </div>
                    </div>
                    <div class="col-md-6">
                            
                            <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Classification	
:</label></div>
                            <div class="col-md-6">  <asp:DropDownList runat="server"   ID="ddlClassification"  class="form-control form-control-sm" 
                                >
                                                   
                                
                                
                                     <asp:ListItem>Select One....</asp:ListItem>
                                <asp:ListItem>External</asp:ListItem>
                                <asp:ListItem>Internal</asp:ListItem>
                                <asp:ListItem>Others</asp:ListItem>
                                                    </asp:DropDownList>
                                </div>
                          </div>
                        
                          <div style="padding-top: 5px;"></div>
                           <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Date:<span   style="color:red; " title="please fill out this field"> * </span></label></div>
                            <div class="col-md-6">  <asp:TextBox runat="server"   ID="txtMeetingDate"    class="form-control form-control-sm" />
                                
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="TopLeft"
                                            Format="dd-MMM-yyyy" PopupButtonID="txtMeetingDate" CssClass="MyCalendar"
                                            TargetControlID="txtMeetingDate" />

                            </div>
                          </div>
                             <div style="padding-top: 5px;"></div>
                                  <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Start Time:</label></div>
                            <div class="col-md-6">  <asp:TextBox runat="server"   ID="txtStartTime" TextMode="Time"  class="form-control form-control-sm" /></div>
                              </div>
                        
                         <div style="padding-top: 5px;"></div>
                              <div class="row">
                              
                                  
                                   <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">End Time:</label></div>
                            <div class="col-md-6">  <asp:TextBox runat="server"   ID="txtEndTime" TextMode="Time"  class="form-control form-control-sm" /></div>
                          </div>
                        
                        
                        <br/>
                       
                         <div class="row">
                       
                             <div class="col-md-3" >
                                 </div>
                         <div class="col-md-3" >
                     
                             <asp:LinkButton  data-toggle="modal" data-target="#exampleModal"  class="btn btn-info  pull-right" runat="server" ID="lblLocation"><i class="fa fa-map-marker"></i>&nbsp; Set Location</asp:LinkButton>
                       
                             </div>
                   </div>

                    </div>
                  </div>
                   
                  
                   <br/>
                     <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;">Add Members</h2>
                   <div class="row">
                       <div class="col-md-2" style="padding-top: 8px">
                            <asp:LinkButton runat="server" ID="LinkButton3"  data-toggle="modal" data-target="#exampleModal2"  CssClass="btn btn-sm btn-secondary">Add More Member  </asp:LinkButton>
                         <%--  <label class="control-label">Attendee Group
</label>--%>
                       </div>
                       <div class="col-md-4">
                           
                        <%--   <asp:DropDownList runat="server" ID="dll" CssClass="form-control form-control-sm"/>--%>
                       </div>
                       <div class="col-md-4"></div>
                   </div>
<br/>                   <div class="row">
                       

                       <div class="col-md-12">
                           
                               <asp:UpdatePanel runat="server">
                                   <ContentTemplate>
                                         
                                   </ContentTemplate>
                               </asp:UpdatePanel>
                          

                       </div>
                   </div>
               </div>
                  </ContentTemplate>
              </asp:UpdatePanel>
            </div>
            <div id="step-8">
               <div class="row">
                       

                       <div class="col-md-12">
                           
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                      
                                    
                            </ContentTemplate>
                        </asp:UpdatePanel>
                            
                     
                       </div>
                   </div>
            </div>
             
             
                <div id="step-9">
               <div class="form-group">
                  <div class="row">
                     
                 <div class="col-md-12">
                        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                    <fieldset class="for-panel">
                               
                            </ContentTemplate>
                            
                            
           <Triggers>  
  
         <asp:PostBackTrigger ControlID="btnDocUp" />  
  
</Triggers> 
                        </asp:UpdatePanel>
                 </div>
                  </div>
               </div>
              
            </div>
               <div id="step-10">
               <div class="form-group">
                <div class="col-md-12">
                      <div class="row">
                
                            <div class="col-md-3">
                                <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;">Approval Path Setup</h2>
                         </div>
             
             <div class="col-md-2">
                                <div class="form-group">
                                     
                                     <asp:LinkButton runat="server" ID="LinkButtaon2"  data-toggle="modal" data-target="#exampleModalAPP"  CssClass="btn btn-sm btn-success">Choose Approval Path  </asp:LinkButton>
                                </div>
                               
                            </div>
             
                <div class="col-md-2">
                        <asp:UpdatePanel runat="server">
                <ContentTemplate>
                                <div class="form-group">
                                     
                                     <asp:LinkButton runat="server"   ID="btnAddMoreforApproval"   data-toggle="modal" data-target="#exampleModalAPPEmpp"  CssClass="btn btn-sm btn-secondary">Add More Member  </asp:LinkButton>
                                </div>
                               </ContentTemplate>
                            </asp:UpdatePanel>
                            </div>
             
             </div>

       
      
       
        <div class="row">

           
           <div class="col-md-12">
               
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                         <asp:GridView Width="100%" ShowHeader="True" ID="gv_ApprovalPathDetail" runat="server" AutoGenerateColumns="false" CssClass="blueTableNew"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                           <asp:TemplateField HeaderText="Mimimum Count for Approval">
                                                    <ItemTemplate>
                                                <asp:CheckBox ID="chkMimimumCount" AutoPostBack="True" OnCheckedChanged="chkMimimumCount_OnCheckedChanged" CssClass="form-control-sm"   runat="server" />
                                                        <asp:HiddenField runat="server" ID="hfIsMinimumApprovalPerson" Value='<%#Eval("IsMinimumApprovalPerson")%>' />
                                                
                                                 
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Sequence">
                                                    <ItemTemplate>
                                                        
                                                        <asp:HiddenField runat="server" ID="hfSeq_No" Value='<%#Eval("Seq_No")%>' />

                                                      <asp:DropDownList runat="server" ID="ddlSequenceList" class="form-control form-control-sm" >
                                                               
                 
                                                           </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 
                                                <asp:TemplateField HeaderText="Employee ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Appbl_EmpMasterCode" runat="server" Text='<%#Eval("EmpMasterCode") %>'></asp:Label>
                                                        
                                                        <asp:HiddenField runat="server" ID="ApphfEmpInfoId" Value='<%#Eval("EmpInfoId")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                             
                                                 <asp:TemplateField HeaderText="Employee Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Applbl_EmpName" runat="server" Text='<%#Eval("EmpName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                 <asp:TemplateField HeaderText="Designation">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Applbl_Designation" runat="server" Text='<%#Eval("Designation") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                               <asp:TemplateField HeaderText="Is Edit">
                                                    <ItemTemplate>
                                                <asp:CheckBox ID="chkIsEdit" AutoPostBack="True" OnCheckedChanged="chkIsEdit_OnSelectedIndexChanged"   CssClass="form-control-sm"   runat="server" />
                                                        <asp:HiddenField runat="server" ID="hfCanEdit" Value='<%#Eval("CanEdit")%>' />
                                                
                                                 
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                              
                                                
                                           <asp:TemplateField HeaderText="Notification">
                                                    <ItemTemplate>
                                                
                                                   <asp:HiddenField runat="server" ID="HiNotificationEmailApp" Value='<%#Eval("NotificationEmail")%>' />
                                                        <asp:HiddenField runat="server" ID="hfNotificationSMSApp" Value='<%#Eval("NotificationSMS")%>' />


                                                 <asp:CheckBoxList runat="server" ID="chkNotificationApp" AutoPostBack="True" OnSelectedIndexChanged="chkNotificationApp_OnSelectedIndexChanged" CssClass="chkChoice" RepeatDirection="Horizontal">
                                                               
                
                 <asp:ListItem>Email</asp:ListItem>
                    <asp:ListItem>SMS</asp:ListItem>
                                                           </asp:CheckBoxList>
                                                 
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                                
                                                  <asp:TemplateField HeaderText="Remove">
                                                    <ItemTemplate>
                                                      <asp:LinkButton runat="server" ID="Appbtn_DetailsRemove" OnClick="Appbtn_DetailsRemove_OnClick"   CssClass="btn btn-sm btn-danger"><i class="fa fa-minus-circle"></i> </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                            </Columns>
                                        </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
                
       
       <br/>
               </div>
       
       </div>
                </div>
       
      <br/>
               </div>
              
            </div>
               <br/>
      
         </div>
        
      </form>
                </div>
       </div>
       <br/>
      
    <asp:UpdatePanel runat="server">
          <ContentTemplate>
       
        <div class="row">

           
      
                   <div class="col-md-4">
               <div class="form-row" runat="server" Visible="False">
                  
                   <div class="col-md-6">
                       <asp:LinkButton ID="submitButton" Visible="False" OnClick="btnSave_OnClick"   OnClientClick="return confirm('Are you sure you want to Submit ?')" CssClass="btn btn-sm btn-info"  runat="server"><i style="font-size: 11px;" class="fa fa-floppy-o" aria-hidden="true"></i>&nbsp;
 Submit</asp:LinkButton>
                        <asp:HiddenField runat="server" ID="id_mastetID"/>
                       
                        <asp:LinkButton ID="editButton" OnClientClick="return confirm('Are you sure you want to Update ?')"  CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick"><i class="fa fa-pencil-square-o" aria-hidden="true"></i>&nbsp; Update</asp:LinkButton>
                                    <asp:LinkButton ID="delButton" OnClientClick="return confirm('Are you sure you want to Delete ?')" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" ><i class="fa fa-pencil-square-o" aria-hidden="true"></i>&nbsp; Delete</asp:LinkButton>
                                       
                   </div>
                  
               </div>
               <div class="form-group">
               </div>
           </div>
          

           <div class="col-md-4">
           </div>
       </div>
              
              </ContentTemplate>
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
                     
   
  <!-- Trigger the modal with a button -->
 

  <!-- Modal -->
  
      <script type="text/javascript">
          $("body").on("click", "#btnUpload", function () {
              debugger;
              $.ajax({
                  url: '/HandlerDoc.ashx',
                  type: 'POST',
                  data: new FormData($('form')[0]),
                  cache: false,
                  contentType: false,
                  processData: false,
                  success: function (file) {
                      $("#cpFormBody_hfDocFile").val('');
                      $("#cpFormBody_hfDocFileName").val('');
                      $("#fileProgress").hide();
                      $("#lblMessage").html("<b>" + file.name + "</b> has been uploaded.");
                      $("#cpFormBody_hfDocFile").val(file.dbfilename);
                      $("#cpFormBody_hfDocFileName").val(file.name);

                  },
                  xhr: function () {
                      var fileXhr = $.ajaxSettings.xhr();
                      if (fileXhr.upload) {
                          $("progress").show();
                          fileXhr.upload.addEventListener("progress", function (e) {
                              if (e.lengthComputable) {
                                  $("#fileProgress").attr({
                                      value: e.loaded,
                                      max: e.total
                                  });
                              }
                          }, false);
                      }
                      return fileXhr;
                  }
              });
          });
    </script>
  
 


                     </div>
        
        </div>
</asp:Content>


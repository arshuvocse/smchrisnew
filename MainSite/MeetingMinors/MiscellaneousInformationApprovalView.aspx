<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="MiscellaneousInformationApprovalView.aspx.cs" Inherits="MeetingMinors_MiscellaneousInformationApprovalView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
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
    
     <div class="content" id="content">
                 <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                             
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" /> Document Approval List </h1>
                        </div>
                         <div class="page-heading__container float-right d-none d-sm-block">
                         
                <asp:Button ID="AddNewButton" Visible="False" Text="Add New" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="AddNewButton_OnClick"  />
                <%--<asp:Button ID="reloadButton" Text="Reload" CssClass="btn btn-sm btn-outline-success" runat="server" OnClick="reloadButton_OnClick" />--%>
            </div>
                    </div>
                     
<div class="card">
   <div class="card-body">
       
         <div class="row" style="display: none">
               
                    <div class="col-md-6">
                        <div class="form-group ">
                                          
                          <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Company:</label></div>
                            <div class="col-md-6">  <asp:DropDownList runat="server"   ID="ddlCompany"  class="form-control form-control-sm" /></div>
                          </div>
                            <div style="padding-top: 5px;"></div>
                                <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Title:</label></div>
                            <div class="col-md-6">  <asp:TextBox runat="server"    ID="DropDownList1"  class="form-control form-control-sm" /></div>
                          </div>
                             <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Propuse:</label></div>
                            <div class="col-md-6">  <asp:TextBox runat="server"      ID="DropDownList2"  class="form-control form-control-sm" /></div>
                          </div>
                            </div>
                        </div>
             
                    <div class="col-md-6">
                        <div class="form-group ">
                                          
                          
                            
                                <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Created By:</label></div>
                            <div class="col-md-6">  <asp:TextBox runat="server"    ID="TextBox1"  class="form-control form-control-sm" /></div>
                          </div>
                            <div style="padding-top: 5px;"></div>
                             <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Created Date:</label></div>
                            <div class="col-md-6">  <asp:TextBox runat="server"   TextMode="Date"    ID="TextBox2"  class="form-control form-control-sm" /></div>
                          </div>
                            <div style="padding-top: 5px;"></div>
                            
                              <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Key Search:</label></div>
                            <div class="col-md-6">  <asp:TextBox runat="server"    ID="TextBox3"  class="form-control form-control-sm" /></div>
                          </div>
                            </div>
                        </div>
             </div>
       
            

                                    <div class="row"  style="display: none">
                                        <div class="col-md-4">
                                            <%--<asp:Button runat="server" ID="Button51" OnClick="btn_Save_OnClick" Text="Search " CssClass="btn btn-outline-info btn-block disabled btn-sm" />--%>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:LinkButton runat="server" ID="btn_Save"   CssClass="btn btn-info   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search </asp:LinkButton>

                                            <asp:LinkButton runat="server" ID="lbReset" CssClass="btn btn-warning   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset </asp:LinkButton>
                                        </div>
                                        <div class="col-md-4">
                                            <%--<asp:Button runat="server" ID="Button51" OnClick="btn_Save_OnClick" Text="Search " CssClass="btn btn-outline-info btn-block disabled btn-sm" />--%>
                                        </div>
                                    </div>
       
        <br />
       <table class="AddToListCssTable">
          <thead>
               <tr>
    <th>Company</th>
    <th>Title</th>
    <th>Purpose</th>
    <th>Initiator</th> 
 
    <th>Action</th>

  </tr>
          </thead>
  
  
  <tbody>
      <tr>
    <td>Company</td>
    <td> Title 01</td>
    <td> Purpose 1</td>
    <td>mr. v</td>
    
    <td> <asp:LinkButton runat="server" ID="LinkButton3"   CssClass="btn btn-sm btn-success"> Go for Approval </asp:LinkButton> </td>
     
     

  </tr>
  </tbody>
           
 
       </table>
       
           <br/> 
      
              </div>
        
        </div>
                               </div>
        
        </div>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="MeetingStatusList2.aspx.cs" Inherits="MeetingMinors_MeetingStatusList2" %>

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
                             
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" /> Meeting Status </h1>
                        </div>
                        <%--                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="detailsViewButton" Text="List Information " CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>--%>
                    </div>
                     
<div class="card">
   <div class="card-body">
       
         <div class="row">
               <div class="col-md-3">
                   </div>
                    <div class="col-md-6">
                        <div class="form-group ">
                                          
                          <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">Company:</label></div>
                            <div class="col-md-6">  <asp:DropDownList runat="server"   ID="ddlCompany"  class="form-control form-control-sm" /></div>
                          </div>
                            <div style="padding-top: 5px;"></div>
                                <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">From Date:</label></div>
                            <div class="col-md-6">  <asp:TextBox runat="server"   TextMode="Date" ID="DropDownList1"  class="form-control form-control-sm" /></div>
                          </div>
                             <div class="row">
                                <div class="col-md-3" style="padding-top: 8px">  <label class="control-label pull-right">To Date:</label></div>
                            <div class="col-md-6">  <asp:TextBox runat="server"   TextMode="Date"    ID="DropDownList2"  class="form-control form-control-sm" /></div>
                          </div>
                            </div>
                        </div>
             </div>
       
        <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;">Meeting Information</h2>
       <table class="table table-bordered dataTable dtr-inline">
           <tr>
    <th>Title</th>
    <th>Purpose</th>
    <th>Meeting Date</th> 
    <th>Status</th>

  </tr>
  
  
  <tr>
    <td>Title 01</td>
    <td> Location 01</td>
    <td>6-12-2018</td> 
    <td>  <asp:DropDownList runat="server" style="color: white;background-color: #4DDEC1" class="form-control form-control-sm"  RepeatDirection="Horizontal">
                                                               
                
                 <asp:ListItem   Selected="true"> Done  </asp:ListItem>
                    <asp:ListItem>Pending </asp:ListItem>
                    <asp:ListItem>Cancel </asp:ListItem>
                                                           </asp:DropDownList> </td>

  </tr>
           
  <tr>
    <td>Title 01</td>
    <td> Location 01</td>
    <td>6-12-2018</td> 
    <td>  <asp:DropDownList runat="server" style="color: white;background-color: #64ADFF" class="form-control form-control-sm"  RepeatDirection="Horizontal">
                                                               
                
                 <asp:ListItem   > Done  </asp:ListItem>
                    <asp:ListItem Selected="true">Pending </asp:ListItem>
                    <asp:ListItem>Cancel </asp:ListItem>
                                                           </asp:DropDownList> </td>

  </tr>
           
             
  <tr>
    <td>Title 01</td>
    <td> Location 01</td>
    <td>6-12-2018</td> 
    <td>  <asp:DropDownList runat="server" style="color: white;background-color: #FF788E" class="form-control form-control-sm"  RepeatDirection="Horizontal">
                                                               
                
                 <asp:ListItem   > Done  </asp:ListItem>
                    <asp:ListItem>Pending </asp:ListItem>
                    <asp:ListItem Selected="true" >Cancel </asp:ListItem>
                                                           </asp:DropDownList> </td>

  </tr>
       </table>
       
           <br/> 
      
       <div class="row">

           
           <div class="col-md-4">
               <div class="form-row">
                  
                   <div class="col-md-6">
                       <asp:Button ID="Button1" Text="Save " CssClass="btn btn-sm btn-info" runat="server" />
                   </div>
                  
               </div>
               <div class="form-group">
               </div>
           </div>

           <div class="col-md-4">
           </div>
       </div>
              </div>
        
        </div>
                               </div>
        
        </div>
</asp:Content>


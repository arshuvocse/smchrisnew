<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="LunchAllownaceCancelSelfEntry.aspx.cs" Inherits="Lunch_Allowance_UI_LunchAllownaceCancelSelfEntry" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    
     <style type="text/css">
        
         </style>
    <div class="content">

        <asp:UpdatePanel runat="server" ID="upFormBody">
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
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png" width="20px"  /> Lunch Allowance Cancel Self Entry </h1>
                        </div>

                        <%-- <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="detailsViewButton" Text="Appraisal List" OnClick="detailsViewButton_OnClick" CssClass="btn btn-sm btn-outline-secondary " runat="server"  />
                        </div>--%>

                        <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />

                            <asp:LinkButton ID="detailsViewButton"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
                            

                        </div>

                    </div>
                    <div class="card">
                        <div class="card-body">
                            
                            
                            <div class="row">
                                <style>
                                    .tblTHColorChang{
                                        background-color: #EDF2F5!important;
                                        font-weight: bold;
                                        font-size: 13px;
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
                                     <table class="table table-bordered table-striped">
                                                    <tr>
                                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Employee ID</td>
                                                        <td> <asp:Label runat="server" ID="lblEmpId"></asp:Label>
                                                            <asp:HiddenField runat="server" ID="EmpInfoId"/>
                                                            <asp:HiddenField runat="server" ID="EmpId"/>
                                                        </td>

                                                        
                                                         <td  class="tblTHColorChang" style="width: 20%; padding: 10px;">Supervisor</td>
                                                        <td>  <asp:Label ID="ReportingLabel"  runat="server"></asp:Label></td>
                                                    </tr>
                                                    
                                                     <tr>
                                                        <td  class="tblTHColorChang" style="width: 20%; padding: 10px;">Employee Name</td>
                                                        <td> <asp:Label runat="server" ID="lblEmployeeName"></asp:Label></td>

                                                         
                                                              
                                                         <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Department</td>
                                                        <td>  <asp:Label ID="deptNameLabel"  runat="server"></asp:Label></td>
                                                    </tr>
                                                    
                                                     <tr>
                                                        <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Designation</td>
                                                        <td>   <asp:Label ID="desigNameLabel" runat="server"></asp:Label></td>

                                                     <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Office</td>
                                                        <td>  <asp:Label ID="LocationLabel"   runat="server"></asp:Label></td>

                                                    </tr>

                                                    
                                                     <tr>
                                                        <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Date Of Joining</td>
                                                        <td>     <asp:Label ID="joiningDateLabel"  runat="server"></asp:Label></td>
  <td style="width: 20%; padding: 10px;" class="tblTHColorChang" >Place</td>
                                                        <td> <asp:Label runat="server" ID="lblPlace"></asp:Label></td>
                                                    </tr>
                                                    
                                                    
                                                 
                                                    
                                                    
                                                    </table>
                            </div>

        
                        
                            
                            <div class="row">
                                
                                <div class="col-md-4"></div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Cancel From Date </label>
                                        <asp:TextBox ID="txtCancelDate" runat="server" AutoCompleteType="Disabled" CausesValidation="true"  placeholder=" Cancel Date " CssClass="form-control form-control-sm"
                                                     ></asp:TextBox>
                                        
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                                      Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                      TargetControlID="txtCancelDate" />
                                        
                                        
                                        
                                        
                          <%--              
                                        <asp:TextBox ID="effectiveDateTextBox" AutoCompleteType="Disabled" runat="server" AutoPostBack="True" OnTextChanged="effectiveDateTextBox_OnTextChanged"  CausesValidation="true" 
                                                     class="form-control form-control-sm"></asp:TextBox>
                                         
                                          
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                                      Format="dd-MMM-yyyy" CssClass="MyCalendar" PopupPosition="TopRight"  
                                                                      TargetControlID="effectiveDateTextBox" />--%>
                                   

                                    </div>
                                    
                                      <div class="form-group">
                                        <label>Cancel To Date </label>
                                        <asp:TextBox ID="txtCancelToDate" runat="server" AutoCompleteType="Disabled" CausesValidation="true"  placeholder=" Cancel Date " CssClass="form-control form-control-sm"
                                                     ></asp:TextBox>
                                        
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                                      Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                                      TargetControlID="txtCancelToDate" />
                                        
                                        
                                        
                                        
                          <%--              
                                        <asp:TextBox ID="effectiveDateTextBox" AutoCompleteType="Disabled" runat="server" AutoPostBack="True" OnTextChanged="effectiveDateTextBox_OnTextChanged"  CausesValidation="true" 
                                                     class="form-control form-control-sm"></asp:TextBox>
                                         
                                          
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                                      Format="dd-MMM-yyyy" CssClass="MyCalendar" PopupPosition="TopRight"  
                                                                      TargetControlID="effectiveDateTextBox" />--%>
                                   

                                    </div>
                                    
                                    <div class="form-group">
                                        <label>Comments </label>
                                        <asp:TextBox ID="txtComments" runat="server"  placeholder="Comments" TextMode="MultiLine" Rows="2" Columns="2" CssClass="form-control"
                                        ></asp:TextBox>
                                    </div>

                                </div>
                                <div class="col-md-4"></div>

                            </div>

                         

                        
                        <div class="form-row">
                            <div class="col-md-4">
                            </div>
                            <div class="col-md-4">
                                <div class="form-group" style="margin-left: 30px;">
                                            <asp:HiddenField runat="server" ID="HFCompanyId" />
                                    <asp:LinkButton runat="server" ID="DraftButton" Visible="False"  OnClick="DraftButton_OnClick"    CssClass="btn btn-success "><span aria-hidden="true" class="fa fa-drafting-compass"></span>  &nbsp; Draft </asp:LinkButton>
                                    

                                    <asp:LinkButton runat="server" ID="btnSubmit" Visible="False" OnClick="btnSubmit_OnClick"  CssClass="btn btn-info"><span aria-hidden="true" class="fa fa-check"></span>  &nbsp; Submit </asp:LinkButton>
                                          
                                </div> 
                            </div>

   
        
                        </div>


                            <link href="../UserSetup/ButtonGrups.css" rel="stylesheet" />

                     
                        </div>
                    </div>
                </div>
            </ContentTemplate>

        </asp:UpdatePanel>

    </div>
</asp:Content>


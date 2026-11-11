<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="KPIDashboard.aspx.cs" Inherits="UserProfile_KPIDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
                        <div class="icon"><img src="../Report_Pages/app.png" width="20px"  /> </div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">KPI Information </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="homeButton" Visible="False" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server"   />
                        <asp:Button ID="detailsViewButton"  Visible="False"  Text="View List" CssClass="btn btn-sm btn-outline-secondary " runat="server"  />
                    </div>

                </div>
                <!-- //END PAGE HEADING -->
                
                   <link href="UserProfileShadow.css" rel="stylesheet" />

       <style>
                    .imgshadow {
                      -webkit-box-shadow: 5px 5px 15px 5px #000000; 
box-shadow: 5px 5px 15px 5px #000000;
                    }
                     
                                                                                                 
                </style>
    

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">

                            <div class="row">

                                <div class="col-md-12">
                                     <div class="row">
            <div class="col-md-4">

                <div class="portlet light profile-sidebar-portlet bordered" style="box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);">
                    <style>
                        .scenter {
                            display: block;
                            margin-left: auto;
                            margin-right: auto;
                            width: 50%;
                        }
                    </style>
                    <div class="profile-userpic">
                        <asp:Image ID="UserImage" runat="server" CssClass="img-responsive scenter" alt="" />
                      <%--  <img src="../Assets/man-icon.png" class="img-responsive scenter" alt="">--%>
                    </div>
                    <div class="profile-usertitle">
                        
                         <div class="profile-usertitle-name">
                           <label class="font-weight-bold" style="color: black">ID: </label><asp:Label runat="server" ID="lblID" />
                        </div>
                      <div class="profile-usertitle-job">
                          <label class="font-weight-bold" style="color: black"> Name:</label> <asp:Label runat="server" ID="lblshortName" />
                        </div>
                 <div class="profile-usertitle-nameDES">
                         <label class="font-weight-bold" style="color: black"> Designation:</label>  <asp:Label runat="server" ID="lblDesignation" />
                        </div>
                    </div>
                  <%--  <div class="profile-userbuttons">--%>
                        <%--<asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>--%>
                     <%--   <asp:Button ID="btnPrintCv" runat="server" OnClick="btnPrintCv_Click" CssClass="btn btn-info  btn-sm" Text="Print CV" />--%>
                        <%--</ContentTemplate>
                             </asp:UpdatePanel>--%>
                 <%--   </div>--%>
                    <br />
                    <%--   <div class="profile-usermenu">
                <ul class="nav">
                     <li class="active">
                        <a href="#">
                            <i class="icon-home"></i> Ticket List </a>
                    </li>
                    <li>
                        <a href="#">
                            <i class="icon-settings"></i> Support Staff </a>
                    </li>
                    <li>
                        <a href="#">
                            <i class="icon-info"></i> Configurations </a>
                    </li>
                </ul>
            </div>--%>
                </div>
            </div>
                                    </div>
                                    
                                    
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
                                         
                                         
                                            <div class="col-md-2" runat="server" Visible="False">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                        <asp:DropDownList ID="ddlFinancialYear" AutoPostBack="true" OnSelectedIndexChanged="ddlFinancialYear_OnSelectedIndexChanged" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                     <table class="table table-bordered table-striped">
                                         
                                      
                                                  
                                                    
                                                     <tr>
                                                        <td  class="tblTHColorChang" style="width: 20%; padding: 10px; text-align: center">KPI</td>
<td  class="tblTHColorChang" style="width: 20%; padding: 10px;text-align: center">Self</td>

                                                         
                                                              
                                          
                                                         
                                                         <td  class="tblTHColorChang" style="width: 20%; padding: 10px;text-align: center">Appraisal</td>
                                                         
                                                         
                                                           <td  class="tblTHColorChang" style="width: 20%; padding: 10px;text-align: center">Approval</td>
                                                    </tr>
                                         
                                         
                                          <tr>
                                           
                                            <td>

                                                <table class="table table-bordered table-striped">
                                                    <tr>
                                                        <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Status</td>
                                                        <td class="tblTHColorChang"> <asp:Label ID="lblKPIStatus" runat="server" Text=""></asp:Label></td>

                                                    </tr>
                                                    </table>
                                                    </td>
                                              
                                              
                                                   
                                            <td>

                                                <table class="table table-bordered table-striped">
                                                    <tr>
                                                        <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Status</td>
                                                        <td class="tblTHColorChang"> <asp:Label ID="lblSelfStatus" runat="server" Text=""></asp:Label></td>

                                                    </tr>
                                                    </table>
                                                    </td>
                                              
                                              
                                                           
                                            <td>

                                                <table class="table table-bordered table-striped">
                                                    <tr>
                                                        <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Status</td>
                                                        <td class="tblTHColorChang"> <asp:Label ID="lblAppraisalStatus" runat="server" Text=""></asp:Label></td>

                                                    </tr>
                                                    </table>
                                                    </td>
                                              
                                              
                                                                                          <td>

                                                <table class="table table-bordered table-striped">
                                                    <tr>
                                                        <td class="tblTHColorChang"  style="width: 20%; padding: 10px;text-align: center">KPI</td>
                                                        <td style="width: 20%; padding: 10px;text-align: center" class="tblTHColorChang">Appraisal</td>

                                                    </tr>
                                                    
                                                    <tr>
                                                          <td>

                                                <table class="table table-bordered table-striped">
                                                    <tr>
                                                        <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Done</td>
                                                        <td class="tblTHColorChang"> <asp:Label ID="lblKPIDoneCount" runat="server" Text=""></asp:Label></td>

                                                    </tr>
                                                    
                                                    
                                                     <tr>
                                                        <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Pending</td>
                                                        <td class="tblTHColorChang"> <asp:Label ID="lblKPIPendingCount" runat="server" Text=""></asp:Label></td>

                                                    </tr>
                                                    </table>
                                                    </td>
                                                        
                                                        
                                                        
                                                           <td>

                                                <table class="table table-bordered table-striped">
                                                    <tr>
                                                        <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Done</td>
                                                        <td class="tblTHColorChang"> <asp:Label ID="lblAppraisalDoneCounting" runat="server" Text=""></asp:Label></td>

                                                    </tr>
                                                    
                                                    
                                                     <tr>
                                                        <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Pending</td>
                                                        <td class="tblTHColorChang"> <asp:Label ID="lblAppraisalPendingCounting" runat="server" Text=""></asp:Label></td>

                                                    </tr>
                                                    </table>
                                                    </td>
                                                    </tr>
                                                    </table>
                                                    </td>

                                              </tr>
                                                    
                                                    
                                    </table>
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


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="EmpOthers.aspx.cs" Inherits="UserSetup_EmpOthers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="container-fluid">
        <div class="page-heading">
            <div class="page-heading__container">
                <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" />Update Information</h1>
            </div>
            <div class="page-heading__container">
                <asp:Label runat="server" ID="lbl_Mode"></asp:Label>
                <span>
                    <label class="btn infoN" style="font-size: 13px;">
                        Employee ID:  &nbsp;
                        <asp:Label runat="server" class="label w3-tag w3-green" Style="font-size: 13px;" ID="empMasterCode"></asp:Label></label>
                  
                    
                  <label class="btn infoN" style="font-size: 13px;">
                      Employee Name: &nbsp; 
                      <asp:Label runat="server" class="label w3-tag w3-green" Style="font-size: 13px;" ID="lblEmpName"></asp:Label></label>
                    
                   <label class="btn infoN" style="font-size: 11px;">Designation: &nbsp; 
                      <asp:Label runat="server" class="label w3-tag w3-green" Style="font-size: 9px;" ID="lblDesignation"></asp:Label></label>
                </span>
            </div>
            <div class="page-heading__container float-right d-none d-sm-block">
                
                <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                        <asp:LinkButton ID="detailsViewButton"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
             <%--   <asp:Button ID="detailsViewButton" Text="Update Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                  <asp:Button ID="btnEditInfo" Text="Back to List"  Visible="False" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="btnEditInfo_OnClick" />--%>
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
            padding: 5px;
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
                padding-left: 6px;
               
            }

                </style>
                
                
                  <style>
                            .NavbarAcc {
                                color: white!important;
    background-color: #5078B3!important;
    font-family: Arial, Sans-Serif!important;
    font-size: 14px!important;
    font-weight: bold!important;
    padding: 10px!important;
    margin-top: 5px!important;
    cursor: pointer!important;
                            }
                        </style>
                        <nav class="navbar navbar-light bg-light NavbarAcc">
 <span>10. Others</span>
</nav>
                 
                 

                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <br />
                                        <div class="form-row">
                                            <div class="col-3">
                                                    <div class="Label_Title">Extra Curriculam Activities List</div>
                                                 <br />
                                                <div style="overflow: scroll; height: 200px">
                                                    <div class="form-group">
                                                          
                                                          <asp:CheckBox runat="server" ID="chkExtraAll" CssClass="SelectchkChoice" AutoPostBack="True" OnCheckedChanged="chkExtraAll_OnCheckedChanged" Text=" Select All / Unselect All" />
                                                                        <br />
                                                        <asp:CheckBoxList runat="server" CssClass="chkChoice" ID="chkExtraCurriculam" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                   <div class="Label_Title">Other Talents List</div>
                                                 <br />
                                                <div style="overflow: scroll; height: 200px">
                                                    <div class="form-group">
                                                        <asp:CheckBox runat="server" ID="chkOtherALL" CssClass="SelectchkChoice" AutoPostBack="True" OnCheckedChanged="chkOtherALL_OnCheckedChanged" Text=" Select All / Unselect All" />
                                                                        <br />
                                                        <asp:CheckBoxList runat="server"  CssClass="chkChoice" ID="chkOtherTalents" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                  <div class="Label_Title">Achievements List</div>
                                                 <br />
                                                <div style="overflow: scroll; height: 200px">
                                                    <div class="form-group">
                                                       <asp:CheckBox runat="server" ID="chkAchievementsALL" CssClass="SelectchkChoice" AutoPostBack="True" OnCheckedChanged="chkAchievementsALL_OnCheckedChanged" Text=" Select All / Unselect All" />
                                                       
                                                                        <br />
                                                        <asp:CheckBoxList runat="server"   CssClass="chkChoice" ID="chkAchievements" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                  <div class="Label_Title">Hobby List</div>
                                                 <br />
                                                <div style="overflow: scroll; height: 200px">
                                                    <div class="form-group">
                                                         <asp:CheckBox runat="server" ID="chkHobbyALL" CssClass="SelectchkChoice" AutoPostBack="True" OnCheckedChanged="chkHobbyALL_OnCheckedChanged" Text=" Select All / Unselect All" />
                                                                        <br />
                                                        <asp:CheckBoxList runat="server" CssClass="chkChoice" ID="chkHobby" />
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                        <br />
                                        <div class="form-row">
                                            <div class="col-md-10">
                                                <asp:HiddenField runat="server" ID="hdpk" />

                                                <link href="ButtonGrups.css" rel="stylesheet" />
                                                <div class="ui-group-buttons">

                                                    <asp:Button runat="server" ID="btn_Save" OnClientClick="return confirm('Are you sure you want to Save?')" OnClick="btn_Save_OnClick" Text="  Save  " CssClass="btn btn-sm btn-info" />
                                                     <div class="or or-sm"></div>
                                                    <asp:Button runat="server" ID="btnNext" OnClientClick="return confirm('Are you sure you want to Save & Next ?')"  OnClick="btn_Next_OnClick" Text="   Save & Next   " CssClass="btn btn-sm btn-success" />

                                                    <div class="or or-sm"></div>
                                                    <asp:Button ID="cancelButton"  OnClientClick="return confirm('Are you sure you want to Exit ?')"  Text="  Exit  " OnClick="cancelButton_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                                                </div>
                                            </div>
                                            
                                            <div class="col-md-2">
                                                  <asp:LinkButton CssClass="hh previous" OnClick="lbPrevious_OnClick" ID="lbPrevious" runat="server">&laquo; Previous</asp:LinkButton>
                                                <asp:LinkButton CssClass="hh next" runat="server" ID="lblNext" OnClick="lblNext_OnClick">Next &raquo;</asp:LinkButton>
                                            
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            
            </div>
        </div>


    </div>
</asp:Content>


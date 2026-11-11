<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="KPIMIdyearStatusSetup.aspx.cs" Inherits="MasterSetup_UI_KPIMIdyearStatusSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .checkbox label {
            display: inline;
        }

        .margin-right {
            margin-right: 5px;
        }
    </style>

    <style>
        .resize {
            resize: none;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">

    <div class="content" id="content" >
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                        <div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">KPI Mid-Year Setup  </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                           <asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="HomeButton_OnClick" />
                        <asp:Button Visible="false" ID="detailsViewButton" Text="Go Back" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>

                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">
                                                       <div class="row">
                               
                               <div class="col-md-2">

     <div class="form-group">
         <label>Company Name: </label>
         <asp:DropDownList ID="companyDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="companyDropDownList_SelectedIndexChanged"></asp:DropDownList>

     </div>
 </div>
 <div class="col-md-2">
     <div class="form-group">
         <label>Financial Year:</label>
         <asp:DropDownList ID="financialYearDropDownList" CssClass="form-control form-control-sm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="financialYearDropDownList_SelectedIndexChanged"></asp:DropDownList>
     </div>
 </div>


                                <div class="col-md-2">
    <div class="form-group">
        <label>Status:</label>

       <p>  <asp:Label ID="lblStatus" class="form-control form-control-sm" runat="server"  ></asp:Label></p>
    </div>
</div>

                           </div>

                         
             
                           <div class="row">
               <div class="col-md-2">
                   <label class="form-label">Select Action:</label>
                    <asp:RadioButtonList ID="RadioButtonList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList_SelectedIndexChanged" TextAlign="Right"  >
                       <asp:ListItem Text="Start Mid Year KPI" Value="Start"   />
                       <asp:ListItem Text="Stop Mid Year KPI" Value="Stop"    />
                   </asp:RadioButtonList>
               </div>
                               </div>
                         
                            <div class="row">
                                
                                <div class="col-md-3">
                                     

                                    <div class="form-group">
                                         
                                        <asp:HiddenField ID="OccupationIdHiddenField" runat="server" />
                                       
                                    </div>

                                    

                               

                      
                                    


                                </div>
                              
                            </div>


                            <div class="row">
                                
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Button ID="submitButton" Text="Save" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="submitButton_Click" />
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
                    </div>
                </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
            

</asp:Content>


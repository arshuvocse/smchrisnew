<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="ExeOfficeDocSubCategory.aspx.cs" Inherits="HealthCare_UI_ExeOfficeDocSubCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
         <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    
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
     <div class="content" id="content" >
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
                        <div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;"> Executive Office Document SubCategory </h1>
                    </div>
                 <%--   <div class="page-heading__container float-right d-none d-sm-block">
                           <asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="HomeButton_OnClick" />
                        <asp:Button ID="detailsViewButton" Text="Go Back" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>--%>
                    
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:LinkButton ID="HomeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>

                        <asp:LinkButton ID="detailsViewButton"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
                    </div>

                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">

                 
                            <div class="row">
                                
                                <div class="col-md-3">
                                     

                                    <div class="form-group">
                                        <label>Executive Office Document Category </label>
                                        <label style="color: #a52a2a">*</label>
                                         
                                        <asp:HiddenField ID="HfPk" runat="server" />
                                      <%--  <asp:TextBox ID="TxtCategory" runat="server" class="form-control form-control-sm"></asp:TextBox>--%>

                                        <asp:DropDownList runat="server" ID="ddlCagetory" class="form-control form-control-sm selectMe"/>
                                        
                                                 <script type="text/javascript">
                                                     function pageLoad() {
                                                         $('.selectMe').chosen({ disable_search_threshold: 5, search_contains: true });
                                          

                                      }
               </script>
                                    </div>
                                    
                                    <div class="form-group">
                                        <label>Executive Office Document SubCategory </label>
                                        <label style="color: #a52a2a">*</label>
                                         
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                        <asp:TextBox ID="txtSubCategory" runat="server"  class="form-control form-control-sm"></asp:TextBox>
                                    </div>

                                </div>
                              
                            </div>

              
                            <div class="row">
                                
                                <div class="col-md-3">
                                    <div class="form-group">

                                        
                                        <asp:Button ID="btn_Save" Text="Save" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="submitButton_Click" />
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
          <%--  <Triggers>
                <asp:AsyncPostBackTrigger ControlID="submit_Button" runat="server"/>
            </Triggers>--%>
        </asp:UpdatePanel>
    </div>
</asp:Content>


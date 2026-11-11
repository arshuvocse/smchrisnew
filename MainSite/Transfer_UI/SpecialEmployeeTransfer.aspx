<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="SpecialEmployeeTransfer.aspx.cs" Inherits="HealthCare_UI_SpecialEmployeeTransfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">\
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

           .SelectchkChoice label {
            padding-left: 6px;
            font-weight: bold;
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
                
                  <%--<asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>--%>
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                        <div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">  Special Employee Transfer </h1>
                    </div>
                   <%-- <div class="page-heading__container float-right d-none d-sm-block">
                           <asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="HomeButton_OnClick" />
                        <asp:Button ID="detailsViewButton" Text="Go Back" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>--%>
                    
                    <div class="page-heading__container float-right d-none d-sm-block">
                       <%-- <asp:LinkButton ID="HomeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="HomeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                        <asp:LinkButton ID="detailsViewButton"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>--%>
                    </div>

                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">
                                 <script type="text/javascript">
                                                         function pageLoad() {

                                                            

                                                             $('.seleceeemm').chosen({ disable_search_threshold: 5, search_contains: true });
                                                         }



</script>
                 
                            <div class="row" runat="server" Visible="False">
                                
                                <div class="col-md-3">
                                     

                                    <div class="form-group">
                                        <label>Executive Office Document Category </label>
                                        <label style="color: #a52a2a">*</label>
                                         
                                        <asp:HiddenField ID="MeetingCategoryIdHiddenField" runat="server" />
                                        <asp:TextBox ID="TxtCategory" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                    </div>

                                </div>
                              
                            </div>
                            
                            
                            <div class="row"  >
                                
                                   <div class="col-md-4">
                                          <div class="form-group">
                                        <label class="pull-right">Transfer Category:</label>
                                              </div>
                                       </div>
                                
                                <div class="col-md-3">
                                     

                                    <div class="form-group">
                                        <asp:RadioButtonList ID="RadioButtonList1" CssClass="SelectchkChoice" runat="server" RepeatLayout="Flow">
                                            <asp:ListItem>Full Transfer</asp:ListItem>
                                            <asp:ListItem>Salary Transfer</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                </div>
                              
                            </div>
                            
                            
                            
                               <div class="row"  >
                                
                                   <div class="col-md-4">
                                          <div class="form-group">
                                        <label class="pull-right">Record Update Type:</label>
                                              </div>
                                       </div>
                                
                                <div class="col-md-3">
                                     

                                    <div class="form-group">
                                        <asp:RadioButtonList ID="RadioButtonList2" CssClass="SelectchkChoice" runat="server" RepeatLayout="Flow">
                                            <asp:ListItem>Salary Transfer</asp:ListItem>
                                            <asp:ListItem> </asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                </div>
                              
                            </div>
                                 <div class="row"  >
                                
                                   <div class="col-md-4">
                                          <div class="form-group">
                                        <label class="pull-right">Record View Type:</label>
                                              </div>
                                       </div>
                                
                                <div class="col-md-3">
                                     

                                    <div class="form-group">
                                        <asp:RadioButtonList ID="RadioButtonList3" CssClass="SelectchkChoice" runat="server" RepeatLayout="Flow">
                                            <asp:ListItem>Only View</asp:ListItem>
                                            <asp:ListItem>Edit & View </asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                </div>
                              
                            </div>
                            
                                   <div class="row"  >
                                
                                   <div class="col-md-4">
                                          <div class="form-group">
                                        <label class="pull-right">From Company :</label>
                                              </div>
                                       </div>
                                
                                <div class="col-md-3">
                                     

                                    <div class="form-group">
                                       <asp:DropDownList ID="companyDropDownList" class="form-control form-control-sm seleceeemm"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="companyDropDownList_OnSelectedIndexChangedanyDropDownList_OnSelectedIndexChanged"  ></asp:DropDownList>
                                    </div>

                                </div>
                              
                            </div>
                            
                                    <div class="row"  >
                                
                                   <div class="col-md-4">
                                          <div class="form-group">
                                        <label class="pull-right">Employee Name:</label>
                                              </div>
                                       </div>
                                
                                <div class="col-md-3">
                                     

                                    <div class="form-group">
                                       <asp:DropDownList  runat="server"   ID="ddlEmpInfo" class="form-control form-control-sm seleceeemm" />
                                    </div>

                                </div>
                              
                            </div>
                            
                            
                               <div class="row"  >
                                
                                   <div class="col-md-4">
                                          <div class="form-group">
                                        <label class="pull-right">To Company :</label>
                                              </div>
                                       </div>
                                
                                <div class="col-md-3">
                                     

                                    <div class="form-group">
                                       <asp:DropDownList ID="ddlToCompany" class="form-control form-control-sm seleceeemm"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="companyDropDownList_OnSelectedIndexChangedanyDropDownList_OnSelectedIndexChanged"  ></asp:DropDownList>
                                    </div>

                                </div>
                              
                            </div>
              
                            <div class="row">
                                  <div class="col-md-4">
                                      </div>
                                <div class="col-md-3">
                                    <div class="form-group">

                                        
                                        <asp:Button ID="btn_Save" Text="Save" CssClass="btn btn-sm btn-info"  runat="server"  />
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


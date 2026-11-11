<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="DesignationInformation.aspx.cs" Inherits="MasterSetup_UI_DesignationInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
       <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <style>
         .resize
    {
        resize:none;
    }
    </style>
    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                        <div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Designation Information  </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="detailsViewButton" Text="View List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>

                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">

                            <div class="row">
                                 
                                <div class="col-md-3">
                                    
                                        <div class="form-group">
                                            <label>Company </label>
                                            <label style="color: #a52a2a">*</label>


                                            <asp:DropDownList ID="CompanyDropDown" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                        </div>
                                    

                                    <div class="form-group">
                                        <label>Employee Category </label>
                                        <label style="color: #a52a2a">*</label>
                                        <asp:DropDownList ID="EmployeeCategoryDropDownList" runat="server" class="form-control form-control-sm selectme"  AutoPostBack="True" OnSelectedIndexChanged="EmployeeCategoryDropDownList_OnSelectedIndexChanged"></asp:DropDownList>
                                        
                                        
                                            <script type="text/javascript">
                                                         function pageLoad() {
                                                             $('.selectme').chosen({ disable_search_threshold: 5, search_contains: true });

                                                         }
                                                     </script>

                                        <asp:HiddenField ID="EmployeeCategoryHiddenField" runat="server" />
                                        <asp:HiddenField ID="EmployeeCategoryIdHiddenField" runat="server" />
                                    </div>

                                 <%--   <div class="form-group">
                                        <label>Salary Type </label>
                                        <label style="color: #a52a2a">*</label>
                                        <asp:DropDownList ID="salaryTypeDropDownList" runat="server" class="form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="salaryTypeDropDownList_OnSelectedIndexChanged"></asp:DropDownList>
                                    </div>--%>
                                    <div class="form-group">
                                        <label>Salary Grade </label>
                                        <label style="color: #a52a2a">*</label>
                                        <asp:DropDownList ID="salaryGradeDropDownList" runat="server" class="form-control form-control-sm selectme" AutoPostBack="True" OnSelectedIndexChanged="salaryGradeDropDownList_OnSelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    
                                    
                                    <div class="form-group">
                                        <label>Designation Name</label>
                                         <asp:HiddenField ID="DesignationIdHiddenField" runat="server" />
                                        <asp:HiddenField ID="DesignationHiddenField" runat="server" />
                                        <asp:TextBox ID="designationTextBox" runat="server" class="form-control form-control-sm" AutoPostBack="False" OnTextChanged="designationTextBox_OnTextChanged"></asp:TextBox>
                                    </div>
                        
                                <%--    <div class="form-group">
                                        <label>Salary Step </label>
                                        <label style="color: #a52a2a">*</label>
                                        <asp:DropDownList ID="salaryStepDropDownList" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>--%>
                                    
                                                          <div class="form-group">
                                          <br />


                                        <div class="checkbox">

                                            <label class="btn btn-default">
                                                <asp:CheckBox ID="isActiveCheckBox" CssClass="checkbox margin-right" runat="server" />
                                                <label>Is Active </label>
                                            </label>

                                        </div>
                                        <style>
                                            .checkbox .btn,
                                            .checkbox-inline .btn {
                                                padding-left: 0em;
                                                min-width: 14em;
                                            }



                                            .checkbox label,
                                            .checkbox-inline label {
                                                text-align: left;
                                                padding-left: 0.7em;
                                            }
                                        </style>
                                    </div>

                                </div>

                                <div class="col-md-3">


                                    <div class="form-group">
                                        <label>Description </label>
                                        <asp:TextBox ID="descriptionTexBox" runat="server" CssClass="form-control resize" Rows="1" Columns="20" TextMode="MultiLine"  ></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label>Remarks </label>
                                        <asp:TextBox ID="remarksTextBox" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                    </div>

              
                                </div>
                                <div class="col-md-4">
                                </div>
                            </div>


                        


                        <div class="row">
                               
                                <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Button ID="submitButton" Text="Save" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="submitButton_Click" />
                                     <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                        <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />
                                      <asp:Button ID="cancelButton" Text="Cancel" CssClass="btn btn-sm warning" Visible="False" runat="server" OnClick="cancelButton_OnClick" BackColor="#FFCC00" />
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

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>


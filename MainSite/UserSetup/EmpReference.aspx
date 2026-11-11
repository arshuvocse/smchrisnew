<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="EmpReference.aspx.cs" Inherits="UserSetup_EmpReference" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
      <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="container-fluid">
        <div class="page-heading">
            <div class="page-heading__container">
                <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" />&nbsp;Update Information</h1>
            </div>
            <div class="page-heading__container">
                <asp:Label runat="server" ID="lbl_Mode"></asp:Label>
                <span>
                    <label class="btn infoN" style="font-size: 14px;">Employee ID:  &nbsp;
                        <asp:Label runat="server" class="label w3-tag w3-green" Style="font-size: 14px;" ID="empMasterCode"></asp:Label></label>
                    &nbsp;
                    
                  <label class="btn infoN" style="font-size: 14px;">Employee Name: &nbsp; 
                      <asp:Label runat="server" class="label w3-tag w3-green" Style="font-size: 14px;" ID="lblEmpName"></asp:Label></label>
                    
                     <label class="btn infoN" style="font-size: 12px;">Designation: &nbsp; 
                      <asp:Label runat="server" class="label w3-tag w3-green" Style="font-size: 10px;" ID="lblDesignation"></asp:Label></label>
                </span>
            </div>
            <div class="page-heading__container float-right d-none d-sm-block">
                
                 <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                        <asp:LinkButton ID="LinkButton1"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
<%--                <asp:Button ID="detailsViewButton" Text="Update Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                
                  <asp:Button ID="btnEditInfo" Text="Back to List" Visible="False"  CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="btnEditInfo_OnClick" />--%>
            </div>
        </div>
        <div class="card">
            <div class="card-body">
                
                
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
 <span>8. Reference</span>
</nav>
                 

                

                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <br />
                                        <div class="form-row">
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Reference Name</label>
                                                    <asp:TextBox runat="server" ID="txt_RefReferenceName" class="form-control form-control-sm" />
                                                </div>
                                            </div>

                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Occupation</label>
                                                    <asp:DropDownList runat="server" ID="ddlRefOccupation" class="form-control form-control-sm clsSelect">
                                                    </asp:DropDownList>
                                                                    <script type="text/javascript">
                                                                        function pageLoad() {
                                                                            $('.clsSelect').chosen({ disable_search_threshold: 5, search_contains: true });







                                                                        }
</script>
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Address</label>
                                                    <asp:TextBox runat="server" ID="txt_RefAddress" class="form-control form-control-sm" />
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Mobile</label>
                                                    <asp:TextBox runat="server" ID="txt_RefMobile" class="form-control form-control-sm" />
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server"
                                                        Enabled="True" TargetControlID="txt_RefMobile" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                            <div class="col-3"
                                                style="margin-top: 19px;">
                                                <div class="form-group">
                                                    <asp:Button runat="server" ID="btnAddReference" Text="Add Reference" CssClass="btn btn-outline-success btn-sm" OnClick="btnAddReference_OnClick" />
                                                </div>
                                            </div>

                                        </div>



                                        <div style="overflow: scroll;">
                                            <asp:GridView Width="100%" ID="gv_Reference" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL#">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex + 1%>
                                                            <asp:HiddenField ID="EmpReferenceId" runat="server" Value='<%#Eval("EmpReferenceId")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Reference Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_ReferenceName" runat="server" Text='<%#Eval("ReferenceName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Ref Occupation">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_RefOccupation" runat="server" Text='<%#Eval("RefOccupationName")%>'></asp:Label>
                                                            <asp:HiddenField runat="server" ID="hfRefOccupation" Value='<%#Eval("RefOccupation")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Ref Address">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_RefAddress" runat="server" Text='<%#Eval("RefAddress")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Ref Mobile">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_RefMobile" runat="server" Text='<%#Eval("RefMobile")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lb_EditReference" runat="server" OnClick="lb_EditReference_OnClick"><img src="../Assets/img/rsz_edit.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Remove">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lb_RemoveReference" runat="server" OnClick="lb_RemoveReference_OnClick"><img src="../Assets/img/delete.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>



                                        <br />
                                        <div class="form-row">
                                            <div class="col-md-10">
                                                <asp:HiddenField runat="server" ID="hdpk" />

                                                <link href="ButtonGrups.css" rel="stylesheet" />
                                                <div class="ui-group-buttons">

                                                    <asp:Button runat="server" ID="btn_Save" OnClientClick="return confirm('Are you sure you want to Save ?')" OnClick="btn_Save_OnClick" Text="  Save  " CssClass="btn btn-sm btn-info" />
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


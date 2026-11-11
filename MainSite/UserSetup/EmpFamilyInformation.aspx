<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="EmpFamilyInformation.aspx.cs" Inherits="UserSetup_EmpFamilyInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
     <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
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
                    <label class="btn infoN" style="font-size: 14px;">Employee ID:  &nbsp;
                        <asp:Label runat="server" class="label w3-tag w3-green" Style="font-size: 14px;" ID="empMasterCode"></asp:Label></label>
                    
                    
                  <label class="btn infoN" style="font-size: 14px;">Employee Name: &nbsp; 
                      <asp:Label runat="server" class="label w3-tag w3-green" Style="font-size: 14px;" ID="lblEmpName"></asp:Label></label>
                    
                     <label class="btn infoN" style="font-size: 11px;">Designation: &nbsp; 
                      <asp:Label runat="server" class="label w3-tag w3-green" Style="font-size: 9px;" ID="lblDesignation"></asp:Label></label>
                </span>
            </div>
            <div class="page-heading__container float-right d-none d-sm-block">
                
                 <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                        <asp:LinkButton ID="LinkButton1"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
              <%--  <asp:Button ID="detailsViewButton" Text="Update Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
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
 <span>4. Family Information</span>
</nav>
                 <br/>

                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="form-row">
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Spouse Name</label>
                                                    <asp:TextBox runat="server" ID="txt_EmpSpouseName" class="form-control form-control-sm" />
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Spouse's Max Education</label>
                                                    <asp:DropDownList runat="server" ID="ddlEmpSpouseMaxEdu" class="form-control form-control-sm">
                                                    </asp:DropDownList>
                                                    
                                                                       <script type="text/javascript">
                                                                           function pageLoad() {
                                                                               $('#cpFormBody_ddlEmpSpouseMaxEdu').chosen({ disable_search_threshold: 5, search_contains: true });

                                                                               $('#cpFormBody_ddlEmpSpouseOccupation').chosen({ disable_search_threshold: 5, search_contains: true });
                                                                               $('#cpFormBody_ddlEmpChildrenOccupation').chosen({ disable_search_threshold: 5, search_contains: true });

                                                                               



                                                                           }
</script>
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Spouse's Occupation</label>
                                                    <asp:DropDownList runat="server" ID="ddlEmpSpouseOccupation" class="form-control form-control-sm">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Spouse's DOB</label>
                                                    <asp:TextBox runat="server" ID="txt_EmpSpouseDOB" class="form-control form-control-sm" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server"
                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                        TargetControlID="txt_EmpSpouseDOB" />
                                                </div>
                                            </div>
                                            <div class="col-3">
                                                <div class="form-group">
                                                    <label>Marriage Date</label>
                                                    <asp:TextBox runat="server" ID="txt_EmpMarriageDate" class="form-control form-control-sm" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server"
                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                        TargetControlID="txt_EmpMarriageDate" />
                                                </div>
                                            </div>
                                            <div class="col-3" style="margin-top: 19px;">
                                                <div class="form-group">
                                                    <asp:HiddenField ID="hfEditSpouseId" runat="server" />
                                                    <asp:Button runat="server" ID="btnAddSpouse" Text="Add Spouse" CssClass="btn btn-outline-success btn-sm" OnClick="btnAddSpouse_OnClick" />
                                                </div>
                                            </div>
                                        </div>
                                        <div>
                                            <div style="overflow: scroll; width: 100%">
                                                <asp:GridView ID="gv_Spouse" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL#">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                                <asp:HiddenField ID="hfSpouseId" runat="server" Value='<%#Eval("SpouseId") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Spouse Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_SpouseName" runat="server" Text='<%#Eval("SName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Max Education">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_SpouseMaxEdu" runat="server" Text='<%#Eval("SpouseMaxEducationName") %>'></asp:Label>
                                                                <asp:HiddenField runat="server" ID="hfSpouseMaxEdu" Value='<%#Eval("SpouseMaxEducation") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Occupation">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_SpouseOccupation" runat="server" Text='<%#Eval("SpouseOccupationName") %>'></asp:Label>
                                                                <asp:HiddenField runat="server" ID="hfSpouseOccupation" Value='<%#Eval("SpouseOccupation") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Spouse DOB">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_SpouseDOB" runat="server" Text='<%#Eval("SDob") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lb_EditSpouse" runat="server" OnClick="lb_EditSpouse_OnClick"><img src="../Assets/img/rsz_edit.png" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remove">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lb_RemoveSpouse" runat="server" OnClick="lb_RemoveSpouse_OnClick"> <img src="../Assets/img/delete.png" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>

                                        <div class="form-row">

                                            <div class="col-2">
                                                <div class="form-group">
                                                    <label>Children Name</label>
                                                    <asp:TextBox runat="server" ID="txt_EmpChildrenName" class="form-control form-control-sm" />
                                                </div>
                                            </div>
                                            <div class="col-2">
                                                <div class="form-group">
                                                    <label>Children Gender</label>
                                                    <asp:DropDownList runat="server" ID="ddlEmpChildrenGender" class="form-control form-control-sm">
                                                        <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                                                        <asp:ListItem Text="FeMale" Value="FeMale"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-2">
                                                <div class="form-group">
                                                    <label>Children Occupation</label>
                                                    <asp:DropDownList runat="server" ID="ddlEmpChildrenOccupation" class="form-control form-control-sm">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-2">
                                                <div class="form-group">
                                                    <label>Children DOB</label>
                                                    <asp:TextBox runat="server" ID="txt_EmpChildrenDOB" class="form-control form-control-sm" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server"
                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                        TargetControlID="txt_EmpChildrenDOB" />

                                                </div>
                                            </div>

                                            <div class="col-2">
                                                <div class="form-group">
                                                    <label>Children Marital Status</label>
                                                    <asp:DropDownList runat="server" ID="ddlChildrenMaritalStatus" class="form-control form-control-sm">
                                                        <asp:ListItem Text="Single" Value="Single"></asp:ListItem>
                                                        <asp:ListItem Text="Married" Value="Married"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-2" style="margin-top: 19px;">
                                                <div class="form-group">
                                                    <asp:Button runat="server" ID="btnAddChildren" Text="Add Children" CssClass="btn btn-outline-success btn-sm" OnClick="btnAddChildren_OnClick" />
                                                </div>
                                            </div>

                                            <div>
                                            </div>
                                        </div>
                                        <div>
                                            <div style="overflow: scroll; width: 100%">
                                                <asp:GridView Margin="0,5,0,0" ID="gv_Children" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL#">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                                <asp:HiddenField ID="EmpChildrenId" runat="server" Value='<%#Eval("EmpChildrenId") %>' />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Children Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ChildrenName" runat="server" Text='<%#Eval("ChildrenName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Children Gender">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ChildrenGender" runat="server" Text='<%#Eval("ChildrenGender") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Children Occupation">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ChildrenOccupation" runat="server" Text='<%#Eval("ChildrenOccupationName") %>'></asp:Label>
                                                                <asp:HiddenField runat="server" ID="hfChildrenOccupation" Value='<%#Eval("ChildrenOccupation") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Children DOB">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ChildrenDOB" runat="server" Text='<%#Eval("ChildrenDOB") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Children Marital Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ChildrenMaritalStatus" runat="server" Text='<%#Eval("ChildrenMaritalStatus") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lb_EditChildren" runat="server" OnClick="lb_EditChildren_OnClick"><img src="../Assets/img/rsz_edit.png" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Remove">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lb_RemoveChildren" runat="server" OnClick="lb_RemoveChildren_OnClick"> <img src="../Assets/img/delete.png" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>



                                        <br />
                                        <div class="form-row">
                                            <div class="col-md-10">
                                                <asp:HiddenField runat="server" ID="hdpk" />


                                                <link href="ButtonGrups.css" rel="stylesheet" />
                                                <div class="ui-group-buttons">

                                                    <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" OnClientClick="return confirm('Are you sure you want to Save ?')" Text="  Save  " CssClass="btn btn-sm btn-info" />
                                                    <div class="or or-sm"></div>
                                                    <asp:Button runat="server" ID="btnNext" OnClick="btn_Next_OnClick" OnClientClick="return confirm('Are you sure you want to Save & Next ?')" Text="   Save & Next   " CssClass="btn btn-sm btn-success" />

                                                    <div class="or or-sm"></div>
                                                    <asp:Button ID="cancelButton" Text="  Exit  " OnClientClick="return confirm('Are you sure you want to Exit ?')" OnClick="cancelButton_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                                                </div>

                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
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


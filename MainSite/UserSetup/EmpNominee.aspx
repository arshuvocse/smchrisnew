<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="EmpNominee.aspx.cs" Inherits="UserSetup_EmpNominee" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

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
                        <asp:LinkButton ID="LinkButton1"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
               
            </div>
        </div>
        <div class="card">
            <div class="card-body">
                
                <style>
                    .imgshadow {
                 -webkit-box-shadow: 0px 10px 13px -7px #000000, 5px 2px 3px 5px rgba(0,0,0,0); 
box-shadow: 0px 10px 13px -7px #000000, 5px 2px 3px 5px rgba(0,0,0,0);  }
                     
                                                                                                 
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
 <span>9. Nominee</span>
</nav>
                 
                <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>--%>
                
                                <br />
                                <div class="form-row">
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Nomination Purpose</label>
                                            <asp:DropDownList runat="server" ID="ddlNomNominationPurpose" class="form-control form-control-sm clsSelect">
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
                                            <label>Nominee Name</label>
                                            <asp:TextBox runat="server" ID="txt_NomNomineeName" class="form-control form-control-sm" />
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Nominee Occupation</label>
                                            <asp:DropDownList runat="server" ID="ddlNomNomineeOccupation" class="form-control form-control-sm clsSelect">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Date of Nomination</label>
                                            <asp:TextBox runat="server" ID="txt_NomDateOfNomination" class="form-control form-control-sm" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender10" runat="server"
                                                Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                TargetControlID="txt_NomDateOfNomination" />
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Percentage</label>
                                            <asp:TextBox runat="server" ID="txt_NomNominationPercentage" class="form-control form-control-sm" />

                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True"
                                                TargetControlID="txt_NomNominationPercentage" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Date of Birth</label>
                                            <asp:TextBox runat="server" ID="txt_NomNomineeDOB" class="form-control form-control-sm" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender11" runat="server"
                                                Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                TargetControlID="txt_NomNomineeDOB" />
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Relation</label>
                                            <asp:DropDownList runat="server" ID="ddlNomNomineeRelation" class="form-control form-control-sm">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Phone No.</label>
                                            <asp:TextBox runat="server" ID="txt_NomNomineeTelephone" class="form-control form-control-sm" />
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server"
                                                Enabled="True" TargetControlID="txt_NomNomineeTelephone" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                   

                                   

                                </div>
                                <div class="form-row">
                                 
                                        <div class="col-6">
                                            
                                             <div class="form-row">
                                             <div class="col-6">
                                        <div class="form-group">
                                            <label>Address</label>
                                            <asp:TextBox runat="server" ID="txt_NomNomineeAddress" class="form-control form-control-sm" />
                                        </div>
                                    </div>
                                    
                                    <div class="col-md-6">
                                        <label>Image Upload</label>
                                          <asp:FileUpload ID="FileUpload1" CssClass="form-control form-control-sm" runat="server" onchange="showpreview(this);" />
                                    </div>
                                              <div class="col-12" >
                                        <div class="form-group">
                                            <asp:Button runat="server" ID="btnAddNominee" Text="Add Nominee" CssClass="btn btn-outline-success btn-sm" OnClick="btnAddNominee_OnClick" />
                                        </div>
                                    </div>
                                                 </div>
                                         
                                        </div>
                                        <div class="col-md-6">
                                         
                                        <img id="imgpreview" height="130" width="120" class="imgshadow" src="" style="border-width: 0px; visibility: hidden;" />
                                      
                                       
                                    </div>
                                
                                    
                                     
                                </div>
                                

                                <div>
                                    <br/>
                                    <br/>
                                    <div style="overflow: scroll;">
                                        <asp:GridView Width="100%" ID="gv_Nominee" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                        <asp:HiddenField ID="EmpNomineeId" runat="server" Value='<%#Eval("EmpNomineeId")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Image">

                                                    <ItemTemplate>

                                                        <asp:Label ID="lbl_NominationImage" Visible="False" runat="server" Text='<%#Eval("NominationImage")%>'>Img</asp:Label>

                                                        <asp:Image ID="Image1rr" runat="server" CssClass="imgshadow"  Width="100" Height="100" ImageUrl='<%# Eval("ShowNominationImage") %>' />
                                                    </ItemTemplate>


                                                   
                                                </asp:TemplateField>
                                                <%--<asp:ImageField DataImageUrlField="ShowNominationImage" ControlStyle-Width="100" ControlStyle-Height="100" HeaderText="Image"></asp:ImageField>--%>

                                                <asp:TemplateField HeaderText="Nomination Purpose">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_NominationPurpose" runat="server" Text='<%#Eval("NominationPurposeName")%>'></asp:Label>
                                                        <asp:HiddenField runat="server" ID="hfNominationPurpose" Value='<%#Eval("NominationPurpose")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nominee Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_NomineeName" runat="server" Text='<%#Eval("NomineeName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date Of Nomination">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_DateOfNomination" runat="server" Text='<%#Eval("DateOfNomination")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nomination Percentage">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_NominationPercentage" runat="server" Text='<%#Eval("NominationPercentage")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nominee DOB">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_NomineeDOB" runat="server" DataFormatString="{0:dd-MMM-yyyy}" Text='<%#Eval("NomineeDOB")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Nominee Relation">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_NomineeRelation" runat="server" Text='<%#Eval("NomineeRelationName")%>'></asp:Label>
                                                        <asp:HiddenField runat="server" ID="hfNomineeRelation" Value='<%#Eval("NomineeRelation")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Nominee Occupation">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_NomineeOccupation" runat="server" Text='<%#Eval("NomineeOccupationName")%>'></asp:Label>
                                                        <asp:HiddenField runat="server" ID="hfNomineeOccupation" Value='<%#Eval("NomineeOccupation")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Nominee Telephone">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_NomineeTelephone" runat="server" Text='<%#Eval("NomineeTelephone")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nominee Address">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_NomineeAddress" runat="server" Text='<%#Eval("NomineeAddress") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lb_EditNominee" runat="server" OnClick="lb_EditNominee_OnClick"><img src="../Assets/img/rsz_edit.png" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Remove">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lb_RemoveNominee" runat="server" OnClick="lb_RemoveNominee_OnClick"><img src="../Assets/img/delete.png" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>

                                <br />
                                <div class="form-row">
                                    <%--  <div>--%>
                                    <asp:HiddenField runat="server" ID="hdpk" />
                                    <link href="ButtonGrups.css" rel="stylesheet" />
                                    <div class="col-md-8">
                                        <div class="ui-group-buttons">

                                            <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" OnClientClick="return confirm('Are you sure you want to Save ?')" Text="  Save  " CssClass="btn btn-sm btn-info" />
                                            <div class="or or-sm"></div>
                                            <asp:Button runat="server" ID="btnNext" OnClick="btn_Next_OnClick" OnClientClick="return confirm('Are you sure you want to Save & Next ?')" Text="   Save & Next   " CssClass="btn btn-sm btn-success" />

                                            <div class="or or-sm"></div>
                                            <asp:Button ID="cancelButton" Text="  Exit  " OnClientClick="return confirm('Are you sure you want to Exit ?')" OnClick="cancelButton_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-2">
                                        <asp:LinkButton CssClass="hh previous" OnClick="lbPrevious_OnClick" ID="lbPrevious" runat="server">&laquo; Previous</asp:LinkButton>
                                        <asp:LinkButton CssClass="hh next" runat="server" ID="lblNext" OnClick="lblNext_OnClick">Next &raquo;</asp:LinkButton>

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
                                    <%-- </div>--%>
                                </div>
                          
                <%-- </ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function showpreview(input) {


            debugger;
            var validExtensions = ['jpg','png','jpeg','JPG','JPEG','PNG']; //array of valid extensions
            var fileName = input.files[0].name;
            var fileNameExt = fileName.substr(fileName.lastIndexOf('.') + 1);
            if ($.inArray(fileNameExt, validExtensions) == -1) {
                input.type = ''
                input.type = 'file'
                $('#imgpreview').attr('src', "");
                alert("Only these file types are accepted : " + validExtensions.join(', '));
            }
            else {
                if (input.files && input.files[0]) {
                    var filerdr = new FileReader();
                    filerdr.onload = function (e) {
                        $('#imgpreview').css('visibility', 'visible');
                              $('#imgpreview').attr('src', e.target.result);
                    }
                    filerdr.readAsDataURL(input.files[0]);
                }
            }

            //if (input.files && input.files[0]) {

            //    var reader = new FileReader();
            //    reader.onload = function (e) {
            //        $('#imgpreview').css('visibility', 'visible');
            //        $('#imgpreview').attr('src', e.target.result);
            //    }
            //    reader.readAsDataURL(input.files[0]);
            //}

        }

    </script>
</asp:Content>


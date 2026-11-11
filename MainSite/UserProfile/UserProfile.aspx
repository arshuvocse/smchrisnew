<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="UserProfile.aspx.cs" Inherits="UserProfile_UserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>




    <link href="UserProfileShadow.css" rel="stylesheet" />

       <style>
                    .imgshadow {
                      -webkit-box-shadow: 5px 5px 15px 5px #000000; 
box-shadow: 5px 5px 15px 5px #000000;
                    }
                     
                                                                                                 
                </style>
    <style>
        #cpFormBody_ExtraCurriculamGridView > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }

        #cpFormBody_ExtraCurriculamGridView > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }


        #cpFormBody_OtherTalentsGridView > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }

        #cpFormBody_OtherTalentsGridView > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }




        #cpFormBody_AchievementsGridView > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }

        #cpFormBody_AchievementsGridView > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }



        #cpFormBody_HobbyGridView > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }

        #cpFormBody_HobbyGridView > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }




        #cpFormBody_gv_Nominee > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }

        #cpFormBody_gv_Nominee > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }



        #cpFormBody_gv_Reference > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }

        #cpFormBody_gv_Reference > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }



        #cpFormBody_gv_Training > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }

        #cpFormBody_gv_Training > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }



        #cpFormBody_gv_Experience > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }

        #cpFormBody_gv_Experience > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }



        #cpFormBody_gv_Education > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }

        #cpFormBody_gv_Education > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }


        #cpFormBody_gv_Children > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }

        #cpFormBody_gv_Children > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }
    </style>
    <div class="col-md-12" style="margin-top: 10px">
        <div class="row">
            <div class="col-md-3">

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
                            <asp:Label runat="server" ID="lblshortName" />
                        </div>
                        <div class="profile-usertitle-job">
                            <asp:Label runat="server" ID="lblDesignation" />
                        </div>
                    </div>
                    <div class="profile-userbuttons">
                        <%--<asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>--%>
                        <asp:Button ID="btnPrintCv" runat="server" OnClick="btnPrintCv_Click" CssClass="btn btn-info  btn-sm" Text="Print CV" />
                        <%--</ContentTemplate>
                             </asp:UpdatePanel>--%>
                    </div>
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
            <div class="col-md-9">
                <div class="portlet light bordered" style="box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);">
                    <div class="portlet-title tabbable-line">
                        <div class="caption caption-md">
                            <i class="icon-globe theme-font hide"></i>
                            <span class="caption-subject font-blue-madison bold uppercase">Your info</span>
                        </div>
                    </div>
                    <div class="portlet-body">
                        <div>

                            <!-- Nav tabs -->
                            <ul class="nav nav-tabs" role="tablist">
                                <li role="presentation" style="padding: 5px;" class="active"><a href="#GeneralInformation" aria-controls="GeneralInformation" role="tab" data-toggle="tab">General Information</a></li>
                                <li role="presentation" style="padding: 5px"><a href="#EmploymentInformation" aria-controls="EmploymentInformation" role="tab" data-toggle="tab">Employment Information</a></li>
                                <li role="presentation" style="padding: 5px"><a href="#Contacts" aria-controls="Contacts" role="tab" data-toggle="tab">Contacts</a></li>
                                <li role="presentation" style="padding: 5px"><a href="#FamilyInformation" aria-controls="FamilyInformation" role="tab" data-toggle="tab">Family Information</a></li>
                                <li role="presentation" style="padding: 5px"><a href="#Education" aria-controls="Education" role="tab" data-toggle="tab">Education</a></li>
                                <li role="presentation" style="padding: 5px"><a href="#Experience" aria-controls="Experience" role="tab" data-toggle="tab">Experience</a></li>
                                <li role="presentation" style="padding: 5px"><a href="#Training" aria-controls="Training" role="tab" data-toggle="tab">Training</a></li>
                                <li role="presentation" style="padding: 5px"><a href="#Reference" aria-controls="Reference" role="tab" data-toggle="tab">Reference</a></li>
                                <li role="presentation" style="padding: 5px"><a href="#Nominee" aria-controls="Nominee" role="tab" data-toggle="tab">Nominee</a></li>
                                <li role="presentation" style="padding: 5px"><a href="#Others" aria-controls="Others" role="tab" data-toggle="tab">Others</a></li>
                            </ul>

                            <!-- Tab panes -->
                            <div class="tab-content">
                                <br />
                                <div role="tabpanel" class="tab-pane active" id="GeneralInformation">
                                    <form>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label for="inputName" class="font-weight-bold">Employee Name: &nbsp;</label>
                                                    <asp:Label runat="server" ID="lblEmpName"></asp:Label>
                                                </div>
                                                <div class="form-group">
                                                    <label for="exampleInputEmail1" class="font-weight-bold">Gender: &nbsp; </label>
                                                    <asp:Label runat="server" ID="lblGender"></asp:Label>
                                                </div>

                                                <div class="form-group">
                                                    <label for="exampleInputEmail1" class="font-weight-bold">Blood Group: &nbsp;</label>
                                                    <asp:Label runat="server" ID="lblBloodGroup"></asp:Label>
                                                </div>

                                                <div class="form-group">
                                                    <label for="exampleInputEmail1" class="font-weight-bold">Tin No.: &nbsp;</label>
                                                    <asp:Label runat="server" ID="lblTinNo"></asp:Label>
                                                </div>


                                                <div class="form-group">
                                                    <label for="exampleInputEmail1" class="font-weight-bold">Father's Name: &nbsp;</label>
                                                    <asp:Label runat="server" ID="lblFatherName"></asp:Label>
                                                </div>

                                                <div class="form-group">
                                                    <label for="exampleInputEmail1" class="font-weight-bold">Father's Occupation: &nbsp;</label>
                                                    <asp:Label runat="server" ID="lblFatherOccupation"></asp:Label>
                                                </div>

                                                <div class="form-group">
                                                    <label for="exampleInputEmail1" class="font-weight-bold">Mother's Name: &nbsp;</label>
                                                    <asp:Label runat="server" ID="lblMotherName"></asp:Label>
                                                </div>

                                                <div class="form-group">
                                                    <label for="exampleInputEmail1" class="font-weight-bold">Mother's Occupation: &nbsp;</label>
                                                    <asp:Label runat="server" ID="lblMotherOccupation"></asp:Label>
                                                </div>

                                                <div class="form-group">
                                                    <label for="exampleInputEmail1" class="font-weight-bold">Date of Birth: &nbsp;</label>
                                                    <asp:Label runat="server" ID="lblDateofBirth"></asp:Label>
                                                </div>
                                            </div>

                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label for="exampleInputEmail1" class="font-weight-bold">Date of Join: &nbsp;</label>
                                                    <asp:Label runat="server" ID="lblDateofJoin"></asp:Label>
                                                </div>

                                                <div class="form-group">
                                                    <label for="exampleInputEmail1" class="font-weight-bold">Religion: &nbsp;</label>
                                                    <asp:Label runat="server" ID="lblReligion"></asp:Label>
                                                </div>


                                                <div class="form-group">
                                                    <label for="exampleInputEmail1" class="font-weight-bold">Marital Status: &nbsp;</label>
                                                    <asp:Label runat="server" ID="lblMaritalStatus"></asp:Label>
                                                </div>



                                                <div class="form-group">
                                                    <label for="exampleInputEmail1" class="font-weight-bold" accesskey>Employee Type: &nbsp;</label>
                                                    <asp:Label runat="server" ID="lblEmployeeType"></asp:Label>
                                                </div>

                                                <div class="form-group">
                                                    <label for="exampleInputEmail1" class="font-weight-bold">Place Of Birth: &nbsp;</label>
                                                    <asp:Label runat="server" ID="lblPlaceOfBirth"></asp:Label>
                                                </div>

                                                <div class="form-group">
                                                    <label for="exampleInputEmail1" class="font-weight-bold">Nationality: &nbsp;</label>
                                                    <asp:Label runat="server" ID="lblNationality"></asp:Label>
                                                </div>

                                                <div class="form-group">
                                                    <label for="exampleInputEmail1" class="font-weight-bold">National ID: &nbsp;</label>
                                                    <asp:Label runat="server" ID="lblNationalID"></asp:Label>
                                                </div>

                                                <div class="form-group">
                                                    <label for="exampleInputEmail1" class="font-weight-bold">Passport No: &nbsp;</label>
                                                    <asp:Label runat="server" ID="lblPassportNo"></asp:Label>
                                                </div>
                                            </div>

                                            <div class=" col-md-4">
                                                <div class="form-group" runat="server" Visible="False">
                                                    <label for="exampleInputEmail1" class="font-weight-bold">Expected Service length(Y): &nbsp;</label>
                                                    <asp:Label runat="server" ID="lblExpectedServiclength"></asp:Label>
                                                </div>

                                                <div class="form-group">
                                                    <label for="exampleInputEmail1" class="font-weight-bold">Date of Retirement: &nbsp;</label>
                                                    <asp:Label runat="server" ID="lblDateofRetirement"></asp:Label>
                                                </div>

                                                <div class="form-group">
                                                    <label for="exampleInputEmail1" class="font-weight-bold">Date of Conformation: &nbsp;</label>
                                                    <asp:Label runat="server" ID="lblDateofConformation"></asp:Label>
                                                </div>

                                                <div class="form-group">
                                                    <label for="exampleInputEmail1" class="font-weight-bold">Conformation Status: &nbsp;</label>
                                                    <asp:Label runat="server" ID="lblConformationStatus"></asp:Label>
                                                </div>

                                                <div class="form-group">
                                                    <label for="exampleInputEmail1" class="font-weight-bold">Reporting Boss: &nbsp;</label>
                                                    <asp:Label runat="server" ID="lblReportingBoss"></asp:Label>
                                                </div>

                                                <div class="form-group" runat="server" visible="False">
                                                    <label for="exampleInputEmail1" class="font-weight-bold">Reporting Boss's Designation: &nbsp;</label>
                                                    <asp:Label runat="server" ID="lblReportingBossDesignation"></asp:Label>
                                                </div>

                                                <div class="form-group">
                                                    <label for="exampleInputEmail1" class="font-weight-bold">
                                                        Is Probationary: &nbsp;</label>
                                                    <asp:Label runat="server" ID="lblIsProbationary"></asp:Label>
                                                </div>

                                                <div class="form-group">
                                                    <label for="exampleInputEmail1" class="font-weight-bold">Probationary End Date: &nbsp;</label>
                                                    <asp:Label runat="server" ID="lblProbationaryEndDate"></asp:Label>
                                                </div>
                                            </div>




                                        </div>






                                    </form>
                                </div>
                                <div role="tabpanel" class="tab-pane" id="EmploymentInformation">

                                    <form>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label for="inputName" class="font-weight-bold">Company: &nbsp;</label>
                                                    <asp:Label runat="server" ID="lblCompany"></asp:Label>
                                                </div>
                                                <div class="form-group">
                                                    <label for="exampleInputEmail1" class="font-weight-bold">Division: &nbsp;</label>
                                                    <asp:Label runat="server" ID="lblDivision"></asp:Label>
                                                </div>

                                                <div class="form-group">
                                                    <label for="exampleInputEmail1" class="font-weight-bold">Wing: &nbsp;</label>
                                                    <asp:Label runat="server" ID="lblWing"></asp:Label>
                                                </div>

                                                <div class="form-group">
                                                    <label for="exampleInputEmail1" class="font-weight-bold">Department: &nbsp;</label>
                                                    <asp:Label runat="server" ID="lblDepartment"></asp:Label>
                                                </div>

                                                <div class="form-group">
                                                    <label for="exampleInputEmail1" class="font-weight-bold">Salary Location: &nbsp;</label>
                                                    <asp:Label runat="server" ID="lblSalaryLocation"></asp:Label>
                                                </div>



                                            </div>

                                            <div class=" col-md-4">
                                                <div class="form-group">
                                                    <label for="inputName" class="font-weight-bold">Section: &nbsp;</label>
                                                    <asp:Label runat="server" ID="lblSection"></asp:Label>
                                                </div>
                                                <div class="form-group">
                                                    <label for="exampleInputEmail1" class="font-weight-bold">Sub Section: &nbsp;</label>
                                                    <asp:Label runat="server" ID="lblSubSection"></asp:Label>
                                                </div>

                                                <div class="form-group">
                                                    <label for="exampleInputEmail1" class="font-weight-bold">Employee Category: &nbsp;</label>
                                                    <asp:Label runat="server" ID="lblEmployeeCategory"></asp:Label>
                                                </div>

                                                <div class="form-group">
                                                    <label for="exampleInputEmail1" class="font-weight-bold">Salary Grade: &nbsp;</label>
                                                    <asp:Label runat="server" ID="lblSalaryGrade"></asp:Label>
                                                </div>




                                            </div>


                                            <div class=" col-md-4">
                                                <div class="form-group">
                                                    <label for="inputName" class="font-weight-bold">Salary Step: &nbsp;</label>
                                                    <asp:Label runat="server" ID="lblSalaryStep"></asp:Label>
                                                </div>
                                                <div class="form-group">
                                                    <label for="exampleInputEmail1" class="font-weight-bold">Designation: &nbsp;</label>
                                                    <asp:Label runat="server" ID="lblDesignationE"></asp:Label>
                                                </div>

                                                <div class="form-group">
                                                    <label for="exampleInputEmail1" class="font-weight-bold">Designation Type : &nbsp;</label>
                                                    <asp:Label runat="server" ID="lblDesignationType"></asp:Label>
                                                </div>

                                                <div class="form-group">
                                                    <label for="exampleInputEmail1" class="font-weight-bold">Job Location: &nbsp;</label>
                                                    <asp:Label runat="server" ID="lblJobLocation"></asp:Label>
                                                </div>




                                            </div>

                                        </div>


                                    </form>
                                </div>
                                <div role="tabpanel" class="tab-pane" id="Contacts">

                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="inputName" class="font-weight-bold">Present Address: &nbsp;</label>
                                                <asp:Label runat="server" ID="lblPresentAddress"></asp:Label>
                                            </div>
                                            <div class="form-group">
                                                <label for="exampleInputEmail1" class="font-weight-bold">Present Division: &nbsp;</label>
                                                <asp:Label runat="server" ID="lblPresentDivision"></asp:Label>
                                            </div>

                                            <div class="form-group">
                                                <label for="exampleInputEmail1" class="font-weight-bold">Present District: &nbsp;</label>
                                                <asp:Label runat="server" ID="lblPresentDistrict"></asp:Label>
                                            </div>

                                            <div class="form-group">
                                                <label for="exampleInputEmail1" class="font-weight-bold">Present Thana: &nbsp;</label>
                                                <asp:Label runat="server" ID="lblPresentThana"></asp:Label>
                                            </div>

                                            <div class="form-group">
                                                <label for="exampleInputEmail1" class="font-weight-bold">Present Tel. No: &nbsp;</label>
                                                <asp:Label runat="server" ID="lblPresentTelNo"></asp:Label>
                                            </div>
                                            <div class="form-group">
                                                <label for="exampleInputEmail1" class="font-weight-bold">Parmanent Address: &nbsp;</label>
                                                <asp:Label runat="server" ID="lblParmanentAddress"></asp:Label>
                                            </div>


                                        </div>

                                        <div class=" col-md-4">
                                            <div class="form-group">
                                                <label for="inputName" class="font-weight-bold">Parmanent Division: &nbsp;</label>
                                                <asp:Label runat="server" ID="lblParmanentDivision"></asp:Label>
                                            </div>
                                            <div class="form-group">
                                                <label for="exampleInputEmail1" class="font-weight-bold">Parmanent District: &nbsp;</label>
                                                <asp:Label runat="server" ID="lblParmanentDistrict"></asp:Label>
                                            </div>

                                            <div class="form-group">
                                                <label for="exampleInputEmail1" class="font-weight-bold">Parmanent Thana: &nbsp;</label>
                                                <asp:Label runat="server" ID="lblParmanentThana"></asp:Label>
                                            </div>

                                            <div class="form-group">
                                                <label for="exampleInputEmail1" class="font-weight-bold">Parmanent Tel. No: &nbsp;</label>
                                                <asp:Label runat="server" ID="lblParmanentTelNo"></asp:Label>
                                            </div>


                                            <div class="form-group">
                                                <label for="exampleInputEmail1" class="font-weight-bold">Personal Email: &nbsp;</label>
                                                <asp:Label runat="server" ID="lblPersonalEmail"></asp:Label>
                                            </div>

                                        </div>


                                        <div class=" col-md-4">
                                            <div class="form-group">
                                                <label for="inputName" class="font-weight-bold">
                                                    Official Email: &nbsp;</label>
                                                <asp:Label runat="server" ID="lblOfficialEmail"></asp:Label>
                                            </div>
                                            <div class="form-group">
                                                <label for="exampleInputEmail1" class="font-weight-bold">Personal Mobile: &nbsp;</label>
                                                <asp:Label runat="server" ID="lblPersonalMobile"></asp:Label>
                                            </div>

                                            <div class="form-group">
                                                <label for="exampleInputEmail1" class="font-weight-bold">Official Mobile: &nbsp;</label>
                                                <asp:Label runat="server" ID="lblOfficialMobile"></asp:Label>
                                            </div>

                                            <div class="form-group">
                                                <label for="exampleInputEmail1" class="font-weight-bold">Fax: &nbsp;</label>
                                                <asp:Label runat="server" ID="lblFax"></asp:Label>
                                            </div>



                                        </div>

                                    </div>

                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="exampleInputEmail1" class="font-weight-bold">Emergency Contact Person: &nbsp;</label>
                                                <asp:Label runat="server" ID="lblEmergencyContactPerson"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="exampleInputEmail1" class="font-weight-bold">Emergency Contact Address: &nbsp;</label>
                                                <asp:Label runat="server" ID="lblEmergencyContactAddress"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="exampleInputEmail1" class="font-weight-bold">Emergency Number: &nbsp;</label>
                                                <asp:Label runat="server" ID="lblEmergencyNumber"></asp:Label>
                                            </div>

                                        </div>
                                    </div>

                                </div>
                                <div role="tabpanel" class="tab-pane" id="FamilyInformation">
                                    <div class="row">
                                        <div class="col-md-8">
                                            <asp:GridView Width="100%" ID="gv_Spouse" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">
                                                <Columns>
                                                    <asp:BoundField DataField="SName" HeaderText="Spouse Name" />
                                                    <asp:BoundField DataField="SpouseMaxEducationName" HeaderText="Spouse's Max Education" />
                                                    <asp:BoundField DataField="SpouseOccupationName" HeaderText="Spouse's Occupation" />
                                                    <asp:BoundField DataField="SDob" HeaderText="Spouse's DOB" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="exampleInputEmail1" class="font-weight-bold">Marriage Date: &nbsp;</label>
                                                <asp:Label runat="server" ID="lblMarriageDate"></asp:Label>
                                            </div>



                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:GridView Width="100%" ID="gv_Children" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL#" Visible="False">
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

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Children DOB">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_ChildrenDOB" runat="server" DataFormatString="{0:dd-MMM-yyyy}" Text='<%#Eval("ChildrenDOB") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Children MaritalStatus">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_ChildrenMaritalStatus" runat="server" Text='<%#Eval("ChildrenMaritalStatus") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>




                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <div role="tabpanel" class="tab-pane" id="Education">
                                    <div style="overflow: scroll;">
                                        <asp:GridView Width="100%" ID="gv_Education" runat="server" ShowFooter="true" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#" Visible="False">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                        <asp:HiddenField ID="EmpEducationId" runat="server" Value='<%#Eval("EmpEducationId") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Education Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_EducationName" runat="server" Text='<%#Eval("EducationName") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Board/University">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_BoardUniversity" runat="server" Text='<%#Eval("BoardUniversity") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Subject/Group">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_SubjectGroup" runat="server" Text='<%#Eval("SubjectGroup") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Educational Institute">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_EducationalInstitute" runat="server" Text='<%#Eval("EducationalInstitute") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Field Of Specialization">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_FieldOfSpecialization" runat="server" Text='<%#Eval("FieldOfSpecialization") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Passing Year">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_PassingYear" runat="server" Text='<%#Eval("PassingYear") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Result">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Result" runat="server" Text='<%#Eval("Result") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="CGPA Or Total Marks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_CgpaOrTotalMarks" runat="server" Text='<%#Eval("CgpaOrTotalMarks") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Is Last Level">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_EduIsLastLevel" runat="server" Text='<%#Eval("EduIsLastLevel") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div role="tabpanel" class="tab-pane" id="Experience">

                                    <div style="overflow: scroll;">
                                        <asp:GridView Width="100%" ID="gv_Experience" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#" Visible="False">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                        <asp:HiddenField ID="EmpExperienceId" runat="server" Value='<%#Eval("EmpExperienceId") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Company/Institute">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_ExpCompany" runat="server" Text='<%#Eval("ExpCompany") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Contact Person">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_ExpContactPerson" runat="server" Text='<%#Eval("ExpContactPerson") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Address">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_ExpAddress" runat="server" Text='<%#Eval("ExpAddress") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nature of Business">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_ExpNatureofBusiness" runat="server" Text='<%#Eval("ExpNatureofBusiness") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Job Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_ExpJobType" runat="server" Text='<%#Eval("ExpJobType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Leaving Salary">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_ExpLeavingSalary" runat="server" Text='<%#Eval("ExpLeavingSalary") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="From Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_ExpFromDate" runat="server" DataFormatString="{0:dd-MMM-yyyy}" Text='<%#Eval("ExpFromDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_ExpToDate" runat="server" DataFormatString="{0:dd-MMM-yyyy}" Text='<%#Eval("ExpToDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Is Last Job">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_ExpLastJob" runat="server" Text='<%#Eval("ExpLastJob") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Designation">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_ExpDesignation" runat="server" Text='<%#Eval("ExpDesignation") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Job Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_ExpJobDescription" runat="server" Text='<%#Eval("ExpJobDescription") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tel No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_ExpTelNo" runat="server" Text='<%#Eval("ExpTelNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_ExpRemarks" runat="server" Text='<%#Eval("ExpRemarks") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>




                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                </div>
                                <div role="tabpanel" class="tab-pane" id="Training">
                                    <div style="overflow: scroll;">
                                        <asp:GridView Width="100%" ID="gv_Training" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#" Visible="False">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                       
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Training Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_TrainingName" runat="server" Text='<%#Eval("TrainingName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Training Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_TrainingType" runat="server" Text='<%#Eval("TrainingTypeName") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Training Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_TrainingDescription" runat="server" Text='<%#Eval("TrainingDescription")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Training Institution">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_TrainingInstitution" runat="server" Text='<%#Eval("TrainingInstitutionName")%>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Training Place">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_TrainingPlace" runat="server" Text='<%#Eval("TrainingPlace")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Training Country">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_TrainingCountry" runat="server" Text='<%#Eval("TrainingCountryName")%>'></asp:Label>
                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Training Achievment">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_TrainingAchievment" runat="server" Text='<%#Eval("TrainingAchievment")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="From Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_TrFromDate" runat="server" DataFormatString="{0:dd-MMM-yyyy}" Text='<%#Eval("TrFromDate")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_TrToDate" runat="server" DataFormatString="{0:dd-MMM-yyyy}" Text='<%#Eval("TrToDate")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Training Days">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_TrainingDays" runat="server" Text='<%#Eval("TrainingDays")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_TrRemarks" runat="server" Text='<%#Eval("TrRemarks")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>





                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div role="tabpanel" class="tab-pane" id="Reference">
                                    <div style="overflow: scroll;">
                                        <asp:GridView Width="100%" ID="gv_Reference" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#" Visible="False">
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




                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div role="tabpanel" class="tab-pane" id="Nominee">
                                    <div style="overflow: scroll;">
                                        <asp:GridView Width="100%" ID="gv_Nominee" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#" Visible="False">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                        <asp:HiddenField ID="EmpNomineeId" runat="server" Value='<%#Eval("EmpNomineeId")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                   <asp:TemplateField HeaderText="Image">

                                                    <ItemTemplate>

                                                       

                                                        <asp:Image ID="Image1rr" runat="server" CssClass="imgshadow"  Width="100" Height="100" ImageUrl='<%# Eval("ShowNominationImage") %>' />
                                                    </ItemTemplate>


                                                   
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Nomination Purpose">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_NominationPurpose" runat="server" Text='<%#Eval("NominationPurposeName")%>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nominee Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_NomineeName" runat="server" Text='<%#Eval("NomineeName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date Of Nomination">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_DateOfNomination" DataFormatString="{0:dd-MMM-yyyy}" runat="server" Text='<%#Eval("DateOfNomination")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nomination Percentage">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_NominationPercentage" runat="server" Text='<%#Eval("NominationPercentage")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nominee DOB">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_NomineeDOB" runat="server" Text='<%#Eval("NomineeDOB")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Nominee Relation">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_NomineeRelation" runat="server" Text='<%#Eval("NomineeRelationName")%>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Nominee Occupation">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_NomineeOccupation" runat="server" Text='<%#Eval("NomineeOccupationName")%>'></asp:Label>

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




                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div role="tabpanel" class="tab-pane" id="Others">

                                    <div class="row">
                                        <div class="col-md-3">
                                            <div style="overflow: scroll;">
                                                <asp:GridView Width="100%" ID="ExtraCurriculamGridView" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL#" Visible="False">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex + 1%>
                                                                <asp:HiddenField ID="EmpExtraCurriculamId" runat="server" Value='<%#Eval("EmpExtraCurriculamId")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Extra Curriculam Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ExtraCurriculamName" runat="server" Text='<%#Eval("ExtraCurriculamName")%>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>





                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div style="overflow: scroll;">
                                                <asp:GridView Width="100%" ID="OtherTalentsGridView" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL#" Visible="False">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex + 1%>
                                                                <asp:HiddenField ID="EmpOtherTalentsIdId" runat="server" Value='<%#Eval("EmpOtherTalentsId")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Other Talents Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_OtherTalentsName" runat="server" Text='<%#Eval("OtherTalentsName")%>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>





                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div style="overflow: scroll;">
                                                <asp:GridView Width="100%" ID="AchievementsGridView" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL#" Visible="False">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex + 1%>
                                                                <asp:HiddenField ID="EmpAchievementsIdId" runat="server" Value='<%#Eval("EmpAchievementsId")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Achievements Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_AchievementsName" runat="server" Text='<%#Eval("AchievementsName")%>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>





                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div style="overflow: scroll;">
                                                <asp:GridView Width="100%" ID="HobbyGridView" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL#" Visible="False">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex + 1%>
                                                                <asp:HiddenField ID="EmpHobbyIdId" runat="server" Value='<%#Eval("EmpHobbyId")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Hobby Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_HobbyName" runat="server" Text='<%#Eval("HobbyName")%>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>





                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>


                    </div>

                </div>
            </div>
        </div>
    </div>

</asp:Content>


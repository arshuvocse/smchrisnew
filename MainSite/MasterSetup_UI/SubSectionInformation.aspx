<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="SubSectionInformation.aspx.cs" Inherits="MasterSetup_UI_SubSectionInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .checkbox label {
            display: inline;
        }

        .margin-right {
            margin-right: 5px;
        }

           .resize {
            resize: none;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">


    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                        <div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Sub Section Information  </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                          <asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="HomeButton_OnClick" />
                        <asp:Button ID="detailsViewButton" Text="View List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>

                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">


                                

                            <div class="row">
                          
                                <div class="col-md-3" runat="server" Visible="True" id="mainid">
                                    <div class="form-group">
                                        <label>Company Name </label>
                                        <label style="color: #a52a2a">*</label>
                                        <asp:DropDownList ID="companyDropDownList" class="form-control form-control-sm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="companyDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>Division Name </label>
                                        <label style="color: #a52a2a">*</label>
                                        <asp:HiddenField ID="subSectionIdHiddenField" runat="server" />
                                        <asp:DropDownList ID="divisionDropDownList" class="form-control form-control-sm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="divisionDropDownList_OnSelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="form-group" runat="server" Visible="False" id="wing">
                                        <label>Wing</label>
                                        <label style="color: #a52a2a">*</label>
                                        <asp:HiddenField ID="wingHiddenField" runat="server" />
                                        <asp:DropDownList ID="divisionWingDropDownList" class="form-control form-control-sm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="divisionWingDropDownList_OnSelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="form-group" runat="server" Visible="False" id="dept">
                                        <label>Department Name </label>
                                        <label style="color: #a52a2a">*</label>
                                        <asp:HiddenField ID="deptHiddenField" runat="server" />
                                        <asp:DropDownList ID="departmentNameDropdownList" runat="server" AutoPostBack="True" OnTextChanged="departmentNameDropdownList_OnTextChanged" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>

                                    <div class="form-group" runat="server" Visible="False" id="sec">
                                        <label>Section Name </label>
                                        <label style="color: #a52a2a">*</label>
                                        <asp:HiddenField ID="secHiddenField" runat="server" />
                                        <asp:DropDownList ID="sectionDropDownList" runat="server" AutoPostBack="True" class="form-control form-control-sm" OnSelectedIndexChanged="sectionDropDownList_OnSelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                     <div class="form-group">
                                            <label> Root </label><label style="color: #a52a2a">*</label>
                                             <asp:RadioButtonList ID="rootRadioButtonList" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rootRadioButtonList_SelectedIndexChanged">
                                                 <asp:ListItem>Division</asp:ListItem>
                                                 <asp:ListItem>Wing</asp:ListItem>
                                                 <asp:ListItem>Department</asp:ListItem>
                                                 <asp:ListItem>Section</asp:ListItem>

                                             </asp:RadioButtonList>
                                        
                                        </div>
                                     <div class="form-group">
                                        <label>Sub-Section Name </label>
                                        <label style="color: #a52a2a">*</label>
                                        <asp:TextBox ID="subSectionTextBox" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                    </div>


                                </div>
                                <div class="col-md-3">
                                   
                                    <div class="form-group">
                                        <label>Short Name </label>
                                        <label style="color: #a52a2a">*</label>
                                        <asp:TextBox ID="shortNameTextBox" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Description </label>
                                        <asp:TextBox ID="descriptionTexBox" runat="server" class="form-control resize" Rows="1" Columns="20"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Remarks </label>
                                        <asp:TextBox ID="remarksTextBox" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                    </div>

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
                                <div class="col-md-4">
                                </div>
                            </div>



                            <div class="row">
                             
                                <div class="col-md-3">

                                    <div class="form-group">
                                        <asp:Button ID="submitButton" Text="Save" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="submitButton_Click" />
                                           <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                        <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />
                                         <asp:Button ID="cancelButton" Text="Cancel" CssClass="btn btn-sm warning" runat="server" Visible="False" OnClick="cancelButton_OnClick" BackColor="#FFCC00" />
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


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="ProjectSetup.aspx.cs" Inherits="MasterSetup_UI_ProjectSetup" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
         <script>
             function MutExChkList(chk) {
                 var chkList = chk.parentNode.parentNode.parentNode;
                 var chks = chkList.getElementsByTagName("input");
                 for (var i = 0; i < chks.length; i++) {
                     if (chks[i] != chk && chk.checked) {
                         chks[i].checked = false;
                     }
                 }
             }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <style>
          .chkChoiceStep label {
            padding-left: 3px;
            padding-right: 10px;
        }

    </style>
    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                       
                        <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png" width="20px"  /> Project Setup Information</h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                          <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                        <asp:LinkButton ID="detailsViewButton"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                    </div>
                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">

                            <form>
                                <div class="row">

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Company </label>
                                            <label style="color: #a52a2a">*</label>


                                            <asp:DropDownList ID="CompanyDropDown" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                        </div>

                                        <div class="form-group">
                                            <label>Project Name </label>
                                            <label style="color: #a52a2a">*</label>


                                            <asp:HiddenField ID="projectSetupIdHiddenField" runat="server" />
                                            <asp:TextBox ID="projectNameTextBox" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                        </div>

                                        <div class="form-group">
                                            <label>Project Start Date  </label>
                                            <label style="color: #a52a2a">*</label>

                                            <asp:TextBox ID="projectStartDate" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender" runat="server" PopupPosition="TopLeft"
                                                Format="dd-MMM-yyyy" PopupButtonID="ImageButtosn1" CssClass="MyCalendar"
                                                TargetControlID="projectStartDate" />
                                        </div>

                                        <div class="form-group">
                                            <br />


                                            <div class="checkbox">

                                                <label class="btn btn-default">
                                                    <asp:CheckBox ID="isActiveCheckBox" Checked="True" CssClass="checkbox margin-right" runat="server" />
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
                                            <label>Project Type  </label>
                                            <label style="color: #a52a2a">*</label>




                                            <asp:RadioButtonList ID="projectNameRadioButtonList" runat="server" Font-Size="Large"
                                                OnSelectedIndexChanged="projectNameRadioButtonList_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="parmanent">Parmanent</asp:ListItem>
                                                <asp:ListItem Value="temporary">Temporary</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>



                                        <div id="projectDate" runat="server" visible="False">

                                            <div class="form-group">
                                                <label>Project End Date  </label>
                                                <label style="color: #a52a2a">*</label>





                                                <asp:TextBox ID="projectEndDate" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="TopLeft"
                                                    Format="dd-MMM-yyyy" PopupButtonID="ImageButtosn1" CssClass="MyCalendar"
                                                    TargetControlID="projectEndDate" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label>Description  </label>



                                            <asp:TextBox ID="projectDescriptionTextBox" runat="server" CssClass="form-control resize" Rows="1" Columns="20" TextMode="MultiLine"></asp:TextBox>
                                        </div>


                                        <div class="form-group">
                                            <label>Remark  </label>
                                            <asp:TextBox ID="remarksTextBox" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                        </div>



                                    </div>
                                    
                                    <div class="col-md-3">
                                        <div class="form-group">
                                                 <asp:CheckBox runat="server"  onclick="MutExChkList(this);"  ID="chkIsOtherProject"   Text="Doner Project" CssClass="chkChoiceStep" /><br/>
                                                     <asp:CheckBox runat="server" onclick="MutExChkList(this);" ID="FundedProjectsCheckBox1" Text="SMC Funded Projects" CssClass="chkChoiceStep" />
                                                    <br/>
                                                     <asp:CheckBox Visible="False" runat="server" onclick="MutExChkList(this);" ID="chkSmcContract" Text="  SMC Contract  " CssClass="chkChoiceStep"/>
                                                    
                                                     <br/>
                                                     <asp:CheckBox  Visible="False"  runat="server" onclick="MutExChkList(this);" ID="chkIsCompanyDirector" Text="  Company Director  " CssClass="chkChoiceStep" />
                                            </div>
                                            </div>

                                </div>

                                <div class="row">
                                    <div class="form-group">

                                        <asp:Button ID="btnSave" Text="Save" CssClass="btn btn-info btn-sm" Visible="False" runat="server" OnClick="submitButton_Click" />
                                        <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                        <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />
                                        <asp:Button ID="btnCancel" Text="Cancel" CssClass="btn btn-warning btn-sm" Visible="False" runat="server" OnClick="cancelButton_OnClick" />

                                    </div>
                                </div>

                            </form>
                        </div>

                    </div>
                </label>







            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="SurveyQuestionSetupEntry.aspx.cs" Inherits="Survey_SurveyQuestionSetupEntry" %>

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
                        
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;"> <img src="../Report_Pages/app.png"  width="20px" />  Survey Question Setup Information  </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                           <asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="HomeButton_OnClick" />
                        <asp:Button ID="detailsViewButton" Text="Go Back" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>

                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">

                            <form>
                            <div class="row">
                                
                                <div class="col-md-3">
                                     
 <asp:HiddenField ID="VacancyIdHiddenField" runat="server" />
                                    <div class="form-group">
                                        <label>Question Title </label>
                                        <label style="color: #a52a2a">*</label>
                                         
                                       
                                        <asp:TextBox ID="VacancyNameTextBox" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                    
                                     <div class="form-group">
                                        <label>Survey Question Group Name </label>
                                        <label style="color: #a52a2a">*</label>
                                         
                                       
                                        <asp:DropDownList ID="SurveyQuestionGroupDropDownList" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                    
                                     <div class="form-group">
                                        <label>Survey Question Type Name </label>
                                        <label style="color: #a52a2a">*</label>
                                         
                                       
                                        <asp:DropDownList ID="SurveyQuestionTypeDropDownList" AutoPostBack="True" OnSelectedIndexChanged="SurveyQuestionTypeDropDownList_OnSelectedIndexChanged" runat="server" class="form-control form-control-sm"></asp:DropDownList>
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
                                
                                <div class="col-md-9" runat="server" id="ShowDiv" Visible="False">
                                     <div class="row">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Answer List</label>
                                                    <asp:TextBox ID="othersTextBox" CssClass="form-control  form-control-sm" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12" style="margin-top: 18px">
                                                      <div class="form-group">
                                                    <asp:Button ID="OtherRequirementsAddButton" Text="Add To List" OnClick="OtherRequirementsAddButton_OnClick" CssClass="btn btn-outline-success btn-block disabled btn-sm"  runat="server" />
                                              
                                                          </div>  </div>

                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <asp:GridView ID="OtherRequirementsGridView" runat="server" Width="100%" CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="False">
                                                        <Columns>
                                                            <%--<asp:BoundField DataField="SizeId" HeaderText="Size Id" Visible="False" HtmlEncode="False" />--%>
                                                            <asp:BoundField DataField="OtherRequirementsName" HeaderText="Answer" HtmlEncode="False" />

                                                            <asp:TemplateField HeaderText="Edit">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="addeduImageButton" runat="server" OnClick="editOtherRequirementsGridViewButton_OnClick"
                                                                        ImageUrl="~/Assets/img/rsz_edit.png" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="deleteImageButton" runat="server" OnClick="deleteOtherRequirementsGridViewButton_OnClick"
                                                                        ImageUrl="~/Assets/img/delete.png" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
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

                                </form>
                        </div>


                    </div>
                    </div>
                </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
            

</asp:Content>


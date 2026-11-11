<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" validateRequest="false" AutoEventWireup="true" CodeFile="MemoPrintJobCirculation.aspx.cs" Inherits="Report_Pages_MemoPrintJobCirculation" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <style>
        fieldset.for-panel {
            background-color: #fcfcfc;
            border: 1px solid #999;
            padding: 15px 10px;
            background-color: white;
            margin-bottom: 12px;
        }

            fieldset.for-panel legend {
                background-color: #fafafa;
                border: 1px solid #ddd;
                border-radius: 1px;
                font-size: 12px;
                font-weight: bold;
                line-height: 10px;
                margin: inherit;
                padding: 7px;
                width: auto;
                margin-bottom: 0;
                color: black;
            }

        #cpFormBody_gvSalaryStep > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }

        #cpFormBody_gvSalaryStep > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }




          #cpFormBody_KeyResponGridView > tbody > tr > th {
                padding: 9px 0;
                color: #fff;
                background-color: #5B799E;
                /*background-color: #98A9C0;*/
            }

            #cpFormBody_KeyResponGridView > tbody > tr:not(th):nth-child(odd) {
                background-color: #DFDFDF;
            }



              #cpFormBody_OtherRequirementsGridView > tbody > tr > th {
                padding: 9px 0;
                color: #fff;
                background-color: #5B799E;
                /*background-color: #98A9C0;*/
            }

            #cpFormBody_OtherRequirementsGridView > tbody > tr:not(th):nth-child(odd) {
                background-color: #DFDFDF;
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
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Job Circulation Letter Information </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />

                    </div>

                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">

                            <asp:HiddenField runat="server" ID="MasterIdHiddenField" />
                            <asp:HiddenField runat="server" ID="IncrementIdHiddenField" />
                            <asp:HiddenField runat="server" ID="ComName" />
                            <asp:HiddenField runat="server" ID="ComId" />
                            <asp:HiddenField runat="server" ID="HiddenFieldJobReqId" />
                            <asp:HiddenField runat="server" ID="EmpIdHiddenfield" />

                            <div class="row">

                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label>Code:</label>
                                            <asp:Label ID="lblLabelInfo" CssClass="form-control form-control-sm col-md-4" runat="server" Text=""></asp:Label>

                                        </div>

                                        <div class="col-md-6"> <asp:Label ID="lblDate" CssClass="form-control form-control-sm col-md-4 pull-right" runat="server" Text=""></asp:Label>
                                              <label class="col-md-2 pull-right" style="margin-top: 10px;">Print Date:</label>
                                           
                                        </div>
                                    </div>
                                    <br/>
                                      <div class="row">

                                <div class="col-md-12">
                                    
                                     <div class="form-group">
                                 <%--   <label>Message Body: </label>
                                    &nbsp;<label style="color: #a52a2a">*</label>--%>
                                         <FTB:FreeTextBox  ID="txtFirstBody"  Height="100" Text="" runat="server"></FTB:FreeTextBox>
                                   
                                    <%-- <textarea  id="txtMessageBody3"  Visible="False"  cols="35" rows="10" class="form-control resize" runat="server"></textarea>
                                         <FTB:FreeTextBox runat="server"></FTB:FreeTextBox>                          <textarea  id="txtMessageBody2"   Visible="False" cols="35" rows="10" class="form-control resize" runat="server"></textarea>--%>
                                </div>
                                    </div>
                                          </div>
                                    <br />
                                    
                                        
                                      <div class="row">

                                <div class="col-md-12">
                                    
                                     <div class="form-group">
                                    <%--<label>Message Body: </label>
                                    &nbsp;<label style="color: #a52a2a">*</label>--%>
                                         <FTB:FreeTextBox  ID="txtSecendBody"  Height="100" Text=" " runat="server"></FTB:FreeTextBox>
                                   
                                    <%-- <textarea  id="txtMessageBody3"  Visible="False"  cols="35" rows="10" class="form-control resize" runat="server"></textarea>
                                         <FTB:FreeTextBox runat="server"></FTB:FreeTextBox>                          <textarea  id="txtMessageBody2"   Visible="False" cols="35" rows="10" class="form-control resize" runat="server"></textarea>--%>
                                </div>
                                    </div>
                                          </div>
                                       <div class="row">

                                <div class="col-md-12">
                                    
                                     <div class="form-group">
                                    <label>Position title: </label>
                                            <asp:TextBox ID="txtPositiontitle" CssClass="form-control form-control-sm" runat="server" Text=""></asp:TextBox>
                                         </div>
                                    </div>
                                          </div>
                                    
                                      <div class="row">

                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>Responsiblities </label>
                                                                    <asp:TextBox ID="jdTextBox" CssClass="form-control form-control-sm"  runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                        
                                                         
                                                                <div class="col-md-2"  style="margin-top: 18px">
                                                                <asp:Button ID="textButton" Text="Add To List" OnClick="textButton_OnClick" CssClass="btn btn-outline-success btn-block disabled btn-sm" runat="server" />
                                                            </div>
                                                          

                                                        </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                              <div class="form-group">
                                                  <asp:HiddenField runat="server" ID="JobReqFormIdHF"/>
                                                        <asp:GridView ID="KeyResponGridView" runat="server" Width="100%" CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="False">
                                                            <Columns>
                                                                <%--<asp:BoundField DataField="SizeId" HeaderText="Size Id" Visible="False" HtmlEncode="False" />--%>
                                                                <asp:BoundField DataField="JobReqKeyResName" HeaderText="Key Responsibilite" HtmlEncode="False" />

                                                                <asp:TemplateField HeaderText="Edit">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="addeduImageButton" runat="server" OnClick="editImageButton_OnClick"
                                                                            ImageUrl="~/Assets/img/rsz_edit.png" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Delete">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="deleteImageButton" runat="server" OnClick="deleteImageButton_OnClick"
                                                                            ImageUrl="~/Assets/img/delete.png" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                        </div>
                                    </div>
                                    
                                       <div class="row">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Qualification & Experience</label>
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
                                                            <asp:BoundField DataField="OtherRequirementsName" HeaderText="Other Requirements" HtmlEncode="False" />

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
                                    
                                 
                                    
                                    
                                         <div class="row">
                                             
                                  
                                                  
                                             
                                <div class="col-md-12">
                                    
                                     <div class="form-group">
                                    <label>What We Offer: </label>
                                    &nbsp;<label style="color: #a52a2a">*</label>
                                         <FTB:FreeTextBox  ID="txtWhatWeOffer"  Height="100" Text=" " runat="server"></FTB:FreeTextBox>
                                   
                                    <%-- <textarea  id="txtMessageBody3"  Visible="False"  cols="35" rows="10" class="form-control resize" runat="server"></textarea>
                                         <FTB:FreeTextBox runat="server"></FTB:FreeTextBox>                          <textarea  id="txtMessageBody2"   Visible="False" cols="35" rows="10" class="form-control resize" runat="server"></textarea>--%>
                                </div>
                                    </div>
                                     
                                          </div>
                                    
                                    
                                           <div class="row">

                                <div class="col-md-12">
                                    
                                     <div class="form-group">
                                    <label>APPLY INSTRACTIONS: </label>
                                    &nbsp;<label style="color: #a52a2a">*</label>
                                         <FTB:FreeTextBox  ID="txtInstraction"  Height="100" Text="" runat="server"></FTB:FreeTextBox>
                                   
                                    <%-- <textarea  id="txtMessageBody3"  Visible="False"  cols="35" rows="10" class="form-control resize" runat="server"></textarea>
                                         <FTB:FreeTextBox runat="server"></FTB:FreeTextBox>                          <textarea  id="txtMessageBody2"   Visible="False" cols="35" rows="10" class="form-control resize" runat="server"></textarea>--%>
                                </div>
                                    </div>
                                          </div>
                                    
                                         <div class="row">

                                <div class="col-md-4">
                                    
                                     <div class="form-group">
                                    <label>Name & Designation: </label>
                                    &nbsp;<label style="color: #a52a2a">*</label>
                                         <FTB:FreeTextBox  ID="txtDesignationEach"  Height="100" Text="" runat="server"></FTB:FreeTextBox>
                                   <%--      <asp:TextBox  ID="txtDesignationEach"  CssClass="form-control" Height="77px" TextMode="MultiLine" Text="" runat="server" Font-Size="11pt" Width="180px"></asp:TextBox>--%>
                                   
                                    <%-- <textarea  id="txtMessageBody3"  Visible="False"  cols="35" rows="10" class="form-control resize" runat="server"></textarea>
                                         <FTB:FreeTextBox runat="server"></FTB:FreeTextBox>                          <textarea  id="txtMessageBody2"   Visible="False" cols="35" rows="10" class="form-control resize" runat="server"></textarea>--%>
                                </div>
                                    </div>
                                          </div>
                                   
                                </div>


                            </div>

                          
                            <div class="form-row">
                                <div class="form-group">
                                    <asp:Button ID="submitButton" Text="Save" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="submitButton_Click" />
                                    <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                    <asp:Button class=" btn btn-sm btn-success " Text="Print" runat="server" ForeColor="black" ID="btnPrint" BackColor="#54C3A7" OnClick="btnPrint_Click" />
                                    <br />
                                    <br />

                                    <asp:Button ID="detailsViewButton" Text="Back to List" CssClass="btn btn-sm text-info" runat="server" OnClick="detailsViewButton_OnClick" />
                                    <%--     <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />
                                            <asp:Button ID="cancelButton" Text="Cancel" CssClass="btn btn-sm warning" Visible="False" runat="server" OnClick="cancelButton_OnClick" BackColor="#FFCC00" />--%>
                                </div>
                            </div>
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

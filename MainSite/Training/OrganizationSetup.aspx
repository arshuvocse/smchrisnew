<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="OrganizationSetup.aspx.cs" Inherits="Training_OrganizationSetup" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>
<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">
       <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=40);
            opacity: 0.4;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            width: 300px;
            border: 3px solid #0DA9D0;
        }
    </style>
    
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
    </style>
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <style type="text/css">
        .form-group.required .control-label:after { 
            color: #d00;
            content: "*";
            position: absolute;
            margin-left: 4px;
            top:4px;
            font-size: large;
        }
    </style>

    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>
                <div class="container-fluid">
                     <div class="page-heading" style="background-color: #F0FFFF;font-style: italic">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;">Organization Information</h1>
                        </div>
                        <%--<div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="detailsViewButton" Text="Organization List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_Click" />
                        </div>--%>
                        
                        <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                        <asp:Button ID="ListViewButton" Visible="True" Text="Go Back" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_Click" />
                    </div>

                    </div>
                    <div class="card">
                        <div class="card-body">
                            <div class="form-row">
                                <Div class="col-md-2">
                                      <div class="form-group  required">
                                        <label class="control-label">Company Name</label>
                                        <asp:DropDownList runat="server" ID="ddlCompany" class="form-control form-control-sm "></asp:DropDownList>

                                    </div>
                                </Div>
                                </div>
     <fieldset class="for-panel" runat="server">
                                <legend>Organization Information</legend>
                              <div class="form-row">
                                
                                <div class="col-md-3">
                                   
                                    <div class="form-group  required">
                                        <label class="control-label">Organization Name</label>
                                        <asp:TextBox runat="server" ID="txt_OrganizationName" class="form-control form-control-sm "></asp:TextBox>

                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group required">
                                        <label class="control-label">Organization Type</label> 
                                        <asp:DropDownList runat="server" ID="ddlOrgType" class="form-control form-control-sm" />

                                    </div>
                                </div>
                                   <div class="col-md-1">
                                       <br/>
                                       <br/>
                                     
                                       <div class="form-group">
                                       <asp:Button Text="Add" runat="server" CssClass="button btn-dark md-btn-large" OnClick="addNewOrgType_Click"  ID="addNewOrgType" />
                                           </div>
                                       </div>
                                   <div class="col-md-2">
                                    <div class="form-group required">
                                        <label class="control-label">Enlisted Date</label>
                                        <asp:TextBox runat="server" ID="txt_RegYear"  class="form-control form-control-sm"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                Format="dd-MMM-yyyy"  CssClass="MyCalendar"
                                                TargetControlID="txt_RegYear" />
                                    </div>
                                </div>
                                   
                               <div class="col-md-2">
                                    <div class="form-group required">
                                        <label class="control-label">Organization Origin</label><br />
                                        <asp:RadioButtonList RepeatLayout="Flow" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rad_OrganizationType_OnSelectedIndexChanged" runat="server" ID="rad_OrganizationType">
                                            <%--<asp:ListItem Value="Inhouse" Text="Inhouse"></asp:ListItem>--%>
                                            <asp:ListItem Value="Local" Text="Local"></asp:ListItem>
                                            <asp:ListItem Value="Foreign" Text="Foreign"></asp:ListItem>

                                        </asp:RadioButtonList>

                                    </div>
                                </div>
                                  <div class="col-md-2" runat="server" ID="CountryDiv" Visible="False">
                                    <div class="form-group required">
                                        <label class="control-label">Country</label>
                                        <asp:DropDownList ID="ddlCountry"  runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                  
                                  <div class="col-md-3">
                                    <div class="form-group required">
                                        <label class="control-label">Contact Person</label>
                                        <asp:TextBox ID="txt_contactPerson"  runat="server" class="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                               <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="control-label">Email</label>
                                        <asp:TextBox ID="txt_Email"  runat="server" class="form-control form-control-sm"></asp:TextBox>
                                       <%-- <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txt_Email" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>--%>
                                    </div>
                                </div>
                                       <div class="col-md-3">
                                    <div class="form-group required">
                                        <label class="control-label">Cell No</label>
                                        <asp:TextBox ID="txt_cellNo" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                       <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderqty" runat="server"
                                                                                        Enabled="True" TargetControlID="txt_cellNo" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>     
                            </div>
         

     
                            <div class="form-row">
                                 
                               
                                 
                              
                            </div>
                            <div class="form-row">
                                <div class="col-md-3">
                                    <div class="form-group required">
                                        <label class="control-label">Address</label>
                                        <asp:TextBox ID="txt_Address" TextMode="multiline" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group required">
                                        <label class="control-label">Organization Profile</label>
                                        <asp:TextBox ID="txt_OrgProfile" TextMode="multiline" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                
                                
                                   <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Major Client List</label>
                                        <asp:TextBox ID="txt_Clients" TextMode="multiline" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Remarks</label>
                                        <asp:TextBox ID="txt_remarks" TextMode="multiline" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                 
                              
                                </div>
         <br/>
         <hr/>
                                  <div class="form-row">
                                
                                      
                                        <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Vendor Audit</label>
                                        <asp:RadioButtonList RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server" ID="rad_vendorAudit">
                                            <asp:ListItem Value="true" Text="Yes"></asp:ListItem>
                                            <asp:ListItem Value="false" Text="No"></asp:ListItem>
                                            

                                        </asp:RadioButtonList>
                                        <%--<asp:TextBox ID="txt_VendorAudit" TextMode="multiline" runat="server" class="form-control"></asp:TextBox>--%>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Client's Recommendations</label>
                                       <%-- <asp:TextBox ID="txt_ClientRecom" TextMode="multiline" runat="server" class="form-control"></asp:TextBox>--%>
                                        <asp:RadioButtonList RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server" ID="rad_clientsRecom">
                                            <asp:ListItem Value="true" Text="Yes"></asp:ListItem>
                                            <asp:ListItem Value="false" Text="No"></asp:ListItem>
                                            

                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                      
                                      
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Logistics Support</label>
                                         <asp:RadioButtonList RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server" ID="rad_logistic">
                                            <asp:ListItem Value="true" Text="Yes"></asp:ListItem>
                                            <asp:ListItem Value="false" Text="No"></asp:ListItem>
                                            

                                        </asp:RadioButtonList>
                                       <%-- <asp:TextBox ID="txt_Logistics" TextMode="multiline" runat="server" class="form-control"></asp:TextBox>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="form-row">
                             
                                <div class="col-md-6">
                                    <br/>
                                    Trainer Information
                                    <hr/>
                                    <asp:GridView Width="100%" ID="gv_LeadTrainer" runat="server" ShowFooter="true"
                                        AutoGenerateColumns="false"   OnRowCreated="gv_LeadTrainer_RowCreated"    CssClass="table table-bordered text-center thead-dark">

                                        <Columns>
                                            <asp:TemplateField HeaderText="SL#">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                   
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Trainer">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_Trainer" runat="server" class="form-control form-control-sm" Text='<%#Eval("TrainerName") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Operation">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lb_RemoveTrainer" OnClick="lb_RemoveTrainer_Click" runat="server"
                                                        >Remove</asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                      <asp:Button ID="btn_AddTrainner" OnClick="btn_AddTrainner_Click" CssClass="btn btn-success btn-sm " runat="server" Text="Add" />
                                                    </FooterTemplate>
                                              
                                            </asp:TemplateField>
                                        </Columns>
                                        </asp:GridView>
                                </div>


                                <div class="col-md-6">
                                      <br/>
                                    Office Branch Information
                                    <hr/>
                                    <asp:GridView Width="100%" ID="gv_OfficeBranchs" OnRowDataBound="gv_OfficeBranchs_OnRowDataBound" runat="server" ShowFooter="true"
                                        AutoGenerateColumns="false"        CssClass="table table-bordered text-center thead-dark">

                                        <Columns>
                                            <asp:TemplateField HeaderText="SL#">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                   
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Office Branch">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_OfficeBranch" runat="server" class="form-control form-control-sm" Text='<%#Eval("BranchDetails") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Office Address">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_BranchAddress" runat="server" class="form-control form-control-sm" Text='<%#Eval("BranchAddress") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Country">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddl_BrunchCountry" runat="server" class="form-control form-control-sm" ></asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Operation">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lb_RemoveBranch" OnClick="lb_RemoveBranch_Click" runat="server"
                                                        >Remove</asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                      <asp:Button ID="btn_AddBranch" OnClick="btn_AddBranch_Click" CssClass="btn btn-success btn-sm " runat="server" Text="Add" />
                                                    </FooterTemplate>
                                              
                                            </asp:TemplateField>
                                        </Columns>
                                        </asp:GridView>
                                </div>
                            </div>
                           


                            <div class="form-row">
                                  
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Affiliation </label>
                                        <asp:TextBox runat="server" ID="txt_affiliation" class="form-control form-control-sm"></asp:TextBox>
                                        
                                    </div>
                                </div>
                                 <div class="col-md-5">
                                    <div class="form-group">
                                        <label>Supplimenting Document </label>
                                       <asp:CheckBoxList ID="chkDocument" RepeatDirection="Horizontal"  OnSelectedIndexChanged="chkDocument_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control chkChoice"  runat="server">
                                             <asp:ListItem Value="Tin">TIN</asp:ListItem>
                                            <asp:ListItem Value="Vat">VAT</asp:ListItem>
                                            <asp:ListItem Value="Trade">Trade License</asp:ListItem>
                                            <asp:ListItem Value ="Bank">Bank Solvency</asp:ListItem>
                                            <asp:ListItem Value="Other">Other</asp:ListItem>
                                       </asp:CheckBoxList>
                                        
                                    </div>
                                </div>

                                <div class="col-md-3" id="div_other" runat="server" Visible="false" >
                                    <div class="form-group">
                                         <label>Other </label>
                                        <asp:TextBox runat="server" ID="txt_Others" Visible="false" TextMode="MultiLine" class="form-control form-control-sm"></asp:TextBox>
                                        
                                    </div>
                                </div>
                                
                            </div>
          <div class="form-row">

                             
                                 <div class="col-md-6" runat="server" Visible="False">
                                    <br/>
                                    Documents
                                    <hr/>
                                    <asp:GridView Width="100%" ID="FileUploadGridView" runat="server" ShowFooter="true"
                                        AutoGenerateColumns="false"   OnRowCreated="FileUploadGridView_RowCreated"    CssClass="table table-bordered text-center thead-dark">

                                        <Columns>
                                            <asp:TemplateField HeaderText="SL#">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                   
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Title">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_Trainer" runat="server" class="form-control form-control-sm" Text='<%#Eval("TitleName") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                             <asp:TemplateField HeaderText="Document">
                                                <ItemTemplate>
                                                    <asp:FileUpload ID="FileUpload1_Doc" Text='<%#Eval("FileUpload1") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Operation">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lb_RemoveTrainer" OnClick="lb_FileUploadGridView_Click" runat="server"
                                                        >Remove</asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                      <asp:Button ID="btn_AddTrainner" OnClick="btn_FileUploadGridView_Click" CssClass="btn btn-success btn-sm " runat="server" Text="Add" />
                                                    </FooterTemplate>
                                              
                                            </asp:TemplateField>
                                        </Columns>
                                        </asp:GridView>
                                </div>
                             
                               
                            </div>
                            <div>
                                <asp:HiddenField runat="server" ID="hdpk"/>
                                
                                
                            <asp:Button ID="submitButton" Text="Save" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="btn_Save_Click" />
                            <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="editButton_OnClick" />
                            <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="delButton_OnClick" />
                                

                                <%--<asp:Button runat="server" ID="btn_Save" Text="Submit " CssClass="btn btn-sm btn-info" OnClick="btn_Save_Click" />--%>
                                <asp:Button ID="cancelButton" Text="Cancel" CssClass="btn btn-sm warning" Visible="False" runat="server" BackColor="#FFCC00" />
                            </div>
         </fieldset>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
     <div>
        <ajaxToolkit:ModalPopupExtender ID="mpe_1" runat="server" TargetControlID="hnd_Test" PopupControlID="pnl_1"
            BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:HiddenField ID="hnd_Test" runat="server"></asp:HiddenField>
        <asp:Panel ID="pnl_1" runat="server" Style="display: none;" Height="500px" Width="90%" CssClass="modalPopup">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <h3 style="text-align: center;">Add Organization Type <span><asp:Label ForeColor="#8B0000" Font="Bold" runat="server" ID="m_MemberName"></asp:Label></span></h3>
                    <div>
                    
                               <div class="form-row">
                                   <div class="col-md-2">

                                   </div>
                                   <div class="col-md-4">
                                        <div class="form-group required">
                                         <label class="control-label">Organization Type </label>
                                        <asp:TextBox runat="server" ID="txt_orgType"   class="form-control form-control-sm"></asp:TextBox>
                                            <br/>
                                           <asp:Button runat="server" ID="btnYes" OnClick="btnYes_Click" Text="Submit " CssClass="btn btn-sm btn-info" />
                        <asp:Button ID="btnNo" Text="Cancel" OnClick="btnNo_Click"  CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                                        </div>
                                   </div>
                               </div>
                        <br/>
                        
                     
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
</asp:Content>


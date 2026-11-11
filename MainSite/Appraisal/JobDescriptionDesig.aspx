<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="JobDescriptionDesig.aspx.cs" Inherits="Appraisal_JobDescription" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
      <style type="text/css">
       
    </style>
    <div class="content">
        
        <asp:UpdatePanel runat="server" ID="upFormBody">
              <ContentTemplate>
                   <div class="container-fluid">
                       
                           <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;">Job Description</h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="detailsViewButton" Text="Job Description List"   OnClick="detailsViewButton_OnClick" CssClass="btn btn-sm btn-outline-secondary " runat="server"  />
                        </div>

                    </div>
                        <div class="card">
                           <div class="card-body">
                               
                            <div class="form-row">
                                  <div class="col-3">
                                    <div class="form-group">
                                        <label>Company</label>
                                        <asp:DropDownList runat="server" ID="ddlCompany" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" CssClass="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                 <div class="col-4">
                                    <div class="form-group">
                                        <label>Job Title</label>
                                        <asp:DropDownList ID="desigDropDownList" CssClass="form-control form-control-sm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="desigDropDownList_OnSelectedIndexChanged" ></asp:DropDownList>
                                         </div>
                                </div>
                                 <div class="col-4">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                        <asp:DropDownList ID="ddlFinancialYear"  AutoPostBack="true" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Salary Grade</label>
                                        <asp:Label runat="server" ID="lblSalGrade"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Division Administration:</label>
                                        <asp:DropDownList ID="divDropDownList" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Job Location</label>
                                        <asp:DropDownList ID="jobLocDropDownList" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                 
                                
                                
                            </div>
                               <div class="form-row">
                                    <Div class="col-md-6">
                                         <div class="form-group">
                                        <label>Job Summary</label>
                                        <asp:TextBox runat="server" ID="txtJobSummary" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    </Div>
                               </div>
                               <div class="form-row">
                                    <Div class="col-md-3">
                                         <div class="form-group">
                                        <label>Reports To</label>
                                        <asp:DropDownList ID="reportToDropDownList" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                    </div>
                                    </Div>
                                    <Div class="col-md-3">
                                         <div class="form-group">
                                        <label>Directly Supervises</label>
                                        <asp:DropDownList ID="directSuperDropDownList" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                    </div>
                                    </Div>
                                   <Div class="col-md-3">
                                         <div class="form-group">
                                        <label>Internal Contacts</label>
                                        <asp:DropDownList ID="interContDropDownList" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                    </div>
                                    </Div>
                                   <Div class="col-md-3">
                                         <div class="form-group">
                                        <label>External Contacts</label>
                                        <asp:DropDownList ID="extContDropDownList" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                    </div>
                                    </Div>
                               </div>
                            
                        <asp:GridView runat="server"   ShowFooter="true" AutoGenerateColumns="False" Width="100%" id="gv_JdDetails" CssClass="table table-bordered text-center thead-dark gridDatatable">
                                
                                                <Columns>

                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                    
                                 <asp:TemplateField HeaderText="Job Description">
                                    <ItemTemplate>
                                       <asp:TextBox runat="server" ID="txtJdDetails" CssClass="form-control  form-control-sm"  TextMode="MultiLine" Text='<%#Eval("JdDetailsInfo") %>' ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              

                               
                                                    

                                                    


                                <asp:TemplateField HeaderText="Operation">
                                    <ItemTemplate>
                                        
                                        <asp:LinkButton ID="btn_Add" OnClick="btn_Add_OnClick" runat="server">Add</asp:LinkButton>|
                                        <asp:LinkButton ID="lb_Remove" OnClick="lb_Remove_OnClick" runat="server">Remove</asp:LinkButton>
                                    </ItemTemplate>
                                    
                                  
                                </asp:TemplateField>
                                
                            </Columns>
                            </asp:GridView>
                               <div class="form-row">
                                    <Div class="col-md-3">
                                         <div class="form-group">
                                        <label>Education</label>
                                        <asp:TextBox runat="server" ID="educationTextBox" CssClass="form-control form-control-sm" ></asp:TextBox>
                                    </div>
                                    </Div>
                                    <Div class="col-md-3">
                                         <div class="form-group">
                                        <label>Relevant Experience</label>
                                        <asp:TextBox runat="server" ID="relexpTextBox" CssClass="form-control form-control-sm" ></asp:TextBox>
                                    </div>
                                    </Div>
                                   <Div class="col-md-2">
                                         <div class="form-group">
                                        <label>Special Skill</label>
                                        <asp:TextBox runat="server" ID="specialSkillTextBox" CssClass="form-control form-control-sm" ></asp:TextBox>
                                    </div>
                                    </Div>
                                   <Div class="col-md-2">
                                         <div class="form-group">
                                        <label>Other Requirments</label>
                                        <asp:TextBox runat="server" ID="otherReqTextBox" CssClass="form-control form-control-sm" ></asp:TextBox>
                                    </div>
                                    </Div>
                                   <Div class="col-md-2">
                                         <div class="form-group">
                                        <label>Computer Skill</label>
                                        <asp:TextBox runat="server" ID="compSkillTextBox" CssClass="form-control form-control-sm" ></asp:TextBox>
                                    </div>
                                    </Div>
                               </div>
                               <asp:HiddenField runat="server" id="masterId"/>
                               <asp:HiddenField runat="server" id="empInfoId"/>
                              
                                      <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" CssClass="btn btn-sm btn-info" Text="Submit"></asp:Button>
                            
                                     <asp:Button ID="cancelButton" Text="Cancel" OnClick="cancelButton_OnClick" CssClass="btn btn-sm warning" runat="server"  BackColor="#FFCC00" />
                           </div>
                        </div>
                    </div>
              </ContentTemplate>
           
        </asp:UpdatePanel>
        
    </div>
</asp:Content>


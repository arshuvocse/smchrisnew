<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="AppraisalPartB.aspx.cs" Inherits="Appraisal_AppraisalPartB" %>


<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    
    <div class="content">
        
        <asp:UpdatePanel runat="server" ID="upFormBody">
              <ContentTemplate>
                   <div class="container-fluid">
                           <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;">Appraisal Behavioral Part</h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="detailsViewButton" Text="Appraisal List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick"  />


                               
                        </div>
                    </div>
                        <div class="card">
                           <div class="card-body">
                                 <div class="form-row" runat="server" Visible="False">
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                        <asp:DropDownList ID="ddlFinancialYear" AutoPostBack="true" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                             <div class="form-row">
                                <div class="col-2">
                                    <div class="form-group">
                                         <label>Employee Name :</label>
                                        <asp:Label ID="empName" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-2" runat="server" visible="false">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                        <asp:DropDownList ID="DropDownList1" AutoPostBack="true" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-row">
                               

                                <div class="col-2" runat="server" visible="false">
                                    <div class="form-group">
                                        <label>Division Name :</label>
                                        <asp:Label ID="divisionNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="divitionIdHiddenField" runat="server" />
                                    </div>
                                </div>

                                
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Designation Name :</label>
                                        <asp:Label ID="desigNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="desigIdHiddenField" runat="server" />
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Department Name :</label>
                                        <asp:Label ID="deptNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="deptIdHiddenField" runat="server" />
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Joining Date :</label>
                                        <asp:Label ID="joiningDateLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                    </div>
                                </div>
                                
                                    <div class="col-2">
                                    <div class="form-group">
                                        <label>Job Location :</label>
                                        <asp:Label ID="LocationLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                    </div>
                                </div>
                                
                                    <div class="col-2">
                                    <div class="form-group">
                                        <label>Supervisor :</label>
                                        <asp:Label ID="ReportingLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="col-3">
                                    <div class="form-group" runat="server" Visible="False">
                                        <label>Wing Name :</label>
                                        <asp:Label ID="divWingNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="divWingIdHiddenField" runat="server" />
                                    </div>
                                </div>
                                 <div class="col-3" runat="server" Visible="False">
                                    <div class="form-group">
                                        <label>Company Name :</label>
                                        <asp:Label ID="comNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="comIdHiddenField" runat="server" />
                                    </div>
                                </div>
                                <div class="col-3" runat="server" Visible="False">
                                    <div class="form-group">
                                        <label>Section Name :</label>
                                        <asp:Label ID="secNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="secIdHiddenField" runat="server" />
                                    </div>
                                </div>


                                <div class="col-3">
                                    <div class="form-group" runat="server" Visible="False">
                                        <label>Sub Section Name :</label>
                                        <asp:Label ID="subSectionLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="subSectionHiddenField" runat="server" />
                                    </div>
                                </div>

                                

                                <div class="col-3">
                                    <div class="form-group" runat="server" Visible="False">
                                        <label>Employee Type:</label>
                                        <asp:Label ID="employeeType" runat="server" class="form-control form-control-sm"></asp:Label>
                                        <asp:HiddenField ID="empTypeHiddenField" runat="server" />
                                    </div>
                                </div>
                                

                            </div>
                               <%--<div class="form-row">
                                           <div class="col-3">
                                    <div class="form-group">
                                        <label>Employee Name :</label>
                                        <asp:Label ID="empName" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Company Name :</label>
                                        <asp:Label ID="comNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="comIdHiddenField" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Division Name :</label>
                                        <asp:Label ID="divisionNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="divitionIdHiddenField" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label> Wing Name :</label>
                                        <asp:Label ID="divWingNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="divWingIdHiddenField" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Department Name :</label>
                                        <asp:Label ID="deptNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="deptIdHiddenField" runat="server" />
                                    </div>
                                </div>
                                         
                                         <div class="col-3">
                                    <div class="form-group">
                                        <label>Section Name :</label>
                                        <asp:Label ID="secNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="secIdHiddenField" runat="server" />
                                    </div>
                                </div>


                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Sub Section Name :</label>
                                        <asp:Label ID="subSectionLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="subSectionHiddenField" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Designation Name :</label>
                                        <asp:Label ID="desigNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="desigIdHiddenField" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Employee Type:</label>
                                        <asp:Label ID="employeeType" runat="server" class="form-control form-control-sm"></asp:Label>
                                        <asp:HiddenField ID="empTypeHiddenField" runat="server" />
                                    </div>
                                </div>
                                  <div class="col-3">
                                    <div class="form-group">
                                        <label>Joining Date :</label>
                                        <asp:Label ID="joiningDateLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>--%>
                              
                            
                        <asp:GridView runat="server" OnRowDataBound="gv_AppraisalFunc_OnRowDataBound"   ShowFooter="true" AutoGenerateColumns="False" Width="100%" id="gv_AppraisalPartB" CssClass="table table-bordered text-center thead-dark gridDatatable">
                                
                                                <Columns>

                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                    
                                   <asp:TemplateField HeaderText="Competencies / Skills">
                                    <ItemTemplate>
                                       <asp:TextBox runat="server" ReadOnly="True" ID="SkillInfo" CssClass="form-control  form-control-sm"  TextMode="MultiLine" Text='<%#Eval("SkillInfo") %>' ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                    
                                   <asp:TemplateField HeaderText="Supporting Example">
                                    <ItemTemplate>
                                       <asp:TextBox runat="server" ReadOnly="True" ID="SupportingEmp"  CssClass="form-control  form-control-sm"  TextMode="MultiLine" Text='<%#Eval("SupportingEmp") %>' ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                    
                                <asp:TemplateField HeaderText="Weight (Number)">
                                    <ItemTemplate>
                                       <asp:TextBox runat="server" ID="Weight" AutoPostBack="True" ReadOnly="True"  CssClass="form-control  form-control-sm"  TextMode="Number" Text='<%#Eval("Score") %>' ></asp:TextBox>
                                    </ItemTemplate>
                                   
                                </asp:TemplateField>
                                                    
                                                    
                                                            
                                                        <asp:TemplateField HeaderText="Weight (Number)">
                                    <ItemTemplate>
                                       <asp:Label runat="server" ID="SetScore"   Text='<%#Eval("SetScore") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                            <asp:Label ID="ddllblTotalSetScore" CssClass="form-control  form-control-sm " runat="server" />
                                        </FooterTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Self Score">
                                    <ItemTemplate>
                                       <asp:TextBox runat="server" ReadOnly="True"   ID="SelfScore" CssClass="form-control  form-control-sm"  TextMode="Number" Text='<%#Eval("SelfScore") %>' ></asp:TextBox>

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            <cc1:FilteredTextBoxExtender ID="aaFisasretasdasdsasasare001sad" runat="server" Enabled="True"
TargetControlID="SelfScore" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                      <asp:Label ID="lblselfscrore"  CssClass="form-control  form-control-sm " runat="server"  />
                                                    </FooterTemplate>
                       


                                </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Supervisor Score">
                                    <ItemTemplate>
                                       <asp:TextBox runat="server"  AutoPostBack="True"  OnTextChanged="Score_OnTextChanged"  ID="SupervisorScore" CssClass="form-control  form-control-sm"  TextMode="Number" Text='<%#Eval("SupervisorScore") %>' ></asp:TextBox>

                                                                                                                                                                                                                                            <cc1:FilteredTextBoxExtender ID="Fil21asdaSupervisosadrScore001" runat="server" Enabled="True"
TargetControlID="SupervisorScore" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                    </ItemTemplate>
                                    
                                    <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                      <asp:Label ID="lblTotalMark"  CssClass="form-control  form-control-sm " runat="server"  />
                                                    </FooterTemplate>


                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Operation" runat="Server" Visible="false">
                                    <ItemTemplate>
                                        
                                        <asp:LinkButton ID="btn_Add" OnClick="btn_Add_OnClick" runat="server">Add</asp:LinkButton>|
                                        <asp:LinkButton ID="lb_Remove" OnClick="lb_Remove_OnClick"  runat="server">Remove</asp:LinkButton>
                                    </ItemTemplate>
                                    
                                  
                                </asp:TemplateField>
                                
                            </Columns>
                            </asp:GridView>
                               
                               <asp:HiddenField runat="server" id="id_mastetID"/>
                              <asp:HiddenField runat="server" id="id_selfID"/>
                               
                                      <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" CssClass="btn btn-sm btn-info" Text="Submit"></asp:Button>
                                         <%--<asp:Button runat="server" ID="btn_Review" OnClick="btn_Review_OnClick" CssClass="btn btn-sm btn-info" Text="Review"></asp:Button>--%>
                                  <%--   <asp:Button ID="cancelButton" Text="Cancel"  CssClass="btn btn-sm warning" runat="server"  BackColor="#FFCC00" />--%>
                           </div>
                        </div>
                    </div>
              </ContentTemplate>
           
        </asp:UpdatePanel>
        
    </div>
</asp:Content>


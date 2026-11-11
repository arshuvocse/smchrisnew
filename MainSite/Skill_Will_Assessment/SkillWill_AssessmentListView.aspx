<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="SkillWill_AssessmentListView.aspx.cs" Inherits="Skill_Will_Assessment_SkillWill_AssessmentListView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
        
        <style>
        
        #cpFormBody_gv_allocateEmp  > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }


        #cpFormBody_gv_allocateEmp td {
           
            padding: 8px;
        }

       #cpFormBody_gv_allocateEmp > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
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
    <link href="../Assets/MyPerfectCalender.css" rel="stylesheet" />
    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>
                   <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" /> Skill Will Assesment Information</h1>
                        </div>
                        <%--<div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="detailsViewButton" Text="KPI Deadline Setup List" OnClick="detailsViewButton_OnClick" CssClass="btn btn-sm btn-outline-secondary " runat="server" />
                        </div>--%>

                        <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                            <asp:Button ID="detailsViewButton" Visible="True" Text="View List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                            
                            <asp:Button ID="btnAdd" Visible="True" Text="Add New" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="AddNewButton_OnClick" />
                        </div>


                    </div>

                    <div class="card">
                        <div class="card-body">
                            <div class="form-row">
                               
                                            <style>
        .lblDesign {
            font-weight: bold;
            align-content: right;
            text-align: right;
            padding: 9px;
        }
                                                </style>


                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Employee Name</label>  
                                        <asp:TextBox ID="subjectTextBox" runat="server"   Enabled="False" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>

                               

                               
                            </div>

                         
                            
                            <fieldset class="for-panel">

                                <legend>Key Accountabilities</legend>


                        <asp:GridView ID="gv_allocateEmp" Width="100%" CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="false"  ShowFooter="true"  runat="server">
                            <Columns>

                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                       
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="KRA">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_empId" runat="server"  Text='<%#Eval("KRA") %>'></asp:Label>
                                    </ItemTemplate>
                                        
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="sdsd" CssClass="lblDesign" runat="server" Text="Overall Status (Average): " />
                                              <br/>
                                            <hr/>
                                            <asp:Label ID="lblssad_SkillT" CssClass="lblDesign" Text="Status in Skill-Will Matrix:"  runat="server" />
                                        </FooterTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="SKILL">
                                    <ItemTemplate>
                                        <asp:Label ID="txtSkill" runat="server"  Text='<%#Eval("SKILL") %>'></asp:Label>
                                    </ItemTemplate>
                                    
                                      <FooterStyle HorizontalAlign="Center" />
                                        <FooterTemplate>
                                            <asp:Label ID="lbl_SkillT" CssClass="lblDesign"  runat="server" />
                                            
                                             <br/>
                                            <hr/>
                                            <asp:Label ID="lbls_SkillT" CssClass="lblDesign" Text=""  runat="server" />
                                        </FooterTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="WILL">
                                    <ItemTemplate>
                                        <asp:Label ID="txtWill" runat="server"   Text='<%#Eval("WILL") %>'></asp:Label>
                                    </ItemTemplate>
                                    
                                           <FooterStyle HorizontalAlign="Center" />
                                        <FooterTemplate>
                                            <asp:Label ID="lbl_WilllT" CssClass="lblDesign" Text="" runat="server" />

                                       <br/>
                                            <hr/>
                                            <asp:Label ID="lbls_WilllTd" CssClass="lblDesign"  runat="server" />
                                        </FooterTemplate>
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Areas considered">
                                        <ItemTemplate>
                                               <asp:TextBox runat="server" ID="txtAreasconsidered"   Text='<%#Eval("Areasconsidered") %>' CssClass="form-control form-control-sm" ></asp:TextBox>
                                            
                                        </ItemTemplate>

                                    </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                                </fieldset>

                     
                           
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                             </div>
                    </div>

                    <asp:HiddenField runat="server" ID="hid_KpiMasrerId" />



                </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>


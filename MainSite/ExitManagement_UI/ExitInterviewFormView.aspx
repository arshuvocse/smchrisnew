<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="ExitInterviewFormView.aspx.cs" Inherits="ExitManagement_UI_ExitInterviewFormView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
      <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <style>
                      .table   thead th {
               background-color: #5B799E;
               color: white;
                font-size: 13px !important;
                                            font-family: "Times New Roman", Times, serif !important;
                                            font-style: italic !important;
                                            font-weight: bold!important;
           }

                </style>
                
                <div class="page-heading">
                    <div class="page-heading__container">
                        <div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;"> Exit Interview Notification </h1>
                    </div>

                    <div class="page-heading__container float-right d-none d-sm-block">
                        
                        <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                       
                        <%--<asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                        <asp:Button ID="addNewButton" Text="Add New Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="addNewButton_OnClick" />--%>
                    </div>
                    


                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">
                            
                             <div class="row" runat="server" Visible="False">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label></label>
                                        <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True" RepeatDirection="Horizontal" Enabled="False">
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                            
                            <div id="gridContainer1" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;">
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered text-center thead-dark" DataKeyNames="EmployeeJobLeftId,EmployeeId"
                                    OnRowCommand="loadGridView_RowCommand" OnRowCreated="loadGridView_OnRowCreated">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField DataField="CompanyName" HeaderText="Company Name" />--%>

                                        <asp:BoundField DataField="EmpName" HeaderText="Empployee Name" />
                                      
                                        <asp:BoundField DataField="JobLeftType" HeaderText="Job Left Type" />
                                        
                                        <asp:BoundField DataField="SubmissionDate" HeaderText="Submission Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="JobLeftDate" HeaderText="Seperation Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        
                                        <%--<asp:BoundField DataField="ExitFormStatus" HeaderText="Status" />--%>
                                            <asp:TemplateField HeaderText="Status" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblExitFormStatus" runat="server"   Text='<%#Eval("ExitFormStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="clearenceImageButton" runat="server" CssClass="btn btn-info btn-sm"
                                                    CommandArgument="<%# Container.DataItemIndex %>"   CommandName="Clearance"
                                                    >Go to Exit Interview Form &#8921;</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>


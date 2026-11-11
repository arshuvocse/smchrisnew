<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="SurveyDeclaretionListForEmployee.aspx.cs" Inherits="Survey_SurveyDeclaretionListForEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server" >
    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                        
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;"><img src="../Report_Pages/app.png"  width="20px" />  Survey</h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                         <asp:Button ID="HomeButton"  Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="HomeButton_OnClick" />
                        <asp:Button ID="addNewButton" Visible="False"  Text="Add New Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="addNewButton_OnClick" />
                    </div>

                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid" style="background-color: #DFF9FB">
                    <div class="card" >
                        <div class="card-body" >
                             <div class="row">
                                    <div class="col-md-3" runat="server" Visible="False">
                                        <div class="form-group">
                                            <label> </label>
                                             <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True"  RepeatDirection="Horizontal" Enabled="False" >
                                        </asp:CheckBoxList>
                                            

                                        </div>
                                    </div>
                                </div>
                            <div id="gridContainer1" style="height: auto; overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;">
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-bordered text-center thead-dark" DataKeyNames="SurveyMasterId, EmployeeId"
                                    OnRowCommand="loadGridView_RowCommand" OnRowCreated="loadGridView_OnRowCreated">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ShortName" HeaderText="Company" />
                                        <asp:BoundField DataField="FinancialYearDesc" HeaderText="Financial Year" />

                                        <asp:BoundField DataField="SurveyName" HeaderText="Survey Name" />
                                        <asp:BoundField DataField="SurveyFrom" HeaderText="Survey From Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="SurveyTo" HeaderText="Survey To Date" DataFormatString="{0:dd-MMM-yyyy}" />

                                      
                                     

                                        <asp:TemplateField HeaderText=" ">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="editImageButton" runat="server"
                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="EditData" Text="Go To Survey Form>>"
                                                    />
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


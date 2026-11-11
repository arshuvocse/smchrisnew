<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="LunchAllowCancelNew.aspx.cs" Inherits="Lunch_Allowance_UI_LunchAllowCancelNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    <div class="content" id="content" >
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                       
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">  <img src="../Report_Pages/app.png"  width="20px" />  Food Rate Information  </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                           <%--<asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="HomeButton_OnClick" />
                        <asp:Button ID="detailsViewButton" Text="Go Back" CssClass="btn btn-sm btn-outline-secondary "   runat="server" OnClick="detailsViewButton_OnClick" />--%>
                    </div>

                </div>   <asp:HiddenField ID="hdpk" runat="server" />
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">

                            <form>
                            <div class="row">
                                
                                <div class="col-md-3">                        

                                     <div class="form-group">
                                        <label>Company Name </label>
                                        <span style="color: red">&nbsp;*</span>
                                        <asp:DropDownList ID="companyDropDownList" class="form-control form-control-sm"  runat="server" AutoPostBack="False"  ></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">                        

                                     <div class="form-group">
                                        <label>Effective Date </label> <span style="color: red">&nbsp;*</span>
                                        

                                        <asp:TextBox ID="effectiveDateTextBox" AutoCompleteType="Disabled" runat="server" AutoPostBack="True"   CausesValidation="true" OnTextChanged="effectiveDateTextBox_OnTextChanged"
                                            class="form-control form-control-sm"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Format="dd-MMM-yyyy" CssClass="MyCalendar" PopupPosition="TopLeft"
                                            TargetControlID="effectiveDateTextBox" />
                                    </div>
                                </div>
                              
                            </div>
                            <div class="row">
                                
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Button ID="Button1" Text=" Search  " CssClass="btn btn-sm btn-info"  runat="server" OnClick="Button1_OnClick" />

                                         
                                        
                                    </div>
                                </div>
                                <div class="col-md-3">
                                </div>

                                <div class="col-md-4">
                                </div>
                            </div>
                           <div class="row">
                                
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False" 
                                                CssClass="table table-bordered text-center thead-dark table-hover table-striped" DataKeyNames="EmpInfoId" ShowFooter="True">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="EmpMasterCode" HeaderText="EmpMasterCode" />
                                                    <asp:BoundField DataField="EmpName" HeaderText="EmpName" />
                                                      <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                                      <asp:BoundField DataField="DepartmentName" HeaderText="DepartmentName" />
                                      
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="inactiveCheckBox" Checked="True" runat="server" />
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
                                        <%--<asp:Button ID="submitButton" Text=" Submit " CssClass="btn btn-sm btn-info"     runat="server" OnClick="submitButton_OnClick" />--%>
                                         
                                        
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


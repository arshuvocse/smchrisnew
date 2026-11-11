<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="MemoPrintEmployeePromotion.aspx.cs" Inherits="Transfer_UI_MemoPrintEmployeePromotion" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

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
    </style>

    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                        
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;"><img src="../Report_Pages/app.png"  width="20px" />&nbsp;Annual Employee Promotion Letter Information </h1>
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
                            <asp:HiddenField runat="server" ID="EmpIdHiddenfield" />

                            <div class="row">

                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:Label ID="lblLabelInfo" CssClass="form-control form-control-sm col-md-4" runat="server" Text=""></asp:Label>

                                        </div>

                                        <div class="col-md-6">
                                            <asp:Label ID="lblDate" CssClass="form-control form-control-sm col-md-4 pull-right" runat="server" Text=""></asp:Label></div>
                                    </div>
                                    <br />
                                    <fieldset class="for-panel">
                                        <legend>Employee Information</legend>
                                        <div class="row">

                                            <div class="col-md-6">


                                                <div class="form-row">
                                                    <div class="col-md-3" style="padding: 10px;">
                                                        <label>Employee ID: </label>
                                                    </div>

                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblEmployeeCode" CssClass="form-control form-control-sm" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>


                                                <div class="form-row">
                                                    <div class="col-md-3" style="padding: 10px;">

                                                        <label>Employee Name: </label>
                                                    </div>

                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblEmp" CssClass="form-control form-control-sm" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>


                                                <div class="form-row">
                                                    <div class="col-md-3" style="padding: 10px;">

                                                        <label>Designation: </label>
                                                    </div>

                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblDesignation" CssClass="form-control form-control-sm" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="form-row">
                                                    <div class="col-md-3" style="padding: 10px;">
                                                        <label>Department: </label>
                                                    </div>

                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblDepartment" CssClass="form-control form-control-sm" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>



                                                <div class="form-row">
                                                    <div class="col-md-3" style="padding: 10px;">
                                                        <label>Place of Posting: </label>
                                                    </div>

                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblOffice" CssClass="form-control form-control-sm" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="col-md-6">

                                                <div class="form-row">
                                                    <div class="col-md-3" style="padding: 10px;">
                                                        <label>Previous Salary Grade: </label>
                                                    </div>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtPreviousSalaryGrade" CssClass="form-control form-control-sm" runat="server" Text=""></asp:TextBox>
                                                    </div>
                                                </div>
                                                
                                                       <div class="form-row">
                                                    <div class="col-md-3" style="padding: 10px;">
                                                        <label>New Salary Grade: </label>
                                                    </div>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtNewSalaryGrade" CssClass="form-control form-control-sm" runat="server" Text=""></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-row">
                                                    <div class="col-md-3" style="padding: 10px;">
                                                        <label>Previous Step: </label>


                                                    </div>

                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtPreSalStep" CssClass="form-control form-control-sm" runat="server" Text=""></asp:TextBox>
                                                    </div>
                                                </div>


                                                <div class="form-row">
                                                    <div class="col-md-3" style="padding: 10px;">
                                                        <label>Incremental Step: </label>

                                                    </div>

                                                    <div class="col-md-9">

                                                        <asp:TextBox ID="txtIncrementalStep" CssClass="form-control form-control-sm" runat="server" Text=""></asp:TextBox>
                                                    </div>
                                                </div>
                                                
                                                
                                                <div class="form-row">
                                                    <div class="col-md-3" style="padding: 10px;">
                                                        <label>Previous Designation: </label>
                                                    </div>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtPreviousDesignation" CssClass="form-control form-control-sm" runat="server" Text=""></asp:TextBox>
                                                    </div>
                                                </div>
                                                
                                                       <div class="form-row">
                                                    <div class="col-md-3" style="padding: 10px;">
                                                        <label>New Designation: </label>
                                                    </div>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtNewDesignation" CssClass="form-control form-control-sm" runat="server" Text=""></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>


                                        </div>
                                    </fieldset>
                                </div>


                            </div>

                            <div class="row">

                                <div class="col-md-6">

                                    <div class="form-row">

                                        <div class="col-md-3" style="padding: 10px;">
                                            <label>Salutation: </label>
                                        </div>

                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtSalutation" CssClass="form-control form-control-sm" runat="server" Text=""></asp:TextBox>
                                        </div>
                                    </div>


                                    <div class="form-row">
                                        <div class="col-md-3" style="padding: 10px;">
                                            <label>Body of the letter: </label>
                                        </div>

                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtBodyofletter" CssClass="form-control" TextMode="MultiLine" placeholder="Write first paragraph" runat="server" Text="" Font-Size="12px" Rows="4"></asp:TextBox>
                                        </div>
                                    </div>

                                    <br />
                                    <div class="form-row">
                                        <div class="col-md-12">
                                            <asp:GridView ID="gvSalaryStep" Width="100%" runat="server" DataKeyNames="ParticularsId" AutoGenerateColumns="False" CssClass="table table-bordered text-center thead-dark">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:BoundField DataField="Particulars" HeaderText="Particulars" />


                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" Text='<%# Bind("NewStepId") %>' ID="txtAmount1" CssClass="form-control form-control-sm" />
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server"
                                                                Enabled="True" TargetControlID="txtAmount1" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:TextBox Text='<%# Bind("PreStepId") %>' runat="server" CssClass="form-control form-control-sm" ID="txtAmount2" />
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtendevr13" runat="server"
                                                                Enabled="True" TargetControlID="txtAmount2" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-md-6">
                                    <div class="form-row">

                                        <div class="col-md-3" style="padding: 10px;">

                                            <label>Complimentary Close: </label>
                                        </div>

                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtComplimentaryClose" CssClass="form-control" TextMode="MultiLine" placeholder="Write first paragraph" runat="server" Text="" Font-Size="12px" Rows="4"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-row">

                                        <div class="col-md-3" style="padding: 10px;">
                                        </div>

                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtSincerely" CssClass="form-control form-control-sm" runat="server" Text="Yours Sincerely,"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />


                                    <div class="form-row">

                                        <div class="col-md-3" style="padding: 10px;">
                                            <label>Name & Designation: </label>
                                        </div>

                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtName" Font-Size="12px" Rows="3" TextMode="MultiLine" CssClass="form-control" runat="server" Text=""></asp:TextBox>
                                        </div>
                                    </div>

                                    <br />
                                    <div class="form-row">

                                        <div class="col-md-3" style="padding: 10px;">

                                            <label>Copy To: </label>
                                        </div>

                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtCopyTO" CssClass="form-control" TextMode="MultiLine" placeholder="Write first paragraph" runat="server" Text="" Font-Size="12px" Rows="4"></asp:TextBox>
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

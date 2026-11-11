<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="BenefitEntry.aspx.cs" Inherits="Benefit_UI_BenefitEntry" %>

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

              .Label_Title {
            background-color: #C7C7C7;
            width: 100%;
            text-align: center;
            margin: 0px;
            padding: 5px;
            text-align: center;
            color: #000;
            margin-right: 5%;
            font-weight: bold;
            font-size: 13px;
        }

    </style>

    <style type="text/css">
        .checkbox label {
            display: inline;
        }

        .margin-right {
            margin-right: 5px;
        }

           .chkChoiceblod label {
            padding-left: 5px;
            padding-right: 30px;
             font-weight: bold;
        }
    </style>

    <style>
        .resize {
            resize: none;
        }

            .chkChoice label {
            padding-left: 5px;
            padding-right: 30px;
        }

    </style>

    <div class="content" id="content">

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                      <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px"
                   />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                       
                        <h1 class="title" style="font-size: 16px; padding-top: 1px;"> <img src="../Report_Pages/app.png"  width="20px" />Benefit Information </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="HomeButton_OnClick" />
                        <asp:Button ID="AddNewButton" Text="View" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="AddNewButton_OnClick" />
                        <%--<asp:Button ID="reloadButton" Text="Reload" CssClass="btn btn-sm btn-outline-success" runat="server" OnClick="reloadButton_OnClick" />--%>
                    </div>

                </div>


                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-3">


                                    <div class="form-group">
                                        <label>Company Name: </label>
                                        <asp:DropDownList ID="companyDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"></asp:DropDownList>

                                    </div>
                                    <div class="form-group">
                                        <label>Benefit Name: </label>

                                        <asp:TextBox ID="benefittextBox" runat="server" class="form-control form-control-sm"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <br />

                                        <label class="btn btn-default">
                                            <asp:CheckBox ID="isactiveCheckBox" CssClass="checkbox margin-right" runat="server" />
                                            <label>Is Active </label>
                                        </label>
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
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <fieldset class="for-panel">
                                        <legend>Employee Category</legend>
                                        <div class="row">
                                            <div class="col-md-6">
                                              <div class="Label_Title">Management List</div>
                                                  <br/>
                                                <div class="form-group">
                                                    <label>
                                                        <asp:CheckBox ID="manCheckBox" Text=" Select All / Unselect All Management" runat="server" CssClass="chkChoiceblod" AutoPostBack="True" OnCheckedChanged="manCheckBox_OnCheckedChanged" />
                                                    </label>
                                                     <div style="overflow: scroll; height: 150px">
                                                        <asp:CheckBoxList ID="managementCheckBoxList" RepeatColumns="5"  CssClass="chkChoice" RepeatDirection="Horizontal" runat="server"></asp:CheckBoxList>
                                                    </div>


                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                 <div class="Label_Title">Grade List</div>
                                                  <br/>
                                                <div class="form-group">
                                                    <label>
                                                        <asp:CheckBox ID="gradedCheckBox" Text=" Select All / Unselect All Graded" AutoPostBack="True" CssClass="chkChoiceblod" runat="server"  OnCheckedChanged="gradedCheckBox_OnCheckedChanged" /></label>
                                                    <div style="overflow: scroll; height: 150px">
                                                        <asp:CheckBoxList ID="gradedCheckBoxList" RepeatColumns="5" CssClass="chkChoice" RepeatDirection="Horizontal" runat="server"></asp:CheckBoxList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <fieldset class="for-panel">
                                        <legend>Fund/Projects</legend>
                                        <div class="row">
                                            <div class="col-md-3">

                                                <div class="form-group">

                                                    <asp:CheckBoxList ID="natureCheckBoxList" AutoPostBack="True" CssClass="chkChoice"  runat="server" OnSelectedIndexChanged="natureCheckBoxList_SelectedIndexChanged">
                                                        <asp:ListItem>Permanent</asp:ListItem>
                                                        <asp:ListItem>Contractual</asp:ListItem>
                                                        <asp:ListItem>Program Contractual</asp:ListItem>
                                                    </asp:CheckBoxList>


                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-3" runat="server" visible="False" id="perm">

                                                <div class="form-group">
                                                    <label>Permanent </label>
                                                    <asp:CheckBoxList ID="permCheckBoxList" Visible="False" CssClass="chkChoice"  runat="server">
                                                        <asp:ListItem>Confirmed</asp:ListItem>
                                                        <asp:ListItem>Probationary</asp:ListItem>
                                                    </asp:CheckBoxList>


                                                </div>
                                            </div>
                                            <div class="col-md-3" runat="server" visible="False" id="cont">

                                                <div class="form-group">
                                                    <label>Contractual </label>
                                                    <asp:CheckBoxList ID="contCheckBoxList" runat="server" CssClass="chkChoice"  Visible="False">
                                                        <asp:ListItem>Confirmed</asp:ListItem>
                                                        <asp:ListItem>Probationary</asp:ListItem>

                                                    </asp:CheckBoxList>


                                                </div>
                                            </div>
                                            <div class="col-md-3" runat="server" visible="False" id="cas">

                                                <div class="form-group">
                                                    <label>Program Contractual </label>
                                                    <asp:CheckBoxList ID="casualCheckBoxList" runat="server" CssClass="chkChoice"  Visible="False">
                                                        <asp:ListItem>Confirmed</asp:ListItem>
                                                        <asp:ListItem>Probationary</asp:ListItem>

                                                    </asp:CheckBoxList>


                                                </div>
                                            </div>
                                            <div class="col-md-3" runat="server" visible="False" id="contyear">
                                                <div class="form-group">
                                                    <label>Condition Job Length in Year</label>
                                                    <asp:TextBox ID="contractyearTextBox" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderqty" runat="server"
                                                        Enabled="True" TargetControlID="contractyearTextBox" FilterType="Custom" ValidChars="0123456789"></cc1:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                            <div class="col-md-3" runat="server" visible="False" id="type">
                                <div class="form-group">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Button ID="saveButton" runat="server" CssClass="btn btn-sm btn-info" Visible="False" Text="Submit" OnClick="saveButton_OnClick" />
                                        <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                        <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />
                                        <asp:HiddenField ID="beneftIdHiddenField" runat="server" />
                                        <%--<asp:Button ID="cancelButton" Text="Cancel" CssClass="btn btn-sm warning"  OnClick="cancelButton_OnClick" runat="server" BackColor="#FFCC00" />--%>
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


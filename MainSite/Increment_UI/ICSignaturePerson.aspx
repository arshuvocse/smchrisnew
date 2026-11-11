<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="ICSignaturePerson.aspx.cs" Inherits="Increment_UI_ICSignaturePerson" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    
       <div class="content" id="content">

        <style type="text/css">
            /*AutoComplete flyout */
           
            </style>

        <style>
            

                .SelectchkChoiceDsss label {
            padding-left:1px;
        }

           .chkChoiceDesignation label {
            padding-left: 8px;
            padding-right: 40px;
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
        <asp:UpdatePanel ID="upFormBody" runat="server">




            <ContentTemplate>



                <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>




                <div class="page-heading">
                    <div class="page-heading__container">
                        <h1 class="title" style="font-size: 18px; padding-top: 0px;"> <img src="../Report_Pages/app.png"  width="20px" /> Increment Signature Person Entry </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="HomeButton_OnClick" />
                        <asp:Button ID="detailsViewButton" Text="View" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>
                </div>
                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">

                            <div class="form-row">


  <div class="col-2">
      </div>
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Company</label><span style="color: red">&nbsp;*</span>
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" class="form-control form-control-sm" />
                                    </div>
                                </div>
                                
                                </div>
                            
                            <div class="form-row">
                                
                                <div class="col-2">
      </div>
                                
                                 <div class="col-md-9">
                                                               
                                                                <div class="Label_Title">Grade Information List<span style="color: red">&nbsp;*</span></div>
                                                                <div class="form-group">
                                                                    <div style="overflow: scroll; height: 200px">
                                                                           <br />
                                                                         <asp:CheckBox runat="server" ID="CHKPartCheck" CssClass="SelectchkChoiceDsss" AutoPostBack="True" OnCheckedChanged="CHKPartCheck_OnCheckedChanged" Text=" Select All / Unselect All" />
                                                                        <br />

                                                                        <asp:CheckBoxList ID="CHKGradeList" CssClass="chkChoiceDesignation" RepeatColumns="5" RepeatDirection="Horizontal" runat="server"></asp:CheckBoxList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                    </div>
                            
                            <div class="row">
                                
                                      <div class="col-2">
      </div>
                                            <div class="col-4">
                                                <div class="form-group ">
                                                    <label>Signature Person <span style="color: red">&nbsp;*</span></label>
                                                    <asp:TextBox ID="SearchEmployeeNameTextBoxTextBox" AutoPostBack="True" runat="server" class="form-control form-control-sm" OnTextChanged="SearchEmployeeNameTextBoxTextBox_OnTextChanged"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                    EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                    ServiceMethod="GetCompanyWiseEmployeeInfoOnlyForPromotion" ServicePath="~/WebService.asmx" TargetControlID="SearchEmployeeNameTextBoxTextBox"
                                                    UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                    ShowOnlyCurrentWordInCompletionListItem="true">
                                                </cc1:AutoCompleteExtender>
                                                    <asp:HiddenField ID="repEmpIdHiddenField" runat="server" />

                                                </div>
                                               
                                           </div>
                                                     </div>
                            
<div class="row">
    
      <div class="col-2">
      </div>
                                 <div class="col-md-9">
                                                        <fieldset class="for-panel">
                                                            <legend>Employee Information</legend>
                                                            <div class="row">

                                                                <div class="col-md-6">


                                                                    <div class="form-group">
                                                                        <label>Employee ID: </label>
                                                                        <asp:Label ID="lblEmployeeCode" runat="server" Text=""></asp:Label>
                                                                    </div>


                                                                    <div class="form-group">

                                                                        <label>Employee Name: </label>
                                                                        <asp:Label ID="lblEmp" runat="server" Text=""></asp:Label>

                                                                    </div>
                                                                    
                                                                          <div class="form-group" >
                                                                        <label>Designation: </label>
                                                                        <asp:Label ID="lblDesignation" runat="server" Text=""></asp:Label>
                                                                    </div>
                                </div>
                            </div>
                                                            
                                         
                        </div>
    </div>
                            
                            <div class="row">
    
      <div class="col-2">
      </div>

                            
                                                  <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Button ID="submitButton" Text="Save" CssClass="btn btn-sm btn-info"  runat="server" OnClick="submitButton_Click" />
                                            <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                            <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />
                                            

                                        </div>


                                    </div>
                                
                                </div>
                    </div>
                </ContentTemplate>
                </asp:UpdatePanel>
           </div>
</asp:Content>


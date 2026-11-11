<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="plSeparation.aspx.cs" Inherits="AllPrintLetter_plSeparation" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    
      <style type="text/css">
       

         #cpFormBody_KeyResponGridView > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }


          #cpFormBody_KeyResponGridView  > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }

          </style>
    <link href="../UserSetup/ButtonGrups.css" rel="stylesheet" />
       <div class="container-fluid">
        <div class="page-heading">
            <div class="page-heading__container">
                <h1 class="title" style="font-size: 18px; padding-top: 0px;">
                     <img src="../Assets/print-1.png" /> </h1>
            </div>
            <div class="page-heading__container">
                <asp:Label runat="server" ID="lbl_Mode"></asp:Label>
                <span>
                    <label class="btn infoN" style="font-size: 14px;">Employee ID:  &nbsp;
                        <asp:Label runat="server" class="label w3-tag w3-green" Style="font-size: 14px;" ID="empMasterCode"></asp:Label></label>
                    &nbsp;
                          <asp:HiddenField runat="server" ID="JobLeftIddPK" />
                          <asp:HiddenField runat="server" ID="hdpk" />
                  <label class="btn infoN" style="font-size: 14px;">Employee Name: &nbsp; 
                      <asp:Label runat="server" class="label w3-tag w3-green" Style="font-size: 14px;" ID="lblEmpName"></asp:Label></label>
                    
                    <label class="btn infoN" style="font-size: 12px;">Designation: &nbsp; 
                      <asp:Label runat="server" class="label w3-tag w3-green" Style="font-size: 10px;" ID="lblDesignation"></asp:Label></label>
                </span>
            </div>
            <div class="page-heading__container float-right d-none d-sm-block">
                <asp:Button ID="detailsViewButton" Text="Update Information" Visible="False" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
            </div>
        </div>
        <div class="card">
            <div class="card-body">
                
                
                
                        
                   <br/>

                                  <div class="page-header text-center">
      <h1  class="elegantshd" ><asp:Label ID="lblHeader" runat="server">TO Whom IT May Concern</asp:Label></h1>
    </div>
             <style>
                 .elegantshd {
  color: #131313;
  
  letter-spacing: .15em;
  text-shadow: 2px 2px 4px #000000;
    
     font-family: 'Kreon', serif;
 vertical-align:middle;  text-decoration-style: wavy;
}
             </style>
                
                
                
                <br/>
                <br/>

                <br/>
                
                <div class="row">
                    <div class="col-md-12">
                            <asp:TextBox runat="server"  CssClass="form-control" TextMode="MultiLine"  ID="txtFirstPara"  ></asp:TextBox>
                    </div>
                    
                     
                </div>
               
            <br/>
            <br/>
                
                
              
                 
              
             
                <br/>
                 <div class="row">
                    <div class="col-md-12">
                         <asp:TextBox runat="server" CssClass="form-control form-control-sm"  ID="txtPara2"  ></asp:TextBox>
                        </div>
                     </div>
               


                 <br/>
                <br/>
                 <div class="row">
                    <div class="col-md-12">
                <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="txtpara3"  ></asp:TextBox>
                        </div>
                     </div>
                
                <br/>
                <br/>
                   <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                               
                                                            <Label> Signature Person </Label>
                                                               
                                                                <div class="form-group">
                                                                    <%--<asp:DropDownList ID="EmployeeDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" ></asp:DropDownList>--%>
                                                                    <asp:TextBox ID="EmployeeNameTextBox" AutoPostBack="True" runat="server" class="form-control form-control-sm" OnTextChanged="EmployeeNameTextBox_OnTextChanged"></asp:TextBox>
                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                                        EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                                        ServiceMethod="GetCompanyWiseEmployeeInfoActiveInactiveAll" ServicePath="~/WebService.asmx" TargetControlID="EmployeeNameTextBox"
                                                                        UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                                        ShowOnlyCurrentWordInCompletionListItem="true">
                                                                    </cc1:AutoCompleteExtender>

                                                                    <asp:HiddenField ID="repEmpIdHiddenField" runat="server" />
                                                                    <asp:HiddenField ID="jobIdHiddenField" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                        
                        </div>
              
                 <div class="row">
                                
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Button ID="submitButton" Text=" Save & Print" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="submitButton_Click" />
                                         <asp:Button ID="editButton" Text="Update & Print" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                    <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />
                                        <asp:Button ID="Button1" Text="Back to List" CssClass="btn  btn-link" runat="server" OnClick="cancelButton_OnClick"    />
                                        <asp:Button ID="cancelButton" Text="Cancel" CssClass="btn btn-sm warning" runat="server" OnClick="cancelButton_OnClick" Visible="False" BackColor="#FFCC00" />
                                        <asp:HiddenField runat="server" ID="MaiMasterId"/>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                </div>

                                <div class="col-md-4">
                                </div>
                            </div>
                </div>
            </div>
           </div>
    
</asp:Content>


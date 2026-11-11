<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="plToWhomITMayConcern.aspx.cs" Inherits="AllPrintLetter_plToWhomITMayConcern" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    
      <style type="text/css">
        /*AutoComplete flyout */
     
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
                    <div class="col-md-2">
                            <asp:TextBox runat="server"  CssClass="form-control form-control-sm"  ID="txtOne" Text="This is to certify "></asp:TextBox>
                    </div>
                    
                     <div class="col-md-2">
                         
                 <asp:TextBox runat="server" CssClass="form-control form-control-sm"   ID="txtName"></asp:TextBox>
                         </div>
                    
                     <div class="col-md-5">
                          <asp:TextBox runat="server"  CssClass="form-control form-control-sm"   ID="txtTwo" Text="has been working in SMC Enterprise Limited since "></asp:TextBox>
                         </div>
                    
                      <div class="col-md-2">
                          
                            <asp:TextBox runat="server"  CssClass="form-control form-control-sm"   ID="txtJobLength"></asp:TextBox>
                          </div>
                </div>
               
            <br/>
            <br/>
                
                <div class="row">
                    <div class="col-md-4">
                          <asp:TextBox runat="server" CssClass="form-control form-control-sm"  ID="txtThree" Text="He wants to take a personal visit to "></asp:TextBox>
                        </div>
                    
                     <div class="col-md-2">
                           <asp:TextBox runat="server" CssClass="form-control form-control-sm"  ID="txtFreeThree"></asp:TextBox>
                        </div>
                    
                          <div class="col-md-4">
                           <asp:TextBox runat="server" CssClass="form-control form-control-sm"  ID="txtFour" Text="during the tentative period from"></asp:TextBox>
                        </div>
                    
                     <div class="col-md-2">
                          <asp:TextBox runat="server" CssClass="form-control form-control-sm" placeholder="Enter From Date"  ID="txtFromDate"></asp:TextBox>
                        </div>
                    
                  
                  
                   
                    </div>
                <br/>
                <div class="row">
                       <div class="col-md-1">
                        <asp:TextBox runat="server" CssClass="form-control form-control-sm"  ID="txtTo" Text="to"></asp:TextBox> 
                     
                    </div>
                     <div class="col-md-2">
                          <asp:TextBox runat="server" placeholder="Enter to Date" CssClass="form-control form-control-sm"  ID="txtToDate"></asp:TextBox> 
                        </div>
                    
                    
                     <div class="col-md-2">
                            <asp:TextBox runat="server" ID="txtFive" CssClass="form-control form-control-sm"  Text="His Passport # is"></asp:TextBox>
                        </div>
                    
                          <div class="col-md-2">
                           <asp:TextBox runat="server" CssClass="form-control form-control-sm"  ID="txtPassport"></asp:TextBox>
                        </div>
                    
                </div>
                 
              
                 
               
               
             
              
                <br/>
                <br/>
                 <div class="row">
                    <div class="col-md-12">
                         <asp:TextBox runat="server" CssClass="form-control form-control-sm"  ID="txtPara2" Text="We do not have any objection for the visit. Any help in facilitating him to take the visit would be highly appreciated."></asp:TextBox>
                        </div>
                     </div>
               


                 <br/>
                <br/>
                 <div class="row">
                    <div class="col-md-12">
                <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="txtpara3" Text="If you have any further queries on him please contact with the undersigned."></asp:TextBox>
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
                                         <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                    <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />
                                        <asp:Button ID="Button1" Text="Back to List" CssClass="btn  btn-link" runat="server" OnClick="cancelButton_OnClick"    />
                                        <asp:Button ID="cancelButton" Text="Cancel" CssClass="btn btn-sm warning" runat="server" OnClick="cancelButton_OnClick" Visible="False" BackColor="#FFCC00" />
                                        
                                    </div>
                                </div>
                                <div class="col-md-3">
                                </div>

                                <div class="col-md-4">
                                </div>
                            </div>
                
                      <div id="gridContainer1" style="height: 200px; overflow: auto; width: auto; overflow-y: scroll; overflow-x: scroll;" >
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False" 
                                    CssClass="table table-bordered text-center thead-dark table-hover table-striped" DataKeyNames="ITMayConcernId"  
                                    OnRowCommand="loadGridView_RowCommand" OnRowCreated="loadGridView_OnRowCreated">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         
                                     
                                       
                                        <asp:BoundField DataField="EntryBy" HeaderText="Create By" />
                                        <asp:BoundField DataField="EntryDate" HeaderText="Create Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                     
                                     
                                        
                                           <asp:TemplateField HeaderText="Print">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ViewReportImageButton" runat="server" class="btn btn-white btn-sm  " CommandArgument='<%#Eval("ITMayConcernId") %>'
                                                        CommandName="ViewReport" ImageUrl="~/Assets/report_magnify.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                </div>
            </div>
           </div>
    
</asp:Content>


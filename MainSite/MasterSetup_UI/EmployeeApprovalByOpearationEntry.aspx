<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="EmployeeApprovalByOpearationEntry.aspx.cs" Inherits="MasterSetup_UI_EmployeeApprovalByOpearationEntry" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
     <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
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

        #cpFormBody_presuperGridView > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }


        #cpFormBody_presuperGridView > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }
    </style>


    <div class="content" id="content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                        <div class="icon"> <img src="../Report_Pages/app.png"  width="20px" /> </div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Approval Step Setup Information  </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="HomeButton_OnClick" />
                        <asp:Button ID="detailsViewButton" Text="Go Back" CssClass="btn btn-sm btn-outline-secondary " Visible="False" runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>

                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">

                            <form>
                                <div class="row">

                                    <div class="col-md-2">

                                        <div class="form-group">
                                            <label>Company Name: </label> &nbsp;<label style="color: #a52a2a">*</label>
                                            <asp:DropDownList ID="companyDropDownList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="companyDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                            
                                            <script type="text/javascript">
                                                function pageLoad() {
                                                    $('.SelectMe').chosen({ disable_search_threshold: 5, search_contains: true });





                                                }
</script>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Operation: </label> &nbsp;<label style="color: #a52a2a">*</label>
                                            <asp:DropDownList ID="OperationDropDownList" runat="server" CssClass="form-control form-control-sm SelectMe" AutoPostBack="True" OnSelectedIndexChanged="OperationDropDownList_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Employee: </label> &nbsp;<label style="color: #a52a2a">*</label>
                                            
                                              <asp:DropDownList runat="server"   ID="ddlEmpInfo" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpInfo_OnSelectedIndexChanged" class="form-control form-control-sm SelectMe"   />
                                                 <asp:TextBox ID="directlySuperTextBox"  Visible="False" AutoPostBack="True" runat="server" class="form-control form-control-sm" OnTextChanged="directlySuperTextBox_OnTextChanged"></asp:TextBox>
                                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" DelimiterCharacters=""
                                                                            EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                                            ServiceMethod="GetCompanyWiseEmployeeInfoOnlyForPromotion" ServicePath="~/WebService.asmx" TargetControlID="directlySuperTextBox"
                                                                            UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                                            ShowOnlyCurrentWordInCompletionListItem="true">
                                                                        </cc1:AutoCompleteExtender>
                                     

                                            <asp:HiddenField ID="repEmpIdHiddenField" runat="server" />

                                            <%--<asp:AutoCompleteExtender
                                                            ID="at_txt_JobCirculation"
                                                            TargetControlID="EmployeeNameTextBox"
                                                            runat="server"
                                                            ServiceMethod="GetParticipantList"
                                                            ServicePath="~/WebService.asmx"
                                                            MinimumPrefixLength="2"
                                                            CompletionInterval="10"
                                                            EnableCaching="false"
                                                            CompletionSetCount="1"
                                                            FirstRowSelected="false">
                                                        </asp:AutoCompleteExtender>--%>
                                        
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="col-md-2" style="margin-top: 18px;">
                                            <asp:Button ID="Button1" Text="Add To List" CssClass="btn btn-sm btn btn-outline-info" runat="server" OnClick="Button1_OnClick" />
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                                   CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender"  DataKeyNames="CompanyId, EmpInfoId,MainMenuId">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="SL">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="serialDropDownList" runat="server" CssClass="form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="serialDropDownList_OnSelectedIndexChanged" >
                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                                <asp:ListItem Value="6">6</asp:ListItem>
                                                                <asp:ListItem Value="7">7</asp:ListItem>
                                                                <asp:ListItem Value="8">8</asp:ListItem>
                                                                <asp:ListItem Value="9">9</asp:ListItem>
                                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ShortName" HeaderText="Company Name" />
                                                    <asp:BoundField DataField="Operation" HeaderText="Operation" />
                                                    <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                                  
                                                     <asp:TemplateField HeaderText="head person">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkIsheadperson" runat="server" />
                                                            </ItemTemplate>
                                                         </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="deleteImageButton" runat="server" OnClick="deleteImageButton_OnClick"
                                                                ImageUrl="~/Assets/img/delete.png" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                    </div>
                                </div>
                                
                                <div class="row">
                                     <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Button ID="submitButton" Text="Submit" CssClass="btn btn-sm btn-info"  runat="server" OnClick="submitButton_Click" />
                                            <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                            <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />
                                            <asp:Button ID="cancelButton" Text="Cancel" CssClass="btn btn-sm warning" Visible="False" runat="server" OnClick="cancelButton_OnClick" BackColor="#FFCC00" />

                                        </div>


                                    </div>
                                </div>
                            </form>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>


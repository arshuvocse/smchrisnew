<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="TrainingBudget2App.aspx.cs" Inherits="Training_TrainingBudget2" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    
    

    <style type="text/css">
        .form-group.required .control-label:after {
            color: #d00;
            content: "*";
            position: absolute;
            margin-left: 4px;
            top: 4px;
            font-size: large;
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
    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>
                <div class="container-fluid">
                   <div class="page-heading" style="font-style: italic">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;">Training Budget</h1>

                        </div>
                        <%--<div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="detailsViewButton" Text="Budget List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                        </div>--%>
                        
                        <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                        <asp:Button ID="ListViewButton" Visible="True" Text="Go Back" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>

                    </div>
                    <div class="card">
                        <div class="card-body">
                            <div class="form-row">
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Company</label>
                                        <asp:DropDownList runat="server" ID="ddlCompany" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" CssClass="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                        <asp:DropDownList ID="ddlFinancialYear" OnSelectedIndexChanged="ddlFinancialYear_OnSelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Total Yearly Budgeted Cost</label>
                                        <asp:TextBox runat="server" ID="txtToalYearlyBudget" ReadOnly="True"  AutoPostBack="true" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-3" runat="server" visible="False">
                                    <div class="form-group">
                                        <label>Total  Budget </label>
                                        <asp:TextBox runat="server" ID="txt_toalBudget" AutoPostBack="true" TextMode="Number" CssClass="form-control form-control-sm" Text="0"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <fieldset class="for-panel" runat="server">
                                <legend>Training Budget Information</legend>
                                <div class="form-row">
                                    <div class="col-2">
                                        <div class="form-group required">
                                            <label>Budget Head</label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:TextBox ID="txt_TrainingTitle" TextMode="MultiLine" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-2">
                                        <div class="form-group required">
                                            <label>Expected Result</label> 
                                            <asp:TextBox runat="server" ID="txt_ExpectedOutcome" TextMode="MultiLine" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-1">
                                        <label>Select Quater</label><span style="color: red">&nbsp;*</span>
                                        <asp:CheckBoxList ID="ddlQuater" AutoPostBack="true" OnSelectedIndexChanged="ddlQuater_OnSelectedIndexChanged" runat="server"></asp:CheckBoxList>
                                    </div>

                                    <div class="col-2">
                                        <label>Select Month</label><%--<span style="color: red">&nbsp;*</span>--%><asp:DropDownList ID="ddlMonth" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                    
                                       <div class="col-2">
                                        <div class="form-group">
                                            <label>Employee Category</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlEmpCategoryEx" class="form-control form-control-sm"  LoadOnDemand="true" OnSelectedIndexChanged="ddlEmpCategoryEx_OnSelectedIndexChanged" >
                                                <asp:ListItem Value="3">Both</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Select Grade</label><span style="color: red">&nbsp;*</span>
                                              <br/>
                                             <asp:CheckBox runat="server" Text="Select/Unselect All" AutoPostBack="True" ID="SelectAll" OnCheckedChanged="SelectAll_Checked"/>
                                          
                                            <div style="max-height: 200px; overflow: scroll">
                                                <asp:CheckBoxList ID="chk_Grade" runat="server" CssClass="form-control"></asp:CheckBoxList>
                                            </div>

                                        </div>


                                    </div>

                                </div>


                                <br>


                                <br></br>
                                <div class="form-row">
                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>
                                            Tentative Participant</label><span style="color: red">&nbsp;*</span>
                                            <asp:TextBox ID="txt_totalQty" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnTextChanged="txt_totalQty_OnTextChanged"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2122" runat="server" Enabled="True" FilterType="Custom" TargetControlID="txt_totalQty" ValidChars="0123456789" />
                                        </div>
                                    </div>
                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>
                                            Budget Amount</label><span style="color: red">&nbsp;*</span>
                                            <asp:TextBox ID="txt_budget" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnTextChanged="txt_budget_OnTextChanged"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True" FilterType="Custom" TargetControlID="txt_budget" ValidChars="0123456789" />
                                        </div>
                                    </div>
                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>
                                            Budgeted Cost Per Participant
                                            </label>
                                            <span style="color: red">&nbsp;*</span>
                                            <asp:TextBox ID="txt_CostPerParticipant" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True" FilterType="Custom" TargetControlID="txt_CostPerParticipant" ValidChars="0123456789." />
                                        </div>
                                    </div>
                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>
                                            Select</label><span style="color: red">&nbsp;*</span>
                                            <asp:RadioButtonList ID="radExIn" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="External">External </asp:ListItem>
                                                <asp:ListItem Value="Internal">Internal </asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>
                                            Select</label><span style="color: red">&nbsp;*</span>
                                            <asp:RadioButtonList ID="rad_fLocal" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="Foeign">Foreign </asp:ListItem>
                                                <asp:ListItem Value="Local">Local </asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>
                                            Remarks</label>
                                            <asp:TextBox ID="txt_remarks" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-row">
                                    <%--  <div class="col-3">
                                        <label></label>
                                        <br>
                                        </br>
                                     
                                     <asp:Button ID="addToList" OnClick="addToList_OnClick" Text="Add to List" runat="server" class="btn btn-sm btn-info"></asp:Button>
                                    </div>--%>
                                    <div runat="server" class="col-3" visible="False">
                                        <div class="form-group">
                                            <label>
                                            Referance</label>
                                            <asp:TextBox ID="txt_ref" runat="server" CssClass="form-control form-control-sm" Text=" "></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <br />
                                            <asp:HiddenField ID="amountHiddenField" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-row">
                                    <div class="col-2 ">
                                    </div>
                                    <div class="col-2 ">
                                    </div>
                                    <div class="col-2 ">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-outline-success btn-block disabled btn-sm" OnClick="addToList_OnClick" Text="Add to list" />
                                        </div>
                                        <div class="col-2 ">
                                        </div>
                                        <div class="col-2 ">
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <label>
                                Budget List</label>
                                <hr />
                                <asp:GridView ID="gv_training" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark" DataKeyNames="TrainingBudget2DetailsId" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL#">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %><%--<asp:HiddenField runat="server" ID="hdpkd" Value='<%#Eval("BoardDetailsId") %>'/>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Budget Head">
                                            <ItemTemplate>
                                                <asp:Label ID="TrainingTitle" runat="server" Text='<%#Eval("TrainingTitle") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Result">
                                            <ItemTemplate>
                                                <asp:Label ID="ExpectedResult" runat="server" Text='<%#Eval("ExpectedResult") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Grade">
                                            <ItemTemplate>
                                                <asp:Label ID="Grade" runat="server" Text='<%#Eval("Grade") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Category" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="Category" runat="server" Text='<%#Eval("Category") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quater">
                                            <ItemTemplate>
                                                <asp:Label ID="Quater" runat="server" Text='<%#Eval("Quater") %>'></asp:Label>
                                                <%--<asp:Label ID="QuaterId" Visible="False" runat="server" class="form-control form-control-sm" Text='<%#Eval("QuaterId") %>'></asp:Label>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="External/Internal">
                                            <ItemTemplate>
                                                <asp:Label ID="InternalExternal" runat="server" Text='<%#Eval("InternalExternal") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Foreign/Local">
                                            <ItemTemplate>
                                                <asp:Label ID="ForeignLocal" runat="server" Text='<%#Eval("ForeignLocal") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Month">
                                            <ItemTemplate>
                                                <asp:Label ID="Month" runat="server" Text='<%#Eval("Month") %>'></asp:Label>
                                                <asp:Label ID="MonthId" runat="server" Text='<%#Eval("MonthId") %>' Visible="False"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tatal Participant">
                                            <ItemTemplate>
                                                <asp:Label ID="TotalParticipant" runat="server" Text='<%#Eval("TotalParticipant") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Budgeted Cost">
                                            <ItemTemplate>
                                                <asp:Label ID="BudgetCostParticipant" runat="server" Text='<%#Eval("BudgetCostParticipant")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Budget Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="Budget" runat="server" Text='<%#Eval("Budget") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Referance" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="Referance" runat="server" Text='<%#Eval("Referance") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label ID="Remarks" runat="server" Text='<%#Eval("Remarks") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Operation">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lb_Remove" runat="server" OnClick="lb_Remove_OnClick">Remove</asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_OnClick">Edit</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <br />


                                </br>
                           
                      


                            



                            </fieldset>

                                              <div class="form-row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Approval Status </label>
                                        &nbsp;<label style="color: #a52a2a">*</label>
                                        <asp:RadioButtonList ID="actionRadioButtonList" runat="server" RepeatDirection="Horizontal">
                                                    
                                            
                                                    
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Comments</label>
                                        <asp:TextBox runat="server" ID="commentsTextBox" CssClass="form-control" ReadOnly="False"  TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                              <div class="col-md-6">
                                                 <div style="max-height: 150px; overflow: scroll">
                                                <asp:GridView ID="AppLogCommentGridView" CssClass="table table-bordered text-center thead-dark" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="JobReqFormAppLogId">
                                                    <Columns>
                                                      
                                                     <asp:BoundField DataField="PreEmp" HeaderText="Initiator" HtmlEncode="False" />
                                                        <asp:BoundField DataField="ForEmp" HeaderText="Approval Person" HtmlEncode="False" />
                                                        <%--<asp:BoundField DataField="Version" HeaderText="Version" HtmlEncode="False" />--%>
                                                        <asp:BoundField DataField="Comments" HeaderText="Comments" HtmlEncode="False" />
                                                        <asp:BoundField DataField="ActionStatus" HeaderText="Action Status" HtmlEncode="False" />
                                                        

                                                        <asp:BoundField DataField="ApproveBy" HeaderText="Approved By" HtmlEncode="False" />
                                                        <asp:BoundField DataField="ApproveDate" HeaderText="Approved Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                     
                                                       
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                            </div>
            
                             <link href="../UserSetup/ButtonGrups.css" rel="stylesheet" />
                                      <div class="ui-group-buttons">
 <asp:LinkButton ID="submitButton" Text="Submit" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="Button2_OnClick" OnClientClick="return confirm('Are you sure you want to Submit ?')" style="box-shadow: 0 0 3px 1px rgba(0,0,0,.35);" ><i class="fa fa-check" aria-hidden="true"></i> &nbsp; Submit

&nbsp; </asp:LinkButton>  
                                    <asp:LinkButton ID="Button1" Text="Submit" CssClass="btn btn-sm btn-success" Visible="False" OnClientClick="return confirm('Are you sure you want to Submit ?')" style="box-shadow: 0 0 3px 1px rgba(0,0,0,.35);"  runat="server" OnClick="Button1_OnClick" ><i class="fa fa-check" aria-hidden="true"></i> &nbsp; Submit

&nbsp; </asp:LinkButton>  
                                            <div class="or or-sm" runat="server"   id="orBTN"></div>
                                    <asp:LinkButton ID="Button2a"   CssClass="btn btn-sm btn-danger"  OnClientClick="return confirm('Are you sure you want to Reject ?')"  runat="server" OnClick="Button2a_OnClick" ><i class="fa fa-times" aria-hidden="true"></i>
&nbsp; Reject </asp:LinkButton> 
                                      </div>
                                    <asp:HiddenField ID="entryempinfoIdHiddenField" runat="server" />
                                    <asp:HiddenField ID="actionstatusHiddenField" runat="server" />
                        </div>

                    </div>

                </div>
                <asp:HiddenField runat="server" ID="hdpk" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>


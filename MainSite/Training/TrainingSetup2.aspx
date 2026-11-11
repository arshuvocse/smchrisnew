<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="TrainingSetup2.aspx.cs" Inherits="Trainning_TrainingSetup2" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="trainingSetupContent" ContentPlaceHolderID="cpFormBody" runat="Server">

     <style type="text/css">
        /*AutoComplete flyout */
      
    </style>
    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>

                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;">Training Setup</h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="detailsViewButton" Text="Training Information " CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_Click" />
                        </div>
                    </div>

                    <div class="card">
                        <div class="card-body">
                            <div class="form-row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Company</label>
                                        <asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" class="form-control form-control-sm" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                        <asp:DropDownList ID="ddlFinancialYear" AutoPostBack="true" OnSelectedIndexChanged="ddlFinancialYear_SelectedIndexChanged" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Quater</label>
                                        <%--<asp:DropDownList ID="ddlQuater" runat="server" class="form-control form-control-sm"></asp:DropDownList>--%>
                                        <asp:DropDownList ID="ddlQuater" AutoPostBack="true" OnSelectedIndexChanged="ddlQuater_SelectedIndexChanged"  CssClass="form-control  form-control-sm" runat="server"></asp:DropDownList>
                                    </div>
                                    
                                </div>
                                
                            </div>
                            <div  class="form-row">
                                <div class="form-group">
                                    <label>From Requisituin:</label>
                                    <asp:CheckBox  runat="server" ID="fromReq" OnCheckedChanged="fromReq_CheckedChanged" AutoPostBack="true" />
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="col-md-3"  runat="server">
                                    <div class="form-group">
                                        <label>Training Title</label>
                                        <%--txt_TrainingTitle--%>
                                        <asp:DropDownList ID="ddl_training" runat="server"  CssClass="form-control  form-control-sm"></asp:DropDownList>  
                                             
                                    </div>
                                </div>

                                
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Trainig Details</label>
                                        <asp:TextBox ID="txt_TrainigDetails" TextMode="MultiLine" runat="server" CssClass="form-control  "></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Trainig Organization</label>
                                        <asp:DropDownList ID="ddlTrainingOrg" AutoPostBack="true" OnSelectedIndexChanged="ddlTrainingOrg_SelectedIndexChanged" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Location</label>
                                        <asp:DropDownList ID="ddlLocation" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="form-row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                         <label>Trainner</label>
                                          <asp:DropDownList ID="ddlTrainer" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                         
                                    </div>
                                </div>
                               
                                <div class="col-md-1">
                                    <div class="form-group">
                                          <label>&nbsp</label>
                                        <asp:Button runat="server" ID="AddTrainner" OnClick="AddTrainner_Click"  CssClass="form-control btn btn-success btn-sm" Text="Add"></asp:Button>
                                    </div>
                                </div>
                                 <div class="col-md-1">
                                    <div class="form-group">
                                          <label>Not Listed</label>
                                       <asp:CheckBox  runat="server" ID="notListedCheck" AutoPostBack="true" OnCheckedChanged="notListedCheck_CheckedChanged"/>
                                    </div>
                                </div>
                                 <div class="col-md-2" id="notListedNameDiv" runat="server" visible="false">
                                    <div class="form-group">
                                         <label>Name</label>
                                          <asp:TextBox ID="txt_NotListedTrainer" runat="server" CssClass="form-control  form-control-sm"></asp:TextBox>
                                         
                                    </div>
                                </div>

                                <div class="col-md-2"  id="notListedDetailsDiv" runat="server" visible="false">
                                    <div class="form-group">
                                         <label>Details</label>
                                          <asp:TextBox ID="txt_NotListedTrainerDetails" runat="server" CssClass="form-control  form-control-sm"></asp:TextBox>
                                         
                                    </div>
                                </div>
                                <div class="col-md-1" >
                                    <div class="form-group">
                                          <label>&nbsp</label>
                                        <asp:Button runat="server" ID="AddNotListed"  visible="false" OnClick="AddNotListed_Click"  CssClass="form-control btn btn-success btn-sm" Text="Add"></asp:Button>
                                    </div>
                                </div>
                            </div>
                         
                            <div class="form-row">
                                <div class="col-md-12">
                                    <asp:GridView ShowFooter="true" ID="gvTrainner" Width="100%"  CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="false" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL#">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                        <%--<asp:HiddenField runat="server" ID="hdpkd" Value='<%#Eval("BoardDetailsId") %>'/>--%>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Trainner">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_Trainner" runat="server" class="form-control form-control-sm" Text='<%#Eval("TrainerName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Details">
                                                <ItemTemplate>
                                                     
                                                    <asp:Label Visible="false" ID="txt_trainerID" runat="server" class="form-control form-control-sm" Text='<%#Eval("TrainerId") %>'></asp:Label>
                                                    <asp:Label ID="txt_TrainnerDetails" runat="server" class="form-control form-control-sm" Text='<%#Eval("TrainerDetails") %>'></asp:Label>
                                               
                                                       <%--<asp:HiddenField runat="server" ID="trainnerId"   />--%>
                                                     </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Operation">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lb_Remove" OnClick="lb_Remove_Click" runat="server"
                                                        >Remove</asp:LinkButton>
                                                </ItemTemplate>
                                             
                                                
                                            </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                </div>
                            </div>
                            
                            <div class="form-row">
                                   <div class="col-md-2">
                                    <div class="form-group">
                                         <label>Total Participant <span style="color:red">*</span></label>
                                          <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control form-control-sm" AutoPostBack="True"> </asp:TextBox>
                                        
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                         <label>Cost per Participant<span style="color:red">*</span></label>
                                          <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control  form-control-sm"></asp:TextBox>
                                      
                                    </div>
                                </div>

                                 <div class="col-md-2">
                                    <div class="form-group">
                                         <label>Total Cost <span style="color:red">*</span> </label>
                                          <asp:TextBox ID="TextBox3" TextMode="Number" runat="server" CssClass="form-control  form-control-sm"></asp:TextBox>
                                         
                                    </div>
                                </div>
                               <div class="col-md-2">
                                    <div class="form-group">
                                         <label>Other Cost <span style="color:red">*</span> </label>
                                          <asp:TextBox ID="TextBox4" TextMode="Number" runat="server" CssClass="form-control  form-control-sm"></asp:TextBox>
                                         
                                    </div>
                                </div>
                                  <div class="col-md-2">
                                    <div class="form-group">
                                         <label>Grand Total <span style="color:red">*</span> </label>
                                          <asp:TextBox ID="TextBox5" TextMode="Number" runat="server" CssClass="form-control  form-control-sm"></asp:TextBox>
                                         
                                    </div>
                                </div>
                                 
                            </div>
                            <div class="form-row">
                                   <div class="col-md-3">
                                    <div class="form-group">
                                         <label>Start Date <span style="color:red">*</span></label>
                                          <asp:TextBox ID="txt_StartDate" runat="server" CssClass="form-control form-control-sm" AutoPostBack="True"> </asp:TextBox>
                                         <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                Format="dd-MMM-yyyy"  CssClass="MyCalendar"
                                                TargetControlID="txt_StartDate" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                         <label>End Date <span style="color:red">*</span></label>
                                          <asp:TextBox ID="txt_EndDate" runat="server" CssClass="form-control  form-control-sm"></asp:TextBox>
                                         <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                Format="dd-MMM-yyyy"  CssClass="MyCalendar"
                                                TargetControlID="txt_EndDate" />
                                    </div>
                                </div>

                                 <div class="col-md-3">
                                    <div class="form-group">
                                         <label>Duration(Houre) <span style="color:red">*</span> </label>
                                          <asp:TextBox ID="txt_Duration" TextMode="Number" runat="server" CssClass="form-control  form-control-sm"></asp:TextBox>
                                         
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                         <label>Evaluation</label><br/>
                                      <asp:RadioButtonList RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server" ID="radTrainingEvaluation">
                                        <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                        <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                        
                                    </asp:RadioButtonList>
                                         
                                    </div>
                                </div>
                                 
                            </div>
                               <div class="form-row">
                                   <div class="col-3">
                                    <div class="form-group">
                                        <label> Target Employee</label>
                                        <asp:TextBox  runat="server" ID="txt_employee"  CssClass="form-control form-control-sm"></asp:TextBox>
                                         <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters=""
                                            EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                            ServiceMethod="GetEployeeAutoComp" ServicePath="~/WebService.asmx" TargetControlID="txt_employee"
                                            UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                            ShowOnlyCurrentWordInCompletionListItem="true">
                                        </cc1:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-1">
                                    <br/>
                                    <br/>
                                    <asp:Button  runat="server" CssClass="btn btn-success btn-xs" Text="Add To List"  ID="addEmployeeToList" OnClick="addEmployeeToList_OnClick"/>
                                </div>
                                
                            </div>
                            <asp:GridView ID="gv_EmpDetails" Width="100%" CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="false" runat="server">

                                        <Columns>
                                            <asp:TemplateField HeaderText="SL#">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quater">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_Quater" runat="server" class="form-control form-control-sm" Text='<%#Eval("quater") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="Employee">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_employee" runat="server" class="form-control form-control-sm" Text='<%#Eval("employee") %>'></asp:Label>

                                                    <asp:HiddenField runat="server" ID="deptId" Value='<%#Eval("employeeId") %>' />
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                          


                                            <asp:TemplateField HeaderText="Operation">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lb_RemoveEmp" OnClick="lb_RemoveEmp_OnClick" runat="server">Remove</asp:LinkButton>
                                                </ItemTemplate>


                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                    <asp:HiddenField runat="server" ID="hdpk"/>
                                    <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick"  Text="Submit " CssClass="btn btn-sm btn-success"/>
                                     <asp:Button ID="cancelButton" Text="Cancel" OnClick="cancelButton_OnClick"  CssClass="btn btn-sm warning" runat="server"  BackColor="#FFCC00" />
                        </div>
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>


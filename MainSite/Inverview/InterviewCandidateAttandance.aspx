<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MainMasterPage.master" CodeFile="InterviewCandidateAttandance.aspx.cs" Inherits="Inverview_InterviewCandidateAttandance" %>
<%@ Register TagPrefix="cc1" Namespace="MKB.TimePicker" Assembly="TimePicker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d25e9f59e49c4d2f" %>
<%@ Register Src="~/Report_Pages/IVSearchControlReport.ascx" TagPrefix="uc1" TagName="IVSearchControl" %>



<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .form-group.required .control-label:after { 
            color: #d00;
            content: "*";
            position: absolute;
            margin-left: 4px;
            top:4px;
            font-size: large;
        }
    </style>
    
        
    <style>
        #cpFormBody_gv_InterviewCList> tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }


          #cpFormBody_gv_InterviewCList  > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }
    </style>
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>
                    <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <div class="container-fluid">
                <div class="page-heading">
                    <div class="page-heading__container">
                        <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png" width="20px"  /> Interview Candidate Attendance</h1>
                    </div>
                     <div class="page-heading__container float-right d-none d-sm-block">
                          
                             <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                       
                        </div>
                </div>

                <div class="card">
                    <div class="card-body">

                        <div class="form-row">
<%--                            <div class="col-3">
                                <div class="form-group required">
                                    <label class="control-label">Company</label>
                                    <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" class="form-control form-control-sm" />
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group required">
                                    <label class="control-label">Job Circulation</label>
                                    <asp:TextBox runat="server" AutoPostBack="True" ID="txt_JobCirculation" class="form-control form-control-sm" OnTextChanged="txt_JobCirculation_OnTextChanged"></asp:TextBox>
                                    <asp:HiddenField runat="server" ID="hdJobID"/>
                                    <ajaxToolkit:AutoCompleteExtender
                                        ID="at_txt_JobCirculation"
                                        TargetControlID="txt_JobCirculation"
                                        runat="server"
                                        ServiceMethod="GetJobCirculationAuto"
                                        ServicePath="~/WebService.asmx"
                                        MinimumPrefixLength="1"
                                        CompletionInterval="1000"
                                        EnableCaching="false"
                                        CompletionSetCount="1"
                                        FirstRowSelected="True">
                                    </ajaxToolkit:AutoCompleteExtender>
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group required">
                                    <label class="control-label">Job Title</label>
                                    <asp:Label runat="server" ID="txt_JobTitle" class="form-control form-control-sm"></asp:Label>
                                </div>
                            </div>--%>
                           <uc1:IVSearchControl runat="server" ID="IVSearchControl" />
                           <%-- <div class="form-row"
                                >--%>
                            <div class="col-3">
                                <div class="form-group">
                                    <label class="control-label">Remarks</label>
                                   <asp:TextBox runat="server" ID="txt_remarks" AutoPostBack="True" OnTextChanged="txt_remarks_OnTextChanged" class="form-control " TextMode="MultiLine" Rows="2"></asp:TextBox>
                                </div>
                            <%--</div>    --%>
                                
                            </div>
                            <div class="col-3" runat="server" Visible="False">
                                <div class="form-group required">
                                    <label class="control-label">Interview Phase</label>
                                    <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlInterviewPhase"  class="form-control form-control-sm" >
                                        <asp:ListItem Text="-----Select" Value="-1" runat="server"></asp:ListItem>
                                        <asp:ListItem Text="1" Value="1" runat="server"></asp:ListItem>
                                        <asp:ListItem Text="2" Value="2" runat="server"></asp:ListItem>
                                        <asp:ListItem Text="3" Value="3" runat="server"></asp:ListItem>
                                        <asp:ListItem Text="4" Value="4" runat="server"></asp:ListItem>
                                        <asp:ListItem Text="5" Value="5" runat="server"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                        </div>
                      <asp:LinkButton runat="server" ID="btn_LoadList" OnClick="btn_LoadList_OnClick" CssClass="btn btn-sm btnMyDesignSearch"><i class="fa fa-search-plus"></i>&nbsp; Search Matching Candidate List</asp:LinkButton>
 
                        
                        <br/>
                        <br/>
                        <div>
                            <asp:GridView Width="100%" ID="gv_InterviewCList" runat="server" ShowFooter="true"
                                          AutoGenerateColumns="false" CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender"
                            >
                                <Columns>
                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            <asp:HiddenField runat="server" ID="hdpkd" Value='<%#Eval("InterviewCandidateAttandanceId") %>' />
                                            <asp:HiddenField runat="server" ID="hdCandidateID" Value='<%#Eval("CandidateID") %>' />
                                            <asp:HiddenField runat="server" ID="hdCompanyId" Value='<%#Eval("CompanyId") %>' />
                                            <asp:HiddenField runat="server" ID="hdJobID" Value='<%#Eval("JobID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        
                                    <asp:TemplateField >
                                        <HeaderTemplate>
                                            <asp:CheckBox runat="server" AutoPostBack="True" ID="chkAll" OnCheckedChanged="chkAll_OnCheckedChanged"/>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="chkSingle" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Candidate Name">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_CandidateName" runat="server"  Text='<%#Eval("CandidateName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Address">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_Address" runat="server" Text='<%#Eval("Address") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Phone No.">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_PhoneNo" runat="server" Text='<%#Eval("PhoneNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Email Adress">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_EmailAdress" runat="server" Text='<%#Eval("EmailAdress") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Years Of Exp.">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_TotalYearsOfExp" runat="server" Text='<%#Eval("TotalYearsOfExp") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Last Organization">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_LastOrganization" runat="server" Text='<%#Eval("LastOrganization") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        
                                    <asp:TemplateField HeaderText="Last Position">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_LastPosition" runat="server" Text='<%#Eval("LastPosition") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Applying Position">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_Position" runat="server" Text='<%#Eval("Position") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Reporting Time" runat="server" Visible="False">
                                        <ItemTemplate>
                                            <cc1:TimeSelector ID="txt_InterviewTime" runat="server" DisplaySeconds="false" DisplayButtons="false">
                                            </cc1:TimeSelector>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:Textbox ID="txt_Remarks" runat="server"  class="form-control" TextMode="MultiLine" Rows="2"  Value='<%#Eval("Remarks") %>'></asp:Textbox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>
                       
                        
                        <br />
                       
                            <asp:HiddenField runat="server" ID="hdpk" />
                          <link href="../UserSetup/ButtonGrups.css" rel="stylesheet" />
                                                <div class="ui-group-buttons">
                            <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" Text="Submit " CssClass="btn btn-sm btn-info" />
                                                      <div class="or or-sm"></div>
                            <asp:Button ID="cancelButton" Text="Cancel" OnClick="cancelButton_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                                                      </div>
                                <br />
                            <br />     <br />
                        <br />     <br />
                        <br />
                        </div>
                     </div>
                    </div>
                </div>








                    
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>




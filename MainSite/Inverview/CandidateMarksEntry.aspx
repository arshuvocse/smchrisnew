<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MainMasterPage.master" CodeFile="CandidateMarksEntry.aspx.cs" Inherits="Inverview_CandidateMarksEntry" %>

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
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>

                <div class="container-fluid">
                <div class="page-heading">
                    <div class="page-heading__container">
                        <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" />&nbsp;Candidate Marks Entry</h1>
                    </div>
                </div>

                <div class="card">
                    <div class="card-body">

                        <div class="form-row">
                            <div class="col-3">
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
                            
<%--                            <div class="col-3">
                                <div class="form-group">
                                    <label>Activity</label><br />
                                    <asp:RadioButtonList RepeatLayout="Table" RepeatDirection="Horizontal" runat="server" ID="rad_InterviewActivity" AutoPostBack="True" >
                                    </asp:RadioButtonList>
                                </div>
                            </div>--%>

                        </div>
                        <asp:Button runat="server" ID="btn_LoadList" OnClick="btn_LoadList_OnClick"  Text="Search Matching Candidate List" CssClass="btn btn-sm btn-outline-secondary"/>
                        <br/>
                        <br/>
                        <div>
                            <asp:GridView Width="100%" ID="gv_InterviewCList" runat="server" ShowFooter="true"
                                          AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark"
                            >
                                <Columns>
                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            <asp:HiddenField runat="server" ID="hdpkd" Value='<%#Eval("InterviewCandidateTotalMarksId") %>' />
                                            <asp:HiddenField runat="server" ID="hdCandidateID" Value='<%#Eval("CandidateID") %>' />
                                            <asp:HiddenField runat="server" ID="hdJobID" Value='<%#Eval("JobID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        
                                    <asp:TemplateField >
                                        <HeaderTemplate>
                                            <asp:CheckBox runat="server" AutoPostBack="True" ID="chkAll"  OnCheckedChanged="chkAll_OnCheckedChanged"/>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="chkSingle" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Candidate Name">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_CandidateName" runat="server" Text='<%#Eval("CandidateName") %>'></asp:Label>
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
                                    <asp:TemplateField HeaderText="Written">
                                        <ItemTemplate>
                                            <asp:TextBox CssClass="form-control input-sm" ID="txt_WrittenMarks" runat="server" Text='<%#Eval("WrittenMarks") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Written OutOf">
                                        <ItemTemplate>
                                            <asp:TextBox CssClass="form-control input-sm" ID="txt_WrittenMarksOutOf" runat="server" Text='<%#Eval("WrittenMarksOutOf") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    
                                    <asp:TemplateField HeaderText="Viva" >
                                        <ItemTemplate>
                                            <asp:TextBox CssClass="form-control input-sm" ID="txt_VivaMarks" runat="server" Text='<%#Eval("VivaMarks") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Viva OutOf">
                                        <ItemTemplate>
                                            <asp:TextBox CssClass="form-control input-sm" ID="txt_VivaMarksOutOf" runat="server" Text='<%#Eval("VivaMarksOutOf") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    
                                    <asp:TemplateField HeaderText="Other">
                                        <ItemTemplate>
                                            <asp:TextBox CssClass="form-control input-sm" ID="txt_OtherMarks" runat="server" Text='<%#Eval("OtherMarks") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Other OutOf">
                                        <ItemTemplate>
                                            <asp:TextBox CssClass="form-control input-sm" ID="txt_OtherMarksOutOf" runat="server" Text='<%#Eval("OtherMarksOutOf") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>
                        <br />
                        <br />
                        <div>
                            <asp:HiddenField runat="server" ID="hdpk" />
                            <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" Text="Submit " CssClass="btn btn-sm btn-info" />
                            <asp:Button ID="cancelButton" Text="Cancel" OnClick="cancelButton_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                        </div>

                    </div>
                </div>








                    
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

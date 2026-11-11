<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MainMasterPage.master" CodeFile="InterviewBoardMemberMarksEntry.aspx.cs" Inherits="Inverview_InterviewBoardMemberMarksEntry" %>

<%@ Register Src="~/Report_Pages/IVSearchControlReport.ascx" TagPrefix="uc1" TagName="IVSearchControl" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<%--<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>--%>
<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .modalBackground {
            background-color: #262626 !important;
            filter: alpha(opacity=50) !important;
            opacity: 0.5 !important;
        }

        .modalPopup {
            background-color: #FFFFFF !important;
            width: 300px;
            border-left: 3px solid #4D97C2 !important;
            border-radius: 12px;
            -webkit-box-shadow: 1px 1px 4px 1px rgba(0,0,0,0.41) !important;
            -moz-box-shadow: 1px 1px 4px 1px rgba(0,0,0,0.41) !important;
            box-shadow: 1px 1px 4px 1px rgba(0,0,0,0.41) !important;
        }

        .form-group.required .control-label:after {
            color: #d00 !important;
            content: "*" !important;
            position: absolute !important;
            margin-left: 4px !important;
            top: 4px !important;
            font-size: large !important;
        }
    </style>

    <style>
        #cpFormBody_gv_InterviewCList > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }


        #cpFormBody_gv_InterviewCList > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }



        #cpFormBody_gv_InterviewCMarks > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }


        #cpFormBody_gv_InterviewCMarks > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }


        #cpFormBody_gv_InterviewCMarks_W > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }


        #cpFormBody_gv_InterviewCMarks_W > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }

        #cpFormBody_pnl_V > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }


        #cpFormBody_pnl_V > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }



        #cpFormBody_gv_InterviewCMarks_V > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }


        #cpFormBody_gv_InterviewCMarks_V > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #5B799E;
            /*background-color: #98A9C0;*/
        }



        #cpFormBody_gv_InterviewCMarks_O > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
        }


        #cpFormBody_gv_InterviewCMarks_O > tbody > tr > th {
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
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png" width="20px"  /> Committee Member Marking</h1>
                        </div>
                            <div class="page-heading__container float-right d-none d-sm-block">
                          
                             <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                       
                        </div>
                    </div>

                    <div class="card">
                        <div class="card-body">

                            <div class="form-row">
                                <%--                                <div class="col-3">
                                    <div class="form-group required">
                                        <label class="control-label">Company</label>
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" class="form-control form-control-sm" />
                                    </div>
                                </div>--%>
                                <%--                                <div class="col-3">
                                    <div class="form-group required">
                                        <label class="control-label">Job Circulation</label>
                                        <asp:TextBox runat="server" AutoPostBack="True" ID="txt_JobCirculation" class="form-control form-control-sm" OnTextChanged="txt_JobCirculation_OnTextChanged"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hdJobID" />
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
                                </div>--%>
                                <%--                                <div class="col-3">
                                    <div class="form-group required">
                                        <label class="control-label">Job Title</label>
                                        <asp:Label runat="server" ID="txt_JobTitle" class="form-control form-control-sm"></asp:Label>
                                    </div>
                                </div>--%>
                                 <uc1:IVSearchControl runat="server" ID="IVSearchControl" />

                                <div class="col-3" runat="server" visible="False">
                                    <div class="form-group required">
                                        <label class="control-label">Interview Phase</label>
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlInterviewPhase" class="form-control form-control-sm">
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
                            <asp:LinkButton runat="server" ID="btn_LoadList" OnClick="btn_LoadList_OnClick" CssClass="btn btn-sm btnMyDesignSearch"><i class="fa fa-search-plus"></i>&nbsp; Search Committee Member List</asp:LinkButton>
 

                            <br />
                            <br />

                            <div>
                                <asp:GridView Width="100%" ID="gv_InterviewCList" runat="server" ShowFooter="true"
                                    AutoGenerateColumns="false"  CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL#">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                                <asp:HiddenField runat="server" ID="hdpkd" Value='<%#Eval("BoardDetailsId") %>' />
                                                <asp:HiddenField runat="server" ID="hdEmployeeId" Value='<%#Eval("EmployeeId") %>' />
                                                <asp:HiddenField runat="server" ID="hdCompanyId" Value='<%#Eval("CompanyId") %>' />
                                                <asp:HiddenField runat="server" ID="hdJobID" Value='<%#Eval("JobTitleId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Member Type">
                                            <ItemTemplate>
                                                <asp:Label ID="txt_MemberType" runat="server" Text='<%#Eval("MemberType") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="txt_Name" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="txt_Designation" runat="server" Text='<%#Eval("Designation") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="txt_Department" runat="server" Text='<%#Eval("Department") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Company">
                                            <ItemTemplate>
                                                <asp:Label ID="txt_Company" runat="server" Text='<%#Eval("Company") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Viva Marks">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lb_MarksEntry" CssClass="btn btn-primary btn-sm" BackColor="#0069D9" BorderStyle="None" OnClick="lb_MarksEntry_OnClick" Text="Viva Marks"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Written Marks">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lb_WrittenMarksEntry" CssClass="btn btn-info btn-sm" BorderStyle="None" OnClick="lb_WrittenMarksEntry_OnClick" Text="Written Marks"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Viva Marks" Visible="False">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lb_VivaMarksEntry" CssClass="btn btn-primary btn-sm" BackColor="#7089A7" BorderStyle="None" OnClick="lb_VivaMarksEntry_OnClick" Text="Viva Marks"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Others Marks">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lb_OthersMarksEntry" CssClass="btn btn-primary btn-sm" BackColor="#660066" BorderStyle="None" OnClick="lb_OthersMarksEntry_OnClick" Text="Others Marks"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="form-row">
                                <div class="col-6">
                                    <div class="form-group">

                                        <asp:LinkButton runat="server" ID="btnModalUp" ForeColor="black" CssClass="btn btn-primary btn-sm" BackColor="#D8E0E5" BorderStyle="None" OnClick="btnModalUp_OnClick" Text="File Upload"></asp:LinkButton>
                                        <div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                             <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                        </div>
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />

                    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>


    <div>
        <ajaxToolkit:ModalPopupExtender ID="mpe_1" runat="server" TargetControlID="hnd_Test" PopupControlID="pnl_1"
            BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:HiddenField ID="hnd_Test" runat="server"></asp:HiddenField>
        <asp:Panel ID="pnl_1" runat="server" Style="display: none; padding: 20px; overflow: scroll" Height="650px" Width="50%" CssClass="modalPopup">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="page-header text-center">
                        <h1 class="elegantshd">
                            <asp:Label ID="Label1" Text="Interview Candidate Marks Entry For Board Member" runat="server"></asp:Label></h1>

                        <h3 style="text-align: center;"><span>
                            <asp:Label ForeColor="#8B0000" Font="Bold" runat="server" ID="m_MemberName"></asp:Label></span></h3>
                    </div>


                    <div>
                        <hr />
                        <asp:HiddenField runat="server" ID="m_hdpkd"></asp:HiddenField>
                        <div style="height: 400px; overflow: scroll">
                            <asp:GridView Width="100%" ID="gv_InterviewCMarks" runat="server" ShowFooter="true"
                                AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark" DataKeyNames="VivaId,CandidateID, VoutOff, VivaDetailsMarkId">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>

                                            <%--<asp:HiddenField runat="server" ID="hdInterviewMarksDetailsId" Value='<%#Eval("InterviewMarksDetailsId") %>' />--%>
                                            <asp:HiddenField runat="server" ID="hdCandidateID" Value='<%#Eval("CandidateID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Candidate Name">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_CandidateName" runat="server" Text='<%#Eval("CandidateName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Viva">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txt_Attitude" runat="server" ReadOnly="True" class="form-control form-control-sm" Text='<%#Eval("VivaInfo") %>'></asp:TextBox>

                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Viva Marks Out Of">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txt_VoutOff" runat="server" ReadOnly="True" class="form-control form-control-sm" Text='<%#Eval("VoutOff") %>'></asp:TextBox>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Marks">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txt_marks" runat="server" AutoPostBack="True" OnTextChanged="txt_marks_OnTextChanged" class="form-control form-control-sm" Text='<%#Eval("MainMarks") %>'></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                TargetControlID="txt_marks" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                </Columns>
                            </asp:GridView>


                        </div>
                        <br />

                        <asp:Button runat="server" ID="btnYes" OnClick="btnYes_Click" Text="Submit " CssClass="btn btn-sm btn-info" />
                        <asp:Button ID="btnNo" Text="Close" OnClick="btnNo_Click" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>



    <div>
        <ajaxToolkit:ModalPopupExtender ID="mpe_W" runat="server" TargetControlID="hnd_W" PopupControlID="pnl_W"
            BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:HiddenField ID="hnd_W" runat="server"></asp:HiddenField>
        <asp:Panel ID="pnl_W" runat="server" Style="display: none; padding: 20px;" Height="600px" Width="50%" CssClass="modalPopup">
            <asp:UpdatePanel ID="UpdatePanel_W" runat="server">
                <ContentTemplate>

                    <br />

                    <div class="page-header text-center">
                        <h2 class="elegantshd">
                            <asp:Label ID="lblHeader" Text="Interview Candidate Written Marks Entry For Board Member" runat="server"></asp:Label></h2>
                        <h3 style="text-align: center;"><span>
                            <asp:Label ForeColor="#8B0000" Font="Bold" runat="server" ID="MemberName_W"></asp:Label></span></h3>
                    </div>
                    <style>
                        .elegantshd {
                            color: #131313;
                            letter-spacing: .15em;
                            text-shadow: 2px 2px 4px #000000;
                            font-family: 'Kreon', serif;
                            vertical-align: middle;
                            text-decoration-style: wavy;
                        }
                    </style>
                    <hr />

                    <div>
                        <asp:HiddenField runat="server" ID="hdpkd_W"></asp:HiddenField>
                        <div class="form-row">

                            <div class="col-md-3" style="padding: 10px;">

                                <label>Written Marks Out Of: </label>
                            </div>

                            <div class="col-md-2">
                                <asp:TextBox ID="txt_WrittenMarksOutOf" runat="server" class="form-control form-control-sm" ReadOnly="True" placeholder="Written Out Of"></asp:TextBox>
                            </div>
                        </div>


                        <div style="height: 320px; overflow: scroll">
                            <asp:GridView Width="100%" ID="gv_InterviewCMarks_W" runat="server" ShowFooter="true"
                                AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>

                                            <asp:HiddenField runat="server" ID="hdInterviewMarksDetailsId" Value='<%#Eval("InterviewMarksDetailsId") %>' />
                                            <asp:HiddenField runat="server" ID="hdCandidateID" Value='<%#Eval("CandidateID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Candidate Name">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_CandidateName" runat="server" Text='<%#Eval("CandidateName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Written Marks">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txt_WrittenMarks" runat="server" AutoPostBack="True" OnTextChanged="txt_WrittenMarks_OnTextChanged" class="form-control" Text='<%#Eval("WrittenMarks") %>'></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                TargetControlID="txt_WrittenMarks" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>
                        <br />

                        <asp:Button runat="server" ID="btnYes_W" OnClick="btnYes_W_Click" Text="Submit " CssClass="btn btn-sm btn-info" />
                        <asp:Button ID="btnNo_W" Text="Close" OnClick="btnNo_W_Click" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
    <div>
        <ajaxToolkit:ModalPopupExtender ID="mpe_V" runat="server" TargetControlID="hnd_V" PopupControlID="pnl_V"
            BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:HiddenField ID="hnd_V" runat="server"></asp:HiddenField>
        <asp:Panel ID="pnl_V" runat="server" Style="display: none; padding: 20px;" Height="600px" Width="50%" CssClass="modalPopup">
            <asp:UpdatePanel ID="UpdatePanel_V" runat="server">
                <ContentTemplate>
                    <h3 style="text-align: center;">Interview Candidate Viva Marks Entry For Board Member <span>
                        <asp:Label ForeColor="#8B0000" Font="Bold" runat="server" ID="MemberName_V"></asp:Label></span></h3>
                    <div>
                        <asp:HiddenField runat="server" ID="hdpkd_V"></asp:HiddenField>

                        <div class="col-3">
                            <div class="form-group required">
                                <label class="control-label">Viva Marks Out Of</label>
                                <asp:TextBox ID="txt_VivaMarksOutOf" runat="server" class="form-control form-control-sm" placeholder="Viva Out Of"></asp:TextBox>
                            </div>
                        </div>

                        <div style="max-height: 550px; overflow: scroll">
                            <asp:GridView Width="100%" ID="gv_InterviewCMarks_V" runat="server" ShowFooter="true"
                                AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>

                                            <asp:HiddenField runat="server" ID="hdInterviewMarksDetailsId" Value='<%#Eval("InterviewMarksDetailsId") %>' />
                                            <asp:HiddenField runat="server" ID="hdCandidateID" Value='<%#Eval("CandidateID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Candidate Name">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_CandidateName" runat="server" Text='<%#Eval("CandidateName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Viva Marks">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txt_VivaMarks" runat="server" class="form-control form-control-sm" Text='<%#Eval("VivaMarks") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>
                        <br />

                        <asp:Button runat="server" ID="btnYes_V" OnClick="btnYes_V_Click" Text="Submit " CssClass="btn btn-sm btn-info" />
                        <asp:Button ID="btnNo_V" Text="Close" OnClick="btnNo_V_Click" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
    <div>
        <ajaxToolkit:ModalPopupExtender ID="mpe_O" runat="server" TargetControlID="hnd_O" PopupControlID="pnl_O"
            BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:HiddenField ID="hnd_O" runat="server"></asp:HiddenField>
        <asp:Panel ID="pnl_O" runat="server" Style="display: none; padding: 20px;" Height="600px" Width="50%" CssClass="modalPopup">
            <asp:UpdatePanel ID="UpdatePanel_O" runat="server">
                <ContentTemplate>
                    <br />

                    <div class="page-header text-center">
                        <h2 class="elegantshd">
                            <asp:Label ID="Label2" Text="Interview Candidate Other Marks Entry For Board Member" runat="server"></asp:Label></h2>
                        <h3 style="text-align: center;"><span>
                            <asp:Label ForeColor="#8B0000" Font="Bold" runat="server" ID="MemberName_O"></asp:Label></asp:Label></span></h3>
                    </div>
                    <hr />
                    <div>
                        <asp:HiddenField runat="server" ID="hdpkd_O"></asp:HiddenField>
                        <div class="form-row">

                            <div class="col-md-2" style="padding: 10px;">

                                <label>Other Marks Out Of: </label>
                            </div>
                            <div class="col-2">
                                <asp:TextBox ID="txt_OtherMarksOutOf" runat="server" ReadOnly="True" class="form-control form-control-sm" placeholder="Other Out Of"></asp:TextBox>
                            </div>
                        </div>

                        <div style="height: 320px; overflow: scroll">


                            <asp:GridView Width="100%" ID="gv_InterviewCMarks_O" runat="server" ShowFooter="true"
                                AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>

                                            <asp:HiddenField runat="server" ID="hdInterviewMarksDetailsId" Value='<%#Eval("InterviewMarksDetailsId") %>' />
                                            <asp:HiddenField runat="server" ID="hdCandidateID" Value='<%#Eval("CandidateID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Candidate Name">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_CandidateName" runat="server" Text='<%#Eval("CandidateName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Other Marks">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txt_OtherMarks" runat="server" AutoPostBack="True" OnTextChanged="txt_OtherMarks_OnTextChanged" class="form-control form-control-sm" Text='<%#Eval("OtherMarks") %>'></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                TargetControlID="txt_OtherMarks" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>
                        <br />

                        <asp:Button runat="server" ID="btnYes_O" OnClick="btnYes_O_Click" Text="Submit " CssClass="btn btn-sm btn-info" />
                        <asp:Button ID="btnNo_O" Text="Close" OnClick="btnNo_O_Click" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>





    <div>
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupFileUp" runat="server" TargetControlID="hnd_ModalPopupFileUp" PopupControlID="pnl_ModalPopupFileUp"
            BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:HiddenField ID="hnd_ModalPopupFileUp" runat="server"></asp:HiddenField>
        <asp:Panel ID="pnl_ModalPopupFileUp" runat="server" Style="display: none; padding: 20px;" Height="600px" Width="50%" CssClass="modalPopup">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <br />

                    <div class="page-header text-center">
                        <h2 class="elegantshd">
                            <asp:Label ID="Label3" Text="File Upload Information" runat="server"></asp:Label></h2>

                    </div>
                    <hr />
                    <div>
                        <asp:HiddenField runat="server" ID="HiddenField2"></asp:HiddenField>


                        <div class="form-row">

                            <div class="col-3">
                                <div class="form-group files">


                                    <asp:FileUpload ID="fu_cv" CssClass="form-control" runat="server" />


                                </div>

                            </div>


                        </div>

                        <input type="file" name="postedFile" runat="server" visible="False" />
                        <input type="button" id="btnUpload" value="Upload" runat="server" visible="False" />
                        <asp:Button runat="server" ID="btnAttSave" Text="Save Attachment" Visible="False" OnClick="btnAttSave_OnClick" CssClass="btn btn-success btn-sm" />
                        <progress id="fileProgress" runat="server" visible="False" style="display: none"></progress>

                        <asp:HiddenField runat="server" ID="hfAttFile" />
                        <span id="lblMessage" runat="server" visible="False" style="color: Green"></span>
                        <br />

                        <asp:Button runat="server" ID="btnSaveFile" OnClick="btnSaveFile_Click" Text="Submit " CssClass="btn btn-sm btn-info" />
                        <asp:Button ID="btnCancelFile" Text="Close" OnClick="btnCancelFile_Click" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                        <hr />
                        <asp:LinkButton runat="server" ID="lb_Download" CssClass="btn btn-primary btn-sm" BackColor="#4169E1" BorderStyle="None" OnClick="lb_Download_OnClick"><i class="fa fa-download"></i> Download</asp:LinkButton>

                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </div>

                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSaveFile" />
                    <asp:PostBackTrigger ControlID="lb_Download" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
    <style>
        .files input {
            outline: 2px dashed #92b0b3;
            outline-offset: -10px;
            -webkit-transition: outline-offset .15s ease-in-out, background-color .15s linear;
            transition: outline-offset .15s ease-in-out, background-color .15s linear;
            padding: 30px 0px 60px 10%;
            text-align: center !important;
            margin: 0;
            width: 400px !important;
        }

            .files input:focus {
                outline: 2px dashed #92b0b3;
                outline-offset: -10px;
                -webkit-transition: outline-offset .15s ease-in-out, background-color .15s linear;
                transition: outline-offset .15s ease-in-out, background-color .15s linear;
                border: 1px solid #92b0b3;
            }

        .files {
            position: relative;
        }

            .files:after {
                pointer-events: none;
                position: absolute;
                top: 60px;
                left: 0;
                width: 250px;
                right: 0;
                height: 33px;
                content: "";
                /*background-image: url(https://image.flaticon.com/icons/png/128/109/109612.png);*/
                display: block;
                margin: 0 auto;
                background-size: 100%;
                background-repeat: no-repeat;
            }

        .color input {
            background-color: #f1f1f1;
        }

        .files:before {
            position: absolute;
            bottom: 10px;
            left: 0;
            pointer-events: none;
            width: 250px;
            right: 0;
            height: 35px;
            content: " or drag it here. ";
            display: block;
            margin: 0 auto;
            color: #2ea591;
            font-weight: 600;
            text-transform: capitalize;
            text-align: center;
        }
    </style>
    <script type="text/javascript">
        $("body").on("click", "#btnUpload", function () {
            $.ajax({
                url: '/FileUploadHandler.ashx',
                type: 'POST',
                data: new FormData($('form')[0]),
                cache: false,
                contentType: false,
                processData: false,
                success: function (file) {
                    $("#fileProgress").hide();
                    //$("#lblMessage").html("<b>" + file.name + "</b> has been uploaded.");
                    $("#lblMessage").html("File has been uploaded.");
                    //console.log(file);
                    $('#cpFormBody_hfAttFile').val(file.dbfilename);
                },
                xhr: function () {
                    var fileXhr = $.ajaxSettings.xhr();
                    if (fileXhr.upload) {
                        $("progress").show();
                        fileXhr.upload.addEventListener("progress", function (e) {
                            if (e.lengthComputable) {
                                $("#fileProgress").attr({
                                    value: e.loaded,
                                    max: e.total
                                });
                            }
                        }, false);
                    }
                    return fileXhr;
                }
            });
        });
    </script>
</asp:Content>




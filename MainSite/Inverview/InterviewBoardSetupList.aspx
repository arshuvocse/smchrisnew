<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MainMasterPage.master" CodeFile="InterviewBoardSetupList.aspx.cs" Inherits="Inverview_InterviewBoardSetupList" %>
<%@ Register Src="~/Report_Pages/IVSearchControlReport.ascx" TagPrefix="uc1" TagName="IVSearchControl" %>
<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">
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
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png" width="20px"  /> Interview Committee Info List</h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                            
                               <asp:LinkButton ID="HomeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                    
                            
                            <asp:LinkButton ID="btn_NewInterviewBoardSetup"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="btn_NewInterviewBoardSetup_OnClick" > <i class="fa fa-plus"></i>&nbsp;Add New</asp:LinkButton>
                        
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-body">


                            <div class="row" runat="server" Visible="False">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label></label>
                                        <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True" RepeatDirection="Horizontal" Enabled="False">
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-row">
               
                    <uc1:IVSearchControl runat="server" ID="IVSearchControl" />
                     <asp:LinkButton runat="server" ID="btn_LoadList" OnClick="btn_LoadList_OnClick" CssClass="btn btn-sm btnMyDesignSearch"><i class="fa fa-search-plus"></i>&nbsp; Search Matching Candidate List</asp:LinkButton>
                    
                          
                                <br/>
                                <br/>
                       
                                       
                                       
                                      
                                <style>
                                    .btnexcel {
                                        background-color: #4CAF50;
                                        border: none;
                                        color: white;
                                        padding: 8px 12px;
                                        text-align: center;
                                        text-decoration: none;
                                        display: inline-block;
                                        font-size: 12px;
                                        margin: 4px 2px;
                                        cursor: pointer;
                                    }
         </style>        
                       
                </div>
                                  <br/>
                               
                            <div>
                                <asp:GridView Width="100%" ID="gv_InterviewBoardMember" runat="server"  
                                    AutoGenerateColumns="false"    CssClass="AddToListCssTable"  OnPreRender="gv_DocumentUpload_PreRender" >
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL#">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                                <asp:HiddenField ID="hdpk" runat="server" Value='<%#Eval("SetupMasterId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Print" Visible="False">
                                          
                                                
                                                  <ItemTemplate>
                                                <asp:LinkButton ID="lb_Print" runat="server" OnClick="lb_Print_Click"> <img src="../Assets/report_magnify.png" /></asp:LinkButton>
                                            </ItemTemplate>
                                                
                                            
                                        </asp:TemplateField>
                                        
                                          <asp:TemplateField HeaderText="Send Mail">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lb_SendMail" OnClick="lb_SendMail_OnClick" runat="server"><img src="../Assets/img/email-1.png"/></asp:LinkButton>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                 <%--       <asp:TemplateField HeaderText="Job Code">
                                            <ItemTemplate>
                                                <asp:Label ID="txt_JobCode" runat="server"  Text='<%#Eval("JobCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                         <asp:TemplateField HeaderText="Company">
                                            <ItemTemplate>
                                                <asp:Label ID="txt_Company" runat="server"  Text='<%#Eval("ShortName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Position">
                                            <ItemTemplate>
                                                <asp:Label ID="txt_Position" runat="server"  Text='<%#Eval("Position") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="InterviewDate" DataFormatString="{0:dd-MMM-yyyy}"  HeaderText="Interview Date" />
                                        
                                        
                                         
                                        
                                       
                                          <asp:TemplateField HeaderText="Start Time">
                                            <ItemTemplate>
                                                <asp:Label ID="txt_InterviewFromTime" runat="server"  Text='<%#Eval("InterviewFromTime") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                          <asp:TemplateField HeaderText="End Time">
                                            <ItemTemplate>
                                                <asp:Label ID="txt_InterviewToTime" runat="server"  Text='<%#Eval("InterviewToTime") %>' ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lb_Edit" runat="server" OnClick="lb_Edit_Click"><img src="../Assets/img/rsz_edit.png" /> </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Remove">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lb_Remove" runat="server" OnClick="lb_Remove_Click"> <img src="../Assets/img/delete.png" /></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lb_view" runat="server" OnClick="lb_View_Click"><img src="../Assets/img/list-view.png" /></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
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
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                        </div>
                    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

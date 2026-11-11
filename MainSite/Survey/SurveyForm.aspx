<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MainMasterPage.master" CodeFile="SurveyForm.aspx.cs" Inherits="Survey_SurveyForm" %>

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
    <script language="javascript" type="text/javascript">
        function switchViews(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);
            if (div.style.display == "none") {
                div.style.display = "inline";
                img.src = "../Assets/img/minus.png";
            } else {
                div.style.display = "none";
                img.src = "../Assets/img/add.png";
            }
        }
    </script>
    <style>
        .about-team-right
        {
            float: right;
            width: 80%;
            z-index: -99;
        }

        .rbl input[type="radio"]
        {
            margin-left: 10px;
            margin-right: 10px;
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
                        <h1 class="title" style="font-size: 18px; padding-top: 0px;">  <img src="../Report_Pages/app.png"  width="20px" />  Survey Form</h1>
                        
                      <asp:Label runat="server" ID="lblSurveyName"></asp:Label>
                        
                          <asp:HiddenField runat="server" ID="hdMasID"  />
                          <asp:HiddenField runat="server" ID="hdEmpId"  />
                    </div>
                     <div class="page-heading__container float-right d-none d-sm-block">
                <asp:Button ID="detailsViewButton" Text="Back To List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
            </div>
                </div>
                <div class="card">
                    <div class="card-body">
                        <asp:GridView Width="100%" ID="gv_Menu" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark" 
                                      OnRowDataBound="gv_Menu_OnRowDataBound" ShowHeader="False">
                            <RowStyle HorizontalAlign="Left"></RowStyle>
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                           
                                        <asp:HiddenField runat="server" ID="hdSurveyQuestionGroupId" Value='<%#Eval("SurveyQuestionGroupId") %>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="txt_SurveyQuestionGroup" runat="server" CssClass="text-bold text-lg text-left text-danger"  Text='<%#Eval("SurveyQuestionGroup") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    
                                <asp:TemplateField ItemStyle-Width="80px" >
                                    <ItemTemplate>
                                        <a href="javascript:switchViews('div<%# Eval("SerialNo") %>', 'one');">
                                            <img id="imgdiv<%# Eval("SerialNo") %>" alt="Click to show/hide orders" border="0"
                                                 src="../Assets/img/minus.png" />
                                        </a>
                                        <asp:HiddenField ID="hfSurveyQuestionGroupId" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "SurveyQuestionGroupId") %>'>
                                        </asp:HiddenField>
                                    </ItemTemplate>
                                    <ItemStyle Width="20px" VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateField>
                                    
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <tr>
                                            <td colspan="100%">
                                                <div id="div<%# Eval("SerialNo") %>" style="overflow: auto; right: 10px; display: initial;
                                                                                                                          position: relative;">
                                                <asp:GridView ID="gv_Child" runat="server" Width="90%" CssClass="about-team-right"
                                                              AutoGenerateColumns="false" DataKeyNames="SurveyQuestionGroupId" ShowHeader="False">
                                                    <RowStyle CssClass="rowStyle" />
                                                    <AlternatingRowStyle CssClass="alternetRowStyle" />
                                                    <RowStyle HorizontalAlign="Left"></RowStyle>
                                                    <Columns>
                                                            
                                                        <asp:TemplateField >
                                                            <ItemTemplate>
                                                                  
                                                                
                                                                   <asp:HiddenField runat="server" ID="hdQuestionGroupId" Value='<%#Eval("SurveyQuestionGroupId") %>'/>
                                                                   <asp:HiddenField runat="server" ID="SurveyQuestionTypeId" Value='<%#Eval("SurveyQuestionTypeId") %>'/>
                                                                   <asp:HiddenField runat="server" ID="hdSurveyQuestionIdN" Value='<%#Eval("SurveyQuestionId") %>'/>
                                                                <asp:Label ID="txt_QuestionTitle" runat="server"  Text='<%#Eval("QuestionTitle") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField >
                                                            <ItemTemplate>
                                                                <asp:RadioButtonList CssClass="rbl" RepeatDirection="Horizontal" runat="server" ID="radSingleAns" Visible="False" >
                                                                   
                                                                    
                                                                </asp:RadioButtonList>
                                                                
                                                                 
                                                                
                                                                <asp:TextBox CssClass="form-control" TextMode="MultiLine" Rows="2" runat="server" ID="txtLongAns" Visible="False"></asp:TextBox>
                                                               
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                            
                                                        
                                                    </Columns>
                                                    <HeaderStyle BackColor="#4D92C1" ForeColor="White" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        
                        <br/>
                        <div runat="server" Visible="False">
                            <div>#Following questions are for classification purpose only. It will not be used to identify any individual.</div>
                            <div>
                                <div style="padding: 10px;">
                                     a) How long are you serving this organization?
                                </div>
                               
                                 <div style="padding: 5px;">  <asp:RadioButtonList runat="server" ID="radClasQ1" Width="1064px">
                                        
                                        <asp:ListItem runat="server" Text="Less than 1 year"></asp:ListItem>
                                        <asp:ListItem runat="server" Text="1 to 3 years"></asp:ListItem>
                                        <asp:ListItem runat="server" Text="3 to 5 years"></asp:ListItem>
                                        <asp:ListItem runat="server" Text="5 to 10 years"></asp:ListItem>
                                        <asp:ListItem runat="server" Text="More than 10 years"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    </div>
                            </div>
                        </div>
                        <br/>
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


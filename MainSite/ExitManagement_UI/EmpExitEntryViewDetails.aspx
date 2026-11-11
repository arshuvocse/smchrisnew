<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="EmpExitEntryViewDetails.aspx.cs" Inherits="ExitManagement_UI_EmpExitEntryViewDetails" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
      <style type="text/css">
        /*AutoComplete flyout */
      
    </style>
    <style>
        .checkboxlist_nowrap {
            display: inline;
        }
        .cls-SelectJq {
            
        }
    </style>
    
         <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">


    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>
                      <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px"
                   />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                             
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" /> Clearance Form Setup</h1>
                        </div>
                                            <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="detailsViewButton" Text="Back to List " CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>
                    </div>

                    <div class="card">
                        <div class="card-body">
                            <fieldset class="for-panel" runat="server" Visible="False">
                                <%--<legend>Search By</legend>--%>
                                <div class="form-row">
                                    <div class="col-3">
                                        <div class="form-group ">
                                            <label class="control-label">Company</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" class="form-control form-control-sm" />
                                        </div>
                                    </div>

                                    <div class="col-4">
                                        <div class="form-group ">
                                            <label class="control-label">Search Employee Name</label>

                                              <asp:DropDownList   runat="server"   ID="ddlEmpInfo" AutoPostBack="True" OnTextChanged="ddlEmpInfo_OnTextChanged" class="form-control form-control-sm" />
                                                    <script type="text/javascript">
                                                        function pageLoad() {
                                                            $('#<%=ddlEmpInfo.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });
                                                            $('.cls-SelectJq').chosen({ disable_search_threshold: 5, search_contains: true });
                                                            

                                                        }
</script>
                                            <asp:TextBox Visible="False" ID="txt_EmpName" runat="server" AutoPostBack="True" Enabled="False" CssClass="form-control form-control-sm" OnTextChanged="txt_EmpName_OnTextChanged"></asp:TextBox>
                                            <asp:HiddenField runat="server" ID="hfEmpInfoId" />
                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                ServiceMethod="GetExitImployee" ServicePath="~/WebService.asmx" TargetControlID="txt_EmpName"
                                                UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                ShowOnlyCurrentWordInCompletionListItem="true">
                                            </cc1:AutoCompleteExtender>
                                        <%--    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                ServiceMethod="GetExitImployee" ServicePath="~/WebService.asmx" TargetControlID="txt_EmpName"
                                                UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                ShowOnlyCurrentWordInCompletionListItem="true">
                                            </cc1:AutoCompleteExtender>--%>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                    

                            <div class="form-row" runat="server" Visible="False">

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Employee Code</label><span style="color: red">&nbsp;*</span>
                                        <asp:Label runat="server" ID="empCode" class="form-control form-control-sm" />
                                        <asp:HiddenField ID="HiddenField2" runat="server" />
                                        <asp:HiddenField ID="deptHiddenField" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Employee Name</label><span style="color: red">&nbsp;*</span>
                                        <br />
                                        <asp:Label runat="server" ID="empName" class="form-control form-control-sm" />
                                        <asp:HiddenField ID="HiddenField" runat="server" />

                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Date of Joining</label><span style="color: red">&nbsp;*</span>
                                        <asp:Label runat="server" ID="dtJoining" class="form-control form-control-sm" />
                                        <asp:HiddenField ID="HiddenField3" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Division</label><span style="color: red">&nbsp;*</span>
                                        <asp:Label runat="server" ID="ddlDivision" class="form-control form-control-sm" />
                                        <asp:HiddenField ID="hfDivision" runat="server" />

                                    </div>
                                </div>
                            </div>

                            <div class="form-row" runat="server" Visible="False">
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Designation</label><span style="color: red">&nbsp;*</span>
                                        <asp:Label runat="server" ID="ddlDesignation" class="form-control form-control-sm" />
                                        <asp:HiddenField ID="hfDesignation" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3" style="display: none">
                                    <div class="form-group">
                                        <label>Slary Grade</label><span style="color: red">&nbsp;*</span>
                                        <asp:Label runat="server" ID="ddlSalaryGrade" class="form-control form-control-sm" />
                                        <asp:HiddenField ID="hfSalaryGrade" runat="server" />
                                    </div>
                                </div>
                                
                                <div class="col-6">
                                    <div class="form-group">
                                        <label> Description </label>
                                        <asp:TextBox runat="server" ID="descriptionTextbox" TextMode="MultiLine" Rows="2"  class="form-control" />
                                    </div>
                                </div>
                            </div>
                            
                            
                            
                                <div class="row">
                                <style>
                                    .tblTHColorChang{
                                        background-color: #EDF2F5!important;
                                        font-weight: bold;
                                        font-size: 13px;
                                    }


.title-widget {
	color: #898989;
	font-size: 20px;
	font-weight: 300;
	line-height: 1;
	position: relative;
	text-transform: uppercase;
	font-family: 'Fjalla One', sans-serif;
	margin-top: 0;
	margin-right: 0;
	margin-bottom: 25px;
	 
	padding-left: 12px;

}
  .chkChoiceStep label {
            padding-left: 5px;
            padding-right: 5px;
        }
.title-widget::before {
    background-color: #ea5644;
    content: "";
    height: 22px;
    left: 0px;
    position: absolute;
    top: -2px;
    width: 5px;
}


                                </style>
                                     
                                     <table class="table table-bordered table-striped">
                                                    <tr>
                                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Employee ID</td>
                                                        <td> <asp:Label runat="server" ID="lblEmpId"></asp:Label></td>

                                                        
                                                         <td  class="tblTHColorChang" style="width: 20%; padding: 10px;">Supervisor</td>
                                                        <td>  <asp:Label ID="ReportingLabel"  runat="server"></asp:Label></td>
                                                    </tr>
                                                    
                                                     <tr>
                                                        <td  class="tblTHColorChang" style="width: 20%; padding: 10px;">Employee Name</td>
                                                        <td> <asp:Label runat="server" ID="lblEmployeeName"></asp:Label></td>

                                                         
                                                              
                                                         <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Department</td>
                                                        <td>  <asp:Label ID="deptNameLabel"  runat="server"></asp:Label></td>
                                                    </tr>
                                                    
                                                     <tr>
                                                        <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Designation</td>
                                                        <td>   <asp:Label ID="desigNameLabel" runat="server"></asp:Label></td>

                                                     <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Office</td>
                                                        <td>  <asp:Label ID="LocationLabel"   runat="server"></asp:Label></td>

                                                    </tr>
                                                    
                                                    
                                                    
                                                    
                                                     
                                                    
                                                     <tr>
                                                        <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Date Of Joining</td>
                                                        <td>     <asp:Label ID="joiningDateLabel"  runat="server"></asp:Label></td>
  <td style="width: 20%; padding: 10px;" class="tblTHColorChang" >Place</td>
                                                        <td> <asp:Label runat="server" ID="lblPlace"></asp:Label></td>
                                                    </tr>
                                                    
                                                    
                                                 
                                                    
                                                    
                                                    </table>
                            </div>
                            

                            <div id="exitReasonGridViewa" style="height: auto;">
                                <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;"> Supervisor List</h2>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-sm table-bordered text-center thead-dark">
                                    <Columns>

                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        

                                        <asp:BoundField DataField="EmpName" HeaderText="Employee Info" />

                                        
                                    </Columns>
                                </asp:GridView>
                            </div>

                            <div id="exitReasonGridView" style="height: auto;">
                                
                                 <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;"> Department List</h2>
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-sm table-bordered text-center thead-dark" >
                                    <Columns>

                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         

                                         <asp:BoundField DataField="EmpName" HeaderText="Employee Info" />

                                       
                                    </Columns>
                                </asp:GridView>
                            </div>
                            
                            
                            
                               <div id="exitReasossnGridView" style="height: auto;">
                                
                                 <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;"> Any Other Attachment List</h2>
                                  <asp:GridView Width="100%" ShowHeader="True" ID="gv_Doc" runat="server" AutoGenerateColumns="false" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL#">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1%>
                                                         
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Document">
                                                    <ItemTemplate>
                                                          <asp:HyperLink ID="HLDocumentLink"   Target="_blank"  runat="server" NavigateUrl='<%# Eval("DocumentLink") %>'  Text='Download'>
        </asp:HyperLink>
                                                        <asp:Label ID="lbl_DocumentLink" Visible="False" runat="server" Text='<%#Eval("DocumentLink")%>'></asp:Label>
                                                        <asp:HiddenField runat="server" ID="hfFileName" Value='<%#Eval("FileName")%>' />

                                                        <asp:HiddenField runat="server" ID="hfDocumentLink" Value='<%#Eval("DocumentLink")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                 
                                                <asp:TemplateField HeaderText="Summary Note	">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_DocumentNote" runat="server" Text='<%#Eval("DocumentNote") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                             

                                               
                                            </Columns>
                                        </asp:GridView>
                            </div>
                     
                            <div>
                                <asp:Button runat="server" Visible="False" ID="btn_Save" OnClick="btn_Save_OnClick" Text="Submit " CssClass="btn btn-sm btn-info" />
                                <asp:Button ID="cancelButton"  Visible="False"  Text="Cancel" OnClick="cancelButton_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>


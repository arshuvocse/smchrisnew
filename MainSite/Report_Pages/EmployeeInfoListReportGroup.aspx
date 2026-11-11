<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MainMasterPage.master" CodeFile="EmployeeInfoListReportGroup.aspx.cs" Inherits="Report_Pages_EmployeeInfoListReportGroup" EnableEventValidation="false" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>


<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">

   
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cpFormBody" runat="Server">
    
    
    <style type="text/css">
       
        </style>
    

    <div class="content" id="content">

        <style>
      

            </style>
        <div class="container-fluid">
            <div class="page-heading">
                <div class="page-heading__container">
                    <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" />&nbsp;Employee Information Report List</h1>
                </div>
                <div class="page-heading__container float-right d-none d-sm-block">
                  
                    <asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="HomeButton_OnClick" />
                    
                      <asp:Button ID="btnEditInfo" Visible="False" Text="Update Information" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="btnEditInfo_OnClick" />
                    <asp:Button ID="btn_New" Text="Create New"  Visible="False"  CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="btn_New_OnClick" />

                </div>
            </div>
            <div class="container-fluid">
                <div class="card">
                    <asp:UpdatePanel runat="server" ID="uppa">
                        <ContentTemplate>
                          <%--  <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="uppa">
                                <ProgressTemplate>
                                    <div class="modal">
                                        <div class="center">
                                            <img alt="" src="/Assets/loader.gif" />
                                        </div>
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>--%>
                            <div class="card-body">
                            
                            
                             <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                                <div class="form-row">
                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>Company</label>
                                            <label style="color: #a52a2a">*</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" class="form-control form-control-sm" />
                                        </div>
                                    </div>
                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>Division</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDivision" class="form-control form-control-sm" OnSelectedIndexChanged="ddlDivision_OnSelectedIndexChanged" />

                                        </div>
                                    </div>
                                    <div class="col-2" runat="server" id="wing" visible="False">
                                        <div class="form-group">
                                            <label>Wing</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlWing" class="form-control form-control-sm" OnSelectedIndexChanged="ddlWing_OnSelectedIndexChanged" />

                                        </div>
                                    </div>
                                    <div class="col-2" runat="server" id="dept">
                                        <div class="form-group">
                                            <label>Department</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDepartment" class="form-control form-control-sm" OnSelectedIndexChanged="ddlDepartment_OnSelectedIndexChanged" />

                                        </div>
                                    </div>
                                    <div class="col-2" runat="server" id="sec" visible="False">
                                        <div class="form-group">
                                            <label>Section</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlSection" class="form-control form-control-sm" OnSelectedIndexChanged="ddlSection_OnSelectedIndexChanged" />

                                        </div>
                                    </div>
                                    <div class="col-2" runat="server" id="subsec" visible="False">
                                        <div class="form-group">
                                            <label>Sub Section</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlSubSection" class="form-control form-control-sm" OnSelectedIndexChanged="ddlSubSection_OnSelectedIndexChanged" />

                                        </div>
                                    </div>

                                    <div class="col-md-2">

                                        <div class="form-group">
                                            <label>Employee ID: </label>
                                            <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="True" placeholder=" Employee ID" CssClass="form-control form-control-sm"
                                                OnTextChanged="EmployeeDropDownList2_SelectedIndexChanged"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters=""
                                                    EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                    ServiceMethod="GetCompanyWiseEmployeeInfoForIDANdNae" ServicePath="~/WebService.asmx" TargetControlID="txtSearch"
                                                    UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                    ShowOnlyCurrentWordInCompletionListItem="true">
                                                </cc1:AutoCompleteExtender>

                                        </div>
                                    </div>


                                    <div class="col-md-2">

                                        <div class="form-group">
                                            <label>Employee Name: </label>
                                            <asp:TextBox ID="NameTextBox" runat="server" AutoPostBack="True" placeholder=" Employee Name" CssClass="form-control form-control-sm" OnTextChanged="EmployeeDropDownList_SelectedIndexChanged"></asp:TextBox>
                                            
                                               <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                    EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                    ServiceMethod="GetCompanyWiseEmployeeInfoForIDANdNae" ServicePath="~/WebService.asmx" TargetControlID="NameTextBox"
                                                    UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                    ShowOnlyCurrentWordInCompletionListItem="true">
                                                </cc1:AutoCompleteExtender>

                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Designation</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDesignation" class="form-control form-control-sm" />

                                        </div>
                                    </div>
                                </div>
                      
                                   <div class="row">


                           
                           
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Office</label>
                                            <asp:DropDownList runat="server" ID="ddlSalaryLocation" class="form-control form-control-sm" />

                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Confirmation Status</label>
                                            <asp:DropDownList runat="server" ID="ddlConformationStatus" CssClass="form-control form-control-sm">
                                                <asp:ListItem Text="Select..." Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                         <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Active Status</label>
                                            <asp:DropDownList runat="server" ID="ActiveStatusDropDownList" CssClass="form-control form-control-sm">
                                                <asp:ListItem Text="Select..." Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
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
                                
                                
                                  <div class="row">
                                      
                                       <div class="col-md-3">
                                                </div>
                                                <div class="col-md-1">
                                                </div>
                                       <div class="col-md-4">
                                        <div class="form-group" style="margin-top: 17px;">

                                           
                                            
                                            
                                              <asp:LinkButton runat="server" ID="SearchButtonDisciplinaryAction" OnClick="SearchButton_OnClick" ToolTip="Click To Search" CssClass="btn btn-info   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search </asp:LinkButton>

                                                        <asp:LinkButton runat="server" ID="ResetSearchButtonDisciplinaryAction" OnClick="btnReset_OnClick" CssClass="btn btn-warning   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset </asp:LinkButton>
                                        </div> 

                                    </div>
                                  </div>
                                     <div class="col-md-2">
                                         </div>
                             
                                  

                                </div>  
                                <div class="row" runat="server" visible="False">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label></label>
                                            <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True" RepeatDirection="Horizontal" Enabled="False">
                                            </asp:CheckBoxList>


                                        </div>
                                    </div>
                                </div>
                                
                                <div class="row" runat="server"  >
                                     <div class="col-md-8">
                                         
                                     </div>
                                    
                                      <style>.ssss {
                                                                                                                 font-size: 13px;
                                                                                                                 font-weight: bold;

                                                                                                                 box-shadow: 0 0 3px 1px rgba(0,0,0,.35);
                                              height: 30px;
                                                                                                                 padding: 10px;
                                                                          
                                               
                                                                                                             }</style>
                              
                                    
                             
                               
       
                                       
                                </div>
                            
                             <div class="form-row" style="padding-right: 10px">

                                <div class="col-md-6" style="padding-left: 12px">
                                    <label style="font-size: 18px;">Details Information</label>
                                </div>
                                <div class="col-md-4 pull-right"  style="padding-top: 10px;">
                                      
                                       
                                            
                                              <asp:Label ID="lblCount" runat="server" CssClass="ssss pull-right badge"   Text="Total : 0" ></asp:Label>
                                             
                               
                                        
                                      
                                     
                                </div>
                                <div class="col-md-2">
                                    <%--  <asp:LinkButton ID="btnExportToExcel" runat="server" CssClass="btnexcel  pull-right" Style="padding-right: 10px;" OnClick="btnExportToExcel_Click"><span aria-hidden="true" style="font-size: 14px; color: #4CAF50;" class="fa fa-file-excel-o"></span> Download to xls</asp:LinkButton>--%>
                                    <style>
                                          .btnexcelcc2 {
            border: none;
            color: #131313;
            padding-left: 36px;
            padding-top: 8px;
            padding-bottom: 8px;
            padding-right: 36px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 12px;
            margin: 4px 2px;
            cursor: pointer;
            background: url(../Assets/excel.png);
            background-position: center;
            background-repeat: no-repeat;
            box-shadow: 0 0 3px 1px rgba(0,0,0,.35);
                                              height: 30px;
        }


                                          #cpFormBody_loadGridView th {
  padding: 10px !important;
    border-style: none !important;

  background-color: #CCCCCC !important;
  color: black !important;
    font-weight: bold !important;
    font-size: 13px !important;
    text-align: center;
}
                                    </style>
                                 
                                    
                                     <asp:LinkButton ID="btnExportToExcel" runat="server"  CssClass="btnexcelcc2  pull-right" OnClick="btnExportToExcel_Click" ToolTip="Export To Excel" > </asp:LinkButton>
                                </div>


                            </div>

                            <hr />
                                <%-- <div id="tbl_container" style="width:100%" class="">

                    </div>--%>
                                  
                                <div id="gridContainer1" style="height: auto; overflow: auto; width: auto;">
                                    <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                        CssClass="table table-bordered text-center thead-dark" DataKeyNames="EmpInfoId"
                                        OnRowCommand="loadGridView_RowCommand" Font-Size="12px" PageIndex="0" AllowPaging="True" PageSize="10"  OnPageIndexChanging="loadGridView_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL">
                                                <ItemTemplate>
                                                    <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                       <asp:HiddenField ID="JdMasterId" runat="server" Value='<%#Eval("EmpInfoId") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                


                                            <asp:BoundField DataField="ShortName" HeaderText="Company Name" />
                                            <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee New ID" />
                                            <asp:BoundField DataField="SMCOldCode" HeaderText="Employee Old ID" />
                                            <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                              <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                            <asp:BoundField DataField="DivisionName" HeaderText="Division" />

                                            <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                          
                                            <asp:BoundField DataField="SalaryLocation" HeaderText="Office" />
                                            <asp:BoundField DataField="Place" HeaderText="Place" />
                                            <asp:BoundField DataField="OfficialMobile" HeaderText="Contact Number" />
                                           


                                       
                                        </Columns>
                                   <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                    </asp:GridView>
                                </div>
 
                    </ContentTemplate>
                        
                        <Triggers>
                 
                 <asp:PostBackTrigger ControlID="btnExportToExcel"/>
             </Triggers>
                    </asp:UpdatePanel>
                    </div>
                            </div>
                       
                </div>
            </div>
         
   <style>.GridPager a,
                                                                                                                                        .GridPager span {
                                                                                                                                            display: inline-block;
                                                                                                                                            padding: 3px 14px;
                                                                                                                                            margin-right: 8px;
                                                                                                                                            border-radius: 3px;
                                                                                                                                            height: 20px;
                                                                                                                                            border: solid 1px #c0c0c0;
                                                                                                                                            background: #e9e9e9;
                                                                                                                                            box-shadow: inset 0px 1px 0px rgba(255,255,255, .8), 0px 1px 3px rgba(0,0,0, .1);
                                                                                                                                            font-size: 14px;
                                                                                                                                            font-weight: bold;
                                                                                                                                            text-decoration: none;
                                                                                                                                            color: #717171;
                                                                                                                                            text-shadow: 0px 1px 0px rgba(255,255,255, 1);
                                                                                                                                        }

                                                                                                                                        .GridPager a {

                                                                                                                                            background-color: #f5f5f5;
                                                                                                                                            color: #969696;
                                                                                                                                            border: 1px solid #969696;
                                                                                                                                        }

                                                                                                                                        .GridPager span {

                                                                                                                                            background: #616161;
                                                                                                                                            box-shadow: inset 0px 0px 8px rgba(0,0,0, .5), 0px 1px 0px rgba(255,255,255, .8);
                                                                                                                                            color: #f0f0f0;
                                                                                                                                            text-shadow: 0px 0px 3px rgba(0,0,0, .5);
                                                                                                                                            border: 1px solid #3AC0F2;
                                                                                                                                        }

   </style>
</asp:Content>

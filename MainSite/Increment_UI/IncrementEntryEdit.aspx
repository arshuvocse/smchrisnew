<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" enableEventValidation="true" CodeFile="IncrementEntryEdit.aspx.cs" Inherits="Increment_UI_IncrementEntryEdit" %>

<%--<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>--%>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
     
         <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">

        <style type="text/css">
            /*AutoComplete flyout */
               .chkChoiceDesignation label {
            padding-left:2px;
            padding-right: 7px;
            font-weight: bold;
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
        <asp:UpdatePanel ID="upFormBody" runat="server">




            <ContentTemplate>



                <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>




                <div class="page-heading">
                    <div class="page-heading__container">
                        <h1 class="title" style="font-size: 18px; padding-top: 0px;"> <img src="../Report_Pages/app.png"  width="20px" /> Increment Entry </h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="HomeButton_OnClick" />
                        <asp:Button ID="detailsViewButton" Text="View" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>
                </div>
                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body">

                            <div class="form-row">



                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Company</label><span style="color: red">&nbsp;*</span>
                                        <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" class="form-control form-control-sm" />
                                    </div>
                                </div>

                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                        <span style="color: red">&nbsp;*</span>
                                        <asp:DropDownList runat="server" ID="ddlFinYear" CssClass="form-control form-control-sm" />
                                        
                                        <%-- OnTextChanged="EffectiveDateTextBox_Changed" AutoPostBack="True" OnSelectedIndexChanged="companyDropDownList_OnSelectedIndexChanged"  AutoPostBack="True" --%> 
                                    </div>
                                </div>
                                
                                 <div class="col-2">
                                    <div class="form-group">
                                        <label>Increment Type</label>
                                        <span style="color: red">&nbsp;*</span>
                                        <asp:DropDownList  runat="server" AutoPostBack="True" ID="ddlIncrementType" CssClass="form-control form-control-sm" />
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label runat="server" ID="Label4">Effective Date </label>
                                        <span style="color: red">&nbsp;*</span>
                                        <asp:HiddenField ID="areaHiddenField" runat="server" />
                                        <asp:HiddenField ID="areaCodeHiddenField" runat="server" />
                                        <asp:TextBox ID="EffectiveDateTextBox" AutoCompleteType="Disabled"  runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupPosition="TopLeft"
                                            Format="dd-MMM-yyyy" PopupButtonID="EffectiveDateTextBox" CssClass="MyCalendar"
                                            TargetControlID="EffectiveDateTextBox" />
                                    </div>
                                </div>

                            </div>
                            <div class="form-row" runat="server" id="HideFilter">
                                <div class="col-md-12">
                                    <fieldset class="for-panel">
                                        <legend>Searching Criteria</legend>
                                        <div class="form-row">
                                            <div class="col-2" runat="server" Visible="False">
                                                <div class="form-group">
                                                    <label>Employee Category</label>
                                                    <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control form-control-sm" />
                                                </div>
                                            </div>
                                            <div class="col-2" runat="server" Visible="False">
                                                <div class="form-group">
                                                    <label>Division</label>
                                                    <asp:DropDownList runat="server" ID="ddlDivision" CssClass="form-control form-control-sm SelectMe22" />
                                                     <script type="text/javascript">
                                                         function pageLoad() {
                                                             $('.SelectMe22').chosen({ disable_search_threshold: 5, search_contains: true });
                                                        }
</script>
                                                </div>
                                            </div>


                                              <div class="col-2" runat="server" Visible="False">
                                        <div class="form-group">
                                            <label>Department</label> 
                                            <asp:DropDownList runat="server" ID="ddlDepartment" CssClass="form-control form-control-sm SelectMe22" />
                                        </div>
                                    </div>
                                            
                                            
                                           
                                              <div class="col-2" runat="server" Visible="False">>
                                                        <div class="form-group">
                                                            <label>Office</label> 
                                                            <asp:DropDownList runat="server"  ID="ddlSalaryLocation"   class="form-control form-control-sm SelectMe22" />

                                                        </div>
                                                    </div>
                                               <div class="col-2" runat="server" Visible="False">
                                                <div class="form-group">
                                                    <label>Employee Type</label>
                                                    
                                                    <asp:DropDownList runat="server"   ID="ddlEmpType" class="form-control form-control-sm"   />

                                                </div>
                                            </div>
                                                    <div class="col-2" runat="server" Visible="False">
                                                        <div class="form-group">
                                                            <asp:CheckBox runat="server" ID="isProbition" Text="Is Probitionary"/>

                                                        </div>
                                                    </div>

                                            <div class="col-md-3" runat="server" Visible="False">

                                                <div class="form-group">
                                                    <label>Search Employee: </label>
                                                     <asp:DropDownList   runat="server"   ID="ddlEmpInfo" class="form-control form-control-sm SelectMe22" />
                                                   
                                                    <asp:TextBox Visible="False" ID="SearchEmployeeNameTextBoxTextBox" AutoPostBack="True" runat="server" class="form-control form-control-sm" OnTextChanged="SearchEmployeeNameTextBoxTextBox_OnTextChanged"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                        EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                        ServiceMethod="GetCompanyWiseEmployeeInfoOnlyForPromotion" ServicePath="~/WebService.asmx" TargetControlID="SearchEmployeeNameTextBoxTextBox"
                                                        UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                        ShowOnlyCurrentWordInCompletionListItem="true">
                                                    </asp:AutoCompleteExtender>


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
                                                    <asp:HiddenField ID="EmployeeIdHiddenField" runat="server" />
                                                </div>

                                            </div>
                                            
                                            <div class="col-2">
                                                <div class="form-group">
                                                    <label>Increment Percent </label>
                                                    
                                                    <asp:DropDownList runat="server"   ID="ddlFeed" class="form-control form-control-sm"   >

                                                        <asp:ListItem Value="1">5</asp:ListItem>
                                                        <asp:ListItem Value="2">10</asp:ListItem>
                                                        <asp:ListItem Value="3">15</asp:ListItem>
                                                        <asp:ListItem  Value="4">20</asp:ListItem>
                                                        <asp:ListItem  Value="5">25</asp:ListItem>
                                                        <asp:ListItem   Value="6">30</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                             
                                            <div class="col-1 " style="margin-top: 18px;" runat="server" Visible="False">
                                                <div class="form-group">
                                                    <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_OnClick" CssClass="btn btn-sm btn-success" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>




                           
                                <asp:GridView ID="loadGridView" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-sm table-bordered  text-center thead-dark" DataKeyNames="EmpInfoId,  DivisionId, DivisionWId, DepartmentId,  SectionId, SubSectionId, DesignationId,SalaryLoationId, JobLocationId , EmpTypeId
                                   "
                                  >
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkSelectAll" Enabled="False" runat="server" CssClass="form-control-sm" AutoPostBack="True" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" CssClass="form-control-sm"  Enabled="False"   runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee Code" />
                                        <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />

                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                        <asp:HiddenField runat="server" ID="HFdesignation" Value='<%#Eval("DesignationId")%>' />
                                                
                                                <asp:Label ID="lbldesignation"  Text='<%#Eval("Designation") %>' runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:HiddenField runat="server" ID="HFDepartment" Value='<%#Eval("DepartmentId")%>' />
                                                 <asp:HiddenField runat="server" ID="hffEmpMasterCode" Value='<%#Eval("EmpMasterCode")%>' />
                                                <asp:Label ID="lblDepartment"  Text='<%#Eval("DepartmentName") %>' runat="server"></asp:Label>
                                                

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="DateOfJoin" HeaderText="Date Of Joining" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="ServiceLength" HeaderText="Service Length(Dayes)" runat="server" Visible="False" />

                                        <asp:TemplateField HeaderText="Salary Grade">
                                            <ItemTemplate>
                                                  <asp:HiddenField runat="server" ID="HFSalaryGrade" Value='<%#Eval("SalaryGradeId")%>' />
                                                
                                                <asp:Label ID="lblSalaryGrade"  Text='<%#Eval("GradeName") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Current Salary Step">
                                            <ItemTemplate>
                                              <asp:HiddenField runat="server" ID="HFSalaryStep" Value='<%#Eval("SalaryStepId")%>' />
                                                
                                                <asp:Label ID="lblSalaryStep"  Text='<%#Eval("SalaryStepName") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Incremental Step">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="incrementalStepDropDownList" runat="server" CssClass="form-control form-control-sm SelectMe22" AutoPostBack="True" Width="170px"
                                                    OnTextChanged="incrementalStepDropDownList_OnTextChanged">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Feed Salary (%)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="feedSalaryTextBox"   runat="server" Text='<%#Eval("feedSalary") %>' CssClass="form-control form-control-sm"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtenderconvRate" runat="server"
                                                    Enabled="True" TargetControlID="feedSalaryTextBox" FilterType="Custom" ValidChars="0123456789"></asp:FilteredTextBoxExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                         
                              
                             <asp:GridView ID="KeyResponGridView" runat="server" Visible="False" Width="100%" CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="False">
                                                            <Columns>
                                                                                                                                 <asp:TemplateField HeaderText="Particulars">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Particulars" runat="server" Text='<%#Eval("ParticularsName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Salary Breakdown">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_SalaryBreakUp" runat="server" Text='<%#Eval("SalaryBreakUp") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                           
                                                            </Columns>
                                                        </asp:GridView>
                      
                            <div class="form-row">
                                <div class="col-12 ">
                                    <asp:RadioButtonList Enabled="False" ID="manualUpdateCheckBox" runat="server" CssClass="chkChoiceDesignation"  AutoPostBack="True" OnSelectedIndexChanged="manualUpdateCheckBox_OnSelectedIndexChanged">
                                      
                                        <asp:ListItem Value="0">Update Through Approval Process</asp:ListItem>
                                        <asp:ListItem Value="1">Skip Approval Process</asp:ListItem>
                                      
                                    </asp:RadioButtonList>
                                    
                                  
                                </div>
                            </div>
                            
                            <br />
                            
                            
                              <div class="row">
                                      <div class="col-md-6" runat="server">
                                        <div class="form-group">
                                            <label class="font-weight-bold">Comments &nbsp;</label><label style="font-size: 10px; color: gray; font-style: italic"> (*Applicable for Final Submit)</label>
                                            
                                         
                                            
                                            <asp:TextBox runat="server"   ID="txtComment" TextMode="MultiLine" Rows="2" class="form-control" />
                                        </div>
                                    </div>
                                 
                                  <div class="col-md-6" runat="server" Visible="False" id="DivShow">
                                      
                                                 <div style="max-height:180px; overflow: scroll">
                                                      <div class="form-group">
                                            <label class="font-weight-bold">Approval Status &nbsp;</label>
                                                          </div>
                                                <asp:GridView ID="AppLogCommentGridView" CssClass="table table-bordered text-center thead-dark" runat="server" Width="100%" AutoGenerateColumns="False">
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

                            <div class="form-row">
                                <div class="col-3 ">
                                    <div class="form-group">
                                          <asp:HiddenField runat="server" ID="hdpk" />
                                        <asp:Button ID="submitButton" Text="Submit" OnClick="submitButton_OnClick" Visible="False" CssClass="btn btn-sm btn-info" runat="server" />
                                        
                                         <asp:Button ID="btn_Update" Text="Update" OnClick="btn_Update_OnClick" Visible="False" CssClass="btn btn-sm btn-info" runat="server" />
                                        <asp:Button ID="clearButton" Text="Cancel" Visible="False" OnClick="clearButton_OnClick" CssClass="btn btn-sm btn-warning" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>




            </ContentTemplate>
        </asp:UpdatePanel>
    </div>



</asp:Content>


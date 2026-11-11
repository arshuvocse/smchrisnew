<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="KPIInformationDetailsView.aspx.cs" Inherits="Appraisal_KPIInformationDetailsView" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
     <style type="text/css">
        /*AutoComplete flyout */
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
    <div class="content">

        <asp:UpdatePanel runat="server" ID="upFormBody">
            <ContentTemplate>
                <div class="container-fluid">

                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px; "><img src="../Report_Pages/app.png" width="20px"  />  KPI Information</h1>
                        </div>

                        <%-- <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="detailsViewButton" Text="Appraisal List" OnClick="detailsViewButton_OnClick" CssClass="btn btn-sm btn-outline-secondary " runat="server"  />
                        </div>--%>

                        <div class="page-heading__container float-right d-none d-sm-block">
                            
                               <asp:LinkButton ID="LinkButton1" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                            
                         <%--   <asp:Button ID="detailsViewButton" Visible="True" Text="&#8920; Back To List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />--%>
                        </div>

                    </div>
                    <div class="card">
                        <div class="card-body">

                            <div class="form-row" runat="server" Visible="False">
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Name</label>
                                        <asp:TextBox runat="server" ReadOnly="True" OnTextChanged="txt_employee_OnTextChanged" AutoPostBack="True" CssClass="form-control form-control-sm" ID="txt_employee"></asp:TextBox>

                                        <%--<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>--%>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                            EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                            ServiceMethod="GetEployeeAutoComp2" ServicePath="~/WebService.asmx" TargetControlID="txt_employee"
                                            UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                            ShowOnlyCurrentWordInCompletionListItem="true">
                                        </cc1:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-2" runat="server" visible="false">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                        <asp:DropDownList ID="ddlFinancialYear" AutoPostBack="true" runat="server" class="form-control form-control-sm"></asp:DropDownList>
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

                            <div class="form-row"  runat="server" Visible="False">
                               

                                <div class="col-2" runat="server" visible="false">
                                    <div class="form-group">
                                        <label>Division Name :</label>
                                        <asp:Label ID="divisionNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="divitionIdHiddenField" runat="server" />
                                    </div>
                                </div>

                                
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Designation Name :</label>
                                      
                                        <asp:HiddenField ID="desigIdHiddenField" runat="server" />
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Department Name :</label>
                                      
                                        <asp:HiddenField ID="deptIdHiddenField" runat="server" />
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Joining Date :</label>
                                   
                                    </div>
                                </div>
                                
                                    <div class="col-2">
                                    <div class="form-group">
                                        <label>Job Location :</label>
                                       
                                    </div>
                                </div>
                                
                                    <div class="col-2">
                                    <div class="form-group">
                                        <label>Supervisor :</label>
                                     
                                    </div>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="col-3">
                                    <div class="form-group" runat="server" Visible="False">
                                        <label>Wing Name :</label>
                                        <asp:Label ID="divWingNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="divWingIdHiddenField" runat="server" />
                                    </div>
                                </div>
                                 <div class="col-3" runat="server" Visible="False">
                                    <div class="form-group">
                                        <label>Company Name :</label>
                                        <asp:Label ID="comNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="comIdHiddenField" runat="server" />
                                    </div>
                                </div>
                                <div class="col-3" runat="server" Visible="False">
                                    <div class="form-group">
                                        <label>Section Name :</label>
                                        <asp:Label ID="secNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="secIdHiddenField" runat="server" />
                                    </div>
                                </div>


                                <div class="col-3">
                                    <div class="form-group" runat="server" Visible="False">
                                        <label>Sub Section Name :</label>
                                        <asp:Label ID="subSectionLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="subSectionHiddenField" runat="server" />
                                    </div>
                                </div>

                                

                                <div class="col-3">
                                    <div class="form-group" runat="server" Visible="False">
                                        <label>Employee Type:</label>
                                        <asp:Label ID="employeeType" runat="server" class="form-control form-control-sm"></asp:Label>
                                        <asp:HiddenField ID="empTypeHiddenField" runat="server" />
                                    </div>
                                </div>
                                

                            </div>
                            <br/>
                            <div class="page-header text-center">
      <h1  class="elegantshd" >KPI Information</h1>
    </div>
             <style>
                 .elegantshd {
  color: #131313;
  
  letter-spacing: .15em;
  text-shadow: 2px 2px 4px #000000;
   text-decoration: underline;
     font-family: 'Kreon', serif;
 vertical-align:middle;  text-decoration-style: wavy;
}
             </style>
                            <br/>
                    
                                <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;">  A.Functional Area (75 Marks)</h2>
                            <hr/>
                            <asp:GridView runat="server" ShowFooter="true" AutoGenerateColumns="False" Width="100%" ID="gv_AppraisalFunc"  CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender" >

                                <Columns>

                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Key Performance Indicator" >
                                        <ItemTemplate>
                                            <asp:Label runat="server"  ID="txtKpi"   Text='<%#Eval("KpiInfo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Weight">
                                        <ItemTemplate>
                                            <asp:Label runat="server"   ID="txtWeight"  Text='<%#Eval("KpiWeight") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalWeight" ReadOnly="True" CssClass="orm-control  form-control-sm " runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Weight %">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ReadOnly="True" ID="txtWeightPer" AutoPostBack="True" OnTextChanged="txtWeightPer_OnTextChanged" CssClass="form-control  form-control-sm"  Text='<%#Eval("KpiWeightPer") %>'></asp:TextBox>
                                        </ItemTemplate>
                                      <%--  <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalWeight" CssClass="orm-control  form-control-sm " runat="server" />
                                        </FooterTemplate>--%>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Target">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ReadOnly="True" AutoPostBack="True" OnTextChanged="txtTarget_OnTextChanged" ID="txtTarget" CssClass="form-control  form-control-sm"   Text='<%#Eval("Target") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Target % " runat="server" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ReadOnly="True" ID="txtTargetPer" AutoPostBack="True" OnTextChanged="txtTargetPer_OnTextChanged" CssClass="form-control   form-control-sm"   Text='<%#Eval("TargetPer") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Deadline">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ReadOnly="True" ID="txtDeadLine" CssClass="form-control  form-control-sm" Text='<%#Eval("Deadline") %>'></asp:TextBox>


                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                TargetControlID="txtDeadLine" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Mid Year Status" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ReadOnly="True" ID="txtMidStatus" CssClass="form-control  form-control-sm" Text='<%#Eval("MidYearStatus") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Self-Mark" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtMark" ReadOnly="True" AutoPostBack="True" OnTextChanged="txtMark_OnTextChanged" CssClass="form-control  form-control-sm"      Text='<%#Eval("SelfMark") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalMark"  CssClass="orm-control  form-control-sm " runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                   <%-- <asp:TemplateField HeaderText="Is Active">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="isActiveCheckBox" Enabled="False" runat="server" Checked='<%#Eval("IsActive") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Operation" Visible="False">
                                        <ItemTemplate>

                                            <asp:LinkButton ID="btn_Add" OnClick="btn_Add_OnClick" runat="server">Add</asp:LinkButton>|
                                        <asp:LinkButton ID="lb_Remove" OnClick="lb_Remove_OnClick" runat="server">Remove</asp:LinkButton>
                                        </ItemTemplate>


                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                            
                            
                            
                            
                     
<br/>
                            <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;">     B.Behavioral Area (25 Marks) </h2>
                            <hr/>
                            <asp:GridView runat="server" ShowFooter="true" AutoGenerateColumns="False" Width="100%" ID="gv_AppraisalPartB" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender" >

                                <Columns>

                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Competencies / Skills">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="SkillInfo"    Text='<%#Eval("SkillInfo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Supporting Example">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="SupportingEmp" Text='<%#Eval("SupportingEmp") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Weight (Number)" >
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" AutoPostBack="True" ReadOnly="True" ID="Score" CssClass="form-control  form-control-sm" OnTextChanged="Score_OnTextChanged"      Text='<%#Eval("Score") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalScore" CssClass="form-control  form-control-sm " runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Expected Number">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ReadOnly="True"   ID="SetScore" CssClass="form-control  form-control-sm"  Text='<%#Eval("SetScore") %>'></asp:TextBox>
                                             </ItemTemplate>
                                             <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="ddllblTotalWeight" CssClass="form-control  form-control-sm " runat="server" />
                                        </FooterTemplate>
                                           </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Operation" Visible="False">
                                        <ItemTemplate>

                                            <asp:LinkButton ID="btn_Add_B" OnClick="btn_Add_B_OnClick" runat="server">Add</asp:LinkButton>|
                                        <asp:LinkButton ID="lb_Remove_b" OnClick="lb_Remove_b_OnClick" runat="server">Remove</asp:LinkButton>
                                        </ItemTemplate>


                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>



                                    <div class="form-row">
           <div class="col-md-12">
               <fieldset class="for-panel">
                   <legend>Approval Status List</legend>
                   <div style="overflow: scroll">
                       <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gv_KPI_App" CssClass="table table-bordered text-center thead-dark gridDatatable">

                           <Columns>

                               <asp:TemplateField HeaderText="SL#">
                                   <ItemTemplate>
                                       <%#Container.DataItemIndex+1 %>
                                   </ItemTemplate>
                               </asp:TemplateField>

                               <asp:TemplateField HeaderText="Employee ">
                                   <ItemTemplate>
                                       <asp:Label runat="server" ID="SkillInfo" Text='<%#Eval("Employee") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>

                               <%--   <asp:TemplateField HeaderText="Status">
                           <ItemTemplate>
                               <asp:Label runat="server" ID="SupportingEmp"  Text='<%#Eval("ApproveStatus") %>'></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>--%>


                               <%--  <asp:TemplateField HeaderText="Remarks">
                           <ItemTemplate>
                               <asp:Label runat="server" ID="Remarks"  TextMode="MultiLine" Text='<%#Eval("Remarks") %>'></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>--%>


                               <asp:TemplateField HeaderText="Comments">
                                   <ItemTemplate>
                                       <asp:Label runat="server" ID="Version" Text='<%#Eval("PreviousVersion") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField HeaderText="Approval Date">
                                   <ItemTemplate>
                                       <asp:Label runat="server" ID="Date" Text='<%#Eval("EntryDate") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Approval Status">
                                   <ItemTemplate>
                                       <asp:Label runat="server" ID="Datssse" Text='<%#Eval("ApproveStatus") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>


                           </Columns>
                       </asp:GridView>
               </fieldset>
           </div>

       </div>

                                                                                <br/>
                            <br/>
                            
                                <div class="page-header text-center">
      <h1  class="elegantshd">Mid-Year Information</h1>
    </div>
                            
                            
                              <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;">  A.Functional Area (75 Marks)</h2>
                            <hr/>
                            <asp:GridView runat="server" OnRowDataBound="gv_AppraisalFunc_OnRowDataBound" ShowFooter="true" AutoGenerateColumns="False" Width="100%" ID="gv_MidYearAppraisalFunc" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender"  DataKeyNames="AppraisalSelfFucAreaId">

                                <Columns>

                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Key Performance Indicator">
                                                            <ItemTemplate>
                                                                <asp:TextBox ReadOnly="True" Width="450px" runat="server" ID="txtKpi" class="form-control " Rows="2" TextMode="MultiLine" Text='<%#Eval("KpiInfo") %>'></asp:TextBox>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Weight (Number)">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="txtWeight"  ReadOnly="True"   CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("KpiWeight") %>'></asp:TextBox>
                                                               
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTotalWeight" CssClass="form-control  form-control-sm " runat="server" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Weight (%) ">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="txtWeightPer" ReadOnly="True" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("KpiWeightPer") %>'></asp:TextBox>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false" HeaderText="Target (Number)">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="txtTarget" ReadOnly="True" CssClass="form-control  form-control-sm"  TextMode="Number" Text='<%#Eval("Target") %>'></asp:TextBox>
                                                                
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Label ID="lbltarget" CssClass="form-control  form-control-sm " runat="server" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Target (%)" runat="server" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="txtTargetPer" ReadOnly="True" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("TargetPer") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Deadline">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="txtDeadLine" CssClass="form-control  form-control-sm" Enabled="False" Text='<%#Eval("Deadline") %>'></asp:TextBox>
                                                               
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="KPI Mid-Year Status">
                                                            <ItemTemplate>


                                                                                                                              <%--<asp:DropDownList Enabled="false"   Width="110px"  runat="server" ID="txtMidStatus" CssClass="form-control  form-control-sm"   >
    <asp:ListItem Value="0">Select One..</asp:ListItem>
    <asp:ListItem Value="Not Started">Not Started</asp:ListItem>
    <asp:ListItem Value="Ongoing">Ongoing</asp:ListItem>
    <asp:ListItem Value="Completed">Completed</asp:ListItem>
    <asp:ListItem Value="Not Completed">Not Completed</asp:ListItem>
</asp:DropDownList>--%>

                                                                
                                                                   <asp:TextBox runat="server" ReadOnly="True" ID="txtMidStatus" CssClass="form-control  form-control-sm" Text='<%#Eval("MidYearStatus") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Results (Year End)" runat="server" Visible="false" >
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="txtResult" CssClass="form-control  form-control-sm"  Text='<%#Eval("ResultYearEnd") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                           
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Mid-Year Self-Mark">
                                                            <ItemTemplate>
                                                                <asp:TextBox  runat="server" ReadOnly="True" ID="txtselfMark" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("SelfMark") %>'></asp:TextBox>

                                                                <%-- <asp:Label runat="server" ID="txtselfMark" CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("SelfMark") %>'></asp:Label>--%>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblselfMark" CssClass="form-control  form-control-sm " runat="server" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Mid-Year Supervisor Mark">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="txtMark"   CssClass="form-control  form-control-sm" TextMode="Number" Text='<%#Eval("SupervisorMark") %>'></asp:TextBox>


                                                             
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblTotalMark" CssClass="form-control  form-control-sm " runat="server" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField Visible="false" HeaderText="Is Active">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="isActiveCheckBox"   runat="server" Checked='<%#Eval("IsActive") %>' AutoPostBack="True" EnableTheming="True" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>





                                   <%-- <asp:TemplateField HeaderText="Operation" runat="server" Visible="false">
                                        <ItemTemplate>

                                            <asp:LinkButton ID="btn_Add" OnClick="btn_Add_OnClick" runat="server">Add</asp:LinkButton>|
                                        <asp:LinkButton ID="lb_Remove" OnClick="lb_Remove_OnClick" runat="server">Remove</asp:LinkButton>
                                        </ItemTemplate>


                                    </asp:TemplateField>--%>

                                </Columns>
                            </asp:GridView>
                            <br/>
                            <br/>
                                <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;">     B.Behavioral Area (25 Marks) </h2>
                            <hr/>
                            
                            <asp:GridView runat="server" OnRowDataBound="gv_AppraisalFunc1_OnRowDataBound"   ShowFooter="true" AutoGenerateColumns="False" Width="100%" id="gv_MidYearAppraisalBehave" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender" >
                                
                                                <Columns>

                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                    
                                   <asp:TemplateField HeaderText="Competencies / Skills">
                                    <ItemTemplate>
                                       <asp:Label runat="server"  ID="SkillInfo"    Text='<%#Eval("SkillInfo") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                    
                                   <asp:TemplateField HeaderText="Supporting Example">
                                    <ItemTemplate>
                                       <asp:Label runat="server"  ID="SupportingEmp"    Text='<%#Eval("SupportingEmp") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                    
                                <asp:TemplateField HeaderText="Weight (Number)">
                                    <ItemTemplate>
                                       <asp:TextBox runat="server" ID="Weight" AutoPostBack="True" ReadOnly="True"  CssClass="form-control  form-control-sm"       Text='<%#Eval("Score") %>' ></asp:TextBox>
                                    </ItemTemplate>
                                     <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblToWeight" CssClass="form-control  form-control-sm " runat="server" />
                                        </FooterTemplate>
                                </asp:TemplateField>
                                                    
                                                    
                                                       <asp:TemplateField HeaderText="Expected Number">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ReadOnly="True"   ID="SetScore" CssClass="form-control  form-control-sm"  Text='<%#Eval("SetScore") %>'></asp:TextBox>
                                             </ItemTemplate>
                                             <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="ddllblTotalWeight" CssClass="form-control  form-control-sm " runat="server" />
                                        </FooterTemplate>
                                           </asp:TemplateField>

                                <asp:TemplateField HeaderText="Mid-Year Self Score">
                                    <ItemTemplate>
                                       <asp:TextBox runat="server" ReadOnly="True"   ID="SelfScore" CssClass="form-control  form-control-sm"       Text='<%#Eval("SelfScore") %>' ></asp:TextBox>

                                                                                                                                                                                                                                                                                                                                                                                                                                                                    <cc1:FilteredTextBoxExtender ID="Fisasasdasdsasasare001sad" runat="server" Enabled="True"
TargetControlID="SelfScore" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                      <asp:Label ID="lblselfscrore"  CssClass="form-control  form-control-sm " runat="server"  />
                                                    </FooterTemplate>
                       


                                </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Mid-Year Supervisor Score">
                                    <ItemTemplate>
                                       <asp:TextBox runat="server"  AutoPostBack="True"  ReadOnly="True"  ID="SupervisorScore" CssClass="form-control  form-control-sm"       Text='<%#Eval("SupervisorScore") %>' ></asp:TextBox>


                                                                                                                                                                                                                                            <cc1:FilteredTextBoxExtender ID="Fil21SupeasdasrvisosadrScore0sad01" runat="server" Enabled="True"
TargetControlID="SupervisorScore" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>

                                    </ItemTemplate>
                                    
                                    <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                      <asp:Label ID="lblTotalMark"  CssClass="form-control  form-control-sm " runat="server"  />
                                                    </FooterTemplate>


                                </asp:TemplateField>



                               <%-- <asp:TemplateField HeaderText="Operation" runat="Server" Visible="false">
                                    <ItemTemplate>
                                        
                                        <asp:LinkButton ID="btn_Add" OnClick="btn_Add_OnClick" runat="server">Add</asp:LinkButton>|
                                        <asp:LinkButton ID="lb_Remove" OnClick="lb_Remove_OnClick"  runat="server">Remove</asp:LinkButton>
                                    </ItemTemplate>
                                    
                                  
                                </asp:TemplateField>--%>
                                
                            </Columns>
                            </asp:GridView>

                               <br/>
                            <br/>


                                                <br/>
                             
                        <br/>
                            <br/>
                            
                                    <div class="form-row">
           <div class="col-md-12" style="display:none">
               <fieldset class="for-panel">
                   <legend>Mid-Year Approval Status List</legend>
                   <div style="overflow: scroll">
                       <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gv_KPI_Mid_App" CssClass="table table-bordered text-center thead-dark gridDatatable">

                           <Columns>

                               <asp:TemplateField HeaderText="SL#">
                                   <ItemTemplate>
                                       <%#Container.DataItemIndex+1 %>
                                   </ItemTemplate>
                               </asp:TemplateField>

                               <asp:TemplateField HeaderText="Employee ">
                                   <ItemTemplate>
                                       <asp:Label runat="server" ID="SkillInfo" Text='<%#Eval("Employee") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>

                               <asp:TemplateField HeaderText="Comments">
                                   <ItemTemplate>
                                       <asp:Label runat="server" ID="Version" Text='<%#Eval("PreviousVersion") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField HeaderText="Approval Date">
                                   <ItemTemplate>
                                       <asp:Label runat="server" ID="Date" Text='<%#Eval("EntryDate") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Approval Status">
                                   <ItemTemplate>
                                       <asp:Label runat="server" ID="Datssse" Text='<%#Eval("ApproveStatus") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>


                           </Columns>
                       </asp:GridView>
               </fieldset>
           </div>

       </div>
                            
                                <div class="page-header text-center">
      <h1  class="elegantshd">Appraisal Information</h1>
    </div>

                            
                              <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;">  A.Functional Area (75 Marks)</h2>
                            <hr/>
                            <asp:GridView runat="server" OnRowDataBound="gv_AppraisalFunc_OnRowDataBound" ShowFooter="true" AutoGenerateColumns="False" Width="100%" ID="GridView1" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender"  DataKeyNames="AppraisalSelfFucAreaId">

                                <Columns>

                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Key Performance Indicator">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="txtKpi"   Text='<%#Eval("KpiInfo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Weight (Number)">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtWeight" AutoPostBack="True"  ReadOnly="True"  CssClass="form-control  form-control-sm"      Text='<%#Eval("KpiWeight") %>'></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                TargetControlID="txtWeight" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalWeight" CssClass="form-control  form-control-sm " runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Weight (%) ">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtWeightPer" AutoPostBack="true" ReadOnly="True"  CssClass="form-control  form-control-sm"      Text='<%#Eval("KpiWeightPer") %>'></asp:TextBox>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Target (Number)">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtTarget" AutoPostBack="True"  ReadOnly="True"  CssClass="form-control  form-control-sm"      Text='<%#Eval("Target") %>'></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2122" runat="server" Enabled="True"
                                                TargetControlID="txtTarget" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lbltarget" CssClass="form-control  form-control-sm " runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Target (%)" runat="server" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtTargetPer" AutoPostBack="True"  ReadOnly="True"  CssClass="form-control  form-control-sm"      Text='<%#Eval("TargetPer") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Dead Line">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtDeadLine" CssClass="form-control  form-control-sm" ReadOnly="True" Text='<%#Eval("Deadline") %>'></asp:TextBox>
                                   <%--         <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                Format="dd-MMM-yyyy" CssClass="custom"
                                                TargetControlID="txtDeadLine" />--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Mid Year Status">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ReadOnly="True" ID="txtMidStatus" CssClass="form-control  form-control-sm" Text='<%#Eval("MidYearStatus") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Results (Year End)" >
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ReadOnly="True" ID="txtResult" CssClass="form-control  form-control-sm"      Text='<%#Eval("ResultYearEnd") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Self-Mark">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ReadOnly="True" ID="txtselfMark" CssClass="form-control  form-control-sm"      Text='<%#Eval("SelfMark") %>'></asp:TextBox>
                                            
                                           <%-- <asp:Label runat="server" ID="txtselfMark" CssClass="form-control  form-control-sm"      Text='<%#Eval("SelfMark") %>'></asp:Label>--%>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblselfMark" CssClass="form-control  form-control-sm " runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Supervisor Mark">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtMark" AutoPostBack="True" ReadOnly="True" CssClass="form-control  form-control-sm"      Text='<%#Eval("SupervisorMark") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalMark" CssClass="form-control  form-control-sm " runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>







                                   <%-- <asp:TemplateField HeaderText="Operation" runat="server" Visible="false">
                                        <ItemTemplate>

                                            <asp:LinkButton ID="btn_Add" OnClick="btn_Add_OnClick" runat="server">Add</asp:LinkButton>|
                                        <asp:LinkButton ID="lb_Remove" OnClick="lb_Remove_OnClick" runat="server">Remove</asp:LinkButton>
                                        </ItemTemplate>


                                    </asp:TemplateField>--%>

                                </Columns>
                            </asp:GridView>
                            <br/>
                            <br/>
                                <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;">     B.Behavioral Area (25 Marks) </h2>
                            <hr/>
                            
                            <asp:GridView runat="server" OnRowDataBound="gv_AppraisalFunc1_OnRowDataBound"   ShowFooter="true" AutoGenerateColumns="False" Width="100%" id="GridView2" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender" >
                                
                                                <Columns>

                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                    
                                   <asp:TemplateField HeaderText="Competencies / Skills">
                                    <ItemTemplate>
                                       <asp:Label runat="server"  ID="SkillInfo"    Text='<%#Eval("SkillInfo") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                    
                                   <asp:TemplateField HeaderText="Supporting Example">
                                    <ItemTemplate>
                                       <asp:Label runat="server"  ID="SupportingEmp"    Text='<%#Eval("SupportingEmp") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                    
                                <asp:TemplateField HeaderText="Weight (Number)">
                                    <ItemTemplate>
                                       <asp:TextBox runat="server" ID="Weight" AutoPostBack="True" ReadOnly="True"  CssClass="form-control  form-control-sm"       Text='<%#Eval("Score") %>' ></asp:TextBox>
                                    </ItemTemplate>
                                     <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lblToWeight" CssClass="form-control  form-control-sm " runat="server" />
                                        </FooterTemplate>
                                </asp:TemplateField>
                                                    
                                                    
                                                       <asp:TemplateField HeaderText="Expected Number">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ReadOnly="True"   ID="SetScore" CssClass="form-control  form-control-sm"  Text='<%#Eval("SetScore") %>'></asp:TextBox>
                                             </ItemTemplate>
                                             <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="ddllblTotalWeight" CssClass="form-control  form-control-sm " runat="server" />
                                        </FooterTemplate>
                                           </asp:TemplateField>

                                <asp:TemplateField HeaderText="Self Score">
                                    <ItemTemplate>
                                       <asp:TextBox runat="server" ReadOnly="True"   ID="SelfScore" CssClass="form-control  form-control-sm"       Text='<%#Eval("SelfScore") %>' ></asp:TextBox>

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            <cc1:FilteredTextBoxExtender ID="Fisasaasdsdasdsasasare001sad" runat="server" Enabled="True"
TargetControlID="SelfScore" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>

                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                      <asp:Label ID="lblselfscrore"  CssClass="form-control  form-control-sm " runat="server"  />
                                                    </FooterTemplate>
                       


                                </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Supervisor Score">
                                    <ItemTemplate>
                                       <asp:TextBox runat="server"  AutoPostBack="True"  ReadOnly="True"  ID="SupervisorScore" CssClass="form-control  form-control-sm"       Text='<%#Eval("SupervisorScore") %>' ></asp:TextBox>

                                                                                                                                                                                                                                            <cc1:FilteredTextBoxExtender ID="Filasd21SupervisadassorScore001" runat="server" Enabled="True"
TargetControlID="SupervisorScore" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                    </ItemTemplate>
                                    
                                    <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                      <asp:Label ID="lblTotalMark"  CssClass="form-control  form-control-sm " runat="server"  />
                                                    </FooterTemplate>


                                </asp:TemplateField>



                               <%-- <asp:TemplateField HeaderText="Operation" runat="Server" Visible="false">
                                    <ItemTemplate>
                                        
                                        <asp:LinkButton ID="btn_Add" OnClick="btn_Add_OnClick" runat="server">Add</asp:LinkButton>|
                                        <asp:LinkButton ID="lb_Remove" OnClick="lb_Remove_OnClick"  runat="server">Remove</asp:LinkButton>
                                    </ItemTemplate>
                                    
                                  
                                </asp:TemplateField>--%>
                                
                            </Columns>
                            </asp:GridView>

                            


                               <br/>
                            <br/>
                                <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;">     C.Training  </h2>
                            <hr/>
                                <asp:GridView runat="server"    AutoGenerateColumns="False" Width="100%" id="gv_AppraisalTraining" CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender" >
                                
                                                <Columns>

                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                    
                                   <asp:TemplateField HeaderText="Training Needs">
                                    <ItemTemplate>
                                       <asp:Label runat="server" ID="TrainingNeeds"     Text='<%#Eval("TrainingNeeds") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                    
                                   <asp:TemplateField HeaderText="Quater">
                                    <ItemTemplate>
                                             <asp:DropDownList ID="QuaterDropDownList1" AutoPostBack="true" runat="server" Enabled="False" CssClass="form-control form-control-sm">
                                                      <asp:ListItem Text="1st Quarter" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="2nd Quarter" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="3rd Quarter" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="4th Quarter" Value="4"></asp:ListItem>

                                             </asp:DropDownList>
                                      
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                    
                            
                                
                            </Columns>
                            </asp:GridView>
                            
                             <br/>
                            <br/>
                                <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;">     D.Overall Status  </h2>
                            <hr/>
                                   <div class="col-md-12">
                                         
                                          <table class="table table-bordered table-striped">
                                                    <tr>
                                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Total Marks Obtained (Out Of 100)</td>
                                                        
                                                        
                                                          <td class="tblTHColorChang" style="width: 10%; padding: 10px;">A:Functional</td>

                                                        <td> <asp:Label runat="server" ID="partAScore"></asp:Label></td>

                                                        
                                                         <td  class="tblTHColorChang" style="width: 10%; padding: 10px;">B:Behavioral</td>
                                                        <td>    <asp:Label ID="partBScore"  runat="server"></asp:Label></td>
                                                        
                                                        
                                                            <td  class="tblTHColorChang" style="width: 10%; padding: 10px;">Total</td>
                                                        <td>    <asp:Label ID="totalScore"  runat="server"></asp:Label></td>
                                                        
                                                        
                                                                    <td  class="tblTHColorChang" style="width: 10%; padding: 10px;">Over All Status</td>
                                                        <td>    <asp:Label ID="lblStatus"  runat="server"></asp:Label></td>


                                                    </tr>
                                              </table>

                                     </div>
                                     <div class="col-md-12">
                                         
                            
                                            <div class="form-row">
                                               <div class="col-md-4">
                                                    <asp:RadioButtonList runat="server" ID="recommend" Enabled="False" AutoPostBack="True" OnSelectedIndexChanged="recommend_SelectedIndexChanged">

                                                   <asp:ListItem Text="Promotion with Increment" Value="6" />
                                                    <asp:ListItem Text="Special Increment" Value="2" />
                                                    <asp:ListItem Text="Promotion" Value="3" />
                                                    <asp:ListItem Text="Performance Improvement Plan" Value="4" />
                                                    <%--<asp:ListItem Text="Promotion with Additional Increment" Value="5" />--%>
                                                   
                                                          <asp:ListItem Text="General Increment"  Value="1" />
                                                </asp:RadioButtonList>
                                               </div>
                                                   <div class="col-md-2" id="Divsteps"   runat="server" visible="False">
                                                        <div class="form-group">
                                                        <label>Steps</label>
                                                        <asp:TextBox ID="txtStep" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteresfsafsadTextBoxExtenderqty" runat="server"
                                                            Enabled="True" TargetControlID="txtStep" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                    </div>
                                                       </div>
                                                
                                                
                                                 <div class="col-md-2" id="Div1Other"   runat="server" visible="False">
                                                        <div class="form-group">
                                                      
                                                        <label>Special Increment</label>
                                                        <asp:TextBox ID="txtnote" CssClass="form-control" TextMode="MultiLine" Rows="2" runat="server"></asp:TextBox>
                                                    </div>
                                                       </div>
                                                
                                                 <div class="col-md-6">
                                                       <div class="form-group">
                                                      
                                                        <label>Promotion & Increment:</label>
                                                        <asp:TextBox ID="txtJustification" CssClass="form-control" TextMode="MultiLine" Rows="2" runat="server"></asp:TextBox>
                                                           
                                                           <br/>
                                                           
                                                           

                                                    </div>
                                                      <div class="form-group">
                                                       
                                                              <label>Document Info:</label>
                                                          <br/>
                                                                   <asp:HyperLink ID="HLDocumentLink"   CssClass=" btn-sm btn btn-outline-dark" Target="_blank"  runat="server"  Text='Preview' ></asp:HyperLink>
                                                          
                                                             <asp:Label runat="server" ID="lbFileName"></asp:Label>
                                                          </div>
                                                     

                    </div>


                    </div>

                                    <div class="form-row">
           <div class="col-md-12" style="display:none">
               <fieldset class="for-panel">
                   <legend>Appraisal Approval Status List</legend>
                   <div style="overflow: scroll">
                       <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gv_Appraisal_App" CssClass="table table-bordered text-center thead-dark gridDatatable">

                           <Columns>

                               <asp:TemplateField HeaderText="SL#">
                                   <ItemTemplate>
                                       <%#Container.DataItemIndex+1 %>
                                   </ItemTemplate>
                               </asp:TemplateField>

                               <asp:TemplateField HeaderText="Employee ">
                                   <ItemTemplate>
                                       <asp:Label runat="server" ID="SkillInfo" Text='<%#Eval("Employee") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>

                               <asp:TemplateField HeaderText="Comments">
                                   <ItemTemplate>
                                       <asp:Label runat="server" ID="Version" Text='<%#Eval("PreviousVersion") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField HeaderText="Approval Date">
                                   <ItemTemplate>
                                       <asp:Label runat="server" ID="Date" Text='<%#Eval("EntryDate") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Approval Status">
                                   <ItemTemplate>
                                       <asp:Label runat="server" ID="Datssse" Text='<%#Eval("ApproveStatus") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>


                           </Columns>
                       </asp:GridView>
               </fieldset>
           </div>

       </div>

                            <div runat="server" Visible="False">
                            <asp:GridView runat="server"  AutoGenerateColumns="False" Width="100%" ID="gv_Versions"  CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender" >

                                <Columns>

                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Employee ">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="SkillInfo" CssClass="form-control  form-control-sm"  Text='<%#Eval("Employee") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="SupportingEmp" CssClass="form-control  form-control-sm"  Text='<%#Eval("ApproveStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                
                                      <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Remarks" CssClass="form-control  form-control-sm" TextMode="MultiLine" Text='<%#Eval("Remarks") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                      
                                      <asp:TemplateField HeaderText="Version">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Version" CssClass="form-control  form-control-sm"  Text='<%#Eval("PreviousVersion") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Date" CssClass="form-control  form-control-sm"  Text='<%#Eval("EntryDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                   

                                </Columns>
                            </asp:GridView>
                            
                                <div/>

                            <asp:HiddenField runat="server" ID="id_mastetID" />
                            <asp:HiddenField runat="server" ID="id_Empid" />
                            <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" CssClass="btn btn-sm btn-info" Text="Submit"></asp:Button>

                            <asp:Button ID="submitButton" Text="Save" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="btn_Save_OnClick" />
                            <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="editButton_OnClick" />
                            <%--<asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="delButton_OnClick" />--%>

                            <%--<asp:Button ID="cancelButton" Text="Cancel" OnClick="cancelButton_Click" CssClass="btn btn-sm warning" runat="server"  BackColor="#FFCC00" />--%>
                        
                    </div>
                </div>
            </ContentTemplate>

        </asp:UpdatePanel>

    </div>
</asp:Content>

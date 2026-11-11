<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="EmpDetails_Sap.aspx.cs" Inherits="HealthCare_UI_EmpDetails_Sap" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    
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
                            <h1 class="title" style="font-size: 18px; padding-top: 0px; "><img src="../Report_Pages/app.png" width="20px"  />  Employee Details Information</h1>
                        </div>

                        <%-- <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="detailsViewButton" Text="Appraisal List" OnClick="detailsViewButton_OnClick" CssClass="btn btn-sm btn-outline-secondary " runat="server"  />
                        </div>--%>

                        <div class="page-heading__container float-right d-none d-sm-block">
                            
                               <asp:LinkButton ID="LinkButton1" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                            
                            <asp:Button ID="detailsViewButton" Visible="True" Text="&#8920; Back To List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
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
                                                        <td  class="tblTHColorChang" style="width: 20%; padding: 10px;">Division</td>
                                                        <td> <asp:Label runat="server" ID="lblEmployee"></asp:Label>
                                                            
                                                             <asp:HiddenField runat="server" ID="SapAction"/>
                                                              <asp:HiddenField runat="server" ID="EmpCode"/>
                                                             <asp:HiddenField runat="server" ID="SapDivi"/>
                                                            <asp:HiddenField runat="server" ID="SapDepartment"/>
                                                            <asp:HiddenField runat="server" ID="SapSection"/>
                                                            <asp:HiddenField runat="server" ID="SapDesig"/>

                                                             <asp:HiddenField runat="server" ID="SapJobLocationID"/>
                                                             <asp:HiddenField runat="server" ID="SapSalaryLoationId"/>
                                                             <asp:HiddenField runat="server" ID="SapSalGrade"/>
                                                            
                                                            
                                                             <asp:HiddenField runat="server" ID="SapEmpCategoryId"/>
                                                             <asp:HiddenField runat="server" ID="SApEmpTypeId"/>
                                                         
                                                        </td>

                                                         
                                                              
                                                         <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Department</td>
                                                        <td>  <asp:Label ID="deptNameLabel"  runat="server"></asp:Label></td>
                                                    </tr>
                                                    
                                                  <tr>
                                                        <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Designation</td>
                                                        <td>   <asp:Label ID="desigNameLabel" runat="server"></asp:Label></td>

                                                     <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Employee Type</td>
                                                        <td>  <asp:Label ID="EmpTtype"   runat="server"></asp:Label></td>

                                                    </tr>
                                                                                                                           
                                                  <tr>
                                                        <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Father Name </td>
                                                        <td> <asp:Label runat="server" ID="FName"></asp:Label></td>

                                                        
                                                         <td  class="tblTHColorChang" style="width: 20%; padding: 10px;">Father Date Of Birth</td>
                                                        <td>  <asp:Label ID="FDob"  runat="server"></asp:Label></td>
                                                    </tr>
                                                                                                                                                                                                                                                                                                           
                                                  <tr>
                                                        <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">FDType</td>
                                                        <td>     <asp:Label ID="FDType"  runat="server"></asp:Label></td>
                                                              <td style="width: 20%; padding: 10px;" class="tblTHColorChang" >Mother Name</td>
                                                        <td> <asp:Label runat="server" ID="MName"></asp:Label></td>
                                                    </tr>
                                         
                                                  <tr>
                                                        <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Father Date Of Date</td>
                                                        <td><asp:Label ID="FDod" runat="server"></asp:Label></td>
                                                              <td style="width: 20%; padding: 10px;" class="tblTHColorChang">Mother Date Of Date</td>
                                                        <td><asp:Label runat="server" ID="MDod"></asp:Label></td>
                                                    </tr>
                                                                                                                         
                                                  <tr>
                                                        <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Mother Date of Birth</td>
                                                        <td><asp:Label ID="MDob" runat="server"></asp:Label></td>

                                                        <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">MDType</td>
                                                        <td><asp:Label ID="MDType" runat="server"></asp:Label></td>
                                                    </tr>
                                                                                                                                                              
                                                  <tr>
                                                        <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Mobile</td>
                                                        <td>   <asp:Label ID="Mobile" runat="server"></asp:Label></td>

                                                        <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Email</td>
                                                        <td>  <asp:Label ID="Email"   runat="server"></asp:Label></td>
                                                    </tr>
                                                                                                                        
                                                  <tr>
                                                        <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">JobLocId</td>
                                                        <td>   <asp:Label ID="JobLocId" runat="server"></asp:Label></td>

                                                     <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Office</td>
                                                        <td>  <asp:Label ID="Office"   runat="server"></asp:Label></td>

                                                    </tr>
                                                                                       
                                                  <tr>
                                                        <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Salary Grade</td>
                                                        <td>   <asp:Label ID="SalGrade" runat="server"></asp:Label></td>

                                                     <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Salary Step</td>
                                                        <td>  <asp:Label ID="SalStep"   runat="server"></asp:Label></td>

                                                    </tr>
                                         
                                                  <tr>
                                                        <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Reporting boss </td>
                                                        <td>   <asp:Label ID="ReportId" runat="server"></asp:Label></td>

                                                     <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Employee Category</td>
                                                        <td>  <asp:Label ID="EmpCat"   runat="server"></asp:Label></td>

                                                    </tr>
                                                                                 
                                                  <tr>
                                                        <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Is  SpTransfer</td>
                                                        <td>   <asp:Label ID="IsSpTransfer" runat="server"></asp:Label></td>

                                                     <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Section</td>
                                                        <td>  <asp:Label ID="SectionId"   runat="server"></asp:Label></td>

                                                    </tr>
                                
                                
                                   <tr>
                                                        <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">kpiApprId</td>
                                                        <td>   <asp:Label ID="kpiApprId" runat="server"></asp:Label></td>

                                                     <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Previous EmpCode</td>
                                                        <td>  <asp:Label ID="PreviousEmpCode"   runat="server"></asp:Label></td>

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
                                
                            
                             <h1  class="elegantshd" >Spouse Information</h1>

                            
                            <asp:GridView runat="server" ShowFooter="true" AutoGenerateColumns="False" Width="100%" ID="gv_AppraisalFunc"  CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender" >

                                <Columns>

                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="SNo" >
                                        <ItemTemplate>
                                            <asp:Label runat="server"  ID="txtKpji"   Text='<%#Eval("SNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    
                                    <asp:TemplateField HeaderText="Name" >
                                        <ItemTemplate>
                                            <asp:Label runat="server"  ID="txyhjtKpi"   Text='<%#Eval("SName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    
                                    
                                               <asp:TemplateField HeaderText="Date of Birth" >
                                        <ItemTemplate>
                                            <asp:Label runat="server"  ID="txtheKpi"   Text='<%#Eval("SDob") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                            <asp:TemplateField HeaderText="Date of Death" >
                                        <ItemTemplate>
                                            <asp:Label runat="server"  ID="txtjkjkheKpi"   Text='<%#Eval("SDod") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    
                                    
                                               <asp:TemplateField HeaderText="SDType" >
                                        <ItemTemplate>
                                            <asp:Label runat="server"  ID="txtMidS5tatus"   Text='<%#Eval("SDType") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                                                                                          
                                </Columns>
                            </asp:GridView>
                            
                            
                            
                             <h1  class="elegantshd" >Child Information</h1>

                                                        
                            
                             <asp:GridView runat="server" ShowFooter="true" AutoGenerateColumns="False" Width="100%" ID="GridView1"  CssClass="AddToListCssTable"   OnPreRender="gv_DocumentUpload_PreRender" >

                                <Columns>

                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="CNo" >
                                        <ItemTemplate>
                                            <asp:Label runat="server"  ID="txtKpi"   Text='<%#Eval("CNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                                                       

                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                         
                                             <asp:Label runat="server"  ID="txtDeadLine"   Text='<%#Eval("CName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                                                                                                                                             
                                    <asp:TemplateField HeaderText="Date of Birth" >
                                        <ItemTemplate>
                                                                                                                                                                   
                                             <asp:Label runat="server"  ID="txtMidStatus1"   Text='<%#Eval("CDob") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                        <asp:TemplateField HeaderText="Date of Death" >
                                        <ItemTemplate>
                                                                                                    
                                              <asp:Label runat="server"  ID="txtMidStatus6"   Text='<%#Eval("CDod") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                                                       
                                    <asp:TemplateField HeaderText="CGen" >
                                        <ItemTemplate>
                                                         
                                             <asp:Label runat="server"  ID="txtMidStatus7"   Text='<%#Eval("CGen") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                                                                                                                                
                                         <asp:TemplateField HeaderText="CDType" >
                                        <ItemTemplate>
                                                                              
                                             <asp:Label runat="server"  ID="txtMidStatus2"   Text='<%#Eval("CDType") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                     
                                </Columns>
                            </asp:GridView>
                            
                            
                            
                            <br/>
                            
                            
                         
                            <div class="row">
                                                                          
                                <div class="col-md-4">
                                    
                                </div>
                                <div class="col-md-4">
                                    <asp:LinkButton runat="server" ID="LinkButton2" OnClick="btn_save_OnClick" CssClass="btn btn-sm btn-success" ><i class="fa fa-check" aria-hidden="true"></i>Approved</asp:LinkButton>
                                </div>
                                                                                 
                            </div>

                                                                                                                        
                    </div>
                </div>
            </ContentTemplate>

        </asp:UpdatePanel>

    </div>

</asp:Content>


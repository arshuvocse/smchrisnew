<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master"  AutoEventWireup="true" CodeFile="AppraisalPartBOwn.aspx.cs" Inherits="Appraisal_AppraisalPartBOwn" %>


<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    
    <div class="content">
        
        <asp:UpdatePanel runat="server" ID="upFormBody">
              <ContentTemplate>
                   <div class="container-fluid">
                           <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" />  Appraisal Behavioral Part</h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                            
                            
                            <asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                          

                             <asp:Button ID="Button1" Text="&#8920; Back to List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                               
                        </div>
                    </div>
                        <div class="card">
                           <div class="card-body">
                               
                               
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
                                                        <td>    <asp:Label ID="ReportingLabel"  runat="server"></asp:Label></td>
                                                    </tr>
                                                    
                                                     <tr>
                                                        <td  class="tblTHColorChang" style="width: 20%; padding: 10px;">Employee Name</td>
                                                        <td>   <asp:Label ID="empName" runat="server"></asp:Label></td>

                                                         
                                                              
                                                         <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Department</td>
                                                        <td>    <asp:Label ID="deptNameLabel"  runat="server"></asp:Label></td>
                                                    </tr>
                                                    
                                                     <tr>
                                                        <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Designation</td>
                                                        <td>     <asp:Label ID="desigNameLabel"   runat="server"></asp:Label></td>

                                                     <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Office</td>
                                                        <td>    <asp:Label ID="LocationLabel"  runat="server"></asp:Label></td>

                                                    </tr>
                                                    
                                                    
                                                    
                                                    
                                                     
                                                    
                                                     <tr>
                                                        <td class="tblTHColorChang"  style="width: 20%; padding: 10px;">Date Of Joining</td>
                                                        <td>     <asp:Label ID="joiningDateLabel" runat="server"></asp:Label></td>
  <td style="width: 20%; padding: 10px;" class="tblTHColorChang" >Place</td>
                                                        <td> <asp:Label runat="server" ID="lblPlace"></asp:Label></td>
                                                    </tr>
                                                    
                                                    
                                                 
                                                    
                                                    
                                                    </table>
                            </div>
                               
                               

                                 <div class="form-row" runat="server" Visible="False">
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                        <asp:DropDownList ID="ddlFinancialYear" AutoPostBack="true" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                             <div class="form-row" runat="server" Visible="False">
                                <div class="col-2">
                                    <div class="form-group">
                                         <label>Employee Name :</label>
                                      
                                    </div>
                                </div>
                                <div class="col-2" runat="server" visible="false">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                        <asp:DropDownList ID="DropDownList1" AutoPostBack="true" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
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
                            <div class="form-row"  runat="server" Visible="False">
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
                            <%--<div class="form-row">
                                           <div class="col-3">
                                    <div class="form-group">
                                        <label>Employee Name :</label>
                                        <asp:Label ID="empName" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Company Name :</label>
                                        <asp:Label ID="comNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="comIdHiddenField" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Division Name :</label>
                                        <asp:Label ID="divisionNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="divitionIdHiddenField" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label> Wing Name :</label>
                                        <asp:Label ID="divWingNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="divWingIdHiddenField" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Department Name :</label>
                                        <asp:Label ID="deptNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="deptIdHiddenField" runat="server" />
                                    </div>
                                </div>
                                         
                                         <div class="col-3">
                                    <div class="form-group">
                                        <label>Section Name :</label>
                                        <asp:Label ID="secNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="secIdHiddenField" runat="server" />
                                    </div>
                                </div>


                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Sub Section Name :</label>
                                        <asp:Label ID="subSectionLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="subSectionHiddenField" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Designation Name :</label>
                                        <asp:Label ID="desigNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="desigIdHiddenField" runat="server" />
                                    </div>
                                </div>

                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Employee Type:</label>
                                        <asp:Label ID="employeeType" runat="server" class="form-control form-control-sm"></asp:Label>
                                        <asp:HiddenField ID="empTypeHiddenField" runat="server" />
                                    </div>
                                </div>
                                  <div class="col-3">
                                    <div class="form-group">
                                        <label>Joining Date :</label>
                                        <asp:Label ID="joiningDateLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>--%>

<br/>
                                 <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;">  B.BEHAVIORAL AREA</h2>
                            <hr/>
                            
                        <asp:GridView runat="server"   ShowFooter="true" AutoGenerateColumns="False" Width="100%" id="gv_AppraisalPartB" CssClass="table table-bordered text-center thead-dark gridDatatable">
                                
                                                <Columns>

                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                    
                                   <asp:TemplateField HeaderText="Competencies / Skills">
                                    <ItemTemplate>
                                       <asp:Label runat="server" ID="SkillInfo"  Text='<%#Eval("SkillInfo") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                    
                                   <asp:TemplateField HeaderText="Supporting Example">
                                    <ItemTemplate>
                                       <asp:Label runat="server" ID="SupportingEmp"   Text='<%#Eval("SupportingEmp") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                    
                                <asp:TemplateField HeaderText="Weight (Number)">
                                    <ItemTemplate>
                                       <asp:Label runat="server" ID="Weight"   Text='<%#Eval("Score") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                              
                                </asp:TemplateField>
                                                    
                                                    
                                                        <asp:TemplateField HeaderText="Weight (Number)">
                                    <ItemTemplate>
                                       <asp:Label runat="server" ID="SetScore"   Text='<%#Eval("SetScore") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                            <asp:Label ID="ddllblTotalSetScore" CssClass="form-control  form-control-sm " runat="server" />
                                        </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Score">
                                    <ItemTemplate>
                                       <asp:TextBox runat="server"  ID="SelfScore" AutoPostBack="True" OnTextChanged="SelfScore_OnTextChanged"  CssClass="form-control  form-control-sm"  TextMode="Number" Text='<%#Eval("SelfScore") %>' ></asp:TextBox>

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            <cc1:FilteredTextBoxExtender ID="Fisasasdasdsasasare001sad" runat="server" Enabled="True"
TargetControlID="SelfScore" FilterType="Custom" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>

                                    </ItemTemplate>
                                    
                                    <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                      <asp:Label ID="lblTotalMarkSelf"  CssClass="form-control  form-control-sm " runat="server"  />
                                                    </FooterTemplate>


                                </asp:TemplateField>



                             
                            </Columns>
                            </asp:GridView>
                               
                               <asp:HiddenField runat="server" id="id_mastetID"/>
                              <asp:HiddenField runat="server" id="id_selfID"/>
                               
                                      <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick"  CssClass="btn btn-sm btn-info" Text="Submit"></asp:Button>
                                         <%--<asp:Button runat="server" ID="btn_Review" OnClick="btn_Review_OnClick" CssClass="btn btn-sm btn-info" Text="Review"></asp:Button>--%>
                                  <%--   <asp:Button ID="cancelButton" Text="Cancel"  CssClass="btn btn-sm warning" runat="server"  BackColor="#FFCC00" />--%>
                           </div>
                        </div>
                    </div>
              </ContentTemplate>
           
        </asp:UpdatePanel>
        
    </div>
</asp:Content>
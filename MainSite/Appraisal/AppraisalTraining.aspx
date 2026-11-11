<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="AppraisalTraining.aspx.cs" Inherits="Appraisal_AppraisalTraining" %>

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
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" /> Appraisal Training Part</h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                           <asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                          

                             <asp:Button ID="Button1" Text="&#8920; Back to List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                        </div>

                    </div>
                        <div class="card">
                           <div class="card-body" >
                               
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
                               
                               

                                       <div class="form-row" runat="server" visible="False">
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                        <asp:DropDownList ID="ddlFinancialYear" AutoPostBack="true" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-row" runat="server" visible="False">
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
                            <div class="form-row" runat="server" visible="False">


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
                            <div class="form-row" runat="server" visible="False">
                                <div class="col-3">
                                    <div class="form-group" runat="server" visible="False">
                                        <label>Wing Name :</label>
                                        <asp:Label ID="divWingNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="divWingIdHiddenField" runat="server" />
                                    </div>
                                </div>
                                <div class="col-3" runat="server" visible="False">
                                    <div class="form-group">
                                        <label>Company Name :</label>
                                        <asp:Label ID="comNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="comIdHiddenField" runat="server" />
                                    </div>
                                </div>
                                <div class="col-3" runat="server" visible="False">
                                    <div class="form-group">
                                        <label>Section Name :</label>
                                        <asp:Label ID="secNameLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="secIdHiddenField" runat="server" />
                                    </div>
                                </div>


                                <div class="col-3">
                                    <div class="form-group" runat="server" visible="False">
                                        <label>Sub Section Name :</label>
                                        <asp:Label ID="subSectionLabel" CssClass="form-control form-control-sm" runat="server"></asp:Label>
                                        <asp:HiddenField ID="subSectionHiddenField" runat="server" />
                                    </div>
                                </div>



                                <div class="col-3">
                                    <div class="form-group" runat="server" visible="False">
                                        <label>Employee Type:</label>
                                        <asp:Label ID="employeeType" runat="server" class="form-control form-control-sm"></asp:Label>
                                        <asp:HiddenField ID="empTypeHiddenField" runat="server" />
                                    </div>
                                </div>


                            </div>
                           
                             <%--  <div class="form-row">
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
                                 <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;"> C.Training AREA</h2>
                            <hr/>
                        <asp:GridView runat="server"    AutoGenerateColumns="False" Width="100%" id="gv_AppraisalTraining" CssClass="table table-bordered text-center thead-dark gridDatatable">
                                
                                                <Columns>

                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                    
                                   <asp:TemplateField HeaderText="Training Needs">
                                    <ItemTemplate>
                                       <asp:TextBox runat="server" ID="TrainingNeeds" CssClass="form-control"  Rows="2" TextMode="MultiLine" Text='<%#Eval("TrainingNeeds") %>' ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                    
                                   <asp:TemplateField HeaderText="Quater">
                                    <ItemTemplate>
                                             <asp:DropDownList ID="QuaterDropDownList1" AutoPostBack="true" runat="server"   class="form-control form-control-sm">
                                                      <asp:ListItem Text="1st Quarter" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="2nd Quarter" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="3rd Quarter" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="4th Quarter" Value="4"></asp:ListItem>

                                             </asp:DropDownList>
                                       <%--<asp:TextBox runat="server" ID="TrainingStart"  CssClass="form-control  form-control-sm"  Text='<%#Eval("TrainingStart") %>' ></asp:TextBox>--%>
                                  <%--      <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                        TargetControlID="TrainingStart" />--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                    
                            <%--    <asp:TemplateField HeaderText="End" runat="server" Visible="False">
                                    <ItemTemplate>
                                       <asp:TextBox runat="server" ID="TrainingEnd" CssClass="form-control  form-control-sm"   Text='<%#Eval("TrainingEnd") %>' ></asp:TextBox>
                                     <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                                        Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                        TargetControlID="TrainingEnd" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                                     <asp:TemplateField HeaderText="Add">
                                        <ItemTemplate>
                                             <asp:LinkButton ID="btn_Add" OnClick="btn_Add_OnClick"  CssClass="btn btn-info btn-sm"  runat="server"><i class="fa fa-plus" aria-hidden="true"></i></asp:LinkButton>
                                              </ItemTemplate>
                                    
                                  
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        
                                      
                                        <asp:LinkButton ID="lb_Remove"   CssClass="btn btn-danger btn-sm"   OnClick="lb_Remove_OnClick"  runat="server"><i class="fa fa-trash" aria-hidden="true"></i></asp:LinkButton>
                                    </ItemTemplate>
                                    
                                  
                                </asp:TemplateField>
                                
                            </Columns>
                            </asp:GridView>
                               
                               <asp:HiddenField runat="server" id="id_mastetID"/>
                               
                                      <asp:Button runat="server" ID="btn_Save"  CssClass="btn btn-sm btn-info" OnClick="btn_Save_OnClick" Text="Submit"></asp:Button>
                            
                                     <%--<asp:Button ID="cancelButton" OnClick="cancelButton_OnClick" Text="Cancel"  CssClass="btn btn-sm warning" runat="server"  BackColor="#FFCC00" />--%>
                           </div>
                        </div>
                    </div>
              </ContentTemplate>
           
        </asp:UpdatePanel>
        
    </div>
</asp:Content>


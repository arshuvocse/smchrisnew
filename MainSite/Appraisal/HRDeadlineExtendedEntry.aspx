<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="HRDeadlineExtendedEntry.aspx.cs" Inherits="Appraisal_HRDeadlineExtendedEntry" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
         <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">

    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>
                 <asp:UpdateProgress ID="UpdateProgress4" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait11" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png" width="20px"  />  Deadline Extension  Request</h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                             <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                        <asp:LinkButton ID="detailsViewButton"  style="font-style: normal!important; font-weight: bold"   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick" > <i class="fa fa-backward"></i>&nbsp;Back to List</asp:LinkButton>
                        </div>
                    </div>

                    <div class="card">
                        <div class="card-body">
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
                            
                            
                            <div class="row">
                                   <div class="col-md-2" runat="server" id="comp" >
                                    <div class="form-group">
                                        <label>Company</label>
                                        <asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" class="form-control form-control-sm" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                </div>
                                   <div class="col-md-6">
                                                <div class="form-group ">
                                                    <label>Employee Name </label>
                                                    
                                                                <asp:DropDownList   runat="server"  AutoPostBack="True" OnSelectedIndexChanged="ddlForwordEmp_OnSelectedIndexChanged"  ID="ddlForwordEmp" CssClass="form-control form-control-sm selectme" />
                                                   
                                                     <script type="text/javascript">
                                                         function pageLoad() {
                                                             $('.selectme').chosen({ disable_search_threshold: 5, search_contains: true });

                                                         }
                                                        </script>
                                                   
                                                      
                                            </div>
                                                
                                                
                                                
                                            </div> 
                            </div>
                            <div class="row">
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
  <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;">  Extension Your Request</h2>
                            <hr/>
                            <div class="row" runat="server" visible="False">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label></label>
                                        <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True" RepeatDirection="Horizontal" Enabled="False">
                                        </asp:CheckBoxList>
                                    </div>
                                </div>


 <div class="col-md-2" style="margin-top: 17px;" runat="server" Visible="False">
                                    <div class="form-group">
                                        <asp:CheckBox ID="chk_Common" runat="server" Text=" Is Common" OnCheckedChanged="chk_Common_OnCheckedChanged" AutoPostBack="True" TextAlign="Right"></asp:CheckBox>




                                    </div>
                                </div>
                            </div>
                            <div class="form-row"  runat="server" >


                             
                             

                                <div class="col-2" style="margin-top: 18px" runat="server" Visible="False">
                                    <div class="form-group">
                                        <asp:RadioButtonList ID="rbDeptOrEmp" AutoPostBack="True" runat="server" Visible="False"
                                             RepeatDirection="Horizontal" OnSelectedIndexChanged="rbDeptOrEmp_SelectedIndexChanged">
                                            <%--<asp:ListItem>Department</asp:ListItem>--%>
                                            <asp:ListItem Selected="True">Employee</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <asp:HiddenField runat="server" ID="HideNDEPT"/>
                                </div>

                                <div class="col-2" id="DptShow" runat="server" visible="False">
                                    <div class="form-group">
                                        <label>Department</label>
                                        <asp:DropDownList runat="server" ID="ddlDept" CssClass="form-control form-control-sm" />
                                    </div>
                                </div>

                                <div class="col-2" runat="server" Visible="False">
                                    <div class="form-group" style="margin-top: 18px;">

                                        <asp:Button ID="SearchButton" Text="Search" CssClass="btn btn-sm btn-info" runat="server" OnClick="SearchButton_OnClick" />
                                    </div>
                                </div>

                            </div>
                            
                            <div class="row">
                                   <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Financial Year *</label>
                                        <asp:DropDownList ID="ddlFinancialYear" AutoPostBack="true" OnSelectedIndexChanged="ddlFinancialYear_OnSelectedIndexChanged" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Operation  *</label>
                                        <asp:DropDownList ID="OperationDropDownList" runat="server" class="form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="OperationDropDownList_OnSelectedIndexChanged">
                                            <asp:ListItem>Select One........</asp:ListItem>
                                              <asp:ListItem Value="BSC/OKR">BSC/OKR</asp:ListItem>
                                            <asp:ListItem Value="Apprisal">Apprisal</asp:ListItem>
                                            <asp:ListItem Value="KPI">KPI</asp:ListItem>

                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">

                               
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Extension Date  *: </label>
                                        <asp:TextBox ID="ExtendedDateTextBox" AutoCompleteType="Disabled" runat="server" AutoPostBack="True" OnTextChanged="ExtendedDateTextBox_TextChanged" CausesValidation="true" class="form-control form-control-sm"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="Calendar1" runat="server"
                                            TargetControlID="ExtendedDateTextBox" 
                                            Format="dd MMM yyyy" CssClass="MyCalendar" PopupPosition="TopLeft" />
                                        <style>
                                                      
                                                        </style>
                                    </div>
                                </div>
                                </div>
<div class="row">
                                    

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Description  *: </label>
                                        <asp:TextBox ID="DescriptionTextBox" runat="server" TextMode="MultiLine" CausesValidation="true" class="form-control"></asp:TextBox>

                                    </div>
                                </div>


    </div>
<div class="row">                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Remarks: </label>
                                        <asp:TextBox ID="RemarksTextBox" runat="server" CausesValidation="true" class="form-control form-control-sm"></asp:TextBox>

                                    </div>
                                </div>



                            </div>

                         

                            <div class="form-row" runat="server" Visible="False">


                            
                                   <asp:GridView ID="gv_allocateEmp" Width="100%" CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="false" DataKeyNames="EmpInfoId" runat="server">
                                <Columns>

                                    <asp:TemplateField Visible="False">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="txt_checkAll" OnCheckedChanged="txt_checkAll_OnCheckedChanged" AutoPostBack="True" runat="server"></asp:CheckBox>
                                            <asp:Label ID="txt_selectAll" runat="server"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                            <asp:CheckBox ID="txt_check" Checked="True" runat="server"></asp:CheckBox>
                                        </ItemTemplate>


                                    </asp:TemplateField >
                                    <asp:TemplateField HeaderText="SL#" Visible="False">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            <asp:HiddenField runat="server" ID="txt_empInfoId" Value='<%#Eval("EmpInfoId") %>' />
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ID">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_empId" runat="server" class="form-control form-control-sm" Text='<%#Eval("EmpMasterCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_name" runat="server" class="form-control form-control-sm" Text='<%#Eval("EmpName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_designation" runat="server" class="form-control form-control-sm" Text='<%#Eval("Designation") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Department">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_dptName" runat="server" class="form-control form-control-sm" Text='<%#Eval("DepartmentName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Division">
                                        <ItemTemplate>
                                            <asp:Label ID="txt_division" runat="server" class="form-control form-control-sm" Text='<%#Eval("DivisionName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

<%--                                    <asp:TemplateField HeaderText="Extension Date">
                                        <ItemTemplate>
                                            <asp:TextBox ID="ExtensionDate" AutoCompleteType="Disabled" runat="server" class="form-control" Width="100%" Height="29px" Text='<%# Eval("ExtensionDate")%>' OnTextChanged="deliveryDateTextBox_TextChanged" AutoPostBack="True"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                TargetControlID="txt_Extended" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <%-- <asp:TemplateField HeaderText="Total Employee">
                                    <ItemTemplate>
                                        <asp:Label ID="TotalEmployee" runat="server" class="form-control form-control-sm" Text='<%#Eval("TotalEmployee") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                              <asp:TemplateField HeaderText="Extension Date">
                                        <ItemTemplate>
                                            <asp:TextBox ID="ExtendedDate" runat="server" class="form-control form-control-sm" Text='<%#Eval("ExtendedDate") %>'></asp:TextBox>

                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                                TargetControlID="ExtendedDate" />
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <%-- <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txt_Remarks" runat="server" class="form-control form-control-sm" Text='<%#Eval("Remarks") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                </Columns>
                            </asp:GridView>



                            </div>

                            <div class="row">
<div class="col-md-5">
       <div class="form-group">
                                    <asp:HiddenField runat="server" ID="hid_KpiMasrerId" />
                                    <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" Text="Submit" CssClass="btn btn-sm btn-info" />
                                    <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                    <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />
                                    <asp:Button ID="cancelButton" Text="Cancel" Visible="False" OnClick="cancelButton_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />
                                </div>
</div>
                             
                                <br />
                                <br />

                            </div>

                            <br />
                            <br />
                            <br />
                            <br />
                        </div>
                    </div>

                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>


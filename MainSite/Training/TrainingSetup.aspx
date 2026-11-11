<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="TrainingSetup.aspx.cs" Inherits="Trainning_TrainingSetup" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>
<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="trainingSetupContent" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>

                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;">Training Setup</h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="detailsViewButton" Text="Training Information " CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_Click" />
                        </div>
                    </div>

                    <div class="card">
                        <div class="card-body">
                            <div class="form-row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Company</label>
                                        <asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" class="form-control form-control-sm" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Financial Year</label>
                                        <asp:DropDownList ID="ddlFinancialYear" AutoPostBack="true" OnSelectedIndexChanged="ddlFinancialYear_SelectedIndexChanged" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Quater</label>
                                        <%--<asp:DropDownList ID="ddlQuater" runat="server" class="form-control form-control-sm"></asp:DropDownList>--%>
                                        <asp:DropDownList ID="ddlQuater" AutoPostBack="true" OnSelectedIndexChanged="ddlQuater_SelectedIndexChanged"  CssClass="form-control  form-control-sm" runat="server"></asp:DropDownList>
                                    </div>
                                    
                                </div>
                                
                            </div>
                            <div  class="form-row">
                                <div class="form-group">
                                    <label>From Requisituin:</label>
                                    <asp:CheckBox  runat="server" ID="fromReq" OnCheckedChanged="fromReq_CheckedChanged" AutoPostBack="true" />
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="col-md-3"  runat="server">
                                    <div class="form-group">
                                        <label>Trainig Title</label>
                                        <%--txt_TrainingTitle--%>
                                        <asp:DropDownList ID="ddl_training" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_training_SelectedIndexChanged" CssClass="form-control  form-control-sm"></asp:DropDownList>  
                                             
                                    </div>
                                </div>

                                
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Trainig Details</label>
                                        <asp:TextBox ID="txt_TrainigDetails" TextMode="MultiLine" runat="server" CssClass="form-control  "></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Trainig Organization</label>
                                        <asp:DropDownList ID="ddlTrainingOrg" AutoPostBack="true" OnSelectedIndexChanged="ddlTrainingOrg_SelectedIndexChanged" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Location</label>
                                        <asp:DropDownList ID="ddlLocation" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="form-row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                         <label>Trainner</label>
                                          <asp:DropDownList ID="ddlTrainer" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                         
                                    </div>
                                </div>
                               
                                <div class="col-md-1">
                                    <div class="form-group">
                                          <label>&nbsp</label>
                                        <asp:Button runat="server" ID="AddTrainner" OnClick="AddTrainner_Click"  CssClass="form-control btn btn-success btn-sm" Text="Add"></asp:Button>
                                    </div>
                                </div>
                                 <div class="col-md-1">
                                    <div class="form-group">
                                          <label>Not Listed</label>
                                       <asp:CheckBox  runat="server" ID="notListedCheck" AutoPostBack="true" OnCheckedChanged="notListedCheck_CheckedChanged"/>
                                    </div>
                                </div>
                                 <div class="col-md-2" id="notListedNameDiv" runat="server" visible="false">
                                    <div class="form-group">
                                         <label>Name</label>
                                          <asp:TextBox ID="txt_NotListedTrainer" runat="server" CssClass="form-control  form-control-sm"></asp:TextBox>
                                         
                                    </div>
                                </div>

                                <div class="col-md-2"  id="notListedDetailsDiv" runat="server" visible="false">
                                    <div class="form-group">
                                         <label>Details</label>
                                          <asp:TextBox ID="txt_NotListedTrainerDetails" runat="server" CssClass="form-control  form-control-sm"></asp:TextBox>
                                         
                                    </div>
                                </div>
                                <div class="col-md-1" >
                                    <div class="form-group">
                                          <label>&nbsp</label>
                                        <asp:Button runat="server" ID="AddNotListed"  visible="false" OnClick="AddNotListed_Click"  CssClass="form-control btn btn-success btn-sm" Text="Add"></asp:Button>
                                    </div>
                                </div>
                            </div>

                            <div class="form-row">
                                <div class="col-md-12">
                                    <asp:GridView ShowFooter="true" ID="gvTrainner" Width="100%"  CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="false" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL#">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                        <%--<asp:HiddenField runat="server" ID="hdpkd" Value='<%#Eval("BoardDetailsId") %>'/>--%>
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Trainner">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_Trainner" runat="server" class="form-control form-control-sm" Text='<%#Eval("TrainerName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Details">
                                                <ItemTemplate>
                                                     
                                                    <asp:Label Visible="false" ID="txt_trainerID" runat="server" class="form-control form-control-sm" Text='<%#Eval("TrainerId") %>'></asp:Label>
                                                    <asp:Label ID="txt_TrainnerDetails" runat="server" class="form-control form-control-sm" Text='<%#Eval("TrainerDetails") %>'></asp:Label>
                                               
                                                       <%--<asp:HiddenField runat="server" ID="trainnerId"   />--%>
                                                     </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Operation">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lb_Remove" OnClick="lb_Remove_Click" runat="server"
                                                        >Remove</asp:LinkButton>
                                                </ItemTemplate>
                                             
                                                
                                            </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                </div>
                            </div>

                            <div class="form-row">
                                   <div class="col-md-3">
                                    <div class="form-group">
                                         <label>Start Date <span style="color:red">*</span></label>
                                          <asp:TextBox ID="txt_StartDate" runat="server" CssClass="form-control form-control-sm" AutoPostBack="True"> </asp:TextBox>
                                         <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                Format="dd-MMM-yyyy"  CssClass="custom"
                                                TargetControlID="txt_StartDate" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                         <label>End Date <span style="color:red">*</span></label>
                                          <asp:TextBox ID="txt_EndDate" runat="server" CssClass="form-control  form-control-sm"></asp:TextBox>
                                         <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                Format="dd-MMM-yyyy"  CssClass="custom"
                                                TargetControlID="txt_EndDate" />
                                    </div>
                                </div>

                                 <div class="col-md-3">
                                    <div class="form-group">
                                         <label>Duration(Houre) <span style="color:red">*</span> </label>
                                          <asp:TextBox ID="txt_Duration" TextMode="Number" runat="server" CssClass="form-control  form-control-sm"></asp:TextBox>
                                         
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                         <label>Evaluation</label><br/>
                                      <asp:RadioButtonList RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server" ID="radTrainingEvaluation">
                                        <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                        <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                        
                                    </asp:RadioButtonList>
                                         
                                    </div>
                                </div>
                            </div>

                            <asp:GridView ID="gv_allocateEmp" Width="100%" CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="false" runat="server">
                                  <Columns>
                                            <asp:TemplateField HeaderText="SL#">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Operation">
                                                <ItemTemplate>
                                                   
                                                      <asp:CheckBox ID="txt_check" runat="server" ></asp:CheckBox>
                                                </ItemTemplate>


                                            </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Quater">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_quater" runat="server" class="form-control form-control-sm" Text='<%#Eval("Quater") %>'></asp:Label>
                                                      
                                                </ItemTemplate>


                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_empCode" runat="server" class="form-control form-control-sm" Text='<%#Eval("EmpMasterCode") %>'></asp:Label>
                                                    <asp:HiddenField runat="server" ID="detailsID" Value='<%#Eval("TrainingBudgetAllocationDetailsId") %>' />
                                                    <asp:HiddenField runat="server" ID="empInfoId" Value='<%#Eval("EmpInfoId") %>' />
                                                </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Name">


                                                <ItemTemplate>
                                                    <asp:Label ID="txt_employee" runat="server" class="form-control form-control-sm" Text='<%#Eval("EmpName") %>'></asp:Label>

                                                   

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
                                            <asp:TemplateField HeaderText="Grade">
                                                <ItemTemplate>
                                                    <asp:Label ID="txt_grdName" runat="server" class="form-control form-control-sm" Text='<%#Eval("GradeName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                           
                                          

                                        </Columns>
                            </asp:GridView>

                                    <asp:HiddenField runat="server" ID="hdpk"/>
                                    <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_Click" Text="Submit " CssClass="btn btn-sm btn-info"/>
                                     <asp:Button ID="cancelButton" Text="Cancel" OnClick="cancelButton_Click" CssClass="btn btn-sm warning" runat="server"  BackColor="#FFCC00" />
                        </div>
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>


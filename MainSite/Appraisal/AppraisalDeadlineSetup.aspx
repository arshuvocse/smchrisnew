<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="AppraisalDeadlineSetup.aspx.cs" Inherits="Appraisal_AppraisalDeadlineSetup" %>

<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="trainingSetupContent" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">
            <style>
              
   #cpFormBody_gv_allocateEmp td {
           
            padding: 8px;
        }



   
     #cpFormBody_SaveGridView td {
           
            padding: 8px;
        }

                
           
                 #cpFormBody_SaveGridView  > tbody > tr > th {
            padding: 9px 0;
            color: #fff;
            background-color: #33B5E5;
            /*background-color: #98A9C0;*/
        }

       #cpFormBody_SaveGridView > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF;
           padding: 18px;
        }

        
                 #cpFormBody_gv_allocateEmp  > tbody > tr > th {
                     padding: 9px 0;
                     color: #fff;
                     background-color: #5B799E;
                     /*background-color: #98A9C0;*/
                 }

                #cpFormBody_gv_allocateEmp > tbody > tr:not(th):nth-child(odd) {
                    background-color: #DFDFDF;
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
                <div class="container-fluid">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"> <img src="../Report_Pages/app.png"  width="20px" />  Appraisal Declaration and Deadline  Setup </h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                            <asp:Button ID="detailsViewButton" Text="Go Back" OnClick="detailsViewButton_OnClick" CssClass="btn btn-sm btn-outline-secondary " runat="server" />
                        </div>
                    </div>

                    <div class="card">
                        <div class="card-body">
                            <div class="form-row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Company</label> &nbsp;<label style="color: #a52a2a">*</label>
                                        <asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" class="form-control form-control-sm" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Financial Year</label> &nbsp;<label style="color: #a52a2a">*</label>
                                        <asp:DropDownList ID="ddlFinancialYear" AutoPostBack="true" OnSelectedIndexChanged="ddlFinancialYear_OnSelectedIndexChanged" runat="server" class="form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                         <label>Subject</label> &nbsp;<label style="color: #a52a2a">*</label>
                                        <asp:TextBox ID="subjectTextBox" runat="server"  CssClass="form-control  form-control-sm"></asp:TextBox>
                                    </div>

                                </div>
                                
                                                            <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Declaration Date</label> &nbsp;<label style="color: #a52a2a">*</label>
                                        <asp:TextBox ID="DeclarationTextBox"  autocomplete="off"  runat="server" CssClass="form-control form-control-sm"   OnTextChanged="DeclarationTextBox_OnTextChanged"  AutoPostBack="True"  ></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                            Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                            TargetControlID="DeclarationTextBox" />

                                    </div>

                                </div>
                                <div class="col-md-1.5" style="margin-top: 17px;"   >
                                    <div class="form-group" >
                                        <asp:CheckBox ID="chk_Common" runat="server" Text=" Is Common" AutoPostBack="True" TextAlign="Right" OnCheckedChanged="chk_Common_OnCheckedChanged"></asp:CheckBox>
                                      
                                       


                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                         <label>Deadline</label>
                                         <asp:TextBox ID="txt_deadLine"  autocomplete="off"  runat="server" CssClass="form-control form-control-sm" AutoPostBack="True" OnTextChanged="txt_deadLine_OnTextChanged"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                            TargetControlID="txt_deadLine" />
                                       
                                    </div>

                                </div>
                                 <div class="col-md-4">
                                    <div class="form-group">
                                         <label>Message</label>
                                        <asp:TextBox ID="txt_commonRemarks" runat="server" TextMode="MultiLine" Rows="2"  AutoPostBack="True" OnTextChanged="txt_commonRemarks_OnTextChanged" CssClass="form-control"></asp:TextBox>
                                    </div>

                                </div>
                            </div>
                              <fieldset class="for-panel">
                                <legend>Search Employee </legend>
                            <div class="form-row">
                                <div class="col-2">
                                        <div class="form-group">
                                            <label>Employee Category</label> 
                                            <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control form-control-sm" />
                                        </div>
                                    </div>
                                    
                                <div class="col-2">
                                        <div class="form-group">
                                            <label>Department</label> 
                                            <asp:DropDownList runat="server" ID="ddlDept" CssClass="form-control form-control-sm" />
                                        </div>
                                    </div>
                                <div class="col-2">
                                        <div class="form-group" style="margin-top: 18px">
                                          
                                            <asp:Button ID="Button1" Text="Search" CssClass="btn btn-outline-success disabled btn-sm" runat="server" OnClick="Button1_OnClick" />
                                        </div>
                                    </div>
                                
                            </div>
                                  
                                  




                      

                        <asp:GridView ID="gv_allocateEmp" Width="100%" CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="false" runat="server">
                            <Columns>

                                <asp:TemplateField   >
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="txt_checkAll" OnCheckedChanged="txt_checkAll_OnCheckedChanged" AutoPostBack="True" runat="server"></asp:CheckBox>
                                        <asp:Label ID="txt_selectAll" runat="server"  ></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>

                                        <asp:CheckBox ID="txt_check" runat="server"></asp:CheckBox>
                                    </ItemTemplate>


                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                        <asp:HiddenField runat="server" ID="txt_empInfoId" Value='<%#Eval("EmpInfoId") %>' />
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee ID">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_empId" runat="server" Text='<%#Eval("EmpMasterCode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_name" runat="server"   Text='<%#Eval("EmpName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_designation" runat="server"   Text='<%#Eval("Designation") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Department"> 
                                    <ItemTemplate>
                                        <asp:Label ID="txt_dptName" runat="server"   Text='<%#Eval("DepartmentName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Division">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_division" runat="server"   Text='<%#Eval("DivisionName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <%-- <asp:TemplateField HeaderText="Total Employee">
                                    <ItemTemplate>
                                        <asp:Label ID="TotalEmployee" runat="server" class="form-control form-control-sm" Text='<%#Eval("TotalEmployee") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Deadline">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_DeadLine" runat="server" autocomplete="off"  class="form-control form-control-sm" AutoPostBack="True" OnTextChanged="txt_DeadLine_ssOnTextChanged" Text='<%#Eval("DeadLine") %>'></asp:TextBox>

                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Format="dd-MMM-yyyy" CssClass="MyCalendar"
                                            TargetControlID="txt_DeadLine" />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Message">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Remarks" runat="server" class="form-control form-control-sm" Text='<%#Eval("Remarks") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>



                            </Columns>
                        </asp:GridView>
                            <div class="row" style="padding: 10px;">
                                       <div class="col-md-10">
                                           </div>
                                      
                                       <div class="col-md-2">
                                            <div class="form-group">
                                            <asp:Button ID="textButton" Text="Add To List" OnClick="textButton_OnClick" CssClass="btn btn-outline-success btn-block disabled btn-sm" runat="server" />
                                        </div>
                                           </div>
                                  </div>

                         </fieldset>

                            <asp:GridView ID="SaveGridView" Width="100%" CssClass="table table-bordered text-center thead-dark" AutoGenerateColumns="false" runat="server">
                            <Columns>

                               
                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                        <asp:HiddenField runat="server" ID="txt_empInfoId" Value='<%#Eval("EmpInfoId") %>' />
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee ID">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_empId" runat="server"   Text='<%#Eval("EmpMasterCode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_name" runat="server"   Text='<%#Eval("EmpName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_designation" runat="server"   Text='<%#Eval("Designation") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Department">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_dptName" runat="server"   Text='<%#Eval("DepartmentName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Division" runat="server" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_division" runat="server"   Text='<%#Eval("DivisionName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <%-- <asp:TemplateField HeaderText="Total Employee">
                                    <ItemTemplate>
                                        <asp:Label ID="TotalEmployee" runat="server" class="form-control form-control-sm" Text='<%#Eval("TotalEmployee") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Deadline">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_DeadLine" runat="server"   Text='<%#Eval("DeadLine") %>'></asp:Label>

                                        
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Message">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_Remarks" runat="server"   Text='<%#Eval("Remarks") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Delete" runat="server">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="deleteImageButtonDirectlySupervices" runat="server" OnClick="deleteImageButtonDirectlySupervices_OnClick"
                                                                    ImageUrl="~/Assets/img/delete.png" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                <%--<asp:TemplateField HeaderText="Delete" runat="server" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="deleteImageButtonDirectlySupervices" runat="server" OnClick="deleteImageButtonDirectlySupervices_OnClick"
                                                                    ImageUrl="~/Assets/img/delete.png" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                            </Columns>
                        </asp:GridView>


                   
                    
                    <asp:HiddenField runat="server" id="hid_KpiMasrerId"/>
                    <asp:Button runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" Visible="False" Text="Submit " CssClass="btn btn-sm btn-info"/>
                    <asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False"  runat="server" OnClick="editButton_OnClick" />
                    <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />
                                     <asp:Button ID="cancelButton" Visible="False" Text="Cancel" OnClick="cancelButton_OnClick" CssClass="btn btn-sm warning" runat="server"  BackColor="#FFCC00" />
             <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                        
                        
                        
                         </div>    </div> </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>


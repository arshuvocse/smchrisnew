<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="MBSCOKRDetailsViewSelf.aspx.cs" Inherits="Appraisal_MBSCOKRDetailsViewSelf" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <style type="text/css">
        /*AutoComplete flyout */
    </style>
    <div class="content">

       
                <div class="container-fluid">

                    <style>
                        .chkChoiceHeader label {
                            padding-left: 4px;
                            padding-right: 10px;
                            font-size: 13px;
                        }

                              .checkbox label,
         .checkbox-inline label {
             text-align: left;
             padding-left: 0.3em;
                 font-weight: bold;
         }
                        #cpFormBody_gv_Versions td {
                            border: 1px solid #ddd !important;
                            padding: 8px;
                        }

                        fieldset.for-panel {
                            background-color: #fcfcfc;
                            border: 1px solid #999;
                            padding: 15px 10px;
                            background-color: white;
                            margin-bottom: 12px;
                        }
                        .fixed-width-serial { width: 50px; }
.fixed-width-dimension { width: 150px; }
.fixed-width-goal { width: 200px; }
.fixed-width-kpi { width: 150px; }
.fixed-width-weightage { width: 80px; }
.fixed-width-initiatives { width: 200px; }
.fixed-width-deadline { width: 120px; }

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
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;">
                                <img src="../Report_Pages/app.png" width="20px" />
                                 <span style="font-weight:bold" id="lblHead"></span> </h1>
                        </div>

                        <%-- <div class="page-heading__container float-right d-none d-sm-block">
                            <asp:Button ID="detailsViewButton" Text="Appraisal List" OnClick="detailsViewButton_OnClick" CssClass="btn btn-sm btn-outline-secondary " runat="server"  />
                        </div>--%>

                        <div class="page-heading__container float-right d-none d-sm-block">
                         <a href="../DashBoard_UI/DashBoard.aspx" class="btn btn-sm btn-outline-secondary">Home</a>
<a href="KPIInformationView.aspx" class="btn btn-sm btn-outline-secondary">&#8920; Back To List</a>

                        </div>

                    </div>
                    <div class="card">
                        
                                      <div class="card-body">

                <style>
    .thead-dark th {
        background-color: #07619D !important;
        color: white;
    }
</style>
                <style>
                    .nested-table {
                        margin-top: 10px;
                        display: none;
                    }
                    .btn-sm {
                        font-size: 0.875rem;
                        padding: 0.25rem 0.5rem;
                    }
                    .form-control-sm {
                        font-size: 0.875rem;
                    }
                </style>

                <div class="row">



                   
                        <div class="col-md-12">
                              <style>
        .tblTHColorChang {
            background-color: #EDF2F5 !important;
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
            <td>
                <label id="lblEmpId"></label>
            </td>

            <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Supervisor</td>
            <td>
                <label id="ReportingLabel"></label>
            </td>
        </tr>

        <tr>
            <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Employee Name</td>
            <td>
                <label id="lblEmployeeName"></label>
            </td>

            <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Department</td>
            <td>
                <label id="deptNameLabel"></label>
            </td>
        </tr>

        <tr>
            <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Designation</td>
            <td>
                <label id="desigNameLabel"></label>
            </td>

            <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Office</td>
            <td>
                <label id="LocationLabel"></label>
            </td>
        </tr>

        <tr>
            <td class="tblTHColorChang" style="width: 20%; padding: 10px;">Date Of Joining</td>
            <td>
                <label id="joiningDateLabel"></label>
            </td>
            <td style="width: 20%; padding: 10px;" class="tblTHColorChang">Place</td>
            <td>
                <label id="lblPlace"></label>
            </td>
        </tr>
    </table>
</div>

                                                    
                 
 <div class="col-md-12">
                                                                            <br/>
                        <div class="page-header text-center">
  <h1  class="elegantshd" >KPI Information</h1>
</div>

  <h2 class="blue title-widget" style="color: #2196F3; text-shadow: 0 0 2px black;">A. Functional Area (100% weightage will be converted to 75 Marks) </h2>
  <hr />
                            
                    <table class="table table-bordered text-center thead-dark gridDatatable">
                        <thead>
                            <tr>
                                <th>SL#</th>
                                <th id="dimensionHeader">Dimension</th>
                                <th>Objective / Goal  </th>
                                <th></th>
                                
                            </tr>
                        </thead>
                        <tbody id="gv_AppraisalFunc">
                            <!-- Sample row -->
                            <tr>
                                <td class="serial-number">1</td>
                                <td>
                                    <select class="form-control form-control-sm ddlBSCDimension">
                                        <option value="">Select One</option>
                                        <option value="1">Financial</option>
                                        <option value="2">Internal Process</option>
                                        <option value="3">Customer</option>
                                        <option value="4">Learning & Growth</option>
                                    </select>
                                </td>
                                <td>
                                    <textarea class="form-control  txtObjectiveGoal" rows="6"></textarea>
                                </td>
                                <td>
                                    <button   style="display:none"  type="button" class="btn btn-success btn-sm btn_ToggleNestedTable">
                                        <i class="fa fa-plus"></i>&nbsp; Add KPI
                                    </button>
                                    <div class="nested-table">
                                        <table class="table table-sm table-bordered">
                                            <thead>
                                                <tr>
                                                    <th class="KPIHeader"> </th>
                                                    <th>Weightage (%)</th>
                                                    <th>Initiatives</th>
                                                    <th>Deadline</th>
                                                  
                                                </tr>
                                            </thead>
                                            <tbody class="nestedTableBody">
                                                <!-- Nested rows go here -->
                                            </tbody>
                                        </table>
                                        <button  style="display:none"  type="button" class="btn btn-success btn-sm mt-2 btn_AddRowNested">
                                            <i class="fa fa-plus"></i>&nbsp; Add KPI
                                        </button>
                                    </div>
                                </td>
                                
                            </tr>
                            <!-- Add more rows as needed -->
                        </tbody>

                        <tfoot>
                            <tr>
                                <td colspan="3" class="text-right"><strong>Total Weightage:</strong></td>
                                <td colspan="1">
                                    <span id="totalWeightage" class="font-weight-bold">0</span>
                                </td>
                            </tr>
                        </tfoot>
                    </table>


                    
                            <h2 class="blue title-widget" style="color: #2196F3; text-shadow: 0 0 2px black;">B.Behavioral Area (25 Marks) </h2>
                            <hr />
                            

                    <table class="table table-bordered text-left thead-dark gridDatatable">
                        <thead class="thead-dark">
                            <tr>
                                <th>SL#</th>
                                <th>Type <span style="color: red;">*</span></th>
                                <th>Competencies / Skills <span style="color: red;">*</span></th>
                                <th>Supporting Example</th>
                                <th>Weight (Number) <span style="color: red;">*</span></th>
                                <th>Expected Number <span style="color: red;">*</span></th>
                            </tr>
                        </thead>
                        <tbody id="gv_AppraisalPartB">
                        </tbody>
                        <tfoot>
                            <tr>
                                 
            <td colspan="4" class="text-right font-weight-bold">Total Weight:</td>
            <td>
                <span id="totalWeight">25</span>
            </td>
            <td>
                <span id="totalExpectedNumber">0</span>
            </td>
        </tr>
                            
                        </tfoot>
                    </table>


                     <h2 class="blue title-widget" style="color: #2196F3; text-shadow: 0 0 2px black;">Approval List </h2>
<hr />
                <table class="table table-bordered text-center thead-dark gridDatatable" style="width: 100%;">
                    <thead>
                        <tr>
                            <th>SL#</th>
                            <th>Employee</th>
                            <th>Comments</th>
                            <th>Approval Date</th>
                            <th>Approval Status</th>
                        </tr>
                    </thead>
                    <tbody id="approvalStatusList">
                        
                    </tbody>
                </table>
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

<!-- Add more controls as needed -->


                 <button style="display:none" type="button" id="btnSubmit" class="btn btn-primary" onclick="submitData();">Submit</button>

                </div>
                </div>
                    <br/>
                        <br/>
                        
                            <div class="page-header text-center">
  <h1  class="elegantshd">Appraisal Information</h1>
</div>
                            
                                            <div class="row">
                                                 <div class="col-md-12">

                                                                   <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;">  A.Functional Area (75 Marks)</h2>
            <hr/>
         <table class="table table-bordered text-center thead-dark gridDatatable">
    <thead>
        <tr>
            <th>SL#</th>
            <th id="dimensionHeaderApp">Dimension</th>
            <th>Objective / Goal <span style="color: red;">*</span></th>
            <th></th>
        
        </tr>
    </thead>
    <tbody id="gv_AppraisalFuncApp">
        <!-- Sample row -->
        <tr>
            <td class="serial-number">1</td>
            <td>
                <select class="form-control form-control-sm ddlBSCDimension">
                    <option value="">Select One</option>
                    <option value="1">Financial</option>
                    <option value="2">Internal Process</option>
                    <option value="3">Customer</option>
                    <option value="4">Learning & Growth</option>
                </select>
            </td>
            <td>
                <textarea class="form-control  txtObjectiveGoal" rows="6"></textarea>
            </td>
            <td>
                <button   style="display:none"  type="button" class="btn btn-success btn-sm btn_ToggleNestedTable">
                    <i class="fa fa-plus"></i>&nbsp; Add KPI
                </button>
                <div class="nested-table">
                    <table class="table table-sm table-bordered">
                        <thead>
                            <tr>
                                <th class="KPIHeader"> </th>
                                <th>Weightage (%)</th>
                                <th>Initiatives</th>
                                <th>Deadline</th>
                                <th>Remove</th>
                            </tr>
                        </thead>
                        <tbody class="nestedTableBodyApp">
                            <!-- Nested rows go here -->
                        </tbody>
                    </table>
                    <button  style="display:none"  type="button" class="btn btn-success btn-sm mt-2 btn_AddRowNested">
                        <i class="fa fa-plus"></i>&nbsp; Add KPI
                    </button>
                </div>
            </td>
             
        </tr>
        <!-- Add more rows as needed -->
    </tbody>

   <tfoot>
    <tr>
        <td colspan="3" class="text-right"><strong>Total Summary:</strong></td>
        <td>
            <div class="nested-table" style="display: block;">
                <table class="table table-sm table-bordered">
                    <tbody>
                        <tr>
                            <td class="fixed-width-weightage"><strong>Total Weightage:</strong></td>
                            <td><span id="totalWeightageApp" class="font-weight-bold">0</span></td>
                        </tr>
                        <tr>
                            <td class="fixed-width-weightage"><strong>Total Self-Mark:</strong></td>
                            <td><span id="selfApp" class="font-weight-bold">0</span></td>
                        </tr>
                        <tr>
                            <td class="fixed-width-weightage"><strong>Total Supervisor Mark:</strong></td>
                            <td><span id="supperApp" class="font-weight-bold">0</span></td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </td>
    </tr>
</tfoot>

</table>


            <br/>
            <br/>
                <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;">     B.Behavioral Area (25 Marks) </h2>
            <hr/>
            
                    <table class="table table-bordered text-left thead-dark gridDatatable">
                        <thead class="thead-dark">
                            <tr>
                                <th>SL#</th>
                                 
                                <th>Competencies / Skills  </th>
                                <th>Supporting Example</th>
                                <th>Weight (Number)  </th>
                                <th>Expected Number  </th>
                                <th>Self Score	  </th>
                                <th>Supervisor Score	  </th>
                            </tr>
                        </thead>
                        <tbody id="gv_AppPartB">
                        </tbody>
                        <tfoot>
                            <tr>
                                 
            <td colspan="3" class="text-right font-weight-bold">Total Weight:</td>
            <td>
                <span id="totalWeightApp">25</span>
            </td>   <td>
                <span id="totalExpectedNumberApp">0</span>
            </td>
            <td>
                <span id="totalSelfNumberApp">0</span>
            </td><td>
                <span id="supperNumberApp">0</span>
            </td>
        </tr>
                            
                        </tfoot>
                    </table>

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

                                                                                                                                                                                                                                                                                                                                                                                                                                                    <cc1:FilteredTextBoxExtender ID="Fisasasdasasddasare001sad" runat="server" Enabled="True"
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


                                                                                                                                                                                                                            <cc1:FilteredTextBoxExtender ID="Filsss21SupervisorScssasore001" runat="server" Enabled="True"
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
              <table id="trainingTable"      class="table table-bordered text-left thead-dark gridDatatable">
    <thead>
        <tr>
            <th>Training Needs</th>
            <th>Quarter</th>
        </tr>
    </thead>
    <tbody>
        <!-- Rows will be dynamically inserted here -->
    </tbody>
</table>

            
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
                           
                                    <asp:Label ID="lblRecommendation" runat="server" />
                               </div>
                                   <div class="col-md-2" id="Divsteps"   runat="server" >
                                        <div class="form-group">
                                        <label>Steps</label>
                                      <asp:Label ID="txtStep" runat="server" />
                                    </div>
                                       </div>
                                
                                
                                 <div class="col-md-2" id="Div1Other"   runat="server" visible="False">
                                        <div class="form-group">
                                      
                                        <label>Special Increment</label>
                                      <asp:Label ID="txtnote" runat="server" />
                                    </div>
                                       </div>
                                
                                 <div class="col-md-6">
                                       <div class="form-group">
                                      
                                        <label>Promotion & Increment:</label>
                                      <asp:Label ID="txtJustification" runat="server" />
                                           
                                           <br/>
                                           
                                           

                                    </div>
                                      <div class="form-group">
                                       
                                              <label>Document Info:</label>
                                          <br/>
                                                   <asp:HyperLink ID="HLDocumentLink" runat="server" Text="Preview" NavigateUrl="#" />
<asp:Label ID="lbFileName" runat="server" />
                                          </div>
                                     

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
            

         
            <%--<asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="delButton_OnClick" />--%>

            <%--<asp:Button ID="cancelButton" Text="Cancel" OnClick="cancelButton_Click" CssClass="btn btn-sm warning" runat="server"  BackColor="#FFCC00" />--%>
        
    </div>
</div>
            </div>
            </div>



            </div>
        </div>
    </div>

        </div>
        

   <script>
       var MValueEdit;
       $(document).ready(function () {

           


           // Function to add a row to the table


           // Function to create rows with unique IDs


           createRows(5);


           function getQueryParam(param) {
               const urlParams = new URLSearchParams(window.location.search);
               return urlParams.get(param);
           }

           // Get the value of 'M' from the URL
           const MValue = getQueryParam('M');
           MValueEdit = getQueryParam('M');

           // Set the Dimension header text based on the value of 'M'
           const dimensionHeader = document.getElementById('dimensionHeader');
           const lblHead = document.getElementById('lblHead');

           const redAsterisk = '<span style="color: red;">*</span>';

           dimensionHeader.innerHTML = (MValue === "OKR" ? "OKR Dimension" : "BSC Dimension") + redAsterisk;
           lblHead.innerHTML = (MValue === "OKR" ? "OKR Information" : "BSC Information");
           $(".KPIHeader").html((MValue === "OKR" ? "Key Results (KR) / KPI" : "KPI / Measure") + redAsterisk);


           // Call this function on relevant events, such as when a row is added, removed, or modified
           $('body').on('click', '.btn_Add, .btn_Remove ', function () {
               updateTotalWeightage();
           });
           $('body').on('input', '.txtWeightage', function () {
               updateTotalWeightage();
           });
           function calculateTotalScorePartB() {
               let total = 0;
               let isValid = true;

               $('#gv_AppraisalPartB .ScorePartB').each(function () {
                   let score = parseFloat($(this).val()) || 0;

                   // Check if the score is greater than 5
                   if (score > 5) {
                       alert('Score cannot be more than 5.');
                       isValid = false;
                       $(this).addClass('input-error');
                       $(this).val('');  // Clear the invalid score input
                   } else {
                       $(this).removeClass('input-error');
                       total += score;
                   }
               });

               // Only update the total if all scores are valid
               if (isValid) {
                   $('#totalScorePartB').text(total.toFixed(2));
               }
           }



           //    // Adding initial 5 rows
           //    for (let i = 0; i < 5; i++) {
           //        addRowPartB();
           //    }

           //    // Add new row
           //    function addRowPartB() {
           //        let newRow = `
           //<tr>
           //    <td class="serial-numberPartB">New</td>
           //    <td>
           //        <textarea type="text" class="form-control CompetenciesPartB" rows="2"></textarea>
           //    </td>
           //    <td>
           //        <textarea class="form-control   SupportingExamplePartB" rows="2"></textarea>
           //    </td>
           //    <td>
           //        <input    type="decimal"       oninput="validateNumericInput(this)"  class="form-control form-control-sm ScorePartB" value="" />
           //    </td>
           //</tr>`;
           //        $('#gv_AppraisalPartB').append(newRow);
           //        updateSerialNumbersPartB();
           //        calculateTotalScorePartB();
           //    }

           //    // Update serial numbers
           //    function updateSerialNumbersPartB() {
           //        $('#gv_AppraisalPartB tr').each(function (index) {
           //            $(this).find('.serial-numberPartB').text(index + 1);
           //        });
           //    }

           //    // Event listener to recalculate total score when the score changes
           //    $('body').on('input', '.ScorePartB', function () {
           //        calculateTotalScorePartB();
           //    });

           //    // Initially calculate the total score
           //    calculateTotalScorePartB();




           EmployeeInfo();
           KPIFuncInfo();
           AppraisalFuncInfo();
           fetchAppraisalTrainingList();
           loadAppraisalData();
           PartBFuncInfo();
           AppPartBFuncInfo();
           fetchApprovalData();
           loadAppraisalScores();
          // fetchRadioOptions();
           // Toggle Nested Table Visibility

           $('body').on('click', '.btn_ToggleNestedTable', function () {

               var nestedTable = $(this).siblings('.nested-table');
               var button = $(this);



               // Add new row


               nestedTable.toggle();

               if (nestedTable.is(':visible')) {

                   // Add a new row to the nested table when it becomes visible

                   $(this).closest('td').find('.btn_AddRowNested').first().trigger('click');


                   //// Update button text and style to reflect the hide option
                   button.html('<i class="fa fa-minus"></i>&nbsp; Hide Objective/Goal')
                       .removeClass("btn-success").attr("hidden", true);


               } else {

                   console.log("Else Clicked");

                   //// Update button text and style to reflect the add option
                   button.html('<i class="fa fa-plus"></i>&nbsp; Add KPI')
                       .removeClass("btn-warning").addClass("btn-success");
               }
           });

           // Add Nested Row
           $('body').on('click', '.btn_AddRowNested', function () {


               console.log("btn_AddRowNested is clicked !");

               var newRow = `
             <tr>
                 <td><textarea  rows="3" class="form-control   txtKPI" rows="3"></textarea></td>
                 <td><input    type="decimal"   oninput="validateNumericInput(this)"      class="form-control  form-control-sm  txtWeightage" /></td>
                 <td><textarea   rows="3" class="form-control txtInitiatives"  rows="3"></textarea></td>
                 <td><input type="date" class="form-control form-control-sm txtDeadline" /></td>
                 <td><button  type="button" class="btn btn-danger btn-sm btn_RemoveRowNested">
                     <i class="fa fa-trash"></i>&nbsp;
                 </button></td>
             </tr>
         `;
               $(this).siblings('table').find('.nestedTableBody').append(newRow);
           });

           // Remove Nested Row
           $('body').on('click', '.btn_RemoveRowNested', function () {
               $(this).closest('tr').remove();
               updateTotalWeightage();
           });

           // Add Main Row
           $('body').on('click', '.btn_Add', function () {
               var newRow = `
             <tr>
                 <td class="serial-number">New</td>
                 <td>
                     <select class="form-control form-control-sm ddlBSCDimension">
                         <option value="">Select One</option>
                         <option value="1">Financial</option>
                         <option value="2">Internal Process</option>
                         <option value="3">Customer</option>
                         <option value="4">Learning & Growth</option>
                     </select>
                 </td>
                 <td>
                     <textarea class="form-control   txtObjectiveGoal" rows="6"></textarea>
                 </td>
                 <td>
                     <button  type="button" class="btn btn-success btn-sm btn_ToggleNestedTable">
                         <i class="fa fa-plus"></i>&nbsp;  Add KPI
                     </button>
                     <div class="nested-table" style="display: none;">
                         <table class="table table-sm table-bordered">
                             <thead>
                                 <tr>
                                     <th  class="KPIHeader">   </th>
                                     <th>Weightage (%)</th>
                                     <th>Initiatives</th>
                                     <th>Deadline</th>
                                     
                                     <th>Remove</th>
                                 </tr>
                             </thead>
                             <tbody class="nestedTableBody">
                                 <!-- Nested rows go here -->
                             </tbody>
                         </table>
                         <button  type="button" class="btn btn-success btn-sm mt-2 btn_AddRowNested">
                             <i class="fa fa-plus"></i>&nbsp; Add KPI
                         </button>
                     </div>
                 </td>
                 <td>
                     <button  type="button" class="btn btn-info btn-sm btn_Add">
                         <i class="fa fa-plus"></i>&nbsp; Add Objective & Dimension
                     </button>
                 </td>
                 <td>
                     <button  type="button"    class="btn btn-danger btn-sm btn_Remove">
                         <i class="fa fa-trash"></i>&nbsp;  
                     </button>
                 </td>
             </tr>
         `;

               $('#gv_AppraisalFunc').append(newRow);
               updateSerialNumbers();
               const redAsterisk = '<span style="color: red;">*</span>';
               $(".KPIHeader").html((MValue === "OKR" ? "Key Results (KR) / KPI" : "KPI / Measure") + redAsterisk);

           });

           // Remove Main Row
           $('body').on('click', '.btn_Remove', function () {
               // Find the closest row and table
               var row = $(this).closest('tr');
               var table = $('#gv_AppraisalFunc');
               var rows = table.find('tbody tr');

               //// Debugging: Log number of rows in the table
               //console.log("Number of rows in table: ", rows.length);
               //alert(rows.length);
               //// Check if the table has only one row (excluding the header row)
               //if (rows.length === 1) {
               //    alert("The last row cannot be removed.");
               //    return; // Exit the function to prevent removal
               //}

               // Remove the selected row
               row.remove();

               // Update serial numbers or any other necessary updates
               updateSerialNumbers();
           });

       });



       // Handle the radio button change event


       function createRows(count) {




           $('#gv_AppraisalPartB').empty(); // Clear existing rows
           for (let i = 1; i <= count; i++) {
               const newRow = `
         <tr>
             <td>${i}</td>
             <td>
                 <div class="form-check">
                     <input class="form-check-input type-radio" type="radio" id="typePersonal_${i}" name="type_${i}" value="Personal">
                     <label class="form-check-label" for="typePersonal_${i}">Personal</label>
                 </div>
                 <div class="form-check">
                     <input class="form-check-input type-radio" type="radio" id="typeTeam_${i}" name="type_${i}" value="Team">
                     <label class="form-check-label" for="typeTeam_${i}">Team</label>
                 </div>
                 <div class="form-check">
                     <input class="form-check-input type-radio" type="radio" id="typeResultFocus_${i}" name="type_${i}" value="Result Focus">
                     <label class="form-check-label" for="typeResultFocus_${i}">Result Focus</label>
                 </div>
                 <div class="form-check">
                     <input class="form-check-input type-radio" type="radio" id="typeInterpersonalSkill_${i}" name="type_${i}" value="Interpersonal Skill">
                     <label class="form-check-label" for="typeInterpersonalSkill_${i}">Interpersonal Skill</label>
                 </div>
                 <div class="form-check">
                     <input class="form-check-input type-radio" type="radio" id="typeLeadership_${i}" name="type_${i}" value="Leadership">
                     <label class="form-check-label" for="typeLeadership_${i}">Leadership</label>
                 </div>
                 <div class="form-check">
                     <input class="form-check-input type-radio" type="radio" id="typeOthers_${i}" name="type_${i}" value="Others">
                     <label class="form-check-label" for="typeOthers_${i}">Others</label>
                 </div>
             </td>
             <td>
                 <select id="ddlSkill_${i}" class="form-control form-control-sm ddl-skill" style="width: 300px;">
                     <option value="">Select From List</option>
                      
                 </select>
                  <input type="text" id="txtOther_${i}" class="form-control form-control-sm txt-other" style="display:none;" >
             </td>
             <td>
                 <textarea id="SupportingEmp_${i}" class="form-control" rows="2"></textarea>
             </td>
             <td>
                  <input type="number" id="Score_${i}" class="form-control form-control-sm" value="5" readonly>
             </td>
             <td>
                 <select id="ddlWeight_${i}" class="form-control form-control-sm ddl-weight">
                   
                     <option value="1">1</option>
                     <option value="2">2</option>
                     <option value="3">3</option>
                     <option value="4">4</option>
                     <option value="5">5</option>
                 </select>
             </td>
         </tr>
     `;

               $('#gv_AppraisalPartB').append(newRow);
           }
       }

       $(document).on('change', '.type-radio', function () {
           const id = $(this).attr('id').split('_')[1];
           const selectedValue = $(this).val();

           if (selectedValue === 'Others') {
               $(`#txtOther_${id}`).show();
               $(`#ddlSkill_${id}`).hide();
           } else {
               $(`#txtOther_${id}`).hide();
               $(`#ddlSkill_${id}`).show();

               // Make AJAX call to fetch data based on the selected value
               $.ajax({
                   type: "POST",
                   url: "BSCOKRSelfKPI.aspx/GetKPIBehaviourNames", // Replace with your actual page name
                   data: JSON.stringify({ type: selectedValue }),
                   contentType: "application/json; charset=utf-8",
                   dataType: "json",
                   success: function (response) {
                       let skills = response.d; // 'd' is where ASP.NET Web Method returns data
                       let options = '<option value="">Select From List</option>';
                       $.each(skills, function (index, skill) {
                           options += `<option value="${skill}">${skill}</option>`;
                       });
                       $(`#ddlSkill_${id}`).html(options);
                   },
                   error: function (xhr, status, error) {
                       console.error("Error: " + error);
                   }
               });
           }
       });

       function validateAppraisalData() {
           var isValid = true;
           var errorMessage = '';
           var totalKpiWeight = 0;

           $('#gv_AppraisalFunc tr').each(function () {
               if ($(this).find('td').length === 0) return; // Skip header or empty rows

               var dimension = $(this).find('.ddlBSCDimension').val();
               var objectiveGoal = $(this).find('.txtObjectiveGoal').val();

               // Validate Dimension
               if (dimension !== undefined && dimension !== 'undefined' && !dimension) {
                   isValid = false;
                   //  errorMessage += 'Dimension is required.\n';
                   $(this).find('.ddlBSCDimension').addClass('input-error');
               } else {
                   $(this).find('.ddlBSCDimension').removeClass('input-error');
               }

               // Validate Objective Goal
               if (objectiveGoal !== undefined && objectiveGoal !== 'undefined' && !objectiveGoal) {
                   isValid = false;
                   //  errorMessage += 'Objective Goal is required.\n';
                   $(this).find('.txtObjectiveGoal').addClass('input-error');
               } else {
                   $(this).find('.txtObjectiveGoal').removeClass('input-error');
               }

               $(this).find('.nestedTableBody tr').each(function () {
                   var kpiMeasure = $(this).find('.txtKPI').val();
                   var kpiWeight = parseFloat($(this).find('.txtWeightage').val());
                   var initiatives = $(this).find('.txtInitiatives').val();
                   var deadline = $(this).find('.txtDeadline').val();

                   // Accumulate total KPI Weight
                   if (!isNaN(kpiWeight)) {
                       totalKpiWeight += kpiWeight;
                   }

                   // Validate KPI Measure
                   if (!kpiMeasure) {
                       isValid = false;
                       errorMessage += 'KPI Measure is required.\n';
                       $(this).find('.txtKPI').addClass('input-error');
                   } else {
                       $(this).find('.txtKPI').removeClass('input-error');
                   }

                   // Validate KPI Weight
                   if (isNaN(kpiWeight) || kpiWeight <= 0 || kpiWeight > 100) {
                       isValid = false;
                       errorMessage += 'KPI Weight must be between 1 and 100.\n';
                       $(this).find('.txtWeightage').addClass('input-error');
                   } else {
                       $(this).find('.txtWeightage').removeClass('input-error');
                   }

                   // Validate Initiatives
                   if (!initiatives) {
                       isValid = false;
                       errorMessage += 'Initiatives are required.\n';
                       $(this).find('.txtInitiatives').addClass('input-error');
                   } else {
                       $(this).find('.txtInitiatives').removeClass('input-error');
                   }

                   // Validate Deadline
                   if (!deadline) {
                       isValid = false;
                       errorMessage += 'Deadline is required.\n';
                       $(this).find('.txtDeadline').addClass('input-error');
                   } else {
                       $(this).find('.txtDeadline').removeClass('input-error');
                   }
               });
           });

           // Validate total KPI Weight
           if (totalKpiWeight > 100) {
               isValid = false;
               errorMessage += 'Total KPI Weight cannot exceed 100.\n';
           }

           if (!isValid) {
               alert('Validation Failed:\n' + errorMessage);
           }

           return isValid;
       }
       function validateAppraisalPartB() {
           var isValid = true;
           var errorMessage = '';
           var totalScore = 0;

           $('#gv_AppraisalPartB tr').each(function () {
               var skillType = $(this).find('input[type="radio"]:checked').siblings('label').text().trim();
               var skillInfo;
               var hasSkillType = skillType !== '';

               // Validate Skill Type
               if (!hasSkillType) {
                   isValid = false;
                   errorMessage += 'Skill Type is required.\n';
                   $(this).find('input[type="radio"]').addClass('input-error');
               } else {
                   $(this).find('input[type="radio"]').removeClass('input-error');
               }

               // Determine Skill Info based on Skill Type
               if (skillType === 'Others') {
                   skillInfo = $(this).find('.txt-other').val();
                   $(this).find('.txt-other').removeClass('input-error');
                   if (!skillInfo) {
                       isValid = false;
                       //   errorMessage += 'Other Skill Info is required.\n';
                       $(this).find('.txt-other').addClass('input-error');
                   }
               } else {
                   skillInfo = $(this).find('.ddl-skill').val();
                   $(this).find('.ddl-skill').removeClass('input-error');
                   if (!skillInfo) {
                       isValid = false;
                       //    errorMessage += 'Skill Info is required.\n';
                       $(this).find('.ddl-skill').addClass('input-error');
                   }
               }

               // Validate Supporting Example
               //var supportingEmp = $(this).find('textarea').val();
               //if (!supportingEmp) {
               //    isValid = false;
               //    errorMessage += 'Supporting Example is required.\n';
               //    $(this).find('textarea').addClass('input-error');
               //} else {
               //    $(this).find('textarea').removeClass('input-error');
               //}

               // Accumulate total Score
               var score = parseFloat($(this).find('input[id^="Score_"]').val());
               if (!isNaN(score)) {
                   totalScore += score;
               }

               // Validate Score
               if (isNaN(score) || score <= 0 || score > 25) {
                   isValid = false;
                   // errorMessage += 'Score must be between 1 and 25.\n';
                   $(this).find('input[id^="Score_"]').addClass('input-error');
               } else {
                   $(this).find('input[id^="Score_"]').removeClass('input-error');
               }
           });

           // Validate total Score
           if (totalScore > 25) {
               isValid = false;
               errorMessage += 'Total Score cannot exceed 25.\n';
           }

           if (!isValid) {
               alert('Validation Failed:\n' + errorMessage);
           }

           return isValid;
       }

       function submitData(actionType) {

           var isPartAValid = validateAppraisalData();

           // Validate Part B
           var isPartBValid = validateAppraisalPartB();

           // If both validations pass, proceed with data submission
           if (isPartAValid && isPartBValid) {
               var funcRowsData = [];

               $('#gv_AppraisalFunc tr').each(function () {
                   if ($(this).find('td').length === 0) return; // Skip header or empty rows
                   var dimensionTxt = $(this).find('.ddlBSCDimension option:selected').text() || null;

                   var dimension = $(this).find('.ddlBSCDimension').val() || null;
                   var objectiveGoal = $(this).find('.txtObjectiveGoal').val() || null;

                   $(this).find('.nestedTableBody tr').each(function () {
                       var rowData = {
                           Dimension: dimension,
                           DimensionStr: dimensionTxt,
                           ObjectiveGoal: objectiveGoal,
                           KPIMeasure: $(this).find('.txtKPI').val() || null,
                           KpiWeight: parseFloat($(this).find('.txtWeightage').val()) || 0,
                           Initiatives: $(this).find('.txtInitiatives').val() || null,
                           Deadline: $(this).find('.txtDeadline').val() ? new Date($(this).find('.txtDeadline').val()).toISOString() : null
                       };

                       funcRowsData.push(rowData);
                   });
               });

               var partBRowsData = [];

               $('#gv_AppraisalPartB tr').each(function () {
                   var selectedType = $(this).find('input[type="radio"]:checked').siblings('label').text().trim();
                   var skillInfo;

                   // Set SkillInfo based on SkillType
                   if (selectedType === 'Others') {
                       skillInfo = $(this).find('.txt-other').val() || null;
                   } else {
                       skillInfo = $(this).find('.ddl-skill').val() || null;
                   }

                   // Collect data from each row
                   var rowData = {
                       SkillType: selectedType || null, // Use the selected radio button text
                       SkillInfo: skillInfo, // Conditionally set SkillInfo
                       SupportingEmp: $(this).find('textarea').val() || null,
                       Score: parseFloat($(this).find('input[id^="Score_"]').val()) || 0,
                       SetScore: parseInt($(this).find('.ddl-weight').val()) || 0
                   };

                   partBRowsData.push(rowData);
               });


               //var requestData = {
               //    FuncData: funcRowsData,
               //    PartBData: partBRowsData
               //};

               //console.log("Sending request data:", JSON.stringify(requestData, null, 2));
               var Comments = document.getElementById('txt_Comments').value;
               $.ajax({
                   type: "POST",
                   url: "BSCOKRSelfKPI.aspx/SaveData",
                   data: JSON.stringify({ FuncData: funcRowsData, PartBData: partBRowsData, actionType: actionType, Comments: Comments }),
                   contentType: "application/json; charset=utf-8",
                   dataType: "json",
                   success: function (response) {
                       var resultData = response.d;

                       if (resultData.Result) {
                           alert(resultData.Message);
                           window.location.href = 'BSCOKRAppraisalSelfList.aspx';
                       } else {
                           alert('Error: ' + resultData.Message);
                       }
                   },
                   error: function (xhr, status, error) {
                       console.error("Error:", error);
                       alert('An error occurred: ' + error);
                   }
               });

           }

       }



       function EmployeeInfo() {
           $.ajax({
               type: "POST",
               url: "BSCSelfFunctionalApproval.aspx/GetEmpinfo",
               data: JSON.stringify({}),
               contentType: "application/json; charset=utf-8",
               dataType: "json",
               success: function (response) {
                   var data = response.d;

                   // Populate the labels with the data
                   $('#lblEmpId').text(data.EmpMasterCode);
                   $('#lblEmployeeName').text(data.EmpName);
                   $('#comNameLabel').text(data.CompanyName);
                   $('#divisionNameLabel').text(data.DivisionName);
                   $('#divWingNameLabel').text(data.DivisionWingName);
                   $('#deptNameLabel').text(data.DepartmentName);
                   $('#secNameLabel').text(data.SectionName);
                   $('#subSectionLabel').text(data.SubSectionName);
                   $('#desigNameLabel').text(data.Designation);
                   $('#joiningDateLabel').text(data.DateOfJoin);
                   $('#LocationLabel').text(data.SalaryLocation);
                   $('#lblPlace').text(data.Location);

                   $('#ReportingLabel').text(data.ReportingToName);

                   // Hidden Fields or other elements
                   $('#HFCompanyId').val(data.CompanyId);
                   $('#comIdHiddenField').val(data.CompanyId);
                   $('#divitionIdHiddenField').val(data.DivisionId);
                   $('#divWingIdHiddenField').val(data.DivisionWingId);
                   $('#deptIdHiddenField').val(data.DepartmentId);
                   $('#secIdHiddenField').val(data.SectionId);
                   $('#subSectionHiddenField').val(data.SubSectionId);
                   $('#desigIdHiddenField').val(data.DesignationId);
                   $('#id_Empid').val(data.EmpInfoId);
               },
               error: function (error) {
                   console.error("Error:", error);
               }
           });

       }

       function formatDate(date) {
           if (!date) return ''; // Return an empty string if date is null
           const options = { day: '2-digit', month: 'short', year: 'numeric' };
           return date.toLocaleDateString('en-US', options).replace(',', '').replace(/\s+/g, '-');
       }

        function KPIFuncInfo() {
            var empInfoId = parseInt(getQueryStringParameter('EmpInfoId') || getQueryStringParameter('empInfoId') || 0);
            var financialYearId = parseInt(getQueryStringParameter('financialYearId') || getQueryStringParameter('FinancialYearId') || 0);
            $.ajax({
                type: "POST",
                url: "BSCSelfFunctionalApproval.aspx/GetFuncDataList",
                data: JSON.stringify({ empInfoId: empInfoId, financialYearId: financialYearId }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var data = response.d;
                    var tbody = $("#gv_AppraisalFunc");
                    if (Array.isArray(data)) {
                        tbody.empty(); // Clear the table body ONCE
                    }
                   var _t = 0;
                   var serialNumber = 1; // Initialize serial number counter
                   if (Array.isArray(data)) {
                       data.forEach(function (item, index) {
                           var isMatched = false;
                           _t = _t + (item.KpiWeight ? parseFloat(item.KpiWeight) : 0);
                           $("#gv_AppraisalFunc > tr").each(function () {

                               var existingDimension = $(this).find('.ddlBSCDimension').val();
                               var existingObjectiveGoal = $(this).find('.txtObjectiveGoal').val();

                               if (existingDimension === item.Dimension && existingObjectiveGoal === item.ObjectiveGoal) {
                                   var deadlineDate = item.Deadline ? new Date(parseInt(item.Deadline.replace(/\/Date\((\d+)\)\//, '$1'))) : null;
                                   var newNestedRow = `
             <tr>
                 <td  class="fixed-width-kpi">${item.KPIMeasure} <textarea style="display:none" rows="3" class="form-control txtKPI">${item.KPIMeasure}</textarea></td>
                 <td  class="fixed-width-weightage">${item.KpiWeight}<input    style="display:none" type="decimal"  oninput="validateNumericInput(this)"      class="form-control form-control-sm txtWeightage" value="${item.KpiWeight}" /></td>
                 <td  class="fixed-width-initiatives">${item.Initiatives}<textarea rows="3"  style="display:none" class="form-control txtInitiatives">${item.Initiatives}</textarea></td>
                 <td class="fixed-width-deadline">${item.deadlineDate}<input type="date"  style="display:none" class="form-control form-control-sm txtDeadline" value="${deadlineDate ? deadlineDate.toISOString().split('T')[0] : ""}" /></td>
               
             </tr>
             `;

                                   var nestedTableBody = $(this).find('.nestedTableBody');
                                   if (nestedTableBody.length > 0) {
                                       nestedTableBody.append(newNestedRow);
                                       $(this).find('.nested-table').show();  // Ensure the nested table is shown
                                       isMatched = true;
                                       return false; // Exit the .each loop once a match is found
                                   }
                               }
                           });

                           if (!isMatched) {
                               var deadlineDate = item.Deadline ? new Date(parseInt(item.Deadline.replace(/\/Date\((\d+)\)\//, '$1'))) : null;
                               var newRow = `
         <tr>
             <td class="serial-number fixed-width-serial">${serialNumber++}</td>
             <td class="fixed-width-dimension">${item.DimensionStr}
                 <select style="display:none"  class="form-control form-control-sm ddlBSCDimension">
                     <option value="">Select One</option>
                     <option value="1" ${item.Dimension === "1" ? "selected" : ""}>Financial</option>
                     <option value="2" ${item.Dimension === "2" ? "selected" : ""}>Internal Process</option>
                     <option value="3" ${item.Dimension === "3" ? "selected" : ""}>Customer</option>
                     <option value="4" ${item.Dimension === "4" ? "selected" : ""}>Learning & Growth</option>
                 </select>
             </td>
             <td  class="fixed-width-goal">
               ${item.ObjectiveGoal}  <textarea style="display:none"  class="form-control txtObjectiveGoal" rows="6">${item.ObjectiveGoal}</textarea>
             </td>
             <td>
                
                 <div class="nested-table" style="display: block;"> <!-- Ensure nested-table is shown -->
                     <table class="table table-sm table-bordered">
                         <thead>
                             <tr>
                                 <th  class="KPIHeader"> </th>
                                 <th>Weightage (%)</th>
                                 <th>Initiatives</th>
                                 <th>Deadline</th>
                                 
                             </tr>
                         </thead>
                         <tbody class="nestedTableBody">
                             <tr>
                                 <td  class="fixed-width-kpi">${item.KPIMeasure}<textarea style="display:none"  rows="3" class="form-control txtKPI">${item.KPIMeasure}</textarea></td>
                                 <td  class="fixed-width-weightage">${item.KpiWeight}<input  style="display:none"   type="decimal"       oninput="validateNumericInput(this)" class="form-control form-control-sm txtWeightage" value="${item.KpiWeight}" /></td>
                                 <td  class="fixed-width-initiatives">${item.Initiatives}<textarea style="display:none"  rows="3" class="form-control txtInitiatives">${item.Initiatives}</textarea></td>
                                 <td class="fixed-width-deadline">${item.deadlineDate}<input type="date" style="display:none"  class="form-control form-control-sm txtDeadline" value="${deadlineDate ? deadlineDate.toISOString().split('T')[0] : ""}" /></td>
                                
                             </tr>
                         </tbody>
                     </table>
                     
                 </div>
             </td>
            
         </tr>
         `;
                               $("#gv_AppraisalFunc").append(newRow);




                              

                               $(".KPIHeader").text(MValueEdit === "OKR" ? "Key Results (KR) / KPI" : "KPI / Measure");
                           }


                       });

                   } else {
                       console.error("Data is not an array:", data);
                   }
                   $('#totalWeightage').text(_t.toFixed(2));
               },
               error: function (error) {
                   console.error("AJAX request failed:", error);
               }
           });
       }

       function loadAppraisalData() {
           console.log("loadAppraisalData function called");  // For debugging
           $.ajax({
               type: "POST",
               url: "BSCSelfFunctionalApproval.aspx/GetAppraisalFinalStatus",
               contentType: "application/json; charset=utf-8",
               dataType: "json",
               success: function (response) {
                        
                   var data = response.d;
                   if (data) {
                       var selectedValue;
                       if (data.GeneralIncrement) selectedValue = "1";
                       else if (data.SpecialIncrement) selectedValue = "2";
                       else if (data.IsPromotion) selectedValue = "3";
                       else if (data.Pip) selectedValue = "4";
                       else if (data.GeneralIncrement && data.IsPromotion) selectedValue = "6";
                       else if (data.Other) selectedValue = "6";

                       var selectedText = "";
                       switch (selectedValue) {
                           case "1":
                               selectedText = "General Increment";
                               break;
                           case "2":
                               selectedText = "Special Increment";
                               break;
                           case "3":
                               selectedText = "Promotion";
                               break;
                           case "4":
                               selectedText = "Performance Improvement Plan";
                               break;
                           case "6":
                               selectedText = "Promotion with Increment"; // Adjust this as necessary
                               break;
                           default:
                               selectedText = "No Selection";
                       }

                       // Set the label text
                       $("#<%= lblRecommendation.ClientID %>").text("Recommendation: " + selectedText);


                       $("#<%= txtStep.ClientID %>").text("Special Step: " + (data.SpecialStep || "N/A"));
                       $("#<%= txtnote.ClientID %>").text("Note: " + (data.Note || "N/A"));
                       $("#<%= txtJustification.ClientID %>").text("Justification: " + (data.Justification || "N/A"));

                       if (data.DocumentLink) {
                           $("#<%= HLDocumentLink.ClientID %>").text("Preview");
                    $("#<%= lbFileName.ClientID %>").text("File Name: " + data.FileName);
                           $("#<%= HLDocumentLink.ClientID %>").attr("href", "../UploadMeetingDocument/" + data.DocumentLink); $("#cpFormBody_HLDocumentLink").attr("target", "_blank"); // Open link in a new tab
                } else {
                    $("#<%= lbFileName.ClientID %>").text("No Document Found");
                    $("#<%= HLDocumentLink.ClientID %>").text("");
                    $("#<%= HLDocumentLink.ClientID %>").attr("href", "");
                }
                   }
               },
               error: function (error) {
                   console.error("Error fetching data:", error);
               }
           });
       }


       function fetchAppraisalTrainingList() {
           $.ajax({
               type: "POST",
               url: "BSCSelfFunctionalApproval.aspx/GetAppraisalTrainingList",
               contentType: "application/json; charset=utf-8",
               dataType: "json",
               success: function (response) {
                   var data = response.d;
                   var tbody = $("#trainingTable tbody");

                   // Clear any existing rows
                   tbody.empty();

                   // Check if data exists
                   if (data && data.length > 0) {
                       data.forEach(function (item) {
                           var row = `
                        <tr>
                            <td>${item.TrainingNeeds}</td>
                            <td>${item.Quater}</td>
                        </tr>
                    `;
                           tbody.append(row); // Append the new row to the table body
                       });
                   } else {
                       // If no data, show a message
                       var emptyRow = `
                    <tr>
                        <td colspan="2" class="text-center">No training data available</td>
                    </tr>
                `;
                       tbody.append(emptyRow);
                   }
               },
               error: function (error) {
                   console.error("Error calling WebMethod:", error);
               }
           });
       }
    
       function AppraisalFuncInfo() {
           $.ajax({
               type: "POST",
               url: "BSCSelfFunctionalApproval.aspx/GetAppraisalFuncDataList",
               data: JSON.stringify({}),
               contentType: "application/json; charset=utf-8",
               dataType: "json",
               success: function (response) {
                   var data = response.d;
                   var tbody = $("#gv_AppraisalFuncApp");
                   tbody.empty(); // Clear the table body
                   var _KpiWeight = 0;
                   var _selfApp = 0;
                   var _supApp = 0;
                   var serialNumber = 1; // Initialize serial number counter

                   if (Array.isArray(data)) {
                       data.forEach(function (item) {
                           var isMatched = false;
                           _KpiWeight += item.KpiWeight ? parseFloat(item.KpiWeight) : 0;
                           _selfApp += item.SelfMark ? parseFloat(item.SelfMark) : 0;
                           _supApp += item.SupervisorMark ? parseFloat(item.SupervisorMark) : 0;

                           $("#gv_AppraisalFuncApp > tr").each(function () {
                               var existingDimension = $(this).find('.ddlBSCDimensionKPI').text().trim();
                               var existingObjectiveGoal = $(this).find('.txtObjectiveGoalKPI').text().trim();

                               // Debugging output
                               console.log("Comparing:", existingDimension, existingObjectiveGoal, "with", item.Dimension, item.ObjectiveGoal);

                               // Fixed comparison condition
                               if (existingDimension === item.DimensionStr.trim() &&
                                   existingObjectiveGoal === item.ObjectiveGoal.trim()) {

                                   var newNestedRow = `
                                <tr>
                                    <td class="fixed-width-kpi">${item.KPIMeasure}</td>
                                    <td class="fixed-width-weightage">${item.KpiWeight}</td>
                                    <td class="fixed-width-initiatives">${item.Initiatives}</td>
                                    <td class="fixed-width-deadline">${item.deadlineDate}</td>
                                    <td class="fixed-width-deadline">${item.MidYearStatus}</td>
                                    <td class="fixed-width-deadline">${item.ResultYearEnd}</td>
                                    <td class="fixed-width-deadline">${item.SelfMark}</td>
                                    <td class="fixed-width-deadline">${item.SupervisorMark}</td>
                                </tr>
                            `;
                                   var nestedTableBody = $(this).find('.nestedTableBodyApp');
                                   if (nestedTableBody.length > 0) {
                                       nestedTableBody.append(newNestedRow);
                                       $(this).find('.nested-table').show();  // Ensure the nested table is shown
                                       isMatched = true;
                                       return false; // Break out of the .each() loop
                                   }
                               }
                           });

                           if (!isMatched) {
                               var newRow = `
                            <tr>
                                <td class="fixed-width-serial serial-number">${serialNumber++}</td>
                                <td class="fixed-width-dimension">
                                    <label class="ddlBSCDimensionKPI">${item.DimensionStr}</label>
                                </td>
                                <td class="fixed-width-goal">
                                    <label class="txtObjectiveGoalKPI">${item.ObjectiveGoal}</label>
                                </td>
                                <td>
                                    <div class="nested-table" style="display: block;">
                                        <table class="table table-sm table-bordered">
                                            <thead>
                                                <tr>
                                                    <th class="KPIHeader fixed-width-kpi">${MValueEdit === "OKR" ? "Key Results (KR) / KPI" : "KPI / Measure"}</th>
                                                    <th class="fixed-width-weightage">Weightage (%)</th>
                                                    <th class="fixed-width-initiatives">Initiatives</th>
                                                    <th class="fixed-width-deadline">Deadline</th>
                                                    <th class="fixed-width-deadline">Mid Year Status</th>
                                                    <th class="fixed-width-deadline">Results (Year End)</th>
                                                    <th class="fixed-width-deadline">Self-Mark</th>
                                                    <th class="fixed-width-deadline">Supervisor Mark</th>
                                                </tr>
                                            </thead>
                                            <tbody class="nestedTableBodyApp">
                                                <tr>
                                                    <td class="fixed-width-kpi">${item.KPIMeasure}</td>
                                                    <td class="fixed-width-weightage">${item.KpiWeight}</td>
                                                    <td class="fixed-width-initiatives">${item.Initiatives}</td>
                                                    <td class="fixed-width-deadline">${item.deadlineDate}</td>
                                                    <td class="fixed-width-deadline">${item.MidYearStatus}</td>
                                                    <td class="fixed-width-deadline">${item.ResultYearEnd}</td>
                                                    <td class="fixed-width-deadline">${item.SelfMark}</td>
                                                    <td class="fixed-width-deadline">${item.SupervisorMark}</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        `;
                               tbody.append(newRow);
                           }
                       });
                   } else {
                       console.error("Data is not an array:", data);
                   }
                   $('#totalWeightageApp').text(_KpiWeight.toFixed(2));
                   $('#selfApp').text(_selfApp.toFixed(2));
                   $('#supperApp').text(_supApp.toFixed(2));
               },
               error: function (error) {
                   console.error("AJAX request failed:", error);
               }
           });
       }


       function bindEventHandlers() {


           //$('body').on('click', '.btn_ToggleNestedTable', function () {
           //    var nestedTable = $(this).siblings('.nested-table');
           //    var button = $(this);

           //    nestedTable.toggle();

           //    if (nestedTable.is(':visible')) {
           //        button.html('<i class="fa fa-minus"></i>&nbsp; Hide Sub');
           //        button.removeClass("btn-success").addClass("btn-warning");
           //    } else {
           //        button.html('<i class="fa fa-plus"></i>&nbsp; Show Sub');
           //        button.removeClass("btn-warning").addClass("btn-success");
           //    }
           //});
           //$('body').on('click', '.btn_ToggleNestedTable', function () {
           //    var nestedTable = $(this).siblings('.nested-table');
           //    var button = $(this);

           //    nestedTable.toggle();

           //    //if (nestedTable.is(':visible')) {
           //    //    button.html('<i class="fa fa-minus"></i>&nbsp; Hide Sub');
           //    //    button.removeClass("btn-success").addClass("btn-warning");
           //    //} else {
           //    //    button.html('<i class="fa fa-plus"></i>&nbsp; Show Sub');
           //    //    button.removeClass("btn-warning").addClass("btn-success");
           //    //}
           //});

           //$('body').on('click', '.btn_AddRowNested', function () {
           //    var newRow = ``;
           //    $(this).siblings('table').find('.nestedTableBody').append(newRow);
           //});

           //$('body').on('click', '.btn_RemoveRowNested', function () {
           //    $(this).closest('tr').remove();
           //});

           //$('body').on('click', '.btn_Remove', function () {
           //    $(this).closest('tr').remove();
           //});
       }


       function handleSkillTypeChange(skillType, index, skillInfo) {
           $.ajax({
               type: "POST",
               url: "BSCOKRSelfKPI.aspx/GetKPIBehaviourNames", // Replace with your actual page name
               data: JSON.stringify({ type: skillType }),
               contentType: "application/json; charset=utf-8",
               dataType: "json",
               success: function (response) {
                   let skills = response.d; // 'd' is where ASP.NET Web Method returns data
                   let options = '<option value="">Select From List</option>';
                   $.each(skills, function (i, skill) {
                       let selected = skill === skillInfo ? 'selected' : '';
                       options += `<option value="${skill}" ${selected}>${skill}</option>`;
                   });
                   $(`#ddlSkill_${index}`).html(options);
               },
               error: function (xhr, status, error) {
                   console.error("Error: " + error);
               }
           });
       }

       // Function to create a new row
       function createNewRow(index, item) {
           let ddlDis = '';
           let txtDis = '';
           let rowHtml = '';

           if (item.SkillType === 'Others') {
               ddlDis = 'none';
           } else {
               txtDis = 'none';
           }

           rowHtml = `
     <tr>
         <td class="serial-numberB">${index + 1}</td>
         <td>
             <div class="form-check">
                 <input class="form-check-input type-radio" type="radio" id="typePersonal_${index}" name="type_${index}" value="Personal" ${item.SkillType === "Personal" ? "checked" : ""}>
                 <label class="form-check-label" for="typePersonal_${index}">Personal</label>
             </div>
             <div class="form-check">
                 <input class="form-check-input type-radio" type="radio" id="typeTeam_${index}" name="type_${index}" value="Team" ${item.SkillType === "Team" ? "checked" : ""}>
                 <label class="form-check-label" for="typeTeam_${index}">Team</label>
             </div>
             <div class="form-check">
                 <input class="form-check-input type-radio" type="radio" id="typeResultFocus_${index}" name="type_${index}" value="Result Focus" ${item.SkillType === "Result Focus" ? "checked" : ""}>
                 <label class="form-check-label" for="typeResultFocus_${index}">Result Focus</label>
             </div>
             <div class="form-check">
                 <input class="form-check-input type-radio" type="radio" id="typeInterpersonalSkill_${index}" name="type_${index}" value="Interpersonal Skill" ${item.SkillType === "Interpersonal Skill" ? "checked" : ""}>
                 <label class="form-check-label" for="typeInterpersonalSkill_${index}">Interpersonal Skill</label>
             </div>
             <div class="form-check">
                 <input class="form-check-input type-radio" type="radio" id="typeLeadership_${index}" name="type_${index}" value="Leadership" ${item.SkillType === "Leadership" ? "checked" : ""}>
                 <label class="form-check-label" for="typeLeadership_${index}">Leadership</label>
             </div>
             <div class="form-check">
                 <input class="form-check-input type-radio" type="radio" id="typeOthers_${index}" name="type_${index}" value="Others" ${item.SkillType === "Others" ? "checked" : ""}>
                 <label class="form-check-label" for="typeOthers_${index}">Others</label>
             </div>
         </td>
         <td>
             <select style="display:${ddlDis};" id="ddlSkill_${index}" class="form-control form-control-sm ddl-skill" style="width: 300px;">
                 <option value="">Select From List</option>
             </select>
             <input type="text" id="txtOther_${index}" class="form-control form-control-sm txt-other" style="display:${txtDis};" value="${item.SkillInfo}">
         </td>
         <td>
             <textarea id="SupportingEmp_${index}" class="form-control" rows="2">${item.SupportingEmp}</textarea>
         </td>
         <td>
             <input type="number" id="Score_${index}" class="form-control form-control-sm" value="${item.Score}" readonly>
         </td>
         <td>
             <select id="ddlWeight_${index}" class="form-control form-control-sm ddl-weight">
                 <option value="1" ${item.SetScore === 1 ? "selected" : ""}>1</option>
                 <option value="2" ${item.SetScore === 2 ? "selected" : ""}>2</option>
                 <option value="3" ${item.SetScore === 3 ? "selected" : ""}>3</option>
                 <option value="4" ${item.SetScore === 4 ? "selected" : ""}>4</option>
                 <option value="5" ${item.SetScore === 5 ? "selected" : ""}>5</option>
             </select>
         </td>
     </tr>
 `;

           // Append the row to the table
           $('#gv_AppraisalPartB').append(rowHtml);

           if (item.SkillType !== 'Others') {
               handleSkillTypeChange(item.SkillType, index, item.SkillInfo);
           }
       }

     function AppcreateNewRow(index, item) {
           let ddlDis = '';
           let txtDis = '';
           let rowHtml = '';

           if (item.SkillType === 'Others') {
               ddlDis = 'none';
           } else {
               txtDis = 'none';
           }

           rowHtml = `
     <tr>
         <td class="serial-numberB">${index + 1}</td>
         
         <td>
            ${item.SkillInfo}
         </td>
         <td>
             ${item.SupportingEmp} 
         </td>
         <td>
              ${item.Score} 
         </td>
         <td>
            ${item.SetScore }
              
         </td>
         <td>
            ${item.SetScore }
              
         </td>
         <td>
            ${item.SetScore }
              
         </td>
     </tr>
 `;

           // Append the row to the table
         $('#gv_AppPartB').append(rowHtml);

           //if (item.SkillType !== 'Others') {
           //    handleSkillTypeChange(item.SkillType, index, item.SkillInfo);
           //}
       }


       // Function to fetch and populate data
       function PartBFuncInfo() {
           $.ajax({
               type: "POST",
               url: "BSCSelfFunctionalApproval.aspx/GetPartBDataList", // Update this URL to your actual web service
               data: JSON.stringify({}),
               contentType: "application/json; charset=utf-8",
               dataType: "json",
               success: function (response) {
                   var data = response.d; // Assuming response.d contains the data

                   var tbody = $("#gv_AppraisalPartB");
                   if (Array.isArray(data)) {
                       data.forEach(function (item, index) {
                           tbody.empty(); // Clear the table body
                       })
                   } // Clear the table body

                   if (Array.isArray(data)) {
                       data.forEach(function (item, index) {
                           var newRow = createNewRow(index, item);
                           tbody.append(newRow);
                       });


                   } else {
                       console.error("Data is not an array:", data);
                   }

                   // Update serial numbers after adding rows
                   updateSerialNumbers();
                   // Calculate total weight after adding rows
                   calculateTotalWeightnn();
               },
               error: function (error) {
                   console.error("AJAX request failed:", error);
               }
           });
       }
       function AppPartBFuncInfo() {
           $.ajax({
               type: "POST",
               url: "BSCSelfFunctionalApproval.aspx/GetAppPartBDataList", // Update this URL to your actual web service
               data: JSON.stringify({}),
               contentType: "application/json; charset=utf-8",
               dataType: "json",
               success: function (response) {
                   var data = response.d; // Assuming response.d contains the data

                   var tbody = $("#gv_AppPartB");
                   tbody.empty(); // Clear the table body once at the start

                   let totalWeight = 0; // Initialize total weight
                   let totalExpectedNumber = 0; // Initialize total expected number
                   let totalSelfScore = 0; // Initialize total expected number
                   let supperNumber = 0; // Initialize supper number
 

                   if (Array.isArray(data)) {
                       data.forEach(function (item, index) {
                           var rowHtml = `
<tr>
    <td class="serial-numberB">${index + 1}</td>
    <td>${item.SkillInfo}</td>
    <td>${item.SupportingEmp}</td>
    <td>${item.Score}</td>
    <td>${item.SetScore}</td>
    <td>${item.SelfScore}</td>
    <td>${item.SupervisorScore}</td>
</tr>`;
                           tbody.append(rowHtml); // Append each row to the table body

                           // Update totals here
                           totalWeight += item.SetScore; // Assuming SetScore is the weight
                           totalExpectedNumber += item.Score; // Adjust if you have a specific field for expected numbers
                           totalSelfScore += item.SelfScore; // Adjust if you have a specific field for expected numbers
                           supperNumber += item.SupervisorScore; // Adjust if this isn't the correct field for supper number
                       });

                       // Update footer with calculated totals
                       $("#totalWeightApp").text(totalWeight);
                       $("#totalExpectedNumberApp").text(totalExpectedNumber);
                       $("#totalSelfNumberApp").text(totalSelfScore);
                       $("#supperNumberApp").text(supperNumber);
                   } else {
                       console.error("Data is not an array:", data);
                   }
               },
               error: function (error) {
                   console.error("AJAX request failed:", error);
               }
           });
       }


       function loadAppraisalScores() {
           $.ajax({
               type: "POST",
               url: "BSCSelfFunctionalApproval.aspx/GetAppraisalScores",
                 contentType: "application/json; charset=utf-8",
               dataType: "json",
               success: function (response) {
                   if (response.d.success) {
                       // Update UI elements with returned values using ClientID
                       $("#<%= partAScore.ClientID %>").text(response.d.partA);
                $("#<%= partBScore.ClientID %>").text(response.d.partB);
                $("#<%= totalScore.ClientID %>").text(response.d.total);
                $("#<%= lblStatus.ClientID %>").text(response.d.status);
            } else {
                // Handle any error messages returned from the server
                alert(response.d.message);
            }
        },
        error: function (error) {
            console.error("Error fetching appraisal scores:", error);
        }
    });
       }

       function fetchApprovalData() {
           $.ajax({
               type: "POST",
               url: "BSCSelfFunctionalApproval.aspx/GetApprovalDataList", // Update this URL to your actual web service
               data: JSON.stringify({}),
               contentType: "application/json; charset=utf-8",
               dataType: "json",
               success: function (response) {
                   var data = response.d; // Assuming response.d contains the data

                   var tbody = $("#approvalStatusList");
                   if (Array.isArray(data)) {
                       data.forEach(function (item, index) {
                           tbody.empty(); // Clear the table body
                       })
                   } // Clear the table body ${index + 1}
                   
                   if (Array.isArray(data)) {
                       data.forEach(function (item, index) {
                           var row = '<tr>' +
                               '<td>' + item.SerialNumber + '</td>' +
                               '<td>' + item.Employee + '</td>' +
                               '<td>' + item.Comments + '</td>' +
                               '<td>' + item.ApprovalDate + '</td>' +
                               '<td>' + item.ApprovalStatusText + '</td>' +
                               '</tr>';
                          
                           tbody.append(row);
                       });


                   } else {
                       console.error("Data is not an array:", data);
                   }


               },
               error: function (error) {
                   console.error("AJAX request failed:", error);
               }
           });
       }



       function fetchRadioOptions() {
           $.ajax({
               type: "POST",
               url: "BSCSelfFunctionalApproval.aspx/RadioTextValue", // This points to the WebMethod in the code-behind
               data: '{}', // No parameters needed
               contentType: "application/json; charset=utf-8",
               dataType: "json",
               success: function (response) {
                   // Handle the success and populate the radio buttons
                   var data = response.d;
                   var radioGroup = $('#radioGroup');
                   radioGroup.empty(); // Clear existing options

                   // Dynamically create radio buttons
                   $.each(data, function (index, item) {
                       var radioOption = '<input type="radio" id="' + item.Value + '" name="approvalStatus" value="' + item.Value + '" class="checkbox"' + (item.Enabled ? '' : ' disabled') + '>';
                       var label = '<label for="' + item.Value + '">' + item.Text + '</label>';
                       radioGroup.append(radioOption + label);
                   });

                   // Optionally, set a default selection
                   if (data.length > 0) {
                       $('#' + data[0].Value).prop('checked', true); // Default to the first option

                       if (data.length > 1 && data[1].Enabled === false) {
                           $('#' + data[1].Value).prop('checked', true); // Select the second option if disabled
                       }
                   }

               },
               error: function (error) {
                   // Handle any errors
                   console.log(error);
               }
           });
       }

       // Call the function to populate data when needed


       //function PartBFuncInfo() {
       //    $.ajax({
       //        type: "POST",
       //        url: "BSCSelfFunctionalApproval.aspx/GetPartBDataList", // Update this URL to your actual web service
       //        data: JSON.stringify({}),
       //        contentType: "application/json; charset=utf-8",
       //        dataType: "json",
       //        success: function (response) {
       //            var data = response.d; // Assuming response.d contains the data

       //            var tbody = $("#gv_AppraisalPartB");
       //            if (Array.isArray(data)) {
       //                data.forEach(function (item, index) {
       //                    tbody.empty(); // Clear the table body
       //                }) }
       //            var totalScore = 0;

       //            if (Array.isArray(data)) {
       //                data.forEach(function (item, index) {
       //                    var newRow = createNewRow(index, item);
       //                    tbody.append(newRow);
       //                });
       //            } else {
       //                console.error("Data is not an array:", data);
       //            }
       //        },
       //        error: function (error) {
       //            console.error("AJAX request failed:", error);
       //        }
       //    });
       //}

       function validateNumericInput(input) {
           let value = input.value;

           // Match the valid pattern for decimal numbers
           const regex = /^\d*\.?\d{0,2}$/;

           // If the input value doesn't match the pattern, revert the value
           if (!regex.test(value)) {
               input.value = value.slice(0, -1); // Remove last character
           }
       }




       // Event handler for ddlWeight change
       // Event handler for ddlWeight change
       // Event handler for ddlWeight change
       $(document).on('change', '.ddl-weight', function () {
           calculateTotalWeight();
       });

       // Function to calculate the total weight
       function calculateTotalWeight() {
           let totalWeight = 0;

           $('.ddl-weight').each(function () {
               totalWeight += parseInt($(this).val()) || 0;
           });

           $('#totalExpectedNumber').text(totalWeight);
       }
       function calculateTotalWeightnn() {
           let totalWeight = 0;

           // Iterate over each weight dropdown and accumulate total
           $('.ddl-weight').each(function () {
               const weight = parseInt($(this).val(), 10); // Ensure base 10 parsing
               if (!isNaN(weight)) {
                   totalWeight += weight;
               }
           });

           // Update the total weight in the DOM
           $('#totalExpectedNumber').text(totalWeight);
       }
       function updateTotalWeightage() {

           let totalWeightage = 0;

           $('#gv_AppraisalFunc tr').each(function () {
               // Check if the row contains a nested table
               $(this).find('.nestedTableBody tr').each(function () {
                   // Get the Weightage value from each nested table row
                   var kpiWeight = parseFloat($(this).find('.txtWeightage').val()) || 0;
                   totalWeightage += kpiWeight;
               });
           });

           // Update the footer with the total Weightage
           $('#totalWeightage').text(totalWeightage.toFixed(2));
       }

       function updateSerialNumbers() {
           // Update serial numbers only for main table rows
           $('#gv_AppraisalFunc > tr').each(function (index) {
               $(this).find('.serial-number').text(index + 1);
           });
       }
        function appupdateSerialNumbers() {
           // Update serial numbers only for main table rows
           $('#gv_AppraisalFunc > tr').each(function (index) {
               $(this).find('.serial-number').text(index + 1);
           });
       }

       function saveFunction() {
           // Confirm the action
           if (!confirm('Are you sure you want to Submit?')) {
               return false; // Prevents the default action if the user cancels
           }

           // Get the values from the fields
           var comments = document.getElementById('commentsTextBox').value;
           var approvalStatus = document.querySelector('input[name="approvalStatus"]:checked');
           var approvalStatusValue = approvalStatus ? approvalStatus.value : '';

           // Prepare data for the AJAX call
           var data = {
               comments: comments,
               approvalStatus: approvalStatusValue
           };

           // Perform the AJAX call
           fetch('BSCSelfFunctionalApproval.aspx/SaveInfo', {
               method: 'POST',
               headers: {
                   'Content-Type': 'application/json',
                   'Accept': 'application/json'
               },
               body: JSON.stringify(data)
           })
               .then(response => response.json())
               .then(responseData => {
                   // Parse the 'd' property to get the actual object
                   var resultData = JSON.parse(responseData.d);

                   if (resultData.result) {
                       // If the result is true, show a success message and redirect
                       alert('Submission successful: ');
                       window.location.href = 'BSCKPIApproval.aspx'; // Replace with your desired redirect URL
                   } else {
                       // If the result is false, show an error message
                       alert('Submission failed: ' + resultData.message);
                   }
               })
               .catch((error) => {
                   console.error('Error:', error);
                   alert('An unexpected error occurred.');
               });

           // Return false to prevent default link action if necessary
           return false;
       }
   </script>
</asp:Content>


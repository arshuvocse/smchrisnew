<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="ContractualEmpApproval.aspx.cs" Inherits="ContractualEmployeeManagement_UI_ContractualEmpApproval" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

 	


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">


    <style type="text/css">
        /*AutoComplete flyout */
        .chkChoiceHeader label {
            padding-left: 2px;
            padding-right: 10px;
            font-size: 13px;
            font-weight: bold;
        }


        #cpFormBody_AppLogCommentGridView td {
            border: 1px solid #ddd !important;
            padding: 8px;
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


    <div class="content" id="content">
    

                <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                        <div class="icon">
                            <img src="../Report_Pages/app.png" width="20px" />
                        </div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Employee State Change Approval</h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                         <span class="alert alert-success"><span style="font-weight: bold">Next Approver:</span>  <asp:Label  ID="lblNextApp" runat="server"></asp:Label></span>
                        <asp:Button ID="homeButton" Visible="True" Text="Home" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="homeButton_OnClick" />
                        <asp:Button ID="detailsViewButton" Text="&#8920; Back to List" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>

                </div>
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                           
                        <div class="card-body">
                        
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>

                            <div class="row">


                                <div class="col-md-4">
                                </div>



                                <div class="col-md-1.5">
                                    <label style="font-weight: bold">Approval Status:&nbsp;<span style="color: #a52a2a">*</span></label>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">

                                        <asp:RadioButtonList ID="actionRadioButtonList" CssClass="chkChoiceHeader" runat="server" RepeatDirection="Horizontal">
                                        </asp:RadioButtonList>
                                    </div>
                                </div>

                            </div>



                            <div class="row">
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label style="font-weight: bold">Comments</label>
                                        <asp:TextBox runat="server" ID="commentsTextBox" CssClass="form-control" ReadOnly="False" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>

                            </div>


                            <div class="row">
                                <div class="col-md-5">

                                    <div runat="server" visible="False">
                                        <asp:HiddenField runat="server" ID="hfToProject" />
                                        <asp:HiddenField runat="server" ID="hfFromProject" />
                                        <asp:HiddenField runat="server" ID="HFEmpTypeID" />
                                        
                                        <asp:GridView ID="loadGridView" Visible="False" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-bordered  text-center thead-dark" DataKeyNames="EmpInfoId">
                                            <Columns>


                                                <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />



                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="col-4 ">
                                    <div class="form-group">
                                        <link href="../UserSetup/ButtonGrups.css" rel="stylesheet" />
                                        <div class="ui-group-buttons">
                                            <asp:LinkButton ID="submitButton" Text="Submit" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="Button2_OnClick" OnClientClick="return confirm('Are you sure you want to Submit ?')" Style="box-shadow: 0 0 3px 1px rgba(0,0,0,.35);"><i class="fa fa-check" aria-hidden="true"></i> &nbsp; Submit&nbsp; </asp:LinkButton>
                                            <asp:LinkButton ID="Button1" Text="Submit" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="Button1_OnClick" OnClientClick="return confirm('Are you sure you want to Submit ?')" Style="box-shadow: 0 0 3px 1px rgba(0,0,0,.35);"><i class="fa fa-check" aria-hidden="true"></i> &nbsp; Submit

&nbsp; </asp:LinkButton>
                                            <div class="or or-sm" Visible="False" runat="server" id="orBTN"></div>
                                            <asp:LinkButton  Visible="False"  ID="Button2a" Text="Cancel" runat="server" OnClick="Button2a_OnClick" OnClientClick="return confirm('Are you sure you want to Reject ?')" CssClass="btn btn-sm btn-danger"><i class="fa fa-times" aria-hidden="true"></i>
&nbsp; Reject </asp:LinkButton>
                                        </div>
                                        <%--<asp:Button ID="submitButton" Text="Save" CssClass="btn btn-sm btn-info" Visible="False" runat="server" OnClick="submitButton_Click" />--%>
                                        <asp:HiddenField ID="mainEmpId" runat="server" />
                                        <asp:HiddenField ID="entryempinfoIdHiddenField" runat="server" />
                                        <asp:HiddenField ID="actionstatusHiddenField" runat="server" />
                                        <%--<asp:Button ID="editButton" Text="Update" CssClass="btn btn-sm btn-success" Visible="False" runat="server" OnClick="editButton_OnClick" />
                                        <asp:Button ID="delButton" Text="Delete" CssClass="btn btn-sm btn-danger" Visible="False" runat="server" OnClick="delButton_OnClick" />
                                        <asp:Button ID="cancelButton" Text="Cancel" CssClass="btn btn-sm warning" runat="server" OnClick="cancelButton_OnClick" BackColor="#FFCC00" />--%>
                                    </div>


                                </div>
                            </div>

                            <div class="row"  runat="server" id="divApp01">
                                <div class="col-md-12">
                                    <table class="table table-bordered table-striped">
                                        <tr>
                                            <td style="width: 10%; padding: 10px;">Company Name</td>
                                            <td>
                                                <asp:Label runat="server" ID="lblCompany"></asp:Label></td>

                                        </tr>








                                        <tr>
                                            <td style="width: 10%; padding: 10px;">Employee Information</td>
                                            <td>

                                                <table class="table table-bordered table-striped">
                                                    <tr>
                                                        <td style="width: 20%; padding: 10px;">Employee ID</td>
                                                        <td>
                                                            <asp:Label ID="lblEmployeeCode" runat="server" Text=""></asp:Label></td>
                                                        <td style="width: 20%; padding: 10px;">Division</td>
                                                        <td>
                                                            <asp:Label ID="lblDivision" runat="server" Text=""></asp:Label></td>
                                                    </tr>


                                                    <tr>
                                                        <td style="width: 20%; padding: 10px;">Employee Name</td>
                                                        <td>

                                                            <asp:Label ID="lblEmp" runat="server" Text=""></asp:Label>
                                                            <asp:Label ID="EmployeeNameTextBox" Visible="False" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 20%; padding: 10px;">Wing</td>
                                                        <td>
                                                            <asp:Label ID="lblWing" runat="server" Text=""></asp:Label></td>
                                                    </tr>

                                                    <tr>
                                                        <td style="width: 20%; padding: 10px;">Date Of Joining</td>
                                                        <td>
                                                            <asp:Label ID="lblJdate" runat="server" Text=""></asp:Label></td>
                                                        <td style="width: 20%; padding: 10px;">Department</td>
                                                        <td>
                                                            <asp:Label ID="lblDepartment" runat="server"></asp:Label></td>
                                                    </tr>


                                                    <tr>
                                                       
                                                        <td style="width: 20%; padding: 10px;">Section</td>
                                                        <td>
                                                            <asp:Label ID="lblSection" runat="server" Text=""></asp:Label></td>
                                                        
                                                           <td style="width: 20%; padding: 10px;">Sub-Section</td>
                                                        <td>
                                                            <asp:Label ID="lblSubSection" runat="server" Text=""></asp:Label></td>
                                                    </tr>


                                                    <tr>
                                                         <td style="width: 20%; padding: 10px;">Designation</td>
                                                        <td>
                                                            <asp:Label ID="lblDesignation" runat="server" Text=""></asp:Label></td>
                                                        <td style="width: 20%; padding: 10px;">Designation Type</td>
                                                        <td>
                                                            <asp:Label ID="lblDesignationType" runat="server" Text=""></asp:Label></td>
                                                     
                                                    </tr>
                                                    
                                                    
                                                        <tr>
                                                        <td style="width: 20%; padding: 10px;">Salary Grade</td>
                                                        <td>
                                                            <asp:Label ID="lblSalaryGrade" runat="server" Text=""></asp:Label></td>
                                                        <td style="width: 20%; padding: 10px;">Salary Step</td>
                                                        <td>
                                                            <asp:Label ID="lblSalaryStep" runat="server" Text=""></asp:Label></td>
                                                    </tr>
                                                    
                                                    
                                                    <tr>
                                                        <td style="width: 20%; padding: 10px;">Office</td>
                                                        <td>
                                                            <asp:Label ID="lblOffice" runat="server" Text=""></asp:Label></td>
                                                        <td style="width: 20%; padding: 10px;">Place</td>
                                                        <td>
                                                            <asp:Label ID="lblPlace" runat="server" Text=""></asp:Label></td>
                                                    </tr>

                                                </table>

                                        </tr>




                                        <tr>

                                            <td style="width: 10%; padding: 10px;">
                                                <label>Employee State Change</label></td>

                                            <td style="width: 20%; padding: 10px;">

                                                <p>
                                                    <asp:Label ID="lblExtention" runat="server" Text=""></asp:Label>





                                                </p>

                                                <asp:Panel runat="server" ID="PtblExtension" Visible="False">
                                                    <table class="table table-bordered table-striped">
                                                        <tr>

                                                            <td style="width: 20%; padding: 10px;">Extension Start Date</td>

                                                            <td>
                                                                <asp:Label runat="server" ID="lblExtensionFromDate"></asp:Label>
                                                            </td>



                                                            <td style="width: 20%; padding: 10px;">Extension End Date</td>

                                                            <td>
                                                                <asp:Label runat="server" ID="lblExtensionToDate"></asp:Label>
                                                            </td>



                                                            <td style="width: 20%; padding: 10px;">Contract Preiod</td>

                                                            <td>
                                                                <asp:Label runat="server" ID="lblExtensionContractPreiod"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>

                                                </asp:Panel>



                                                <asp:Panel runat="server" ID="PtblRenew" Visible="False">
                                                    <table class="table table-bordered table-striped">
                                                        <tr>

                                                            <td style="width: 20%; padding: 10px;">Renew Start Date</td>

                                                            <td>
                                                                <asp:Label runat="server" ID="lblRenewStartDate"></asp:Label>
                                                            </td>



                                                            <td style="width: 20%; padding: 10px;">Renew End Date</td>

                                                            <td>
                                                                <asp:Label runat="server" ID="lblRenewEndDate"></asp:Label>
                                                            </td>



                                                            <td style="width: 20%; padding: 10px;">Contract Preiod</td>

                                                            <td>
                                                                <asp:Label runat="server" ID="lblRenewContractPreiod"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>

                                                </asp:Panel>




                                                <asp:Panel runat="server" ID="PtblPermanentToContractual" Visible="False">
                                                    <table class="table table-bordered table-striped">
                                                        <tr>

                                                            <td style="width: 20%; padding: 10px;">Permanent to Contractual Start Date</td>

                                                            <td>
                                                                <asp:Label runat="server" ID="lblPermanentToContractualStartDate"></asp:Label>
                                                            </td>



                                                            <td style="width: 20%; padding: 10px;">Permanent to Contractual End Date</td>

                                                            <td>
                                                                <asp:Label runat="server" ID="lblPermanentToContractualEndDate"></asp:Label>
                                                            </td>



                                                            <td style="width: 20%; padding: 10px;">Contract Preiod</td>

                                                            <td>
                                                                <asp:Label runat="server" ID="lblPertoConPreiod"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>

                                                </asp:Panel>





                                                <asp:Panel runat="server" ID="PtblContractualToPermanent" Visible="False">
                                                    <table class="table table-bordered table-striped">
                                                        <tr>

                                                            <td style="width: 20%; padding: 10px;">Contractual to Permanent Effective Date</td>

                                                            <td>
                                                                <asp:Label runat="server" ID="lblContractualToPermanentDateTextBox"></asp:Label>
                                                            </td>







                                                            <td colspan="2" style="width: 20%; padding: 10px;">Contract Preiod</td>

                                                            <td>
                                                                <asp:Label runat="server" ID="lblConToPerPreiod"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>

                                                </asp:Panel>
                                            </td>






                                        </tr>

                                        <tr runat="server" Visible="False">
                                            <td style="width: 10%; padding: 10px;">Increment Info </td>
                                            <td>
                                                <asp:Label runat="server" ID="lblIncrementInfo"></asp:Label></td>

                                        </tr>

                                        <tr runat="server" Visible="False">
                                            <td style="width: 10%; padding: 10px;">Facility Info</td>
                                            <td>
                                                <asp:Label runat="server" ID="lblFacilityInfo"></asp:Label></td>

                                        </tr>


                                        <tr>
                                            <td style="width:10%; padding: 10px;">Remarks</td>
                                            <td style="word-wrap: anywhere">
                                                <span  runat="server" ID="lblRemarks"></span></td>

                                        </tr>
                                        
                                         
                                        
                                         <tr  runat="server" Visible="False" id="trSeparate">
                                            <td style="width: 10%; padding: 10px;">   Separate
                                                             </td>
                                             
                                             <td>
                                             <table class="table table-bordered table-striped">
                                        <tr>
                                            <td  style="width:20%; padding: 10px;">
                                                 
                                       Job Left Type
                                                 </td>
                                            <td>
                                        <asp:Label runat="server" ID="tr_JobLeftType"></asp:Label> 
                                       </td>
                                     
                                              <td  style="width:20%; padding: 10px;">   
                                                
                                         Separation Date</td> <td>
                                        <asp:Label runat="server" ID="tr_SeparationDate"></asp:Label>
                                      </td>
                                  
                                           

                                        </tr></table>
                                                 
                                                 </td>
                                        </tr>
                                        
                                        
                                         <tr runat="server" Visible="False" id="trOrganization">
                                            <td style="width: 10%; padding: 10px;">    Organization Hierarchy  
                                                             </td>
                                             
                                               <td>
                                             <table class="table table-bordered table-striped">
                                        <tr>
                                            <td  style="width:20%; padding: 10px;">
                                                 
                                      Division
                                                 </td>
                                            <td>
                                         <asp:Label runat="server" ID="tr_Division"></asp:Label>
                                                            
                                       </td>
                                     
                                              <td  style="width:20%; padding: 10px;">   
                                                
                                         Wing</td> <td>
                                              <asp:Label runat="server" ID="tr_Wing"></asp:Label>

                                      </td>
                                  
                                           

                                        </tr>
                                                 
                                                <tr>
                                            <td  style="width:20%; padding: 10px;">
                                                 
                                      Department
                                                 </td>
                                            <td>
                                     <asp:Label runat="server" ID="tr_Department"></asp:Label>                     
                                       </td>
                                     
                                              <td  style="width:20%; padding: 10px;">   
                                                
                                         Section</td> <td>
                                            <asp:Label runat="server" ID="tr_Section"></asp:Label>

                                      </td>
                                  
                                           

                                        </tr>  
                                                 
                                                    <tr>
                                            <td  style="width:20%; padding: 10px;">
                                                 
                                      Sub-Section
                                                 </td>
                                            <td>
                                    <asp:Label runat="server" ID="tr_SubSection"></asp:Label>
                                                                          
                                       </td>
                                     
                                              <td  style="width:20%; padding: 10px;">   
                                                
                                          </td> <td>
                                            

                                      </td>
                                  
                                           

                                        </tr>    

                                             </table>
                                                 
                                                 </td>
                                           

                                        </tr>
                                        
                                        
                                           <tr runat="server"  Visible="False" id="trSalary">
                                            <td style="width: 10%; padding: 10px;">  Salary 
                                                             </td>
                                               

                                           
                                                
                                                 <td>
                                             <table class="table table-bordered table-striped">
                                        <tr>
                                            <td  style="width:20%; padding: 10px;">
                                                 
                                      Salary Grade
                                                 </td>
                                            <td>
                                                                                                     <asp:Label runat="server" ID="tr_SalaryGrade"></asp:Label>
 
                                       </td>
                                     
                                              <td  style="width:20%; padding: 10px;">   
                                                
                                Salary Step</td> <td>
                                                                                                   <asp:Label runat="server" ID="tr_SalaryStep"></asp:Label>

                                      </td>
                                  
                                           

                                        </tr></table>
                                                 
                                                 </td>
                                               
                                      

                                        </tr>
                                        
                                        
                                        
                                          <tr runat="server"  Visible="False" id="trPlace">
                                            <td style="width: 10%; padding: 10px;">   Place 
                                                             </td>
                                            <td>
                                                 <table class="table table-bordered table-striped">
                                        <tr>
                                            <td  style="width:20%; padding: 10px;">
                                                 
                                      Office
                                                 </td>
                                            <td>     <asp:Label runat="server" ID="tr_SalaryLocation"></asp:Label>
                                                        
 
                                       </td>
                                     
                                              <td  style="width:20%; padding: 10px;">   
                                                
                               Place</td> <td>
                                                                                                 <asp:Label runat="server" ID="tr_Place"></asp:Label>
                                                        

                                      </td>
                                  
                                           

                                        </tr>
                                                      <tr>
                                            <td  style="width:20%; padding: 10px;">
                                                 
                                      Floor
                                                 </td>
                                            <td>       <asp:Label runat="server" ID="tr_Floor"></asp:Label>
                                                        
                                                        
 
                                       </td>
                                     
                                              <td  style="width:20%; padding: 10px;">   
                                                
                                </td> <td>
                                                                                                 

                                      </td>
                                  
                                           

                                        </tr> 

                                                 </table>
                                             
                                    
                                            </td>

                                        </tr>
                                        
                                        
                                         <tr runat="server"  Visible="False" id="trDesignation">
                                            <td style="width: 10%; padding: 10px;">  Designation
                                                             </td>
                                            <td>
                                                  <table class="table table-bordered table-striped">
                                        <tr>
                                            <td  style="width:20%; padding: 10px;">
                                                 
                                    Designation
                                                 </td>
                                            <td>
                                                                                                     <asp:Label runat="server" ID="tr_Designation"></asp:Label>
 
                                       </td>
                                     
                                              <td  style="width:20%; padding: 10px;">   
                                                
                          Designation Type</td> <td>
                                                                                           <asp:Label runat="server" ID="tr_DesignationType"></asp:Label>
                                                          

                                      </td>
                                  
                                           

                                        </tr></table>
                                   
                                            </td>

                                        </tr>
                                    </table>


                                    <div runat="server" visible="False">

                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Company Name </label>
                                                    <asp:DropDownList ID="companyDropDownList" class="form-control form-control-sm" OnSelectedIndexChanged="companyDropDownList_OnSelectedIndexChanged" runat="server" AutoPostBack="True"></asp:DropDownList>
                                                </div>

                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Search Employee: </label>
                                                    <asp:TextBox ID="SearchEmployeeNameTextBoxTextBox" AutoPostBack="True" runat="server" class="form-control form-control-sm" OnTextChanged="SearchEmployeeNameTextBoxTextBox_OnTextChanged"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                        EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                        ServiceMethod="GetCompanyWiseEmployeeInfo" ServicePath="~/WebService.asmx" TargetControlID="SearchEmployeeNameTextBoxTextBox"
                                                        UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                        ShowOnlyCurrentWordInCompletionListItem="true">
                                                    </cc1:AutoCompleteExtender>

                                                    <asp:HiddenField ID="repEmpIdHiddenField" runat="server" />

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
                                                    <asp:HiddenField ID="jobIdHiddenField" runat="server" />
                                                </div>
                                            </div>

                                        </div>

                                        <asp:Panel runat="server" ID="ShowPanel" Visible="False">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <fieldset class="for-panel">
                                                        <legend>Basic Information </legend>
                                                        <div class="row">

                                                            <div class="col-md-12">



                                                                <div class="form-group" style="display: none">

                                                                    <label>Company Name: </label>
                                                                    <asp:Label ID="lblComName" runat="server" Text=""></asp:Label>

                                                                </div>

                                                                <div class="form-group">

                                                                    <label>Employee Name: </label>


                                                                </div>
                                                                <div class="form-group">
                                                                    <label>Employee Code: </label>

                                                                </div>

                                                                <div class="form-group">
                                                                    <label>Joining Date: </label>

                                                                </div>

                                                                <div class="form-group">
                                                                    <label>Designation: </label>

                                                                </div>


                                                                <div class="form-group">
                                                                    <label>Salary Grade: </label>

                                                                </div>



                                                            </div>

                                                        </div>
                                                    </fieldset>
                                                </div>



                                                <div class="col-md-6">
                                                    <fieldset class="for-panel">
                                                        <legend>Functional Structure</legend>
                                                        <div class="row">

                                                            <div class="col-md-12">

                                                                <div class="form-group">

                                                                    <label>Division: </label>


                                                                </div>
                                                                <div class="form-group">
                                                                    <label>Wing: </label>

                                                                </div>

                                                                <div class="form-group">
                                                                    <label>Department: </label>

                                                                </div>

                                                                <div class="form-group">
                                                                    <label>Section: </label>

                                                                </div>


                                                                <div class="form-group">
                                                                    <label>Sub-Section: </label>

                                                                </div>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                            </div>

                                        </asp:Panel>
                                    </div>
                                    
                                    
                                 </div>
                                 </div>

                                    <div class="row"  runat="server" id="divApp02">

                                        <div class="col-md-12">

                                            <div runat="server">

                                                <div class="row">
                                                    <asp:HiddenField runat="server" ID="ContractualEmpManageIdHiddenField" />
                                                  
                                                </div>

                                              
                                                
                                              
                                            </div>
                                            


                                            <div   runat="server"  >

                                                <div class="row"  runat="server" id="slaShoe">
                                                    <div class="col-md-4" runat="server" Visible="False">
                                                        <div class="form-group">
                                                            <asp:RadioButtonList ID="SalaryIncrementRadioButtonList1" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem>&nbsp;  Salary Increment &nbsp; &nbsp;</asp:ListItem>
                                                                <asp:ListItem>&nbsp;  No Increment &nbsp; &nbsp;</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                </div>
                                                


                                                <div class="row"   runat="server" id="fASSHOE" >
                                                    <div class="col-md-4" runat="server" Visible="False">
                                                        <div class="form-group">
                                                            <asp:RadioButtonList ID="FacilityRadioButtonList" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem>&nbsp;  Facility Included &nbsp; &nbsp;</asp:ListItem>
                                                                <asp:ListItem> &nbsp;  No Facility &nbsp; &nbsp;</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="display: none">
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>Remarks: </label>
                                                            <asp:TextBox ID="RemarksTextBox" Rows="2" TextMode="MultiLine" runat="server" class="form-control"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                            <div class="row" id="divForm"  runat="server" > 
                                                
                                                  
                                <div class="col-md-12">
                                    <table class="table table-bordered table-striped">
                                        
                                         <tr  runat="server" id="divEffectivedate">
                                            <td style="width: 30%; padding: 10px;"> <span class="SelectchkChoiceDsss">Effective Date</span>  &nbsp;<label style="color: #a52a2a">*</label>
                                                             </td>
                                            <td>
                                                        <asp:TextBox ID="txtEffectiveDate" AutoCompleteType="Disabled" runat="server"   CausesValidation="true" class="form-control form-control-sm col-md-3"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                        TargetControlID="txtEffectiveDate"
                                                        Format="dd MMM yyyy" CssClass="MyCalendar" PopupPosition="TopLeft" />
                                            </td>

                                        </tr>
                                        
                                          <tr  id="rdshoe" runat="server" >
                                            <td style="width: 30%; padding: 10px;"> <span class="SelectchkChoiceDsss"> Employee State Change Options </span> &nbsp;<label style="color: #a52a2a">*</label>
                                                             </td>
                                            <td>
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <asp:RadioButtonList   ID="ExtentionRenewRadioButtonList" AutoPostBack="True"  runat="server" OnSelectedIndexChanged="ExtentionRenewRadioButtonList_SelectedIndexChanged" RepeatColumns="7" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                          <%--      <asp:ListItem>&nbsp; &nbsp;Extension&nbsp; &nbsp;</asp:ListItem>
                                                                <asp:ListItem>&nbsp; &nbsp;Renew&nbsp; &nbsp;</asp:ListItem>
                                                                <asp:ListItem>&nbsp; &nbsp;Permanent to Contractual&nbsp; &nbsp;</asp:ListItem>
                                                                <asp:ListItem>&nbsp; &nbsp;Contractual to Permanent&nbsp; &nbsp;</asp:ListItem>
                                                                <asp:ListItem>&nbsp; &nbsp;Project Change&nbsp; &nbsp;</asp:ListItem>--%>
                                                                
                                                                
                                                                <asp:ListItem>&nbsp; &nbsp;Extension&nbsp; &nbsp;</asp:ListItem>
                                                                <asp:ListItem>&nbsp; &nbsp;Renew&nbsp; &nbsp;</asp:ListItem>
                                                                <asp:ListItem>&nbsp; &nbsp;Permanent to Contractual&nbsp; &nbsp;</asp:ListItem>
                                                                <asp:ListItem>&nbsp; &nbsp;Contractual to Permanent&nbsp; &nbsp;</asp:ListItem>
                                                                <asp:ListItem >&nbsp; &nbsp;SMC Funded Projects to SMC Contract&nbsp; &nbsp;</asp:ListItem>
                                                    
                                                                <asp:ListItem >&nbsp; &nbsp;SMC Contract to SMC Funded Projects&nbsp; &nbsp;</asp:ListItem>
                                                                <asp:ListItem>&nbsp; &nbsp;Project Change&nbsp; &nbsp;</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                        <div class="col-md-4" runat="server" id="divReappointment" visible="False">
                                                            <div class="form-group" runat="server" Visible="False">
                                                                <asp:CheckBox runat="server" ID="chkReappointment" Text="Emp. Information Update" AutoPostBack="True" OnCheckedChanged="chkReappointment_OnCheckedChanged" />
                                                            </div>
                                                        </div>
                                                    </div> 
                                                
                                                
                                                  <div class="row"   runat="server"  >
                                                    <div class="col-md-7">
                                                        <asp:Panel runat="server" ID="ExtensionPanelView" Visible="False">
                                                            <div class="row"  runat="server" >

                                                                <div class="col-md-6">
                                                                    <div class="form-group">
                                                                        <label>Extension From Date: </label> &nbsp;<label style="color: #a52a2a">*</label>
                                                                        <asp:TextBox ID="ExtensionFromDateTextBox" AutoCompleteType="Disabled" runat="server" OnTextChanged="ExtensionFromDateTextBox_TextChanged"   AutoPostBack="True" CausesValidation="true" class="form-control form-control-sm"></asp:TextBox>
                                                                        <ajaxToolkit:CalendarExtender ID="Calendar1" runat="server"
                                                                            TargetControlID="ExtensionFromDateTextBox"
                                                                            Format="dd MMM yyyy" CssClass="MyCalendar" PopupPosition="TopLeft" />
                                                                        <style>
                                                      
                                                        </style>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group">
                                                                        <label>Extension To Date: </label> &nbsp;<label style="color: #a52a2a">*</label>
                                                                        <asp:TextBox ID="ExtensionToDateTextBox"   AutoPostBack="True" AutoCompleteType="Disabled" runat="server" OnTextChanged="ExtensionToDateTextBox_TextChanged" CausesValidation="true" class="form-control form-control-sm"></asp:TextBox>
                                                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupPosition="TopLeft"
                                                                            Format="dd-MMM-yyyy" PopupButtonID="ExtensionToDateTextBox" CssClass="MyCalendar"
                                                                            TargetControlID="ExtensionToDateTextBox" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>

                                                        <asp:Panel runat="server" ID="RenewPanelView" Visible="False">
                                                            <div class="row">

                                                                <div class="col-md-6">
                                                                    <div class="form-group">
                                                                        <label>Renew Start Date: </label>  &nbsp;<label style="color: #a52a2a">*</label>
                                                                        <asp:TextBox ID="RenewStartDateTextBox"  AutoCompleteType="Disabled" runat="server" OnTextChanged="RenewStartDateTextBox_TextChanged"  AutoPostBack="True" CausesValidation="true" class="form-control form-control-sm"></asp:TextBox>
                                                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" PopupPosition="TopLeft"
                                                                            Format="dd-MMM-yyyy" PopupButtonID="RenewStartDateTextBox" CssClass="MyCalendar"
                                                                            TargetControlID="RenewStartDateTextBox" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group">
                                                                        <label>Renew To Date: </label>  &nbsp;<label style="color: #a52a2a">*</label>
                                                                        <asp:TextBox ID="RenewToDateTextBox" AutoCompleteType="Disabled" runat="server" OnTextChanged="RenewToDateTextBox_TextChanged"     AutoPostBack="True" CausesValidation="true" class="form-control form-control-sm"></asp:TextBox>
                                                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" PopupPosition="TopLeft"
                                                                            Format="dd-MMM-yyyy" PopupButtonID="RenewToDateTextBox" CssClass="MyCalendar"
                                                                            TargetControlID="RenewToDateTextBox" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>


                                                        <asp:Panel runat="server" ID="PermanentToContractualPanelView" Visible="False">
                                                            <div class="row">

                                                                <div class="col-md-6">
                                                                    <div class="form-group">
                                                                        <label>Start Date: </label>
                                                                        &nbsp;<label style="color: #a52a2a">*</label>
                                                                        <asp:TextBox ID="PermanentToContractualEffectiveDaeTextBox" AutoCompleteType="Disabled" runat="server" OnTextChanged="PermanentToContractualEffectiveDaeTextBox_TextChanged"   AutoPostBack="True" CausesValidation="true" class="form-control form-control-sm"></asp:TextBox>
                                                                        <asp:CalendarExtender ID="CalendarExtender5" runat="server" PopupPosition="TopLeft"
                                                                            Format="dd-MMM-yyyy" PopupButtonID="PermanentToContractualEffectiveDaeTextBox" CssClass="MyCalendar"
                                                                            TargetControlID="PermanentToContractualEffectiveDaeTextBox" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group">
                                                                        <label>End Date: </label>
                                                                        &nbsp;<label style="color: #a52a2a">*</label>
                                                                        <asp:TextBox ID="PermanentToContractualEndDateTextBox"   AutoPostBack="True" AutoCompleteType="Disabled" runat="server" OnTextChanged="PermanentToContractualEffectiveDaeTextBox_TextChanged" CausesValidation="true" class="form-control form-control-sm"></asp:TextBox>
                                                                        <asp:CalendarExtender ID="CalendarExtender7" runat="server" PopupPosition="TopLeft"
                                                                            Format="dd-MMM-yyyy" PopupButtonID="PermanentToContractualEndDateTextBox" CssClass="MyCalendar"
                                                                            TargetControlID="PermanentToContractualEndDateTextBox" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                        
                                                        
                                                          <asp:Panel runat="server" ID="rbOther" Visible="False">
                                        <div class="row">
                                            <asp:HiddenField runat="server" ID="hfIsProgramContractualOP"/>
                                            <asp:HiddenField runat="server" ID="hfIsSMCFundedProjects"/>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Project Name: </label> &nbsp;<label style="color: #a52a2a">*</label>
                                                  <asp:DropDownList runat="server"  ID="ddlProject"  class="form-control form-control-sm">
                                                      <asp:ListItem Value="0">Select One.....</asp:ListItem>
                                                      <asp:ListItem Value="1">SMC Funded Projects</asp:ListItem>
                                                      <asp:ListItem Value="2">SMC Contract</asp:ListItem>
                                                      <asp:ListItem Value="3">Other Projects</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            
                                        </div>
                                    </asp:Panel>
                                                        <%--    <asp:Panel runat="server" ID="PermanentToContractualPanelView" Visible="False">
                                        <div class="row">

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Effective Date: </label>
                                                    <asp:TextBox ID="PermanentToContractualEffectiveDaeTextBox" AutoCompleteType="Disabled" runat="server" OnTextChanged="PermanentToContractualEffectiveDaeTextBox_TextChanged" CausesValidation="true" class="form-control form-control-sm"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender5" runat="server" PopupPosition="TopLeft"
                                                        Format="dd-MMM-yyyy" PopupButtonID="PermanentToContractualEffectiveDaeTextBox" CssClass="MyCalendar"
                                                        TargetControlID="PermanentToContractualEffectiveDaeTextBox" />
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                            </div>
                                        </div>
                                    </asp:Panel>--%>


                                                        <asp:Panel runat="server" ID="ContractualToPermanentPanelView" Visible="False">
                                                            <div class="row" style="display: none">
                                                                <div class="col-md-3">
                                                                    <div class="form-group">
                                                                        <label>Effective Date: </label>
                                                                        <asp:TextBox ID="ContractualToPermanentDateTextBox"   AutoCompleteType="Disabled" runat="server" OnTextChanged="ContractualToPermanentTextBox_TextChanged"   AutoPostBack="True" CausesValidation="true" class="form-control form-control-sm"></asp:TextBox>
                                                                        <asp:CalendarExtender ID="CalendarExtender6" runat="server" PopupPosition="TopLeft"
                                                                            Format="dd-MMM-yyyy" PopupButtonID="ContractualToPermanentDateTextBox" CssClass="MyCalendar"
                                                                            TargetControlID="ContractualToPermanentDateTextBox" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-3">
                                                                </div>
                                                            </div>
                                                        </asp:Panel>

                                                    </div>
                                                    <div class="col-md-5" runat="server" ID="ContractPreiod" Visible="False">
                                                        <div class="form-group" >

                                                            <label>Contract Preiod</label>
                                                            <asp:TextBox runat="server" ID="txtContractualPreiod" ReadOnly="True" CssClass="form-control form-control-sm" />


                                                        </div>
                                                    </div>
                                                </div>      
                                            </td>

                                        </tr>

                                        <tr>
                                            <td style="width: 30%; padding: 10px;">  <asp:CheckBox runat="server" ID="chkIsSeparation"  CssClass="SelectchkChoiceDsss" AutoPostBack="True"  Text="Do you want to Separate?" OnCheckedChanged="chkIsSeparation_OnCheckedChanged" />
                                                             </td>
                                            <td>
                                                <div class="form-row" runat="server" Visible="False" id="divSeparate">
                                                 <div class="col-md-4" runat="server" Visible="True" id="septype">
                                    <div class="form-group">
                                        <label>Job Left Type: </label>
                                        <span style="color: red">&nbsp;*</span>
                                        <asp:DropDownList ID="JobLeftTypeDropDownList" OnSelectedIndexChanged="JobLeftTypeDropDownList_OnSelectedIndexChanged" AutoPostBack="True"  class="form-control form-control-sm" runat="server"></asp:DropDownList>
                                             <asp:CheckBox ID="chkIsSubmissionDate" Visible="False" Text="Is Submission Date" CssClass="checkbox margin-right" runat="server" />
                                    </div>
                                </div>
                                                
                                                 <div class="col-md-4" runat="server" Visible="True" id="sepDate">
                                    <div class="form-group">
                                        <label>Separation Date: </label>
                                        <span style="color: red">&nbsp;*</span>
                                        <asp:TextBox ID="JobLeftDateTextBox" AutoCompleteType="Disabled" runat="server" AutoPostBack="True" OnTextChanged="JobLeftDateTextBox_TextChanged" CausesValidation="true" class="form-control form-control-sm"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender8" runat="server"
                                            Format="dd-MMM-yyyy" CssClass="MyCalendar" PopupPosition="TopLeft"
                                            TargetControlID="JobLeftDateTextBox" />
                                    </div>
                                </div>
                                                    
                                                    </div>
                                            </td>

                                        </tr>
                                        
                                        
                                         <tr>
                                            <td style="width: 30%; padding: 10px;">  <asp:CheckBox runat="server" ID="chkOrganization"  CssClass="SelectchkChoiceDsss" AutoPostBack="True"  Text="Do you want to change Organization?" OnCheckedChanged="chkOrganization_OnCheckedChanged" />
                                                             </td>
                                            <td>
                                                
                                                <div class="form-row" runat="server" Visible="False" id="divOrganization">
                                                     <div class="col-3" runat="server" Visible="False">
                                                        <div class="form-group">
                                                            <label>Company</label>
                                                            <label style="color: #a52a2a">*</label>
                                                            <asp:DropDownList runat="server" Enabled="False" AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" CssClass="form-control form-control-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="col-4">
                                                        <div class="form-group">
                                                            <label>Division</label>
                                                            <label style="color: #a52a2a">*</label>
                                                            <br/>
                                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDivision" class="selectMee form-control form-control-sm" OnSelectedIndexChanged="ddlDivision_OnSelectedIndexChanged" />
                                                            
                                                        <script type="text/javascript">
                                                            function pageLoad() {

                                                                $('.selectMee').chosen({ disable_search_threshold: 5, search_contains: true });

                                                            }
</script>

                                                        </div>
                                                    </div>
                                                    <div class="col-4" runat="server" id="wing">
                                                        <div class="form-group">
                                                            <label>Wing</label>
                                                               <br/>
                                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlWing" class="selectMee form-control form-control-sm" OnSelectedIndexChanged="ddlWing_OnSelectedIndexChanged" />

                                                        </div>
                                                    </div>
                                                    <div class="col-4" runat="server" id="dept">
                                                        <div class="form-group">
                                                            <label>Department</label>
                                                            <label style="color: #a52a2a">*</label>
                                                               <br/>
                                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDepartment" class="selectMee form-control form-control-sm" OnSelectedIndexChanged="ddlDepartment_OnSelectedIndexChanged" />

                                                        </div>
                                                    </div>
                                                    <div class="col-4" runat="server" id="sec">
                                                        <div class="form-group">
                                                            <label>Section</label>
                                                               <br/>
                                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlSection" class="selectMee form-control form-control-sm" OnSelectedIndexChanged="ddlSection_OnSelectedIndexChanged" />

                                                        </div>
                                                    </div>
                                                    <div class="col-4" runat="server" id="subsec">
                                                        <div class="form-group">
                                                            <label>Sub Section</label>
                                                               <br/>
                                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlSubSection" class="selectMee form-control form-control-sm" OnSelectedIndexChanged="ddlSubSection_OnSelectedIndexChanged" />

                                                        </div>
                                                    </div>
                                      </div>
                                            </td>

                                        </tr>
                                        
                                        
                                           <tr>
                                            <td style="width: 30%; padding: 10px;">  <asp:CheckBox runat="server" ID="chkSalary"  CssClass="SelectchkChoiceDsss" AutoPostBack="True"  Text="Do you want to change Salary?" OnCheckedChanged="chkSalary_OnCheckedChanged" />
                                                             </td>
                                            <td>
                                               
                                      <div class="form-row" runat="server" Visible="False" id="divSalary">
                                             <div class="col-4">
                                                        <div class="form-group">
                                                            <label>Salary Grade</label><label style="color: #a52a2a">*</label>
                                                               <br/>
                                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlSalaryGrade" class="selectMee form-control form-control-sm" OnSelectedIndexChanged="ddlSalaryGrade_OnSelectedIndexChanged" />

                                                        </div>
                                                    </div>
                                                    <div class="col-4">
                                                        <div class="form-group">
                                                            <label>Salary Step</label><label style="color: #a52a2a">*</label>
                                                               <br/>
                                                            <asp:DropDownList runat="server"   ID="ddlSalaryStep" class="selectMee form-control form-control-sm" />

                                                        </div>
                                                    </div>
                                      </div>
                                            </td>

                                        </tr>
                                        
                                        
                                        
                                          <tr>
                                            <td style="width: 30%; padding: 10px;">  <asp:CheckBox runat="server" ID="chkPlace"  CssClass="SelectchkChoiceDsss" AutoPostBack="True"  Text="Do you want to change Place?" OnCheckedChanged="chkPlace_OnCheckedChanged" />
                                                             </td>
                                            <td>
                                               
                                      <div class="form-row" runat="server" Visible="False" id="divPlace">
                                          
                                              <div class="col-3">
                                                        <div class="form-group">
                                                            <label>Office</label><label style="color: #a52a2a">*</label>
                                                               <br/>
                                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlSalaryLocation" OnSelectedIndexChanged="ddlSalaryLocation_OnSelectedIndexChanged" class="selectMee form-control form-control-sm" />

                                                        </div>
                                                    </div>
                                                    <div class="col-3">
                                                        <div class="form-group">
                                                            <label>Place</label>
                                                            <br/>
                                                            <asp:DropDownList runat="server"  ID="ddlJobLocation" class="selectMee form-control form-control-sm" />

                                                        </div>
                                                    </div>
                                                    
                                                    
                                                     <div class="col-3">
                                                        <div class="form-group">
                                                            <label>Floor</label>
                                                            <asp:TextBox runat="server"   ID="txtFloor" class="form-control form-control-sm" />

                                                        </div>
                                                    </div>
                                      </div>
                                            </td>

                                        </tr>
                                        
                                        
                                         <tr>
                                            <td style="width: 30%; padding: 10px;">  <asp:CheckBox runat="server" ID="chkDesignation"  CssClass="SelectchkChoiceDsss" AutoPostBack="True"  Text="Do you want to change Designation?" OnCheckedChanged="chkDesignation_OnCheckedChanged" />
                                                             </td>
                                            <td>
                                               
                                      <div class="form-row" runat="server" Visible="False" id="divDesignation">
                                          
                                              <div class="col-3">
                                                        <div class="form-group">
                                                            <label>Designation</label><label style="color: #a52a2a">*</label>
                                                               <br/>
                                                          <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlDesignation"  CssClass="selectMee form-control form-control-sm" />

                                                        </div>
                                                    </div>
                                                    <div class="col-3">
                                                        <div class="form-group">
                                                           <label>Designation Type</label>
                                                               <br/>
                                                            <asp:DropDownList runat="server"   ID="ddlDesignationType" class="selectMee form-control form-control-sm" />

                                                        </div>
                                                    </div>
                                                    
                                                    
                                      </div>
                                            </td>

                                        </tr>
                                        </table>

                                                  
                                        
                                        
                                       
                                                <div class="form-row" runat="server" Visible="False">
                                              
                                                    <div class="col-3">
                                                        <div class="form-group">
                                                            <label>Employee Category</label>
                                                            <label style="color: #a52a2a">*</label>
                                                               <br/>
                                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlEmpCategory" class="selectMee form-control form-control-sm" OnSelectedIndexChanged="ddlEmpCategory_OnSelectedIndexChanged" />

                                                        </div>
                                                    </div>
                                                 
                                                    

                                                    <div class="col-3">
                                                        <div class="form-group">
                                                            <label><asp:CheckBox runat="server" ID="chkRedesignation" Text="Re-Designation" AutoPostBack="True" OnCheckedChanged="chkRedesignation_OnCheckedChanged"/></label><label style="color: #a52a2a">*</label>
                                                               <br/>
                                                          

                                                        </div>
                                                    </div>
                                                    <div class="col-3">
                                                        <div class="form-group">
                                                         
                                                        </div>
                                                    </div>
                                                
                                                   
                                                </div>

                                               
                                              
                                    </div>
                                            </div>
                                            <div runat="server" id="evgrid" Visible="False">
                                                
                                                
                                                <asp:HiddenField runat="server" ID="ReportingBoss"/>
                                                <asp:HiddenField runat="server" ID="hfGetPreviousMainId"/>
                                                <asp:HiddenField runat="server" ID="hfForwardMainId"/>
                                                <asp:HiddenField runat="server" ID="hfIsForward"/>
                                                <asp:HiddenField runat="server" ID="EmpId"/>
                                                <asp:HiddenField runat="server" ID="EffectiveDate"/>
                                                <div style="text-align: center;">
                                                    Key Rating Criterions to be evaluated
                                        <br />
                                                    <br />
                                                    (Please tick (√) your actual rating in the appropriate box)
                                    <br />
                                                    <br />
                                                </div>
                                                <asp:GridView Width="100%" ID="gv_ProbationEvaluation" runat="server"
                                                    AutoGenerateColumns="false" CssClass="table table-bordered text-center thead-dark">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL#">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                                <asp:HiddenField runat="server" ID="hdpkd" Value='<%#Eval("ValueField") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Key Rating Criterions">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txt_RatingCriterions" runat="server" Text='<%#Eval("TextField") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Rating Scale">
                                                            <ItemTemplate>
                                                                <asp:RadioButtonList runat="server" ID="rad_RatingScale" RepeatDirection="Horizontal" BorderStyle="None">
                                                                    <asp:ListItem Value="4" Text="Excellent" runat="server"></asp:ListItem>
                                                                    <asp:ListItem Value="3" Text="Good" runat="server"></asp:ListItem>
                                                                    <asp:ListItem Value="2" Text="Satisfactory" runat="server"></asp:ListItem>
                                                                    <asp:ListItem Value="1" Text="Not Satisfactory" runat="server"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            
                                         </div>
                                        
                                    </div>

 	 <fieldset class="for-panel" runat="server" Visible="False" id="DivMemo">
                                                <legend>Memo</legend>
                                                <div class="row">

                                <div class="col-md-12">
                                    
                                        <div class="form-row">
                                        
                                      
                                                                                
 <div class="col-md-2" style="padding: 10px;">
                                            
                                        </div>
                                        <div class="col-md-6"  style="padding: 10px;">
                                            <asp:Label ID="txtHeader1" ReadOnly="True" CssClass="form-control form-control-sm" runat="server" Text="Social Marketing Company"  ></asp:Label>
                                        </div>
                                    </div>
                                    
                                    
                                        <div class="form-row">
                                        
                                       <div class="col-md-2" style="padding: 10px;">
                                            
                                        </div>

                                        <div class="col-md-6"  style="padding: 10px;">
                                            <asp:Label ID="txtHeader2"  ReadOnly="True"  CssClass="form-control form-control-sm" runat="server" Text="Internal Memo"></asp:Label>
                                        </div>
                                    </div>
                                    
                                    
                                               <div class="form-row">
                                        
                                      
 <div class="col-md-2" style="padding: 10px;">
                                            <label>To: </label>
                                        </div>

                                        <div class="col-md-10">
                                            <asp:Label ID="txtTo"  ReadOnly="True"  CssClass="form-control" runat="server" Text="Annual Increment"></asp:Label>
                                        </div>
                                    </div>
                                    
                                    
                                        <div class="form-row">
                                        
                                      
 <div class="col-md-2" style="padding: 10px;">
                                            <label>From: </label>
                                        </div>

                                        <div class="col-md-10">
                                            <asp:Label ID="txtFrom"  CssClass="form-control form-control-sm" runat="server" Text="Annual Increment"></asp:Label>
                                        </div>
                                    </div>
                                    
                                    
                                    <div class="form-row">
                                        
                                        <div class="col-md-2" style="padding: 10px;">
                                            <label>Subject: </label>
                                        </div>

                                        <div class="col-md-10">
                                            <asp:TextBox ID="txtSubject" CssClass="form-control form-control-sm" runat="server" Text="Annual Increment"></asp:TextBox>
                                        </div>
                                    </div>

                                          <div class="form-row">
                                        
                                      
 <div class="col-md-2" style="padding: 10px;">
                                            <label>Date: </label>
                                        </div>

                                        <div class="col-md-10">
                                            <asp:Label ID="txtDate" CssClass="form-control form-control-sm" runat="server" ></asp:Label>
                                        </div>
                                    </div>

                                
 

                                    <div class="form-row">
                                        <div class="col-md-2" style="padding: 10px;">
                                            <label>Body of the letter: </label>
                                        </div>

                                        <div class="col-md-10">
                                            <asp:Label ID="txtBodyofletter" CssClass="form-control"   TextMode="MultiLine" placeholder="Write first paragraph" runat="server" Text="" Font-Size="12px" Rows="8"></asp:Label>
                                        </div>
                                    </div>
                                    <br/>
                                    
                                        <div class="form-row">
                                        <div class="col-md-2" style="padding: 10px;">
                                            
                                        </div>

                                        <div class="col-md-10">
                                            <asp:Label ID="txtGridviewBefore" CssClass="form-control"   TextMode="MultiLine" placeholder="Write first paragraph" runat="server" Text="" Font-Size="12px" Rows="2"></asp:Label>
                                        </div>
                                    </div>
                                    
                                         <br/>
                                    <div class="form-row">
                                        <div class="col-md-2" style="padding: 10px;">
                                            
                                        </div>

                                        <div class="col-md-10">
                                                           <table class="table table-bordered table-striped">
                                                               
                                                               
                                                               
                                                                <tr>
                                                        <td style="width: 10%; padding: 10px;">Employee ID</td>
                                                        <td>
                                                            <asp:Label ID="mem_EmployeeID" runat="server" Text=""></asp:Label></td>
                                              
                                               
                                                    </tr>
                                                               
                                                                   <tr>
                                                                       
                                                                        <td style="width: 10%; padding: 10px;">Employee Name</td>
                                                        <td>
                                                            <asp:Label ID="mem_EmployeeName" runat="server" Text=""></asp:Label></td>
                                                      
                                                                       </tr>

                                                                            <tr  runat="server" Visible="False" id="mem_Separate">
                                            <td style="width: 10%; padding: 10px;">   Separate
                                                             </td>
                                             
                                             <td>
                                             <table class="table table-bordered table-striped">
                                        <tr>
                                            <td  style="width:20%; padding: 10px;">
                                                 
                                       Job Left Type
                                                 </td>
                                            <td>
                                        <asp:Label runat="server" ID="mem_JobLeftType"></asp:Label> 
                                       </td>
                                     
                                              <td  style="width:20%; padding: 10px;">   
                                                
                                         Separation Date</td> <td>
                                        <asp:Label runat="server" ID="mem_SeparationDate"></asp:Label>
                                      </td>
                                  
                                           

                                        </tr></table>
                                                 
                                                 </td>
                                        </tr>
                                         <tr runat="server" Visible="False" id="mem_Organization">
                                            <td style="width: 10%; padding: 10px;">    Organization Hierarchy  
                                                             </td>
                                             
                                               <td>
                                             <table class="table table-bordered table-striped">
                                        <tr>
                                            
                                              <td  style="width:20%; padding: 10px;">
                                                 
                                    Present   Division:
                                                 </td>
                                            <td>
                                         <asp:Label runat="server" ID="Pmem_Division"></asp:Label>
                                                            
                                       </td>
                                            <td  style="width:20%; padding: 10px;">
                                                 
                                     Proposed  Division:
                                                 </td>
                                            <td>
                                         <asp:Label runat="server" ID="mem_Division"></asp:Label>
                                                            
                                       </td>
                                            
                                            <td  style="width:20%; padding: 10px;">   
                                                
                                     Present    Wing:</td> <td>
                                              <asp:Label runat="server" ID="Pmem_Wing"></asp:Label>

                                      </td>
                                  
                                     
                                              <td  style="width:20%; padding: 10px;">   
                                                
                                      Proposed   Wing:</td> <td>
                                              <asp:Label runat="server" ID="mem_Wing"></asp:Label>

                                      </td>
                                  
                                           

                                        </tr>
                                                 
                                                <tr>
                                                    
                                                      <td  style="width:20%; padding: 10px;">
                                                 
                                    Present  Department:
                                                 </td>
                                            <td>
                                     <asp:Label runat="server" ID="Pmem_Department"></asp:Label>                     
                                       </td>
                                            <td  style="width:20%; padding: 10px;">
                                                 
                                    Proposed  Department:
                                                 </td>
                                            <td>
                                     <asp:Label runat="server" ID="mem_Department"></asp:Label>                     
                                       </td>
                                        <td  style="width:20%; padding: 10px;">   
                                                
                                       Present  Section:</td> <td>
                                            <asp:Label runat="server" ID="Pmem_Section"></asp:Label>

                                      </td>
                                  
                                              <td  style="width:20%; padding: 10px;">   
                                                
                                     Proposed    Section:</td> <td>
                                            <asp:Label runat="server" ID="mem_Section"></asp:Label>

                                      </td>
                                  
                                           

                                        </tr>  
                                                 
                                                    <tr>
                                                        
                                                         <td  style="width:20%; padding: 10px;">
                                                 
                                     Present Sub-Section:
                                                 </td>
                                            <td>
                                    <asp:Label runat="server" ID="Pmem_SubSection"></asp:Label>
                                                                          
                                       </td>
                                            <td  style="width:20%; padding: 10px;">
                                                 
                                   Proposed   Sub-Section"
                                                 </td>
                                            <td>
                                    <asp:Label runat="server" ID="mem_SubSection"></asp:Label>
                                                                          
                                       </td>
                                     
                                              
                                  
                                           

                                        </tr>    

                                             </table>
                                                 
                                                 </td>
                                           

                                        </tr>
                                        
                                        
                                           <tr runat="server"  Visible="False" id="mem_Salary">
                                            <td style="width: 10%; padding: 10px;">  Salary 
                                                             </td>
                                               

                                           
                                                
                                                 <td>
                                             <table class="table table-bordered table-striped">
                                        <tr>
                                            
                                            
                                               <td  style="width:20%; padding: 10px;">
                                                 
                                   Present   Salary Grade:
                                                 </td>
                                            <td>
                                                                                                     <asp:Label runat="server" ID="Pmem_SalaryGrade"></asp:Label>
 
                                       </td>

                                            <td  style="width:20%; padding: 10px;">
                                                 
                                     Proposed Salary Grade"
                                                 </td>
                                            <td>
                                                                                                     <asp:Label runat="server" ID="mem_SalaryGrade"></asp:Label>
 
                                       </td>
                                            
                                            
                                             <td  style="width:20%; padding: 10px;">   
                                                
                              Present  Salary Step:</td> <td>
                                                                                                   <asp:Label runat="server" ID="Pmem_SalaryStep"></asp:Label>

                                      </td>
                                     
                                              <td  style="width:20%; padding: 10px;">   
                                                
                            Proposed    Salary Step:</td> <td>
                                                                                                   <asp:Label runat="server" ID="mem_SalaryStep"></asp:Label>

                                      </td>
                                  
                                             

                                        </tr>
                                                 
                                                 
                                                    <tr>
                                            
                                            
                                               <td  style="width:20%; padding: 10px;">
                                                 
                                   Existing   Salary :
                                                 </td>
                                            <td>
                                                                                                     <asp:Label runat="server" ID="mem_P_salary"></asp:Label>
 
                                       </td>

                                            <td  style="width:20%; padding: 10px;">
                                                 
                                     Proposed Salary :
                                                 </td>
                                            <td>
                                                                                                     <asp:Label runat="server" ID="mem_N_salary"></asp:Label>
 
                                       </td>
                                                        
                                                          <td  style="width:20%; padding: 10px;">
                                                 
                                    %:
                                                 </td>
                                            <td>
                                                                                                     <asp:Label runat="server" ID="mem_Percent"></asp:Label>
 
                                       </td>
                                                        </tr>
                                                 

                                             </table>
                                                 
                                                 </td>
                                               
                                      

                                        </tr>
                                        
                                        
                                        
                                          <tr runat="server"  Visible="False" id="divmem_Place">
                                            <td style="width: 10%; padding: 10px;">   Place 
                                                             </td>
                                            <td>
                                                 <table class="table table-bordered table-striped">
                                        <tr>
                                            
                                            
                                             <td  style="width:20%; padding: 10px;">
                                                 
                                     Present Office:
                                                 </td>
                                            <td>     <asp:Label runat="server" ID="Pmem_SalaryLocation"></asp:Label>
                                                        
 
                                       </td>

                                            <td  style="width:20%; padding: 10px;">
                                                 
                                   Proposed   Office:
                                                 </td>
                                            <td>     <asp:Label runat="server" ID="mem_SalaryLocation"></asp:Label>
                                                        
 
                                       </td>
                                            
                                             <td  style="width:20%; padding: 10px;">   
                                                
                             Present  Place:</td> <td>
                                                                                                 <asp:Label runat="server" ID="Pmem_Place"></asp:Label>
                                                        

                                      </td>
                                            
                                     
                                              <td  style="width:20%; padding: 10px;">   
                                                
                             Proposed  Place:</td> <td>
                                                                                                 <asp:Label runat="server" ID="mem_Place"></asp:Label>
                                                        

                                      </td>
                                  
                                           

                                        </tr>
                                                      <tr>
                                                          
                                                          
                                                          
                                                            <td  style="width:20%; padding: 10px;">
                                                 
                                   Present   Floor:
                                                 </td>
                                            <td>       <asp:Label runat="server" ID="Pmem_Floor"></asp:Label>
                                                        
                                                        
 
                                       </td>

                                            <td  style="width:20%; padding: 10px;">
                                                 
                                     Proposed Floor:
                                                 </td>
                                            <td>       <asp:Label runat="server" ID="mem_Floor"></asp:Label>
                                                        
                                                        
 
                                       </td>
                                     
                                           
                                   
                                           

                                        </tr> 

                                                 </table>
                                             
                                    
                                            </td>

                                        </tr>
                                        
                                        
                                         <tr runat="server"  Visible="False" id="tr2">
                                            <td style="width: 10%; padding: 10px;">  Designation
                                                             </td>
                                            <td>
                                                  <table class="table table-bordered table-striped">
                                        <tr>
                                            <td  style="width:20%; padding: 10px;">
                                                 
                                    Designation:
                                                 </td>
                                            <td>
                                                                                                     <asp:Label runat="server" ID="mem_Designation"></asp:Label>
 
                                       </td>
                                     
                                              <td  style="width:20%; padding: 10px;">   
                                                
                          Designation Type:</td> <td>
                                                                                           <asp:Label runat="server" ID="mem_DesignationType"></asp:Label>
                                                          

                                      </td>
                                  
                                           

                                        </tr></table>
                                   
                                            </td>

                                        </tr>
                                    </table>
                                                           
                                        </div>
                                    </div>
                                    <br/>
                                    
                               <div class="form-row">
                                        <div class="col-md-2" style="padding: 10px;">
                                            
                                        </div>

                                        <div class="col-md-10">
                                            <asp:Label ID="txtFooter01" CssClass="form-control"   TextMode="MultiLine" placeholder="Write first paragraph" runat="server" Text="" Font-Size="12px" Rows="2"></asp:Label>
                                        </div>
                                    </div>
                                    
                                         <br/>
                                    
                                          <div class="form-row">
                                        <div class="col-md-2" style="padding: 10px;">
                                            
                                        </div>

                                        <div class="col-md-10">
                                            <asp:Label ID="TextBox3" CssClass="form-control"   TextMode="MultiLine" placeholder="Write first paragraph" runat="server" Text="Therefore, this is placed for the MD & CEO’s kind approval." Font-Size="12px" Rows="2"></asp:Label>
                                        </div>
                                    </div>
                               

                                </div>
                              
                            </div>
          </fieldset>


                                            <fieldset class="for-panel"  runat="server" id="DivAppStatus">
                                                <legend>Approval Status List</legend>
                                                <div class="col-md-12">


                                               
                                                    
                                                    
                                                    


                                                    <div style="height: 350px; overflow: scroll">
                                                        <asp:GridView ID="AppLogCommentGridView" CssClass="table table-bordered text-center thead-dark" runat="server" Width="100%" AutoGenerateColumns="False">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="SL">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                             <asp:BoundField DataField="PreEmp" HeaderText="Approval Person" HtmlEncode="False" />
                                                
                                                      <asp:BoundField DataField="Comments" HeaderText="Comments" HtmlEncode="False" />

                                                    <%--<asp:BoundField DataField="ForEmp" HeaderText="Approval Person" HtmlEncode="False" />--%>
                                                
                                                  <asp:TemplateField HeaderText="Action Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_ActionStatus" runat="server" Text='<%#Eval("ActionStatus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                    <%-- <asp:BoundField DataField="ActionStatus" HeaderText="Action Status" HtmlEncode="False" />--%>
                                                  

                                                    <%--<asp:BoundField DataField="Version" HeaderText="Version" HtmlEncode="False" />--%>
                                                  


                                                    <%--<asp:BoundField DataField="ApproveBy" HeaderText="Approved By" HtmlEncode="False" />--%>
                                                    <asp:BoundField DataField="ApproveDate" HeaderText="Approved Date" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}" />
                                                     
                                               
                                                
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>


                                            </fieldset>
                                        
                                        
                                        
                                        
                                                
                                               

                                        
                        </ContentTemplate>
                        </asp:UpdatePanel>

                                        </div>
                                        
                      
                                    </div>
          
    </div>
    
    </div>
    
        
                                        <ajaxToolkit:ModalPopupExtender ID="MPBehavioral" runat="server" TargetControlID="Behavioral_Test" PopupControlID="pnl_Behavioral"
                                                                        BackgroundCssClass="modalBackground">
                                        </ajaxToolkit:ModalPopupExtender>

                                        <asp:HiddenField ID="Behavioral_Test" runat="server"></asp:HiddenField>    
                                        
                                        
                                          <asp:Panel ID="pnl_Behavioral" runat="server" Style="display: none; overflow: scroll; padding: 10px" Height="600px" Width="90%" CssClass="modalPopup">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                     
                                         <asp:UpdateProgress ID="UpdateProgress2" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWaeeeewit" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>                                                           <div class="row">
                                                    <div class="col-md-6" style="padding-left: 15px;padding-top: 12px;">
                                                          <div class="text-left">
                                                   <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" /> Employment Information</h1>
                                                </div>
                                                    </div>
                                                    
                                                    <div class="col-md-6" style="padding-top: 15px;padding-right: 15px;">
                                                          <asp:LinkButton ID="btnBehavioralClose"   OnClick="btnBehavioralClose_Click" CssClass="btn btn-xs btn-danger pull-right  pull-right" runat="server"><i style="font-size: 16px" class="fa fa-times" aria-hidden="true"></i>
</asp:LinkButton>
                                                    </div>
                                                </div>
                                             
                                             <hr/>
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


   div#cpFormBody_ddlDivision_chosen {
    width: 270px!important;
}

    div#cpFormBody_ddlDivision_chosen {
    width: 270px!important;
}


                                    div#cpFormBody_ddlWing_chosen {
                                        width: 270px !important;
                                    }





                                      div#cpFormBody_ddlDepartment_chosen {
    width: 270px!important;
}


                                        div#cpFormBody_ddlSection_chosen {
    width: 270px!important;
}



                                          div#cpFormBody_ddlSubSection_chosen {
    width: 270px!important;
}



                                            div#cpFormBody_ddlEmpCategory_chosen {
    width: 270px!important;
}



                                                   div#cpFormBody_ddlSalaryGrade_chosen {
    width: 270px!important;
}



                                                          div#cpFormBody_ddlSalaryStep_chosen {
    width: 270px!important;
}


                                                          
                                                          div#cpFormBody_ddlDesignation_chosen {
    width: 270px!important;
}


                                                                  
                                                          div#cpFormBody_ddlDesignationType_chosen {
    width: 270px!important;
}

                                                          


                                                                                               div#cpFormBody_ddlSalaryLocation_chosen {
    width: 270px!important;
}



                                                                                                                                    div#cpFormBody_ddlJobLocation_chosen {
    width: 270px!important;
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
                                             .SelectchkChoiceDsss label {
           font-size: 14px!important;color: darkred !important;
                                                 padding: 2px;
        }
                                </style>
                                     

                                  
                                     
                                     
                                    
                            </div>
                               
                  
                 

                         <hr/>

                    
                           
                        <%--<asp:Button runat="server" ID="btnBehave" OnClick="btnBehave_OnClick" Text="Submit " CssClass="btn btn-sm btn-info" />--%>
                     <%--   <asp:Button ID="btnFunctionalCancel" Text="Close" OnClick="btnFunctionalCancel_OnClick" CssClass="btn btn-sm warning" runat="server" BackColor="#FFCC00" />--%>
                           <br />
                           <br />
                        </div>
                
                    </div>
                </div>
                        </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
</asp:Content>


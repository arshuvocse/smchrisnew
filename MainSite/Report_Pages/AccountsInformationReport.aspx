<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="AccountsInformationReport.aspx.cs" Inherits="Report_Pages_AccountsInformationReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    
     

    
    
            <style>
                fieldset.for-panel {
                    background-color: #fcfcfc;
                    border: 1px solid #999;
                    padding: 15px 10px;
                    background-color: white;
                    margin-bottom: 12px;
                }

                .chkChoiceHeader label {
                    padding-left: 10px;
                    padding-right: 40px;
                    font-size: 14px;
                    font-weight: bold;
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

        <div class="container-fluid" style="background-color: white">
            <div class="page-heading">
                <div class="page-heading__container">
                    <h1 class="title" style="font-size: 18px; padding-top: 0px;">
                        <img src="app.png"  width="20px" />  Employee Information Report</h1>
                </div>
                <%--<div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="detailsViewButton" Text="List Information " CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="detailsViewButton_OnClick" />
                    </div>--%>
            </div>

            <%--    <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                        <ProgressTemplate>
                            <div class="divWaiting">
                                <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>--%>
              <asp:UpdatePanel runat="server" ID="ssas">
                                    <ContentTemplate>
                                          <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divWaiting">
                            <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            <div class="card">
                <div class="card-body" style="background-color: white">
                    

                    <div class="row">
                        <div class="col-md-12">
                           

                                <%--        <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                        <ProgressTemplate>
                            <div class="divWaiting">
                                <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>--%>
                                <div class="row">
                                   
                                    <div class="col-md-4">
                                           <div class="form-group ">
                                                    <label class="control-label">Company </label>
                                                    <asp:DropDownList runat="server" ID="ddlCompany" class="form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged"  />
                                                </div>

                                          <fieldset class="for-panel">
                                <legend>Report List</legend>

                                        <asp:RadioButtonList ID="cblHeader" RepeatDirection="Horizontal" RepeatColumns="1" AutoPostBack="True" OnSelectedIndexChanged="cblHeader_OnSelectedIndexChanged" CssClass="chkChoiceHeader" runat="server">
                                            <asp:ListItem Value="NewJoinerList" >New Joiner List</asp:ListItem>
                                            <asp:ListItem Value="SeparationList">Separation List</asp:ListItem>
                                            <asp:ListItem Value="ProbationaryEmployeeList">Probationary Employee List</asp:ListItem>
                                            <asp:ListItem  Value="Transfer" >Transfer</asp:ListItem>
                                            <asp:ListItem  Value="Promotion" >Promotion</asp:ListItem>
                                            <asp:ListItem  Value="Upgradation">Up-gradation</asp:ListItem>
                                            <asp:ListItem Value="LeaveEncashment">Leave Encashment</asp:ListItem>
                                            <asp:ListItem Value="ContractRenewal">Contract Renewal</asp:ListItem>
                                            <asp:ListItem Value="OtherDeduction">Other Deduction</asp:ListItem>
                                            <asp:ListItem Value="ActiveEmployeeList">Active Employee List

</asp:ListItem>
                                            <asp:ListItem Value="WPPFEntitlementList">WPPF Entitlement List
</asp:ListItem>
                                            <asp:ListItem Value="IncentiveEntitlementList">Incentive Entitlement List
</asp:ListItem>
                                            <asp:ListItem Value="FinalSettlementNoteMemo">Final Settlement Note / Memo
</asp:ListItem>
                                            <asp:ListItem Value="LoanApprovalMemoNote">Loan Approval Memo/Note
</asp:ListItem>
                                            <asp:ListItem Value="BillsTrainervendor">Bills (Trainer / vendor)
</asp:ListItem>
                                            <asp:ListItem Value="confirmationemployeelist">confirmation employee list
</asp:ListItem>
                                            <asp:ListItem Value="Annualmonthlyincrement">Annual/monthly increment
</asp:ListItem>
                                            <asp:ListItem Value="redesignation">re-designation
</asp:ListItem>
                                            <asp:ListItem Value="pickupanddropaddandcancel">pickup and drop add and cancel
</asp:ListItem>



                                            <asp:ListItem Value="lwp">lwp
</asp:ListItem>
                                            <asp:ListItem Value="reappointment">re-appointment
</asp:ListItem>
                                            <asp:ListItem Value="stopdisbusementofsalary">stop disbusement of salary
</asp:ListItem>
                                            

                                        </asp:RadioButtonList>
                                               </fieldset>



                                 </div>

  <div class="col-md-8">
       <br/>

<style>
    
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


        .AspNet-TreeView {
            width: 200px;
            border-top: solid 1px #DDD;
        }

            .AspNet-TreeView ul {
                list-style: none;
            }

        .AspNet-TreeView-Leaf {
            border-bottom: solid 1px #DDD;
            background: url(../../images/structure/node-dot.gif) 8px 9px no-repeat;
        }

        .AspNet-TreeView-Root {
            border-bottom: solid 1px #DDD;
        }

            .AspNet-TreeView-Root a {
                display: block;
                width: 170px;
                margin-left: 20px;
                padding: 5px 5px 5px 5px;
            }

        .AspNet-TreeView-Selected {
            background: #F6F6F6 url(../Assets/arrow-right.png) 8px 9px no-repeat;
        }

        .AspNet-TreeView-Expand {
            display: block;
            float: left;
            margin: 9px 0px 0px 8px;
            padding: 6px 4px 5px 4px;
            height: 0px !important;
            background: url(../Assets/plus-sign.png) 0px 0px no-repeat;
            cursor: pointer;
        }

        .AspNet-TreeView-Collapse {
            display: block;
            float: left;
            margin: 9px 0px 0px 8px;
            padding: 6px 4px 5px 4px;
            height: 0px !important;
            background: url(../Assets/minus-sign.png) 0px 0px no-repeat;
            cursor: pointer;
        }

        .AspNet-TreeView-Show li {
            border-top: solid 1px #DDD;
            background-position: 28px 9px;
        }

        .AspNet-TreeView-Hide {
            display: none;
        }

        .AspNet-TreeView ul li ul li {
            text-indent: 20px;
            border-bottom: none;
            font-size: 11px;
        }

        .treeNode {
            color: black;
            padding-left: 10px;
            padding-right: 10px;
            padding-top: 10px;
               FONT-SIZE: 12px;
            font-weight: bold;
        }

        .rootNode {
            width: 100%;
            padding-left: 10px;
            padding-right: 10px;
            padding-top: 10px;
            FONT-SIZE: 12px;
            font-weight: bold;
        }

        .leafNode {
            padding-left: 10px;
            padding-right: 10px;
            background-color: #eeeeee;
        }
         .Label_Title {
            background-color: #C7C7C7;
            width: 100%;
            text-align: center;
            margin: 0px;
            padding: 7px;
            text-align: center;
            color: #000;
            margin-right: 5%;
            font-weight: bold;
            font-size: 13px;
        }


           #cpFormBody_gv_NewJoinerList td {
            border: 1px solid #ddd;
            padding: 8px;
        }
#cpFormBody_gv_NewJoinerList tr:hover {background-color:#F5F5DC!important;}
#cpFormBody_gv_NewJoinerList tr:nth-child(even) { background-color: #f2f2f2 !important;}
      #cpFormBody_gv_NewJoinerList > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF!important;
        }
  
 

        #cpFormBody_gv_NewJoinerList th {
            padding: 10px;
            border-style: none;

            background-color: #CCCCCC;
            color: black;
            font-weight: bold;
            font-size: 13px;
        }

            #cpFormBody_gv_NewJoinerList > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF!important;
        }



           








            /*jjjjjjjjjjjjj*/


               #cpFormBody_gv_SeparationList td {
            border: 1px solid #ddd;
            padding: 8px;
        }
 #cpFormBody_gv_SeparationList tr:hover {background-color:#F5F5DC!important;}
 #cpFormBody_gv_SeparationList tr:nth-child(even) { background-color: #f2f2f2 !important;}
      #cpFormBody_gv_SeparationList > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF!important;
        }
  
 

        #cpFormBody_gv_SeparationList th {
            padding: 10px;
            border-style: none;

            background-color: #CCCCCC;
            color: black;
            font-weight: bold;
            font-size: 13px;
        }

            #cpFormBody_gv_SeparationList > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF!important;
        }


            /*kkkkkkkk*/
                 #cpFormBody_gv_ProbationaryEmployee td {
            border: 1px solid #ddd;
            padding: 8px;
        }
 #cpFormBody_gv_ProbationaryEmployee tr:hover {background-color:#F5F5DC!important;}
 #cpFormBody_gv_ProbationaryEmployee tr:nth-child(even) { background-color: #f2f2f2 !important;}
      #gv_ProbationaryEmployee > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF!important;
        }
  
 

        #cpFormBody_gv_ProbationaryEmployee th {
            padding: 10px;
            border-style: none;

            background-color: #CCCCCC;
            color: black;
            font-weight: bold;
            font-size: 13px;
        }

            #cpFormBody_gv_ProbationaryEmployee > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF!important;
        }



              /*kkkkkkkk*/
                 #cpFormBody_gv_redesignation td {
            border: 1px solid #ddd;
            padding: 8px;
        }
 #cpFormBody_gv_redesignation tr:hover {background-color:#F5F5DC!important;}
 #cpFormBody_gv_redesignation tr:nth-child(even) { background-color: #f2f2f2 !important;}
      #cpFormBody_gv_redesignation > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF!important;
        }
  
 

        #cpFormBody_gv_redesignation th {
            padding: 10px;
            border-style: none;

            background-color: #CCCCCC;
            color: black;
            font-weight: bold;
            font-size: 13px;
        }

            #cpFormBody_gv_redesignation > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF!important;
        }
</style>
                          
      <asp:Panel runat="server" ID="pNewJoinerList" Visible="False">
      
      <fieldset class="for-panel">
                                                <legend> Filtering Criteria </legend>
                                                <div class="row">
                                                     <div class="col-md-12">
                                                         
                                                         
                                                            <div class="row">
                                                            <div class="col-md-8">
                                                                <div class="Label_Title">Heirarchical Position List</div>
                                                                <div class="form-group">
                                                                    <div style="overflow: scroll; height: 200px">

                                                                        <br />
                                                                        <asp:TreeView ID="heirerchicalTreeView" NodeStyle-CssClass="treeNode" ShowExpandCollapse="True"
                                                                            RootNodeStyle-CssClass="rootNode"
                                                                            LeafNodeStyle-CssClass="leafNode"
                                                                            ExpandImageUrl="../Assets/plus-sign.png" CollapseImageUrl="../Assets/minus-sign.png" runat="server" ShowCheckBoxes="All" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                                <div class="col-md-4">
                                                                            <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="form-group">
                                                                    <label>Date of Join From </label>
                                                                    <asp:TextBox ID="joiningDtFrTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar" PopupButtonID="joiningDtFrTextBox"
                                                                        TargetControlID="joiningDtFrTextBox" />

                                                                </div>
                                                            </div>

                                                            <div class="col-md-12">
                                                                <div class="form-group">
                                                                    <label>Date of Join To </label>
                                                                    <asp:TextBox ID="joiningDtToTextBox" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server"
                                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar" PopupButtonID="joiningDtToTextBox"
                                                                        TargetControlID="joiningDtToTextBox" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                                </div>


                                                        </div>
                                                         </div>
                                                    </div>

          </fieldset>
          </asp:Panel>
      

      <asp:Panel runat="server" ID="PSeparationList" Visible="False">
      
      <fieldset class="for-panel">
                                                <legend> Filtering Criteria </legend>
                                                <div class="row">
                                                     <div class="col-md-12">
                                                         
                                                         
                                                            <div class="row">
                                                            <div class="col-md-8">
                                                                <div class="Label_Title">Heirarchical Position List</div>
                                                                <div class="form-group">
                                                                    <div style="overflow: scroll; height: 200px">

                                                                        <br />
                                                                        <asp:TreeView ID="TreeViewSeparationList" NodeStyle-CssClass="treeNode" ShowExpandCollapse="True"
                                                                            RootNodeStyle-CssClass="rootNode"
                                                                            LeafNodeStyle-CssClass="leafNode"
                                                                            ExpandImageUrl="../Assets/plus-sign.png" CollapseImageUrl="../Assets/minus-sign.png" runat="server" ShowCheckBoxes="All" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                                <div class="col-md-4">
                                                                            <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="form-group">
                                                                    <label>Effective From Date </label>
                                                                    <asp:TextBox ID="SeparationFromDt" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar" PopupButtonID="SeparationFromDt"
                                                                        TargetControlID="SeparationFromDt" />

                                                                </div>
                                                            </div>

                                                            <div class="col-md-12">
                                                                <div class="form-group">
                                                                    <label>Effective To Date </label>
                                                                    <asp:TextBox ID="SeparationToDT" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar" PopupButtonID="SeparationToDT"
                                                                        TargetControlID="SeparationToDT" />
                                                                </div>
                                                            </div>
                                                                                
                                                                                <div class="col-md-12">
                                                                <div class="form-group">
                                                                     <asp:RadioButtonList ID="SperationManualAuto" RepeatDirection="Horizontal" RepeatColumns="1"   CssClass="chkChoiceHeader" runat="server">
                                            <asp:ListItem Value="Manual" >Manual Updated</asp:ListItem>
                                            <asp:ListItem Value="Final">Final Approved</asp:ListItem>
                                                                   </asp:RadioButtonList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                                </div>


                                                        </div>
                                                         </div>
                                                    </div>

          </fieldset>
          </asp:Panel>
      
      
      
       <asp:Panel runat="server" ID="PProbationaryEmployee" Visible="False">
      
      <fieldset class="for-panel">
                                                <legend> Filtering Criteria </legend>
                                                <div class="row">
                                                     <div class="col-md-12">
                                                         
                                                         
                                                            <div class="row">
                                                            <div class="col-md-8">
                                                                <div class="Label_Title">Heirarchical Position List</div>
                                                                <div class="form-group">
                                                                    <div style="overflow: scroll; height: 200px">

                                                                        <br />
                                                                        <asp:TreeView ID="TreeViewProbationaryEmployee" NodeStyle-CssClass="treeNode" ShowExpandCollapse="True"
                                                                            RootNodeStyle-CssClass="rootNode"
                                                                            LeafNodeStyle-CssClass="leafNode"
                                                                            ExpandImageUrl="../Assets/plus-sign.png" CollapseImageUrl="../Assets/minus-sign.png" runat="server" ShowCheckBoxes="All" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                                <div class="col-md-4">
                                                                            <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="form-group">
                                                                    <label>Probationn From Date</label>
                                                                    <asp:TextBox ID="ProbationaryEmployeeFromDt" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server"
                                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar" PopupButtonID="ProbationaryEmployeeFromDt"
                                                                        TargetControlID="ProbationaryEmployeeFromDt" />

                                                                </div>
                                                            </div>

                                                            <div class="col-md-12">
                                                                <div class="form-group">
                                                                    <label>Probation To Date </label>
                                                                    <asp:TextBox ID="ProbationaryEmployeeToDt" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server"
                                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar" PopupButtonID="ProbationaryEmployeeToDt"
                                                                        TargetControlID="ProbationaryEmployeeToDt" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                                </div>


                                                        </div>
                                                         </div>
                                                    </div>

          </fieldset>
          </asp:Panel>
      
      
        <asp:Panel runat="server" ID="Predesignation" Visible="False">
      
      <fieldset class="for-panel">
                                                <legend> Filtering Criteria </legend>
                                                <div class="row">
                                                     <div class="col-md-12">
                                                         
                                                         
                                                            <div class="row">
                                                            <div class="col-md-8">
                                                                <div class="Label_Title">Heirarchical Position List</div>
                                                                <div class="form-group">
                                                                    <div style="overflow: scroll; height: 200px">

                                                                        <br />
                                                                        <asp:TreeView ID="TreeViewredesignation" NodeStyle-CssClass="treeNode" ShowExpandCollapse="True"
                                                                            RootNodeStyle-CssClass="rootNode"
                                                                            LeafNodeStyle-CssClass="leafNode"
                                                                            ExpandImageUrl="../Assets/plus-sign.png" CollapseImageUrl="../Assets/minus-sign.png" runat="server" ShowCheckBoxes="All" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                                <div class="col-md-4">
                                                                            <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="form-group">
                                                                    <label>Effective From Date</label>
                                                                    <asp:TextBox ID="redesignationEffectivefromDT" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server"
                                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar" PopupButtonID="redesignationEffectivefromDT"
                                                                        TargetControlID="redesignationEffectivefromDT" />

                                                                </div>
                                                            </div>

                                                            <div class="col-md-12">
                                                                <div class="form-group">
                                                                    <label>Effective To Date </label>
                                                                    <asp:TextBox ID="redesignationEffectiveToDT" CssClass="form-control form-control-sm" runat="server" CausesValidation="True"></asp:TextBox>
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender8" runat="server"
                                                                        Format="dd-MMM-yyyy" CssClass="MyCalendar" PopupButtonID="redesignationEffectiveToDT"
                                                                        TargetControlID="redesignationEffectiveToDT" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                                </div>


                                                        </div>
                                                         </div>
                                                    </div>

          </fieldset>
          </asp:Panel>
      
        <br/>
      <br/>
      
       <div class="row">
            <div class="col-md-4">
                </div>
           <div class="col-md-4">
<asp:LinkButton runat="server" ID="btn_Save" OnClick="btn_Save_OnClick" CssClass="btn btn-info   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search </asp:LinkButton>

                                            <asp:LinkButton runat="server" ID="lbReset" OnClick="lbReset_OnClick" CssClass="btn btn-warning   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset </asp:LinkButton>  
               </div> 
          </div> 
      <br/>
      <br/>
      <br/>
      
         
      </div>

                                </div>
                            
                            <br/>
                            <br/>
                               <div class="row">
                                   <div class="col-md-6">
                                        <h2 class="blue title-widget" style="color:#2196F3; text-shadow:  0 0 2px black;"> Details Information List</h2>
                                   </div>
                                    <div class="col-md-4">
                                        </div>
                                    <div class="col-md-2">
                                          <asp:LinkButton ID="btnExportToExcel" runat="server"  CssClass="btnexcel  pull-right" OnClick="btnExportToExcel_Click" ><i class="fa fa-file-excel-o"></i>&nbsp; Excel</asp:LinkButton>
                                        </div>
                               </div>
                            <hr/>
      
     
         <div style="overflow: scroll; height: 500px; width: 100%">
                                            <asp:GridView ID="gv_NewJoinerList" runat="server" AutoGenerateColumns="False"
                                                CssClass="table table-bordered  text-center thead-light" DataKeyNames="EmpInfoId" PageIndex="0">

                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="ShortName" HeaderText="Company Name" />
                                                    <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee ID" />
                                                 

                                                    <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                                    <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                                     <asp:BoundField DataField="DateOfJoin" HeaderText="Date of Join" DataFormatString="{0:dd-MMM-yyyy}" />
                                                    <asp:BoundField DataField="Grade" HeaderText="Grade" />
    </Columns>
                                            </asp:GridView>
             
             
             


              <asp:GridView ID="gv_SeparationList" runat="server" AutoGenerateColumns="False"
                                                CssClass="table table-bordered  text-center thead-light" DataKeyNames="EmpInfoId" PageIndex="0">

                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="ShortName" HeaderText="Company Name" />
                                                    <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee ID" />
                                                 

                                                    <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                                    <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                                     <%--<asp:BoundField DataField="DateOfJoin" HeaderText="Date of Join" DataFormatString="{0:dd-MMM-yyyy}" />--%>
                                                    
                                                  
                                    <%--<asp:BoundField DataField="SubmissionDate" HeaderText="Submission Date" DataFormatString="{0:dd-MMM-yyyy}" />--%>

                                     <asp:BoundField DataField="JobLeftDate" HeaderText="Date of Seperation" DataFormatString="{0:dd-MMM-yyyy}" />
                                                  <asp:BoundField DataField="JobLeftType" HeaderText="Type of Separation" />
    </Columns>
                                            </asp:GridView>
             
             
             
             
                <asp:GridView ID="gv_ProbationaryEmployee" runat="server" AutoGenerateColumns="False"
                                                CssClass="table table-bordered  text-center thead-light" DataKeyNames="EmpInfoId" PageIndex="0">

                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="ShortName" HeaderText="Company Name" />
                                                    <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee ID" />
                                                 

                                                    <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                                    <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                                     <asp:BoundField DataField="DateOfJoin" HeaderText="Date of Join" DataFormatString="{0:dd-MMM-yyyy}" />
                                                    
                                                  
                                 

                                     
                                                 
    </Columns>
                                            </asp:GridView>
             
                <asp:GridView ID="gv_redesignation" runat="server" AutoGenerateColumns="False"
                                                CssClass="table table-bordered  text-center thead-light" DataKeyNames="EmpInfoId" PageIndex="0">

                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="ShortName" HeaderText="Company Name" />
                                                    <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee ID" />
                                                 

                                                    <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                                    <asp:BoundField DataField="NDesignation" HeaderText="New Designation" />
                                                     <asp:BoundField DataField="Effectivedate" HeaderText="Effective Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                                    <asp:BoundField DataField="Grade" HeaderText="Grade" />
    </Columns>
                                            </asp:GridView>
                                        </div>
                           
                        </div>
                    </div>
</div>
                </div>
                              
                                           </ContentTemplate>
                     <Triggers>
                 
                 <asp:PostBackTrigger ControlID="btnExportToExcel"/>
             </Triggers>
           </asp:UpdatePanel>        

</div>
        

</asp:Content>


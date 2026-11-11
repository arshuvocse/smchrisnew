<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="TrainingRecommendationsReport.aspx.cs" Inherits="Report_Pages_TrainingRecommendationsReport" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">

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

             .btnexcelcc {
            border: none;
            color: #131313;
            padding-left: 36px;
            padding-top: 8px;
            padding-bottom: 8px;
            padding-right: 36px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 12px;
            margin: 4px 2px;
            cursor: pointer;
            background: url(../Assets/excel.png);
            background-position: center;
            background-repeat: no-repeat;
            box-shadow: 0 0 3px 1px rgba(0,0,0,.35);
        }

    </style>
    <style>
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


        .Label_Title {
            background-color: #C7C7C7;
            width: 100%;
            text-align: center;
            margin: 0px;
            padding: 3px;
            text-align: center;
            color: #000;
            margin-right: 5%;
            font-weight: bold;
            font-size: 13px;
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
        }

        .rootNode {
            width: 100%;
            padding-left: 10px;
            padding-right: 10px;
            padding-top: 10px;
        }

        .leafNode {
            padding-left: 10px;
            padding-right: 10px;
            background-color: #eeeeee;
        }
    </style>

    <div class="content" id="content" style="background-color: white">


        <div class="container-fluid" style="background-color: white">
            <div class="page-heading">
                <div class="page-heading__container">
                    <h1 class="title" style="font-size: 18px; padding-top: 0px;">
                        <img src="app.png" width="18px" />
                        Training Recommendation </h1>
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

            <div class="card">
                <div class="card-body" style="background-color: white">


                    <div class="row">
                        <div class="col-md-12">
                            <fieldset class="for-panel">
                                <legend>Filtering Criteria
                                </legend>

                                <asp:UpdatePanel runat="server" ID="UpdsatePanel1">
                                    <ContentTemplate>
                                               <asp:UpdateProgress ID="progress" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                        <ProgressTemplate>
                            <div class="divWaiting">
                                <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="row">
                                                    <div class="col-8">
                                                        <div class="form-group ">
                                                            <label class="control-label">Company </label>
                                                            <asp:DropDownList runat="server" ID="ddlCompany" class="form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" />
                                                        </div>
                                                    </div>



                                                </div>

                                                <div class="row">
                                                    <div class="col-8">
                                                        <div class="form-group ">
                                                            <label>Financial Year</label>
                                                            &nbsp;<label style="color: #a52a2a">*</label>
                                                            <asp:DropDownList ID="KPIddlFinancialYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="KPIddlFinancialYear_OnSelectedIndexChanged" class="form-control form-control-sm"></asp:DropDownList>
                                                        </div>
                                                    </div>

                                                </div>

                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="form-group ">
                                                            <label>Employee Name </label>
                                                            <asp:TextBox ID="SearchEmployeeNameTextBoxTextBox" AutoPostBack="True" runat="server" class="form-control form-control-sm" OnTextChanged="SearchEmployeeNameTextBoxTextBox_OnTextChanged"></asp:TextBox>
                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                                EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                                ServiceMethod="GetCompanyWiseEmployeeInfoOnlyForPromotion" ServicePath="~/WebService.asmx" TargetControlID="SearchEmployeeNameTextBoxTextBox"
                                                                UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                                ShowOnlyCurrentWordInCompletionListItem="true">
                                                            </cc1:AutoCompleteExtender>
                                                            <asp:HiddenField ID="repEmpIdHiddenField" runat="server" />

                                                        </div>

                                                    </div>
                                                </div>

                                            </div>

                                            <div class="col-md-5">


                                                <div class="row" runat="server" visible="False">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <div class="form-group">
                                                                <label>Heirarchical Position </label>

                                                                <asp:DropDownList ID="hierchicalDropDownList" class="form-control form-control-sm" runat="server">
                                                                    <asp:ListItem> In </asp:ListItem>
                                                                    <asp:ListItem> Not In </asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="Label_Title">Heirarchical Position List</div>
                                                        <div class="form-group">
                                                            <div style="overflow: scroll; height: 150px">

                                                                <br />
                                                                <asp:TreeView ID="heirerchicalTreeView" NodeStyle-CssClass="treeNode" ShowExpandCollapse="True"
                                                                    RootNodeStyle-CssClass="rootNode"
                                                                    LeafNodeStyle-CssClass="leafNode"
                                                                    ExpandImageUrl="../Assets/plus-sign.png" CollapseImageUrl="../Assets/minus-sign.png" runat="server" ShowCheckBoxes="All" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>

                                        </div>
                                        <br/>
                                        <br/>
                                       


                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </fieldset>
                            
                            <br/>
                            <br/>
                             <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                    <ContentTemplate>
                                               <asp:UpdateProgress ID="UpdateProgress1" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                        <ProgressTemplate>
                            <div class="divWaiting">
                                <asp:Image ID="imgWssasait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                              <div class="row">
                                        <div class="col-md-4">
                                            <%--<asp:Button runat="server" ID="Button51" OnClick="btn_Save_OnClick" Text="Search " CssClass="btn btn-outline-info btn-block disabled btn-sm" />--%>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:LinkButton runat="server" ID="lbEducationSearch" OnClick="lbEducationSearch_OnClick" CssClass="btn btn-info   btn-sm"><span aria-hidden="true" class="fa fa-search-plus"></span>  &nbsp; Search </asp:LinkButton>

                                            <asp:LinkButton runat="server" ID="lbEducationReset" OnClick="lbEducationReset_OnClick" CssClass="btn btn-warning   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset </asp:LinkButton>
                                        </div>
                                        <div class="col-md-4">
                                            <%--<asp:Button runat="server" ID="Button51" OnClick="btn_Save_OnClick" Text="Search " CssClass="btn btn-outline-info btn-block disabled btn-sm" />--%>
                                        </div>
                                    </div>
                        
                          </ContentTemplate>
                                </asp:UpdatePanel>
                        
                        
                          <br/>
                            <br/>
                            
                            
                                <div class="form-row" style="padding-right: 10px">

                                <div class="col-md-6" style="padding-left: 2px">
                                    <label style="font-size: 18px;">Details Information</label>
                                </div>
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-2">
                                    <%--  <asp:LinkButton ID="btnExportToExcel" runat="server" CssClass="btnexcel  pull-right" Style="padding-right: 10px;" OnClick="btnExportToExcel_Click"><span aria-hidden="true" style="font-size: 14px; color: #4CAF50;" class="fa fa-file-excel-o"></span> Download to xls</asp:LinkButton>--%>

                                    <input type="button" id="btnExportDisciplinary" title="Export to Excel" class="pull-right btnexcelcc " value="" />
                                </div>


                            </div>

                            <hr />
                            
                             <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                    <ContentTemplate>
                                               <asp:UpdateProgress ID="UpdateProgress2" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                        <ProgressTemplate>
                            <div class="divWaiting">
                                <asp:Image ID="imgWasdsassasait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                                <asp:GridView runat="server"    AutoGenerateColumns="False" Width="100%" id="gv_AppraisalTraining" CssClass="table table-bordered text-center thead-dark gridDatatable">
                                
                                                <Columns>

                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                    
                                                    
                                                    <asp:TemplateField HeaderText="Employee ID">
                                    <ItemTemplate>
                                       <asp:Label runat="server" ID="txtEmployeeID"    Text='<%#Eval("EmpMasterCode") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                    
                                                        <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                       <asp:Label runat="server" ID="txtEmpName"   Text='<%#Eval("EmpName") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                    
                                                    
                                                           

                                                           
                                                    
                                   <asp:TemplateField HeaderText="Training Needs">
                                    <ItemTemplate>
                                       <asp:Label runat="server" ID="TrainingNeeds"    TextMode="MultiLine" Text='<%#Eval("TrainingNeeds") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                    
                                   <asp:TemplateField HeaderText="Quater">
                                    <ItemTemplate>
                                             <asp:DropDownList ID="QuaterDropDownList1" AutoPostBack="true" runat="server" Enabled="False"  CssClass="form-control form-control-sm">
                                                      <asp:ListItem Text="1st Quarter" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="2nd Quarter" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="3rd Quarter" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="4th Quarter" Value="4"></asp:ListItem>

                                             </asp:DropDownList>
                                      
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                    
                            
                                
                            </Columns>
                            </asp:GridView>
                                        </ContentTemplate>
                                </asp:UpdatePanel>
                    </div>
                </div>
                    </div>
            </div>
        </div>
    </div>
    
      <script src="../Assets/table2excel.js"></script>
    <script type="text/javascript">
        $("body").on("click", "#btnExportDisciplinary", function () {
            $("[id*=gv_AppraisalTraining]").table2excel({
                filename: "Education_raining_Recommendation_Info.xls"
            });
        });

    </script>
</asp:Content>


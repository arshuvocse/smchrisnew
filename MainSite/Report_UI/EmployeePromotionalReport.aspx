




<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="EmployeePromotionalReport.aspx.cs" Inherits="Report_UI_EmployeePromotionalReport" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="Server">
    

     <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js" integrity="sha512-rMGGF4wg1R73ehtnxXBt5mbUfN9JUJwbk21KMlnLZDJh7BkPmeovBuddZCENJddHYYMkCh9hPFnPmS9sspki8g==" crossorigin="anonymous"></script>
    
    
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" integrity="sha512-yVvxUQV0QESBt1SyZbNJMAwyKvFTLMyXSyBHDO4BG5t7k/Lw34tyqlSDlKIrIENIzCl+RVUNjmCPG+V/GMesRw==" crossorigin="anonymous" />
 
    <style type="text/css">
        /*AutoComplete flyout */
      
    </style>
    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=40);
            opacity: 0.4;
        }
        
        .SelectchkChoice label {
            padding-left: 6px;
            font-weight: bold;
        }

        .modalPopup {
            background-color: #FFFFFF;
            width: 300px;
            border: 3px solid #0DA9D0;
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


    <style>
        .btnexcel {
            background-color: #4CAF50;
            border: none;
            color: white;
            padding: 8px 12px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 12px;
            margin: 4px 2px;
            cursor: pointer;
            text-decoration: none;
        }

        .btnPDF {
            background-color: #008CBA;
            border: none;
            color: white;
            padding: 8px 12px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 12px;
            margin: 4px 2px;
            cursor: pointer;
        }



        .chkChoice label {
            padding-left: 10px;
            padding-right: 30px;
        }

        .chkChoiceStep label {
            padding-left: 10px;
            padding-right: 15px;
        }

        .chkChoiceDesignation label {
            padding-left: 10px;
            padding-right: 8px;
        }

        .chkChoiceHeader label {
            padding-left: 10px;
            padding-right: 40px;
            font-size: 13px;
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
 </style>  <%-- 
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/themes/base/jquery-ui.css" rel="stylesheet" type="text/css"/>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/jquery-ui.min.js"></script> 
<script type="text/javascript">
    $(document).ready(function () {
        SearchText();
    });
    function SearchText() {
        
        $(".autosuggest").autocomplete({
            source: function (request, response) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "EmployeeProfile.aspx/GetAutoCompleteData",
                    data: "{'username':'" + document.getElementById('txtSearch').value + "'}",
                    dataType: "json",
                    success: function (data) {
                        response(data.d);
                    },
                    error: function (result) {
                        alert("Error");
                    }
                });
            }
        });
    }
</script>
    
    
    
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
<script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
<link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
    rel="Stylesheet" type="text/css" />
<script type="text/javascript">
    $(function () {
        $("[id$=SearchEmployeeNameTextBoxInProfile1]").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "EmployeeProfile.aspx/GetCustomers",

                    data: "{ 'prefix': '" + request.term + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                label: item.split('-')[0],
                                val: item.split('-')[1]
                            }
                        }))
                    },
                    error: function (response) {
                        alert(response.responseText);
                    },
                    failure: function (response) {
                        alert(response.responseText);
                    }
                });
            },
            select: function (e, i) {
                $("[id$=repEmpIdHiddenField]").val(i.item.val);
            },
            minLength: 1
        });
    });
</script>--%>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cpFormBody" runat="Server">


    <div class="content" id="content" style="background-color: white">
        <asp:UpdatePanel ID="upFormBody" runat="server">
            <ContentTemplate>

                <div class="container-fluid" style="background-color: white">
                    <div class="page-heading">
                        <div class="page-heading__container">
                            <h1 class="title" style="font-size: 18px; padding-top: 0px;"><img src="../Report_Pages/app.png"  width="20px" /> Employee Promotional Report</h1>
                        </div>
                        <div class="page-heading__container float-right d-none d-sm-block">
                          <asp:LinkButton ID="homeButton" style="font-style: normal!important; font-weight: bold" CssClass="btn btn-sm btn-outline-secondary " runat="server"  OnClick="homeButton_OnClick"><i class="fa fa-home"></i>&nbsp; Home</asp:LinkButton>
                    </div>
                    </div>

                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" ClientIDMode="Static" DisplayAfter="0" DynamicLayout="true">
                        <ProgressTemplate>
                            <div class="divWaiting">
                                <asp:Image ID="imgWait" CssClass="position-set" runat="server" ImageAlign="Middle" ImageUrl="~/Assets/img/progress-bar-opt.gif" Width="120px" Height="120px" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>

                    <div class="card">
                        <div class="card-body" style="background-color: white">
                            
                            
                             
                                       <div class="form-row">
                                <div class="col-md-12">
                                    <fieldset class="for-panel">
                                        <legend>Filtering Criteria </legend>
                                        <div class="row" runat="server" visible="false">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label></label>
                                                    <asp:CheckBoxList runat="server" ID="CheckBoxList1" AutoPostBack="True" RepeatDirection="Horizontal" Enabled="False">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">

                                            <div class="col-3">
                                                <div class="form-group ">
                                                    <label class="control-label">Company </label>
                                                    <asp:DropDownList runat="server" ID="ddlCompany" class="form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" />
                                                </div>
                                            </div>

                                       

                                            <div class="col-5">
                                                <div class="form-group ">
                                                    <label>Employee Name </label>
                                                    <asp:DropDownList runat="server" ID="ddlEmpInfo" class="form-control form-control-sm" />
                                                    <script type="text/javascript">
                                                        function pageLoad() {
                                                            $('#<%=ddlEmpInfo.ClientID%>').chosen({ disable_search_threshold: 5, search_contains: true });

                                                           
                                                        }

                                                    
</script>

                                             <%--       <script>

                                                        $('#<%=ddlEmpInfo.ClientID%>').chosen();
    </script>--%>
                                                       
                                                     <%--<input type="text" id="txtSearch" class="autosuggest" />--%>
                                                    <asp:TextBox Visible="False" ID="SearchEmployeeNameTextBoxInProfile1"  runat="server" AutoPostBack="True" class="form-control form-control-sm" OnTextChanged="SearchEmployeeNameTextBoxTextBox_OnTextChanged"></asp:TextBox>
                                                <%--    <cc1:AutoCompleteExtender ID="AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                                        EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                        ServiceMethod="GetAutoCompleteData2" ServicePath="~/WebService.asmx" TargetControlID="SearchEmployeeNameTextBoxInProfile1"
                                                        UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                        ShowOnlyCurrentWordInCompletionListItem="true">
                                                    </cc1:AutoCompleteExtender>--%>
                                                    
                                                    
                                                <%--      <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                        EnableCaching="true" Enabled="True" MinimumPrefixLength="1" CompletionSetCount="10"
                                                        ServiceMethod="GetEmpProfile" ServicePath="~/WebService.asmx" TargetControlID="SearchEmployeeNameTextBoxInProfile1"
                                                        UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                        ShowOnlyCurrentWordInCompletionListItem="true">
                                                    </cc1:AutoCompleteExtender>--%>
                                                    <asp:HiddenField ID="repEmpIdHiddenField" runat="server" />

                                                </div>
                                            </div>

                                        </div>
                                      


                                    
                                    </fieldset>






                                    <div class="row">
                                <div class="col-md-12">
                                    <fieldset class="for-panel">
                                        <legend>Name of Information</legend>

                                        <div class="row">
                                           

                                            <div class="col-md-12">
                                                
                                                  <asp:CheckBox Visible="False" runat="server" ID="SSGradeCheck" CssClass="SelectchkChoice" AutoPostBack="True" OnCheckedChanged="SSGradeCheck_OnCheckedChanged" Text=" Select All / Unselect All" />
                                                                        <br />
                                                <asp:RadioButtonList ID="cblHeader"     RepeatDirection="Vertical" RepeatColumns="3" CssClass="chkChoiceHeader" runat="server">
                                                  
                                                 
                                                    
                                                    <asp:ListItem  Value="PI">Promotion History of Employee</asp:ListItem>
                                                    <asp:ListItem Value="UGI">Upgradation History of Employee</asp:ListItem>
                                                    <asp:ListItem  Value="SP"> History of Special Increment</asp:ListItem>
                                                    
                                                    
                                                    <%--<asp:ListItem   Value="INI">Increment Information</asp:ListItem>--%>
                                               <%--     <asp:ListItem  Value="RE">Re-Designation Information</asp:ListItem>
                                                     <asp:ListItem  Value="CN">Contractual (Extension/Renew/Permanent to Contractual/Contractual to Permanent) Information</asp:ListItem>--%>


                                                </asp:RadioButtonList>

                                            </div>
                                        </div>
                                </div>
                            </div>
                                              

                                    <div class="row">
                                        <div class="col-md-5">
                                            <%--<asp:Button runat="server" ID="Button51" OnClick="btn_Save_OnClick" Text="Search " CssClass="btn btn-outline-info btn-block disabled btn-sm" />--%>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:LinkButton runat="server" ID="LinkButton1" OnClick="submitButton_OnClick" AutoPostBack="True" CssClass="btn btn-info   btn-sm"><span aria-hidden="true" class="fa fa-print"></span>  &nbsp; View Report </asp:LinkButton>
                                            
                                            <asp:LinkButton runat="server" ID="LinkButton2" OnClick="lbReset_OnClick" CssClass="btn btn-warning   btn-sm"><span aria-hidden="true" class="fa fa-retweet"></span>  &nbsp; Reset </asp:LinkButton>
                                        </div>
                                        <div class="col-md-4">
                                            <%--<asp:Button runat="server" ID="Button51" OnClick="btn_Save_OnClick" Text="Search " CssClass="btn btn-outline-info btn-block disabled btn-sm" />--%>
                                        </div>
                                    </div>
                                    

                                
                              
  

                            
                                    <div class="row" runat="server" id="data">
                                
                                
                                
                                    </div>
                                    
                                    
                           
              
                                



                                </div>

                            </div>
                                      
                                      

                             


                                     


                                   


                                 

                                      

                                       
                                        
                                        
                                       


                                     

                                   
                              


                                 

                            <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                            <br/>
                                </div>

                            </div>

                          
                           


                          

                         
                        

                        </div>

                    </div>
                </div>
            </ContentTemplate>
           
        </asp:UpdatePanel>

      
        
        
     
       

    
   
         






                
              </div>
  

 
   
     

       
 
    
</asp:Content>







<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MainMasterPage.master" CodeFile="MPBudgetApproved.aspx.cs" Inherits="MPBudget_MPBudgetList" %>


<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">
    <link href="../Assets/MaterialDT/dataTables.material.min.css" rel="stylesheet" />
    <link href="../Assets/MaterialDT/material.min.css" rel="stylesheet" />
    <link href="../Assets/assets/css/bootstrap.css" rel="stylesheet" />
    <style type="text/css">
        
    </style>
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <div class="content" id="content">
        

                <div class="container-fluid">
                <div class="page-heading">
                    <div class="page-heading__container">
                        <h1 class="title" style="font-size: 18px; padding-top: 0px;">Manpower Budget List</h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                        <asp:Button ID="btn_New" Text="Create New" CssClass="btn btn-sm btn-outline-secondary " runat="server" OnClick="btn_New_OnClick" />
                        
                    </div>
                </div>
                <div class="card">
                    <div class="card-body">
                        <div class="form-row">
                            <div class="col-3">
                                <div class="form-group">
                                    <label>Company</label>
                                    <asp:DropDownList runat="server" AutoPostBack="True" ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" class="form-control form-control-sm" />
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group">
                                    <label>Department</label>
                                    <asp:DropDownList runat="server"  ID="ddlDepartment" class="form-control form-control-sm" />
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group">
                                    <label>Financial Year</label>
                                    <asp:DropDownList runat="server" ID="ddlFinYear" class="form-control form-control-sm" />
                                </div>
                            </div>
                            <div class="col-2 ">
                                <div class="form-group">
                                    <br/>
<%--                                    <asp:Button ID="btnFilterSearch" Text="Filter Search" OnClick="btnFilterSearch_OnClick" CssClass="btn btn-sm activity-success" runat="server" BackColor="#FFCC00" />--%>
                                    <input type="button" class="btn btn-sm activity-success" style="background-color: #FFCC00;" id="btnFilterSearch" value="Filter"></input>

                                </div>
                            </div>
                        </div>
                        <br/>
                            <div id="tbl_container" style="width:100%" class="">

                            </div>
                        
                          
                    </div>
                </div>
            
    </div>
    </div>
    <%--<script src="../Assets/MaterialDT/jquery-3.3.1.js"></script>--%>
    <script src="../Assets/MaterialDT/jquery.dataTables.min.js"></script>
    <script src="../Assets/MaterialDT/dataTables.material.min.js"></script>
    <script type="text/javascript">
        $.getScript('../AppJs/MPBudgetList.js', function (data, textStatus, jqxhr) {
            console.log('Script File Load Status=' + jqxhr.status);
        });
    </script>
</asp:Content>
    

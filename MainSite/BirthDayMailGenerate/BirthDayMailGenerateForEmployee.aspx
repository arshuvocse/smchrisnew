<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MainMasterPage.master" CodeFile="BirthDayMailGenerateForEmployee.aspx.cs" Inherits="BirthDayMailGenerate_BirthDayMailGenerateForEmployee" EnableEventValidation="false" %>


<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        
        </style>
</asp:Content>
<asp:Content ID="c2" ContentPlaceHolderID="cpFormBody" runat="Server">
   
    <div class="content" id="content">

        <style>
      

        </style>
        <div class="container-fluid">
            <div class="page-heading">
                <div class="page-heading__container">
                    <h1 class="title" style="font-size: 18px; padding-top: 0px;">Month Wise Birth Day Employee Information List</h1>
                </div>
                <div class="page-heading__container float-right d-none d-sm-block">
                    <asp:Button ID="HomeButton" Text="Home" CssClass="btn btn-sm btn-outline-secondary " Visible="False" runat="server" OnClick="HomeButton_OnClick" />
                    <asp:Button ID="btn_New" Text="Create New" CssClass="btn btn-sm btn-outline-secondary " Visible="False" runat="server" OnClick="btn_New_OnClick" />

                </div>
            </div>
            <div class="container-fluid">
                <div class="card">
                    <%-- <asp:UpdatePanel runat="server" ID="uppa">
                        <ContentTemplate>--%>
                    <%--  <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="uppa">
                                <ProgressTemplate>
                                    <div class="modal">
                                        <div class="center">
                                            <img alt="" src="/Assets/loader.gif" />
                                        </div>
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>--%>
                    <div class="card-body">
                        <asp:UpdatePanel runat="server" ID="uppa">
                            <ContentTemplate>
                                 <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="uppa">
                                <ProgressTemplate>
                                    <div class="modal">
                                        <div class="center">
                                            <img alt="" src="/Assets/loader.gif" />
                                        </div>
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                                <div class="form-row">
                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>Company</label>
                                            <label style="color: #a52a2a">*</label>
                                            <asp:DropDownList runat="server"  ID="ddlCompany" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" class="form-control form-control-sm" />
                                        </div>
                                    </div>
                                    
                                    
                                        <div class="col-2">
                                        <div class="form-group">
                                            <label>Month</label>
                                            <label style="color: #a52a2a">*</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" ID="MonthDropDownList"   class="form-control form-control-sm" >
                                                <asp:ListItem Value="0">Please Select a Month</asp:ListItem>
                                                <asp:ListItem Value="January"></asp:ListItem>
                                                <asp:ListItem Value="February"></asp:ListItem>
                                                <asp:ListItem Value="March"></asp:ListItem>
                                                <asp:ListItem Value="April"></asp:ListItem>
                                                <asp:ListItem Value="May"></asp:ListItem>
                                                <asp:ListItem Value="June"></asp:ListItem>
                                                <asp:ListItem Value="July"></asp:ListItem>
                                                <asp:ListItem Value="August"></asp:ListItem>
                                                <asp:ListItem>September</asp:ListItem>
                                                <asp:ListItem Value="October"></asp:ListItem>
                                                <asp:ListItem Value="November"></asp:ListItem>
                                                <asp:ListItem Value="December"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    
                                    <div class="col-md-2">
                                        <div class="form-group" style="margin-top: 17px;">

                                            <asp:Button runat="server" ID="EmpBirthDaySearchButton" OnClick="EmpBirthDaySearchButton_OnClick" ToolTip="Click To Search" Width="80" Text="Search" class="btn btn-info btn-sm" />

                                            &nbsp;&nbsp;

                                            <asp:Button runat="server" ID="EmpBirthDaybtnReset" OnClick="EmpBirthDaybtnReset_OnClick" ToolTip="Click To Reset" Text="Reset" Width="80" CssClass="btn btn-sm warning" BackColor="#FFCC00" />
                                        </div>
                                    </div>

                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                            <ContentTemplate>
                                <div class="row">


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
                                        }
                                    </style>
                                    
                                </div>
                                <div class="col-md-2">
                                </div>



                                </div> 
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="row" runat="server" visible="False">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label></label>
                                    <asp:CheckBoxList runat="server" ID="lchk_Company" AutoPostBack="True" RepeatDirection="Horizontal" Enabled="False">
                                    </asp:CheckBoxList>


                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-8">
                            </div>
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-2" style="margin-top: 17px;">
                                <asp:LinkButton ID="btnExportToExcel" runat="server" CssClass="btnexcel  pull-right" OnClick="btnExportToExcel_Click"><span aria-hidden="true" class="glyphicon glyphicon-ok"></span>Export To Excel</asp:LinkButton>
                            </div>
                        </div>
                        <%-- <div id="tbl_container" style="width:100%" class="">

                    </div>--%>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                            <ContentTemplate>
                                <div id="gridContainessr1" style="height: auto; overflow: auto; width: auto;">
                                    <asp:GridView ID="EmpBirthDayloadGridView" runat="server" AutoGenerateColumns="False"
                                        CssClass="table table-bordered text-center thead-dark" 
                                       >
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL">
                                                <ItemTemplate>
                                                    <asp:Label ID="LabelSL" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                       
                                            <asp:BoundField DataField="EmpMasterCode" HeaderText="Employee ID" />
                                            <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                            <asp:BoundField DataField="DivisionName" HeaderText="Division" />

                                            <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                            <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                            <asp:BoundField DataField="DateOfBirth" HeaderText="Date of Birth"  DataFormatString="{0:dd-MMM-yyyy}"/>
                                           



                                           
                                        </Columns>
                                        
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                </div>

            </div>
        </div>
    </div>




    <%--   <script src="../Assets/MaterialDT/jquery.dataTables.min.js"></script>
    <script src="../Assets/MaterialDT/dataTables.material.min.js"></script>
    <script type="text/javascript">
        $.getScript('../AppJs/EmployeeInfoList.js', function (data, textStatus, jqxhr) {
            console.log('Script File Load Status=' + jqxhr.status);
        });
    </script>--%>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="MISReport.aspx.cs" Inherits="Report_Pages_MISReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" runat="Server">
    <style>
         .btnexcel {
           
            border: none;
            color: #131313!important;
           
            padding-left: 24px !important;
            padding-top: 8px !important;
            padding-bottom: 8px !important;
            padding-right: 24px !important;
            text-align: center!important;
            text-decoration: none!important;
            display: inline-block!important;
            font-size: 12px!important;
            margin: 4px 2px!important;
            cursor: pointer!important;
            background: url(../Assets/excel.png);
            background-position:center!important;
            background-repeat:no-repeat!important;
            box-shadow: 0 0 3px 1px rgba(0,0,0,.35)!important;
        }
    </style>

    <div class="content" id="content">

        <asp:UpdatePanel runat="server" ID="uppa">
            <ContentTemplate>

                <div class="container-fluid" style="background-color: white">
                    <div class="card" style="background-color: white">
                        <div class="card-body" style="background-color: white">
                            <div id="coverScreen" class="LockOn">
                            </div>




                            <div class="row">
                                <div class="col-md-10">
                                    <h1><i class="fa fa-dashboard"></i>&nbsp; Total Employee Count List</h1>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Company</label>
                                        <asp:DropDownList ID="ddlCompany" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                </div>

                            </div>

                            <div class="row" style="padding: 5px;" runat="server" visible="False">
                                <div class="col-md-10"></div>
                                <div class="col-md-2">

                                    <input type="button" id="btngv_Table01Export" onclick="tableToExcel('testTable', 'W3C Example Table')" title="Export to Excel" class="pull-right btnexcel " value=" " />


                                </div>
                            </div>

                            <div class="row">

                                <div class="col-md-12">
                                    <div class=" text-center">
                                        <asp:GridView ID="gv_Table01_DepartmentEmployment" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-bordered text-center thead-dark table-striped table-hover">
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            
                          <input type="button" id="btngv" onclick="tableToExcel('testTable', 'W3C Example Table')" title="Export to Excel"   class="pull-right btnexcel " value=" " />
                            <br/>
                            <br/>
                  <div id="repotDiv">
                      
                               
                                
                                  <div style="text-align: center">
                                      
                                 
  <tr style="text-align: center">
     
    <th><h3><label runat="server" id="lblCompany"></label></h3></th>
    
  </tr>
   <tr>
     
    <th  style="text-align: center"><h4>HR Division</h4></th>
    
  </tr>
  
   <tr  style="text-align: center">
     
    <th><h5>Staff information (as on <label runat="server" id="lblDate"></label>)</h5></th>
    
  </tr>
   
</div>
                               
                               
                            



                            <div class="row">
                                <div class="col-md-6">
                                    <div class="row" style="padding: 5px;" runat="server" visible="False">
                                        <div class="col-md-10"></div>
                                        <div class="col-md-2">

                                            <input type="button" id="btngv_Table03Export" title="Export to Excel" class="pull-right btnexcel " value=" " />


                                        </div>
                                    </div>
                                    <div class=" text-center">
                                        <asp:GridView ID="gv_Table03_ProjectFundWisemalefemaleRatio" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-bordered text-center thead-dark table-striped table-hover">
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="col-md-6">

                                    <div class="row" style="padding: 5px;" runat="server" visible="False">
                                        <div class="col-md-10"></div>
                                        <div class="col-md-2">

                                            <input type="button" id="btngv_Table04Export" title="Export to Excel" class="pull-right btnexcel " value=" " />


                                        </div>
                                    </div>
                                    <div class=" text-center">
                                        <asp:GridView ID="gv_Table04_ProjectWiseStaff" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-bordered text-center thead-dark table-striped table-hover">
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                            <div class="row" style="padding: 5px;" runat="server" visible="False">
                                <div class="col-md-10"></div>
                                <div class="col-md-2">

                                    <input type="button" id="btngv_Table05Export" title="Export to Excel" class="pull-right btnexcel " value=" " />


                                </div>
                            </div>


                      <br />
                      <br />
                            <div class="row">

                                <div class="col-md-12">
                                    <div class=" text-center">

                                        <asp:GridView ID="gv_Table05_GradeWiseStaff" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-bordered text-center thead-dark table-striped table-hover">
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>



                            <div class="row" style="padding: 5px;" runat="server" visible="False">
                                <div class="col-md-10"></div>
                                <div class="col-md-2">

                                    <input type="button" id="btngv_Table05_GradeWiseStaff" onclick="tableToExcel('testTable', 'W3C Example Table')" title="Export to Excel" class="pull-right btnexcel " value=" " />


                                </div>
                            </div>
                            
                             <div class="row">

                                <div class="col-md-12" runat="server" id="departmentTypeWiseStaff"></div>
                                 </div>
                        <br />
                      <br />
                            <div class="row">
                                 
                                <div class="col-md-4" runat="server" id="staffByDesignation">
                                    <table>
                                        <tr>
                                            <%--<td>Table-2: Staff by designation</td>--%>
                                        </tr>
                                    </table>
                                </div>
                                <div class="col-md-3" runat="server" id="departmentGenderWiseStaff">
                                    <table>
                                        <tr>
                                            <%--<td>Table-2: Staff by designation</td>--%>
                                        </tr>
                                    </table>
                                </div>

                            </div>
                              <br />
                      <br />
                            <div class="row">

                                <div class="col-md-12" runat="server" id="gradeWiseStaff"></div>
                                <%--<div class="col-md-4" runat="server" id="gradeWiseManWoman"></div>--%>
                            </div>

                            <%--Department & Grade wise Staff--%>

                            <div class="row">

                                <div class="col-md-12" runat="server" id="departmentGradeWiseStaff">
                                </div>
                            </div>
                      <br />
                      <br />
                             <div class="row">

                                <div class="col-md-8"  runat="server" id="table7">
                                    
                                    </div>
                            
                                 
                                   <div class="col-md-4" runat="server" id="divTable8">
                                </div>
                            </div>


                            <div class="row" style="padding: 5px;" runat="server" visible="False">
                                <div class="col-md-6"></div>
                                <div class="col-md-2">

                                    <input type="button" id="btndepartmentTypeWiseStaff" onclick="tableToExcel('testTable', 'W3C Example Table')" title="Export to Excel" class="pull-right btnexcel " value=" " />


                                </div>

                                <div class="col-md-2"></div>
                                <div class="col-md-2" runat="server" visible="False">

                                    <input type="button" id="btndepartmentGenderWiseStaff" onclick="tableToExcel('testTable', 'W3C Example Table')" title="Export to Excel" class="pull-right btnexcel " value=" " />


                                </div>
                            </div>



                            <div class="row" style="padding: 5px;" runat="server" visible="False">
                                <div class="col-md-10"></div>
                                <div class="col-md-2">

                                    <input type="button" id="btngv_gradeWiseStaff" onclick="tableToExcel('testTable', 'W3C Example Table')" title="Export to Excel" class="pull-right btnexcel " value=" " />


                                </div>
                            </div>
                  </div>
                            
                          
                        </div>




                    </div>
                </div>
            </ContentTemplate>

        </asp:UpdatePanel>
    </div>
        <script src="../Assets/table2excel.js"></script>
    <script>
        
        $("#btngv").click(function (e) {
            debugger;

            $("#repotDiv :hidden").remove();
            let file = new Blob([$('#repotDiv').html()], { type: "application/vnd.ms-excel" });
            let url = URL.createObjectURL(file);

            let a = $("<a />", {
                href: url,
                download: "Staff information Report.xlsx"
            }).appendTo("body").get(0).click();
            e.preventDefault();




        });
       
    </script>

</asp:Content>


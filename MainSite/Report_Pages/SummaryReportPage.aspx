<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="SummaryReportPage.aspx.cs" Inherits="Report_Pages_SummaryReportPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <script src="https://code.jquery.com/jquery-3.3.1.js"></script>
       <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.19/css/dataTables.bootstrap4.min.css" />
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.19/js/dataTables.bootstrap4.min.js"></script>
 <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/buttons/1.5.6/js/dataTables.buttons.min.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/buttons/1.5.6/js/buttons.flash.min.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/buttons/1.5.6/js/buttons.html5.min.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/buttons/1.5.6/js/buttons.print.min.js"></script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
    <style>
          .LockOn {
            position: fixed;
            left: 0px;
            top: 0px;
            z-index: 2147483647;
            width: 100%;
            height: 2215px;
            background-color: #676767;
            vertical-align: bottom;
            padding-top: 20%;
            filter: alpha(opacity=85);
            opacity: 0.85;
            font-size: large;
            color: #676767;
            font-style: italic;
            font-weight: 400;
            background-image: url("../Assets/img/LatestLoading.gif");
            background-repeat: no-repeat;
            background-attachment: fixed;
            background-position: center;
        }

    </style>
    <div class="content" id="content">
        <div class="container-fluid" style="background-color: white">
            <div class="card" style="background-color: white">
                <div class="card-body" style="background-color: white">
                           <div id="coverScreen" class="LockOn">
                </div>
                 

                    <div class="row"  >
                        <div class="col-md-10"> <h1><i class="fa fa-dashboard"></i>&nbsp; Total Employee Count List</h1></div>
                        <div class="col-md-2">
                      <div class="form-group">
                                <label>Company</label>
                            <select id="ddlCompany" class="form-control form-control-sm"></select>
                      </div>
                        </div>

                    </div>

                    <div class="row" style="padding: 5px;">
                        <div class="col-md-10"></div>
                        <div class="col-md-2">
                            
                            <input type="button" id="btngv_Table01Export" onclick="tableToExcel('testTable', 'W3C Example Table')" title="Export to Excel"   class="pull-right btnexcel " value=" " />
                                
                  
                        </div>
                    </div>
                    
                       <div class="row">

                        <div class="col-md-12">
                            <div class=" text-center">
                            <asp:GridView ID="gv_Table01_DepartmentEmployment" runat="server" AutoGenerateColumns="False"
                                CssClass="table table-bordered text-center thead-dark table-striped table-hover"  >
                                 
                             
                            </asp:GridView>

                                
                                </div>
                        </div>
                    </div>
                    
                            <div class="row">
                             
                    

                        <div class="col-md-6">
                               <div class="row" style="padding: 5px;">
                        <div class="col-md-10"></div>
                        <div class="col-md-2">
                            
                            <input type="button" id="btngv_Table03Export"   title="Export to Excel"   class="pull-right btnexcel " value=" " />
                                
                  
                        </div>
                    </div>
                            <div class=" text-center">
                            <asp:GridView ID="gv_Table03_ProjectFundWisemalefemaleRatio" runat="server" AutoGenerateColumns="False"
                                CssClass="table table-bordered text-center thead-dark table-striped table-hover"  >
                                 
                                 
                            </asp:GridView>
                                </div>
                        </div>
                                    <div class="col-md-6">
                                        
                                           <div class="row" style="padding: 5px;">
                        <div class="col-md-10"></div>
                        <div class="col-md-2">
                            
                            <input type="button" id="btngv_Table04Export"   title="Export to Excel"   class="pull-right btnexcel " value=" " />
                                
                  
                        </div>
                    </div>
                            <div class=" text-center">
                            <asp:GridView ID="gv_Table04_ProjectWiseStaff" runat="server" AutoGenerateColumns="False"
                                CssClass="table table-bordered text-center thead-dark table-striped table-hover"  >
                                 
                                 
                            </asp:GridView>
                                </div>
                        </div>
                    </div>
                    
     <div class="row" style="padding: 5px;">
                        <div class="col-md-10"></div>
                        <div class="col-md-2">
                            
                            <input type="button" id="btngv_Table05Export"   title="Export to Excel"   class="pull-right btnexcel " value=" " />
                                
                  
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-md-12">
                            <div class=" text-center">
                              
                            <asp:GridView ID="gv_Table05_GradeWiseStaff" runat="server" AutoGenerateColumns="False"
                                CssClass="table table-bordered text-center thead-dark table-striped table-hover"  >
                                 

                            </asp:GridView>
                                </div>
                        </div>
                    </div>

                    </div>
                </div>
            </div>
        </div>
    <style>
        .bgcolornull {
            background-color: white;
        }
  #cpFormBody_gv_Table01_DepartmentEmployment td {
            border: 1px solid #ddd;
            padding: 8px;
        }
tr:hover {background-color:#F5F5DC!important;}
tr:nth-child(even) { background-color: #f2f2f2 !important;}
      #cpFormBody_gv_Table01_DepartmentEmployment > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF!important;
        }
  
 

        #cpFormBody_gv_Table01_DepartmentEmployment th {
            padding: 10px;
            border-style: none;

            background-color: #CCCCCC;
            color: black;
            font-weight: bold;
            font-size: 13px;
        }

            #cpFormBody_gv_Table01_DepartmentEmployment > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF!important;
        }



            #cpFormBody_gv_Table05_GradeWiseStaff td {
            border: 1px solid #ddd;
            padding: 8px;
        }


      #cpFormBody_gv_Table05_GradeWiseStaff > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF!important;
        }
  
 

        #cpFormBody_gv_Table05_GradeWiseStaff th {
            padding: 10px;
          

            background-color: #CCCCCC;
            color: black;
            font-weight: bold;
            font-size: 13px;
        }

            #cpFormBody_gv_Table05_GradeWiseStaff > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF!important;
        }







                #cpFormBody_gv_Table03_ProjectFundWisemalefemaleRatio td {
            border: 1px solid #ddd!important;
            padding: 8px;
        }


      #cpFormBody_gv_Table03_ProjectFundWisemalefemaleRatio > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF!important;
        }
  
 

        #cpFormBody_gv_Table03_ProjectFundWisemalefemaleRatio th {
            padding: 10px;
          

            background-color: #CCCCCC;
            color: black;
            font-weight: bold;
            font-size: 13px;
        }

            #cpFormBody_gv_Table03_ProjectFundWisemalefemaleRatio > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF!important;
        }


                #cpFormBody_gv_Table04_ProjectWiseStaff td {
            border: 1px solid #ddd;
            padding: 8px;
        }


      #cpFormBody_gv_Table04_ProjectWiseStaff > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF!important;
        }
  
 

        #cpFormBody_gv_Table04_ProjectWiseStaff th {
            padding: 10px;
          

            background-color: #CCCCCC;
            color: black;
            font-weight: bold;
            font-size: 13px;
        }

            #cpFormBody_gv_Table04_ProjectWiseStaff > tbody > tr:not(th):nth-child(odd) {
            background-color: #DFDFDF!important;
        }


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
    <%--<script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.8.0/Chart.bundle.js" integrity="sha256-qSIshlknROr4J8GMHRlW3fGKrPki733tLq+qeMCR05Q=" crossorigin="anonymous"></script>--%>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.8.0/Chart.bundle.min.js" integrity="sha256-xKeoJ50pzbUGkpQxDYHD7o7hxe0LaOGeguUidbq6vis=" crossorigin="anonymous"></script>
    <%--<script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.8.0/Chart.js" integrity="sha256-arMsf+3JJK2LoTGqxfnuJPFTU4hAK57MtIPdFpiHXOU=" crossorigin="anonymous"></script>--%>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.8.0/Chart.min.js" integrity="sha256-Uv9BNBucvCPipKQ2NS9wYpJmi8DTOEfTA/nH2aoJALw=" crossorigin="anonymous"></script>
    <%--<script src="https://cdn.jsdelivr.net/gh/emn178/chartjs-plugin-labels/src/chartjs-plugin-labels.js" integrity="sha256-Uv9BNBucvCPipKQ2NS9wYpJmi8DTOEfTA/nH2aoJALw=" crossorigin="anonymous"></script>--%>
    <%--<script src="https://cdn.jsdelivr.net/gh/emn178/chartjs-plugin-labels/src/chartjs-plugin-labels.js"></script>--%>
    
    


    <script>

        $(document).ready(function() {


            $(document).ajaxStart(function() {
                $("#coverScreen").show();
            });

            $(document).ajaxStop(function() {
                $("#coverScreen").hide();
            });

            $.ajax({
                type: "POST",
                async: false,
                url: "SummaryReportPage.aspx/LoadCompany",
                dataType: "JSON",
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $('#ddlCompany').empty();
                     

                    var result = data.d;

                    var selectOptions = '';
                    for (var i in result) {
                        selectOptions += '<option value="' + result[i].Value + '">' + result[i].TextField + '</option>';
                    }
                    $('#ddlCompany').append(selectOptions);
                   

                },
                error: function (err) {
                    alert(err);
                }
            });
        });


        $(document).ready(function () {
            //$("#ddlDept")[0].selectedIndex = 1;
            //var selectedDept = $('#ddlDept').val();
            //$('#ddlDept').val(selectedDept).trigger('change');////Dept val 14 is for Human Resource



            var comId = $('#ddlCompany').val();
            $('#ddlCompany').val(comId).trigger('change');
           

        });

        $('#ddlCompany').on('change',
            function () {
       

                var comId = $('#ddlCompany').val();
                $.ajax({
                    type: "POST",
                    async: false,
                    dataType: "JSON",
                    url: "SummaryReportPage.aspx/GetTable01_DepartmentEmploymentData",

                    contentType: "application/json;charset=utf-8",
                    data: JSON.stringify({ "comId": comId }),

                    success: function (data) {

                       
                        $("#cpFormBody_gv_Table01_DepartmentEmployment").empty();


                        if (data.d.length > 0) {

                            $("#cpFormBody_gv_Table01_DepartmentEmployment").append(" <thead> <tr>  <th colspan='8' style=' background-color: #EBF1DE;'> Department & employment type wise # of staff</th> < </tr> " +
                                "<tr><th>Sl</th><th>Department</th><th>Permanent</th> <th>Contractual</th>   <th>Sub Total</th>  <th>Casual</th>   <th> All Total</th></tr> </thead>");
                            for (var i = 0; i < data.d.length; i++) {
                     
                                $("#cpFormBody_gv_Table01_DepartmentEmployment").append("<tr><td>" +
                                    data.d[i].ItemNo + "</td> <td>" +
                                    data.d[i].DepartmentName + "</td> <td id='lblPermnaent'>" +
                                    data.d[i].Permnaent + "</td> <td id='lblCon'>" +
                                    data.d[i].Contractual + "</td> <td id='lblSubTotal'>" +
                                    data.d[i].SubTotal + "</td> <td id='lblCasual'>" +
                                    data.d[i].Casual + "</td> <td id='lblAllTotal'>" +
                                    data.d[i].AllTotal + "</td></tr>");


                            }
                            $("#cpFormBody_gv_Table01_DepartmentEmployment").append("  <tfoot><tr> <th colspan='2' >Total Staffs</th>  <th><span id='lblsumPermnaent'></span></th> <th><span id='lblsumContract'></span></th> <th><span id='lblSumSubTotal'></span></th>  <th><span id='lblSumCasual'></span></th> <th><span id='lblSumAllTotal'></span></th> </tr> </tfoot>");
                            debugger;
                            var ContractualgrnTotal = 0;
                            $("[id*=lblCon]").each(function() {
                                ContractualgrnTotal = ContractualgrnTotal + parseInt($(this).html());
                            });
                            $("[id*=lblsumContract]").html(" " + ContractualgrnTotal.toString());


                            var permnaentgrnTotal = 0;
                            $("[id*=lblPermnaent]").each(function () {
                                permnaentgrnTotal = permnaentgrnTotal + parseInt($(this).html());
                            });
                            $("[id*=lblsumPermnaent]").html(" " + permnaentgrnTotal.toString());



                            var grndSumSubTota = 0;
                            $("[id*=lblSubTotal]").each(function () {
                                grndSumSubTota = grndSumSubTota + parseInt($(this).html());
                            });
                            $("[id*=lblSumSubTotal]").html(" " + grndSumSubTota.toString());


                            var grSumCasual = 0;
                            $("[id*=lblCasual]").each(function () {
                                grSumCasual = grSumCasual + parseInt($(this).html());
                            });
                            $("[id*=lblSumCasual]").html(" " + grSumCasual.toString());


                            var grSumTotal = 0;
                            $("[id*=lblAllTotal]").each(function () {
                                grSumTotal = grSumTotal + parseInt($(this).html());
                            });
                            $("[id*=lblSumAllTotal]").html(" " + grSumTotal.toString());
                        }
                    },
                    error: function (result) {


                    }


                });
            });


     
        $('#ddlCompany').on('change',
            function () {
                 

                var comId = $('#ddlCompany').val();
               
                    $.ajax({
                    type: "POST",
                    async: false,
                    dataType: "JSON",
                    url: "SummaryReportPage.aspx/GeBindTable03_PrjectWiseMaleFemaleData",

                    contentType: "application/json;charset=utf-8",
                    data: JSON.stringify({ "comId": comId }),

                    success: function(data) {

                        $("#cpFormBody_gv_Table03_ProjectFundWisemalefemaleRatio").empty();

                        if (data.d.length > 0) {

                            $("#cpFormBody_gv_Table03_ProjectFundWisemalefemaleRatio").append(" <thead>  <tr>  <th colspan='7' style=' background-color: #EBF1DE;'> Project/Fund wise male-female ratio</th>  </tr> " +
                                "  <tr>  <th colspan='3' style=' background-color: #EBF1DE;'>  </th>  <th colspan='2' style=' background-color: #EBF1DE;'> Male</th>" +
                                "<th colspan='2' style=' background-color: #EBF1DE;'> Female</th>  </tr>" +
                                "" +
                                "<tr><th>Sl</th><th>Project</th><th># of Employee</th> <th>Number</th>   <th>%</th> <th>Number</th>   <th>%</th>  </tr> </thead>");
                            for (var i = 0; i < data.d.length; i++) {

                                $("#cpFormBody_gv_Table03_ProjectFundWisemalefemaleRatio").append("<tr><td>" +
                                    data.d[i].ItemNo + "</td> <td>" +
                                    data.d[i].ProjectName + "</td> <td id='lbltThAllTotal' >" +
                                    data.d[i].AllTotal + "</td> <td id='lbltThMale' >" +
                                    data.d[i].Male + "</td> <td>" +
                                    data.d[i].MalePer + "</td> <td id='lbltThFemale'>" +
                                    data.d[i].Female + "</td> <td>" +
                                    data.d[i].FemalePer + "</td></tr>");


                            }

                            $("#cpFormBody_gv_Table03_ProjectFundWisemalefemaleRatio").append("  <tfoot><tr> <th colspan='2' >Total Staffs</th><th><span id='sumlbltThAllTotal'></span></th> <th><span id='lbltThMaleSum'></span></th> <th></th> <th><span id='lbltThFemaleSum'></span></th><th></th>  </tr> </tfoot>");
                          
                          
                            var grnTotalTTh = 0;
                            $("[id^=lbltThAllTotal]").each(function () {
                                grnTotalTTh = grnTotalTTh + parseInt($(this).html());
                            });
                            $("[id*=sumlbltThAllTotal]").html(" " + grnTotalTTh.toString());



                            var grndMaleTTh = 0;
                            $("[id=lbltThMale]").each(function () {
                                grndMaleTTh = grndMaleTTh + parseInt($(this).html());
                            });
                            $("[id=lbltThMaleSum]").html(" " + grndMaleTTh.toString());


                            var grSumlbltThFemale = 0;
                            $("[id=lbltThFemale]").each(function () {
                                grSumlbltThFemale = grSumlbltThFemale + parseInt($(this).html());
                            });
                            $("[id=lbltThFemaleSum]").html(" " + grSumlbltThFemale.toString());

                        }
                    },
                    error: function(result) {


                    }

                });
            
            });



        $('#ddlCompany').on('change',
            function () {
                 

                var comId = $('#ddlCompany').val();
                $.ajax({
                    type: "POST",
                    async: false,
                    dataType: "JSON",
                    url: "SummaryReportPage.aspx/GetTable04_ProjectWiseStaffData",

                    contentType: "application/json;charset=utf-8",
                    data: JSON.stringify({ "comId": comId }),

                    success: function (data) {

                        $("#cpFormBody_gv_Table04_ProjectWiseStaff").empty();

                        if (data.d.length > 0) {

                            $("#cpFormBody_gv_Table04_ProjectWiseStaff").append("  <thead> <tr>  <th colspan='8' style=' background-color: #EBF1DE;'> Project wise # of staffs in Progrm Division</th> < </tr> " +
                                "<tr><th>Sl</th><th>Project</th><th>Permanent</th> <th>Contractual</th>   <th>Sub Total</th>  <th>Casual</th>   <th> All Total</th></tr> </thead>");
                            for (var i = 0; i < data.d.length; i++) {

                                $("#cpFormBody_gv_Table04_ProjectWiseStaff").append("<tr><td>" +
                                    data.d[i].ItemNo + "</td> <td>" +
                                    data.d[i].ProjectName + "</td> <td id='tFoPermnaent'>" +
                                    data.d[i].Permnaent + "</td> <td id='tFoContractual'>" +
                                    data.d[i].Contractual + "</td> <td id='tFoSubTotal'>" +
                                    data.d[i].SubTotal + "</td> <td id='tFoCasual'>" +
                                    data.d[i].Casual + "</td> <td id='tFoAllTotal'>" +
                                    data.d[i].AllTotal + "</td></tr>");


                            }


                            $("#cpFormBody_gv_Table04_ProjectWiseStaff").append("  <tfoot><tr> <th colspan='2' >Total Staffs</th><th><span id='sumtFoPermnaent'></span></th> <th><span id='sumtFoContractual'></span></th> <th><span id='sumtFoSubTotal'></span></th> <th><span id='sumtFoCasual'></span></th> <th><span id='sumtFoAllTotal'></span></th>    </tr> </tfoot>");


                          
                            var tFoPermnaent = 0;
                            $("[id=tFoPermnaent]").each(function () {
                                tFoPermnaent = tFoPermnaent + parseInt($(this).html());
                            });
                            $("[id=sumtFoPermnaent]").html(" " + tFoPermnaent.toString());


                            var tFoContractual = 0;
                            $("[id=tFoContractual]").each(function () {
                                tFoContractual = tFoContractual + parseInt($(this).html());
                            });
                            $("[id=sumtFoContractual]").html(" " + tFoContractual.toString());


                            var tFoSubTotal = 0;
                            $("[id=tFoSubTotal]").each(function () {
                                tFoSubTotal = tFoSubTotal + parseInt($(this).html());
                            });
                            $("[id=sumtFoSubTotal]").html(" " + tFoSubTotal.toString());


                            var tFoCasual = 0;
                            $("[id=tFoCasual]").each(function () {
                                tFoCasual = tFoCasual + parseInt($(this).html());
                            });
                            $("[id=sumtFoCasual]").html(" " + tFoCasual.toString());


                            var tFoAllTotal = 0;
                            $("[id=tFoAllTotal]").each(function () {
                                tFoAllTotal = tFoAllTotal + parseInt($(this).html());
                            });
                            $("[id=sumtFoAllTotal]").html(" " + tFoAllTotal.toString());


                        }
                    },
                    error: function (result) {


                    }


                });
            });





        $('#ddlCompany').on('change',
            function () {
                 

                var comId = $('#ddlCompany').val();
                $.ajax({
                    type: "POST",
                    async: false,
                    dataType: "JSON",
                    url: "SummaryReportPage.aspx/GeBindTable05_GradeWiseStaffData",

                    contentType: "application/json;charset=utf-8",
                    data: JSON.stringify({ "comId": comId }),

                    success: function (data) {

                        $("#cpFormBody_gv_Table05_GradeWiseStaff").empty();

                        if (data.d.length > 0) {
                            //<th>Desig.</th>
                            $("#cpFormBody_gv_Table05_GradeWiseStaff").append("  <tr>  <th colspan='6' style=' background-color: #EBF1DE;'>Grade wise # of staffs</th>  <th colspan='4' style=' background-color: #EBF1DE;'>Grade wise # of male-female staffs</th>   </tr>" +
                                "<tr>  <th colspan='6' style=' background-color: #EBF1DE;'> </th>  <th colspan='2' style=' background-color: #EBF1DE;'>Male</th> <th colspan='2' style=' background-color: #EBF1DE;'>Female</th>  </tr>" +
                                " " +
                                "<tr><th>Sl</th><th>Grade</th> <th># of Staff</th>   <th>Permanent</th> <th>Contract</th>  <th>Casual</th>  <th> Number</th> <th> %</th>  <th> Number</th> <th> %</th></tr>");
                            for (var i = 0; i < data.d.length; i++) {

                                $("#cpFormBody_gv_Table05_GradeWiseStaff").append("<tr><td>" +
                                    data.d[i].ItemNo + "</td> <td>" +
                                    //data.d[i].Designation + "</td> <td>" +
                                    data.d[i].GradeName + "</td> <td id='AllTotal5'>" +
                                    data.d[i].AllTotal + "</td> <td  id='Permnaent5'>" +
                                    data.d[i].Permnaent + "</td> <td   id='Contractual5'>" +
                                    data.d[i].Contractual + "</td> <td   id='Casual5'>" +
                                    data.d[i].Casual + "</td> <td   id='Male5'>" +
                                    data.d[i].Male + "</td> <td>" +
                                    data.d[i].MalePer + "</td> <td  id='Female5'>" +
                                    data.d[i].Female + "</td> <td>" +
                      
                                    data.d[i].FeMalePer + "</td></tr>");


                            }

                            $("#cpFormBody_gv_Table05_GradeWiseStaff").append("  <tfoot><tr> <th colspan='2' >Total Staffs</th><th><span id='sumAllTotal5'></span></th> <th><span id='sumPermnaent5'></span></th> <th><span id='sumContractual5'></span></th> <th><span id='sumCasual5'></span></th>  <th><span id='sumMale5'></span></th> <th> </th>  <th><span id='sumFemale5'></span></th>  <th> </th>    </tr> </tfoot>");



                            var AllTotal5 = 0;
                            $("[id=AllTotal5]").each(function () {
                                AllTotal5 = AllTotal5 + parseInt($(this).html());
                            });
                            $("[id=sumAllTotal5]").html(" " + AllTotal5.toString());


                            var Permnaent5 = 0;
                            $("[id=Permnaent5]").each(function () {
                                Permnaent5 = Permnaent5 + parseInt($(this).html());
                            });
                            $("[id=sumPermnaent5]").html(" " + Permnaent5.toString());


                            var Contractual5 = 0;
                            $("[id=Contractual5]").each(function () {
                                Contractual5 = Contractual5 + parseInt($(this).html());
                            });
                            $("[id=sumContractual5]").html(" " + Contractual5.toString());


                            var Casual5 = 0;
                            $("[id=Casual5]").each(function () {
                                Casual5 = Casual5 + parseInt($(this).html());
                            });
                            $("[id=sumCasual5]").html(" " + Casual5.toString());


                            var Male5 = 0;
                            $("[id=Male5]").each(function () {
                                Male5 = Male5 + parseInt($(this).html());
                            });
                            $("[id=sumMale5]").html(" " + Male5.toString());



                            var Female5 = 0;
                            $("[id=Female5]").each(function () {
                                Female5 = Female5 + parseInt($(this).html());
                            });
                            $("[id=sumFemale5]").html(" " + Female5.toString());

                        }
                    },
                    error: function (result) {


                    }


                });
            });

        $("body").on("click", "#btngv_Table01Export", function () {
            $("[id*=gv_Table01_DepartmentEmployment]").table2excel({
                filename: "Department & employment type wise # of staff",

                preserveColors:true,
                name:"Worksheet Name",
          
                fileext:".xls",
                exclude_img:true,
 
                exclude_links:true,
                exclude_inputs:true

            });
        });



        $("body").on("click", "#btngv_Table03Export", function () {
            $("[id*=gv_Table03_ProjectFundWisemalefemaleRatio]").table2excel({
                filename: "Project/Fund wise male-female ratio",

                preserveColors: true,
                name: "Worksheet Name",

                fileext: ".xls",
                exclude_img: true,

                exclude_links: true,
                exclude_inputs: true

            });
        });



        $("body").on("click", "#btngv_Table04Export", function () {
            $("[id*=gv_Table04_ProjectWiseStaff]").table2excel({
                filename: "Department & employment type wise # of staff",

                preserveColors: true,
                name: "Worksheet Name",

                fileext: ".xls",
                exclude_img: true,

                exclude_links: true,
                exclude_inputs: true

            });
        });


        $("body").on("click", "#btngv_Table05Export", function () {
            $("[id*=gv_Table05_GradeWiseStaff]").table2excel({
                filename: "Grade wise # of staffs",

                preserveColors: true,
                name: "Worksheet Name",

                fileext: ".xls",
                exclude_img: true,

                exclude_links: true,
                exclude_inputs: true

            });
        });


    </script>

 

  <script>
       
      //$(document).ready(function () {
      //    $('#cpFormBody_gv_Table01_DepartmentEmployment thead th').each(function (i) {
      //        calculateColumn(i);
      //    });
      //});
 
      //function calculateColumn(index) {
      //    var total = 0;
      //    $('#cpFormBody_gv_Table01_DepartmentEmployment tr').each(function () {
              
      //       var value = parseInt($('td', this).eq(index).text());
      //     //    var value = parseInt(currentRow.find("td:eq(10)").text());
      //    //    var value = parseInt($row.find('td:nth-child(5)').eq(index).text());
      //    //    var value = parseInt($row.find('td:nth-child(5)').eq(index).text());
      //        if (!isNaN(value)) {
      //            total += value;
      //        }
      //    });
      //    $('#cpFormBody_gv_Table01_DepartmentEmployment tfoot td').eq(index).text('Total: ' + total);
      //}




      $(document).ready(function () {
          // Setup - add a text input to each footer cell
         
      });
  </script>

<%--<input type="button" onclick="tableToExcel('testTable', 'W3C Example Table')" value="Export to Excel">--%>

  <script src="tableexport-xls-bold-headers.js"></script>

    <script src="../Assets/table2excel.js"></script>
   
</asp:Content>

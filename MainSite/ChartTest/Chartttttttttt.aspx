<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Chartttttttttt.aspx.cs" Inherits="ChartTest_Chartttttttttt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
      <script src="../Assets/js/vendors/jquery/jquery.min.js"></script>
         <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>--%>
    <%--<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.8.0/Chart.css" integrity="sha256-IvM9nJf/b5l2RoebiFno92E5ONttVyaEEsdemDC6iQA=" crossorigin="anonymous" />--%>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.8.0/Chart.min.css" integrity="sha256-aa0xaJgmK/X74WM224KMQeNQC2xYKwlAt08oZqjeF0E=" crossorigin="anonymous" />
     <link rel="stylesheet" href="../Assets/css/styles2c70.css?v=1.0.3">
    
    
      <%--<script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.8.0/Chart.bundle.js" integrity="sha256-qSIshlknROr4J8GMHRlW3fGKrPki733tLq+qeMCR05Q=" crossorigin="anonymous"></script>--%>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.8.0/Chart.bundle.min.js" integrity="sha256-xKeoJ50pzbUGkpQxDYHD7o7hxe0LaOGeguUidbq6vis=" crossorigin="anonymous"></script>
    <%--<script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.8.0/Chart.js" integrity="sha256-arMsf+3JJK2LoTGqxfnuJPFTU4hAK57MtIPdFpiHXOU=" crossorigin="anonymous"></script>--%>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.8.0/Chart.min.js" integrity="sha256-Uv9BNBucvCPipKQ2NS9wYpJmi8DTOEfTA/nH2aoJALw=" crossorigin="anonymous"></script>
    <%--<script src="https://cdn.jsdelivr.net/gh/emn178/chartjs-plugin-labels/src/chartjs-plugin-labels.js" integrity="sha256-Uv9BNBucvCPipKQ2NS9wYpJmi8DTOEfTA/nH2aoJALw=" crossorigin="anonymous"></script>--%>
    <%--<script src="https://cdn.jsdelivr.net/gh/emn178/chartjs-plugin-labels/src/chartjs-plugin-labels.js"></script>--%>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
          <select id="ddlCompany" class="form-control form-control-sm"></select>
        <canvas id="genderPieChartByDept"></canvas>
    </div>
    </form>
</body>
    
    <script>
        $(document).ready(function() {
            $.ajax({
                type: "POST",
                async: false,
                url: "Chartttttttttt.aspx/LoadCompany",
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
              debugger;
              var comId = $('#ddlCompany').val();

              $.ajax({
                  async: false,
                  type: "POST",
                  url: "Chartttttttttt.aspx/GetEmpCountForDept",
                  dataType: "JSON",
                  contentType: "application/json;charset=utf-8",
                  data: JSON.stringify({ "comId": comId}),
                  success: function (data) {
                      $('#genderPieChartByDept').empty();
                      var result = data.d;
                      var piChartData = [];
                      var piChartLabel = [];
                      for (var i in result) {
                          piChartData.push(result[i].Value);
                          piChartLabel.push(result[i].TextField);
                      }
                      data = {
                          datasets: [
                              {
                                  data: piChartData,
                                  backgroundColor: ['rgba(33, 199, 199, 1)', 'rgba(5, 155, 255, 1)'],
                                  hoverBackgroundColor: [
                                      "#61b0ff"
                                  ],
                                  labels: piChartLabel
                              }
                          ],
                          // These labels appear in the legend and in the tooltips when hovering different arcs
                          labels: piChartLabel
                      };

                      var ctxPie = document.getElementById('genderPieChartByDept').getContext('2d');
                      var myPieChart = new Chart(ctxPie, {
                          type: 'pie',
                          data: data, animationEnabled: true,
                          legend: {
                              verticalAlign: "center",
                              horizontalAlign: "left",
                              fontSize: 20,
                              fontFamily: "Helvetica"
                          },
                          theme: "light2",

                          title: {
                              display: true

                          },
                          animation: {
                              animateScale: true,
                              animateRotate: true
                          },
                          options: {

                              showAllTooltips: true

                          }
                      });
                      if (window.myLine != undefined) {
                          window.myLine.destroy();
                      }

                      window.myLine = myPieChart;

                  },
                  error: function (err) {
                      alert(err);
                  }
              });
          });
        
    </script>
</html>

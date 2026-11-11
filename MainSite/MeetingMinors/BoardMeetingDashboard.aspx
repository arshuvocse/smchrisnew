<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MainMasterPage.master" AutoEventWireup="true" CodeFile="BoardMeetingDashboard.aspx.cs" Inherits="MeetingMinors_BoardMeetingDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
      <link href="../Assets/EvenCal/main.css" rel="stylesheet" />
    <script src="../Assets/EvenCal/main.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpFormBody" Runat="Server">
     <div class="content" id="content">
        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
                <!-- PAGE HEADING -->
                <div class="page-heading">
                    <div class="page-heading__container">
                        <div class="icon">
                            <img src="../Report_Pages/app.png" width="20px" />
                        </div>
                        <span></span>
                        <h1 class="title" style="font-size: 18px; padding-top: 9px;">Board-Meeting Dashboard</h1>
                    </div>
                    <div class="page-heading__container float-right d-none d-sm-block">
                          <%--<asp:LinkButton ID="detailsViewButton" 	
   CssClass="btn btn-sm btn-outline-success " runat="server" OnClick="detailsViewButton_OnClick" >
                            
                            
                            &#8920; &nbsp;	Back To List

                        </asp:LinkButton>--%>
                    </div>
                </div>

            
                <!-- //END PAGE HEADING -->

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-body" style="background:white!important">
                            
                            
                            
                         
                                    <div id='calendar'></div>
                                  
                                </div>
                            </div>
                        </div>
                    </div>
       
    
    <script>

        document.addEventListener('DOMContentLoaded', function () {
            var calendarEl = document.getElementById('calendar');
            debugger;
            var sresult;
         

            $.ajax({


                type: "POST",
                url: "BoardMeetingDashboard.aspx/getMeetingCalender",
                data: JSON.stringify({}),
                dataType: "JSON",
                contentType: "application/json;charset=utf-8",
                async: false,
                success: function (data) {

                    debugger;
                    sresult = data.d;
            


                }
            });
            //debugger;
          

            
            var calendar = new FullCalendar.Calendar(calendarEl, {
                headerToolbar: {
                    left: 'prevYear,prev,next,nextYear today',
                    center: 'title',
                    right: 'dayGridMonth,dayGridWeek,dayGridDay'
                },
                //initialDate: '2021-02-13',
                navLinks: true, // can click day/week names to navigate views
                editable: false,
                dayMaxEvents: true,
                // allow "more" link when too many events
                center: 'title',
                events: sresult 
                });

            calendar.render();

        });

</script>
<style>

  body {
   
    font-family: Arial, Helvetica Neue, Helvetica, sans-serif;
    font-size: 14px;
  }

  #calendar {
    /*max-width: 1100px;
    margin: 0 auto;*/
  }
 .fc-h-event .fc-event-title{
        word-wrap: break-word!important;
        height: 110px!important;
           font-size: 1em!important;
     color: white!important;
     text-transform: capitalize!important;
     text-shadow: 0 0 3px black;
  }
 .fc-title{
    font-size: .7em!important;
}
     
 .fc-header-title{
       color: #0052ba;
       background: #000;
       font-size: 20px;
}
     .fc-event-time, .fc-event-title {
padding: 0 1px!important;
white-space: normal!important; 
}

     .fc-header-title{
       color: #0052ba!important;
       background: #000!important;
       font-size: 11px!important;
}
</style>
</asp:Content>


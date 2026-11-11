//console.log('InterviewCandidateGrading ok');
$(document).ready(function () {
    //LoadInterviewCandidateGrading();
});



function LoadInterviewCandidateGrading() {
    $.ajax({
        type: "POST",
        url: "InterviewCandidateGrading.aspx/LoadInterviewCandidateGrading",
        dataType: "JSON",
        contentType: "application/json;charset=utf-8",
        //data: JSON.stringify({ "EMP_ID": EMP_ID }),
        success: function (data) {
            //console.log(data);
            var result = data.d;
            
            formatDataTable(result);
        },
        error: function (err) {
            alert(err);
        }
    });
}

function formatDataTable(data) {
    //debugger;
    let thead = '';
        thead = '<thead>' +
            '<tr>' +
            '<th width="13%">CandidateName</th>' +
            '<th width="9%">Written Marks</th>' +
            '<th width="9%">Viva Marks</th>' +
            '<th width="9%">Other Marks</th>' +
            '<th width="9%">Interview Marks</th>' +
            '<th width="9%">Total Marks</th>' +
            '<th width="9%">Grade</th>' +
            '<th width="10%">IsRecommended</th>' +
            '<th width="10%">Waiting</th>' +
            '<th width="13%">Remarks</th>' +
            '</tr>' +
            '</thead > ';
    

    let tds = BindApprovalTable(data);
    let tbl = '<table id="tbl_Idea" class=" table table-bordered text-center thead-dark table-hover table-striped" style="table-layout: fixed; width:100%;">' +
        thead +
        '<tbody id="tbody_Idea">' +
        tds +
        '</tbody></table >';

    $('#tbl_container').empty();
    $('#tbl_container').append(tbl);
    $('#tbl_Idea').DataTable({
        "order": []
    });
}

function BindApprovalTable(allTr) {
    var contentTrs = '';
    if (allTr.length > 0) {
        for (var i = 0; i < allTr.length; i++) {
            let thisTr = allTr[i];
            contentTrs += AddNewTr(i + 1, thisTr);
        }
    } else {
        contentTrs += '<tr><td colspan="10"><div style="text-align:center;font-size: 2rem;color: red;">No Data Found...</div></td></tr>';
    }
    return contentTrs;
}
function AddNewTr(sl, data) {
    var newTr = '';
    newTr = '<tr data-pkid="' + data.InterviewCandidateGradingId + '" data-JobID="' + data.JobID + '">' +
            '<td data-candidateid="' + data.CandidateID + '">' + data.CandidateName + '</td>' +
            '<td>' + data.WrittenMarks + '</td>' +
            '<td>' + data.VivaMarks + '</td>' +
            '<td>' + data.OtherMarks + '</td>' +
            '<td>' + data.InterviewMarks + '</td>' +
            '<td>' + data.TotalMarks + '</td>' +
            '<td>' + data.LetterGrade + '</td>' +
            '<td><input type="checkbox"  class="btnRIsRecommended btn btn-xs btn-warning"></input></td>' +
            '<td><input type="checkbox"  class="btnRIsWaiting btn btn-xs btn-warning"></input></td>' +
            '<td><input type="text"  class="btnRIsApproved form-control form-control-sm" style="max-width: 100px;" value="' + data.Remarks + '"></input></td>' +
            '</tr>';
    return newTr;
}

$('#tbl_container').on('click', '.btnRIsRecommended', function (e) {
    //e.preventDefault();
    //debugger;
    let thisTr = $(this).closest('tr');
    var InterviewCandidateGradingId = thisTr.data('pkid');
    var JobID = thisTr.data('jobid');

    var CandidateID = thisTr.find('td:eq(0)').data('candidateid');
    var Attitude = thisTr.find('td:eq(1)').html();
    var Language = thisTr.find('td:eq(2)').html();
    var TechnicalSkill = thisTr.find('td:eq(3)').html();
    var IQ = thisTr.find('td:eq(4)').html();
    var GeneralKnowledge = thisTr.find('td:eq(5)').html();
    var Others = thisTr.find('td:eq(6)').html();
    var TimeSence = thisTr.find('td:eq(7)').html();
    var TotalMarks = thisTr.find('td:eq(8)').html();
    var LetterGrade = thisTr.find('td:eq(9)').html();

    var IsRecommended = thisTr.find('td:eq(10)').find('.btnRIsRecommended').prop('checked');

    var CandidateGrading = {
        InterviewCandidateGradingId: InterviewCandidateGradingId,
        CandidateID: CandidateID,
        JobID:JobID,
        Attitude: Attitude,
        Language: Language,
        TechnicalSkill: TechnicalSkill,
        IQ: IQ,
        GeneralKnowledge: GeneralKnowledge,
        Others: Others,
        TimeSence: TimeSence,
        TotalMarks: TotalMarks,
        LetterGrade: LetterGrade,
        IsRecommended: IsRecommended

    }
    console.log(CandidateGrading);

    $.ajax({
        type: "POST",
        url: "InterviewCandidateGrading.aspx/RIsRecommended",
        dataType: "JSON",
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify({ "CandidateGrading": CandidateGrading }),
        success: function (data) {
            console.log(data);
            var result = data.d;

            if (parseInt(result) > 0) {

                alert('Operation Successful...');
                thisTr.data('pkid', result);
            }
        },
        error: function (err) {
            alert(err);
        }
    });
    
});

$('#tbl_container').on('click', '.btnRDelete', function (e) {
    e.preventDefault();
    //debugger;
    let thisTr = $(this).closest('tr');
    var MPBudgetId = thisTr.data('pkid');
    //console.log(MPBudgetId);
    $.ajax({
        type: "POST",
        url: "MPBudgetList.aspx/DeleteMPBudgetRowByMId",
        dataType: "JSON",
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify({ "MPBudgetId": MPBudgetId }),
        success: function (data) {
            console.log(data);
            var result = data.d;

            if (result == 'ok') {
                $(thisTr).remove();
            }
        },
        error: function (err) {
            alert(err);
        }
    });
});






var parameter = Sys.WebForms.PageRequestManager.getInstance();

parameter.add_endRequest(function () {
    //jquery code again for working after postback
    $('#btnSearch').on('click', function (e) {
        //debugger;
        var CompanyId = $("#cpFormBody_IVSearchControl_ddlCompany").val();
        var JobID = $("#cpFormBody_IVSearchControl_hfJobID").val();
        $.ajax({
            type: "POST",
            url: "InterviewCandidateGrading.aspx/LoadInterviewCandidateGrading",
            dataType: "JSON",
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify({ "CompanyId": CompanyId, "JobID": JobID }),
            success: function (data) {
                console.log(data);
                var result = data.d;

                formatDataTable(result);
            },
            error: function (err) {
                alert(err);
            }
        });



    });
});


//$(document).ready(function () {
//$('#btnSearch').on('click', function (e) {
//    //e.preventDefault();
//    //debugger;
//    alert('ook');
//    console.log($("#cpFormBody_ddlCompany").val());
//});
//});
//'#<%= ddlCompany.ClientID %>'
//$(document).ready(function () {
//    $(<%=lstBoxTest.ClientID%>).SumoSelect({ selectAll: true });
//});
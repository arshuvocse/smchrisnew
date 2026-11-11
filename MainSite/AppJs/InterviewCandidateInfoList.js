console.log('InterviewCandidateInfoList ok');
$(document).ready(function () {
    LoadInterviewCandidateInfoList();
});

function LoadInterviewCandidateInfoList() {
    $.ajax({
        type: "POST",
        url: "InterviewCandidateInfoList.aspx/LoadInterviewCandidateInfoList",
        dataType: "JSON",
        contentType: "application/json;charset=utf-8",
        //data: JSON.stringify({ "EMP_ID": EMP_ID }),
        success: function (data) {
            console.log(data);
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
            //'<th width="1%">SL#</th>' +
            //'<th width="5%">Company</th>' +
            '<th width="10%">Candidate Name</th>' +
            '<th width="10%">Address</th>' +
            '<th width="8%">PhoneNo</th>' +
            '<th width="8%">Email</th>' +
            '<th width="8%">Years Of Exp</th>' +
            '<th width="8%">Expected Salary</th>' +
            '<th width="8%">Current Salary</th>' +
            //'<th width="5%">Entry By</th>' +
            //'<th width="8%">Entry Date</th>' +
            //'<th width="7%">Update by</th>' +
            //'<th width="8%">Update Date</th>' +
            '<th width="6%">Edit</th>' +
            '<th width="6%">Delete</th>' +
            '<th width="6%">CV</th>' +
            '</tr>' +
            '</thead > ';
    

    let tds = BindApprovalTable(data);
    let tbl = '<table id="tbl_Idea" class="mdl-data-table" style="table-layout: fixed; width:100%;">' +
        thead +
        '<tbody id="tbody_Idea">' +
        tds +
        '</tbody></table >';

    $('#tbl_container').empty();
    $('#tbl_container').append(tbl);
    $('#tbl_Idea').DataTable();
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
    newTr = '<tr data-pkid="' + data.CandidateID + '">' +
            //'<td>' + sl + '</td>' +
            //'<td>' + data.CompanyShortName + '</td>' +
            '<td>' + data.CandidateName + '</td>' +
            '<td>' + data.Address + '</td>' +
            '<td>' + data.PhoneNo + '</td>' +
            '<td>' + data.EmailAdress + '</td>' +
            '<td>' + data.TotalYearsOfExp + '</td>' +
            '<td>' + data.ExpectedSalary + '</td>' +
            '<td>' + data.CurrentSalary + '</td>' +
            //'<td>' + data.EntryBy + '</td>' +
            //'<td>' + data.EntryDate + '</td>' +
            //'<td>' + data.Updateby + '</td>' +
            //'<td>' + data.UpdateDate + '</td>' +
            '<td><button  class="btnREdit btn btn-xs btn-warning">Edit</button></td>' +
            '<td><button  class="btnRDelete btn btn-xs btn-danger">Delete</button></td>' +
            '<td><button  class="btnCVDownload btn btn-xs btn-warning">Download</button></td>' +
            '</tr>';
    

    return newTr;
}

$('#tbl_container').on('click', '.btnREdit', function (e) {
    e.preventDefault();
    let thisTr = $(this).closest('tr');
    var CandidateID = thisTr.data('pkid');
    window.location.href = "/Inverview/InterviewCandidateInfo.aspx?mid=" + CandidateID;
});

$('#tbl_container').on('click', '.btnRDelete', function (e) {
    e.preventDefault();
    let thisTr = $(this).closest('tr');
    var CandidateID = thisTr.data('pkid');
    $.ajax({
        type: "POST",
        url: "InterviewCandidateInfoList.aspx/DeleteInterviewCandidateInfo",
        dataType: "JSON",
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify({ "CandidateID": CandidateID }),
        success: function (data) {
            //console.log(data);
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
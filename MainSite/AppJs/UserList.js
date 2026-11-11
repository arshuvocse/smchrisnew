console.log('UserList ok');
$(document).ready(function () {
    LoadUserList();
});

function LoadUserList() {
    $.ajax({
        type: "POST",
        url: "UserList.aspx/LoadUserList",
        dataType: "JSON",
        contentType: "application/json;charset=utf-8",
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
            '<th width="10%">UserName</th>' +
            //'<th width="10%">Company</th>' +
            '<th width="10%">User Type</th>' +
            '<th width="10%">ContactNo</th>' +
            '<th width="10%">Email</th>' +
            '<th width="6%">IsActive</th>' +
            //'<th width="10%">ActiveDate</th>' +
            //'<th width="10%">InActiveDate</th>' +
            '<th width="6%">Remarks</th>' +
            '<th width="6%">Edit</th>' +
            '<th width="6%">Delete</th>' +
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
    newTr = '<tr data-pkid="' + data.UserId + '">' +
            '<td>' + data.UserName + '</td>' +
            //'<td>' + data.CompanyName + '</td>' +
            '<td>' + data.UserType + '</td>' +
            '<td>' + data.ContactNo + '</td>' +
            '<td>' + data.Email + '</td>' +
            '<td>' + data.IsActive + '</td>' +
            //'<td>' + data.ActiveDate + '</td>' +
            //'<td>' + data.InActiveDate + '</td>' +
            '<td>' + data.Remarks + '</td>' +
            '<td><button  class="btnREdit btn btn-xs btn-warning">Edit</button></td>' +
            '<td><button  class="btnRDelete btn btn-xs btn-danger">Delete</button></td>' +
            '</tr>';
    

    return newTr;
}

$('#tbl_container').on('click', '.btnREdit', function (e) {
    e.preventDefault();
    let thisTr = $(this).closest('tr');
    var UserId = thisTr.data('pkid');
    window.location.href = "/UserSetup/UserEntry.aspx?mid=" + UserId;
});

$('#tbl_container').on('click', '.btnRDelete', function (e) {
    e.preventDefault();
    let thisTr = $(this).closest('tr');
    var UserId = thisTr.data('pkid');
    $.ajax({
        type: "POST",
        url: "UserList.aspx/DeleteUser",
        dataType: "JSON",
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify({ "UserId": UserId }),
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
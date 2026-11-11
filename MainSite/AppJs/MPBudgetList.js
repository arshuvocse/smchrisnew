console.log('ok');
$(document).ready(function () {
    LoadMPBudgetTable();
});

function LoadMPBudgetTable() {
    $.ajax({
        type: "POST",
        url: "MPBudgetList.aspx/LoadMPBudgetTable",
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
            '<th width="20%">Company</th>' +
            '<th width="20%">Year</th>' +
            '<th width="20%">Budget Code</th>' +
            '<th width="20%">Department</th>' +
            '<th width="10%">Edit</th>' +
            '<th width="10%">Del</th>' +
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
        contentTrs += '<tr><td colspan="6"><div style="text-align:center;font-size: 2rem;color: red;">No Data Found...</div></td></tr>';
    }
    return contentTrs;
}
function AddNewTr(sl, data) {
    var newTr = '';
    newTr = '<tr data-pkid="' + data.MPBudgetMasterId + '">' +
            '<td>' + data.CompanyShortName + '</td>' +
            '<td>' + data.FinancialYearDesc + '</td>' +
            '<td>' + data.BudgetCode + '</td>' +
            '<td>' + data.DepartmentName + '</td>' +
            '<td><button  class="btnREdit btn btn-xs btn-warning">Edit</button></td>' +
            '<td><button  class="btnRDelete btn btn-xs btn-danger">Delete</button></td>' +
            '</tr>';
    

    return newTr;
}

$('#tbl_container').on('click', '.btnREdit', function (e) {
    e.preventDefault();
    //debugger;
    let thisTr = $(this).closest('tr');
    var MPBudgetId = thisTr.data('pkid');
    //console.log(MPBudgetId);
    window.location.href = "/MPBudget/MPBudgetEntry.aspx?mid=" + MPBudgetId;
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


$('#btnFilterSearch').on('click', function () {
    var company = $("[id$='ddlCompany'] option:selected").val();
    var dept = $("[id$='ddlDepartment'] option:selected").val();
    var finyear = $("[id$='ddlFinYear'] option:selected").val();

    if (company=='-1') {
        alert('Company required...');
        return;
    }


    $.ajax({
        type: "POST",
        url: "MPBudgetList.aspx/FilterMPBudgetTable",
        dataType: "JSON",
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify({ "company": company, "dept": dept, "finyear": finyear }),
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
var IsDateFilter = false;
var FromDate = "", ToDate = "";
var IsParty = false;
var PartyList = [];
var FullPartyList;
var ProductWiseGrid = [];
var PreviouslySelectedFromPickerDate = "", type = "";
var PreviouslySelectedToPickerDate = "";
var grid, dialog;

$(document).ready(function ()
{
    $(".preloader").hide();
    if ($("#GroupId").val() != 0)
    {
        $("#PartyName").prop("readonly", "readonly");
        $('#IsParty').attr('disabled', 'disabled');
    }
    else
    {
        $("#PartyName").prop("readonly", "");
        $('#IsParty').prop("readonly", "");
        $("#PartyName").val('All');
        $('#IsParty').removeAttr('disabled');
    }
    GetAllParty();

    $('#IsDateFilter').on('ifChecked', function () {
        IsDateFilter = true;
        FromDate = "";
        ToDate = "";
        $("#StartDate").val("All");
        $("#StartDate").prop("readonly", "readonly");
        $("#EndDate").val("All");
        $("#EndDate").prop("readonly", "readonly");
    });

    $('#IsDateFilter').on('ifUnchecked', function () {
        FromDate = "";
        ToDate = "";
        IsDateFilter = false;
        $("#StartDate").val("");
        $("#StartDate").prop("readonly", "");
        $("#EndDate").val("");
        $("#EndDate").prop("readonly", "");
    });

    $("#StartDate").datetimepicker({
        format: 'DD-MM-YYYY',
        widgetPositioning: {
            horizontal: 'auto',
            vertical: 'bottom'
        }

    }).on('dp.change', function (e) {
        var selectedDate = e.date._d;
        var lengthOfMonth = ((selectedDate.getMonth() + 1).toString()).length;
        var twoDigitMonth = (lengthOfMonth > 1) ? (selectedDate.getMonth() + 1) : '0' + (selectedDate.getMonth() + 1);
        var lengthOfDate = ((selectedDate.getDate()).toString()).length;
        var twoDigitdate = (lengthOfDate > 1) ? (selectedDate.getDate()) : '0' + (selectedDate.getDate());

        var newFromDate = twoDigitdate + "-" + twoDigitMonth + "-" + selectedDate.getFullYear();

        FromDate = newFromDate;
        PreviouslySelectedFromPickerDate = newFromDate;
        ToDate = PreviouslySelectedToPickerDate;
    });

    $("#EndDate").datetimepicker({
        format: 'DD-MM-YYYY',
        widgetPositioning: {
            horizontal: 'auto',
            vertical: 'bottom'
        }

    }).on('dp.change', function (e) {
        var selectedDate = e.date._d;
        var lengthOfMonth = ((selectedDate.getMonth() + 1).toString()).length;
        var twoDigitMonth = (lengthOfMonth > 1) ? (selectedDate.getMonth() + 1) : '0' + (selectedDate.getMonth() + 1);

        var lengthOfDate = ((selectedDate.getDate()).toString()).length;
        var twoDigitdate = (lengthOfDate > 1) ? (selectedDate.getDate()) : '0' + (selectedDate.getDate());

        var newFromDate = twoDigitdate + "-" + twoDigitMonth + "-" + selectedDate.getFullYear();

        ToDate = newFromDate;
        PreviouslySelectedToPickerDate = newFromDate;
        FromDate = PreviouslySelectedFromPickerDate;
    });




    $('#IsParty').on('ifChecked', function () {
        IsParty = true;
        $("#PartyName").val('All');
        $("#PartyName").prop("readonly", "readonly");
        $("#PartyCode").val('0');
    });

    $('#IsParty').on('ifUnchecked', function () {
        IsParty = false;
        $("#PartyName").val('');
        $("#PartyName").prop("readonly", "");
        $("#PartyCode").val('');
    });

    $("#PartyName").autocomplete({
        source: function (request, response) {
            var results = $.ui.autocomplete.filter(PartyList, request.term);
            response(results.slice(0, 50));

        },
        minLength: 1,
        scroll: true,
        select: function (event, ui) {
            $("#PartyName").val(ui.item.label);
            SetSpecificCode("Party", ui.item.label);
            return false;
        },
    }).focus(function () {
        $(this).autocomplete("search", "");
    });

    $(".getReport").on('click', function () {
        $(".preloader").show();
        type = $(this).attr("data-type");
        ProductWiseGrid = [];
        fillGrid(type);
        PartyCode = $("#PartyCode").val();

        var DateError = false;
        if (FromDate != "" && FromDate != null && ToDate != null && ToDate != null) {
            var d1 = toDate(FromDate);
            var d2 = toDate(ToDate);

            if (d1 > d2) {

                DateError = true;
                $(".preloader").hide();
                OpenDialog("dialogMessage", "From Date should be less than To Date!", "false");
            }
            else {
                DateError = false;

            }
        }
        else {
            DateError = false;
            if (DateError == false) {
                if (IsDateFilter == false) {
                }
            }
            if (FromDate == "" || FromDate == null) {
                FromDate = "All";
            }
            if (ToDate == "" || ToDate == null) {
                ToDate = "All";
            }

        }

        if (DateError == false) {
            var InvoiceTypeVal = "";
            $.ajax({
                url: '/Report/GetPaymentSummaryReport',
                type: 'POST',
                data: { "FromDate": FromDate, "ToDate": ToDate, "PartyCode": PartyCode, "Type": type },
                async: false,
                dataType: "json",
                success: function (objResult) {
                    ProductWiseGrid = [];
                    if (objResult.length > 0) {
                        for (var i = 0; i < objResult.length; i++) {
                            ProductWiseGrid.push({ "BillDate": objResult[i].BillDate, "BillNo": objResult[i].BillNo, "BillBy": objResult[i].BillBy, "Name": objResult[i].Name, "Order": objResult[i].Order, "IDNo": objResult[i].IDNo, "IdName": objResult[i].IdName, "Amount": objResult[i].Amount, "Cash": objResult[i].Cash, "Cheque": objResult[i].Cheque, "dd": objResult[i].dd, "BankDeposit": objResult[i].BankDeposit, "CreditCard": objResult[i].CreditCard, "Cheque": objResult[i].Cheque, "DeditCard": objResult[i].DeditCard, "NetBanking": objResult[i].NetBanking, "Credit": objResult[i].Credit, "Wallet": objResult[i].Wallet });
                        }
                    }
                    fillGrid(type);
                    $(".preloader").hide();
                },
                error: function (xhr, data) {
                    console.log(xhr);
                    console.log("Error:", data);
                }
            });
        }
    });

});

function GetAllParty() {
    $.ajax({
        url: '/Report/GetAllPartyListForReports',
        dataType: 'JSON',
        method: 'GET',
        success: function (data) {
            FullPartyList = data;
            PartyList = [];
            if (data != null) {
                var i = 0;
                for (i = 0; i < data.length; i++) {
                    PartyList.push(data[i].PartyName);
                }
            }
        },
        error: function (error) {
            console.log(error);
        }
    });
}

function toDate(dateStr) {
    var parts = dateStr.split("-");
    return new Date(parts[2], parts[1] - 1, parts[0]);
}

function SetSpecificCode(type, label) {
    if (type == "Party") {
        for (var i = 0; i < FullPartyList.length; i++) {
            if (FullPartyList[i].PartyName == label) {
                $("#PartyCode").val(FullPartyList[i].PartyCode);
                PartyCode = FullPartyList[i].PartyCode;
                break;
            }
        }
    }
}

function OpenDialog(dialogId, Message, isConfirmation) {
    $("#" + dialogId).empty();
    $("#" + dialogId).append('<p>' + Message + '</p>');
    if (isConfirmation == "true") {
        $("#" + dialogId).dialog({
            modal: true,
            buttons: [{
                text: "Yes",
                id: "btnYes" + dialogId,
                click: function () {
                    IsYes = true;
                    $("#" + dialogId).dialog("close");
                }
            },
            {
                text: "No",
                id: "btnNo" + dialogId,
                click: function () {
                    IsYes = false;
                    $("#" + dialogId).dialog("close");
                }
            }
            ]
        });
        $(".ui-dialog-titlebar-close").empty();
        $(".ui-dialog-titlebar-close").append('<i class="fa fa-close"></i>');
    }
    else {


        $("#" + dialogId).dialog({
            modal: true,
            buttons: [{
                text: "OK",
                id: "btn" + dialogId,
                click: function () {
                    $("#" + dialogId).dialog("close");
                }
            }]
        });
        $(".ui-dialog-titlebar-close").empty();
        $(".ui-dialog-titlebar-close").append('<i class="fa fa-close"></i>');
    }

}

function fillGrid(type) {
    $("#noRecord").hide();
    if (grid != null) {
        grid.destroy();
        $('#grid').empty();

    }
    var billno = true;
    var billDate = true;
    var idno = true;
    var idname = true;
    var order = true;

    var cash = true;
    var cheque = true;
    var dd = true;
    var bank = true;
    var creditcard = true;
    var debitcard = true;
    var netbanking = true;
    var credit = true;
    var wallet = true;

    $('input[name="paymode"]:checked').each(function (i) {
        var val = $(this).val();

        if (val == "Cash") {
            cash = false;
        }
        else if (val == "Cheque") {
            cheque = false;
        }
        else if (val == "DD") {
            dd = false;
        }
        else if (val == "Bank Deposit") {
            bank = false;
        }
        else if (val == "Credit Card") {
            creditcard = false;
        }
        else if (val == "Debit Card") {
            debitcard = false;
        }
        else if (val == "Net Banking") {
            netbanking = false;
        }
        else if (val == "Credit") {
            credit = false;
        }
        else if (val == "Wallet") {
            wallet = false;
        }
    });

    if (type == "B") {
        billno = false;
        billDate = false;
        idno = false;
        idname = false;
        order = false;
    }
    else if (type == "D") {
        billDate = false;
    }

    grid = $('#grid').grid({
        dataSource: ProductWiseGrid,
        uiLibrary: 'bootstrap',
        headerFilter: true,
        columns: [
            { field: 'BillNo', title: 'BillNo', width: 100, sortable: true, hidden: billno, filterable: false },
            { field: 'BillDate', title: 'BillDate', width: 150, sortable: true, hidden: billDate, filterable: false },
            { field: 'BillBy', title: 'Bill By', width: 80, sortable: true, hidden: false, filterable: false },
            { field: 'Name', title: 'Name', width: 200, sortable: true, hidden: false, filterable: false },
            { field: 'Order', title: 'Order', width: 120, sortable: true, hidden: order, filterable: false },
            { field: 'IDNo', title: 'ID No.', width: 80, sortable: true, hidden: idno, filterable: false },
            { field: 'IdName', title: 'Name', width: 150, sortable: true, hidden: idname, filterable: false },
            { field: 'Amount', title: 'Bill Amount', width: 80, sortable: true, hidden: false, filterable: false },
            { field: 'Cash', title: 'Cash', width: 80, sortable: true, hidden: cash, filterable: false },
            { field: 'Cheque', title: 'Cheque', width: 80, sortable: true, hidden: cheque, filterable: false },
            { field: 'dd', title: 'DD', width: 80, sortable: true, hidden: dd, filterable: false },
            { field: 'BankDeposit', title: 'Bank Deposit', width: 80, sortable: true, hidden: bank, filterable: false },
            { field: 'CreditCard', title: 'Credit Card', width: 80, sortable: true, hidden: creditcard, filterable: false },
            { field: 'DeditCard', title: 'Dedit Card', width: 80, sortable: true, hidden: debitcard, filterable: false },
            { field: 'NetBanking', title: 'Net Banking', width: 80, sortable: true, hidden: netbanking, filterable: false },
            { field: 'Credit', title: 'Credit', width: 80, sortable: true, hidden: credit, filterable: false },
            { field: 'Wallet', title: 'Wallet', width: 80, sortable: true, hidden: wallet, filterable: false }
        ],
        pager: { limit: 20, sizes: [15, 20, 35, 50, 65, 80, 95, 100] }
    });
}
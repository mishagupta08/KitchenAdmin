var IsDateFilter = false;
var FromDate = "", ToDate = "";
var IsParty = false;
var PartyList = [];
var FullPartyList;
var ProductWiseGrid = [];
var PreviouslySelectedFromPickerDate = "";
var PreviouslySelectedToPickerDate = "";
var grid, dialog;

$(document).ready(function () {
    $(".preloader").hide();

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
        ProductWiseGrid = [];
        fillGrid();
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
                url: '/Report/GetSaleRegisterReport',
                type: 'POST',
                data: { "FromDate": FromDate, "ToDate": ToDate, "PartyCode": PartyCode},
                async: false,
                dataType: "json",
                success: function (objResult) {
                    ProductWiseGrid = [];
                    if (objResult.length > 0) {
                        for (var i = 0; i < objResult.length; i++) {
                            ProductWiseGrid.push({ "BillNo": objResult[i].BillNo, "Billdate": objResult[i].Billdate, "Code": objResult[i].Code, "PartyName": objResult[i].PartyName, "GSTIN": objResult[i].GSTIN, "ExemptSale": objResult[i].ExemptSale, "Discount": objResult[i].Discount, "Basic_5": objResult[i].Basic_5, "IGST_5": objResult[i].IGST_5, "CGST1_25": objResult[i].CGST1_25, "CGST2_25": objResult[i].CGST2_25, "Basic_12": objResult[i].Basic_12, "IGST_12": objResult[i].IGST_12, "CGST_6": objResult[i].CGST_6, "SGST_6": objResult[i].SGST_6, "Basic_for_18": objResult[i].Basic_for_18, "IGST_18": objResult[i].IGST_18, "CGST_9": objResult[i].CGST_9, "SGST_9": objResult[i].SGST_9, "Basic_28": objResult[i].Basic_28, "IGST_28": objResult[i].IGST_28, "CGST_14": objResult[i].CGST_14, "SGST_14": objResult[i].SGST_14, "TotalAmt": objResult[i].TotalAmt, "TotalIGST": objResult[i].TotalIGST, "TotalCGST": objResult[i].TotalCGST, "TotalSGST": objResult[i].TotalSGST, "RndOff": objResult[i].RndOff, "BillAmount": objResult[i].BillAmount });
                        }
                    }
                    fillGrid();
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

function fillGrid() {
    $("#noRecord").hide();
    if (grid != null) {
        grid.destroy();
        $('#grid').empty();

    }    

    grid = $('#grid').grid({
        dataSource: ProductWiseGrid,
        uiLibrary: 'bootstrap',
        headerFilter: true,
        columns:[
        { field: 'BillNo', title: 'BillNo', width: 120, sortable: true, hidden: false, filterable: false },
{ field: 'Billdate', title: 'Billdate', width: 120, sortable: true, hidden: false, filterable: false },
{ field: 'Code', title: 'Code', width: 80, sortable: true, hidden: false, filterable: false },
{ field: 'PartyName', title: 'PartyName', width: 200, sortable: true, hidden: false, filterable: false },
{ field: 'GSTIN', title: 'GSTIN', width: 80, sortable: true, hidden: false, filterable: false },
{ field: 'ExemptSale', title: 'ExemptSale', width: 80, sortable: true, hidden: false, filterable: false },
{ field: 'Discount', title: 'Discount', width: 80, sortable: true, hidden: false, filterable: false },
{ field: 'Basic_5', title: 'Basic 5%', width: 80, sortable: true, hidden: false, filterable: false },
{ field: 'IGST_5', title: 'IGST@5%', width: 80, sortable: true, hidden: false, filterable: false },
{ field: 'CGST1_25', title: 'CGST@2.5%', width: 80, sortable: true, hidden: false, filterable: false },
{ field: 'CGST2_25', title: 'CGST @2.5%', width: 80, sortable: true, hidden: false, filterable: false },
{ field: 'Basic_12', title: 'Basic 12%', width: 80, sortable: true, hidden: false, filterable: false },
{ field: 'IGST_12', title: 'IGST @12%', width: 80, sortable: true, hidden: false, filterable: false },
{ field: 'CGST_6', title: 'CGST @6%', width: 80, sortable: true, hidden: false, filterable: false },
{ field: 'SGST_6', title: 'SGST @6%', width: 80, sortable: true, hidden: false, filterable: false },
{ field: 'Basic_for_18', title: 'Basic for 18%', width: 80, sortable: true, hidden: false, filterable: false },
{ field: 'IGST_18', title: 'IGST @18%', width: 80, sortable: true, hidden: false, filterable: false },
{ field: 'CGST_9', title: 'CGST @9%', width: 80, sortable: true, hidden: false, filterable: false },
{ field: 'SGST_9', title: 'SGST @9%', width: 80, sortable: true, hidden: false, filterable: false },
{ field: 'Basic_28', title: 'Basic 28%', width: 80, sortable: true, hidden: false, filterable: false },
{ field: 'IGST_28', title: 'IGST @28%', width: 80, sortable: true, hidden: false, filterable: false },
{ field: 'CGST_14', title: 'CGST @14%', width: 80, sortable: true, hidden: false, filterable: false },
{ field: 'SGST_14', title: 'SGST @14%', width: 80, sortable: true, hidden: false, filterable: false },
{ field: 'TotalAmt', title: 'Total Amt.', width: 80, sortable: true, hidden: false, filterable: false },
{ field: 'TotalIGST', title: 'Total IGST', width: 80, sortable: true, hidden: false, filterable: false },
{ field: 'TotalCGST', title: 'Total CGST', width: 80, sortable: true, hidden: false, filterable: false },
{ field: 'TotalSGST', title: 'Total SGST', width: 80, sortable: true, hidden: false, filterable: false },
{ field: 'RndOff', title: 'Rnd.off', width: 80, sortable: true, hidden: false, filterable: false },
{ field: 'BillAmount', title: 'Bill Amount', width: 80, sortable: true, hidden: false, filterable: false }
        ],
        pager: { limit: 20, sizes: [15, 20, 35, 50, 65, 80, 95, 100] }
    });
}
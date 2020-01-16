var grid, dialog;
    var IsYes = false;
    var ProductWiseGrid = [{}];
    var ProductList = [];
    var FullProductList;
    var CategoryList = [];
    var FullCategoryList;
    var SalesType = "ProductWise";
    var IsCategory = false;
    var IsProduct = false;
    var IsDateFilter = false;
    var FromDate="", ToDate="";
    var ProductCode = "0", CategoryCode = "0",PartyCode = "0";
    var PreviouslySelectedFromDate = "";
    var PreviouslySelectedToDate = "";
    var PreviouslySelectedFromPickerDate = "",type="";
    var PreviouslySelectedToPickerDate = "";
    var IsParty = false;
    var PartyList = [];
    var FullPartyList;

$(document).ready(function () {
    $(".preloader").hide();

    if (IsAdministrator == "True") {
        GetAllParty();
    }
    else {
        $("#PartyCode").val(CurrentPartyCode);
    }
    GetAllCategory();
    GetAllProducts(0);
    $("#ProductName").autocomplete({
        source: function (request, response) {
            var results = $.ui.autocomplete.filter(ProductList, request.term); 
            response(results.slice(0, 50));
        },
        minLength: 1,
        scroll: true,
        select: function (event, ui) {
            $("#ProductName").val(ui.item.label);
            SetSpecificCode("Product", ui.item.label);
            return false;
        },

    }).focus(function () {
        $(this).autocomplete("search", "");
    });

    $("#CategoryName").autocomplete({
        source: function (request, response) {
            var results = $.ui.autocomplete.filter(CategoryList, request.term);
            response(results.slice(0, 50));
        },
        minLength: 1,
        scroll: true,
        select: function (event, ui) {
            $("#CategoryName").val(ui.item.label);
            SetSpecificCode("Category", ui.item.label);
            return false;
        },

    }).focus(function () {
        $(this).autocomplete("search", "");
    });

    $('#IsCategory').on('ifChecked', function () {
        IsCategory = true;
        CategoryCode = "";
        $("#CategoryName").val("All");
        $("#CategoryName").prop("readonly", "readonly");
        $("#CategoryCode").val(0);
        $('#IsProduct').trigger('ifChecked');
        var CheckBoxEle = $("#IsProductDiv").find('.icheckbox_flat-green');
        $(CheckBoxEle).addClass("checked");
    });
    $('#IsProduct').on('ifChecked', function () {
        IsProduct = true;
        ProductCode = "";
        $("#ProductName").val("All");
        $("#ProductName").prop("readonly", "readonly");
        $("#ProductCode").val(0);

    });

    $('#IsProduct').on('ifUnchecked', function () {
        IsProduct = false;
        ProductCode = "";
        $("#ProductName").val("");
        $("#ProductName").prop("readonly", "");
        $("#ProductCode").val("");
    });
    $('#IsCategory').on('ifUnchecked', function () {
        IsCategory = false;
        CategoryCode = "";
        $("#CategoryName").val("");
        $("#CategoryName").prop("readonly", "");
        $("#CategoryCode").val("");
        GetAllCategory();
    });

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

    $("#btnExport").on('click', function () {
        var UserTypeStr = "SalesReturnReport";       
        var tableString = "";
        if (type == "detail") {
            tableString = "<thead><tr>";
            tableString += "<th>S.No.</th>";
            tableString += "<th>STR.No.</th>";
            tableString += "<th>STR Date</th>";
            tableString += "<th>ReturnBy</th>";
            tableString += "<th>Name</th>";
            tableString += "<th>Product Code</th>";
            tableString += "<th>Product Name</th>";
            tableString += "<th>Quantity</th>";
            tableString += "<th>Rate</th>";
            tableString += "<th>Tax Per.</th>";
            tableString += "<th>Tax Amt.</th>";
            tableString += "<th>Basic Amt</th>";
            tableString += "<th>Total Amt.</th>";
            tableString += "<th>Bill No</th>";
            tableString += "<th>Return To</th>";
            tableString += "</tr></thead><tbody>";
            for (var i = 0; i < ProductWiseGrid.length; i++) {
                tableString += "<tr>";
                tableString += "<td>" + ProductWiseGrid[i].SNo + "</td>";
                tableString += "<td>" + ProductWiseGrid[i].STRNO + "</td>";
                tableString += "<td>" + ProductWiseGrid[i].STRDate + "</td>";
                tableString += "<td>" + ProductWiseGrid[i].ReturnBy + "</td>";
                tableString += "<td>" + ProductWiseGrid[i].ReturnByName + "</td>";
                tableString += "<td class='formatText'>" + ProductWiseGrid[i].ProductCode + "</td>";
                tableString += "<td>" + ProductWiseGrid[i].ProductName + "</td>";
                tableString += "<td>" + ProductWiseGrid[i].Quantity + "</td>";
                tableString += "<td>" + ProductWiseGrid[i].Rate + "</td>";
                tableString += "<td>" + ProductWiseGrid[i].Tax + "</td>";
                tableString += "<td>" + ProductWiseGrid[i].TaxAmt + "</td>";
                tableString += "<td>" + ProductWiseGrid[i].BasicAmt + "</td>";
                tableString += "<td>" + ProductWiseGrid[i].TotalAmt + "</td>";

                tableString += "<td>" + ProductWiseGrid[i].BillNo + "</td>";
                tableString += "<td>" + ProductWiseGrid[i].ReturnTo + "</td>";
                tableString += "</tr>";
            }
        }
        else {
            tableString = "<thead><tr>";
            tableString += "<th>S.No.</th>";

            tableString += "<th>ReturnBy</th>";

            tableString += "<th>Product Code</th>";
            tableString += "<th>Product Name</th>";
            tableString += "<th>Quantity</th>";
            tableString += "<th>Rate</th>";

            tableString += "<th>Tax Amt.</th>";
            tableString += "<th>Basic Amt</th>";
            tableString += "<th>Total Amt.</th>";

            tableString += "<th>Return To</th>";
            tableString += "</tr></thead><tbody>";
            for (var i = 0; i < ProductWiseGrid.length; i++) {
                tableString += "<tr>";
                tableString += "<td>" + ProductWiseGrid[i].SNo + "</td>";

                tableString += "<td>" + ProductWiseGrid[i].ReturnBy + "</td>";

                tableString += "<td class='formatText'>" + ProductWiseGrid[i].ProductCode + "</td>";
                tableString += "<td>" + ProductWiseGrid[i].ProductName + "</td>";
                tableString += "<td>" + ProductWiseGrid[i].Quantity + "</td>";
                tableString += "<td>" + ProductWiseGrid[i].Rate + "</td>";

                tableString += "<td>" + ProductWiseGrid[i].TaxAmt + "</td>";
                tableString += "<td>" + ProductWiseGrid[i].BasicAmt + "</td>";
                tableString += "<td>" + ProductWiseGrid[i].TotalAmt + "</td>";


                tableString += "<td>" + ProductWiseGrid[i].ReturnTo + "</td>";
                tableString += "</tr>";
            }
        }


        tableString += "</tbody>";
        $("#gridExport").empty();
        $("#gridExport").append(tableString);
        tableToExcel('gridExport', UserTypeStr + "_Export");
    });

    $(".getReport").on('click', function () {
        $(".preloader").show();
        type = $(this).attr("data-type");
        ProductWiseGrid = [];
        fillGrid(type);
        PartyCode = $("#PartyCode").val();
        var PartyType = $("#partytype").val();
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
        if (ProductCode == "" || ProductCode == null) {
            ProductCode = 0;
        }
        if (CategoryCode == "" || CategoryCode == null) {
            CategoryCode = 0;
        }
        if (DateError == false) {
            var InvoiceTypeVal = "";
            $.ajax({
                //GetSalesReturnReport(FromDate, ToDate, PartyCode, ProductCode, CategoryCode, PartyType)
                url: '/Report/GetSalesReturnReport',
                type: 'POST',
                data: { "FromDate": FromDate, "ToDate": ToDate, "PartyCode": PartyCode, "ProductCode": ProductCode, "CategoryCode": CategoryCode, "PartyType": PartyType, "Type": type },
                async: false,
                dataType: "json",
                success: function (objResult) {
                    ProductWiseGrid = [];
                    if (objResult.length > 0) {                        
                        for (var i = 0; i < objResult.length; i++) {
                            ProductWiseGrid.push({ "SNo": i + 1, "STRNO": objResult[i].STRNo, "STRDate": objResult[i].STRDate, "ReturnBy": objResult[i].ReturnBy, "ReturnByName": objResult[i].ReturnByName, "ProductName": objResult[i].ProductName, "ProductCode": objResult[i].ProductCode, "BillNo": objResult[i].BillNo, "Quantity": objResult[i].Quantity, "Rate": objResult[i].Rate, "Tax": objResult[i].Tax, "TaxAmt": objResult[i].TaxAmt, "TotalAmt": objResult[i].TotalAmt, "BasicAmt": objResult[i].BasicAmt, "ReturnTo": objResult[i].ReturnTo });
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
function GetAllProducts(catId) {
    $.ajax({
        url: '/Report/GetAllProduct',
        dataType: "json",
        method: 'POST',
        async: false,
        data: { 'CategoryCode': catId },
        success: function (data) {
            FullProductList = data;
            ProductList = [];
            if (data != null) {
                var i = 0;
                for (i = 0; i < data.length; i++) {
                    ProductList.push(data[i].ProductName);
                }
            }
        },
        error: function (xhr, status, error) {
            console.log("xhr.responseText:", xhr.responseText);

        }
    });
}



function fillGrid(type) {

    $("#noRecord").hide();
    if (grid != null) {
        grid.destroy();
        $('#grid').empty();

    }
    if (type == "detail") {
        grid = $('#grid').grid({
            dataSource: ProductWiseGrid,
            uiLibrary: 'bootstrap',
            headerFilter: true,
            columns: [                
                { field: 'SNo', title: 'S.No.', width: 80, sortable: true, hidden: false, filterable: false },
                { field: 'STRNO', title: 'STR.No.', width: 100, sortable: true, hidden: false, filterable: true },
                { field: 'STRDate', title: 'STR Date', width: 100, sortable: true, hidden: false, filterable: true },
                { field: 'ReturnBy', title: 'ReturnBy', width: 80, sortable: true, hidden: false, filterable: true },
                { field: 'ReturnByName', title: 'Name', width: 120, sortable: true, hidden: false, filterable: true },
                { field: 'ProductCode', title: 'Product Code', width: 80, sortable: true, hidden: false, filterable: true },
                { field: 'ProductName', title: 'Product Name', width: 150, sortable: true, hidden: false, filterable: true },
                { field: 'Quantity', title: 'Quantity', width: 80, sortable: true, hidden: false, filterable: true },
                { field: 'Rate', title: 'Rate', width: 80, sortable: true, hidden: false, filterable: false },
                { field: 'Tax', title: 'Tax', width: 80, sortable: true, hidden: false, filterable: false },
                { field: 'TaxAmt', title: 'Tax Amt', width: 80, sortable: true, hidden: false, filterable: false },
                { field: 'TotalAmt', title: 'Total Amt', width: 80, sortable: true, hidden: false, filterable: false },
                { field: 'BasicAmt', title: 'Basic Amt', width: 80, sortable: true, hidden: false, filterable: false },
                { field: 'BillNo', title: 'Bill No', width: 80, sortable: true, hidden: false, filterable: true },
                { field: 'ReturnTo', title: 'Return To', width: 150, sortable: true, hidden: false, filterable: true }                
            ],
            pager: { limit: 20, sizes: [15, 20, 35, 50, 65, 80, 95, 100] }
        });
    }
    else {
        grid = $('#grid').grid({
            dataSource: ProductWiseGrid,
            uiLibrary: 'bootstrap',
            headerFilter: true,
            columns: [
                { field: 'SNo', title: 'S.No.', width: 80, sortable: true, hidden: false, filterable: false },                
                { field: 'ReturnBy', title: 'ReturnBy', width: 100, sortable: true, hidden: false, filterable: true },               
                { field: 'ProductCode', title: 'Product Code', width: 80, sortable: true, hidden: false, filterable: true },
                { field: 'ProductName', title: 'Product Name', width: 150, sortable: true, hidden: false, filterable: true },
                { field: 'Quantity', title: 'Quantity', width: 80, sortable: true, hidden: false, filterable: false },
                { field: 'Rate', title: 'Rate', width: 80, sortable: true, hidden: false, filterable: false },                
                { field: 'TaxAmt', title: 'Tax Amt', width: 80, sortable: true, hidden: false, filterable: false },
                { field: 'BasicAmt', title: 'Basic Amt', width: 80, sortable: true, hidden: false, filterable: false },
                { field: 'TotalAmt', title: 'Total Amt', width: 80, sortable: true, hidden: false, filterable: false },                               
                { field: 'ReturnTo', title: 'Return To', width: 150, sortable: true, hidden: false, filterable: true }
            ],
            pager: { limit: 20, sizes: [15, 20, 35, 50, 65, 80, 95, 100] }
        });
    }


}

function SetSpecificCode(type, label) {    
    if (type == "Product") {
        for (var i = 0; i < FullProductList.length; i++) {
            if (FullProductList[i].ProductName == label) {
                $("#ProductCode").val(parseFloat(FullProductList[i].ProductCodeStr));
                ProductCode = parseFloat(FullProductList[i].ProductCodeStr);
                break;
            }
        }
    }
    if (type == "Category") {
        for (var i = 0; i < FullCategoryList.length; i++) {
            if (FullCategoryList[i].CategoryName == label) {
                $("#CategoryCode").val(FullCategoryList[i].CategoryId);
                CategoryCode = FullCategoryList[i].CategoryId;
                GetAllProducts(FullCategoryList[i].CategoryId);
                break;
            }
        }
    }
    if (type == "Party") {
        for (var i = 0; i < FullPartyList.length; i++) {
            if (FullPartyList[i].PartyName == label) {
                $("#PartyCode").val(FullPartyList[i].PartyCode);
                PartyCode = FullPartyList[i].PartyCode;
                break;
            }
        }
    }
    console.log("CategoryCode", CategoryCode);
    console.log("ProductCode", ProductCode);
}


function GetAllCategory() {
    $.ajax({
        url: '/Report/GetAllCategory',
        dataType: 'JSON',
        method: 'GET',
        async: false,
        success: function (data) {
            console.log("data:", data);
            FullCategoryList = data;
            CategoryList = [];
            if (data != null) {
                var i = 0;
                for (i = 0; i < data.length; i++) {
                    CategoryList.push(data[i].CategoryName);
                }
            }
        },
        error: function (error) {
            console.log(error);
        }
    });
}
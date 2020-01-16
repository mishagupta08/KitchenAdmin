var ProductGrid = [];
var grid = null;
var formdata;
var freeprodlistcount = 0;
$(document).ready(function () {

    $(".preloader").hide();
    $("#IDno").focus();

    $("#IDno").focusout(function (e) {
        e.preventDefault();
        e.stopPropagation();
        var Value = $("#IDno").val();
        if (Value != "")
            GetCustomerInfo(Value);
    });

    $("#NewKitId").change(function (e) {
        var Value = $("#NewKitId").val();
        if (Value != "" && Value != 0) {
            $("#KitId_").val(Value);
            getGridDetail(Value);
        }
    });

    $("form[name=upgradeIdForm]").unbind("submit");
    $("form[name=upgradeIdForm]").bind('submit', function (e) {       
        var ListObjStr = JSON.stringify(ProductGrid);
        $("#productstring").val(ListObjStr);
        SaveUpgradeIDForm();
    });

    $("#btnCancel").click(function () {
        resetform();
    });

});

function resetform() {

    $('#upgradeIdForm')[0].reset();
    ProductGrid = [];
    fillGrid();

}

function SaveUpgradeIDForm() {
    formdata = new FormData($("#upgradeIdForm")[0]);
    $(".preloader").show();
    $.ajax({
        url: '/Transaction/ActivateIdWithPackage',
        type: 'POST',
        data: formdata,
        processData: false,
        contentType: false,
        dataType: "json",
        success: function (objResult) {
            $(".preloader").hide();
            if (objResult != null) {
                if (objResult.ResponseStatus == "OK") {
                    resetform();
                    OpenDialog("dialogMessage", "Saved Successfully", "false");
                    window.location.href = "UpgradeID";
                    }
                else {
                    OpenDialog("dialogMessage", objResult.ResponseMessage, "false");
                }
            }
            $(".preloader").hide();
        }
    });
}

function GetCustomerInfo(IdNo) {
    $(".preloader").show();

    $.ajax({
        url: '/Transaction/GetCustomerKitDetail',
        type: 'POST',
        data: { "IdNo": IdNo },
        dataType: "json",
        success: function (objResult) {
            $(".preloader").hide();
            if (objResult != null) {
                if (objResult.MemberName == "") {
                    OpenDialog("dialogMessage", "Record does not exists.", "false");
                    $("#IDno").val('');
                }
                else
                {
                    $("#MemberName").val(objResult.MemberName);
                    $("#KitAmount").val(objResult.KitAmount);
                    $("#KitName").val(objResult.KitName);
                    $("#MacAdres").val(objResult.MacAdres);
                    $("#BillType").val(objResult.BillType);
                    $("#NewKitId").html("");
                    $("#NewKitId").append($("<option></option>").val(0).html("--Select Package--"));
                    $.each(objResult.NewKitList, function (data, value) {
                        $("#NewKitId").append($("<option></option>").val(value.kitId).html(value.kitName));
                    })

                }

            }
            $(".preloader").hide();
        },
        error: function (xhr, data) {
            $(".preloader").hide();            
            console.log(xhr);
            console.log("Error:", data);
        }
    });
}

function getGridDetail(kitId)
{
    $(".preloader").show();
    $.ajax({
        url: '/Transaction/GetKitProductList',
        type: 'POST',
        data: { "kitId": kitId },
        dataType: "json",
        success: function (objResult) {

            if (objResult != null) {
                $("#KitAmount").val(objResult.KitAmount);
                $("#promoId").val(objResult.promoId);
                var newKitname=$("#NewKitId option:selected").text();
                $("#KitName_").val(newKitname);
                ProductGrid = [];
                if (objResult.objListProduct.length > 0) {
                    for (var i = 0; i < objResult.objListProduct.length; i++) {
                        ProductGrid.push({ "Barcode": objResult.objListProduct[i].Barcode, "ProductTye": objResult.objListProduct[i].ProductTye, "PV": objResult.objListProduct[i].PV, "CV": objResult.objListProduct[i].CV, "DispStatus": objResult.objListProduct[i].DispStatus, "TaxPer": objResult.objListProduct[i].TaxPer, "TaxAmt": objResult.objListProduct[i].TaxAmt, "Amount": objResult.objListProduct[i].Amount, "Rate": objResult.objListProduct[i].Rate, "ProductId": objResult.objListProduct[i].IdNo, "ProductName": objResult.objListProduct[i].ProductName, "RP": objResult.objListProduct[i].RP, "DP": objResult.objListProduct[i].DP, "MRP": objResult.objListProduct[i].MRP, "BV": objResult.objListProduct[i].BV, "Quantity": objResult.objListProduct[i].Quantity, "DiscAmt": objResult.objListProduct[i].DiscAmt, "DP1": objResult.objListProduct[i].DP1 });
                    }
                }
                $("#products").empty();

                fillGrid();
            }
            $(".preloader").hide();
        },
        error: function (xhr, data) {
            $(".preloader").hide();
            console.log(xhr);
            console.log("Error:", data);
        }
    });
}

function fillGrid(type) {
    $("#noRecord").hide();
    if (grid != null) {
        grid.destroy();
        $('#grid').empty();
    }

    grid = $('#grid').grid({
        dataSource: ProductGrid,
        uiLibrary: 'bootstrap',
        headerFilter: true,
        columns: [
            { field: 'ProductId', title: 'Prod Id', width: 80, sortable: true, hidden: false, filterable: false },
            { field: 'ProductName', title: 'Product Name', width: 100, sortable: true, hidden: false, filterable: true },
            { field: 'MRP', title: 'MRP', width: 100, sortable: true, hidden: false, filterable: true },
            { field: 'DP1', title: 'DP', width: 80, sortable: true, hidden: false, filterable: true },
            { field: 'Rate', title: 'Rate', width: 80, sortable: true, hidden: true, filterable: true },
            { field: 'RP', title: 'RP', width: 80, sortable: true, hidden: false, filterable: true },
            { field: 'BV', title: 'BV', width: 80, sortable: true, hidden: false, filterable: true },
            { field: 'Quantity', title: 'Quantity', width: 80, sortable: true, hidden: true, filterable: true },
            { field: 'DiscAmt', title: 'DiscAmt', width: 80, sortable: true, hidden: true, filterable: true },
        ],
        pager: { limit: 20, sizes: [15, 20, 35, 50, 65, 80, 95, 100] }
    });
}

function OpenDialog(dialogId, Message, isConfirmation) {
    $("#" + dialogId).empty();
    if (Message != "" || Message != null) {

        $("#" + dialogId).append('<p>' + Message + '</p>');
    }
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
            }]
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
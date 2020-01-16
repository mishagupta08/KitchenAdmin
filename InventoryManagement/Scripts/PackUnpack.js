var ProductGrid = [];
var grid = null;
var formdata;

$(document).ready(function () {
    $(".preloader").hide();

    
    $("form[name=PackUnpackForm]").unbind("submit");
    $("form[name=PackUnpackForm]").bind('submit', function (e) {
        var Kit = $("#kitId").val();
        var qunatity = $("#qunatity").val();
        var ListObjStr = JSON.stringify(ProductGrid);       
        $("#productstring").val(ListObjStr);
        if (Kit != "0") {
            if (qunatity != "" && qunatity != null && qunatity != "0") {
                SavePackUnpackForm();
            }
            else {
                
                OpenDialog("dialogMessage", "Please add quantity.", "false");
            }
        }
        else {
            OpenDialog("dialogMessage", "Please select kit.", "false");
        }
        
    });

    $("#btnCancel").click(function () {
       resetform();
    });

    $("#kitId").change(function () {
        showKitDetail();
    });

    $('input[type=radio][name=PackOrUnpack]').change(function () {
        showKitDetail();
    });
    
});

function showKitDetail()
{
 $(".preloader").show();
        var PackUnpack = $("input[name='PackOrUnpack']:checked").val();
        var kitname = $('#kitId :selected').text();
        $("#kitName").val(kitname);
        var Kit = $("#kitId").val();
        debugger;
        var prodID = $('#kitId :selected').attr("data-productId");
        $.ajax({
            url: '/Transaction/GetPackUnpackProducts',
            type: 'POST',
            data: { "PackUnpack": PackUnpack, "KitId": Kit,"prodID":prodID },
            async: false,
            dataType: "json",
            success: function (objResult) {
                ProductGrid = [];
                if (objResult.length > 0) {
                    $("#maxkit").val(objResult[0].MaxPack);
                    console.log(objResult[0].MaxPack);
                    for (var i = 0; i < objResult.length; i++) {                        
                        ProductGrid.push({ "ProductId": objResult[i].ProductId, "ProductName": objResult[i].ProductName, "Qunatity": objResult[i].Qunatity, "AvailStock": objResult[i].AvailStock});
                    }
                }
                fillGrid();
                $(".preloader").hide();
            },
            error: function (xhr, data) {
                console.log(xhr);
                console.log("Error:", data);
                $(".preloader").hide();
            }
        });
}

function resetform() {

    $('#PackUnpackForm')[0].reset();    
    ProductGrid = [];
    fillGrid();

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
            { field: 'Qunatity', title: 'Qunatity', width: 100, sortable: true, hidden: false, filterable: true },
            { field: 'AvailStock', title: 'Avail Stock', width: 80, sortable: true, hidden: false, filterable: true },
        ],
        pager: { limit: 20, sizes: [15, 20, 35, 50, 65, 80, 95, 100] }
    });

}

function SavePackUnpackForm()
{
    
    var availqty = $("#maxkit").val();
    console.log("maxQty: ", availqty);
    var qty = $("#qunatity").val();
    console.log("qty: ", qty);
    var selectedtype = $('input[name="PackOrUnpack"]:checked').attr("value");
    console.log("selectedtype: ", selectedtype);
    if (parseInt(qty) > parseInt(availqty))
    { alert("Your available stock could " + selectedtype + " " + availqty + " qty."); }
    else {
        formdata = new FormData($("#PackUnpackForm")[0]);
        $(".preloader").show();
        $.ajax({
            url: '/Transaction/SavePackUnpack',
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
                    }
                    else {
                        OpenDialog("dialogMessage", objResult.ResponseMessage, "false");
                    }
                }
                $(".preloader").hide();
            }
        });
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
                    //var isOpen = $("#dialog").dialog("isOpen");
                    //if (isOpen == true) {
                    //    okCallBack();
                    //}
                    IsYes = true;
                    $("#" + dialogId).dialog("close");


                }
            },
            {
                text: "No",
                id: "btnNo" + dialogId,
                click: function () {
                    //var isOpen = $("#dialog").dialog("isOpen");
                    //if (isOpen == true) {
                    //    okCallBack();
                    //}
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
                    //var isOpen = $("#dialog").dialog("isOpen");
                    //if (isOpen == true) {
                    //    okCallBack();
                    //}

                    $("#" + dialogId).dialog("close");


                }
            }]
        });
        $(".ui-dialog-titlebar-close").empty();
        $(".ui-dialog-titlebar-close").append('<i class="fa fa-close"></i>');
    }

}
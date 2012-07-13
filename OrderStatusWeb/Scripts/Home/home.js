var OrderDialog;
var SingleOrder;

$(document).ready(function () {
    initDialogs();
    bindButtonsActions();
});

function bindButtonsActions() {
    $("#btnOrders").click(function () {
        pullOrders();
    });
    $("#btnSingleOrder").click(function () {
        pullSingleOrder();
    }); 
}

function pullOrders() {
    $.ajax({
        type: 'POST',
        url: '/Home/PullOrders',
        cache: false,
        data: {
    },
    dataType: "json",
    success: function (data) {
        OrderDialog.dialog("open");
    },
    error: function (data) {

    }
});
}

function pullSingleOrder() {
    var storeId = $("#SelectedStore option:selected").val();
    var invoiceNumber = $("#txtInvoice").val();
    if (invoiceNumber != "") {
        $.ajax({
            type: 'POST',
            url: '/Home/PullAnOrder',
            cache: false,
            data: {
                storeId: storeId,
                invoiceNumber: invoiceNumber
            },
            dataType: "json",
            success: function (data) {
                $("#orderResult").text(data);
                SingleOrder.dialog("open");
            },
            error: function (data) {
                $("#orderResult").text(data);
                SingleOrder.dialog("open");

            }
        });
    } else {
        
    }
}

function initDialogs() {
    OrderDialog = $('#AllOrders').dialog({
        modal: true,
        autoOpen: false,
        resizable: false,
        draggable: false,
        width:550,    
        buttons: {
            'ok': {
                text: 'Ok',
                click: function () {
                    $(this).dialog("close");
                }
            }
        }
    });
    SingleOrder  = $('#SingleOrder').dialog({
        modal: true,
        autoOpen: false,
        resizable: false,
        draggable: false,
        width: 550,
        buttons: {
            'ok': {
                text: 'Ok',
                click: function () {
                    $(this).dialog("close");
                }
            }
        }
    });
}
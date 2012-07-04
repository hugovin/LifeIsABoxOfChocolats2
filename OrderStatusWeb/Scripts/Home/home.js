var OrderDialog;

$(document).ready(function () {
    initDialogs();
    bindButtonsActions();
});

function bindButtonsActions() {
    $("#btnOrders").click(function () {
        pullOrders();
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
}
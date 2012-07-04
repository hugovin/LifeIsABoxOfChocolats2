var ConfirmationDialog;
$(document).ready(function () {
    initDialogs();
    bindButtonsActions();
});

function bindButtonsActions() {
    $("#btnUps").bind('click', function () {
        AllEOD();
    });
}

function AllEOD() {
    $.ajax({
        type: 'POST',
        url: '/EOD/RunOrdersEod',
        cache: false,
        data: {
        },
        dataType: "json",
        success: function (data) {
            ConfirmationDialog.dialog("open");
        },
        error: function (data) {

        }
    });
}


function initDialogs() {
    ConfirmationDialog = $('#confirmation_dialog').dialog({
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
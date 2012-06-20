var upsDialog, encidiaDialog;

$(document).ready(function () {
    initDialogs();
    bindButtonsActions();
});

function bindButtonsActions() {
    $("#btnUps").bind('click', function () {
        UpsEOD();
    });
    $("#btnEncidia").bind('click', function () {
        EncidiaEOD();
    }); 
}

function UpsEOD() {
    $.ajax({
        type: 'POST',
        url: '/EOD/RunUpsEod',
        cache: false,
        data: {
        },
        dataType: "json",
        success: function (data) {
            upsDialog.dialog("open");
        },
        error: function (data) {

        }
    });
}



function EncidiaEOD() {
    $.ajax({
        type: 'POST',
        url: '/EOD/RunEncidiaEod',
        cache: false,
        data: {
        },
        dataType: "json",
        success: function (data) {
            encidiaDialog.dialog("open");
        },
        error: function (data) {

        }
    });
}

function initDialogs() {
    upsDialog = $('#ups_dialog').dialog({
        modal: true,
        autoOpen: false,
        resizable: false,
        draggable: false,
        buttons: {
            'ok': {
                text: 'Ok',
                click: function () {
                    $(this).dialog("close");
                }
            }
        }
    });
    encidiaDialog = $('#Encidia_dialog').dialog({
        modal: true,
        autoOpen: false,
        resizable: false,
        draggable: false,
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
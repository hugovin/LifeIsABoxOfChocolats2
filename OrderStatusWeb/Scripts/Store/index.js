var removeDialog;
var removeId;

$(document).ready(function () {
    initDialog();
});

function RemoveStore(id) {
    removeId = id;
    removeDialog.dialog("open");
}

function initDialog() {
    removeDialog = $('#remove_dialog').dialog({
        modal: true,
        autoOpen: false,
        resizable: false,
        draggable: false,
        buttons: {
            'cancel': {
                text: 'Cancel',
                click: function () {
                    $(this).dialog("close");
                }
            },
            'ok': {
                text: 'Remove',
                'class': 'blue',
                click: function () {
                    ajaxRemoveStore();
                }
            }

        }
    });
}

function ajaxRemoveStore() {
    $.ajax({
        type: 'POST',
        url: '/Store/Remove',
        cache: false,
        data: {
            id: removeId
        },
        dataType: "json",
        success: function (data) {
            var href = "../Store/Index";
            window.location = href;
        },
        error: function (data) {

        }
    });
    
}

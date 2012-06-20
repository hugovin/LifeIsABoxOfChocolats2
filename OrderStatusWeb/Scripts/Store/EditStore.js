var CustomShipments = 2;

$(document).ready(function () {

    bindRadioButtonAction();
    bindCustomShipmmntAction();
    bindAddMore();
    bindCloseCustomShip2();
    bindCloseCustomShip3();
    SelectCheckBox();

});

function bindAddMore() {
    $('#spAddmore').bind('click', function () {
        $("#CustomShip" + CustomShipments).val("");
        $("#liCus" + CustomShipments).removeClass("dn");
        CustomShipments++;
    });

}

function bindRadioButtonAction() {
    $('input[type=radio]').live('change', function () { $('#CustomOrderStatus').val(""); });


}

function bindCustomShipmmntAction() {
    $('#CustomOrderStatus').focus(function () {
        $("input:radio").attr("checked", false);
    });
    $('#CustomOrderStatus').blur(function () {
        if ($('#CustomOrderStatus').val() == "") {
            $("input[name='rdOrderStatus'][value='1']").attr("checked", "checked");
        }
    });
}

function bindCloseCustomShip2() {
    $('#spnCustomShip2').bind('click', function () {
        CustomShipments = 2;
        $("#CustomShip" + CustomShipments).val("");
        $("#liCus" + CustomShipments).addClass("dn");
    });
}

function bindCloseCustomShip3() {
    $('#spnCustomShip3').bind('click', function () {
        CustomShipments = 3;
        $("#CustomShip" + CustomShipments).val("");
        $("#liCus" + CustomShipments).addClass("dn");
    });
}

function SelectCheckBox() {
    var ids = ListIds.split(',');
    for(var i = 0;i < ids.length;i++) {
        $(".checkboxDummy [value='" + ids[i] + "']").prop("checked", true);
    }
}
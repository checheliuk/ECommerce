$(".ukrposhta, .nova-posta, #cash-on-delivery-box, #delivery-box").hide();

var isCheckoutFNValid = false;
var isCheckoutLNValid = false;
var isCheckoutPhoneValid = false;
var isCheckoutDeliveryValid = false;
var isCheckoutPayment = false;
var isCheckoutArea = false;
var isCheckoutTown = false;
var isCheckoutZipcode = false;
var isCheckoutAddress = false;
var isCheckoutWarehouses = false;

calculation();

$("#checkout-fn").keyup(function () {
    var value = $(this).val();
    isCheckoutFNValid = value.length > 2 ? true : false;
    setCalidationClass($(this), isCheckoutFNValid)
    checkValidation();
});

$("#checkout-ln").keyup(function () {
    var value = $(this).val();
    isCheckoutLNValid = value.length > 2 ? true : false;
    setCalidationClass($(this), isCheckoutLNValid)
    checkValidation();
});

$("#checkout-phone").keyup(function () {
    var value = $(this).val();
    //isCheckoutPhoneValid = (value.length == 9 || value.length == 10) ? true : false;
    isCheckoutPhoneValid = (value.match(/^\d{9}$/) || value.match(/^\d{10}$/)) ? true : false;
    setCalidationClass($(this), isCheckoutPhoneValid)
    checkValidation();
});

$("#checkout-delivery").change(function () {
    isCheckoutDeliveryValid = $(this).val() != 0 ? true : false;
    setCalidationClass($(this), isCheckoutDeliveryValid)
    calculation();
    checkValidation();
});

$("#checkout-payment").change(function () {
    isCheckoutPayment = $(this).val() != 0 ? true : false;
    setCalidationClass($(this), isCheckoutPayment)
    calculation();
    checkValidation();
});

$("#checkout-area").change(function () {
    isCheckoutArea = $(this).val() != 0 ? true : false;
    setCalidationClass($(this), isCheckoutArea)
    checkValidation();
});

$("#checkout-town").keyup(function () {
    var value = $(this).val();
    isCheckoutTown = value.length > 2 ? true : false;
    setCalidationClass($(this), isCheckoutTown)
    checkValidation();
});

$("#checkout-zipcode").keyup(function () {
    var value = $(this).val();
    isCheckoutTown = value.length == 5 ? true : false;
    setCalidationClass($(this), isCheckoutTown)
    checkValidation();
});

$("#checkout-address").keyup(function () {
    var value = $(this).val();
    isCheckoutAddress = value.length > 2 ? true : false;
    setCalidationClass($(this), isCheckoutAddress)
    checkValidation();
});

$("#checkout-warehouses").keyup(function () {
    var value = $(this).val();
    isCheckoutWarehouses = value.length > 2 ? true : false;
    setCalidationClass($(this), isCheckoutWarehouses)
    checkValidation();
});

function calculation()
{ 
    var delivery = 0;
    var cashOnDelivery = 0;

    switch ($("#checkout-delivery").val()) {
        case "1":
            $(".ukrposhta").hide();
            $(".nova-posta, #delivery-box").show();
            $("#delivery").text(novaPostaDelivery);
            delivery = novaPostaDelivery;
            break;
        case "2":
            $(".nova-posta").hide();
            $(".ukrposhta, #delivery-box").show();
            $("#delivery").text(ukrPoshtaDelivery);
            delivery = ukrPoshtaDelivery;
            break;
        default:
            $(".ukrposhta, .nova-posta, #delivery-box").hide();
            $("#delivery").text('');
            break;
    }

    switch ($("#checkout-payment").val()) {
        case "2":
            switch ($("#checkout-delivery").val()) {
                case "1":
                    $("#cash-on-delivery-box").show();
                    var value = total / 100 * novaPostaInterest + novaPostaBase;
                    $("#cash-on-delivery").text(value.toFixed(2));
                    cashOnDelivery = value;
                    break;
                case "2":
                    $("#cash-on-delivery-box").show();
                    var value = total / 100 * ukrPoshtaInterest + ukrPoshtaBase;
                    $("#cash-on-delivery").text(value.toFixed(2));
                    cashOnDelivery = value;
                    break;
                default:
                    $("#cash-on-delivery-box").hide();
                    $("#cash-on-delivery").text('');
                    cashOnDelivery = 0
                    break;
            }
            break;
        default:
            $("#cash-on-delivery-box").hide();
            $("#cash-on-delivery").text('');
            cashOnDelivery = 0
            break;
    }

    $("#sum").text((total + delivery + cashOnDelivery).toFixed(2));
}

function checkValidation() {
    var disabled = true;

    if (isCheckoutFNValid && isCheckoutLNValid && isCheckoutPhoneValid && isCheckoutDeliveryValid && isCheckoutPayment && isCheckoutArea && isCheckoutTown)
    {
        switch ($("#checkout-delivery").val()) {
            case "1":
                disabled = !isCheckoutWarehouses ? true : false;
                break;
            case "2":
                disabled = !isCheckoutZipcode && !isCheckoutAddress ? true : false;
                break;
        }
    } 

    $('#checkout-submit').prop('disabled', disabled);
}

function setCalidationClass(item, value) {
    if (value) {
        item.addClass("form-control-success").removeClass("form-control-danger");
        item.parent().addClass("has-success").removeClass("has-danger");
    } else {
        item.addClass("form-control-danger").removeClass("form-control-success");
        item.parent().addClass("has-danger").removeClass("has-success");
    }
}
function addToCart(productID, count, sizeID, colorID) {
    $.ajax({
        url: "/catalog/addtocart",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: "{'productID':'" + productID + "','count':'" + count + "','sizeID':'" + sizeID + "','colorID':'" + colorID + "'}",
        dataType: "text",
        success: function () {
            setTimeout(function () {
                location.reload();
            }, 3000);
        }
    });
}

function addToCartFromDetails(productID) {
    var count = $("#quantity").val();
    var sizeID = $("#size").val();
    var colorID = $("#color").val();
    $.ajax({
        url: "/catalog/addtocart",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: "{'productID':'" + productID + "','count':'" + count + "','sizeID':'" + sizeID + "','colorID':'" + colorID + "'}",
        dataType: "text",
        success: function () {
            setTimeout(function () {
                location.reload();
            }, 3000);
        }
    });
}

function removeFromCart(productShippingID) {
    $.ajax({
        url: "/catalog/removefromcart",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: "{'productShippingID':'" + productShippingID + "'}",
        dataType: "text",
        success: function () {
            location.reload();
        }
    });
}

function removeAllFromCart() {
    $.ajax({
        url: "/catalog/removeallfromcart",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "text",
        success: function () {
            location.reload();
        }
    });
}

function addEmail() {
    var email = $("#email").val();
    var re = /[A-Z0-9._%+-]+@[A-Z0-9.-]+.[A-Z]{2,4}/igm;
    if (email != '' && re.test(email)) {
      $.ajax({
            url: "/post/email",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: "{'email':'" + email + "'}",
            dataType: "text",
            success: function () {
                $("#email-input-box").hide();
                $("#email-subscrib-box").show();
            }
        });
    } 
}

function checkCookie() {
    var cookieEnabled = navigator.cookieEnabled;
    if (!cookieEnabled) {
        document.cookie = "test-cookie";
        cookieEnabled = document.cookie.indexOf("test-cookie") != -1;
    }
    return cookieEnabled || $("#modal-message").modal();
}

checkCookie();
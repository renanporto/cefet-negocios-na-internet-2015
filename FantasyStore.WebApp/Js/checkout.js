
var loadTotal = function () {
    var result = 0;
    $('table tr.table-row').each(function () {
        var amount = parseInt($(this).find(".txt-amount").val());
        var priceString = $(this).find(".current-price").html();
        var replaced = priceString.replace(",", ".");
        var price = parseFloat(replaced);
        result += price * amount;
    });

    var total = result.toFixed(2).toString().replace(".", ",");
    $(".total").html(total);
};

var Client = (function () {
    function client() { }

    client.prototype.removeItem = function (productId) {
        var serviceUrl = "http://localhost:26722/Checkout/Remove/?productId=" + productId;

        var ajaxConfig = {
            url: serviceUrl,
            type: 'GET',
        };

        $.ajax(ajaxConfig)
            .done(function () {
                location.reload();
            });
    };

    client.prototype.updateCart = function (cartCode, itemId, quantity, total) {
        var serviceUrl = "http://localhost:26722/Checkout/UpdateCart/?cartCode=" + cartCode + "&itemId=" + itemId + "&quantity=" + quantity + "&total=" + total;

        var ajaxConfig = {
            url: serviceUrl,
            type: 'GET',
        };

        $.ajax(ajaxConfig)
            .done(function () {
                window.location.assign('http://localhost:26722/Checkout/Payment');
            });

    };

    return client;
})();

$(document).ready(function () {
    loadTotal();
    $(".txt-amount").on("change", function (e) {
        var result = 0;
        $('table tr.table-row').each(function () {
            var amount = parseInt($(this).find(".txt-amount").val());
            var priceString = $(this).find(".current-price").html();
            var replaced = priceString.replace(",", ".");
            var price = parseFloat(replaced);
            result += price * amount;
        });
        var total = result.toFixed(2).toString().replace(".", ",");
        $(".total").html(total);
    });

    $(".goto-payment").on("click", function () {
        var client = new Client();
        var cartCode = $(".cartCode").val();
        var table = $(".items > tbody");
        var total = $(".total").html().replace(",", ".");
        table.find('tr').each(function () {
            var itemId = $(this).find('.itemId').val();
            var tds = $(this).find('td');
            tds.each(function() {
                var quantity = $(this).find('.txt-amount').val();
                if (typeof (quantity) !== 'undefined') {
                    client.updateCart(cartCode, itemId, quantity, total);
                }
            });
        });

    });

    $(".remove-item").on("click", function () {
        var productId = $(this).data("productid");
        var client = new Client();
        client.removeItem(productId);
    });

});
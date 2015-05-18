
var loadTotal = function() {
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


    $(".remove-item").on("click", function() {
        var productId = $(this).data("productid");
        var client = new Client();
        client.removeItem(productId);
    });

});
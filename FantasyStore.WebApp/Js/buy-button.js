

var MvcClient = (function () {
    function mvcClient() { }

    mvcClient.prototype.addToCart = function (productId, quantity) {
        var serviceUrl = "http://localhost:26722/Checkout/Add/?productId=" + productId + "&quantity=" + quantity;
        
        var ajaxConfig = {
            url: serviceUrl,
            type: 'GET',
        };

        $.ajax(ajaxConfig)
            .done(function() {
                window.location.assign("http://localhost:26722/Checkout/Cart");
            });
    };

    return mvcClient;
})();


$(document).ready(function () {
    $(".buy-button").on("click", function (e) {
        e.preventDefault();
        var productId = $(this).data("productid");
        var quantity = $(".qntd").val();
        var mvcClient = new MvcClient();
        if (typeof (quantity) === 'undefined') {
            mvcClient.addToCart(productId, 1);
        } else {
            if (quantity === null || quantity === "") {
                alert("Informe a quantidade de produtos que deseja comprar");
            } else {
                mvcClient.addToCart(productId, quantity);
            }
        }
        
    });


});
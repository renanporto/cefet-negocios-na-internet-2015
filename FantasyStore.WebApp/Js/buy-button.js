

var MvcClient = (function () {
    function mvcClient() { }

    mvcClient.prototype.addToCart = function (productId) {
        var serviceUrl = "http://localhost:26722/Checkout/Add/?productId=" + productId;
        
        var ajaxConfig = {
            url: serviceUrl,
            type: 'GET',
        };

        $.ajax(ajaxConfig)
            .done(function(data) {
                alert(data);
            });
    };

    return mvcClient;
})();


$(document).ready(function () {
    $(".buy-button").on("click", function (e) {
        e.preventDefault();
        var productId = $(this).data("productid");
        var mvcClient = new MvcClient();
        mvcClient.addToCart(productId);
    });


});
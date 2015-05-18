

var OrderClient = (function () {
    function orderClient() { }

    orderClient.prototype.confirm = function () {
        var serviceUrl = "http://localhost:26722/Checkout/Payment/?productId=" + productId;

        var ajaxConfig = {
            url: serviceUrl,
            type: 'GET',
        };

        $.ajax(ajaxConfig)
            .done(function () {
                location.reload();
            });
    };

    return orderClient;
})();

$(document).ready(function () {
    

});
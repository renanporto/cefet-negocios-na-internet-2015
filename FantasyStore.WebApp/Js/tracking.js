
var TrackingClient = (function () {
    function trackingClient() { }

    trackingClient.prototype.search = function (orderNumber) {
        var serviceUrl = "http://localhost:26722/Order/Search/?orderNumber=" + orderNumber;

        var ajaxConfig = {
            url: serviceUrl,
            type: 'GET',
        };

        $.ajax(ajaxConfig)
            .done(function (data) {
                if (data === null) {
                    $(".not-found").html("Não foi encontrado nenhum pedido com o número " + orderNumber);
                } else {
                    hideElement(".searching");
                    $(".search-result").html(data);
                }
            })
            .fail(function() {
                hideElement(".searching");
            });
    };

    return trackingClient;
})();

var hideElement = function(selector) {
    $(selector).css('display', 'none');
};

var showElement = function (selector) {
    $(selector).css('display', 'block');
};

$(document).ready(function () {

    $(".search").on("click", function(e) {
        e.preventDefault();
        $(".searching").html("Buscando...");
        showElement(".searching");
        var number = $(".order-number").val();
        var client = new TrackingClient();
        client.search(number);
    });

});
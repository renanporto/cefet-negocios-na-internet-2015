

var hideElement = function (selector) {
    $(selector).css('display', 'none');
};

var showElement = function (selector) {
    $(selector).css('display', 'block');
};



var WishListClient = (function () {
    function wishListClient() { }

    wishListClient.prototype.searchByName = function (name) {
        var serviceUrl = "http://localhost:26722/WishLists/SearchByName/?name=" + name;

        var ajaxConfig = {
            url: serviceUrl,
            type: 'GET',
        };

        $.ajax(ajaxConfig)
            .done(function (data) {
                if (data === "404") {
                    hideElement(".search-result");
                    hideElement(".searching");
                    $(".not-found").html("Não foi encontrada nenhuma lista com o nome " + name);
                    showElement(".not-found");
                } else {
                    hideElement(".not-found");
                    hideElement(".searching");
                    $(".search-result").html(data);
                    showElement(".search-result");
                }
            });
    };
    
    wishListClient.prototype.searchByCode = function (code) {
        var serviceUrl = "http://localhost:26722/WishLists/SearchByCode/?code=" + code;

        var ajaxConfig = {
            url: serviceUrl,
            type: 'GET',
        };

        $.ajax(ajaxConfig)
            .done(function (data) {
                if (data === "404") {
                    hideElement(".search-result");
                    hideElement(".searching");
                    $(".not-found").html("Não foi encontrada nenhuma lista com o código " + code);
                    showElement(".not-found");
                } else {
                    hideElement(".not-found");
                    hideElement(".searching");
                    $(".search-result").html(data);
                    showElement(".search-result");
                }
            });
    };

    return wishListClient;
})();




$(document).ready(function () {

    $(".search").on("click", function (e) {
        e.preventDefault();
        var criteria = $(".ddl-criteria").val();
        var term = $(".search-box").val();
        var client = new WishListClient();
        $(".searching").html("Buscando...");
        showElement(".searching");
        if (criteria === "1") {
            client.searchByCode(term);
        } else {
            client.searchByName(term);
        }
    });

});
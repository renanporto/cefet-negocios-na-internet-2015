



var ProductsClient = (function () {
    function productsClient() { }

    productsClient.prototype.addToList = function (productId, listId) {
        var serviceUrl = "http://localhost:26722/WishLists/AddToList/?listId=" + listId + "&productId=" + productId;

        var ajaxConfig = {
            url: serviceUrl,
            type: 'GET',
        };

        $.ajax(ajaxConfig)
            .done(function (data) {
                alert("Item adicionado com sucesso");
                window.location.assign("http://localhost:26722/WishLists/Details/" + data);
            });
    };

    return productsClient;
})();

$(document).ready(function () {

    $(".add-to-list").on("click", function (e) {
        $(".wishlist-wrapper").css('display', 'block');
    });

    $(".add-to-list-req").on("click", function (e) {
        var client = new ProductsClient();
        var listId = $(".ddl-list").val();
        var productId = $(".productId").html();
        client.addToList(productId, listId);
    });

});
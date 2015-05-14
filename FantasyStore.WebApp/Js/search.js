

var MvcClient = (function () {
    function mvcClient() { }

    mvcClient.prototype.search = function (criteria, term) {
        var serviceUrl = "http://localhost:26722/Search/Query?criteria=" + criteria + "&term=" + term;
        
        var ajaxConfig = {
            url: serviceUrl,
            type: 'GET',
            cache: false,
            contentType: false,
            processData: false,
            timeout: 50000,
        };

        $.ajax(ajaxConfig);
    };

    return mvcClient;
})();

$(document).ready(function () {
    $(".search").on("click", function () {
        var mvcClient = new MvcClient();
        mvcClient.search("1", "1");
    });


});
CardRibbn.db = window.CardRibbn.db || {};
(function () {
    var image_url = CardRibbn.config.entities('images');
    var db = {
        images: {
            get: function () {
                var d = new $.Deferred();
                $.when(BooksMobile.dbREST.loadData(image_url).load()).done(function (response) {
                    d.resolve(response);
                });
                return d.promise();
            },
            getById: function (par) {
                var d = new $.Deferred();
                $.when(BooksMobile.dbREST.loadDataPar(image_url, par).load()).done(function (response) {
                    d.resolve(response);
                });
                return d.promise();
            },
            add: function (par) {
                var d = new $.Deferred();
                $.when(BooksMobile.dbREST.loadDataPar(image_url, par).load()).done(function (response) {
                    d.resolve(response);
                });
                return d.promise();
            },
            del: function (id) {
                var d = new $.Deferred();
                $.when(BooksMobile.dbREST.delete(image_url, id).load()).done(function (response) {
                    d.resolve(response);
                });
                return d.promise();
            }

        }
    };
    $.extend(CardRibbn.db, db);
})();
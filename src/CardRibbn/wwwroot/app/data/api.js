CardRibbn.db = window.CardRibbn.db || {};
(function () {
    var db = {
        api: function (url) {
            return {
                get: function () {
                    var d = new $.Deferred();
                    $.when(CardRibbn.dbREST.loadData(url)).done(function (response) {
                        d.resolve(response);
                    });
                    return d.promise();
                },
                getById: function (par) {
                    var d = new $.Deferred();
                    CardRibbn.dbREST.loadDataPar(url, par).done(function (response) {
                        d.resolve(response);
                    });
                    return d.promise();
                },
                add: function (par) {
                    var d = new $.Deferred();
                    $.when(CardRibbn.dbREST.sendPost(url, par)).done(function (response) {
                        d.resolve(response);
                    });
                    return d.promise();
                },
                delete: function (id) {
                    var d = new $.Deferred();
                    CardRibbn.dbREST.delete(url, id).done(function (response) {
                        d.resolve(response);
                    });
                    return d.promise();
                }

            };
        }
    };
    $.extend(CardRibbn.db, db);
})();
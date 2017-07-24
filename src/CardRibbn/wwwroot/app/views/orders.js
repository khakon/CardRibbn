CardRibbn.views = window.CardRibbn.views || {};
CardRibbn.views.orders = function () {
    var Product = function () {
        return {
            id: ko.observable(0),
            title: ko.observable(''),
            description: ko.observable(''),
            price: ko.observable(0),
            taxes: ko.observable(false)
        };
    };
    var url = CardRibbn.config.entities('orders');
    var searchLiter = ko.observable(''),
        allItems = ko.observableArray([]),
        currentItem = ko.observable('');
    var viewModel = {
        searchLiter: searchLiter,
        allItems: allItems,
        currentItem: currentItem
    };
    viewModel.list = ko.computed(function () {
        var results = viewModel.allItems(),
            filterLiter = viewModel.searchLiter();
        if (filterLiter.length > 1) {
            results = ko.utils.arrayFilter(results, function (item) {
                return item.title.toLowerCase().indexOf(filterLiter.toLowerCase()) > -1 || item.description.toLowerCase().indexOf(filterLiter.toLowerCase()) > -1;
            });
        }
        return results;
    });

    viewModel.add = function () {
        console.log(new Product());
        currentItem(new Product());
    };
    viewModel.edit = function (item) {
        currentItem(new Product());
        currentItem().id(item.id);
        currentItem().title(item.title);
        currentItem().description(item.description);
        currentItem().price(item.price);
        currentItem().taxes(item.taxes);
        $('#myModal').modal('show');
    };
    viewModel.delete = function (item) {
        console.log(item);
        if (!item.id > 0) return;
        CardRibbn.db.api(url).delete(item.id).done(function (response) {
            if (response)
                if (response.success) allItems(response.model);
                else alert(response.message);
        });
    };
    viewModel.save = function (item) {
        var par = {
            id: currentItem().id(), title: currentItem().title(), description: currentItem().description(), price: currentItem().price(), taxes: currentItem().taxes()
        };
        console.log(par);
        CardRibbn.db.api(url).add(par).done(function (response) {
            if (response.success) {
                allItems(response.model);
                $('#myModal').modal('hide');
            }
            else alert(response.message);
        })
    };
    viewModel.clearSearch = function () {
        searchLiter('');
    };
    CardRibbn.db.api(url).get().done(function (response) {
        if (response.success) allItems(response.model);
        else alert(response.message);
    });
    return viewModel;
}
ko.applyBindings(CardRibbn.views.orders());
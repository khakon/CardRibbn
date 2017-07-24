CardRibbn.views = window.CardRibbn.views || {};
CardRibbn.views.collections = function () {
    var Collection = function () {
        return {
            id: ko.observable(0),
            title: ko.observable('')
        };
    };
    var url = CardRibbn.config.entities('collections');
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
        currentItem(new Collection());
    };
    viewModel.edit = function (item) {
        currentItem(new Collection());
        currentItem().id(item.id);
        currentItem().title(item.title);
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
            id: currentItem().id(), title: currentItem().title()
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
ko.applyBindings(CardRibbn.views.collections());
CardRibbn.views = window.CardRibbn.views || {};
CardRibbn.views.customers = function () {
    var Customer = function () {
        return {
            id: ko.observable(0),
            name: ko.observable(''),
            mail: ko.observable(''),
            phone: ko.observable(''),
            adress: ko.observable('')
        };
    };
    var url = CardRibbn.config.entities('customers');
    var searchLiter = ko.observable(''),
        allItems = ko.observableArray([]),
        currentItem = ko.observable('');
    var viewModel = {
        searchLiter: searchLiter,
        allItems: allItems,
        currentItem: currentItem
    };
    viewModel.list = ko.computed(function () {
        var results = allItems(),
            filterLiter = searchLiter();
        if (filterLiter.length > 1) {
            results = ko.utils.arrayFilter(results, function (item) {
                return item.name.toLowerCase().indexOf(filterLiter.toLowerCase()) > -1 || item.mail.toLowerCase().indexOf(filterLiter.toLowerCase()) > -1 || item.adress.toLowerCase().indexOf(filterLiter.toLowerCase()) > -1;
            });
        }
        return results;
    });
    viewModel.add = function () {
        console.log(new Customer());
        currentItem(new Customer());
    };
    viewModel.edit = function (item) {
        currentItem(new Customer());
        currentItem().id(item.id);
        currentItem().name(item.name);
        currentItem().mail(item.mail);
        currentItem().phone(item.phone);
        currentItem().adress(item.adress);
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
            id: currentItem().id(), name: currentItem().name(), mail: currentItem().mail(), phone: currentItem().phone(), adress: currentItem().adress()
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
ko.applyBindings(CardRibbn.views.customers());

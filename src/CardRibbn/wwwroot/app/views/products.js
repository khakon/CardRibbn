CardRibbn.views = window.CardRibbn.views || {};
CardRibbn.views.products = function () {
    var Product = function () {
        return {
            id: ko.observable(0),
            title: ko.observable(''),
            description: ko.observable(''),
            price: ko.observable(0),
            taxes: ko.observable(false),
            type: ko.observable(),
            vendor: ko.observable()
        };
    };
    var Image = function (id) {
        return {
            id: ko.observable(id),
            image: ko.observable('')
        };
    };
    var url = CardRibbn.config.entities('products'),
        url_product_collection = CardRibbn.config.entities('product_collections'),
        url_product_tag = CardRibbn.config.entities('product_tags'),
        url_images = CardRibbn.config.entities('images'),
        url_vendor = CardRibbn.config.entities('vendors'),
        url_type = CardRibbn.config.entities('product_types'),
        url_collection = CardRibbn.config.entities('collections'),
        url_tag = CardRibbn.config.entities('tags');


    var searchLiter = ko.observable(''),
        allItems = ko.observableArray([]),
        types = ko.observableArray([]),
        vendors = ko.observableArray([]),
        currentImage = ko.observable(),
        selectedType = ko.observable(0),
        selectedVendor = ko.observable(0),
        collections = ko.observableArray([]),
        tags = ko.observableArray([]),
        selectedCollections = ko.observableArray([]),
        selectedTags = ko.observableArray([]),
        currentItem = ko.observable('');
    var viewModel = {
        searchLiter: searchLiter,
        allItems: allItems,
        currentItem: currentItem,
        currentImage: currentImage,
        types: types,
        vendors: vendors,
        selectedType: selectedType,
        selectedVendor: selectedVendor,
        tags: tags,
        collections: collections,
        selectedCollections: selectedCollections,
        selectedTags: selectedTags
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
        currentItem(new Product());
    };
    viewModel.addImage = function (item) {
        currentImage(new Image(item.id));
        $('#myImage').modal('show');
    };
    viewModel.addImageDefault = function (item) {
        currentImage(new Image(1111111));
    };
    viewModel.edit = function (item) {
        currentItem(new Product());
        currentItem().id(item.id);
        currentItem().title(item.title);
        currentItem().description(item.description);
        currentItem().price(item.price);
        currentItem().taxes(item.taxes);
        selectedType(item.type.id);
        selectedVendor(item.vendor.id);
        $.when(CardRibbn.db.api(url_product_collection).getById({ id: item.id }), CardRibbn.db.api(url_product_tag).getById({ id: item.id })).done(function (collections, tags) {
            if (collections.success) {
                var result = ko.utils.arrayMap(collections.model, function (item) {
                    return item.collectionId;
                });
                selectedCollections(result);
            }
            else alert(collections.message);
            if (tags.success) {
                var result = ko.utils.arrayMap(tags.model, function (item) {
                    return item.tagId;
                });
                selectedTags(result);
            }
            else alert(tags.message);
            $('#myModal').modal('show');
        });
    };
    viewModel.isNew = function () {
        return !(currentItem().id() === 0);
    }
    viewModel.delete = function (item) {
        console.log(item);
        if (!item.id > 0) return;
        CardRibbn.db.api(url).delete(item.id).done(function (response) {
            if (response)
                if (response.success) allItems(response.model);
                else alert(response.message);
        });
    };
    viewModel.save = function () {
        var product = {
            id: currentItem().id(), title: currentItem().title(), description: currentItem().description()
            , price: currentItem().price(), taxes: currentItem().taxes(), vendorid: selectedVendor(), typeid: selectedType()
        },
            collections = [],
            tags = [];
        ko.utils.arrayForEach(selectedCollections(), function (item) {
            collections.push({ id: 0, collectionId: item, productId: currentItem().id() });
        });
        ko.utils.arrayForEach(selectedTags(), function (item) {
            tags.push({ id: 0, tagId: item, productId: currentItem().id() });
        });
        $.when(CardRibbn.db.api(url).add(product), CardRibbn.db.api(url_product_collection).add({ model: collections }), CardRibbn.db.api(url_product_tag).add({ model: tags })).done(function (products, collections) {
            if (products.success) {
                allItems(products.model);
                $('#myModal').modal('hide');
            }
            else alert(products.message);
            alert(collections.message);
            selectedCollections([]);
        })
    };
    viewModel.saveImage = function () {
        var par = {
            id: currentImage().id(), image: currentImage().image()
        };
        CardRibbn.db.api(url_images).add(par).done(function (response) {
            if (response.success) {
                allItems(response.model);
                $('#myImage').modal('hide');
            }
            else alert(response.message);
        })
    };
    viewModel.clearSearch = function () {
        searchLiter('');
    };
    var init = function () {
        CardRibbn.db.api(url).get().done(function (response) {
            if (response.success) allItems(response.model);
            else alert(response.message);
        });
        CardRibbn.db.api(url_vendor).get().done(function (response) {
            if (response.success) vendors(response.model);
            else alert(response.message);
        });
        CardRibbn.db.api(url_type).get().done(function (response) {
            if (response.success) types(response.model);
            else alert(response.message);
        });
        CardRibbn.db.api(url_collection).get().done(function (response) {
            if (response.success) collections(response.model);
            else alert(response.message);
        });
        CardRibbn.db.api(url_tag).get().done(function (response) {
            if (response.success) tags(response.model);
            else alert(response.message);
        });
    };
    init();
    return viewModel;
}
ko.applyBindings(CardRibbn.views.products());
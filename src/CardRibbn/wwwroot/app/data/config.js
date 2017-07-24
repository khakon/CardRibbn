CardRibbn.config = window.CardRibbn.config || {};
(function () {
    var config = {
        entities: function (entity) {
            var urls = {
                customers: '/api/customers',
                products: '/api/products',
                orders: '/api/orders',
                product_types: '/api/product_types',
                product_collections: '/api/product_collections',
                product_tags: '/api/product_tags',
                images: '/api/images',
                vendors: '/api/vendors',
                product_types: '/api/product_types',
                collections: '/api/collections',
                tags: '/api/tags'
            }
            return urls[entity];
        }
    };
    $.extend(CardRibbn.config, config);
})();
(function ($) {
    ko.bindingHandlers.on = {
        init: function (element, valueAccessor, allBindings, viewModel) {
            var config = valueAccessor(),
                domNode = $(element);

            for (var rule in config) if (config.hasOwnProperty(rule)) {
                var handler = createEventHandlerFor(config, rule);
                rule = rule.split(/\s+/, 2);
                if (rule[1]) {
                    domNode.on(rule[0], rule[1], handler);
                } else {
                    domNode.on(rule[0], handler);
                }
            }
        }
    };

    function lookupMethodIn(context, methodName) {
        var scopes = [context.$data].concat(context.$parents),
            i = 0,
            count = scopes.length;

        do {
            var scope = scopes[i];
        } while (++i < count && !(scope[methodName] && scope[methodName].call));

        if (!scope[methodName] || !scope[methodName].call) {
            throw new Error('Unknown method "' + methodName + '" in context');
        }
        return scope[methodName].bind(scope);
    }

    function createEventHandlerFor(config, rule) {
        var methodName = config[rule],
            dataKey = ko.utils.unwrapObservable(config.data);

        return function (event) {
            var context = ko.contextFor(this),
                data = $(this).data(dataKey);

            if (data.bind) {
                delete data.bind;
            }
            var method = lookupMethodIn(context, methodName);
            var result = method(data, context.$data, event);
            if (result !== true) {
                event.preventDefault();
            }
        };
    }
})(jQuery);
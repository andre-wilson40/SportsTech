// base namespace
var site = site || {};

if (typeof String.prototype.startsWith != 'function') {
    // see below for better implementation!
    String.prototype.startsWith = function (str) {
        return this.indexOf(str) == 0;
    };
}

site.core = {

    dateF: function () {
        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1; //January is 0!

        var yyyy = today.getFullYear();

        if (dd < 10) {
            dd = '0' + dd;
        }

        if (mm < 10) {
            mm = '0' + mm;
        }

        return dd + '/' + mm + '/' + yyyy;
    },

    // Returns 0 if the value supplied is not a number otherwise the integer representation  
    numberOrZero: function (value) {
        if (isNaN(value)) {
            return 0;
        } else {
            return value;
        }
    },

    wrapControlAndValidationMessage: function ($validationMessage) {
        var controlId, $control;

        controlId = $validationMessage.attr('id');
        $control = $('#' + controlId);

        $control.add($control.nextUntil($validationMessage).add($validationMessage)).wrapAll('<div class="validated-control-container inline-block">');
    },

    createDatePicker: function (selector) {
        return $(selector).Zebra_DatePicker({ format: 'd/m/Y' });
    },

    toggleOverlay: function ($element, overlayId, showOverlay) {
        showOverlay ? this.addOverlay($element, overlayId) : this.removeOverlay("#" + overlayId);
    },

    addSpinnerOverlay: function ($element, overlayId) {

        // If the overlay already exists we don't need to re-add it
        if ($element.children(overlayId).length) return;

        var $overlay = this.addOverlay($element, overlayId);
        $overlay.html('<div class="spinner" ">');
    },

    addOverlay: function ($element, overlayId) {
        var pos = $.extend({
            width: $element.outerWidth(),
            height: $element.outerHeight()
        }, $element.position());

        var $overlay = $('<div>', {
            id: overlayId,
            css: {
                position: 'absolute',
                top: pos.top,
                left: pos.left,
                width: $element.width(),
                height: $element.height(),
                backgroundColor: '#fff',
                opacity: 0.50
            }
        });

        $overlay.appendTo($element);

        return $overlay;
    },

    removeOverlay: function (overlayId) {
        $(overlayId).remove();
    },

    redirect: function (url) {
        window.location.replace(url);
    }
};

$.extend({

    ajaxLoad: function (element$, options) {

        options.success = options.success || function (html) {
            element$.html(html);
        };

        $.ajaxUnobtrusive(options);
    },

    // $.ajaxWithRedirect
    //
    // Provides a transparent implemention of the jQuery $.ajax method with the additional alternative of being
    // able to automatically redirect when a Json object is returned with the RedirectUrl parameter.
    //
    // If the caller wants to perform or cancel the redirect they can provide a beforeRedirect function
    // and return false.        
    ajaxWithRedirect: function (options) {
        "use strict";

        var deferred = $.Deferred(), successHandler, errorHandler, xhr;

        // force no-cache
        options.cache = false;

        // get a copy of the success handler...
        successHandler = options.success;

        errorHandler = options.error;

        options.error = function (data) {

            //supress the callers error callback, since the options.statusCode below will redirect anyway, or, a status of zero indicates the window is navigating away to another page.
            if (data.status == 0 || data.status == 401 || options.status == 403) {
                return;
            }

            if (typeof errorHandler === 'function') {
                errorHandler.apply(null, arguments);
                deferred.resolve.apply(null, arguments);
            } else {
                alert('Oops unfortunately something went wrong.');
            }
        };

        // ... and replace it with this one
        options.success = function (data) {
            var contentType = xhr.getResponseHeader("Content-Type"),
                performRedirect = true,
                redirectUrl = null,
                args = [].slice.call(arguments, 0) || {},
                json;

            // If response isn't there or isn't json,
            // skip all the redirect logic
            if (data && (/json/i).test(contentType)) {
                // If json was requested, and json received, jQuery will
                // have parsed it already. Otherwise, we'll have to do it
                if (options.dataType === 'json') {
                    redirectUrl = data.RedirectUrl;
                } else {
                    try {
                        json = $.parseJSON(data);
                        redirectUrl = json.RedirectUrl;
                    } catch (e) {
                        // not json
                    }
                }

                // check the redirect url
                if (redirectUrl && typeof redirectUrl === 'string') {
                    // Is there a beforeRedirect handler?
                    if (typeof options.beforeRedirect === 'function') {
                        // pass all the arguments to the beforeRedirect handler
                        performRedirect = options.beforeRedirect.apply(null, args);
                    }

                    // unless strictly false, go ahead with the redirect
                    if (performRedirect !== false) {
                        site.core.redirect(redirectUrl);
                        // and stop here. No success and/or deferred handlers
                        // will be called since we're redirecting anyway
                        return;
                    }
                }
            }

            // no redirect; forward everything to the success handler(s)
            if (typeof successHandler === 'function') {
                successHandler.apply(null, args);
                deferred.resolve.apply(null, args);
            }
        };

        // Handling when the user is currently not authorized but they have tried to implement an ajax related action
        options.statusCode =
        {
            401: function (data) {
                var redirectUrl = (data && data.responseText && data.responseText.length > 0) ? data.responseText : "/";

                site.core.redirect(redirectUrl);
            },
            403: function (data) {
                site.core.redirect("/");
            }
        };

        // Make the request
        xhr = $.ajax(options);

        // Forward the deferred promise method(s)
        xhr.fail(deferred.reject);
        xhr.progress(deferred.notify);

        // Replace the ones already on the xhr obj
        deferred.promise(xhr);

        return xhr;
    },

    // $.ajaxUnobtrusive
    //
    // Uses the ajaxWithRedirect to provide a transparent interface by which we can make ajax
    // requests and have any returned html automatically ensure the elements are bound using jQuery validation.
    //
    // Otherwise the elements will not validate when they are added to the dom
    //
    // returns the xhr provided by the underlying $.ajax method
    ajaxUnobtrusive: function (options) {
        "use strict";

        // copy the actual success handler that has been defined
        var successHandler = options.success;

        options.success = function () {
            // forward to event success handler prior to validation to ensure
            // any html is loaded as necessary
            if (typeof successHandler === 'function') {
                successHandler.apply(null, [].slice.call(arguments, 0));

                // rebind the newly loaded validation elements       
                if (typeof options.dynamicValidation != 'undefined') {
                    $.validator.unobtrusive.parseDynamicContent(options.dynamicValidation);
                }
            }
        };

        // fall through to our redirect ajax method
        return $.ajaxWithRedirect(options);
    }
});
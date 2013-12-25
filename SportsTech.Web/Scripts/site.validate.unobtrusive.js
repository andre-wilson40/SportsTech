/*!
 * Site specific unobtrusive validation
 *
 * NOTE:  If changes and updates are made to either the jquery.validate.unobtrusive.js
 *        file those changes will need to be replicated into the min as the min is what
 *        is used when the site is deployed in release mode or debug=false in web.config
 *
 *        We got these changes from:  http://nickstips.wordpress.com/2011/08/18/asp-net-mvc-displaying-client-and-server-side-validation-using-qtip-tooltips/
 */

$(function () {

    // Run this function for all validation error messages
    $('.field-validation-error').each(function () {
        // Get the name of the element the error message is intended for
        // Note: ASP.NET MVC replaces the '[', ']', and '.' characters with an
        // underscore but the data-valmsg-for value will have the original characters
        var inputElem = '#' + $(this).attr('data-valmsg-for').replace('.', '_').replace('[', '_').replace(']', '_');

        var corners = ['left center', 'right center'];
        var flipIt = $(inputElem).parents('span.right').length > 0;

        // Hide the default validation error
        $(this).hide();

        // Show the validation error using qTip
        $(inputElem).filter(':not(.valid)').qtip({
            content: { text: $(this).text() }, // Set the content to be the error message
            position: {
                my: corners[flipIt ? 0 : 1],
                at: corners[flipIt ? 1 : 0],
                viewport: $(window)
            },
            show: { ready: true },
            hide: false,
            style: { classes: 'qtip-red' }
        });
    });

});
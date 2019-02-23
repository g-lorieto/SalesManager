$(document).ready(function () {

    var wrapper = $('.add-items');

    $(".btn-add-item").click(function (e) {
        e.preventDefault();

        var clone = $('.add-item:first-child').clone(true);

        counter = clone.data('counter') + 1;

        clone.attr('data-counter', counter);

        clone.find('[name]').attr('name', function (_, name) {
            return name.replace(/\[\d+\]/, '[' + counter + ']');
        });

        clone.appendTo(wrapper);

        $('.add-item .btn-remove-item').show();
    });

    $('.btn-remove-item').click(function (e) {
        e.preventDefault();
        $(this).parents('.add-item').remove();

        removeButton();
    });

    function removeButton() {
        if ($('.add-item').length == 1) {
            $('.add-item .btn-remove-item').hide();
        }
    }
});
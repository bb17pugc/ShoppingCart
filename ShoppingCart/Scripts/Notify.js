Notify = function (text, callback, close_callback, style) {

    var time = '10000';
    var $container = $('#notifications');
    $container.empty();
    var icon = '<img id="ProductImage" style="width:40px;height:40px;" />';

    if (typeof style == 'undefined') style = 'warning'

    var html = $('<div class="alert alert-' + style + '  hide" style = "width : 100%">' + icon + " " + text + '</div>');

    $('<a>', {
        text: '×',
        class: 'button close',
        style: 'padding-left: 10px;',
        href: '#',
        click: function (e) {
            e.preventDefault()
            close_callback && close_callback()
            remove_notice()
        }
    }).prependTo(html)

    $container.append(html)
    html.removeClass('hide').hide().fadeIn('fast')

    function remove_notice() {
        html.stop().fadeOut('fast').remove()
    }

    var timer = setInterval(remove_notice, time);

    $(html).hover(function () {
        clearInterval(timer);
    }, function () {
        timer = setInterval(remove_notice, time);
    });

    html.on('click', function () {
        clearInterval(timer)
        callback && callback()
        remove_notice()
    });


}
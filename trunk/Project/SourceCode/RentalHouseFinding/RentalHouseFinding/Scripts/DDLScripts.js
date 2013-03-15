$(document).ready(function () {
    $('select.select').each(function () {
        var title = $(this).attr('title');
        var id = $(this).attr('id');
        if ($('option:selected', this).val() != '') {
            title = $('option:selected', this).text();
        } else {
            $('option:selected', this).val('0');
            title = $('option:selected', this).val('0').text();
        }
        $(this)
                .css({ 'opacity': 0 })
                .after('<span class="select" id="sp' + id + '">' + title + '</span>')
                .change(function () {
                    val = $('option:selected', this).text();
                    $(this).next().text(val);
                })
    });
});
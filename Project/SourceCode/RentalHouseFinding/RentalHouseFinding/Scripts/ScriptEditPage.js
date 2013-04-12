

$(document).ready(function () {
    var idDis = $("#DistrictId option:selected").val();
    var select = $("#locations");
    select.empty();
    $.getJSON("/service/GetLocationWithDistrictId", { id: idDis },
                function (myData) {
                    $.each(myData, function (index, itemData) {
                        if (itemData.Text.length != 0 && itemData.Value != idDis) {
                            select.append($('<option/>', {
                                value: itemData.Value,
                                text: itemData.Text
                            }));
                        }
                    });
                    select.listbox({
                        'class': 'listBox',
                        'searchbar': true,
                        'multiselect': false
                    })
                    $('.listBox .lbjs-item').live('click', function () {
                        $('input.tag').addNew($(this).html(), $(this).attr("id"));
                    });
                });

});
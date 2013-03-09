//HungCQ
$(document).ready(function () {
    //For page postback
    var idPro = $("#ProvinceId option:selected").val();
    var select = $("#DistrictId");
    select.empty();
    select.append($('<option/>', {
        value: '',
        text: '-- Chọn Quận/Huyện --'
    }));
    if (idPro != 0) {
        $.getJSON("service/GetDistrictList", { id: idPro },
                function (myData) {
                    $.each(myData, function (index, itemData) {
                        if (itemData.Text.lenght != 0) {
                            select.append($('<option/>', {
                                value: itemData.Value,
                                text: itemData.Text
                            }));
                        }
                    });
                });
    };
    $("#ProvinceId").change(function () {
        var idPro = $("#ProvinceId option:selected").val();
        var select = $("#DistrictId");
        select.empty();
        select.append($('<option/>', {
            value: '',
            text: '-- Chọn Quận/Huyện --'
        }));
        $.getJSON("service/GetDistrictList", { id: idPro },
        function (myData) {
            $.each(myData, function (index, itemData) {
                if (itemData.Text.lenght != 0) {
                    select.append($('<option/>', {
                        value: itemData.Value,
                        text: itemData.Text
                    }));
                }
            });
        });
    });
//    $("#KeyWord").change(function () {
//        $.getJSON("service/GetFullTextSuggestion?categoryId=1&provinceId=1&districtId=1&keyWord=post", {  },
//        function (myData) {
//            alert(myData);
//            $.each(myData, function (index, itemData) {
//                if (itemData.Text.lenght != 0) {
//                    select.append($('<option/>', {
//                        value: itemData.Value,
//                        text: itemData.Text
//                    }));
//                }
//            });
//        });
//    });

//    $('#KeyWord').autocomplete("service/GetFullTextSuggestion?categoryId=1&provinceId=1&districtId=1&keyWord=" + $('#KeyWord').val(), {
//        dataType: 'json',
//        parse: function (data) {
//            var rows = new Array();
//            for (var i = 0; i < data.length; i++) {
//                rows[i] = { data: data[i], value: data[i].Text };
//            }
//            return rows;
//        },
//        formatItem: function (row, i, n) {
//            return row.Text;
//        }
//    });

    $("#KeyWord").autocomplete({
        source: function (request, response) {  
            if(
            $.ajax({
                url: "service/GetFullTextSuggestion", type: "GET", dataType: "json",
                data: { categoryId: $("#CategoryId").val(), provinceId: $("#ProvinceId").val(), districtId: $("#DistrictId").val(), keyWord: request.term },
                success: function (data) {
                    response($.map(data, function (item) {

                        return { label: item.Text, value: item.Value }; //updated code
                    }));
                }
            });
        },
        select: function (event, ui) {
            $("#MovieID").val(ui.item.value);
            $("#KeyWord").val(ui.item.Text);
            return false;
        }
    });
});
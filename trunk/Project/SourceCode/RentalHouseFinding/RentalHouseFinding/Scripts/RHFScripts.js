//HungCQ
$(document).ready(function () {
    //For page postback
    var idPro = $("#ProvinceId option:selected").val();
    var select = $("#DistrictId");
    select.empty();
    select.append($('<option/>', {
        value: '0',
        text: 'Chọn Quận/Huyện'
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
            value: '0',
            text: 'Chọn Quận/Huyện'
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

    $("#KeyWord").autocomplete({
        source: function (request, response) {  
            if($("#ProvinceId option:selected").val() == 0)
            {
                alert("Vui lòng chọn tỉnh thành phố");
                return false;
            }
            $.ajax({
                url: "service/GetFullTextSuggestion", type: "GET", dataType: "json",
                data: { categoryId: $("#CategoryId option:selected").val(), provinceId: $("#ProvinceId option:selected").val(), districtId: $("#DistrictId option:selected").val(), keyWord: request.term, skip: 0, take: 10 },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { value: item.Text, id: item.Value };
                    }));
                }
            });
        },
        select: function (event, ui) {
            $("#PostIdSuggest").val(ui.item.id);
            $("#KeyWord").val(ui.item.label);
            return false;
        }
    });
});
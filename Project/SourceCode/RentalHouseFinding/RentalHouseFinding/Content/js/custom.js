function toggle_visibility(id) {
    var e = document.getElementById(id);

    if (e.id == 'resultPanel') {
        $('#resultContent').html('<img src="/Content/images/ajax-loader.gif")" />');
        $.ajax({
            url: '/service/GetAllUserFavorite?callback=?',
            contentType: 'application/html; charset=utf-8',
            type: 'GET',
            dataType: 'html'
        })
        .success(function (result) {
            $('#resultContent').html(result);
            $('input:checkbox').screwDefaultButtons({
                image: 'url("/Content/images/index_cbx.png")',
                width: 17,
                height: 17
            });
        })
        .error(function (xhr, status) {
            xhr.abort();
        });
    }
    if (e.style.display == 'block') {
        e.style.display = 'none';
        if (e.id == 'listInfo') {
            $('#infoMap').width('1040px');
            document.getElementById('btnShowList').setAttribute('class', 'btnShowListNormal');
            var currCenter = window.map.getCenter();
            google.maps.event.trigger(map, 'resize');
            map.setCenter(currCenter);
        }
    } else {
        e.style.display = 'block';
        if (e.id == 'listInfo') {
            $('#infoMap').width('710px');
            document.getElementById('btnShowList').setAttribute('class', 'btnShowListClicked');
            var currCenter = window.map.getCenter();
            google.maps.event.trigger(map, 'resize');
            map.setCenter(currCenter);
        }
    }
}
function removeFavoriteInList(id) {
    if (confirm("Bạn có chắc chắn muốn xóa bài này khỏi danh sách?")) {
        $.getJSON("/Post/RemoveFavorite", { id: id }, function (success) {
            if (success) {
                $('#resultContent').html('<img src="/Content/images/ajax-loader.gif")" />');
                $.ajax({
                    url: '/service/GetAllUserFavorite?callback=?',
                    contentType: 'application/html; charset=utf-8',
                    type: 'GET',
                    dataType: 'html'
                })
            .success(function (result) {
                $('#resultContent').html(result);
                $('input:checkbox').screwDefaultButtons({
                    image: 'url("/Content/images/index_cbx.png")',
                    width: 17,
                    height: 17
                });
            })
            .error(function (xhr, status) {
                xhr.abort();
            });
            }
        });
    }
}
function ComparePost() {    
    if ($("input:checkbox:checked").length > 3) {
        alert("Hiện tại chúng tôi chỉ hỗ trợ so sánh tối đa 3 bài đăng");
        return false;
    } else {
        var data = "";
        for (var i = 0; i < $("input:checkbox:checked").length; i++) {
            data += $("input:checkbox:checked")[i].id + "|";
    }
    $.ajax({
        url: '/service/ComparePost?callback=?',
        contentType: 'application/html; charset=utf-8',
        type: 'GET',
        dataType: 'html',
        data: {
            lstId : data
        }
    })
    .success(function (result) {
        $.fancybox({
            'content': result,
            'padding': 20
        });
    })
    .error(function (xhr, status) {
        xhr.abort();
    });
        
    }
}

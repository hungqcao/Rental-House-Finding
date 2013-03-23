function toggle_visibility(id) {
    var e = document.getElementById(id);
    var currCenter = window.map.getCenter();

    if (e.style.display == 'block') {
        e.style.display = 'none';
        if (e.id == 'listInfo') {
            $('#infoMap').width('1040px');
            document.getElementById('btnShowList').setAttribute('class', 'btnShowListNormal');
            google.maps.event.trigger(map, 'resize');
            map.setCenter(currCenter);
        }
    } else {
        e.style.display = 'block';
        if (e.id == 'listInfo') {
            $('#infoMap').width('710px');
            document.getElementById('btnShowList').setAttribute('class', 'btnShowListClicked');
            google.maps.event.trigger(map, 'resize');
            map.setCenter(currCenter);
        }
    }
}
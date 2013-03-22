function toggle_visibility(id) {
    var e = document.getElementById(id);

    if (e.style.display == 'block') {
        e.style.display = 'none';
        if (e.id == 'listInfo') {
            $('#infoMap').width('1024px');
            document.getElementById('btnShowList').setAttribute('class', 'btnShowListNormal');
        }
    } else {
        e.style.display = 'block';
        if (e.id == 'listInfo') {
            $('#infoMap').width('690px');
            document.getElementById('btnShowList').setAttribute('class', 'btnShowListClicked');
        }
    }
}
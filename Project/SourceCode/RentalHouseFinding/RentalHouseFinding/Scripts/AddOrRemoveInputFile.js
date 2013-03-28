var cellTemplates = new Array();
cellTemplates[0] = '<div id="field{counter}" class="uploadItem"><label>Chọn ảnh: </label>';
cellTemplates[1] = '<input id="image{counter}" name="images" type="file" />';
cellTemplates[2] = '<a href="#" id="lnkRemoveFile{counter}" class="removeImage" onclick="return removeFile(this);">Xóa</a></div>';
var counter = 0;

//adds a file input row
function addFile(description) {
    //increment counter
    counter++;
    var html = "";
    for (var i = 0; i < cellTemplates.length; i++) {

        //format the cell template text
        html += cellTemplates[i].replace(/\{counter\}/g, counter).replace(/\{value\}/g,
                    (description == null) ? '' : description);
    }
    $("#uploadFile").append(html);
}
//removes a file input row
function removeFile(obj) {
    $('html, body').animate({
        scrollTop: $("#uploadFile").offset().top
    }, 500);
    $(obj).parent().remove();
}
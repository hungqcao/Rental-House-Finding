var cellTemplates = new Array();
cellTemplates[0] = '<div id="field{counter}" class="uploadItem"><label>Chọn ảnh: </label>';
cellTemplates[1] = '<input id="image{counter}" name="images" type="file" />';
cellTemplates[2] = '</div>';
var counter = 0;

//adds a file input row
function addFile(description) {
    //increment counter
    counter++;
    if (counter <= 11) {
        var html = "";
        for (var i = 0; i < cellTemplates.length; i++) {

            //format the cell template text
            html += cellTemplates[i].replace(/\{counter\}/g, counter).replace(/\{value\}/g,
                    (description == null) ? '' : description);
        }
        $("#uploadFile").append(html);
    } else {
        alert('Vượt quá số lượng file qui định');
    }
}
//removes a file input row
function removeFile(obj) {
    if (counter == 0) {
        $("#field" + counter).remove();
        var html = '<div id="field0"><label>Chọn ảnh: </label><input id="file0" name="images" type="file" /><a href="javascript:;" onclick="return removeFile();" class="removeImage">Thêm ảnh</a><a href="javascript:;" onclick="return addFile();" class="addImage">Thêm ảnh</a></div>';
        $("#uploadFile").append(html);
    } else {
        $("#field" + counter).remove();
        counter--;
    }
    return false;
}
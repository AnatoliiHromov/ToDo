$(document).ready(function () {
    // Стандарный input для файлов
    var fileInput = $('#file-field');
    
    // ul-список, содержащий миниатюрки выбранных файлов
    var img = $('#uimg');
    ////////////////////////////////////////////////////////////////////////////
    
    // Отображение выбраных файлов и создание миниатюр
    function displayFiles(files) {
        var imageType = /image.*/;
        
        $.each(files, function (i, file) {

            // Отсеиваем не картинки
            if (!file.type.match(imageType)) {
                var msg = $("[data-valmsg-for='file-field']").text("asdffg");
                alert(msg.text);
                return false;
            }
            else {


                var reader = new FileReader();
                reader.onload = (function (aImg) {
                    return function (e) {
                        aImg.attr('src', e.target.result);
                        aImg.attr('width', 150);
                    };
                })(img);

                reader.readAsDataURL(file);
            }
        });
        
        
    }
    
    
    ////////////////////////////////////////////////////////////////////////////


    // Обработка события выбора файлов через стандартный input
    // (при вызове обработчика в свойстве files элемента input содержится объект FileList,
    //  содержащий выбранные файлы)
    fileInput.bind({
        change: function() {
            displayFiles(this.files);
        }
    });
});
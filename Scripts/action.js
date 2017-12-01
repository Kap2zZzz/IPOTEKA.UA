

var UserId = null;


var SelectId = 25;

function Delete() {
    if (SelectId == null) {
        alert('Зазначте елемент на списку');
    }
    else {

        if (confirm("Ви дійсно бажаєте видалити даного користувача ?")) {
            var url = '@Url.Action("Delete","Admin")';
            //alert('delete');
            $.ajax({
                url: url,
                type: 'GET',
                data: { id: SelectId },
                dataType: "html",
                success: function (result) {
                    //$("#tb-res #tr-res").removeClass();
                    location.reload();
                }
            })
        }
    }
}

/*Вибір елементу з таблиці*/
$("#tb-res #tr-res").click(function () {

    SelectId = $(this).find('td:first').html();

    if ($(this).hasClass('tb-select')) {
        $(this).removeClass();
        SelectId = null;
        //$('#AppView').html(null);
        //$('#AppView').hide();
    }
    else {
        $(this).addClass('tb-select').siblings().removeClass('tb-select');
        //$.ajax({
        //    url: url,
        //    type: 'GET',
        //    data: { id: AppId },
        //    dataType: "html",
        //    success: function (partialView) {
        //        $('#AppView').html(partialView);
        //        $('#AppView').show();
        //    }
        //success: function () {
        //    alert("success");
        //}
        //});
    }
});
function GetCartCount() {
    $.ajax({
        type: "GET",
        url: "/Cart/TotalItems",
        dataType: "json",
        success: function (data) {
            $("#CartCount").html(data);
        }
    });
}


$(window).load(function () {
    $.ajax({
        type: "GET",
        url: "/Cart/TotalItems",
        dataType: "json",
        success: function (data) {
            $("#CartCount").html(data);
        }
    });
});

function AddtoCart(id) {
    debugger;
    contentType = false;
    processData = false;
    $.ajax({
        type: "POST",
        url: '/Cart/AddtoCart/' + id,
        dataType: "json",
        contentType: contentType,
        processData: processData,
        success: function (result) {
            if (result.NoItem != null) {

            }
            else {
                Notify(result.Message, null, null, 'success');
                debugger;
                $("#ProductImage").attr('src', result.ImagePath);
                GetCartCount();
            }
        }
    });
}
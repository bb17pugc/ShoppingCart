
$(document).ready(function () {
    
});
function Menu() {
    var i;
    if ($("#MenuDiv").hasClass("CloseMenu")) {
        $("#MenuDiv").addClass("OpenMenu");
        $("#MenuDiv").removeClass("CloseMenu");
    }
    else {
        $("#MenuDiv").removeClass("OpenMenu");
        $("#MenuDiv").addClass("CloseMenu");
    }
}
$("#MaTP").change(function () {
    var maTP = $(this).val()
    debugger
    $.ajax({
        type: "post",
        url: "/SignIn/GetDSQuan?MaTP=" + maTP,
        contentType: "html",
        success: function (response) {
            debugger
            $("#MaQuan").empty();
            $("#MaQuan").append(response)
        }
    })
})
$("#MaQuan").change(function () {
    var maQuan = $(this).val()
    debugger
    $.ajax({
        type: "post",
        url: "/SignIn/GetDSPhuong?MaQuan=" + maQuan,
        contentType: "html",
        success: function (response) {
            debugger
            $("#MaPhuong").empty();
            $("#MaPhuong").append(response)
        }
    })
})
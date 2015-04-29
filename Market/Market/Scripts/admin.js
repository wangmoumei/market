$(document).ready(function(e) {
    document.getElementById("loading").style.display = "none";
    $("#loading").ajaxStart(function () {
        $(this).show();
    });
    document.getElementById("sub").onclick = function () {
        var UserID = $("#UserID").val();
        var Password = $("#Password").val();
        if (UserID == "") {
            document.getElementById("UserID").focus();
            document.getElementById("UserID").className = "error";
            document.getElementById("Password").className = "";
            return 0;
        }
        if (Password == "") {
            document.getElementById("Password").focus();
            document.getElementById("Password").className = "error";
            document.getElementById("UserID").className = "";
            return 0;
        }
        $.ajax({
            url: "/Admin/Login",
            type: "POST",
            dataType: "json",
            data: "UserID=" + UserID + '&Password=' + Password,
            success: function (data) {
                document.getElementById("loading").style.display = "none";
                if (data) {
                    if (data == 1)
                        window.location.href = "/admin/Manage";
                    if (data == 2) {
                        document.getElementById("Password").focus();
                        document.getElementById("Password").className = "error";
                        document.getElementById("UserID").className = "";
                    }
                } else {
                    document.getElementById("loading").style.display = "none";
                    document.getElementById("UserID").focus();
                    document.getElementById("UserID").className = "error";
                    document.getElementById("Password").className = "";
                }
                
            },
            timeout: 10000,
            error: function (data) {
                document.getElementById("loading").style.display = "none";
                document.getElementById("UserID").focus();
                document.getElementById("UserID").className = "error";
                document.getElementById("Password").className = "";
            }
            
        })
    }

});


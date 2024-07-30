var loginUserRole = $('#LoginUserRole').val();
var istransporterchose = false;
$(document).ready(function () {

   
    $('#UserName').on('keypress', function (e) {
        if (e.which == 32) {
            return false;
        }
    });

    $('#Password').on('keypress', function (e) {
        if (e.which == 32) {
            return false;
        }
    });

    $('#UserName').keyup(function (e) {
        this.value = this.value.toLowerCase();
    });

    $('#btnCancel').click(function () {
        location.reload();
    });

    $('#btnSignup').click(function () {
        $(this).attr("disabled", true);
        ClearError();

        var isvalid = false;
        var username = $('#username').val();
        var fullname = $('#fullname').val();
        var password = $('#password').val();
        var phone = $('#phone').val();
        if (username != "") {
            if (fullname != "") {
                if (password != "") {
                    if (phone != "") {
                        isvalid = true;
                    }
                    else {
                        $('#phone').closest(".form-control").addClass("invalid");
                    }
                }
                else {
                    $('#password').closest(".form-control").addClass("invalid");
                }
            }
            else {
                $('#fullname').closest(".form-control").addClass("invalid");
            }
        }
        else {
            $('#username').closest(".form-control").addClass("invalid");
        }
        
        if (isvalid == true) {
            $.ajax({
                type: "POST",
                url: registerUrl,
                data: GetModel(),
                success: function (data) {
                    showMessage(data.MessageType, data.Message);
                }
            })
        } else {
            swal({
                title: "Please Fill all require fields",
                text: "",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-primary",
                type: "error"
            }).catch(swal.noop)
        }
        $(this).attr("disabled", false);

    });


});


function GetModel() {
    var model = {};
    model.UserName = $('#username').val();
    model.Password = $('#password').val();
    model.FullName = $('#fullname').val();
    model.Phone = $('#phone').val();

    return model;
}

function ClearError() {
    $('#username').closest(".form-control").removeClass("invalid");
    $('#password').closest(".form-control").removeClass("invalid");
    $('#fullName').closest(".form-control").removeClass("invalid");
    $('#phone').closest(".form-control").removeClass("invalid");
}

function showMessage(status, message) {
    if (status == 1) {
        swal({
            title: "Account created! Login to proceed",
            text: "",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-primary",
            type: "success"
        }).then((result) => {
            if (result.value) {
                window.location = homeUrl
            }
        });
    }
    else if (status == 2) {
        swal({
            title: message,
            text: "",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-primary",
            type: "error"
        }).catch(swal.noop)
    } else {
        swal({
            title: message,
            text: "",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-primary",
            type: "success"
        }).then((result) => {
            window.location.href = "./Home/Signup";
        });
    }

}

function validateEmail(email) {
    const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}
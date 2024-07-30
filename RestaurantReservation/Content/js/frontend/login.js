$(document).ready(function () {

    $(document).keypress(function (event) {
       
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            $('#btnLogin').prop("disabled", true);
            var instance = Ladda.create(document.querySelector('#btnLogin'));
            instance.start();
            ClearError();

            var isvalid = false;
            var UserName = $('#UserName').val();
            var Password = $('#Password').val();
            if (UserName != "") {
                if (Password != "") {
                    isvalid = true;
                }
                else {
                    $('#Password').closest(".form-control").addClass("invalid");
                    $('#Password-error').show();
                }
            }
            else {
                $('#UserName').closest(".form-control").addClass("invalid");
                $('#UserName-error').show();
            }
            if (isvalid == true) {
                $.ajax({
                    type: "POST",
                    url: loginUrl,
                    data: GetModel(),
                    success: function (data) {
                        $('#btnLogin').prop("disabled", false);
                        instance.stop();

                        if (data.MessageType == "1") {
                            window.location.href = homeUrl;
                        }
                        else {
                            showMessage(data.MessageType, data.Message);
                        }

                        
                    }
                })
            } else {
                $('#btnLogin').prop("disabled", false);

            }

        }
    });


    $('#btnLogin').click(function () {
        $('#btnLogin').prop("disabled", true);
        var instance = Ladda.create(document.querySelector('#btnLogin'));
        instance.start();
        ClearError();

        var isvalid = false;
        var UserName = $('#UserName').val();
        var Password = $('#Password').val();
        if (UserName != "") {
            if (Password != "") {
                isvalid = true;
            }
            else {
                $('#Password').closest(".form-control").addClass("invalid");
                $('#Password-error').show();
            }
        }
        else {
            $('#UserName').closest(".form-control").addClass("invalid");
            $('#UserName-error').show();
        }
        if (isvalid == true) {
            $.ajax({
                type: "POST",
                url: loginUrl,
                data: GetModel(),
                dataType: "json",
                success: function (data) {
                    $('#btnLogin').prop("disabled", false);
                    instance.stop();
                    if (data.MessageType == "1") {
                        window.location.href = homeUrl;
                    }
                    else {
                        showMessage(data.MessageType, data.Message);
                    }

                    

                }
            })
        } else {
            $('#btnLogin').prop("disabled", false);
            instance.stop();
        }
    });
});

function showMessage(status, Message) {

    if (status == 2) {
        swal({
            title: Message,
            text: "",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-primary",
            type: "error"
        }).catch(swal.noop)
    }
    else {
        swal({
            title: Message,
            text: "",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-primary",
            type: "error"
        }).catch(swal.noop)
    }

}


function GetModel() {
    var model = {};
    model.UserName = $('#UserName').val();
    model.Password = $('#Password').val();

    return model;
}


function ClearError() {
    $('#UserName').closest(".form-control").removeClass("invalid");
    $('#UserName-error').hide();
    $('#Password').closest(".form-control").removeClass("invalid");
    $('#Password-error').hide();
}
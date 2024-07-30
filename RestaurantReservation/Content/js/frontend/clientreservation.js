

$(document).ready(function () {
    var datestring = "Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday";
    var realdate = $('#hdOpenDays').val();

    // Convert the strings to arrays
    var dateArray = datestring.split(', ').map(day => day.trim());
    var realdateArray = realdate.split(', ').map(day => day.trim());

    // Days of the week with corresponding integer values
    var dayMap = {
        "Sunday": 0,
        "Monday": 1,
        "Tuesday": 2,
        "Wednesday": 3,
        "Thursday": 4,
        "Friday": 5,
        "Saturday": 6
    };

    // Find missing days
    var missingDays = dateArray.filter(day => !realdateArray.includes(day));

    // Convert missing days to their corresponding integer values
    var missingDaysIntegers = missingDays.map(day => dayMap[day]);

    const today = new Date() - 1;
    new Litepicker({
        element: document.getElementById('date'),
        minDate: today,
        format: 'DD/MM/YYYY',
        setup: (picker) => {
            picker.on('selected', (date1, date2) => {
            });
        },
        lockDaysFilter: (day) => {
            const d = day.getDay();

            return missingDaysIntegers.includes(d);
        },

    })

    const time1 = $('#hdFromtime').val();
    const time2 = $('#hdTotime').val();

    const convertedTime1 = convertTo24Hour(time1); // "19:00"
    const convertedTime2 = convertTo24Hour(time2); // "22:00"

    $('#time').timepicker({
        timeFormat: 'hh:mm tt',
        interval: 30,
        minTime: convertedTime1, // 7 PM
        maxTime: convertedTime2, // 10 PM
        startTime: convertedTime1, // Start at 7 PM
        dynamic: false,
        dropdown: true,
        scrollbar: true,
        showSecond: false,
        showMillisec: false,
        showMicrosec: false,
        showTimezone: false,
    });

    //$('#time').val(convertedTime1);
    $('#btnBookNow').click(function () {
        $(this).attr("disabled", true);
        ClearError();

        var isvalid = false;
        var date = $('#date').val();
        var time = $('#time').val();
        var people = $('#people').val();
        if (date != "") {
            if (time != "") {
                if (people != "") {
                    isvalid = true;
                }
                else {
                    $('#people').closest(".form-control").addClass("invalid");
                }
            }
            else {
                $('#time').closest(".form-control").addClass("invalid");
            }
        }
        else {
            $('#date').closest(".form-control").addClass("invalid");
        }

        if (isvalid == true) {
            $.ajax({
                type: "POST",
                url: bookUrl,
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

function convertTo24Hour(time) {
    const [hours, modifier] = time.split(' ');
    let [hoursPart, minutesPart] = hours.split(':');

    if (modifier === 'PM' && hoursPart !== '12') {
        hoursPart = parseInt(hoursPart, 10) + 12;
    }
    if (modifier === 'AM' && hoursPart === '12') {
        hoursPart = '00';
    }

    return `${hoursPart}:${minutesPart || '00'}`;
}

function GetModel() {
    var model = {};
    model.RestaurantId = $('#hdRestaurantId').val();
    model.sReservationDate = $('#date').val();
    model.sTime = $('#time').val();
    model.NumberPeople = $('#people').val();
    model.Remark = $('#remarks').val();

    return model;
}

function ClearError() {
    $('#date').closest(".form-control").removeClass("invalid");
    $('#time').closest(".form-control").removeClass("invalid");
    $('#people').closest(".form-control").removeClass("invalid");
    $('#remarks').closest(".form-control").removeClass("invalid");
}

function showMessage(status, message) {
    if (status == 1) {
        swal({
            title: "Reservation Successful",
            text: "",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-primary",
            type: "success"
        }).then((result) => {
            if (result.value) {
                window.location = redirectUrl
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
            window.location.href = "./Home/Index";
        });
    }

}
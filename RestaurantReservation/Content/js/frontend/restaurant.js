$(document).ready(function () {

    BindGrid();

    $('#btnCancel').click(function () {
        location.reload();
    });

    $('#btnSave').click(function () {
        $(this).attr("disabled", true);
        var instance = Ladda.create(document.querySelector('#btnSave'));
        instance.start();
        ClearError();

        var isvalid = false;
        var Name = $('#Name').val();
        var Type = $('#Type').val();
        var FromTimeClock = $('#FromTimeClock').val();
        var ToTimeClock = $('#ToTimeClock').val();
        if (Name != "") {
            if (Type != "") {
                if (FromTimeClock != "") {
                    if (ToTimeClock != "") {
                        isvalid = true;
                    }
                    else {
                        $('#ToTimeClock').closest(".form-control").addClass("invalid");
                        $('#ToTimeClock-error').show();
                    }
                }
                else {
                    $('#FromTimeClock').closest(".form-control").addClass("invalid");
                    $('#FromTimeClock-error').show();
                }
            }
            else {
                $('#Type').closest(".form-control").addClass("invalid");
                $('#Type-error').show();
            }
        }
        else {
            $('#Name').closest(".form-control").addClass("invalid");
            $('#Name-error').show();
        }
        if (isvalid == true) {
            $.ajax({
                type: "POST",
                url: saveUrl,
                data: GetModel(),
                success: function (data) {
                    showMessage(data.MessageType, data.Message);
                    instance.stop();
                    BindGrid();
                }
            })
        }
        $(this).attr("disabled", false);
        instance.stop();
    });

    $("#exampleModalLong").on("hidden.bs.modal", function () {
        ClearError();
        $('#lblheader').text('Add New Restaurant');
        $("#Id").val('');
        $('#Name').val('');
        $('#Type').val('');
        $('#Description').val('');
        $('#btnSave').html('<span class="ladda-label"><i class="fa fa-save"></i>&nbsp;Save</span>');
    });
});

function BindGrid() {

    $(document).ready(function () {

        $('#overlay').fadeIn();

        $('#tbl').dataTable().fnDestroy();
        $.ajax({
            type: "POST",
            url: listUrl,
            dataType: "json",
            success: function (data) {

                oTable = $("#tbl").dataTable({
                    "columnDefs": [
                        {
                            className: "text-center", "targets": [6]
                        }
                    ],
                    "retrieve": true,
                    "pagingType": "full_numbers",
                    "lengthMenu": [
                        [10, 25, 50, -1],
                        [10, 25, 50, "All"]
                    ],
                    //"fnRowCallback": function (nRow, aData, iDisplayIndex) {
                    //    $("td:first", nRow).html(iDisplayIndex + 1);
                    //    return nRow;
                    //},
                    responsive: true,
                    data: data,

                    "aaSorting": [],
                    "aoColumns": [
                        {
                            "mData": "No", "bSearchable": false, "bSortable": false, "width": "5%",
                            "mRender": function (data, type, full) {
                                return "<center>" + data.toString() + "</center>";
                            }
                        },
                        { "mData": "Name", "bSearchable": true, "bSortable": true, "width": "15%" },
                        { "mData": "Type", "bSearchable": true, "bSortable": true, "width": "10%" },
                        { "mData": "Description", "bSearchable": true, "bSortable": true, "width": "10%" },
                        { "mData": "OpenDays", "bSearchable": true, "bSortable": true, "width": "20%" },
                        {
                            "mData": "Id", "bSearchable": false, "bSortable": false, "width": "15%",
                            "mRender": function (data, type, full) {
                                var FromTimeClock = full.FromTimeClock.toString();
                                var FromTimeAMPM = full.FromTimeAMPM.toString();
                                var ToTimeClock = full.ToTimeClock.toString();
                                var ToTimeAMPM = full.ToTimeAMPM.toString();

                                var shtml = FromTimeClock + " " + FromTimeAMPM + " - " + ToTimeClock + " " + ToTimeAMPM;

                                return shtml;

                            }
                        },
                        {
                            "mData": "Id", "bSearchable": false, "bSortable": false, "width": "15%",
                            "mRender": function (data, type, full) {
                                var id = full.Id.toString();

                                var actions = "";

                                //var edit = "";
                                var del = "";


                                //edit = '<a onclick="Edit(\'' + id + '\')"><i class="ti-pencil mr-1 btn btn-info"></i> </a>&nbsp;&nbsp;';
                                del = '<a onclick="Delete(\'' + id + '\')"><i class="ti-trash btn btn-danger"></i></a>';



                                //actions += edit;
                                actions += del;
                                return actions;

                            }
                        },
                    ]
                });

                //oTable.destroy();
                //oTable.on('click', 'tbody tr td:not(:last-child)', function (e) {
                //    var row = $(this).closest('tr');
                //    var data = oTable.fnGetData(row);

                //    View(data.Id);

                //});
                $('#dataTable2_wrapper').css("width", "100%");
            }
        });
    });
}

function showMessage(status, message) {
    if (status == 1) {
        swal({
            title: message,
            text: "",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-primary",
            type: "success"
        }).then((result) => {
            if (result.value) {
                location.reload();
            }
        });
    }
    else {
        swal({
            title: message,
            text: "",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-primary",
            type: "error"
        }).catch(swal.noop)
    }

}

function Edit(id) {
    $.ajax({
        url: editUrl,
        type: 'GET',
        data: { "ID": id },
        success: function (data) {
            $('#lblheader').text('Edit Restaurant');

            $('#exampleModalLong').modal('show');
            $("#Id").val(data.Id);
            $('#Name').val(data.Name);
            $('#Type').val(data.Type);

            var InDraught = data.InDraught;
            if (InDraught == true) {
                $("#chkInDraught").prop("checked", true);
            } else {
                $("#chkInDraught").prop("checked", false);
            }

            var InNonDraught = data.InNonDraught;
            if (InNonDraught == true) {
                $("#chkInNonDraught").prop("checked", true);
            } else {
                $("#chkInNonDraught").prop("checked", false);
            }

            var InWarehouse = data.InWarehouse;
            if (InWarehouse == true) {
                $("#chkInWarehouse").prop("checked", true);
            } else {
                $("#chkInWarehouse").prop("checked", false);
            }

            $('#btnSave').html('<span class="ladda-label"><i class="fa fa-edit"></i>&nbsp;Update</span>');
            $('html, body').animate({
                scrollTop: 0
            }, 800);
        },
    })
}


function Delete(id) {

    swal({
        title: "Do you want to delete this Restaurant?",
        text: "Deleting Restaurants may cause errors in production.",
        type: 'question',
        showCancelButton: true,
        cancelButtonColor: '#ff6258',
        confirmButtonText: 'Yes',
        cancelButtonText: 'No',
        confirmButtonClass: "btn btn-primary",
        cancelButtonClass: "btn btn-danger",
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: deleteUrl,
                type: 'GET',
                data: { "ID": id },
                success: function (data) {
                    showMessage(data.MessageType, data.Message);
                },
            })
            BindGrid();
        }
    });




}

function GetModel() {
    var model = {};
    model.Id = $('#Id').val();
    model.Name = $('#Name').val();
    model.Type = $('#Type').val();
    model.Description = $('#Description').val();
    model.FromTimeClock = $('#FromTimeClock').val();
    model.FromTimeAMPM = $('#ddlFromTimeAMPM').val();
    model.ToTimeClock = $('#ToTimeClock').val();
    model.ToTimeAMPM = $('#ddlToTimeAMPM').val();

    var checkedValues = [];
    $("input[name='chkDays']:checked").each(function () {
        checkedValues.push($(this).val());
    });
    var checkedValuesString = checkedValues.join(', ');
    model.OpenDays = checkedValuesString;
    return model;
}

function ClearError() {
    $('#Name').closest(".form-control").removeClass("invalid");
    $('#Name-error').hide();
    $('#Type').closest(".form-control").removeClass("invalid");
    $('#Type-error').hide();
    $('#FromTimeClock').closest(".form-control").removeClass("invalid");
    $('#FromTimeClock-error').hide();
    $('#ToTimeClock').closest(".form-control").removeClass("invalid");
    $('#ToTimeClock-error').hide();
}
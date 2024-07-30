$(document).ready(function () {

    BindGrid();

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
                        { "mData": "CustomerName", "bSearchable": true, "bSortable": true, "width": "15%" },
                        { "mData": "RestaurantName", "bSearchable": true, "bSortable": true, "width": "15%" },
                        { "mData": "sReservationDate", "bSearchable": true, "bSortable": true, "width": "10%" },
                        { "mData": "sReservationTime", "bSearchable": true, "bSortable": true, "width": "10%" },
                        { "mData": "NumberPeople", "bSearchable": true, "bSortable": true, "width": "5%" },
                        { "mData": "Remark", "bSearchable": true, "bSortable": true, "width": "20%" },
                        
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

var dt = $("#table1").DataTable({
    "columns": [
        {
            "searchable": false,
            "visible": false
        },
        null,
        null,
        null,
        {
            "searchable": false,
            "orderable": false,
        },
    ],
    "order": [[2, "asc"]],
    "paging": false
});
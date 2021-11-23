var dt = $("#table1").DataTable({
    "columns": [
        {
            "searchable": false,
            "visible": false
        },
        null,
        null,
        null,
        { "searchable": false },
    ]
});

var handleSearchDatatable = function () {
    const filterSearch = document.querySelector('[data-kt-docs-table-filter="search"]');
    filterSearch.addEventListener('keyup', function (e) {
        dt.search(e.target.value).draw();
    });
}

handleSearchDatatable();
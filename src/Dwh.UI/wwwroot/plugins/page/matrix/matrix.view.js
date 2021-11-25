var table = $("#table1").DataTable({
    "paging": false,
    "ordering": false,
    "info": false
});

$('#table1 tbody')
    .on('mouseenter', 'td', function () {
        var colIdx = table.cell(this).index().column;

        var rowIdx = table.cell(this).index().row;


        $(table.cells().nodes()).removeClass('bg-gray-200');
        $(table.rows().nodes()).removeClass('bg-gray-200');


        $(table.column(colIdx).nodes()).addClass('bg-gray-200');
        $(table.row(rowIdx).nodes()).addClass('bg-gray-200');
    });
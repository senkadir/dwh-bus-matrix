var table = $("#table1").DataTable({
    "paging": false,
    "ordering": false,
    "info": false,
    

});

$('#table1 tbody')
    .on('mouseenter', 'td', function () {

        const cellColor = 'bg-gray-300';
        var colIdx = table.cell(this).index().column;

        var rowIdx = table.cell(this).index().row;


        $(table.cells().nodes()).removeClass(cellColor);
        $(table.rows().nodes()).removeClass(cellColor);


        $(table.column(colIdx).nodes()).addClass(cellColor);
        $(table.row(rowIdx).nodes()).addClass(cellColor);
    });

$('#table1 tbody')
    .on('dblclick', 'td', function () {

        alert("what");
    });


const submitButton = document.getElementById('btn-new-matrix-item');

submitButton.addEventListener('click', function (e) {
    // Prevent default button action
    e.preventDefault();

    const form = document.getElementById('form-new-matrix-item');

    var validator = FormValidation.formValidation(
        form,
        {
            fields: {
                'dimensions': {
                    validators: {
                        notEmpty: {
                            message: 'Select dimension'
                        }
                    }
                },
                'facts': {
                    validators: {
                        notEmpty: {
                            message: 'Select fact'
                        }
                    }
                },
            },

            plugins: {
                trigger: new FormValidation.plugins.Trigger(),
                bootstrap: new FormValidation.plugins.Bootstrap5({
                    rowSelector: '.fv-row',
                    eleInvalidClass: '',
                    eleValidClass: ''
                })
            }
        }
    );

    // Validate form before submit
    if (validator) {
        validator.validate().then(function (status) {
            if (status == 'Valid') {
                submitButton.setAttribute('data-kt-indicator', 'on');

                submitButton.disabled = true;

                var matrixId = document.getElementById("matrix-id").value;

                axios.post(form.action,
                    {
                        matrixId: matrixId,
                        dimensionId: form.elements["dimensions"].value,
                        factId: form.elements["facts"].value
                    })
                    .then(function (response)
                    {
                        location.reload();
                    })
                    .catch(function (error)
                    {
                        submitButton.removeAttribute('data-kt-indicator');
                        submitButton.disabled = false;

                        toastr.error(error.response.data.error, error.response.data.code);
                    });

            }
        });
    }
});

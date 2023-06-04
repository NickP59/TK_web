function CreateReport(parameters) {
    const url = parameters.url;

    $.ajax({
        type: 'GET',
        url: url,
        success: function (response) {
            Swal.fire({
                title: 'Информация',
                text: 'Отчет сформирован',
                icon: 'success',
                confirmButtonText: 'Окей'
            })
        },
        failure: function () {
            Swal.fire({
                title: 'Информация',
                text:  'Произошла ошибка',
                icon: 'error',
                confirmButtonText: 'Окей'
            })
        },
        error: function (response) {
            alert(response.responseText);

        }
    });
};
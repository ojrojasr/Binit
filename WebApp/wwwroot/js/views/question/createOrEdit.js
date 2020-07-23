$(function () {
    $("#add-answer").click(function () {
        var tableId = "#answer-details"
        var rows = $(`${tableId} tbody tr`)
        var lastRow = null

        // If there is any item
        if (rows.length != 0) {
            lastRow = rows.last()
        }

        $.ajax({
            dataType: 'html',
            type: 'GET',
            url: `/Question/GetAnswerRow?rowsLength=${rows.length + 1}`,
            success: function (result) {

                if (lastRow != null) {
                    lastRow.after(result)
                } else {
                    var body = $(`${tableId} tbody`).append(result)
                }
                $('.floating-labels .form-control').on('focus blur', function (e) {
                    $(this).parents('.form-group').toggleClass('focused', (e.type === 'focus' || this.value.length > 0));
                }).trigger('blur');
            },
            error: function (error) {
                alert("Error")
            }
        });
    });

    $("form").submit(function (e) {
        var tableId = "#answer-details"
        var rows = $(`${tableId} tbody tr`)
        rows.each(function (index, row) {
            var rowId = `answer-row-${index}`
            var inputs = $(row).find("input")
            inputs.each(function (inputIndex, input) {
                var id = $(input).attr("id")
                var name = $(input).attr("name")
                $(input).attr("id", id.replace(/\d+/, index))
                $(input).attr("name", name.replace(/\d+/, index))
            })
        })
    })
});
$(".answer_btn").click(function () {
    var answer_id = $(this).attr("name");
    $("button").off("click");
    var next = document.getElementById("next");
    

    $.ajax({
        dataType: 'json',
        type: 'POST',
        url: `/Game/CheckAnswer?answerId=${answer_id}`,
        success: function (result) {

            if (result === answer_id) {

                $('[name ="' + result + '"]').removeClass('btn-light');
                $('[name ="' + result + '"]').addClass('btn-success');
                
            }
            else {
                $('[name ="' + result + '"]').removeClass('btn-light');
                $('[name ="' + result + '"]').addClass('btn-success');

                $('[name ="' + answer_id + '"]').removeClass('btn-light');
                $('[name ="' + answer_id + '"]').addClass('btn-danger');
            }
            next.style.display = "";
            },
        error: function (error) {
            alert("Error")
        }
    });

});
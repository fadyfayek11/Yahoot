function remove(elem) {
    elem.parentNode.removeChild(elem);
    var count = document.getElementById("counter-player");
    count.innerText = parseInt(count.innerText) - 1;
    var id = elem.firstElementChild.value;
    $.ajax({
        type: "POST",
        url: '/Admin/DeleteUser?id='+id,
        contentType: 'application/json; charset=utf-8',
        data: id ,
        dataType: 'json',
        success: function (msg) {
        },
        error: function (req, status, error) {
           
        }
    });
}

function SubmitAnswer(answerIndex) {
    var userId = $("#user-id").val();
    var model = {
        "Index": answerIndex,
        "QuizId": $("#quiz-id").val(),
        "QuestionId": $("#question-id").val(),
        "UserId": $("#user-id").val()
    }
    console.log(model);
    $.ajax({
        url: '/Home/CheckCorrectAnswer',
        type: 'POST',
        dataType: "json",
        contentType: "application/x-www-form-urlencoded",
        data: model,
        success: function (data) {
            console.log(data);
            if (data.success) {
                swal("!عاش", "برااااافووو علييييييك", "success");
                $("#score-" + userId).html(data.score);
                $("#loader").show();
                $("#message").show();
                $("#question-section").hide();
            } else {
                swal("ينفع كدا؟", "!غلط يا استاذ قولنا نركز", "error");
                $("#loader").show();
                $("#message").show();
                $("#question-section").hide();
            }
        },
        error: function () {
        }
    });

}
  

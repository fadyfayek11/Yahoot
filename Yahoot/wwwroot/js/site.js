// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code

function remove(elem) {
    elem.parentNode.removeChild(elem);
    var count = document.getElementById("counter-player");
    count.innerText = parseInt(count.innerText) - 1;
    var name = elem.innerText;
    $.ajax({
        type: "POST",
        url: '/Admin/DeleteUser?name='+name,
        contentType: 'application/json; charset=utf-8',
        data: name ,
        dataType: 'json',
        success: function (msg) {
        },
        error: function (req, status, error) {
           
        }
    });
}

function SubmitAnswer(answerIndex) {

    var model = {
        "Index": answerIndex,
        "QuizId": $("#quiz-id").val(),
        "QuestionId": $("#question-id").val(),
        "UserId":$("#user-id").val()
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
            } else {
                swal("ينفع كدا؟", "!غلط يا استاذ قولنا نركز", "error");
            }
            
        },
        error: function () {
        }
    });

}
  

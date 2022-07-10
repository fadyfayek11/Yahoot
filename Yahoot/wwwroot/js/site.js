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

function SubmitAnswer(questionIndex,userId) {
    alert("your choice is :"+questionIndex);
}
  

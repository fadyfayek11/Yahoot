"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/yahoot").build();



connection.start().then(result => {
    console.log("SignalR is now connected");
});


document.getElementById("join-game").addEventListener("click", function (event) {
    var name = document.getElementById("student-name").value;
    var code = document.getElementById("code").value;
    connection.invoke("SendJoinMessageToAdmin", code, name).catch(function (err) {
        return console.error(err.toString());
    });   
    event.preventDefault();
});
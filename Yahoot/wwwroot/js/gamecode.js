"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/yahoot").build();


connection.start().then(result => {
    console.log("SignalR is now connected");
});

document.getElementById("gameCode").addEventListener("click", function (event) {
    var gameId = document.getElementById("game-input").value;
    connection.invoke("joinGroup", gameId).catch(function (err) {
        return console.error(err.toString());
    });
    document.getElementById("enter-form").submit();
    event.preventDefault();
});
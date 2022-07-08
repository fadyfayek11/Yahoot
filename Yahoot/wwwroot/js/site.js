// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code

"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/yahoot").build();


connection.start().then(result => {
    console.log("SignalR is now connected");
});

connection.on("sendJoinMessageToAdmin", function (message) {
    var li = document.createElement("li");
    var count = document.getElementById("counter-player");

    li.style.listStyle = "none";
    li.style.backgroundColor = "#a81818";
    li.style.display = "inline-block";
    li.style.margin = "11px";
    li.style.fontWeight = "bold";
    li.style.fontSize = "29px";
    li.style.borderRadius = "9px";
    li.style.padding = "12px";
    document.getElementById("players").appendChild(li);

    count.innerText = parseInt(count.innerText)+1;
    li.textContent = `${message}`;
});

document.getElementById("join-game").addEventListener("click", function (event) {
    var name = document.getElementById("student-name").value;
    connection.invoke("SendJoinMessageToAdmin", name).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
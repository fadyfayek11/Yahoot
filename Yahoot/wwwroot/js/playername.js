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
    li.style.cursor = "not-allowed";
    li.setAttribute("onclick", "remove(this)");
    document.getElementById("players").appendChild(li);

    count.innerText = parseInt(count.innerText) + 1;
    li.textContent = `${message}`;
});

document.getElementById("join-game").addEventListener("click", function (event) {
    var name = document.getElementById("student-name").value;
    connection.invoke("SendJoinMessageToAdmin", name).catch(function (err) {
        return console.error(err.toString());
    });
    document.getElementById("user-join").submit();
    event.preventDefault();
});
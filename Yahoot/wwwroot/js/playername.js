﻿"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/yahoot").build();


connection.start().then(result => {
    console.log("SignalR is now connected");
});

connection.on("sendJoinMessageToAdmin", function (message) {
    var li = document.createElement("li");
    var count = document.getElementById("counter-player");

    li.style.listStyle = "none";
    li.style.backgroundColor = "#381273";
    li.style.display = "inline-block";
    li.style.margin = "11px";
    li.style.fontWeight = "bold";
    li.style.fontSize = "29px";
    li.style.borderRadius = "6px";
    li.style.padding = "12px";
    li.style.cursor = "not-allowed";
    li.setAttribute("onclick", "remove(this)");
    document.getElementById("players").appendChild(li);

    count.innerText = parseInt(count.innerText) + 1;
    li.textContent = `${message}`;
});
connection.on("AdminSendQuestionId", function (qId) {
    document.body.style.backgroundColor = "white";

    var questionSection = document.getElementById("question-section");
    questionSection.style.display = "";

    var questionId = document.getElementById("question-id");
    questionId.value = qId.toString();

    var joinSection = document.getElementById("join-section");
    joinSection.style.display = "none";

    alert(questionId.value);
});

document.getElementById("join-game").addEventListener("click", function (event) {
    var name = document.getElementById("student-name").value;
    connection.invoke("SendJoinMessageToAdmin", name).catch(function (err) {
        return console.error(err.toString());
    });
    document.getElementById("user-join").submit();
    event.preventDefault();
});
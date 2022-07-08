// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code
"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/yahootHub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;
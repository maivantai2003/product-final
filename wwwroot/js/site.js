// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.
"use strict"
var connection = new signalR.HubConnectionBuilder().withUrl("/my-hub").build();
connection.start().catch(function (err) {
    return console.error(err.toString());
});
const loadCart = () => {
    window.location.href = "/Cart"
}
connection.on("render", function () {
    loadCart()
})
connection.on("delete", function () {
    loadCart()
})
connection.on("deleteAll", function () {
    loadCart()
})

$(document).on("click", "#sendButton", function () {
    var id = $("#receiverSelect").val()
    var user = $("#userInput").val()
    var message = $("#messageInput").val()
    console.log(id)
    console.log(user)
    console.log(message)
    const msg = document.createElement("div");
    msg.innerHTML = `<strong>${user}</strong>: ${message}`;
    document.getElementById("chatBox").appendChild(msg);
    document.getElementById("chatBox").scrollTop = document.getElementById("chatBox").scrollHeight;
    connection.invoke("SendMessage", id, user, message)
})

connection.on("ReceiveMessage", (user, message) => {
    console.log(message)
    //renderMessage(message)
    const msg = document.createElement("div");
    msg.innerHTML = `<strong>${user}</strong>: ${message}`;
    document.getElementById("chatBox").appendChild(msg);
    document.getElementById("chatBox").scrollTop = document.getElementById("chatBox").scrollHeight;
})

const renderMessage = (message) => {
    const msg = document.createElement("div");
    msg.innerHTML = `<strong>${user}</strong>: ${message}`;
    document.getElementById("chatBox").appendChild(msg);
    document.getElementById("chatBox").scrollTop = document.getElementById("chatBox").scrollHeight;
}
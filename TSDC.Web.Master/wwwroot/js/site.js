// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function logOut(el) {
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.open("GET", el.getAttribute("data-actionUrl"), false); // false for synchronous request
    xmlHttp.send(null);
    
    setTimeout("preventBack()", 0);
    window.onunload = function () { null };
}

function preventBack() {
    window.history.forward();
}

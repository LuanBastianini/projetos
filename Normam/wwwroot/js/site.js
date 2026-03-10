// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$("#btnChat").click(function(){
    var model = $('form').serializeObject();
    $("#chatList").append('<li class="uk-text-right">'+ model.inputChat +'</li>')
});
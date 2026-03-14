var site = (function () {
    var config = {
        url: {
            chat: ''
        },
        el: ''
    };

    var init = function ($config) {
        config = $config;
        config.el = document.getElementById("divChat");
        config.el.scrollTop = config.el.scrollHeight;
    };

    var get = function(){

        var input = $('#inputChat').val();
        $("#chatList").append('<li style="float: right;"><div class="balao-fala pergunta">'+ input +'</div></li>');
        $("#btnChat").prop('disabled', true);
        $('#inputChat').val("");
        $('#loading').show();

        config.el.scrollTop = config.el.scrollHeight;
        
        $.get(config.url.chat, {
            input: input
        }).done(function (data) {
            $('#loading').hide();
            $("#chatList").append('<li style="float: left; width: 90%;"><div class="balao-fala resposta"> '+ data.content +' </div></li>');
            $("#btnChat").prop('disabled', false);
            config.el.scrollTop = config.el.scrollHeight;
        }).fail(function(){
            $('#loading').hide();
            $("#chatList").append('<li style="float: left; width: 90%;"><div class="balao-fala resposta"> Ocorreu um problema. </div></li>');
            $("#btnChat").prop('disabled', false);
            config.el.scrollTop = config.el.scrollHeight;
        });
    };

    return {
        init: init,
        get: get
    };
})();


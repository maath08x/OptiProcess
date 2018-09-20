function rediLogin() {

    var urlEntrar = window.location.pathname.replace("UsuarioCad", "") + "Login";

    window.location.href = (urlEntrar);

}

function rediCad() {

    var urlEntrar = window.location.pathname.replace("Login", "") + "UsuarioCad";

    window.location.href = (urlEntrar);

}

function validaAcesso() {

    var usuario = window.localStorage.getItem("usuario");

    if (usuario != "") {

    } else {

        var urlEntrar = "localhost:59946/Login";

        window.location.href = (urlEntrar);

    }

}
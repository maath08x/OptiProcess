function rediLogin() {

    
    window.localStorage.removeItem("usuario");

    var urlsair = "http://" + location.host + "/Login";

    window.location.href = (urlsair);
}

function rediCad() {

 
    var urlsair = "http://" + location.host + "/UsuarioCad";

    window.location.href = (urlsair);

}

function validaAcesso() {

    var usuario = window.localStorage.getItem("usuario");

    if (usuario != null) {

        toastr.info("", "Sua Sessão Expirou !")  

    } else {
        
        var urlsair = "http://" + location.host + "/Login";

        window.location.href = (urlsair);

        toastr.success("", "Bem Vindo " + usuario)
    }

}
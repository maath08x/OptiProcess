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

    if (usuario == null) {

        //toastr.info("", "Sua Sessão Expirou !");  
        var urlsair = "http://" + location.host + "/Login";

        window.location.href = (urlsair);

    } else {

        pesquisarUser();
      
    }

}

function redihome() {

    
        var urlsair = "http://" + location.host;

        window.location.href = (urlsair);

          

}
function entrar() {

    var usuario = document.getElementById("usuario").value;
    var senha = document.getElementById("senha").value;


    if (usuario != "" && senha != "") {
        var sRequest = "Usuario=" + usuario + "&Senha=" + senha;
               
        var xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {

                var ret = xhttp.response;

                if (ret == "True") {

                    
                    window.localStorage.setItem("usuario", usuario);

                    var urlEntrar = "http://" + location.host;

                    window.location.href = (urlEntrar);

                    toastr.success("", "Bem Vindo " + usuario )

                } else {
                    toastr.error("", "Usuario ou Senha Invalidos !")  
                }
            }


        }

        var sURL = "http://" + location.host + "/Login/Autenticar?" + sRequest;

        xhttp.open("POST", sURL, true);
        xhttp.send();

    } else {
        toastr.error("", "Usuario ou Senha Invalidos !")      
        
    }
}



function sair() {


    window.localStorage.removeItem("usuario");
    
    var urlsair = "http://" + location.host + "/Login";
    

    window.location.href = (urlsair);
}

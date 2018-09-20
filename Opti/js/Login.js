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

                    var urlEntrar = window.location.pathname.replace("Login", "");

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
    
    var urlsair = window.location.pathname + "Login";

    window.location.href = (urlsair);
}
/*
function Alterar() {
    var nID = glb_nMaquinarioID;
    var sNome = document.getElementById("Nome_Edit");
    var nTipo = document.getElementById("Tipo_Edit");
    var sDescricao = document.getElementById("Descrição_Edit");

    var sRequest = "ID=" + escape(nID) + "&Nome=" + escape(sNome.value) + "&Descricao=" + escape(sDescricao.value) + "&Tipo=" + escape(nTipo.value);

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            alert(xhttp.responseText);
        }
    }

    var sURL = "http://" + location.host + "/Maquinarios/Alterar?" + sRequest;

    xhttp.open("POST", sURL, true);
    xhttp.send();
}

*/

function alterar() {

    var nome = document.getElementById("nome").value;
    var email = document.getElementById("email").value;
    var usuario = document.getElementById("usuario").value;
    var senha = document.getElementById("senha").value;
    var confsenha = document.getElementById("confsenha").value;
    var endereco = document.getElementById("endereco").value;
    var numero = document.getElementById("numero").value;
    var cidade = document.getElementById("cidade").value;
    var estado = document.getElementById("estado").value;
    var fone = document.getElementById("fone").value;
    var datanasc = document.getElementById("datanasc").value;

    var sRequest = "Nome=" + escape(nome.value) + "&Email=" + escape(email.value) + "&Usuario=" + escape(usuario.value) +
        "&Senha=" + escape(senha.value) + + "&Rua=" + escape(endereco.value) + "&Numero=" + escape(numero.value) +
        "&Cidade=" + escape(cidade.value) + "&Estado=" + escape(estado.value) + "&Telefone=" + escape(fone.value)+
        "&Nascimento=" + escape(datanasc.value);

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            alert(xhttp.responseText);
        }
    }

    var sURL = "http://" + location.host + "/Usuarios/Alterar?" + sRequest;

    xhttp.open("POST", sURL, true);
    xhttp.send();

}

function inserir() {

    var nome = document.getElementById("nome").value;
    var email = document.getElementById("email").value;
    var usuario = document.getElementById("usuario").value;
    var senha = document.getElementById("senha").value;
    var confsenha = document.getElementById("confsenha").value;
    var tipoPessoa = "8";
    var fantasia = nome.substring(0, nome.indexOf(" "));

    if (senha != confsenha) {

        toastr.error("", "Senhas Divergentes !") 

    } else {

        var sRequest = "Nome=" + nome + "&Email=" + email + "Tipo=" + tipoPessoa + "&Fantasia=" + fantasia +
            "&Usuario=" + usuario + "&Senha=" + senha;

        var xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                alert(xhttp.responseText);
            }
        }

        var sURL = "http://" + location.host + "/Pessoas/AdicionarLogins?" + sRequest;

        xhttp.open("POST", sURL, true);
        xhttp.send();

    }
}



function pesquisarUser() {

    var usuario = window.localStorage.getItem("usuario");    
    

       var sRequest = "Usuario=" + usuario;

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var tBody = document.getElementById("tBody");
            var response = JSON.parse(xhttp.response);
            var sInner = "";


            var nome = response[0]["Pessoas"]["nome"];
            var email = response[0]["Pessoas"]["email"];
            var usuario = response[0]["login"];
            var senha = response[0]["senha"];
            var confsenha = response[0]["Nome"];
            var endereco = response[0]["Pessoas"]["rua"];
            var numero = response[0]["Pessoas"]["numero"];
            var cidade = response[0]["Pessoas"]["cidade"];
            var estado = response[0]["Pessoas"]["estado"];
            var fone = response[0]["Pessoas"]["telefone"];
            var datanasc = response[0]["Pessoas"]["nascimento"];

            document.getElementById('nome').value = nome;
            document.getElementById('email').value = email;
            document.getElementById('usuario').value = usuario;
            document.getElementById('senha').value = senha;
            document.getElementById('confsenha').value = confsenha;
            document.getElementById('endereco').value = endereco;
            document.getElementById('numero').value = numero;
            document.getElementById('cidade').value = cidade;
            document.getElementById('estado').value = estado;
            document.getElementById('fone').value = fone;
            document.getElementById('datanasc').value = new Date(datanasc);
        }
    }


        var sURL = "http://" + location.host + "/Login/Pesquisar?" + sRequest;

        xhttp.open("GET", sURL, true);
        xhttp.send();


}



 



﻿var glb_nPessoaID;
var glb_nTipo;

function alterar()
{
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

    var sRequest = "ID=" + glb_nPessoaID + "&Tipo=" + glb_nTipo + "&Nome=" + nome + "&Email=" + email + "&Usuario=" + usuario + "&Senha=" + senha + "&Rua=" + endereco +
        "&Numero=" + numero + "&Cidade=" + cidade + "&Estado=" + estado + "&Telefone=" + fone + "&Nascimento=" + datanasc;

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            alert(xhttp.responseText);
        }
    }

    var sURL = "http://" + location.host + "/Pessoas/Alterar?" + sRequest;

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
    var fantasia = nome.substring(0, (nome.indexOf(" ") == -1 ? nome.length : nome.indexOf(" ")));

    if (senha != confsenha || nome == "" || email == "" || usuario == "" || senha == "" || confsenha == "") {

        //toastr.error("", "Senhas Divergentes!");
        alert("Dados Inválidos!");

    } else {

        var sRequest = "Nome=" + nome + "&Email=" + email + "&Tipo=" + tipoPessoa + "&Fantasia=" + fantasia +
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

function pesquisarUser()
{

    var usuario = window.localStorage.getItem("usuario");    
    

    var sRequest = "Usuario=" + usuario;

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var tBody = document.getElementById("tBody");
            var response = JSON.parse(xhttp.response);
            var sInner = "";

            glb_nPessoaID = response[0]["Pessoas"]["pessoaID"];
            glb_nTipo = response[0]["Pessoas"]["tipoPessoa"];
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
            var dt = new Date(parseInt(datanasc.replace('/Date(', '')));
            var dt1 = dt.getDate() + "/" + dt.getMonth() + "/" + dt.getFullYear();
            document.getElementById('datanasc').value = dt1;
        }
    }


    var sURL = "http://" + location.host + "/Login/Pesquisar?" + sRequest;

    xhttp.open("GET", sURL, true);
    xhttp.send();


}



 



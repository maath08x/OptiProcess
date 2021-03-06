﻿var glb_nPessoaID;

var glb_Tipos;
var xhttpTipos = new XMLHttpRequest();
xhttpTipos.onreadystatechange = function () {
    if (this.readyState == 4 && this.status == 200) {
        glb_Tipos = JSON.parse(xhttpTipos.response);
    }
};
var sURL = "http://" + location.host + "/TiposGerais/Pesquisar?TelaID=3";
xhttpTipos.open("GET", sURL, true);
xhttpTipos.send();

function Pesquisar() {
    var nID = document.getElementById("ID_Search");
    var sNome = document.getElementById("Nome_Search");

    var sRequest = "ID=" + escape(nID.value) + "&Nome=" + escape(sNome.value);

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var tBody = document.getElementById("tBody");
            var response = JSON.parse(xhttp.response);
            var sInner = "";
            var dt;
            for (var i = 0; i < response.length; i++) {
                sInner += "<tr>";

                sInner += "<td id=\"pessoaID" + i + "\">" + (response[i]["pessoaID"] == null ? "0" : response[i]["pessoaID"]) + "</td>";
                sInner += "<td id=\"nome" + i + "\">" + response[i]["nome"] + "</td>";
                sInner += "<td id=\"fantasia" + i + "\">" + response[i]["fantasia"] + "</td>";

                for (var x = 0; x < glb_Tipos.length; x++) {
                    if (glb_Tipos[x]["tipoID"] == response[i]["tipoPessoa"]) {
                        sInner += "<td id=\"tipoPessoa" + i + "\">" + glb_Tipos[x]["nome"] + "</td>";
                    }
                }
                
                if (response[i]["nascimento"] != null) {
                    dt = new Date(parseInt(response[i]["nascimento"].replace('/Date(', '')));
                    sInner += "<td id=\"nascimento" + i + "\">" + dt.toLocaleDateString() + "</td>";
                }
                else {
                    sInner += "<td id=\"nascimento" + i + "\"></td>";
                }

                sInner += "<td id=\"documento" + i + "\">" + response[i]["documento"] + "</td>";
                sInner += "<td id=\"cidade" + i + "\">" + response[i]["cidade"] + "</td>";
                sInner += "<td id=\"estado" + i + "\">" + response[i]["estado"] + "</td>";
                sInner += "<td id=\"rua" + i + "\">" + response[i]["rua"] + "</td>";
                sInner += "<td id=\"numero" + i + "\">" + response[i]["numero"] + "</td>";
                sInner += "<td id=\"descricao" + i + "\">" + response[i]["telefone"] + "</td>";
                sInner += "<td id=\"email" + i + "\">" + response[i]["email"] + "</td>";
                sInner += "<td id=\"telefone" + i + "\">" + response[i]["telefone"] + "</td>";
                
                sInner += "<td id=\"btn" + i + "\"><button type=\"button\" class=\"btn btn-primary d-md-inline-block form-inline mr-0 mr-md-2 ml-3\" data-toggle=\"modal\" data-target=\"#editModal\" onclick=\"idAtual(" + response[i]["pessoaID"] + ")\"><div class=\"fas fa-fw fa-pen\"></div></button></td>";

                sInner += "</tr>";
            }
            tBody.innerHTML = sInner;
        }
    };

    var sURL = "http://" + location.host + "/Pessoas/Pesquisar?" + sRequest;

    xhttp.open("GET", sURL, true);
    xhttp.send();
}

function Adicionar() {
    var sNome = document.getElementById("Nome_Add");
    var sFantasia = document.getElementById("Fantasia_Add");
    var dtNascimento = document.getElementById("Nascimento_Add");
    var sDocumento = document.getElementById("Documento_Add");
    var sCidade = document.getElementById("Cidade_Add");
    var sEstado = document.getElementById("Estado_Add");
    var sRua = document.getElementById("Rua_Add");
    var nNumero = document.getElementById("Numero_Add");
    var sDescricao = document.getElementById("Descrição_Add");
    var sEmail = document.getElementById("Email_Add");
    var nTelefone = document.getElementById("Telefone_Add");

    var sRequest = "Nome=" + escape(sNome.value) + "&Fantasia=" + escape(sFantasia.value) + "&Nascimento=" + escape(dtNascimento.value) +
        "&Documento=" + escape(sDocumento.value) + "&Cidade=" + escape(sCidade.value) + "&Estado=" + escape(sEstado.value) + "&Rua=" + escape(sRua.value) +
        "&Numero=" + escape(nNumero.value) + "&Descricao=" + escape(sDescricao.value) + "&Email=" + escape(sEmail.value) + "&Telefone=" + escape(nTelefone.value);

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            alert(xhttp.responseText);
        }
    };

    var sURL = "http://" + location.host + "/Pessoas/Adicionar?" + sRequest;

    xhttp.open("POST", sURL, true);
    xhttp.send();
}



function Alterar() {
    var nID = glb_nPessoaID;
    var sNome = document.getElementById("Nome_Edit");
    var sFantasia = document.getElementById("Fantasia_Edit");
    var dtNascimento = document.getElementById("Nascimento_Edit");
    var nDocumento = document.getElementById("Documento_Edit");
    var sCidade = document.getElementById("Cidade_Edit");
    var sEstado = document.getElementById("Estado_Edit");
    var sRua = document.getElementById("Rua_Edit");
    var nNumero = document.getElementById("Numero_Edit");
    var sDescricao = document.getElementById("Descrição_Edit");
    var sEmail = document.getElementById("Email_Edit");
    var nTelefone = document.getElementById("Telefone_Edit");

    var sRequest = "ID=" + escape(nID) + "&Nome=" + escape(sNome.value) + "&Fantasia=" + escape(sFantasia.value) + "&Nascimento=" + escape(dtNascimento.value) +
        "&Documento=" + escape(nDocumento.value) + "&Cidade=" + escape(sCidade.value) + "&Estado=" + escape(sEstado.value) + "&Rua=" + escape(sRua.value) +
        "&Numero=" + escape(nNumero.value) + "&Descricao=" + escape(sDescricao.value) + "&Email=" + escape(sEmail.value) + "&Telefone=" + escape(nTelefone.value);

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

function Deletar() {
    var nID = glb_nPessoaID;

    var sRequest = "ID=" + escape(nID);

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            alert(xhttp.responseText);
        }
    }

    var sURL = "http://" + location.host + "/Pessoas/Deletar?" + sRequest;

    xhttp.open("POST", sURL, true);
    xhttp.send();
}

function idAtual(nPessoaID) {
    glb_nPessoaID = nPessoaID;
    var sRequest = "ID=" + escape(nPessoaID);

    var sNome = document.getElementById("Nome_Edit");
    var sFantasia = document.getElementById("Fantasia_Edit");
    var dtNascimento = document.getElementById("Nascimento_Edit");
    var nDocumento = document.getElementById("Documento_Edit");
    var sCidade = document.getElementById("Cidade_Edit");
    var sEstado = document.getElementById("Estado_Edit");
    var sRua = document.getElementById("Rua_Edit");
    var nNumero = document.getElementById("Numero_Edit");
    var sDescricao = document.getElementById("Descrição_Edit");
    var sEmail = document.getElementById("Email_Edit");
    var nTelefone = document.getElementById("Telefone_Edit");

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var response = JSON.parse(xhttp.response);

            sNome.value = response[0]["nome"];
            sFantasia.value = response[0]["fantasia"];

            if (response[0]["nascimento"] != null) {
                var dt = new Date(parseInt(response[0]["nascimento"].replace('/Date(', '')));
                dtNascimento.value = dt.toLocaleDateString()
            }
            else {
                dtNascimento.value = "";
            }

            nDocumento.value = response[0]["documento"];
            sCidade.value = response[0]["cidade"];
            sEstado.value = response[0]["estado"];
            sRua.value = response[0]["rua"];
            nNumero.value = response[0]["numero"];
            sDescricao.value = response[0]["descricao"];
            sEmail.value = response[0]["email"];
            nTelefone.value = response[0]["telefone"];
        }
    }

    var sURL = "http://" + location.host + "/Pessoas/Pesquisar?" + sRequest;

    xhttp.open("GET", sURL, true);
    xhttp.send();
}
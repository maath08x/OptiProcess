var glb_nTipoID;

function Adicionar() {
    var sNome = document.getElementById("Nome_Add");

    var sRequest = "Nome=" + escape(sNome.value);

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            alert(xhttp.responseText);
        }
    };

    var sURL = "http://" + location.host + "/TiposGerais/Adicionar?TelaID=1&" + sRequest;

    xhttp.open("POST", sURL, true);
    xhttp.send();
}

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
                if (response[i]["telaID"] == 1) {
                    sInner += "<tr>";

                    sInner += "<td id=\"tipoID" + i + "\">" + response[i]["tipoID"] + "</td>";
                    sInner += "<td id=\"nome" + i + "\">" + response[i]["nome"] + "</td>";

                    sInner += "<td id=\"btn" + i + "\"><button type=\"button\" class=\"btn btn-primary d-md-inline-block form-inline mr-0 mr-md-2 ml-3\" data-toggle=\"modal\" data-target=\"#editModal\" onclick=\"idAtual(" + response[i]["tipoID"] + ")\"><div class=\"fas fa-fw fa-pen\"></div></button></td>";

                    sInner += "</tr>";
                }
            }
            tBody.innerHTML = sInner;
        }
    };

    var sURL = "http://" + location.host + "/TiposGerais/Pesquisar?" + sRequest;

    xhttp.open("GET", sURL, true);
    xhttp.send();
}

function Alterar() {
    var nID = glb_nTipoID;
    var sNome = document.getElementById("Nome_Edit");

    var sRequest = "ID=" + escape(nID) + "&Nome=" + escape(sNome.value);

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            alert(xhttp.responseText);
        }
    }

    var sURL = "http://" + location.host + "/TiposGerais/Alterar?" + sRequest;

    xhttp.open("POST", sURL, true);
    xhttp.send();
}

function Deletar() {
    var nID = glb_nTipoID;

    var sRequest = "ID=" + escape(nID);

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            alert(xhttp.responseText);
        }
    }

    var sURL = "http://" + location.host + "/TiposGerais/Deletar?" + sRequest;

    xhttp.open("POST", sURL, true);
    xhttp.send();
}

function idAtual(nTipoID) {
    glb_nTipoID = nTipoID;
    var sRequest = "ID=" + escape(nTipoID);
    var sNome = document.getElementById("Nome_Edit");

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var response = JSON.parse(xhttp.response);
            sNome.value = response[0]["nome"];
        }
    }

    var sURL = "http://" + location.host + "/TiposGerais/Pesquisar?" + sRequest;

    xhttp.open("GET", sURL, true);
    xhttp.send();
}
var glb_nMaquinarioID;

var glb_Tipos;

var xhttpg = new XMLHttpRequest();
xhttpg.onreadystatechange = function () {
    if (this.readyState == 4 && this.status == 200) {
        glb_Tipos = JSON.parse(xhttpg.response);

        // Preenche combobox
        var selTipo = document.getElementById("SelTipo_Add");
        for (var i = 0; i < glb_Tipos.length; i++) {
            selTipo.innerHTML += "<option value=" + glb_Tipos[i]["tipoID"] + ">" + glb_Tipos[i]["nome"] + "</option>";
        }

        // Preenche combobox
        selTipo = document.getElementById("SelTipo_Edit");
        for (var i = 0; i < glb_Tipos.length; i++) {
            selTipo.innerHTML += "<option value=" + glb_Tipos[i]["tipoID"] + ">" + glb_Tipos[i]["nome"] + "</option>";
        }
    }
};

var sURL = "http://" + location.host + "/TiposGerais/Pesquisar?TelaID=1";

xhttpg.open("GET", sURL, true);
xhttpg.send();

function Adicionar() {
    var nTipo = document.getElementById("SelTipo_Add");
    var sNome = document.getElementById("Nome_Add");
    var sDescricao = document.getElementById("Descrição_Add");

    var sRequest = "Tipo=" + escape(nTipo.value) + "&Nome=" + escape(sNome.value) + "&Descricao=" + escape(sDescricao.value);

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            alert(xhttp.responseText);
        }
    };

    var sURL = "http://" + location.host + "/Maquinarios/Adicionar?Status=0&" + sRequest;

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
                sInner += "<tr>";

                sInner += "<td id=\"maquinarioID" + i + "\">" + response[i]["maquinarioID"] + "</td>";
                sInner += "<td id=\"nome" + i + "\">" + response[i]["nome"] + "</td>";

                for (var x = 0; x < glb_Tipos.length; x++) {
                    if (glb_Tipos[x]["tipoID"] == response[i]["tipoMaquinario"]) {
                        sInner += "<td id=\"tipoMaquinario" + i + "\">" + glb_Tipos[x]["nome"] + "</td>";
                    }
                }
                sInner += "<td id=\"descricao" + i + "\">" + response[i]["descricao"] + "</td>";
                sInner += "<td id=\"statusMaquinario" + i + "\">" + ((response[i]["statusMaquinario"] == null ? "0" : response[i]["statusMaquinario"]) == "0" ? "Liberado" : "Ocupado") + "</td>";
                if (response[i]["dtOcupacao"] != null) {
                    dt = new Date(parseInt(response[i]["dtOcupacao"].replace('/Date(', '')));
                    sInner += "<td id=\"dtOcupacao" + i + "\">" + dt.toLocaleDateString() + "</td>";
                }
                else {
                    sInner += "<td id=\"dtOcupacao" + i + "\"></td>";
                }
                if (response[i]["dtDesocupacao"] != null) {
                    dt = new Date(parseInt(response[i]["dtDesocupacao"].replace('/Date(', '')));
                    sInner += "<td id=\"dtDesocupacao" + i + "\">" + dt.toLocaleDateString() + "</td>";
                }
                else {
                    sInner += "<td id=\"dtDesocupacao" + i + "\"></td>";
                }
                sInner += "<td id=\"btn" + i + "\"><button type=\"button\" class=\"btn btn-primary d-md-inline-block form-inline mr-0 mr-md-2 ml-3\" data-toggle=\"modal\" data-target=\"#editModal\" onclick=\"idAtual(" + response[i]["maquinarioID"] + ")\"><div class=\"fas fa-fw fa-pen\"></div></button></td>";

                sInner += "</tr>";
            }
            tBody.innerHTML = sInner;
        }
    };

    var sURL = "http://" + location.host + "/Maquinarios/Pesquisar?" + sRequest;

    xhttp.open("GET", sURL, true);
    xhttp.send();
}

function Alterar() {
    var nID = glb_nMaquinarioID;
    var sNome = document.getElementById("Nome_Edit");
    var nTipo = document.getElementById("SelTipo_Edit");
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

function Deletar() {
    var nID = glb_nMaquinarioID;

    var sRequest = "ID=" + escape(nID);

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            alert(xhttp.responseText);
        }
    }

    var sURL = "http://" + location.host + "/Maquinarios/Deletar?" + sRequest;

    xhttp.open("POST", sURL, true);
    xhttp.send();
}

function idAtual(nMaquinarioID) {
    glb_nMaquinarioID = nMaquinarioID;
    var sRequest = "ID=" + escape(nMaquinarioID);
    var sNome = document.getElementById("Nome_Edit");
    var nTipo = document.getElementById("SelTipo_Edit");
    var sDescricao = document.getElementById("Descrição_Edit");

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var response = JSON.parse(xhttp.response);
            sNome.value = response[0]["nome"];
            nTipo.value = response[0]["tipoMaquinario"];
            sDescricao.value = response[0]["descricao"];
        }
    }

    var sURL = "http://" + location.host + "/Maquinarios/Pesquisar?Tipo=0&Nome=&" + sRequest;

    xhttp.open("GET", sURL, true);
    xhttp.send();
}
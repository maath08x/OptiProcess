var glb_nProdutoID;

function Adicionar() {
    var sDescricao = document.getElementById("Descrição_Add");
    var nLeadTime = document.getElementById("LeadTime_Add");
    var sNome = document.getElementById("Nome_Add");

    var sRequest = "Descricao=" + sDescricao.value + "&LeadTime=" + escape(nLeadTime.value) + "&Nome=" + escape(sNome.value);

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            alert(xhttp.responseText);
        }
    };

    var sURL = "http://" + location.host + "/Produtos/Adicionar?" + sRequest;

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

                sInner += "<td id=\"produtoID" + i + "\">" + response[i]["produtoID"] + "</td>";
                sInner += "<td id=\"nome" + i + "\">" + response[i]["nome"] + "</td>";
                sInner += "<td id=\"descricao" + i + "\">" + response[i]["descricao"] + "</td>";
                sInner += "<td id=\"estoque" + i + "\">" + (response[i]["estoque"] == null ? "0" : response[i]["estoque"]) + "</td>";
                sInner += "<td id=\"leadTime" + i + "\">" + (response[i]["leadTime"] == null ? "0" : response[i]["leadTime"]) + "</td>";

                sInner += "<td id=\"btn" + i + "\"><button type=\"button\" class=\"btn btn-primary d-md-inline-block form-inline mr-0 mr-md-2 ml-3\" data-toggle=\"modal\" data-target=\"#editModal\" onclick=\"idAtual(" + response[i]["produtoID"] + ")\"><div class=\"fas fa-fw fa-pen\"></div></button>";
                sInner += "<button type=\"button\" class=\"btn btn-primary d-md-inline-block form-inline mr-0 mr-md-2 ml-3\" data-toggle=\"modal\" data-target=\"#detailProdutosModal\" onclick=\"idAtualDetail(" + response[i]["produtoID"] + ")\"><div class=\"fas fa-fw fa-book-open\"></div></button></td>";

                sInner += "</tr>";
            }
            tBody.innerHTML = sInner;
        }
    };

    var sURL = "http://" + location.host + "/Produtos/Pesquisar?" + sRequest;

    xhttp.open("GET", sURL, true);
    xhttp.send();
}

function Alterar() {
    var nID = glb_nProdutoID;
    var sNome = document.getElementById("Nome_Edit");
    var nLeadTime = document.getElementById("LeadTime_Edit");
    var sDescricao = document.getElementById("Descrição_Edit");

    var sRequest = "ID=" + escape(nID) + "&Nome=" + escape(sNome.value) + "&Descricao=" + escape(sDescricao.value) + "&LeadTime=" + escape(nLeadTime.value);

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            alert(xhttp.responseText);
        }
    }

    var sURL = "http://" + location.host + "/Produtos/Alterar?" + sRequest;

    xhttp.open("POST", sURL, true);
    xhttp.send();
}

function Deletar() {
    var nID = glb_nProdutoID;

    var sRequest = "ID=" + escape(nID);

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            alert(xhttp.responseText);
        }
    }

    var sURL = "http://" + location.host + "/Produtos/Deletar?" + sRequest;

    xhttp.open("POST", sURL, true);
    xhttp.send();
}

function idAtual(nProdutosID) {
    glb_nProdutoID = nProdutosID;
    var sRequest = "ID=" + escape(nProdutosID);
    var sNome = document.getElementById("Nome_Edit");
    var nLeadTime = document.getElementById("LeadTime_Edit");
    var sDescricao = document.getElementById("Descrição_Edit");

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var response = JSON.parse(xhttp.response);
            sNome.value = response[0]["nome"];
            nLeadTime.value = response[0]["leadTime"];
            sDescricao.value = response[0]["descricao"];
        }
    }

    var sURL = "http://" + location.host + "/Produtos/Pesquisar?" + sRequest;

    xhttp.open("GET", sURL, true);
    xhttp.send();
}

function idAtualDetail(nProdutoID) {
    glb_nProdutoID = nProdutoID;
    var sRequest = "ID=" + escape(nProdutoID);

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {

            // Escreve na primeira tabela
            var tBodyPedidoDetail = document.getElementById("tBodyProdutoDetail");
            var response = JSON.parse(xhttp.response);
            var sInner = "";
            var dt;

            sInner += "<tr>";

            sInner += "<td id=\"produtoID" + 0 + "\">" + response[0]["Produtos1"]["produtoID"] + "</td>";
            sInner += "<td id=\"nome" + 0 + "\">" + response[0]["Produtos1"]["nome"] + "</td>";
            sInner += "<td id=\"descricao" + 0 + "\">" + response[0]["Produtos1"]["descricao"] + "</td>";
            sInner += "<td id=\"estoque" + 0 + "\">" + response[0]["Produtos1"]["qntEstoque"] + "</td>";
            sInner += "<td id=\"leadTime" + 0 + "\">" + response[0]["Produtos1"]["leadTime"] + "</td>";

            sInner += "</tr>";

            tBodyProdutoDetail.innerHTML = sInner;

            // Escreve na segunda tabela
            var tBodySubProdutoDetail = document.getElementById("tBodySubProdutoDetail");
            sInner = "";
            for (var i = 0; i < response.length; i++) {
                sInner += "<tr>";
                sInner += "<td id=\"produtoID" + i + "\">" + response[i]["Produtos"]["produtoID"] + "</td>";
                sInner += "<td id=\"nome" + i + "\">" + response[i]["Produtos"]["nome"] + "</td>";
                sInner += "<td id=\"descricao" + i + "\">" + response[i]["Produtos"]["descricao"] + "</td>";
                sInner += "<td id=\"quantidade" + i + "\">" + response[i]["quantidade"] + "</td>";
                sInner += "<td id=\"btn" + i + "\"><button type=\"button\" class=\"btn btn-primary d-md-inline-block form-inline mr-0 mr-md-2 ml-3\" data-toggle=\"modal\" data-target=\"#editSubProdutoModal\" onclick=\"subProdutoID(" + response[i]["produtosFilhosID"] + ")\"><div class=\"fas fa-fw fa-pen\"></div></button>";
                sInner += "<button type=\"button\" class=\"btn btn-danger d-md-inline-block form-inline mr-0 mr-md-2 ml-3\" onclick=\"DeletarSubProduto(" + response[i]["produtosFilhosID"] + ")\"><div class=\"fas fa-fw fa-times\"></div></button></td>";
                sInner += "</tr>";
            }
            tBodySubProdutoDetail.innerHTML = sInner;
        }
    }

    var sURL = "http://" + location.host + "/Produtos/PesquisarProdutosFilhos?" + sRequest;

    xhttp.open("GET", sURL, true);
    xhttp.send();
}
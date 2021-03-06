﻿var glb_nEditID;

var glb_Produtos;
var xhttpProdutos = new XMLHttpRequest();
xhttpProdutos.onreadystatechange = function () {
    if (this.readyState == 4 && this.status == 200) {
        glb_Produtos = JSON.parse(xhttpProdutos.response);

        // Preenche combobox
        selProduto = document.getElementById("SelProdutoID_Search");
        for (var i = 0; i < glb_Produtos.length; i++) {
            selProduto.innerHTML += "<option value=" + glb_Produtos[i]["produtoID"] + ">" + glb_Produtos[i]["nome"] + "</option>";
        }
    }
};
var sURL = "http://" + location.host + "/Produtos/Pesquisar?";
xhttpProdutos.open("GET", sURL, true);
xhttpProdutos.send();

var glb_Maquinarios;
var xhttpMaquinarios = new XMLHttpRequest();
xhttpMaquinarios.onreadystatechange = function () {
    if (this.readyState == 4 && this.status == 200) {
        glb_Maquinarios = JSON.parse(xhttpMaquinarios.response);
    }
};
var sURL = "http://" + location.host + "/Maquinarios/Pesquisar?";
xhttpMaquinarios.open("GET", sURL, true);
xhttpMaquinarios.send();

function Pesquisar() {
    var nOPID = document.getElementById("OPID_Search");
    var nProdutoID = document.getElementById("SelProdutoID_Search");
    var nPedidoID = document.getElementById("PedidoID_Search");

    var sRequest = "OPID=" + escape(nOPID.value) + "&ProdutoID=" + escape(nProdutoID.value) + "&PedidoID=" + escape(nPedidoID.value);

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var tBody = document.getElementById("tBody");
            var response = JSON.parse(xhttp.response);
            var sInner = "";
            var dt;
            for (var i = 0; i < response.length; i++) {
                sInner += "<tr>";

                sInner += "<td id=\"ordemProducaoID" + i + "\">" + response[i]["ordemProducaoID"] + "</td>";

                for (var x = 0; x < glb_Produtos.length; x++) {
                    if (glb_Produtos[x]["produtoID"] == response[i]["produtoID"]) {
                        sInner += "<td id=\"produtoID" + i + "\">" + glb_Produtos[x]["nome"] + "</td>";
                    }
                }
                
                sInner += "<td id=\"quantidade" + i + "\">" + response[i]["quantidade"] + "</td>";

                for (var x = 0; x < glb_Maquinarios.length; x++) {
                    if (glb_Maquinarios[x]["maquinarioID"] == response[i]["maquinarioID"]) {
                        sInner += "<td id=\"maquinarioID" + i + "\">" + glb_Maquinarios[x]["nome"] + "</td>";
                    }
                }
                
                sInner += "<td id=\"pedidoID" + i + "\">" + response[i]["pedidoID"] + "</td>";
                if (response[i]["dtOrdemProd"] != null) {
                    dt = new Date(parseInt(response[i]["dtOrdemProd"].replace('/Date(', '')));
                    sInner += "<td id=\"dtOrdemProd" + i + "\">" + dt.toLocaleDateString() + "</td>";
                }
                else {
                    sInner += "<td id=\"dtOrdemProd" + i + "\"></td>";
                }
                if (response[i]["dtPrevisao"] != null) {
                    dt = new Date(parseInt(response[i]["dtPrevisao"].replace('/Date(', '')));
                    sInner += "<td id=\"dtPrevisao" + i + "\">" + dt.toLocaleDateString() + "</td>";
                }
                else {
                    sInner += "<td id=\"dtPrevisao" + i + "\"></td>";
                }
                if (response[i]["dtConclusao"] != null) {
                    dt = new Date(parseInt(response[i]["dtConclusao"].replace('/Date(', '')));
                    sInner += "<td id=\"dtConclusao" + i + "\">" + dt.toLocaleDateString() + "</td>";
                    sInner += "<td></td>";
                }
                else {
                    sInner += "<td id=\"dtConclusao" + i + "\"></td>";

                    sInner += "<td id=\"btn" + i + "\"><button type=\"button\" class=\"btn btn-primary d-md-inline-block form-inline mr-0 mr-md-2 ml-3\" onclick=\"Concluir(" + response[i]["ordemProducaoID"] + ")\"><div class=\"fas fa-fw fa-check\"></div></button></td>";
                }


                sInner += "</tr>";
            }
            tBody.innerHTML = sInner;
        }
    };

    var sURL = "http://" + location.host + "/OrdemProducao/Pesquisar?" + sRequest;

    xhttp.open("GET", sURL, true);
    xhttp.send();
}

function Concluir(opID) {
    var sRequest = "OPID=" + escape(opID);

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200)
            alert(xhttp.responseText);
    }

    var sURL = "http://" + location.host + "/OrdemProducao/Concluir?" + sRequest;

    xhttp.open("POST", sURL, true);
    xhttp.send();
}
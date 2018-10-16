var glb_nPedidoID;
var glb_nPedidoProdutoID;

var glb_Pessoas;
var xhttpPessoas = new XMLHttpRequest();
xhttpPessoas.onreadystatechange = function () {
    if (this.readyState == 4 && this.status == 200) {
        glb_Pessoas = JSON.parse(xhttpPessoas.response);
    }
};
var sURL = "http://" + location.host + "/Pessoas/Pesquisar?";
xhttpPessoas.open("GET", sURL, true);
xhttpPessoas.send();

var glb_Tipos;
var xhttpTipos = new XMLHttpRequest();
xhttpTipos.onreadystatechange = function () {
    if (this.readyState == 4 && this.status == 200) {
        glb_Tipos = JSON.parse(xhttpTipos.response);
    }
};
var sURL = "http://" + location.host + "/TiposGerais/Pesquisar?TelaID=2";
xhttpTipos.open("GET", sURL, true);
xhttpTipos.send();

var glb_Produtos;
var xhttpProdutos = new XMLHttpRequest();
xhttpProdutos.onreadystatechange = function () {
    if (this.readyState == 4 && this.status == 200) {
        glb_Produtos = JSON.parse(xhttpProdutos.response);
    }
};
var sURL = "http://" + location.host + "/Produtos/Pesquisar?";
xhttpProdutos.open("GET", sURL, true);
xhttpProdutos.send();

function Pesquisar() {
    var nID = document.getElementById("ID_Search");
    var nPessoaID = document.getElementById("PessoaID_Search");
    var nTipo = document.getElementById("Tipo_Search");

    var sRequest = "ID=" + escape(nID.value) + "&PedidoID=" + escape(nPessoaID.value) + "&Tipo=" + escape(nTipo.value);

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var tBody = document.getElementById("tBody");
            var response = JSON.parse(xhttp.response);
            var sInner = "";
            var dt;
            for (var i = 0; i < response.length; i++) {
                sInner += "<tr>";

                sInner += "<td id=\"pedidoID" + i + "\">" + response[i]["pedidoID"] + "</td>";

                for (var x = 0; x < glb_Pessoas.length; x++) {
                    if (glb_Pessoas[x]["pessoaID"] == response[i]["pessoaID"]) {
                        sInner += "<td id=\"pessoaID" + i + "\">" + glb_Pessoas[x]["nome"] + "</td>";
                    }
                }

                for (var x = 0; x < glb_Tipos.length; x++) {
                    if (glb_Tipos[x]["tipoID"] == response[i]["tipoPedido"]) {
                        sInner += "<td id=\"tipoPedido" + i + "\">" + glb_Tipos[x]["nome"] + "</td>";
                    }
                }

                if (response[i]["dtPedido"] != null) {
                    dt = new Date((response[i]["dtPedido"]));
                    sInner += "<td id=\"dtPedido" + i + "\">" + dt.toLocaleDateString() + "</td>";
                }
                else {
                    sInner += "<td id=\"dtPedido" + i + "\"></td>";
                }
                if (response[i]["dtPrevisao"] != null) {
                    dt = new Date((response[i]["dtPrevisao"]));
                    sInner += "<td id=\"dtPrevisao" + i + "\">" + dt.toLocaleDateString() + "</td>";
                }
                else {
                    sInner += "<td id=\"dtPrevisao" + i + "\"></td>";
                }
                sInner += "<td id=\"finalizado" + i + "\">" + (response[i]["finalizado"] == true ? "Sim" : "Não") + "</td>";

                sInner += "<td id=\"btn" + i + "\"><button type=\"button\" class=\"btn btn-primary from-control form-inline ml-1\" data-toggle=\"modal\" data-target=\"#editModal\" onclick=\"idAtual(" + response[i]["pedidoID"] + ")\"><div class=\"fas fa-fw fa-pen\"></div></button>" +
                    "<button type=\"button\" class=\"btn btn-primary from-control form-inline ml-1\" data-toggle=\"modal\" data-target=\"#detailPedidosModal\" onclick=\"idAtualDetail(" + response[i]["pedidoID"] + ")\"><div class=\"fas fa-fw fa-book-open\"></div></button>";

                if (!response[i]["finalizado"]) {
                    sInner += "<button type=\"button\" class=\"btn btn-primary from-control form-inline ml-1\" onclick=\"finalizarPedido(" + response[i]["pedidoID"] + ")\"><div class=\"fas fa-fw fa-check\"></div></button> ";
                    sInner += "<button type=\"button\" class=\"btn btn-primary from-control form-inline\" onclick=\"EmitirOP(" + response[i]["pedidoID"] + ")\"><div class=\"fas fa-fw fa-boxes\"></div></button></td > ";
                }
                else
                    sInner += "</td>";

                sInner += "</tr>";
            }
            tBody.innerHTML = sInner;
        }
    };

    var sURL = "http://" + location.host + "/Pedidos/PesquisarPedidos?" + sRequest;

    xhttp.open("GET", sURL, true);
    xhttp.send();
}

function Alterar() {
    var nID = glb_nPedidoID;
    var nPessoaID = document.getElementById("PessoaID_Edit");

    var sRequest = "ID=" + escape(nID) + "&PessoaID=" + escape(nPessoaID.value);

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            alert(xhttp.responseText);
        }
    }

    var sURL = "http://" + location.host + "/Pedidos/Alterar?" + sRequest;

    xhttp.open("POST", sURL, true);
    xhttp.send();
}


function idAtual(nPedidoID) {
    glb_nPedidoID = nPedidoID;
    var sRequest = "ID=" + escape(nPedidoID);
    var nPessoaID = document.getElementById("PessoaID_Edit");

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var response = JSON.parse(xhttp.response);
            nPessoaID.value = response[0]["pessoaID"];
        }
    }

    var sURL = "http://" + location.host + "/Pedidos/PesquisarPedidos?" + sRequest;

    xhttp.open("GET", sURL, true);
    xhttp.send();
}

function idAtualDetail(nPedidoID) {
    glb_nPedidoID = nPedidoID;
    var sRequest = "PedidoID=" + escape(nPedidoID);

    var xhttp = new XMLHttpRequest();
    
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {

            // Escreve na primeira tabela
            var tBodyPedidoDetail = document.getElementById("tBodyPedidoDetail");
            var response = JSON.parse(xhttp.response);
            var sInner = "";
            var dt;

            sInner += "<tr>";

            sInner += "<td id=\"pedidoID" + 0 + "\">" + response[0]["Pedidos"]["pedidoID"] + "</td>";

            for (var x = 0; x < glb_Pessoas.length; x++) {
                if (glb_Pessoas[x]["pessoaID"] == response[0]["Pedidos"]["pessoaID"]) {
                    sInner += "<td id=\"pessoaID" + 0 + "\">" + glb_Pessoas[x]["nome"] + "</td>";
                }
            }

            for (var x = 0; x < glb_Tipos.length; x++) {
                if (glb_Tipos[x]["tipoID"] == response[0]["Pedidos"]["tipoPedido"]) {
                    sInner += "<td id=\"tipoPedido" + 0 + "\">" + glb_Tipos[x]["nome"] + "</td>";
                }
            }

            if (response[0]["Pedidos"]["dtPedido"] != null) {
                dt = new Date((response[0]["Pedidos"]["dtPedido"]));
                sInner += "<td id=\"dtPedido" + 0 + "\">" + dt.toLocaleDateString() + "</td>";
            }
            else {
                sInner += "<td id=\"dtPedido" + 0 + "\"></td>";
            }
            if (response[0]["Pedidos"]["dtPrevisao"] != null) {
                dt = new Date((response[0]["Pedidos"]["dtPrevisao"]));
                sInner += "<td id=\"dtPrevisao" + 0 + "\">" + dt.toLocaleDateString() + "</td>";
            }
            else {
                sInner += "<td id=\"dtPrevisao" + 0 + "\"></td>";
            }
            sInner += "<td id=\"finalizado" + 0 + "\">" + (response[0]["Pedidos"]["finalizado"] == true ? "Sim" : "Não") + "</td>";

            sInner += "</tr>";

            tBodyPedidoDetail.innerHTML = sInner;

            // Escreve na segunda tabela
            var tBodyProdutoDetail = document.getElementById("tBodyProdutoDetail");
            sInner = "";
            for (var i = 0; i < response.length; i++) {
                sInner += "<tr>";

                for (var x = 0; x < glb_Tipos.length; x++) {
                    if (glb_Produtos[x]["produtoID"] == response[i]["produtoID"]) {
                        sInner += "<td id=\"produtoID" + i + "\">" + glb_Produtos[x]["nome"] + "</td>";
                    }
                }
                

                sInner += "<td id=\"qntPedido" + i + "\">" + response[i]["qntPedido"] + "</td>";
                sInner += "<td id=\"btn" + i + "\"><button type=\"button\" class=\"btn btn-primary d-md-inline-block form-inline mr-0 mr-md-2 ml-3\" data-toggle=\"modal\" data-target=\"#editProdutoModal\" onclick=\"pedProdutoID(" + response[i]["pedProdutosID"] + ")\"><div class=\"fas fa-fw fa-pen\"></div></button>";
                sInner += "<button type=\"button\" class=\"btn btn-danger d-md-inline-block form-inline mr-0 mr-md-2 ml-3\" onclick=\"DeletarProduto(" + response[i]["pedProdutosID"] + ")\"><div class=\"fas fa-fw fa-times\"></div></button></td>";
                sInner += "</tr>";
            }
            tBodyProdutoDetail.innerHTML = sInner;
        }
    }

    var sURL = "http://" + location.host + "/Pedidos/PesquisarPedidosProdutos?" + sRequest;

    xhttp.open("GET", sURL, true);
    xhttp.send();
}

function pedProdutoID(nPedidoProdutoID) {
    glb_nPedidoProdutoID = nPedidoProdutoID;
    var sRequest = "PedidoProdutoID=" + escape(nPedidoProdutoID);
    
    var nQntPedido = document.getElementById("Edit_Quantidade");
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var response = JSON.parse(xhttp.response);
            nQntPedido.value = response[0]["qntPedido"];
        }
    }

    var sURL = "http://" + location.host + "/Pedidos/PesquisarPedidosProdutos?" + sRequest;

    xhttp.open("GET", sURL, true);
    xhttp.send();
}

function AlterarProduto() {
    var nQntPedido = document.getElementById("Edit_Quantidade");

    var sRequest = "PedidoProdutoID=" + escape(glb_nPedidoProdutoID);
    sRequest += "&QntPedido=" + escape(nQntPedido.value);

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            alert(xhttp.responseText);
        }
    }
    
    var sURL = "http://" + location.host + "/Pedidos/AlterarPedidosProdutos?" + sRequest;

    xhttp.open("POST", sURL, true);
    xhttp.send();
}

function AdicionarProduto() {
    var nProdutoID = document.getElementById("Add_ProdutoID");
    var nQntPedido = document.getElementById("Add_Quantidade");

    if (nQntPedido.value <= 0) {
        alert("Por favor, utilize um numero inteiro diferente de zero.")
        return;
    }


    if ((nQntPedido.value).indexOf(',') == -1 && (nQntPedido.value).indexOf('.') == -1) {

        var sRequest = "PedidoID=" + escape(glb_nPedidoID) + "&ProdutoID=" + escape(nProdutoID.value) + "&QntPedido=" + escape(nQntPedido.value);

        var xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                alert(xhttp.responseText);
            }
        }

        var sURL = "http://" + location.host + "/Pedidos/AdicionarPedidosProdutos?" + sRequest;

        xhttp.open("POST", sURL, true);
        xhttp.send();
    }
    else
    {
        alert("Por favor, utilize um numero inteiro diferente de zero.");
        return;
    }
}

function DeletarProduto(nPedProdutoID) {
    var sRequest = "PedProdutoID=" + escape(nPedProdutoID);

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            alert(xhttp.responseText);
        }
    }

    var sURL = "http://" + location.host + "/Pedidos/DeletarPedidosProdutos?" + sRequest;

    xhttp.open("POST", sURL, true);
    xhttp.send();
}

function DeletarProduto(nPedProdutoID) {
    var sRequest = "PedProdutoID=" + escape(nPedProdutoID);

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            alert(xhttp.responseText);
        }
    }

    var sURL = "http://" + location.host + "/Pedidos/DeletarPedidosProdutos?" + sRequest;

    xhttp.open("POST", sURL, true);
    xhttp.send();
}

function Deletar() {
    var sRequest = "PedidoID=" + escape(glb_nPedidoID);

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            alert(xhttp.responseText);
        }
    }

    var sURL = "http://" + location.host + "/Pedidos/DeletarPedido?" + sRequest;

    xhttp.open("POST", sURL, true);
    xhttp.send();
}

function Adicionar() {
    var nPessoaID = document.getElementById("PessoaID_Add");
    var nTipo = document.getElementById("Tipo_Add");

    var sRequest = "PessoaID=" + escape(nPessoaID.value) + "&Tipo=" + nTipo.value;

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            alert(xhttp.responseText);
        }
    }

    var sURL = "http://" + location.host + "/Pedidos/AdicionarPedido?" + sRequest;

    xhttp.open("POST", sURL, true);
    xhttp.send();
}

function finalizarPedido(nPedidoID) {
    var sRequest = "PedidoID=" + nPedidoID;

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            alert(xhttp.responseText);
        }
    }

    var sURL = "http://" + location.host + "/Pedidos/FinalizarPedido?" + sRequest;

    xhttp.open("POST", sURL, true);
    xhttp.send();
}

function EmitirOP(nPedidoID) {
    var sRequest = "PedidoID=" + nPedidoID;

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            alert(xhttp.responseText);
        }
    }

    var sURL = "http://" + location.host + "/Pedidos/EmitirOP?" + sRequest;

    xhttp.open("POST", sURL, true);
    xhttp.send();
}


function EstoqueSeguro() {

    var sRequest = "MediaProduto=" + mediaProduto.value + "&MediaFornecedor=" + mediaFornecedor.value;

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var response = JSON.parse(xhttp.response);
            estoqueSeguro.value = response;
        }
    }

    var sURL = "http://" + location.host + "/Pedidos/EstoqueSeguro?" + sRequest;

    xhttp.open("POST", sURL, true);
    xhttp.send();


}

